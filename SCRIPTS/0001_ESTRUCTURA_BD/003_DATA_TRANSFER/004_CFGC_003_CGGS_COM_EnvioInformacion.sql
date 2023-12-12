Set NoCount On
Go--#SQL   


---------------------------------------------- 
/* 
	Informacion enviada a Oficina Central
*/ 
---------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_COM_EnvioDetalles' and xType = 'U' ) 
   Drop Table CFGC_COM_EnvioDetalles  
Go--#SQL   

Create Table CFGC_COM_EnvioDetalles 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGC_COM_EnvioDetalles Add Constraint PK_CFGC_COM_EnvioDetalles Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGC_COM_EnvioDetalles ( NombreTabla ) values ( 'COM_FAR_Pedidos' ) 
 Insert Into CFGC_COM_EnvioDetalles ( NombreTabla ) values ( 'COM_FAR_Pedidos_Productos' ) 


Update CFGC_COM_EnvioDetalles Set IdOrden = IdEnvio + 100 
Go--#SQL  

--    Select * From CFGC_COM_EnvioDetalles (nolock) 
