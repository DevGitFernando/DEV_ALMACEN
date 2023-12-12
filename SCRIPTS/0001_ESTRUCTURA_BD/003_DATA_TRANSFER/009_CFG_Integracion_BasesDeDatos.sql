-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_RegistroIntegracionBD' and xType = 'U' )
   Drop Table CFG_RegistroIntegracionBD  
Go--#SQL   

Create Table CFG_RegistroIntegracionBD 
(
    IdRegistro int identity(1,1),  
    NombreBD varchar (200) Not Null Default '',  
    Version varchar(20) Not Null Default '', 
    FechaRegistro datetime Not Null Default getdate(),  
	TipoResultado smallint Not Null Default 1, 
    HostName varchar(100) Not Null Default host_name()      
)
Go--#SQL   

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_ConfigurarIntegracionBDS' and xType = 'U' )
   Drop Table CFG_ConfigurarIntegracionBDS 
Go--#SQL   

Create Table CFG_ConfigurarIntegracionBDS 
(
	RutaBDS_Integrar varchar(500) Not Null Default '', 
	RutaBDS_Integradas varchar(500) Not Null Default '', 
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
	 MesesInformacionMigrar Int Not Null Default 12 
)
Go--#SQL   


-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_DetallesExcepcionBDS' and xType = 'U' ) 
   Drop Table CFG_DetallesExcepcionBDS 
Go--#SQL   

Create Table CFG_DetallesExcepcionBDS 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table CFG_DetallesExcepcionBDS Add Constraint PK_CFG_DetallesExcepcionBDS Primary Key ( NombreTabla ) 
Go--#SQL   

Set NoCount On 
Go--#SQL   

Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'Net_Usuarios', 1 
Go--#SQL   

Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'Movtos_Inv_Tipos_Farmacia', 2 
Go--#SQL   


Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'ValesEnc', 3 

Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'ValesDet', 4 

Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'ValesDet_Lotes', 5 
Go--#SQL   


Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'Vales_EmisionEnc', 6 

Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'Vales_EmisionDet', 7 

Insert Into CFG_DetallesExcepcionBDS ( NombreTabla, IdOrden ) 
Select 'Vales_Emision_InformacionAdicional', 8 
Go--#SQL  