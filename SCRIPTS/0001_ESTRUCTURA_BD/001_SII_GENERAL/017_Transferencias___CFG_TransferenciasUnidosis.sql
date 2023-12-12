
If Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'CFG_Transferencias_FarmaciasRelacionadas' and xType = 'U' )  Drop Table CFG_Transferencias_FarmaciasRelacionadas 
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'CFG_Transferencias_FarmaciasRelacionadas' and xType = 'U' )  
Begin 	

	Create Table CFG_Transferencias_FarmaciasRelacionadas  
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdFarmaciaRelacionada varchar(4) Not Null, 
		Status varchar(1) Not Null Default '', 
		MD5 varchar(500) Not Null Default ''  
	)

	Alter Table CFG_Transferencias_FarmaciasRelacionadas Add Constraint PK_CFG_Transferencias_FarmaciasRelacionadas 
		Primary Key ( IdEstado, IdFarmacia, IdFarmaciaRelacionada ) 


	Alter Table CFG_Transferencias_FarmaciasRelacionadas Add Constraint FK_CFG_Transferencias_FarmaciasRelacionadas___CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

	Alter Table CFG_Transferencias_FarmaciasRelacionadas Add Constraint FK_CFG_Transferencias_FarmaciasRelacionadas___CatFarmaciasRelacionada 
		Foreign Key ( IdEstado, IdFarmaciaRelacionada ) References CatFarmacias ( IdEstado, IdFarmacia ) 

End 
Go--#SQL



