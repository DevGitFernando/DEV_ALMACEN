-------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosCartasCanje_Enc' and xType = 'U' ) 
Begin 
	Create Table CambiosCartasCanje_Enc 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,	
		FolioCambio varchar(30) Not Null,
		FolioCarta varchar(8) Not Null,
		FolioTransferenciaVenta varchar(8) Not Null,
		Tipo varchar(1) Not Null Default '',
		TipoMovtoInv varchar(4) Not Null Default 'ECP', 
		FechaRegistro datetime Default GetDate(),
		IdPersonal varchar(6) Not Null Default '', 
		Observaciones varchar(500) Not Null Default '', 
		SubTotal Numeric(14,4) Not Null Default 0, 
		Iva Numeric(14,4) Not Null Default 0, 
		Total Numeric(14,4) Not Null Default 0, 
		Keyx int identity(1,1), 	
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CambiosCartasCanje_Enc Add Constraint PK_CambiosCartasCanje_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio  )

	Alter Table CambiosCartasCanje_Enc Add Constraint FK_CambiosCartasCanje_Enc_CatEmpresas 
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 

	Alter Table CambiosCartasCanje_Enc Add Constraint FK_CambiosCartasCanje_Enc_CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosCartasCanje_Det_CodigosEAN' and xType = 'U' ) 
Begin 
	Create Table CambiosCartasCanje_Det_CodigosEAN 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,	
		FolioCambio varchar(30) Not Null, 
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null, 
		Cantidad Numeric(14,4) Not Null Default 0, 
		Costo Numeric(14,4) Not Null Default 0, 
		TasaIva Numeric(14,4) Not Null Default 0, 	
		Importe Numeric(14,4) Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CambiosCartasCanje_Det_CodigosEAN Add Constraint PK_CambiosCartasCanje_Det_CodigosEAN 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN ) 

	Alter Table CambiosCartasCanje_Det_CodigosEAN Add Constraint FK_CambiosCartasCanje_Det_CodigosEAN_CambiosCartasCanje_Enc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio ) References CambiosCartasCanje_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio ) 

	Alter Table CambiosCartasCanje_Det_CodigosEAN Add Constraint FK_CambiosCartasCanje_Det_CodigosEAN_CatProductos_CodigosRelacionados 
		Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )  

	Alter Table CambiosCartasCanje_Det_CodigosEAN With NoCheck 
		Add Constraint CK_CambiosCartasCanje_Det_CodigosEAN_Cantidad Check Not For Replication (Cantidad > 0)

End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosCartasCanje_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin 
	Create Table CambiosCartasCanje_Det_CodigosEAN_Lotes 
	(
 		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,	
		IdSubFarmacia varchar(2) Not Null,  		
		FolioCambio varchar(30) Not Null, 
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null, 
		ClaveLote varchar(30) Not Null, 
		EsConsignacion tinyint Not Null Default 0,  	
		Cantidad Numeric(14,4) Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0  
	)

	Alter Table CambiosCartasCanje_Det_CodigosEAN_Lotes Add Constraint PK_CambiosCartasCanje_Det_CodigosEAN_Lotes 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCambio, IdProducto, CodigoEAN, ClaveLote ) 

	Alter Table CambiosCartasCanje_Det_CodigosEAN_Lotes Add Constraint FK_CambiosCartasCanje_Det_CodigosEAN_Lotes_CambiosCartasCanje_Det_CodigosEAN
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN ) 
		References CambiosCartasCanje_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN ) 

	Alter Table CambiosCartasCanje_Det_CodigosEAN_Lotes With NoCheck 
		Add Constraint CK_CambiosCartasCanje_Det_CodigosEAN_Lotes_Cantidad Check Not For Replication (Cantidad > 0)

End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'U' ) 
Begin 
	Create Table CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones 
	(
 		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,	  		
		FolioCambio varchar(30) Not Null, 
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null,
		IdSubFarmacia varchar(2) Not Null, 
		ClaveLote varchar(30) Not Null, 
		EsConsignacion tinyint Not Null Default 0,
		IdPasillo int Not Null, 
		IdEstante int Not Null, 
		IdEntrepaño int Not Null,  	
		Cantidad Numeric(14,4) Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0  
	)

	Alter Table CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint PK_CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote,
						IdPasillo, IdEstante, IdEntrepaño ) 
						
	Alter Table CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint FK_CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones_CambiosCartasCanje_Det_CodigosEAN_Lotes
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCambio, IdProducto, CodigoEAN, ClaveLote ) 
		References CambiosCartasCanje_Det_CodigosEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCambio, IdProducto, CodigoEAN, ClaveLote ) 

End 
Go--#SQL 


