If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ComprasEnc' and xType = 'V' ) 
	Drop View vw_ComprasEnc 
Go--#SQL
 	

Create View vw_ComprasEnc 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,  
	     M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
	     M.IdFarmacia, F.NombreFarmacia as Farmacia, M.IdAlmacen, M.EsCompraAlmacen, 
	     M.EsPromocionRegalo, 
		 M.FolioCompra as Folio, M.FolioMovtoInv, 
		 M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		 M.FechaSistema, 
		 M.IdProveedor, P.Nombre as Proveedor, 
		 M.ReferenciaDocto, M.FechaDocto, M.FechaVenceDocto, M.Observaciones, 
		 M.SubTotal, M.Iva, M.Total, M.DoctoPagado, M.AbonoDocto, M.SaldoDocto, M.FechaPagoDocto, M.FechaRegistro, 
		 M.Status  
	From ComprasEnc M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join CatProveedores P (NoLock) On ( M.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
Go--#SQL	
 
-------------  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ComprasDet_CodigosEAN' and xType = 'V' ) 
	Drop View vw_ComprasDet_CodigosEAN 
Go--#SQL
 	

Create View vw_ComprasDet_CodigosEAN 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio,
		M.IdPersonal, M.NombrePersonal, M.IdProveedor, M.Proveedor,
		M.ReferenciaDocto, D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto,
		P.ClaveSSA, P.DescripcionSAl,  
		D.UnidadDeEntrada, D.TasaIva, -- ( D.Cant_Recibida - D.Cant_Devuelta ) as Cantidad, 
		D.CostoUnitario as Costo, -- , D.PrecioUnitario as Importe --, D.Keyx as KeyxDetalle  
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida,
		M.Status As StatusCompra
	From vw_ComprasEnc M (NoLock) 
	Inner Join ComprasDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioCompra ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto And D.CodigoEAN = P.CodigoEAN) 
	-- Where D.Status = 'A' 
Go--#SQL
 	


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ComprasDet_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_ComprasDet_CodigosEAN_Lotes 
Go--#SQL
 	

Create View vw_ComprasDet_CodigosEAN_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia,
		E.Folio, E.IdPersonal, E.NombrePersonal, E.IdProveedor, E.Proveedor, E.ReferenciaDocto, L.IdProducto, L.CodigoEAN, E.DescProducto,
		E.ClaveSSA, E.DescripcionSal, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, --, D.Keyx as KeyxDetalleLote
		TasaIva, Costo As CostoUnitario, 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida,
		Cast( (Costo * D.CantidadRecibida) As Numeric(14,4)) As SubTotalLote, 
		Cast( ( ( Costo * D.CantidadRecibida) * (TasaIva / 100)) As Numeric(14, 4)) As ImpteIvaLote,
		Cast( ( ( Costo * D.CantidadRecibida) * ( 1 +(TasaIva / 100) ) ) As Numeric(14, 4)) As ImporteLote,
		Cast( (Costo * D.Cant_Devuelta) As Numeric(14,4)) As SubTotalLoteDevuelto, 
		Cast( ( ( Costo * D.Cant_Devuelta) * (TasaIva / 100)) As Numeric(14, 4)) As ImpteIvaLoteDevuelto,
		Cast( ( ( Costo * D.Cant_Devuelta) * ( 1 +(TasaIva / 100) ) ) As Numeric(14, 4)) As ImporteLoteDevuelto,
		E.StatusCompra
	From vw_ComprasDet_CodigosEAN E (NoLock) 
	Inner Join ComprasDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioCompra and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL
 	

--------------------------------- ALMACENES 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ComprasDet_CodigosEAN_Lotes_Ubicaciones' and xType = 'V' ) 
   Drop View vw_ComprasDet_CodigosEAN_Lotes_Ubicaciones 
Go--#SQL	
 	

Create View vw_ComprasDet_CodigosEAN_Lotes_Ubicaciones  
With Encryption 
As 	

	Select D.IdEmpresa, E.Nombre As Empresa, D.IdEstado, F.Estado, D.IdFarmacia, F.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		D.FolioCompra As Folio, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño,
		-- P.FechaRegistro as FechaReg, P.FechaCaducidad as FechaCad,		 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		L.Existencia, cast(D.CantidadRecibida as int) as Cantidad 
	From ComprasDet_Lotes_Ubicaciones D (NoLock)		
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote and
			 D.IdPasillo = L.IdPasillo and D.IdEstante = L.IdEstante and D.IdEntrepaño = L.IdEntrepaño )
	Inner Join CatEmpresas E (NoLock) On ( D.IdEmpresa = E.IdEmpresa )	 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia )
----	Inner Join FarmaciaProductos_CodigoEAN_Lotes P (NoLock) 
----		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.IdSubFarmacia = P.IdSubFarmacia and  
----			 D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and D.ClaveLote = P.ClaveLote )

--		Select * From vw_ComprasDet_CodigosEAN_Lotes_Ubicaciones (Nolock)

Go--#SQL 
