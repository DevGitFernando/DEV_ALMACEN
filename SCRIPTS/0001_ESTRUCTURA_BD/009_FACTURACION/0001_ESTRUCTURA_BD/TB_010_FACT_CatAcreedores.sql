


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CatAcreedores' and xType = 'U' ) 
	Drop Table FACT_CatAcreedores   
Go--#SQL  

Create Table FACT_CatAcreedores 
(
	IdEstado varchar(2) Not Null,
	IdAcreedor varchar(4) Not Null, 
	Nombre varchar(100) Not Null, 
	RFC varchar(15) Not Null Default '', 	
	Pais varchar(100) Not Null Default '', 
	Estado varchar(100) Not Null Default '', 		
	Municipio varchar(100) Not Null Default '', 
	Localidad varchar(100) Not Null Default '', 	
	Colonia varchar(100) Not Null Default '', 		
	Calle varchar(100) Not Null Default '',  
	NoExterior varchar(8) Not Null Default '',  
	NoInterior varchar(8) Not Null Default '', 
	CodigoPostal varchar(100) Not Null Default '', 
	Referencia varchar(100) Not Null Default '', 
	Email varchar(100) Not Null Default '', 	
	Email_Interno varchar(100) Not Null Default '', 	
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 	
Go--#SQL 

Alter Table FACT_CatAcreedores Add Constraint PK_FACT_CatAcreedores Primary Key ( IdEstado, IdAcreedor  ) 
Go--#SQL 

Alter Table FACT_CatAcreedores Add Constraint FK_FACT_CatAcreedores_CatEstados 
	Foreign Key ( IdEstado  ) References CatEstados ( IdEstado ) 
Go--#SQL