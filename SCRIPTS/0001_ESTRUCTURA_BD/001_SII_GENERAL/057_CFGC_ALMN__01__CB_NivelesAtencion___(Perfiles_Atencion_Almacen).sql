---------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_ALMN_CB_NivelesAtencion_ClavesSSA' and xType = 'U'  ) 
   Drop Table CFGC_ALMN_CB_NivelesAtencion_ClavesSSA   
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_ALMN_CB_NivelesAtencion' and xType = 'U'  ) 
   Drop Table CFGC_ALMN_CB_NivelesAtencion 
Go--#SQL 

Create Table CFGC_ALMN_CB_NivelesAtencion 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPerfilAtencion int Not Null, 
	
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 	
		
	Descripcion varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFGC_ALMN_CB_NivelesAtencion Add Constraint PK_CFGC_ALMN_CB_NivelesAtencion 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion, IdCliente, IdSubCliente ) 
Go--#SQL     

Alter Table CFGC_ALMN_CB_NivelesAtencion 
	Add Constraint FK_CFGC_ALMN_CB_NivelesAtencion___CatSubClientes  
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente ) 
Go--#SQL     

Alter Table CFGC_ALMN_CB_NivelesAtencion 
	Add Constraint FK_CFGC_ALMN_CB_NivelesAtencion___CatSubProgramas  
	Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma ) 
Go--#SQL     


Insert Into CFGC_ALMN_CB_NivelesAtencion Select '001', '21', '1182', 1, '0002', '0005', '0003', '0001', 'Oportunidades', 'A', 0 
Insert Into CFGC_ALMN_CB_NivelesAtencion Select '001', '21', '1182', 2, '0002', '0005', '0002', '0001', 'Seguro Popular', 'A', 0 
Insert Into CFGC_ALMN_CB_NivelesAtencion Select '001', '21', '1182', 3, '0002', '0005', '0002', '1312', 'Poblacion abierta', 'A', 0 
Go--#SQL 
 
 
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_ALMN_CB_NivelesAtencion_ClavesSSA' and xType = 'U'  ) 
   Drop Table CFGC_ALMN_CB_NivelesAtencion_ClavesSSA   
Go--#SQL 

Create Table CFGC_ALMN_CB_NivelesAtencion_ClavesSSA 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPerfilAtencion int Not Null, 

	IdClaveSSA varchar(4) Not Null Default '', 
	ClaveSSA varchar(20) Not Null, 

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL 
	

Alter Table CFGC_ALMN_CB_NivelesAtencion_ClavesSSA Add Constraint PK_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion, ClaveSSA ) 
Go--#SQL 



