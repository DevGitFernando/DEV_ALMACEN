--	set dateformat ymd 


	Select * -- distinct T.ClaveSSA  
	From 
	( 
		select CodigoCliente, Idfarmacia, NombreFarmacia, IdAnexo, FolioRemision, ClaveSSA, 
			SUM(cantidad) as Cantidad, 
			SUM(SubTotal) as SubTotal, 
			SUM(Iva) as Iva, 
			SUM(ImporteTotal) as ImporteTotal  
		from INT_ND__Dispensacion (NoLock) 
		where -- IdFarmacia = 11 and --YEAR(FechaRemision) = 2015 and MONTH(FechaRemision)= 1 and day(FechaRemision) = 29 and 
			Cantidad > 0 
			and 
			Incluir = 1 and EnResguardo = 0 
		group by CodigoCliente, Idfarmacia, NombreFarmacia, IdAnexo, FolioRemision, ClaveSSA 
		--		060.550.0446   
	) T 
	inner Join 
	( 
		select CodigoCliente, Idfarmacia, NombreFarmacia, -- Folio, IdBeneficiario, 
			IdAnexo, FolioRemision, ClaveSSA, 
			SUM(cantidad) as Cantidad, 
			SUM(SubTotal) as SubTotal, 
			SUM(Iva) as Iva, 
			SUM(ImporteTotal) as ImporteTotal  
		from INT_ND__Dispensacion____Exec_2K150520_0805____PI20140801_PF20150228_____Final___NoBorrar (NoLock) 
		where -- IdFarmacia = 15 and -- YEAR(FechaRegistro) = 2015 and MONTH(FechaRegistro)= 1 and day(FechaRegistro) = 29 and 
			Cantidad > 0 
			and 
			Incluir = 1 and EnResguardo = 0 
			-- and FolioRemision = '1.1.5/01/310115/C' 
		group by CodigoCliente, Idfarmacia, NombreFarmacia, -- Folio, IdBeneficiario, 
			IdAnexo, FolioRemision, ClaveSSA 
	) X on ( T.IdFarmacia = X.IdFarmacia and T.IdAnexo = X.IdAnexo and T.FolioRemision = X.FolioRemision 
		-- X.FolioRemision like '%' + T.FolioRemision + '%' 
		and T.ClaveSSA = X.ClaveSSA ) 
	where -- T.Cantidad <> X.Cantidad  
		-- T.Iva <> X.Iva 
		T.ImporteTotal <> IsNull(X.ImporteTotal, 0)  
		-- T.ClaveSSA = 'SC-MC-851' 
		-- and T.FolioRemision = '1.2.11/01/020115/CN' 
	Order by T.IdFarmacia, T.FolioRemision 
					


