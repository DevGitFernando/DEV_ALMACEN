-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_0001_General' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_0001_General
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_0001_General 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '',

	@CLUES_Emisora varchar(50) = '', 
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
	@CedulaDeMedico varchar(50) = ''
	  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)
	
	
	Select @sFolio = Folio 
	From INT_RE_SIGHO__RecetasElectronicas_XML (NoLock)
	Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And Folio_SIADISSEP = @FolioReceta And uMedica = @CLUES_Emisora


	If @FolioAfiliacionSPSS = 'PA' or @FolioAfiliacionSPSS = ''  
	Begin 
		Set @FolioAfiliacionSPSS = 'PA' 
		Set @FechaIniciaVigencia = convert(varchar(10), getdate(), 120) 
		Set @FechaTerminaVigencia = convert(varchar(10), getdate() + 60, 120)  		
	End 


	If Not Exists ( Select * From INT_RE_SIGHO__RecetasElectronicas_0001_General (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio 
						) 
		Begin 
			Insert Into INT_RE_SIGHO__RecetasElectronicas_0001_General 
			( 
				IdEmpresa, IdEstado, IdFarmacia, Folio, CLUES_Emisora, FolioReceta, FechaReceta, FechaEnvioReceta, 
				FolioAfiliacionSPSS, FechaIniciaVigencia, FechaTerminaVigencia, 
				NombreBeneficiario, ApPaternoBeneficiario, ApMaternoBeneficiario, Sexo, FechaNacimientoBeneficiario, 
				Expediente, FolioAfiliacionOportunidades, EsPoblacionAbierta, 
				ClaveDeMedico, NombreMedico, ApPaternoMedico, ApMaternoMedico, CedulaDeMedico 
			) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @sFolio, @CLUES_Emisora, @FolioReceta, @FechaReceta, @FechaEnvioReceta, 
				@FolioAfiliacionSPSS, @FechaIniciaVigencia, @FechaTerminaVigencia, 
				@NombreBeneficiario, @ApPaternoBeneficiario, @ApMaternoBeneficiario, @Sexo, @FechaNacimientoBeneficiario, 
				@Expediente, @FolioAfiliacionOportunidades, @EsPoblacionAbierta, 
				@ClaveDeMedico, @NombreMedico, @ApPaternoMedico, @ApMaternoMedico, @CedulaDeMedico 			 
		End
	Else
		Begin
			Update INT_RE_SIGHO__RecetasElectronicas_0001_General Set FechaProcesado = getdate()
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio 
		End
		
	
	Select @sFolio As Folio
	
End 
Go--#SQL 
	