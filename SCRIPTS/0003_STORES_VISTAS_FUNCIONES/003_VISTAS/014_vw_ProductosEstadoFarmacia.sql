-------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_ProductosEstadoFarmacia' and xType = 'V' ) 
   Drop View vw_ProductosEstadoFarmacia
Go--#SQL	
    
Create View vw_ProductosEstadoFarmacia 
With Encryption 
As 

	Select Distinct E.IdEstado, E.Nombre as Estado, E.ClaveRENAPO, 
		-- FE.IdFarmacia, 
		P.IdProducto, P.IdClaveSSA_Sal, P.ClaveSSA_Base, P.ClaveSSA, P.ClaveSSA_Aux, P.DescripcionSal, P.Descripcion, P.DescripcionCorta, P.IdClasificacion, P.Clasificacion, 
		P.IdTipoProducto, P.TipoDeProducto, P.TasaIva, 
		P.EsControlado, P.EsSectorSalud, 
		P.IdFamilia, P.Familia, P.IdSubFamilia, P.SubFamilia, P.IdLaboratorio, 
		P.Laboratorio, P.IdPresentacion, P.Presentacion, P.Despatilleo, P.ContenidoPaquete, P.ManejaCodigosEAN, P.PrecioMaxPublico, 
		P.Status as StatusProducto,  
		Cr.CodigoEAN, Cr.CodigoEAN_Interno, -- IsNull(FE.Existencia, 0) as Existencia, 
		Cr.Status as StatusEAN 
	From vw_Productos P (NoLock) 
	Inner Join CatProductos_CodigosRelacionados Cr (NoLock) On ( P.IdProducto = Cr.IdProducto and Cr.Status =  'A' ) 
	Inner Join CatEstados E (NoLock) On ( E.IdEstado = E.IdEstado ) 
	--Inner Join CatProductos_Estado PE (NoLock) On ( P.IdProducto = PE.IdProducto and Pe.Status = 'A' ) 
	--Inner Join CatEstados E (NoLock) On ( PE.IdEstado = E.IdEstado ) 
	---- Left Join FarmaciaProductos_CodigoEAN FE (NoLock) On ( FE.IdProducto = Cr.IdProducto and FE.CodigoEAN = Cr.CodigoEAN ) 

Go--#SQL	


-------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_ProductosExistenEnEstadoFarmacia' and xType = 'V' ) 
   Drop View vw_ProductosExistenEnEstadoFarmacia
Go--#SQL 

Create View vw_ProductosExistenEnEstadoFarmacia 
With Encryption 
As 

	Select Distinct E.IdEstado, E.Nombre as Estado, E.ClaveRENAPO, 
		F.IdFarmacia, 
		P.IdProducto, P.IdClaveSSA_Sal, P.ClaveSSA_Base, P.ClaveSSA, P.ClaveSSA_Aux, P.DescripcionSal, P.Descripcion, P.DescripcionCorta, P.IdClasificacion, P.Clasificacion, 
		P.IdTipoProducto, P.TipoDeProducto, P.TasaIva, 
		P.EsControlado, P.EsSectorSalud, 
		P.IdFamilia, P.Familia, P.IdSubFamilia, P.SubFamilia, P.IdLaboratorio, 
		P.Laboratorio, P.IdPresentacion, P.Presentacion, P.Despatilleo, P.ContenidoPaquete, P.ManejaCodigosEAN, P.Status as StatusProducto,  
		P.CodigoEAN, P.CodigoEAN_Interno, 
		
		(Case When Ex.Status In ( 'I', 'S' ) Then 0 Else (F.Existencia - ( F.ExistenciaEnTransito + F.ExistenciaSurtidos ))End) as Existencia, 

		-- F.Existencia, 
		Ex.Status as StatusDeProducto, 
		P.Status as StatusEAN 
	From FarmaciaProductos_CodigoEAN F (NoLock) 
	Inner Join FarmaciaProductos Ex (NoLock) 
		On ( F.IdEmpresa = Ex.IdEmpresa and F.IdEstado = Ex.IdEstado and F.IdFarmacia = Ex.IdFarmacia and F.IdProducto = Ex.IdProducto ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = F.IdProducto and P.CodigoEAN = F.CodigoEAN and P.StatusCodigoRel =  'A' ) 
	-- Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = F.IdProducto and P.StatusCodigoRel =  'A' ) 	
	--Inner Join CatProductos_Estado PE (NoLock) On ( F.IdEstado = PE.IdEstado and P.IdProducto = PE.IdProducto and Pe.Status = 'A' ) 
	Inner Join CatEstados E (NoLock) On ( E.IdEstado = E.IdEstado ) 
	-- Left Join FarmaciaProductos_CodigoEAN FE (NoLock) On ( FE.IdProducto = Cr.IdProducto and FE.CodigoEAN = Cr.CodigoEAN ) 

Go--#SQL

