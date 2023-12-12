-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_AMPM__RecetasElectronicas_0001_General' and xType = 'P' ) 
   Drop Proc spp_INT_AMPM__RecetasElectronicas_0001_General
Go--#SQL 

Create Proc spp_INT_AMPM__RecetasElectronicas_0001_General 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '',
	@Folio varchar(12) = '',

	@FolioReceta varchar(50) = '', 
	@FechaReceta varchar(30) = '', 
	@FechaEnvioReceta varchar(30) = '', 

	@FolioConsulta varchar(50) = '', 
	@IdUsuario varchar(50) = '', 
	@IdEstudiosPaciente varchar(50) = '', 
	@Indicaciones varchar(500) = '', 
	@Diagnostico varchar(500) = '',
	
	@FolioAfiliacionSPSS varchar(50) = '', 	
	@FechaIniciaVigencia varchar(30) = '', 
	@FechaTerminaVigencia varchar(30) = '',

	@Expediente varchar(50) = '', 
	@NombreBeneficiario varchar(100) = '', 		
	@ApPaternoBeneficiario varchar(100) = '', 		
	@ApMaternoBeneficiario varchar(100)= '', 		
	@Sexo varchar(1) = '', 	
	@FechaNacimientoBeneficiario varchar(10) = '', 
	
		
	@ClaveDeMedico varchar(50) = '', 			
	@NombreMedico varchar(100) = '', 		
	@ApPaternoMedico varchar(100) = '', 		
	@ApMaternoMedico varchar(100) = '', 	
	@CedulaDeMedico varchar(50) = '',
	@LicenciaturaDeMedico varchar(100) = '',   	
	@FirmaImagen varchar(300) = '',
	@procedencia  varchar(300),
	@CIE10 Varchar(30) =  '',
	@NHC Varchar(30) = ''


	--EsSurtido bit Not Null Default 'False',  
	--FolioSurtido varchar(10) Not Null Default '', 		
	--FechaDeSurtido datetime Default getdate(), 

	--EsCancelado bit Not Null Default 'False', 
	--FechaDeCancelacion datetime Default getdate(), 
	--FechaSolicitudDeCancelacion datetime Default getdate(), 
	  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)
	
	
	Select @sFolio = Folio 
	From INT_AMPM__RecetasElectronicas_0001_General (NoLock)
	Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And Folio = @FolioReceta


	if (@FechaReceta = '')
	Begin
		Set @FechaReceta = GetDate()
	End

	if (@FechaEnvioReceta = '')
	Begin
		Set @FechaEnvioReceta = GetDate()
	End


	If Not Exists ( Select * From INT_AMPM__RecetasElectronicas_0001_General (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio 
						) 
		Begin 
			Insert Into INT_AMPM__RecetasElectronicas_0001_General 
			( 
				IdEmpresa, IdEstado, IdFarmacia, Folio, FolioReceta, FechaReceta, FechaEnvioReceta, 
				FolioConsulta, IdUsuario, IdEstudiosPaciente, Indicaciones, Diagnostico, FolioAfiliacionSPSS, 	
				FechaIniciaVigencia, FechaTerminaVigencia, Expediente, NombreBeneficiario, ApPaternoBeneficiario, 		
				ApMaternoBeneficiario, Sexo, FechaNacimientoBeneficiario, ClaveDeMedico,
				NombreMedico, ApPaternoMedico, ApMaternoMedico, CedulaDeMedico, LicenciaturaDeMedico, FirmaImagen,
				procedencia, CIE10, NHC
			) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @FolioReceta, @FechaReceta, @FechaEnvioReceta, 
				@FolioConsulta, @IdUsuario, @IdEstudiosPaciente, @Indicaciones, @Diagnostico, @FolioAfiliacionSPSS, 	
				@FechaIniciaVigencia, @FechaTerminaVigencia, @Expediente, @NombreBeneficiario, @ApPaternoBeneficiario, 		
				@ApMaternoBeneficiario, @Sexo, @FechaNacimientoBeneficiario, @ClaveDeMedico,
				@NombreMedico, @ApPaternoMedico, @ApMaternoMedico, @CedulaDeMedico, @LicenciaturaDeMedico, @FirmaImagen,
				@procedencia, @CIE10, @NHC
		End
	Else
		Begin
			Update INT_AMPM__RecetasElectronicas_0001_General Set FechaProcesado = getdate()
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio 
		End
	
End 
Go--#SQL 
	