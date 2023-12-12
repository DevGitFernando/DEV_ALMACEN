
---	select * from IMach_CFGC_Productos 


	insert into IMach_CFGC_Productos  
	select distinct P.IdProducto, P.CodigoEAN, 0, 0, 'A', 0 
	from vw_Productos_CodigoEAN C 
	Inner Join vw_Productos_CodigoEAN P On ( C.ClaveSSA = P.ClaveSSA and C.TipoDeClave = '02' and C.IdClaveSSA_Sal <> '0000' )
	where 1 = 1 -- C.IdEstado = 21 and C.IdFarmacia = 2406 
		  and Not Exists 
			( 
				Select * 
				From IMach_CFGC_Productos I (NoLock) 
				Where P.IdProducto = I.IdProducto and P.CodigoEAN = I.CodigoEAN 
			)   		




	insert into IMach_CFGC_Clientes_Productos 
	select '0001', IdProducto, CodigoEAN, 1, 0 
	from IMach_CFGC_Productos  P 
	where 
		  Not Exists 
			( 
				Select * 
				From IMach_CFGC_Clientes_Productos I (NoLock) 
				Where I.IdCliente = '0001' and P.IdProducto = I.IdProducto and P.CodigoEAN = I.CodigoEAN 
			)   


