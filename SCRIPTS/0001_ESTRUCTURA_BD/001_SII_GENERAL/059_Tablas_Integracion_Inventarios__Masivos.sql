If Exists ( Select * From sysobjects (NoLock) Where Name = 'INV__Inventario_CargaMasiva' and xType = 'U' ) 
   Drop Table INV__Inventario_CargaMasiva
Go--#SQL 

Create Table INV__Inventario_CargaMasiva  
( 
	IdEmpresa varchar(5) default '', 
	IdEstado varchar(4) default '', 
	IdFarmacia varchar(6) default '', 		
	IdSubFarmacia varchar(2) default '', 	
	IdPasillo varchar(20) default '', 
	IdEstante varchar(10) default '', 
	IdEntrepaño varchar(10) default '', 
	CodigoEAN varchar(30) default '', 
	Costo numeric(14,4) default 0, 
	ClaveLote varchar(30) default '', 		
	Caducidad varchar(30) default '', 
	Cantidad int 
) 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From sysobjects (NoLock) Where Name = 'INV__InventarioInterno_CargaMasiva' and xType = 'U' ) 
   Drop Table INV__InventarioInterno_CargaMasiva
Go--#SQL 

Create Table INV__InventarioInterno_CargaMasiva  
( 
	IdEmpresa varchar(5) default '', 
	IdEstado varchar(4) default '', 
	IdFarmacia varchar(6) default '', 		
	IdSubFarmacia varchar(2) default '', 
	IdPasillo varchar(20) default '', 
	IdEstante varchar(10) default '', 
	IdEntrepaño varchar(10) default '', 
	IdProducto varchar(30) default '', 	
	CodigoEAN varchar(30) default '', 
	ClaveSSA varchar(30) default '', 
	DescripcionClaveSSA varchar(max) default '', 
	ContenidoPaquete numeric(14,2) Not Null Default 0, 
	Costo numeric(14,4) default 0, 
	TasaIVA numeric(14,4) default 0, 
	ClaveLote varchar(30) default '', 
	EsConsignacion bit Not Null default 'false', 
	Caducidad varchar(30) default '', 
	Cantidad int, 
	CajasCompletas numeric(14,2) Not Null Default 1, 
	TipoDeInventario smallint Not Null Default 1, 
	TipoDeInventarioCorrecto smallint Not Null Default 1, 
	EnInventario bit Not Null Default 'true'  
) 
Go--#SQL 
