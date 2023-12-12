If Exists ( Select Name From Sysobjects Where Name = 'spp_INF_Procesar_Informacion' and xType = 'P' )
	Drop Proc spp_INF_Procesar_Informacion 
Go--#SQL 

---  Exec spp_INF_Procesar_Informacion 'SII_PtoVta_EnBlanco', '[CENTRAL].[SII_OficinaCentral]', 'SII_PtoVta_EnBlanco'

-- Exec spp_INF_Procesar_Informacion 'SII_OficinaCentral', '[SVRPUEBLA].[SII_RegionalPuebla]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 

-- Exec spp_INF_Procesar_Informacion 'SII_OficinaCentral', '[SVROAXACA].[SII_Oaxaca]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 


Create Proc spp_INF_Procesar_Informacion 
(
	@TablaDeControl varchar(100) = 'CFGC_EnvioDetalles',  
	@TipoProceso int = 1, 
	@Ejecutar int = 0  		 	
)
With Encryption 
As
Begin 
Set NoCount On 
Declare @sTabla varchar(500), 
        @sSql varchar(max), 
        @iError int, 
		@Esquema varchar(100), 
		@sMsj varchar(1000),  
		@iActualizado int, 
		@sFiltro varchar(500), 
		@iValor int   		         

----Begin tran 
----print 'Begin tran' 

	Set @Esquema = '.dbo.'           
    Set @sSql = '' 
    Set @iError = 0 
	
	Set @iActualizado = 0  
	Set @sFiltro = 0 
	Set @iValor = 0 
	
	Set @sMsj = 'El tipo de proceso solicitado no es válido, verifique.' 
	
	if @TablaDeControl = '' 
	   Set @TablaDeControl = 'CFGC_EnvioDetalles' 
	
	If @TipoProceso = 1 
	Begin 
	   Set @iActualizado = 1 
	   Set @sFiltro = ' Where Actualizado not in ( 3, 11 ) ' 
	   Set @iValor = 10   
	End  
	   
	If @TipoProceso = 2 
	Begin 
	   Set @iActualizado = 2 
	   Set @sFiltro = ' Where Actualizado in ( 10 ) ' 
	   Set @iValor = 11   
	End  
	
	
	If @iActualizado = 0
	Begin
		RaisError (@sMsj, 10,16 )
		Return
	End 
	
	
	if exists ( select * from tempdb..sysobjects(nolock) where Name = '#CFGC_EnvioDetalles' ) 
       drop table #CFGC_EnvioDetalles 

	Select space(200) as NombreTabla, 0 as Procesada, 0 as IdOrden, 1 as Existe Into #CFGC_EnvioDetalles Where 1 = 0 
----    Set @sSql = 'Insert Into #CFGC_EnvioDetalles ' + 
----		'Select NombreTabla, 0, IdOrden, 1   ' + 
----        'From ' + @BaseDeDatosEstructura + @Esquema + '' + @TablaDeControl + ' T (Nolock) ' + 
----        'Inner Join ' + @BaseDeDatosEstructura + @Esquema + ' Sysobjects S (NoLock) On ( T.NombreTabla = S.Name )  ' + 
----        'Where T.Status = ' + char(39) + 'A' + char(39) + ' Order By T.IdOrden ' 
----	Exec(@sSql) 

    Set @sSql = 'Insert Into #CFGC_EnvioDetalles ' + 
		'Select NombreTabla, 0, IdOrden, 1   ' + 
        'From ' + @TablaDeControl + ' T (Nolock) ' + 
        'Inner Join Sysobjects S (NoLock) On ( T.NombreTabla = S.Name )  ' + 
        'Where T.Status = ' + char(39) + 'A' + char(39) + ' Order By T.IdOrden ' 
	Exec(@sSql) 

	Delete From #CFGC_EnvioDetalles Where NombreTabla = 'Net_Usuarios' 
	Delete From #CFGC_EnvioDetalles Where NombreTabla = 'Movtos_Inv_Tipos_Farmacia' 


    Declare #Llave_Tablas Cursor For 
        Select NombreTabla 
        From #CFGC_EnvioDetalles 
        -- Where 1 = 0 -- NombreTabla <> 'Net_Usuarios' 
        -- Where Existe = 1 
        Order By IdOrden 
	open #Llave_Tablas 
	Fetch From #Llave_Tablas into @sTabla 
	while @@Fetch_status = 0 and @iError = 0 
		begin 
		    Set @sSql = '' 
		    -- Print @sTabla 
		    Set @sSql = 'Update T Set Actualizado = ' + cast(@iValor as varchar) + ' ' +  -- + ' ' + char(13) + (10) + ''
				'From ' + @sTabla + ' T (NoLock) ' + @sFiltro  
		    --Exec(@sSql) 
		    
		    
		    --		Exec spp_INF_Procesar_Informacion '', 2, 0 
		     

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
		    
		    Fetch next From #Llave_Tablas into @sTabla 
		end 
    close #Llave_Tablas  
	deallocate #Llave_Tablas          
    
End 
Go--#SQL 


---------------------------------------------- 
