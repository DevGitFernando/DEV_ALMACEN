---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_RP_ConfigurarConexiones' and xType = 'U' )
   Drop Table CFG_RP_ConfigurarConexiones 
Go--#SQL   

Create Table CFG_RP_ConfigurarConexiones 
(
	IdConexion int Not Null identity(1,1), 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	

	SSL bit Not Null Default 'false', 
	Servidor varchar(100) Not Null Default '', 
	WebService varchar(100) Not Null Default '', 
	PaginaWeb varchar(100) Not Null Default '', 
	IdOrden int Not Null Default 0, 
		
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL   

Alter Table CFG_RP_ConfigurarConexiones Add Constraint PK_CFG_RP_ConfigurarConexiones Primary Key ( Servidor, WebService, PaginaWeb )
Go--#SQL   

	Insert Into CFG_RP_ConfigurarConexiones ( IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status ) 
	Select  '000', '00', '0000', 1, 'www.intermedgto.mx', 'wsReplicacion' as WebService, 'wsReplicacion' as PaginaWeb, 1, 'A' 
	
	----Insert Into CFG_RP_ConfigurarConexiones ( IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status ) 
	----Select  '001', '11', '0005', 1, 'intermedsql.cloudapp.net', 'wsReplicacion' as WebService, 'wsReplicacion' as PaginaWeb, 1, 'A' 	
Go--#SQL   


/* 
	
	select * from CFG_RP_ConfigurarConexiones  
	
delete from 	CFG_RP_ConfigurarConexiones 
	
	truncate table CFG_RP_ConfigurarConexiones  
	
	Insert Into CFG_RP_ConfigurarConexiones ( IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status ) 
	Select  '001', '11', '0047', 0, 'intermedsql.cloudapp.net' as Servidor, 'wsInt-OficinaCentral' as WebService, 'wsOficinaCentral' as PaginaWeb, 1, 'A' 

	Insert Into CFG_RP_ConfigurarConexiones ( IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status ) 
	Select  '001', '11', '0047', 0, 'intermed.homeip.net' as Servidor, 'wsInt-OficinaCentral' as WebService, 'wsOficinaCentral' as PaginaWeb, 1, 'A' 

	Insert Into CFG_RP_ConfigurarConexiones ( IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status ) 
	Select  '001', '11', '0047', 0, 'lapJesus' as Servidor, 'wsOficinaCentral/CAPACITACION' as WebService, 'wsOficinaCentral' as PaginaWeb, 1, 'A' 


	Insert Into CFG_RP_ConfigurarConexiones ( IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status ) 
	Select  '001', '11', '0047', 0, 'intermedsql.cloudapp.net', 'wsReplicacion' as WebService, 'wsReplicacion' as PaginaWeb, 1, 'A' 

*/ 


---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_RP_EnvioInformacion' and xType = 'U' ) 
   Drop Table CFG_RP_EnvioInformacion 
Go--#SQL   

Create Table CFG_RP_EnvioInformacion 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	FiltroFecha int Not Null Default 0, 	
	IdOrden int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table CFG_RP_EnvioInformacion Add Constraint PK_CFG_RP_EnvioInformacion Primary Key ( NombreTabla, Status ) 
Go--#SQL   

	Insert Into CFG_RP_EnvioInformacion ( NombreTabla, FiltroFecha, Status ) values ( 'CFGC_EnvioDetalles', 1, 'A' ) 
	Insert Into CFG_RP_EnvioInformacion ( NombreTabla, FiltroFecha, Status ) values ( 'CFGC_EnvioDetallesTrans', 1, 'A' )
	Insert Into CFG_RP_EnvioInformacion ( NombreTabla, FiltroFecha, Status ) values ( 'CFGC_EnvioControles', 0, 'A' )
	-- Insert Into CFG_RP_EnvioInformacion ( NombreTabla, FiltroFecha, Status ) values ( 'CFGS_EnvioCatalogos', 0, 'C' ) 
Go--#SQL    
 
Update CFG_RP_EnvioInformacion Set IdOrden = IdEnvio + 100 
Go--#SQL  




---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_RP_EnvioInformacionCatalogos' and xType = 'U' ) 
   Drop Table CFGS_RP_EnvioInformacionCatalogos 
Go--#SQL   

Create Table CFGS_RP_EnvioInformacionCatalogos 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	FiltroFecha int Not Null Default 0, 	
	IdOrden int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table CFGS_RP_EnvioInformacionCatalogos Add Constraint PK_CFGS_RP_EnvioInformacionCatalogos Primary Key ( NombreTabla, Status ) 
Go--#SQL   


	----insert into CFGS_RP_EnvioInformacionCatalogos ( NombreTabla, FiltroFecha, IdOrden, Status, Actualizado ) 
	----select NombreTabla, 0 as FiltroFecha, IdOrden, Status, Actualizado  
	----from CFGS_EnvioCatalogos
	----order by IdOrden 
	
	Insert Into CFGS_RP_EnvioInformacionCatalogos ( NombreTabla, FiltroFecha, Status ) values ( 'CFGS_EnvioCatalogos', 1, 'A' ) 	
Go--#SQL    
 
Update CFGS_RP_EnvioInformacionCatalogos Set IdOrden = IdEnvio + 100 
Go--#SQL  
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_EnvioControles' and xType = 'U' ) 
   Drop Table CFGC_EnvioControles 
Go--#SQL   

Create Table CFGC_EnvioControles 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	FiltroFecha int Not Null Default 0, 	
	IdOrden int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table CFGC_EnvioControles Add Constraint PK_CFGC_EnvioControles Primary Key ( NombreTabla, Status ) 
Go--#SQL
	
	Insert Into CFGC_EnvioControles ( NombreTabla, FiltroFecha, Status ) values ( 'Ctl_Replicaciones', 1, 'A' ) 	
Go--#SQL    
 
	Update CFGC_EnvioControles Set IdOrden = IdEnvio + 100 
Go--#SQL 

