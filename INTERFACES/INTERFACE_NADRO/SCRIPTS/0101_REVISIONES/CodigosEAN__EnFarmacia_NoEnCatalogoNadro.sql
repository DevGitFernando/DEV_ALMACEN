
/* 

	select * 
	into #tmp___vw_ExistenciaPorCodigoEAN 
	from vw_ExistenciaPorCodigoEAN_Lotes -- vw_ExistenciaPorCodigoEAN 
--	where IdSubFarmacia = 5 
	
	
	select * 
	into #tmp___vw_ExistenciaPorCodigoEAN_Lotes  
	from vw_ExistenciaPorCodigoEAN_Lotes  

	
*/ 


/* 

select top 1 * from #tmp___vw_ExistenciaPorCodigoEAN	

drop table #tmp___vw_ExistenciaPorCodigoEAN  

drop table #tmp___ProductosFarmacias 


	select IdFarmacia, Farmacia, IdProducto, CodigoEAN, DescripcionProducto, Laboratorio, 
	replace(ClaveSSA_Base, '.', '') as ClaveSSA_Base, ClaveSSA, DescripcionSal, max(ContenidoPaquete)as ContenidoPaquete, 
	IdSubFarmacia, 
	ceiling((sum(Existencia) / max(ContenidoPaquete * 1.0)))  as Existencia   
	
	into #tmp___ProductosFarmacias 
	from #tmp___vw_ExistenciaPorCodigoEAN F (nolock) 
	where IdEstado = 16  and IdFarmacia <= 100 
	and not Exists 
	( 
		select * 
		from INT_ND_Productos_CargaMasiva N (nolock) 
		where -- RIGHT('0000000000000000000000000000000' + F.CodigoEAN, 20) = RIGHT('0000000000000000000000000000000' + N.CodigoEAN, 20)  
			F.CodigoEAN = N.CodigoEAN 
	) 
	group by IdFarmacia, Farmacia, IdProducto, DescripcionProducto, CodigoEAN, Laboratorio, IdSubFarmacia, 
		replace(ClaveSSA_Base, '.', ''), ClaveSSA, DescripcionSal  


	Select IdFarmacia, Farmacia, COUNT(*) 
	From 
	( 
		select IdFarmacia, Farmacia, IdProducto, CodigoEAN, ClaveSSA, DescripcionSal  
		from #tmp___vw_ExistenciaPorCodigoEAN F (nolock) 
		where IdEstado = 16  and IdFarmacia <= 100 
		and not Exists 
		( 
			select * 
			from INT_ND_Productos_CargaMasiva N (nolock) 
			where -- RIGHT('0000000000000000000000000000000' + F.CodigoEAN, 20) = RIGHT('0000000000000000000000000000000' + N.CodigoEAN, 20)  
				F.CodigoEAN = N.CodigoEAN 
		) 
		group by IdFarmacia, Farmacia, IdProducto, CodigoEAN, ClaveSSA, DescripcionSal  
	) X 	
	Group by IdFarmacia, Farmacia 
	order by 3 desc 
	
--	select top 1 * from #tmp___ProductosFarmacias 

	
	select IdFarmacia, Farmacia, IdProducto, CodigoEAN, ClaveSSA, DescripcionSal   
	from #tmp___ProductosFarmacias 
	--where Idfarmacia in ( 13, 15, 11, 14, 16 ) 
	group by IdFarmacia, Farmacia, IdProducto, CodigoEAN, ClaveSSA, DescripcionSal   
	
*/ 
		
		
		
	
/* 	

	select IdProducto, CodigoEAN, ClaveSSA, DescripcionSal  
	from #tmp___vw_ExistenciaPorCodigoEAN F (nolock) 
	where IdEstado = 16  and IdFarmacia <= 100 
	and not Exists 
	( 
		select * 
		from INT_ND_Productos_CargaMasiva N (nolock) 
		where -- RIGHT('0000000000000000000000000000000' + F.CodigoEAN, 20) = RIGHT('0000000000000000000000000000000' + N.CodigoEAN, 20)  
			F.CodigoEAN = N.CodigoEAN 
	) 
	group by IdProducto, CodigoEAN, ClaveSSA, DescripcionSal  
	order by CodigoEAN 
	
*/ 


	------select -- IdFarmacia, Farmacia, 
	------	IdProducto, CodigoEAN, DescripcionProducto, Laboratorio, 
	------	-- (case when IsNumeric(replace(ClaveSSA_Base, '.', '')) = 1 then cast(replace(ClaveSSA_Base, '.', '') as int) else ClaveSSA_Base end) as ClaveSSA_ND, 
	------	ClaveSSA, DescripcionSal   
	------from #tmp___ProductosFarmacias 
	------where 1= 1 --and Idfarmacia in ( 16 ) 
	------	 --and Descripcionsal like '%venda%enyesa%'  
	------	 and ClaveSSA like '%%'
	------	 and CodigoEAN like '%%' 
	------	 and DescripcionProducto like '%%'
	------group by IdProducto, CodigoEAN, DescripcionProducto, Laboratorio, 
	------	-- (case when IsNumeric(replace(ClaveSSA_Base, '.', '')) = 1 then cast(replace(ClaveSSA_Base, '.', '') as int) else ClaveSSA_Base end), 
	------	ClaveSSA, DescripcionSal  
	------order by CodigoEAN 
	

/* 

	select * 
	From #tmp___vw_ExistenciaPorCodigoEAN_Lotes 
	where IdProducto = 10526 
	
*/ 



	select 
		-- IdFarmacia, Farmacia, 
		IdSubFarmacia, 
		IdProducto, CodigoEAN, DescripcionProducto, Laboratorio, 
		ClaveSSA_Base as ClaveSSA_ND, 
		ClaveSSA, DescripcionSal, ContenidoPaquete, sum(Existencia) as Existencia    
	from #tmp___ProductosFarmacias 
	where 1 = 1 --and Idfarmacia in ( 16 ) 
		 --and Descripcionsal like '%venda%enyesa%'  
		 --and IdProducto = 15217 
		 and ClaveSSA like '%%'
		 and CodigoEAN like '%%' 
		 and DescripcionProducto like '%%' 
		 and IdSubFarmacia like '%2%'	 
	group by 
		-- IdFarmacia, Farmacia, 
		IdSubFarmacia, 
		IdProducto, CodigoEAN, DescripcionProducto, Laboratorio, 
		ClaveSSA_Base, ClaveSSA, DescripcionSal, ContenidoPaquete  
	order by IdProducto, IdSubFarmacia 
	
	
	
	
	