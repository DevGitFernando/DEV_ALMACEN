
	If exists ( select * from tempdb..sysobjects where name like '#tmpVentas%' and xType = 'U' ) 
		Drop Table #tmpVentas 

	Select IdEstado, IdFarmacia, space(200) as Farmacia, 
		space(4) as IdJurisdiccion, space(150) as Jurisdiccion, 
		P.IdProducto, P.CodigoEAN, P.ClaveSSA, P.DescripcionClave, P.Laboratorio, cast(sum(CantidadVendida) as int) as CantidadVendida 
	Into #tmpVentas 	
	From VentasDet D (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.ClaveSSA = '4158' ) 	
	Group by IdEstado, IdFarmacia, P.IdProducto, P.CodigoEAN, P.Laboratorio, P.ClaveSSA, P.DescripcionClave  
		
	
	Update V Set Farmacia = F.Farmacia, IdJurisdiccion = F.IdJurisdiccion, Jurisdiccion = F.Jurisdiccion 
	From #tmpVentas V 
	Inner Join vw_Farmacias F On ( F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia  ) 
		
		
	select * 
	from #tmpVentas 
			