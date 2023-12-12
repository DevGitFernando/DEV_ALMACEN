If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Traspasos' and xType = 'V' ) 
	Drop View vw_Impresion_Traspasos 
Go--#SQL	

Create View vw_Impresion_Traspasos 
With Encryption 
As 
	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   E.IdSubFarmaciaOrigen, dbo.fg_ObtenerNombreSubFarmacia(E.IdEstado, E.IdFarmacia, E.IdSubFarmaciaOrigen) as SubFarmaciaOrigen,
		   E.IdSubFarmaciaDestino, dbo.fg_ObtenerNombreSubFarmacia(E.IdEstado, E.IdFarmacia, E.IdSubFarmaciaDestino) as SubFarmaciaDestino, 		   
		   E.Folio, E.TipoTraspaso, 
		   E.FechaTraspaso as  FechaSistema, E.FechaReg, 
		   -- E.Referencia, E.MovtoAplicado, 
		   E.IdPersonal, E.NombrePersonal, E.Observaciones,		    		   		   	   
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.CodigoEAN, -- D.Cantidad, D.Costo, D.Importe, 
		   L.ClaveLote, L.Cantidad as CantidadLote, F.FechaCaducidad, F.FechaRegistro  
	From vw_TraspasosEnc E (NoLock) 
	Inner Join TraspasosDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioTraspaso ) 
	Inner Join TraspasosDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioTraspaso = L.FolioTraspaso 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = D.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto ) 
Go--#SQL 
	