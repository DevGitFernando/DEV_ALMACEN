
--	Select * From PedidosEnvioDet_Lotes_Registrar (Nolock)

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_PedidosEnvioDet_Lotes_Registrar' and xType = 'P')
    Drop Proc spp_Mtto_PedidosEnvioDet_Lotes_Registrar
Go--#SQL
  
Create Proc spp_Mtto_PedidosEnvioDet_Lotes_Registrar
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), @FolioPedido varchar(32), 
	@IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32), @Renglon int, @Existencia numeric(14, 4), 
    @FechaCaducidad varchar(10), @iOpcion smallint
) 
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,   
	    @EsConsignacion bit 

	
	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From PedidosEnvioDet_Lotes_Registrar (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								And FolioPedido = @FolioPedido 
								And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
								And ClaveLote = @ClaveLote And Renglon = @Renglon ) 
			  Begin 
				Insert Into PedidosEnvioDet_Lotes_Registrar ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, 
					ClaveLote, Renglon, Existencia, EsConsignacion, FechaCaducidad, FechaRegistro, Status, Actualizado )
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioPedido, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @Renglon, @Existencia, @EsConsignacion, @FechaCaducidad, GetDate(), @sStatus, @iActualizado 

              End 
 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C'
			Update PedidosEnvioDet_Lotes_Registrar Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
				And FolioPedido = @FolioPedido And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
				And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedido as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
