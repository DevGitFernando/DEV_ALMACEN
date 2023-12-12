If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ProductosSelectos' and xType = 'V' )
	Drop View vw_ProductosSelectos
Go--#SQL

Create View vw_ProductosSelectos
With Encryption 
As
	Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA,
		   DescripcionSal, DescripcionCortaClave, IdPresentacion, Presentacion, C.Status
	From CFG_ProductosSelectos C (NoLock)
	Inner Join vw_ClavesSSA_Sales S (NoLock) On (C.IdClaveSSA_Sal = S.IdClaveSSA_Sal)
	Where C.Status = 'A'
	
Go--#SQL