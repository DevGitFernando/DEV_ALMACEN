----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Net_CFGC_Parametros' and xType = 'TR' ) 
   Drop Trigger TR_Net_CFGC_Parametros 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Parametros' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CtlCortesParciales' and xType = 'TR' ) 
   Drop Trigger TR_CtlCortesParciales 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesParciales' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CtlCortesDiarios' and xType = 'TR' ) 
   Drop Trigger TR_CtlCortesDiarios 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesDiarios' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos_CodigoEAN' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos_CodigoEAN 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_FarmaciaProductos_CodigoEAN_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Movtos_Inv_Tipos_Farmacia' and xType = 'TR' ) 
   Drop Trigger TR_Movtos_Inv_Tipos_Farmacia 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Movtos_Inv_Tipos_Farmacia' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Enc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Det_CodigosEAN 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_MovtosInv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_MovtosInv_Det_CodigosEAN_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Enc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Det' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Det 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_AjustesInv_Det_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_AjustesInv_Det_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Enc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Det_CodigosEAN 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_CambiosProv_Det_CodigosEAN_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CatMedicos' and xType = 'TR' ) 
   Drop Trigger TR_CatMedicos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_CatBeneficiarios' and xType = 'TR' ) 
   Drop Trigger TR_CatBeneficiarios 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_VentasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEnc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasDet' and xType = 'TR' ) 
   Drop Trigger TR_VentasDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_VentasDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEstDispensacion' and xType = 'TR' ) 
   Drop Trigger TR_VentasEstDispensacion 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstDispensacion' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_VentasInformacionAdicional 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasInformacionAdicional' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_VentasEstadisticaClavesDispensadas' and xType = 'TR' ) 
   Drop Trigger TR_VentasEstadisticaClavesDispensadas 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstadisticaClavesDispensadas' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesEnc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesDet' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_RemisionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_RemisionesDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_ComprasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasEnc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasEnc' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasDet' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosEnc' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosEnc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosDet' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TraspasosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_TraspasosDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesEnc' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesDet' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_DevolucionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_DevolucionesDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Pedidos_RC' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Pedidos_RC 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Pedidos_RC_Det' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Pedidos_RC_Det 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC_Det' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC_Claves' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC_Claves 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Claves' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'TR' ) 
   Drop Trigger TR_ALMJ_Concentrado_PedidosRC_Pedidos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Ventas_ALMJ_PedidosRC_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_Ventas_ALMJ_PedidosRC_Surtido 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_ALMJ_PedidosRC_Surtido' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_Ventas_TiempoAire' and xType = 'TR' ) 
   Drop Trigger TR_Ventas_TiempoAire 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_TiempoAire' and xType = 'U' ) 
Begin 
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
End 
Go--#S#QL 

 
--------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasEnvioEnc' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasEnvioEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioEnc' and xType = 'U' ) 
Begin 
     Create Trigger TR_TransferenciasEnvioEnc 
     On TransferenciasEnvioEnc 
     With Encryption 
     For Delete 
     As 
     Begin 
        --- Deshacer la eliminacion de datos 
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror ('Esta acción no esta permitida', 1, 1) 
     End 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasEnvioDet' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasEnvioDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet' and xType = 'U' ) 
Begin 
     Create Trigger TR_TransferenciasEnvioDet 
     On TransferenciasEnvioDet 
     With Encryption 
     For Delete 
     As 
     Begin 
        --- Deshacer la eliminacion de datos 
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror ('Esta acción no esta permitida', 1, 1) 
     End 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasEnvioDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasEnvioDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_Lotes' and xType = 'U' ) 
Begin 
     Create Trigger TR_TransferenciasEnvioDet_Lotes 
     On TransferenciasEnvioDet_Lotes 
     With Encryption 
     For Delete 
     As 
     Begin 
        --- Deshacer la eliminacion de datos 
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror ('Esta acción no esta permitida', 1, 1) 
     End 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_TransferenciasEnvioDet_LotesRegistrar' and xType = 'TR' ) 
   Drop Trigger TR_TransferenciasEnvioDet_LotesRegistrar 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_LotesRegistrar' and xType = 'U' ) 
Begin 
     Create Trigger TR_TransferenciasEnvioDet_LotesRegistrar 
     On TransferenciasEnvioDet_LotesRegistrar 
     With Encryption 
     For Delete 
     As 
     Begin 
        --- Deshacer la eliminacion de datos 
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror ('Esta acción no esta permitida', 1, 1) 
     End 
End 
Go--#S#QL 
 


