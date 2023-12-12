If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_ComConcentrado_Pedidos_Claves' And xType = 'P' ) 
	Drop Proc spp_COM_OCEN_ComConcentrado_Pedidos_Claves
Go--#SQL
 
Create Proc spp_COM_OCEN_ComConcentrado_Pedidos_Claves 
(
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdFarmaciaPedido varchar(4), 
	@IdPersonal varchar(4), @IdTipoPedido varchar(2), @FolioPedido varchar(6) -- , @Efecto smallint = 0  
)
As 
Begin 
-- Set NoCount On 
Declare @iEfecto int 

	Set @iEfecto = 1 
	-- If @Efecto = 1 
	--   Set @iEfecto = -1 

--- Obtener las Claves del Pedido 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdFarmaciaPedido as IdFarmaciaPedido, 
		C.IdClaveSSA, C.CodigoEAN, ceiling(C.Cantidad_Surtida * @iEfecto)as Cantidad 
	Into #tmpClaves 
	From COM_OCEN_PedidosDet C (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( C.IdClaveSSA = P.IdClaveSSA_Sal and C.CodigoEAN = P.CodigoEAN ) 
	Where C.IdEmpresa = @IdEmpresa and C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmaciaPedido 
		and IdTipoPedido = @IdTipoPedido and FolioPedido = @FolioPedido 


--- Actualizar la Cantidad Requerida por Clave 
	Update C Set Cant_Requerida = (C.Cant_Requerida + T.Cantidad)
	From COM_OCEN_Concentrado_Pedidos_Claves C (NoLock) 
	Inner Join #tmpClaves T (NoLock) On ( C.IdEmpresa = T.IdEmpresa and C.IdEstado = T.IdEstado and C.IdFarmacia = T.IdFarmacia
		-- And C.IdFarmaciaPedido = T.IdFarmaciaPedido 
		and C.IdClaveSSA = T.IdClaveSSA and C.CodigoEAN = T.CodigoEAN and C.IdPersonal = @IdPersonal )


--- Agregar las Claves Nuevas 
	Insert Into COM_OCEN_Concentrado_Pedidos_Claves 
		(  IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, IdPersonal, IdClaveSSA, CodigoEAN, Cant_Requerida, Cant_PreSolicitada, Status, Actualizado ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, @IdPersonal as IdPersonal, IdClaveSSA, CodigoEAN, Cantidad, 0 as Cant_PreSolicitada, 
		'A' as Status, 0 as Actualizado
	From #tmpClaves T (NoLock) 
	Where Not Exists ( Select C.CodigoEAN From COM_OCEN_Concentrado_Pedidos_Claves C 
						 Where C.IdEmpresa = T.IdEmpresa and C.IdEstado = T.IdEstado and C.IdFarmacia = T.IdFarmacia
			                   and C.IdClaveSSA = T.IdClaveSSA and C.CodigoEAN = T.CodigoEAN and C.IdPersonal = @IdPersonal ) 

End 
Go--#SQL 

-- sp_listacolumnas COM_OCEN_Concentrado_Pedidos_Claves 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_REG_ComConcentrado_Pedidos_Claves' And xType = 'P' ) 
	Drop Proc spp_COM_OCEN_REG_ComConcentrado_Pedidos_Claves
Go--#SQL
 
Create Proc spp_COM_OCEN_REG_ComConcentrado_Pedidos_Claves 
(
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdFarmaciaPedido varchar(4), 
	@IdPersonal varchar(4), @IdTipoPedido varchar(2), @FolioPedido varchar(6) -- , @Efecto smallint = 0  
)
As 
Begin 
-- Set NoCount On 
Declare @iEfecto int 

	Set @iEfecto = 1 
	-- If @Efecto = 1 
	--   Set @iEfecto = -1 

--- Obtener las Claves del Pedido 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdFarmaciaPedido as IdFarmaciaPedido, 
		C.IdClaveSSA, C.CodigoEAN, ceiling(C.Cantidad_Surtida * @iEfecto)as Cantidad 
	Into #tmpClaves 
	From COM_OCEN_REG_PedidosDet C (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( C.IdClaveSSA = P.IdClaveSSA_Sal and C.CodigoEAN = P.CodigoEAN ) 
	Where C.IdEmpresa = @IdEmpresa and C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmaciaPedido 
		and IdTipoPedido = @IdTipoPedido and FolioPedido = @FolioPedido 


--- Actualizar la Cantidad Requerida por Clave 
	Update C Set Cant_Requerida = (C.Cant_Requerida + T.Cantidad)
	From COM_OCEN_REG_Concentrado_Pedidos_Claves C (NoLock) 
	Inner Join #tmpClaves T (NoLock) On ( C.IdEmpresa = T.IdEmpresa and C.IdEstado = T.IdEstado and C.IdFarmacia = T.IdFarmacia
		-- And C.IdFarmaciaPedido = T.IdFarmaciaPedido 
		and C.IdClaveSSA = T.IdClaveSSA and C.CodigoEAN = T.CodigoEAN and C.IdPersonal = @IdPersonal )


--- Agregar las Claves Nuevas 
	Insert Into COM_OCEN_REG_Concentrado_Pedidos_Claves 
		(  IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, IdPersonal, IdClaveSSA, CodigoEAN, Cant_Requerida, Cant_PreSolicitada, Status, Actualizado ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, @IdPersonal as IdPersonal, IdClaveSSA, CodigoEAN, Cantidad, 0 as Cant_PreSolicitada, 
		'A' as Status, 0 as Actualizado
	From #tmpClaves T (NoLock) 
	Where Not Exists ( Select C.CodigoEAN From COM_OCEN_REG_Concentrado_Pedidos_Claves C 
						 Where C.IdEmpresa = T.IdEmpresa and C.IdEstado = T.IdEstado and C.IdFarmacia = T.IdFarmacia
			                   and C.IdClaveSSA = T.IdClaveSSA and C.CodigoEAN = T.CodigoEAN and C.IdPersonal = @IdPersonal ) 

End 
Go--#SQL 