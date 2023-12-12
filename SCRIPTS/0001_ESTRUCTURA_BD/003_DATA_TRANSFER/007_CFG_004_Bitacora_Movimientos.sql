If Exists ( Select Name From Sysobjects Where Name = 'CFGS_Bitacora_Movimientos' and xType = 'U' )
      Drop Table CFGS_Bitacora_Movimientos 
Go--#SQL  

Create Table CFGS_Bitacora_Movimientos
(
     IdMovimiento varchar(13) Not Null,
     FechaMovimiento datetime Not Null Default getdate(),
     IdTipoArchivo varchar(2) Null Default '',
     IdArchivo varchar(60) Null Default '',
     Motivo varchar(100) Null Default '',
     Descripcion varchar(500) Null Default '', 
     Keyx int identity(1,1) 
)
Go--#SQL  

Alter Table CFGS_Bitacora_Movimientos Add Constraint Pk_CFGS_Bitacora_Movimientos Primary Key ( IdMovimiento )
Go--#SQL  



If Exists ( Select Name From Sysobjects Where Name = 'CFGC_Bitacora_Movimientos' and xType = 'U' )
      Drop Table CFGC_Bitacora_Movimientos 
Go--#SQL  

Create Table CFGC_Bitacora_Movimientos
(
     IdMovimiento varchar(13) Not Null,
     FechaMovimiento datetime Not Null Default getdate(),
     IdTipoArchivo varchar(2) Null Default '',
     IdArchivo varchar(60) Null Default '',
     Motivo varchar(100) Null Default '',
     Descripcion varchar(500) Null Default '', 
     Keyx int identity(1,1) 
)
Go--#SQL  

Alter Table CFGC_Bitacora_Movimientos Add Constraint Pk_CFGC_Bitacora_Movimientos Primary Key ( IdMovimiento )
Go--#SQL  


