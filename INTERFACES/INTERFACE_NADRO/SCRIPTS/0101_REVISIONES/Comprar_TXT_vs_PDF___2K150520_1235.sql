
--	set dateformat ymd 
------------------------------------------- Borrar Datos 
	drop table #tmp_01 
	drop table #tmp_02  


------------------------------------------- Datos 
	select CodigoCliente, Idfarmacia, NombreFarmacia, IdAnexo, FolioRemision, ClaveSSA, 
		SUM(cantidad) as Cantidad, 
		SUM(SubTotal) as SubTotal, 
		SUM(Iva) as Iva, 
		SUM(ImporteTotal) as ImporteTotal  
	into #tmp_01 
	from INT_ND__Dispensacion (NoLock) 
	where IdFarmacia = 11 and YEAR(FechaRemision) = 2014 and MONTH(FechaRemision)= 9 and 
		Cantidad > 0 and 
		Incluir = 1 and EnResguardo = 0 
	group by CodigoCliente, Idfarmacia, NombreFarmacia, IdAnexo, FolioRemision, ClaveSSA  


	select CodigoCliente, Idfarmacia, Farmacia, IdAnexo, FolioRemision, ClaveSSA, 
		SUM(cantidad) as Cantidad, 
		SUM(SubTotal) as SubTotal, 
		SUM(Iva) as Iva, 
		SUM(ImporteTotal) as ImporteTotal  
	into #tmp_02 	
	from INT_ND_RptAdmonDispensacion_Detallado__General (NoLock) 
	where IdFarmacia = 11 and YEAR(FechaRegistro) = 2014 and MONTH(FechaRegistro)= 9 and 
		Cantidad > 0 
		and 
		Incluir = 1 and EsEnResguardo = 0 
	group by CodigoCliente, Idfarmacia, Farmacia, IdAnexo, FolioRemision, ClaveSSA 
	
	

------------------------------------------- Validar datos 

	Select * 
	From #tmp_01 T (NoLock)  
	Where Not Exists 
	(
		Select * 
		From #tmp_02 X (NoLock) 
		Where T.CodigoCliente = X.CodigoCliente and T.IdAnexo = X.IdAnexo and T.FolioRemision = X.FolioRemision 
	) 
 

	Select * 
	From #tmp_02 T (NoLock)  
	Where Not Exists 
	(
		Select * 
		From #tmp_01 X (NoLock) 
		Where T.CodigoCliente = X.CodigoCliente and T.IdAnexo = X.IdAnexo and T.FolioRemision = X.FolioRemision 
	) 

