
-------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Svr_UnidadesRegistradas' and xType = 'U' ) 
   Drop Table CFG_Svr_UnidadesRegistradas 
Go--#SQL  

Create Table CFG_Svr_UnidadesRegistradas 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	EsRegional bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFG_Svr_UnidadesRegistradas Add Constraint PK_CFG_Svr_UnidadesRegistradas Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CFG_Svr_UnidadesRegistradas Add Constraint FK_CFG_Svr_UnidadesRegistradas_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL  

Alter Table CFG_Svr_UnidadesRegistradas Add Constraint FK_CFG_Svr_UnidadesRegistradas_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

