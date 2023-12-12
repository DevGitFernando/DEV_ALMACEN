


---------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_FAR_CB_NivelesAtencion_ClavesSSA' and xType = 'U'  ) 
   Drop Table CFGC_FAR_CB_NivelesAtencion_ClavesSSA   
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_FAR_CB_NivelesAtencion' and xType = 'U'  ) 
   Drop Table CFGC_FAR_CB_NivelesAtencion 
Go--#SQL 

Create Table CFGC_FAR_CB_NivelesAtencion 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	--IdFarmacia varchar(4) Not Null, 
	IdPerfilAtencion int Not Null, 
	IdSubPerfilAtencion int Not Null, 
		
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 	
		
	Descripcion varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFGC_FAR_CB_NivelesAtencion Add Constraint PK_CFGC_FAR_CB_NivelesAtencion
	Primary Key ( IdEmpresa, IdEstado, IdPerfilAtencion, IdSubPerfilAtencion, IdCliente, IdSubCliente ) 
Go--#SQL     

Alter Table CFGC_FAR_CB_NivelesAtencion 
	Add Constraint FK_CFGC_FAR_CB_NivelesAtencion___CatSubClientes  
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente ) 
Go--#SQL     

Alter Table CFGC_FAR_CB_NivelesAtencion 
	Add Constraint FK_CFGC_FAR_CB_NivelesAtencion___CatSubProgramas  
	Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma ) 
Go--#SQL     

 
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_FAR_CB_NivelesAtencion_ClavesSSA' and xType = 'U'  ) 
   Drop Table CFGC_FAR_CB_NivelesAtencion_ClavesSSA   
Go--#SQL 

Create Table CFGC_FAR_CB_NivelesAtencion_ClavesSSA 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	--IdFarmacia varchar(4) Not Null, 
	IdPerfilAtencion int Not Null, 
	IdSubPerfilAtencion int Not Null, 

	ClaveSSA varchar(20) Not Null, 

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0,
	IdClaveSSA varchar(4) Not Null Default '',
)
Go--#SQL 
	

Alter Table CFGC_FAR_CB_NivelesAtencion_ClavesSSA Add Constraint PK_CFGC_FAR_CB_NivelesAtencion_ClavesSSA
	Primary Key ( IdEmpresa, IdEstado, IdPerfilAtencion, IdSubPerfilAtencion, ClaveSSA ) 
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_FAR_CB_NivelesAtencion_Miembros' and xType = 'U'  ) 
   Drop Table CFGC_FAR_CB_NivelesAtencion_Miembros 
Go--#SQL 

Create Table CFGC_FAR_CB_NivelesAtencion_Miembros 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	NombreFarmacia varchar(100) Not Null Default '', 
	IdPerfilAtencion int Not Null,		
	IdSubPerfilAtencion int Not Null, 
		
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFGC_FAR_CB_NivelesAtencion_Miembros Add Constraint PK_CFGC_FAR_CB_NivelesAtencion_Miembros
	Primary Key ( IdEstado, IdFarmacia, IdPerfilAtencion, IdSubPerfilAtencion ) 
Go--#SQL     



