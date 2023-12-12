------------------------------------------------------------------------------------------------ 
/* 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_ListaDePreciosContadoHistorico' and xType = 'U' ) 
   Drop Table COM_OCEN_ListaDePreciosContadoHistorico  
Go 
*/ 

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_ListaDePreciosHistorico_Claves' and xType = 'U' ) 
   Drop Table COM_OCEN_ListaDePreciosHistorico_Claves  
Go--#SQL

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_ListaDePreciosHistorico' and xType = 'U' ) 
   Drop Table COM_OCEN_ListaDePreciosHistorico  
Go--#SQL 


-------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_ListaDePrecios' and xType = 'U' ) 
   Drop Table COM_OCEN_ListaDePrecios  
Go--#SQL 

Create Table COM_OCEN_ListaDePrecios 
(
	IdProveedor varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	CodigoEAN varchar(30) Not Null, 	
	Precio Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 			
	Iva Numeric(14,4) Not Null Default 0, 	
	PrecioUnitario Numeric(14,4) Not Null Default 0, 		
		
	FechaRegistro datetime Not Null Default getdate(), 
	FechaFinVigencia datetime Not Null Default getdate() + 15, 	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table COM_OCEN_ListaDePrecios Add Constraint PK_COM_OCEN_ListaDePrecios Primary Key ( IdProveedor, IdClaveSSA, CodigoEAN ) 
Go--#SQL 

Alter Table COM_OCEN_ListaDePrecios Add Constraint PK_COM_OCEN_ListaDePrecios_CatProveedores 
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor )  
Go--#SQL 	

Alter Table COM_OCEN_ListaDePrecios Add Constraint PK_COM_OCEN_ListaDePrecios_CatProductos_CodigosRelacionados 
	Foreign Key ( CodigoEAN ) References CatProductos_CodigosRelacionados ( CodigoEAN )  
Go--#SQL 	

Alter Table COM_OCEN_ListaDePrecios Add Constraint PK_COM_OCEN_ListaDePrecios_CatClavesSSA_Sales  
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )  
Go--#SQL 	

---------------------------
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_ListaDePreciosHistorico' and xType = 'U' ) 
   Drop Table COM_OCEN_ListaDePreciosHistorico  
Go--#SQL 

Create Table COM_OCEN_ListaDePreciosHistorico 
(
	IdProveedor varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 	
	CodigoEAN varchar(30) Not Null, 
	Precio Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 			
	Iva Numeric(14,4) Not Null Default 0, 	
	PrecioUnitario Numeric(14,4) Not Null Default 0, 		
		
	FechaActualizacion datetime Not Null Default getdate(), 	
	
	FechaRegistro datetime Not Null Default getdate(), 
	FechaFinVigencia datetime Not Null Default getdate() + 15, 	
	Keyx int identity(1,1), 
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table COM_OCEN_ListaDePreciosHistorico 
	Add Constraint PK_COM_OCEN_ListaDePreciosHistorico Primary Key ( IdProveedor, IdClaveSSA, CodigoEAN, FechaActualizacion ) 
Go--#SQL 

Alter Table COM_OCEN_ListaDePreciosHistorico 
	Add Constraint FK_COM_OCEN_ListaDePreciosHistorico_COM_OCEN_ListaDePrecios 
	Foreign Key ( IdProveedor, IdClaveSSA, CodigoEAN ) References COM_OCEN_ListaDePrecios ( IdProveedor, IdClaveSSA, CodigoEAN )  
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------
---------------------  MANEJO POR CLAVES --------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_ListaDePrecios_Claves' and xType = 'U' ) 
   Drop Table COM_OCEN_ListaDePrecios_Claves  
Go--#SQL 

Create Table COM_OCEN_ListaDePrecios_Claves 
(
	IdProveedor varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	--CodigoEAN varchar(30) Not Null, 	
	Precio Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 			
	Iva Numeric(14,4) Not Null Default 0, 	
	PrecioUnitario Numeric(14,4) Not Null Default 0, 		
		
	FechaRegistro datetime Not Null Default getdate(), 
	FechaFinVigencia datetime Not Null Default getdate() + 15, 	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table COM_OCEN_ListaDePrecios_Claves Add Constraint PK_COM_OCEN_ListaDePrecios_Claves Primary Key ( IdProveedor, IdClaveSSA ) 
Go--#SQL 

Alter Table COM_OCEN_ListaDePrecios_Claves Add Constraint PK_COM_OCEN_ListaDePrecios_Claves_CatProveedores 
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor )  
Go--#SQL 	

Alter Table COM_OCEN_ListaDePrecios_Claves Add Constraint PK_COM_OCEN_ListaDePrecios_Claves_CatClavesSSA_Sales  
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )  
Go--#SQL 	

---------------------------
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_ListaDePreciosHistorico_Claves' and xType = 'U' ) 
   Drop Table COM_OCEN_ListaDePreciosHistorico_Claves  
Go--#SQL 

Create Table COM_OCEN_ListaDePreciosHistorico_Claves 
(
	IdProveedor varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 	
	--CodigoEAN varchar(30) Not Null, 
	Precio Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 			
	Iva Numeric(14,4) Not Null Default 0, 	
	PrecioUnitario Numeric(14,4) Not Null Default 0, 		
		
	FechaActualizacion datetime Not Null Default getdate(), 	
	
	FechaRegistro datetime Not Null Default getdate(), 
	FechaFinVigencia datetime Not Null Default getdate() + 15, 	
	Keyx int identity(1,1), 
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table COM_OCEN_ListaDePreciosHistorico_Claves 
	Add Constraint PK_COM_OCEN_ListaDePreciosHistorico_Claves Primary Key ( IdProveedor, IdClaveSSA, FechaActualizacion ) 
Go--#SQL 

Alter Table COM_OCEN_ListaDePreciosHistorico_Claves 
	Add Constraint FK_COM_OCEN_ListaDePreciosHistorico_Claves_COM_OCEN_ListaDePrecios_Claves 
	Foreign Key ( IdProveedor, IdClaveSSA ) References COM_OCEN_ListaDePrecios_Claves  ( IdProveedor, IdClaveSSA )  
Go--#SQL 