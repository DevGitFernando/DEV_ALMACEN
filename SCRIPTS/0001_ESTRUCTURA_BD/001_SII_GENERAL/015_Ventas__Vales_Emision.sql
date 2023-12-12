
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_Envio' and xType = 'U' ) 
	Drop Table Vales_Emision_Envio 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_InformacionAdicional' and xType = 'U' ) 
	Drop Table Vales_Emision_InformacionAdicional 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionDet' and xType = 'U' ) 
	Drop Table Vales_EmisionDet 
Go--#SQL  

-------------------------------------------
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionEnc' and xType = 'U' ) 
	Drop Table Vales_EmisionEnc
Go--#SQL  

Create Table Vales_EmisionEnc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVale varchar(30) Not Null, 
	FolioVenta varchar(30) Not Null, 
	EsSegundoVale bit Not Null Default 'false', 
	FolioCierre Int Not Null Default 0, 	
	FechaRegistro datetime Default getdate(), 
	FechaCanje datetime Default GetDate(),     --- Fecha en que se canjeo el vale  	
	IdPersonal varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 	

	IdPersonaFirma Varchar(100) Not Null Default '', 
	FolioTimbre Int Not Null Default 0, 
	FechaSistema Datetime Not Null Default getdate(),  

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate() 
)
Go--#SQL  

Alter Table Vales_EmisionEnc Add Constraint PK_Vales_EmisionEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
Go--#SQL  

--Alter Table Vales_EmisionEnc Add Constraint FK_Vales_EmisionEnc_VentasEnc 
--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
--Go--#SQL 

Alter Table Vales_EmisionEnc Add Constraint FK_Vales_EmisionEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table Vales_EmisionEnc Add Constraint FK_Vales_EmisionEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table Vales_EmisionEnc Add Constraint FK_Vales_EmisionEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_Envio' and xType = 'U' ) 
Begin 
Create Table Vales_Emision_Envio 
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	FolioVale varchar(30) Not Null,

	FechaRegistro datetime Default getdate(),
	
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_Envio' and xType = 'PK' )
Begin
Alter Table Vales_Emision_Envio Add Constraint PK_Vales_Emision_Envio
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale )
End 
Go--#SQL 

Alter Table Vales_Emision_Envio 
	Add Constraint FK_Vales_Emision_Envio___Vales_EmisionEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) References Vales_EmisionEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
Go--#SQL





------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionDet' and xType = 'U' ) 
	Drop Table Vales_EmisionDet 
Go--#SQL  

Create Table Vales_EmisionDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVale varchar(30) Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null,  	
	Cantidad Numeric(14,4) Not Null Default 0, 
	Cantidad_2 Numeric(14,4) Not Null Default 0, 	
	IdPresentacion varchar(3) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate() 	 
)
Go--#SQL 

Alter Table Vales_EmisionDet Add Constraint PK_Vales_EmisionDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale, IdClaveSSA_Sal ) 
Go--#SQL  

Alter Table Vales_EmisionDet Add Constraint FK_Vales_EmisionDet_Vales_EmisionEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) References Vales_EmisionEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
Go--#SQL  

Alter Table Vales_EmisionDet Add Constraint FK_Vales_EmisionDet_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL 

Alter Table Vales_EmisionDet Add Constraint FK_Vales_EmisionDet_CatPresentaciones 
	Foreign Key ( IdPresentacion ) References CatPresentaciones ( IdPresentacion ) 
Go--#SQL  
-------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_InformacionAdicional' and xType = 'U' ) 
	Drop Table Vales_Emision_InformacionAdicional 
Go--#SQL  

Create Table Vales_Emision_InformacionAdicional  
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVale varchar(30) Not Null, 
	IdBeneficiario varchar(8) Not Null, 
	IdTipoDeDispensacion varchar(2) Not Null Default '00', 
	NumReceta varchar(20) Not Null, 
	FechaReceta datetime Not Null default getdate(), 
	IdMedico varchar(6) Not Null, 
	IdBeneficio varchar(4) Not Null Default '0000', 
	IdDiagnostico varchar(6) Not Null, 
    IdUMedica varchar(6) Not Null Default '000000', 	
	IdServicio varchar(3) Not Null, 
	IdArea varchar(3) Not Null,  
	RefObservaciones varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate() 	 
) 
Go--#SQL  

Alter Table Vales_Emision_InformacionAdicional Add Constraint PK_Vales_Emision_InformacionAdicional Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
Go--#SQL  

Alter Table Vales_Emision_InformacionAdicional Add Constraint FK_Vales_Emision_InformacionAdicional_Vales_EmisionEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) References Vales_EmisionEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
Go--#SQL  

Alter Table Vales_Emision_InformacionAdicional Add Constraint FK_Vales_Emision_InformacionAdicional_CatMedicos 
	Foreign Key ( IdEstado, IdFarmacia, IdMedico ) References CatMedicos ( IdEstado, IdFarmacia, IdMedico ) 
Go--#SQL  

Alter Table Vales_Emision_InformacionAdicional Add Constraint FK_Vales_Emision_InformacionAdicional_CatServicios_Areas  
	Foreign Key ( IdServicio, IdArea ) References CatServicios_Areas ( IdServicio, IdArea ) 
Go--#SQL  

Alter Table Vales_Emision_InformacionAdicional Add Constraint FK_Vales_Emision_InformacionAdicional_CatBeneficios  
	Foreign Key ( IdBeneficio ) References CatBeneficios ( IdBeneficio ) 
Go--#SQL  

Alter Table Vales_Emision_InformacionAdicional Add Constraint FK_Vales_Emision_InformacionAdicional_CatTiposDispensacion  
	Foreign Key ( IdTipoDeDispensacion ) References CatTiposDispensacion ( IdTipoDeDispensacion ) 
Go--#SQL  

Alter Table Vales_Emision_InformacionAdicional Add Constraint FK_Vales_Emision_InformacionAdicional_CatUnidadesMedicas 
    Foreign Key ( IdUMedica ) References CatUnidadesMedicas ( IdUMedica )  
Go--#SQL  


--------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet_Lotes' and xType = 'U' ) 
	Drop Table ValesDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet' and xType = 'U' ) 
	Drop Table ValesDet 
Go--#SQL  

-------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesEnc' and xType = 'U' ) 
	Drop Table ValesEnc 
Go--#SQL  

Create Table ValesEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Folio varchar(30) Not Null, 
	FolioVale varchar(30) Not Null, 
    FolioVentaGenerado varchar(30) Not Null Default '', 
	FolioCierre Int Not Null Default 0, 	
	IdPersonal varchar(4) Not Null, 
	FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento 
	IdProveedor varchar(4) Not Null Default '', 
	ReferenciaDocto varchar(20) Not Null Default '', 
	FechaDocto datetime Not Null Default getdate(), 
	FechaVenceDocto datetime Not Null Default getdate() + 30, 
	Observaciones varchar(100) Not Null Default '', 	
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0,	
	FechaRegistro datetime Not Null Default getdate(),  
	EsReembolso Bit Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate() 	 
)
Go--#SQL  


Alter Table ValesEnc Add Constraint PK_ValesEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL  

Alter Table ValesEnc Add Constraint FK_ValesEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table ValesEnc Add Constraint FK_ValesEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table ValesEnc Add Constraint FK_ValesEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 


Alter Table ValesEnc Add Constraint FK_ValesEnc_CatProveedores 
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet' and xType = 'U' ) 
	Drop Table ValesDet 
Go--#SQL  

Create Table ValesDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	Folio varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 	
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,   	
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
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate() 
)
Go--#SQL 

Alter Table ValesDet Add Constraint PK_ValesDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table ValesDet Add Constraint FK_ValesDet_ValesEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) References ValesEnc ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL  

Alter Table ValesDet Add Constraint FK_ValesDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL  

---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet_Lotes' and xType = 'U' ) 
	Drop Table ValesDet_Lotes  
Go--#SQL  

Create Table ValesDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	Folio varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate() 
)
Go--#SQL 

Alter Table ValesDet_Lotes Add Constraint PK_ValesDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Folio, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table ValesDet_Lotes Add Constraint FK_ValesDet_Lotes_VentasDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, CodigoEAN, Renglon ) 
	References ValesDet ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table ValesDet_Lotes Add Constraint FK_ValesDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  




