-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0001_General' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0001_General
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0001_General 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@Folio varchar(12) = '', 

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
	@idPaciente Varchar(50) = ''
	  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	If Not Exists ( Select * From INT_SIADISSEP__RecetasElectronicas_0001_General (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
						) 
	Begin 
		Insert Into INT_SIADISSEP__RecetasElectronicas_0001_General 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, FolioReceta, FechaReceta, FechaEnvioReceta, 
			FolioAfiliacionSPSS, FechaIniciaVigencia, FechaTerminaVigencia, 
			NombreBeneficiario, ApPaternoBeneficiario, ApMaternoBeneficiario, Sexo, FechaNacimientoBeneficiario, 
			Expediente, FolioAfiliacionOportunidades, EsPoblacionAbierta, 
			ClaveDeMedico, NombreMedico, ApPaternoMedico, ApMaternoMedico, CedulaDeMedico,
			FolioColectivo, idTipoServicio, idServicio, CamaPaciente, idEpisodio, idPaciente
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @FolioReceta, @FechaReceta, @FechaEnvioReceta, 
			@FolioAfiliacionSPSS, @FechaIniciaVigencia, @FechaTerminaVigencia, 
			@NombreBeneficiario, @ApPaternoBeneficiario, @ApMaternoBeneficiario, @Sexo, @FechaNacimientoBeneficiario, 
			@Expediente, @FolioAfiliacionOportunidades, @EsPoblacionAbierta, 
			@ClaveDeMedico, @NombreMedico, @ApPaternoMedico, @ApMaternoMedico, @CedulaDeMedico,
			@FolioColectivo, @idTipoServicio, @idServicio, @camaPaciente, @idEpisodio, @idPaciente
	End 
	
End 
Go--#SQL 
	