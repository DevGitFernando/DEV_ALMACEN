If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasEnc' and xType = 'P' )
   Drop Proc spp_Mtto_TransferenciasEnc 
Go--#SQL

Create Proc spp_Mtto_TransferenciasEnc 
( 
  @IdEmpresa varchar(3), 
  @IdEstado varchar(2), @CveRenapo varchar(2), @IdFarmacia varchar(4), @IdAlmacen varchar(2) = '00', @EsTransferenciaAlmacen smallint = 0, 
  @FolioTransferencia varchar(18) = '*', 
  @FolioMovtoInv varchar(30) = '', @FolioMovtoInvRef varchar(30) = '', @FolioTransferenciaRef varchar(30) = '', 
  @TipoTransferencia varchar(2) = 'TS', 
  @DestinoEsAlmacen smallint = 0, @IdPersonal varchar(4) = '0001', @Observaciones varchar(500) = '', 
  @SubTotal Numeric(14,4) = 0, @Iva Numeric(14,4) = 0, @Total Numeric(14,4) = 0, 
  @IdEstadoRecibe varchar(2) = '25', @CveRenapoRecibe varchar(2) = 'SL', @IdFarmaciaRecibe varchar(4) = '0002', @IdAlmacenRecibe varchar(2) = '00',
  @TransferenciaAplicada tinyint = 0, @IdPersonalRegistra varchar(4) = '0001'  
)  
With Encryption 
As 
Begin 
Set NoCount On --- 250001SLTX00000001
--	 @TipoTransferencia == TE '' TS
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  
		

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If @FolioTransferencia = '*' 
	   Begin 
		  -- Tomar solo los primeros 8 caracteres de la derecha para formar el consecutivo 
          Select @FolioTransferencia =  max(cast(right(FolioTransferencia, 8) as int)) + 1  
          From TransferenciasEnc (NoLock) 
          Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and TipoTransferencia = @TipoTransferencia 
       End 

	   
	-- Formatear el folio 
	Set @FolioTransferencia = IsNull(@FolioTransferencia, '1') 
	-- Set @FolioTransferencia = @IdEmpresa + @IdEstado + @IdFarmacia + @CveRenapo + @TipoTransferencia + right( replicate('0', 8) + @FolioTransferencia, 8 )
	Set @FolioTransferencia = @TipoTransferencia + right( replicate('0', 8) + @FolioTransferencia, 8 )


	-- Validar que no exista el Folio de transferencia 
	If Not Exists ( Select IdEstado From TransferenciasEnc (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTransferencia = @FolioTransferencia )
	   Begin 
	      Insert Into TransferenciasEnc ( IdEmpresa, 
				 IdEstado, IdFarmacia, IdAlmacen, EsTransferenciaAlmacen, FolioTransferencia, 
				 FolioMovtoInv, FolioMovtoInvRef, FolioTransferenciaRef, TipoTransferencia, DestinoEsAlmacen, FechaTransferencia, 
				 FechaRegistro, IdPersonal, Observaciones, SubTotal, Iva, Total, IdEstadoRecibe, IdFarmaciaRecibe, IdAlmacenRecibe, 
				 TransferenciaAplicada, IdPersonalRegistra, FechaAplicada, Status, Actualizado ) 
	      Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdAlmacen, @EsTransferenciaAlmacen, @FolioTransferencia, 
				 @FolioMovtoInv, @FolioMovtoInvRef, @FolioTransferenciaRef, @TipoTransferencia, @DestinoEsAlmacen, getdate(),  
	             getdate(), @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @IdEstadoRecibe, @IdFarmaciaRecibe, @IdAlmacenRecibe, 
				 @TransferenciaAplicada, @IdPersonalRegistra, getdate(), @sStatus, @iActualizado 
	      Set @sMensaje = 'Información guardada satisfactoriamente, con el folio ' + right(@FolioTransferencia, 10) 
	   End  
	Else 
		Begin 
			Update TransferenciasEnc Set TransferenciaAplicada = @TransferenciaAplicada, IdPersonalRegistra = @IdPersonalRegistra, FechaAplicada = getdate()
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTransferencia = @FolioTransferencia

			Set @sMensaje = 'Información Actualizada satisfactoriamente, con el folio ' + right(@FolioTransferencia, 10)
		End

	Select @FolioTransferencia as FolioTransferencia, @sMensaje as Mensaje  

End 
Go--#SQL

