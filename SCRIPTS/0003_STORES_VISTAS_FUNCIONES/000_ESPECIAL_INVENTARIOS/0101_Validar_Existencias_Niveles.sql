
	If exists ( select * from tempdb..sysobjects where name like '#tmp_Concentrado%' ) drop table tempdb..#tmp_Concentrado 

	Select 
		IdProducto, cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible, 
		0 as Existencia_EAN, 0 as ExistenciaEnTransito_EAN, 0 as ExistenciaDisponible_EAN,  
		0 as Existencia_Lote, 0 as ExistenciaEnTransito_Lote, 0 as ExistenciaDisponible_Lote,  
		0 as Existencia_U, 0 as ExistenciaEnTransito_U, 0 as ExistenciaDisponible_U, 
		0 as Error   				
	Into #tmp_Concentrado 
	From FarmaciaProductos (NoLock) 
	Where IdEmpresa = 1 and IdEstado = 21 and IdFarmacia = 3182  -- and IdProducto = 242 


	If exists ( select * from tempdb..sysobjects where name like '#tmp_Concentrado_EAN%' ) drop table tempdb..#tmp_Concentrado_EAN 
	
	Select 
		IdProducto, CodigoEAN, cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible 
	Into #tmp_Concentrado_EAN  
	From FarmaciaProductos_CodigoEAN (NoLock) 
	Where IdEmpresa = 1 and IdEstado = 21 and IdFarmacia = 3182  -- and IdProducto = 242 
	
	
	
	If exists ( select * from tempdb..sysobjects where name like '#tmp_Concentrado_Lote%' ) drop table tempdb..#tmp_Concentrado_Lote 
	
	Select 
		IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible 
	Into #tmp_Concentrado_Lote  
	From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
	Where IdEmpresa = 1 and IdEstado = 21 and IdFarmacia = 3182  -- and IdProducto = 242 	
	
	
	
	If exists ( select * from tempdb..sysobjects where name like '#tmp_Concentrado_U%' ) drop table tempdb..#tmp_Concentrado_U 
	
	Select 
		IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, 
		IdPasillo, IdEstante, IdEntrepaño, 
		cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible 
	Into #tmp_Concentrado_U  
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) 
	Where IdEmpresa = 1 and IdEstado = 21 and IdFarmacia = 3182  -- and IdProducto = 242 	
	
	
-------------------------------------------------------------------------------------------------------------------------------------------------  		
	Update C Set 
		Existencia_EAN = IsNull((select sum(Existencia) From #tmp_Concentrado_EAN D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaEnTransito_EAN = IsNull((select sum(ExistenciaEnTransito) From #tmp_Concentrado_EAN D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaDisponible_EAN = IsNull((select sum(ExistenciaDisponible) From #tmp_Concentrado_EAN D Where D.IdProducto = C.IdProducto ), 0) 		
	From #tmp_Concentrado C 
	
	Update C Set 
		Existencia_Lote = IsNull((select sum(Existencia) From #tmp_Concentrado_Lote D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaEnTransito_Lote = IsNull((select sum(ExistenciaEnTransito) From #tmp_Concentrado_Lote D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaDisponible_Lote = IsNull((select sum(ExistenciaDisponible) From #tmp_Concentrado_Lote D Where D.IdProducto = C.IdProducto ), 0) 		
	From #tmp_Concentrado C 
						
	Update C Set 
		Existencia_U = IsNull((select sum(Existencia) From #tmp_Concentrado_U D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaEnTransito_U = IsNull((select sum(ExistenciaEnTransito) From #tmp_Concentrado_U D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaDisponible_U = IsNull((select sum(ExistenciaDisponible) From #tmp_Concentrado_U D Where D.IdProducto = C.IdProducto ), 0) 		
	From #tmp_Concentrado C 	
	
	
	
	Select * 
	From #tmp_Concentrado 
	Where 
		( 
		(ExistenciaDisponible <> ExistenciaDisponible_EAN) or 
		(ExistenciaDisponible <> ExistenciaDisponible_Lote) or (ExistenciaDisponible <> ExistenciaDisponible_U) 		 	
		) -- and Existencia > 0 


	----Select * 
	----From #tmp_Concentrado 
	----Where 
	----	( 
	----	(ExistenciaDisponible < 0) or 
	----	(ExistenciaDisponible_EAN < 0) or (ExistenciaDisponible_Lote < 0) 		 	
	----	) 		
	
/* 	


Begin tran 	
	
--	rollback tran 
	
--	commit tran 	
	
	Update C set Existencia = 0 
	From FarmaciaProductos C (NoLock) 	
	Inner Join #tmp_Concentrado D On ( D.IdProducto = C.IdProducto ) 
	Where 
		( 
		(ExistenciaDisponible <> ExistenciaDisponible_EAN) or 
		(ExistenciaDisponible <> ExistenciaDisponible_Lote) or (ExistenciaDisponible <> ExistenciaDisponible_U) 		 	
		) and D.Existencia = 0 
	
	
	Update C set Existencia = 0 
	From FarmaciaProductos_CodigoEAN C (NoLock) 	
	Inner Join #tmp_Concentrado D On ( D.IdProducto = C.IdProducto ) 
	Where 
		( 
		(ExistenciaDisponible <> ExistenciaDisponible_EAN) or 
		(ExistenciaDisponible <> ExistenciaDisponible_Lote) or (ExistenciaDisponible <> ExistenciaDisponible_U) 		 	
		) and D.Existencia = 0 
	
	
	Update C set Existencia = 0 
	From FarmaciaProductos_CodigoEAN_Lotes C (NoLock) 	
	Inner Join #tmp_Concentrado D On ( D.IdProducto = C.IdProducto ) 
	Where 
		( 
		(ExistenciaDisponible <> ExistenciaDisponible_EAN) or 
		(ExistenciaDisponible <> ExistenciaDisponible_Lote) or (ExistenciaDisponible <> ExistenciaDisponible_U) 		 	
		) and D.Existencia = 0 

	
	Update C set Existencia = 0 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones C (NoLock) 	
	Inner Join #tmp_Concentrado D On ( D.IdProducto = C.IdProducto ) 			
	Where 
		( 
		(ExistenciaDisponible <> ExistenciaDisponible_EAN) or 
		(ExistenciaDisponible <> ExistenciaDisponible_Lote) or (ExistenciaDisponible <> ExistenciaDisponible_U) 		 	
		) and D.Existencia = 0 

		
*/ 		
		
		