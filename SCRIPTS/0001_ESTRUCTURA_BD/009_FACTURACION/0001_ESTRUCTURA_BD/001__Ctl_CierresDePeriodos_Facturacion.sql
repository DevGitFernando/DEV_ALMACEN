


------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'Ctl_CierresDePeriodos_Facturacion' and So.xType = 'U' ) 
   Drop Table Ctl_CierresDePeriodos_Facturacion 
Go--#SQL 

Create Table Ctl_CierresDePeriodos_Facturacion 
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

Alter Table Ctl_CierresDePeriodos_Facturacion Add Constraint PK_Ctl_CierresDePeriodos_Facturacion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre )     
Go--#SQL 

Alter Table Ctl_CierresDePeriodos_Facturacion Add Constraint FK_Ctl_CierresDePeriodos_Facturacion_CatEmpresas 
    Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL     

Alter Table Ctl_CierresDePeriodos_Facturacion Add Constraint FK_Ctl_CierresDePeriodos_Facturacion_CatFarmacias  
    Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL     

Alter Table Ctl_CierresDePeriodos_Facturacion Add Constraint FK_Ctl_CierresDePeriodos_Facturacion_CatPersonal  
    Foreign Key ( IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra  ) 
    References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL


