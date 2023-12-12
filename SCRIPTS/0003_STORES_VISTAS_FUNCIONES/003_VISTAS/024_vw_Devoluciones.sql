If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesEnc_Ventas' and xType = 'V' ) 
	Drop View vw_DevolucionesEnc_Ventas 
Go--#SQL

Create View vw_DevolucionesEnc_Ventas 
With Encryption 
As 
	Select 
		D.IdEmpresa, V.Empresa, 
		D.IdEstado, V.Estado, V.ClaveRenapo, 
		D.IdFarmacia, V.Farmacia, V.EsAlmacen, 
		D.FolioDevolucion as Folio, D.TipoDeDevolucion, 'Venta' as NombreTipoDeDevolucion, 
		V.TipoDeVenta, V.NombreTipoDeVenta, D.Referencia as FolioVenta, 
		D.FolioMovtoInv, D.FechaSistema, D.FechaSistemaDevol, D.FechaRegistro, 
		D.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		D.Observaciones, 
		D.SubTotal, D.Iva, D.Total, D.Corte, D.Status, 
		V.IdCliente, V.NombreCliente, V.IdSubCliente, V.NombreSubCliente, 
		V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma,
		V.IdMunicipio, V.Municipio, V.IdColonia, V.Colonia, V.Domicilio	
	From DevolucionesEnc D (nolock) 
	Inner Join vw_Personal vP (NoLock) On ( D.IdEstado = vP.IdEstado and D.IdFarmacia = vP.IdFarmacia and D.IdPersonal = vP.IdPersonal )  			
	Inner Join vw_VentasEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.Referencia = V.Folio ) 
	Where D.TipoDeDevolucion = 1 	
Go--#SQL


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesDet_CodigosEAN_Ventas' and xType = 'V' ) 
	Drop View vw_DevolucionesDet_CodigosEAN_Ventas 
Go--#SQL 	

Create View vw_DevolucionesDet_CodigosEAN_Ventas 
With Encryption 
As 

	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, M.FolioVenta, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, P.ContenidoPaquete, 
		D.UnidadDeEntrada, D.TasaIva, D.Cant_Devuelta as Cantidad, 
		D.PrecioCosto_Unitario as Costo, 
		( D.Cant_Devuelta * D.PrecioCosto_Unitario ) * ( 1 + ( D.TasaIva / 100.00 ) )  as Importe,
		M.Corte, M.FechaSistema, M.FechaSistemaDevol, M.TipoDeDevolucion, M.TipoDeVenta, M.NombreTipoDeVenta, 
		M.IdPersonal, M.NombrePersonal
	From vw_DevolucionesEnc_Ventas M (NoLock) 
	Inner Join DevolucionesDet D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioDevolucion ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesDet_CodigosEAN_Lotes_Ventas' and xType = 'V' ) 
	Drop View vw_DevolucionesDet_CodigosEAN_Lotes_Ventas 
Go--#SQL
 	

Create View vw_DevolucionesDet_CodigosEAN_Lotes_Ventas  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
	    D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.Folio, E.FolioVenta, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, 
		cast(D.Cant_Devuelta as int) as Cantidad, --, D.Keyx as KeyxDetalleLote 
		(cast(D.Cant_Devuelta as int) / (E.ContenidoPaquete * 1.0)) as CantidadCajas 
	From vw_DevolucionesDet_CodigosEAN_Ventas E (NoLock) 
	Inner Join DevolucionesDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDevolucion and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL


------------------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesEnc_Compras' and xType = 'V' ) 
	Drop View vw_DevolucionesEnc_Compras 
Go--#SQL

Create View vw_DevolucionesEnc_Compras 
With Encryption 
As 
	Select 
		D.IdEmpresa, V.Empresa, D.IdEstado, V.Estado, V.ClaveRenapo, D.IdFarmacia, V.Farmacia, 
		D.FolioDevolucion as Folio, D.TipoDeDevolucion, 'Compra' as NombreTipoDeDevolucion, 
		D.Referencia as FolioCompra, D.FolioMovtoInv, D.FechaSistema, D.FechaSistemaDevol, D.FechaRegistro, 
		D.IdPersonal, vP.NombreCompleto as NombrePersonal, V.IdProveedor, V.Proveedor, V.ReferenciaDocto, V.FechaDocto, V.FechaVenceDocto, 
		D.Observaciones, 
		D.SubTotal, D.Iva, D.Total, D.Corte, D.Status
	From DevolucionesEnc D (nolock) 
	Inner Join vw_Personal vP (NoLock) On ( D.IdEstado = vP.IdEstado and D.IdFarmacia = vP.IdFarmacia and D.IdPersonal = vP.IdPersonal )  			
	Inner Join vw_ComprasEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.Referencia = V.Folio ) 
	Where D.TipoDeDevolucion = 2 	
Go--#SQL


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesDet_CodigosEAN_Compras' and xType = 'V' ) 
	Drop View vw_DevolucionesDet_CodigosEAN_Compras 
Go--#SQL 	

Create View vw_DevolucionesDet_CodigosEAN_Compras 
With Encryption 
As 

	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, M.FolioCompra, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		D.UnidadDeEntrada, D.TasaIva, D.Cant_Devuelta as Cantidad, 
		D.PrecioCosto_Unitario as Costo, 
		( D.Cant_Devuelta * D.PrecioCosto_Unitario ) * ( 1 + ( D.TasaIva / 100.00 ) )  as Importe,
		M.FechaSistema, M.FechaSistemaDevol, M.TipoDeDevolucion, 
		M.IdPersonal, M.NombrePersonal
	From vw_DevolucionesEnc_Compras M (NoLock) 
	Inner Join DevolucionesDet D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioDevolucion ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesDet_CodigosEAN_Lotes_Compras' and xType = 'V' ) 
	Drop View vw_DevolucionesDet_CodigosEAN_Lotes_Compras 
Go--#SQL
 	

Create View vw_DevolucionesDet_CodigosEAN_Lotes_Compras  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
	    D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.Folio, E.FolioCompra, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, cast(D.Cant_Devuelta as int) as Cantidad --, D.Keyx as KeyxDetalleLote 
	From vw_DevolucionesDet_CodigosEAN_Compras E (NoLock) 
	Inner Join DevolucionesDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDevolucion and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL


------------------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesEnc_Pedidos' and xType = 'V' ) 
	Drop View vw_DevolucionesEnc_Pedidos 
Go--#SQL

Create View vw_DevolucionesEnc_Pedidos 
With Encryption 
As 
	Select 
		D.IdEmpresa, V.Empresa, D.IdEstado, V.Estado, V.ClaveRenapo, D.IdFarmacia, V.Farmacia, 
		D.FolioDevolucion as Folio, D.TipoDeDevolucion, 'Pedido' as NombreTipoDeDevolucion, 
		D.Referencia as FolioPedido, D.FolioMovtoInv, D.FechaSistema, D.FechaSistemaDevol, D.FechaRegistro, 
		D.IdPersonal, vP.NombreCompleto as NombrePersonal, V.IdDistribuidor, V.Distribuidor, V.ReferenciaPedido, -- V.FechaDocto, V.FechaVenceDocto, 
		D.Observaciones, 
		D.SubTotal, D.Iva, D.Total, D.Corte, D.Status
	From DevolucionesEnc D (nolock) 
	Inner Join vw_Personal vP (NoLock) On ( D.IdEstado = vP.IdEstado and D.IdFarmacia = vP.IdFarmacia and D.IdPersonal = vP.IdPersonal )  			
	Inner Join vw_PedidosEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.Referencia = V.Folio ) 
	Where D.TipoDeDevolucion In( 4, 5 ) 	
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionesEnc_Entradas_Consignacion' and xType = 'V' ) 
	Drop View vw_DevolucionesEnc_Entradas_Consignacion
Go--#SQL 

Create View vw_DevolucionesEnc_Entradas_Consignacion 
With Encryption 
As 
	Select 
		D.IdEmpresa, V.Empresa, D.IdEstado, V.Estado, V.ClaveRenapo, D.IdFarmacia, V.Farmacia, 
		D.FolioDevolucion as Folio, D.TipoDeDevolucion, 'Entrada' as NombreTipoDeDevolucion, 
		D.Referencia as FolioEntrada, D.FolioMovtoInv, D.FechaSistema, D.FechaSistemaDevol, D.FechaRegistro, 
		D.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		-- V.IdDistribuidor, V.Distribuidor, 
		'' as IdDistribuidor, '' as Distribuidor, 
		V.ReferenciaPedido, -- V.FechaDocto, V.FechaVenceDocto, 
		D.Observaciones, 
		D.SubTotal, D.Iva, D.Total, D.Corte, D.Status
	From DevolucionesEnc D (nolock) 
	Inner Join vw_Personal vP (NoLock) On ( D.IdEstado = vP.IdEstado and D.IdFarmacia = vP.IdFarmacia and D.IdPersonal = vP.IdPersonal )  			
	Inner Join vw_EntradasEnc_Consignacion V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.Referencia = V.Folio ) 
	Where D.TipoDeDevolucion = 3  	
Go--#SQL
