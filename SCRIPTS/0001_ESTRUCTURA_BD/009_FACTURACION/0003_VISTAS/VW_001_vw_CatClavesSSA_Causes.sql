If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CatClavesSSA_Causes' and xType = 'V' ) 
	Drop View vw_CatClavesSSA_Causes 
Go--#SQL 

Create View vw_CatClavesSSA_Causes 
With Encryption 
As 
	Select Distinct 
		C.ClaveSSA, S.DescripcionSal As Descripcion, C.IdPresentacion, S.Presentacion, C.ContenidoPaquete,
		S.TipoDeClave, S.TipoDeClaveDescripcion, C.EsSeguroPopular, C.EsControlado, C.EsAntibiotico,
		C.Año, C.PrecioBase, C.Porcentaje, C.PrecioAdmon, C.PrecioNeto, C.EsDollar, 
		(Case When C.Status = 'A' Then 'ACTIVA' Else 'CANCELADA' End) As DescStatus, C.Status
	From CatClavesSSA_Causes C (Nolock)
	Inner Join vw_ClavesSSA_Sales S (Nolock) On ( C.ClaveSSA = S.ClaveSSA ) 
	
Go--#SQL 