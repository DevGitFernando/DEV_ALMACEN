If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Clientes_Terminales' and xType = 'U' ) 
   Drop Table IMach_CFGC_Clientes_Terminales
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Clientes_Productos' and xType = 'U' ) 
   Drop Table IMach_CFGC_Clientes_Productos
Go--#SQL   


---------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Clientes' and xType = 'U' ) 
   Drop Table IMach_CFGC_Clientes
Go--#SQL   

Create Table IMach_CFGC_Clientes 
( 
	IdCliente varchar(4) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL   

Alter Table IMach_CFGC_Clientes Add Constraint PK_IMach_CFGC_Clientes Primary Key ( IdCliente ) 
Go--#SQL   

Alter Table IMach_CFGC_Clientes Add Constraint FK_IMach_CFGC_Clientes_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL   

------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Terminales' and xType = 'U' ) 
   Drop Table IMach_CFGC_Terminales
Go--#SQL   

Create Table IMach_CFGC_Terminales 
( 
	IdTerminal varchar(4) Not Null, 
	Nombre varchar(50) Not Null, 
	MAC_Address varchar(20) Not Null Unique, 
	EsDeSistema bit Not Null Default 'False', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table IMach_CFGC_Terminales Add Constraint PK_IMach_CFGC_Terminales Primary Key ( IdTerminal ) 
Go--#SQL   


------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Clientes_Terminales' and xType = 'U' ) 
   Drop Table IMach_CFGC_Clientes_Terminales
Go--#SQL   

Create Table IMach_CFGC_Clientes_Terminales 
( 
	IdCliente varchar(4) Not Null, 
	IdTerminal varchar(4) Not Null, 
	Asignada bit Not Null Default 'false', 
    EsDeSistemas bit Not Null Default 'false', 
	Activa bit Not Null Default 'false', 
	PuertoDispensacion tinyint Not Null Default 3, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL   

Alter Table IMach_CFGC_Clientes_Terminales Add Constraint PK_IMach_CFGC_Clientes_Terminales Primary Key ( IdCliente, IdTerminal ) 
Go--#SQL   

Alter Table IMach_CFGC_Clientes_Terminales Add Constraint PK_IMach_CFGC_Clientes_Terminales_PK_IMach_CFGC_Clientes 
	Foreign Key ( IdCliente ) References IMach_CFGC_Clientes ( IdCliente ) 
Go--#SQL   

Alter Table IMach_CFGC_Clientes_Terminales Add Constraint PK_IMach_CFGC_Clientes_Terminales_PK_IMach_CFGC_Terminales 
	Foreign Key ( IdTerminal ) References IMach_CFGC_Terminales ( IdTerminal ) 
Go--#SQL   

------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Clientes_Productos' and xType = 'U' ) 
   Drop Table IMach_CFGC_Clientes_Productos
Go--#SQL   

Create Table IMach_CFGC_Clientes_Productos 
( 
	IdCliente varchar(4) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	Status bit Not Null Default 'false', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL   	

Alter Table IMach_CFGC_Clientes_Productos Add Constraint PK_IMach_CFGC_Clientes_Productos 
	Primary Key ( IdCliente, IdProducto, CodigoEAN) 
Go--#SQL   

Alter Table IMach_CFGC_Clientes_Productos Add Constraint PK_IMach_CFGC_Clientes_Productos_IMach_CFGC_Clientes 
	Foreign Key ( IdCliente ) References IMach_CFGC_Clientes ( IdCliente ) 
Go--#SQL   

Alter Table IMach_CFGC_Clientes_Productos Add Constraint PK_IMach_CFGC_Clientes_Productos_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL   

