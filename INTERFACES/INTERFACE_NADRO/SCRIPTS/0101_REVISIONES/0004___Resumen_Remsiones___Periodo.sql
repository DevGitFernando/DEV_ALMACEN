
set dateformat ymd 


	----select EsEnResguardo, SUM(Remisiones) as Remisiones 
	----from 
	----( 
	----select YEAR(fecharegistro) as Año, MONTH(fecharegistro) as Mes, EsEnResguardo , COUNT(distinct codigoCliente_FolioRemision) as Remisiones  
	----from INT_ND_RptAdmonDispensacion_Detallado__General 
	------ Where EsEnResguardo = 0 
	----group by -- CodigoCliente, 
	----	YEAR(fecharegistro), MONTH(fecharegistro), EsEnResguardo 
	----order by 1, 2 
	----) t 
	----group by EsEnResguardo 
	
select sum(ImporteTotal) as Total 
From INT_ND_RptAdmonDispensacion_Detallado__General (NoLock) 


	select CodigoCliente, Idfarmacia, Farmacia  
		, sum(SubTotal) as SubTotal
		, sum(Iva) as Iva 
		, sum(ImporteTotal) as ImporteTotal
		, COUNT(distinct IdFarmacia + '__' + FolioRemision) as Remisiones, 
		sum(Cantidad) as Cantidad   
	from INT_ND_RptAdmonDispensacion_Detallado__General 
	Where EsEnResguardo = 0 and Incluir = 1 and Cantidad > 0  
		-- and year(FechaRegistro) = 2015 and month(FechaRegistro) = 1 
	group by CodigoCliente, Idfarmacia, Farmacia   
	order by 2 			
	
	select CodigoCliente, Idfarmacia, Farmacia  
		, YEAR(fecharegistro) as Año, MONTH(fecharegistro) as Mes 
		, sum(SubTotal) as SubTotal
		, sum(Iva) as Iva 
		, sum(ImporteTotal) as ImporteTotal
		, COUNT(distinct IdFarmacia + '__' + FolioRemision) as Remisiones, 
		sum(Cantidad) as Cantidad   
	from INT_ND_RptAdmonDispensacion_Detallado__General 
	Where EsEnResguardo = 0 and Incluir = 1 and Cantidad > 0  
		-- and year(FechaRegistro) = 2015 and month(FechaRegistro) = 1 
	group by CodigoCliente, Idfarmacia, Farmacia   
		, YEAR(fecharegistro), MONTH(fecharegistro) -- , EsEnResguardo 
	order by 2  
		, 4, 5 
	
 
		
		
		