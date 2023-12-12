If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_ComprasEnc' and xType = 'U' ) 
	Drop Table Adt_ComprasEnc 
Go--#SQL 

Create Table Adt_ComprasEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioCompra varchar(30) Not Null, 
	FolioMovto varchar(30) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdProveedor varchar(4) Not Null Default '', 
	ReferenciaDocto varchar(20) Not Null Default '', 
	FechaDocto datetime Not Null Default getdate(),  
	FechaRegistro datetime Not Null Default getdate(),  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table Adt_ComprasEnc Add Constraint PK_Adt_ComprasEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCompra, FolioMovto ) 
Go--#SQL  

Alter Table Adt_ComprasEnc Add Constraint FK_Adt_ComprasEnc_ComprasEnc
	Foreign Key (  IdEmpresa, IdEstado, IdFarmacia, FolioCompra ) 
	References ComprasEnc (  IdEmpresa, IdEstado, IdFarmacia, FolioCompra ) 
Go--#SQL 

Alter Table Adt_ComprasEnc Add Constraint FK_Adt_ComprasEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 

Alter Table Adt_ComprasEnc Add Constraint FK_Adt_ComprasEnc_CatProveedores 
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL  

----Alter Table Adt_ComprasEnc Add Constraint FK_Adt_ComprasEnc_CatEmpresas 
----	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
----Go--#SQL 
----
----Alter Table Adt_ComprasEnc Add Constraint FK_Adt_ComprasEnc_CatFarmacias 
----	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
----Go--#SQL 

