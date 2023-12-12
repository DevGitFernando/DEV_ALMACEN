------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects Where Name = 'spp_CFG_Script_Integrar_BaseDeDatos' and xType = 'P' )
	Drop Proc spp_CFG_Script_Integrar_BaseDeDatos
Go--#SQL 

--- exec spp_CFG_Script_Integrar_BaseDeDatos 'SII_21_0271', 'SII_21_0253_TEPEACA', 'SII_21_0253_TEPEACA', 'CtlCortesParciales' 

Create Proc spp_CFG_Script_Integrar_BaseDeDatos 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_11_0003__CEDIS_GTO', 
    @BaseDeDatosDestino varchar(100) = 'SII_Regional_Guanajuato_2K131125', 
	@BaseDeDatosEstructura varchar(100) = 'SII_Regional_Guanajuato_2K131125', 	     
    @Tabla varchar(100) = 'VentasEnc', @SalidaFinal varchar(max) = '' output, 
    @Update bit = true, @SoloDiferencias bit = true, 
    @Meses_FechaControl int = 2   
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
	@sFechaControl_Desde varchar(max)    		
	

--	@ParteUpDate bit 

-- Declare @sValorActualizado varchar(2)
--	Set @sValorActualizado = '1'

-- Declare @Criterio varchar(8000), @ParteUpDate bit 

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

			Select top 0 sc.name, Sc.ColId  
			into #tmpColumnasPK 
			From Syscolumns as sc 
			Inner Join sysindexkeys as si on ( sc.id = si.id and sc.colid = si.colid) 
			Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
			    and Sc.status <> 128 -- Ignorar las columnas Identidad
			Order by Sc.ColId
			
			Set @QueryFinal = 'Set NoCount On ' + char(10) + 
			    'Insert into #tmpColumnasPK ' + char(13) +  
			    'Select distinct sc.name, Sc.ColId   
			    From ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc 
			    Inner Join ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys as si on ( sc.id = si.id and sc.colid = si.colid ) 
				Inner Join ' + @BaseDeDatosDestino + '.dbo.Syscolumns as sce on ( sc.name = sce.name  )  
			    Where sc.id = ' + cast(@Padre as varchar) + ' and si.indid = (Select min(indid) From ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys Where id = ' + cast(@Padre as varchar) + ' ) 
			        and Sc.status <> 128 
			    Order by Sc.ColId ' 
			Exec(@QueryFinal) 
--			print @QueryFinal 

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
	    'Insert into #tmpColumnas ' + char(13) + 
	    'Select distinct sc.name, Sc.ColId   
	    From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects as so 
	    Inner Join ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc on ( so.id = sc.id  )  
	    Inner Join ' + @BaseDeDatosDestino + '.dbo.Syscolumns as sce on ( sc.name = sce.name  )  
	    Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + ' Order by Sc.ColId ' 
	Exec(@QueryFinal) 	


	
	Select top 0 sc.name, Sc.ColId    
	into #tmpColumnas_Aux  
	From Sysobjects as so 
	Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 	
	    'Insert into #tmpColumnas_Aux ' +  char(13) + 
	    'Select distinct sc.name, Sc.ColId   
	    From ' + @BaseDeDatosOrigen + '.dbo.Sysobjects as so 
	    Inner Join ' + @BaseDeDatosOrigen + '.dbo.Syscolumns as sc on (so.id = sc.id)  
	    Inner Join ' + @BaseDeDatosDestino + '.dbo.Syscolumns as sce on ( sc.name = sce.name  )  
	    Where so.name = ' + char(39) + @Tabla + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + ' Order by Sc.ColId ' 
	Exec(@QueryFinal) 	
		
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
	
	
----	select name, cast( IsNull(( select 1 from #tmpColumnasPK PK Where C.Name = PK.name ), 0 ) as bit)   
----	from #tmpColumnas C  
----	
		

----	Declare query_Cursor Cursor For 
----		Select sc.name From Sysobjects as so 
----		Inner Join Syscolumns as sc on (so.id = sc.id) 
----	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad
	Declare query_Cursor Cursor For 
	select name, cast( IsNull(( select top 1 1 from #tmpColumnasPK PK Where C.Name = PK.name ), 0 ) as bit)   
	from #tmpColumnas C  
	-- where not exists ( select * from #tmpColumnasPK PK Where C.Name = PK.name )   @EsPartePK
	Order by ColId
	open Query_cursor Fetch From Query_cursor into @campo, @EsPartePK 
	Set @query = '' 	
	Set @query = char(13) + char(10) + char(39) + ' Insert Into ' + @Tabla + ' ( ' -- + char(39) 
	
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
			      		
            If @EsPartePK = 1 
               Begin 
                  Set @EsPartePK_Ant = @EsPartePK 
                  Set @query = '' 
               End 
			      		
----			If @EsPartePK = 0 
----			   Begin 
--------			      Set @query = @campo 
--------			      Set @Campo_Aux = @campo   			   
----			      Set @Parte_Lista_Campos_Update = @Alias_Destino  + @campo + ' = ' + @Alias_Origen + @campo + '' 
----			   End 	
							
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
	           
---------- Implementar Esquema 
	--select @QueryFinal 

    Set @Parte_Update = '' 
    If @Update = 1 
        Begin 
            If @Tiene_Pk = 1 
            Begin 
            Set @Parte_Update = 'Set NoCount On ' + char(10) + 
                @QueryFinal + char(13) + 
	            'From ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13) + 
	            'Inner Join ' + @BaseDeDatosOrigen + @Esquema + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  + 
	            'Where ' + @sFiltro_Actualizado + char(13) + char(10)   
	            
	            If @iExiste_FechaControl = 1 
	            Begin 
					Set @Parte_Update = @Parte_Update + ' And ' + @sFiltro_FechaControl  + char(13) + char(10) 
	            End 
	        End     
	    End 
	
    Set @Parte_Insersion = 'Set NoCount On ' + char(10) + 
        'Insert Into ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ( ' + @Parte_Lista_Campos + ' ) ' + char(13) +
        'Select ' + @Parte_Lista_Campos + char(13) + 
	    'From ' + @BaseDeDatosOrigen + @Esquema + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) ' + char(13) 
	If @Tiene_Pk = 1     
	   Begin  
    	   Set @Parte_Insersion = @Parte_Insersion + 
	        'Where Not Exists ( Select * From ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) '  + char(13) +  
	        '   Where ' + @Parte_Relacion_Tablas + ' ) ' + 
	        ' and ' + @sFiltro_Actualizado 
	        
            If @iExiste_FechaControl = 1 
            Begin 
				Set @Parte_Insersion = @Parte_Insersion + ' And ' + @sFiltro_FechaControl  + char(13) + char(10) 
            End 	        
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


	if ('Ctl_Replicaciones' = @Tabla) 
	Begin

		Declare
			@FechaInicio Varchar(200) = Convert(Varchar(10), DateAdd(mm, @Meses_FechaControl * -1, GetDate()), 120),
			@FechaFin Varchar(10) = Convert(Varchar(10), GetDate(), 120)


		Exec spp_CrearStor_Ctl_Replicaciones @BaseDeDatosOrigen = @BaseDeDatosOrigen, @BaseDeDatosDestino = @BaseDeDatosDestino,
							@FechaInicio = @FechaInicio, @FechaFin = @FechaFin,
							@VersionExe = 'xxxx', @SalidaFinal = @SalidaFinal output
		
	End

    print @SalidaFinal 
        

----	-- Ejecutar la salida Final 
----	Exec(@QueryFinal)
------  spp_CFG_Script_Integrar_BaseDeDatos 'CatEstados'

----    Print @QueryFinal 

	
End
Go--#SQL