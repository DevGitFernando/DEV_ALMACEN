
---		drop table CFG_CFDI_PermisosEspeciales 

-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'CFG_CFDI_PermisosEspeciales' and xType = 'U' )  
Begin 	

	Create Table CFG_CFDI_PermisosEspeciales  
	( 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		Tipo smallint Not Null Default 0, 
		Status varchar(1) Not Null Default '', 
		MD5 varchar(500) Not Null Default '' 
	) 

	Alter Table CFG_CFDI_PermisosEspeciales Add Constraint PK_CFG_CFDI_PermisosEspeciales 
		Primary Key ( IdEstado, IdFarmacia ) 

	Alter Table CFG_CFDI_PermisosEspeciales Add Constraint FK_CFG_CFDI_PermisosEspeciales___CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

End 
Go--#SQL

