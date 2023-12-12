

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALM_AgregarExistenciaClaveDistribucion' and xType = 'P' ) 
   Drop Proc spp_ALM_AgregarExistenciaClaveDistribucion 
Go--#SQL 

--		Exec spp_ALM_AgregarExistenciaClaveDistribucion '001', '11', '0003', '1706'

Create Proc spp_ALM_AgregarExistenciaClaveDistribucion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', @ClaveSSA varchar(30) = '1706'
) 
With Encryption 
As 
Begin 
Set NoCount On 
	

-------------------------- Obtener informacion DE LA EXISTENCIA DE LA CLAVESSA para la distribucion de pedidos 	
	Select getdate() as FechaGeneracion, U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, 
		U.ClaveSSA, U.DescripcionClave, U.Presentacion, U.IdProducto, U.CodigoEAN, U.ClaveLote, 
		U.FechaCaducidad, U.MesesParaCaducar AS MesesCaducar, 
		(case when U.EsConsignacion = 1 then 1 else 2 end) as EsConsignacion, 
		(case when U.EsDePickeo = 1 then 1 else 2 end) as EsDePickeo, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, U.ExistenciaAux AS Existencia, U.ExistenciaAux AS Existencia_Aux   
	Into #tmpDistribucion_Lotes_Ubicaciones 	
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
	Where U.IdEmpresa = @IdEmpresa and U.IdEstado = @IdEstado and U.IdFarmacia = @IdFarmacia AND U.ClaveSSA = @ClaveSSA
	Order By U.EsConsignacion Desc, U.EsDePickeo Desc
		  

	Update F Set Existencia = T.Existencia, Existencia_Aux = T.Existencia_Aux
	From FarmaciaProductos_ALM_Distribucion F
	Inner Join #tmpDistribucion_Lotes_Ubicaciones T 
						On ( T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado AND T.IdFarmacia = F.IdFarmacia
						AND T.ClaveSSA = F.ClaveSSA AND T.IdProducto = F.IdProducto AND T.CodigoEAN = F.CodigoEAN
						AND T.ClaveLote = F.ClaveLote AND T.IdPasillo = F.IdPasillo AND T.IdEstante = F.IdEstante
						AND T.IdEntrepaño = F.IdEntrepaño AND T.EsConsignacion = F.EsConsignacion 
						AND T.EsDePickeo = F.EsDePickeo )
						
						
						
	Insert Into FarmaciaProductos_ALM_Distribucion
	Select F.FechaGeneracion, F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, 
		F.ClaveSSA, F.DescripcionClave, F.Presentacion, F.IdProducto, F.CodigoEAN, F.ClaveLote, 
		F.FechaCaducidad, F.MesesCaducar, F.EsConsignacion, F.EsDePickeo, 
		F.IdPasillo, F.IdEstante, F.IdEntrepaño, F.Existencia, F.Existencia_Aux
	From #tmpDistribucion_Lotes_Ubicaciones F
	Where Not Exists ( Select * From FarmaciaProductos_ALM_Distribucion T 
						Where T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado AND T.IdFarmacia = F.IdFarmacia
						AND T.ClaveSSA = F.ClaveSSA AND T.IdProducto = F.IdProducto AND T.CodigoEAN = F.CodigoEAN
						AND T.ClaveLote = F.ClaveLote AND T.IdPasillo = F.IdPasillo AND T.IdEstante = F.IdEstante
						AND T.IdEntrepaño = F.IdEntrepaño AND T.EsConsignacion = F.EsConsignacion 
						AND T.EsDePickeo = F.EsDePickeo ) 
	
	
End 
Go--#SQL 
	

