USE [msdb]
GO
/****** Objeto:  Job [Respaldos_Automaticos_BD]    Fecha de la secuencia de comandos: 01/25/2012 10:08:15 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT 

Declare 
	@Nombre_BD varchar(200),  
	@Ruta_Respaldos varchar(300), 
	@sTruncate varchar(200), 
	@sBK varchar(200)  	  
	
	Set @Nombre_BD = 'SII_OAXACA' 
	Set @Ruta_Respaldos = 'D:\SII_BD\RESPALDOS_AUTOMATICOS_SQL\'  

--- 
	Set @sTruncate = 'Exec sp_TruncateLog 1, ' + char(39) + @Nombre_BD + char(39) 
	Set @sBK = 'Exec sp_BackupDB 1, '''', '''', ' + char(39) + @Ruta_Respaldos + char(39) 
	
	-- Select @sTruncate, @sBK  


SELECT @ReturnCode = 0
/****** Objeto:  JobCategory [Database Maintenance]    Fecha de la secuencia de comandos: 01/25/2012 10:08:15 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'Respaldos_Automaticos_BD', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Trunca la BD especificada y Genera el respaldo de la misma en la Ruta especifcada.', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Objeto:  Step [001_Truncar_Log_De_Datos]    Fecha de la secuencia de comandos: 01/25/2012 10:08:17 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'001_Truncar_Log_De_Datos', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		
		-- @command=N'Exec sp_TruncateLog 1, ''SII_RegionalPuebla''', 
		-- @database_name=N'SII_RegionalPuebla', 
		
		@command=@sTruncate, 
		@database_name=@Nombre_BD, 				
		@flags=0 
		
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Objeto:  Step [002_Eliminar_LogErrores]    Fecha de la secuencia de comandos: 01/25/2012 10:08:17 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'002_Eliminar_LogErrores', 
		@step_id=2, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'If Exists ( Select * From Sysobjects (NoLock) Where Name = ''CtlErrores'' and xType = ''U'' )
Begin 
	drop table CtlErrores 
End', 
		-- @database_name=N'SII_RegionalPuebla', 
		@database_name=@Nombre_BD, 		
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Objeto:  Step [003_Generar_BackUp]    Fecha de la secuencia de comandos: 01/25/2012 10:08:18 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'003_Generar_BackUp', 
		@step_id=3, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 

		-- @command=N'Exec sp_BackupDB 1, '''', '''', ''D:\SII_BD\RESPALDOS_AUTOMATICOS_SQL\''', 
		-- @database_name=N'SII_RegionalPuebla', 

		@command=@sBK, 
		@database_name=@Nombre_BD, 
		@flags=0 
		
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'003_Ejecutar_Proceso_BK', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=8, 
		@freq_subday_interval=8, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20111017, 
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
