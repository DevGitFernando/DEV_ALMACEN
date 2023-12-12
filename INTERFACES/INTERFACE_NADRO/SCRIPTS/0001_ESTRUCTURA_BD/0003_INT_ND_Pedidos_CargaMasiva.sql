----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Pedidos_CargaMasiva' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_CargaMasiva 
Go--#SQL 

Create Table INT_ND_Pedidos_CargaMasiva 
(
	CodigoCliente varchar(20)  Not Null,
	ReferenciaPedido varchar(50)  Not Null,
	CodigoProducto varchar(50)  Not Null,	
	CodigoEAN varchar(20)  Not Null,
	Cantidad int not null,
	Precio numeric(14, 4) not null, 
	CodigoEAN_Existe int Not Null Default 0     
) 	
Go--#SQL 

 