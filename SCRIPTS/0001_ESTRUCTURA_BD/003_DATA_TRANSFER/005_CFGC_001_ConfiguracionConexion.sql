---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ConfigurarConexion' and xType = 'U' )
   Drop Table CFGC_ConfigurarConexion 
Go--#SQL   

Create Table CFGC_ConfigurarConexion 
(
	Servidor varchar(100) Not Null Default '', 
	WebService varchar(100) Not Null Default '', 
	PaginaWeb varchar(100) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 

    ServidorFTP varchar(100) Not Null Default '', 	
    UserFTP varchar(50) Not Null Default '', 
    PasswordFTP varchar(500) Not Null Default '', 	
	SSL bit Not Null Default 'false', 
	ModoActivoDeTransferenciaFTP bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A'
) 
Go--#SQL   

Alter Table CFGC_ConfigurarConexion Add Constraint PK_CFGC_ConfigurarConexion Primary Key ( Servidor, WebService, PaginaWeb )
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_ConfigurarConexiones' and xType = 'U' )
   Drop Table CFGS_ConfigurarConexiones 
Go--#SQL   

Create Table CFGS_ConfigurarConexiones 
(
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	Servidor varchar(100) Not Null Default '', 
	WebService varchar(100) Not Null Default '', 
	PaginaWeb varchar(100) Not Null Default '', 
	SSL bit Not Null Default 'false',  
	ModoActivoDeTransferenciaFTP bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CFGS_ConfigurarConexiones Add Constraint PK_CFGS_ConfigurarConexiones Primary Key ( IdEstado, IdFarmacia )
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ConfigurarObtencion' and xType = 'U' )
   Drop Table CFGC_ConfigurarObtencion 
Go--#SQL   

Create Table CFGC_ConfigurarObtencion 
(
	Periodicidad smallint Not Null Default 1, 
	Semanas smallint Not Null Default 1, 
	bLunes bit Not Null Default 'false',
	bMartes bit Not Null Default 'false',
	bMiercoles bit Not Null Default 'false',
	bJueves bit Not Null Default 'false',
	bViernes bit Not Null Default 'false',
	bSabado bit Not Null Default 'false',	
	bDomingo bit Not Null Default 'false',
	HoraEjecucion SmallDatetime Not Null Default getdate(), 
	bRepetirProceso bit Not Null Default 'false', 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate()+30, 
	RutaUbicacionArchivos varchar(500) Not Null Default '', 
	RutaUbicacionArchivosEnviados varchar(500) Not Null Default '' , 
	ServicioHabilitado bit Not Null Default 'true', 
	
	TipoReplicacion int Not Null Default 1, 
	DiasRevision int Not Null Default 15, 
	TipoDePaquete int Not Null Default 3, 
	TamañoDePaquete int Not Null Default 2   	
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_ConfigurarObtencion' and xType = 'U' )
   Drop Table CFGS_ConfigurarObtencion 
Go--#SQL   

Create Table CFGS_ConfigurarObtencion 
(
	Periodicidad smallint Not Null Default 1, 
	Semanas smallint Not Null Default 1, 
	bLunes bit Not Null Default 'false',
	bMartes bit Not Null Default 'false',
	bMiercoles bit Not Null Default 'false',
	bJueves bit Not Null Default 'false',
	bViernes bit Not Null Default 'false',
	bSabado bit Not Null Default 'false',	
	bDomingo bit Not Null Default 'false',
	HoraEjecucion SmallDatetime Not Null Default getdate(), 
	bRepetirProceso bit Not Null Default 'false', 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate()+30, 
	RutaUbicacionArchivos varchar(500) Not Null Default '', 
	RutaUbicacionArchivosEnviados varchar(500) Not Null Default '', 
	ServicioHabilitado bit Not Null Default 'true', 
	
	TipoReplicacion int Not Null Default 1, 
	DiasRevision int Not Null Default 15, 
	TipoDePaquete int Not Null Default 3, 
	TamañoDePaquete int Not Null Default 2   	
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ConfigurarIntegracion' and xType = 'U' )
   Drop Table CFGC_ConfigurarIntegracion 
Go--#SQL   

Create Table CFGC_ConfigurarIntegracion 
(
	RutaArchivosRecibidos varchar(500) Not Null Default '', 
	RutaArchivosIntegrados varchar(500) Not Null Default '', 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate(), 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	ServicioHabilitado bit Not Null Default 'true' 
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_ConfigurarIntegracion' and xType = 'U' )
   Drop Table CFGS_ConfigurarIntegracion 
Go--#SQL   

Create Table CFGS_ConfigurarIntegracion 
(
	RutaArchivosRecibidos varchar(500) Not Null Default '', 
	RutaArchivosIntegrados varchar(500) Not Null Default '', 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate(), 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	ServicioHabilitado bit Not Null Default 'true'    
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_ConfigurarConexion' and xType = 'U' )
   Drop Table CFGS_ConfigurarConexion 
Go--#SQL   

Create Table CFGS_ConfigurarConexion 
(
	Servidor varchar(100) Not Null Default '', 
	WebService varchar(100) Not Null Default '', 
	PaginaWeb varchar(100) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	SSL bit Not Null Default 'false',  
	ModoActivoDeTransferenciaFTP bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL   

Alter Table CFGS_ConfigurarConexion Add Constraint PK_CFGS_ConfigurarConexion Primary Key ( Servidor, WebService, PaginaWeb )
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_ConfigurarConexion' and xType = 'U' )
   Drop Table CFGSC_ConfigurarConexion 
Go--#SQL   

Create Table CFGSC_ConfigurarConexion 
(
	Servidor varchar(100) Not Null Default '', 
	WebService varchar(100) Not Null Default '', 
	PaginaWeb varchar(100) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	
    ServidorFTP varchar(100) Not Null Default '', 	
    UserFTP varchar(50) Not Null Default '', 
    PasswordFTP varchar(500) Not Null Default '', 
	SSL bit Not Null Default 'false',  	
	ModoActivoDeTransferenciaFTP bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL   

Alter Table CFGSC_ConfigurarConexion Add Constraint PK_CFGSC_ConfigurarConexion Primary Key ( Servidor, WebService, PaginaWeb )
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_ConfigurarConexiones' and xType = 'U' )
   Drop Table CFGSC_ConfigurarConexiones 
Go--#SQL   

Create Table CFGSC_ConfigurarConexiones 
(
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	Servidor varchar(100) Not Null Default '', 
	WebService varchar(100) Not Null Default '', 
	PaginaWeb varchar(100) Not Null Default '', 
	SSL bit Not Null Default 'false',  
	ModoActivoDeTransferenciaFTP bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CFGSC_ConfigurarConexiones Add Constraint PK_CFGSC_ConfigurarConexiones Primary Key ( IdEstado, IdFarmacia )
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_ConfigurarObtencion' and xType = 'U' )
   Drop Table CFGSC_ConfigurarObtencion 
Go--#SQL   

Create Table CFGSC_ConfigurarObtencion 
(
	Periodicidad smallint Not Null Default 1, 
	Semanas smallint Not Null Default 1, 
	bLunes bit Not Null Default 'false',
	bMartes bit Not Null Default 'false',
	bMiercoles bit Not Null Default 'false',
	bJueves bit Not Null Default 'false',
	bViernes bit Not Null Default 'false',
	bSabado bit Not Null Default 'false',	
	bDomingo bit Not Null Default 'false',
	HoraEjecucion SmallDatetime Not Null Default getdate(), 
	bRepetirProceso bit Not Null Default 'false', 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate()+30, 
	RutaUbicacionArchivos varchar(500) Not Null Default '', 
	RutaUbicacionArchivosEnviados varchar(500) Not Null Default '', 
	ServicioHabilitado bit Not Null Default 'true', 
	
	TipoReplicacion int Not Null Default 1, 
	DiasRevision int Not Null Default 15, 
	TipoDePaquete int Not Null Default 3, 
	TamañoDePaquete int Not Null Default 2  
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_ConfigurarIntegracion' and xType = 'U' )
   Drop Table CFGSC_ConfigurarIntegracion 
Go--#SQL   

Create Table CFGSC_ConfigurarIntegracion 
(
	RutaArchivosRecibidos varchar(500) Not Null Default '', 
	RutaArchivosIntegrados varchar(500) Not Null Default '', 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate(), 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	ServicioHabilitado bit Not Null Default 'true'   
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGCR_ConfigurarObtencion' and xType = 'U' )
   Drop Table CFGCR_ConfigurarObtencion 
Go--#SQL   

Create Table CFGCR_ConfigurarObtencion 
(
	Periodicidad smallint Not Null Default 1, 
	Semanas smallint Not Null Default 1, 
	bLunes bit Not Null Default 'false',
	bMartes bit Not Null Default 'false',
	bMiercoles bit Not Null Default 'false',
	bJueves bit Not Null Default 'false',
	bViernes bit Not Null Default 'false',
	bSabado bit Not Null Default 'false',	
	bDomingo bit Not Null Default 'false',
	HoraEjecucion SmallDatetime Not Null Default getdate(), 
	bRepetirProceso bit Not Null Default 'false', 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate()+30, 
	RutaUbicacionArchivos varchar(500) Not Null Default '', 
	RutaUbicacionArchivosEnviados varchar(500) Not Null Default '', 
	
	ServicioHabilitado bit Not Null Default 'true', 
	
	TipoReplicacion int Not Null Default 1, 
	DiasRevision int Not Null Default 15, 
	TipoDePaquete int Not Null Default 3, 
	TamañoDePaquete int Not Null Default 2   	
		
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGCR_ConfigurarIntegracion' and xType = 'U' )
   Drop Table CFGCR_ConfigurarIntegracion 
Go--#SQL   

Create Table CFGCR_ConfigurarIntegracion 
(
	RutaArchivosRecibidos varchar(500) Not Null Default '', 
	RutaArchivosIntegrados varchar(500) Not Null Default '', 
	bFechaTerminacion bit Not Null Default 'false', 
	FechaTerminacion datetime Not Null Default getdate(), 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	ServicioHabilitado bit Not Null Default 'true' 
)
Go--#SQL   