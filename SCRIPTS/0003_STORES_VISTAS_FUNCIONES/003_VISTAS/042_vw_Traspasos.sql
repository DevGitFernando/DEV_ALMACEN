If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TraspasosEnc' and xType = 'V' ) 
	Drop View vw_TraspasosEnc 
Go--#SQL 	

Create View vw_TraspasosEnc 
With Encryption 
As 
	Select -- IsNull(Tx.FolioTransferenciaRef, '*') as FolioReferencia,
	    T.IdEmpresa, Ce.Nombre as Empresa, 
		T.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		T.IdFarmacia, F.NombreFarmacia as Farmacia,		  
		T.FolioTraspaso as Folio, 
		T.IdSubFarmaciaOrigen, S.Descripcion As SubFarmaciaOrigen,
		T.IdSubFarmaciaDestino, SD.Descripcion As SubFarmaciaDestino,  
		T.FolioTraspasoRef, T.FolioMovtoInv, T.TipoTraspaso, 
		T.FechaTraspaso, T.FechaRegistro as FechaReg, 
		T.IdPersonal as IdPersonal, 
		vP.NombreCompleto as NombrePersonal, 
		T.Observaciones, 
		T.SubTotal, T.Iva, T.Total, T.Status, 
		T.Status as StatusTransferencia 
		-- T.Keyx   
	From TraspasosEnc T (NoLock) 
	Inner Join CatEmpresas Ce (NoLock) On ( T.IdEmpresa = Ce.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )
	Inner Join CatFarmacias_SubFarmacias S (Nolock) 
		On ( T.IdEstado = S.IdEstado and T.IdFarmacia = S.IdFarmacia and T.IdSubFarmaciaOrigen = S.IdSubFarmacia )
	Inner Join CatFarmacias_SubFarmacias SD (Nolock) 
		On ( T.IdEstado = SD.IdEstado and T.IdFarmacia = SD.IdFarmacia and T.IdSubFarmaciaDestino = SD.IdSubFarmacia )	 
	Inner Join vw_Personal vP (NoLock) On ( T.IdEstado = vP.IdEstado and T.IdFarmacia = vP.IdFarmacia and T.IdPersonal = vP.IdPersonal )	
Go--#SQL


---------------------------------------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TraspasosDet_CodigosEAN' and xType = 'V' ) 
	Drop View vw_TraspasosDet_CodigosEAN 
Go--#SQL	

Create View vw_TraspasosDet_CodigosEAN 
With Encryption 
As 
	Select T.IdEmpresa, T.Empresa, T.IdEstado, T.Estado, T.ClaveRenapo, T.IdFarmacia, T.Farmacia, T.Folio, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		1 as UnidadDeSalida, D.TasaIva, D.Cantidad, 
		D.CostoUnitario as Costo, D.SubTotal as Importe, D.ImpteIva as Iva, D.Importe as Total -- , D.Keyx as KeyxDetalle  
	From vw_TraspasosEnc T (NoLock) 
	Inner Join TraspasosDet D (NoLock) On ( T.IdEmpresa = D.IdEmpresa and T.IdEstado = D.IdEstado and T.IdFarmacia = D.IdFarmacia and T.Folio = D.FolioTraspaso ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TraspasosDet_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_TraspasosDet_CodigosEAN_Lotes 
Go--#SQL	


Create View vw_TraspasosDet_CodigosEAN_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, cast(D.Cantidad as int) as Cantidad -- , D.Keyx as KeyxDetalleLote 
	From vw_TraspasosDet_CodigosEAN E (NoLock) 
	Inner Join TraspasosDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioTraspaso and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL	


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TraspasosDet_CodigosEAN_Lotes_Ubicaciones' and xType = 'V' ) 
	Drop View vw_TraspasosDet_CodigosEAN_Lotes_Ubicaciones 
Go--#SQL	


Create View vw_TraspasosDet_CodigosEAN_Lotes_Ubicaciones  
With Encryption 
As 	

	Select D.IdEmpresa, E.Nombre As Empresa, D.IdEstado, F.Estado, D.IdFarmacia, F.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		D.FolioTraspaso As Folio, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño,				 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		L.Existencia, cast(D.Cantidad as int) as Cantidad 
	From TraspasosDet_Lotes_Ubicaciones D (NoLock)		
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote and
			 D.IdPasillo = L.IdPasillo and D.IdEstante = L.IdEstante and D.IdEntrepaño = L.IdEntrepaño )
	Inner Join CatEmpresas E (NoLock) On ( D.IdEmpresa = E.IdEmpresa )	 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) 
Go--#SQL

