If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_StockProductos' and xType = 'U' ) 
   Drop Table IMach_StockProductos
Go--#SQL   

Create Table IMach_StockProductos
(
	CodigoEAN varchar(30) Not Null, 
	Existencia int Not Null Default 0,  
	ExistenciaIMach int Not Null Default 0,  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate()  
)
Go--#SQL   

----------------------------------------------------------------------- 