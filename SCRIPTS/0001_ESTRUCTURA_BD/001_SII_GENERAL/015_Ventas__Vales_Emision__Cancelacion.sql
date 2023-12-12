If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Cancelacion' and xType = 'U' ) 
	Drop Table Vales_Cancelacion
Go--#SQL  

Create Table Vales_Cancelacion 
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	FolioCancelacionVale varchar(30) Not Null,
	RefFolioVale varchar(30) Not Null,
	FechaSistema datetime Default getdate(),
	FechaRegistro  datetime Default getdate(),
	IdPersonal varchar(4) Not Null,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  


Alter Table Vales_Cancelacion Add Constraint PK_Vales_Cancelacion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCancelacionVale )
Go--#SQL  

Alter Table Vales_Cancelacion Add Constraint FK_Vales_Cancelacion_Vales_EmisionEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, RefFolioVale ) References Vales_EmisionEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVale )
Go--#SQL 

Alter Table Vales_Cancelacion Add Constraint FK_Vales_Cancelacion 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )
Go--#SQL 

Alter Table Vales_Cancelacion Add Constraint FK_Vales_Cancelacion_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL

Alter Table Vales_Cancelacion Add Constraint FK_Vales_Cancelacion_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  
