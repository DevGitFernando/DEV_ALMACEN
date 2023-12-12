If Exists ( Select Name From Sysobjects Where Name = 'spp_INF_SEND' and xType = 'P' )
	Drop Proc spp_INF_SEND 
Go--#SQL 

---  Exec spp_INF_SEND 'SII_PtoVta_EnBlanco', '[CENTRAL].[SII_OficinaCentral]', 'SII_PtoVta_EnBlanco'

-- Exec spp_INF_SEND 'SII_OficinaCentral', '[SVRPUEBLA].[SII_RegionalPuebla]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 

-- Exec spp_INF_SEND 'SII_OficinaCentral', '[SVROAXACA].[SII_Oaxaca]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 


Create Proc spp_INF_SEND 
(
    @BaseDeDatosOrigen varchar(100) = '[SVRCENTRAL].[SII_OficinaCentral]', 
	@BaseDeDatosDestino varchar(100) = 'SII_OficinaCentral_Test', 
	@BaseDeDatosEstructura varchar(100) = 'SII_OficinaCentral_Test', 
	@TablaDeControl varchar(100) = 'CFGSC_EnvioCatalogos',  
	@TipoEnvio int = 1, 
	@Ejecutar int = 1  		 
)
With Encryption 
As
Begin 
Set NoCount On 
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

	Select space(200) as NombreTabla, 0 as Procesada, 0 as IdOrden, 1 as Existe Into #CFGC_EnvioDetalles Where 1 = 0 
    Set @sSql = 'Insert Into #CFGC_EnvioDetalles ' + 
		'Select NombreTabla, 0, IdOrden, 1   ' + 
        'From ' + @BaseDeDatosEstructura + @Esquema + '' + @TablaDeControl + '(Nolock) ' + 
        'Where Status = ' + char(39) + 'A' + char(39) + ' Order By IdOrden ' 
	Exec(@sSql) 
	
----	Set @sSql = 
----	    'Update T Set Existe = 1 ' + char(10) + 
----	    'From #CFGC_EnvioDetalles T ' + char(10) + 
----	    'Inner Join ' + @BaseDeDatosDestino + @Esquema + 
----	    'Sysobjects So (NoLock) On ( T.NombreTabla = So.Name and So.xType = ' + char(39) + 'U' + char(39) + ' ) ' 
------	print @sSql 	
----	Exec(@sSql)  


--    select * from #CFGC_EnvioDetalles 

--    delete  from #CFGC_EnvioDetalles where NombreTabla <> 'Net_CFGC_TipoCambio' 

    Declare Llave_Tablas Cursor For 
        Select NombreTabla 
        From #CFGC_EnvioDetalles 
        -- Where 1 = 0 -- NombreTabla <> 'Net_Usuarios' 
        -- Where Existe = 1 
        Order By IdOrden 
	open Llave_Tablas 
	Fetch From Llave_Tablas into @sTabla 
	while @@Fetch_status = 0 and @iError = 0 
		begin 
		    Set @sSql = '' 
		    -- Print @sTabla 
		    Exec spp_INF_SEND_Detalles @BaseDeDatosOrigen, @BaseDeDatosDestino, @BaseDeDatosEstructura, @sTabla, @sSql output 

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
