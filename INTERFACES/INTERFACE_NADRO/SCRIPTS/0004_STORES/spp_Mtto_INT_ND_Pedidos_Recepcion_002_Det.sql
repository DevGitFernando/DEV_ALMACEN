If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Pedidos_Recepcion_002_Det' and xType = 'P')
    Drop Proc spp_Mtto_INT_ND_Pedidos_Recepcion_002_Det
Go--#SQL 
  
Create Proc spp_Mtto_INT_ND_Pedidos_Recepcion_002_Det 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioPedido varchar(32), @IdProducto varchar(10), 
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

		   If Not Exists ( Select * From INT_ND_PedidosDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into INT_ND_PedidosDet 
				(  
					IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon, CantidadPrometida, 
					Cant_Recibida, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @IdProducto, @CodigoEAN, @Renglon, @CantidadPrometida, 
					@Cant_Recibida, @Cant_Recibida, @CostoUnitario, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update INT_ND_PedidosDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedido as Clave, @sMensaje as Mensaje 
    
End
Go--#SQL




