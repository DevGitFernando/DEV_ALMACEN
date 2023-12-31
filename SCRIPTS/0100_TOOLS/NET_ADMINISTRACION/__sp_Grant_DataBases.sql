/* 
USE [SII_Almacen_OAX_02]
GO
EXEC dbo.sp_changedbowner @loginame = N'sa', @map = false
GO
*/ 

  
If Exists ( Select * From Sysobjects Where Name = 'sp_Grant_DataBases' and xType = 'P' )
	Drop Proc sp_Grant_DataBases
Go--#SQL

Create Proc sp_Grant_DataBases  
(
    @BaseDeDatosDestino varchar(100) = 'SII_SINALOA_GENERAL', @Usuario varchar(100) = 'sa' 
) 
-- with Encryption 
As
Begin
Declare @BaseDeDatosOrigen varchar(200),  
        @sSql varchar(200), 
        @iError int 
         
       
    Set @iError = 0    
    Set @BaseDeDatosOrigen = ''  
    Set @sSql = '' 

    Declare Llave_BD Cursor For 
        Select Name as BaseDeDatos 
        From master..sysdatabases 
        Where name not in ( 'master', 'model', 'msdb', 'tempdb'  )  -- like '%sii_0%'
        Order by Name 
	open Llave_BD 
	Fetch From Llave_BD into @BaseDeDatosOrigen 
	while @@Fetch_status = 0 and @iError = 0 
		begin 
		    -- Print 'Origen ==> ' + @BaseDeDatosOrigen + '        ' + 'Destino ==> ' +  @BaseDeDatosDestino 
--		    Exec spp_CFG_MigrarDatos @BaseDeDatosOrigen, @BaseDeDatosDestino 
        Set @sSql = 'ALTER AUTHORIZATION ON DATABASE::' + @BaseDeDatosOrigen + ' TO ' + @Usuario + ''; 
        --print @sSql  
        Exec(@sSql) 

		    If (@@error <> 0 ) 
		       Set @iError = 1  
		    
		    Fetch next From Llave_BD into @BaseDeDatosOrigen  
		end 
    close Llave_BD  
	deallocate Llave_BD 
	
End 
Go--#SQL 
 	