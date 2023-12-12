-----------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatRegistrosSanitarios' and xType = 'U' ) 
Begin 
	Create Table CatRegistrosSanitarios 
	( 
		Folio Varchar(8) Not Null Default '',
		IdLaboratorio Varchar(4) Not Null Default '',
		IdClaveSSA_Sal varchar(4) Not Null Default '',
		Consecutivo varchar(4) Not Null Default '',
		Tipo varchar(3) Not Null Default '',
		Año varchar(4) Not Null Default '',	
		FechaVigencia DateTime Not Null,
		Documento text default '',
		NombreDocto varchar(200) Default '', 
		FolioRegistroSanitario Varchar(30) Not Null Default '', 

		MD5 varchar(50) Not Null Default '',  
		FechaUltimaActualizacion datetime Not Null Default getdate(), 
		FechaRegistro datetime Not Null Default getdate(),  
		TipoArchivo varchar(10) Not Null Default '', 	
		TamañoArchivo_MB numeric(14, 4) Not Null Default 0, 
		TamañoArchivoAux_MB numeric(14, 4) Not Null Default 0, 
		IdPaisFabricacion varchar(3) Not Null Default '000', 
		IdPresentacion varchar(3) Not Null Default '000', 
		TipoCaducidad smallint Not Null Default 0, 
		Caducidad smallint Not Null Default 0, 

		Status varchar(4) Not Null Default '', 
		Actualizado tinyint Not Null Default 0 
	)
	
	Alter Table CatRegistrosSanitarios Add Constraint PK_CatRegistrosSanitarios Primary Key ( Folio )
		
	Alter Table CatRegistrosSanitarios Add Constraint FK_CatRegistrosSanitarios_CatLaboratorios
		Foreign Key (IdLaboratorio) References CatLaboratorios ( IdLaboratorio ) 

	Alter Table CatRegistrosSanitarios Add Constraint FK_CatRegistrosSanitarios_CatClavesSSA_Sales
		Foreign Key (IdClaveSSA_Sal) References CatClavesSSA_Sales ( IdClaveSSA_Sal )

	Alter Table CatRegistrosSanitarios Add Constraint FK_CatRegistrosSanitarios__CatRegistrosSanitarios_PaisFabricacion 
		Foreign Key ( IdPaisFabricacion ) References CatRegistrosSanitarios_PaisFabricacion ( IdPais )  

	Alter Table CatRegistrosSanitarios Add Constraint FK_CatRegistrosSanitarios__CatRegistrosSanitarios_Presentaciones 
		Foreign Key ( IdPresentacion ) References CatRegistrosSanitarios_Presentaciones ( IdPresentacion ) 


End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatRegistrosSanitarios_CodigoEAN' and xType = 'U' ) 
Begin 
	Create Table CatRegistrosSanitarios_CodigoEAN 
	( 
		Folio Varchar(8) Not Null Default '',
		IdProducto varchar(8) Not Null,
		CodigoEAN varchar(30) Not Null,
		Status varchar(4) Not Null Default '', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CatRegistrosSanitarios_CodigoEAN Add Constraint PK_CatRegistrosSanitarios_CodigoEAN 
		Primary Key ( Folio, IdProducto, CodigoEAN )

	Alter Table CatRegistrosSanitarios_CodigoEAN Add Constraint FK_CatRegistrosSanitarios_CodigoEAN_CatRegistrosSanitarios
		Foreign Key (Folio) References CatRegistrosSanitarios ( Folio )

	Alter Table CatRegistrosSanitarios_CodigoEAN Add Constraint FK_CatRegistrosSanitarios_CodigoEAN_CodigosRelacionados
		Foreign Key (IdProducto, CodigoEAN) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
	
End 	
Go--#SQL