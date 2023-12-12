------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Geograficos' and xType = 'V' ) 
	Drop View vw_CFDI_Geograficos 
Go--#SQL 

Create View vw_CFDI_Geograficos 
With Encryption 
As 

	Select 
		E.IdEstado, E.Nombre as Estado,  
		IsNull(M.IdMunicipio, '') as IdMunicipio, IsNull(M.Descripcion, '') as Municipio,  
		IsNull(C.IdColonia, '') as IdColonia, IsNull(C.Descripcion, '') as Colonia, 
		IsNull(C.CodigoPostal, 0) as CodigoPostal    		
	From CatEstados E (NoLock) 
	Left Join CatMunicipios M On ( E.IdEstado = M.IdEstado ) 
	Left Join CatColonias C On ( M.IdEstado = C.IdEstado and M.IdMunicipio = C.IdMunicipio ) 

Go--#SQL 


--		select * from vw_CFDI_Geograficos 

