-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Net_CFGC_Parametros' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Net_CFGC_Parametros 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Parametros' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Net_CFGC_Parametros 
     On Net_CFGC_Parametros 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Net_CFGC_Parametros ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CtlCortesParciales' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CtlCortesParciales 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesParciales' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CtlCortesParciales 
     On CtlCortesParciales 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CtlCortesParciales ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CtlCortesDiarios' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CtlCortesDiarios 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesDiarios' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CtlCortesDiarios 
     On CtlCortesDiarios 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CtlCortesDiarios ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Ctl_CierresDePeriodos' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Ctl_CierresDePeriodos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresDePeriodos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Ctl_CierresDePeriodos 
     On Ctl_CierresDePeriodos 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Ctl_CierresDePeriodos ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Ctl_CierresPeriodosDetalles' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Ctl_CierresPeriodosDetalles 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresPeriodosDetalles' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Ctl_CierresPeriodosDetalles 
     On Ctl_CierresPeriodosDetalles 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Ctl_CierresPeriodosDetalles ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CatPasillos' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CatPasillos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CatPasillos 
     On CatPasillos 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CatPasillos ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CatPasillos_Estantes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CatPasillos_Estantes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CatPasillos_Estantes 
     On CatPasillos_Estantes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CatPasillos_Estantes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CatPasillos_Estantes_Entrepaños' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CatPasillos_Estantes_Entrepaños 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepaños' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CatPasillos_Estantes_Entrepaños 
     On CatPasillos_Estantes_Entrepaños 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CatPasillos_Estantes_Entrepaños ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_FarmaciaProductos' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_FarmaciaProductos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_FarmaciaProductos 
     On FarmaciaProductos 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ FarmaciaProductos ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_FarmaciaProductos_CodigoEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_FarmaciaProductos_CodigoEAN 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_FarmaciaProductos_CodigoEAN 
     On FarmaciaProductos_CodigoEAN 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ FarmaciaProductos_CodigoEAN ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_FarmaciaProductos_CodigoEAN_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_FarmaciaProductos_CodigoEAN_Lotes 
     On FarmaciaProductos_CodigoEAN_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ FarmaciaProductos_CodigoEAN_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
     On FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Movtos_Inv_Tipos_Farmacia' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Movtos_Inv_Tipos_Farmacia 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Movtos_Inv_Tipos_Farmacia' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Movtos_Inv_Tipos_Farmacia 
     On Movtos_Inv_Tipos_Farmacia 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Movtos_Inv_Tipos_Farmacia ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_MovtosInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_MovtosInv_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_MovtosInv_Enc 
     On MovtosInv_Enc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ MovtosInv_Enc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_MovtosInv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_MovtosInv_Det_CodigosEAN 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_MovtosInv_Det_CodigosEAN 
     On MovtosInv_Det_CodigosEAN 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ MovtosInv_Det_CodigosEAN ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_MovtosInv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_MovtosInv_Det_CodigosEAN_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_MovtosInv_Det_CodigosEAN_Lotes 
     On MovtosInv_Det_CodigosEAN_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ MovtosInv_Det_CodigosEAN_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
     On MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_EntradasEnc_Consignacion' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_EntradasEnc_Consignacion 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasEnc_Consignacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_EntradasEnc_Consignacion 
     On EntradasEnc_Consignacion 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ EntradasEnc_Consignacion ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_EntradasDet_Consignacion' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_EntradasDet_Consignacion 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_EntradasDet_Consignacion 
     On EntradasDet_Consignacion 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ EntradasDet_Consignacion ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_EntradasDet_Consignacion_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_EntradasDet_Consignacion_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_EntradasDet_Consignacion_Lotes 
     On EntradasDet_Consignacion_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ EntradasDet_Consignacion_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_EntradasDet_Consignacion_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_EntradasDet_Consignacion_Lotes_Ubicaciones 
     On EntradasDet_Consignacion_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ EntradasDet_Consignacion_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_AjustesInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_AjustesInv_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_AjustesInv_Enc 
     On AjustesInv_Enc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ AjustesInv_Enc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_AjustesInv_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_AjustesInv_Det 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_AjustesInv_Det 
     On AjustesInv_Det 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ AjustesInv_Det ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_AjustesInv_Det_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_AjustesInv_Det_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_AjustesInv_Det_Lotes 
     On AjustesInv_Det_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ AjustesInv_Det_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_AjustesInv_Det_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_AjustesInv_Det_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_AjustesInv_Det_Lotes_Ubicaciones 
     On AjustesInv_Det_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ AjustesInv_Det_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CambiosProv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CambiosProv_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CambiosProv_Enc 
     On CambiosProv_Enc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CambiosProv_Enc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CambiosProv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CambiosProv_Det_CodigosEAN 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CambiosProv_Det_CodigosEAN 
     On CambiosProv_Det_CodigosEAN 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CambiosProv_Det_CodigosEAN ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CambiosProv_Det_CodigosEAN_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CambiosProv_Det_CodigosEAN_Lotes 
     On CambiosProv_Det_CodigosEAN_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CambiosProv_Det_CodigosEAN_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CatMedicos' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CatMedicos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CatMedicos 
     On CatMedicos 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CatMedicos ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_CatBeneficiarios' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_CatBeneficiarios 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_CatBeneficiarios 
     On CatBeneficiarios 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ CatBeneficiarios ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_VentasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_VentasEnc 
     On VentasEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ VentasEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_VentasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_VentasDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_VentasDet 
     On VentasDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ VentasDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_VentasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_VentasDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_VentasDet_Lotes 
     On VentasDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ VentasDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_VentasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_VentasDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_VentasDet_Lotes_Ubicaciones 
     On VentasDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ VentasDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_VentasEstDispensacion' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_VentasEstDispensacion 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstDispensacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_VentasEstDispensacion 
     On VentasEstDispensacion 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ VentasEstDispensacion ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_VentasInformacionAdicional 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasInformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_VentasInformacionAdicional 
     On VentasInformacionAdicional 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ VentasInformacionAdicional ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_VentasEstadisticaClavesDispensadas' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_VentasEstadisticaClavesDispensadas 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstadisticaClavesDispensadas' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_VentasEstadisticaClavesDispensadas 
     On VentasEstadisticaClavesDispensadas 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ VentasEstadisticaClavesDispensadas ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Adt_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Adt_VentasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Adt_VentasEnc 
     On Adt_VentasEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Adt_VentasEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Adt_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Adt_VentasInformacionAdicional 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasInformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Adt_VentasInformacionAdicional 
     On Adt_VentasInformacionAdicional 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Adt_VentasInformacionAdicional ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Adt_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Adt_ComprasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_ComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Adt_ComprasEnc 
     On Adt_ComprasEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Adt_ComprasEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Adt_FarmaciaProductos_CodigoEAN_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Adt_FarmaciaProductos_CodigoEAN_Lotes 
     On Adt_FarmaciaProductos_CodigoEAN_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Adt_FarmaciaProductos_CodigoEAN_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Vales_EmisionEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Vales_EmisionEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Vales_EmisionEnc 
     On Vales_EmisionEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Vales_EmisionEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Vales_EmisionDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Vales_EmisionDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Vales_EmisionDet 
     On Vales_EmisionDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Vales_EmisionDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Vales_Emision_InformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Vales_Emision_InformacionAdicional 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_InformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Vales_Emision_InformacionAdicional 
     On Vales_Emision_InformacionAdicional 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Vales_Emision_InformacionAdicional ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Vales_Cancelacion' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Vales_Cancelacion 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Cancelacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Vales_Cancelacion 
     On Vales_Cancelacion 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Vales_Cancelacion ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ValesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ValesEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ValesEnc 
     On ValesEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ValesEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ValesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ValesDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ValesDet 
     On ValesDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ValesDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ValesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ValesDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ValesDet_Lotes 
     On ValesDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ValesDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_RemisionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_RemisionesEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_RemisionesEnc 
     On RemisionesEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ RemisionesEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_RemisionesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_RemisionesDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_RemisionesDet 
     On RemisionesDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ RemisionesDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_RemisionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_RemisionesDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_RemisionesDet_Lotes 
     On RemisionesDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ RemisionesDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ComprasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ComprasEnc 
     On ComprasEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ComprasEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ComprasDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ComprasDet 
     On ComprasDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ComprasDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ComprasDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ComprasDet_Lotes 
     On ComprasDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ComprasDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ComprasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ComprasDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ComprasDet_Lotes_Ubicaciones 
     On ComprasDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ComprasDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_COM_OCEN_OrdenesCompra_Claves_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_COM_OCEN_OrdenesCompra_Claves_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_COM_OCEN_OrdenesCompra_Claves_Enc 
     On COM_OCEN_OrdenesCompra_Claves_Enc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ COM_OCEN_OrdenesCompra_Claves_Enc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_OrdenesDeComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_OrdenesDeComprasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_OrdenesDeComprasEnc 
     On OrdenesDeComprasEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ OrdenesDeComprasEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_OrdenesDeComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_OrdenesDeComprasDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_OrdenesDeComprasDet 
     On OrdenesDeComprasDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ OrdenesDeComprasDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_OrdenesDeComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_OrdenesDeComprasDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_OrdenesDeComprasDet_Lotes 
     On OrdenesDeComprasDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ OrdenesDeComprasDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_OrdenesDeComprasDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_OrdenesDeComprasDet_Lotes_Ubicaciones 
     On OrdenesDeComprasDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ OrdenesDeComprasDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TransferenciasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TransferenciasEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TransferenciasEnc 
     On TransferenciasEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TransferenciasEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TransferenciasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TransferenciasDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TransferenciasDet 
     On TransferenciasDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TransferenciasDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TransferenciasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TransferenciasDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TransferenciasDet_Lotes 
     On TransferenciasDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TransferenciasDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TransferenciasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TransferenciasDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TransferenciasDet_Lotes_Ubicaciones 
     On TransferenciasDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TransferenciasDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TraspasosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TraspasosEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TraspasosEnc 
     On TraspasosEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TraspasosEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TraspasosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TraspasosDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TraspasosDet 
     On TraspasosDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TraspasosDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TraspasosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TraspasosDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TraspasosDet_Lotes 
     On TraspasosDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TraspasosDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_TraspasosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_TraspasosDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_TraspasosDet_Lotes_Ubicaciones 
     On TraspasosDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ TraspasosDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_DevolucionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_DevolucionesEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_DevolucionesEnc 
     On DevolucionesEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ DevolucionesEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_DevolucionesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_DevolucionesDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_DevolucionesDet 
     On DevolucionesDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ DevolucionesDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_DevolucionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_DevolucionesDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_DevolucionesDet_Lotes 
     On DevolucionesDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ DevolucionesDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_DevolucionesDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_DevolucionesDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_DevolucionesDet_Lotes_Ubicaciones 
     On DevolucionesDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ DevolucionesDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PreSalidasPedidosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PreSalidasPedidosEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PreSalidasPedidosEnc 
     On PreSalidasPedidosEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PreSalidasPedidosEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PreSalidasPedidosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PreSalidasPedidosDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PreSalidasPedidosDet 
     On PreSalidasPedidosDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PreSalidasPedidosDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PreSalidasPedidosDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PreSalidasPedidosDet_Lotes_Ubicaciones 
     On PreSalidasPedidosDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PreSalidasPedidosDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosEnc 
     On PedidosEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosDet 
     On PedidosDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosDet_Lotes 
     On PedidosDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosDet_Lotes_Ubicaciones 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosDet_Lotes_Ubicaciones 
     On PedidosDet_Lotes_Ubicaciones 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosDet_Lotes_Ubicaciones ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosEnvioEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosEnvioEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosEnvioEnc 
     On PedidosEnvioEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosEnvioEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosEnvioDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosEnvioDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosEnvioDet 
     On PedidosEnvioDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosEnvioDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosEnvioDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosEnvioDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosEnvioDet_Lotes 
     On PedidosEnvioDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosEnvioDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosEnvioDet_Lotes_Registrar' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosEnvioDet_Lotes_Registrar 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes_Registrar' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosEnvioDet_Lotes_Registrar 
     On PedidosEnvioDet_Lotes_Registrar 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosEnvioDet_Lotes_Registrar ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosDistEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosDistEnc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosDistEnc 
     On PedidosDistEnc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosDistEnc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosDistDet' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosDistDet 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosDistDet 
     On PedidosDistDet 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosDistDet ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_PedidosDistDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_PedidosDistDet_Lotes 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_PedidosDistDet_Lotes 
     On PedidosDistDet_Lotes 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ PedidosDistDet_Lotes ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Pedidos_Cedis_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Pedidos_Cedis_Enc 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Pedidos_Cedis_Enc 
     On Pedidos_Cedis_Enc 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Pedidos_Cedis_Enc ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Pedidos_Cedis_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Pedidos_Cedis_Det 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Pedidos_Cedis_Det 
     On Pedidos_Cedis_Det 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Pedidos_Cedis_Det ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Pedidos_Cedis_Det_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Pedidos_Cedis_Det_Surtido 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Pedidos_Cedis_Det_Surtido 
     On Pedidos_Cedis_Det_Surtido 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Pedidos_Cedis_Det_Surtido ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Pedidos_Cedis_Det_Pedido_Distribuidor' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Pedidos_Cedis_Det_Pedido_Distribuidor 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Pedido_Distribuidor' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Pedidos_Cedis_Det_Pedido_Distribuidor 
     On Pedidos_Cedis_Det_Pedido_Distribuidor 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Pedidos_Cedis_Det_Pedido_Distribuidor ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ALMJ_Pedidos_RC' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ALMJ_Pedidos_RC 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ALMJ_Pedidos_RC 
     On ALMJ_Pedidos_RC 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ALMJ_Pedidos_RC ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ALMJ_Pedidos_RC_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ALMJ_Pedidos_RC_Det 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ALMJ_Pedidos_RC_Det 
     On ALMJ_Pedidos_RC_Det 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ALMJ_Pedidos_RC_Det ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ALMJ_Concentrado_PedidosRC' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ALMJ_Concentrado_PedidosRC 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ALMJ_Concentrado_PedidosRC 
     On ALMJ_Concentrado_PedidosRC 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ALMJ_Concentrado_PedidosRC ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ALMJ_Concentrado_PedidosRC_Claves' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ALMJ_Concentrado_PedidosRC_Claves 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Claves' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ALMJ_Concentrado_PedidosRC_Claves 
     On ALMJ_Concentrado_PedidosRC_Claves 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ALMJ_Concentrado_PedidosRC_Claves ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_ALMJ_Concentrado_PedidosRC_Pedidos 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_ALMJ_Concentrado_PedidosRC_Pedidos 
     On ALMJ_Concentrado_PedidosRC_Pedidos 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ ALMJ_Concentrado_PedidosRC_Pedidos ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Ventas_ALMJ_PedidosRC_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Ventas_ALMJ_PedidosRC_Surtido 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_ALMJ_PedidosRC_Surtido' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Ventas_ALMJ_PedidosRC_Surtido 
     On Ventas_ALMJ_PedidosRC_Surtido 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Ventas_ALMJ_PedidosRC_Surtido ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnDelete_Ventas_TiempoAire' and xType = 'TR' ) 
   Drop Trigger TR_OnDelete_Ventas_TiempoAire 
Go--#S#QL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_TiempoAire' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnDelete_Ventas_TiempoAire 
     On Ventas_TiempoAire 
     With Encryption 
     For Delete  
     As 
     Begin 
        --- Deshacer la eliminacion de datos  
        Rollback 
              --- Enviar el mensaje de error 
        Raiserror (''Esta acción no esta permitida para el objeto [ Ventas_TiempoAire ]'', 1, 1) 
     End 
     '
Exec ( @sSql ) 
End 
Go--#S#QL 
 
