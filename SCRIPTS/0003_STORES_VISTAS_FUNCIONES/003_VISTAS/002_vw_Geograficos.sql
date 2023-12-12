If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Geograficos' and xType = 'V' ) 
   Drop View vw_Geograficos 
Go--#SQL  

Create View vw_Geograficos 
With Encryption 
As 

	Select E.IdEstado, E.Nombre as Estado, E.ClaveRenapo, E.Status as EdoStatus, 
		   M.IdMunicipio, M.Descripcion as Municipio, M.Status as MunStatus, 
		   C.IdColonia, C.Descripcion as Colonia, C.Status as ColStatus  
	From CatEstados E (NoLock) 
	Inner Join CatMunicipios M (NoLock) On ( E.IdEstado = M.IdEstado) 	
	Inner Join CatColonias C (NoLock) On ( M.IdEstado = C.IdEstado and M.IdMunicipio = C.IdMunicipio ) 
Go--#SQL  
   