--------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_CB_Clasificacion_AEI__ClavesSSA'  and xType = 'V' ) 
   Drop View vw_CB_Clasificacion_AEI__ClavesSSA 
Go--#SQL 

Create View vw_CB_Clasificacion_AEI__ClavesSSA  
With Encryption 
As 

	Select 
		C.IdEstado, C.IdCliente, C.IdSubCliente, 
		C.ClaveGrupoAEI, C.ClaveAEI, D.NombreAEI, 
		C.IdClaveSSA, CC.ClaveSSA, CC.Descripcion, C.Status
	From CFG_CB_Clasificacion_AEI__ClavesSSA C (NoLock) 
	Inner Join CatClavesSSA_Sales CC (NoLock) On ( C.IdClaveSSA = CC.IdClaveSSA_Sal ) 
	Inner Join CFG_CB_Clasificacion_AEI__Detalles D (NoLock) 
		On ( C.IdEstado = D.IdEstado and C.IdCliente = D.IdCliente and C.IdSubCliente = D.IdSubCliente 
			and C.ClaveGrupoAEI = D.ClaveGrupoAEI and C.ClaveAEI = D.ClaveAEI  ) 

Go--#SQL

--	select * from vw_CB_Clasificacion_AEI__ClavesSSA 

