------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasInformacionAdicional' and xType = 'U' ) 
	Drop Table Adt_VentasInformacionAdicional 
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasEnc' and xType = 'U' ) 
	Drop Table Adt_VentasEnc 
Go--#SQL 

Create Table Adt_VentasEnc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null,	  	
	FolioMovto varchar(30) Not Null,	 
	FechaRegistro datetime Default getdate(),	
	IdPersonal varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null,
	TipoDeVenta smallint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table Adt_VentasEnc Add Constraint PK_Adt_VentasEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovto ) 
Go--#SQL 

Alter Table Adt_VentasEnc Add Constraint FK_Adt_VentasEnc_VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta )
Go--#SQL 

----Alter Table Adt_VentasEnc Add Constraint FK_Adt_VentasEnc_CatEmpresas 
----	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
----Go--#SQL 
----
----Alter Table Adt_VentasEnc Add Constraint FK_Adt_VentasEnc_CatFarmacias 
----	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
----Go--#SQL 
----
Alter Table Adt_VentasEnc Add Constraint FK_Adt_VentasEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasInformacionAdicional' and xType = 'U' ) 
	Drop Table Adt_VentasInformacionAdicional 
Go--#SQL  

Create Table Adt_VentasInformacionAdicional  
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null,
	FolioMovto varchar(30) Not Null,
	IdPersonal varchar(4) Not Null,	  	
	IdBeneficiario varchar(8) Not Null, 
	NumReceta varchar(20) Not Null, 
	FechaReceta datetime Not Null default getdate(), 
	IdMedico varchar(6) Not Null, 
	IdBeneficio varchar(4) Not Null, 
	IdDiagnostico varchar(6) Not Null, 
	IdServicio varchar(3) Not Null, 
	IdArea varchar(3) Not Null,  
	RefObservaciones varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table Adt_VentasInformacionAdicional Add Constraint PK_Adt_VentasInformacionAdicional Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovto ) 
Go--#SQL  

Alter Table Adt_VentasInformacionAdicional Add Constraint FK_Adt_VentasInformacionAdicional_VentasInformacionAdicional 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	References VentasInformacionAdicional ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  

----Alter Table Adt_VentasInformacionAdicional Add Constraint FK_Adt_VentasInformacionAdicional_VentasEnc 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
----Go--#SQL  

Alter Table Adt_VentasInformacionAdicional Add Constraint FK_Adt_VentasInformacionAdicional_CatMedicos 
	Foreign Key ( IdEstado, IdFarmacia, IdMedico ) References CatMedicos ( IdEstado, IdFarmacia, IdMedico ) 
Go--#SQL  

Alter Table Adt_VentasInformacionAdicional Add Constraint FK_Adt_VentasInformacionAdicional_CatServicios_Areas  
	Foreign Key ( IdServicio, IdArea ) References CatServicios_Areas ( IdServicio, IdArea ) 
Go--#SQL  

Alter Table Adt_VentasInformacionAdicional Add Constraint FK_Adt_VentasInformacionAdicional_CatBeneficios  
	Foreign Key ( IdBeneficio ) References CatBeneficios ( IdBeneficio ) 
Go--#SQL 
