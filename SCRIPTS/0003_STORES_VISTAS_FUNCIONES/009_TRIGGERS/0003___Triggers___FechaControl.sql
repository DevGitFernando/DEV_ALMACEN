----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Net_CFGC_Parametros__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Net_CFGC_Parametros__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Parametros' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Net_CFGC_Parametros__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CtlCortesParciales__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CtlCortesParciales__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesParciales' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CtlCortesParciales__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CtlCortesDiarios__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CtlCortesDiarios__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CtlCortesDiarios' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CtlCortesDiarios__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Ctl_CierresDePeriodos__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Ctl_CierresDePeriodos__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresDePeriodos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Ctl_CierresDePeriodos__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Ctl_CierresPeriodosDetalles__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Ctl_CierresPeriodosDetalles__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_CierresPeriodosDetalles' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Ctl_CierresPeriodosDetalles__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CatPasillos__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CatPasillos__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CatPasillos__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CatPasillos_Estantes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CatPasillos_Estantes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CatPasillos_Estantes__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CatPasillos_Estantes_Entrepaños__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CatPasillos_Estantes_Entrepaños__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepaños' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CatPasillos_Estantes_Entrepaños__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__FarmaciaProductos__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__FarmaciaProductos__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__FarmaciaProductos__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__FarmaciaProductos_CodigoEAN__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes__Historico__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes__Historico__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes__Historico' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__FarmaciaProductos_CodigoEAN_Lotes__Historico__FechaControl 
     On FarmaciaProductos_CodigoEAN_Lotes__Historico 
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
                From FarmaciaProductos_CodigoEAN_Lotes__Historico U 
                Inner Join Inserted I 
             		On ( I.FechaOperacion = U.FechaOperacion and I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__MovtosInv_Enc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__MovtosInv_Enc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__MovtosInv_Enc__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__MovtosInv_Det_CodigosEAN__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__MovtosInv_Det_CodigosEAN__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__MovtosInv_Det_CodigosEAN__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__MovtosInv_Det_CodigosEAN_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__MovtosInv_Det_CodigosEAN_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__MovtosInv_Det_CodigosEAN_Lotes__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__EntradasEnc_Consignacion__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__EntradasEnc_Consignacion__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasEnc_Consignacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__EntradasEnc_Consignacion__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__EntradasDet_Consignacion__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__EntradasDet_Consignacion__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__EntradasDet_Consignacion__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__EntradasDet_Consignacion_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__EntradasDet_Consignacion_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__EntradasDet_Consignacion_Lotes__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__EntradasDet_Consignacion_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__EntradasDet_Consignacion_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__EntradasDet_Consignacion_Lotes_Ubicaciones__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__AjustesInv_Enc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__AjustesInv_Enc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__AjustesInv_Enc__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__AjustesInv_Det__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__AjustesInv_Det__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__AjustesInv_Det__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__AjustesInv_Det_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__AjustesInv_Det_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__AjustesInv_Det_Lotes__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__AjustesInv_Det_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__AjustesInv_Det_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__AjustesInv_Det_Lotes_Ubicaciones__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CambiosProv_Enc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CambiosProv_Enc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CambiosProv_Enc__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CambiosProv_Det_CodigosEAN__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CambiosProv_Det_CodigosEAN__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CambiosProv_Det_CodigosEAN__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CambiosProv_Det_CodigosEAN_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CambiosProv_Det_CodigosEAN_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CambiosProv_Det_CodigosEAN_Lotes__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CatMedicos__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CatMedicos__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CatMedicos__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CatBeneficiarios__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CatBeneficiarios__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CatBeneficiarios__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__VentasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__VentasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__VentasEnc__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__VentasDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__VentasDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__VentasDet__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__VentasDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__VentasDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__VentasDet_Lotes__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__VentasDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__VentasDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__VentasDet_Lotes_Ubicaciones__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__VentasEstDispensacion__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__VentasEstDispensacion__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstDispensacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__VentasEstDispensacion__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__VentasInformacionAdicional__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__VentasInformacionAdicional__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasInformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__VentasInformacionAdicional__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__VentasEstadisticaClavesDispensadas__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__VentasEstadisticaClavesDispensadas__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstadisticaClavesDispensadas' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__VentasEstadisticaClavesDispensadas__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Vales_EmisionEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Vales_EmisionEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Vales_EmisionEnc__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Vales_EmisionDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Vales_EmisionDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Vales_EmisionDet__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Vales_Emision_InformacionAdicional__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Vales_Emision_InformacionAdicional__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_InformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Vales_Emision_InformacionAdicional__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Vales_Cancelacion__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Vales_Cancelacion__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Cancelacion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Vales_Cancelacion__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ValesEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ValesEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ValesEnc__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ValesDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ValesDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ValesDet__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ValesDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ValesDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ValesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ValesDet_Lotes__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__RemisionesEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__RemisionesEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__RemisionesEnc__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__RemisionesDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__RemisionesDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__RemisionesDet__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__RemisionesDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__RemisionesDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__RemisionesDet_Lotes__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ComprasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ComprasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ComprasEnc__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ComprasDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ComprasDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ComprasDet__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ComprasDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ComprasDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ComprasDet_Lotes__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ComprasDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ComprasDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ComprasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ComprasDet_Lotes_Ubicaciones__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__OrdenesDeComprasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__OrdenesDeComprasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__OrdenesDeComprasEnc__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__OrdenesDeComprasDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__OrdenesDeComprasDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__OrdenesDeComprasDet__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__OrdenesDeComprasDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__OrdenesDeComprasDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__OrdenesDeComprasDet_Lotes__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__OrdenesDeComprasDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__OrdenesDeComprasDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__OrdenesDeComprasDet_Lotes_Ubicaciones__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionTransferenciasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionTransferenciasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionTransferenciasEnc__FechaControl 
     On DevolucionTransferenciasEnc 
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
                From DevolucionTransferenciasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioDevolucion = U.FolioDevolucion  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionTransferenciasDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionTransferenciasDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionTransferenciasDet__FechaControl 
     On DevolucionTransferenciasDet 
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
                From DevolucionTransferenciasDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioDevolucion = U.FolioDevolucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionTransferenciasDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionTransferenciasDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionTransferenciasDet_Lotes__FechaControl 
     On DevolucionTransferenciasDet_Lotes 
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
                From DevolucionTransferenciasDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioDevolucion = U.FolioDevolucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.IdSubFarmacia = U.IdSubFarmacia and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionTransferenciasDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionTransferenciasDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionTransferenciasDet_Lotes_Ubicaciones__FechaControl 
     On DevolucionTransferenciasDet_Lotes_Ubicaciones 
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
                From DevolucionTransferenciasDet_Lotes_Ubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioDevolucion = U.FolioDevolucion and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasEnc__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasDet__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasDet_Lotes__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasDet_Lotes_Ubicaciones__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasEstadisticaClavesDispensadas__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasEstadisticaClavesDispensadas__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEstadisticaClavesDispensadas' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasEstadisticaClavesDispensadas__FechaControl 
     On TransferenciasEstadisticaClavesDispensadas 
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
                From TransferenciasEstadisticaClavesDispensadas U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioTransferencia = U.FolioTransferencia and I.IdClaveSSA = U.IdClaveSSA  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasEnvioEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasEnvioEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasEnvioEnc__FechaControl 
     On TransferenciasEnvioEnc 
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
                From TransferenciasEnvioEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstadoEnvia = U.IdEstadoEnvia and I.IdFarmaciaEnvia = U.IdFarmaciaEnvia and I.FolioTransferencia = U.FolioTransferencia  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasEnvioDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasEnvioDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasEnvioDet__FechaControl 
     On TransferenciasEnvioDet 
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
                From TransferenciasEnvioDet U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstadoEnvia = U.IdEstadoEnvia and I.IdFarmaciaEnvia = U.IdFarmaciaEnvia and I.FolioTransferencia = U.FolioTransferencia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasEnvioDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasEnvioDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasEnvioDet_Lotes__FechaControl 
     On TransferenciasEnvioDet_Lotes 
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
                From TransferenciasEnvioDet_Lotes U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstadoEnvia = U.IdEstadoEnvia and I.IdFarmaciaEnvia = U.IdFarmaciaEnvia and I.IdSubFarmaciaEnvia = U.IdSubFarmaciaEnvia and I.FolioTransferencia = U.FolioTransferencia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TransferenciasEnvioDet_LotesRegistrar__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TransferenciasEnvioDet_LotesRegistrar__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_LotesRegistrar' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TransferenciasEnvioDet_LotesRegistrar__FechaControl 
     On TransferenciasEnvioDet_LotesRegistrar 
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
                From TransferenciasEnvioDet_LotesRegistrar U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstadoEnvia = U.IdEstadoEnvia and I.IdFarmaciaEnvia = U.IdFarmaciaEnvia and I.IdSubFarmaciaEnvia = U.IdSubFarmaciaEnvia and I.FolioTransferencia = U.FolioTransferencia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TraspasosEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TraspasosEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TraspasosEnc__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TraspasosDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TraspasosDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TraspasosDet__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TraspasosDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TraspasosDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TraspasosDet_Lotes__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__TraspasosDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__TraspasosDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__TraspasosDet_Lotes_Ubicaciones__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionesEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionesEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionesEnc__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionesDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionesDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionesDet__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionesDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionesDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionesDet_Lotes__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__DevolucionesDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__DevolucionesDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__DevolucionesDet_Lotes_Ubicaciones__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PreSalidasPedidosEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PreSalidasPedidosEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PreSalidasPedidosEnc__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PreSalidasPedidosDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PreSalidasPedidosDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PreSalidasPedidosDet__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PreSalidasPedidosDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PreSalidasPedidosDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PreSalidasPedidosDet_Lotes_Ubicaciones__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosEnc__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosDet__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosDet_Lotes__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosDet_Lotes_Ubicaciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosDet_Lotes_Ubicaciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosDet_Lotes_Ubicaciones__FechaControl 
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
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdSubFarmacia = U.IdSubFarmacia and I.FolioPedido = U.FolioPedido and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño and I.Renglon = U.Renglon  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosEnvioEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosEnvioEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosEnvioEnc__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosEnvioDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosEnvioDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosEnvioDet__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosEnvioDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosEnvioDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosEnvioDet_Lotes__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosEnvioDet_Lotes_Registrar__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosEnvioDet_Lotes_Registrar__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes_Registrar' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosEnvioDet_Lotes_Registrar__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosDistEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosDistEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosDistEnc__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosDistDet__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosDistDet__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosDistDet__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__PedidosDistDet_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__PedidosDistDet_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__PedidosDistDet_Lotes__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__CatPersonalCEDIS__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__CatPersonalCEDIS__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonalCEDIS' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__CatPersonalCEDIS__FechaControl 
     On CatPersonalCEDIS 
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
                From CatPersonalCEDIS U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.IdPersonal = U.IdPersonal  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Enc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Enc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Enc__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Det__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Det__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Det__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Det_Pedido_Distribuidor__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Det_Pedido_Distribuidor__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Pedido_Distribuidor' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Det_Pedido_Distribuidor__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Enc_Surtido__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Enc_Surtido__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc_Surtido' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Enc_Surtido__FechaControl 
     On Pedidos_Cedis_Enc_Surtido 
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
                From Pedidos_Cedis_Enc_Surtido U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioSurtido = U.FolioSurtido  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Enc_Surtido_Atenciones__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Enc_Surtido_Atenciones__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc_Surtido_Atenciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Enc_Surtido_Atenciones__FechaControl 
     On Pedidos_Cedis_Enc_Surtido_Atenciones 
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
                From Pedidos_Cedis_Enc_Surtido_Atenciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioSurtido = U.FolioSurtido and I.FechaRegistro = U.FechaRegistro  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Det_Surtido__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Det_Surtido__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Det_Surtido__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Det_Surtido_Distribucion__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Det_Surtido_Distribucion__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Det_Surtido_Distribucion__FechaControl 
     On Pedidos_Cedis_Det_Surtido_Distribucion 
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
                From Pedidos_Cedis_Det_Surtido_Distribucion U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioSurtido = U.FolioSurtido and I.IdSurtimiento = U.IdSurtimiento and I.ClaveSSA = U.ClaveSSA and I.IdSubFarmacia = U.IdSubFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso__FechaControl 
     On Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso 
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
                From Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioSurtido = U.FolioSurtido and I.IdSurtimiento = U.IdSurtimiento and I.FechaRegistro = U.FechaRegistro and I.ClaveSSA = U.ClaveSSA and I.IdSubFarmacia = U.IdSubFarmacia and I.IdProducto = U.IdProducto and I.CodigoEAN = U.CodigoEAN and I.ClaveLote = U.ClaveLote and I.IdPasillo = U.IdPasillo and I.IdEstante = U.IdEstante and I.IdEntrepaño = U.IdEntrepaño  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ALMJ_Pedidos_RC__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ALMJ_Pedidos_RC__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ALMJ_Pedidos_RC__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ALMJ_Pedidos_RC_Det__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ALMJ_Pedidos_RC_Det__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC_Det' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ALMJ_Pedidos_RC_Det__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ALMJ_Concentrado_PedidosRC__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ALMJ_Concentrado_PedidosRC__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ALMJ_Concentrado_PedidosRC__FechaControl 
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
 
------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ALMJ_Concentrado_PedidosRC_Claves__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ALMJ_Concentrado_PedidosRC_Claves__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Claves' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ALMJ_Concentrado_PedidosRC_Claves__FechaControl 
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
 
-------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__ALMJ_Concentrado_PedidosRC_Pedidos__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__ALMJ_Concentrado_PedidosRC_Pedidos__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__ALMJ_Concentrado_PedidosRC_Pedidos__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Ventas_ALMJ_PedidosRC_Surtido__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Ventas_ALMJ_PedidosRC_Surtido__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_ALMJ_PedidosRC_Surtido' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Ventas_ALMJ_PedidosRC_Surtido__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Ventas_TiempoAire__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Ventas_TiempoAire__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ventas_TiempoAire' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Ventas_TiempoAire__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Adt_VentasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Adt_VentasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Adt_VentasEnc__FechaControl 
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
 
---------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Adt_VentasInformacionAdicional__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Adt_VentasInformacionAdicional__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_VentasInformacionAdicional' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Adt_VentasInformacionAdicional__FechaControl 
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
 
-----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Adt_ComprasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Adt_ComprasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_ComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Adt_ComprasEnc__FechaControl 
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
 
----------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Adt_FarmaciaProductos_CodigoEAN_Lotes__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Adt_FarmaciaProductos_CodigoEAN_Lotes__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Adt_FarmaciaProductos_CodigoEAN_Lotes__FechaControl 
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
 
--------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate__Adt_OrdenesDeComprasEnc__FechaControl' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate__Adt_OrdenesDeComprasEnc__FechaControl 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Adt_OrdenesDeComprasEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate__Adt_OrdenesDeComprasEnc__FechaControl 
     On Adt_OrdenesDeComprasEnc 
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
                From Adt_OrdenesDeComprasEnc U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa and I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.FolioOrdenCompra = U.FolioOrdenCompra and I.FolioMovto = U.FolioMovto  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 
 
