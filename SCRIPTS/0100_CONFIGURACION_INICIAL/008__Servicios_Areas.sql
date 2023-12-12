


------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpServiciosAreas%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpServiciosAreas

	select P.IdEstado, X.IdFarmacia, P.IdServicio, P.IdArea, 'A' as Status, 0 as Actualizado 
	Into #tmpServiciosAreas 
	from CatServicios_Areas_Farmacias P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 7 and IdFarmacia >= 101
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 7 and P.IdFarmacia = 101 
	
---		select top 1 * from CatServicios_Areas_Farmacias 	
	
	
	Insert Into CatServicios_Areas_Farmacias 
	Select * 
	from #tmpServiciosAreas P 
	Where Not Exists 
		( 
			Select * From CatServicios_Areas_Farmacias C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdServicio = C.IdServicio and P.IdArea = C.IdArea
		
		) 
		
		