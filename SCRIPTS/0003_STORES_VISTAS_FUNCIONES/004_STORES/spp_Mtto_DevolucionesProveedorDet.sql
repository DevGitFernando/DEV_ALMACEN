
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_DevolucionesProveedorDet' and xType = 'P')
    Drop Proc spp_Mtto_DevolucionesProveedorDet
Go--#SQL
  
Create Proc spp_Mtto_DevolucionesProveedorDet 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioDevProv varchar(32), @IdProducto varchar(10), 
    @CodigoEAN varchar(32), @Renglon int, @UnidadDeEntrada smallint, @Cant_Recibida numeric(14, 4),
	@CostoUnitario numeric(14, 4), @TasaIva numeric(14, 4), @SubTotal numeric(14, 4), @ImpteIva numeric(14, 4), 
    @Importe numeric(14, 4), @iOpcion smallint 
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From DevolucionesProveedorDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevProv = @FolioDevProv 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into DevolucionesProveedorDet 
				(  
					IdEmpresa, IdEstado, IdFarmacia, FolioDevProv, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
					Cant_Recibida, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioDevProv, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
					@Cant_Recibida, @Cant_Recibida, @CostoUnitario, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update DevolucionesProveedorDet Set 
					IdEmpresa = @IdEmpresa , IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioDevProv = @FolioDevProv, 
					IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, Renglon = @Renglon, 
					UnidadDeEntrada = @UnidadDeEntrada, Cant_Recibida = @Cant_Recibida, 
					CantidadRecibida = @Cant_Recibida, CostoUnitario = @CostoUnitario, 
					TasaIva = @TasaIva, SubTotal = @SubTotal, ImpteIva = @ImpteIva, 
					Importe = @Importe, Status = @sStatus, Actualizado = @iActualizado
			   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevProv = @FolioDevProv
					 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioDevProv 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update DevolucionesProveedorDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevProv = @FolioDevProv 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioDevProv + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioDevProv as Folio, @sMensaje as Mensaje 
End
Go--#SQL	
