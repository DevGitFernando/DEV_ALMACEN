-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0001_General' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0001_General
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0001_General 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@Folio varchar(12) = '', 
	@FolioXML varchar(12) = '', 

	@FolioReceta varchar(50) = '', 
	@FechaReceta varchar(30) = '', 
	@FechaEnvioReceta varchar(30) = '', 
	
	@FolioAfiliacionSPSS varchar(50) = '', 	
	@FechaIniciaVigencia varchar(30) = '', 
	@FechaTerminaVigencia varchar(30) = '', 
	@Expediente varchar(50) = '', 
	@NombreBeneficiario varchar(100) = '', 		
	@ApPaternoBeneficiario varchar(100) = '', 		
	@ApMaternoBeneficiario varchar(100)= '', 		
	@Sexo varchar(1) = '', 	
	@FechaNacimientoBeneficiario varchar(10) = '', 
	
	@FolioAfiliacionOportunidades varchar(50) = '', 		
	@EsPoblacionAbierta varchar(4) = '', 
		
	@ClaveDeMedico varchar(50) = '', 			
	@NombreMedico varchar(100) = '', 		
	@ApPaternoMedico varchar(100) = '', 		
	@ApMaternoMedico varchar(100) = '', 	
	@CedulaDeMedico varchar(50) = '',
	@FolioColectivo Varchar(50)= '',
	@idTipoServicio Varchar(50) = '',
	@idServicio Varchar(50) = '',
	@camaPaciente Varchar(30) = '',
	@idEpisodio Varchar(50) = '',
	@idPaciente Varchar(50) = '', 
	@TipoDeProceso smallint = 1, 
	
	@EsResurtible bit = 0, 
	@NumeroDePartidas int = 1 
 

) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@iTiene_RecepcionPrevia int, 
	@iNumeroDeRecepciones int, 
	@iPartidas int 


	----Select cast(max(cast(Folio as int)) + 1 as varchar) 
	----From INT_SESEQ__RecetasElectronicas_0001_General (NoLock) 
	----Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  

	Set @Folio = '' 
	Set @iTiene_RecepcionPrevia = 0  
	Set @iNumeroDeRecepciones = 1  


	If Exists ( Select * 
				From INT_SESEQ__RecetasElectronicas_0001_General E 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioReceta =  @FolioReceta )
	Begin 
		Set @iTiene_RecepcionPrevia = 1 

		Select @iNumeroDeRecepciones = count(*) + 0 
		From INT_SESEQ__RecetasElectronicas_0001_General E 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioReceta =  @FolioReceta  

		Set @iNumeroDeRecepciones = IsNull(@iNumeroDeRecepciones, 0) + 1 

	End
	
	Set @iTiene_RecepcionPrevia = IsNull(@iTiene_RecepcionPrevia, 0) 
	Set @iNumeroDeRecepciones = IsNull(@iNumeroDeRecepciones, 1) 


	----If @iTiene_RecepcionPrevia = 1  
	----Begin
	----	Set @Folio = right('00000000000000000000' + @Folio, 12) 

	----	Update E Set RecepcionDuplicada = 1, NumeroDeRecepciones = NumeroDeRecepciones + 1 
	----	From INT_SESEQ__RecetasElectronicas_0001_General E 
	----	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioReceta =  @FolioReceta 
	----End 

	--If @iTiene_RecepcionPrevia = 0 
	Begin 

		Select @Folio = cast(max(cast(Folio as int)) + 1 as varchar) 
		From INT_SESEQ__RecetasElectronicas_0001_General (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
	

		Set @Folio = IsNull(@Folio, '1') 
		Set @Folio = right('00000000000000000000' + @Folio, 12) 


		If Not Exists ( Select * From INT_SESEQ__RecetasElectronicas_0001_General (NoLock) 
						Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
							) 
		Begin 
			Insert Into INT_SESEQ__RecetasElectronicas_0001_General 
			( 
				IdEmpresa, IdEstado, IdFarmacia, Folio, FolioXML, TipoDeProceso, FolioReceta, FechaReceta, FechaEnvioReceta, 
				FolioAfiliacionSPSS, FechaIniciaVigencia, FechaTerminaVigencia, 
				NombreBeneficiario, ApPaternoBeneficiario, ApMaternoBeneficiario, Sexo, FechaNacimientoBeneficiario, 
				Expediente, FolioAfiliacionOportunidades, EsPoblacionAbierta, 
				ClaveDeMedico, NombreMedico, ApPaternoMedico, ApMaternoMedico, CedulaDeMedico,
				FolioColectivo, idTipoServicio, idServicio, CamaPaciente, idEpisodio, idPaciente, RecepcionDuplicada, NumeroDeRecepciones, 
				EsResurtible, NumeroDePartidas  
			) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @FolioXML, @TipoDeProceso, @FolioReceta, @FechaReceta, @FechaEnvioReceta, 
				@FolioAfiliacionSPSS, @FechaIniciaVigencia, @FechaTerminaVigencia, 
				@NombreBeneficiario, @ApPaternoBeneficiario, @ApMaternoBeneficiario, @Sexo, @FechaNacimientoBeneficiario, 
				@Expediente, @FolioAfiliacionOportunidades, @EsPoblacionAbierta, 
				@ClaveDeMedico, @NombreMedico, @ApPaternoMedico, @ApMaternoMedico, @CedulaDeMedico,
				@FolioColectivo, @idTipoServicio, @idServicio, @camaPaciente, @idEpisodio, @idPaciente, @iTiene_RecepcionPrevia, @iNumeroDeRecepciones, 
				@EsResurtible, @NumeroDePartidas  
		End 
	
	End 


	If @iTiene_RecepcionPrevia = 0 and @EsResurtible = 1 
	Begin 
		/* 
			@EsResurtible int = 0, 
			@NumeroDePartidas int = 1, 
			@ClavePrograma varchar(10) = '0'  
		*/ 
		Set @iPartidas = 1 

		While @iPartidas <= @NumeroDePartidas 
		Begin 
			Insert Into INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas ( IdEmpresa, IdEstado, IdFarmacia, Folio, FolioReceta, Partida ) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @FolioReceta, @iPartidas 

			Set @iPartidas = @iPartidas + 1 
		End 

	End 


	----------------- Regresar el resultado 
	Select @Folio as Folio, @iTiene_RecepcionPrevia as Tiene_RecepcionPrevia 


End 
Go--#SQL 
	