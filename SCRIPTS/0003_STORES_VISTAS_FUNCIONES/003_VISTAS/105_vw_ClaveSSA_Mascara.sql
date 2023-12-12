------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ClaveSSA_Mascara' and xType = 'V' ) 
   Drop View vw_ClaveSSA_Mascara 
Go--#SQL

Create View vw_ClaveSSA_Mascara
With Encryption 
As 
	Select M.IdEstado, E.Nombre As Estado, M.IdCliente, C.NombreCliente, M.IdSubCliente, C.NombreSubCliente,
		M.IdClaveSSA, S.ClaveSSA, S.DescripcionCortaClave As Descripcion, 
		S.TipoDeClave, S.TipoDeClaveDescripcion, 
		M.Mascara, M.Descripcion As DescripcionMascara, DescripcionCorta, 
		M.Presentacion, 
		--Presentacion, 
		M.Status
	From CFG_ClaveSSA_Mascara M (NoLock)
	Inner Join CatEstados E (NoLock) On (M.IdEstado = E.IdEstado)  
	Inner Join vw_Clientes_SubClientes C (NoLock) On (M.IdCliente = C.IdCliente ANd M.IdSubCliente = C.IdSubCliente)
	-- Inner Join CatClavesSSA_Sales S (NoLock) ON ( M.IdClaveSSA = S.IdClaveSSA_Sal )
	Inner Join vw_ClavesSSA_Sales S (NoLock) ON ( M.IdClaveSSA = S.IdClaveSSA_Sal )

Go--#SQL 	 	
	