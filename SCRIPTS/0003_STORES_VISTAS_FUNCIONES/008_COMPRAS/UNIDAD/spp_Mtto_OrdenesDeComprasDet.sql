If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_OrdenesDeComprasDet' and xType = 'P')
    Drop Proc spp_Mtto_OrdenesDeComprasDet
Go--#SQL 
  
Create Proc spp_Mtto_OrdenesDeComprasDet 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioOrdenCompra varchar(32), @IdProducto varchar(10), 
    @CodigoEAN varchar(32), @Renglon int, @CantidadPrometida numeric(14, 4), @Cant_Recibida numeric(14, 4), 
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

		   If Not Exists ( Select * From OrdenesDeComprasDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrdenCompra = @FolioOrdenCompra 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into OrdenesDeComprasDet 
				(  
					IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, Renglon, CantidadPrometida, 
					Cant_Recibida, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrdenCompra, @IdProducto, @CodigoEAN, @Renglon, @CantidadPrometida, 
					@Cant_Recibida, @Cant_Recibida, @CostoUnitario, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioOrdenCompra 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update OrdenesDeComprasDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrdenCompra = @FolioOrdenCompra 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioOrdenCompra + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioOrdenCompra as Clave, @sMensaje as Mensaje 
    
End
Go--#SQL




