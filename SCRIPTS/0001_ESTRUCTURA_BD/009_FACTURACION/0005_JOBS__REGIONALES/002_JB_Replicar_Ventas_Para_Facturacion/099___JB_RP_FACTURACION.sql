USE [msdb]
GO
/****** Objeto:  Job [RP_FACTURACION]    Fecha de la secuencia de comandos: 09/08/2012 11:17:45 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT


Declare 
	@sPrefijo varchar(100), 
	@sJob_Name varchar(500), 
	@sPaso_01_name varchar(500), 
		
	@sExecSQL varchar(500), 
	@Nombre_BD varchar(200),  
	@Nombre_Servidor_Destino varchar(200),  		
	@Nombre_BD_Destino varchar(200),  	
	@sTablaControl varchar(100),  
	@sDescripcion varchar(500),

	@FechaCorte varchar(10)  

--- Configurar JOB 		
	Set @Nombre_BD = 'SII_Regional_Hidalgo' 
	Set @Nombre_Servidor_Destino = 'FACTURACION' 
	Set @Nombre_BD_Destino = 'SII_Facturacion_Hidalgo_PRUEBAS'  	
	Set @sTablaControl = 'CFGSC_EnvioDetallesVentas' 
	Set @sDescripcion = 'Replicar la Informacion de Ventas, Necesaria para Facturacion' 
	Set @FechaCorte = '2017-03-01'	
	

--- Configurar JOB 
	Set @sPrefijo = 'RP_FACT_' 
	Set @sJob_Name = 'RP_FACTURACION' 	 
	
	Set @sPaso_01_name = @sPrefijo + '_Replicar_Facturacion' + @Nombre_BD_Destino	
	

	Set @sExecSQL = 'Exec spp_INF_SEND_FacturacionVtas ' + char(39) + @Nombre_BD + char(39) + ', ' + 
		char(39) + '[' + @Nombre_Servidor_Destino + '].['  + @Nombre_BD_Destino + ']' + char(39) + ', ' + char(39) + @Nombre_BD + char(39) + 
		', ' + char(39) + @sTablaControl + char(39) + ', ' + char(39) + @FechaCorte + char(39) + ', 1'
	Print @sExecSQL 

		
---	Quitar el JOB en caso de Existir 	
	If Exists ( Select * From msdb.dbo.sysjobs_view Where Name = @sJob_Name  ) 
	Begin 
		Exec msdb.dbo.sp_delete_job @job_name = @sJob_Name, @delete_history = 1, @delete_unused_schedule = 1
	End


SELECT @ReturnCode = 0
/****** Objeto:  JobCategory [[Uncategorized (Local)]]]    Fecha de la secuencia de comandos: 09/08/2012 11:17:45 ******/
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
/****** Objeto:  Step [RP_FACT_01_Facturacion]    Fecha de la secuencia de comandos: 09/08/2012 11:17:46 ******/
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
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'RP_01_Facturacion', 
		@enabled=1, 
		@freq_type=8, 
		@freq_interval=64, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=1, 
		@active_start_date=20120907, 
		@active_end_date=99991231, 
		@active_start_time=210000, 
		@active_end_time=235959
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
