------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_Sub_CuadroBasico_Claves' and xType = 'U' ) 
	Drop Table CFG_CB_Sub_CuadroBasico_Claves
Go--#SQL 

Create Table CFG_CB_Sub_CuadroBasico_Claves 
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null,
	IdNivel int Not Null,
	IdFarmacia varchar(4) Not Null,
	IdPrograma varchar(4) Not Null,
	IdSubPrograma varchar(4) Not Null,	 
	IdClaveSSA varchar(4) Not Null,
	Cantidad int Not Null Default 0, 
	FechaUpdate datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
	
)
Go--#SQL  

Alter Table CFG_CB_Sub_CuadroBasico_Claves Add Constraint PK_CFG_CB_Sub_CuadroBasico_Claves 
Primary Key ( IdEstado, IdCliente, IdNivel, IdFarmacia, IdPrograma, IdSubPrograma, IdClaveSSA ) 
Go--#SQL  

Alter Table CFG_CB_Sub_CuadroBasico_Claves Add Constraint FK_CFG_CB_Sub_CuadroBasico_Claves_CFG_CB_NivelesAtencion_Miembros
	Foreign Key ( IdEstado, IdCliente, IdNivel, IdFarmacia ) References CFG_CB_NivelesAtencion_Miembros ( IdEstado, IdCliente, IdNivel, IdFarmacia  )
Go--#SQL 

Alter Table CFG_CB_Sub_CuadroBasico_Claves Add Constraint FK_CFG_CB_Sub_CuadroBasico_Claves_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL

Alter Table CFG_CB_Sub_CuadroBasico_Claves Add Constraint FK_CFG_CB_Sub_CuadroBasico_Claves_CatSubProgramas
	Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma )
Go--#SQL 
