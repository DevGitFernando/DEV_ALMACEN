If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Proveedores_Sancionados' and xType = 'U' ) 
   Drop Table COM_OCEN_Proveedores_Sancionados 
Go--#SQL 

Create Table COM_OCEN_Proveedores_Sancionados 
(
	IdProveedor varchar(4) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	Motivo varchar(500) Not Null Default '', 
	FechaSancion datetime Not Null Default getdate()  
) 
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Sancionados Add Constraint PK_COM_OCEN_Proveedores_Sancionados 
	Primary Key ( IdProveedor, FechaSancion ) 
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Sancionados Add Constraint FK_COM_OCEN_Proveedores_Sancionados__CatProveedores  
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Proveedores_Sancionados_Activos' and xType = 'U' ) 
   Drop Table COM_OCEN_Proveedores_Sancionados_Activos 
Go--#SQL 

Create Table COM_OCEN_Proveedores_Sancionados_Activos  
(
	IdProveedor varchar(4) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	Motivo varchar(500) Not Null Default '', 
	FechaActivacion datetime Not Null Default getdate()  
) 
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Sancionados_Activos Add Constraint PK_COM_OCEN_Proveedores_Sancionados_Activos 
	Primary Key ( IdProveedor, FechaActivacion ) 
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Sancionados_Activos Add Constraint FK_COM_OCEN_Proveedores_Sancionados_Activos__CatProveedores  
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------------------------  
---------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Proveedores_Sancionados_Historico' and xType = 'U' ) 
   Drop Table COM_OCEN_Proveedores_Sancionados_Historico  
Go--#SQL 

Create Table COM_OCEN_Proveedores_Sancionados_Historico 
(
	IdProveedor varchar(4) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	Motivo varchar(500) Not Null Default '', 
	FechaSancion datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(), 
	Keyx int identity(1, 1) 
) 
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Sancionados_Historico Add Constraint PK_COM_COM_OCEN_Proveedores_Sancionados_Historico 
	Primary Key ( IdProveedor, FechaSancion ) 
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Sancionados_Historico Add Constraint FK_COM_OCEN_Proveedores_Sancionados_Historico__CatProveedores  
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL     
