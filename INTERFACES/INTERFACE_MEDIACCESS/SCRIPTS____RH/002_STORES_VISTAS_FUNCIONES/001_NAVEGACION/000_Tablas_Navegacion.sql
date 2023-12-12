Set NoCount On
Go--#SQL 

If Exists ( Select Name From Sysobjects Where Name = 'Net_Privilegios_Grupo' and xType = 'U' )
   Drop Table Net_Privilegios_Grupo 
Go--#SQL 

If Exists ( Select Name From Sysobjects Where Name = 'Net_Navegacion' and xType = 'U' ) 
   Drop Table Net_Navegacion 
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'Net_Grupos_Usuarios_Miembros' and xType = 'U' )
   Drop Table Net_Grupos_Usuarios_Miembros
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'Net_Grupos_De_Usuarios' and xType = 'U' )
   Drop Table Net_Grupos_De_Usuarios
Go--#SQL  

------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects Where Name = 'Net_Arboles' and xType = 'U' )
   Drop Table Net_Arboles
Go--#SQL  

Create Table Net_Arboles 
(
--	IdEstado varchar(2) Not Null, 
	Arbol varchar(4) Collate Latin1_General_CI_AI Not Null, 
	Nombre varchar(50) Collate Latin1_General_CI_AI Not Null, 
	Keyx int identity(1,1), 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Arboles Add Constraint PK_Net_Arboles Primary Key  ( Arbol )
Go--#SQL 	


------- Agregar los Arboles(Módulos) del Sistema
If Not Exists ( Select * From Net_Arboles Where Arbol = 'CFGN' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'CFGN', 'Configuración general' )    Else Update Net_Arboles Set Nombre = 'Configuración general' Where Arbol = 'CFGN'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'CFGS' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'CFGS', 'Configuración Sitio Central' )    Else Update Net_Arboles Set Nombre = 'Configuración Sitio Central' Where Arbol = 'CFGS'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'CFGC' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'CFGC', 'Configuración Sitio Cliente' )    Else Update Net_Arboles Set Nombre = 'Configuración Sitio Cliente' Where Arbol = 'CFGC'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'OCEN' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'OCEN', 'Oficina central' )    Else Update Net_Arboles Set Nombre = 'Oficina central' Where Arbol = 'OCEN'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'PFAR' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'PFAR', 'Punto de Venta Farmacia' )    Else Update Net_Arboles Set Nombre = 'Punto de Venta Farmacia' Where Arbol = 'PFAR'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'PFAR' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'COMP', 'Compras' )    Else Update Net_Arboles Set Nombre = 'Compras' Where Arbol = 'COMP'
----Go--#SQL  

------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects Where Name = 'Net_Usuarios' and xType = 'U' )
   Drop Table Net_Usuarios
Go--#SQL  

Create Table Net_Usuarios 
(
	IdPersonal varchar(8) Collate Latin1_General_CI_AI Not Null Default '', 
	Nombre varchar(100) Collate Latin1_General_CI_AI Not Null Default '' Unique, 
	LoginUser varchar(50) Collate Latin1_General_CI_AI Not Null Default '', 
	Password varchar(500) Collate Latin1_General_CI_AI Not Null Default '', 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table Net_Usuarios Add Constraint Pk_Net_Usuarios Primary Key ( IdPersonal )	
Go--#SQL 


------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects Where Name = 'Net_Grupos_De_Usuarios' and xType = 'U' )
   Drop Table Net_Grupos_De_Usuarios
Go--#SQL  

Create Table Net_Grupos_De_Usuarios 
(
	IdGrupo int Not Null,
	NombreGrupo varchar(50) Collate Latin1_General_CI_AI Not Null Default '', 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Grupos_De_Usuarios Add Constraint PK_Net_Grupos_De_Usuarios Primary Key ( IdGrupo )	
Go--#SQL 

------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects Where Name = 'Net_Grupos_Usuarios_Miembros' and xType = 'U' )
   Drop Table Net_Grupos_Usuarios_Miembros
Go--#SQL  

Create Table Net_Grupos_Usuarios_Miembros 
(
	IdGrupo int Not Null,
	IdPersonal varchar(8) Collate Latin1_General_CI_AI Not Null, 
	LoginUser varchar(50) Collate Latin1_General_CI_AI Not Null, 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Grupos_Usuarios_Miembros Add Constraint PK_Net_Grupos_Usuarios_Miembros Primary Key ( IdGrupo, IdPersonal ) 
Go--#SQL 

Alter Table Net_Grupos_Usuarios_Miembros Add Constraint FK_Net_Grupos_Usuarios_Miembros_Net_Usuarios  
	Foreign Key ( IdPersonal ) References Net_Usuarios ( IdPersonal ) 
Go--#SQL  

------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects Where Name = 'Net_Navegacion' and xType = 'U' )
   Drop Table Net_Navegacion 
Go--#SQL  

Create Table Net_Navegacion 
(
	Arbol varchar(4) Collate Latin1_General_CI_AI Not Null, 
	Rama int Not Null, 
	Nombre varchar(255) Collate Latin1_General_CI_AI Not Null, 
	Padre int Not Null, 
	FormaLoad varchar(100) Collate Latin1_General_CI_AI , 
	GrupoOpciones varchar(100) Collate Latin1_General_CI_AI , 
	IdOrden int Not Null default 0,
	RutaCompleta varchar(100) Collate Latin1_General_CI_AI , 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Navegacion Add Constraint PK_Net_Navegacion Primary Key ( Arbol, Rama ) 
Go--#SQL  

Alter Table Net_Navegacion Add Constraint FK_NetArboles_NetNavegacion 
	Foreign Key ( Arbol ) References Net_Arboles ( Arbol ) 
Go--#SQL  



-- sp_GeneraInserts Net_Navegacion, 1 
/* 
Set NoCount On 
Delete From Net_Navegacion 
Go 

If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 1 )  Insert Into Net_Navegacion Values ( 'CFGN', 1, 'Configuración', -1, '', '', 0, '1' )    Else Update Net_Navegacion Set Nombre = 'Configuración', Padre = -1, FormaLoad = '', GrupoOpciones = '', IdOrden = 0, RutaCompleta = '1' Where Arbol = 'CFGN' and Rama = 1
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 3 )  Insert Into Net_Navegacion Values ( 'CFGN', 3, 'Grupos de usuarios', 15, 'FrmGruposUsuarios', 'DllFarmaciaSoft', 2, '1|15|3' )    Else Update Net_Navegacion Set Nombre = 'Grupos de usuarios', Padre = 15, FormaLoad = 'FrmGruposUsuarios', GrupoOpciones = 'DllFarmaciaSoft', IdOrden = 2, RutaCompleta = '1|15|3' Where Arbol = 'CFGN' and Rama = 3
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 4 )  Insert Into Net_Navegacion Values ( 'CFGN', 4, 'Permisos por grupo', 15, 'FrmGruposPermisos', 'DllFarmaciaSoft', 3, '1|15|4' )    Else Update Net_Navegacion Set Nombre = 'Permisos por grupo', Padre = 15, FormaLoad = 'FrmGruposPermisos', GrupoOpciones = 'DllFarmaciaSoft', IdOrden = 3, RutaCompleta = '1|15|4' Where Arbol = 'CFGN' and Rama = 4
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 5 )  Insert Into Net_Navegacion Values ( 'CFGN', 5, 'Estructura menú de opciones', 15, 'FrmEstructuraNavegacion', 'SC_SolutionsSystem', 4, '1|15|5' )    Else Update Net_Navegacion Set Nombre = 'Estructura menú de opciones', Padre = 15, FormaLoad = 'FrmEstructuraNavegacion', GrupoOpciones = 'SC_SolutionsSystem', IdOrden = 4, RutaCompleta = '1|15|5' Where Arbol = 'CFGN' and Rama = 5
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 6 )  Insert Into Net_Navegacion Values ( 'CFGN', 6, 'Catálogos', 1, '', '', 1, '1|6' )    Else Update Net_Navegacion Set Nombre = 'Catálogos', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|6' Where Arbol = 'CFGN' and Rama = 6
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 7 )  Insert Into Net_Navegacion Values ( 'CFGN', 7, 'Personal', 6, 'FrmPersonal', 'DllFarmaciaSoft', 3, '1|6|7' )    Else Update Net_Navegacion Set Nombre = 'Personal', Padre = 6, FormaLoad = 'FrmPersonal', GrupoOpciones = 'DllFarmaciaSoft', IdOrden = 3, RutaCompleta = '1|6|7' Where Arbol = 'CFGN' and Rama = 7
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 8 )  Insert Into Net_Navegacion Values ( 'CFGN', 8, 'Geograficos', 6, '', '', 1, '1|6|8' )    Else Update Net_Navegacion Set Nombre = 'Geograficos', Padre = 6, FormaLoad = '', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|6|8' Where Arbol = 'CFGN' and Rama = 8
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 9 )  Insert Into Net_Navegacion Values ( 'CFGN', 9, 'Estados', 8, 'FrmEstados', 'OficinaCentral', 3, '1|6|8|9' )    Else Update Net_Navegacion Set Nombre = 'Estados', Padre = 8, FormaLoad = 'FrmEstados', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|6|8|9' Where Arbol = 'CFGN' and Rama = 9
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 10 )  Insert Into Net_Navegacion Values ( 'CFGN', 10, 'Municipios', 8, 'FrmMunicipios', 'OficinaCentral', 5, '1|6|8|10' )    Else Update Net_Navegacion Set Nombre = 'Municipios', Padre = 8, FormaLoad = 'FrmMunicipios', GrupoOpciones = 'OficinaCentral', IdOrden = 5, RutaCompleta = '1|6|8|10' Where Arbol = 'CFGN' and Rama = 10
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 11 )  Insert Into Net_Navegacion Values ( 'CFGN', 11, 'Colonias', 8, 'FrmColonias', 'OficinaCentral', 7, '1|6|8|11' )    Else Update Net_Navegacion Set Nombre = 'Colonias', Padre = 8, FormaLoad = 'FrmColonias', GrupoOpciones = 'OficinaCentral', IdOrden = 7, RutaCompleta = '1|6|8|11' Where Arbol = 'CFGN' and Rama = 11
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 12 )  Insert Into Net_Navegacion Values ( 'CFGN', 12, 'Jurisdicciones', 6, 'FrmJurisdicciones', 'OficinaCentral', 9, '1|6|12' )    Else Update Net_Navegacion Set Nombre = 'Jurisdicciones', Padre = 6, FormaLoad = 'FrmJurisdicciones', GrupoOpciones = 'OficinaCentral', IdOrden = 9, RutaCompleta = '1|6|12' Where Arbol = 'CFGN' and Rama = 12
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 14 )  Insert Into Net_Navegacion Values ( 'CFGN', 14, 'Farmacias', 6, 'FrmFarmacias', 'OficinaCentral', 3, '1|6|14' )    Else Update Net_Navegacion Set Nombre = 'Farmacias', Padre = 6, FormaLoad = 'FrmFarmacias', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|6|14' Where Arbol = 'CFGN' and Rama = 14
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 15 )  Insert Into Net_Navegacion Values ( 'CFGN', 15, 'Administracion', 1, '', '', 3, '1|15' )    Else Update Net_Navegacion Set Nombre = 'Administracion', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|15' Where Arbol = 'CFGN' and Rama = 15
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 16 )  Insert Into Net_Navegacion Values ( 'CFGN', 16, 'Regiones', 6, 'FrmRegiones', 'OficinaCentral', 5, '1|6|16' )    Else Update Net_Navegacion Set Nombre = 'Regiones', Padre = 6, FormaLoad = 'FrmRegiones', GrupoOpciones = 'OficinaCentral', IdOrden = 5, RutaCompleta = '1|6|16' Where Arbol = 'CFGN' and Rama = 16
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 17 )  Insert Into Net_Navegacion Values ( 'CFGN', 17, 'Sub-Regiones', 6, 'FrmSubRegiones', 'OficinaCentral', 7, '1|6|17' )    Else Update Net_Navegacion Set Nombre = 'Sub-Regiones', Padre = 6, FormaLoad = 'FrmSubRegiones', GrupoOpciones = 'OficinaCentral', IdOrden = 7, RutaCompleta = '1|6|17' Where Arbol = 'CFGN' and Rama = 17
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 18 )  Insert Into Net_Navegacion Values ( 'CFGN', 18, 'Configuración', 1, '', '', 5, '1|18' )    Else Update Net_Navegacion Set Nombre = 'Configuración', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 5, RutaCompleta = '1|18' Where Arbol = 'CFGN' and Rama = 18
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 19 )  Insert Into Net_Navegacion Values ( 'CFGN', 19, 'Parametros Punto de Venta', 18, 'FrmParametrosPtoCliente', 'Configuracion', 5, '1|18|19' )    Else Update Net_Navegacion Set Nombre = 'Parametros Punto de Venta', Padre = 18, FormaLoad = 'FrmParametrosPtoCliente', GrupoOpciones = 'Configuracion', IdOrden = 5, RutaCompleta = '1|18|19' Where Arbol = 'CFGN' and Rama = 19
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 20 )  Insert Into Net_Navegacion Values ( 'CFGN', 20, 'Parametros Oficina Central', 18, 'FrmParametrosOC', 'Configuracion', 7, '1|18|20' )    Else Update Net_Navegacion Set Nombre = 'Parametros Oficina Central', Padre = 18, FormaLoad = 'FrmParametrosOC', GrupoOpciones = 'Configuracion', IdOrden = 7, RutaCompleta = '1|18|20' Where Arbol = 'CFGN' and Rama = 20
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 21 )  Insert Into Net_Navegacion Values ( 'CFGN', 21, 'Empresas', 6, 'FrmEmpresas', 'OficinaCentral', 1, '1|6|21' )    Else Update Net_Navegacion Set Nombre = 'Empresas', Padre = 6, FormaLoad = 'FrmEmpresas', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|6|21' Where Arbol = 'CFGN' and Rama = 21
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 22 )  Insert Into Net_Navegacion Values ( 'CFGN', 22, 'Relación de Farmacias por Empresa', 18, 'FrmEmpresasFarmacias', 'OficinaCentral', 3, '1|18|22' )    Else Update Net_Navegacion Set Nombre = 'Relación de Farmacias por Empresa', Padre = 18, FormaLoad = 'FrmEmpresasFarmacias', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|18|22' Where Arbol = 'CFGN' and Rama = 22
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGN' and Rama = 23 )  Insert Into Net_Navegacion Values ( 'CFGN', 23, 'Relación de Empresas-Estados', 18, 'FrmEmpresaEstado', 'OficinaCentral', 1, '1|18|23' )    Else Update Net_Navegacion Set Nombre = 'Relación de Empresas-Estados', Padre = 18, FormaLoad = 'FrmEmpresaEstado', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|18|23' Where Arbol = 'CFGN' and Rama = 23

*/ 

------------------- 
--- Esta tabla 
If Exists ( Select Name From Sysobjects Where Name = 'Net_Privilegios_Grupo' and xType = 'U' )
   Drop Table Net_Privilegios_Grupo 
Go--#SQL  

Create Table Net_Privilegios_Grupo 
(
	IdGrupo int Not Null, 
	Arbol varchar(4) Collate Latin1_General_CI_AI Not Null, 
	Rama int Not Null, 
	Ruta varchar(500) Collate Latin1_General_CI_AI , 
	TipoRama varchar(1) Collate Latin1_General_CI_AI ,
	RutaCompleta varchar(100) Collate Latin1_General_CI_AI , 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Privilegios_Grupo Add Constraint PK_Net_Privilegios_Grupo Primary Key ( IdGrupo, Arbol, Rama )	
Go--#SQL 

Alter Table Net_Privilegios_Grupo Add Constraint FK_NetGruposDeUsuarios_Net_Privilegios_Grupo 
	Foreign Key ( IdGrupo ) References Net_Grupos_De_Usuarios ( IdGrupo ) 
Go--#SQL  

-- Alter Table Net_Privilegios_Grupo Add Constraint FK_Net_Privilegios_Grupo_Net_Navegacion 
--	Foreign Key ( Arbol, Rama ) References Net_Navegacion ( Arbol, Rama ) 
--	ON UPDATE  NO ACTION 
--	ON DELETE  CASCADE 
Go--#SQL 
