If Exists ( Select Name From Sysobjects Where Name = 'spp_INF_SEND_FC' and xType = 'P' )
	Drop Proc spp_INF_SEND_FC 
Go--#SQL 

---  Exec spp_INF_SEND_FC 'SII_PtoVta_EnBlanco', '[CENTRAL].[SII_OficinaCentral]', 'SII_PtoVta_EnBlanco'

-- Exec spp_INF_SEND_FC 'SII_OficinaCentral', '[SVRPUEBLA].[SII_RegionalPuebla]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 

-- Exec spp_INF_SEND_FC 'SII_OficinaCentral', '[SVROAXACA].[SII_Oaxaca]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 


Create Proc spp_INF_SEND_FC 
(
    @BaseDeDatosOrigen varchar(100) = '[SVRCENTRAL].[SII_OficinaCentral]', 
	@BaseDeDatosDestino varchar(100) = 'SII_OficinaCentral_Test', 
	@BaseDeDatosEstructura varchar(100) = 'SII_OficinaCentral_Test', 
	@TablaDeControl varchar(100) = 'CFGSC_EnvioCatalogos',  
	@HorasRevision int = 12, 
	@TipoEnvio int = 1, 
	@Ejecutar int = 0 
)
With Encryption 
As
Begin 
Set NoCount On 
Declare 
	@sTabla varchar(500), 
    @sSql varchar(max), 
    @iError int, 
	@Esquema varchar(100), 
	@iActualizado int, 
	@FechaInicial datetime, 
	@FechaFinal datetime  

----Begin tran 
----print 'Begin tran' 

	Set @Esquema = '.dbo.'           
    Set @sSql = '' 
    Set @iError = 0 
	
	Set @iActualizado = 1  	   	
	If @TipoEnvio = 1 
	   Set @iActualizado = 1 
	   
	If @TipoEnvio = 2 
	   Set @iActualizado = 2 	
	
--- Preparar el filtro por fechas 	
   	Set @FechaFinal = getdate() 
	Set @FechaInicial = dateadd(Hh, -1 * @HorasRevision, getdate() ) 
			
	
	if exists ( select * from tempdb..sysobjects(nolock) where Name = '#CFGC_EnvioDetalles' ) 
       drop table #CFGC_EnvioDetalles 

	Select space(200) as NombreTabla, 0 as Procesada, 0 as IdOrden, 1 as Existe Into #CFGC_EnvioDetalles Where 1 = 0 
    Set @sSql = 'Insert Into #CFGC_EnvioDetalles ' + 
		'Select NombreTabla, 0, IdOrden, 1   ' + 
        'From ' + @BaseDeDatosEstructura + @Esquema + '' + @TablaDeControl + ' T (Nolock) ' + 
        'Inner Join ' + @BaseDeDatosEstructura + @Esquema + ' Sysobjects S (NoLock) On ( T.NombreTabla = S.Name )  ' + 
        'Where T.Status = ' + char(39) + 'A' + char(39) + ' Order By T.IdOrden ' 
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
		    Exec spp_INF_SEND_FC_Detalles 
				@BaseDeDatosOrigen = @BaseDeDatosOrigen, 
				@BaseDeDatosDestino = @BaseDeDatosDestino, 
				@BaseDeDatosEstructura = @BaseDeDatosEstructura, 
				@Tabla = @sTabla, 
				@SalidaFinal = @sSql output, 
				@TipoEnvio = @iActualizado, 
				@FechaInicial = @FechaInicial, 
				@FechaFinal = @FechaFinal, 
				@HorasRevision = @HorasRevision  

/* 
Create Proc spp_INF_SEND_FC_Detalles 
(

     datetime = null, 
	 datetime = null,   
    @Update bit = true,    
    @SalidaFinal_Insert varchar(max) = '' output,         
    @SalidaFinal_Update varchar(max) = '' output, 
    @HorasRevision int = 2     
)
*/  

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
