--------------------------------------------------------------------------------------------------     
--------------------------------------------------------------------------------------------------     
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFarmacias' and xType = 'U' ) 
----   Drop Table CatFarmacias 
----Go--#xSQL 



--------------------------------------------------------------------------------------------------     
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatEstados' and xType = 'U' ) 
----   Drop Table CatEstados 
----Go--#SQL 

----Create Table CatEstados  --- PLAZAS 
----(
----	IdEstado varchar(2) Not Null Default '', 
----	Nombre varchar(100) Not Null Default '' Unique, 
----	ClaveRENAPO varchar(2) Not Null Default '', 
----	Status varchar(1) Not Null Default 'A', 
----	Actualizado tinyint Not Null Default 0  
----)
----Go--#xSQL 
    
----Alter Table CatEstados Add Constraint PK_CatEstados Primary key ( IdEstado ) 
----Go--#xSQL 

--------------------------------------------------------------------------------------------------     
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFarmacias' and xType = 'U' ) 
----   Drop Table CatFarmacias 
----Go--#SQL 

----Create Table CatFarmacias  --- PLAZAS 
----(
----	IdEstado varchar(2) Not Null Default '', 
----	IdFarmacia varchar(4) Not Null Default '', 	
----	NombreFarmacia varchar(100) Not Null Default '', 
----	Status varchar(1) Not Null Default 'A', 
----	Actualizado tinyint Not Null Default 0  
----)
----Go--#xSQL 
    
----Alter Table CatFarmacias Add Constraint PK_CatFarmacias Primary key ( IdEstado, IdFarmacia ) 
----Go--#SQL 

----Alter Table CatFarmacias Add Constraint FK_CatFarmacias_CatEstados 
----	Foreign key ( IdEstado ) References CatEstados ( IdEstado ) 
----Go--#xSQL 
    
----------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatPuestos' and xType = 'U' ) 
   Drop Table CatPuestos 
Go--#SQL 

Create Table CatPuestos  --- PLAZAS 
(
	IdPuesto varchar(3) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatPuestos Add Constraint PK_CatPuestos Primary key ( IdPuesto ) 
Go--#SQL     


----------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatDepartamentos' and xType = 'U' ) 
   Drop Table CatDepartamentos 
Go--#SQL 

Create Table CatDepartamentos  --- PLAZAS 
(
	IdDepartamento varchar(3) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatDepartamentos Add Constraint PK_CatDepartamentos Primary key ( IdDepartamento ) 
Go--#SQL     

----------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatEscolaridades' and xType = 'U' ) 
   Drop Table CatEscolaridades  
Go--#SQL 

Create Table CatEscolaridades  --- PLAZAS 
(
	IdEscolaridad varchar(2) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatEscolaridades Add Constraint PK_CatEscolaridades Primary key ( IdEscolaridad ) 
Go--#SQL   

----------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatEdades' and xType = 'U' ) 
   Drop Table CatEdades  
Go--#SQL 

Create Table CatEdades  --- PLAZAS 
(
	IdEdad varchar(2) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatEdades Add Constraint PK_CatEdades Primary key ( IdEdad ) 
Go--#SQL   


----------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatTipoContrato' and xType = 'U' ) 
   Drop Table CatTipoContrato   
Go--#SQL 

Create Table CatTipoContrato  
(
	IdTipoContrato varchar(2) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatTipoContrato Add Constraint PK_CatTipoContrato Primary key ( IdTipoContrato ) 
Go--#SQL   


----------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatAntiguedades' and xType = 'U' ) 
   Drop Table CatAntiguedades   
Go--#SQL 

Create Table CatAntiguedades  --- PLAZAS 
(
	IdAntiguedad varchar(2) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatAntiguedades Add Constraint PK_CatAntiguedades Primary key ( IdAntiguedad ) 
Go--#SQL   
