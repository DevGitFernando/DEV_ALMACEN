----------------------------------------------------------------------------------------------------------------------------   
If Not Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'Ctl_CierresGeneral' and So.xType = 'U' ) 
Begin 
	Create Table Ctl_CierresGeneral 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		FolioCierre Int Not Null, 
		IdPersonal varchar(4) Not Null Default '', 
		IdEstadoRegistra varchar(2) Not Null Default '', 
		IdFarmaciaRegistra varchar(4) Not Null Default '', 
		IdPersonalRegistra varchar(4) Not Null Default '',     
		FechaRegistro datetime Not Null Default getdate(), 
		FechaInicial datetime Not Null Default getdate(), 
		FechaFinal datetime Not Null Default getdate(), 
		Status varchar(1) Not Null Default '', 
		Actualizado tinyint Not Null Default 0,
		FechaControl datetime Not Null Default getdate() 
	) 

	Alter Table Ctl_CierresGeneral Add Constraint PK_Ctl_CierresGeneral Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre )     

	Alter Table Ctl_CierresGeneral Add Constraint FK_Ctl_CierresGeneral_CatEmpresas 
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
		
	Alter Table Ctl_CierresGeneral Add Constraint FK_Ctl_CierresGeneral_CatFarmacias  
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

	Alter Table Ctl_CierresGeneral Add Constraint FK_Ctl_CierresGeneral_CatPersonal  
		Foreign Key ( IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra  ) 
		References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 

End 
Go--#SQL     

------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresGeneralDetalles' and xType = 'U' ) 
Begin 
	Create Table Ctl_CierresGeneralDetalles 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,	  
		FolioCierre Int Not Null, 	 
		IdTipoMovto_Inv varchar(6) Not Null, 
		IdClaveSSA varchar(4) Not Null Default '', 
		Claves int Not Null Default 0, 
		Piezas int Not Null Default 0, 
		Importe_Licitacion numeric(14, 4) Not Null Default 0, 
		Importe_Inventario numeric(14, 4) Not Null Default 0, 

		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0,
		FechaControl datetime Not Null Default getdate() 
	)

	Alter Table Ctl_CierresGeneralDetalles Add Constraint PK_Ctl_CierresGeneralDetalles 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre, IdTipoMovto_Inv ) 

	Alter Table Ctl_CierresGeneralDetalles Add Constraint FK_Ctl_CierresGeneralDetalles_Ctl_CierresGeneral 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre ) 
		References Ctl_CierresGeneral ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre ) 
		
End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresGeneralDet_Existencia' and xType = 'U' ) 
Begin 
	Create Table Ctl_CierresGeneralDet_Existencia 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,	  
		FolioCierre Int Not Null, 	
		TipoInsumo int Not Null Default 0,  --- 1 = Med_SP , 2 = Med_No_SP, 3 = Mat. Curacion
		Claves_Venta int Not Null Default 0,
		Claves_Consigna int Not Null Default 0,
		Existencia_Venta int Not Null Default 0,
		Existencia_Consigna int Not Null Default 0,
		Importe_Venta numeric(14, 4) Not Null Default 0,
		Importe_Consigna numeric(14, 4) Not Null Default 0,
		Importe_Licitacion numeric(14, 4) Not Null Default 0,
		Importe_Inventario numeric(14, 4) Not Null Default 0,
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0,
		FechaControl datetime Not Null Default getdate() 
	)

	Alter Table Ctl_CierresGeneralDet_Existencia Add Constraint PK_Ctl_CierresGeneralDet_Existencia 
		Primary Key  ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre, TipoInsumo ) 

End 
Go--#SQL 