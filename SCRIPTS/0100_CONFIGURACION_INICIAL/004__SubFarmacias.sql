

------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpSubFarmacias%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpSubFarmacias

	select P.IdEstado, X.IdFarmacia, P.IdSubFarmacia, P.Descripcion, 'A' as Status, 0 as Actualizado 
	Into #tmpSubFarmacias 
	from CatFarmacias_SubFarmacias P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia 
		From CatFarmacias F 
		Where IdEstado = 16 and IdFarmacia >= 11
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 16 and P.IdFarmacia = 11  
	
	
	Insert Into CatFarmacias_SubFarmacias 
	Select * 
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From CatFarmacias_SubFarmacias C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdSubFarmacia = C.IdSubFarmacia 
		) 


	----select * 
	----from CatFarmacias_SubFarmacias 
	----where IdEstado = 11 
	
	