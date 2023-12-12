----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInvConsignacion_NoEnCatalogo_Det' and xType = 'U' ) 
   Drop Table MovtosInvConsignacion_NoEnCatalogo_Det 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInvConsignacion_NoEnCatalogo_Enc' and xType = 'U' ) 
   Drop Table MovtosInvConsignacion_NoEnCatalogo_Enc 
Go--#SQL   

----------------------------------------------------------------------------------------------   
Create Table MovtosInvConsignacion_NoEnCatalogo_Enc(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	Folio varchar(6) NOT NULL,
	FechaRegistro datetime NOT NULL DEFAULT getdate(),
	IdPersonal varchar(4) NOT NULL,
	Observaciones varchar(200) NOT NULL DEFAULT '',
	MovtoAplicado varchar(1) NOT NULL DEFAULT 'N',
	Status varchar(1) NOT NULL DEFAULT 'A',
	Actualizado tinyint NOT NULL DEFAULT 0
)
Go--#SQL  

Alter Table MovtosInvConsignacion_NoEnCatalogo_Enc Add Constraint PK_MovtosInvConsignacion_NoEnCatalogo_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL  

Alter Table MovtosInvConsignacion_NoEnCatalogo_Enc Add Constraint FK_MovtosInvConsignacion_NoEnCatalogo_Enc_CatEmpresas  
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL  	

Alter Table MovtosInvConsignacion_NoEnCatalogo_Enc Add Constraint FK_MovtosInvConsignacion_NoEnCatalogo_Enc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) 
	References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table MovtosInvConsignacion_NoEnCatalogo_Enc Add Constraint FK_MovtosInvConsignacion_NoEnCatalogo_Enc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL   


----------------------------------------------------------------------------------------------   

Create Table MovtosInvConsignacion_NoEnCatalogo_Det(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	Folio varchar(6) NOT NULL,
	ClaveSSA varchar(20) NOT NULL,
	Descripcion varchar(100) NOT NULL DEFAULT (''),
	CodigoEAN varchar(30) NOT NULL,
	NombreComercial varchar(100) NOT NULL DEFAULT (''),
	Laboratorio varchar(100) NOT NULL DEFAULT (''),
	ClaveLote varchar(30) NOT NULL,
	FechaCaducidad datetime NOT NULL,
	CantidadLote int NOT NULL DEFAULT ((0)),
	FechaRegistro datetime NOT NULL DEFAULT getdate(),
	Status varchar(1) NOT NULL DEFAULT 'A',
	Actualizado tinyint NOT NULL DEFAULT 0
)
Go--#SQL  

Alter Table MovtosInvConsignacion_NoEnCatalogo_Det Add Constraint PK_MovtosInvConsignacion_NoEnCatalogo_Det Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA, Descripcion, CodigoEAN, NombreComercial, Laboratorio, ClaveLote) 
Go--#SQL  

Alter Table MovtosInvConsignacion_NoEnCatalogo_Det Add Constraint FK_MovtosInvConsignacion_NoEnCatalogo_Det_MovtosInvConsignacion_NoEnCatalogo_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References MovtosInvConsignacion_NoEnCatalogo_Enc ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL  