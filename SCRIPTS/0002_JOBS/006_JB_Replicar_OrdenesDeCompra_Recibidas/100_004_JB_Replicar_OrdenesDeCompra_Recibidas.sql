USE [msdb]
GO
/****** Objeto:  Job [Respaldos_Automaticos_BD]    Fecha de la secuencia de comandos: 01/25/2012 10:08:15 ******/
BEGIN TRANSACTION 
DECLARE @ReturnCode INT 

Declare 
	@iTipo_Proceso int, 
	@sPrefijo varchar(100), 
	@sJob_Name varchar(500), 
	@sPaso_01_name varchar(500), 
	@sTM_01_name varchar(500), 
	@iFrecuencia int, 	
		
	@sExecSQL varchar(500), 
	@Nombre_BD varchar(200),  
	@sDescripcion varchar(500), 
	
	@Nombre_Servidor_Destino varchar(200),  		
	@Nombre_BD_Destino varchar(200),  	
	@TablaDeControl varchar(100)  


--- Configurar JOB
	Set @iTipo_Proceso = 2 
	--  1 == Almacen	==>		Regional 
	--  2 == Regional	==>		Central   	
 		
	Set @Nombre_BD = 'PtoVta_Obregon_SII_09_001_0011' 
	Set @Nombre_Servidor_Destino = 'CENTRAL' 
	Set @Nombre_BD_Destino = 'SII_OficinaCentral'  	
	Set @TablaDeControl = 'CFGC_COM_EnvioDetalles' 


--- Configurar JOB 
	If @iTipo_Proceso = 1 
	Begin 
		Set @sPrefijo = 'RP_PED_01_' 
		Set @sJob_Name = 'RP_ORD_COM_ALM_REG_' + @Nombre_BD + '' 
		Set @sDescripcion = 'Envia al Servidor Regional las Ordenes de Compra recepcionadas' 
	End 
	
	If @iTipo_Proceso = 2 
	Begin 
		Set @sPrefijo = 'RP_PED_02_' 
		Set @sJob_Name = 'RP_ORD_COM_ALM_CEN_' + @Nombre_BD + '' 
		Set @sDescripcion = 'Envia al Servidor Central las Ordenes de Compra recepcionadas' 
	End 
	


	Set @sPaso_01_name = @sPrefijo + '_01_Enviar_Pedidos__' + @Nombre_BD	-- '001_Enviar_Pedidos'
	Set @sTM_01_name =	 @sPrefijo + '_02_Ejecutar_Proceso__' + @Nombre_BD		
	Set @iFrecuencia = 5 -- Minutos  

	Set @sExecSQL = 'Exec spp_INF_SEND__OC ' + char(39) + @Nombre_BD + char(39) + ', ' + 
		char(39) + '[' + @Nombre_Servidor_Destino + '].['  + @Nombre_BD_Destino + ']' + char(39) + ', ' + char(39) + @Nombre_BD + char(39) + 
		', ' + char(39) + @TablaDeControl + char(39) + ', 1, 1, 1440 '  

--	Print @sExecSQL 
	
		
---	Quitar el JOB en caso de Existir 	
	If Exists ( Select * From msdb.dbo.sysjobs_view Where Name = @sJob_Name  ) 
	Begin 
		Exec msdb.dbo.sp_delete_job @job_name = @sJob_Name, @delete_history = 1, @delete_unused_schedule = 1
	End



SELECT @ReturnCode = 0
----------------------------------------------------------------------------------------------- 

/****** Objeto:  JobCategory [[Uncategorized (Local)]]]    Fecha de la secuencia de comandos: 03/28/2012 09:23:45 ******/
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
/****** Objeto:  Step [001_Enviar_Pedidos]    Fecha de la secuencia de comandos: 03/28/2012 09:23:47 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_01_name, 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=2, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
--		@command=N'Exec spp_INF_SEND ''SII_21_0182_CEDISPUEBLA'', ''[svrCompras].[SII_RegionalPuebla_Test]'', ''SII_21_0182_CEDISPUEBLA'', ''CFGC_COM_EnvioDetalles'', 1, 1', 
--		@database_name=N'SII_21_0182_CEDISPUEBLA',
		@command=@sExecSQL, 
		@database_name=@Nombre_BD,
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_01_name, 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=4, 
		@freq_subday_interval=@iFrecuencia, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20100101, 
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
