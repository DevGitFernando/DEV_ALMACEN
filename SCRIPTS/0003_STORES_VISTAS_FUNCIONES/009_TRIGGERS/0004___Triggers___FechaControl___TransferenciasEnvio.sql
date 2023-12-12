 
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
 
