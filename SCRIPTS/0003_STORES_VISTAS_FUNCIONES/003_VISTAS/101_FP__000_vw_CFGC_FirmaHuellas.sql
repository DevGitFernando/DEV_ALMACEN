

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFGC_FirmaHuellas' and xType = 'V' ) 
   Drop View vw_CFGC_FirmaHuellas
Go--#SQL

Create View vw_CFGC_FirmaHuellas 
With Encryption 
As 

	Select ('http://' + IsNull(C.Servidor, '') + '/' + IsNull(C.WebService, '') + '/' + IsNull(C.PaginaWeb, '') + '.asmx') as UrlFarmacia, 
		IsNull(C.Status, 'S') as StatusUrl, C.Servidor, C.WebService, C.PaginaWeb  
	From CFGC_FirmaHuellas C (NoLock) 
Go--#SQL 