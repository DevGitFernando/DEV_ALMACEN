

------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpEmpFarmacias%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpEmpFarmacias

	select P.IdEmpresa, P.IdEstado, X.IdFarmacia, 'A' as Status, 0 as Actualizado 
	Into #tmpEmpFarmacias 
	from CFG_EmpresasFarmacias P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 16 and IdFarmacia >= 11
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 16 and P.IdFarmacia = 11  

	
---		select top 1 * from CFG_EmpresasFarmacias 	
	
---		select top 1 * from #tmpEmpFarmacias  

	
	
--	Insert Into CFG_EmpresasFarmacias 
	Select * 
	from #tmpEmpFarmacias P 
	Where Not Exists 
		( 
			Select * From CFG_EmpresasFarmacias C (NoLock) 
			Where P.IdEmpresa = C.IdEmpresa and  P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia  
		) 
		
		