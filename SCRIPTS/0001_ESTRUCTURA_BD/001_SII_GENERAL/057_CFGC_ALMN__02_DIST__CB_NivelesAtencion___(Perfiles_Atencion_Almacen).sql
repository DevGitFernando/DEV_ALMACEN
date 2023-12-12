---------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA' and xType = 'U'  ) 
   Drop Table CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA   
Go--#SQL


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_ALMN_DIST_CB_NivelesAtencion' and xType = 'U'  ) 
   Drop Table CFGC_ALMN_DIST_CB_NivelesAtencion 
Go--#SQL 

Create Table CFGC_ALMN_DIST_CB_NivelesAtencion 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPerfilAtencion int Not Null,		
	Descripcion varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFGC_ALMN_DIST_CB_NivelesAtencion Add Constraint PK_CFGC_ALMN_DIST_CB_NivelesAtencion
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion ) 
Go--#SQL     

----Insert Into CFGC_ALMN_DIST_CB_NivelesAtencion Select '001', '21', '0004', 1, 'Oportunidades', 'A', 0 
----Insert Into CFGC_ALMN_DIST_CB_NivelesAtencion Select '001', '21', '0004', 2, 'Seguro Popular', 'A', 0 
----Insert Into CFGC_ALMN_DIST_CB_NivelesAtencion Select '001', '21', '0004', 3, 'Poblacion abierta', 'A', 0 
----Go--#SQL 
 
 
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA' and xType = 'U'  ) 
   Drop Table CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA   
Go--#SQL 

Create Table CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPerfilAtencion int Not Null,
	ClaveSSA varchar(20) Not Null,
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL 
	

Alter Table CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA Add Constraint PK_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion, ClaveSSA ) 
Go--#SQL 

