-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_TransferenciasEnvioEnc_Status' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_TransferenciasEnvioEnc_Status 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioEnc' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     ' 
     Create Trigger TR_OnUpdate_TransferenciasEnvioEnc_Status 
     On TransferenciasEnvioEnc 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(Status) 
             Begin 
                If Exists ( Select Status From Deleted Where Status = ''I'' ) 
                Begin 
	                If Not Exists ( Select Status From Inserted Where Status = ''I'' ) 
	                Begin 
						--- Deshacer la actualización de datos  
						Rollback 
		              						--- Enviar el mensaje de error 
						Raiserror (''Esta acción no esta permitida.'', 1, 1) 
					End 
                End 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 

 