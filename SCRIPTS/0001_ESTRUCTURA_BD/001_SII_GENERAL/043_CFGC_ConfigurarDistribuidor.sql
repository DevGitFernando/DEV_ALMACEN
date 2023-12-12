
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ConfigurarDistribuidor' and xType = 'U' ) 
   Drop Table CFGC_ConfigurarDistribuidor
Go--#SQL   

Create Table CFGC_ConfigurarDistribuidor 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdDistribuidor varchar(4) Not Null, 
	CodigoCliente varchar(20) Not Null, 
	Nombre varchar(50) Not Null,
	Servidor varchar(100) Not Null,
	WebService varchar(100) Not Null,
	PaginaWeb varchar(100) Not Null,
	EsDistribuidor bit Default 'false',
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CFGC_ConfigurarDistribuidor Add Constraint PK_CFGC_ConfigurarDistribuidor Primary Key ( IdEstado, IdFarmacia, IdDistribuidor, CodigoCliente ) 
Go--#SQL  

Alter Table CFGC_ConfigurarDistribuidor Add Constraint FK_CFGC_ConfigurarDistribuidor_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table CFGC_ConfigurarDistribuidor Add Constraint FK_CFGC_ConfigurarDistribuidor_CatDistribuidores 
	Foreign Key ( IdDistribuidor ) References CatDistribuidores ( IdDistribuidor ) 
Go--#SQL 

