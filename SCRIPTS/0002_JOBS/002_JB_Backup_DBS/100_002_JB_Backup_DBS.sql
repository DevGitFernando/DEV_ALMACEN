USE [msdb]
GO
/****** Objeto:  Job [Respaldos_Automaticos_BD]    Fecha de la secuencia de comandos: 01/25/2012 10:08:15 ******/
BEGIN TRANSACTION 
DECLARE @ReturnCode INT 

Declare 
	@sPrefijo varchar(100), 
	@sJob_Name varchar(500), 
	@sPaso_01_name varchar(500), 
	@sPaso_02_name varchar(500), 
	@sPaso_03_name varchar(500), 
	@sPaso_04_name varchar(500), 
	@sTM_01_name varchar(500), 
	@iFrecuencia int, 	
	@iFechaDeInicio int, 
		
	@Nombre_BD varchar(200),  
	@Ruta_Respaldos varchar(300), 
	@Ruta_RAR varchar(300), 	
	@sTruncateCtlErrores varchar(300), 	
	@sTruncate varchar(200), 
	@sShrinkDataBase varchar(200), 	
	@sBK varchar(200)  	  

--- Configurar JOB 		
	Set @Nombre_BD = 'SII_22_0104__PHARMAJAL_QRO' 
	Set @Ruta_Respaldos = 'C:\SII_BASES_DE_DATOS\RESPALDOS_AUTOMATICOS_SQL_PHARMAJAL\'  
	Set @Ruta_RAR = 'C:\Program Files\WinRAR\' 
	Set @Ruta_RAR = 'C:\Program Files (x86)\WinRAR' 	

--- Configurar JOB 
	Set @sPrefijo = 'BK' 
	Set @sJob_Name = 'BK_AUTO__' + @Nombre_BD + '' 
	Set @sPaso_01_name = @sPrefijo + '_01_01_Reducir_BD__' + @Nombre_BD				-- N'BK_001_01_Reducir_BD'
	Set @sPaso_02_name = @sPrefijo + '_01_02_Eliminar_LogErrores__' + @Nombre_BD		-- N'BK_001_03_Eliminar_LogErrores'
	Set @sPaso_03_name = @sPrefijo + '_01_03_Truncar_Log_De_Datos__' + @Nombre_BD		-- N'BK_001_02_Truncar_Log_De_Datos'	
	Set @sPaso_04_name = @sPrefijo + '_01_04_Generar_BackUp__' + @Nombre_BD			-- N'BK_001_04_Generar_BackUp'
	Set @sTM_01_name =	 @sPrefijo + '_02_01_Ejecutar_Proceso__' + @Nombre_BD			-- N'003_Ejecutar_Proceso_BK'
	Set @iFrecuencia = 8 -- Horas 
	Set @iFechaDeInicio = convert(varchar(10), getdate(), 112) 
--- 
	Set @sShrinkDataBase = 'DBCC SHRINKDATABASE ( ' + char(39) + @Nombre_BD + char(39) + ' ) ' 
	Set @sTruncate = 'Exec sp_TruncateLog 1, ' + char(39) + @Nombre_BD + char(39) 
	Set @sBK = 'Exec sp_BackupDB 1, '''', '''', ' + char(39) + @Ruta_Respaldos + char(39) + ', ' +  char(39) + @Ruta_RAR + char(39)  
	
	Set @sTruncateCtlErrores = 
		'If Exists ( Select * From Sysobjects (NoLock) Where Name = ''CtlErrores'' and xType = ''U'' ) ' + char(13) + char(10) + 
			'   Begin ' + char(13) + char(10) + 
			'         Drop Table CtlErrores ' + char(13) + char(10) + 
			'   End	' 		
			
		
---	Quitar el JOB en caso de Existir 	
	If Exists ( Select * From msdb.dbo.sysjobs_view Where Name = @sJob_Name  ) 
	Begin 
		Exec msdb.dbo.sp_delete_job @job_name = @sJob_Name, @delete_history = 1, @delete_unused_schedule = 1
	End



SELECT @ReturnCode = 0
----------------------------------------------------------------------------------------------- 

/****** Objeto:  JobCategory [Database Maintenance]    Fecha de la secuencia de comandos: 03/27/2012 17:47:22 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=@sJob_Name, 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Trunca la BD especificada y Genera el respaldo de la misma en la Ruta especificada.', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Objeto:  Step [BK_001_01_Reducir_BD]    Fecha de la secuencia de comandos: 03/27/2012 17:47:22 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_01_name, 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		
		-- @command=N'DBCC SHRINKDATABASE(N''SII_Regional_Hidalgo'' )', 
		-- @database_name=N'SII_Regional_Hidalgo', 
		
		@command=@sShrinkDataBase, 
		@database_name=@Nombre_BD, 	
		
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Objeto:  Step [BK_001_03_Eliminar_LogErrores]    Fecha de la secuencia de comandos: 03/27/2012 17:47:23 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_02_name, 
		@step_id=2, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=@sTruncateCtlErrores, 
		@database_name=@Nombre_BD, 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Objeto:  Step [BK_001_02_Truncar_Log_De_Datos]    Fecha de la secuencia de comandos: 03/27/2012 17:47:23 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_03_name, 
		@step_id=3, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 		
		-- @command=N'Exec sp_TruncateLog 1, ''SII_Regional_Hidalgo''', 
		-- @database_name=N'SII_Regional_Hidalgo', 		
		@command=@sTruncate, 
		@database_name=@Nombre_BD, 			
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Objeto:  Step [BK_001_04_Generar_BackUp]    Fecha de la secuencia de comandos: 03/27/2012 17:47:23 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_04_name, 
		@step_id=4, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		--@command=N'Exec sp_BackupDB 1, '''', '''', ''D:\SII_BD\RESPALDOS_AUTOMATICOS_SQL\'', ''C:\Program Files (x86)\WinRAR''', 		
		@command=@sBK, 
		@database_name=@Nombre_BD, 		
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 2
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_01_name, 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=8, 
		@freq_subday_interval=@iFrecuencia,  -- 6, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=@iFechaDeInicio, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)' 
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
