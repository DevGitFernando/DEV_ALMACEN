------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TiposTelefonos' and xType = 'U' ) 
	Drop Table CFDI_TiposTelefonos   
Go--#SQL 

Create Table CFDI_TiposTelefonos  
(
	IdTipoTelefono varchar(4) Not Null, 
	Descripcion varchar(100) Not Null Unique, 
	Actualizado tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL  

Alter Table CFDI_TiposTelefonos Add Constraint PK_CFDI_TiposTelefonos Primary Key ( IdTipoTelefono ) 
Go--#SQL  

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TiposEmail' and xType = 'U' ) 
	Drop Table CFDI_TiposEmail   
Go--#SQL 

Create Table CFDI_TiposEmail  
(
	IdTipoEMail varchar(4) Not Null, 
	Descripcion varchar(100) Not Null Unique, 
	Actualizado tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL  

Alter Table CFDI_TiposEmail Add Constraint PK_CFDI_TiposEmail Primary Key ( IdTipoEMail ) 
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_ProveedoresEMail' and xType = 'U' ) 
	Drop Table CFDI_ProveedoresEMail  
Go--#SQL 

Create Table CFDI_ProveedoresEMail  
(
	IdProveedorEMail varchar(4) Not Null, 
	Descripcion varchar(100) Not Null Unique, 
	Actualizado tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL  

Alter Table CFDI_ProveedoresEMail Add Constraint PK_CFDI_ProveedoresEMail Primary Key ( IdProveedorEMail ) 
Go--#SQL  

