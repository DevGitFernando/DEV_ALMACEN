If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Pedidos_Distribucion' and xType = 'V' ) 
	Drop View vw_Impresion_Pedidos_Distribucion 
Go--#SQL	

Create View vw_Impresion_Pedidos_Distribucion 
With Encryption 
As 
	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmaciaEnvia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmaciaEnvia) as SubFarmaciaEnvia, 		   
		   E.Folio, E.TipoSalida, 
		   E.FechaSalida as  FechaSistema, E.FechaRegistro as FechaReg, 
		   -- E.Referencia, E.MovtoAplicado, 
		   E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdEstadoRecibe, E.EstadoRecibe, E.ClaveRenapoRecibe, E.IdFarmaciaRecibe, E.FarmaciaRecibe, 	
		   L.IdSubFarmaciaRecibe, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmaciaRecibe) as SubFarmaciaRecibe, 		   		   	   
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.CodigoEAN, -- D.Cantidad, D.Costo, D.Importe, 
		   L.ClaveLote, L.CantidadEnviada as CantidadLote, F.FechaCaducidad, F.FechaRegistro  
	From vw_PedidosDistEnc E (NoLock) 
	Inner Join PedidosDistDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioDistribucion ) 
	Inner Join PedidosDistDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDistribucion = L.FolioDistribucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = D.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmaciaEnvia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto ) 
Go--#SQL 
	