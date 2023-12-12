--------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFGSC_EnvioDetallesVentas' and xType = 'U' ) 
   Drop Table CFGSC_EnvioDetallesVentas  
Go--#SQL   

Create Table CFGSC_EnvioDetallesVentas 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGSC_EnvioDetallesVentas Add Constraint PK_CFGSC_EnvioDetallesVentas Primary Key ( NombreTabla ) 
Go--#SQL   

	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'CatMedicos' )   
	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'CatBeneficiarios' )   
 
 	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos' )   
 	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN' )   
 	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN_Lotes' )   
 	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' )   


	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasEnc' ) 
	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasDet' ) 
	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasDet_Lotes' ) 
	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasInformacionAdicional' )
	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasEstadisticaClavesDispensadas' ) 

	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'Vales_EmisionEnc' )
	Insert Into CFGSC_EnvioDetallesVentas ( NombreTabla ) values ( 'ValesEnc' ) 

 Update CFGSC_EnvioDetallesVentas Set IdOrden = IdEnvio + 100 
Go--#SQL  


--------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFGS_EnvioDetallesVentas' and xType = 'U' ) 
   Drop Table CFGS_EnvioDetallesVentas  
Go--#SQL   

Create Table CFGS_EnvioDetallesVentas 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGS_EnvioDetallesVentas Add Constraint PK_CFGS_EnvioDetallesVentas Primary Key ( NombreTabla ) 
Go--#SQL   


	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'CatMedicos' )   
	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'CatBeneficiarios' ) 

 	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos' )   
 	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN' )   
 	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN_Lotes' )   
 	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' )   


	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasEnc' ) 
	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasDet' ) 
	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasDet_Lotes' ) 
	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasInformacionAdicional' )
	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'VentasEstadisticaClavesDispensadas' )  

	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'Vales_EmisionEnc' )
	Insert Into CFGS_EnvioDetallesVentas ( NombreTabla ) values ( 'ValesEnc' ) 

 Update CFGS_EnvioDetallesVentas Set IdOrden = IdEnvio + 100 
Go--#SQL  

