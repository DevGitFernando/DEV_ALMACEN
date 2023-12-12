If Exists ( Select * From Sysobjects (NoLock) Where Name = 'PedidosDet_Consignacion' and xType = 'U' ) 
   Drop Table PedidosDet_Consignacion
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFG_CNSGN_Proveedores_SubFarmacias' and xType = 'U' ) 
   Drop Table CFG_CNSGN_Proveedores_SubFarmacias  
Go--#SQL 

Create Table CFG_CNSGN_Proveedores_SubFarmacias 
( 
	IdEstado varchar(2) Not Null, 
	IdProveedor varchar(4) Not Null Default '',
	IdSubFarmacia varchar(2) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado int Not Null Default 0 
) 
Go--#SQL 

Alter Table CFG_CNSGN_Proveedores_SubFarmacias Add Constraint PK_CFG_CNSGN_Proveedores_SubFarmacias 
	Primary Key ( IdEstado, IdProveedor ) 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'PedidosEnc_Consignacion' and xType = 'U' ) 
   Drop Table PedidosEnc_Consignacion
Go--#SQL 

Create Table PedidosEnc_Consignacion 
( 
    IdEmpresa varchar(3) Not Null, 
    IdEstado varchar(2) Not Null, 
    IdFarmacia varchar(4) Not Null, 
    Folio varchar(30) Not Null,
    IdPersonal varchar(4) Not Null, 
    FechaRegistro datetime Not Null Default getdate(),  --- Fecha de Sistema en que se realizo el movimiento
    FechaPedido datetime Not Null Default getdate(),
    FechaEntrega datetime Not Null Default getdate(),
    IdProveedor varchar(4) Not Null Default '', 
    ReferenciaPedido varchar(20) Not Null Default '', 
    Observaciones varchar(100) Not Null Default '', 
	
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_PedidosEnc_Consignacion' and xType = 'PK' ) 
Begin 
    Alter Table PedidosEnc_Consignacion Add Constraint PK_PedidosEnc_Consignacion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_PedidosEnc_Consignacion_CatEmpresas' and xType = 'F' ) 
Begin 
    Alter Table PedidosEnc_Consignacion Add Constraint FK_PedidosEnc_Consignacion_CatEmpresas 
	    Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_PedidosEnc_Consignacion_CatFarmacias' and xType = 'F' ) 
Begin 
    Alter Table PedidosEnc_Consignacion Add Constraint FK_PedidosEnc_Consignacion_CatFarmacias 
	    Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_PedidosEnc_Consignacion_CatPersonal' and xType = 'F' ) 
Begin 
    Alter Table PedidosEnc_Consignacion Add Constraint FK_PedidosEnc_Consignacion_CatPersonal  
	    Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_PedidosEnc_Consignacion_CatPersonal' and xType = 'F' ) 
Begin 
    Alter Table PedidosEnc_Consignacion Add Constraint FK_PedidosEnc_Consignacion_CatPersonal  
	    Foreign Key ( Idproveedor ) References CatProveddores 
End 
Go--#SQL

------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'PedidosDet_Consignacion' and xType = 'U' ) 
   Drop Table PedidosDet_Consignacion
Go--#SQL 

    Create Table PedidosDet_Consignacion 
    (
	    IdEmpresa varchar(3) Not Null, 
	    IdEstado varchar(2) Not Null, 
	    IdFarmacia varchar(4) Not Null,  
	    Folio varchar(30) Not Null, 
		IdClaveSSA varchar(4) Not Null Default '',
		ClaveSSA varchar(30) Not Null default '', 
		DescripcionClaveSSA varchar(max) default '', 
		Costo numeric(14,4) default 0, 
		Cantidad int,
		Iva numeric(14,4) default 0, 
	    Status varchar(1) Not Null Default 'A', 
	    Actualizado tinyint Not Null Default 0 
    ) 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_PedidosDet_Consignacion' and xType = 'PK' ) 
Begin 
    Alter Table PedidosDet_Consignacion Add Constraint PK_PedidosDet_Consignacion 
        Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdClaveSSA, ClaveSSA  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_PedidosDet_Consignacion_PedidosEnc_Consignacion' and xType = 'F' ) 
Begin 
    Alter Table PedidosDet_Consignacion Add Constraint FK_PedidosDet_Consignacion_PedidosEnc_Consignacion 
	    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) References PedidosEnc_Consignacion
End 
Go--#SQL