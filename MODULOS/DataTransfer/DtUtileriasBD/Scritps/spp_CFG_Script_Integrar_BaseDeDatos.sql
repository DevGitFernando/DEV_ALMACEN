
------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects Where Name = 'spp_CFG_Script_Integrar_BaseDeDatos' and xType = 'P' )
	Drop Proc spp_CFG_Script_Integrar_BaseDeDatos
Go--#SQL 

--- exec spp_CFG_Script_Integrar_BaseDeDatos 'SII_21_0271', 'SII_21_0253_TEPEACA', 'SII_21_0253_TEPEACA', 'CtlCortesParciales' 

Create Proc spp_CFG_Script_Integrar_BaseDeDatos 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ_20200903_163827', 
    @BaseDeDatosDestino varchar(100) = 'SII_21_4182_CEDIS_REGIONAL_20200825_122124', 
	@BaseDeDatosEstructura varchar(100) = 'SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ_20200903_163827', 	     
    @Tabla varchar(max) = 'VentasEnc', 
	@ValidarEstructura_Origen int = 0,  
	@SalidaFinal varchar(max) = '' output, 
    @Update bit = true, 
	@SoloDiferencias bit = true, 
    @Meses_FechaControl int = 120, 
	@Criterio varchar(max) = ''    
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
	@Tipo tinyint, 
	@Padre varchar(max), 
	@Tiene_Pk bit, 
	@EsPartePK bit, 	@EsPartePK_Ant bit, 	
	@Parte_Exists varchar(max), 
	@Parte_Relacion_Tablas varchar(max),	
	@Parte_Lista_Campos varchar(max),		
	@Parte_Lista_Campos_Update varchar(max),			
	@Parte_Update varchar(max), 
	@Parte_Insersion varchar(max), 	
	@bParamVal int, @sMsj varchar(max),    
	@Alias_Origen varchar(10),
	@Alias_Destino varchar(10),	
	@Alias_Origen_Base varchar(10),
	@Alias_Destino_Base varchar(10), 
	@Esquema varchar(max), 
	@sFiltro_Actualizado varchar(max), 
	@iExiste_FechaControl int, 
	@sFiltro_FechaControl varchar(max), 
	@sFechaControl_Desde varchar(max), 
	@iItems int, 
	@iItems_Procesados int     		
	

Declare 
	@sCollate varchar(200) 

	Set @sCollate = ' Collate Latin1_General_CI_AI ' 

--	@ParteUpDate bit 

-- Declare @sValorActualizado varchar(2)
--	Set @sValorActualizado = '1'

-- Declare @Criterio varchar(8000), @ParteUpDate bit 
	Set @iItems = 5 
	Set @iItems_Procesados = 0 

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
    Set @Parte_Lista_Campos_Update = ''  
    Set @Parte_Insersion = '' 
    Set @Campo_Aux = '' 
    Set @Alias_Origen_Base = 'Bd_O'
    Set @Alias_Destino_Base = 'Bd_D' 
	Set @iExiste_FechaControl = 0 

	Set @Esquema = '.dbo.'
    Set @Alias_Origen = 'Bd_O.'
    Set @Alias_Destino = 'Bd_D.' 
    

--- 
    Set @sFiltro_Actualizado = ' 1 = 1 ' 
    If @SoloDiferencias = 0 
       Set @sFiltro_Actualizado = ' Bd_O.Actualizado in ( 0, 2 ) ' 
    
    
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
	Set @Tiene_Pk = ( Select IdPadre From #tmpPadre (NoLock) )
	Set @Tiene_Pk = IsNull(@Tiene_Pk, 0) 
	
--	Select @Padre, @Tiene_Pk 

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

			Select top 0 sc.name, Sc.ColId, sc.xType 
			into #tmpColumnasPK 
			From Syscolumns as sc 
			Inner Join sysindexkeys as si on ( sc.id = si.id and sc.colid = si.colid) 
			Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
			    and Sc.status <> 128 -- Ignorar las columnas Identidad
			Order by Sc.ColId
			
			---- tomar el tipo de datos de la BD DESTINO 
			Set @QueryFinal = 'Set NoCount On ' + char(10) + 
			    'Insert into #tmpColumnasPK ' + char(13) +  
			    'Select distinct sc.name, Sc.ColId, sc.xType   
			    From ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc 
			    Inner Join ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys as si on ( sc.id = si.id and sc.colid = si.colid  ) 
				Inner Join ' + @BaseDeDatosDestino + '.dbo.Syscolumns as sce on ( sc.name = sce.name ' + @sCollate + ' )  
			    Where sc.id = ' + cast(@Padre as varchar) + ' and si.indid = (Select min(indid) From ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys Where id = ' + cast(@Padre as varchar) + ' ) 
			        and Sc.status <> 128 
			    Order by Sc.ColId ' 
			Exec(@QueryFinal) 
			--print @QueryFinal 


			Set @QueryFinal = 
			    'Update sc Set xType = sc.xType   
			    From ' + @BaseDeDatosEstructura + '.dbo.#tmpColumnasPK as sc 
				Inner Join ' + @BaseDeDatosDestino + '.dbo.Syscolumns as sce on ( sc.name = sce.name ' + @sCollate + ' )  ' 
			Exec( @QueryFinal ) 
			--print @QueryFinal 

---  spp_CFG_MigrarDatos_Catalogos 
            
---     spp_CFG_Script_Integrar_BaseDeDatos 
			
--			select * from #tmpColumnasPK 
----			print @QueryFinal 
----			print ''
----			print ''
----			print ''						

---   spp_CFG_Script_Integrar_BaseDeDatos 					

----			Declare Llave_Cursor Cursor For 
----			Select sc.name  From Syscolumns as sc 
----				Inner Join sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
----				Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
----					and Sc.status <> 128 -- Ignorar las columnas Identidad
----					Order by Sc.ColId
            Declare Llave_Cursor 
			Cursor For  
				Select name, xType  
				From #tmpColumnasPK 
				Order by ColId 
			open Llave_Cursor 
			Fetch From Llave_Cursor into @Campo, @Tipo     

			Set @Parte_Exists = 'Select ' + char(39) + 'If Not Exists ( Select * From [' + @Tabla + '] (NoLock)  Where ' + char(39) + ' + ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
            Set @Parte_Exists = 'Where ' 
            Set @Parte_Relacion_Tablas = '' 

			while @@Fetch_status = 0 
				begin               
					--Print @sCollate  + '  ' + cast(@tipo as varchar(100)) 
			        Set @query = @campo 
			        Set @query = @Alias_Destino  + @query + ' = ' + @Alias_Origen + @query + '' 
        							

					------------- Aplicar COLLATION  
						--If @tipo in ( 48, 52, 56, 59, 60, 62, 104, 106, 108, 122, 127 ) 
						If @Tipo in ( 99, 167, 175, 231,239, 241 ) --- Tipos de datos [ Caracter ] 
							begin -- char(13) + char(10) + char(39) +
								-- Set @Parte_Exists = char(39) + @campo + ' = ' + char(39) + ' + ' + 'ltrim(rtrim(convert(varchar(50), ' + @campo + '))) + ' 
								Set @query = @query + @sCollate   
							end 
						----Else 
						----	If @tipo in (61) 
						----		begin 
						----			-- Set @Parte_Exists = char(39) + @campo + ' = ' + char(39) + ' + char(39) + ' + 'ltrim(rtrim(convert(varchar(20), ' + @campo + ',120))) + char(39) + ' 
						----			Set @query = @query + @sCollate 
						----		end 
						----	Else 
						----		begin 
						----			-- Set @Parte_Exists = char(39) + @campo + ' = ' + char(39) + ' + char(39) + ' + 'ltrim(rtrim(' + @campo + ')) + char(39) + ' 
						----			Set @query = @query + @sCollate 
						----		end 

					--print @query   + '  ' + cast(@tipo as varchar(100))  
					------------- Aplicar COLLATION 


			        Fetch next From Llave_Cursor into @campo, @Tipo  
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
	

	Set @QueryFinal = 
		'Insert into #tmpColumnas ' + char(10) + 
		'Select  B.Name, B.ColId' + char(10) + 
		'From ' + char(10) +
		'( ' + char(10) + 
	    '	Select sc.name, Sc.ColId ' + char(10) +  
	    '	From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects as so ' + char(10) + 
	    '	Inner Join ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc on ( so.id = sc.id )  ' + char(10) + 
	    '	Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + char(10)  + 
		') B ' + char(10) + 
		'Inner Join ' + char(10) + 
		'( ' + char(10) +
	    '	Select sc.name, Sc.ColId ' + char(10) +  
	    '	From ' + @BaseDeDatosDestino + '.dbo.Sysobjects as so ' + char(10) + 
	    '	Inner Join ' + @BaseDeDatosDestino + '.dbo.Syscolumns as sc on ( so.id = sc.id )  ' + char(10) + 
	    '	Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + char(10)  + 
		') D On ( B.Name = D.Name '  + @sCollate +  ' and B.ColId = B.ColId ) ' + char(10) 
		

	if @ValidarEstructura_Origen =  1  
	Begin 
		Set @QueryFinal = @QueryFinal + char(13) + char(10) + 
			'Inner Join ' + char(10) + 
			'( ' + char(10) +
			'	Select sc.name, Sc.ColId ' + char(10) +  
			'	From ' + @BaseDeDatosOrigen + '.dbo.Sysobjects as so ' + char(10) + 
			'	Inner Join ' + @BaseDeDatosOrigen + '.dbo.Syscolumns as sc on ( so.id = sc.id )  ' + char(10) + 
			'	Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + char(10)  + 
			') E On ( B.Name = E.Name '  + @sCollate +  ' and B.ColId = B.ColId ) ' + char(10) 
	End 

	Set @QueryFinal = @QueryFinal + char(13) + char(10) + 'Order by B.ColId '  
	Exec(@QueryFinal) 	


	print @QueryFinal 

	

	Select top 0 sc.name, Sc.ColId    
	into #tmpColumnas_Aux  
	From Sysobjects as so 
	Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 	
	    'Insert into #tmpColumnas_Aux ' +  char(13) + 
	    'Select distinct sc.name, Sc.ColId   
	    From ' + @BaseDeDatosOrigen + '.dbo.Sysobjects as so 
	    Inner Join ' + @BaseDeDatosOrigen + '.dbo.Syscolumns as sc on ( so.id = sc.id )  
	    Inner Join ' + @BaseDeDatosDestino + '.dbo.Syscolumns as sce on ( sc.name = sce.name ' + @sCollate +  ' )  
	    Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + ' Order by Sc.ColId ' 
	Exec(@QueryFinal) 	
	--print @queryFinal 
		
	-- print '_____xxxxx' + @QueryFinal
--------------------------------------------- Obtener columnas   		
	
-------------------------------- FECHA DE CONTROL 	
    Set @sFiltro_FechaControl = '' 
    Set @sFechaControl_Desde = convert(varchar(10), dateadd(mm, -1 * @Meses_FechaControl, getdate()), 120) 
  
	If Exists ( Select * from #tmpColumnas_Aux Where name = 'FechaControl' ) 
	Begin  
		Set @iExiste_FechaControl = 1 
		Set @sFiltro_FechaControl =  ' convert(varchar(10), Bd_O.FechaControl, 120) >=  ' + char(39) + @sFechaControl_Desde + char(39) 
		Delete From #tmpColumnas Where name = 'FechaControl' 
    End 
    -- print @sFechaControl_Desde    
-------------------------------- FECHA DE CONTROL 	    	
	
	
	
---   spp_CFG_Script_Integrar_BaseDeDatos 					


	Set @iItems = 5 
	Set @iItems_Procesados = 0 


	Declare query_Cursor Cursor For 
	select name, cast( IsNull(( select top 1 1 from #tmpColumnasPK PK Where C.Name = PK.name ), 0 ) as bit)   
	from #tmpColumnas C  
	-- where not exists ( select * from #tmpColumnasPK PK Where C.Name = PK.name )   @EsPartePK
	Order by ColId
	open Query_cursor Fetch From Query_cursor into @campo, @EsPartePK 
	Set @query = '' 	
	Set @query = char(13) + char(10) + char(39) + ' Insert Into [' + @Tabla + '] ( ' -- + char(39) 
	
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
			Set @query = @Alias_Destino  + @campo + ' = ' + @Alias_Origen + @campo + ''			      		

			--print 'AQUI' 

            If @EsPartePK = 1 
               Begin 
                  Set @EsPartePK_Ant = @EsPartePK 
                  Set @query = '' 
               End 
			      		
							
			Fetch next From query_cursor into @campo, @EsPartePK  
			If @@Fetch_status = 0 
				begin 
				   Set @query = @query + ', ' 
		           Set @Campo_Aux = @Campo_Aux + ', ' 	               		           
				end 
			 Else 
				begin 	            				
					Set @query = @query + ' ' --- + char(13) + char(10)
			        Set @Campo_Aux = @Campo_Aux + ' ' 						
				end 
	

             If @EsPartePK_Ant = 1 
                Set @query = '' 
                   
             Set @EsPartePK_Ant =  @EsPartePK      			 
	         Set @QueryFinal = @QueryFinal + @Query 
	         Set @Parte_Lista_Campos = @Parte_Lista_Campos + @Campo_Aux 
	         
		end 
	close Query_cursor              
	deallocate Query_cursor 		
	

	Set @QueryFinal = ltrim(rtrim(@QueryFinal)) 
	if right(@QueryFinal, 1) = ',' 
	   Set @QueryFinal = left(@QueryFinal, len(@QueryFinal) -1) 


	--select 'POR AQUI', @Parte_Lista_Campos_Update
	 
----	print '' 
----	print '' 
----	print '' 
	
--------------------- Generar parte Update 
    Set @Parte_Update = '' 
    If @Update = 1 
        Begin 
            If @Tiene_Pk = 1 
            Begin 
            Set @Parte_Update = 'Set NoCount On ' + char(10) + 
                @QueryFinal + char(13) + 
	            'From ' + @BaseDeDatosDestino + @Esquema + '[' + @Tabla + '] ' + @Alias_Destino_Base + ' (NoLock) ' + char(13) + 
	            'Inner Join ' + @BaseDeDatosOrigen + @Esquema + '[' + @Tabla + '] ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  + 
	            'Where ' + @sFiltro_Actualizado + char(13) + char(10)   
	            
	            If @iExiste_FechaControl = 1 
	            Begin 
					Set @Parte_Update = @Parte_Update + ' And ' + @sFiltro_FechaControl  + char(13) + char(10) 
	            End 
	        End    
			
			If @Criterio <> '' 
			Begin 
				Set @Parte_Update = @Parte_Update + ' and ' + @Criterio  + char(13) + char(10) 
			End 			 
	    End 
	
    Set @Parte_Insersion = 'Set NoCount On ' + char(10) + 
        'Insert Into ' + @BaseDeDatosDestino + @Esquema + '[' +@Tabla + '] ( ' + @Parte_Lista_Campos + ' ) ' + char(13) +
        'Select ' + @Parte_Lista_Campos + char(13) + 
	    'From ' + @BaseDeDatosOrigen + @Esquema + '[' + @Tabla + '] ' + @Alias_Origen_Base + ' (NoLock) ' + char(13) 
	If @Tiene_Pk = 1     
	   Begin  
    	   Set @Parte_Insersion = @Parte_Insersion + 
	        'Where Not Exists ( Select * From ' + @BaseDeDatosDestino + @Esquema + '[' + @Tabla + '] ' + @Alias_Destino_Base + ' (NoLock) '  + char(13) +  
	        '   Where ' + @Parte_Relacion_Tablas + ' ) ' + 
	        ' and ' + @sFiltro_Actualizado 
	        
            If @iExiste_FechaControl = 1 
            Begin 
				Set @Parte_Insersion = @Parte_Insersion + ' And ' + @sFiltro_FechaControl  + char(13) + char(10) 
            End 	        
       End 
    

	If @Criterio <> '' 
	Begin 
		Set @Parte_Insersion = @Parte_Insersion + ' and ' + @Criterio  + char(13) + char(10) 
	End 

    Set @Parte_Insersion = @Parte_Insersion + char(13) + char(10)    
       

	           
	    -- 'From ' + @BaseDeDatosDestino + '..' + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13)  
	    -- 'Inner Join ' + @BaseDeDatosOrigen + '..' + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  	
	
-- @BaseDeDatosDestino + ' ' + @Alias_Origen_Base 	
----   spp_CFG_Script_Integrar_BaseDeDatos 

	
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
    print @SalidaFinal 
        

----	-- Ejecutar la salida Final 
----	Exec(@QueryFinal)
------  spp_CFG_Script_Integrar_BaseDeDatos 'CatEstados'

----    Print @QueryFinal 

	
End
Go--#SQL