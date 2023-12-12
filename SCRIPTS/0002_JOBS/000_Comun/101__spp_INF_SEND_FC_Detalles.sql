If Exists ( Select Name From Sysobjects Where Name = 'spp_INF_SEND_FC_Detalles' and xType = 'P' )
	Drop Proc spp_INF_SEND_FC_Detalles
Go--#SQL 

/* 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 
	Exec spp_INF_SEND_FC_Detalles 'SII_21_0050___Santa_Ana_DelConde', 'SII_11_0003__CEDIS__2K140523_1437', 'SII_21_0050___Santa_Ana_DelConde', 
		 'VentasEnc', @sSql output  
		 
	Print @sSql 	 

*/ 

Create Proc spp_INF_SEND_FC_Detalles 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_11_0092___HG_FCO_DEL_RINCON', 
    @BaseDeDatosDestino varchar(100) = 'SII_GTO___CONCENTRADO', 
	@BaseDeDatosEstructura varchar(100) = 'SII_11_0092___HG_FCO_DEL_RINCON', 	     
    @Tabla varchar(50) = 'VentasEnc', 
    @SalidaFinal varchar(max) = '' output, 
    @TipoEnvio int = 1, 
    @FechaInicial datetime = null, 
	@FechaFinal datetime = null,   
    @Update bit = true,    
    @SalidaFinal_Insert varchar(max) = '' output,         
    @SalidaFinal_Update varchar(max) = '' output, 
    @HorasRevision int = 2     
)
with Encryption 
As
Begin 
Set NoCount On 
Declare -- @Tabla varchar(50), 
	@QueryFinal varchar (max),
	@Query varchar(max),
	@Campo varchar(max), 
	@Campo_Aux varchar(max), 	
	@Campo_Diferencia varchar(max), 		
	@Tipo tinyint, 
	@Padre varchar(80), 
	@Tiene_Pk bit, 
	@Parte_Exists varchar(max),
	@Parte_Relacion_Tablas varchar(max),	
	@Parte_Lista_Campos varchar(max),		
	@Parte_Update varchar(max), 
	@Parte_Update_Pura varchar(max), 	
	@Parte_Insersion varchar(max), 	
	@Parte_Insersion_Pura varchar(max), 		
	@bParamVal int, @sMsj varchar(max),    
	@Alias_Origen varchar(10),
	@Alias_Destino varchar(10),	
	@Alias_Origen_Base varchar(10),
	@Alias_Destino_Base varchar(10), 
	@Esquema varchar(100), 
	@iActualizado int, 
	@FiltroFechaControl varchar(max), 
	@dFechaInicial datetime, 
	@dFechaFinal datetime,   
	@iExiste_FechaControl int, 
	@sFiltro_FechaControl varchar(200), 
	@sFechaControl_Desde varchar(100)    		
	
	Set @iActualizado = 0  	   	
	If @TipoEnvio = 1 
	   Set @iActualizado = 0 
	   
	If @TipoEnvio = 2 
	   Set @iActualizado = 10  	   
		
	
--	@ParteUpDate bit 

-- Declare @sValorActualizado varchar(2)
--	Set @sValorActualizado = '1'

-- Declare @Criterio varchar(max), @ParteUpDate bit 

	Set @bParamVal = 1
	Set @sMsj = 'Error al generar  ' + char(13) + char(10)
	If @Tabla = ''
	Begin
		Set @bParamVal = 0
		Set @sMsj = @sMsj + char(13) + char(10) + 'Falta proporcionar el nombre de tabla a cual se le obtendra la información.'
	End

	If @bParamVal = 0
	Begin
		RaisError (@sMsj, 10,16 )
		Return
	End	

    
    Set @QueryFinal = '' 
    Set @Parte_Relacion_Tablas = '' 
    Set @Parte_Lista_Campos = '' 
    Set @Parte_Insersion = '' 
    Set @Campo_Aux = '' 
    Set @Campo_Diferencia = '' 
    Set @Parte_Update_Pura = '' 
    Set @Parte_Insersion_Pura = '' 
    Set @Alias_Origen_Base = 'Bd_O'
    Set @Alias_Destino_Base = 'Bd_D' 
	Set @iExiste_FechaControl = 0 

	Set @Esquema = '.dbo.' 
    Set @Alias_Origen = 'Bd_O.' 
    Set @Alias_Destino = 'Bd_D.' 
    
    
 ----  	Set @FechaFinal = getdate() 
	----Set @FechaInicial = getdate() - ( @DiasRevision + 1 ) 
    Set @FechaInicial = IsNull(@FechaInicial, getdate()) 
    Set @FechaFinal = IsNull(@FechaFinal, getdate())  
	Set @FiltroFechaControl = 'convert(varchar(16), ' + @Alias_Origen + 'FechaControl, 120) Between ' + 
		char(39) + cast(convert(varchar(16), @FechaInicial, 120) as varchar) + char(39) + ' and ' + 
		char(39) + cast(convert(varchar(16), @FechaFinal, 120) as varchar) + char(39)  

 	
	
--	select @FiltroFechaControl 

---		spp_INF_SEND_FC_Detalles 	
 

--- Set @Tabla = 'OrdenCompra_Det'
--- Inicia proceso

	-- Buscar si la tabla tiene llave primaria              
	Select 0 as IdPadre Into #tmpPadre where 1 = 0 
	Select 0 as TienePK Into #tmpPK where 1 = 0 	
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 
	    'Insert into #tmpPadre ' +  
	    'Select so.Id  ' +
	    'From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects so (NoLock) ' + 
	    'Where so.Name = ' + char(39) + @Tabla + char(39) + ' and xType = ' + char(39) + 'U' + char(39) 
    Exec(@QueryFinal ) 	
	Set @Padre = ( Select IdPadre From #tmpPadre (NoLock) )

	Set @QueryFinal = 'Set NoCount On ' + char(10) + 
	    'Insert into #tmpPK ' +  
	    'Select 1  ' +
	    'From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects so (NoLock) ' + 
	    'Where so.parent_obj = ' + char(39) + cast(@Padre as varchar) + char(39) + ' and xType = ' + char(39) + 'PK' + char(39) 
    Exec(@QueryFinal ) 	
	Set @Tiene_Pk = ( Select TienePK From #tmpPK (NoLock) )
	Set @Tiene_Pk = IsNull(@Tiene_Pk, 0) 
	
	
----	Select @Padre, @Tiene_Pk 
----	select * from #tmpPadre 	
----	select * from #tmpPK 

----	If Exists ( Select so1.name From Sysobjects as so1 Where so1.xtype = 'PK' and so1.parent_obj = @Padre ) 
----		Set @Tiene_Pk = 1 
----	Else 
----		Set @Tiene_Pk = 0 

	-- Select @Tiene_Pk
	Set @QueryFinal = ''
	-- Set @Criterio = ''
	Set @Parte_Exists = ''
	Set @Campo = ''
	Set @Tipo = 0
	
----- Este Parametro se envia dependiendo el tipo de Tabla 
--	Set @ParteUpDate = 1

	If @Tiene_Pk = 1 
		begin 

			Select top 0 sc.name, Sc.ColId  
			into #tmpColumnasPK 
			From Syscolumns as sc 
			Inner Join sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
			Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
			    and Sc.status <> 128 -- Ignorar las columnas Identidad
			Order by Sc.ColId
			
			Set @QueryFinal = 'Set NoCount On ' + char(10) + 
			    'Insert into #tmpColumnasPK ' +  
			    'Select sc.name, Sc.ColId   
			    From ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc 
			    Inner Join ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
			    Where sc.id = ' + cast(@Padre as varchar) + ' and si.indid = (Select min(indid) From ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys Where id = ' + cast(@Padre as varchar) + ' ) 
			        and Sc.status <> 128 
			    Order by Sc.ColId ' 
			Exec(@QueryFinal) 
--			print @QueryFinal 

---  spp_CFG_MigrarDatos_Catalogos 
            
---     spp_INF_SEND_FC_Detalles 
			
--			select * from #tmpColumnasPK 
----			print @QueryFinal 
----			print ''
----			print ''
----			print ''						

---   spp_INF_SEND_FC_Detalles 					

----			Declare Llave_Cursor Cursor For 
----			Select sc.name  From Syscolumns as sc 
----				Inner Join sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
----				Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
----					and Sc.status <> 128 -- Ignorar las columnas Identidad
----					Order by Sc.ColId
            Declare Llave_Cursor Cursor For  
            Select name From #tmpColumnasPK Order by ColId 
			open Llave_Cursor 
			Fetch From Llave_Cursor into @Campo 

			Set @Parte_Exists = 'Select ' + char(39) + 'If Not Exists ( Select * From ' + @Tabla + ' (NoLock)  Where ' + char(39) + ' + ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
            Set @Parte_Exists = 'Where ' 
            Set @Parte_Relacion_Tablas = '' 

			while @@Fetch_status = 0 
				begin               
			        Set @query = @campo 
			        Set @query = @Alias_Destino  + @query + ' = ' + @Alias_Origen + @query + '' 
        							
			        Fetch next From Llave_Cursor into @campo -- , @tipo 
			        If @@Fetch_status = 0 
				        begin 
					        Set @query = @query + ' and ' 
				        end 
			         Else 
				        begin 
					        Set @query = @query + ' ' --- + char(13) + char(10)
				        end 
			         Set @Parte_Exists = @Parte_Exists + @query 
			         Set @Parte_Relacion_Tablas = @Parte_Relacion_Tablas + @query 			         
				end              
			
			close Llave_Cursor              
			deallocate Llave_cursor               
		end
	Else
		begin 
			Set @Parte_Exists = 'Select ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
		end 


--------------------------------------------- Obtener columnas   		
	-- 2K9-0709.0828 
	-- Barrer todos los campos de la tabla
	Set @QueryFinal = '' 
	
	Select top 0 sc.name, Sc.ColId    
	into #tmpColumnas 
	From Sysobjects as so 
	Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 	
	    'Insert into #tmpColumnas ' +  
	    'Select sc.name, Sc.ColId   
	    From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects as so 
	    Inner Join ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc on (so.id = sc.id)  
	    Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + ' Order by Sc.ColId ' 
	Exec(@QueryFinal) 	
	

	Select top 0 sc.name, Sc.ColId    
	into #tmpColumnas_Aux  
	From Sysobjects as so 
	Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 	
	    'Insert into #tmpColumnas_Aux ' +  
	    'Select sc.name, Sc.ColId   
	    From ' + @BaseDeDatosOrigen + '.dbo.Sysobjects as so 
	    Inner Join ' + @BaseDeDatosOrigen + '.dbo.Syscolumns as sc on (so.id = sc.id)  
	    Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + ' Order by Sc.ColId ' 
	Exec(@QueryFinal) 		
	-- print '_____xxxxx' + @QueryFinal
--------------------------------------------- Obtener columnas 	
	
-------------------------------- FECHA DE CONTROL 	
    Set @sFiltro_FechaControl = '' 
    Set @sFechaControl_Desde = convert(varchar(16), dateadd(hh, -1 * @HorasRevision, getdate()), 120) 
  
	If Exists ( Select * from #tmpColumnas_Aux Where name = 'FechaControl' ) 
	Begin  
		Set @iExiste_FechaControl = 1 
		Set @sFiltro_FechaControl =  ' convert(varchar(16), Bd_O.FechaControl, 120) >=  ' + char(39) + @sFechaControl_Desde + char(39) 
		--Delete From #tmpColumnas Where name = 'FechaControl' 
    End 
	Set @FiltroFechaControl = @sFiltro_FechaControl  
-------------------------------- FECHA DE CONTROL 		
	
	
---   spp_INF_SEND_FC_Detalles 					
	

----	Declare query_Cursor Cursor For 
----		Select sc.name From Sysobjects as so 
----		Inner Join Syscolumns as sc on (so.id = sc.id) 
----	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad
	Declare query_Cursor Cursor For 
	select name from #tmpColumnas  Order by ColId
	open Query_cursor Fetch From Query_cursor into @campo --, @tipo              
	Set @query = '' 	
	Set @query = char(13) + char(10) + char(39) + 'Insert Into ' + @Tabla + ' ( ' -- + char(39) 
	
	-- @BaseDeDatosOrigen varchar(50) = '', @BaseDeDatosDestino  
    Set @Alias_Origen = 'Bd_O.'
    Set @Alias_Destino = 'Bd_D.' 
    	
	Set @query = 'Update ' + @Alias_Destino_Base + ' Set ' 
	
	-- Set @query = char(13) + char(10) + ' Insert Into ' + @Tabla + ' ( ' -- + char(39) 
	Set @QueryFinal = @QueryFinal + @Query 
	Set @QueryFinal = @Query 
	while @@Fetch_status = 0 
		begin      
			Set @query = @campo 
			Set @Campo_Aux = @campo 			
			Set @query = @Alias_Destino  + @query + ' = ' + @Alias_Origen + @query + '' 
			Set @Campo_Diferencia = @Campo_Diferencia + '(' + @Alias_Origen + @campo + '<>' + @Alias_Destino + @campo + ')' 
							
			Fetch next From query_cursor into @campo -- , @tipo 
			If @@Fetch_status = 0 
				begin 
					Set @query = @query + ', ' 
			        Set @Campo_Aux = @Campo_Aux + ', ' 	
			        Set @Campo_Diferencia = @Campo_Diferencia + ' OR '
				end 
			 Else 
				begin 
					Set @query = @query + ' ' --- + char(13) + char(10)
			        Set @Campo_Aux = @Campo_Aux + ' ' 						
				end 
			 Set @QueryFinal = @QueryFinal + @Query 
			 Set @Parte_Lista_Campos = @Parte_Lista_Campos + @Campo_Aux 
		end 
	close Query_cursor              
	deallocate Query_cursor 		
	
--	Print @Campo_Diferencia 
	
--------------------- Generar parte Update 
--  @BaseDeDatosOrigen varchar(50) = 'SII_ElDiez', @BaseDeDatosDestino
    ----------Set @Parte_Update = 
    ----------    @QueryFinal + char(13) + 
	   ---------- 'From ' + @BaseDeDatosDestino + '..' + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13) + 
	   ---------- 'Inner Join ' + @BaseDeDatosOrigen + '..' + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  
	
    ----------Set @Parte_Insersion = 
    ----------    'Insert Into ' + @BaseDeDatosDestino + '..' + @Tabla + ' ( ' + @Parte_Lista_Campos + ' ) ' + char(13) +
    ----------    'Select ' + @Parte_Lista_Campos + char(13) + 
	   ---------- 'From ' + @BaseDeDatosOrigen + '..' + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) ' + char(13) + 
	   ---------- 'Where Not Exists ( Select * From ' + @BaseDeDatosDestino + '..' + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) '  + char(13) +  
	   ---------- '   Where ' + @Parte_Relacion_Tablas + ' ) ' 
	           
---- Implementar Esquema 
    Set @Parte_Update = '' 
    If @Update = 1 
        Begin 
            If @Tiene_Pk = 1 
            Begin 
				Set @Parte_Update_Pura = '-------------------------- Parte Update ' + char(10) + 
					'Set NoCount On ' + char(10) + 
					@QueryFinal + char(13) + 
					'From ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13) + 
					'Inner Join ' + @BaseDeDatosOrigen + @Esquema + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)              
		            
				Set @Parte_Update = @Parte_Update_Pura + 'Where ' + @Alias_Origen + 'Actualizado in ( 0, 1, 2 ) ' --- + char(10)  
		            
				----Set @Parte_Update = @Parte_Update + char(10) + char(13) + 
				----	@QueryFinal + char(13) + 
				----	'From ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13) + 
				----	'Inner Join ' + @BaseDeDatosOrigen + @Esquema + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  
				----	+ 'Where ' + @Alias_Origen + 'Actualizado in ( 0, 1, 2 ) ' -- + char(10) + char(13) 	       	        	            
	        End  
	        
			If @iExiste_FechaControl = 1 
			Begin 
				Set @Parte_Update = @Parte_Update + ' And ' + @sFiltro_FechaControl  + char(13) + char(10) 
			End 			
			Set @Parte_Update = @Parte_Update + char(13) + char(10) 	           
	    End 
	
	
    Set @Parte_Insersion = '-------------------------- Parte Insert ' + char(10) + 
        'Set NoCount On ' + char(10) + 
        'Insert Into ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ( ' + @Parte_Lista_Campos + ' ) ' + char(13) +
        'Select ' + @Parte_Lista_Campos + char(13) + 
	    'From ' + @BaseDeDatosOrigen + @Esquema + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) ' + char(13) 
	   
	If @Tiene_Pk = 1     
	   Begin  
    	   Set @Parte_Insersion = @Parte_Insersion + 
	        'Where ' + @Alias_Origen + 'Actualizado in ( 0, 1, 2 ) ' + char(13) +   
	        ' and Not Exists ( Select * From ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) '  + char(13) +  
	        '   Where ' + @Parte_Relacion_Tablas + ' ) ' 	           
       End 
      
    If @iExiste_FechaControl = 1 
    Begin 
		Set @Parte_Insersion = @Parte_Insersion + ' And ' + char(13) + char(10) + @sFiltro_FechaControl  ---- + char(13) + char(10) 
    End             
	Set @Parte_Update = @Parte_Update + char(13) + char(10) 	           
    		
    			  
--		spp_INF_SEND_FC_Detalles        
	           
	           
	    -- 'From ' + @BaseDeDatosDestino + '..' + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13)  
	    -- 'Inner Join ' + @BaseDeDatosOrigen + '..' + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  	
	
-- @BaseDeDatosDestino + ' ' + @Alias_Origen_Base 	
----   spp_INF_SEND_FC_Detalles 

	
--	print @Parte_Exists 
--	print @Parte_Relacion_Tablas 			
--	print @QueryFinal 		
----	print @Parte_Update 
----	print '' 
----	print '' 
----	print @Parte_Insersion



------------------
    Set @SalidaFinal = '' 
    Set @SalidaFinal = @Parte_Update + char(13) + @Parte_Insersion 
    Set @SalidaFinal = '------------------------------------------------ ' + @Tabla + char(10) + 
                       @Parte_Update + char(13) + @Parte_Insersion + char(10) + 
                       '------------------------------------------------ ' + @Tabla + char(10) + char(10) 


	Set @SalidaFinal_Insert = '' 
	Set @SalidaFinal_Insert = @Parte_Insersion     
	
	Set @SalidaFinal_Update = '' 	
	Set @SalidaFinal_Update = @Parte_Update_Pura                       
    
    -- print @FiltroFechaControl 
    print '' 
    print @SalidaFinal 
        
--		spp_INF_SEND_FC_Detalles  


----	-- Ejecutar la salida Final 
----	Exec(@QueryFinal)
------  spp_INF_SEND_FC_Detalles 'CatEstados'

----    Print @QueryFinal 

	
End
Go--#SQL