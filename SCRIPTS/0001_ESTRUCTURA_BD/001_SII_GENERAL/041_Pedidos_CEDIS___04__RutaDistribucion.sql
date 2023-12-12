------------------------------------------------------------------------------------------------------------------------------------------------------    
------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__RutaDistribucion_Transferencia' and xType = 'U' ) 
   Drop Table CFGC_ALMN__RutaDistribucion_Transferencia 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__RutaDistribucion_Beneficiario' and xType = 'U' ) 
   Drop Table CFGC_ALMN__RutaDistribucion_Beneficiario
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__RutaDistribucion' and xType = 'U' ) 
   Drop Table CFGC_ALMN__RutaDistribucion 
Go--#SQL   

Create Table CFGC_ALMN__RutaDistribucion
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdRuta varchar(4) Not Null, 
	Descripcion varchar(500) Not Null Default '',
	FechaRegistro DateTime Not Null Default GetDate(),
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL 


Alter Table CFGC_ALMN__RutaDistribucion Add Constraint PK_CFGC_ALMN__RutaDistribucion Primary Key (IdEstado, IdFarmacia, IdRuta ) 
Go--#SQL  

Alter Table CFGC_ALMN__RutaDistribucion Add Constraint FK_CFGC_ALMN__RutaDistribucion___CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL



------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__RutaDistribucion_Beneficiario' and xType = 'U' ) 
   Drop Table CFGC_ALMN__RutaDistribucion_Beneficiario 
Go--#SQL   

Create Table CFGC_ALMN__RutaDistribucion_Beneficiario 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdRuta varchar(4) Not Null, 
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null, 
	IdBeneficiario varchar(8) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL  

Alter Table CFGC_ALMN__RutaDistribucion_Beneficiario Add Constraint PK_CFGC_ALMN__RutaDistribucion_Beneficiario
	Primary Key ( IdEstado, IdFarmacia, IdRuta, IdCliente, IdSubCliente, IdBeneficiario ) 
Go--#SQL  

Alter Table CFGC_ALMN__RutaDistribucion_Beneficiario Add Constraint FK_CFGC_ALMN__RutaDistribucion_Beneficiario___CFGC_ALMN__RutaDistribucion
	Foreign Key ( IdEstado, IdFarmacia, IdRuta ) References CFGC_ALMN__RutaDistribucion ( IdEstado, IdFarmacia, IdRuta ) 
Go--#SQL  

Alter Table CFGC_ALMN__RutaDistribucion_Beneficiario Add Constraint FK_CFGC_ALMN__RutaDistribucion_Beneficiario___CatBeneficiarios
	Foreign Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario )
	References CatBeneficiarios ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 
Go--#SQL  



------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__RutaDistribucion__Beneficiario_Historico' and xType = 'U' ) 
   Drop Table CFGC_ALMN__RutaDistribucion__Beneficiario_Historico 
Go--#SQL   

Create Table CFGC_ALMN__RutaDistribucion__Beneficiario_Historico 
( 
	Keyx int identity( 1, 1 ), 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdRuta varchar(4) Not Null, 
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null, 
	IdBeneficiario varchar(8) Not Null, 
	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL  

Alter Table CFGC_ALMN__RutaDistribucion__Beneficiario_Historico Add Constraint PK_CFGC_ALMN__RutaDistribucion__Beneficiario_Historico
	Primary Key ( IdEstado, IdFarmacia, IdRuta, IdCliente, IdSubCliente, IdBeneficiario, FechaRegistro ) 
Go--#SQL 




------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__RutaDistribucion_Transferencia' and xType = 'U' ) 
   Drop Table CFGC_ALMN__RutaDistribucion_Transferencia 
Go--#SQL   

Create Table CFGC_ALMN__RutaDistribucion_Transferencia
(
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	IdRuta varchar(4) Not Null,
	IdEstadoEnvia varchar(2) Not Null,
	IdFarmaciaEnvia varchar(4) Not Null,
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table CFGC_ALMN__RutaDistribucion_Transferencia Add Constraint PK_CFGC_ALMN__RutaDistribucion_Transferencia
	Primary Key ( IdEstado, IdFarmacia, IdRuta, IdEstadoEnvia, IdFarmaciaEnvia ) 
Go--#SQL

Alter Table CFGC_ALMN__RutaDistribucion_Transferencia Add Constraint FK_CFGC_ALMN__RutaDistribucion_Transferencia___CFGC_ALMN__RutaDistribucion
	Foreign Key ( IdEstado, IdFarmacia, IdRuta )
	References CFGC_ALMN__RutaDistribucion ( IdEstado, IdFarmacia, IdRuta )
Go--#SQL

Alter Table CFGC_ALMN__RutaDistribucion_Transferencia Add Constraint FK_CFGC_ALMN__RutaDistribucion_Transferencia___CatBeneficiarios
	Foreign Key ( IdEstadoEnvia,  IdFarmaciaEnvia )
	References CatFarmacias ( IdEstado, IdFarmacia )
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__RutaDistribucion_Transferencia__Beneficiario_Historico' and xType = 'U' ) 
   Drop Table CFGC_ALMN__RutaDistribucion_Transferencia__Beneficiario_Historico 
Go--#SQL   

Create Table CFGC_ALMN__RutaDistribucion_Transferencia__Beneficiario_Historico
(
	Keyx int identity( 1, 1 ), 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdRuta varchar(4) Not Null,
	IdEstadoEnvia varchar(2) Not Null,
	IdFarmaciaEnvia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

