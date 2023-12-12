
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PedidosDistEnc' and xType = 'P' )
   Drop Proc spp_Mtto_PedidosDistEnc 
Go--#SQL

Create Proc spp_Mtto_PedidosDistEnc 
( 
  @IdEmpresa varchar(3), 
  @IdEstado varchar(2), @CveRenapo varchar(2), @IdFarmacia varchar(4), @IdAlmacen varchar(2) = '00', @EsSalidaAlmacen smallint = 0, 
  @FolioDistribucion varchar(18) = '*', 
  @FolioMovtoInv varchar(30) = '', @FolioMovtoInvRef varchar(30) = '', @FolioPedidoRef varchar(30) = '', 
  @TipoSalida varchar(3) = 'SPD', 
  @DestinoEsAlmacen smallint = 0, @IdPersonal varchar(4) = '0001', @Observaciones varchar(500) = '', 
  @SubTotal Numeric(14,4) = 0, @Iva Numeric(14,4) = 0, @Total Numeric(14,4) = 0, 
  @IdEstadoRecibe varchar(2) = '25', @CveRenapoRecibe varchar(2) = 'SL', @IdFarmaciaRecibe varchar(4) = '0002', @IdAlmacenRecibe varchar(2) = '00'  
)  
With Encryption 
As 
Begin 
Set NoCount On --- 250001SLTX00000001
--	 @TipoSalida == TE '' TS
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  
		

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If @FolioDistribucion = '*' 
	   Begin 
		  -- Tomar solo los primeros 8 caracteres de la derecha para formar el consecutivo 
          Select @FolioDistribucion =  max(cast(right(FolioDistribucion, 8) as int)) + 1  
          From PedidosDistEnc (NoLock) 
          Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and TipoSalida = @TipoSalida 
       End 

	-- Formatear el folio 
	Set @FolioDistribucion = IsNull(@FolioDistribucion, '1') 
	Set @FolioDistribucion = right( replicate('0', 8) + @FolioDistribucion, 8 )

	-- Validar que no exista el Folio de transferencia 
	If Not Exists ( Select IdEstado From PedidosDistEnc (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioDistribucion = @FolioDistribucion )
	   Begin 
	      Insert Into PedidosDistEnc ( IdEmpresa, 
				 IdEstado, IdFarmacia, IdAlmacen, EsSalidaAlmacen, FolioDistribucion, 
				 FolioMovtoInv, FolioMovtoInvRef, FolioPedidoRef, TipoSalida, DestinoEsAlmacen, FechaSalida, 
				 FechaRegistro, IdPersonal, Observaciones, SubTotal, Iva, Total, IdEstadoRecibe, IdFarmaciaRecibe, IdAlmacenRecibe, Status, Actualizado ) 
	      Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdAlmacen, @EsSalidaAlmacen, @FolioDistribucion, 
				 @FolioMovtoInv, @FolioMovtoInvRef, @FolioPedidoRef, @TipoSalida, @DestinoEsAlmacen, getdate(),  
	             getdate(), @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @IdEstadoRecibe, @IdFarmaciaRecibe, @IdAlmacenRecibe, @sStatus, @iActualizado 
	      Set @sMensaje = 'Información guardada satisfactoriamente, con el folio ' + right(@FolioDistribucion, 10) 
	   End  


	Select @FolioDistribucion as FolioDistribucion, @sMensaje as Mensaje  

End 
Go--#SQL

