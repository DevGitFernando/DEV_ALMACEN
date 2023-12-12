Set NoCount On 
Go--#SQL  

--------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_EnvioCatalogos_Unidades' and xType = 'U' ) 
	Drop Table CFGS_EnvioCatalogos_Unidades
	 
Go--#SQL  

Create Table CFGS_EnvioCatalogos_Unidades 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL  

Alter Table CFGS_EnvioCatalogos_Unidades Add Constraint PK_CFGS_EnvioCatalogos_Unidades Primary Key ( IdEstado, IdFarmacia ) 
Go--#SQL

Alter Table CFGS_EnvioCatalogos_Unidades Add Constraint FK_CFGS_EnvioCatalogos_Unidades___CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 