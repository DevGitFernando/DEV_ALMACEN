
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Proveedores_Status' and xType = 'U' ) 
	Drop Table COM_OCEN_Proveedores_Status 
Go--#SQL 

Create Table COM_OCEN_Proveedores_Status
(
	IdProveedor varchar(4) Not Null, 
	ObservacionesOCEN varchar(500) Not Null, 
	ObservacionesProveedor varchar(500) Not Null, 
	FechaRegistro datetime Not Null default getdate(),
	IdPersonal varchar(4) Not Null, 
	StatusOCEN varchar(1) Not Null Default 'A', 
	StatusProveedor varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
)
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Status Add Constraint FK_COM_OCEN_Proveedores_Status_CatProveedores
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL

-----------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Proveedores_Status_Historico' and xType = 'U' ) 
	Drop Table COM_OCEN_Proveedores_Status_Historico 
Go--#SQL 

Create Table COM_OCEN_Proveedores_Status_Historico 
(
	IdProveedor varchar(4) Not Null, 
	ObservacionesOCEN varchar(500) Not Null, 
	ObservacionesProveedor varchar(500) Not Null, 
	FechaRegistro datetime Not Null default getdate(),
	IdPersonal varchar(4) Not Null, 
	StatusOCEN varchar(1) Not Null Default 'A', 
	StatusProveedor varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,
	Keyx int identity(1,1)  
)
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Status_Historico Add Constraint FK_COM_OCEN_Proveedores_Status_Historico_CatProveedores
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL

