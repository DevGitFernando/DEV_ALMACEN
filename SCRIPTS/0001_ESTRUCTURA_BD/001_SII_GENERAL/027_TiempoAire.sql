Set NoCount On 
Go--#SQL  
--------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Ventas_TiempoAire' And xType = 'U' )
	Drop Table Ventas_TiempoAire
Go--#SQL 

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CatCompaniasTA_Montos' And xType = 'U' )
	Drop Table CatCompaniasTA_Montos 
Go--#SQL  

------------------------------------------------------  
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CatCompaniasTiempoAire' And xType = 'U' )
	Drop Table CatCompaniasTiempoAire
Go--#SQL 

Create Table dbo.CatCompaniasTiempoAire
(
	IdCompania varchar(2) Not Null,
	Descripcion varchar(200) Not Null,
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL 

Alter Table CatCompaniasTiempoAire Add Constraint PK_CatCompaniasTiempoAire Primary Key ( IdCompania )
Go--#SQL 

--------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CatCompaniasTA_Montos' And xType = 'U' )
	Drop Table CatCompaniasTA_Montos 
Go--#SQL 

Create Table CatCompaniasTA_Montos
(
	IdCompania varchar(2) Not Null, 
	IdMonto varchar(2) Not Null, 
	Descripcion varchar(50) Not Null, 
	Monto int Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)

Alter Table CatCompaniasTA_Montos Add Constraint PK_CatCompaniasTA_Montos Primary Key ( IdCompania, IdMonto )
Go--#SQL 

Alter Table CatCompaniasTA_Montos Add Constraint FK_CatCompaniasTA_Montos_CatCompaniasTiempoAire 
	Foreign Key (IdCompania) References CatCompaniasTiempoAire ( IdCompania )
Go--#SQL 


---------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonalTA' and xType = 'U' ) 
   Drop Table CatPersonalTA 
Go--#SQL  

Create Table CatPersonalTA 
( 	
	IdPersonal varchar(4) Not Null, 
	Nombre varchar(100) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatPersonalTA Add Constraint PK_CatPersonalTA Primary Key ( IdPersonal )
Go--#SQL  

Insert Into CatPersonalTA ( IdPersonal, Nombre ) Values ( '0001', 'PUBLICO GENERAL' ) 
Go--#SQL 


--------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Ventas_TiempoAire' And xType = 'U' )
	Drop Table Ventas_TiempoAire
Go--#SQL 

Create Table Ventas_TiempoAire
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdCompania varchar(2) Not Null, 
	IdFolioTiempoAire varchar(8) Not Null, 
    FechaSistema datetime Not Null Default getdate(), 
    
     		
	IdMonto varchar(2) Not Null, 	
	Monto int Not Null,   -- Referencia a la Tabla de Montos por Compañia de TA 
	
	TipoTA tinyint Not Null Default 1,  -- 	1 ==> Publico General, 2 ==> Personal de Intermed 
	IdPersonalTA varchar(4) Not Null Default '', 

	NumeroCelular varchar(50) Not Null, 
	IdPersonal varchar(4) Not Null,          --- Personal de Farmacia que registra la Venta TA 
	IdPersonalAutoriza varchar(4) Not Null,  --- Personal de Farmacia que autoriza la Venta TA para Personal de Intermed 

	Corte tinyint Not Null default 0,  	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table Ventas_TiempoAire Add Constraint PK_Ventas_TiempoAire Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdCompania, IdFolioTiempoAire )
Go--#SQL  

Alter Table Ventas_TiempoAire Add Constraint FK_Ventas_TiempoAire_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL  

Alter Table Ventas_TiempoAire Add Constraint FK_Ventas_TiempoAire_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table Ventas_TiempoAire Add Constraint FK_Ventas_TiempoAire_CatCompaniasTA_Montos 
	Foreign Key ( IdCompania, IdMonto ) References CatCompaniasTA_Montos ( IdCompania, IdMonto ) 
Go--#SQL  	 

Alter Table Ventas_TiempoAire Add Constraint FK_Ventas_TiempoAire_CatPersonalTA  
	Foreign Key ( IdPersonalTA ) References CatPersonalTA ( IdPersonal ) 
Go--#SQL  

Alter Table Ventas_TiempoAire Add Constraint FK_Ventas_TiempoAire_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

Alter Table Ventas_TiempoAire Add Constraint FK_Ventas_TiempoAire_CatPersonal_Autoriza  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonalAutoriza ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

