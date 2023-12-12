---------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Entradas_Consignacion' and xType = 'V' ) 
	Drop View vw_Impresion_Entradas_Consignacion 
Go--#SQL	

Create View vw_Impresion_Entradas_Consignacion 
With Encryption 
As 

	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, 
		   E.FechaRegistro As FechaRegistroPed, E.IdPersonal, E.NombrePersonal, E.Observaciones, 

		   E.IdProveedor, E.Proveedor, 
		   -- E.IdDistribuidor, E.Distribuidor, 
		   '' as IdDistribuidor, '' as Distribuidor, 
		   E.ReferenciaPedido, E.ReferenciaDePedidoOC, 
		   
		   E.SubTotal, E.Iva, E.Total, E.Status, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA_Base, vP.ClaveSSA, vP.ClaveSSA_Aux, vP.DescripcionSal, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, D.CodigoEAN, 
		   -- (D.CantidadRecibida * D.CostoUnitario) as Importe, 
		   L.ClaveLote, D.CostoUnitario, L.CantidadRecibida as CantidadLote, 
		   D.TasaIva, (D.CostoUnitario * L.CantidadRecibida) as SubTotalLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro, E.EsReferenciaDePedido, E.FolioPedido, E.EsPosFechado  
	From vw_EntradasEnc_Consignacion E (NoLock) 
	--Inner Join vw_Proveedores P (NoLock) On ( E.IdProveedor = P.IdProveedor ) 
	----Left Join vw_PedidosEnc_Consignacion PC (NoLock) 
	----	On ( E.IdEmpresa = PC.IdEmpresa and E.IdEstado = PC.IdEstado and E.IdFarmacia = PC.IdFarmacia and E.FolioPedido = PC.FolioPedido ) 
	Inner Join EntradasDet_Consignacion D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioEntrada ) 
	Inner Join EntradasDet_Consignacion_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioEntrada = L.FolioEntrada 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN ) 
	
	
	
Go--#SQL
