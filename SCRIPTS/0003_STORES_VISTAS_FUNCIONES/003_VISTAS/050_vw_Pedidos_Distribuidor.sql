If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosEnc' and xType = 'V' ) 
	Drop View vw_PedidosEnc 
Go--#SQL
 	
Create View vw_PedidosEnc 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,  
	     M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
	     M.IdFarmacia, F.NombreFarmacia as Farmacia, 
	     M.FolioPedido as Folio, M.FolioMovtoInv,
		 M.EsConsignacion, M.TipoDeEntrada, 
		 M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		 M.FechaRegistro, 
		 M.IdDistribuidor, P.NombreDistribuidor as Distribuidor, 
		 M.ReferenciaPedido, M.Observaciones, 
		 M.SubTotal, M.Iva, M.Total,  
		 M.Status  
	From PedidosEnc M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join CatDistribuidores P (NoLock) On ( M.IdDistribuidor = P.IdDistribuidor ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
Go--#SQL	
 
------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosDet_CodigosEAN' and xType = 'V' ) 
	Drop View vw_PedidosDet_CodigosEAN 
Go--#SQL 	

Create View vw_PedidosDet_CodigosEAN 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		D.UnidadDeEntrada, D.TasaIva, -- ( D.Cant_Recibida - D.Cant_Devuelta ) as Cantidad, 
		D.CostoUnitario as Costo, -- , D.PrecioUnitario as Importe --, D.Keyx as KeyxDetalle  
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida   		
	From vw_PedidosEnc M (NoLock) 
	Inner Join PedidosDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioPedido ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL
 	

------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosDet_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_PedidosDet_CodigosEAN_Lotes 
Go--#SQL  	

Create View vw_PedidosDet_CodigosEAN_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, --, D.Keyx as KeyxDetalleLote
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida   
	From vw_PedidosDet_CodigosEAN E (NoLock) 
	Inner Join PedidosDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioPedido and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL
 	
