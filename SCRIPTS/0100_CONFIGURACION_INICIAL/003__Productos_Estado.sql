
	select * 
	from CatProductos_Estado 
	Where IdEstado = 11 
	
	
	Insert Into CatProductos_Estado 
	Select '11' as IdEstado, IdProducto, 'A', 0 
	From CatProductos P (NoLock) 
	Where Not Exists  
		(	
			Select * 
			From CatProductos_Estado E (NoLock) 
			Where E.IdEstado = '11' and P.IdProducto = E.IdProducto 
		) 
	