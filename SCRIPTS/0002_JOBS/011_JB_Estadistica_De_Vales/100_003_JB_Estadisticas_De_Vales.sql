
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
	@sTM_01_name varchar(500), 
	@sTM_02_name varchar(500), 
	@sTM_03_name varchar(500), 		
	@sTM_04_name varchar(500), 			
	@iFrecuencia int, 	
	@sDescripcion varchar(2000),  
		
	@sExecSQL varchar(500), 
	@sExecSQL_Aux varchar(500), 	
	@Nombre_BD varchar(200),  
	@sIdEmpresa char(3),   
	@sIdEstado char(2), 
	@DiasRevision int   


--- Configurar JOB 		
	Set @Nombre_BD = 'SII_Regional_Puebla' 
	Set @sIdEmpresa = '001' 
	Set @sIdEstado = '21' 
	Set @DiasRevision = 45  



--- Configurar JOB 
	Set @sDescripcion =  'Calculo de Estadisticas de Vales.' 
	Set @sPrefijo = 'PRCS_' 
	Set @sJob_Name = @sPrefijo + '_Estadistica_Mensual_Vales____' + @Nombre_BD + '' 
	Set @sPaso_01_name = @sPrefijo + '_01_Estadistica_Vales__' + @Nombre_BD	
	-- Set @sPaso_02_name = @sPrefijo + '_02_Generar_Existencias__' + @Nombre_BD		
	
	Set @sTM_01_name =	 @sPrefijo + '_02_Ejecutar_Proceso__01_' + @Nombre_BD		
	Set @sTM_02_name =	 @sPrefijo + '_02_Ejecutar_Proceso__02_' + @Nombre_BD		
	Set @sTM_03_name =	 @sPrefijo + '_02_Ejecutar_Proceso__03_' + @Nombre_BD				
	Set @sTM_04_name =	 @sPrefijo + '_02_Ejecutar_Proceso__04_' + @Nombre_BD					
	
	Set @iFrecuencia = 4 -- Horas 

--	Set @sExecSQL = 'Exec sp_BackupDB 1, '''', '''', ' + char(39) + @Ruta_Respaldos + char(39) + ', ' +  char(39) + @Ruta_RAR + char(39)  

	Set @sExecSQL = 'Exec spp_JOB_ADMI_Vales_Estadisticas  ' + char(39) + @sIdEmpresa + char(39) + ', ' + 
		char(39) + @sIdEstado + char(39) + ', ' + char(39) + cast(@DiasRevision as varchar) + char(39)
	Print @sExecSQL  	


--	Exec spp_JOB_Replica_Direcciones 'SII_RegionalPuebla', '[svrCentralDatos].[SII_OficinaCentral]', '21'   
--	Exec spp_JOB_Replica_Direcciones 'SII_RegionalPuebla', '[svrCentralDatos].[SII_OficinaCentral]', '21'
		
---	Quitar el JOB en caso de Existir 	
	If Exists ( Select * From msdb.dbo.sysjobs_view Where Name = @sJob_Name  ) 
	Begin 
		Exec msdb.dbo.sp_delete_job @job_name = @sJob_Name, @delete_history = 1, @delete_unused_schedule = 1
	End



SELECT @ReturnCode = 0
----------------------------------------------------------------------------------------------- 

/****** Object:  JobCategory [[Uncategorized (Local)]]]    Script Date: 03/18/2014 13:41:41 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
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
		@description=@sDescripcion, 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [PRCS__01_Consumos_Mensuales__SII_Regional_DF]    Script Date: 03/18/2014 13:38:18 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_01_name, 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=@sExecSQL, 
		@database_name=@Nombre_BD, 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
----/****** Object:  Step [PRCS__02_GenerarExistencias]    Script Date: 03/18/2014 13:38:18 ******/
----EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_02_name, 
----		@step_id=2, 
----		@cmdexec_success_code=0, 
----		@on_success_action=1, 
----		@on_success_step_id=0, 
----		@on_fail_action=2, 
----		@on_fail_step_id=0, 
----		@retry_attempts=0, 
----		@retry_interval=0, 
----		@os_run_priority=0, @subsystem=N'TSQL', 
----		@command=@sExecSQL_Aux, 
----		@database_name=@Nombre_BD, 
----		@flags=0
----IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback


EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_01_name, 
		@enabled=1, 
		@freq_type=16, 
		@freq_interval=1, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=1, 
		@active_start_date=20130111, 
		@active_end_date=99991231, 
		@active_start_time=60000, 
		@active_end_time=235959, 
		@schedule_uid=N'6a138e55-8c26-4f65-a46a-333b12d83e80'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_02_name, 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=8, 
		@freq_subday_interval=1, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20140125, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959, 
		@schedule_uid=N'459a5f06-d2e9-45c8-842f-e45934e19d2a'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_03_name, 
		@enabled=1, 
		@freq_type=16, 
		@freq_interval=16, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=1, 
		@active_start_date=20130111, 
		@active_end_date=99991231, 
		@active_start_time=60000, 
		@active_end_time=235959, 
		@schedule_uid=N'40ca05fd-8a1f-4466-ae54-271805924a8e'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_04_name, 
		@enabled=1, 
		@freq_type=16, 
		@freq_interval=28, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=1, 
		@active_start_date=20130111, 
		@active_end_date=99991231, 
		@active_start_time=60000, 
		@active_end_time=235959, 
		@schedule_uid=N'8bc0c6f9-7203-4c00-bd8e-2918fdbb5671'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO


