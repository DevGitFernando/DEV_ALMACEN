If Exists ( Select * From sysobjects (NoLock) Where Name = 'RutasDistribucionDet' and xType = 'U' ) 
   Drop Table RutasDistribucionDet
Go--#SQL 

--------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'RutasDistribucionEnc' and xType = 'U' )  
Begin 
	Create Table RutasDistribucionEnc
	(
		IdEmpresa Varchar(3) Default '' Not Null, 
		IdEstado varchar(2) default '' Not Null, 
		IdFarmacia varchar(4) default '' Not Null, 
		Folio varchar(8) default '' Not Null, 
		IdVehiculo varchar(8) default '' Not Null, 
		IdPersonal varchar(4) default '', 
		IdPersonalCaptura varchar(4) Not Null Default '0001', 
		Observaciones Varchar(Max) Default '' Not Null, 
		FechaRegistro datetime Not Null Default GetDate(), 
		Firmado Bit Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table RutasDistribucionEnc Add Constraint PK_RutasDistribucionEnc
	Primary Key ( IdEstado, IdFarmacia, Folio )  

	Alter Table RutasDistribucionEnc Add Constraint FK_RutasDistribucionEnc_CatEmpresas
	Foreign Key ( IdEmpresa) References CatEmpresas  

	Alter Table RutasDistribucionEnc Add Constraint FK_RutasDistribucionEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia) References CatFarmacias   
	
	Alter Table RutasDistribucionEnc Add Constraint FK_RutasDistribucionEnc_CatVehiculos 
	Foreign Key ( IdEstado, IdFarmacia, IdVehiculo) References CatVehiculos   

	Alter Table RutasDistribucionEnc Add Constraint FK_RutasDistribucionEnc_CatPersonalCedis 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPersonal) References CatPersonalCedis  

End 
Go--#SQL


--------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'RutasDistribucionDet' and xType = 'U' ) 
Begin 
	Create Table RutasDistribucionDet
	(
		IdEmpresa Varchar(3) Default '' Not Null, 
		IdEstado varchar(2) default '' Not Null, 
		IdFarmacia varchar(4) default '' Not Null, 
		Folio varchar(8) default '' Not Null, 
		FolioTransferenciaVenta varchar(8) Not Null, 
		Bultos int Not Null, 
		Tipo varchar(1) Not Null Default '', 

		FolioDevuelto Bit Not Null Default 0, 
		FechaDevuelto datetime Not Null Default getdate(), 

		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table RutasDistribucionDet Add Constraint FK_RutasDistribucionDet_RutasDistribucionEnc
	Foreign Key ( IdEstado, IdFarmacia, Folio) References RutasDistribucionEnc 
End 
Go--#SQL


--------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'RutasDistribucionLeyenda' and xType = 'U' ) 
   Drop Table RutasDistribucionLeyenda
Go--#SQL 

Create Table RutasDistribucionLeyenda
(
	IdEmpresa Varchar(3) Default '' Not Null,
	IdEstado varchar(2) default '' Not Null,
	IdFarmacia varchar(4) default '' Not Null,
	Leyenda varchar(800) default '' Not Null,
	Status varchar(1) Not Null Default 'A',

)
Go--#SQL

	Alter Table RutasDistribucionLeyenda Add Constraint PK_RutasDistribucionLeyenda
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia )
Go--#SQL  

If Not Exists ( Select * From RutasDistribucionLeyenda Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '0003' )  Insert Into RutasDistribucionLeyenda (  IdEmpresa, IdEstado, IdFarmacia, Leyenda, Status )  Values ( '001', '11', '0003', 'La entrega de la totalidad de las mercancías que se describen en el presente documento, es la responsabilidad del transportista desde el momento de su recepción, durante el trayecto y hasta su entrega en el destino indicado, en el caso de faltantes o merma por daño causado por indebido manejo de las mismas durante el proceso de entrega, será cuantificado en moneda nacional de curso legal y cobrado a satisfacción de Intercontinental de Medicamentos, S.A. De C.V.', 'A' )    Else Update RutasDistribucionLeyenda Set Leyenda = 'La entrega de la totalidad de las mercancías que se describen en el presente documento, es la responsabilidad del transportista desde el momento de su recepción, durante el trayecto y hasta su entrega en el destino indicado, en el caso de faltantes o merma por daño causado por indebido manejo de las mismas durante el proceso de entrega, será cuantificado en moneda nacional de curso legal y cobrado a satisfacción de Intercontinental de Medicamentos, S.A. De C.V.', Status = 'A' Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '0003'
If Not Exists ( Select * From RutasDistribucionLeyenda Where IdEmpresa = '001' and IdEstado = '20' and IdFarmacia = '0044' )  Insert Into RutasDistribucionLeyenda (  IdEmpresa, IdEstado, IdFarmacia, Leyenda, Status )  Values ( '001', '20', '0044', 'La entrega de la totalidad de las mercancías que se describen en el presente documento, es la responsabilidad del transportista desde el momento de su recepción, durante el trayecto y hasta su entrega en el destino indicado, en el caso de faltantes o merma por daño causado por indebido manejo de las mismas durante el proceso de entrega, será cuantificado en moneda nacional de curso legal y cobrado a satisfacción de Intercontinental de Medicamentos, S.A. De C.V.', 'A' )    Else Update RutasDistribucionLeyenda Set Leyenda = 'La entrega de la totalidad de las mercancías que se describen en el presente documento, es la responsabilidad del transportista desde el momento de su recepción, durante el trayecto y hasta su entrega en el destino indicado, en el caso de faltantes o merma por daño causado por indebido manejo de las mismas durante el proceso de entrega, será cuantificado en moneda nacional de curso legal y cobrado a satisfacción de Intercontinental de Medicamentos, S.A. De C.V.', Status = 'A' Where IdEmpresa = '001' and IdEstado = '20' and IdFarmacia = '0044'
If Not Exists ( Select * From RutasDistribucionLeyenda Where IdEmpresa = '001' and IdEstado = '21' and IdFarmacia = '1182' )  Insert Into RutasDistribucionLeyenda (  IdEmpresa, IdEstado, IdFarmacia, Leyenda, Status )  Values ( '001', '21', '1182', 'La entrega de la totalidad de las mercancías que se describen en el presente documento, es la responsabilidad del transportista desde el momento de su recepción, durante el trayecto y hasta su entrega en el destino indicado, en el caso de faltantes o merma por daño causado por indebido manejo de las mismas durante el proceso de entrega, será cuantificado en moneda nacional de curso legal y cobrado a satisfacción de Intercontinental de Medicamentos, S.A. De C.V.', 'A' )    Else Update RutasDistribucionLeyenda Set Leyenda = 'La entrega de la totalidad de las mercancías que se describen en el presente documento, es la responsabilidad del transportista desde el momento de su recepción, durante el trayecto y hasta su entrega en el destino indicado, en el caso de faltantes o merma por daño causado por indebido manejo de las mismas durante el proceso de entrega, será cuantificado en moneda nacional de curso legal y cobrado a satisfacción de Intercontinental de Medicamentos, S.A. De C.V.', Status = 'A' Where IdEmpresa = '001' and IdEstado = '21' and IdFarmacia = '1182'
Go--#SQL  

-----------------------------------------------------------------------------------------------------
If Exists ( Select * From sysobjects (NoLock) Where Name = 'RutasDistribucionLeyenda_Historico' and xType = 'U' ) 
   Drop Table RutasDistribucionLeyenda_Historico
Go--#SQL 

Create Table RutasDistribucionLeyenda_Historico
(
	IdEmpresa Varchar(3) Default '' Not Null,
	IdEstado varchar(2) default '' Not Null,
	IdFarmacia varchar(4) default '' Not Null,
	Leyenda varchar(200) default '' Not Null,
	FechaRegistro datetime Not Null Default getdate(), 
	IdPersonal varchar(4) Not Null, 
	Status varchar(1) Not Null Default 'A',

)
Go--#SQL  

	Alter Table RutasDistribucionLeyenda_Historico Add Constraint PK_RutasDistribucionLeyenda_Historico
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia )
Go--#SQL  

	Alter Table RutasDistribucionLeyenda_Historico Add Constraint FK_RutasDistribucionLeyenda_Historico_CatPersonal
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal) References CatPersonal 
Go--#SQL 

