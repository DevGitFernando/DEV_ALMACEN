If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Compras' and xType = 'V' ) 
	Drop View vw_Impresion_Compras 
Go--#SQL	

Create View vw_Impresion_Compras 
With Encryption 
As 

	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, E.IdAlmacen, E.EsCompraAlmacen, E.EsPromocionRegalo, 
		   E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdProveedor, E.Proveedor, E.ReferenciaDocto, E.FechaDocto, E.FechaVenceDocto, 
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, D.CodigoEAN, 
		   -- (D.CantidadRecibida * D.CostoUnitario) as Importe, 
		   L.ClaveLote, D.CostoUnitario, L.CantidadRecibida as CantidadLote, 
		   D.TasaIva, (D.CostoUnitario * L.CantidadRecibida) as SubTotalLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro  
	From vw_ComprasEnc E (NoLock) 
	Inner Join ComprasDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioCompra ) 
	Inner Join ComprasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioCompra = L.FolioCompra 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) -- and D.ClaveLote = L.ClaveLote )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto ) 
Go--#SQL
	
	