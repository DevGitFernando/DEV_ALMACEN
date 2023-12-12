------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INV_GenerarPlantilla_Inventario' and xType = 'P' )  
   Drop Proc spp_INV_GenerarPlantilla_Inventario  
Go--#SQL 

Create Proc spp_INV_GenerarPlantilla_Inventario 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', @IdFarmacia varchar(4) = '0003', @ManejaUbicaciones int = 1   
) 	
With Encryption 
As 
Begin 
Set NoCount On 

	
	Select Top 0 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdSubFarmacia, SubFarmacia, IdPasillo, IdEstante, IdEntrepaño, 
		 TipoDeClaveDescripcion, ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete,  V.CodigoEAN, 
		(Case When dbo.fg_ObtenerCostoPromedio(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdProducto) < 1 Then 1 
			Else 
				dbo.fg_ObtenerCostoPromedio(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdProducto) 
		End) As Costo,  
		V.ClaveLote, Convert(Varchar(10), V.FechaCaducidad, 120) As Caducidad, 0 As Cantidad, 
		 (case when V.EsControlado = 1 Then 'SI' else 'NO' end) as EsControlado, 
		 (case when EsAntibiotico = 1 Then 'SI' else 'NO' end) as EsAntibiotico,  
		 (case when EsRefrigerado = 1 Then 'SI' else 'NO' end) as EsRefrigerado  
	Into #tmpDetallado 
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones V (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes  U (NoLock) 
		On (V.IdEmpresa = U.IdEmpresa And V.IdEstado = U.IdEstado And V.IdFarmacia = U.IdFarmacia And 
   		V.CodigoEAN = U.CodigoEAN And V.ClaveLote = U.ClaveLote And V.IdSubFarmacia = U.IdSubFarmacia) 
	Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
	Order by DescripcionSal 



	If @ManejaUbicaciones = 1 
		Begin 
			Insert Into #tmpDetallado  
			Select   
				V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdSubFarmacia, SubFarmacia, IdPasillo, IdEstante, IdEntrepaño, 
				 TipoDeClaveDescripcion, ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete,  V.CodigoEAN, 
				(Case When dbo.fg_ObtenerCostoPromedio(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdProducto) < 1 Then 1 
					Else 
						dbo.fg_ObtenerCostoPromedio(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdProducto) 
				End) As Costo,  
				V.ClaveLote, Convert(Varchar(10), V.FechaCaducidad, 120) As Caducidad, 0 As Cantidad, 
				 (case when V.EsControlado = 1 Then 'SI' else 'NO' end) as EsControlado, 
				 (case when EsAntibiotico = 1 Then 'SI' else 'NO' end) as EsAntibiotico,  
				 (case when EsRefrigerado = 1 Then 'SI' else 'NO' end) as EsRefrigerado  
			From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones V (NoLock) 
			Inner Join FarmaciaProductos_CodigoEAN_Lotes  U (NoLock) 
				On (V.IdEmpresa = U.IdEmpresa And V.IdEstado = U.IdEstado And V.IdFarmacia = U.IdFarmacia And 
   				V.CodigoEAN = U.CodigoEAN And V.ClaveLote = U.ClaveLote And V.IdSubFarmacia = U.IdSubFarmacia) 
			Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
			Order by DescripcionSal 
		End 
	Else 
		Begin 
			Insert Into #tmpDetallado 
			Select 
				IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, SubFarmacia, '' as IdPasillo, '' as IdEstante, '' as IdEntrepaño, 
				 TipoDeClaveDescripcion, ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete,  V.CodigoEAN, 
				(Case When dbo.fg_ObtenerCostoPromedio(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdProducto) < 1 Then 1 
					Else 
						dbo.fg_ObtenerCostoPromedio(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdProducto) 
				End) As Costo,  
				V.ClaveLote, Convert(Varchar(10), V.FechaCaducidad, 120) As Caducidad, 0 As Cantidad, 
				 (case when V.EsControlado = 1 Then 'SI' else 'NO' end) as EsControlado, 
				 (case when EsAntibiotico = 1 Then 'SI' else 'NO' end) as EsAntibiotico,  
				 (case when EsRefrigerado = 1 Then 'SI' else 'NO' end) as EsRefrigerado  
			From vw_ExistenciaPorCodigoEAN_Lotes V (NoLock) 
			Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
			Order by DescripcionSal 
	End 	




--------------------------------------- SALIDA FINAL 	

	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, SubFarmacia, IdPasillo, IdEstante, IdEntrepaño, 
		'Tipo de clave' = TipoDeClaveDescripcion, 
		EsControlado, EsAntibiotico, EsRefrigerado,  
		ClaveSSA, 'Descripción Clave SSA' = DescripcionClave, 
		Presentacion, ContenidoPaquete, 
		CodigoEAN, Costo, ClaveLote, Caducidad, Cantidad  
	From #tmpDetallado 
	Order By ClaveSSA, CodigoEAN  

--------------------------------------- SALIDA FINAL 	

	

End    
Go--#SQL


