If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesParciales' and xType = 'U' ) 
   Drop Table CtlCortesParciales 
Go--#SQL  

Create Table CtlCortesParciales
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdCorte varchar(2) Not Null, 
	FechaSistema datetime Not Null Default getdate(), 
	FechaCierre datetime Not Null Default getdate(), 
	IdPersonalCorte varchar(4) Not Null Default '', 
	DotacionInicial numeric(14, 4) Not Null Default 0, 
	VentaDiaContado numeric(14, 4) Not Null Default 0, 
	VentaDiaCredito numeric(14, 4) Not Null Default 0, 
	DevVentaDiaContado numeric(14, 4) Not Null Default 0, 
	DevVentaDiaCredito numeric(14, 4) Not Null Default 0, 
	DevVentaDiaAntContado numeric(14, 4) Not Null Default 0, 
	DevVentaDiaAntCredito numeric(14, 4) Not Null Default 0, 
	VentaTAContado int Not Null Default 0, 
	VentaTACredito int Not Null Default 0, 	
	Comentario varchar(300) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
) 
Go--#SQL  

Alter Table CtlCortesParciales Add Constraint PK_CtlCortesParciales Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPersonal, IdCorte, FechaSistema ) 
Go--#SQL  

Alter Table CtlCortesParciales Add Constraint FK_CtlCortesParciales_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL  

Alter Table CtlCortesParciales Add Constraint FK_CtlCortesParciales_CatFarmacias 
	Foreign key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CtlCortesParciales Add Constraint FK_CtlCortesParciales_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

Alter Table CtlCortesParciales Add Constraint FK_CtlCortesParciales_CatPersonalCorte 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonalCorte ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

