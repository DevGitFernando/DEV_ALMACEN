If Exists ( Select Name From Sysobjects Where Name = 'spp_CFG_ObtenerDatos' and xType = 'P' )
	Drop Proc spp_CFG_ObtenerDatos
Go--#SQL  

--		Exec spp_CFG_ObtenerDatos 'CatEmpresasEstados'   
--		Exec spp_CFG_ObtenerDatos @Tabla = 'CatEmpresasEstados', @Criterio = [ Where Actualizado in ( 0, 1) ], @sValorActualizado = '0', @IdEstado = '11'  

Create Proc spp_CFG_ObtenerDatos 
( 
	@Tabla varchar(max) = 'CatMunicipios', @Criterio varchar(max) = '',  
	@sValorActualizado varchar(2) = '0', @CriterioAux varchar(max) = '', @ParteUpDate int = 1, 
	@IdEstado varchar(2) = '21', @Alias varchar(max) = '', @Ejecutar int = 1, @Salida varchar(max) = '' output     
) 
with Encryption 
As 
Begin  

Declare -- @Tabla varchar(50), 
	@QueryFinal varchar(max), 
	@QueryFinalAux varchar (max), 			
	@QueryFinal_Update varchar (max), 	
	@Query varchar(max),
	@Campo varchar(max), 
	@ListaCampos varchar(max), 	
	@ListaCamposInserts varchar(max), 			
	@Tipo tinyint, 
	@Padre varchar(max), 
	@Tiene_Pk bit, 
	@Parte_Exists varchar(max),
	@bParamVal int, @sMsj varchar(max), 
	@sCriterio_Estado varchar(max), 
	@Alias_Tabla_Aux varchar(max),
	@FarmaciasFinal Varchar(Max) = '',
	@Farmacias Varchar(20) = '',
	@Registros Int = 0,
	@bFiltroFarmacia Bit = 0
--	@ParteUpDate bit 

-- Declare @sValorActualizado varchar(2)
--	Set @sValorActualizado = '1'

-- Declare @Criterio varchar(8000), @ParteUpDate bit

	Set @Alias_Tabla_Aux = @Alias 
	If @Alias <> '' 
	   Set @Alias_Tabla_Aux = @Alias + '.' 


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

--- Set @Tabla = 'OrdenCompra_Det'
--- Inicia proceso

	-- Buscar si la tabla tiene llave primaria              
	Set @Padre = ( Select id From Sysobjects (NoLock) Where name = @Tabla and xtype = 'U' )

	If Exists ( Select so1.name From Sysobjects as so1 Where so1.xtype = 'PK' and so1.parent_obj = @Padre ) 
		Set @Tiene_Pk = 1 
	Else 
		Set @Tiene_Pk = 0 

	-- Select @Tiene_Pk
	Set @QueryFinal = '' 
	Set @QueryFinal_Update = '' 
	-- Set @Criterio = ''
	Set @Parte_Exists = ''
	Set @Campo = ''
	Set @Tipo = 0 
	Set @ListaCampos = '' 
	Set @ListaCamposInserts = '' 	
	Set @sCriterio_Estado = '' 
	
	---	print 'PK   ' + cast(@Tiene_Pk as varchar) 
	
	Set @Criterio = @Criterio + ' ' + @CriterioAux 
	Set @CriterioAux = '' 
	
----- Este Parametro se envia dependiendo el tipo de Tabla 
--	Set @ParteUpDate = 1

	If @Tiene_Pk = 1 
		begin 
			Declare Llave_Cursor Cursor For 
			Select sc.name, sc.xtype From Syscolumns as sc 
				Inner Join sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
				Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
					and Sc.status <> 128 -- Ignorar las columnas Identidad
					Order by Sc.ColId
			open Llave_Cursor 
			Fetch From Llave_Cursor into @Campo, @tipo 

			Set @Parte_Exists = 'Select ' + char(39) + 'If Not Exists ( Select * From ' + @Tabla + ' (NoLock)  Where ' + char(39) + ' + ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
			Set @QueryFinal_Update = '' + char(39) + 'Update ' + @Tabla + ' Set Actualizado = 1 Where ' + char(39) + ' + '

			while @@Fetch_status = 0 
				begin               
					Set @Parte_Exists = ''          
						If @tipo in ( 48, 52, 56, 59, 60, 62, 104, 106, 108, 122, 127 ) 
							begin -- char(13) + char(10) + char(39) +
								Set @Parte_Exists = char(39) + @campo + ' = ' + char(39) + ' + ' + 'ltrim(rtrim(convert(varchar(50), ' + @campo + '))) + ' 
							end 
						Else 
							If @tipo in (61) 
								begin 
									Set @Parte_Exists = char(39) + @campo + ' = ' + char(39) + ' + char(39) + ' + 'ltrim(rtrim(convert(varchar(20), ' + @campo + ',120))) + char(39) + ' 
								end 
							Else 
								begin 
									Set @Parte_Exists = char(39) + @campo + ' = ' + char(39) + ' + char(39) + ' + 'ltrim(rtrim(' + @campo + ')) + char(39) + ' 
								end 
					
					Set @QueryFinal = @QueryFinal + @Parte_Exists  
					Set @QueryFinal_Update = @QueryFinal_Update + @Parte_Exists 
					
					--- 
					Fetch next From Llave_Cursor into @Campo, @tipo 
				    If @@Fetch_status = 0 
				    begin 
						Set @QueryFinal = @QueryFinal + char(39) + ' and ' + char(39) + ' + ' 
						Set @QueryFinal_Update = @QueryFinal_Update + char(39) + ' and ' + char(39) + ' + ' 
			        end 
				    Else -----*****  
				    Begin 
						Set @QueryFinal = @QueryFinal  + char(39) + ' ) ' + char(39) + ' + ' 	
						Set @QueryFinal_Update = @QueryFinal_Update + char(39) + char(39) 
					End 	
				end              
			
			close Llave_cursor              
			deallocate Llave_cursor 
			
--			print 	@QueryFinal 
--			print 	@QueryFinal_Update 
		end
	Else
		begin 
			Set @Parte_Exists = 'Select ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
		end 
	
	-- 2K9-0709.0828 
	-- Barrer todos los campos de la tabla
	Declare query_Cursor Cursor For 
		Select sc.name From Sysobjects as so 
		Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad
	open Query_cursor Fetch From Query_cursor into @campo --, @tipo              
	Set @query = '' 	
	Set @query = char(13) + char(10) + char(39) + ' Insert Into ' + @Tabla + ' ( ' -- + char(39) 
	-- Set @query = char(13) + char(10) + ' Insert Into ' + @Tabla + ' ( ' -- + char(39) 
	Set @QueryFinal = @QueryFinal + @Query 
	while @@Fetch_status = 0 
		begin      
			Set @query = @campo 
		
			Fetch next From query_cursor into @campo -- , @tipo 
			If @@Fetch_status = 0 
				begin 
					Set @query = @query + ', ' 
				end 
			 Else 
				begin 
					Set @query = @query + ' ) ' + char(39) + char(13) + char(10)
				end 
			 Set @QueryFinal = @QueryFinal + @Query 
		end 
	close Query_cursor              
	deallocate Query_cursor 		
	-- print @QueryFinal 		
			
	-- Barrer todos los campos de la tabla
	Declare query_Cursor Cursor For 
		Select sc.name, sc.xtype From Sysobjects as so 
		Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad
	open Query_cursor Fetch From Query_cursor into @campo, @tipo              
	Set @query = '' 
	-- Set @query = char(13) + char(10) + char(39) + ' Insert Into ' + @Tabla + ' Values ( ' + char(39) 
	Set @query = char(13) + char(10) + char(39) + ' Values ( ' + char(39) 	
	Set @QueryFinal = @QueryFinal + ' + ' + @Query 
	while @@Fetch_status = 0 
		begin      
			If @tipo in ( 48, 52, 56, 59, 60, 62, 104, 106, 108, 122, 127 )
				Set @query = ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else ltrim(rtrim(convert(varchar(50), ' + case when @campo = 'actualizado' then @sValorActualizado else @campo end + '))) End ' 
			Else 
				If @tipo in (61) 
					begin 
						Set @query = ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'null' + char(39) + ' Else char(39) + ltrim(rtrim(convert(char(20), '  + @campo + ',120))) + char(39) End ' 
					end 
				Else 
					If @tipo in (34) 
						begin 
							Set @query = ' + ' +  char(39) + 'Null' + char(39) 
						end 
					Else 
						begin 
							Set @query = ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else char(39) + ltrim(rtrim( ' + @campo + ')) + char(39) End ' 
						end 
			
			Fetch next From query_cursor into @campo, @tipo 
			If @@Fetch_status = 0 
				begin 
					Set @query = @query + ' + ' + char(39) + ', ' + char(39) 
				end 
			 Else 
				begin 
					Set @query = @query + ' + ' + char(39) + ' ) ' + char(39) + char(13) + char(10)
				end 
			 Set @QueryFinal = @QueryFinal + char(13) + char(10) + @Query 
		end 
	close Query_cursor              
	deallocate Query_cursor


	--- Revisar si tiene PK y se solicito la parte Update
	If @ParteUpDate = 1 and @Tiene_Pk = 1  
	Begin
		Declare query_Cursor Cursor For 
		Select sc.Name, sc.Xtype From Sysobjects As so                       
		Inner Join Syscolumns As sc On ( so.Id = sc.Id )                      
		Where so.Name=@Tabla and so.Xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad -- 2K9-0709.0828 
		and sc.Name not in ( Select sc.Name           
								From Syscolumns As sc                       
								Inner Join sysIndexKeys As si On ( sc.Id = si.Id and sc.colId = si.colId)                      
								Inner Join SysTypes As st On ( sc.Xtype = st.Xtype and sc.Xtype = st.xusertype)          
								Where sc.Id = @Padre                
								and si.indId= (Select Min(indId) From sysIndexKeys Where Id = @Padre) ) 
		open Query_cursor 
		Fetch From Query_cursor into @campo, @tipo 
		Set @query = '' 
		Set @query = ' + char(13) + char(10) ' + ' + ' + char(39) +  ' Else Update ' + @Tabla + ' Set ' + char(39) 
		Set @QueryFinal = @QueryFinal + @Query 

		while @@Fetch_status = 0              
		begin               
			 If @tipo in ( 48, 52, 56, 59, 60, 62, 104, 106, 108, 122, 127 ) 
			 	Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else Ltrim(Rtrim(convert(varchar(50),' + case when @campo = 'actualizado' then @sValorActualizado else @campo end + '))) End ' 
			 Else              
			 	If @tipo in (61) 
			 		Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'null' + char(39) + ' Else char(39) + Ltrim(Rtrim(convert(char(20),'  + @campo + ',120))) + char(39) End ' 
			 	Else 
			 		If @tipo in (34) 
			 			Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + ' +  char(39) + 'Null' + char(39) 			               
			 		Else 
			 			Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else char(39) + Ltrim(Rtrim( ' + @campo + ')) + char(39) End ' 		                
		              
			Fetch next From query_cursor into @campo,@tipo               
			If @@Fetch_status = 0 
				Set @query = @query + ' + ' + char(39) + ', ' + char(39) 

			Set @QueryFinal = @QueryFinal + char(13) + char(10) + @Query 
		end            
		close Query_cursor 
		deallocate Query_cursor  
		  
		Declare Llave_Cursor Cursor For 
		Select sc.name, sc.xtype From Syscolumns as sc 
			Inner Join sysindexkeys as si on ( sc.id = si.id and sc.colid = si.colid ) 
			Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre )  		              
		open Llave_cursor 
		Fetch From Llave_cursor into @campo, @tipo 
		Set @query = '' 
		Set @query = ' + ' + char(39) +  ' Where ' + char(39) 
		Set @QueryFinal=@QueryFinal + @Query 

		while @@Fetch_status = 0 
		begin               
			If @tipo in ( 48, 52, 56, 59, 60, 62, 104, 106, 108, 122, 127 ) 
				Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else Ltrim(Rtrim(convert(varchar(50),' + @campo + '))) End ' 		              
			Else 
				If @tipo in (61) 
					Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else char(39) + Ltrim(Rtrim(convert(char(20),'  + @campo + ',120))) + char(39) End '                 
				Else 
					If @tipo in (34) 
						Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + ' +  char(39) + 'Null' + char(39) 
					Else 
						Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else char(39) + Ltrim(Rtrim( ' + @campo + ')) + char(39) End ' 
		              
			Fetch next From Llave_cursor into @campo, @tipo 
			If @@Fetch_status = 0 
				Set @query=@query + ' + ' + char(39) + ' and ' + char(39) 

			Set @QueryFinal=@QueryFinal + char(13) + char(10) + @Query 
		end 
		close Llave_cursor 
		deallocate Llave_cursor 
	End -- Fin del If del la parte update

	
	Set @sCriterio_Estado = '' 
	If @IdEstado <> '' 
	Begin 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFG_Replicacion_Filtro_Estado' ) 
		Begin 	
			If Exists ( Select * From CFG_Replicacion_Filtro_Estado (NoLock) Where NombreTabla = @Tabla ) 
			Begin 
				If Exists ( 
					Select  * 
					From sysobjects so (nolock) 
					Inner Join syscolumns sc (nolock) On ( So.Id = Sc.Id ) 
					Where So.Name = @Tabla and Sc.Name = 'IdEstado' ) 
				Begin 
					Set @Criterio = ltrim(rtrim(@Criterio)) 
					
					If @Criterio <> '' 
					   Set @sCriterio_Estado = ' and IdEstado = ' + char(39) + @IdEstado + char(39)  
					else 
					   Set @sCriterio_Estado = ' Where IdEstado = ' + char(39) + @IdEstado + char(39)

					If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGS_EnvioCatalogos_Unidades' ) 
					Begin
						If Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
						Where So.Name = @Tabla and Sc.Name = 'IdFarmacia' )  
							Begin
								Set @bFiltroFarmacia = 1
								Set @FarmaciasFinal = ' And IdFarmacia In ('
							End
						If Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
						Where So.Name = @Tabla and Sc.Name = 'IdSucursal' )  
							Begin
								Set @bFiltroFarmacia = 1
								Set @FarmaciasFinal = ' And IdSucursal In ('
							End
					End
					
					If (@bFiltroFarmacia = 1)
					Begin
						Select IdFarmacia
						Into #Farmacia
						From CFGS_EnvioCatalogos_Unidades (NoLock)
						Where IdEstado = @IdEstado And Status = 'A'

						Declare Llave_Cursor Cursor For 
							Select IdFarmacia
							From #Farmacia
						open Llave_Cursor
						Fetch From Llave_Cursor into @Campo
						--Set @FarmaciasFinal = ' And IdFarmacia In ('
						while @@Fetch_status = 0 
							begin				
									Set @Farmacias = Char(39) + @campo + Char(39)
			
									Fetch next From Llave_Cursor into @campo -- , @tipo 
									If @@Fetch_status = 0 
										begin 
											Set @Farmacias = @Farmacias + ', ' 
										end 
									 Else 
										begin 
											Set @Farmacias = @Farmacias + ' ) ' --+ char(13) + char(10)
										end

									 Set @FarmaciasFinal = @FarmaciasFinal + @Farmacias 
			 
							end			
						close Llave_cursor              
						deallocate Llave_cursor

						Select @Registros = Count(*) From #Farmacia

						 If (@Registros = 0 )
						 Begin
							Set @FarmaciasFinal = ''
						 End
						 Set @sCriterio_Estado = @sCriterio_Estado + @FarmaciasFinal
					End
				End 
			End 
		End 
	End
	
--		Exec spp_CFG_ObtenerDatos @Tabla = 'CatEmpresasEstados', @Criterio = [ Where Actualizado in ( 0, 1) ], @sValorActualizado = '0', @IdEstado = '11'  	

--		Exec spp_CFG_ObtenerDatos @Tabla = 'CatEmpresasEstados', @Criterio = [  ], @sValorActualizado = '0', @IdEstado = '11'  

	
------ Ejecutar la salida Final  
------ Agregar la tabla al query, agregar nombre a la columna de salida 
/* 	    
	Exec(	@QueryFinal + ' As Resultado, ' + 
	    @QueryFinal_Update + ' as ResultadoUpdate ' +   
	    ' From ' + @Tabla + ' ' + @Alias + ' (NoLock) ' + @Criterio + ' ' + @CriterioAux + ' ' + @sCriterio_Estado  ) 	
*/ 


	Set @QueryFinal = 
		@QueryFinal + ' As Resultado, ' + 
	    @QueryFinal_Update + ' as ResultadoUpdate ' +  char(13) + 
	    ' From ' + @Tabla + ' ' + @Alias + ' (NoLock) ' + @Criterio + ' ' + @CriterioAux + ' ' + @sCriterio_Estado  
	Set @Salida = @QueryFinal 
		
	if @Ejecutar = 1 
	Begin 
	Exec ( @QueryFinal ) 
	End 


---- Jesus Diaz 2K120111.1800 
	--Print @IdEstado 
	print @QueryFinal 
	
--		spp_CFG_ObtenerDatos 'CatEstados'  	    
	    
	
	--print 	@QueryFinal 
	-- print 	@QueryFinal_Update 	
	
	-- Generar la salida final
	-- Select @QueryFinal as Salida
	-- Print @QueryFinal 

	--If @Ejecutar = 1
	--	Exec(@QueryFinal)

------ Ejecutar la salida Final 
--	Exec(@QueryFinal) 
--	Print @QueryFinal 

--		spp_CFG_ObtenerDatos 'CatEstados'  
	
End
Go--#SQL  
