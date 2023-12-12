---------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IATP2_StockProductos' and xType = 'U' ) 
   Drop Table IATP2_StockProductos 
Go--#SQL   

Create Table IATP2_StockProductos
(
	CodigoEAN varchar(30) Not Null, 
	Existencia int Not Null Default 0,  
	ExistenciaIATP2 int Not Null Default 0,  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate()  
)
Go--#SQL   

 