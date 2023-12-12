
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_Pedidos_Productos' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_OCEN_Pedidos_Productos
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_Pedidos_Productos
( 
@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdTipoPedido varchar(2), 
@FolioPedido varchar(6), @IdClaveSSA varchar(4),  @CodigoEAN varchar(30), @Cantidad_Pedido int, 
@Cantidad_Surtida int, @CantidadEnviarCentral int  )
As
Begin
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint 

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 3 

 
	-- Se Inserta en la tabla COM_FAR_Pedidos_Productos
	If Not Exists ( Select FolioPedido From COM_OCEN_PedidosDet (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido 
		And FolioPedido = @FolioPedido And IdClaveSSA = @IdClaveSSA And CodigoEAN = @CodigoEAN)
	 Begin
		Insert Into COM_OCEN_PedidosDet( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, CodigoEAN, Cantidad_Pedido, Cantidad_Surtida, Cantidad_EnviadaCentral, Status, Actualizado )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @IdClaveSSA, @CodigoEAN, @Cantidad_Pedido, @Cantidad_Surtida, @CantidadEnviarCentral, @sStatus, @iActualizado

		-- Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' se guardo satisfactoriamente'
	 End	
 

	
----	Update P Set Cantidad_Surtir = @Cantidad_Surtida, Actualizado = 0 
----	From COM_FAR_Pedidos_Productos P  
----	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido 
----		And FolioPedido = @FolioPedido And IdClaveSSA = @IdClaveSSA	and CodigoEAN = @CodigoEAN 
	

	-- Regresar la Clave Generada 
    -- Select @FolioPedido as Clave, @sMensaje as Mensaje
End
Go--#SQL



If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_REG_Pedidos_Productos' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_OCEN_REG_Pedidos_Productos
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_REG_Pedidos_Productos
( 
@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdTipoPedido varchar(2), 
@FolioPedido varchar(6), @IdClaveSSA varchar(4),  @CodigoEAN varchar(30), @Cantidad_Pedido int, 
@Cantidad_Surtida int   )
As
Begin
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint 

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 3 

 
	-- Se Inserta en la tabla COM_FAR_Pedidos_Productos
	If Not Exists ( Select FolioPedido From COM_OCEN_REG_PedidosDet (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido 
		And FolioPedido = @FolioPedido And IdClaveSSA = @IdClaveSSA )
	 Begin
		Insert Into COM_OCEN_REG_PedidosDet( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, CodigoEAN, Cantidad_Pedido, Cantidad_Surtida, Status, Actualizado )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @IdClaveSSA, @CodigoEAN, @Cantidad_Pedido, @Cantidad_Surtida, @sStatus, @iActualizado

		-- Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' se guardo satisfactoriamente'
	 End	
 

	
----	Update P Set Cantidad_Surtir = @Cantidad_Surtida, Actualizado = 0 
----	From COM_FAR_Pedidos_Productos P  
----	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido 
----		And FolioPedido = @FolioPedido And IdClaveSSA = @IdClaveSSA	and CodigoEAN = @CodigoEAN 
	

	-- Regresar la Clave Generada 
    -- Select @FolioPedido as Clave, @sMensaje as Mensaje
End
Go--#SQL

