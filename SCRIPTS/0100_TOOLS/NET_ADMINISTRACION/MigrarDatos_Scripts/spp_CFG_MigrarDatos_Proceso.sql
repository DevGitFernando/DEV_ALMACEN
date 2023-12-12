  
If Exists ( Select Name From Sysobjects Where Name = 'spp_CFG_MigrarDatos_Proceso' and xType = 'P' )
	Drop Proc spp_CFG_MigrarDatos_Proceso
Go--#SQL

Create Proc spp_CFG_MigrarDatos_Proceso  
(
    @BaseDeDatosDestino varchar(100) = 'SII_SINALOA_GENERAL'
) 
-- with Encryption 
As
Begin
Declare @BaseDeDatosOrigen varchar(200), 
        @iError int 
         
       
    Set @iError = 0    
    Set @BaseDeDatosOrigen = ''  

    Declare Llave_BD Cursor For 
        Select top 100 Name as BaseDeDatos 
        From master..sysdatabases 
        Where name like '%sii_0%'
        Order by Name 
	open Llave_BD 
	Fetch From Llave_BD into @BaseDeDatosOrigen 
	while @@Fetch_status = 0 and @iError = 0 
		begin 
		    Print 'Origen ==> ' + @BaseDeDatosOrigen + '        ' + 'Destino ==> ' +  @BaseDeDatosDestino
		    Exec spp_CFG_MigrarDatos @BaseDeDatosOrigen, @BaseDeDatosDestino 

		    If (@@error <> 0 ) 
		       Set @iError = 1  
		    
		    Fetch next From Llave_BD into @BaseDeDatosOrigen  
		end 
    close Llave_BD  
	deallocate Llave_BD 
	
End 
Go--#SQL 
 	