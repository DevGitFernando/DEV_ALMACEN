USE [msdb]
GO
/****** Objeto:  Job [Respaldos_Automaticos_BD]    Fecha de la secuencia de comandos: 01/25/2012 10:08:15 ******/
BEGIN TRANSACTION 
DECLARE @ReturnCode INT 

Declare 
	@sPrefijo varchar(100), 
	@sJob_Name varchar(500), 
	@sPaso_01_name varchar(500), 
	@sTM_01_name varchar(500), 
	@iFrecuencia int, 	
		
	@sExecSQL varchar(500), 
	@Nombre_BD varchar(200),  
	@Nombre_Servidor_Destino varchar(200),  			
	@Nombre_Servidor_Origen varchar(200),  		
	@Nombre_BaseDeDatos_Origen varchar(200),  		
	@Nombre_BD_Destino varchar(200),  	
	@sTablaControl varchar(100),  
	@sDescripcion varchar(500)  

--- Configurar JOB 		
	Set @Nombre_BD = 'SII_OficinaCentral' 
	Set @Nombre_Servidor_Origen = 'CENTRAL' 	
	Set @Nombre_Servidor_Destino = 'CENTRAL' 
	Set @Nombre_BD_Destino = 'SII_Regional_Michoacan'  	
	Set @sTablaControl = 'CFGC_EnvioDetalles'    --- De unidad a servidor 
	Set @sTablaControl = 'CFGSC_EnvioCatalogos'  --- De servidor a servidor 
	--Set @sTablaControl = 'CFGSC_EnvioCatalogos__Facturacion'  --- De servidor central a servidor facturacion 
	--Set @sTablaControl = 'CFGSC_EnvioCatalogos__Mail'  --- De servidor central a servidor correo 	


	Set @sDescripcion = 'Migra los catalogos generales al Servidor de Michoacan' 
--	Set @sDescripcion = 'Migra los catalogos generales al Servidor de Correo' 
	-- Set @sDescripcion = 'Migra la informacion Regional de Jalisco al Servidor Central' 	

--- Configurar JOB 
	Set @sPrefijo = 'RP_INF_' 
	Set @sJob_Name = 'Descargar_Catalogos__CENTRAL' --+ @Nombre_BD_Destino + '' 
	--Set @sJob_Name = 'Replicar_Informacion__Central__CENTRAL' --- + @Nombre_BD + '' 
	
	Set @sPaso_01_name = @sPrefijo + '_01_Descargar_Informacion__' + @Nombre_BD_Destino	
	Set @sTM_01_name =	 @sPrefijo + '_02_Ejecutar_Proceso__' + @Nombre_BD_Destino 		
	Set @iFrecuencia = 1 -- Horas 

--	Set @sExecSQL = 'Exec sp_BackupDB 1, '''', '''', ' + char(39) + @Ruta_Respaldos + char(39) + ', ' +  char(39) + @Ruta_RAR + char(39)  

	Set @sExecSQL = 'Exec spp_INF_SEND ' + char(39) +  + '[' + @Nombre_Servidor_Origen + '].['  + @Nombre_BD + ']' + char(39) + ', ' + 
		char(39) + '['  + @Nombre_BD_Destino + ']' + char(39) + ', ' + char(39) + @Nombre_BD_Destino + char(39) + 
		', ' + char(39) + @sTablaControl + char(39) + ', 1, 1'
	Print @sExecSQL 

-- Exec spp_INF_SEND ''SII_OficinaCentral'', ''[SVRPUEBLA].[SII_RegionalPuebla]'', ''SII_OficinaCentral'', ''CFGSC_EnvioCatalogos'', 1, 1  
-- Exec spp_INF_SEND 'SII_OficinaCentral', '[localhost].[SII_Regional_Jalisco]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1


--	Exec spp_JOB_Replica_Direcciones 'SII_RegionalPuebla', '[svrCentralDatos].[SII_OficinaCentral]', '21'   
--	Exec spp_JOB_Replica_Direcciones 'SII_RegionalPuebla', '[svrCentralDatos].[SII_OficinaCentral]', '21'
		
---	Quitar el JOB en caso de Existir 	
	If Exists ( Select * From msdb.dbo.sysjobs_view Where Name = @sJob_Name  ) 
	Begin 
		Exec msdb.dbo.sp_delete_job @job_name = @sJob_Name, @delete_history = 1, @delete_unused_schedule = 1
	End



SELECT @ReturnCode = 0
----------------------------------------------------------------------------------------------- 

/****** Objeto:  JobCategory [[Uncategorized (Local)]]]    Fecha de la secuencia de comandos: 06/07/2012 17:39:54 ******/
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
/****** Objeto:  Step [Catalogos]    Fecha de la secuencia de comandos: 06/07/2012 17:39:58 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@sPaso_01_name, 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=1, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		-- @command=N'Exec spp_INF_SEND ''SII_OficinaCentral'', ''[SVRPUEBLA].[SII_RegionalPuebla]'', ''SII_OficinaCentral'', ''CFGSC_EnvioCatalogos'', 1, 1 ', 
		-- @database_name=N'SII_OficinaCentral', 
		@command=@sExecSQL, 
		@database_name=@Nombre_BD_Destino, 		
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@sTM_01_name, 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=8, 
		@freq_subday_interval=1, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20110815, 
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
