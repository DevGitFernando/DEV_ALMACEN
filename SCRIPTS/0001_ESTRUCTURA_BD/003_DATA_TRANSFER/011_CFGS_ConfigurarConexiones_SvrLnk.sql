If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_ConfigurarConexiones_SvrLnk' and xType = 'U' )
   Drop Table CFGS_ConfigurarConexiones_SvrLnk 
Go--#SQL   

Create Table CFGS_ConfigurarConexiones_SvrLnk 
(
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '',
	SvrLnk varchar(100) Not Null Default '',	
	Host varchar(100) Not Null Default '',
	NombreBD varchar(100) Not Null Default '',
	Usuario varchar(100) Not Null Default '',
	Password varchar(500) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CFGS_ConfigurarConexiones_SvrLnk Add Constraint PK_CFGS_ConfigurarConexiones_SvrLnk Primary Key ( IdEstado, IdFarmacia )
Go--#SQL   