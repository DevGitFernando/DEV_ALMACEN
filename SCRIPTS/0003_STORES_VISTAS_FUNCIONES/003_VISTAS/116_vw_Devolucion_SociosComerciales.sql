If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Devolucion_SociosComerciales' and xType = 'V' ) 
	Drop View vw_Devolucion_SociosComerciales
Go--#SQL 

Create View vw_Devolucion_SociosComerciales
With Encryption 
As 
	Select
		D.IdEmpresa, V.Empresa, D.IdEstado, V.Estado, D.IdFarmacia, V.Farmacia, D.FolioDevolucion as Folio, 
		V.IdSocioComercial, V.Nombre As SocioComercial, V.IdSucursal, V.NombreSucursal,
		D.TipoDeDevolucion, 'Entrada' as NombreTipoDeDevolucion, 
		D.Referencia as FolioVenta, D.FolioMovtoInv, D.FechaSistema, D.FechaSistemaDevol, D.FechaRegistro, 
		D.IdPersonal, vP.NombreCompleto as NombrePersonal,  
		D.Observaciones,
		D.SubTotal, D.Iva, D.Total, D.Corte, D.Status
	From DevolucionesEnc D (nolock) 
	Inner Join vw_Personal vP (NoLock) On ( D.IdEstado = vP.IdEstado and D.IdFarmacia = vP.IdFarmacia and D.IdPersonal = vP.IdPersonal )  			
	Inner Join vw_VentasEnc_SociosComerciales V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.referencia = V.folioventa ) 
	Where D.TipoDeDevolucion = 10
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devolucion_SociosComerciales' and xType = 'V' ) 
	Drop View vw_Impresion_Devolucion_SociosComerciales 
Go--#SQL	

Create View vw_Impresion_Devolucion_SociosComerciales
With Encryption 
As 

	Select  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, E.FolioVenta, E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones,
		   E.IdSocioComercial, E.SocioComercial, E.IdSucursal, E.NombreSucursal,
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, D.CodigoEAN, 
		   -- (D.Cant_Devuelta * D.PrecioCosto_Unitario) as Importe, 
		   L.ClaveLote, D.PrecioCosto_Unitario, L.Cant_Devuelta as CantidadLote, 
		   D.TasaIva, (D.PrecioCosto_Unitario * L.Cant_Devuelta) as SubTotalLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro as FechaRegistroLote
	From vw_Devolucion_SociosComerciales E (NoLock) 
	Inner Join DevolucionesDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDevolucion ) 
	Inner Join DevolucionesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN )  
	
Go--#SQL	

