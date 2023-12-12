----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_Layouts ' and xType = 'U' ) 
   Drop Table INT_ND_CFG_Layouts  
Go--#SQL 

Create Table INT_ND_CFG_Layouts  
( 
	IdEstado varchar(2) Not Null, 
	IdLayout int Not Null Default 0, 
	NombreLayout varchar(200) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default '' 
) 	
Go--#SQL 

Alter Table INT_ND_CFG_Layouts  Add Constraint PK_INT_ND_CFG_Layouts  Primary Key  ( IdEstado, IdLayout ) 
Go--#SQL 

Insert into INT_ND_CFG_Layouts Select 16, 1, 'CLIENTES', 'A'  
Insert into INT_ND_CFG_Layouts Select 16, 2, 'CONDICIONES DE VENTA', 'A' 
Insert into INT_ND_CFG_Layouts Select 16, 3, 'ARTICULOS', 'A' 
Insert into INT_ND_CFG_Layouts Select 16, 4, 'EXISTENCIA', 'A' 
Insert into INT_ND_CFG_Layouts Select 16, 5, 'PEDIDOS DE VENTA DIRECTA', 'A'  
Insert into INT_ND_CFG_Layouts Select 16, 6, 'PEDIDOS DE TRASPASO', 'A' 
Go--#SQL 


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_Layouts_Columnas ' and xType = 'U' ) 
   Drop Table INT_ND_CFG_Layouts_Columnas  
Go--#SQL 

Create Table INT_ND_CFG_Layouts_Columnas   
( 
	IdEstado varchar(2) Not Null, 
	IdLayout int Not Null Default 0, 
	NombreColumna varchar(200) Not Null Default '', 
	OrdenColumna int Not Null Default 0, 
	SeIntegra bit Not Null Default 'false', 
	Status varchar(1) Not Null Default '' 	
) 	
Go--#SQL  	

Insert into INT_ND_CFG_Layouts_Columnas Select 16, 1, 'Id Sucursal', 1, 'false', 'A' 
Insert into INT_ND_CFG_Layouts_Columnas Select 16, 1, 'Nombre Sucursal', 2, 'false', 'A' 
Insert into INT_ND_CFG_Layouts_Columnas Select 16, 1, 'Codigo Cliente', 3, 'false', 'A' 
Insert into INT_ND_CFG_Layouts_Columnas Select 16, 1, 'Nombre Cliente', 4, 'false', 'A' 
Go--#SQL  	



