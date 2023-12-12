-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGR_RegistroFTP' and xType = 'U' )
   Drop Table CFGR_RegistroFTP  
Go--#SQL   

Create Table CFGR_RegistroFTP 
(
    IdRegistro int identity(1,1),  
    NombreBD varchar (200) Not Null Default '',  
    Version varchar(20) Not Null Default '', 
    FechaRegistro datetime Not Null Default getdate(),  
    HostName varchar(100) Not Null Default host_name()      
)
Go--#SQL   

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGR_ConfigurarFTP_BDS' and xType = 'U' )
   Drop Table CFGR_ConfigurarFTP_BDS 
Go--#SQL   

Create Table CFGR_ConfigurarFTP_BDS 
(
	RutaFTP_BDS_Integrar varchar(500) Not Null Default '', 
	Habilitado bit Not Null Default 'false', 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0  
)
Go--#SQL   


-------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_RegistroFTP' and xType = 'U' )
   Drop Table CFGS_RegistroFTP  
Go--#SQL   

Create Table CFGS_RegistroFTP 
(
    IdRegistro int identity(1,1),  
    NombreBD varchar (200) Not Null Default '',  
    Version varchar(20) Not Null Default '', 
    FechaRegistro datetime Not Null Default getdate(),  
    HostName varchar(100) Not Null Default host_name()      
)
Go--#SQL   

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_ConfigurarFTP_BDS' and xType = 'U' )
   Drop Table CFGS_ConfigurarFTP_BDS 
Go--#SQL   

Create Table CFGS_ConfigurarFTP_BDS 
(
	RutaFTP_BDS_Integrar varchar(500) Not Null Default '', 
	Habilitado bit Not Null Default 'false', 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0 
)
Go--#SQL   
