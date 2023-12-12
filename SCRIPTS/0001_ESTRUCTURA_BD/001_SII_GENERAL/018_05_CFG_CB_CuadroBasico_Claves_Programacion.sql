------------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------------ 
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_CuadroBasico_Claves_Programacion_Excepciones' and xType = 'U' ) 
----	Drop Table CFG_CB_CuadroBasico_Claves_Programacion_Excepciones
----Go--#xxSQL 


------------------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_CuadroBasico_Claves_Programacion' and xType = 'U' ) 
--	Drop Table CFG_CB_CuadroBasico_Claves_Programacion
Begin 
	Create Table CFG_CB_CuadroBasico_Claves_Programacion 
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdCliente varchar(4) Not Null, 
		IdSubCliente varchar(4) Not Null, 	
		Año int Not Null Default 0, 
		Mes int Not Null Default 0, 	
		IdClaveSSA varchar(4) Not Null, 
		Cantidad int Not Null Default 0, 
		FechaRegistro  datetime Not Null Default getdate(), 
		Status varchar(1) Not Null Default 'A', 
		
		IdEstadoRegistra varchar(2) Not Null, 
		IdFarmaciaRegistra varchar(4) Not Null, 
		IdPersonalRegistra varchar(4) Not Null, 
			
		Actualizado tinyint Not Null Default 0 	
	) 

	Alter Table CFG_CB_CuadroBasico_Claves_Programacion 
		Add Constraint PK_CFG_CB_CuadroBasico_Claves_Programacion 
		Primary Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA ) 

End 
Go--#SQL  


----Alter Table CFG_CB_CuadroBasico_Claves_Programacion Add Constraint FK_CFG_CB_CuadroBasico_Claves_Programacion_CFG_CB_NivelesAtencion_Miembros
----	Foreign Key ( IdEstado, IdCliente, IdNivel, IdFarmacia ) References CFG_CB_NivelesAtencion_Miembros ( IdEstado, IdCliente, IdNivel, IdFarmacia  )
----Go--#xSQL 

----Alter Table CFG_CB_CuadroBasico_Claves_Programacion Add Constraint FK_CFG_CB_CuadroBasico_Claves_Programacion_CatClavesSSA_Sales 
----	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
----Go--#xSQL

----Alter Table CFG_CB_CuadroBasico_Claves_Programacion Add Constraint FK_CFG_CB_CuadroBasico_Claves_Programacion_CatSubProgramas
----	Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma )
----Go--#xSQL 


------------------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_CuadroBasico_Claves_Programacion_Excepciones' and xType = 'U' ) 
--	Drop Table CFG_CB_CuadroBasico_Claves_Programacion_Excepciones
Begin 
	Create Table CFG_CB_CuadroBasico_Claves_Programacion_Excepciones 
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdCliente varchar(4) Not Null, 
		IdSubCliente varchar(4) Not Null, 	
		Año int Not Null Default 0, 
		Mes int Not Null Default 0, 	
		IdClaveSSA varchar(4) Not Null, 
		IdExcepcion int Not Null, 
		FechaRegistro  datetime Not Null Default getdate(), 
			
		Cantidad int Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		
		IdEstadoRegistra varchar(2) Not Null, 
		IdFarmaciaRegistra varchar(4) Not Null, 
		IdPersonalRegistra varchar(4) Not Null, 
			
		Actualizado tinyint Not Null Default 0 	
	)

	Alter Table CFG_CB_CuadroBasico_Claves_Programacion_Excepciones 
		Add Constraint PK_CFG_CB_CuadroBasico_Claves_Programacion_Excepciones 
		Primary Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, IdExcepcion ) 
		
	Alter Table CFG_CB_CuadroBasico_Claves_Programacion_Excepciones
		Add Constraint PK_CFG_CB_CuadroBasico_Claves_Programacion_Excepciones___CFG_CB_CuadroBasico_Claves_Programacion  
		Foreign Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA ) 
		References CFG_CB_CuadroBasico_Claves_Programacion ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA ) 

End 
Go--#SQL 	
