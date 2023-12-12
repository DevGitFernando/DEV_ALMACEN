----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_CodigosEAN__Nadro' and xType = 'V' ) 
   Drop View vw_INT_ND_CodigosEAN__Nadro 
Go--#SQL  

Create View vw_INT_ND_CodigosEAN__Nadro  
With Encryption 
As 
	
	Select Distinct C.IdEstado, C.CodigoEAN, 
		C.CodigoEAN_ND, 
		P.Descripcion as DescripcionComercial,  
		PN.Descripcion as DescripcionComercial_ND, 
		P.Laboratorio, 
		P.ClaveSSA, P.ClaveSSA_Base, 
		PN.ClaveSSA_ND,  
		P.DescripcionClave, 
		CN.Descripcion as DescripcionClave_ND, 
		P.TipoDeClave, P.TipoDeClaveDescripcion 
	From INT_ND_CFG_CodigosEAN C (NoLock) 
	Inner Join vw_Productos_CodigoEAN P On ( C.CodigoEAN = P.CodigoEAN ) 
	Left Join INT_ND_Productos PN (NoLock) 
		--On ( C.CodigoEAN_ND = PN.CodigoEAN_ND ) 
		On ( right(replicate('0', 30) + C.CodigoEAN_ND, 30) = right(replicate('0', 30) + PN.CodigoEAN, 30) ) 
	Left Join INT_ND_Claves CN On ( PN.ClaveSSA_ND = CN.ClaveSSA_ND ) 	
		
	
Go--#SQL 	

