

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFG_COM_Perfiles_Personal' and xType = 'V' ) 
   Drop View vw_CFG_COM_Perfiles_Personal
Go--#SQL
 

Create View vw_CFG_COM_Perfiles_Personal 
With Encryption 
As 
	Select C.IdEstado, P.Estado, C.IdFarmacia, P.Farmacia, C.IdPersonal, P.NombreCompleto as Personal,
		 C.Status, (case when C.Status = 'A' Then 'Activo' Else 'Cancelado' End) as StatusRelacion 
	From CFG_COM_Perfiles_Personal C (NoLock)  
	Inner Join vw_Personal P (Nolock) On ( C.IdEstado = P.IdEstado and C.IdFarmacia = P.IdFarmacia and C.IdPersonal = P.IdPersonal )  

Go--#SQL


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFG_COM_Perfiles_Personal_ClavesSSA' and xType = 'V' ) 
   Drop View vw_CFG_COM_Perfiles_Personal_ClavesSSA 
Go--#SQL
 

Create View vw_CFG_COM_Perfiles_Personal_ClavesSSA 
With Encryption 
As 
	Select C.IdEstado, C.Estado, C.IdFarmacia, C.Farmacia, C.IdPersonal, C.Personal, 
		 Cc.IdClaveSSA_Sal as IdClaveSSA, 
		 S.ClaveSSA_Base as ClaveSSA, S.ClaveSSA_Aux, S.DescripcionClave as DescripcionClave, 
		 Cc.Status, (case when Cc.Status = 'A' Then 'Activa' Else 'Cancelada' End) as StatusRelacion 
	From CFG_COM_Perfiles_Personal_ClavesSSA Cc (NoLock)  
	Inner Join vw_CFG_COM_Perfiles_Personal C (NoLock) On ( C.IdEstado = Cc.IdEstado and C.IdFarmacia = Cc.IdFarmacia and C.IdPersonal = Cc.IdPersonal ) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( Cc.IdClaveSSA_Sal = S.IdClaveSSA_Sal )  
 
Go--#SQL