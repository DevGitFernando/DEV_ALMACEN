-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_Imagenes' and xType = 'U' ) 
Begin 
	Create Table CatProductos_Imagenes 
	(
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null,	
		Consecutivo int Not Null,
			
		NombreImagen varchar(200) Not Null Default '',
		Imagen varchar(max) Not Null Default '', 
		Ancho numeric(14, 4) Not Null Default 800, 
		Alto numeric(14, 4) Not Null Default 800, 

		FechaRegistro datetime Not Null Default GetDate(),
		IdPersonal varchar(4) Not Null Default '',
		
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table CatProductos_Imagenes Add Constraint PK_CatProductos_Imagenes Primary Key ( IdProducto, CodigoEAN, Consecutivo ) 

	Alter Table CatProductos_Imagenes Add Constraint FK_CatProductos_Imagenes_CatProductos_CodigosRelacionados 
		Foreign Key ( IdProducto, CodigoEAN )  References CatProductos_CodigosRelacionados  ( IdProducto, CodigoEAN ) 
	
End 	
Go--#SQL  
