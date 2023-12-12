----------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_CartasFaltantes_Detalles' and xType = 'U' ) 
   Drop Table COM_CartasFaltantes_Detalles 
Go--#SQL


----------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_CartasFaltantes' and xType = 'U' ) 
   Drop Table COM_CartasFaltantes 
Go--#SQL 

Create Table COM_CartasFaltantes 
(
	Folio varchar(8) Not Null, 
	IdProveedor varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default GetDate(), 
	Observaciones varchar(200) Not Null Default '', 
	Documento text default '', -------
	NombreDocto varchar(200) Default '', 
	Status varchar(1) Not Null Default 'A',	
	Actualizado tinyint Not Null Default 0				
) 
Go--#SQL 

Alter Table COM_CartasFaltantes Add Constraint PK_COM_CartasFaltantes Primary Key ( Folio ) 
Go--#SQL 


----------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_CartasFaltantes_Detalles' and xType = 'U' ) 
   Drop Table COM_CartasFaltantes_Detalles 
Go--#SQL 

Create Table COM_CartasFaltantes_Detalles 
(
	Folio varchar(8) Not Null,
	ClaveSSA varchar(30) Not Null,
	Status varchar(1) Not Null Default 'A',	
	Actualizado tinyint Not Null Default 0				
) 
Go--#SQL 

	Alter Table COM_CartasFaltantes_Detalles Add Constraint PK_COM_CartasFaltantes_Detalles 
	Primary Key ( Folio, ClaveSSA ) 
Go--#SQL 

Alter Table COM_CartasFaltantes_Detalles Add Constraint FK_COM_CartasFaltantes_Detalles_COM_CartasFaltantes 
	Foreign Key ( Folio ) References COM_CartasFaltantes ( Folio ) 
Go--#SQL


---------------------------------------------------------------------------------------------------------------     
---------------------------  Datos que se envian a la farmacia 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_CartasFaltantes_ClavesSSA' and xType = 'U' ) 
   Drop Table COM_CartasFaltantes_ClavesSSA 
Go--#SQL 

Create Table COM_CartasFaltantes_ClavesSSA 
(	
	ClaveSSA varchar(30) Not Null, 
	Status varchar(2) Not Null Default 'A',	
	Actualizado tinyint Not Null Default 0				
) 
Go--#SQL 

	Alter Table COM_CartasFaltantes_ClavesSSA Add Constraint PK_COM_CartasFaltantes_ClavesSSA 
	Primary Key ( ClaveSSA ) 
Go--#SQL 


