

	update R set 
		CodigoCliente = REPLACE(CodigoCliente, char(160), ''), 
		IdFarmacia = REPLACE(IdFarmacia, char(160), ''), 
		Farmacia = REPLACE(Farmacia, char(160), ''), 
		IdAnexo = REPLACE(IdAnexo, char(160), ''), 
		NombreAnexo = REPLACE(NombreAnexo, char(160), ''), 
		Prioridad = REPLACE(Prioridad, char(160), ''), 
		Modulo = REPLACE(Modulo, char(160), ''), 
		NombrePrograma = REPLACE(NombrePrograma, char(160), ''), 
		FolioRemision = REPLACE(FolioRemision, char(160), ''), 
		TipoDispensacion = REPLACE(TipoDispensacion, char(160), ''), 
		EsCauses = REPLACE(EsCauses, char(160), ''), 			
		ClaveSSA_ND = REPLACE(ClaveSSA_ND, char(160), ''), 
		ClaveSSA_Mascara = REPLACE(ClaveSSA_Mascara, char(160), ''), 
		Descripcion_Mascara = REPLACE(Descripcion_Mascara, char(160), ''), 
		TipoDeInsumo = REPLACE(TipoDeInsumo, char(160), '')  
	from INT_ND_RptAdmonDispensacion_Detallado__General R -- _0501 (NoLock) 


	update R set 
		CodigoCliente = REPLACE(CodigoCliente, char(160), ''), 
		IdFarmacia = REPLACE(IdFarmacia, char(160), ''), 
		NombreFarmacia = REPLACE(NombreFarmacia, char(160), ''), 
		IdAnexo = REPLACE(IdAnexo, char(160), ''), 
		NombreAnexo = REPLACE(NombreAnexo, char(160), ''), 
		Prioridad = REPLACE(Prioridad, char(160), ''), 
		Modulo = REPLACE(Modulo, char(160), ''), 
		NombrePrograma = REPLACE(NombrePrograma, char(160), ''), 
		FolioRemision = REPLACE(FolioRemision, char(160), ''), 
		--TipoDispensacion = REPLACE(TipoDispensacion, char(160), ''), 
		EsCauses = REPLACE(EsCauses, char(160), ''), 			
		ClaveSSA_ND = REPLACE(ClaveSSA_ND, char(160), ''), 
		ClaveSSA_Mascara = REPLACE(ClaveSSA_Mascara, char(160), ''), 
		Descripcion_Mascara = REPLACE(Descripcion_Mascara, char(160), '')  
		--TipoDeInsumo = REPLACE(TipoDeInsumo, char(160), '')  
	from INT_ND__Dispensacion R -- _0501 (NoLock) 
