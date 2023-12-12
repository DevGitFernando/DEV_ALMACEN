-------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario_004' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario_004 
Go--#SQL 

---		Exec spp_Mtto_CierreDeInventario_004 '002', '20', '0035', '0135', 65, 'RutaParaRespaldos'  -- 'D:\BD_TEST\' 

Create Proc spp_Mtto_CierreDeInventario_004  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '0188', @IdEmpresaNueva varchar(3) = '001', @IdFarmaciaNueva varchar(4) = '0200', 
	@dPorce numeric(14, 2) = 65, @RutaRespaldo varchar(500) = 'D:\BD_TEST\'   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
---- spp_Mtto_CierreDeInventario_004_DepurarInformacion  

Declare 
	@sBD varchar(100) 

Declare @sNombreTabla varchar(200), 
		@sWhereEmpresa varchar(200), 
		@iTieneEmpresa smallint, 
		@sSql varchar(8000), 
		-- @dPorce numeric(14,2), 
		@dIncr numeric(14, 2)   

	Set @sNombreTabla = '' 
	Set @sWhereEmpresa = '' 
	Set @iTieneEmpresa = 0 
	Set @sSql = '' 
	Set @sBD = db_name() 
	
	--Set @dPorce = 90 
	Select @dIncr = (90-@dPorce) / (count(*)* 1.0) From CierreInventario_Tablas_Limpiar (NoLock)  
	
--- Respaldo Final 
	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Generando respaldo', @dPorce 
	Exec spp_Mtto_CierreDeInventario_005 @sBD, @RutaRespaldo, 'Antes_Depuracion'
	
--- Borrar Tabla Rpt_CTE_VentasEstadisticaClavesDispensadas
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_VentasEstadisticaClavesDispensadas' and xType = 'U' ) 
	Begin 
		Drop Table Rpt_CTE_VentasEstadisticaClavesDispensadas
	End
		
	
	-- Se eliminan los registros de la Farmacia que se cerro.
	Declare #Tablas Cursor For 
		Select Tabla, TieneEmpresa 
		From CierreInventario_Tablas_Limpiar 
		-- where 1 = 1 
		Order By Keyx
	Open #Tablas Fetch #Tablas Into @sNombreTabla, @iTieneEmpresa 
		While (@@Fetch_Status = 0 )  
			Begin 
				If( @iTieneEmpresa = 0 ) 
				  Begin 
					Set @sWhereEmpresa = '' 
				  End 
				Else 
				  Begin 
					Set @sWhereEmpresa = 'And IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' ' 
				  End 

				Set @sSql = 'Delete From ' + @sNombreTabla + ' Where 1 = 1 ' + 
							@sWhereEmpresa + ' ' + 
							'And IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + 
							'And IdFarmacia = ' + char(39) + @IdFarmacia + char(39) 
				Exec (@sSql) 
				--Print @sSql 
				
				Set @dPorce = @dPorce + @dIncr 
				Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Depurando información', @dPorce   				
				

				Fetch #Tablas Into @sNombreTabla, @iTieneEmpresa 
			End		
	Close #Tablas 
	DeAllocate #Tablas 



	------------------------------------------ Actualizar referencias 
	Delete From Net_CFGC_Parametros Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and NombreParametro = 'FechaOperacionSistema' 	
	
	Update Net_CFGC_Parametros Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 

	Update CFGC_ConfigurarConexion Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado -- And IdFarmacia = @IdFarmacia  

	Update CFG_Svr_UnidadesRegistradas Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	

	Update CatBeneficiarios Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	

	Update CatPersonalCEDIS Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
	Update CFG_ALMN_Ubicaciones_Estandar Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	


	Update CFGC_Titulos_Reportes_Detallado_Venta Set IdEmpresa = @IdEmpresaNueva, IdFarmacia = @IdFarmaciaNueva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	



	---------- Receta electronica 
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'INT_RecetaElectronica' and xType = 'U' ) 
	Begin 
		Update INT_RecetaElectronica Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  		
	End 

	If Exists ( Select * From sysobjects (NoLock) Where Name = 'INT_SIADISSEP__CFG_Farmacias_UMedicas' and xType = 'U' ) 
	Begin 
		Update INT_SIADISSEP__CFG_Farmacias_UMedicas Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
	End 

	If Exists ( Select * From sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__CFG_Farmacias_UMedicas' and xType = 'U' ) 
	Begin 
		Update INT_RE_INTERMED__CFG_Farmacias_UMedicas Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
	End 
	---------- Receta electronica 


	---------- Robots dispensadores 
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'INT_RobotDispensador' and xType = 'U' ) 
	Begin 
		Update INT_RobotDispensador Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
	End 

	If Exists ( Select * From sysobjects (NoLock) Where Name = 'IMach_CFGC_Clientes' and xType = 'U' ) 
	Begin 
		Update IMach_CFGC_Clientes Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
	End 

	If Exists ( Select * From sysobjects (NoLock) Where Name = 'IATP2_CFGC_Clientes' and xType = 'U' ) 
	Begin 
		Update IATP2_CFGC_Clientes Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
	End 

	If Exists ( Select * From sysobjects (NoLock) Where Name = 'IGPI_CFGC_Clientes' and xType = 'U' ) 
	Begin 
		Update IGPI_CFGC_Clientes Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
	End 
	---------- Robots dispensadores 


	------------------------------------------ Actualizar referencias 





	--------------------------------- MEDICOS 
	Insert Into CatMedicos ( IdEstado, IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, IdEspecialidad, Status, Actualizado ) 
	Select  IdEstado, @IdFarmaciaNueva as IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, IdEspecialidad, Status, Actualizado 
	From CatMedicos (NoLock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  

	Update CatMedicos_Direccion Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  

	Delete From CatMedicos Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
	--------------------------------- MEDICOS 
		

	--------------------------------- CUADRO BASICO  	
	--		sp_listacolumnas CFG_CB_NivelesAtencion_Miembros 
	--Update CFG_CB_NivelesAtencion_Miembros Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  		

	Insert Into CFG_CB_NivelesAtencion_Miembros (  IdEstado, IdCliente, IdNivel, IdFarmacia, FechaUpdate, Status, Actualizado ) 
	Select  IdEstado, IdCliente, IdNivel, @IdFarmaciaNueva as IdFarmacia, FechaUpdate, Status, Actualizado
	From CFG_CB_NivelesAtencion_Miembros C (NoLock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
		and Not Exists 
		( 
			Select * 
			From CFG_CB_NivelesAtencion_Miembros M (NoLock) 
			Where C.IdEstado = M.IdEstado and M.IdFarmacia = @IdFarmaciaNueva 
		) 

	Delete CFG_CB_NivelesAtencion_Miembros 
	From CFG_CB_NivelesAtencion_Miembros CB 
	Where IdEstado = @IdEstado And IdFarmacia In ( Select IdFarmacia From CatFarmacias_Migracion M (NoLock) Where CB.IdEstado = M.IdEstado )  
	--------------------------------- CUADRO BASICO  	
	

	--------------------------------- FARMACIAS CONVENIO 
	--		sp_listacolumnas CFG_Farmacias_ConvenioVales 
	
	-- Update CFG_Farmacias_ConvenioVales Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  

	Insert Into CFG_Farmacias_ConvenioVales (  IdEstado, IdFarmacia, IdFarmaciaConvenio, Status, Actualizado ) 
	Select  IdEstado, @IdFarmaciaNueva as IdFarmacia, IdFarmaciaConvenio, Status, Actualizado
	From CFG_Farmacias_ConvenioVales C (NoLock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
		and Not Exists 
		( 
			Select * 
			From CFG_CB_NivelesAtencion_Miembros M (NoLock) 
			Where C.IdEstado = M.IdEstado and M.IdFarmacia = @IdFarmaciaNueva 
		) 

	Delete CFG_Farmacias_ConvenioVales 
	From CFG_Farmacias_ConvenioVales CB 
	Where IdEstado = @IdEstado And IdFarmacia In ( Select IdFarmacia From CatFarmacias_Migracion M (NoLock) Where CB.IdEstado = M.IdEstado )  
	--------------------------------- FARMACIAS CONVENIO 			

	
	
	
		

	----   Update CFG_EmpresasFarmacias Set IdFarmacia = @IdFarmaciaNueva Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	



	-- Se eliminan los registros de la tabla Net_Usuarios ya que este no cuenta con IdFarmacia sino IdSucursal.
	if (@IdEmpresaNueva = @IdEmpresa)
	Begin 
		Delete From Net_Grupos_Usuarios_Miembros Where IdEstado = @IdEstado And IdSucursal = @IdFarmacia 
		Delete From Net_Usuarios Where IdEstado = @IdEstado And IdSucursal = @IdFarmacia
	End
		Delete From CFGC_Terminales Where EsServidor = 0


--- Respaldo Final 
	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Generando respaldo', 95 
	Exec spp_Mtto_CierreDeInventario_005 @sBD, @RutaRespaldo, 'Despues_Depuracion' 
			

End 
Go--#SQL 

--		select * from Net_CFGC_Respaldos  

