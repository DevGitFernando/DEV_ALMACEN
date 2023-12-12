
------------------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesEnc_Orden_Compra' and xType = 'V' ) 
	Drop View vw_DevolucionesEnc_Orden_Compra 
Go--#SQL

Create View vw_DevolucionesEnc_Orden_Compra 
With Encryption 
As 
	Select 
		D.IdEmpresa, V.Empresa, D.IdEstado, V.Estado, V.ClaveRenapo, D.IdFarmacia, V.Farmacia, 
		D.FolioDevolucion as Folio, D.TipoDeDevolucion, 'Orden Compra' as NombreTipoDeDevolucion, 
		D.Referencia as FolioOrdenCompra, V.FolioOrdenCompraReferencia as ReferenciaFolioOrdenCompra,
		D.FolioMovtoInv, D.FechaSistema, D.FechaSistemaDevol, D.FechaRegistro, 
		D.IdPersonal, vP.NombreCompleto as NombrePersonal, V.IdProveedor, V.Proveedor, V.ReferenciaDocto, V.FechaDocto, V.FechaVenceDocto, 
		D.Observaciones, 
		D.SubTotal, D.Iva, D.Total, D.Corte, D.Status
	From DevolucionesEnc D (nolock) 
	Inner Join vw_Personal vP (NoLock) On ( D.IdEstado = vP.IdEstado and D.IdFarmacia = vP.IdFarmacia and D.IdPersonal = vP.IdPersonal )  			
	Inner Join vw_OrdenesDeComprasEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.Referencia = V.Folio ) 
	Where D.TipoDeDevolucion = 6 	
Go--#SQL


------------------------------------------------------------------------------------------------------



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devolucion_Orden_Compra' and xType = 'V' ) 
	Drop View vw_Impresion_Devolucion_Orden_Compra 
Go--#SQL 	

Create View vw_Impresion_Devolucion_Orden_Compra 
With Encryption 
As 

	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, S.SubFarmacia, E.Folio, E.FolioOrdenCompra, E.ReferenciaFolioOrdenCompra,
		   E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdProveedor, E.Proveedor, E.ReferenciaDocto, E.FechaDocto, E.FechaVenceDocto, 
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, vP.Presentacion_ClaveSSA, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, vP.Presentacion, D.CodigoEAN, 
		   -- (D.CantidadRecibida * D.CostoUnitario) as Importe, 
		   L.ClaveLote, D.PrecioCosto_Unitario, L.Cant_Devuelta as CantidadLote, 
		   D.TasaIva, (D.PrecioCosto_Unitario * L.Cant_Devuelta) as SubTotalLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.PrecioCosto_Unitario * L.Cant_Devuelta) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro  
	From vw_DevolucionesEnc_Orden_Compra E (NoLock) 
	Inner Join DevolucionesDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDevolucion ) 
	Inner Join DevolucionesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = D.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto )
	Inner Join vw_Farmacias_SubFarmacias S (Nolock)
		On (  L.IdEstado = S.IdEstado and L.IdFarmacia = S.IdFarmacia and L.IdSubFarmacia = S.IdSubFarmacia ) 
	
Go--#SQL


