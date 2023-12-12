----------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CB_Anexos_Miembros' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CB_Anexos_Miembros  
Go--#SQL 


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CB_Anexos' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CB_Anexos 
Go--#SQL 

Create Table INT_ND_CFG_CB_Anexos   
( 
	IdEstado varchar(2) Not Null, 
	IdAnexo varchar(50) Not Null Default '', 
	NombreAnexo varchar(200) Not Null Default '', 
	NombrePrograma varchar(200) Not Null Default '',  
	Prioridad int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A' 	   
) 
Go--#SQL 

Alter Table INT_ND_CFG_CB_Anexos Add Constraint PK_INT_ND_CFG_CB_Anexos Primary Key  ( IdEstado, IdAnexo, Prioridad  ) 
Go--#SQL 


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CB_Anexos_Causes' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CB_Anexos_Causes  
Go--#SQL 

Create Table INT_ND_CFG_CB_Anexos_Causes   
( 
	IdEstado varchar(2) Not Null, 
	IdAnexo varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A' 	   
) 
Go--#SQL 

Alter Table INT_ND_CFG_CB_Anexos_Causes Add Constraint PK_INT_ND_CFG_CB_Anexos_Causes Primary Key  ( IdEstado, IdAnexo ) 
Go--#SQL 


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CB_Anexos_Miembros' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CB_Anexos_Miembros  
Go--#SQL 

Create Table INT_ND_CFG_CB_Anexos_Miembros   
( 
	IdEstado varchar(2) Not Null, 
	CodigoCliente varchar(20) Not Null, 
	IdAnexo varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A'
) 
Go--#SQL 

Alter Table INT_ND_CFG_CB_Anexos_Miembros Add Constraint PK_INT_ND_CFG_CB_Anexos_Miembros Primary Key  ( IdEstado, CodigoCliente, IdAnexo  ) 
Go--#SQL 

----Alter Table INT_ND_CFG_CB_Anexos_Miembros Add Constraint FK_INT_ND_CFG_CB_Anexos_Miembros___INT_ND_CFG_CB_Anexos 
----	Foreign Key ( IdEstado, IdAnexo ) References INT_ND_CFG_CB_Anexos ( IdEstado, IdAnexo ) 
----Go--#SQL 	



----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CB_CuadrosBasicos' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CB_CuadrosBasicos 
Go--#SQL 

Create Table INT_ND_CFG_CB_CuadrosBasicos 
( 
	FechaRegistro datetime Not Null Default getdate(), 
	Keyx int Identity(1, 1), 
	Keyx_Auxiliar int Not Null, 	
	IdEstado varchar(2) Not Null, 
	IdAnexo varchar(50) Not Null Default '', 
	Prioridad int Not Null Default '', 
	ClaveSSA varchar(50) Not Null Default '',		---- Clave SSA del SII 
	ClaveSSA_ND varchar(20) Not Null Default '', 
	ClaveSSA_Mascara varchar(20) Not Null Default '', 
	ManejaIva smallint Not Null Default 0, 
	PrecioVenta numeric(14,4) Not Null Default 0,
	PrecioServicio numeric(14,4) Not Null Default 0, 
	Descripcion_Mascara varchar(max) Not null Default '', 
	
	Lote varchar(20) Not null Default '', 
	ContenidoPaquete int Not Null Default 0, 
	UnidadDeMedida varchar(500) Not Null Default '', 
	Vigencia datetime Not Null Default getdate(), 	 
) 	
Go--#SQL 

----Alter Table INT_ND_CFG_CB_CuadrosBasicos Add Constraint PK_INT_ND_CFG_CB_CuadrosBasicos Primary Key  ( IdEstado  ) 
----Go--#xSxQL 

----Alter Table INT_ND_CFG_CB_CuadrosBasicos Add Constraint PK_INT_ND_CFG_CB_CuadrosBasicos Primary Key ( Keyx_Auxiliar ) 
--------Go--#xSxQL 

--------update T Set Keyx_Auxiliar = Keyx
--------from INT_ND_CFG_CB_CuadrosBasicos T 

--------alter table INT_ND_CFG_CB_CuadrosBasicos add Keyx int identity(1, 1) Not null 
--------alter table INT_ND_CFG_CB_CuadrosBasicos add Keyx_Auxiliar int  Not null Default 0 
