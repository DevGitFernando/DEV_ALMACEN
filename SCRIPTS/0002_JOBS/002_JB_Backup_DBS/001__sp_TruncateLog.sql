------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects Where Name = 'sp_TruncateLog' and xType = 'P' )
    Drop Proc sp_TruncateLog
Go--#SQL 

Create Proc sp_TruncateLog 
( 
	@Truncate int = 0, 
    @BaseDeDatos varchar(100) = '', 
	@RutaRAR varchar(200) = ''  
) 
With Encryption                          
As
Begin
    Set NoCount On

Declare 
	@TamReducccion as float, 
    @NombreData     varchar(50),
    @nombreLog      varchar(200),
    @RutaArchivo    varchar(500),
    @NombreBd       varchar(100),
    @ExecTruncate   varchar(1000),
    @TamGbD as int,     @TamMbD as float,      @TamKbD as float,
    @TamGbL as int,    @TamMbL as float,      @TamKbL as float,
    @Bd_Conection varchar(100),
    @sSql varchar(500)

Declare 
	@svrVersion varchar(50), 
	@sqlVersion int 

Declare @sRAR  varchar(1000), 
		@Comprimir smallint -- = 0  

	Set @TamReducccion = 0.1  

    If @BaseDeDatos = '' 
       Set @Bd_Conection = db_name()
    Else 
       Set @Bd_Conection = @BaseDeDatos 

    -- Select @Bd_Conection as [ Base de datos actual ] 

	Set @sRAR = '' 	
	if @RutaRAR <>  '' 
	   Set @Comprimir  = 1 


	Select @svrVersion = ltrim(rtrim(convert(varchar(50), ServerProperty('productversion')))) 
	Set @sqlVersion = cast(Left(@svrVersion, PatIndex('%.%', @svrVersion) - 1) as int)
	-- Select @svrVersion as VersionSQL, Left(@svrVersion, PatIndex('%.%', @svrVersion) - 1)  


    Select * Into #tmpSysFiles From Sysfiles Where 1 = 0
    Set @sSql = 'Insert Into #tmpSysFiles Select *  From ' + 
        @Bd_Conection + '..Sysfiles'
    Execute(@sSql)

    Select @NombreData = ltrim(rtrim(Name)), 
        @RutaArchivo = ltrim(rtrim(FileName)), 
        @TamGbD = (((size * 8) / 1024.0000) / 1024.0000), 
        @TamMbD = (((size * 8) / 1024.0000)), 
        @TamKbD = (size * 8)
    From #tmpSysFiles where FileId = 1    

    Select 
		-- @NombreLog = ltrim(rtrim(Name)), 
		@NombreLog = cast(FileId as varchar(10)),
        @TamGbL = (((size * 8) / 1024.0000) / 1024.0000), 
        @TamMbL = (((size * 8) / 1024.0000)),  
        @TamKbL = (size * 8)        
    From #tmpSysFiles where FileId = 2 
    
    Select @NombreBd = ltrim(rtrim(Name)) 
    From master..Sysdatabases where FileName = @RutaArchivo   


--- Datos de Version de SQL Server 
--	Select @svrVersion as 'Version de SQL', @sqlVersion as 'Version'


    If @Truncate = 1
       Begin
            -- Propiedades antes de la reduccion
            Select @svrVersion as 'Version de SQL', @sqlVersion as 'Version', 
					'Propiedades antes de la reduccion de : ' + 
                    space(5) + Upper(@Bd_Conection) as Antes
            Select 'Archivo de datos' = @NombreData, 'Gb_Data' = @TamGbD, 
				   'Mb_Data' = @TamMbD, 
				   'KB_Data' = @TamKbD, 
                   'Archivo log de transacciones' = @NombreLog, 
                   'Gb_Log' = @TamGbL, 
				   'Mb_Log' = @TamMbL, 
				   'KB_Log' = @TamKbL, 				   
				   'Ruta' = @RutaArchivo

			--- Preparar el Truncado del Log
			if @sqlVersion <= 9 
			   Begin 
					Select @ExecTruncate = ' CheckPoint     
					Backup Log ' + @NombreBd + ' with Truncate_Only     
					DBCC Shrinkfile (' + @NombreLog + ', 1, TruncateOnly)'            
			   End 
			Else 			   
			   Begin 
					Select @ExecTruncate = 'Alter Database [' + @NombreBd + '] Set Recovery Simple With No_Wait ' + char(13)  			   
					Select @ExecTruncate = @ExecTruncate + ' CheckPoint DBCC Shrinkfile (' + @NombreLog + ', 1, TruncateOnly)' + char(13)  
					Select @ExecTruncate = @ExecTruncate + 'Alter Database [' + @NombreBd + '] Set Recovery Full With No_Wait ' 					
			   End 
			  			   
			--- Ejecutar el truncado 
			Exec(@ExecTruncate)    


            --- Obtener las propiedades
            Delete From #tmpSysFiles
			Set @sSql = 'Insert Into #tmpSysFiles Select *  From ' + 
				@Bd_Conection + '..Sysfiles'
			Execute(@sSql)    
                
            Select @NombreData = ltrim(rtrim(Name)), 
                   @RutaArchivo = ltrim(rtrim(FileName)), 
				   @TamGbD = (((size * 8) / 1024.0000) / 1024.0000), 
                   @TamMbD = (((size * 8) / 1024.0000)), 
				   @TamKbD = (size * 8)                    
            From #tmpSysFiles where FileId = 1

            Select @NombreLog = ltrim(rtrim(Name)), 
                   @TamGbL = (((size * 8) / 1024.0000) / 1024.0000), 
				   @TamMbL = (((size * 8) / 1024.0000)), 
			       @TamKbL = (size * 8) 
            From #tmpSysFiles where FileId = 2
            
            Select @NombreBd = ltrim(rtrim(Name)) 
            From master..Sysdatabases where FileName = @RutaArchivo

            -- Propiedades despues de la reduccion
            Select 'Propiedades despues de la reduccion de : ' + 
                    space(5) + Upper(@Bd_Conection) as Despues
            Select 'Base de datos' = @NombreBd, 
                   'Archivo de datos' = @NombreData, 'Gb_Data' = @TamGbD, 
				   'Mb_Data' = @TamMbD, 
				   'KB_Data' = @TamKbD, 
                   'Archivo log de transacciones' = @NombreLog, 
                   'Gb_Log' = @TamGbL, 
				   'Mb_Log' = @TamMbL, 
				   'KB_Log' = @TamKbL, 
				   'Ruta' = @RutaArchivo 
       End
    Else
       Begin
        Select @svrVersion as 'Version de SQL', @sqlVersion as 'Version', 
				'Propiedades de la base de datos : ' + 
                space(5) + Upper(@Bd_Conection) as Propiedades
        Select 'Archivo de datos' = @NombreData, 'Gb_Data' = @TamGbD, 
			   'Mb_Data' = @TamMbD, 'KB_Data' = @TamKbD, 
               'Archivo log de transacciones' = @NombreLog, 'Gb_Log' = @TamGbL, 
			   'Mb_Log' = @TamMbL, 'KB_Log' = @TamKbL, 'Ruta' = @RutaArchivo
       End

    Set NoCount Off
End
Go--#SQL 