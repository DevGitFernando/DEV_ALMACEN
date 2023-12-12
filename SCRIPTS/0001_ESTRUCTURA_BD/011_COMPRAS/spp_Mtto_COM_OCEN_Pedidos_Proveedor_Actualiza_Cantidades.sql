
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_Pedidos_Proveedor_Actualiza_Cantidades' And xType = 'P' )
	Drop Proc spp_Mtto_COM_OCEN_Pedidos_Proveedor_Actualiza_Cantidades
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_Pedidos_Proveedor_Actualiza_Cantidades 
( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4),
  @IdClaveSSA varchar(4), @CodigoEAN varchar(30), @IdProveedor varchar(8), @iCantidadSolicitada int, 
  @GUID varchar(50), @FolioPedido varchar(6), @EsCentral tinyint, @Unidad varchar(4) )
As
Begin
	Declare
		@sSql varchar(8000),
		@iContenidoPaquete int,
		@CantidadSurtida int,
		@IdTipoPedido varchar(2)

	-- Se Inicializan variables.
	Set @sSql = ''
	Set @iContenidoPaquete = 0
	Set @IdClaveSSA = ''
	Set @CantidadSurtida = 0
	Set @IdTipoPedido = '04'


	-- Se obtiene el Contenido del paquete del codigo EAN
	Select @iContenidoPaquete = ContenidoPaquete, @IdClaveSSA = IdClaveSSA_Sal
	From vw_Productos_CodigoEAN(NoLock)
	Where CodigoEAN = @CodigoEAN

	If @EsCentral = 1
		Begin 	
			Set @CantidadSurtida =  ( @iCantidadSolicitada + (Select Cantidad_Surtida From COM_OCEN_REG_PedidosDet_Claves (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @Unidad And IdTipoPedido = @IdTipoPedido 
								And FolioPedido =  @FolioPedido And IdClaveSSA = @IdClaveSSA ) )
		End
	Else
		Begin 
			Set @CantidadSurtida =  ((Select CantidadAComprar From COM_OCEN_PedidosDet_Claves (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @Unidad And IdTipoPedido = @IdTipoPedido 
								And FolioPedido =  @FolioPedido And IdClaveSSA = @IdClaveSSA ) - @iCantidadSolicitada )
		End

	
	-- Se elimina el proveedor de la tabla temporal.
	Set @sSql = 
	'Update  Com_Pedidos_Compras Set cant_a_pedir = 0 ' +
	'Where  GUID = ' + Char(39) + @GUID + Char(39) + ' And FolioPedido = ' + Char(39) + @FolioPedido + Char(39) + ' And IdClaveSSA = ' + Char(39) +
		@IdClaveSSA + Char(39) + ' And CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' ' + 
	'And IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' '
	Exec (@sSql)

----	-- Se actualizan la cantidad requerida y pre-solicitada de la tabla COM_Concentrado_Pedidos_Claves_OCEN																		
----	Update COM_Concentrado_Pedidos_Claves_OCEN
----	Set Cant_Requerida = ( Cant_Requerida - @iCantidadSolicitada  ), 
----		Cant_PreSolicitada = ( Cant_PreSolicitada + @iCantidadSolicitada  ) 
----	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonal = @IdPersonal
----		And IdClaveSSA = @IdClaveSSA -- And CodigoEAN = @CodigoEAN

	If @EsCentral = 1
		Begin 
			Update C Set C.Cantidad_Surtida = @CantidadSurtida, C.CantidadASurtir = ( C.Cantidad_Pedido - @CantidadSurtida )
			From COM_OCEN_REG_PedidosDet_Claves C (Nolock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @Unidad And IdTipoPedido = @IdTipoPedido 
			And FolioPedido =  @FolioPedido And IdClaveSSA = @IdClaveSSA
		End
	Else
		Begin
			Update C Set C.CantidadAComprar = @CantidadSurtida
			From COM_OCEN_PedidosDet_Claves C (Nolock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @Unidad And IdTipoPedido = @IdTipoPedido 
			And FolioPedido =  @FolioPedido And IdClaveSSA = @IdClaveSSA
		End

End
Go--#SQL



