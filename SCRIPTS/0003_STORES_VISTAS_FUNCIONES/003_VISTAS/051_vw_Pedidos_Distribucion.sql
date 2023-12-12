If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosDistEnc' and xType = 'V' ) 
	Drop View vw_PedidosDistEnc 
Go--#SQL 	 

Create View vw_PedidosDistEnc 
With Encryption 
As 
	Select 
	    T.IdEmpresa, Ce.Nombre as Empresa, 
		T.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		T.IdFarmacia, F.NombreFarmacia as Farmacia, 
		T.IdAlmacen, T.EsSalidaAlmacen,  
		T.FolioDistribucion as Folio, T.FolioPedidoRef,  
		T.FolioMovtoInv, T.TipoSalida, T.DestinoEsAlmacen, 
		T.FechaSalida, T.FechaRegistro, 
		T.IdPersonal as IdPersonal, 
		vP.NombreCompleto as NombrePersonal, 
		T.Observaciones, 
		T.SubTotal, T.Iva, T.Total, T.Status, 
		T.IdEstadoRecibe, Ex.Nombre as EstadoRecibe, Ex.ClaveRenapo as ClaveRenapoRecibe, 
		T.IdFarmaciaRecibe, Fx.NombreFarmacia as FarmaciaRecibe,  
		T.IdAlmacenRecibe, T.Status as StatusTransferencia 
		-- T.Keyx   
	From PedidosDistEnc T (NoLock) 
	Inner Join CatEmpresas Ce (NoLock) On ( T.IdEmpresa = Ce.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatEstados Ex (NoLock) On ( T.IdEstadoRecibe = Ex.IdEstado ) 
	Inner Join CatFarmacias Fx (NoLock) On ( T.IdEstadoRecibe = Fx.IdEstado and T.IdFarmaciaRecibe = Fx.IdFarmacia ) 
	Inner Join vw_Personal vP (NoLock) On ( T.IdEstado = vP.IdEstado and T.IdFarmacia = vP.IdFarmacia and T.IdPersonal = vP.IdPersonal )	
Go--#SQL

-------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosDistDet' and xType = 'V' ) 
	Drop View vw_PedidosDistDet 
Go--#SQL	

Create View vw_PedidosDistDet 
With Encryption 
As 
	Select T.IdEmpresa, T.Empresa, T.IdEstado, T.Estado, T.ClaveRenapo, T.IdFarmacia, T.Farmacia, T.Folio, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		D.UnidadDeEntrada, D.TasaIva, D.Cant_Enviada as Cantidad, 
		D.CostoUnitario as Costo, D.SubTotal as Importe, D.ImpteIva as Iva, D.Importe as Total -- , D.Keyx as KeyxDetalle  
	From vw_PedidosDistEnc T (NoLock) 
	Inner Join PedidosDistDet D (NoLock) On ( T.IdEmpresa = D.IdEmpresa and T.IdEstado = D.IdEstado and T.IdFarmacia = D.IdFarmacia and T.Folio = D.FolioDistribucion ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL


-------------------------------------- Select * From PedidosDistEncDet_Lotes(NoLock) 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosDistDet_Lotes' and xType = 'V' ) 
	Drop View vw_PedidosDistDet_Lotes 
Go--#SQL	

Create View vw_PedidosDistDet_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmaciaEnvia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmaciaEnvia) as SubFarmaciaEnvia, 
		D.IdSubFarmaciaRecibe, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmaciaRecibe) as SubFarmaciaRecibe, 
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, D.Renglon, L.FechaRegistro, L.FechaCaducidad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as StatusLote, 
		cast(L.Existencia as Int) as Existencia, cast(D.CantidadEnviada as int) as Cantidad -- , D.Keyx as KeyxDetalleLote 
	From vw_PedidosDistDet E (NoLock) 
	Inner Join PedidosDistDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDistribucion and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmaciaEnvia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL	
