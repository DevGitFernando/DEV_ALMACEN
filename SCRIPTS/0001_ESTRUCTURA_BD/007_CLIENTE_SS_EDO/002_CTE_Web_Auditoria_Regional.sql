If Exists( Select Name From SysObjects(Nolock) Where Name = 'CteReg_Web_Auditoria_Movimientos' And xType = 'U' ) 
	Drop Table CteReg_Web_Auditoria_Movimientos
Go--#SQL


---------------------------------------- 
If Exists( Select Name From SysObjects(Nolock) Where Name = 'CteReg_Web_Auditoria_Login' And xType = 'U' ) 
	Drop Table CteReg_Web_Auditoria_Login
Go--#SQL

Create Table CteReg_Web_Auditoria_Login
(
	IdEstado varchar(2) Not Null Default '',
	IdFarmacia varchar(4) Not Null Default '',
	IdUsuario varchar(4) Not Null Default '',
	IdSesion varchar(8) Not Null Default '',
	FechaRegistro datetime Default GetDate(),
	IP_Address varchar(20) Not Null Default '', 
	Actualizado smallint Not Null Default 0
)
Go--#SQL

Alter Table CteReg_Web_Auditoria_Login Add Constraint PK_CteReg_Web_Auditoria_Login Primary Key ( IdEstado, IdFarmacia, IdUsuario, IdSesion )
Go--#SQL

--------------------------------------------------------------
--------------------------------------------------------------

If Exists( Select Name From SysObjects(Nolock) Where Name = 'CteReg_Web_Auditoria_Movimientos' And xType = 'U' ) 
	Drop Table CteReg_Web_Auditoria_Movimientos
Go--#SQL

Create Table CteReg_Web_Auditoria_Movimientos
(
	IdEstado varchar(2) Not Null Default '',
	IdFarmacia varchar(4) Not Null Default '',
	IdUsuario varchar(4) Not Null Default '',
	IdSesion varchar(8) Not Null Default '',
	IdMovto varchar(8) Not Null Default '',
	FechaRegistro datetime Default GetDate(),
	IP_Address varchar(20) Not Null Default '', 
	Modulo varchar(50) Not Null Default '',
	Pantalla varchar(100) Not Null Default '', 
	Instruccion varchar(7500) Not Null Default '',
	Url_Farmacia varchar(200) Not Null Default '',
	Actualizado smallint Not Null Default 0
)
Go--#SQL

Alter Table CteReg_Web_Auditoria_Movimientos Add Constraint PK_CteReg_Web_Auditoria_Movimientos Primary Key ( IdEstado, IdFarmacia, IdUsuario, IdSesion, IdMovto )
Go--#SQL

Alter Table CteReg_Web_Auditoria_Movimientos Add Constraint FK_CteReg_Web_Auditoria_Movimientos_CteReg_Web_Auditoria_Login
	Foreign Key ( IdEstado, IdFarmacia, IdUsuario, IdSesion ) References CteReg_Web_Auditoria_Login ( IdEstado, IdFarmacia, IdUsuario, IdSesion ) 
Go--#SQL

