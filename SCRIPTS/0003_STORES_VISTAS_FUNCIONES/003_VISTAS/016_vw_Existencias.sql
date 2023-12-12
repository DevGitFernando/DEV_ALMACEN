
------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'V' ) 
   Drop View vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones   
Go--#SQL

Create View vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
With Encryption 
As 

	Select	U.IdEmpresa, E.Nombre as Empresa, U.IdEstado as IdEstado, F.Estado, U.IdFarmacia, F.Farmacia, 
			U.IdSubFarmacia, FS.Descripcion as SubFarmacia, 
			P.IdClaveSSA_Sal, P.ClaveSSA_Base, P.ClaveSSA, P.DescripcionSal, P.DescripcionClave, P.DescripcionCortaClave, 
			P.TipoDeClave, P.TipoDeClaveDescripcion, P.EsControlado, P.EsAntibiotico, P.EsRefrigerado, 
			P.IdPresentacion_ClaveSSA, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA,
			P.IdProducto, U.CodigoEAN, P.CodigoEAN_Interno, P.Descripcion as DescripcionProducto,
			FP.FechaCaducidad, datediff(mm, getdate(), IsNull(FP.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar, FP.FechaRegistro,  
			P.IdPresentacion, P.Presentacion, P.ContenidoPaquete, P.IdLaboratorio, P.Laboratorio, 
			U.ClaveLote, U.EsConsignacion, U.IdPasillo, PE.DescripcionPasillo as Pasillo, U.IdEstante, PE.DescripcionEstante as Estante, 
			U.IdEntrepaño, PE.DescripcionEntrepaño as Entrepaño, 
			
			IsNull(U.Existencia, 0) as ExistenciaAux, 
			IsNull(U.ExistenciaEnTransito, 0) as ExistenciaEnTransito,
			(Case When dbo.fg_INV_GetStatusProducto(IsNull(U.IdEmpresa, ''), IsNull(U.IdEstado, ''), IsNull(U.IdFarmacia, ''), P.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else IsNull((U.ExistenciaSurtidos), 0) End) as ExistenciaSurtidos,
			(Case When dbo.fg_INV_GetStatusProducto(IsNull(U.IdEmpresa, ''), IsNull(U.IdEstado, ''), IsNull(U.IdFarmacia, ''), P.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else IsNull((U.Existencia - (U.ExistenciaEnTransito + U.ExistenciaSurtidos)), 0) End) as Existencia, 
			
			(IsNull((U.Existencia - U.ExistenciaEnTransito), 0) * (case when F.EsUnidosis = 0 Then P.ContenidoPiezasUnitario else 1 end) ) as Existencia_DosisUnitaria, 

			IsNull(FP.CostoPromedio, 0) As CostoPromedio, IsNull(FP.UltimoCosto, 0) As UltimoCosto, 
			PE.EsDePickeo , U.Status			
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( U.IdProducto = P.IdProducto And U.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_Farmacias F (NoLock) On ( U.IdEstado = F.IdEstado and U.IdFarmacia = F.IdFarmacia )	
	Inner Join CatEmpresas E (NoLock) On ( U.IdEmpresa = E.IdEmpresa ) 
	Inner Join CatFarmacias_SubFarmacias FS(NoLock) On ( U.IdEstado = FS.IdEstado And U.IdFarmacia = FS.IdFarmacia And U.IdSubFarmacia = FS.IdSubFarmacia ) 
	Inner Join vw_Pasillos_Estantes_Entrepaños PE(NoLock) On ( U.IdEmpresa = PE.IdEmpresa And U.IdEstado = PE.IdEstado And U.IdFarmacia = PE.IdFarmacia 
		And U.IdPasillo = PE.IdPasillo And U.IdEstante = PE.IdEstante And U.IdEntrepaño = PE.IdEntrepaño )
	Inner Join FarmaciaProductos_CodigoEAN_Lotes FP (NoLock) 
		On ( U.IdEmpresa = FP.IdEmpresa and U.IdEstado = FP.IdEstado and U.IdFarmacia = FP.IdFarmacia and U.IdSubFarmacia = FP.IdSubFarmacia 
		and U.IdProducto = FP.IdProducto and U.CodigoEAN = FP.CodigoEAN and U.ClaveLote = FP.ClaveLote  )  
		
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ExistenciaPorCodigoEAN_Lotes' and xType = 'V' ) 
   Drop View vw_ExistenciaPorCodigoEAN_Lotes   
Go--#SQL 

Create View vw_ExistenciaPorCodigoEAN_Lotes 
With Encryption 
As 

	Select 
		IsNull(F.IdEmpresa, '') as IdEmpresa,  
		IsNull(E.Nombre, '') as Empresa,  		
		IsNull(F.IdEstado, '') as IdEstado, 
		IsNull(Fa.Estado, '') as Estado, 		
		IsNull(F.IdFarmacia, '') as IdFarmacia, 
		IsNull(Fa.Farmacia, '') as Farmacia, 		

		IsNull(F.IdSubFarmacia, '') as IdSubFarmacia,	
		IsNull(FS.Descripcion, '') as SubFarmacia, 

		S.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal, S.DescripcionSal as DescripcionClave, 
		S.TipoDeClave, S.TipoDeClaveDescripcion, 
		S.EsControlado, S.EsAntibiotico, vwP.EsRefrigerado, 
		
		vwP.IdPresentacion_ClaveSSA, vwP.Presentacion_ClaveSSA, 
		IsNull(vwP.IdProducto, '') as IdProducto, IsNull(F.CodigoEAN, '') as CodigoEAN, 
		vwP.Descripcion as DescripcionProducto,  
		vwP.IdPresentacion, vwP.Presentacion, vwP.ContenidoPaquete, vwP.IdLaboratorio, vwP.Laboratorio, 
		vwP.TasaIva, 
		IsNull(F.ClaveLote, '') as ClaveLote, 
		IsNull(F.EsConsignacion, 0) as EsConsignacion, 		
		-- (case when D.EsConsignacion = 1 then 1 else 0 end) as EsConsignacion, 
		
		---- Jesús Díaz  2K121008.1415 
		(Case When dbo.fg_INV_GetStatusProducto(IsNull(F.IdEmpresa, ''), IsNull(F.IdEstado, ''), IsNull(F.IdFarmacia, ''), vwP.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else sum(IsNull(F.Existencia, 0)) End) as ExistenciaAux, 					
		(Case When dbo.fg_INV_GetStatusProducto(IsNull(F.IdEmpresa, ''), IsNull(F.IdEstado, ''), IsNull(F.IdFarmacia, ''), vwP.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else IsNull(F.ExistenciaEnTransito, 0) End) as ExistenciaEnTransito,
		(Case When dbo.fg_INV_GetStatusProducto(IsNull(F.IdEmpresa, ''), IsNull(F.IdEstado, ''), IsNull(F.IdFarmacia, ''), vwP.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else IsNull(F.ExistenciaSurtidos, 0) End) as ExistenciaSurtidos, 
		(Case When dbo.fg_INV_GetStatusProducto(IsNull(F.IdEmpresa, ''), IsNull(F.IdEstado, ''), IsNull(F.IdFarmacia, ''), vwP.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else sum(IsNull((F.Existencia - (F.ExistenciaEnTransito + F.ExistenciaSurtidos)), 0)) End) as Existencia,

		sum(IsNull((F.Existencia - (F.ExistenciaEnTransito - F.ExistenciaSurtidos)), 0) * (case when FA.EsUnidosis = 0 Then vwP.ContenidoPiezasUnitario else 1 end) ) as Existencia_DosisUnitaria, 

		vwP.ContenidoPiezasUnitario, vwP.ContenidoCorrugado, vwP.Cajas_Cama, vwP.Cajas_Tarima, 
			
		IsNull(F.CostoPromedio, 0) As CostoPromedio, IsNull(F.UltimoCosto, 0) As UltimoCosto,				   
		IsNull(F.FechaCaducidad, cast('2000-01-01' as datetime)) as FechaCaducidad, 
		datediff(mm, getdate(), IsNull(F.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar, 		
		IsNull(F.FechaRegistro, cast('2000-01-01' as datetime)) as FechaRegistro		
	From vw_ClavesSSA_Sales S (NoLock) 
	Inner Join vw_Productos_CodigoEAN vwP (NoLock) On ( S.IdClaveSSA_Sal = vwP.IdClaveSSA_Sal ) 
	Left Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) On ( vwP.IdProducto = F.IdProducto and vwP.CodigoEAN = F.CodigoEAN )  
	Left Join vw_Farmacias Fa (NoLock) On ( F.IdEstado = Fa.IdEstado and Fa.IdFarmacia = F.IdFarmacia )	
	Left Join CatEmpresas E (NoLock) On ( F.IdEmpresa = E.IdEmpresa ) 
	Left Join CatFarmacias_SubFarmacias FS(NoLock) On ( F.IdEstado = FS.IdEstado And F.IdFarmacia = FS.IdFarmacia And F.IdSubFarmacia = FS.IdSubFarmacia ) 

	Group by 
		S.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal,
		S.TipoDeClave, S.TipoDeClaveDescripcion, 
		S.EsControlado, S.EsAntibiotico, vwP.EsRefrigerado, 
		vwP.IdPresentacion_ClaveSSA, vwP.Presentacion_ClaveSSA, 
		vwP.IdProducto, 
		vwP.IdPresentacion, vwP.Presentacion, vwP.ContenidoPaquete, vwP.IdLaboratorio, vwP.Laboratorio,
		vwP.TasaIva, 
		IsNull(F.CodigoEAN, ''), IsNull(F.ClaveLote, ''), 
		IsNull(F.EsConsignacion, 0), F.ExistenciaSurtidos, 
		F.ExistenciaEnTransito, CostoPromedio, UltimoCosto,
		IsNull(F.FechaCaducidad, cast('2000-01-01' as datetime)), IsNull(F.FechaRegistro, cast('2000-01-01' as datetime)), 
		vwP.Descripcion, IsNull(F.IdEmpresa, ''), IsNull(E.Nombre, ''), 
		IsNull(F.IdEstado, ''), IsNull(Fa.Estado, ''), IsNull(F.IdFarmacia, ''), IsNull(Fa.Farmacia, ''), 
		IsNull(F.IdSubFarmacia, ''), IsNull(FS.Descripcion, ''), 
		vwP.ContenidoPiezasUnitario, vwP.ContenidoCorrugado, vwP.Cajas_Cama, vwP.Cajas_Tarima  

Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ExistenciaPorCodigoEAN' and xType = 'V' ) 
   Drop View vw_ExistenciaPorCodigoEAN  
Go--#SQL  

Create View vw_ExistenciaPorCodigoEAN 
With Encryption 
As 

	Select 
		IsNull(v.IdEmpresa, '') as IdEmpresa, 
		IsNull(v.Empresa, '') as Empresa, 		
		IsNull(v.IdEstado, '') as IdEstado, 
		IsNull(v.Estado, '') as Estado, 		
		IsNull(v.IdFarmacia, '') as IdFarmacia, 
		IsNull(v.Farmacia, '') as Farmacia, 
		v.IdClaveSSA_Sal, 
		v.ClaveSSA_Base, 
		v.ClaveSSA, v.DescripcionSal, 
		v.TipoDeClave, v.TipoDeClaveDescripcion, 
		vwP.EsControlado, vwP.EsAntibiotico, vwP.EsRefrigerado, 
		vwP.IdProducto, IsNull(v.CodigoEAN, '') as CodigoEAN, 
		vwP.Descripcion as DescripcionProducto, 
		vwP.IdPresentacion, vwP.Presentacion, vwP.ContenidoPaquete, 
		vwP.IdLaboratorio, vwP.Laboratorio, 
		vwP.ContenidoPiezasUnitario, vwP.ContenidoCorrugado, vwP.Cajas_Cama, vwP.Cajas_Tarima, 
		sum(IsNull(V.Existencia, 0)) as Existencia, 
		sum(IsNull(V.Existencia_DosisUnitaria, 0)) as Existencia_DosisUnitaria  	
	From vw_Productos_CodigoEAN vwP (NoLock) 
	Left Join vw_ExistenciaPorCodigoEAN_Lotes v (NoLock) On ( vwP.IdProducto = v.IdProducto and vwP.CodigoEAN = V.CodigoEAN  ) 
	Group by 
		vwP.IdProducto, IsNull(v.CodigoEAN, ''), vwP.Descripcion, 
		vwP.EsControlado, vwP.EsAntibiotico, vwP.EsRefrigerado, 
		vwP.IdPresentacion, vwP.Presentacion, vwP.ContenidoPaquete, vwP.IdLaboratorio, vwP.Laboratorio, 
		IsNull(v.IdEmpresa, ''), IsNull(v.Empresa, ''), 
		IsNull(v.IdEstado, ''), IsNull(v.Estado, ''), IsNull(v.IdFarmacia, ''), IsNull(v.Farmacia, ''), 
		v.IdClaveSSA_Sal, v.ClaveSSA_Base, v.ClaveSSA, v.DescripcionSal, 
		v.TipoDeClave, v.TipoDeClaveDescripcion, 
		vwP.ContenidoPiezasUnitario, vwP.ContenidoCorrugado, vwP.Cajas_Cama, vwP.Cajas_Tarima  
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ExistenciaPorProducto' and xType = 'V' ) 
   Drop View vw_ExistenciaPorProducto  
Go--#SQL   

Create View vw_ExistenciaPorProducto 
With Encryption 
As 

	Select 
		IsNull(v.IdEmpresa, '') as IdEmpresa,  
		IsNull(v.Empresa, '') as Empresa,  			
		IsNull(v.IdEstado, '') as IdEstado, 
		IsNull(v.Estado, '') as Estado, 		
		IsNull(v.IdFarmacia, '') as IdFarmacia, 
		IsNull(v.Farmacia, '') as Farmacia, 		
		v.IdClaveSSA_Sal, v.ClaveSSA_Base, v.ClaveSSA, v.DescripcionSal, 
		v.TipoDeClave, v.TipoDeClaveDescripcion,     
		vwP.EsControlado, vwP.EsAntibiotico, vwP.EsRefrigerado, 
		vwP.IdProducto, vwP.Descripcion as DescripcionProducto, 
		vwP.IdPresentacion, vwP.Presentacion, vwP.ContenidoPaquete, 	
		sum(IsNull(V.Existencia, 0)) as Existencia, 
		sum(IsNull(V.Existencia_DosisUnitaria, 0)) as Existencia_DosisUnitaria 
	From vw_Productos vwP (NoLock) 
	Left Join vw_ExistenciaPorCodigoEAN_Lotes v (NoLock) On ( vwP.IdProducto = v.IdProducto ) 
	Group by vwP.IdProducto, vwP.Descripcion, 
		vwP.EsControlado, vwP.EsAntibiotico,  vwP.EsRefrigerado, 
		vwP.IdPresentacion, vwP.Presentacion, vwP.ContenidoPaquete, 
		IsNull(v.IdEmpresa, ''), IsNull(v.Empresa, ''), 
		IsNull(v.IdEstado, ''), IsNull(v.Estado, ''), IsNull(v.IdFarmacia, ''), IsNull(v.Farmacia, ''), 
		v.IdClaveSSA_Sal, v.ClaveSSA_Base, v.ClaveSSA, v.DescripcionSal, 
		v.TipoDeClave, v.TipoDeClaveDescripcion   
Go--#SQL  
 

------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ExistenciaPorSales' and xType = 'V' ) 
   Drop View vw_ExistenciaPorSales 
Go--#SQL   

Create View vw_ExistenciaPorSales 
With Encryption 
As 

	Select 
		IsNull(v.IdEmpresa, '') as IdEmpresa, IsNull(v.Empresa, '') as Empresa, 
		IsNull(v.IdEstado, '') as IdEstado, IsNull(v.Estado, '') as Estado, 
		IsNull(v.IdFarmacia, '') as IdFarmacia, IsNull(v.Farmacia, '') as Farmacia, 		
		S.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA, S.DescripcionSal as DescripcionSal, 
		S.DescripcionClave, 
		S.Presentacion, S.ContenidoPaquete, 
		S.TipoDeClave, S.TipoDeClaveDescripcion, 
		S.EsControlado, S.EsAntibiotico, S.EsRefrigerado, 
		sum(IsNull(V.Existencia, 0)) as Existencia, 
		sum(IsNull(V.Existencia_DosisUnitaria, 0)) as Existencia_DosisUnitaria 
	From vw_ClavesSSA_Sales S (NoLock) 
	Inner Join vw_ExistenciaPorCodigoEAN_Lotes V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) 
	Group by 
		IsNull(v.IdEmpresa, ''), IsNull(v.Empresa, ''),  
		IsNull(v.IdEstado, ''), IsNull(v.Estado, ''), IsNull(v.IdFarmacia, ''), IsNull(v.Farmacia, ''),  	
		S.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA, S.DescripcionSal, 
		S.DescripcionClave, 
		S.EsControlado, S.EsAntibiotico, S.EsRefrigerado,  
		S.Presentacion, S.ContenidoPaquete,  
		S.TipoDeClave, S.TipoDeClaveDescripcion  
		
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ExistenciaPorClaves' and xType = 'V' ) 
   Drop View vw_ExistenciaPorClaves 
Go--#SQL   

Create View vw_ExistenciaPorClaves 
With Encryption 
As

	Select * 
	From vw_ExistenciaPorSales 

Go--#SQL  

