Set DateFormat YMD 
Go--#SQL 

Set NoCount Off  
Go--#SQL 


	----	select top 1 * from INT_ND__PRCS_Existencias 
		

	Update E Set CodigoEAN_ND = H.CodigoEAN_ND -- , CodigoRelacionado = 1, Procesado = E.Procesado + 1  
	From INT_ND__PRCS_Existencias E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 


	Update E Set CodigoEAN_ND = H.CodigoEAN_ND -- , CodigoRelacionado = 2, Procesado = E.Procesado + 1   
	From INT_ND__PRCS_Existencias E 
	Inner Join INT_ND_CFG_CodigosEAN H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN, 30) )   	
	
	
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, CodigoEAN_ND = H.CodigoEAN_ND, DescripcionComercial = H.Descripcion
		-- , Procesado = E.Procesado -- + 1   
	From INT_ND__PRCS_Existencias E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN_ND, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 


	
	Update E Set DescripcionClave = L.Descripcion --, Procesado = E.Procesado + 1 
	From INT_ND__PRCS_Existencias E 
	inner Join INT_ND_Claves L (NoLock) On ( E.ClaveSSA_ND = L.ClaveSSA_ND ) 		

	
	