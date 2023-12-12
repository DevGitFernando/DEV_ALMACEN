------------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSociosComerciales_Sucursales' and xType = 'U' ) 
	Drop Table CatSociosComerciales_Sucursales  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSociosComercialesTelefonos' and xType = 'U' ) 
	Drop Table CatSociosComercialesTelefonos   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSociosComercialesEMails' and xType = 'U' ) 
	Drop Table CatSociosComercialesEMails   
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSociosComerciales' and xType = 'U' ) 
	Drop Table CatSociosComerciales 
Go--#SQL 

Create Table CatSociosComerciales 
(
	IdSocioComercial varchar(8) Not Null, 
	Nombre varchar(200) Not Null, 
	RFC varchar(15) Not Null Default '', 	
	NombreComercial varchar(200) Not Null, 	
	FechaRegistro datetime Not Null Default getdate(), 
	
	Pais varchar(100) Not Null, 
	IdEstado varchar(2) Not Null,  
	IdMunicipio varchar(4) Not Null, 
	IdColonia varchar(4) Not Null, 		
	Calle varchar(200) Not Null, 
	NumeroExterior varchar(20) Not Null, 
	NumeroInterior varchar(20) Not Null, 	
	CodigoPostal varchar(10) Not Null, 		
	Referencia varchar(100) Not Null, 
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL  

Alter Table CatSociosComerciales Add Constraint PK_SociosComerciales Primary Key ( IdSocioComercial ) 
Go--#SQL  

Alter Table CatSociosComerciales Add Constraint FK_SociosComerciales___CatColonias  
	Foreign Key ( IdEstado, IdMunicipio, IdColonia ) References CatColonias ( IdEstado, IdMunicipio, IdColonia ) 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSociosComerciales_Sucursales' and xType = 'U' ) 
	Drop Table CatSociosComerciales_Sucursales  
Go--#SQL 

Create Table CatSociosComerciales_Sucursales 
( 
	IdSocioComercial varchar(8) Not Null, 
	IdSucursal varchar(8) Not Null, 
	NombreSucursal varchar(200) Not Null, 
	
	Pais varchar(100) Not Null, 
	IdEstado varchar(2) Not Null,  
	IdMunicipio varchar(4) Not Null, 
	IdColonia varchar(4) Not Null, 		
	Calle varchar(200) Not Null, 
	NumeroExterior varchar(20) Not Null, 
	NumeroInterior varchar(20) Not Null, 	
	CodigoPostal varchar(10) Not Null, 		
	Referencia varchar(100) Not Null, 	
	Telefonos varchar(100) Not Null Default '', 
	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  		
) 
Go--#SQL 

Alter Table CatSociosComerciales_Sucursales Add Constraint PK_SociosComerciales_Sucursales Primary Key ( IdSocioComercial, IdSucursal ) 
Go--#SQL 

Alter Table CatSociosComerciales_Sucursales Add Constraint FK_SociosComerciales_Sucursales___SociosComerciales  
	Foreign Key ( IdSocioComercial ) References CatSociosComerciales ( IdSocioComercial ) 
Go--#SQL 

Alter Table CatSociosComerciales_Sucursales Add Constraint FK_SociosComerciales_Sucursales___CatColonias  
	Foreign Key ( IdEstado, IdMunicipio, IdColonia ) References CatColonias ( IdEstado, IdMunicipio, IdColonia ) 
Go--#SQL 





---------------------------------------------------------------------------------------------------------------------------- 
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'SociosComercialesTelefonos' and xType = 'U' ) 
----	Drop Table SociosComercialesTelefonos   
----Go--#xSQL 

----Create Table SociosComercialesTelefonos 
----( 
----	IdSocioComercial varchar(8) Not Null, 
----	IdTelefono varchar(2) Not Null, 	
----	IdTipoTelefono varchar(4) Not Null, 
----	Telefono varchar(20) Not Null Default '', 
----	Status varchar(1) Not Null Default 'A', 
----	Actualizado tinyint Not Null Default 0  		
----) 
----Go--#xSQL 

----Alter Table SociosComercialesTelefonos Add Constraint PK_SociosComercialesTelefonos Primary Key ( IdSocioComercial, IdTelefono ) 
----Go--#xSQL 

----Alter Table SociosComercialesTelefonos Add Constraint FK_SociosComercialesTelefonos___SociosComerciales  
----	Foreign Key ( IdSocioComercial ) References SociosComerciales ( IdSocioComercial ) 
----Go--#xSQL 

----Alter Table SociosComercialesTelefonos Add Constraint FK_SociosComercialesTelefonos___CFDI_TiposTelefonos  
----	Foreign Key ( IdTipoTelefono ) References CFDI_TiposTelefonos ( IdTipoTelefono ) 
----Go--#xSQL 



---------------------------------------------------------------------------------------------------------------------------- 
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'SociosComercialesEMails' and xType = 'U' ) 
----	Drop Table SociosComercialesEMails   
----Go--#xSQL 

----Create Table SociosComercialesEMails 
----( 
----	IdSocioComercial varchar(8) Not Null, 
----	IdEmail varchar(2) Not Null, 	
----	IdTipoEMail varchar(4) Not Null, 
----	Email varchar(100) Not Null Default '', 
----	Status varchar(1) Not Null Default 'A', 
----	Actualizado tinyint Not Null Default 0  		
----) 
----Go--#xSQL 

----Alter Table SociosComercialesEMails Add Constraint PK_SociosComercialesEMails Primary Key ( IdSocioComercial, IdEmail ) 
----Go--#xSQL 

----Alter Table SociosComercialesEMails Add Constraint FK_SociosComercialesEMails___SociosComerciales  
----	Foreign Key ( IdSocioComercial ) References SociosComerciales ( IdSocioComercial ) 
----Go--#xSQL 

----Alter Table SociosComercialesEMails Add Constraint FK_SociosComercialesEMails___CFDI_TiposEmail  
----	Foreign Key ( IdTipoEMail ) References CFDI_TiposEmail ( IdTipoEMail ) 
----Go--#xSQL

