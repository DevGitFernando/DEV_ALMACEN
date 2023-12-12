-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_AMPM__RecetasElectronicas_0002_Somatometria' and xType = 'P' ) 
   Drop Proc spp_INT_AMPM__RecetasElectronicas_0002_Somatometria
Go--#SQL 

Create Proc spp_INT_AMPM__RecetasElectronicas_0002_Somatometria 
( 
	@IdEmpresa varchar(3) = '',
	@IdEstado varchar(2) = '',
	@IdFarmacia varchar(4) = '',
	@Folio varchar(12) = '',

	@IdEnfermeria varchar(10) = '',
	@Estatura numeric(14,4) = 0,
	@Peso numeric(14,4) = 0,
	@Cintura numeric(14,4) = 0,
	@PerimetroCadera numeric(14,4) = 0,
	@DiametroCefalico numeric(14,4) = 0,
	@DiametroAbdominal numeric(14,4) = 0,

	@PresionSistolica numeric(14,4) = 0,
	@PresionDiastolica numeric(14,4) = 0,
	@FrecuenciaCardiaca numeric(14,4) = 0,
	@FrecuenciaRespiratoria numeric(14,4) = 0,
	@Temperatura numeric(14,4) = 0,
	@IdUsuario varchar(10) = '',
	@FechaEnfermeria varchar(10) = '',
	@HoraEnfermeria varchar(10) = '',

	@Glucosa varchar(100) = '',
	@TipoGlucosa varchar(100) = '', 

	@Diagnostico varchar(500) = '',
	@Observaciones varchar(500) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	--@sFolio varchar(20), 
	@sMensaje varchar(200)
	
	--Select @sFolio = Folio 
	--From INT_RE_INTERMED__RecetasElectronicas_XML (NoLock)
	--Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And Folio_SIADISSEP = @FolioReceta And  uMedica = @Clues_Emisor


	If Not Exists ( Select * From INT_AMPM__RecetasElectronicas_0002_Somatometria (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio) 
	Begin 
		Insert Into INT_AMPM__RecetasElectronicas_0002_Somatometria
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, IdEnfermeria, Estatura, Peso, Cintura, PerimetroCadera, DiametroCefalico, 
			DiametroAbdominal, PresionSistolica, PresionDiastolica, FrecuenciaCardiaca, FrecuenciaRespiratoria, Temperatura, 
			IdUsuario, FechaEnfermeria, HoraEnfermeria, Glucosa, TipoGlucosa, Diagnostico, Observaciones 	
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @IdEnfermeria, @Estatura, @Peso, @Cintura, @PerimetroCadera, @DiametroCefalico, 
			@DiametroAbdominal, @PresionSistolica, @PresionDiastolica, @FrecuenciaCardiaca, @FrecuenciaRespiratoria, @Temperatura, 
			@IdUsuario, @FechaEnfermeria, @HoraEnfermeria, @Glucosa, @TipoGlucosa, @Diagnostico, @Observaciones 	
	End 
	
End 
Go--#SQL 
	