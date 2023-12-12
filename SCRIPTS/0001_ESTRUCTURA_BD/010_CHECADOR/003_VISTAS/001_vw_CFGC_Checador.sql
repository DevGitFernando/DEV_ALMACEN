

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFGC_Checador' and xType = 'V' ) 
   Drop View vw_CFGC_Checador 
Go--#SQL

Create View vw_CFGC_Checador  
With Encryption 
As 

	Select ('http://' + IsNull(C.Servidor, '') + '/' + IsNull(C.WebService, '') + '/' + IsNull(C.PaginaWeb, '') + '.asmx') as UrlFarmacia, 
		IsNull(C.Status, 'S') as StatusUrl, C.Servidor, C.WebService, C.PaginaWeb  
	From CFGC_Checador C (NoLock) 
Go--#SQL 