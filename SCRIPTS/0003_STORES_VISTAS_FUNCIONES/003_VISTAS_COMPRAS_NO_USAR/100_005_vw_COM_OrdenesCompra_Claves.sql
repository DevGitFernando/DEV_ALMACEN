



If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_OrdenesCompras_Claves_Enc' And xType = 'V' )
	Drop view vw_OrdenesCompras_Claves_Enc
Go--#SQL


Create view vw_OrdenesCompras_Claves_Enc
With Encryption
As
	Select M.IdEmpresa, Ex.Nombre as Empresa,		    
		 M.IdEstado, E.Nombre as Estado, M.IdFarmacia, F.NombreFarmacia as Farmacia, 
		 M.FolioOrden as Folio,  
		 M.FacturarA, EF.Nombre as FacturarAEmpresa, EF.Domicilio As DomicilioEmpresa, EF.Colonia, EF.EdoCiudad, EF.CodigoPostal, EF.RFC,
		 M.IdProveedor, P.Nombre as Proveedor,  
		 M.IdPersonal, vP.NombreCompleto as NombrePersonal, M.FechaRegistro,
		 M.EstadoEntrega, CE.Nombre as NomEstadoEntrega, M.EntregarEn, VF.Farmacia as FarmaciaEntregarEn,
		 ( VF.Domicilio + ' ' + IsNull(VF.Colonia, '') + ' ' + IsNull(VF.Municipio, '') + ' ' + IsNull(VF.Estado, '') ) as Domicilio,
		 M.FechaRequeridaEntrega, M.FechaColocacion, M.TipoOrden, 
		 M.Observaciones, M.Status, M.EsAutomatica, M.FolioPedido As FolioPedidoUnidad, M.EsCentral  
	From COM_OCEN_OrdenesCompra_Claves_Enc M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join CatProveedores P (NoLock) On ( M.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )
	Inner Join CatEstados CE (NoLock) On ( M.EstadoEntrega = CE.IdEstado ) 
	Inner Join vw_Farmacias VF (NoLock) On ( M.EstadoEntrega = VF.IdEstado and M.EntregarEn = VF.IdFarmacia )
	Inner Join CatEmpresas EF (NoLock) On ( M.FacturarA = EF.IdEmpresa )


--	Select * From vw_OrdenesCompras_Claves_Enc (Nolock)

	
Go--#SQL


--------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_OrdenesCompras_Claves_Det' and xType = 'V' ) 
	Drop View vw_OrdenesCompras_Claves_Det 
Go--#SQL 	
 	

Create View vw_OrdenesCompras_Claves_Det 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.IdFarmacia, M.Farmacia, M.Folio, 
		P.ClaveSSA, D.IdClaveSSA, P.Descripcion, M.TipoOrden, D.Cantidad, D.Precio, D.Descuento, 
		D.TasaIva, D.Iva, D.PrecioUnitario, D.ImpteIva, D.Importe		
	From vw_OrdenesCompras_Claves_Enc M (NoLock) 
	Inner Join COM_OCEN_OrdenesCompra_Claves_Det D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioOrden ) 
	Inner Join CatClavesSSA_Sales P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal )

--	Select * From vw_OrdenesCompras_Claves_Det (Nolock)
 
Go--#SQL	
 	
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_OrdenesCompras_Claves' and xType = 'V' ) 
	Drop View vw_Impresion_OrdenesCompras_Claves 
Go--#SQL 


Create view vw_Impresion_OrdenesCompras_Claves
With Encryption
As
	Select M.IdEmpresa, M.Empresa,		    
		 M.IdEstado, M.Estado, M.IdFarmacia, M.Farmacia, 
		 M.Folio, M.FacturarA, M.FacturarAEmpresa,
		 M.DomicilioEmpresa, M.Colonia, M.EdoCiudad, M.CodigoPostal, M.RFC, 
		 M.IdProveedor, M.Proveedor,  
		 M.IdPersonal, M.NombrePersonal, M.FechaRegistro,
		 M.EstadoEntrega, M.NomEstadoEntrega, M.EntregarEn, M.FarmaciaEntregarEn,
		 M.Domicilio,
		 M.FechaRequeridaEntrega, M.FechaColocacion, M.TipoOrden,
		 M.Observaciones, M.Status,
		 D.ClaveSSA, D.IdClaveSSA, D.Descripcion, D.Cantidad, D.Precio, D.Descuento, 
		 D.TasaIva, D.Iva, D.PrecioUnitario, D.ImpteIva, D.Importe, 
		 ( Select TOP 1 Valor From COM_Nota_OrdenDeCompra (Nolock) 
			Where ArbolModulo in ( 'COMP',  'COMR' ) And NombreParametro = 'NotaOrdenDeCompra' ) As NotaOrdenCompra
	From vw_OrdenesCompras_Claves_Enc M (NoLock) 
	Inner Join vw_OrdenesCompras_Claves_Det D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.Folio )

--	Select * From vw_Impresion_OrdenesCompras_Claves (Nolock)

Go--#SQL

--------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_OrdenesCompras_CodigosEAN_Det' and xType = 'V' ) 
	Drop View vw_OrdenesCompras_CodigosEAN_Det 
Go--#SQL 	
 	

Create View vw_OrdenesCompras_CodigosEAN_Det 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.IdFarmacia, M.Farmacia, M.Folio,
		P.IdClaveSSA_Sal, P.ClaveSSA_Base As ClaveSSA, P.DescripcionSal, P.DescripcionCortaClave, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA, 
		P.IdProducto, D.CodigoEAN, P.DescripcionCorta As Descripcion, P.Presentacion, M.TipoOrden,
		P.ContenidoPaquete,
		D.Cantidad as CantidadCajas, 
		(D.Cantidad * P.ContenidoPaquete) as Cantidad, 
		D.Precio, D.Descuento, 
		D.TasaIva, D.Iva, 
		D.PrecioUnitario as PrecioCaja, 
		cast((D.PrecioUnitario / (P.ContenidoPaquete * 1.0)) as numeric(14,4)) as PrecioUnitario, 
		D.ImpteIva, D.Importe, D.CantidadSobreCompra, D.ObservacionesSobreCompra		
	From vw_OrdenesCompras_Claves_Enc M (NoLock) 
	Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioOrden ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) 
		On ( D.CodigoEAN = P.CodigoEAN )

--	Select * From vw_OrdenesCompras_CodigosEAN_Det (Nolock)
 
Go--#SQL	



---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_OrdenesCompras_CodigosEAN' and xType = 'V' ) 
	Drop View vw_Impresion_OrdenesCompras_CodigosEAN 
Go--#SQL 


Create view vw_Impresion_OrdenesCompras_CodigosEAN
With Encryption
As
	Select M.IdEmpresa, M.Empresa,		    
		 M.IdEstado, M.Estado, M.IdFarmacia, M.Farmacia, 
		 M.Folio, M.FacturarA, M.FacturarAEmpresa,
		 M.DomicilioEmpresa, M.Colonia, M.EdoCiudad, M.CodigoPostal, M.RFC, 
		 M.IdProveedor, M.Proveedor,  
		 M.IdPersonal, M.NombrePersonal, M.FechaRegistro,
		 M.EstadoEntrega, M.NomEstadoEntrega, M.EntregarEn, M.FarmaciaEntregarEn,
		 M.Domicilio,
		 M.FechaRequeridaEntrega, M.FechaColocacion, M.TipoOrden,
		 M.Observaciones, M.Status,
		 D.IdClaveSSA_Sal, D.ClaveSSA, D.DescripcionSal, D.DescripcionCortaClave, D.Presentacion_ClaveSSA, D.ContenidoPaquete_ClaveSSA,
		 D.IdProducto, D.CodigoEAN, D.Descripcion, D.Presentacion, D.ContenidoPaquete,
		 D.Cantidad, D.CantidadCajas, D.Precio, D.Descuento, 
		 D.TasaIva, D.Iva, D.PrecioUnitario, D.PrecioCaja, D.ImpteIva, D.Importe, 
		 ( Select TOP 1 Valor From COM_Nota_OrdenDeCompra (Nolock) 
			Where ArbolModulo in ( 'COMP',  'COMR' ) And NombreParametro = 'NotaOrdenDeCompra' ) As NotaOrdenCompra
	From vw_OrdenesCompras_Claves_Enc M (NoLock) 
	Inner Join vw_OrdenesCompras_CodigosEAN_Det D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.Folio )

--	Select * From vw_Impresion_OrdenesCompras_CodigosEAN (Nolock)

Go--#SQL