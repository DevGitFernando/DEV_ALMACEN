Set NoCount On
Go--#SQL 

If Exists ( Select Name From Sysobjects Where Name = 'Net_CTE_Privilegios_Grupo' and xType = 'U' )
   Drop Table Net_CTE_Privilegios_Grupo 
Go--#SQL 

If Exists ( Select Name From Sysobjects Where Name = 'Net_CTE_Grupos_Usuarios_Miembros' and xType = 'U' )
   Drop Table Net_CTE_Grupos_Usuarios_Miembros
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'Net_CTE_Grupos_De_Usuarios' and xType = 'U' )
   Drop Table Net_CTE_Grupos_De_Usuarios
Go--#SQL  

----------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'Net_CTE_Grupos_De_Usuarios' and xType = 'U' )
   Drop Table Net_CTE_Grupos_De_Usuarios
Go--#SQL  

Create Table Net_CTE_Grupos_De_Usuarios 
(
	IdEstado varchar(2) Collate Latin1_General_CI_AI Not Null, 
	IdFarmacia varchar(4) Collate Latin1_General_CI_AI Not Null, 
	IdGrupo int,
	NombreGrupo varchar(50) Collate Latin1_General_CI_AI, 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0, 
	Constraint PK_Net_CTE_Grupos_De_Usuarios Primary Key Clustered  ( IdEstado, IdFarmacia, IdGrupo )
) On [Primary]
Go--#SQL 

Alter Table Net_CTE_Grupos_De_Usuarios Add Constraint FK_CatFarmacias_Net_CTE_Grupos_De_Usuarios 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

----------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'Net_CTE_Grupos_Usuarios_Miembros' and xType = 'U' )
   Drop Table Net_CTE_Grupos_Usuarios_Miembros
Go--#SQL  

Create Table Net_CTE_Grupos_Usuarios_Miembros 
(
	IdEstado varchar(2) Collate Latin1_General_CI_AI Not Null, 
	IdFarmacia varchar(4) Collate Latin1_General_CI_AI Not Null, 
	IdGrupo int,
	IdUsuario varchar(4) Collate Latin1_General_CI_AI Not Null, 
	LoginUser varchar(50) Collate Latin1_General_CI_AI Not Null, 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0, 
	Constraint PK_Net_CTE_Grupos_Usuarios_Miembros Primary Key Clustered ( IdEstado, IdFarmacia, IdGrupo, IdUsuario )  
) On [Primary]
Go--#SQL 

Alter Table Net_CTE_Grupos_Usuarios_Miembros Add Constraint FK_Net_CTE_Grupos_Usuarios_Miembros_Net_CTE_Grupos_De_Usuarios  
	Foreign Key ( IdEstado, IdFarmacia, IdGrupo ) References Net_CTE_Grupos_De_Usuarios ( IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  

Alter Table Net_CTE_Grupos_Usuarios_Miembros Add Constraint FK_Net_CTE_Grupos_Usuarios_Miembros_Net_Usuarios 
	Foreign Key ( IdEstado, IdFarmacia, IdUsuario ) References Net_Regional_Usuarios ( IdEstado, IdFarmacia, IdUsuario ) 
Go--#SQL  


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'Net_CTE_Privilegios_Grupo' and xType = 'U' )
   Drop Table Net_CTE_Privilegios_Grupo 
Go--#SQL  

Create Table Net_CTE_Privilegios_Grupo 
(
	IdEstado varchar(2) Collate Latin1_General_CI_AI Not Null, 
	IdFarmacia varchar(4) Collate Latin1_General_CI_AI Not Null, 
	IdGrupo int Not Null, 
	Arbol varchar(4) Collate Latin1_General_CI_AI Not Null, 
	Rama int Not Null, 
	Ruta varchar(500) Collate Latin1_General_CI_AI , 
	TipoRama varchar(1) Collate Latin1_General_CI_AI ,
	RutaCompleta varchar(100) Collate Latin1_General_CI_AI , 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0, 	
	Constraint PK_Net_CTE_Privilegios_Grupo Primary Key Clustered  ( IdEstado, IdFarmacia, IdGrupo, Arbol, Rama )	
) On [Primary]
Go--#SQL 

Alter Table Net_CTE_Privilegios_Grupo Add Constraint FK_Net_CTE_Privilegios_Grupo_Net_CTE_Grupos_De_Usuarios
	Foreign Key ( IdEstado, IdFarmacia, IdGrupo ) References Net_CTE_Grupos_De_Usuarios ( IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  
----------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'Net_CTE_Navegacion_Mascaras' and xType = 'U' )
   Drop Table Net_CTE_Navegacion_Mascaras 
Go--#SQL 

Create Table Net_CTE_Navegacion_Mascaras 
(
	IdEstado Varchar(2) Not Null,
	Arbol varchar(4) Not Null, 
	Rama int Not Null, 
	Nombre varchar(255) Not Null,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 	
)
Go--#SQL

Alter Table Net_CTE_Navegacion_Mascaras Add Constraint PK_Net_CTE_Navegacion_Mascaras 
	Primary Key ( Arbol, Rama ) 
Go--#SQL

Alter Table Net_CTE_Navegacion_Mascaras Add Constraint FK_NetNavegacion_Net_CTE_Navegacion_Mascaras
	Foreign Key ( Arbol, Rama ) References Net_Navegacion ( Arbol, Rama ) 
Go--#SQL 
