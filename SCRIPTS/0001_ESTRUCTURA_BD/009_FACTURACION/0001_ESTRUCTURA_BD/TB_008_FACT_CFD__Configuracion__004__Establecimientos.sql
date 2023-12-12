

---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFD_Establecimientos' and xType = 'U' ) 
	Drop Table FACT_CFD_Establecimientos
Go--#SQL 

Create Table FACT_CFD_Establecimientos 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdEstablecimiento varchar(6) Not Null, 
	NombreEstablecimiento varchar(500) Not Null, 

	Pais varchar(100) Not Null Default '', 
	Estado varchar(100) Not Null Default '', 		
	Municipio varchar(100) Not Null Default '', 
	Localidad varchar(100) Not Null Default '', 	
	Colonia varchar(100) Not Null Default '', 		
	Calle varchar(100) Not Null Default '',  
	NoExterior varchar(8) Not Null Default '',  
	NoInterior varchar(8) Not Null Default '', 
	CodigoPostal varchar(100) Not Null Default '', 
	Referencia varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default '' 
) 	
Go--#SQL 

Alter Table FACT_CFD_Establecimientos Add Constraint PK_FACT_CFD_Establecimientos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdEstablecimiento  ) 
Go--#SQL 


Alter Table FACT_CFD_Establecimientos Add Constraint FK_FACT_CFD_Establecimientos___FACT_CFD_Sucursales  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia ) References FACT_CFD_Sucursales ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL 



---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFD_Establecimientos_Receptor' and xType = 'U' ) 
	Drop Table FACT_CFD_Establecimientos_Receptor
Go--#SQL 

Create Table FACT_CFD_Establecimientos_Receptor 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	IdCliente varchar(6) Not Null, 
	IdEstablecimiento varchar(6) Not Null, 
	NombreEstablecimiento varchar(500) Not Null, 

	Pais varchar(100) Not Null Default '', 
	Estado varchar(100) Not Null Default '', 		
	Municipio varchar(100) Not Null Default '', 
	Localidad varchar(100) Not Null Default '', 	
	Colonia varchar(100) Not Null Default '', 		
	Calle varchar(100) Not Null Default '',  
	NoExterior varchar(8) Not Null Default '',  
	NoInterior varchar(8) Not Null Default '', 
	CodigoPostal varchar(100) Not Null Default '', 
	Referencia varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default '' 

) 	
Go--#SQL 

Alter Table FACT_CFD_Establecimientos_Receptor Add Constraint PK_FACT_CFD_Establecimientos_Receptor Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdEstablecimiento  ) 
Go--#SQL 

Alter Table FACT_CFD_Establecimientos_Receptor Add Constraint FK_FACT_CFD_Establecimientos_Receptor___FACT_CFD_Sucursales  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia ) References FACT_CFD_Sucursales ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table FACT_CFD_Establecimientos_Receptor Add Constraint FK_FACT_CFD_Establecimientos_Receptor___FACT_CFD_Clientes  
	Foreign Key ( IdCliente ) References FACT_CFD_Clientes ( IdCliente ) 
Go--#SQL 


