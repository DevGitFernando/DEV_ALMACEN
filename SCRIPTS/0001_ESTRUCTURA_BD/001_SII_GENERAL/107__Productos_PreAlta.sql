------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_PreAlta' and xType = 'U' ) 
Begin 
	Create Table CatProductos_PreAlta 
	(
		FechaSolicitud datetime Not Null Default getdate(), 	
		IdProducto_Alta varchar(8) Not Null, 
		IdClaveSSA_Sal varchar(4) Not Null, 
		Descripcion varchar(200) Not Null Default '', 
		DescripcionCorta varchar(100) Not Null Default '', 	
		IdClasificacion varchar(4) Not Null Default '', 
		IdTipoProducto varchar(2) Not Null Default '1', 
		EsControlado bit default 'False', 
		EsAntibiotico bit default 'False', 	
		IdLaboratorio varchar(4) Default '', 
		IdPresentacion varchar(3) Not Null, 
		Descomponer bit Not Null default 'false', 
		ContenidoPaquete int Not Null Default 0, 
		ManejaCodigoEAN bit Not Null default 'true', 
		PrecioMaxPublico numeric(14, 4) Not Null Default 0,
		EsSectorSalud bit Not Null Default 'false',  
		Status varchar(1) Not Null Default 'A' 
	)

	Alter Table CatProductos_PreAlta Add Constraint PK_CatProductos_PreAlta Primary Key ( IdProducto_Alta ) 

	Alter Table CatProductos_PreAlta Add Constraint FK_CatProductos_PreAlta_CatClavesSSA_Sales 
		Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )

	Alter Table CatProductos_PreAlta Add Constraint FK_CatProductos_PreAlta_CatLaboratorios 
		Foreign Key ( IdLaboratorio ) References CatLaboratorios ( IdLaboratorio )

	Alter Table CatProductos_PreAlta Add Constraint FK_CatProductos_PreAlta_CatClasificacionesSSA 
		Foreign Key ( IdClasificacion ) References CatClasificacionesSSA ( IdClasificacion ) 

	Alter Table CatProductos_PreAlta Add Constraint FK_CatProductos_PreAlta_CatPresentaciones 
		Foreign Key ( IdPresentacion ) References CatPresentaciones ( IdPresentacion ) 

	Alter Table CatProductos_PreAlta Add Constraint FK_CatProductos_PreAlta_CatTiposDeProducto 
		Foreign Key ( IdTipoProducto ) References CatTiposDeProducto ( IdTipoProducto ) 

End 
Go--#SQL  



-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_PreAlta_Imagenes' and xType = 'U' ) 
Begin 
	Create Table CatProductos_PreAlta_Imagenes 
	(
		IdProducto varchar(8) Not Null, 
		Consecutivo int Not Null,
	
	
		NombreImagen varchar(200) Not Null Default '',
		Imagen varchar(max) Not Null Default '', 
		Ancho numeric(14, 4) Not Null Default 800,   
		Alto numeric(14, 4) Not Null Default 800, 
	
	
		FechaRegistro datetime Not Null Default GetDate(), 
	
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table CatProductos_PreAlta_Imagenes Add Constraint PK_CatProductos_PreAlta_Imagenes Primary Key ( IdProducto, Consecutivo ) 
End 
Go--#SQL 

	