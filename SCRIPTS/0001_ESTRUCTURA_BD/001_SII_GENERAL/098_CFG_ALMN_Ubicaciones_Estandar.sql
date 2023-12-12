--------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_ALMN_Ubicaciones_Estandar' and xType = 'U' )
Begin 
	Create Table CFG_ALMN_Ubicaciones_Estandar 
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		NombrePosicion varchar(100) Not Null,
		
		IdRack int Not Null,
		IdNivel int Not Null,
		IdEntrepaño int Not Null,
		
		IdPersonal varchar(4) Not Null,
		FechaRegistro datetime Not Null Default GetDate(), 
		Status bit Not Null Default 0, 
		Actualizado tinyint Not Null Default 0,		
		FechaControl datetime Not Null Default Getdate()
	)


	Alter Table CFG_ALMN_Ubicaciones_Estandar Add Constraint PK_CFG_ALMN_Ubicaciones_Estandar 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, NombrePosicion ) 

	Alter Table CFG_ALMN_Ubicaciones_Estandar Add Constraint FK_CFG_ALMN_Ubicaciones_Estandar_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 

	Alter Table CFG_ALMN_Ubicaciones_Estandar Add Constraint FK_CFG_ALMN_Ubicaciones_Estandar_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

	Alter Table CFG_ALMN_Ubicaciones_Estandar Add Constraint FK_CFG_ALMN_Ubicaciones_Estandar_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 

	Alter Table CFG_ALMN_Ubicaciones_Estandar Add Constraint FK_CFG_ALMN_Ubicaciones_Estandar_Cat_ALMN_Ubicaciones_Estandar 
	Foreign Key ( NombrePosicion ) References Cat_ALMN_Ubicaciones_Estandar ( NombrePosicion ) 

End 
Go--#SQL


---------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_ALMN_Ubicaciones_Estandar_Historico' and xType = 'U' )
Begin  
Create Table CFG_ALMN_Ubicaciones_Estandar_Historico 
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	NombrePosicion varchar(100) Not Null,
	FechaRegistro datetime Not Null, 
	
	IdRack int Not Null,
	IdNivel int Not Null,
	IdEntrepaño int Not Null,
	
	IdPersonal varchar(4) Not Null,	
	Status bit Not Null Default 0, 
	Actualizado tinyint Not Null Default 0,		
	Keyx int identity(1,1) 
)


	Alter Table CFG_ALMN_Ubicaciones_Estandar_Historico Add Constraint PK_CFG_ALMN_Ubicaciones_Estandar_Historico 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, NombrePosicion, FechaRegistro ) 

	Alter Table CFG_ALMN_Ubicaciones_Estandar_Historico Add Constraint FK_CFG_ALMN_Ubicaciones_Estandar_Historico_CFG_ALMN_Ubicaciones_Estandar 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, NombrePosicion ) 
	References CFG_ALMN_Ubicaciones_Estandar ( IdEmpresa, IdEstado, IdFarmacia, NombrePosicion ) 

End 
Go--#SQL 
