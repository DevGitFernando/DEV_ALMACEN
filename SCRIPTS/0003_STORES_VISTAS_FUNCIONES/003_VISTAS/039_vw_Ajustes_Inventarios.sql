
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_AjustesInv_Enc' and xType = 'V' ) 
	Drop View vw_AjustesInv_Enc 
Go--#SQL	

Create View vw_AjustesInv_Enc 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		M.IdFarmacia, F.NombreFarmacia as Farmacia, 
		M.Poliza as Poliza, 
		-- M.EsConsignacion, 
		-- 0 as EsConsignacion, 
		M.FechaSistema, M.FechaRegistro, 
		M.IdPersonalRegistra as IdPersonal, 
		vP.NombreCompleto as NombrePersonal, 
		M.Observaciones, 
		M.SubTotal, M.Iva, M.Total, M.PolizaAplicada, M.MovtoAplicado, 
		M.FolioVentaEntrada, M.FolioVentaSalida, M.FolioConsignacionEntrada, M.FolioConsignacionSalida, 
		M.Status, M.Keyx   
	From AjustesInv_Enc M (NoLock) 
	Inner Join CatEmpresas Ex On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonalRegistra = vP.IdPersonal )
Go--#SQL	

-------------------------------------------------------------------------  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_AjustesInv_Det' and xType = 'V' ) 
	Drop View vw_AjustesInv_Det
Go--#SQL	

Create View vw_AjustesInv_Det
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Poliza, M.PolizaAplicada, M.MovtoAplicado,
		M.IdPersonal, M.NombrePersonal, M.FechaRegistro,
		P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.ClaveSSA_Base, 
		P.DescripcionCortaClave as DescripcionClave, 
		D.IdProducto, D.CodigoEAN, P.DescripcionCorta as DescProducto, P.IdPresentacion, P.Presentacion, P.ContenidoPaquete,
		D.UnidadDeSalida, D.TasaIva, D.ExistenciaFisica, D.Costo, D.Importe, D.ExistenciaSistema,
		IsNull( Cast(L.Existencia as Int), 0 )  as ExistenciaActualFarmacia,
		-- D.IdTipoMovto_Inv, D.TipoES, 
		D.Status as StatusDet, 
		D.Keyx as KeyxDetalle, M.Observaciones  
	From vw_AjustesInv_Enc M (NoLock) 
	Inner Join AjustesInv_Det D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Poliza = D.Poliza ) 
	Left Join FarmaciaProductos_CodigoEAN L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and 
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	-- Where D.Status = 'A' 
Go--#SQL	
-------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_AjustesInv_Det_Lotes' and xType = 'V' ) 
	Drop View vw_AjustesInv_Det_Lotes 
Go--#SQL 	

Create View vw_AjustesInv_Det_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		IsNull(D.IdSubFarmacia, '') as IdSubFarmacia, IsNull(S.Descripcion, '') as SubFarmacia, 		
		E.Poliza, E.PolizaAplicada, E.MovtoAplicado, E.IdPersonal, E.NombrePersonal, E.FechaRegistro,
		E.IdClaveSSA, E.ClaveSSA, E.ClaveSSA_Base, E.DescripcionClave, 
		E.IdProducto, E.CodigoEAN, E.DescProducto, E.IdPresentacion, E.Presentacion, E.ContenidoPaquete,
		IsNull(D.ClaveLote, '') as ClaveLote, IsNull(D.EsConsignacion, 0) as EsConsignacion, 
		IsNull(cast(D.ExistenciaFisica as int), 0) as ExistenciaFisica, IsNull(D.Costo, 0) as Costo, IsNull(D.Importe, 0) as Importe, 
		IsNull(cast(D.ExistenciaSistema as Int), 0) as ExistenciaSistema,
		IsNull(L.FechaRegistro, GetDate()) as FechaReg, 
		IsNull(L.FechaCaducidad, GetDate()) as FechaCad, 
		convert(varchar(7), IsNull(L.FechaCaducidad, GetDate()), 120) as FechaCad_Aux, 	
		IsNull( Cast(L.Existencia as Int), 0 ) as ExistenciaActualFarmacia,
		(IsNull(cast(D.ExistenciaFisica as Int), 0) - IsNull(cast(D.ExistenciaSistema as int), 0)) as Diferencia, 
		IsNull(D.Referencia, '') as Referencia, IsNull(D.Status, '') as StatusDet_Lotes, 
		IsNull(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End, '') as StatusFarmaciaLote, 
		IsNull(D.Keyx, 0) as KeyxDetalleLote, E.Observaciones 
	From vw_AjustesInv_Det E (NoLock) 
	Inner Join AjustesInv_Det_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Poliza = D.Poliza and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and 
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	Inner Join CatFarmacias_SubFarmacias S(NoLock) On ( D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdSubFarmacia = S.IdSubFarmacia )

Go--#SQL	


-------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_AjustesInv_Det_Lotes_Ubicaciones' and xType = 'V' ) 
	Drop View vw_AjustesInv_Det_Lotes_Ubicaciones 
Go--#SQL 	

Create View vw_AjustesInv_Det_Lotes_Ubicaciones  
With Encryption 
As 	

	Select distinct 
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		IsNull(D.IdSubFarmacia, '') as IdSubFarmacia, IsNull(S.Descripcion, '') as SubFarmacia, 
		E.Poliza, E.PolizaAplicada, E.MovtoAplicado, E.IdPersonal, E.NombrePersonal, E.FechaRegistro,
		E.IdClaveSSA, E.ClaveSSA, E.DescripcionClave, 
		E.IdProducto, E.CodigoEAN, E.DescProducto, E.IdPresentacion, E.Presentacion, E.ContenidoPaquete,
		IsNull(D.ClaveLote, '') as ClaveLote, IsNull(D.EsConsignacion, 0) as EsConsignacion, 
		IsNull(D.IdPasillo, '') as IdPasillo, IsNull(P.DescripcionPasillo, '') as Pasillo, 
		IsNull(D.IdEstante, '') as IdEstante, IsNull(P.DescripcionEstante, '') as Estante, 
		IsNull(D.IdEntrepaño, '') as IdEntrepaño, IsNull(P.DescripcionEntrepaño, '') as Entrepaño, 
		IsNull(cast(D.ExistenciaFisica as int), 0) as ExistenciaFisica, IsNull(D.Costo, 0) as Costo, IsNull(D.Importe, 0) as Importe, 
		IsNull(cast(D.ExistenciaSistema as Int), 0) as ExistenciaSistema,
		IsNull( Cast(L.Existencia as Int), 0 ) as ExistenciaActualFarmacia,
		(IsNull(cast(D.ExistenciaFisica as Int), 0) - IsNull(cast(D.ExistenciaSistema as int), 0)) as Diferencia,
		--D.IdTipoMovto_Inv, D.TipoES, 
		IsNull(D.Referencia, '') as Referencia, IsNull(D.Status, '') as StatusDet_Lotes, 
		IsNull(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End, '') as StatusFarmaciaLote, 
		IsNull(D.Keyx, 0) as KeyxDetalleLote, E.Observaciones 
	From vw_AjustesInv_Det E (NoLock) 
	Left Join AjustesInv_Det_Lotes_Ubicaciones D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Poliza = D.Poliza and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Left Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and 
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote 
			 And D.IdPasillo = L.IdPasillo And D.IdEstante = L.IdEstante And D.IdEntrepaño = L.IdEntrepaño )
	Left Join CatFarmacias_SubFarmacias S(NoLock) On ( D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdSubFarmacia = S.IdSubFarmacia )
	Left Join vw_Pasillos_Estantes_Entrepaños P(NoLock)
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and 
		     D.IdPasillo = P.IdPasillo And D.IdEstante = P.IdEstante And D.IdEntrepaño = P.IdEntrepaño ) 

Go--#SQL	


