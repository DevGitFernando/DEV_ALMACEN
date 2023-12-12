If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_AIE_Claves_Licitadas' and xType = 'V' ) 
   Drop View vw_AIE_Claves_Licitadas 
Go--#SQL 

Create View vw_AIE_Claves_Licitadas 
As 
	Select L.IdAccesoExterno, L.ClaveSSA, 
		C.DescripcionClave, C.Presentacion, C.ContenidoPaquete, C.EsControlado, C.EsAntibiotico, C.GrupoTerapeutico,    
		L.Status 
	From AIE_CFG_Claves_Licitadas L (NoLock) 
	Inner Join vw_ClavesSSA_Sales C (NoLock) 
		On ( L.ClaveSSA = C.ClaveSSA ) 
		-- On ( L.ClaveSSA = C.ClaveSSA_Base ) 	



--	select top 1 * from vw_ClavesSSA_Sales 

Go--#SQL 

	
--	Select * from 	vw_AIE_Claves_Licitadas 
	
	
	