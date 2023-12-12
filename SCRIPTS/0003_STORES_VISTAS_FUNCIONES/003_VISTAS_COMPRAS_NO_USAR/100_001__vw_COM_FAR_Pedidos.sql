
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_FAR_PedidosEnc' and xType = 'V' ) 
	Drop View vw_COM_FAR_PedidosEnc 
Go--#SQL 	

Create View vw_COM_FAR_PedidosEnc 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,  
	     M.IdEstado, F.Estado as Estado, F.ClaveRenapo, M.IdFarmacia, F.Farmacia as Farmacia, 
		 M.IdTipoPedido, PT.TipoPedido, PT.Descripcion as TipoPedidoDesc,
		 M.FolioPedido as Folio,
		 M.FechaSistema, M.FechaRegistro, M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		 M.Observaciones, M.StatusPedido, M.Status,
		 Case When M.TipoDePedido = 1 Then 'PEDIDO ESPECIAL DE CLAVES' Else 'PEDIDO ESPECIAL DE PRODUCTOS' End As DescTipoDePedido
	From COM_FAR_Pedidos M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	-- Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia )
	Inner Join COM_FAR_Pedidos_Tipos PT(NoLock) On ( M.IdTipoPedido = PT.IdTipoPedido )
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )  	
Go--#SQL 

----------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_FAR_Pedidos_Productos' and xType = 'V' ) 
	Drop View vw_COM_FAR_Pedidos_Productos 
Go--#SQL 	

Create View vw_COM_FAR_Pedidos_Productos 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, 
		M.IdTipoPedido, PT.TipoPedido, PT.Descripcion as TipoPedidoDesc,
		M.Folio, D.IdClaveSSA, S.ClaveSSA, D.CodigoEAN, S.DescripcionSal, S.Descripcion as DescripcionProducto, 
		M.FechaSistema, M.FechaRegistro, M.IdPersonal, M.NombrePersonal,
		S.ContenidoPaquete, D.Cantidad_Pedido as Cantidad, IsNull(Cantidad_Surtir, 0 ) as CantidadSurtir, 
		cast(ceiling((D.Cantidad_Pedido / (S.ContenidoPaquete*1.0))) as int ) as CantidadCajas, 
		M.Observaciones, M.StatusPedido, M.Status 
	From vw_COM_FAR_PedidosEnc M (NoLock) 
	Inner Join COM_FAR_Pedidos_Productos D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia 
			 And M.IdTipoPedido = D.IdTipoPedido and M.Folio = D.FolioPedido ) 
	Inner Join COM_FAR_Pedidos_Tipos PT(NoLock) On ( M.IdTipoPedido = PT.IdTipoPedido )
	Inner Join vw_Productos_CodigoEAN S (NoLock) On ( S.CodigoEAN = D.CodigoEAN  ) 
	-- Inner Join vw_ClavesSSA_Sales S (NoLock) On ( D.IdClaveSSA = S.IdClaveSSA_SAl ) 
	
Go--#SQL 	
