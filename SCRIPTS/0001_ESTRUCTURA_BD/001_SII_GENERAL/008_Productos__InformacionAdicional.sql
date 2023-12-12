
---- If Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'CatProductos_InformacionAdicional' and xType = 'U' ) Drop Table CatProductos_InformacionAdicional

-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'CatProductos_InformacionAdicional' and xType = 'U' )  
Begin 	

	Create Table CatProductos_InformacionAdicional  
	(
		IdProducto varchar(8) Not Null, 
		Descripcion varchar(200) Not Null Default '',  
		Laboratorio varchar(100) Not Null Default '' 
	) 

	Alter Table CatProductos_InformacionAdicional Add Constraint PK_CatProductos_InformacionAdicional 
		Primary Key ( IdProducto ) 

	Alter Table CatProductos_InformacionAdicional Add Constraint FK_CatProductos_InformacionAdicional___CatProductos 
		Foreign Key ( IdProducto ) References CatProductos ( IdProducto ) 

End 
Go--#SQL 

--	I.Descripcion as Descripcion_InformacionAdicional, I.Concentracion as Concentracion_InformacionAdicional, I.Presentacion as Presentacion_InformacionAdicional

---	select * from CatProductos_InformacionAdicional 
