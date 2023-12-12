


------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpEdoFarmaciaCTE%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpEdoFarmaciaCTE

	select P.IdEstado, X.IdFarmacia, P.IdCliente, 'A' as Status, 0 as Actualizado 
	Into #tmpEdoFarmaciaCTE 
	from CFG_EstadosFarmaciasClientes P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 7 and IdFarmacia >= 101
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 7 and P.IdFarmacia = 101 
	
---		select top 1 * from CFG_EstadosFarmaciasClientes 	
	
	
	Insert Into CFG_EstadosFarmaciasClientes 
	Select * 
	from #tmpEdoFarmaciaCTE P 
	Where Not Exists 
		( 
			Select * From CFG_EstadosFarmaciasClientes C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdCliente = C.IdCliente 
		
		) 
		
		
		
------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpEdoFarmaciaSCTE%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpEdoFarmaciaSCTE

	select P.IdEstado, X.IdFarmacia, P.IdCliente, P.IdSubCliente, 'A' as Status, 0 as Actualizado 
	Into #tmpEdoFarmaciaSCTE 
	from CFG_EstadosFarmaciasClientesSubClientes P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 7 and IdFarmacia >= 101
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 7 and P.IdFarmacia = 101 and IdCliente = 2 and IdSubCliente = 8 
	
---		select top 1 * from CFG_EstadosFarmaciasClientesSubClientes 	
--	delete from 	CFG_EstadosFarmaciasClientesSubClientes where IdEstado = 11 
	
	
	Insert Into CFG_EstadosFarmaciasClientesSubClientes 
	Select * 
	from #tmpEdoFarmaciaSCTE P 
	Where Not Exists 
		( 
			Select * From CFG_EstadosFarmaciasClientesSubClientes C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdCliente = C.IdCliente 
		
		) 		
		
		
		