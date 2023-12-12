
/* 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'V' ) 
   Drop View vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones   
Go--#--SQL

Create View vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
With Encryption 
As 

	Select	U.IdEmpresa, E.Nombre as Empresa, U.IdEstado as IdEstado, F.Estado, U.IdFarmacia, F.Farmacia, 
			U.IdSubFarmacia, FS.Descripcion as SubFarmacia, 
			P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, P.DescripcionClave, P.DescripcionCortaClave,
			P.IdPresentacion_ClaveSSA, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA,
			P.IdProducto, U.CodigoEAN, P.CodigoEAN_Interno, P.Descripcion as DescripcionProducto,
			FP.FechaCaducidad, datediff(mm, getdate(), IsNull(FP.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar, FP.FechaRegistro,  
			P.IdPresentacion, P.Presentacion, P.ContenidoPaquete, P.IdLaboratorio, P.Laboratorio, 
			U.ClaveLote, U.EsConsignacion, U.IdPasillo, PE.DescripcionPasillo as Pasillo, U.IdEstante, PE.DescripcionEstante as Estante, 
			U.IdEntrepaño, PE.DescripcionEntrepaño as Entrepaño, 
			IsNull(U.Existencia, 0) as ExistenciaAux,    				
			(Case When dbo.fg_INV_GetStatusProducto(IsNull(U.IdEmpresa, ''), IsNull(U.IdEstado, ''), IsNull(U.IdFarmacia, ''), P.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else IsNull(U.Existencia, 0) End) as Existencia,
			U.Status			
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
Go--#--SQL

*/ 

