---------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_BancosCuentas__Receptor' and xType = 'U' )  
	Drop Table FACT_CFDI_BancosCuentas__Receptor  
Go--#SQL 

Create Table FACT_CFDI_BancosCuentas__Receptor  
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	
	RFC_Banco varchar(15) Not Null Default '',  
	NumeroDeCuenta varchar(50) Not Null Default '',  
	Status varchar(1) Not Null Default 'A'  
) 
Go--#SQL 


Alter Table FACT_CFDI_BancosCuentas__Receptor Add Constraint PK_FACT_CFDI_BancosCuentas__Receptor 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, RFC_Banco, NumeroDeCuenta ) 
Go--#SQL 

Alter Table FACT_CFDI_BancosCuentas__Receptor Add Constraint FK_FACT_CFDI_BancosCuentas__Receptor____FACT_CFDI__Bancos  
	Foreign Key ( RFC_Banco ) References FACT_CFDI__Bancos ( RFC )  
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_BancosCuentas__Emisor' and xType = 'U' )  
	Drop Table FACT_CFDI_BancosCuentas__Emisor  
Go--#SQL 

Create Table FACT_CFDI_BancosCuentas__Emisor  
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	
	RFC_Emisor varchar(15) Not Null Default '', 

	RFC_Banco varchar(15) Not Null Default '',  
	NumeroDeCuenta varchar(50) Not Null Default '',  
	Status varchar(1) Not Null Default 'A'  
) 
Go--#SQL 


Alter Table FACT_CFDI_BancosCuentas__Emisor Add Constraint PK_FACT_CFDI_BancosCuentas__Emisor 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, RFC_Emisor, RFC_Banco, NumeroDeCuenta ) 
Go--#SQL 

Alter Table FACT_CFDI_BancosCuentas__Emisor Add Constraint FK_FACT_CFDI_BancosCuentas__Emisor____FACT_CFD_Clientes  
	Foreign Key ( RFC_Emisor ) References FACT_CFD_Clientes ( RFC )  
Go--#SQL 

Alter Table FACT_CFDI_BancosCuentas__Emisor Add Constraint FK_FACT_CFDI_BancosCuentas__Emisor____FACT_CFDI__Bancos  
	Foreign Key ( RFC_Banco ) References FACT_CFDI__Bancos ( RFC )  
Go--#SQL 




If Not Exists ( Select * From FACT_CFDI_BancosCuentas__Receptor Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Banco = 'RFC_x_002' and NumeroDeCuenta = 'BANAMEX-1111' )  Insert Into FACT_CFDI_BancosCuentas__Receptor (  IdEmpresa, IdEstado, IdFarmacia, RFC_Banco, NumeroDeCuenta, Status )  Values ( '001', '13', '0001', 'RFC_x_002', 'BANAMEX-1111', 'A' ) 
 Else Update FACT_CFDI_BancosCuentas__Receptor Set Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Banco = 'RFC_x_002' and NumeroDeCuenta = 'BANAMEX-1111'  
If Not Exists ( Select * From FACT_CFDI_BancosCuentas__Receptor Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Banco = 'RFC_x_002' and NumeroDeCuenta = 'BANAMEX-8888' )  Insert Into FACT_CFDI_BancosCuentas__Receptor (  IdEmpresa, IdEstado, IdFarmacia, RFC_Banco, NumeroDeCuenta, Status )  Values ( '001', '13', '0001', 'RFC_x_002', 'BANAMEX-8888', 'A' ) 
 Else Update FACT_CFDI_BancosCuentas__Receptor Set Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Banco = 'RFC_x_002' and NumeroDeCuenta = 'BANAMEX-8888'  
If Not Exists ( Select * From FACT_CFDI_BancosCuentas__Receptor Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Banco = 'RFC_x_002' and NumeroDeCuenta = 'BANAMEX-9999' )  Insert Into FACT_CFDI_BancosCuentas__Receptor (  IdEmpresa, IdEstado, IdFarmacia, RFC_Banco, NumeroDeCuenta, Status )  Values ( '001', '13', '0001', 'RFC_x_002', 'BANAMEX-9999', 'A' ) 
 Else Update FACT_CFDI_BancosCuentas__Receptor Set Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Banco = 'RFC_x_002' and NumeroDeCuenta = 'BANAMEX-9999'  

Go--#SQL 


If Not Exists ( Select * From FACT_CFDI_BancosCuentas__Emisor Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Emisor = 'SSH9611185F9' and RFC_Banco = 'RFC_x_014' and NumeroDeCuenta = 'SANTANDER-1357' )  Insert Into FACT_CFDI_BancosCuentas__Emisor (  IdEmpresa, IdEstado, IdFarmacia, RFC_Emisor, RFC_Banco, NumeroDeCuenta, Status )  Values ( '001', '13', '0001', 'SSH9611185F9', 'RFC_x_014', 'SANTANDER-1357', 'A' ) 
 Else Update FACT_CFDI_BancosCuentas__Emisor Set Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and RFC_Emisor = 'SSH9611185F9' and RFC_Banco = 'RFC_x_014' and NumeroDeCuenta = 'SANTANDER-1357'  
Go--#SQL 
