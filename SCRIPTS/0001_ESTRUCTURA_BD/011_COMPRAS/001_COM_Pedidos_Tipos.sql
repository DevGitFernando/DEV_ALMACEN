If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_FAR_Pedidos_Tipos' and xType = 'U' ) 
Begin 
Create Table COM_FAR_Pedidos_Tipos 
( 
	IdTipoPedido varchar(2) Not Null, 
	TipoPedido varchar(4) Not Null Unique Clustered, 	
	Descripcion varchar(50) Not Null, 
	Keyx int identity, 
	Status varchar(1) default 'A', 
	Actualizado tinyint default 0 
) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_COM_FAR_Pedidos_Tipos' and xType = 'PK' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Tipos Add Constraint PK_COM_FAR_Pedidos_Tipos Primary Key ( IdTipoPedido ) 
End 
Go--#SQL  


Set NoCount On 
/* 
Insert Into COM_FAR_Pedidos_Tipos ( IdTipoPedido, TipoPedido, Descripcion ) Values ( '01', 'PEAP', 'Pedido Automatico de Productos' ) 
Insert Into COM_FAR_Pedidos_Tipos ( IdTipoPedido, TipoPedido, Descripcion ) Values ( '02', 'PEEP', 'Pedido Especial de Productos' ) 
Insert Into Com_Far_Pedidos_Tipos ( IdTipoPedido, TipoPedido, Descripcion ) Values ( '03', 'PPRO', 'Pedido de Productos'   )    
*/ 



If Not Exists ( Select * From COM_FAR_Pedidos_Tipos Where TipoPedido = 'PEAP' )  Insert Into COM_FAR_Pedidos_Tipos (  IdTipoPedido, TipoPedido, Descripcion, Status, Actualizado )  Values ( '01', 'PEAP', 'Pedido Automatico de Productos', 'A', 0 )    Else Update COM_FAR_Pedidos_Tipos Set IdTipoPedido = '01', Descripcion = 'Pedido Automatico de Productos', Status = 'A', Actualizado = 0 Where TipoPedido = 'PEAP'
If Not Exists ( Select * From COM_FAR_Pedidos_Tipos Where TipoPedido = 'PEEP' )  Insert Into COM_FAR_Pedidos_Tipos (  IdTipoPedido, TipoPedido, Descripcion, Status, Actualizado )  Values ( '02', 'PEEP', 'Pedido Especial de Productos', 'A', 0 )    Else Update COM_FAR_Pedidos_Tipos Set IdTipoPedido = '02', Descripcion = 'Pedido Especial de Productos', Status = 'A', Actualizado = 0 Where TipoPedido = 'PEEP'
If Not Exists ( Select * From COM_FAR_Pedidos_Tipos Where TipoPedido = 'PEAC' )  Insert Into COM_FAR_Pedidos_Tipos (  IdTipoPedido, TipoPedido, Descripcion, Status, Actualizado )  Values ( '03', 'PEAC', 'Pedido Automatico de Claves', 'A', 0 )    Else Update COM_FAR_Pedidos_Tipos Set IdTipoPedido = '03', Descripcion = 'Pedido Automatico de Claves', Status = 'A', Actualizado = 0 Where TipoPedido = 'PEAC'
If Not Exists ( Select * From COM_FAR_Pedidos_Tipos Where TipoPedido = 'PEEC' )  Insert Into COM_FAR_Pedidos_Tipos (  IdTipoPedido, TipoPedido, Descripcion, Status, Actualizado )  Values ( '04', 'PEEC', 'Pedido Especial de Claves', 'A', 0 )    Else Update COM_FAR_Pedidos_Tipos Set IdTipoPedido = '04', Descripcion = 'Pedido Especial de Claves', Status = 'A', Actualizado = 0 Where TipoPedido = 'PEEC' 



/* 
If Not Exists ( Select * From COM_FAR_Pedidos_Tipos Where TipoPedido = 'PEAP' )  Insert Into COM_FAR_Pedidos_Tipos Values ( '01', 'PEAP', 'Pedido Automatico de Productos', 'A', 0 )    Else Update COM_FAR_Pedidos_Tipos Set IdTipoPedido = '01', Descripcion = 'Pedido Automatico de Productos', Keyx = 1, Status = 'A', Actualizado = 0 Where TipoPedido = 'PEAP'
If Not Exists ( Select * From COM_FAR_Pedidos_Tipos Where TipoPedido = 'PEEP' )  Insert Into COM_FAR_Pedidos_Tipos Values ( '02', 'PEEP', 'Pedido Especial de Productos', 'A', 0 )    Else Update COM_FAR_Pedidos_Tipos Set IdTipoPedido = '02', Descripcion = 'Pedido Especial de Productos', Keyx = 2, Status = 'A', Actualizado = 0 Where TipoPedido = 'PEEP'
If Not Exists ( Select * From COM_FAR_Pedidos_Tipos Where TipoPedido = 'PPRO' )  Insert Into COM_FAR_Pedidos_Tipos Values ( '03', 'PPRO', 'Pedido de Productos', 'A', 0 )    Else Update COM_FAR_Pedidos_Tipos Set IdTipoPedido = '03', Descripcion = 'Pedido de Productos', Keyx = 3, Status = 'A', Actualizado = 0 Where TipoPedido = 'PPRO' 
*/ 
Go--#SQL 

------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_FAR_Pedidos_Tipos_Farmacias' and xType = 'U' ) 
Begin 
Create Table COM_FAR_Pedidos_Tipos_Farmacias 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 
	Consecutivo int Not Null Default 0, 
	Status varchar(1) default 'A', 
	Actualizado tinyint default 0 	
) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_COM_FAR_Pedidos_Tipos_Farmacias' and xType = 'PK' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Tipos_Farmacias Add Constraint 
		PK_COM_FAR_Pedidos_Tipos_Farmacias Primary Key ( IdEstado, IdFarmacia, IdTipoPedido ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_Tipos_Farmacias_COM_FAR_Pedidos_Tipos' and xType = 'F' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Tipos_Farmacias Add Constraint FK_COM_FAR_Pedidos_Tipos_Farmacias_COM_FAR_Pedidos_Tipos 
		Foreign Key ( IdTipoPedido ) References COM_FAR_Pedidos_Tipos ( IdTipoPedido ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_Tipos_Farmacias_CatFarmacias' and xType = 'F' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Tipos_Farmacias Add Constraint FK_COM_FAR_Pedidos_Tipos_Farmacias_CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
End 
Go--#SQL 
