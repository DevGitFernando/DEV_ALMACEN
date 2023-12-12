----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Net_CFGC_Parametros' and xType = 'TR' ) 
   Drop Trigger TR_Net_CFGC_Parametros 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CtlCortesParciales' and xType = 'TR' ) 
   Drop Trigger TR_CtlCortesParciales 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CtlCortesDiarios' and xType = 'TR' ) 
   Drop Trigger TR_CtlCortesDiarios 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos_CodigoEAN' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos_CodigoEAN 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Movtos_Inv_Tipos_Farmacia' and xType = 'TR' ) 
   Drop Trigger TR_Movtos_Inv_Tipos_Farmacia 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Enc 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Det_CodigosEAN 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Enc 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Det' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Det 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Det_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Det_Lotes 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Enc 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Det_CodigosEAN 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Det_CodigosEAN_Lotes 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CatMedicos' and xType = 'TR' ) 
   Drop Trigger TR_CatMedicos 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CatBeneficiarios' and xType = 'TR' ) 
   Drop Trigger TR_CatBeneficiarios 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_VentasEnc 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasDet' and xType = 'TR' ) 
   Drop Trigger TR_VentasDet 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_VentasDet_Lotes 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEstDispensacion' and xType = 'TR' ) 
   Drop Trigger TR_VentasEstDispensacion 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_VentasInformacionAdicional 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEstadisticaClavesDispensadas' and xType = 'TR' ) 
   Drop Trigger TR_VentasEstadisticaClavesDispensadas 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesEnc 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesDet' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesDet 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesDet_Lotes 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_ComprasEnc 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_ComprasDet 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_ComprasDet_Lotes 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasEnc' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasEnc 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasDet' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasDet 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasDet_Lotes 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosEnc' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosEnc 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosDet' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosDet 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosDet_Lotes 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesEnc 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesDet' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesDet 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesDet_Lotes 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Pedidos_RC' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Pedidos_RC 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Pedidos_RC_Det' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Pedidos_RC_Det 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC_Claves' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC_Claves 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC_Pedidos 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Ventas_ALMJ_PedidosRC_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_Ventas_ALMJ_PedidosRC_Surtido 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Ventas_TiempoAire' and xType = 'TR' ) 
   Drop Trigger TR_Ventas_TiempoAire 
Go--#SQL 
 
