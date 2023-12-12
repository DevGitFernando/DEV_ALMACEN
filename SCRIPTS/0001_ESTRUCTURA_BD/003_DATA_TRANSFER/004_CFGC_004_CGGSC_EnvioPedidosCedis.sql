If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_EnvioPedidosCedis' and xType = 'U' ) 
   Drop Table CFGSC_EnvioPedidosCedis  
Go--#SQL   

Create Table CFGSC_EnvioPedidosCedis 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGSC_EnvioPedidosCedis Add Constraint PK_CFGSC_EnvioPedidosCedis Primary Key ( NombreTabla, Status ) 
Go--#SQL   

 Insert Into CFGSC_EnvioPedidosCedis ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Enc', 'A' ) 
 Insert Into CFGSC_EnvioPedidosCedis ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det', 'A' ) 
  
 
 Update CFGSC_EnvioPedidosCedis Set IdOrden = IdEnvio + 100 
Go--#SQL  