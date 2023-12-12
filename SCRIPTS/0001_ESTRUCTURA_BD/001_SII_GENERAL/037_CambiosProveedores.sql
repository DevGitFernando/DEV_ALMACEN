If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
	Drop Table CambiosProv_Det_CodigosEAN_Lotes 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN' and xType = 'U' ) 
	Drop Table CambiosProv_Det_CodigosEAN 
Go--#SQL  


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Enc' and xType = 'U' ) 
	Drop Table CambiosProv_Enc 
Go--#SQL  

Create Table CambiosProv_Enc 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	FolioCambio varchar(30) Not Null, 
	IdProveedor varchar(4) Not Null Default '', 
	TipoMovto varchar(4) Not Null Default 'ECP', 	              ---- ECP SCP --- 
	FechaRegistro datetime Default GetDate(),  --- Fecha en que se registro movimiento
	IdPersonal varchar(6) Not Null Default '', 
	Observaciones varchar(500) Not Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	Keyx int identity(1,1), 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CambiosProv_Enc Add Constraint PK_CambiosProv_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio  )
Go--#SQL  

Alter Table CambiosProv_Enc Add Constraint FK_CambiosProv_Enc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table CambiosProv_Enc Add Constraint FK_CambiosProv_Enc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN' and xType = 'U' ) 
	Drop Table CambiosProv_Det_CodigosEAN 
Go--#SQL  

Create Table CambiosProv_Det_CodigosEAN 
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
Go--#SQL 

Alter Table CambiosProv_Det_CodigosEAN Add Constraint PK_CambiosProv_Det_CodigosEAN 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN ) 
Go--#SQL  

--Alter Table CambiosProv_Det_CodigosEAN Add Constraint FK_CambiosProv_Det_CodigosEAN_CambiosProv_ 
--	Foreign Key ( IdEstado, IdFarmacia, FolioCambio, IdProducto ) References CambiosProv ( IdEstado, IdFarmacia, FolioCambio , IdProducto ) 
--Go
Alter Table CambiosProv_Det_CodigosEAN Add Constraint FK_CambiosProv_Det_CodigosEAN_CambiosProv_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio ) References CambiosProv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio ) 
Go--#SQL 

Alter Table CambiosProv_Det_CodigosEAN Add Constraint FK_CambiosProv_Det_CodigosEAN_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )  
Go--#SQL  

Alter Table CambiosProv_Det_CodigosEAN With NoCheck 
	Add Constraint CK_CambiosProv_Det_CodigosEAN_Cantidad Check Not For Replication (Cantidad > 0)
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
	Drop Table CambiosProv_Det_CodigosEAN_Lotes 
Go--#SQL  

Create Table CambiosProv_Det_CodigosEAN_Lotes 
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
Go--#SQL 

Alter Table CambiosProv_Det_CodigosEAN_Lotes Add Constraint PK_CambiosProv_Det_CodigosEAN_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCambio, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  

Alter Table CambiosProv_Det_CodigosEAN_Lotes Add Constraint FK_CambiosProv_Det_CodigosEAN_Lotes_CambiosProv_Det_CodigosEAN
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN ) 
	References CambiosProv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN ) 
Go--#SQL 

----Alter Table CambiosProv_Det_CodigosEAN_Lotes Add Constraint FK_CambiosProv_Det_CodigosEAN_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote ) 
----	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote ) 
----Go--#SQL  

Alter Table CambiosProv_Det_CodigosEAN_Lotes With NoCheck 
	Add Constraint CK_CambiosProv_Det_CodigosEAN_Lotes_Cantidad Check Not For Replication (Cantidad > 0)
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'U' ) 
	Drop Table CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones 
Go--#SQL  

Create Table CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones 
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
Go--#SQL 

Alter Table CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint PK_CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote,
					IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL  

Alter Table CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint FK_CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones_CambiosProv_Det_CodigosEAN_Lotes
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote ) 
	References CambiosProv_Det_CodigosEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote ) 
Go--#SQL 


