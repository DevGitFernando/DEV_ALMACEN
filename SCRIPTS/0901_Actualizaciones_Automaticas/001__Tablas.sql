If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFG_Versiones_Archivos' and xType = 'U' ) 
   Drop Table Net_CFG_Versiones_Archivos
Go--#zzSQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFG_Versiones_Cambios' and xType = 'U' ) 
   Drop Table Net_CFG_Versiones_Cambios
Go--#zzSQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Versiones' and xType = 'U' ) 
   Drop Table Net_CFGC_Versiones
Go--#zzSQL  


----------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFG_Versiones' and xType = 'U' ) 
   Drop Table Net_CFG_Versiones
Go--#zzSQL     

Create Table Net_CFG_Versiones 
(
	IdVersion varchar(4) Not Null, 
	NombreVersion varchar(50) Not Null Unique, 
	Version varchar(10) Not Null Unique, 

	MD5 varchar(40) Not Null Default '', 
	ContenidoVersion Text,  

	FechaRegistro datetime Not Null Default getdate(), 	
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#zzSQL  

Alter Table Net_CFG_Versiones Add Constraint PK_Net_CFG_Versiones Primary Key ( IdVersion ) 
Go--#zzSQL  

---------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Versiones' and xType = 'U' ) 
   Drop Table Net_CFGC_Versiones
Go--#zzSQL     

Create Table Net_CFGC_Versiones 
(
	IdVersion varchar(4) Not Null, 
	NombreVersion varchar(50) Not Null Unique, 
	Version varchar(10) Not Null Unique, 
	FechaRegistro datetime Not Null Default getdate(), 	
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#zzSQL  

Alter Table Net_CFGC_Versiones Add Constraint PK_Net_CFGC_Versiones Primary Key ( IdVersion ) 
Go--#zzSQL  

Alter Table Net_CFGC_Versiones Add Constraint FK_Net_CFGC_Versiones_Net_CFG_Versiones 
	Foreign Key ( IdVersion ) References Net_CFG_Versiones ( IdVersion )  
Go--#zzSQL  

---------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFG_Versiones_Archivos' and xType = 'U' ) 
   Drop Table Net_CFG_Versiones_Archivos
Go--#zzSQL 

Create Table Net_CFG_Versiones_Archivos 
(
	IdVersion varchar(4) Not Null, 
	IdArchivo varchar(3) Not Null, 
	NombreArchivo varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
)
Go--#zzSQL  

Alter Table Net_CFG_Versiones_Archivos Add Constraint PK_Net_CFG_Versiones_Archivos Primary Key ( IdVersion, IdArchivo ) 
Go--#zzSQL  

Alter Table Net_CFG_Versiones_Archivos Add Constraint FK_Net_CFG_Versiones_Archivos_Net_CFG_Versiones 
	Foreign Key ( IdVersion ) References Net_CFG_Versiones ( IdVersion )  
Go--#zzSQL  


---------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFG_Versiones_Cambios' and xType = 'U' ) 
   Drop Table Net_CFG_Versiones_Cambios
Go--#zzSQL     

Create Table Net_CFG_Versiones_Cambios 
(
	IdVersion varchar(4) Not Null, 
	IdCambio varchar(3) Not Null, 
	Comentario varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#zzSQL  	

Alter Table Net_CFG_Versiones_Cambios Add Constraint PK_Net_CFG_Versiones_Cambios Primary Key ( IdVersion, IdCambio ) 
Go--#zzSQL  

Alter Table Net_CFG_Versiones_Cambios Add Constraint FK_Net_CFG_Versiones_Cambios_Net_CFG_Versiones 
	Foreign Key ( IdVersion ) References Net_CFG_Versiones ( IdVersion )  
Go--#zzSQL  

