If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_OrdenesDeComprasDet_Lotes' and xType = 'P')
    Drop Proc spp_Mtto_OrdenesDeComprasDet_Lotes
Go--#SQL 
  
Create Proc spp_Mtto_OrdenesDeComprasDet_Lotes 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), 
	@FolioOrdenCompra varchar(32), @IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32), 
	@Renglon int, @CantidadRecibida numeric(14, 4), @iOpcion smallint
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

		   If Not Exists ( Select * From OrdenesDeComprasDet_Lotes (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								 And FolioOrdenCompra = @FolioOrdenCompra 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
								 And ClaveLote = @ClaveLote And Renglon = @Renglon ) 
			  Begin 
				Insert Into OrdenesDeComprasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, 
					ClaveLote, Renglon, Cant_Recibida, Cant_Devuelta, CantidadRecibida, Status, Actualizado )

				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioOrdenCompra, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @Renglon, @CantidadRecibida, 0, @CantidadRecibida, @sStatus, @iActualizado 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioOrdenCompra 
	   End 
    Else 
       Begin 

			Set @sStatus = 'C'

			Update OrdenesDeComprasDet_Lotes Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
				  And FolioOrdenCompra = @FolioOrdenCompra 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
				  And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioOrdenCompra + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioOrdenCompra as Clave, @sMensaje as Mensaje 
    
End
Go--#SQL

