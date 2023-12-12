-----------------------------------------------------------------------------------------------------------------  
-----------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Emisores_PAC' and xType = 'U' ) 
	Drop Table FACT_CFDI_Emisores_PAC
Go--#SQL  

-----------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_PACs' and xType = 'U' ) 
	Drop Table FACT_CFDI_PACs
Go--#SQL  

Create Table FACT_CFDI_PACs 
( 
	IdPAC varchar(3) Not Null, 
	NombrePAC varchar(100) Not Null Default '' Unique,  
	UrlProduccion varchar(500) Not Null,  
	UrlPruebas varchar(500) Not Null 	
) 
Go--#SQL  

Alter Table FACT_CFDI_PACs Add Constraint PK_FACT_CFDI_PACs Primary Key ( IdPAC ) 
Go--#SQL 

Insert Into FACT_CFDI_PACs Select '001', 'Tralix', 'http://xsa5.factura-e.biz', 'http://xsa5.factura-e.biz'  
Insert Into FACT_CFDI_PACs Select '002', 'PAX Facturación', 
	'https://www.paxfacturacion.com.mx:453', 
	'https://test.paxfacturacion.com.mx:453'  
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Emisores_PAC' and xType = 'U' ) 
	Drop Table FACT_CFDI_Emisores_PAC
Go--#SQL  

Create Table FACT_CFDI_Emisores_PAC 
( 
	IdEmpresa varchar(3) Not Null, 
	IdPAC varchar(3) Not Null, 
	Usuario varchar(1000) Not Null Default '', 
	Password varchar(1000) Not Null Default '', 
	EnProduccion bit Not Null Default 'false'  
	
) 	
Go--#SQL 

Alter Table FACT_CFDI_Emisores_PAC Add Constraint PK_FACT_CFDI_Emisores_PAC Primary Key ( IdEmpresa ) 
Go--#SQL 

----Alter Table FACT_CFDI_Emisores_PAC Add Constraint FK_FACT_CFDI_Emisores_PAC___FACT_CFDI_Emisores 
----	Foreign Key ( IdEmpresa  ) References FACT_CFDI_Emisores ( IdEmpresa ) 
----Go--#SQL 

Alter Table FACT_CFDI_Emisores_PAC Add Constraint FK_FACT_CFDI_Emisores_PAC___FACT_CFDI_PACs 
	Foreign Key ( IdPAC  ) References FACT_CFDI_PACs ( IdPAC ) 
Go--#SQL 


Insert Into FACT_CFDI_Emisores_PAC select '001', '001', 'IME970211V92', '98706e390a7aeba1728ebeeb7ac2c488', 1  
--Insert Into FACT_CFDI_Emisores_PAC select '001', '002', 'WSDL_PAX', 'wqfCr8O3xLfEhMOHw4nEjMSrxJnvv7bvvr4cVcKuKkBEM++/ke+/gCPvv4nvvrfvvaDvvb/vvqTvvoA=', 'false' 
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------  
-----------------------------------------------------------------------------------------------------------------  
-----------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Emisores_PAC_Cuentas' and xType = 'U' ) 
	Drop Table FACT_CFDI_Emisores_PAC_Cuentas
Go--#SQL  

Create Table FACT_CFDI_Emisores_PAC_Cuentas 
( 
	IdEmpresa varchar(3) Not Null, 
	IdPAC varchar(3) Not Null, 
	Usuario varchar(1000) Not Null Default '', 
	Password varchar(1000) Not Null Default '', 
	EnProduccion bit Not Null Default 'false'  
	
) 	
Go--#SQL 

Alter Table FACT_CFDI_Emisores_PAC_Cuentas Add Constraint PK_FACT_CFDI_Emisores_PAC_Cuentas Primary Key ( IdEmpresa, IdPAC ) 
Go--#SQL 

----Alter Table FACT_CFDI_Emisores_PAC_Cuentas Add Constraint FK_FACT_CFDI_Emisores_PAC_Cuentas___FACT_CFDI_Emisores 
----	Foreign Key ( IdEmpresa  ) References FACT_CFDI_Emisores ( IdEmpresa ) 
----Go--#SQL 

Alter Table FACT_CFDI_Emisores_PAC_Cuentas Add Constraint FK_FACT_CFDI_Emisores_PAC_Cuentas___FACT_CFDI_PACs 
	Foreign Key ( IdPAC  ) References FACT_CFDI_PACs ( IdPAC ) 
Go--#SQL 

Insert Into FACT_CFDI_Emisores_PAC_Cuentas select '001', '001', 'IME970211V92', '98706e390a7aeba1728ebeeb7ac2c488', 1  
Insert Into FACT_CFDI_Emisores_PAC_Cuentas select '002', '001', 'IME970211V92', '98706e390a7aeba1728ebeeb7ac2c488', 1  
Insert Into FACT_CFDI_Emisores_PAC_Cuentas select '001', '002', 'IME_tmb_systems', 'wpHCmcSKw7bCnMK7w53EhcK9wrTvv5vvv7dwNV3vv7JZwpYZRO+/ue+/ve++k+++vO+/nO++qO+9v++9uDI=', 'false' 
Go--#SQL 
