If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IGPI_ExistenciaK' and xType = 'U' ) 
   Drop Table IGPI_ExistenciaK
Go--#SQL   

Create Table IGPI_ExistenciaK
(
	CodigoEAN varchar(30) Not Null, 
	Cantidad int Not Null Default 0,  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL   

-------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IGPI_ExistenciaK_Historico' and xType = 'U' ) 
   Drop Table IGPI_ExistenciaK_Historico
Go--#SQL   

Create Table IGPI_ExistenciaK_Historico
(
	Keyx int identity(1,1) Not Null, 
	FechaReg datetime Not Null Default getdate(), 
	CodigoEAN varchar(30) Not Null, 
	Cantidad int Not Null Default 0,  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  
