
	Update L Set IdProducto = right('00000000' + IdProducto, 8) 
	From CatProductos_CFDI_Load L (NoLock) 

	
	Update P Set SAT_ClaveProducto_Servicio = C.SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida = C.SAT_UnidadDeMedida
	From CatProductos_CFDI P (NoLock) 
	Inner Join CatProductos_CFDI_Load C (NoLock) On ( cast(P.IdProducto as int)= cast(C.IdProducto as int) ) 


	 
	Insert Into CatProductos_CFDI (  IdProducto, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida )
	select  distinct IdProducto, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida 
	from CatProductos_CFDI_Load C (NoLock) 
	Where Not Exists 
	( 
		Select * 
		From CatProductos_CFDI P (NoLock) 
		Where C.IdProducto = P.IdProducto 
	) 

	Go--#SQL 

