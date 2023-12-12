----------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CFGC_ALMN_DIST_CB_NivelesAtencion' and xType = 'V' ) 
	Drop View vw_CFGC_ALMN_DIST_CB_NivelesAtencion 
Go--#SQL 

Create view vw_CFGC_ALMN_DIST_CB_NivelesAtencion 
With Encryption 
As 
	Select B.IdEmpresa, B.IdEstado, B.IdFarmacia, B.IdPerfilAtencion, B.Descripcion as PerfilDeAtencion,  B.Status 
	From CFGC_ALMN_DIST_CB_NivelesAtencion  B (NoLock) 
		
Go--#SQL  


---------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA' and xType = 'V' ) 
	Drop View vw_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA 
Go--#SQL 

Create view vw_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA 
With Encryption 
As 

	Select -- E.* 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdPerfilAtencion, E.PerfilDeAtencion, 
		E.Status as StatusPerfilDeAtencion, 
		IsNull(C.ClaveSSA, '') as ClaveSSA, 
		IsNull(( Select Top 1 DescripcionClave From vw_ClavesSSA_Sales D (Nolock) Where D.ClaveSSA = C.ClaveSSA ), '') as DescripcionClaveSSA, 
		IsNull(C.Status, '') as StatusClaveSSA    	
	From vw_CFGC_ALMN_DIST_CB_NivelesAtencion E (NoLock) 
	Left Join CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA C (NoLock) 
		On ( E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia and E.IdPerfilAtencion = C.IdPerfilAtencion )  

Go--#SQL 


		