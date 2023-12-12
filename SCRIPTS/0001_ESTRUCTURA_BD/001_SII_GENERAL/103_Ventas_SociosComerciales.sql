If Exists ( Select Name From SysObjects(NoLock) Where Name = 'VentasDet_SociosComerciales_Lotes_Ubicaciones' and xType = 'U' )
    Drop Table VentasDet_SociosComerciales_Lotes_Ubicaciones
Go--#SQL

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'VentasDet_SociosComerciales_Lotes' and xType = 'U' )
    Drop Table VentasDet_SociosComerciales_Lotes
Go--#SQL

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'VentasDet_SociosComerciales' and xType = 'U' )
    Drop Table VentasDet_SociosComerciales
Go--#SQL

------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'VentasEnc_SociosComerciales' and xType = 'U' )
    Drop Table VentasEnc_SociosComerciales
Go--#SQL

Create Table VentasEnc_SociosComerciales
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	FolioVenta varchar(8) Not Null,
	FolioMovtoInv varchar(30) Not Null,
	IdSocioComercial varchar(8) Not Null,
	IdSucursal varchar(8) Not Null,
	IdPersonal varchar(4) Not Null,
	FechaRegistro datetime Not Null Default getdate(),  --- Fecha de Sistema en que se realizo el movimiento
	Observaciones varchar(100) Not Null Default '',
	SubTotal Numeric(14,4) Not Null Default 0,
	Iva Numeric(14,4) Not Null Default 0,
	Total Numeric(14,4) Not Null Default 0,
	
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_VentasEnc_SociosComerciales' and xType = 'PK' )
Begin
    Alter Table VentasEnc_SociosComerciales Add Constraint PK_VentasEnc_SociosComerciales Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta )
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasEnc_SociosComerciales_CatEmpresas' and xType = 'F' )
Begin
    Alter Table VentasEnc_SociosComerciales Add Constraint FK_VentasEnc_SociosComerciales_CatEmpresas
	    Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasEnc_SociosComerciales_CatFarmacias' and xType = 'F' )
Begin
    Alter Table VentasEnc_SociosComerciales Add Constraint FK_VentasEnc_SociosComerciales_CatFarmacias
	    Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasEnc_SociosComerciales_CatPersonal' and xType = 'F' )
Begin
    Alter Table VentasEnc_SociosComerciales Add Constraint FK_VentasEnc_SociosComerciales_CatPersonal
	    Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasEnc_SociosComerciales_CatSociosComerciales_Sucursales' and xType = 'F' )
Begin
    Alter Table VentasEnc_SociosComerciales Add Constraint FK_VentasEnc_SociosComerciales_CatSociosComerciales_Sucursales
	    Foreign Key ( IdSocioComercial, IdSucursal ) References CatSociosComerciales_Sucursales ( IdSocioComercial, IdSucursal )
End
Go--#SQL

------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'VentasDet_SociosComerciales' and xType = 'U' )
    Drop Table VentasDet_SociosComerciales
Go--#SQL

Create Table VentasDet_SociosComerciales
(
    IdEmpresa varchar(3) Not Null,
    IdEstado varchar(2) Not Null,
    IdFarmacia varchar(4) Not Null,
    FolioVenta varchar(8) Not Null,
    IdProducto varchar(8) Not Null,
    CodigoEAN varchar(30) Not Null,
    Renglon int Not Null Default 0,
    UnidadDeEntrada smallint Not Null Default 1,
    Cant_Vendida Numeric(14,4) Not Null Default 0,
    EsFacturado bit Not Null Default 0,
    FolioFactura Varchar(20) Default '',
    FcehaFacturado DateTime Default GetDate(),
    Cant_Devuelta Numeric(14,4) Not Null Default 0,
    CantidadVendida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
    Precio Numeric(14,4) Not Null Default 0,
    TasaIva Numeric(14,4) Not Null Default 0,
    SubTotal Numeric(14,4) Not Null Default 0,
    ImpteIva Numeric(14,4) Not Null Default 0,
    Importe Numeric(14,4) Not Null Default 0,
    Status varchar(1) Not Null Default 'A',
    Actualizado tinyint Not Null Default 0
)
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_VentasDet_SociosComerciales' and xType = 'PK' )
Begin
    Alter Table VentasDet_SociosComerciales Add Constraint PK_VentasDet_SociosComerciales
        Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN )
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasDet_SociosComerciales_VentasEnc_SociosComerciales' and xType = 'F' )
Begin
    Alter Table VentasDet_SociosComerciales Add Constraint FK_VentasDet_SociosComerciales_VentasEnc_SociosComerciales
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta )
	    References VentasEnc_SociosComerciales ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasDet_SociosComerciales_CatProductos_CodigosRelacionados' and xType = 'F' )
Begin
    Alter Table VentasDet_SociosComerciales Add Constraint FK_VentasDet_SociosComerciales_CatProductos_CodigosRelacionados
	    Foreign Key ( IdProducto, CodigoEAN )
	    References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )
End
Go--#SQL
-------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'VentasDet_SociosComerciales_Lotes' and xType = 'U' )
    Drop Table VentasDet_SociosComerciales_Lotes
Go--#SQL

Create Table VentasDet_SociosComerciales_Lotes
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(8) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	IdSubFarmacia varchar(2) Not Null,
	ClaveLote varchar(30) Not Null, 
	Renglon int not null Default 0,  
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Vendida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadVendida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)   
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_VentasDet_SociosComerciales_Lotes' and xType = 'PK' ) 
Begin 
    Alter Table VentasDet_SociosComerciales_Lotes Add Constraint PK_VentasDet_SociosComerciales_Lotes 
	    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasDet_SociosComerciales_Lotes_VentasDet_SociosComerciales' and xType = 'F' ) 
Begin 
    Alter Table VentasDet_SociosComerciales_Lotes Add Constraint FK_VentasDet_SociosComerciales_Lotes_VentasDet_SociosComerciales
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN ) 
	    References VentasDet_SociosComerciales ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasDet_SociosComerciales_Lotes_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'F' ) 
Begin 
    Alter Table VentasDet_SociosComerciales_Lotes Add Constraint FK_VentasDet_SociosComerciales_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	    References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
End 
Go--#SQL
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'VentasDet_SociosComerciales_Lotes_Ubicaciones' and xType = 'U' )
    Drop Table VentasDet_SociosComerciales_Lotes_Ubicaciones
Go--#SQL

Create Table VentasDet_SociosComerciales_Lotes_Ubicaciones 
(
    IdEmpresa varchar(3) Not Null,
    IdEstado varchar(2) Not Null,
    IdFarmacia varchar(4) Not Null,
    FolioVenta varchar(8) Not Null,
    IdProducto varchar(8) Not Null,
    CodigoEAN varchar(30) Not Null,
    IdSubFarmacia varchar(2) Not Null,
    ClaveLote varchar(30) Not Null,
	
    IdPasillo int Not Null,
    IdEstante int Not Null,
    IdEntrepaño int Not Null,
 
    Renglon int not null Default 0,
    EsConsignacion Bit Not Null Default 'false',
    Cant_Vendida Numeric(14,4) Not Null Default 0,
    Cant_Devuelta Numeric(14,4) Not Null Default 0,
    CantidadVendida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
    Status varchar(1) Not Null Default 'A',
    Actualizado tinyint Not Null Default 0
)
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_VentasDet_SociosComerciales_Lotes_Ubicaciones' and xType = 'PK' )
Begin
    Alter Table VentasDet_SociosComerciales_Lotes_Ubicaciones Add Constraint PK_VentasDet_SociosComerciales_Lotes_Ubicaciones
	    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote,
	                  IdPasillo, IdEstante, IdEntrepaño )
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_VentasDet_SociosComerciales_Lotes_Ubicaciones_VentasDet_SociosComerciales_Lotes' and xType = 'F' )
Begin
    Alter Table VentasDet_SociosComerciales_Lotes_Ubicaciones Add Constraint FK_VentasDet_SociosComerciales_Lotes_Ubicaciones_VentasDet_SociosComerciales_Lotes
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote )
	    References VentasDet_SociosComerciales_Lotes ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote )
End
Go--#SQL



