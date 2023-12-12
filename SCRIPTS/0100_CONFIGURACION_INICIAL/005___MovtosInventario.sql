
------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpSubFarmacias%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpSubFarmacias

	select X.IdEstado, X.IdFarmacia, P.IdTipoMovto_Inv, 0 as Consecutivo, 'A' as Status, 0 as Actualizado 
	Into #tmpSubFarmacias 
	from Movtos_Inv_Tipos  P, 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 16 and IdFarmacia >= 11
	) as X 	-- On ( x.IdEstado = 16 )
	where X.IdEstado = 16  --and X.IdFarmacia = 44 
	
---		select top 1 * from Movtos_Inv_Tipos_Farmacia 	
	
	
--	Insert Into Movtos_Inv_Tipos_Farmacia 
	Select IdEstado, IdFarmacia, IdTipoMovto_Inv, Consecutivo, Status, Actualizado 
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From Movtos_Inv_Tipos_Farmacia C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdTipoMovto_Inv = C.IdTipoMovto_Inv 
		) 
		
		