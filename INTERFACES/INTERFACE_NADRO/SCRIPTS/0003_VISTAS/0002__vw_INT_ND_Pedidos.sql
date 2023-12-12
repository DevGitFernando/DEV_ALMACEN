If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_INT_ND_PedidosEnc' And xType = 'V' )
	Drop view vw_INT_ND_PedidosEnc
Go--#SQL 

Create view vw_INT_ND_PedidosEnc 
With Encryption
As 

	Select distinct 
		M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, M.IdFarmacia, F.NombreFarmacia as Farmacia, 
		M.FolioPedido as Folio, 
		M.FolioPedidoReferencia, 
		M.FolioMovtoInv, 
		M.IdPersonal, vP.NombreCompleto as NombrePersonal, M.FechaRegistro, 
		M.IdProveedor, P.Nombre as Proveedor, 
		I.ReferenciaFolioPedido, I.TipoDeFarmacias, 
		I.CodigoCliente, I.NombreCliente, I.IdFarmaciaPedido, I.FarmaciaPedido, 
		M.Observaciones, 
		M.SubTotal, M.Iva, M.Total, 
		I.FechaPromesaEntrega,  
		M.Status   		  
	From INT_ND_PedidosEnc M (NoLock)   
	Inner Join 
	( 
		Select 
		OC.IdEmpresa, OC.IdEstado, OC.IdFarmacia, OC.Farmacia, OC.Folio as FolioPedido, OC.TipoDeFarmacias, 
		OC.IdProveedor, OC.FechaRegistro, OC.FechaPromesaEntrega, OC.Status, OC.CodigoCliente, 
		OC.NombreCliente, OC.ReferenciaFolioPedido, OC.IdFarmaciaPedido, OC.FarmaciaPedido  
		From vw_INT_ND_PedidosUnidades OC (NoLock) 
	) as I On ( M.IdEmpresa = I.IdEmpresa and M.IdEstado = I.IdEstado and M.IdFarmacia = I.IdFarmaciaPedido 
				and M.ReferenciaFolioPedido = I.ReferenciaFolioPedido  ) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join CatProveedores P (NoLock) On ( M.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
	
	
Go--#SQL 
 
 
 
---------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INT_ND_PedidosDet' and xType = 'V' ) 
	Drop View vw_INT_ND_PedidosDet 
Go--#SQL 	  	

Create View vw_INT_ND_PedidosDet 
With Encryption 
As 
	Select Distinct 
		M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, M.ReferenciaFolioPedido, 
		P.ClaveSSA, P.DescripcionClave, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, D.Renglon, 
		D.CantidadPrometida, 
		(D.CantidadPrometida * P.ContenidoPaquete) as CantidadPrometidaPiezas,  
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida, 
		D.CostoUnitario as Costo, D.TasaIva, D.SubTotal, D.ImpteIva, D.Importe		
	From vw_INT_ND_PedidosEnc M (NoLock) 
	Inner Join INT_ND_PedidosDet D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioPedido ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	
	
Go--#SQL	
 
 
 
---------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INT_ND_PedidosDet_Lotes' and xType = 'V' ) 
	Drop View vw_INT_ND_PedidosDet_Lotes 
Go--#SQL  	

Create View vw_INT_ND_PedidosDet_Lotes  
With Encryption 
As 	

	Select Distinct 
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, L.IdSubFarmacia, F.SubFarmacia,
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		D.Renglon,  
		cast(L.Existencia as Int) as Existencia, 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida		 
	From vw_INT_ND_PedidosDet E (NoLock) 
	Inner Join INT_ND_PedidosDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioPedido and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and 
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	Inner Join vw_Farmacias_SubFarmacias F (Nolock)
		On (  L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia )
Go--#SQL 	
 	

 
---------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INT_ND_PedidosDet_Lotes_Ubicaciones' and xType = 'V' ) 
	Drop View vw_INT_ND_PedidosDet_Lotes_Ubicaciones 
Go--#SQL	 	

Create View vw_INT_ND_PedidosDet_Lotes_Ubicaciones  
With Encryption 
As 	

	Select D.IdEmpresa, E.Nombre As Empresa, D.IdEstado, F.Estado, D.IdFarmacia, F.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		D.FolioPedido As Folio, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño,
		-- P.FechaRegistro as FechaReg, P.FechaCaducidad as FechaCad,		 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		L.Existencia, 
		cast(D.CantidadRecibida as int) as CantidadRecibida, 
		cast(D.CantidadRecibida as int) as Cantidad 
	From INT_ND_PedidosDet_Lotes_Ubicaciones D (NoLock)		
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote and
			 D.IdPasillo = L.IdPasillo and D.IdEstante = L.IdEstante and D.IdEntrepaño = L.IdEntrepaño )
	Inner Join CatEmpresas E (NoLock) On ( D.IdEmpresa = E.IdEmpresa )	 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia )
----	Inner Join FarmaciaProductos_CodigoEAN_Lotes P (NoLock) 
----		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.IdSubFarmacia = P.IdSubFarmacia and  
----			 D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and D.ClaveLote = P.ClaveLote )

--		Select * From vw_INT_ND_PedidosDet_Lotes_Ubicaciones (Nolock)

Go--#SQL 



