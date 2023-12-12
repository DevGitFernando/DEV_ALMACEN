
-- Store_GeneraQueryInserts2 OrdenCompra_Det 
If Exists ( Select * From Sysobjects Where Name = 'sp_FormatearTabla' and xType = 'P' )
	Drop Proc sp_FormatearTabla
Go--#SQL  

Create Proc sp_FormatearTabla ( @Tabla varchar(200) = '', @Ejecutar int = 1 )
With Encryption 
As
Begin 

Declare -- @Tabla varchar(50), 
	@QueryFinal varchar (max), 
	@QueryFinalAux varchar (max), 		
	@Query varchar(max),
	@Campo varchar(max), 
	@CampoAux varchar(max), 	
	@ListaCampos varchar(max), 	
	@ListaCamposInserts varchar(max), 		
	@Tipo tinyint, 
	@Padre varchar(max), 
	@Tiene_Pk bit, 
	@Parte_Exists varchar(max), 
	@bParamVal int, @sMsj varchar(max), 
	@iCampos int  

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
	
	Set @iCampos = 0 
	Set @Query = '' 
	Set @CampoAux = '' 
	

------------------------------------------------------------- Generar la tabla base del proceso 
	Select sc.name, sc.xtype, Sc.ColId  
	Into #tmpColumnas___FormateoTablas 
	From Sysobjects as so 
	Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad 
	Order by Sc.ColId


	If Not Exists ( select top 1 * From #tmpColumnas___FormateoTablas ) 
	Begin 
		Insert Into #tmpColumnas___FormateoTablas 
		Select sc.name, sc.xtype, Sc.ColId   
		From tempdb..Sysobjects as so 
		Inner Join tempdb..Syscolumns as sc on (so.id = sc.id) 
		Where so.name like '' + @Tabla + '%' and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad 
		Order by Sc.ColId 
	End 
	

	Set @query = '' 
	Set @ListaCampos = '' 
	Set @ListaCamposInserts = ' '  
	-- Select * from #tmpColumnas___FormateoTablas 

	-- Barrer todos los campos de la tabla
	Declare #query_Cursor 
	Cursor For 
	--Select sc.name, sc.xtype 
	--From Sysobjects as so 
	--Inner Join Syscolumns as sc on (so.id = sc.id) 
	--Where so.name = @Tabla and so.xtype = 'U' and Sc.status <> 128 -- Ignorar las columnas Identidad 
	--Order by Sc.ColId
		Select name, xtype 
		From #tmpColumnas___FormateoTablas 
		Order by ColId
	open #query_Cursor Fetch From #query_Cursor into @campo, @tipo              
	while @@Fetch_status = 0 
		begin      
			Set @ListaCamposInserts = @ListaCamposInserts + @Campo 		    
			-- Set @CampoAux = @Campo 
			
			If @tipo in ( 35, 167, 175, 231, 239 )  
			Begin 
				If @iCampos = 0 
					Begin 
					   Set @CampoAux = 'ltrim(rtrim(replace([' + @campo + '], char(39), ' + char(39) + '' + char(39) + ')))'  
					   Set @CampoAux = 'replace(' + @CampoAux + ', char(13), ' + char(39) + '' + char(39) + ')' 
					   Set @CampoAux = 'replace(' + @CampoAux + ', char(10), ' + char(39) + '' + char(39) + ')' 
					   Set @CampoAux = space(5) + '[' + @campo + '] = ' + @CampoAux 
					   Set @Query = @CampoAux 
					End 
				Else 
					Begin 
					   Set @CampoAux = 'ltrim(rtrim(replace([' + @campo + '], char(39), ' + char(39) + '' + char(39) + ')))' 
					   Set @CampoAux = 'replace(' + @CampoAux + ', char(13), ' + char(39) + '' + char(39) + ')' 
					   Set @CampoAux = 'replace(' + @CampoAux + ', char(10), ' + char(39) + '' + char(39) + ')' 
					   Set @CampoAux = space(5) + '[' + @campo + '] = ' + @CampoAux 
					   Set @Query = @Query + ', ' + char(13) + @CampoAux 			       
					End 
			   
			   Set @iCampos = @iCampos + 1 
			   
			End 

		    -- Set @query = ' + Case When ' + @campo + ' Is Null Then ' + char(39) + 'Null' + char(39) + ' Else ltrim(rtrim(convert(varchar(50), ' + @campo + '))) End ' 
			-- print @CampoAux 
			Fetch next From #query_Cursor into @campo, @tipo 
		end 
	close #query_Cursor              
	deallocate #query_Cursor


	Set @QueryFinal = 'Update T Set ' + char(13) + @Query + ' ' + char(13) + 'From ' + @Tabla + ' T (NoLock) '  

	Print '' 
	Print @QueryFinal 
	If @Ejecutar = 1 
		Exec(@QueryFinal)
	Else 
		Print @QueryFinal 	 



End
Go--#SQL 