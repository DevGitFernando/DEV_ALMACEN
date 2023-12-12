
-- Store_GeneraQueryInserts2 OrdenCompra_Det 
If Exists ( Select * From Sysobjects Where Name = 'sp_GeneraInserts' and xType = 'P' )
	Drop Proc sp_GeneraInserts
Go--#SQL  

Create Proc sp_GeneraInserts ( @Tabla varchar(80) = '', @Ejecutar int = 0, @Criterio varchar(8000) = '', @ParteUpDate bit = 0 )
With Encryption 
As
Begin

Declare -- @Tabla varchar(50),
	@QueryFinal varchar(max), 
	@QueryFinalAux varchar(max), 		
	@Query varchar(max),
	@Campo varchar(max), 
	@CampoAux varchar(max), 	
	@ListaCampos varchar(max), 	
	@ListaCamposInserts varchar(max), 		
	@Tipo tinyint, 
	@Padre varchar(80), 
	@Tiene_Pk bit, 
	@Parte_Exists varchar(max),
	@bParamVal int, @sMsj varchar(max) 

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
	Set @Criterio = ''
	Set @Parte_Exists = ''
	Set @Campo = ''
	Set @Tipo = 0
	Set @ParteUpDate = 1
	Set @ListaCampos = '' 
	Set @ListaCamposInserts = '' 
	
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

			Set @Parte_Exists = 'Select ' + char(39) + 'If Not Exists ( Select * From ' + @Tabla + ' Where ' + char(39) + ' + ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 

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
					
					---
					Fetch next From Llave_Cursor into @Campo, @tipo 
				    If @@Fetch_status = 0 
						Set @QueryFinal = @QueryFinal + char(39) + ' and ' + char(39) + ' + ' 
				    Else -----*****             
						Set @QueryFinal = @QueryFinal  + char(39) + ' ) ' + char(39) + ' + ' 
				end              
			
			close Llave_cursor              
			deallocate Llave_cursor    
			
			Set @QueryFinal = @QueryFinal + ' '            
		end
	Else
		begin 
			Set @Parte_Exists = 'Select ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
		end 
	
	-- Barrer todos los campos de la tabla
	Declare query_Cursor Cursor For 
		Select sc.name, sc.xtype From Sysobjects as so 
		Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad 
	Order by Sc.ColId
	open Query_cursor Fetch From Query_cursor into @campo, @tipo              
	Set @query = '' 
	Set @ListaCampos = '' 
	Set @ListaCamposInserts = ' ' 
		
	Set @query = char(13) + char(10) + char(39) + ' Insert Into ' + @Tabla + ' Values ( ' + char(39) 
    Set @QueryFinalAux = ' ' 
	Set @QueryFinal = @QueryFinal  -- + @Query 
	while @@Fetch_status = 0 
		begin      
			Set @ListaCamposInserts = @ListaCamposInserts + @Campo 		    
			-- Set @CampoAux = @Campo 
			
			If @tipo in ( 48, 52, 56, 59, 60, 62, 104, 106, 108, 122, 127 )
				Set @query = ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else ltrim(rtrim(convert(varchar(50), ' + @campo + '))) End ' 
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
			-- print @CampoAux 
			Fetch next From query_cursor into @campo, @tipo 
			Set @CampoAux = @Campo 	
			If @@Fetch_status = 0 
				begin 
				    Set @ListaCamposInserts = @ListaCamposInserts + ', ' 
				    -- Set @ListaCamposInserts = @ListaCamposInserts + @CampoAux + ', ' 				
					Set @query = @query + ' + ' + char(39) + ', ' + char(39) 
				end 
			 Else 
				begin 
				    -- Set @ListaCamposInserts = @ListaCamposInserts + ' ' 									
					-- Set @query = @query + ' + ' + char(39) + ' ' + char(39) + char(13) + char(10)
					Set @query = @query + ' + ' + '' + char(13) + char(10)					
				end 
			 Set @QueryFinalAux = @QueryFinalAux + char(13) + char(10) + @Query 
			 -- Set @QueryFinal = @QueryFinal + char(13) + char(10) + @Query 			 
		end 
	close Query_cursor              
	deallocate Query_cursor

--    print @QueryFinalAux 
--    print @ListaCamposInserts 

--    Print @ListaCamposInserts 
--    Print @QueryFinal 
--    Print @QueryFinalAux 
	Set @query = char(13) + char(10) + char(39) + ' Insert Into ' + @Tabla + ' ( ' + @ListaCamposInserts + ' ) ' + ' Values ( ' + char(39) + @QueryFinalAux + char(39) + ' ) ' + char(39)  
    Set @QueryFinal = @QueryFinal + @Query
    -- Print @QueryFinal 
    
	--- Revisar si tiene PK y se solicito la parte Update
	If @ParteUpDate = 1 and @Tiene_Pk = 1 
	Begin
		Declare query_Cursor Cursor For 
		Select sc.Name, sc.Xtype From Sysobjects As so                       
		Inner Join Syscolumns As sc On ( so.Id = sc.Id )                      
		Where so.Name=@Tabla and so.Xtype = 'U' and Sc.status <> 128
		and sc.Name not in ( Select sc.Name           
								From Syscolumns As sc                       
								Inner Join sysIndexKeys As si On ( sc.Id = si.Id and sc.colId = si.colId)                      
								Inner Join SysTypes As st On ( sc.Xtype = st.Xtype and sc.Xtype = st.xusertype)          
								Where sc.Id = @Padre                
								and si.indId= (Select Min(indId) From sysIndexKeys Where Id = @Padre) ) 
		open Query_cursor 
		Fetch From Query_cursor into @campo, @tipo 
		Set @query = '' 
		--Set @query = ' + char(13) + char(10) ' + ' + ' + char(39) +  ' Else Update ' + @Tabla + ' Set ' + char(39) 
		Set @query = ' ' + ' + ' + char(39) +  ' Else Update ' + @Tabla + ' Set ' + char(39) 
		Set @QueryFinal = @QueryFinal + @Query 

		while @@Fetch_status = 0              
		begin               
			 If @tipo in ( 48, 52, 56, 59, 60, 62, 104, 106, 108, 122, 127 ) 
			 	Set @query = ' + ' + char(39) + @campo + ' = ' + char(39) + ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else Ltrim(Rtrim(convert(varchar(50),' + @campo + '))) End ' 
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

	-- Agregar la tabla al query
	Set @QueryFinal = @QueryFinal + ' + space(2)  ' + char(13) + char(10) + 'From ' + @Tabla + ' (NoLock) ' + @Criterio
	
	-- Generar la salida final
	Select @QueryFinal as Salida

	If @Ejecutar = 1 
		Exec(@QueryFinal) 

End
Go--#SQL 