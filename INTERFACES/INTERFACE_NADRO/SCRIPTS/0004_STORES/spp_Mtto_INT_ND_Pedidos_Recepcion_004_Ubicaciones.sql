-- Select * From INT_ND_PedidosDet_Lotes_Ubicaciones (Nolock) 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Pedidos_Recepcion_004_Ubicaciones' and xType = 'P')
    Drop Proc spp_Mtto_INT_ND_Pedidos_Recepcion_004_Ubicaciones
Go--#SQL 
  
Create Proc spp_Mtto_INT_ND_Pedidos_Recepcion_004_Ubicaciones 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), 
	@FolioPedido varchar(32), @IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32), 
	@IdPasillo int, @IdEstante int, @IdEntrepaño int, @CantidadRecibida numeric(14, 4), @iOpcion smallint
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
		   If Not Exists ( Select * From INT_ND_PedidosDet_Lotes_Ubicaciones (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								 And FolioPedido = @FolioPedido And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
								 And ClaveLote = @ClaveLote And IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño  )  
			  Begin 
				Insert Into INT_ND_PedidosDet_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, 
					ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cant_Recibida, Cant_Devuelta, CantidadRecibida, Status, Actualizado )

				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioPedido, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @IdPasillo, @IdEstante, @IdEntrepaño, @CantidadRecibida, 0, @CantidadRecibida, @sStatus, @iActualizado 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   End 
    Else 
       Begin 

			Set @sStatus = 'C'

			Update INT_ND_PedidosDet_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
				  And FolioPedido = @FolioPedido 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
				  And IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 

		   Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedido as Clave, @sMensaje as Mensaje 
    
End
Go--#SQL 

