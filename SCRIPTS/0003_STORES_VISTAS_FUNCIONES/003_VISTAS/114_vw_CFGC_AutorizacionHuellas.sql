

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFGC_AutorizacionHuellas' and xType = 'V' ) 
   Drop View vw_CFGC_AutorizacionHuellas
Go--#SQL

Create View vw_CFGC_AutorizacionHuellas 
With Encryption 
As 

	Select ('http://' + IsNull(C.Servidor, '') + '/' + IsNull(C.WebService, '') + '/' + IsNull(C.PaginaWeb, '') + '.asmx') as UrlFarmacia, 
		IsNull(C.Status, 'S') as StatusUrl, C.Servidor, C.WebService, C.PaginaWeb  
	From CFGC_AutorizacionHuellas C (NoLock) 
Go--#SQL 