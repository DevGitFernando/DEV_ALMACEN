-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_ALMN_Unidades_Atendidas' and xType = 'U' ) 
Begin 
	Create Table CFG_ALMN_Unidades_Atendidas  
	(
		IdEstado varchar(2) Not Null, 
		IdAlmacen varchar(4) Not Null, 
		IdFarmacia varchar(4) Not Null,
		EsAsignado bit Not Null Default 0, 		
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table CFG_ALMN_Unidades_Atendidas 
		Add Constraint PK_CFG_ALMN_Unidades_Atendidas Primary Key ( IdEstado, IdAlmacen, IdFarmacia  ) 

	Alter Table CFG_ALMN_Unidades_Atendidas 
		Add Constraint FK_CFG_ALMN_Unidades_Atendidas__CatFarmacias__01  
		Foreign Key ( IdEstado, IdAlmacen ) References CatFarmacias ( IdEstado, IdFarmacia ) 
		
	Alter Table CFG_ALMN_Unidades_Atendidas 
		Add Constraint FK_CFG_ALMN_Unidades_Atendidas__CatFarmacias__02  
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
End 
Go--#SQL 


	----Insert Into CFG_ALMN_Unidades_Atendidas Select '11', '0003', '0011', 1, 'A', 0  
	----Insert Into CFG_ALMN_Unidades_Atendidas Select '11', '0003', '0012', 1, 'A', 0  
	----Insert Into CFG_ALMN_Unidades_Atendidas Select '11', '0003', '0013', 1, 'A', 0  	
Go--#SQL 


