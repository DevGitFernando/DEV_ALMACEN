If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CostosPromediosPrecios' and xType = 'U' )
   Drop Table CFG_CostosPromediosPrecios 
Go--#SQL  

Create Table CFG_CostosPromediosPrecios 
(
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	CostoPromedio numeric(14,4) Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table CFG_CostosPromediosPrecios Add Constraint PK_CFG_CostosPromediosPrecios Primary Key ( IdProducto, CodigoEAN ) 
Go--#SQL  

Alter Table CFG_CostosPromediosPrecios Add Constraint FK_CFG_CostosPromediosPrecios_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 	

--- Insert Into CFG_CostosPromediosPrecios values ( '00000001', 10.50, 'A', 0 ) 

--	select * from CFG_CostosPromediosPrecios 
	