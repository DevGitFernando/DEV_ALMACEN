----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_Anexos_Causes' and xType = 'V' ) 
   Drop View vw_INT_ND_Anexos_Causes 
Go--#SQL  

Create View vw_INT_ND_Anexos_Causes  
With Encryption 
As 
	
	Select 
		A.IdEstado, 
		A.IdAnexo, A.NombreAnexo, A.NombrePrograma, A.Prioridad, M.Status as StatusMiembro  
	From INT_ND_CFG_CB_Anexos A (NoLock) 
	Inner Join INT_ND_CFG_CB_Anexos_Causes M (NoLock) On ( A.IdAnexo = M.IdAnexo ) 
	
Go--#SQL 	


----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_Anexos_Miembros' and xType = 'V' ) 
   Drop View vw_INT_ND_Anexos_Miembros 
Go--#SQL  

Create View vw_INT_ND_Anexos_Miembros  
With Encryption 
As 
	
	Select 
		A.IdEstado, F.Estado, M.CodigoCliente, C.Nombre as NombreCliente, 
		C.IdFarmacia, F.Farmacia, F.IdTipoUnidad, F.TipoDeUnidad, 
		A.IdAnexo, (case when IsNull(EC.IdAnexo, '') = '' then 'NO' else 'SI' end) as EsCauses, 
		A.NombreAnexo, A.NombrePrograma, A.Prioridad, M.Status as StatusMiembro  
	From INT_ND_CFG_CB_Anexos A (NoLock) 
	Inner Join INT_ND_CFG_CB_Anexos_Miembros M (NoLock) On ( A.IdAnexo = M.IdAnexo ) 
	Inner Join INT_ND_Clientes C (NoLock) On ( M.IdEstado = C.IdEstado and M.CodigoCliente = C.CodigoCliente )
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia and EsDeSurtimiento = 0   ) 
	Left Join INT_ND_CFG_CB_Anexos_Causes EC (NoLock) On ( A.IdAnexo = EC.IdAnexo ) 
	Where M.CodigoCliente <> '' 
	
Go--#SQL 	
	
	
----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_CuadrosBasicos' and xType = 'V' ) 
   Drop View vw_INT_ND_CuadrosBasicos 
Go--#SQL  	
	
Create view vw_INT_ND_CuadrosBasicos 
With Encryption 
As 	
	
	Select 
		Distinct 
		M.IdEstado, M.Estado, M.CodigoCliente, M.NombreCliente, 
		M.IdFarmacia, M.Farmacia, M.IdTipoUnidad, M.TipoDeUnidad, 
		M.IdAnexo, M.EsCauses, M.NombreAnexo, M.NombrePrograma, M.Prioridad, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 
		C.ClaveSSA, C.ClaveSSA_ND, C.ClaveSSA_Mascara, C.ManejaIva, C.PrecioVenta, C.PrecioServicio, C.Descripcion_Mascara, 
		M.StatusMiembro 
	From vw_INT_ND_Anexos_Miembros M (NoLock) 
	Inner Join INT_ND_CFG_CB_CuadrosBasicos C (NoLock) On ( M.IdEstado = C.IdEstado and M.IdAnexo = C.IdAnexo and M.Prioridad = C.Prioridad ) 
	Inner Join vw_ClavesSSA_Sales P (NoLock) On ( C.ClaveSSA = P.ClaveSSA ) 

	
		
Go--#SQL   
	
--	sp_listacolumnas vw_INT_ND_Anexos_Miembros  



----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_CuadrosBasicos_Claves' and xType = 'V' ) 
   Drop View vw_INT_ND_CuadrosBasicos_Claves  
Go--#SQL  	
	
Create view vw_INT_ND_CuadrosBasicos_Claves
With Encryption 
As 	
	
	
	Select 
		Distinct 
		C.IdAnexo, 
		(case when IsNull(EC.IdAnexo, '') = '' then 'NO' else 'SI' end) as EsCauses, 
		IsNull(M.NombreAnexo, 'SIN ESPECIFICAR') as NombreAnexo, 
		IsNull(M.NombrePrograma, 'SIN ESPECIFICAR') as NombrePrograma, 
		C.Prioridad, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 
		C.ClaveSSA, C.ClaveSSA_ND, C.ClaveSSA_Mascara, C.ManejaIva, C.PrecioVenta, C.PrecioServicio, C.Descripcion_Mascara  		 
	From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) 
	Left Join INT_ND_CFG_CB_Anexos M (NoLock) On ( M.IdEstado = C.IdEstado and M.IdAnexo = C.IdAnexo and M.Prioridad = C.Prioridad ) 
	Left Join INT_ND_CFG_CB_Anexos_Causes EC (NoLock) On ( M.IdAnexo = EC.IdAnexo ) 	
	Left Join vw_ClavesSSA_Sales P (NoLock) On ( C.ClaveSSA = P.ClaveSSA ) 


Go--#SQL   



----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_Claves_Relacionadas' and xType = 'V' ) 
   Drop View vw_INT_ND_Claves_Relacionadas  
Go--#SQL  	
	
Create view vw_INT_ND_Claves_Relacionadas 
With Encryption 
As 	

	----Select * 
	----From INT_ND_CFG__ManejoDeClaves_OPM (NoLock) 
	
	Select  
		IdEstado, 
		(case when (IsNull(P.IdClaveSSA_Sal, '') = '' or IsNull(C.ClaveSSA, '') = '') Then 0 else 1 end) as ClaveRelacionada, 
		IsNull(P.IdClaveSSA_Sal, '') as IdClaveSSA, 
		IsNull(C.ClaveSSA, '') as ClaveSSA, C.ClaveSSA_ND, 
		P.Descripcion as DescripcionClaveSSA, 
		CN.Descripcion as DescripcionClaveSSA_ND, 
		-- CV.Descripcion_Mascara, 
		IsNull(P.IdTipoProducto, '00') as TipoDeClave, 
		-- IsNull(P.TipoDeClaveDescripcion, 'NO IDENTIFICADO') as TipoDeClaveDescripcion   	
		(case when P.IdTipoProducto = '01' Then 'MATERIAL DE CURACIÓN'
			  when P.IdTipoProducto = '02' Then 'MEDICAMENTO'  
		      Else 
				'NO IDENTIFICADO' 
		      End ) as TipoDeClaveDescripcion,  
		 (case when C.Status = 'A' Then 1 else 0 end) as StatusRelacion,
		 (case when C.Status = 'A' Then 'ACTIVO' else 'CANCELADO' end) as StatusRelacionDescripcion 		 
	From INT_ND_CFG_ClavesSSA C (NoLock)  
	Inner Join INT_ND_Claves CN (NoLock) On ( C.ClaveSSA_ND = CN.ClaveSSA_ND ) 
	Inner Join CatClavesSSA_Sales P (NoLock) On ( C.ClaveSSA = P.ClaveSSA ) 

Go--#SQL 

