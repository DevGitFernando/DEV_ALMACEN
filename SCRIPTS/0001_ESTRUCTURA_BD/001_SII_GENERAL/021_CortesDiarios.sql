--------------------
-- CORTES DIARIOS --
--------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesDiarios' and xType = 'U' ) 
   Drop Table CtlCortesDiarios 
Go--#SQL  

Create Table CtlCortesDiarios
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdCorte varchar(2) Not Null, 
	FechaSistema datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(), 
	TotalCorte numeric(14, 4) Not Null Default 0,
	TotalVentaTA int Not Null Default 0, 
	Comentario varchar(300) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
) 
Go--#SQL  

Alter Table CtlCortesDiarios Add Constraint PK_CtlCortesDiarios Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPersonal, IdCorte, FechaSistema ) 
Go--#SQL  

Alter Table CtlCortesDiarios Add Constraint FK_CtlCortesDiarios_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL  

Alter Table CtlCortesDiarios Add Constraint FK_CtlCortesDiarios_CatFarmacias 
	Foreign key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CtlCortesDiarios Add Constraint FK_CtlCortesDiarios_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  


