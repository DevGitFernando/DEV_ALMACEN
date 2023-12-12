----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Net_CFGC_Parametros' and xType = 'TR' ) 
   Drop Trigger TR_Net_CFGC_Parametros 
Go--#SQL 

Create Trigger TR_Net_CFGC_Parametros 
On Net_CFGC_Parametros 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CtlCortesParciales' and xType = 'TR' ) 
   Drop Trigger TR_CtlCortesParciales 
Go--#SQL 

Create Trigger TR_CtlCortesParciales 
On CtlCortesParciales 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CtlCortesDiarios' and xType = 'TR' ) 
   Drop Trigger TR_CtlCortesDiarios 
Go--#SQL 

Create Trigger TR_CtlCortesDiarios 
On CtlCortesDiarios 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos 
Go--#SQL 

Create Trigger TR_FarmaciaProductos 
On FarmaciaProductos 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos_CodigoEAN' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos_CodigoEAN 
Go--#SQL 

Create Trigger TR_FarmaciaProductos_CodigoEAN 
On FarmaciaProductos_CodigoEAN 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 

Create Trigger TR_FarmaciaProductos_CodigoEAN_Lotes 
On FarmaciaProductos_CodigoEAN_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Movtos_Inv_Tipos_Farmacia' and xType = 'TR' ) 
   Drop Trigger TR_Movtos_Inv_Tipos_Farmacia 
Go--#SQL 

Create Trigger TR_Movtos_Inv_Tipos_Farmacia 
On Movtos_Inv_Tipos_Farmacia 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Enc 
Go--#SQL 

Create Trigger TR_MovtosInv_Enc 
On MovtosInv_Enc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Det_CodigosEAN 
Go--#SQL 

Create Trigger TR_MovtosInv_Det_CodigosEAN 
On MovtosInv_Det_CodigosEAN 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL 

Create Trigger TR_MovtosInv_Det_CodigosEAN_Lotes 
On MovtosInv_Det_CodigosEAN_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Enc 
Go--#SQL 

Create Trigger TR_AjustesInv_Enc 
On AjustesInv_Enc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Det' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Det 
Go--#SQL 

Create Trigger TR_AjustesInv_Det 
On AjustesInv_Det 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Det_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Det_Lotes 
Go--#SQL 

Create Trigger TR_AjustesInv_Det_Lotes 
On AjustesInv_Det_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Enc 
Go--#SQL 

Create Trigger TR_CambiosProv_Enc 
On CambiosProv_Enc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Det_CodigosEAN 
Go--#SQL 

Create Trigger TR_CambiosProv_Det_CodigosEAN 
On CambiosProv_Det_CodigosEAN 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Det_CodigosEAN_Lotes 
Go--#SQL 

Create Trigger TR_CambiosProv_Det_CodigosEAN_Lotes 
On CambiosProv_Det_CodigosEAN_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CatMedicos' and xType = 'TR' ) 
   Drop Trigger TR_CatMedicos 
Go--#SQL 

Create Trigger TR_CatMedicos 
On CatMedicos 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CatBeneficiarios' and xType = 'TR' ) 
   Drop Trigger TR_CatBeneficiarios 
Go--#SQL 

Create Trigger TR_CatBeneficiarios 
On CatBeneficiarios 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_VentasEnc 
Go--#SQL 

Create Trigger TR_VentasEnc 
On VentasEnc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasDet' and xType = 'TR' ) 
   Drop Trigger TR_VentasDet 
Go--#SQL 

Create Trigger TR_VentasDet 
On VentasDet 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_VentasDet_Lotes 
Go--#SQL 

Create Trigger TR_VentasDet_Lotes 
On VentasDet_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEstDispensacion' and xType = 'TR' ) 
   Drop Trigger TR_VentasEstDispensacion 
Go--#SQL 

Create Trigger TR_VentasEstDispensacion 
On VentasEstDispensacion 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_VentasInformacionAdicional 
Go--#SQL 

Create Trigger TR_VentasInformacionAdicional 
On VentasInformacionAdicional 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEstadisticaClavesDispensadas' and xType = 'TR' ) 
   Drop Trigger TR_VentasEstadisticaClavesDispensadas 
Go--#SQL 

Create Trigger TR_VentasEstadisticaClavesDispensadas 
On VentasEstadisticaClavesDispensadas 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesEnc 
Go--#SQL 

Create Trigger TR_RemisionesEnc 
On RemisionesEnc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesDet' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesDet 
Go--#SQL 

Create Trigger TR_RemisionesDet 
On RemisionesDet 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesDet_Lotes 
Go--#SQL 

Create Trigger TR_RemisionesDet_Lotes 
On RemisionesDet_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_ComprasEnc 
Go--#SQL 

Create Trigger TR_ComprasEnc 
On ComprasEnc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_ComprasDet 
Go--#SQL 

Create Trigger TR_ComprasDet 
On ComprasDet 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_ComprasDet_Lotes 
Go--#SQL 

Create Trigger TR_ComprasDet_Lotes 
On ComprasDet_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasEnc' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasEnc 
Go--#SQL 

Create Trigger TR_TransferenciasEnc 
On TransferenciasEnc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasDet' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasDet 
Go--#SQL 

Create Trigger TR_TransferenciasDet 
On TransferenciasDet 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasDet_Lotes 
Go--#SQL 

Create Trigger TR_TransferenciasDet_Lotes 
On TransferenciasDet_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosEnc' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosEnc 
Go--#SQL 

Create Trigger TR_TraspasosEnc 
On TraspasosEnc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosDet' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosDet 
Go--#SQL 

Create Trigger TR_TraspasosDet 
On TraspasosDet 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosDet_Lotes 
Go--#SQL 

Create Trigger TR_TraspasosDet_Lotes 
On TraspasosDet_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesEnc 
Go--#SQL 

Create Trigger TR_DevolucionesEnc 
On DevolucionesEnc 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesDet' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesDet 
Go--#SQL 

Create Trigger TR_DevolucionesDet 
On DevolucionesDet 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesDet_Lotes 
Go--#SQL 

Create Trigger TR_DevolucionesDet_Lotes 
On DevolucionesDet_Lotes 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Pedidos_RC' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Pedidos_RC 
Go--#SQL 

Create Trigger TR_ALMJ_Pedidos_RC 
On ALMJ_Pedidos_RC 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Pedidos_RC_Det' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Pedidos_RC_Det 
Go--#SQL 

Create Trigger TR_ALMJ_Pedidos_RC_Det 
On ALMJ_Pedidos_RC_Det 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC 
Go--#SQL 

Create Trigger TR_ALMJ_Concentrado_PedidosRC 
On ALMJ_Concentrado_PedidosRC 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC_Claves' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC_Claves 
Go--#SQL 

Create Trigger TR_ALMJ_Concentrado_PedidosRC_Claves 
On ALMJ_Concentrado_PedidosRC_Claves 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC_Pedidos 
Go--#SQL 

Create Trigger TR_ALMJ_Concentrado_PedidosRC_Pedidos 
On ALMJ_Concentrado_PedidosRC_Pedidos 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Ventas_ALMJ_PedidosRC_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_Ventas_ALMJ_PedidosRC_Surtido 
Go--#SQL 

Create Trigger TR_Ventas_ALMJ_PedidosRC_Surtido 
On Ventas_ALMJ_PedidosRC_Surtido 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Ventas_TiempoAire' and xType = 'TR' ) 
   Drop Trigger TR_Ventas_TiempoAire 
Go--#SQL 

Create Trigger TR_Ventas_TiempoAire 
On Ventas_TiempoAire 
With Encryption 
For Delete 
As 
Begin 
   --- Deshacer la eliminacion de datos 
   Rollback 
    --- Enviar el mensaje de error 
   Raiserror ('Esta acción no esta permitida', 1, 1) 
End 
Go--#SQL  
 
