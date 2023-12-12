USE [msdb]
GO


BEGIN TRANSACTION 
DECLARE @ReturnCode INT 

Declare 
	@sGUID varchar(500), 
	@sPrefijo varchar(100), 
	@sJob_Name varchar(500), 
	@sPaso_01_name varchar(500), 
	@sPaso_02_name varchar(500), 	
	@sTM_01_name varchar(500), 
	@sTM_02_name varchar(500), 
	@sTM_03_name varchar(500), 		
	@iFrecuencia int, 	
	@sDescripcion varchar(2000),  
	@sRutaAplicacion varchar(2000), 
	@Nombre_BD varchar(200), 
	@sFechaDeInicio varchar(10)  


	Set @sFechaDeInicio = replace(convert(varchar(10), getdate(), 120), '-', '') 


--- Configurar JOB 		
	Set @Nombre_BD = 'SII_22_001_0011__HG_QUERETARO_FARMACIA' 
	Set @sRutaAplicacion = 'E:\SII_PUNTO_DE_VENTA\ISESEQ SERVICIOS.exe IN P2'


--- Tiempo entre ejecuciones, en minutos 
	Set @iFrecuencia = 20 


--- Configurar JOB 
	Set @sDescripcion =  'Envio de información operativa.' 
	Set @sPrefijo = 'RP_002' 
	Set @sJob_Name = @sPrefijo + '__SND_INFOP_ISESEQ__' + @Nombre_BD + '' 
	
	Set @sPaso_01_name = @sPrefijo + '__01_INFOP_Acuses__' + @Nombre_BD	
	
	Set @sTM_01_name =	 @sPrefijo + '_02_INFOP_Acuses__' + @Nombre_BD		
	


	--Set @sRutaAplicacion = 'D:\PROYECTOS\SII_PROYECT\SII_OPERATIVO\INTERFACES\INTERFACE_ECE__SIADISSEP\Archivos_Generados\' + 'ISIADISSEP SERVICIOS.exe IN '
	-- N'D:\PROYECTOS\SII_PROYECT\SII_OPERATIVO\INTERFACES\INTERFACE_MEDIACCESS\Archivos_Generados\JOB\MA Servicios.exe',  	
	-- Set @sRutaAplicacion = 'E:\SII_PUNTO_DE_VENTA\ISESEQ SERVICIOS.exe IN P2'

	Set @sGUID = cast(NEWID() as varchar(max)) 

		
---	Quitar el JOB en caso de Existir 	
	If Exists ( Select * From msdb.dbo.sysjobs_view Where Name = @sJob_Name  ) 
	Begin 
		Exec msdb.dbo.sp_delete_job @job_name = @sJob_Name, @delete_history = 1, @delete_unused_schedule = 1
	End



SELECT @ReturnCode = 0
----------------------------------------------------------------------------------------------- 


/****** Object:  JobCategory [Database Engine Tuning Advisor]    Script Date: 02/10/2016 12:07:33 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Engine Tuning Advisor' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Engine Tuning Advisor'
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
		@description=N'Envio de respuestas para el Expediente Electrónico.', 
		@category_name=N'Database Engine Tuning Advisor', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Test_Envio_IMediaccess]    Script Date: 02/10/2016 12:07:34 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_01_name, 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'CmdExec', 
		@command=@sRutaAplicacion, 
		-- N'D:\PROYECTOS\SII_PROYECT\SII_OPERATIVO\INTERFACES\INTERFACE_MEDIACCESS\Archivos_Generados\JOB\MA Servicios.exe', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_01_name, 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=4, 
		@freq_subday_interval= @iFrecuencia, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date= @sFechaDeInicio, 
		@active_end_date=99991231, 
		@active_start_time=10000, 
		@active_end_time=15959, 
		@schedule_uid= @sGUID --N'e09ee140-f292-47e2-a7e7-d1db7fdecd0c'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO


