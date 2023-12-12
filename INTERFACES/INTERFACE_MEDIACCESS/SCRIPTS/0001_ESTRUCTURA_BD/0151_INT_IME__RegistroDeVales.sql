Set NoCount On 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_IME__RegistroDeVales_003_Surtidos' and xType = 'U' ) 
   Drop Table INT_IME__RegistroDeVales_003_Surtidos 
Go--#SQL    

--------------------------------------------------------------------------------------------------------------------------------------------  
--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_IME__RegistroDeVales_002_Claves' and xType = 'U' ) 
   Drop Table INT_IME__RegistroDeVales_002_Claves 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_IME__RegistroDeVales_001_Encabezado' and xType = 'U' ) 
   Drop Table INT_IME__RegistroDeVales_001_Encabezado  
Go--#SQL   

Create Table INT_IME__RegistroDeVales_001_Encabezado 
( 
	Folio varchar(12) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 	

	IdSocioComercial varchar(8) Not Null, 
	IdSucursal varchar(8) Not Null, 		
	Folio_Vale varchar(30) Not Null Default '',  
	FechaEmision_Vale datetime Not Null Default getdate(), 	
	EsValeManual bit Not Null Default 0, 

	Surtido smallint Not Null Default 0, 
	FechaSurtido datetime Not Null Default getdate(), 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	IdPersonal varchar(4) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_IME__RegistroDeVales_001_Encabezado 
	Add Constraint PK_INT_IME__RegistroDeVales_001_Encabezado Primary Key ( IdSocioComercial, IdSucursal, Folio_Vale ) --, Elegibilidad ) 
Go--#SQL   

Alter Table INT_IME__RegistroDeVales_001_Encabezado 
	Add Constraint FK_INT_IME__RegistroDeVales_001_Encabezado___CatEmpresas Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL   

Alter Table INT_IME__RegistroDeVales_001_Encabezado 
	Add Constraint FK_INT_IME__RegistroDeVales_001_Encabezado___CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL

Alter Table INT_IME__RegistroDeVales_001_Encabezado 
	Add Constraint FK_INT_IME__RegistroDeVales_001_Encabezado___CatSociosComerciales_Sucursales
	Foreign Key ( IdSocioComercial, IdSucursal ) References CatSociosComerciales_Sucursales ( IdSocioComercial, IdSucursal ) 
Go--#SQL   



--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_IME__RegistroDeVales_002_Claves' and xType = 'U' ) 
   Drop Table INT_IME__RegistroDeVales_002_Claves 
Go--#SQL 

Create Table INT_IME__RegistroDeVales_002_Claves 
( 
	IdSocioComercial varchar(8) Not Null, 
	IdSucursal varchar(8) Not Null, 		
	Folio_Vale varchar(30) Not Null Default '',  
	Partida int Not Null Default 0, 
	ClaveSSA varchar(30) Not Null Default '',  
	CantidadSolicitada int Not Null Default 0, 
	CantidadSurtida int Not Null Default 0 
)
Go--#SQL   

Alter Table INT_IME__RegistroDeVales_002_Claves 
	Add Constraint PK_INT_IME__RegistroDeVales_002_Claves Primary Key ( IdSocioComercial, IdSucursal, Folio_Vale, ClaveSSA ) 
Go--#SQL   

Alter Table INT_IME__RegistroDeVales_002_Claves 
	Add Constraint FK_INT_IME__RegistroDeVales_002_Claves____INT_IME__RegistroDeVales_001_Encabezado  
	Foreign Key ( IdSocioComercial, IdSucursal, Folio_Vale )  
	References INT_IME__RegistroDeVales_001_Encabezado ( IdSocioComercial, IdSucursal, Folio_Vale ) 
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_IME__RegistroDeVales_003_Surtidos' and xType = 'U' ) 
   Drop Table INT_IME__RegistroDeVales_003_Surtidos 
Go--#SQL 

Create Table INT_IME__RegistroDeVales_003_Surtidos
( 
	IdSocioComercial varchar(8) Not Null, 
	IdSucursal varchar(8) Not Null, 		
	Folio_Vale varchar(30) Not Null Default '',  
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '',
	FolioVenta Varchar(30) Not Null Default ''
)
Go--#SQL   

Alter Table INT_IME__RegistroDeVales_003_Surtidos 
	Add Constraint PK_INT_IME__RegistroDeVales_003_Surtidos Primary Key ( IdSocioComercial, IdSucursal, Folio_Vale, IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL   

Alter Table INT_IME__RegistroDeVales_003_Surtidos 
	Add Constraint FK_INT_IME__RegistroDeVales_003_Surtidos____VentasEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta )
	References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL   

