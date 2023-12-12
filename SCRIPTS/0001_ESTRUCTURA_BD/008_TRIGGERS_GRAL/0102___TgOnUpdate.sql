-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Net_CFGC_Parametros' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Net_CFGC_Parametros 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Parametros' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Net_CFGC_Parametros 
     On Net_CFGC_Parametros 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Net_CFGC_Parametros U 
                Inner Join Inserted I 
             		On ( I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.ArbolModulo = U.ArbolModulo and I.NombreParametro = U.NombreParametro  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CtlCortesParciales' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CtlCortesParciales 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesParciales' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CtlCortesParciales 
     On CtlCortesParciales 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CtlCortesParciales U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdPersonal = U.IdPersonal and I.IdCorte = U.IdCorte and I.FechaSistema = U.FechaSistema  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CtlCortesDiarios' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CtlCortesDiarios 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesDiarios' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CtlCortesDiarios 
     On CtlCortesDiarios 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CtlCortesDiarios U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdPersonal = U.IdPersonal and I.IdCorte = U.IdCorte and I.FechaSistema = U.FechaSistema  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ctl_CierresDePeriodos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ctl_CierresDePeriodos 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresDePeriodos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Ctl_CierresDePeriodos 
     On Ctl_CierresDePeriodos 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Ctl_CierresDePeriodos U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCierre = U.FolioCierre  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ctl_CierresPeriodosDetalles' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ctl_CierresPeriodosDetalles 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresPeriodosDetalles' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Ctl_CierresPeriodosDetalles 
     On Ctl_CierresPeriodosDetalles 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Ctl_CierresPeriodosDetalles U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioCierre = U.FolioCierre and I.IdCliente = U.IdCliente and I.IdSubCliente = U.IdSubCliente and I.IdPrograma = U.IdPrograma and I.IdSubPrograma = U.IdSubPrograma and I.AñoRegistro = U.AñoRegistro and I.MesRegistro = U.MesRegistro and I.AñoReceta = U.AñoReceta and I.MesReceta = U.MesReceta and I.TipoInventario = U.TipoInventario  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatPasillos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatPasillos 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CatPasillos 
     On CatPasillos 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CatPasillos U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdPasillo = U.IdPasillo  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatPasillos_Estantes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatPasillos_Estantes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CatPasillos_Estantes 
     On CatPasillos_Estantes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CatPasillos_Estantes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatPasillos_Estantes_Entrepaños' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatPasillos_Estantes_Entrepaños 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepaños' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CatPasillos_Estantes_Entrepaños 
     On CatPasillos_Estantes_Entrepaños 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CatPasillos_Estantes_Entrepaños U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_FarmaciaProductos 
     On FarmaciaProductos 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From FarmaciaProductos U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdProducto = U.IdProducto  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos_CodigoEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN 
     On FarmaciaProductos_CodigoEAN 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From FarmaciaProductos_CodigoEAN U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes 
     On FarmaciaProductos_CodigoEAN_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From FarmaciaProductos_CodigoEAN_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
     On FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Movtos_Inv_Tipos_Farmacia' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Movtos_Inv_Tipos_Farmacia 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Movtos_Inv_Tipos_Farmacia' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Movtos_Inv_Tipos_Farmacia 
     On Movtos_Inv_Tipos_Farmacia 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Movtos_Inv_Tipos_Farmacia U 
                Inner Join Inserted I 
             		On ( I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdTipoMovto_Inv = U.IdTipoMovto_Inv  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Enc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_MovtosInv_Enc 
     On MovtosInv_Enc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From MovtosInv_Enc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioMovtoInv = U.FolioMovtoInv  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN 
     On MovtosInv_Det_CodigosEAN 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From MovtosInv_Det_CodigosEAN U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioMovtoInv = U.FolioMovtoInv and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes 
     On MovtosInv_Det_CodigosEAN_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From MovtosInv_Det_CodigosEAN_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioMovtoInv = U.FolioMovtoInv and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
     On MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioMovtoInv = U.FolioMovtoInv and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasEnc_Consignacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasEnc_Consignacion 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasEnc_Consignacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_EntradasEnc_Consignacion 
     On EntradasEnc_Consignacion 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From EntradasEnc_Consignacion U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioEntrada = U.FolioEntrada  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasDet_Consignacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasDet_Consignacion 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_EntradasDet_Consignacion 
     On EntradasDet_Consignacion 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From EntradasDet_Consignacion U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioEntrada = U.FolioEntrada and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasDet_Consignacion_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasDet_Consignacion_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_EntradasDet_Consignacion_Lotes 
     On EntradasDet_Consignacion_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From EntradasDet_Consignacion_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioEntrada = U.FolioEntrada and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_EntradasDet_Consignacion_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_EntradasDet_Consignacion_Lotes_Ubicaciones 
     On EntradasDet_Consignacion_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From EntradasDet_Consignacion_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioEntrada = U.FolioEntrada and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Enc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_AjustesInv_Enc 
     On AjustesInv_Enc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From AjustesInv_Enc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.Poliza = U.Poliza  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Det 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_AjustesInv_Det 
     On AjustesInv_Det 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From AjustesInv_Det U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.Poliza = U.Poliza and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Det_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Det_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_AjustesInv_Det_Lotes 
     On AjustesInv_Det_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From AjustesInv_Det_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.Poliza = U.Poliza and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_AjustesInv_Det_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_AjustesInv_Det_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_AjustesInv_Det_Lotes_Ubicaciones 
     On AjustesInv_Det_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From AjustesInv_Det_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.Poliza = U.Poliza and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CambiosProv_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CambiosProv_Enc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CambiosProv_Enc 
     On CambiosProv_Enc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CambiosProv_Enc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCambio = U.FolioCambio  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CambiosProv_Det_CodigosEAN' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CambiosProv_Det_CodigosEAN 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CambiosProv_Det_CodigosEAN 
     On CambiosProv_Det_CodigosEAN 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CambiosProv_Det_CodigosEAN U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCambio = U.FolioCambio and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CambiosProv_Det_CodigosEAN_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CambiosProv_Det_CodigosEAN_Lotes 
     On CambiosProv_Det_CodigosEAN_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CambiosProv_Det_CodigosEAN_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCambio = U.FolioCambio and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.IdSubFarmacia = U.IdSubFarmacia and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatMedicos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatMedicos 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CatMedicos 
     On CatMedicos 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CatMedicos U 
                Inner Join Inserted I 
             		On ( I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdMedico = U.IdMedico  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_CatBeneficiarios' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_CatBeneficiarios 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_CatBeneficiarios 
     On CatBeneficiarios 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From CatBeneficiarios U 
                Inner Join Inserted I 
             		On ( I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdCliente = U.IdCliente and I.IdSubCliente = U.IdSubCliente and I.IdBeneficiario = U.IdBeneficiario  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_VentasEnc 
     On VentasEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From VentasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_VentasDet 
     On VentasDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From VentasDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_VentasDet_Lotes 
     On VentasDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From VentasDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioVenta = U.FolioVenta and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_VentasDet_Lotes_Ubicaciones 
     On VentasDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From VentasDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioVenta = U.FolioVenta and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasEstDispensacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasEstDispensacion 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstDispensacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_VentasEstDispensacion 
     On VentasEstDispensacion 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From VentasEstDispensacion U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasInformacionAdicional 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasInformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_VentasInformacionAdicional 
     On VentasInformacionAdicional 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From VentasInformacionAdicional U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_VentasEstadisticaClavesDispensadas' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_VentasEstadisticaClavesDispensadas 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstadisticaClavesDispensadas' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_VentasEstadisticaClavesDispensadas 
     On VentasEstadisticaClavesDispensadas 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From VentasEstadisticaClavesDispensadas U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta and I.IdClaveSSA = U.IdClaveSSA  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_VentasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_VentasEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Adt_VentasEnc 
     On Adt_VentasEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Adt_VentasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta and I.FolioMovto = U.FolioMovto  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_VentasInformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_VentasInformacionAdicional 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasInformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Adt_VentasInformacionAdicional 
     On Adt_VentasInformacionAdicional 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Adt_VentasInformacionAdicional U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta and I.FolioMovto = U.FolioMovto  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_ComprasEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_ComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Adt_ComprasEnc 
     On Adt_ComprasEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Adt_ComprasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCompra = U.FolioCompra and I.FolioMovto = U.FolioMovto  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Adt_FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Adt_FarmaciaProductos_CodigoEAN_Lotes 
     On Adt_FarmaciaProductos_CodigoEAN_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Adt_FarmaciaProductos_CodigoEAN_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.FolioMovto = U.FolioMovto  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_EmisionEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_EmisionEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Vales_EmisionEnc 
     On Vales_EmisionEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Vales_EmisionEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVale = U.FolioVale  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_EmisionDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_EmisionDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Vales_EmisionDet 
     On Vales_EmisionDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Vales_EmisionDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVale = U.FolioVale and I.IdClaveSSA_Sal = U.IdClaveSSA_Sal  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_Emision_InformacionAdicional' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_Emision_InformacionAdicional 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_InformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Vales_Emision_InformacionAdicional 
     On Vales_Emision_InformacionAdicional 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Vales_Emision_InformacionAdicional U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVale = U.FolioVale  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Vales_Cancelacion' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Vales_Cancelacion 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Cancelacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Vales_Cancelacion 
     On Vales_Cancelacion 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Vales_Cancelacion U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCancelacionVale = U.FolioCancelacionVale  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ValesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ValesEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ValesEnc 
     On ValesEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ValesEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.Folio = U.Folio  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ValesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ValesDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ValesDet 
     On ValesDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ValesDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.Folio = U.Folio and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ValesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ValesDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ValesDet_Lotes 
     On ValesDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ValesDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.Folio = U.Folio and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_RemisionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_RemisionesEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_RemisionesEnc 
     On RemisionesEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From RemisionesEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioRemision = U.FolioRemision  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_RemisionesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_RemisionesDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_RemisionesDet 
     On RemisionesDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From RemisionesDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioRemision = U.FolioRemision and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_RemisionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_RemisionesDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_RemisionesDet_Lotes 
     On RemisionesDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From RemisionesDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioRemision = U.FolioRemision and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ComprasEnc 
     On ComprasEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ComprasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCompra = U.FolioCompra  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ComprasDet 
     On ComprasDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ComprasDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioCompra = U.FolioCompra and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ComprasDet_Lotes 
     On ComprasDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ComprasDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioCompra = U.FolioCompra and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ComprasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ComprasDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ComprasDet_Lotes_Ubicaciones 
     On ComprasDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ComprasDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioCompra = U.FolioCompra and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_COM_OCEN_OrdenesCompra_Claves_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_COM_OCEN_OrdenesCompra_Claves_Enc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_COM_OCEN_OrdenesCompra_Claves_Enc 
     On COM_OCEN_OrdenesCompra_Claves_Enc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From COM_OCEN_OrdenesCompra_Claves_Enc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioOrden = U.FolioOrden  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_OrdenesDeComprasEnc 
     On OrdenesDeComprasEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From OrdenesDeComprasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioOrdenCompra = U.FolioOrdenCompra  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_OrdenesDeComprasDet 
     On OrdenesDeComprasDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From OrdenesDeComprasDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioOrdenCompra = U.FolioOrdenCompra and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_OrdenesDeComprasDet_Lotes 
     On OrdenesDeComprasDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From OrdenesDeComprasDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioOrdenCompra = U.FolioOrdenCompra and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_OrdenesDeComprasDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_OrdenesDeComprasDet_Lotes_Ubicaciones 
     On OrdenesDeComprasDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From OrdenesDeComprasDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioOrdenCompra = U.FolioOrdenCompra and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TransferenciasEnc 
     On TransferenciasEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TransferenciasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioTransferencia = U.FolioTransferencia  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TransferenciasDet 
     On TransferenciasDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TransferenciasDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioTransferencia = U.FolioTransferencia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TransferenciasDet_Lotes 
     On TransferenciasDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TransferenciasDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmaciaEnvia = U.IdSubFarmaciaEnvia and I.FolioTransferencia = U.FolioTransferencia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TransferenciasDet_Lotes_Ubicaciones 
     On TransferenciasDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TransferenciasDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmaciaEnvia = U.IdSubFarmaciaEnvia and I.FolioTransferencia = U.FolioTransferencia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TraspasosEnc 
     On TraspasosEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TraspasosEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioTraspaso = U.FolioTraspaso  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TraspasosDet 
     On TraspasosDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TraspasosDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioTraspaso = U.FolioTraspaso and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TraspasosDet_Lotes 
     On TraspasosDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TraspasosDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioTraspaso = U.FolioTraspaso and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TraspasosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TraspasosDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_TraspasosDet_Lotes_Ubicaciones 
     On TraspasosDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From TraspasosDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioTraspaso = U.FolioTraspaso and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_DevolucionesEnc 
     On DevolucionesEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From DevolucionesEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioDevolucion = U.FolioDevolucion  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_DevolucionesDet 
     On DevolucionesDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From DevolucionesDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioDevolucion = U.FolioDevolucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_DevolucionesDet_Lotes 
     On DevolucionesDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From DevolucionesDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioDevolucion = U.FolioDevolucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_DevolucionesDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_DevolucionesDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_DevolucionesDet_Lotes_Ubicaciones 
     On DevolucionesDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From DevolucionesDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioDevolucion = U.FolioDevolucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PreSalidasPedidosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PreSalidasPedidosEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PreSalidasPedidosEnc 
     On PreSalidasPedidosEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PreSalidasPedidosEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPreSalida = U.FolioPreSalida  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PreSalidasPedidosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PreSalidasPedidosDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PreSalidasPedidosDet 
     On PreSalidasPedidosDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PreSalidasPedidosDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPreSalida = U.FolioPreSalida and I.IdClaveSSA = U.IdClaveSSA  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PreSalidasPedidosDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PreSalidasPedidosDet_Lotes_Ubicaciones 
     On PreSalidasPedidosDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PreSalidasPedidosDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPreSalida = U.FolioPreSalida and I.IdClaveSSA = U.IdClaveSSA and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño and I.IdSubFarmacia = U.IdSubFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosEnc 
     On PedidosEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedido = U.FolioPedido  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosDet 
     On PedidosDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosDet_Lotes 
     On PedidosDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDet_Lotes_Ubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosDet_Lotes_Ubicaciones 
     On PedidosDet_Lotes_Ubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosEnvioEnc 
     On PedidosEnvioEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosEnvioEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedido = U.FolioPedido  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosEnvioDet 
     On PedidosEnvioDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosEnvioDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosEnvioDet_Lotes 
     On PedidosEnvioDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosEnvioDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosEnvioDet_Lotes_Registrar' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosEnvioDet_Lotes_Registrar 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes_Registrar' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosEnvioDet_Lotes_Registrar 
     On PedidosEnvioDet_Lotes_Registrar 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosEnvioDet_Lotes_Registrar U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDistEnc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDistEnc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosDistEnc 
     On PedidosDistEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosDistEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioDistribucion = U.FolioDistribucion  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDistDet' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDistDet 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosDistDet 
     On PedidosDistDet 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosDistDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioDistribucion = U.FolioDistribucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_PedidosDistDet_Lotes' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_PedidosDistDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_PedidosDistDet_Lotes 
     On PedidosDistDet_Lotes 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From PedidosDistDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmaciaEnvia = U.IdSubFarmaciaEnvia and I.FolioDistribucion = U.FolioDistribucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Enc' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Enc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Pedidos_Cedis_Enc 
     On Pedidos_Cedis_Enc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Pedidos_Cedis_Enc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedido = U.FolioPedido  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Pedidos_Cedis_Det 
     On Pedidos_Cedis_Det 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Pedidos_Cedis_Det U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedido = U.FolioPedido and I.IdClaveSSA = U.IdClaveSSA  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det_Surtido 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Pedidos_Cedis_Det_Surtido 
     On Pedidos_Cedis_Det_Surtido 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Pedidos_Cedis_Det_Surtido U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioSurtido = U.FolioSurtido and I.IdClaveSSA = U.IdClaveSSA  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Pedidos_Cedis_Det_Pedido_Distribuidor' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Pedidos_Cedis_Det_Pedido_Distribuidor 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Pedido_Distribuidor' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Pedidos_Cedis_Det_Pedido_Distribuidor 
     On Pedidos_Cedis_Det_Pedido_Distribuidor 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Pedidos_Cedis_Det_Pedido_Distribuidor U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Pedidos_RC' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Pedidos_RC 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ALMJ_Pedidos_RC 
     On ALMJ_Pedidos_RC 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ALMJ_Pedidos_RC U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedidoRC = U.FolioPedidoRC  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Pedidos_RC_Det' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Pedidos_RC_Det 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ALMJ_Pedidos_RC_Det 
     On ALMJ_Pedidos_RC_Det 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ALMJ_Pedidos_RC_Det U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioPedidoRC = U.FolioPedidoRC and I.IdClaveSSA = U.IdClaveSSA  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Concentrado_PedidosRC' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC 
     On ALMJ_Concentrado_PedidosRC 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ALMJ_Concentrado_PedidosRC U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioConcentrado = U.FolioConcentrado  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Claves' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Claves 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Claves' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Claves 
     On ALMJ_Concentrado_PedidosRC_Claves 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ALMJ_Concentrado_PedidosRC_Claves U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioConcentrado = U.FolioConcentrado and I.IdClaveSSA = U.IdClaveSSA  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Pedidos 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_ALMJ_Concentrado_PedidosRC_Pedidos 
     On ALMJ_Concentrado_PedidosRC_Pedidos 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From ALMJ_Concentrado_PedidosRC_Pedidos U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioConcentrado = U.FolioConcentrado and I.IdEmpresaPed = U.IdEmpresaPed and I.IdEstadoPed = U.IdEstadoPed and I.IdFarmaciaPed = U.IdFarmaciaPed and I.FolioPedidoRCPed = U.FolioPedidoRCPed  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ventas_ALMJ_PedidosRC_Surtido' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ventas_ALMJ_PedidosRC_Surtido 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_ALMJ_PedidosRC_Surtido' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Ventas_ALMJ_PedidosRC_Surtido 
     On Ventas_ALMJ_PedidosRC_Surtido 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Ventas_ALMJ_PedidosRC_Surtido U 
                Inner Join Inserted I 
             		On ( I.IdEmpresaRC = U.IdEmpresaRC and I.IdEstadoRC = U.IdEstadoRC and I.IdFarmaciaRC = U.IdFarmaciaRC and I.FolioPedidoRC = U.FolioPedidoRC and I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioVenta = U.FolioVenta  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ventas_TiempoAire' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ventas_TiempoAire 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_TiempoAire' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Ventas_TiempoAire 
     On Ventas_TiempoAire 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Ventas_TiempoAire U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdCompania = U.IdCompania and I.IdFolioTiempoAire = U.IdFolioTiempoAire  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
