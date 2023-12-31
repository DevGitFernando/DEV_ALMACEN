If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Relacion_ClavesSSA_Claves' and xType = 'V' ) 
   Drop View vw_Relacion_ClavesSSA_Claves 
Go--#SQL 

Create View vw_Relacion_ClavesSSA_Claves 
with Encryption 
As 
	
	Select R.IdEstado, E.Nombre as Estado, 
		 R.IdClaveSSA as IdClaveSSA, 
		 S.ClaveSSA as ClaveSSA, S.ClaveSSA as ClaveSSA_Aux, S.Descripcion as Descripcion, 
		 S.ContenidoPaquete, 
		 
		 R.IdClaveSSA_Relacionada as IdClaveSSA_Relacionada, 
		 Sc.ClaveSSA as ClaveSSA_Relacionada, Sc.ClaveSSA as ClaveSSA_Aux_Relacionada, 
		 Sc.Descripcion as DescripcionRelacionada, 
	     Sc.ContenidoPaquete as ContenidoPaquete_Relacionado,
	     R.Multiplo, R.Status, ( case when R.Status = 'A' Then 'Activo' Else 'Cancelado' End ) as Status_Aux
	From CFG_ClavesSSA_ClavesRelacionadas R (NoLock) 
	Inner Join CatEstados E (NoLock) On ( R.IdEstado = E.IdEstado )  
	Inner Join CatClavesSSA_Sales S (NoLock) On ( R.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Inner Join CatClavesSSA_Sales Sc (NoLock) On ( R.IdClaveSSA_Relacionada = Sc.IdClaveSSA_Sal ) 	

Go--#SQL 
