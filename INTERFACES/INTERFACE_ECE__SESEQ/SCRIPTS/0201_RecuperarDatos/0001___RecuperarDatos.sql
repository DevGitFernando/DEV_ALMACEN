
Set dateformat YMD 
Go--#SQL 


Begin tran 


--		Rollback tran 

--		commit tran 


Declare 
	@sSQL varchar(max), 
	@sTabla varchar(500),  
	@BaseDeDatosOrigen varchar(500),  
	@BaseDeDatosDestino varchar(500),  
	@BaseDeDatosEstructura varchar(500) 			


	Set @sSQL = '' 
	Set @sTabla = ''  

	Set @BaseDeDatosOrigen = 'INTERCALACION_ANTES_SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ'  
	Set @BaseDeDatosDestino = 'SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ'  
	Set @BaseDeDatosEstructura = 'SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ' 


	Select 'BORRADO' as Proceso 

	Declare #cursorTablas_Borrar   
	Cursor For 
		Select NombreTabla 
		From INT_SESEQ__CFG_Replicacion T 
		Order by Orden desc 
	Open #cursorTablas_Borrar  
	FETCH NEXT FROM #cursorTablas_Borrar  Into @sTabla 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
		
				Set @sSQL = 'Delete From ' + @BaseDeDatosDestino + '.dbo.' + @sTabla 
				Exec(@sSQL) 
				Select @sSQL as SQL, @@rowcount as Registros 
				Print '' 
				Print '' 

			FETCH NEXT FROM #cursorTablas_Borrar  Into @sTabla   
		END	 
	Close #cursorTablas_Borrar  
	Deallocate #cursorTablas_Borrar  	

	Select 'MIGRACION' as Proceso 


	Declare #cursorTablas   
	Cursor For 
		Select NombreTabla 
		From INT_SESEQ__CFG_Replicacion T 
		Where 1 = 0  
		Order by Orden 
	Open #cursorTablas  
	FETCH NEXT FROM #cursorTablas  Into @sTabla 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			--Set @sTabla = '[' + @sTabla + ']' 


			Exec SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.spp_CFG_Script_Integrar_BaseDeDatos 
				@BaseDeDatosOrigen = @BaseDeDatosOrigen, 
				@BaseDeDatosDestino = @BaseDeDatosDestino, 
				@BaseDeDatosEstructura = @BaseDeDatosEstructura,
				@Tabla = @sTabla, 
				@SalidaFinal = @sSQL output, 
				@Update = 1, 
				@SoloDiferencias = 0, 
				@Meses_FechaControl = 120, 
				@Criterio = [ ] 
 
			--    datosTablas.BaseDeDatos, datosCnn.BaseDeDatos, sTabla, 1, PreparaTablaMigrarTipo(sTabla), iMeses_Migracion, sFiltro_Where); 


				--Exec(@sSQL) 
				Select @sSQL as SQL, @@rowcount as Registros 

				Print '' 
				Print '' 


			FETCH NEXT FROM #cursorTablas  Into @sTabla   
		END	 
	Close #cursorTablas  
	Deallocate #cursorTablas  	




--Set NoCount On 
Update Bd_D Set Bd_D.URL_Interface = Bd_O.URL_Interface, Bd_D.CapturaInformacion = Bd_O.CapturaInformacion

Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__CFG_Farmacias_UMedicas] ( IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion  ) 

 
--Set NoCount On 
Update Bd_D Set Bd_D.FechaRegistro = Bd_O.FechaRegistro, Bd_D.UMedica = Bd_O.UMedica, Bd_D.Folio_SESEQ = Bd_O.Folio_SESEQ, Bd_D.TipoDeProceso = Bd_O.TipoDeProceso, Bd_D.DisponibleSurtido = Bd_O.DisponibleSurtido, Bd_D.Surtidos = Bd_O.Surtidos, Bd_D.Surtidos_Aplicados = Bd_O.Surtidos_Aplicados, Bd_D.InformacionXML = Bd_O.InformacionXML
 

Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__RecetasElectronicas_XML__Log] ( IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro, UMedica, Folio_SESEQ, TipoDeProceso, DisponibleSurtido, Surtidos, Surtidos_Aplicados, InformacionXML  ) 

--Set NoCount On 
Update Bd_D Set Bd_D.FechaRegistro = Bd_O.FechaRegistro, Bd_D.UMedica = Bd_O.UMedica, Bd_D.Folio_SESEQ = Bd_O.Folio_SESEQ, Bd_D.TipoDeProceso = Bd_O.TipoDeProceso, Bd_D.DisponibleSurtido = Bd_O.DisponibleSurtido, Bd_D.Surtidos = Bd_O.Surtidos, Bd_D.Surtidos_Aplicados = Bd_O.Surtidos_Aplicados, Bd_D.InformacionXML = Bd_O.InformacionXML
 

Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__RecetasElectronicas_XML] ( IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro, UMedica, Folio_SESEQ, TipoDeProceso, DisponibleSurtido, Surtidos, Surtidos_Aplicados, InformacionXML  ) 


--Set NoCount On 
Update Bd_D Set Bd_D.FolioXML = Bd_O.FolioXML, Bd_D.EsSurtido = Bd_O.EsSurtido, Bd_D.EsSurtido_Electronico = Bd_O.EsSurtido_Electronico, 
 

Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__RecetasElectronicas_0001_General] ( IdEmpresa, IdEstado, IdFarmacia, Folio, FolioXML, EsSurtido, EsSurtido_Electronico, FolioSurtido, FechaDeSurtido, EsCancelado, FechaDeCancelacion, FechaSolicitudDeCancelacion, FolioReceta, FechaReceta, FechaEnvioReceta, FolioColectivo, idTipoServicio, idServicio, idEpisodio, idPaciente, CamaPaciente, FolioAfiliacionSPSS, FechaIniciaVigencia, FechaTerminaVigencia, Expediente, NombreBeneficiario, ApPaternoBeneficiario, ApMaternoBeneficiario, Sexo, FechaNacimientoBeneficiario, FolioAfiliacionOportunidades, EsPoblacionAbierta, ClaveDeMedico, NombreMedico, ApPaternoMedico, ApMaternoMedico, CedulaDeMedico, Procesado, FechaProcesado, RecepcionDuplicada, NumeroDeRecepciones, IntentosDeEnvio  ) 


 
 



--Set NoCount On 
Update Bd_D Set Bd_D.DescripcionDiagnostico = Bd_O.DescripcionDiagnostico
 

Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__RecetasElectronicas_0003_Diagnosticos] ( IdEmpresa, IdEstado, IdFarmacia, Folio, CIE10, DescripcionDiagnostico  ) 

 
 
--Set NoCount On 
Update Bd_D Set Bd_D.TipoDeInsumo = Bd_O.TipoDeInsumo, Bd_D.CantidadRequerida = Bd_O.CantidadRequerida, Bd_D.CantidadEntregada = Bd_O.CantidadEntregada, Bd_D.EmitioVale = Bd_O.EmitioVale, Bd_D.CantidadVale = Bd_O.CantidadVale, Bd_D.RecepcionDuplicada = Bd_O.RecepcionDuplicada
 

Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__RecetasElectronicas_0004_Insumos] ( IdEmpresa, IdEstado, IdFarmacia, Folio, TipoDeInsumo, ClaveSSA, CantidadRequerida, CantidadEntregada, EmitioVale, CantidadVale, RecepcionDuplicada  ) 


----Set NoCount On 
--Update Bd_D Set Bd_D.FolioXML = Bd_O.FolioXML, Bd_D.FolioReceta = Bd_O.FolioReceta, Bd_D.FechaReceta = Bd_O.FechaReceta, Bd_D.FechaEnvioReceta = Bd_O.FechaEnvioReceta, Bd_D.Expediente = Bd_O.Expediente, Bd_D.Procesado = Bd_O.Procesado, Bd_D.FechaProcesado = Bd_O.FechaProcesado
 

--Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas] ( IdEmpresa, IdEstado, IdFarmacia, Folio, FolioXML, FolioReceta, FechaReceta, FechaEnvioReceta, Expediente, Procesado, FechaProcesado  ) 


------Set NoCount On 
----Update Bd_D Set Bd_D.Clave_Oracle = Bd_O.Clave_Oracle, Bd_D.Descripcion = Bd_O.Descripcion, Bd_D.Presentacion = Bd_O.Presentacion

----Insert Into SII_22_001_0013__HE_NINO_Y_MUJER_DR_FELIPE_NUNEZ.dbo.[INT_SESEQ__CGF_ClavesSIAM] ( ClaveSSA, Clave_SIAM, Clave_Oracle, Descripcion, Presentacion  ) 


 


Go--#SQL 


