
-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'CatClavesSSA_InformacionAdicional' and xType = 'U' )  
Begin 	

	Create Table CatClavesSSA_InformacionAdicional  
	(
		IdClaveSSA_Sal varchar(4) Not Null, 
		Descripcion varchar(200) Not Null Default '', 
		Concentracion varchar(100) Not Null Default '', 
		Presentacion varchar(100) Not Null Default ''  
	)

	Alter Table CatClavesSSA_InformacionAdicional Add Constraint PK_CatClavesSSA_InformacionAdicional 
		Primary Key ( IdClaveSSA_Sal ) 

	Alter Table CatClavesSSA_InformacionAdicional Add Constraint FK_CatClavesSSA_InformacionAdicional___CatClavesSSA_Sales 
		Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 

End 
Go--#SQL


--	I.Descripcion as Descripcion_InformacionAdicional, I.Concentracion as Concentracion_InformacionAdicional, I.Presentacion as Presentacion_InformacionAdicional

