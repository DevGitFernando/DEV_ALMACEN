	
	If exists ( select * from tempdb..sysobjects where name like '#tmp_Concentrado_Nivel_Lote%' ) drop table tempdb..#tmp_Concentrado_Nivel_Lote 
	
	Select 
		IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible, 
		0 as Existencia_U, 0 as ExistenciaEnTransito_U, 0 as ExistenciaDisponible_U  						 
	Into #tmp_Concentrado_Nivel_Lote  
	From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
	Where IdEmpresa = 1 and IdEstado = 21 and IdFarmacia = 3182 --and IdProducto = 00014233 	
	
	
	
	If exists ( select * from tempdb..sysobjects where name like '#tmp_Concentrado_U%' ) drop table tempdb..#tmp_Concentrado_U 
	
	Select 
		IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, 
		IdPasillo, IdEstante, IdEntrepaño, 
		cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible 
	Into #tmp_Concentrado_U  
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) 
	Where IdEmpresa = 1 and IdEstado = 21 and IdFarmacia = 3182 --and IdProducto = 00014233 	
	
	
-------------------------------------------------------------------------------------------------------------------------------------------------  		
					
	Update C Set 
		Existencia_U = 
		IsNull((
			select sum(Existencia) From #tmp_Concentrado_U D 
			Where D.IdSubFarmacia = C.IdSubFarmacia and D.IdProducto = C.IdProducto and D.CodigoEAN = C.CodigoEAN and D.ClaveLote = C.ClaveLote 
		), 0),   
		ExistenciaEnTransito_U = 
		IsNull((
			select sum(ExistenciaEnTransito) From #tmp_Concentrado_U D 
			Where D.IdSubFarmacia = C.IdSubFarmacia and D.IdProducto = C.IdProducto and D.CodigoEAN = C.CodigoEAN and D.ClaveLote = C.ClaveLote 
		), 0),   
		ExistenciaDisponible_U = -- (select sum(ExistenciaDisponible) From #tmp_Concentrado_U D Where D.IdProducto = C.IdProducto ) 		
		IsNull((
			select sum(ExistenciaDisponible) From #tmp_Concentrado_U D 
			Where D.IdSubFarmacia = C.IdSubFarmacia and D.IdProducto = C.IdProducto and D.CodigoEAN = C.CodigoEAN and D.ClaveLote = C.ClaveLote 
		), 0)		
	From #tmp_Concentrado_Nivel_Lote C 	
	
	
	
	Select * 
	From #tmp_Concentrado_Nivel_Lote 
	Where (ExistenciaDisponible <> ExistenciaDisponible_U) 		 	
		-- or ExistenciaDisponible <> 0 
		-- or IdProducto = 00022052 


/* 
		select * 
		from #tmp_Concentrado_Nivel_Lote 
		where Existencia > 0 

							
		select * 
		from #tmp_Concentrado_U 
		where Existencia > 0 

*/ 
