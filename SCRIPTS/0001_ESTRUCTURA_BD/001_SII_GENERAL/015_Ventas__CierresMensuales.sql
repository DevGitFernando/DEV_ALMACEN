------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'Ctl_CierresDePeriodos' and So.xType = 'U' ) 
   Drop Table Ctl_CierresDePeriodos 
Go--#SQL 

Create Table Ctl_CierresDePeriodos 
( 
    IdEmpresa varchar(3) Not Null, 
    IdEstado varchar(2) Not Null, 
    IdFarmacia varchar(4) Not Null, 
    FolioCierre Int Not Null, 
    IdPersonal varchar(4) Not Null Default '', 
    IdEstadoRegistra varchar(2) Not Null Default '', 
    IdFarmaciaRegistra varchar(4) Not Null Default '', 
    IdPersonalRegistra varchar(4) Not Null Default '',    
    FechaCorte datetime Not Null Default getdate(), 
    FechaRegistro datetime Not Null Default getdate(), 
    FechaInicial datetime Not Null Default getdate(), 
    FechaFinal datetime Not Null Default getdate(), 
    Status varchar(1) Not Null Default '', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Ctl_CierresDePeriodos Add Constraint PK_Ctl_CierresDePeriodos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre )     
Go--#SQL 

Alter Table Ctl_CierresDePeriodos Add Constraint FK_Ctl_CierresDePeriodos_CatEmpresas 
    Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL     

Alter Table Ctl_CierresDePeriodos Add Constraint FK_Ctl_CierresDePeriodos_CatFarmacias  
    Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL     

Alter Table Ctl_CierresDePeriodos Add Constraint FK_Ctl_CierresDePeriodos_CatPersonal  
    Foreign Key ( IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra  ) 
    References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL     

------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresPeriodosDetalles' and xType = 'U' ) 
	Drop Table Ctl_CierresPeriodosDetalles 
Go--#SQL  

Create Table Ctl_CierresPeriodosDetalles 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null,  
	FolioCierre Int Not Null, 

	IdCliente varchar(4) Not Null, 
	Cliente varchar(200) Not Null,  
	IdSubCliente varchar(4) Not Null, 	
	SubCliente varchar(200) Not Null,  		
	IdPrograma varchar(4) Not Null, 
	Programa varchar(200) Not Null,  
	IdSubPrograma varchar(4) Not Null, 	
	SubPrograma varchar(200) Not Null,  				
	
	AñoRegistro Int Not Null Default 0, 	
	MesRegistro Int Not Null Default 0,		
	AñoReceta Int Not Null Default 0, 
	MesReceta Int Not Null Default 0, 
	Tickets Int Not Null Default 0, 
	Piezas Numeric(14,4) Not Null Default 0, 
	Monto Numeric(14,4) Not Null Default 0, 	
	ValesEmitidos Int Not Null Default 0, 
	ValesRegistrados Int Not Null Default 0, 
	Efectividad Numeric(14,4) Not Null Default 0, 
	Perdida Numeric(14,4) Not Null Default 0,
	FechaRegistro DateTime Default GetDate(),
	TipoInventario int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table Ctl_CierresPeriodosDetalles Add Constraint PK_Ctl_CierresPeriodosDetalles 
	Primary Key 
	( 
	    IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCierre, 
	    IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
	    AñoRegistro, MesRegistro, AñoReceta, MesReceta, TipoInventario  
	 ) 
Go--#SQL 

Alter Table Ctl_CierresPeriodosDetalles Add Constraint FK_Ctl_CierresPeriodosDetalles_Ctl_CierresDePeriodos 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre ) 
	References Ctl_CierresDePeriodos ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresPeriodosDetalles_Historico' and xType = 'U' ) 
	Drop Table Ctl_CierresPeriodosDetalles_Historico 
Go--#SQL  

Create Table Ctl_CierresPeriodosDetalles_Historico 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null,  
	FolioCierre Int Not Null, 
	
	IdCliente varchar(4) Not Null, 
	Cliente varchar(200) Not Null,  
	IdSubCliente varchar(4) Not Null, 	
	SubCliente varchar(200) Not Null,  		
	IdPrograma varchar(4) Not Null, 
	Programa varchar(200) Not Null,  
	IdSubPrograma varchar(4) Not Null, 	
	SubPrograma varchar(200) Not Null,  					
	
	AñoRegistro Int Not Null Default 0, 	
	MesRegistro Int Not Null Default 0,		
	AñoReceta Int Not Null Default 0, 
	MesReceta Int Not Null Default 0, 
	Tickets Int Not Null Default 0, 
	Piezas Numeric(14,4) Not Null Default 0, 
	Monto Numeric(14,4) Not Null Default 0, 	
	ValesEmitidos Int Not Null Default 0, 
	ValesRegistrados Int Not Null Default 0, 
	Efectividad Numeric(14,4) Not Null Default 0, 
	Perdida Numeric(14,4) Not Null Default 0,
	FechaRegistro DateTime Default GetDate(),
	FechaUpdate DateTime Default GetDate(),
	TipoInventario int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 
