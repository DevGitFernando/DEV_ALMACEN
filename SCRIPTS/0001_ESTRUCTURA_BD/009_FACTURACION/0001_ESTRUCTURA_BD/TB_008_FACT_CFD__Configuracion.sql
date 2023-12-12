
----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Clientes' and xType = 'U' ) 
	Drop Table FACT_CFD_Clientes   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Sucursales_Series' and xType = 'U' ) 
	Drop Table FACT_CFD_Sucursales_Series  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_SeriesFolios' and xType = 'U' ) 
	Drop Table FACT_CFD_SeriesFolios   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Certificados' and xType = 'U' ) 
	Drop Table FACT_CFD_Certificados  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Sucursales_Domicilio' and xType = 'U' ) 
	Drop Table FACT_CFD_Sucursales_Domicilio  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Empresas_DomicilioFiscal' and xType = 'U' ) 
	Drop Table FACT_CFD_Empresas_DomicilioFiscal  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Sucursales' and xType = 'U' ) 
	Drop Table FACT_CFD_Sucursales  
Go--#SQL 

-------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFD_Proveedores'  and xType = 'U' ) 
   Drop Table FACT_CFD_Proveedores   
Go--#SQL    

Create Table FACT_CFD_Proveedores 
( 
    IdProveedor varchar(2) Not Null Default '',  
    Nombre varchar(50) Not Null Default '' Unique, 
    DireccionUrl varchar(200) Not Null Default '', 
    Telefonos varchar(100) Not Null Default '',     
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0     --- 100 ==> Actualizado 
) 
Go--#SQL 

Alter Table FACT_CFD_Proveedores Add Constraint PK_FACT_CFD_Proveedores Primary Key ( IdProveedor ) 
Go--#SQL 

--Insert Into FACT_CFD_Proveedores Select '02', 'Tralix-Demo', 'https://demo-partners.xsa.com.mx', '0', 'A', 0  
Go--#SQL 


-------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Empresas' and xType = 'U' ) 
	Drop Table FACT_CFD_Empresas   
Go--#SQL 

Create Table FACT_CFD_Empresas 
( 
	IdEmpresa varchar(3) Not Null, 
	Nombre varchar(200) Not Null, 
	RFC varchar(15) Not Null Unique NonClustered, ---- Asegurar que no se repitan los RFC 
	KeyLicencia varchar(200) Not Null Default '', 	 
    NombreProveedor varchar(50) Not Null Default '', 
    DireccionUrl varchar(200) Not Null Default '', 
    Telefonos varchar(100) Not Null Default '',     	
    Status varchar(1) Not Null Default '', 
    Actualizado smallint Not Null Default 0 
) 
Go--#SQL 

Alter Table FACT_CFD_Empresas Add Constraint PK_FACT_CFD_Empresas Primary Key ( IdEmpresa ) 
Go--#SQL 

Insert FACT_CFD_Empresas 
Select '002', 'PHOENIX FARMACEUTICA S.A. DE C.V.', 'PFA070614LD5', '4585d3f7518f5ffa062fa4e3b4e64831', 'Tralix-Demo', 'https://demo-partners.xsa.com.mx', '0', 'A', 0  
Go--#SQL 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Sucursales' and xType = 'U' ) 
	Drop Table FACT_CFD_Sucursales  
Go--#SQL 

Create Table FACT_CFD_Sucursales 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Nombre varchar(100) Not Null, 
	RFC varchar(15) Not Null, ---- Asegurar que no se repitan los RFC 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 	
) 
Go--#SQL 

Alter Table FACT_CFD_Sucursales Add Constraint PK_FACT_CFD_Sucursales Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table FACT_CFD_Sucursales Add Constraint FK_FACT_CFD_Sucursales___FACT_CFD_Empresas 
	Foreign Key ( IdEmpresa  ) References FACT_CFD_Empresas ( IdEmpresa ) 
Go--#SQL 

Alter Table FACT_CFD_Sucursales Add Constraint FK_FACT_CFD_Sucursales___CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia  ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Empresas_DomicilioFiscal' and xType = 'U' ) 
	Drop Table FACT_CFD_Empresas_DomicilioFiscal  
Go--#SQL  

Create Table FACT_CFD_Empresas_DomicilioFiscal 
(
	IdEmpresa varchar(3) Not Null, 
	Pais varchar(100) Not Null Default '', 
	Estado varchar(100) Not Null Default '', 		
	Municipio varchar(100) Not Null Default '', 
	Localidad varchar(100) Not Null Default '', 	
	Colonia varchar(100) Not Null Default '', 		
	Calle varchar(100) Not Null Default '',  
	NoExterior varchar(8) Not Null Default '',  
	NoInterior varchar(8) Not Null Default '', 
	CodigoPostal varchar(100) Not Null Default '', 
	Referencia varchar(100) Not Null Default '' 
) 	
Go--#SQL

Alter Table FACT_CFD_Empresas_DomicilioFiscal Add Constraint PK_FACT_CFD_Empresas_DomicilioFiscal Primary Key ( IdEmpresa  ) 
Go--#SQL 

Alter Table FACT_CFD_Empresas_DomicilioFiscal Add Constraint FK_FACT_CFD_Empresas_DomicilioFiscal___FACT_CFD_Empresas 
	Foreign Key ( IdEmpresa  ) References FACT_CFD_Empresas ( IdEmpresa ) 
Go--#SQL 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Sucursales_Domicilio' and xType = 'U' ) 
	Drop Table FACT_CFD_Sucursales_Domicilio  
Go--#SQL 

Create Table FACT_CFD_Sucursales_Domicilio 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Pais varchar(100) Not Null Default '', 
	Estado varchar(100) Not Null Default '', 		
	Municipio varchar(100) Not Null Default '', 
	Localidad varchar(100) Not Null Default '', 	
	Colonia varchar(100) Not Null Default '', 		
	Calle varchar(100) Not Null Default '',  
	NoExterior varchar(8) Not Null Default '',  
	NoInterior varchar(8) Not Null Default '', 
	CodigoPostal varchar(100) Not Null Default '', 
	Referencia varchar(100) Not Null Default '' 
) 	
Go--#SQL

Alter Table FACT_CFD_Sucursales_Domicilio Add Constraint PK_FACT_CFD_Sucursales_Domicilio Primary Key ( IdEmpresa, IdEstado, IdFarmacia  ) 
Go--#SQL 


Alter Table FACT_CFD_Sucursales_Domicilio Add Constraint FK_FACT_CFD_Sucursales_Domicilio___FACT_CFD_Sucursales  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia ) References FACT_CFD_Sucursales ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Certificados' and xType = 'U' ) 
	Drop Table FACT_CFD_Certificados  
Go--#SQL 

Create Table FACT_CFD_Certificados 
( 
	IdEmpresa varchar(3) Not Null, 
	NombreCertificado varchar(100) Not Null, 
	Certificado text, 
	ValidoDesde varchar(100) Not Null Default '', 
	ValidoHasta varchar(100) Not Null Default '', 
	FechaInicio datetime Not Null Default getdate(), 
	FechaFinal datetime Not Null Default getdate(), 

	Serie varchar(100) Not Null Default '', 
	Serial varchar(100) Not Null Default '', 	
	
	NombreLlavePrivada varchar(100) Not Null Default '',
	LlavePrivada text,
	PasswordPublico varchar(100) Not Null Default '', 
	
	AvisoVencimiento bit Not Null Default 'false', 
	TiempoAviso int Not Null Default 15, 
	
	Status varchar(1) Not Null Default '', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL  

Alter Table FACT_CFD_Certificados Add Constraint PK_FACT_CFD_Certificados Primary Key ( IdEmpresa ) 
Go--#SQL 

Alter Table FACT_CFD_Certificados Add Constraint FK_FACT_CFD_Certificados___FACT_CFD_Empresas 
	Foreign Key ( IdEmpresa  ) References FACT_CFD_Empresas ( IdEmpresa ) 
Go--#SQL 

------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados' and xType = 'U' ) 
	Drop Table FACT_CFD_Documentos_Generados  
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_TiposDeDocumentos' and xType = 'U' ) 
	Drop Table FACT_CFD_TiposDeDocumentos  
Go--#SQL 

Create Table FACT_CFD_TiposDeDocumentos 
(
	IdTipoDocumento varchar(3) Not Null, 
	NombreDocumento varchar(100) Not Null Default '' Unique, 
	Alias varchar(10) Not Null Default '', 
	TipoDeComprobante smallint Not Null Default 0,  	
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL 

Alter Table FACT_CFD_TiposDeDocumentos Add Constraint PK_FACT_CFD_TiposDeDocumentos Primary Key ( IdTipoDocumento ) 
Go--#SQL 

Insert Into FACT_CFD_TiposDeDocumentos values ( '001', 'Factura', 'FAC', 1, 'A' ) 
Insert Into FACT_CFD_TiposDeDocumentos values ( '002', 'Nota de crédito', 'NCR', 1, 'A' ) 
Insert Into FACT_CFD_TiposDeDocumentos values ( '003', 'Nota de débito', 'NDD', 1, 'A' ) 
Insert Into FACT_CFD_TiposDeDocumentos values ( '004', 'Comprabante de ingresos', 'CDI', 1, 'A' ) 
Insert Into FACT_CFD_TiposDeDocumentos values ( '005', 'Carta porte', 'CPO', 1, 'A' ) 
Insert Into FACT_CFD_TiposDeDocumentos values ( '006', 'Nota de cargo', 'NCC', 1, 'A' ) 
Go--#SQL 

If Not Exists ( Select * From FACT_CFD_TiposDeDocumentos Where IdTipoDocumento = '001' )  Insert Into FACT_CFD_TiposDeDocumentos (  IdTipoDocumento, NombreDocumento, Alias, Status )  Values ( '001', 'Factura', 'FAC', 'A' )    Else Update FACT_CFD_TiposDeDocumentos Set NombreDocumento = 'Factura', Alias = 'FAC', Status = 'A' Where IdTipoDocumento = '001'  
If Not Exists ( Select * From FACT_CFD_TiposDeDocumentos Where IdTipoDocumento = '002' )  Insert Into FACT_CFD_TiposDeDocumentos (  IdTipoDocumento, NombreDocumento, Alias, Status )  Values ( '002', 'Nota de crédito', 'NCR', 'A' )    Else Update FACT_CFD_TiposDeDocumentos Set NombreDocumento = 'Nota de crédito', Alias = 'NCR', Status = 'A' Where IdTipoDocumento = '002'  
If Not Exists ( Select * From FACT_CFD_TiposDeDocumentos Where IdTipoDocumento = '003' )  Insert Into FACT_CFD_TiposDeDocumentos (  IdTipoDocumento, NombreDocumento, Alias, Status )  Values ( '003', 'Nota de débito', 'NDD', 'A' )    Else Update FACT_CFD_TiposDeDocumentos Set NombreDocumento = 'Nota de débito', Alias = 'NDD', Status = 'A' Where IdTipoDocumento = '003'  
If Not Exists ( Select * From FACT_CFD_TiposDeDocumentos Where IdTipoDocumento = '004' )  Insert Into FACT_CFD_TiposDeDocumentos (  IdTipoDocumento, NombreDocumento, Alias, Status )  Values ( '004', 'Comprabante de ingresos', 'CDI', 'A' )    Else Update FACT_CFD_TiposDeDocumentos Set NombreDocumento = 'Comprabante de ingresos', Alias = 'CDI', Status = 'A' Where IdTipoDocumento = '004'  
If Not Exists ( Select * From FACT_CFD_TiposDeDocumentos Where IdTipoDocumento = '005' )  Insert Into FACT_CFD_TiposDeDocumentos (  IdTipoDocumento, NombreDocumento, Alias, Status )  Values ( '005', 'Carta porte', 'CPO', 'A' )    Else Update FACT_CFD_TiposDeDocumentos Set NombreDocumento = 'Carta porte', Alias = 'CPO', Status = 'A' Where IdTipoDocumento = '005'  
If Not Exists ( Select * From FACT_CFD_TiposDeDocumentos Where IdTipoDocumento = '006' )  Insert Into FACT_CFD_TiposDeDocumentos (  IdTipoDocumento, NombreDocumento, Alias, Status )  Values ( '006', 'Nota de cargo', 'NCC', 'A' )    Else Update FACT_CFD_TiposDeDocumentos Set NombreDocumento = 'Nota de cargo', Alias = 'NCC', Status = 'A' Where IdTipoDocumento = '006'  
Go--#SQL 


------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_SeriesFolios' and xType = 'U' ) 
	Drop Table FACT_CFD_SeriesFolios   
Go--#SQL 

Create Table FACT_CFD_SeriesFolios 
( 
	IdEmpresa varchar(3) Not Null,  	
	AñoAprobacion int Not Null Default 0, 
	NumAprobacion int Not Null Default 0, 	
	Serie varchar(10) Not Null Default '', 
	IdTipoDocumento varchar(3) Not Null, 	
	NombreDocumento varchar(50) Not Null Default '', 	
	FolioInicial int Not Null Default 0, 
	FolioFinal int Not Null Default 0, 	
	FolioUtilizado int Not Null Default 0, 

	Status varchar(1) Not Null Default '', 
	Actualizado tinyint Not Null Default 0, 
	IdentificadorSerie int identity(1, 1)  Unique	
) 
Go--#SQL  

Alter Table FACT_CFD_SeriesFolios Add Constraint PK_FACT_CFD_SeriesFolios Primary Key ( IdEmpresa, Serie )  
Go--#SQL 

Alter Table FACT_CFD_SeriesFolios Add Constraint FK_FACT_CFD_SeriesFolios___FACT_CFD_Empresas 
	Foreign Key ( IdEmpresa ) References FACT_CFD_Empresas ( IdEmpresa ) 
Go--#SQL 
 
Alter Table FACT_CFD_SeriesFolios Add Constraint FK_FACT_CFD_SeriesFolios___FACT_CFD_TiposDeDocumentos 
	Foreign Key ( IdTipoDocumento ) References FACT_CFD_TiposDeDocumentos ( IdTipoDocumento ) 
Go--#SQL  
 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Sucursales_Series' and xType = 'U' ) 
	Drop Table FACT_CFD_Sucursales_Series  
Go--#SQL 

Create Table FACT_CFD_Sucursales_Series  
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdentificadorSerie int Not Null, 
	Status varchar(2) Not Null Default 'A', 
) 
Go--#SQL 

Alter Table FACT_CFD_Sucursales_Series Add Constraint PK_FACT_CFD_Sucursales_Series Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdentificadorSerie ) 
Go--#SQL  

Alter Table FACT_CFD_Sucursales_Series Add Constraint FK_FACT_CFD_SeriesFolios___FACT_CFD_Sucursales 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia  ) References FACT_CFD_Sucursales ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table FACT_CFD_Sucursales_Series Add Constraint FACT_CFD_Sucursales_Series___FACT_CFD_SeriesFolios 
	Foreign Key ( IdentificadorSerie  ) References FACT_CFD_SeriesFolios ( IdentificadorSerie ) 
Go--#SQL 

 
------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Clientes' and xType = 'U' ) 
	Drop Table FACT_CFD_Clientes   
Go--#SQL  

Create Table FACT_CFD_Clientes 
(
	IdCliente varchar(6) Not Null, 
	Nombre varchar(100) Not Null, 
	RFC varchar(15) Not Null Unique, 	
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
	Status varchar(1) Not Null Default 'A' 
) 	
Go--#SQL 

Alter Table FACT_CFD_Clientes Add Constraint PK_FACT_CFD_Clientes Primary Key ( IdCliente  ) 
Go--#SQL 
 
