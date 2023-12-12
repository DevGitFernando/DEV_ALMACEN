-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Net_CFGC_Parametros' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Net_CFGC_Parametros 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CtlCortesParciales' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CtlCortesParciales 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CtlCortesDiarios' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CtlCortesDiarios 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ctl_CierresDePeriodos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ctl_CierresDePeriodos 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ctl_CierresPeriodosDetalles' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ctl_CierresPeriodosDetalles 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatPasillos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatPasillos 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatPasillos_Estantes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatPasillos_Estantes 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatPasillos_Estantes_Entrepaños' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatPasillos_Estantes_Entrepaños 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos_CodigoEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes__Historico' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes__Historico 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Enc 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasEnc_Consignacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasEnc_Consignacion 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasDet_Consignacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasDet_Consignacion 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasDet_Consignacion_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasDet_Consignacion_Lotes 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasDet_Consignacion_Lotes_Ubicaciones 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Enc 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Det 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Det_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Det_Lotes 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Det_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Det_Lotes_Ubicaciones 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CambiosProv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CambiosProv_Enc 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CambiosProv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CambiosProv_Det_CodigosEAN 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CambiosProv_Det_CodigosEAN_Lotes 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatMedicos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatMedicos 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatBeneficiarios' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatBeneficiarios 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasEnc 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasDet 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasDet_Lotes 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasDet_Lotes_Ubicaciones 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasEstDispensacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasEstDispensacion 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasInformacionAdicional 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasEstadisticaClavesDispensadas' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasEstadisticaClavesDispensadas 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_EmisionEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_EmisionEnc 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_EmisionDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_EmisionDet 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_Emision_InformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_Emision_InformacionAdicional 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_Cancelacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_Cancelacion 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ValesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ValesEnc 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ValesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ValesDet 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ValesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ValesDet_Lotes 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_RemisionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_RemisionesEnc 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_RemisionesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_RemisionesDet 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_RemisionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_RemisionesDet_Lotes 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasEnc 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasDet 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasDet_Lotes 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasDet_Lotes_Ubicaciones 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasEnc 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasDet 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasDet_Lotes 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasDet_Lotes_Ubicaciones 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionTransferenciasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionTransferenciasEnc 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionTransferenciasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionTransferenciasDet 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionTransferenciasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionTransferenciasDet_Lotes 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionTransferenciasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionTransferenciasDet_Lotes_Ubicaciones 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEnc 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasDet 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasDet_Lotes 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasDet_Lotes_Ubicaciones 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEstadisticaClavesDispensadas' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEstadisticaClavesDispensadas 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEnvioEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEnvioEnc 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEnvioDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEnvioDet 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEnvioDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEnvioDet_Lotes 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEnvioDet_LotesRegistrar' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEnvioDet_LotesRegistrar 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosEnc 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosDet 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosDet_Lotes 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosDet_Lotes_Ubicaciones 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesEnc 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesDet 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesDet_Lotes 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesDet_Lotes_Ubicaciones 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PreSalidasPedidosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PreSalidasPedidosEnc 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PreSalidasPedidosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PreSalidasPedidosDet 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PreSalidasPedidosDet_Lotes_Ubicaciones 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnc 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDet 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDet_Lotes 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDet_Lotes_Ubicaciones 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioEnc 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioDet 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioDet_Lotes 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioDet_Lotes_Registrar' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioDet_Lotes_Registrar 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDistEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDistEnc 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDistDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDistDet 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDistDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDistDet_Lotes 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatPersonalCEDIS' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatPersonalCEDIS 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Enc 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det_Pedido_Distribuidor' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det_Pedido_Distribuidor 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Enc_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Enc_Surtido 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Enc_Surtido_Atenciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Enc_Surtido_Atenciones 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det_Surtido 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det_Surtido_Distribucion 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Pedidos_RC' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Pedidos_RC 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Pedidos_RC_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Pedidos_RC_Det 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Concentrado_PedidosRC' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Claves' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Claves 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Pedidos 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ventas_ALMJ_PedidosRC_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ventas_ALMJ_PedidosRC_Surtido 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ventas_TiempoAire' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ventas_TiempoAire 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_VentasEnc 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_VentasInformacionAdicional 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_ComprasEnc 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_OrdenesDeComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_OrdenesDeComprasEnc 
Go--#SQL 
 
