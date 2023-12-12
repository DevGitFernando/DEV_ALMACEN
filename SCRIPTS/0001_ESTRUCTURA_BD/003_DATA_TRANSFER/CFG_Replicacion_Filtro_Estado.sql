-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Replicacion_Filtro_Estado' and xType = 'U' )
   Drop Table CFG_Replicacion_Filtro_Estado   
Go--#SQL   

Create Table CFG_Replicacion_Filtro_Estado 
(
    IdRegistro int identity(1,1),  
    NombreTabla varchar (200) Not Null Default '', 
    IdOrden int Not Null Default 0  
)
Go--#SQL   




