If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IGPI_CFGC_Productos_Status' and xType = 'U' ) 
   Drop Table IGPI_CFGC_Productos_Status 
Go--#SQL  

Create Table IGPI_CFGC_Productos_Status 
(
	StatusIGPI tinyint Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL   

Alter Table IGPI_CFGC_Productos_Status Add Constraint PK_IGPI_CFGC_Productos_Status Primary Key ( StatusIGPI ) 
Go--#SQL   

Insert Into IGPI_CFGC_Productos_Status ( StatusIGPI, Descripcion ) Values ( 0, 'Ingreso permitido' ) 
Insert Into IGPI_CFGC_Productos_Status ( StatusIGPI, Descripcion ) Values ( 1, 'Ingreso no permitido' ) 
Insert Into IGPI_CFGC_Productos_Status ( StatusIGPI, Descripcion ) Values ( 2, 'Producto debe ser almacenado con caducidad' ) 
Insert Into IGPI_CFGC_Productos_Status ( StatusIGPI, Descripcion ) Values ( 4, 'Sin intrucciones de almacenamiento' ) 
Insert Into IGPI_CFGC_Productos_Status ( StatusIGPI, Descripcion ) Values ( 5, 'Almacenar producto en refrigerador' ) 
Go--#SQL   

------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IGPI_CFGC_Productos' and xType = 'U' ) 
   Drop Table IGPI_CFGC_Productos 
Go--#SQL   

Create Table IGPI_CFGC_Productos
( 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, -- Unique, 
	
	StatusIGPI tinyint Not Null Default 0, 
	
	EsMultipicking bit Not Null Default 'true', 	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL   

Alter Table IGPI_CFGC_Productos Add Constraint PK_IGPI_CFGC_Productos Primary Key ( IdProducto, CodigoEAN )
Go--#SQL   

Alter Table IGPI_CFGC_Productos Add Constraint FK_IGPI_CFGC_Productos_IGPI_CFGC_Productos_Status 
	Foreign Key ( StatusIGPI ) References IGPI_CFGC_Productos_Status ( StatusIGPI ) 
Go--#SQL   	 

/*  
Alter Table IGPI_CFGC_Productos Add Constraint FK_IGPI_CFGC_Productos_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SxQL   
*/ 

/* 
	drop table #tmpDatos
	
	select * from IGPI_CFGC_Productos 

	Insert Into IGPI_CFGC_Productos 
	select IdProducto, CodigoEAN, 0, 'A', 1  
	from #tmpDatos 

	select distinct v.IdProducto, v.CodigoEAN 
	into #tmpDatos 
	from IGPI_ExistenciaK_Historico H 
	inner Join vw_productos_codigoEAN v on ( H.CodigoEAN = v.CodigoEAN  )  
	order by IdProducto 
	
*/ 	