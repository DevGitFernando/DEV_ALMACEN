If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_RP_EnvioPedidosCedis' and xType = 'U' ) 
   Drop Table INT_ND_RP_EnvioPedidosCedis  
Go--#SQL   

Create Table INT_ND_RP_EnvioPedidosCedis 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table INT_ND_RP_EnvioPedidosCedis Add Constraint PK_INT_ND_RP_EnvioPedidosCedis Primary Key ( NombreTabla, Status ) 
Go--#SQL   

Insert Into INT_ND_RP_EnvioPedidosCedis ( NombreTabla, Status ) values ( 'INT_ND_Pedidos_Enviados', 'A' ) 
Insert Into INT_ND_RP_EnvioPedidosCedis ( NombreTabla, Status ) values ( 'INT_ND_Pedidos_Enviados_Det', 'A' ) 


Update INT_ND_RP_EnvioPedidosCedis Set IdOrden = IdEnvio + 100 

Go--#SQL  

