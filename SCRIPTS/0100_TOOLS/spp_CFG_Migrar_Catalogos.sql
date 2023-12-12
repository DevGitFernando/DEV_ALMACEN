If Exists ( Select Name From Sysobjects Where Name = 'spp_CFG_MigrarDatos_Catalogos' and xType = 'P' )
	Drop Proc spp_CFG_MigrarDatos_Catalogos
Go--#SQL 

---  Exec spp_CFG_MigrarDatos_Catalogos 'SII_PtoVta_EnBlanco', '[CENTRAL].[SII_OficinaCentral]', 'SII_PtoVta_EnBlanco'

Create Proc spp_CFG_MigrarDatos_Catalogos 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_PtoVta_EnBlanco', 
	@BaseDeDatosDestino varchar(100) = 'SII_OficinaCentral_Test', 
	@BaseDeDatosEstructura varchar(100) = 'SII_PtoVta_EnBlanco', 
	@TablaDeControl varchar(100) = 'CFGSC_EnvioCatalogos',  
	@Ejecutar int = 1  		 
) 
With Encryption 
As
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sTabla varchar(200), 
        @sSql varchar(8000), 
        @iError int, 
		@Esquema varchar(100) 		         

----Begin tran 
----print 'Begin tran' 

	Set @Esquema = '.dbo.'           
    Set @sSql = '' 
    Set @iError = 0 
	
	if exists ( select * from tempdb..sysobjects(nolock) where Name = '#CFGC_EnvioDetalles' ) 
       drop table #CFGC_EnvioDetalles 

	Select space(200) as NombreTabla, 0 as Procesada, 0 as IdOrden Into #CFGC_EnvioDetalles Where 1 = 0 
    Set @sSql = ' Insert Into #CFGC_EnvioDetalles ' + 
		'Select NombreTabla, 0, IdOrden   ' + 
        'From ' + @BaseDeDatosEstructura + @Esquema + '' + @TablaDeControl + '(Nolock) ' + 
        'Where Status = ' + char(39) + 'A' + char(39) + ' Order By IdOrden ' 
	Exec(@sSql) 
--	print @sSql 
--    select * from #CFGC_EnvioDetalles 


    Declare Llave_Tablas Cursor For 
        Select NombreTabla 
        From #CFGC_EnvioDetalles 
        -- Where 1 = 0 -- NombreTabla <> 'Net_Usuarios' 
        Order By IdOrden 
	open Llave_Tablas 
	Fetch From Llave_Tablas into @sTabla 
	while @@Fetch_status = 0 and @iError = 0 
		begin 
		    Exec spp_CFG_Script_MigrarDatos_General @BaseDeDatosOrigen, @BaseDeDatosDestino, @BaseDeDatosEstructura, @sTabla, @sSql output 

			If @Ejecutar = 1 
			   Exec(@sSql) 		
			Else 
			   Print @sSql 

--			print @sSql 

		    if (@@error <> 0 ) 
				begin 
				   Set @iError = 1  
				   update #CFGC_EnvioDetalles Set Procesada = 2 where NombreTabla = @sTabla  
				end 
			else 
				begin 
				   update #CFGC_EnvioDetalles Set Procesada = 1 where NombreTabla = @sTabla  
				end 
		    
		    Fetch next From Llave_Tablas into @sTabla 
		end 
    close Llave_Tablas  
	deallocate Llave_Tablas 
    
    
----    if @iError = 0 
----        begin 
----           commit tran 
----           print 'Commit tran' 
----        end    
----    else 
----        begin 
----           rollback tran 
----           print 'Rollback tran'            
----        end    
          
    
End 
Go--#SQL 


---------------------------------------------- 
