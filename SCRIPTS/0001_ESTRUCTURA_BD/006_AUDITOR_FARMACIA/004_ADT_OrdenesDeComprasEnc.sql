
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_OrdenesDeComprasEnc' and xType = 'U' ) 
	Drop Table Adt_OrdenesDeComprasEnc 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_OrdenesDeComprasEnc' and xType = 'U' ) 
Begin 
Create Table Adt_OrdenesDeComprasEnc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioOrdenCompra varchar(30) Not Null, 
	FolioMovto varchar(30) Not Null, 

	ReferenciaDocto varchar(20) Not Null Default '', 
	FechaDocto datetime Not Null Default getdate(), 
	FechaVenceDocto datetime Not Null Default getdate(), 
	ImporteFactura numeric(14, 4) Not Null Default 0, 

	ReferenciaDocto_Anterior varchar(20) Not Null Default '', 
	FechaDocto_Anterior datetime Not Null Default getdate(), 
	FechaVenceDocto_Anterior datetime Not Null Default getdate(), 
	ImporteFactura_Anterior numeric(14, 4) Not Null Default 0, 

	IdPersonal varchar(4) Not Null, 	
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_Adt_OrdenesDeComprasEnc' and xType = 'PK' ) 
Begin 
	Alter Table Adt_OrdenesDeComprasEnc Add Constraint PK_Adt_OrdenesDeComprasEnc 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, FolioMovto ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_Adt_OrdenesDeComprasEnc_OrdenesDeComprasEnc' and xType = 'F' ) 
Begin 
	Alter Table Adt_OrdenesDeComprasEnc Add Constraint FK_Adt_OrdenesDeComprasEnc_OrdenesDeComprasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra ) 
		References OrdenesDeComprasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_Adt_OrdenesDeComprasEnc_CatPersonal' and xType = 'F') 
Begin 
	Alter Table Adt_OrdenesDeComprasEnc Add Constraint FK_Adt_OrdenesDeComprasEnc_CatPersonal  
		Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
End
Go--#SQL