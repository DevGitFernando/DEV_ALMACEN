If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGS_Parametros' and xType = 'U' ) 
   Drop Table Net_CFGS_Parametros 
Go--#SQL    

Create Table Net_CFGS_Parametros 
( 
--	IdEstado varchar(2) Not Null, 
--	IdFarmacia varchar(4) Not Null, 
	ArbolModulo varchar(4) Not Null, 
	NombreParametro varchar(100) Not Null, 
	Valor varchar(200) Not Null Default '', 
	Descripcion varchar(500) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL  

Alter Table Net_CFGS_Parametros Add Constraint PK_Net_CFGS_Parametros Primary Key ( ArbolModulo, NombreParametro ) 
Go--#SQL  


-------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Parametros' and xType = 'U' ) 
   Drop Table Net_CFGC_Parametros 
Go--#SQL     

Create Table Net_CFGC_Parametros 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	ArbolModulo varchar(4) Not Null, 
	NombreParametro varchar(100) Not Null, 
	Valor varchar(200) Not Null Default '', 
	Descripcion varchar(500) Not Null, 
	EsDeSistema bit Not Null Default 'False', 
	EsEditable bit Not Null Default 'true' 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL  

Alter Table Net_CFGC_Parametros Add Constraint PK_Net_CFGC_Parametros Primary Key ( IdEstado, IdFarmacia, ArbolModulo, NombreParametro ) 
Go--#SQL  

Alter Table Net_CFGC_Parametros Add Constraint FK_Net_CFGC_Parametros 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  


-------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_TipoCambio' and xType = 'U' ) 
   Drop Table Net_CFGC_TipoCambio  
Go--#SQL  

Create Table Net_CFGC_TipoCambio 
(
	TipoDeCambio numeric(14,2) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL  
 