--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Farmacias_Urls' and xType = 'V' ) 
   Drop View vw_Farmacias_Urls 
Go--#SQL 

Create View vw_Farmacias_Urls  
With Encryption 
As 

	Select -- Distinct 
		E.IdEmpresa, 
		F.IdEstado, F.Estado, F.ClaveRenapo, F.EdoStatus, F.IdFarmacia, F.Farmacia, F.EsDeConsignacion,  
		F.EsAlmacen, F.IdTipoUnidad, F.TipoDeUnidad, 
		F.FarmaciaStatus, F.FarmaciaStatusAux, 
		('http' + (case when C.SSL = 1 then 's' else '' end) + '://' + IsNull(C.Servidor, '') + '/' + IsNull(C.WebService, '') + '/' + IsNull(C.PaginaWeb, '') + '.asmx') as UrlFarmacia, 
		IsNull(C.Status, 'S') as StatusUrl, E.Status as StatusRelacion  
	From CFG_EmpresasFarmacias E (nolock) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and E.Status = 'A' )  	
	Left Join CFGS_ConfigurarConexiones C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia )   

Go--#SQL  	


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_OficinaCentral_Url' and xType = 'V' ) 
   Drop View vw_OficinaCentral_Url 
Go--#SQL	
 
Create View vw_OficinaCentral_Url 
With Encryption 
As 

	Select 
		(IsNull(C.Prefijo, T.Prefijo) + IsNull(C.Servidor, '') + '/' + IsNull(C.WebService, '') + '/' + IsNull(C.PaginaWeb, '') + '.asmx') as Url, 
		IsNull(C.Status, 'S') as StatusUrl 
	From (Select 1 as Tipo, 'http://' as Prefijo) T 
	Left Join (
				Select Top 1 (1) as Tipo, *, 'http' + (case when C.SSL = 1 then 's' else '' end) + '://' as Prefijo  
				From CFGSC_ConfigurarConexion C (NoLock)
			   ) C On (T.Tipo=C.Tipo) 
	
Go--#SQL 	


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_OficinaCentralRegional_Url' and xType = 'V' ) 
   Drop View vw_OficinaCentralRegional_Url 
Go--#SQL	
 
Create View vw_OficinaCentralRegional_Url 
With Encryption 
As 

	Select 
		(IsNull(C.Prefijo, T.Prefijo) + IsNull(C.Servidor, '') + '/' + IsNull(C.WebService, '') + '/' + IsNull(C.PaginaWeb, '') + '.asmx') as Url, 
		IsNull(C.Status, 'S') as StatusUrl 
	From (Select 1 as Tipo, 'http://' as Prefijo) T 
	Left Join (
				Select Top 1 (1) as Tipo, *, 'http' + (case when C.SSL = 1 then 's' else '' end) + '://' as Prefijo 
				From CFGC_ConfigurarConexion C (NoLock)
			  ) C On (T.Tipo=C.Tipo) 
	
Go--#SQL 	


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Regionales_Urls' and xType = 'V' ) 
   Drop View vw_Regionales_Urls 
Go--#SQL

Create View vw_Regionales_Urls  
With Encryption 
As 

	Select Distinct 
		E.IdEmpresa, 
		F.IdEstado, F.Estado, F.ClaveRenapo, F.EdoStatus, F.IdFarmacia, F.Farmacia, F.EsDeConsignacion,  
		F.FarmaciaStatus, F.FarmaciaStatusAux, 
		('http' + (case when C.SSL = 1 then 's' else '' end) + '://' + IsNull(C.Servidor, '') + '/' + IsNull(C.WebService, '') + '/' + IsNull(C.PaginaWeb, '') + '.asmx') as UrlFarmacia, 
		IsNull(C.Status, 'S') as StatusUrl, E.Status as StatusRelacion  
	From CFG_EmpresasFarmacias E (nolock) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and E.Status = 'A' )  	
	Inner Join CFGSC_ConfigurarConexiones C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia )   
	Where E.IdEmpresa in ( 1, 2 )  
	
Go--#SQL  
