If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TraspasosEnc' and xType = 'P' )
   Drop Proc spp_Mtto_TraspasosEnc 
Go--#SQL 

Create Proc spp_Mtto_TraspasosEnc 
( 
  @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
  @FolioTraspaso varchar(18) = '*', @IdSubFarmaciaOrigen varchar(2), @IdSubFarmaciaDestino varchar(2),
  @FolioMovtoInv varchar(30) = '', @FolioMovtoInvRef varchar(30) = '', @FolioTraspasoRef varchar(30) = '', 
  @TipoTraspaso varchar(3) = 'TIS', @IdPersonal varchar(4) = '0001', @Observaciones varchar(500) = '', 
  @SubTotal Numeric(14,4) = 0, @Iva Numeric(14,4) = 0, @Total Numeric(14,4) = 0 
)  
With Encryption 
As 
Begin 
Set NoCount On --- 250001SLTX00000001
--	 @TipoTraspaso == TIE '' TIS
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  
		

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If @FolioTraspaso = '*' 
	   Begin 
		  -- Tomar solo los primeros 8 caracteres de la derecha para formar el consecutivo 
          Select @FolioTraspaso =  max(cast(right(FolioTraspaso, 8) as int)) + 1  
          From TraspasosEnc (NoLock) 
          Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and TipoTraspaso = @TipoTraspaso 
       End 

	   
	-- Formatear el folio 
	Set @FolioTraspaso = IsNull(@FolioTraspaso, '1')	
	Set @FolioTraspaso = @TipoTraspaso + right( replicate('0', 8) + @FolioTraspaso, 8 )


	-- Validar que no exista el Folio de Traspaso 
	If Not Exists ( Select IdEstado From TraspasosEnc (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTraspaso = @FolioTraspaso )
	   Begin 
	      Insert Into TraspasosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso, IdSubFarmaciaOrigen, IdSubFarmaciaDestino, 
				 FolioMovtoInv, FolioMovtoInvRef, FolioTraspasoRef, TipoTraspaso, FechaTraspaso, 
				 FechaRegistro, IdPersonal, Observaciones, SubTotal, Iva, Total, Status, Actualizado ) 
	      Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTraspaso, @IdSubFarmaciaOrigen, @IdSubFarmaciaDestino,
				 @FolioMovtoInv, @FolioMovtoInvRef, @FolioTraspasoRef, @TipoTraspaso, getdate(),  
	             getdate(), @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @sStatus, @iActualizado
 
	      Set @sMensaje = 'Información guardada satisfactoriamente, con el folio ' + @FolioTraspaso 
	   End  


	Select @FolioTraspaso as FolioTraspaso, @sMensaje as Mensaje  

End 
Go--#SQL 

