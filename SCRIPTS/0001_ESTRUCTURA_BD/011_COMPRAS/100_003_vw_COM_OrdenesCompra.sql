------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_OrdenesDeComprasEnc' And xType = 'V' )
	Drop view vw_OrdenesDeComprasEnc
Go--#SQL 

Create view vw_OrdenesDeComprasEnc 
With Encryption
As
	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, M.IdFarmacia, F.NombreFarmacia as Farmacia, 
		 M.FolioOrdenCompra as Folio, 
		 M.FolioOrdenCompraReferencia, OC.FechaRegistro as FechaGeneracionOC, 
		 OC.IdEstado as EstadoGenera, OC.IdFarmacia as FarmaciaGenera, 
		 M.FolioMovtoInv, 
		 M.IdPersonal, vP.NombreCompleto as NombrePersonal, M.FechaRegistro, M.FechaSistema, 
		 M.IdProveedor, P.Nombre as Proveedor, M.ReferenciaDocto, M.EsFacturaOriginal, M.FechaDocto, M.FechaVenceDocto, M.Observaciones, 
		 M.SubTotal, M.Iva, M.Total, M.ImporteFactura, M.FechaPromesaEntrega,  
		 M.Status  
	From OrdenesDeComprasEnc M (NoLock) 
	Inner Join COM_OCEN_OrdenesCompra_Claves_Enc OC (NoLock) 
		On ( 
				M.IdEmpresa = OC.FacturarA and M.IdEstado = OC.EstadoEntrega and M.IdFarmacia = OC.EntregarEn and M.FolioOrdenCompraReferencia = OC.FolioOrden 
		   )  
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join CatProveedores P (NoLock) On ( M.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	


Go--#SQL


------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_OrdenesDeComprasDet' and xType = 'V' ) 
	Drop View vw_OrdenesDeComprasDet 
Go--#SQL 	  	

Create View vw_OrdenesDeComprasDet 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.FarmaciaGenera, M.IdFarmacia, M.Farmacia, M.Folio, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, D.Renglon, 
		D.CantidadPrometida, 
		(D.CantidadPrometida * P.ContenidoPaquete) as CantidadPrometidaPiezas,  
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida, 
		D.CostoUnitario as Costo, D.TasaIva, D.SubTotal, D.ImpteIva, D.Importe		
	From vw_OrdenesDeComprasEnc M (NoLock) 
	Inner Join OrdenesDeComprasDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioOrdenCompra ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto )  

Go--#SQL	
 	

------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_OrdenesDeComprasDet_Lotes' and xType = 'V' ) 
	Drop View vw_OrdenesDeComprasDet_Lotes 
Go--#SQL  	

Create View vw_OrdenesDeComprasDet_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.FarmaciaGenera, E.IdFarmacia, E.Farmacia, L.IdSubFarmacia, F.SubFarmacia,
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		D.Renglon,  
		cast(L.Existencia as Int) as Existencia, 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida		 
	From vw_OrdenesDeComprasDet E (NoLock) 
	Inner Join OrdenesDeComprasDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioOrdenCompra and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and 
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	Inner Join vw_Farmacias_SubFarmacias F (Nolock)
		On (  L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) 


Go--#SQL 	
 	


------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'V' ) 
	Drop View vw_OrdenesDeComprasDet_Lotes_Ubicaciones 
Go--#SQL	 	

Create View vw_OrdenesDeComprasDet_Lotes_Ubicaciones  
With Encryption 
As 	

	Select D.IdEmpresa, E.Nombre As Empresa, D.IdEstado, F.Estado, 
		--M.FarmaciaGenera, 
		D.IdFarmacia, F.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		D.FolioOrdenCompra As Folio, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño,
		-- P.FechaRegistro as FechaReg, P.FechaCaducidad as FechaCad,		 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		L.Existencia, 
		cast(D.CantidadRecibida as int) as CantidadRecibida, 
		cast(D.CantidadRecibida as int) as Cantidad 
	From OrdenesDeComprasDet_Lotes_Ubicaciones D (NoLock)		
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote and
			 D.IdPasillo = L.IdPasillo and D.IdEstante = L.IdEstante and D.IdEntrepaño = L.IdEntrepaño )
	Inner Join CatEmpresas E (NoLock) On ( D.IdEmpresa = E.IdEmpresa )	 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia )
----	Inner Join FarmaciaProductos_CodigoEAN_Lotes P (NoLock) 
----		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.IdSubFarmacia = P.IdSubFarmacia and  
----			 D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and D.ClaveLote = P.ClaveLote )

--		Select * From vw_OrdenesDeComprasDet_Lotes_Ubicaciones (Nolock)

Go--#SQL 

