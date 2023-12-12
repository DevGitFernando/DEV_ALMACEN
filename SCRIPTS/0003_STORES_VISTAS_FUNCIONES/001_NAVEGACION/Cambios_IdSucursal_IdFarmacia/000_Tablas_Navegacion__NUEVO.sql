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

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
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

Alter Table Net_Arboles	Add Constraint PK_Net_Arboles Primary Key ( Arbol )
Go--#SQL 	


------- Agregar los Arboles(Módulos) del Sistema
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'CFGN' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'CFGN', 'Configuración general' )    Else Update Net_Arboles Set Nombre = 'Configuración general' Where Arbol = 'CFGN'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'CFGS' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'CFGS', 'Configuración Sitio Central' )    Else Update Net_Arboles Set Nombre = 'Configuración Sitio Central' Where Arbol = 'CFGS'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'CFGC' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'CFGC', 'Configuración Sitio Cliente' )    Else Update Net_Arboles Set Nombre = 'Configuración Sitio Cliente' Where Arbol = 'CFGC'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'OCEN' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'OCEN', 'Oficina central' )    Else Update Net_Arboles Set Nombre = 'Oficina central' Where Arbol = 'OCEN'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'PFAR' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'PFAR', 'Punto de Venta Farmacia' )    Else Update Net_Arboles Set Nombre = 'Punto de Venta Farmacia' Where Arbol = 'PFAR'
----If Not Exists ( Select * From Net_Arboles Where Arbol = 'PFAR' )  Insert Into Net_Arboles ( Arbol, Nombre ) Values ( 'COMP', 'Compras' )    Else Update Net_Arboles Set Nombre = 'Compras' Where Arbol = 'COMP'
----Go--#SQL  

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects Where Name = 'Net_Usuarios' and xType = 'U' )
   Drop Table Net_Usuarios
Go--#SQL  

Create Table Net_Usuarios 
(
	IdEstado varchar(2) Collate Latin1_General_CI_AI Not Null, 
	IdFarmacia varchar(4) Collate Latin1_General_CI_AI Not Null, 
	IdPersonal varchar(4) Collate Latin1_General_CI_AI Not Null, 
	LoginUser varchar(50) Collate Latin1_General_CI_AI Not Null,
	Password varchar(500) Collate Latin1_General_CI_AI Not Null, 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table Net_Usuarios Add Constraint Pk_Net_Usuarios 
    Primary Key ( IdEstado, IdFarmacia, IdPersonal )	
Go--#SQL 
	
Alter Table Net_Usuarios Add Constraint FK_CatPersonal_Net_Usuarios 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects Where Name = 'Net_Grupos_De_Usuarios' and xType = 'U' )
   Drop Table Net_Grupos_De_Usuarios
Go--#SQL  

Create Table Net_Grupos_De_Usuarios 
(
	IdEstado varchar(2) Collate Latin1_General_CI_AI Not Null, 
	IdFarmacia varchar(4) Collate Latin1_General_CI_AI Not Null, 
	IdGrupo int,
	NombreGrupo varchar(50) Collate Latin1_General_CI_AI, 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Grupos_De_Usuarios Add Constraint PK_Net_Grupos_De_Usuarios 
    Primary Key ( IdEstado, IdFarmacia, IdGrupo )
Go--#SQL 

Alter Table Net_Grupos_De_Usuarios Add Constraint FK_CatFarmacias_NetGruposDeUsuarios 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects Where Name = 'Net_Grupos_Usuarios_Miembros' and xType = 'U' )
   Drop Table Net_Grupos_Usuarios_Miembros
Go--#SQL  

Create Table Net_Grupos_Usuarios_Miembros 
(
	IdEstado varchar(2) Collate Latin1_General_CI_AI Not Null, 
	IdFarmacia varchar(4) Collate Latin1_General_CI_AI Not Null, 
	IdGrupo int,
	IdPersonal varchar(4) Collate Latin1_General_CI_AI Not Null, 
	LoginUser varchar(50) Collate Latin1_General_CI_AI Not Null, 
	Status varchar(1) Collate Latin1_General_CI_AI Not Null Default 'A', 
	FechaUpdate datetime Not Null Default getdate(), 	
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Grupos_Usuarios_Miembros Add Constraint PK_Net_Grupos_Usuarios_Miembros 
    Primary Key ( IdEstado, IdFarmacia, IdGrupo, IdPersonal )  
Go--#SQL 	

Alter Table Net_Grupos_Usuarios_Miembros Add Constraint FK_NetGruposDeUsuarios_NetGruposUsuariosMiembros 
	Foreign Key ( IdEstado, IdFarmacia, IdGrupo ) References Net_Grupos_De_Usuarios ( IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  

Alter Table Net_Grupos_Usuarios_Miembros Add Constraint FK_Net_Usuarios_NetGruposUsuariosMiembros 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References Net_Usuarios ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
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

Alter Table Net_Navegacion Add Constraint PK_Net_Navegacion 
    Primary Key ( Arbol, Rama )	
Go--#SQL 	

Alter Table Net_Navegacion Add Constraint FK_NetArboles_NetNavegacion 
	Foreign Key ( Arbol ) References Net_Arboles ( Arbol ) 
Go--#SQL  

-- sp_GeneraInserts Net_Navegacion, 1 
/* 
Set NoCount On 
Delete From Net_Navegacion 
Go 
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGC' and Rama = 1 )  Insert Into Net_Navegacion Values ( 'CFGC', 1, 'Configuración Sitio Cliente', -1, '', '', 0, '1' )    Else Update Net_Navegacion Set Nombre = 'Configuración Sitio Cliente', Padre = -1, FormaLoad = '', GrupoOpciones = '', IdOrden = 0, RutaCompleta = '1' Where Arbol = 'CFGC' and Rama = 1
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGC' and Rama = 2 )  Insert Into Net_Navegacion Values ( 'CFGC', 2, 'Configurar conexión con Sitio Central', 1, 'FrmConfigurarConexionOficinaCentral', 'DllTransferenciaSoft', 5, '1|2' )    Else Update Net_Navegacion Set Nombre = 'Configurar conexión con Sitio Central', Padre = 1, FormaLoad = 'FrmConfigurarConexionOficinaCentral', GrupoOpciones = 'DllTransferenciaSoft', IdOrden = 5, RutaCompleta = '1|2' Where Arbol = 'CFGC' and Rama = 2
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGC' and Rama = 3 )  Insert Into Net_Navegacion Values ( 'CFGC', 3, 'Configurar integración', 1, 'FrmConfigIntegrarInformacion', 'DllTransferenciaSoft', 1, '1|3' )    Else Update Net_Navegacion Set Nombre = 'Configurar integración', Padre = 1, FormaLoad = 'FrmConfigIntegrarInformacion', GrupoOpciones = 'DllTransferenciaSoft', IdOrden = 1, RutaCompleta = '1|3' Where Arbol = 'CFGC' and Rama = 3
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGC' and Rama = 4 )  Insert Into Net_Navegacion Values ( 'CFGC', 4, 'Configurar obtención de información', 1, 'FrmConfigObtenerInformacion', 'DllTransferenciaSoft', 3, '1|4' )    Else Update Net_Navegacion Set Nombre = 'Configurar obtención de información', Padre = 1, FormaLoad = 'FrmConfigObtenerInformacion', GrupoOpciones = 'DllTransferenciaSoft', IdOrden = 3, RutaCompleta = '1|4' Where Arbol = 'CFGC' and Rama = 4
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
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGS' and Rama = 1 )  Insert Into Net_Navegacion Values ( 'CFGS', 1, 'Configuración Sitio Central', -1, '', '', 0, '1' )    Else Update Net_Navegacion Set Nombre = 'Configuración Sitio Central', Padre = -1, FormaLoad = '', GrupoOpciones = '', IdOrden = 0, RutaCompleta = '1' Where Arbol = 'CFGS' and Rama = 1
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGS' and Rama = 2 )  Insert Into Net_Navegacion Values ( 'CFGS', 2, 'Configurar integración', 1, 'FrmConfigIntegrarInformacion', 'DllTransferenciaSoft', 1, '1|2' )    Else Update Net_Navegacion Set Nombre = 'Configurar integración', Padre = 1, FormaLoad = 'FrmConfigIntegrarInformacion', GrupoOpciones = 'DllTransferenciaSoft', IdOrden = 1, RutaCompleta = '1|2' Where Arbol = 'CFGS' and Rama = 2
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGS' and Rama = 3 )  Insert Into Net_Navegacion Values ( 'CFGS', 3, 'Configurar obtención de información', 1, 'FrmConfigObtenerInformacion', 'DllTransferenciaSoft', 3, '1|3' )    Else Update Net_Navegacion Set Nombre = 'Configurar obtención de información', Padre = 1, FormaLoad = 'FrmConfigObtenerInformacion', GrupoOpciones = 'DllTransferenciaSoft', IdOrden = 3, RutaCompleta = '1|3' Where Arbol = 'CFGS' and Rama = 3
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'CFGS' and Rama = 4 )  Insert Into Net_Navegacion Values ( 'CFGS', 4, 'Configurar conexiones con Puntos de venta', 1, 'FrmConfigurarConexionClientes', 'DllTransferenciaSoft', 5, '1|4' )    Else Update Net_Navegacion Set Nombre = 'Configurar conexiones con Puntos de venta', Padre = 1, FormaLoad = 'FrmConfigurarConexionClientes', GrupoOpciones = 'DllTransferenciaSoft', IdOrden = 5, RutaCompleta = '1|4' Where Arbol = 'CFGS' and Rama = 4
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 1 )  Insert Into Net_Navegacion Values ( 'OCEN', 1, 'Oficina central', -1, '', '', 0, '1' )    Else Update Net_Navegacion Set Nombre = 'Oficina central', Padre = -1, FormaLoad = '', GrupoOpciones = '', IdOrden = 0, RutaCompleta = '1' Where Arbol = 'OCEN' and Rama = 1
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 2 )  Insert Into Net_Navegacion Values ( 'OCEN', 2, 'Catalogos generales', 1, '', '', 3, '1|2' )    Else Update Net_Navegacion Set Nombre = 'Catalogos generales', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|2' Where Arbol = 'OCEN' and Rama = 2
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 4 )  Insert Into Net_Navegacion Values ( 'OCEN', 4, 'Inventarios', 1, '', '', 1, '1|4' )    Else Update Net_Navegacion Set Nombre = 'Inventarios', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|4' Where Arbol = 'OCEN' and Rama = 4
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 5 )  Insert Into Net_Navegacion Values ( 'OCEN', 5, 'Tipos de productos', 40, 'FrmTiposDeProductos', 'OficinaCentral', 6, '1|2|40|5' )    Else Update Net_Navegacion Set Nombre = 'Tipos de productos', Padre = 40, FormaLoad = 'FrmTiposDeProductos', GrupoOpciones = 'OficinaCentral', IdOrden = 6, RutaCompleta = '1|2|40|5' Where Arbol = 'OCEN' and Rama = 5
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 6 )  Insert Into Net_Navegacion Values ( 'OCEN', 6, 'Presentaciones', 40, 'FrmPresentaciones', 'OficinaCentral', 7, '1|2|40|6' )    Else Update Net_Navegacion Set Nombre = 'Presentaciones', Padre = 40, FormaLoad = 'FrmPresentaciones', GrupoOpciones = 'OficinaCentral', IdOrden = 7, RutaCompleta = '1|2|40|6' Where Arbol = 'OCEN' and Rama = 6
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 7 )  Insert Into Net_Navegacion Values ( 'OCEN', 7, 'Familias', 40, 'FrmFamilias', 'OficinaCentral', 8, '1|2|40|7' )    Else Update Net_Navegacion Set Nombre = 'Familias', Padre = 40, FormaLoad = 'FrmFamilias', GrupoOpciones = 'OficinaCentral', IdOrden = 8, RutaCompleta = '1|2|40|7' Where Arbol = 'OCEN' and Rama = 7
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 8 )  Insert Into Net_Navegacion Values ( 'OCEN', 8, 'Sub-Familias', 40, 'FrmSubFamilias', 'OficinaCentral', 9, '1|2|40|8' )    Else Update Net_Navegacion Set Nombre = 'Sub-Familias', Padre = 40, FormaLoad = 'FrmSubFamilias', GrupoOpciones = 'OficinaCentral', IdOrden = 9, RutaCompleta = '1|2|40|8' Where Arbol = 'OCEN' and Rama = 8
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 9 )  Insert Into Net_Navegacion Values ( 'OCEN', 9, 'Laboratorios', 40, 'FrmLaboratorios', 'OficinaCentral', 10, '1|2|40|9' )    Else Update Net_Navegacion Set Nombre = 'Laboratorios', Padre = 40, FormaLoad = 'FrmLaboratorios', GrupoOpciones = 'OficinaCentral', IdOrden = 10, RutaCompleta = '1|2|40|9' Where Arbol = 'OCEN' and Rama = 9
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 10 )  Insert Into Net_Navegacion Values ( 'OCEN', 10, 'Clasificaciones SSA', 40, 'FrmClasificacionesSSA', 'OficinaCentral', 4, '1|2|40|10' )    Else Update Net_Navegacion Set Nombre = 'Clasificaciones SSA', Padre = 40, FormaLoad = 'FrmClasificacionesSSA', GrupoOpciones = 'OficinaCentral', IdOrden = 4, RutaCompleta = '1|2|40|10' Where Arbol = 'OCEN' and Rama = 10
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 11 )  Insert Into Net_Navegacion Values ( 'OCEN', 11, 'Claves-Sales SSA', 40, 'FrmClavesSSASales', 'OficinaCentral', 3, '1|2|40|11' )    Else Update Net_Navegacion Set Nombre = 'Claves-Sales SSA', Padre = 40, FormaLoad = 'FrmClavesSSASales', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|2|40|11' Where Arbol = 'OCEN' and Rama = 11
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 13 )  Insert Into Net_Navegacion Values ( 'OCEN', 13, 'Estados', 41, 'FrmEstados', 'OficinaCentral', 1, '1|2|41|13' )    Else Update Net_Navegacion Set Nombre = 'Estados', Padre = 41, FormaLoad = 'FrmEstados', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|2|41|13' Where Arbol = 'OCEN' and Rama = 13
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 15 )  Insert Into Net_Navegacion Values ( 'OCEN', 15, 'Programas', 42, 'FrmProgramas', 'OficinaCentral', 11, '1|2|42|15' )    Else Update Net_Navegacion Set Nombre = 'Programas', Padre = 42, FormaLoad = 'FrmProgramas', GrupoOpciones = 'OficinaCentral', IdOrden = 11, RutaCompleta = '1|2|42|15' Where Arbol = 'OCEN' and Rama = 15
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 16 )  Insert Into Net_Navegacion Values ( 'OCEN', 16, 'Sub-Programas', 42, 'FrmSubProgramas', 'OficinaCentral', 12, '1|2|42|16' )    Else Update Net_Navegacion Set Nombre = 'Sub-Programas', Padre = 42, FormaLoad = 'FrmSubProgramas', GrupoOpciones = 'OficinaCentral', IdOrden = 12, RutaCompleta = '1|2|42|16' Where Arbol = 'OCEN' and Rama = 16
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 17 )  Insert Into Net_Navegacion Values ( 'OCEN', 17, 'Regiones', 42, 'FrmRegiones', 'OficinaCentral', 13, '1|2|42|17' )    Else Update Net_Navegacion Set Nombre = 'Regiones', Padre = 42, FormaLoad = 'FrmRegiones', GrupoOpciones = 'OficinaCentral', IdOrden = 13, RutaCompleta = '1|2|42|17' Where Arbol = 'OCEN' and Rama = 17
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 18 )  Insert Into Net_Navegacion Values ( 'OCEN', 18, 'Sub-Regiones', 42, 'FrmSubRegiones', 'OficinaCentral', 14, '1|2|42|18' )    Else Update Net_Navegacion Set Nombre = 'Sub-Regiones', Padre = 42, FormaLoad = 'FrmSubRegiones', GrupoOpciones = 'OficinaCentral', IdOrden = 14, RutaCompleta = '1|2|42|18' Where Arbol = 'OCEN' and Rama = 18
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 19 )  Insert Into Net_Navegacion Values ( 'OCEN', 19, 'Productos', 40, 'FrmProductos', 'OficinaCentral', 2, '1|2|40|19' )    Else Update Net_Navegacion Set Nombre = 'Productos', Padre = 40, FormaLoad = 'FrmProductos', GrupoOpciones = 'OficinaCentral', IdOrden = 2, RutaCompleta = '1|2|40|19' Where Arbol = 'OCEN' and Rama = 19
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 20 )  Insert Into Net_Navegacion Values ( 'OCEN', 20, 'Kardex de articulos', 4, '', '', 1, '1|4|20' )    Else Update Net_Navegacion Set Nombre = 'Kardex de articulos', Padre = 4, FormaLoad = '', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|4|20' Where Arbol = 'OCEN' and Rama = 20
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 21 )  Insert Into Net_Navegacion Values ( 'OCEN', 21, 'Proximos a caducar', 4, '', '', 3, '1|4|21' )    Else Update Net_Navegacion Set Nombre = 'Proximos a caducar', Padre = 4, FormaLoad = '', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|4|21' Where Arbol = 'OCEN' and Rama = 21
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 22 )  Insert Into Net_Navegacion Values ( 'OCEN', 22, 'Consulta de movimientos de inventario por Farmacia', 4, '', '', 5, '1|4|22' )    Else Update Net_Navegacion Set Nombre = 'Consulta de movimientos de inventario por Farmacia', Padre = 4, FormaLoad = '', GrupoOpciones = '', IdOrden = 5, RutaCompleta = '1|4|22' Where Arbol = 'OCEN' and Rama = 22
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 23 )  Insert Into Net_Navegacion Values ( 'OCEN', 23, 'Por Estado', 20, 'FrmTest', 'OficinaCentral', 1, '1|4|20|23' )    Else Update Net_Navegacion Set Nombre = 'Por Estado', Padre = 20, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|4|20|23' Where Arbol = 'OCEN' and Rama = 23
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 24 )  Insert Into Net_Navegacion Values ( 'OCEN', 24, 'Por Farmacia', 20, 'FrmTest', 'OficinaCentral', 3, '1|4|20|24' )    Else Update Net_Navegacion Set Nombre = 'Por Farmacia', Padre = 20, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|4|20|24' Where Arbol = 'OCEN' and Rama = 24
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 25 )  Insert Into Net_Navegacion Values ( 'OCEN', 25, 'Por Sal (Clave SSA)', 20, 'FrmTest', 'OficinaCentral', 5, '1|4|20|25' )    Else Update Net_Navegacion Set Nombre = 'Por Sal (Clave SSA)', Padre = 20, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 5, RutaCompleta = '1|4|20|25' Where Arbol = 'OCEN' and Rama = 25
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 26 )  Insert Into Net_Navegacion Values ( 'OCEN', 26, 'Por Codigo interno', 20, 'FrmTest', 'OficinaCentral', 7, '1|4|20|26' )    Else Update Net_Navegacion Set Nombre = 'Por Codigo interno', Padre = 20, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 7, RutaCompleta = '1|4|20|26' Where Arbol = 'OCEN' and Rama = 26
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 27 )  Insert Into Net_Navegacion Values ( 'OCEN', 27, 'Por Codigo EAN', 20, 'FrmTest', 'OficinaCentral', 9, '1|4|20|27' )    Else Update Net_Navegacion Set Nombre = 'Por Codigo EAN', Padre = 20, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 9, RutaCompleta = '1|4|20|27' Where Arbol = 'OCEN' and Rama = 27
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 28 )  Insert Into Net_Navegacion Values ( 'OCEN', 28, 'Por Estado', 21, 'FrmTest', 'OficinaCentral', 1, '1|4|21|28' )    Else Update Net_Navegacion Set Nombre = 'Por Estado', Padre = 21, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|4|21|28' Where Arbol = 'OCEN' and Rama = 28
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 29 )  Insert Into Net_Navegacion Values ( 'OCEN', 29, 'Por Farmacia', 21, 'FrmTest', 'OficinaCentral', 3, '1|4|21|29' )    Else Update Net_Navegacion Set Nombre = 'Por Farmacia', Padre = 21, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|4|21|29' Where Arbol = 'OCEN' and Rama = 29
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 30 )  Insert Into Net_Navegacion Values ( 'OCEN', 30, 'Por Sal (Clave SSA)', 21, 'FrmTest', 'OficinaCentral', 5, '1|4|21|30' )    Else Update Net_Navegacion Set Nombre = 'Por Sal (Clave SSA)', Padre = 21, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 5, RutaCompleta = '1|4|21|30' Where Arbol = 'OCEN' and Rama = 30
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 31 )  Insert Into Net_Navegacion Values ( 'OCEN', 31, 'Por Codigo interno', 21, 'FrmTest', 'OficinaCentral', 7, '1|4|21|31' )    Else Update Net_Navegacion Set Nombre = 'Por Codigo interno', Padre = 21, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 7, RutaCompleta = '1|4|21|31' Where Arbol = 'OCEN' and Rama = 31
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 32 )  Insert Into Net_Navegacion Values ( 'OCEN', 32, 'Por Codigo EAN', 21, 'FrmTest', 'OficinaCentral', 9, '1|4|21|32' )    Else Update Net_Navegacion Set Nombre = 'Por Codigo EAN', Padre = 21, FormaLoad = 'FrmTest', GrupoOpciones = 'OficinaCentral', IdOrden = 9, RutaCompleta = '1|4|21|32' Where Arbol = 'OCEN' and Rama = 32
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 33 )  Insert Into Net_Navegacion Values ( 'OCEN', 33, 'Ventas', 22, 'FrmTest', '', 1, '1|4|22|33' )    Else Update Net_Navegacion Set Nombre = 'Ventas', Padre = 22, FormaLoad = 'FrmTest', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|4|22|33' Where Arbol = 'OCEN' and Rama = 33
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 34 )  Insert Into Net_Navegacion Values ( 'OCEN', 34, 'Compras', 22, 'FrmTest', '', 3, '1|4|22|34' )    Else Update Net_Navegacion Set Nombre = 'Compras', Padre = 22, FormaLoad = 'FrmTest', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|4|22|34' Where Arbol = 'OCEN' and Rama = 34
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 35 )  Insert Into Net_Navegacion Values ( 'OCEN', 35, 'Trasnferencias', 22, 'FrmTest', '', 5, '1|4|22|35' )    Else Update Net_Navegacion Set Nombre = 'Trasnferencias', Padre = 22, FormaLoad = 'FrmTest', GrupoOpciones = '', IdOrden = 5, RutaCompleta = '1|4|22|35' Where Arbol = 'OCEN' and Rama = 35
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 36 )  Insert Into Net_Navegacion Values ( 'OCEN', 36, 'Devoluciones', 22, '', '', 7, '1|4|22|36' )    Else Update Net_Navegacion Set Nombre = 'Devoluciones', Padre = 22, FormaLoad = '', GrupoOpciones = '', IdOrden = 7, RutaCompleta = '1|4|22|36' Where Arbol = 'OCEN' and Rama = 36
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 37 )  Insert Into Net_Navegacion Values ( 'OCEN', 37, 'Ventas', 36, 'FrmTest', '', 1, '1|4|22|36|37' )    Else Update Net_Navegacion Set Nombre = 'Ventas', Padre = 36, FormaLoad = 'FrmTest', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|4|22|36|37' Where Arbol = 'OCEN' and Rama = 37
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 38 )  Insert Into Net_Navegacion Values ( 'OCEN', 38, 'Compras', 36, 'FrmTest', '', 3, '1|4|22|36|38' )    Else Update Net_Navegacion Set Nombre = 'Compras', Padre = 36, FormaLoad = 'FrmTest', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|4|22|36|38' Where Arbol = 'OCEN' and Rama = 38
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 39 )  Insert Into Net_Navegacion Values ( 'OCEN', 39, 'Transferencias', 36, 'FrmTest', '', 5, '1|4|22|36|39' )    Else Update Net_Navegacion Set Nombre = 'Transferencias', Padre = 36, FormaLoad = 'FrmTest', GrupoOpciones = '', IdOrden = 5, RutaCompleta = '1|4|22|36|39' Where Arbol = 'OCEN' and Rama = 39
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 40 )  Insert Into Net_Navegacion Values ( 'OCEN', 40, 'Productos', 2, '', '', 5, '1|2|40' )    Else Update Net_Navegacion Set Nombre = 'Productos', Padre = 2, FormaLoad = '', GrupoOpciones = '', IdOrden = 5, RutaCompleta = '1|2|40' Where Arbol = 'OCEN' and Rama = 40
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 41 )  Insert Into Net_Navegacion Values ( 'OCEN', 41, 'Geograficos', 2, '', '', 3, '1|2|41' )    Else Update Net_Navegacion Set Nombre = 'Geograficos', Padre = 2, FormaLoad = '', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|2|41' Where Arbol = 'OCEN' and Rama = 41
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 42 )  Insert Into Net_Navegacion Values ( 'OCEN', 42, 'Programas y Regiones', 2, '', '', 7, '1|2|42' )    Else Update Net_Navegacion Set Nombre = 'Programas y Regiones', Padre = 2, FormaLoad = '', GrupoOpciones = '', IdOrden = 7, RutaCompleta = '1|2|42' Where Arbol = 'OCEN' and Rama = 42
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 43 )  Insert Into Net_Navegacion Values ( 'OCEN', 43, 'Clientes y Proveedores', 2, '', '', 9, '1|2|43' )    Else Update Net_Navegacion Set Nombre = 'Clientes y Proveedores', Padre = 2, FormaLoad = '', GrupoOpciones = '', IdOrden = 9, RutaCompleta = '1|2|43' Where Arbol = 'OCEN' and Rama = 43
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 44 )  Insert Into Net_Navegacion Values ( 'OCEN', 44, 'Clientes', 43, 'FrmClientes', 'OficinaCentral', 1, '1|2|43|44' )    Else Update Net_Navegacion Set Nombre = 'Clientes', Padre = 43, FormaLoad = 'FrmClientes', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|2|43|44' Where Arbol = 'OCEN' and Rama = 44
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 45 )  Insert Into Net_Navegacion Values ( 'OCEN', 45, 'Proveedores', 43, 'FrmProveedores', 'OficinaCentral', 3, '1|2|43|45' )    Else Update Net_Navegacion Set Nombre = 'Proveedores', Padre = 43, FormaLoad = 'FrmProveedores', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|2|43|45' Where Arbol = 'OCEN' and Rama = 45
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 46 )  Insert Into Net_Navegacion Values ( 'OCEN', 46, 'Municipios', 41, 'FrmMunicipios', 'OficinaCentral', 3, '1|2|41|46' )    Else Update Net_Navegacion Set Nombre = 'Municipios', Padre = 41, FormaLoad = 'FrmMunicipios', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|2|41|46' Where Arbol = 'OCEN' and Rama = 46
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 47 )  Insert Into Net_Navegacion Values ( 'OCEN', 47, 'Colonias', 41, 'FrmColonias', 'OficinaCentral', 5, '1|2|41|47' )    Else Update Net_Navegacion Set Nombre = 'Colonias', Padre = 41, FormaLoad = 'FrmColonias', GrupoOpciones = 'OficinaCentral', IdOrden = 5, RutaCompleta = '1|2|41|47' Where Arbol = 'OCEN' and Rama = 47
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 48 )  Insert Into Net_Navegacion Values ( 'OCEN', 48, 'Sub-Programas por Farmacia', 42, 'FrmSubProgramas_Farmacias', 'OficinaCentral', 15, '1|2|42|48' )    Else Update Net_Navegacion Set Nombre = 'Sub-Programas por Farmacia', Padre = 42, FormaLoad = 'FrmSubProgramas_Farmacias', GrupoOpciones = 'OficinaCentral', IdOrden = 15, RutaCompleta = '1|2|42|48' Where Arbol = 'OCEN' and Rama = 48
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 49 )  Insert Into Net_Navegacion Values ( 'OCEN', 49, 'Grupos terapeuticos', 40, 'FrmGruposTerapeuticos', 'OficinaCentral', 2, '1|2|40|49' )    Else Update Net_Navegacion Set Nombre = 'Grupos terapeuticos', Padre = 40, FormaLoad = 'FrmGruposTerapeuticos', GrupoOpciones = 'OficinaCentral', IdOrden = 2, RutaCompleta = '1|2|40|49' Where Arbol = 'OCEN' and Rama = 49
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 50 )  Insert Into Net_Navegacion Values ( 'OCEN', 50, 'Configuraciónes', 1, '', '', 5, '1|50' )    Else Update Net_Navegacion Set Nombre = 'Configuraciónes', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 5, RutaCompleta = '1|50' Where Arbol = 'OCEN' and Rama = 50
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 51 )  Insert Into Net_Navegacion Values ( 'OCEN', 51, 'Relación de Clientes por Estado', 50, 'FrmEstadosClientes', 'OficinaCentral', 5, '1|50|51' )    Else Update Net_Navegacion Set Nombre = 'Relación de Clientes por Estado', Padre = 50, FormaLoad = 'FrmEstadosClientes', GrupoOpciones = 'OficinaCentral', IdOrden = 5, RutaCompleta = '1|50|51' Where Arbol = 'OCEN' and Rama = 51
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 52 )  Insert Into Net_Navegacion Values ( 'OCEN', 52, 'Relación de Clientes por Farmacia', 50, 'FrmEstadosFarmaciasClientes', 'OficinaCentral', 7, '1|50|52' )    Else Update Net_Navegacion Set Nombre = 'Relación de Clientes por Farmacia', Padre = 50, FormaLoad = 'FrmEstadosFarmaciasClientes', GrupoOpciones = 'OficinaCentral', IdOrden = 7, RutaCompleta = '1|50|52' Where Arbol = 'OCEN' and Rama = 52
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 53 )  Insert Into Net_Navegacion Values ( 'OCEN', 53, 'Relación de Sales por Cliente', 50, 'FrmSalesClientes', 'OficinaCentral', 9, '1|50|53' )    Else Update Net_Navegacion Set Nombre = 'Relación de Sales por Cliente', Padre = 50, FormaLoad = 'FrmSalesClientes', GrupoOpciones = 'OficinaCentral', IdOrden = 9, RutaCompleta = '1|50|53' Where Arbol = 'OCEN' and Rama = 53
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 54 )  Insert Into Net_Navegacion Values ( 'OCEN', 54, 'Empresas y Farmacias', 2, '', '', 1, '1|2|54' )    Else Update Net_Navegacion Set Nombre = 'Empresas y Farmacias', Padre = 2, FormaLoad = '', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|2|54' Where Arbol = 'OCEN' and Rama = 54
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 55 )  Insert Into Net_Navegacion Values ( 'OCEN', 55, 'Empresas', 54, 'FrmEmpresas', 'OficinaCentral', 1, '1|2|54|55' )    Else Update Net_Navegacion Set Nombre = 'Empresas', Padre = 54, FormaLoad = 'FrmEmpresas', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|2|54|55' Where Arbol = 'OCEN' and Rama = 55
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 56 )  Insert Into Net_Navegacion Values ( 'OCEN', 56, 'Farmacias', 54, 'FrmFarmacias', 'OficinaCentral', 3, '1|2|54|56' )    Else Update Net_Navegacion Set Nombre = 'Farmacias', Padre = 54, FormaLoad = 'FrmFarmacias', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|2|54|56' Where Arbol = 'OCEN' and Rama = 56
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 57 )  Insert Into Net_Navegacion Values ( 'OCEN', 57, 'Relación de Farmacias por Empresa', 50, 'FrmEmpresasFarmacias', 'OficinaCentral', 3, '1|50|57' )    Else Update Net_Navegacion Set Nombre = 'Relación de Farmacias por Empresa', Padre = 50, FormaLoad = 'FrmEmpresasFarmacias', GrupoOpciones = 'OficinaCentral', IdOrden = 3, RutaCompleta = '1|50|57' Where Arbol = 'OCEN' and Rama = 57
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'OCEN' and Rama = 58 )  Insert Into Net_Navegacion Values ( 'OCEN', 58, 'Relación de Empresas-Estados', 50, 'FrmEmpresaEstado', 'OficinaCentral', 1, '1|50|58' )    Else Update Net_Navegacion Set Nombre = 'Relación de Empresas-Estados', Padre = 50, FormaLoad = 'FrmEmpresaEstado', GrupoOpciones = 'OficinaCentral', IdOrden = 1, RutaCompleta = '1|50|58' Where Arbol = 'OCEN' and Rama = 58
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 1 )  Insert Into Net_Navegacion Values ( 'PFAR', 1, 'Punto de Venta Farmacia', -1, '', '', 0, '1' )    Else Update Net_Navegacion Set Nombre = 'Punto de Venta Farmacia', Padre = -1, FormaLoad = '', GrupoOpciones = '', IdOrden = 0, RutaCompleta = '1' Where Arbol = 'PFAR' and Rama = 1
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 2 )  Insert Into Net_Navegacion Values ( 'PFAR', 2, 'Ventas', 1, '', '', 1, '1|2' )    Else Update Net_Navegacion Set Nombre = 'Ventas', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 1, RutaCompleta = '1|2' Where Arbol = 'PFAR' and Rama = 2
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 3 )  Insert Into Net_Navegacion Values ( 'PFAR', 3, 'Compras', 1, '', '', 3, '1|3' )    Else Update Net_Navegacion Set Nombre = 'Compras', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|3' Where Arbol = 'PFAR' and Rama = 3
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 4 )  Insert Into Net_Navegacion Values ( 'PFAR', 4, 'Transferencias', 1, '', '', 5, '1|4' )    Else Update Net_Navegacion Set Nombre = 'Transferencias', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 5, RutaCompleta = '1|4' Where Arbol = 'PFAR' and Rama = 4
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 5 )  Insert Into Net_Navegacion Values ( 'PFAR', 5, 'Inventarios', 1, '', '', 7, '1|5' )    Else Update Net_Navegacion Set Nombre = 'Inventarios', Padre = 1, FormaLoad = '', GrupoOpciones = '', IdOrden = 7, RutaCompleta = '1|5' Where Arbol = 'PFAR' and Rama = 5
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 6 )  Insert Into Net_Navegacion Values ( 'PFAR', 6, 'Salida de medicamentos', 2, 'FrmVentas', 'Farmacia', 3, '1|2|6' )    Else Update Net_Navegacion Set Nombre = 'Salida de medicamentos', Padre = 2, FormaLoad = 'FrmVentas', GrupoOpciones = 'Farmacia', IdOrden = 3, RutaCompleta = '1|2|6' Where Arbol = 'PFAR' and Rama = 6
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 7 )  Insert Into Net_Navegacion Values ( 'PFAR', 7, 'Devolución de medicamentos', 2, 'FrmVentasDevoluciones', 'Farmacia', 5, '1|2|7' )    Else Update Net_Navegacion Set Nombre = 'Devolución de medicamentos', Padre = 2, FormaLoad = 'FrmVentasDevoluciones', GrupoOpciones = 'Farmacia', IdOrden = 5, RutaCompleta = '1|2|7' Where Arbol = 'PFAR' and Rama = 7
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 8 )  Insert Into Net_Navegacion Values ( 'PFAR', 8, 'Reeimpresion de tickects', 2, 'FrmReimpresionVentas', 'Farmacia', 7, '1|2|8' )    Else Update Net_Navegacion Set Nombre = 'Reeimpresion de tickects', Padre = 2, FormaLoad = 'FrmReimpresionVentas', GrupoOpciones = 'Farmacia', IdOrden = 7, RutaCompleta = '1|2|8' Where Arbol = 'PFAR' and Rama = 8
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 9 )  Insert Into Net_Navegacion Values ( 'PFAR', 9, 'Verificador de precios', 2, 'FrmVerificadorDePrecios', 'Farmacia', 9, '1|2|9' )    Else Update Net_Navegacion Set Nombre = 'Verificador de precios', Padre = 2, FormaLoad = 'FrmVerificadorDePrecios', GrupoOpciones = 'Farmacia', IdOrden = 9, RutaCompleta = '1|2|9' Where Arbol = 'PFAR' and Rama = 9
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 10 )  Insert Into Net_Navegacion Values ( 'PFAR', 10, 'Consultar existencias en otras farmacias', 2, 'FrmConsultarExistenciasEnFarmacias', 'Farmacia', 11, '1|2|10' )    Else Update Net_Navegacion Set Nombre = 'Consultar existencias en otras farmacias', Padre = 2, FormaLoad = 'FrmConsultarExistenciasEnFarmacias', GrupoOpciones = 'Farmacia', IdOrden = 11, RutaCompleta = '1|2|10' Where Arbol = 'PFAR' and Rama = 10
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 11 )  Insert Into Net_Navegacion Values ( 'PFAR', 11, 'Registro de compras directas', 3, 'FrmComprasFarmacia', 'Farmacia', 1, '1|3|11' )    Else Update Net_Navegacion Set Nombre = 'Registro de compras directas', Padre = 3, FormaLoad = 'FrmComprasFarmacia', GrupoOpciones = 'Farmacia', IdOrden = 1, RutaCompleta = '1|3|11' Where Arbol = 'PFAR' and Rama = 11
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 12 )  Insert Into Net_Navegacion Values ( 'PFAR', 12, 'Devolución de compras directas', 3, 'FrmComprasDevoluciones', 'Farmacia', 3, '1|3|12' )    Else Update Net_Navegacion Set Nombre = 'Devolución de compras directas', Padre = 3, FormaLoad = 'FrmComprasDevoluciones', GrupoOpciones = 'Farmacia', IdOrden = 3, RutaCompleta = '1|3|12' Where Arbol = 'PFAR' and Rama = 12
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 13 )  Insert Into Net_Navegacion Values ( 'PFAR', 13, 'Salidas por transferencia', 4, 'FrmTransferenciaSalidas', 'Farmacia', 1, '1|4|13' )    Else Update Net_Navegacion Set Nombre = 'Salidas por transferencia', Padre = 4, FormaLoad = 'FrmTransferenciaSalidas', GrupoOpciones = 'Farmacia', IdOrden = 1, RutaCompleta = '1|4|13' Where Arbol = 'PFAR' and Rama = 13
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 14 )  Insert Into Net_Navegacion Values ( 'PFAR', 14, 'Entradas por transferencia', 4, 'FrmTransferenciaEntradas', 'Farmacia', 3, '1|4|14' )    Else Update Net_Navegacion Set Nombre = 'Entradas por transferencia', Padre = 4, FormaLoad = 'FrmTransferenciaEntradas', GrupoOpciones = 'Farmacia', IdOrden = 3, RutaCompleta = '1|4|14' Where Arbol = 'PFAR' and Rama = 14
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 15 )  Insert Into Net_Navegacion Values ( 'PFAR', 15, 'Seguimiento de salidas por transferencia', 4, 'FrmSeguimientoTransferenciaSalidas', 'Farmacia', 5, '1|4|15' )    Else Update Net_Navegacion Set Nombre = 'Seguimiento de salidas por transferencia', Padre = 4, FormaLoad = 'FrmSeguimientoTransferenciaSalidas', GrupoOpciones = 'Farmacia', IdOrden = 5, RutaCompleta = '1|4|15' Where Arbol = 'PFAR' and Rama = 15
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 16 )  Insert Into Net_Navegacion Values ( 'PFAR', 16, 'Kardex de producto', 5, 'FrmKardexProducto', 'Farmacia', 3, '1|5|16' )    Else Update Net_Navegacion Set Nombre = 'Kardex de producto', Padre = 5, FormaLoad = 'FrmKardexProducto', GrupoOpciones = 'Farmacia', IdOrden = 3, RutaCompleta = '1|5|16' Where Arbol = 'PFAR' and Rama = 16
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 17 )  Insert Into Net_Navegacion Values ( 'PFAR', 17, 'Consulta de existencia', 5, '', '', 3, '1|5|17' )    Else Update Net_Navegacion Set Nombre = 'Consulta de existencia', Padre = 5, FormaLoad = '', GrupoOpciones = '', IdOrden = 3, RutaCompleta = '1|5|17' Where Arbol = 'PFAR' and Rama = 17
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 18 )  Insert Into Net_Navegacion Values ( 'PFAR', 18, 'Por Clave SSA', 17, 'FrmExistenciaPorClaveSSA', 'Farmacia', 1, '1|5|17|18' )    Else Update Net_Navegacion Set Nombre = 'Por Clave SSA', Padre = 17, FormaLoad = 'FrmExistenciaPorClaveSSA', GrupoOpciones = 'Farmacia', IdOrden = 1, RutaCompleta = '1|5|17|18' Where Arbol = 'PFAR' and Rama = 18
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 19 )  Insert Into Net_Navegacion Values ( 'PFAR', 19, 'Por Codigo interno', 17, 'FrmExistenciaPorCodigoInterno', 'Farmacia', 3, '1|5|17|19' )    Else Update Net_Navegacion Set Nombre = 'Por Codigo interno', Padre = 17, FormaLoad = 'FrmExistenciaPorCodigoInterno', GrupoOpciones = 'Farmacia', IdOrden = 3, RutaCompleta = '1|5|17|19' Where Arbol = 'PFAR' and Rama = 19
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 20 )  Insert Into Net_Navegacion Values ( 'PFAR', 20, 'Por Codigo EAN', 17, 'FrmExistenciaPorCodigoEAN', 'Farmacia', 5, '1|5|17|20' )    Else Update Net_Navegacion Set Nombre = 'Por Codigo EAN', Padre = 17, FormaLoad = 'FrmExistenciaPorCodigoEAN', GrupoOpciones = 'Farmacia', IdOrden = 5, RutaCompleta = '1|5|17|20' Where Arbol = 'PFAR' and Rama = 20
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 22 )  Insert Into Net_Navegacion Values ( 'PFAR', 22, 'Proximos a caducar', 5, 'FrmProximosACaducar', 'Farmacia', 5, '1|5|22' )    Else Update Net_Navegacion Set Nombre = 'Proximos a caducar', Padre = 5, FormaLoad = 'FrmProximosACaducar', GrupoOpciones = 'Farmacia', IdOrden = 5, RutaCompleta = '1|5|22' Where Arbol = 'PFAR' and Rama = 22
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 23 )  Insert Into Net_Navegacion Values ( 'PFAR', 23, 'Productos por debajo de stock minimo', 5, 'FrmProductosStockMinimo', 'Farmacia', 7, '1|5|23' )    Else Update Net_Navegacion Set Nombre = 'Productos por debajo de stock minimo', Padre = 5, FormaLoad = 'FrmProductosStockMinimo', GrupoOpciones = 'Farmacia', IdOrden = 7, RutaCompleta = '1|5|23' Where Arbol = 'PFAR' and Rama = 23
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 25 )  Insert Into Net_Navegacion Values ( 'PFAR', 25, 'Procesos', 1, '', 'Farmacia', 11, '1|25' )    Else Update Net_Navegacion Set Nombre = 'Procesos', Padre = 1, FormaLoad = '', GrupoOpciones = 'Farmacia', IdOrden = 11, RutaCompleta = '1|25' Where Arbol = 'PFAR' and Rama = 25
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 26 )  Insert Into Net_Navegacion Values ( 'PFAR', 26, 'Cambio de cajero', 25, 'FrmCambioDeCajero', 'Farmacia', 1, '1|25|26' )    Else Update Net_Navegacion Set Nombre = 'Cambio de cajero', Padre = 25, FormaLoad = 'FrmCambioDeCajero', GrupoOpciones = 'Farmacia', IdOrden = 1, RutaCompleta = '1|25|26' Where Arbol = 'PFAR' and Rama = 26
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 27 )  Insert Into Net_Navegacion Values ( 'PFAR', 27, 'Corte Parcial', 25, 'FrmCorteParcial', 'Farmacia', 3, '1|25|27' )    Else Update Net_Navegacion Set Nombre = 'Corte Parcial', Padre = 25, FormaLoad = 'FrmCorteParcial', GrupoOpciones = 'Farmacia', IdOrden = 3, RutaCompleta = '1|25|27' Where Arbol = 'PFAR' and Rama = 27
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 28 )  Insert Into Net_Navegacion Values ( 'PFAR', 28, 'Cambio de dia', 25, 'FrmCorteDelDia', 'Farmacia', 5, '1|25|28' )    Else Update Net_Navegacion Set Nombre = 'Cambio de dia', Padre = 25, FormaLoad = 'FrmCorteDelDia', GrupoOpciones = 'Farmacia', IdOrden = 5, RutaCompleta = '1|25|28' Where Arbol = 'PFAR' and Rama = 28
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 29 )  Insert Into Net_Navegacion Values ( 'PFAR', 29, 'Captura de Inventario Inicial', 5, 'FrmInventarioInicial', 'Farmacia', 1, '1|5|29' )    Else Update Net_Navegacion Set Nombre = 'Captura de Inventario Inicial', Padre = 5, FormaLoad = 'FrmInventarioInicial', GrupoOpciones = 'Farmacia', IdOrden = 1, RutaCompleta = '1|5|29' Where Arbol = 'PFAR' and Rama = 29
If Not Exists ( Select * From Net_Navegacion Where Arbol = 'PFAR' and Rama = 30 )  Insert Into Net_Navegacion Values ( 'PFAR', 30, 'Venta al Publico', 2, 'FrmVentasPubGen', 'Farmacia', 1, '1|2|30' )    Else Update Net_Navegacion Set Nombre = 'Venta al Publico', Padre = 2, FormaLoad = 'FrmVentasPubGen', GrupoOpciones = 'Farmacia', IdOrden = 1, RutaCompleta = '1|2|30' Where Arbol = 'PFAR' and Rama = 30
Go
*/ 

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
--- Esta tabla 
If Exists ( Select Name From Sysobjects Where Name = 'Net_Privilegios_Grupo' and xType = 'U' )
   Drop Table Net_Privilegios_Grupo 
Go--#SQL  

Create Table Net_Privilegios_Grupo 
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
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Privilegios_Grupo Add Constraint PK_Net_Privilegios_Grupo 
    Primary Key ( IdEstado, IdFarmacia, IdGrupo, Arbol, Rama )	
Go--#SQL 

Alter Table Net_Privilegios_Grupo Add Constraint FK_NetGruposDeUsuarios_Net_Privilegios_Grupo 
	Foreign Key ( IdEstado, IdFarmacia, IdGrupo ) References Net_Grupos_De_Usuarios ( IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  

-- Alter Table Net_Privilegios_Grupo Add Constraint FK_Net_Privilegios_Grupo_Net_Navegacion 
--	Foreign Key ( Arbol, Rama ) References Net_Navegacion ( Arbol, Rama ) 
--	ON UPDATE  NO ACTION 
--	ON DELETE  CASCADE 
Go--#SQL 
