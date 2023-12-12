/* 

    drop table EntradasDet_Consignacion_Lotes_Ubicaciones
    drop table EntradasDet_Consignacion_Lotes
    drop table EntradasDet_Consignacion 
    drop table EntradasEnc_Consignacion

*/ 

-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasEnc_Consignacion' and xType = 'U' ) 
Begin 
    Create Table EntradasEnc_Consignacion 
    ( 
	    IdEmpresa varchar(3) Not Null, 
	    IdEstado varchar(2) Not Null, 
	    IdFarmacia varchar(4) Not Null, 
	    FolioEntrada varchar(30) Not Null, 

		IdProveedor varchar(4) Not Null Default '1026', 

	    -- EsConsignacion tinyint Not Null Default 0,  	
	    FolioMovtoInv varchar(30) Not Null, 
	    IdPersonal varchar(4) Not Null, 

	    -- FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento 
	    FechaRegistro datetime Not Null Default getdate(),  --- Fecha de Sistema en que se realizo el movimiento 	
	    -- IdDistribuidor varchar(4) Not Null Default '', 
	    ReferenciaPedido varchar(20) Not Null Default '', 
	    Observaciones varchar(100) Not Null Default '', 
    	
	    -- Descuento Numeric(14,4) Not Null Default 0, 
	    SubTotal Numeric(14,4) Not Null Default 0, 
	    Iva Numeric(14,4) Not Null Default 0, 
	    Total Numeric(14,4) Not Null Default 0,
    	
		ReferenciaDePedidoOC varchar(29) Not Null Default '', 
		EsReferenciaDePedido bit Not Null Default 0, 
		FolioPedido Varchar(8) Not Null Default '', 
		EsPosFechado bit Not Null Default 0, 


	    Status varchar(1) Not Null Default 'A', 
	    Actualizado tinyint Not Null Default 0 
    )
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_EntradasEnc_Consignacion' and xType = 'PK' ) 
Begin 
    Alter Table EntradasEnc_Consignacion Add Constraint PK_EntradasEnc_Consignacion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioEntrada ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasEnc_Consignacion_CatEmpresas' and xType = 'F' ) 
Begin 
    Alter Table EntradasEnc_Consignacion Add Constraint FK_EntradasEnc_Consignacion_CatEmpresas 
	    Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasEnc_Consignacion_CatFarmacias' and xType = 'F' ) 
Begin 
    Alter Table EntradasEnc_Consignacion Add Constraint FK_EntradasEnc_Consignacion_CatFarmacias 
	    Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasEnc_Consignacion_CatPersonal' and xType = 'F' ) 
Begin 
    Alter Table EntradasEnc_Consignacion Add Constraint FK_EntradasEnc_Consignacion_CatPersonal  
	    Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects So (NoLock) Where Name = 'FK_EntradasEnc_Consignacion__CatProveedores' and xType = 'F' ) 
Begin 
	Alter Table EntradasEnc_Consignacion Add Constraint FK_EntradasEnc_Consignacion__CatProveedores 
		Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion' and xType = 'U' ) 
Begin 
    Create Table EntradasDet_Consignacion 
    (
	    IdEmpresa varchar(3) Not Null, 
	    IdEstado varchar(2) Not Null, 
	    IdFarmacia varchar(4) Not Null,  
	    FolioEntrada varchar(30) Not Null, 
	    IdProducto varchar(8) Not Null, 	
	    CodigoEAN varchar(30) Not Null, 
	    Renglon int Not Null Default 0,  
	    -- EsConsignacion tinyint Not Null Default 0,  	
	    UnidadDeEntrada smallint Not Null Default 1, 
	    Cant_Recibida Numeric(14,4) Not Null Default 0, 
	    Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	    CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	    CostoUnitario Numeric(14,4) Not Null Default 0, 	
	    TasaIva Numeric(14,4) Not Null Default 0, 
	    SubTotal Numeric(14,4) Not Null Default 0, 
	    ImpteIva Numeric(14,4) Not Null Default 0, 
	    Importe Numeric(14,4) Not Null Default 0, 
	    Status varchar(1) Not Null Default 'A', 
	    Actualizado tinyint Not Null Default 0 
    ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_EntradasDet_Consignacion' and xType = 'PK' ) 
Begin 
    Alter Table EntradasDet_Consignacion Add Constraint PK_EntradasDet_Consignacion 
        Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, IdProducto, CodigoEAN  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasDet_Consignacion_EntradasEnc_Consignacion' and xType = 'F' ) 
Begin 
    Alter Table EntradasDet_Consignacion Add Constraint FK_EntradasDet_Consignacion_EntradasEnc_Consignacion 
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioEntrada ) 
	    References EntradasEnc_Consignacion ( IdEmpresa, IdEstado, IdFarmacia, FolioEntrada ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasDet_Consignacion_CatProductos_CodigosRelacionados' and xType = 'F' ) 
Begin 
    Alter Table EntradasDet_Consignacion Add Constraint FK_EntradasDet_Consignacion_CatProductos_CodigosRelacionados 
	    Foreign Key ( IdProducto, CodigoEAN ) 
	    References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
End 
Go--#SQL  


-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes' and xType = 'U' ) 
Begin 
    Create Table EntradasDet_Consignacion_Lotes 
    (
	    IdEmpresa varchar(3) Not Null, 
	    IdEstado varchar(2) Not Null, 
	    IdFarmacia varchar(4) Not Null, 
	    IdSubFarmacia varchar(2) Not Null, 	
	    FolioEntrada varchar(30) Not Null, 
	    IdProducto varchar(8) Not Null, 
	    CodigoEAN varchar(30) Not Null, 
	    ClaveLote varchar(30) Not Null, 
	    Renglon int not null Default 0,  
	    EsConsignacion Bit Not Null Default 'false',  	
	    Cant_Recibida Numeric(14,4) Not Null Default 0, 
	    Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	    CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	    Status varchar(1) Not Null Default 'A', 
	    Actualizado tinyint Not Null Default 0 
    ) 
End     
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_EntradasDet_Consignacion_Lotes' and xType = 'PK' ) 
Begin 
    Alter Table EntradasDet_Consignacion_Lotes Add Constraint PK_EntradasDet_Consignacion_Lotes 
	    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, ClaveLote ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasDet_Consignacion_Lotes_EntradasDet_Consignacion' and xType = 'F' ) 
Begin 
    Alter Table EntradasDet_Consignacion_Lotes Add Constraint FK_EntradasDet_Consignacion_Lotes_EntradasDet_Consignacion
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, IdProducto, CodigoEAN ) 
	    References EntradasDet_Consignacion ( IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, IdProducto, CodigoEAN ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasDet_Consignacion_Lotes_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'F' ) 
Begin 
    Alter Table EntradasDet_Consignacion_Lotes Add Constraint FK_EntradasDet_Consignacion_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	    References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
End 
Go--#SQL  


-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'U' ) 
Begin 
    Create Table EntradasDet_Consignacion_Lotes_Ubicaciones 
    (
	    IdEmpresa varchar(3) Not Null, 
	    IdEstado varchar(2) Not Null, 
	    IdFarmacia varchar(4) Not Null, 
	    IdSubFarmacia varchar(2) Not Null, 	
	    FolioEntrada varchar(30) Not Null, 
	    IdProducto varchar(8) Not Null, 
	    CodigoEAN varchar(30) Not Null, 
	    ClaveLote varchar(30) Not Null,
    	
	    IdPasillo int Not Null, 
	    IdEstante int Not Null, 
	    IdEntrepaño int Not Null,
     
	    Renglon int not null Default 0,  
	    EsConsignacion Bit Not Null Default 'false',  	
	    Cant_Recibida Numeric(14,4) Not Null Default 0, 
	    Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	    CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	    Status varchar(1) Not Null Default 'A', 
	    Actualizado tinyint Not Null Default 0 
    ) 
End     
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'PK' ) 
Begin 
    Alter Table EntradasDet_Consignacion_Lotes_Ubicaciones Add Constraint PK_EntradasDet_Consignacion_Lotes_Ubicaciones
	    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, ClaveLote, 
	                  IdPasillo, IdEstante, IdEntrepaño ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_EntradasDet_Consignacion_Lotes_Ubicaciones_EntradasDet_Consignacion_Lotes' and xType = 'F' ) 
Begin 
    Alter Table EntradasDet_Consignacion_Lotes_Ubicaciones Add Constraint FK_EntradasDet_Consignacion_Lotes_Ubicaciones_EntradasDet_Consignacion_Lotes
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, ClaveLote ) 
	    References EntradasDet_Consignacion_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, ClaveLote ) 
     
End 
Go--#SQL 



