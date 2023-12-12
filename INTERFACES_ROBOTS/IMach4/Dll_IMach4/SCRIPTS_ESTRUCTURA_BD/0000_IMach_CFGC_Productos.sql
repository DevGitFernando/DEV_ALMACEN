If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Productos_Status' and xType = 'U' ) 
   Drop Table IMach_CFGC_Productos_Status 
Go--#SQL  

Create Table IMach_CFGC_Productos_Status 
(
	StatusIMach tinyint Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL   

Alter Table IMach_CFGC_Productos_Status Add Constraint PK_IMach_CFGC_Productos_Status Primary Key ( StatusIMach ) 
Go--#SQL   

Insert Into IMach_CFGC_Productos_Status ( StatusIMach, Descripcion ) Values ( 0, 'Ingreso permitido' ) 
Insert Into IMach_CFGC_Productos_Status ( StatusIMach, Descripcion ) Values ( 1, 'Ingreso no permitido' ) 
Insert Into IMach_CFGC_Productos_Status ( StatusIMach, Descripcion ) Values ( 2, 'Producto debe ser almacenado con caducidad' ) 
Insert Into IMach_CFGC_Productos_Status ( StatusIMach, Descripcion ) Values ( 4, 'Sin intrucciones de almacenamiento' ) 
Insert Into IMach_CFGC_Productos_Status ( StatusIMach, Descripcion ) Values ( 5, 'Almacenar producto en refrigerador' ) 
Go--#SQL   

------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Productos' and xType = 'U' ) 
   Drop Table IMach_CFGC_Productos 
Go--#SQL   

Create Table IMach_CFGC_Productos
( 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, -- Unique, 
	
	StatusIMach tinyint Not Null Default 0, 
	
	EsMultipicking bit Not Null Default 'true', 	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL   

Alter Table IMach_CFGC_Productos Add Constraint PK_IMach_CFGC_Productos Primary Key ( IdProducto, CodigoEAN )
Go--#SQL   

Alter Table IMach_CFGC_Productos Add Constraint FK_IMach_CFGC_Productos_IMach_CFGC_Productos_Status 
	Foreign Key ( StatusIMach ) References IMach_CFGC_Productos_Status ( StatusIMach ) 
Go--#SQL   	 

/*  
Alter Table IMach_CFGC_Productos Add Constraint FK_IMach_CFGC_Productos_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SxQL   
*/ 

/* 
	drop table #tmpDatos
	
	select * from IMach_CFGC_Productos 

	Insert Into IMach_CFGC_Productos 
	select IdProducto, CodigoEAN, 0, 'A', 1  
	from #tmpDatos 

	select distinct v.IdProducto, v.CodigoEAN 
	into #tmpDatos 
	from IMach_ExistenciaK_Historico H 
	inner Join vw_productos_codigoEAN v on ( H.CodigoEAN = v.CodigoEAN  )  
	order by IdProducto 
	
*/ 	