If Exists ( Select * From sysobjects (NoLock) Where Name = 'COM_OCEN_Proveedores_Ordenes_Leidas' and xType = 'U' )
   Drop Table COM_OCEN_Proveedores_Ordenes_Leidas 
Go--#SQL 

Create Table COM_OCEN_Proveedores_Ordenes_Leidas 
(
	IdProveedor varchar(4) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioOrden varchar(8) Not Null  
) 
Go--#SQL 

Alter Table COM_OCEN_Proveedores_Ordenes_Leidas Add Constraint PK_COM_OCEN_Proveedores_Ordenes_Leidas 
	Primary Key ( IdProveedor, IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
Go--#SQL 

