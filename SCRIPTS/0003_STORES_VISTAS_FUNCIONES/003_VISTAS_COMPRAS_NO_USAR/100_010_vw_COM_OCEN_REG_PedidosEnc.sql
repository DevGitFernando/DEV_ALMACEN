



--------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_OCEN_REG_PedidosEnc' and xType = 'V' ) 
	Drop View vw_COM_OCEN_REG_PedidosEnc 
Go--#SQL 	

Create View vw_COM_OCEN_REG_PedidosEnc 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,  
	     M.IdEstado, F.Estado as Estado, F.ClaveRenapo, M.IdFarmacia, F.Farmacia as Farmacia, 
		 M.IdTipoPedido, PT.TipoPedido, PT.Descripcion as TipoPedidoDesc,
		 M.FolioPedido as Folio,
		 M.FechaSistema, M.FechaRegistro, M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		 M.Observaciones, M.StatusPedido, M.Status, M.FolioPedidoUnidad, 
		 Case When M.StatusPedido = 'S' Then 'NO TERMINADO' When M.StatusPedido = 'P' Then 'TERMINADO'
		 Else 'SIN PROCESAR' End As StatusDePedido
	From COM_OCEN_REG_Pedidos M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	-- Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia )
	Inner Join COM_FAR_Pedidos_Tipos PT(NoLock) On ( M.IdTipoPedido = PT.IdTipoPedido )
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstadoRegistra = vP.IdEstado and M.IdFarmaciaRegistra = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )  	
Go--#SQL 

----------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_OCEN_REG_PedidosDet_Claves' and xType = 'V' ) 
	Drop View vw_COM_OCEN_REG_PedidosDet_Claves
Go--#SQL 	

Create View vw_COM_OCEN_REG_PedidosDet_Claves 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, 
		M.IdTipoPedido, PT.TipoPedido, PT.Descripcion as TipoPedidoDesc,
		M.Folio, D.IdClaveSSA, S.ClaveSSA, S.DescripcionClave As Descripcion, S.Presentacion, S.ContenidoPaquete, 
		M.FechaSistema, M.FechaRegistro, M.IdPersonal, M.NombrePersonal,
		D.Cantidad_Pedido as Cantidad, D.Cantidad_Surtida As Cantidad_Comprada, D.CantidadASurtir As CantASurtir, 	
		M.Observaciones, M.StatusPedido, M.Status 
	From vw_COM_OCEN_REG_PedidosEnc M (NoLock) 
	Inner Join COM_OCEN_REG_PedidosDet_Claves D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia 
			 And M.IdTipoPedido = D.IdTipoPedido and M.Folio = D.FolioPedido ) 
	Inner Join COM_FAR_Pedidos_Tipos PT(NoLock) On ( M.IdTipoPedido = PT.IdTipoPedido )
	--Inner Join vw_Productos_CodigoEAN S (NoLock) On ( S.CodigoEAN = D.CodigoEAN  ) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( D.IdClaveSSA = S.IdClaveSSA_SAl ) 
	
Go--#SQL 	
-- select * from vw_COM_OCEN_REG_PedidosDet_Claves (Nolock) 