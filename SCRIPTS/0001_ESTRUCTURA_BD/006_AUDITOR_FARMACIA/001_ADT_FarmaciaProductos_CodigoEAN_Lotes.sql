---- Manejar Existencia por CodigoEAN Lotes
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
	Drop Table Adt_FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 

Create Table Adt_FarmaciaProductos_CodigoEAN_Lotes
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	FolioMovto varchar(30) Not Null, 
	FechaCaducidad datetime Not Null Default getdate(), 
	FechaCaducidad_Anterior datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
    IdPersonal varchar(4) Not Null, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table Adt_FarmaciaProductos_CodigoEAN_Lotes Add Constraint PK_Adt_FarmaciaProductos_CodigoEAN_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FolioMovto )
Go--#SQL

Alter Table Adt_FarmaciaProductos_CodigoEAN_Lotes Add Constraint FK_Adt_FarmaciaProductos_CodigoEAN_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL 

----Alter Table Adt_FarmaciaProductos_CodigoEAN_Lotes Add Constraint FK_Adt_FarmaciaProductos_CodigoEAN_Lotes_FarmaciaProductos_CodigoEAN 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN ) 
----	References FarmaciaProductos_CodigoEAN ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN ) 
----Go--#SQL 

Alter Table Adt_FarmaciaProductos_CodigoEAN_Lotes Add Constraint FK_Adt_FarmaciaProductos_CodigoEAN_Lotes_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 
