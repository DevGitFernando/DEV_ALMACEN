If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_VentasEnc_SociosComerciales' and xType = 'V' ) 
	Drop View vw_VentasEnc_SociosComerciales 
Go--#SQL

Create View vw_VentasEnc_SociosComerciales
With Encryption 
As 

	Select S.IdEmpresa, E.Nombre As Empresa, S.IdEstado, F.Estado, S.IdFarmacia, F.Farmacia, S.FolioVenta, S.FolioMovtoInv,
		S.IdSocioComercial, O.Nombre, S.IdSucursal, U.NombreSucursal, S.IdPersonal, P.NombreCompleto, S.FechaRegistro,
		S.Observaciones, S.SubTotal, S.Iva, S.Total, S.Status
	From VentasEnc_SociosComerciales S (NoLock)
	Inner Join 	CatEmpresas E (NoLock) On (S.IdEmpresa = E.IdEmpresa)
	Inner Join vw_Farmacias F (NoLock) ON (S.IdEstado = F.IdEstado And S.IdFarmacia = F.IdFarmacia)
	Inner Join vw_Personal P (NoLock) On (S.IdEstado = P.IdEstado And S.IdFarmacia = P.IdFarmacia And S.IdPersonal = P.IdPersonal)
	Inner Join CatSociosComerciales O (NoLock) On (S.IdSocioComercial = O.IdSocioComercial)
	Inner Join CatSociosComerciales_Sucursales U (NoLock) On (S.IdSocioComercial = U.IdSocioComercial And S.IdSucursal = U.IdSucursal)
	
Go--#SQL
------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_VentasDet_SociosComerciales' and xType = 'V' ) 
	Drop View vw_VentasDet_SociosComerciales 
Go--#SQL

Create View vw_VentasDet_SociosComerciales
With Encryption 
As 

	Select
		D.IdEmpresa, E.Empresa, D.IdEstado, E.Estado, D.IdFarmacia, E.Farmacia, D.FolioVenta,
		E.IdPersonal, E.NombreCompleto,
		D.IdProducto, P.ClaveSSA, P.DescripcionSal, D.CodigoEAN, P.Descripcion, D.Renglon, D.TasaIva,
		D.Precio, D.Cant_Vendida, D.Cant_Devuelta, D.CantidadVendida
	From VentasDet_SociosComerciales D (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN)
	Inner Join 	vw_VentasEnc_SociosComerciales E (NoLock)
		On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.FolioVenta = E.FolioVenta ) 
		
Go--#SQL

------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_VentasDet_SociosComerciales_Lotes' and xType = 'V' ) 
	Drop View vw_VentasDet_SociosComerciales_Lotes 
Go--#SQL  	

Create View vw_VentasDet_SociosComerciales_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.FolioVenta, E.ClaveSSA, E.DescripcionSal,
		L.IdProducto, L.CodigoEAN, E.Descripcion, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, --, D.Keyx as KeyxDetalleLote
		cast( (D.Cant_Vendida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Vendida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadVendida as int) as CantidadVendida,
		E.Precio, E.TasaIva		
	From vw_VentasDet_SociosComerciales E (NoLock) 
	Inner Join VentasDet_SociosComerciales_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	
Go--#SQL
--------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_VentasDet_SociosComerciales_Lotes_Ubicaciones' and xType = 'V' ) 
	Drop View vw_VentasDet_SociosComerciales_Lotes_Ubicaciones 
Go--#SQL	 	

Create View vw_VentasDet_SociosComerciales_Lotes_Ubicaciones  
With Encryption 
As 	

	Select D.IdEmpresa, E.Nombre As Empresa, D.IdEstado, F.Estado, D.IdFarmacia, F.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		D.FolioVenta, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño,	 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		L.Existencia, cast(D.CantidadVendida as int) as Cantidad 
	From VentasDet_SociosComerciales_Lotes_Ubicaciones D (NoLock)		
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote and
			 D.IdPasillo = L.IdPasillo and D.IdEstante = L.IdEstante and D.IdEntrepaño = L.IdEntrepaño )
	Inner Join CatEmpresas E (NoLock) On ( D.IdEmpresa = E.IdEmpresa )	 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia )

Go--#SQL