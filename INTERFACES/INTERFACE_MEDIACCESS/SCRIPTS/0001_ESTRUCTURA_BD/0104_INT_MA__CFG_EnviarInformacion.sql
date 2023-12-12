Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__CFG_Envio_FTP' and xType = 'U' ) 
   Drop Table INT_MA__CFG_Envio_FTP 
Go--#SQL   

Create Table INT_MA__CFG_Envio_FTP 
(
	TipoEnvio smallint Not Null Default 0, 
	Alias varchar(200) Not Null Default '', 
	URL_Produccion varchar(200) Not Null Default '', 
	Usuario varchar(100) Not Null Default '', 
	Password varchar(200) Not Null Default '', 
	ModoActivoDeTransferencia bit Not Null Default 'false', 
	DirectorioDeTrabajo varchar(500) Not Null Default '', 
	Status varchar(1) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_MA__CFG_Envio_FTP 
	Add Constraint PK_INT_MA__CFG_Envio_FTP Primary Key ( TipoEnvio, URL_Produccion, Usuario, Password ) 
Go--#SQL   


If Not Exists ( Select * From INT_MA__CFG_Envio_FTP Where TipoEnvio = 1 and URL_Produccion = 'casedbsql.cloudapp.net' and Usuario = 'MedClinicas' and Password = 'M3d1nt3rm3d$' )  Insert Into INT_MA__CFG_Envio_FTP (  TipoEnvio, Alias, URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, DirectorioDeTrabajo, Status )  Values ( 1, 'Envio de catálogos', 'casedbsql.cloudapp.net', 'MedClinicas', 'M3d1nt3rm3d$', 1, '', 'A' )    Else Update INT_MA__CFG_Envio_FTP Set Alias = 'Envio de catálogos', ModoActivoDeTransferencia = 1, DirectorioDeTrabajo = '', Status = 'A' Where TipoEnvio = 1 and URL_Produccion = 'casedbsql.cloudapp.net' and Usuario = 'MedClinicas' and Password = 'M3d1nt3rm3d$'  
If Not Exists ( Select * From INT_MA__CFG_Envio_FTP Where TipoEnvio = 1 and URL_Produccion = 'intermed.homeip.net' and Usuario = 'JesusDiaz' and Password = '2K11kiubo' )  Insert Into INT_MA__CFG_Envio_FTP (  TipoEnvio, Alias, URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, DirectorioDeTrabajo, Status )  Values ( 1, 'Envio de catálogos', 'intermed.homeip.net', 'JesusDiaz', '2K11kiubo', 0, '', 'C' )    Else Update INT_MA__CFG_Envio_FTP Set Alias = 'Envio de catálogos', ModoActivoDeTransferencia = 0, DirectorioDeTrabajo = '', Status = 'C' Where TipoEnvio = 1 and URL_Produccion = 'intermed.homeip.net' and Usuario = 'JesusDiaz' and Password = '2K11kiubo'  
If Not Exists ( Select * From INT_MA__CFG_Envio_FTP Where TipoEnvio = 1 and URL_Produccion = 'intermedcom.cloudapp.net' and Usuario = 'JesusDiaz' and Password = '2K11kiubo' )  Insert Into INT_MA__CFG_Envio_FTP (  TipoEnvio, Alias, URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, DirectorioDeTrabajo, Status )  Values ( 1, 'Envio de catálogos', 'intermedcom.cloudapp.net', 'JesusDiaz', '2K11kiubo', 1, '', 'C' )    Else Update INT_MA__CFG_Envio_FTP Set Alias = 'Envio de catálogos', ModoActivoDeTransferencia = 1, DirectorioDeTrabajo = '', Status = 'C' Where TipoEnvio = 1 and URL_Produccion = 'intermedcom.cloudapp.net' and Usuario = 'JesusDiaz' and Password = '2K11kiubo'  
If Not Exists ( Select * From INT_MA__CFG_Envio_FTP Where TipoEnvio = 1 and URL_Produccion = 'localhost' and Usuario = 'JesusDiaz' and Password = '2K11kiubo' )  Insert Into INT_MA__CFG_Envio_FTP (  TipoEnvio, Alias, URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, DirectorioDeTrabajo, Status )  Values ( 1, 'Envio de catálogos', 'localhost', 'JesusDiaz', '2K11kiubo', 1, '', 'C' )    Else Update INT_MA__CFG_Envio_FTP Set Alias = 'Envio de catálogos', ModoActivoDeTransferencia = 1, DirectorioDeTrabajo = '', Status = 'C' Where TipoEnvio = 1 and URL_Produccion = 'localhost' and Usuario = 'JesusDiaz' and Password = '2K11kiubo'  
If Not Exists ( Select * From INT_MA__CFG_Envio_FTP Where TipoEnvio = 2 and URL_Produccion = '189.208.105.85' and Usuario = 'farmacia' and Password = 'BN66n7!eg' )  Insert Into INT_MA__CFG_Envio_FTP (  TipoEnvio, Alias, URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, DirectorioDeTrabajo, Status )  Values ( 2, 'Envio de archivos de facturación', '189.208.105.85', 'farmacia', 'BN66n7!eg', 1, '/Farmacias/Facturacion/Archivos', 'A' )    Else Update INT_MA__CFG_Envio_FTP Set Alias = 'Envio de archivos de facturación', ModoActivoDeTransferencia = 1, DirectorioDeTrabajo = '/Farmacias/Facturacion/Archivos', Status = 'A' Where TipoEnvio = 2 and URL_Produccion = '189.208.105.85' and Usuario = 'farmacia' and Password = 'BN66n7!eg'  

------Insert Into INT_MA__CFG_Envio_FTP ( URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, Status ) 
------Select 'casedbsql.cloudapp.net', 'MedClinicas', 'M3d1nt3rm3d$', 1, 'C'

------Insert Into INT_MA__CFG_Envio_FTP ( URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, Status ) 
------Select 'intermed.homeip.net', 'JesusDiaz', '2K11kiubo', 0, 'A'

Go--#SQL   

-- sp_listacolumnas INT_MA__CFG_Envio_FTP 
