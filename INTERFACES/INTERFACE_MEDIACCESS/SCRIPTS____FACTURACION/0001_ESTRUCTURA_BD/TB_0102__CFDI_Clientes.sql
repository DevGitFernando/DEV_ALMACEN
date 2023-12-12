------------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_Direcciones' and xType = 'U' ) 
	Drop Table CFDI_Clientes_Direcciones  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_Telefonos' and xType = 'U' ) 
	Drop Table CFDI_Clientes_Telefonos   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_EMails' and xType = 'U' ) 
	Drop Table CFDI_Clientes_EMails   
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes' and xType = 'U' ) 
	Drop Table CFDI_Clientes 
Go--#SQL 

Create Table CFDI_Clientes 
(
	IdCliente varchar(8) Not Null, 
	Nombre varchar(200) Not Null, 
	RFC varchar(15) Not Null Default '', 	
	NombreComercial varchar(200) Not Null, 	
	FechaRegistro datetime Not Null Default getdate(), 
	TipoDeCliente tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL  

Alter Table CFDI_Clientes Add Constraint PK_CFDI_Clientes Primary Key ( IdCliente ) 
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_Direcciones' and xType = 'U' ) 
	Drop Table CFDI_Clientes_Direcciones  
Go--#SQL 

Create Table CFDI_Clientes_Direcciones 
( 
	IdCliente varchar(8) Not Null, 
	IdDireccion varchar(2) Not Null, 	
	
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

Alter Table CFDI_Clientes_Direcciones Add Constraint PK_CFDI_Clientes_Direcciones Primary Key ( IdCliente, IdDireccion ) 
Go--#SQL 

Alter Table CFDI_Clientes_Direcciones Add Constraint FK_CFDI_Clientes_Direcciones___CFDI_Clientes  
	Foreign Key ( IdCliente ) References CFDI_Clientes ( IdCliente ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_Telefonos' and xType = 'U' ) 
	Drop Table CFDI_Clientes_Telefonos   
Go--#SQL 

Create Table CFDI_Clientes_Telefonos 
( 
	IdCliente varchar(8) Not Null, 
	IdTelefono varchar(2) Not Null, 	
	IdTipoTelefono varchar(4) Not Null, 
	Telefono varchar(20) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  		
) 
Go--#SQL 

Alter Table CFDI_Clientes_Telefonos Add Constraint PK_CFDI_Clientes_Telefonos Primary Key ( IdCliente, IdTelefono ) 
Go--#SQL 

Alter Table CFDI_Clientes_Telefonos Add Constraint FK_CFDI_Clientes_Telefonos___CFDI_Clientes  
	Foreign Key ( IdCliente ) References CFDI_Clientes ( IdCliente ) 
Go--#SQL 

Alter Table CFDI_Clientes_Telefonos Add Constraint FK_CFDI_Clientes_Telefonos___CFDI_TiposTelefonos  
	Foreign Key ( IdTipoTelefono ) References CFDI_TiposTelefonos ( IdTipoTelefono ) 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_EMails' and xType = 'U' ) 
	Drop Table CFDI_Clientes_EMails   
Go--#SQL 

Create Table CFDI_Clientes_EMails 
( 
	IdCliente varchar(8) Not Null, 
	IdEmail varchar(2) Not Null, 	
	IdTipoEMail varchar(4) Not Null, 
	Email varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  		
) 
Go--#SQL 

Alter Table CFDI_Clientes_EMails Add Constraint PK_CFDI_Clientes_EMails Primary Key ( IdCliente, IdEmail ) 
Go--#SQL 

Alter Table CFDI_Clientes_EMails Add Constraint FK_CFDI_Clientes_EMails___CFDI_Clientes  
	Foreign Key ( IdCliente ) References CFDI_Clientes ( IdCliente ) 
Go--#SQL 

Alter Table CFDI_Clientes_EMails Add Constraint FK_CFDI_Clientes_EMails___CFDI_TiposEmail  
	Foreign Key ( IdTipoEMail ) References CFDI_TiposEmail ( IdTipoEMail ) 
Go--#SQL

