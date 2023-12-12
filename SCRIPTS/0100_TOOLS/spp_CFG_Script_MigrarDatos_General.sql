If Exists ( Select Name From Sysobjects Where Name = 'spp_CFG_Script_MigrarDatos_General' and xType = 'P' )
	Drop Proc spp_CFG_Script_MigrarDatos_General
Go--#SQL

Create Proc spp_CFG_Script_MigrarDatos_General 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_PtoVta_EnBlanco', 
    @BaseDeDatosDestino varchar(100) = 'SII_OficinaCentral_Test', 
	@BaseDeDatosEstructura varchar(100) = 'SII_PtoVta_EnBlanco', 	     
    @Tabla varchar(50) = 'CatEmpresas', @SalidaFinal varchar(8000) = '' output, @Update bit = true    
)
with Encryption 
As
Begin 
Set NoCount On 
Declare -- @Tabla varchar(50), 
	@QueryFinal varchar (8000),
	@Query varchar(8000), 
	@Campo varchar(200), 
	@Campo_Aux varchar(200), 	
	@Tipo tinyint, 
	@Padre varchar(80), 
	@Tiene_Pk bit, 
	@Parte_Exists varchar(2000),
	@Parte_Relacion_Tablas varchar(2000),	
	@Parte_Lista_Campos varchar(2000),		
	@Parte_Update varchar(8000), 
	@Parte_Insersion varchar(8000), 	
	@bParamVal int, @sMsj varchar(1000),    
	@Alias_Origen varchar(10),
	@Alias_Destino varchar(10),	
	@Alias_Origen_Base varchar(10),
	@Alias_Destino_Base varchar(10), 
	@Esquema varchar(100) 		
	

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
    Set @Parte_Insersion = '' 
    Set @Campo_Aux = '' 
    Set @Alias_Origen_Base = 'Bd_O'
    Set @Alias_Destino_Base = 'Bd_D' 

	Set @Esquema = '.dbo.'
    Set @Alias_Origen = 'Bd_O.'
    Set @Alias_Destino = 'Bd_D.' 
    

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
			Inner Join sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
			Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
			    and Sc.status <> 128 -- Ignorar las columnas Identidad
			Order by Sc.ColId
			
			Set @QueryFinal = 'Set NoCount On ' + char(10) + 
			    'Insert into #tmpColumnasPK ' +  
			    ' Select sc.name, Sc.ColId   
			    From ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc 
			    Inner Join ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
			    Where sc.id = ' + cast(@Padre as varchar) + ' and si.indid = (Select min(indid) From ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys Where id = ' + cast(@Padre as varchar) + ' ) 
			        and Sc.status <> 128 
			    Order by Sc.ColId ' 
			Exec(@QueryFinal) 
--			print @QueryFinal 

---  spp_CFG_MigrarDatos_Catalogos 
            
---     spp_CFG_Script_MigrarDatos_General 
			
--			select * from #tmpColumnasPK 
----			print @QueryFinal 
----			print ''
----			print ''
----			print ''						

---   spp_CFG_Script_MigrarDatos_General 					

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
	-- print '_____xxxxx' + @QueryFinal
	
---   spp_CFG_Script_MigrarDatos_General 					
	

----	Declare query_Cursor Cursor For 
----		Select sc.name From Sysobjects as so 
----		Inner Join Syscolumns as sc on (so.id = sc.id) 
----	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad
	Declare query_Cursor Cursor For 
	select name 
	from #tmpColumnas  C 
	where not exists ( select * from #tmpColumnasPK PK Where C.Name = PK.name ) 	
	Order by ColId
	open Query_cursor Fetch From Query_cursor into @campo --, @tipo              
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
			Set @query = @Alias_Destino  + @query + ' = ' + @Alias_Origen + @query + '' 
							
			Fetch next From query_cursor into @campo -- , @tipo 
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
			 Set @QueryFinal = @QueryFinal + @Query 
			 Set @Parte_Lista_Campos = @Parte_Lista_Campos + @Campo_Aux 
		end 
	close Query_cursor              
	deallocate Query_cursor 		
	
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
            Set @Parte_Update = 'Set NoCount On ' + char(10) + 
                @QueryFinal + char(13) + 
	            'From ' + @BaseDeDatosDestino + @Esquema + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13) + 
	            'Inner Join ' + @BaseDeDatosOrigen + @Esquema + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  
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
	        '   Where ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  + char(13) + char(10)  	           
       End 

	           
	    -- 'From ' + @BaseDeDatosDestino + '..' + @Tabla + ' ' + @Alias_Destino_Base + ' (NoLock) ' + char(13)  
	    -- 'Inner Join ' + @BaseDeDatosOrigen + '..' + @Tabla + ' ' + @Alias_Origen_Base + ' (NoLock) On ( ' + @Parte_Relacion_Tablas + ' ) ' + char(13) + char(10)  	
	
-- @BaseDeDatosDestino + ' ' + @Alias_Origen_Base 	
----   spp_CFG_Script_MigrarDatos_General 

	
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
--    print @SalidaFinal 
        

----	-- Ejecutar la salida Final 
----	Exec(@QueryFinal)
------  spp_CFG_Script_MigrarDatos_General 'CatEstados'

----    Print @QueryFinal 

	
End
Go--#SQL