-------------------------------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CatProductos__ListasDePrecios_ClientesSubClientes_Historico' and xType = 'U' ) 
   Drop Table CatProductos__ListasDePrecios_ClientesSubClientes_Historico   
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CatProductos__ListasDePrecios_ClientesSubClientes' and xType = 'U' ) 
   Drop Table CatProductos__ListasDePrecios_ClientesSubClientes  
Go--#SQL 

Create Table CatProductos__ListasDePrecios_ClientesSubClientes 
( 
	IdCliente varchar(4) Not Null,  
	IdSubCliente varchar(4) Not Null,  
	IdProducto varchar(8) Not Null,  

	Descuento numeric(14, 4) Not Null Default 0, 

	FechaRegistro datetime Not Null Default getdate(), 
	FechaUltimaModificacion datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A'
) 
Go--#SQL 

Alter Table CatProductos__ListasDePrecios_ClientesSubClientes Add Constraint PK_CatProductos__ListasDePrecios_ClientesSubClientes Primary Key ( IdCliente, IdSubCliente, IdProducto ) 
Go--#SQL 

Alter Table CatProductos__ListasDePrecios_ClientesSubClientes Add Constraint FK_CatProductos__ListasDePrecios_ClientesSubClientes____CatSubClientes 
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente ) 
Go--#SQL

Alter Table CatProductos__ListasDePrecios_ClientesSubClientes Add Constraint FK_CatProductos__ListasDePrecios_ClientesSubClientes____CatProductos 
	Foreign Key ( IdProducto ) References CatProductos ( IdProducto ) 
Go--#SQL 



-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CatProductos__ListasDePrecios_ClientesSubClientes_Historico' and xType = 'U' ) 
   Drop Table CatProductos__ListasDePrecios_ClientesSubClientes_Historico   
Go--#SQL 

Create Table CatProductos__ListasDePrecios_ClientesSubClientes_Historico 
( 
	Keyx int identity(1, 1), 
	IdCliente varchar(4) Not Null,  
	IdSubCliente varchar(4) Not Null,  
	IdProducto varchar(8) Not Null,  
	Descuento numeric(14, 4) Not Null Default 0, 

	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	IdPersonal varchar(4) Not Null Default '', 

	FechaUltimaModificacion datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A'
) 
Go--#SQL 

Alter Table CatProductos__ListasDePrecios_ClientesSubClientes_Historico 
	Add Constraint PK_CatProductos__ListasDePrecios_ClientesSubClientes_Historico Primary Key ( IdCliente, IdSubCliente, IdProducto, Keyx ) 
Go--#SQL 

Alter Table CatProductos__ListasDePrecios_ClientesSubClientes_Historico 
	Add Constraint FK_CatProductos__ListasDePrecios_ClientesSubClientes_Historico___CatProductos__ListasDePrecios_ClientesSubClientes  
	Foreign Key ( IdCliente, IdSubCliente, IdProducto ) References CatProductos__ListasDePrecios_ClientesSubClientes ( IdCliente, IdSubCliente, IdProducto)  
Go--#SQL


