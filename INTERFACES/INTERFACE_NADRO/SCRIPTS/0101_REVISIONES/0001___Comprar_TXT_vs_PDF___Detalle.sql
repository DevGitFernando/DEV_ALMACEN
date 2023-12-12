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
	Inner Join 
	( 
		select CodigoCliente, Idfarmacia, Farmacia, -- Folio, IdBeneficiario, 
			IdAnexo, FolioRemision, ClaveSSA, 
			SUM(cantidad) as Cantidad, 
			SUM(SubTotal) as SubTotal, 
			SUM(Iva) as Iva, 
			SUM(ImporteTotal) as ImporteTotal  
		from INT_ND_RptAdmonDispensacion_Detallado__General (NoLock) 
		where -- IdFarmacia = 15 and -- YEAR(FechaRegistro) = 2015 and MONTH(FechaRegistro)= 1 and day(FechaRegistro) = 29 and 
			Cantidad > 0 
			and 
			Incluir = 1 and EsEnResguardo = 0 
			-- and FolioRemision = '1.1.5/01/310115/C' 
		group by CodigoCliente, Idfarmacia, Farmacia, -- Folio, IdBeneficiario, 
			IdAnexo, FolioRemision, ClaveSSA 
	) X on ( T.IdFarmacia = X.IdFarmacia and T.IdAnexo = X.IdAnexo and T.FolioRemision = X.FolioRemision 
		-- X.FolioRemision like '%' + T.FolioRemision + '%' 
		and T.ClaveSSA = X.ClaveSSA ) 
	where T.Cantidad <> X.Cantidad  
		-- T.Iva <> X.Iva 
		-- T.ImporteTotal <> X.ImporteTotal 
		-- T.ClaveSSA = 'SC-MC-851' 
		-- and T.FolioRemision = '1.2.11/01/020115/CN' 
				


