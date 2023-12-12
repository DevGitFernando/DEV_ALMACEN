

----------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_REG_Pedidos_Claves' and xType = 'V' ) 
	Drop View vw_COM_REG_Pedidos_Claves 
Go--#SQL 	

Create View vw_COM_REG_Pedidos_Claves 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, 
		M.IdTipoPedido, PT.TipoPedido, PT.Descripcion as TipoPedidoDesc,
		M.Folio, D.IdClaveSSA, S.ClaveSSA, S.DescripcionSal, 
		M.FechaSistema, M.FechaRegistro, M.IdPersonal, M.NombrePersonal,
		S.ContenidoPaquete, S.Presentacion, D.Cantidad_Pedido as Cantidad, IsNull(Cantidad_Surtir, 0 ) as CantidadSurtir, 
		D.Cantidad_Pedido as CantidadCajas, 
		M.Observaciones, M.StatusPedido, M.Status 
	From vw_COM_REG_PedidosEnc M (NoLock) 
	Inner Join COM_REG_Pedidos_Claves D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia 
			 And M.IdTipoPedido = D.IdTipoPedido and M.Folio = D.FolioPedido ) 
	Inner Join COM_FAR_Pedidos_Tipos PT(NoLock) On ( M.IdTipoPedido = PT.IdTipoPedido )
	--Inner Join vw_Productos_CodigoEAN S (NoLock) On ( S.CodigoEAN = D.CodigoEAN  ) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( D.IdClaveSSA = S.IdClaveSSA_SAl ) 
	
Go--#SQL 	
-- select * from vw_COM_REG_Pedidos_Claves 