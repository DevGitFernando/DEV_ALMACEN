


------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpProgramasAtencion%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpProgramasAtencion

	select P.IdEstado, X.IdFarmacia, P.IdCliente, P.IdSubCliente, P.IdPrograma, P.IdSubPrograma, 'A' as Status, 0 as Actualizado 
	Into #tmpProgramasAtencion 
	from CFG_EstadosFarmaciasProgramasSubProgramas P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia 
		From CatFarmacias F 
		Where IdEstado = 7 and IdFarmacia >= 101
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 7 and P.IdFarmacia = 101 
	
---		select top 1 * from CFG_EstadosFarmaciasProgramasSubProgramas 	
	
	
	Insert Into CFG_EstadosFarmaciasProgramasSubProgramas 
	Select * 
	from #tmpProgramasAtencion P 
	Where Not Exists 
		( 
			Select * From CFG_EstadosFarmaciasProgramasSubProgramas C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia 
				  and P.IdCliente = C.IdCliente and P.IdSubCliente = C.IdSubCliente 
				  and P.IdPrograma = C.IdPrograma and P.IdSubPrograma = C.IdSubPrograma 
		
		) 
		
		