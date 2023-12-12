-------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Modulos_Relacionados' and xType = 'U' ) 
   Drop Table Net_Modulos_Relacionados 
Go--#SQL  

-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Modulos_Tipos' and xType = 'U' ) 
   Drop Table Net_Modulos_Tipos
Go--#SQL     

Create Table Net_Modulos_Tipos 
(
	IdTipoModulo varchar(3) Not Null, 
	Nombre varchar(100) Not Null, 

	Extension varchar(10) Not Null Unique,  
	Keyx int identity(1,1), 
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table Net_Modulos_Tipos Add Constraint PK_Net_Modulos_Tipos Primary Key ( IdTipoModulo ) 
Go--#SQL  

/* 
	Insert into Net_Modulos_Tipos values ( '001', 'Ejecutables', 'Exe', 'A', 0 ) 
	Insert into Net_Modulos_Tipos values ( '002', 'Dlls', 'Dll', 'A', 0 ) 	
	Insert into Net_Modulos_Tipos values ( '003', 'Reportes', 'Rpt', 'A', 0 ) 
	Insert into Net_Modulos_Tipos values ( '004', 'Reportes Excel', 'xls', 'A', 0 ) 
	Insert into Net_Modulos_Tipos values ( '005', 'Reportes Excel', 'xlsx', 'A', 0 ) 
	Insert into Net_Modulos_Tipos values ( '006', 'Scripts', 'Sql', 'A', 0 ) 
	Insert into Net_Modulos_Tipos values ( '007', 'Imagenes', 'Jpge', 'A', 0 ) 			
*/ 
	
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '001' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '001', 'Ejecutables', 'Exe', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Ejecutables', Extension = 'Exe', Status = 'A', Actualizado = 0 Where IdTipoModulo = '001'
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '002' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '002', 'Dlls', 'Dll', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Dlls', Extension = 'Dll', Status = 'A', Actualizado = 0 Where IdTipoModulo = '002'
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '003' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '003', 'Reportes', 'Rpt', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Reportes', Extension = 'Rpt', Status = 'A', Actualizado = 0 Where IdTipoModulo = '003'
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '004' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '004', 'Reportes Excel', 'xls', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Reportes Excel', Extension = 'xls', Status = 'A', Actualizado = 0 Where IdTipoModulo = '004'
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '005' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '005', 'Reportes Excel xlsx', 'xlsx', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Reportes Excel', Extension = 'xlsx', Status = 'A', Actualizado = 0 Where IdTipoModulo = '005'
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '006' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '006', 'Scripts', 'Sql', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Scripts', Extension = 'Sql', Status = 'A', Actualizado = 0 Where IdTipoModulo = '006'
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '007' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '007', 'Imagenes', 'Jpge', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Imagenes', Extension = 'Jpge', Status = 'A', Actualizado = 0 Where IdTipoModulo = '007'
If Not Exists ( Select * From Net_Modulos_Tipos Where IdTipoModulo = '008' )  Insert Into Net_Modulos_Tipos (  IdTipoModulo, Nombre, Extension, Status, Actualizado )  Values ( '008', 'Paquetes de Actualización', 'SII', 'A', 0 )    Else Update Net_Modulos_Tipos Set Nombre = 'Paquetes de Actualización', Extension = 'SII', Status = 'A', Actualizado = 0 Where IdTipoModulo = '008' 	
Go--#SQL  	
	
	
-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Modulos' and xType = 'U' ) 
   Drop Table Net_Modulos
Go--#SQL     

Create Table Net_Modulos 
(
	IdModulo varchar(6) Not Null, 
	Nombre varchar(100) Not Null Unique, 

	Extension varchar(10) Not Null Default '',  
	Version varchar(10) Not Null Default '', 
	VersionArchivo varchar(50) Not Null Default '', 	
	MD5 varchar(40) Not Null Default '', 	
	EsModuloBase int Not Null Default 0, 
	EmpacadoModulo Text,  	
	Tamaño numeric(14,2) Not Null Default 0, 
	FechaRegistro datetime Not Null Default getdate(), 	
	FechaActualizacion datetime Not Null Default getdate(), 		
	
	Keyx int identity(1,1), 
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table Net_Modulos Add Constraint PK_Net_Modulos Primary Key ( IdModulo ) 
Go--#SQL  

-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Modulos_Historico' and xType = 'U' ) 
   Drop Table Net_Modulos_Historico 
Go--#SQL     

Create Table Net_Modulos_Historico 
(
	IdModulo varchar(6) Not Null, 
	Nombre varchar(100) Not Null , 

	Extension varchar(10) Not Null Default '',  
	Version varchar(10) Not Null Default '', 
	VersionArchivo varchar(50) Not Null Default '', 		
	MD5 varchar(40) Not Null Default '', 	
	EmpacadoModulo Text,  	
	Tamaño numeric(14,2) Not Null Default 0, 
		
	FechaRegistro datetime Not Null Default getdate(), 	
	Keyx int identity(1,1), 	
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table Net_Modulos_Historico Add Constraint PK_Net_Modulos_Historico Primary Key ( IdModulo, FechaRegistro ) 
Go--#SQL 

-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Modulos_Relacionados' and xType = 'U' ) 
   Drop Table Net_Modulos_Relacionados 
Go--#SQL  

Create Table Net_Modulos_Relacionados 
(
	IdModuloRelacion varchar(6) Not Null, 
	IdModulo varchar(6) Not Null, 	
	IdModuloRelacionado varchar(6) Not Null, 
	
	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table Net_Modulos_Relacionados Add Constraint PK_Net_Modulos_Relacionados 
	Primary Key ( IdModuloRelacion ) 
Go--#SQL  

Alter Table Net_Modulos_Relacionados Add Constraint PK_Net_Modulos_Relacionados_Net_Modulos  
	Foreign Key ( IdModulo ) References Net_Modulos ( IdModulo ) 
Go--#SQL  

Alter Table Net_Modulos_Relacionados Add Constraint PK_Net_Modulos_Relacionados_Net_ModulosRelacionado   
	Foreign Key ( IdModuloRelacionado ) References Net_Modulos ( IdModulo ) 
Go--#SQL 

