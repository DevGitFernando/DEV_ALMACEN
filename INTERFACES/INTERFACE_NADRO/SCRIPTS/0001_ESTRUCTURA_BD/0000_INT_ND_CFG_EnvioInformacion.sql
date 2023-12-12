Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_EnvioInformacion' and xType = 'U' ) 
   Drop Table INT_ND_CFG_EnvioInformacion 
Go--#SQL   

Create Table INT_ND_CFG_EnvioInformacion 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table INT_ND_CFG_EnvioInformacion Add Constraint PK_INT_ND_CFG_EnvioInformacion Primary Key ( NombreTabla, Status ) 
Go--#SQL   

Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_Clientes', 'A' ) 
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_Existencias', 'A' ) 
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_Claves', 'A' )  
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_Productos', 'A' ) 

Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_CB_CuadrosBasicos', 'A' )   
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_CB_Anexos_Causes', 'A' )  
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_CB_Anexos', 'A' )  
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_CB_Anexos_Miembros', 'A' )  

Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_ClavesSSA', 'A' ) 
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_CodigosEAN', 'A' ) 
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_ClavesSSA_Excluir', 'A' )  
Insert Into INT_ND_CFG_EnvioInformacion ( NombreTabla, Status ) values ( 'INT_ND_CFG_ClavesSSA_ContenidoPaquete', 'A' )  


Update INT_ND_CFG_EnvioInformacion Set IdOrden = IdEnvio + 100 
Go--#SQL  

