Set NoCount On
Go--#SQL   

---------------------------------------------- 
/* 
	Informacion enviada a Oficina Central desde Oficinas Regionales 
*/ 
---------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGCR_EnvioDetalles' and xType = 'U' ) 
   Drop Table CFGCR_EnvioDetalles  
Go--#SQL   

Create Table CFGCR_EnvioDetalles 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGCR_EnvioDetalles Add Constraint PK_CFGCR_EnvioDetalles Primary Key ( NombreTabla ) 
Go--#SQL   

------------------------- Tablas de Configuracion 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'Net_Usuarios' ) 


------------------------- 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'CtlCortesParciales' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'CtlCortesDiarios' ) 
 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'FarmaciaProductos' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'FarmaciaProductos_CodigoEAN_Lotes' ) 

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'Movtos_Inv_Tipos_Farmacia' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'MovtosInv_Enc' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'MovtosInv_Det_CodigosEAN' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'MovtosInv_Det_CodigosEAN_Lotes' ) 

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'CatMedicos' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'CatBeneficiarios' ) 

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'VentasEnc' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'VentasDet' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'VentasDet_Lotes' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'VentasEstDispensacion' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'VentasInformacionAdicional' )  

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ComprasEnc' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ComprasDet' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ComprasDet_Lotes' ) 

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'TransferenciasEnc' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'TransferenciasDet' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'TransferenciasDet_Lotes' ) 

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'DevolucionesEnc' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'DevolucionesDet' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'DevolucionesDet_Lotes' ) 

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ALMJ_Pedidos_RC' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ALMJ_Pedidos_RC_Det' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ALMJ_Concentrado_PedidosRC' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ALMJ_Concentrado_PedidosRC_Claves' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'ALMJ_Concentrado_PedidosRC_Pedidos' ) 
 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'Ventas_ALMJ_PedidosRC_Surtido' )  

 Insert Into CFGCR_EnvioDetalles ( NombreTabla ) values ( 'Ventas_TiempoAire' ) 
   
Update CFGCR_EnvioDetalles Set IdOrden = IdEnvio + 100 
Go--#SQL  

--    Select * From CFGCR_EnvioDetalles (nolock) 


------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGCR_EnvioDetallesTrans' and xType = 'U' ) 
   Drop Table CFGCR_EnvioDetallesTrans  
Go--#SQL   

Create Table CFGCR_EnvioDetallesTrans 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGCR_EnvioDetallesTrans Add Constraint PK_CFGCR_EnvioDetallesTrans Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGCR_EnvioDetallesTrans ( NombreTabla ) values ( 'TransferenciasEnvioEnc' ) 
 Insert Into CFGCR_EnvioDetallesTrans ( NombreTabla ) values ( 'TransferenciasEnvioDet' ) 
 Insert Into CFGCR_EnvioDetallesTrans ( NombreTabla ) values ( 'TransferenciasEnvioDet_Lotes' ) 
 Insert Into CFGCR_EnvioDetallesTrans ( NombreTabla ) values ( 'TransferenciasEnvioDet_LotesRegistrar' )  
 
 Update CFGCR_EnvioDetallesTrans Set IdOrden = IdEnvio + 100 
Go--#SQL  

