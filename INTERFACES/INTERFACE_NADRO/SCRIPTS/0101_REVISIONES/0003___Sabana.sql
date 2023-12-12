


	select  
		CodigoCliente, IdFarmacia, Farmacia, IdAnexo, NombreAnexo, 
		-- Prioridad, Modulo, 
		NombrePrograma, FolioRemision, 
		-- TipoDispensacion, EsCauses, 		
		FechaRegistro, 
		Folio, NumReceta, 
		ClaveSSA_ND, ClaveSSA_Mascara, Descripcion_Mascara, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, 
		ContenidoPaquete_Auxiliar, Piezas, 
		Cantidad, 
		TipoDeInsumo 
	from INT_ND_RptAdmonDispensacion_Detallado__General (NoLock) 
	where EsEnResguardo = 0 and Incluir = 1 and Prioridad <> 0 and Cantidad > 0 
		-- and ClaveSSA_ND  = '9980000396' 
		-- and claveSSA = 'SC-MED-50' 
		

/* 
	select CodigoCliente + '____' + FolioRemision,  * 
	from INT_ND__Dispensacion -- _0501 (NoLock) 
	where -- ClaveSSA_ND = '9980000037' 
		Prioridad <> 0 
		-- PrecioVenta = 0 
		-- Descripcion_Mascara like '%salbuta%' 
		-- claveSSA = 'SC-MED-50' 
		
		
	select * -- CodigoCliente + '____' + FolioRemision,  * 
	from INT_ND__Dispensacion -- _0501 (NoLock) 
	where CodigoCliente + '____' + FolioRemision not in 
	(
		select CodigoCliente + '____' + FolioRemision 
		from INT_ND_RptAdmonDispensacion_Detallado__General
	) 		
*/ 
	
	