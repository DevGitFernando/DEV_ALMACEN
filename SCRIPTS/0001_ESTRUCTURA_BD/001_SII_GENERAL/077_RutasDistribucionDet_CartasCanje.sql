If Exists ( Select * From sysobjects (NoLock) Where Name = 'PK_RutasDistribucionDet_CartasCanje' and xType = 'PK' ) 
   Alter Table RutasDistribucionDet_CartasCanje drop constraint PK_RutasDistribucionDet_CartasCanje
Go--#SQL

If Exists ( Select * From sysobjects (NoLock) Where Name = 'FK_RutasDistribucionDet_CartasCanje_RutasDistribucionDet' and xType = 'F' ) 
   Alter Table RutasDistribucionDet_CartasCanje drop constraint FK_RutasDistribucionDet_CartasCanje_RutasDistribucionDet
Go--#SQL

If Exists ( Select * From sysobjects (NoLock) Where Name = 'PK_RutasDistribucionDet' and xType = 'PK' ) 
   Alter Table RutasDistribucionDet drop constraint PK_RutasDistribucionDet
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'PK_RutasDistribucionDet' and xType = 'PK' ) 
	Alter Table RutasDistribucionDet Add Constraint PK_RutasDistribucionDet Primary Key ( IdEstado, IdFarmacia, Folio, FolioTransferenciaVenta, Tipo )  
Go--#SQL 

If Exists ( Select * From sysobjects (NoLock) Where Name = 'RutasDistribucionDet_CartasCanje' and xType = 'U' ) 
   Drop Table RutasDistribucionDet_CartasCanje
Go--#SQL


If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'RutasDistribucionDet_CartasCanje' and xType = 'U' ) 
Begin 
	Create Table RutasDistribucionDet_CartasCanje
	(
		IdEmpresa Varchar(3) Default '' Not Null,
		IdEstado varchar(2) default '' Not Null,
		IdFarmacia varchar(4) default '' Not Null,
		FolioRuta varchar(8) default '' Not Null,
		FolioCarta varchar(8) default '' Not Null,
		Titulo_00 Varchar(500) default '' Not Null,
		MesesCaducar int default 0 Not Null,
		FolioTransferenciaVenta varchar(8) Not Null,
		Tipo varchar(1) Not Null Default '',
		IdProducto varchar(8) Not Null,
		CodigoEAN varchar(30) Not Null,
		IdSubFarmacia varchar(2) Not Null,
		ClaveLote varchar(30) Not Null,
		Cant_Enviada Numeric(14,4) Not Null Default 0,
		Cant_Devuelta Numeric(14,4) Not Null Default 0,
		CantidadEnviada Numeric(14,4) Not Null Default 0,
		CartaDevuelta Bit Not Null Default 0,
		FechaDev datetime Not Null Default getdate(),
		Status varchar(1) Not Null Default 'A',
		Actualizado tinyint Not Null Default 0
	)
	
	Alter Table RutasDistribucionDet_CartasCanje Add Constraint PK_RutasDistribucionDet_CartasCanje
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRuta, FolioCarta, FolioTransferenciaVenta, Tipo, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote ) 

	Alter Table RutasDistribucionDet_CartasCanje Add Constraint FK_RutasDistribucionDet_CartasCanje_RutasDistribucionDet
	Foreign Key ( IdEstado, IdFarmacia, FolioRuta, FolioTransferenciaVenta, Tipo) References RutasDistribucionDet 
End 
Go--#SQL