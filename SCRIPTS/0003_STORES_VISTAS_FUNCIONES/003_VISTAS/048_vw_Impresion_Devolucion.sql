--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devolucion_Compras' and xType = 'V' ) 
	Drop View vw_Impresion_Devolucion_Compras 
Go--#SQL	

Create View vw_Impresion_Devolucion_Compras 
With Encryption 
As 

	Select  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, E.FolioCompra, E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdProveedor, E.Proveedor, E.ReferenciaDocto, E.FechaDocto, E.FechaVenceDocto, 
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, D.CodigoEAN, 
		   -- (D.Cant_Devuelta * D.PrecioCosto_Unitario) as Importe, 
		   L.ClaveLote, D.PrecioCosto_Unitario, L.Cant_Devuelta as CantidadLote, 
		   D.TasaIva, (D.PrecioCosto_Unitario * L.Cant_Devuelta) as SubTotalLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro as FechaRegistroLote
	From vw_DevolucionesEnc_Compras E (NoLock) 
	Inner Join DevolucionesDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDevolucion ) 
	Inner Join DevolucionesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN ) 
	
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devolucion_Ventas_Contado' and xType = 'V' ) 
	Drop View vw_Impresion_Devolucion_Ventas_Contado   
Go--#SQL   

Create View vw_Impresion_Devolucion_Ventas_Contado 
With Encryption 
As 
	Select 
		v.IdEmpresa, v.Empresa, v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.Folio, v.FolioVenta, v.FechaSistema, v.FechaSistemaDevol, 
		v.FechaRegistro, v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		v.Corte, v.Status, v.SubTotal, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, vP.DescripcionClave, vP.DescripcionCortaClave, 
		D.IdProducto, L.CodigoEAN, vP.Descripcion, vP.DescripcionCorta,
		L.ClaveLote, D.PrecioCosto_Unitario, L.Cant_Devuelta as CantidadLote, 
		   D.TasaIva, (D.PrecioCosto_Unitario * L.Cant_Devuelta) as SubTotalLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro as FechaRegistroLote,
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio	 
	From vw_DevolucionesEnc_Ventas v (NoLock) 
	Inner Join DevolucionesDet D (NoLock) On ( v.IdEmpresa = D.IdEmpresa and v.IdEstado = D.IdEstado and v.IdFarmacia = D.IdFarmacia and v.Folio = D.FolioDevolucion ) 
	Inner Join DevolucionesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN )  
	Where TipoDeVenta = 1 
	
Go--#SQL

--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devolucion_Ventas_Credito' and xType = 'V' ) 
	Drop View vw_Impresion_Devolucion_Ventas_Credito 
Go--#SQL  

Create View vw_Impresion_Devolucion_Ventas_Credito 
With Encryption 
As 

	Select 
		v.IdEmpresa, v.Empresa, v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.EsAlmacen, 
		v.Folio, v.FolioVenta, V.Observaciones, v.FechaSistema, v.FechaRegistro, 
		v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		-- v.IdPaciente, v.FolioDerechoHabiencia, v.FolioReceta, 
		B.IdBeneficiario, B.NombreCompleto as Beneficiario, 
		B.FolioReferencia, I.NumReceta, I.FechaReceta, 
		I.IdMedico, I.Medico, v.SubTotal, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, vP.DescripcionClave, vP.DescripcionCortaClave, 
		D.IdProducto, L.CodigoEAN, vP.Descripcion, vP.Descripcion as DescProducto, 
		vP.ContenidoPaquete, vP.DescripcionCorta,
		L.ClaveLote, D.PrecioCosto_Unitario, 
		L.Cant_Devuelta as CantidadLote, 
		(L.Cant_Devuelta / (vP.ContenidoPaquete * 1.0)) as CantidadCajasLote, 
		D.TasaIva, (D.PrecioCosto_Unitario * L.Cant_Devuelta) as SubTotalLote, 
		(D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		(D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		F.FechaCaducidad, F.FechaCaducidad as FechaCad, F.FechaRegistro as FechaRegistroLote,
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio	 
		-- , I.Sexo, I.SexoAux, I.IdMedico, I.Medico, I.IdDiagnostico, I.Diagnostico, 		I.IdServicio, I.Servicio, I.IdArea, I.Area_Servicio as Area  
	From vw_DevolucionesEnc_Ventas v (NoLock) 
	Inner Join vw_VentasDispensacion_InformacionAdicional I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.Folio ) 
	Left Join vw_Beneficiarios B (NoLock) 
		On ( I.IdEstado = B.IdEstado and I.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente 
			 and I.IdBeneficiario = B.IdBeneficiario ) 	
	Inner Join DevolucionesDet D (NoLock) On ( v.IdEmpresa = D.IdEmpresa and v.IdEstado = D.IdEstado and v.IdFarmacia = D.IdFarmacia and v.Folio = D.FolioDevolucion ) 
	Inner Join DevolucionesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN ) 
	Where TipoDeVenta = 2 
	
Go--#SQL

--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devolucion_Pedidos' and xType = 'V' ) 
	Drop View vw_Impresion_Devolucion_Pedidos 
Go--#SQL	

Create View vw_Impresion_Devolucion_Pedidos 
With Encryption 
As 

	Select  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, E.FolioPedido, E.TipoDeDevolucion, E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdDistribuidor, E.Distribuidor, E.ReferenciaPedido, --E.FechaDocto, E.FechaVenceDocto, 
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, D.CodigoEAN, 
		   -- (D.Cant_Devuelta * D.PrecioCosto_Unitario) as Importe, 
		   L.ClaveLote, D.PrecioCosto_Unitario, L.Cant_Devuelta as CantidadLote, 
		   D.TasaIva, (D.PrecioCosto_Unitario * L.Cant_Devuelta) as SubTotalLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro as FechaRegistroLote
	From vw_DevolucionesEnc_Pedidos E (NoLock) 
	Inner Join DevolucionesDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDevolucion ) 
	Inner Join DevolucionesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN )  
	 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devolucion_Entradas_Consignacion' and xType = 'V' ) 
	Drop View vw_Impresion_Devolucion_Entradas_Consignacion 
Go--#SQL	

Create View vw_Impresion_Devolucion_Entradas_Consignacion 
With Encryption 
As 

	Select  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, E.FolioEntrada, E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdDistribuidor, E.Distribuidor, E.ReferenciaPedido, --E.FechaDocto, E.FechaVenceDocto, 
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, D.CodigoEAN, 
		   -- (D.Cant_Devuelta * D.PrecioCosto_Unitario) as Importe, 
		   L.ClaveLote, D.PrecioCosto_Unitario, L.Cant_Devuelta as CantidadLote, 
		   D.TasaIva, (D.PrecioCosto_Unitario * L.Cant_Devuelta) as SubTotalLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro as FechaRegistroLote
	From vw_DevolucionesEnc_Entradas_Consignacion E (NoLock) 
	Inner Join DevolucionesDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDevolucion ) 
	Inner Join DevolucionesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN )  
	
Go--#SQL	

