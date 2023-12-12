-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Spp_Rpt_INT_Receta__AMPM' And xType = 'P' )
	Drop Proc Spp_Rpt_INT_Receta__AMPM
Go--#SQL

Create Procedure Spp_Rpt_INT_Receta__AMPM
( 
	@IdEmpresa Varchar(3) = '002', @IdEstado Varchar(2) = '09', @IdFarmacia Varchar(4) = '0101', @FolioReceta Varchar(30) = '000000000001'
) 
With Encryption
As
Begin
	Declare
		--@Folio Varchar(30),
		--@Clinica Varchar(20),
		@Descripcion varchar(200),
		@Direccion Varchar(500),
		@Licencia_Sanitaria Varchar(30),
		@Clue Varchar(30)
		--@sSql Varchar(Max),
	--	@sWhereReferenciaDocto Varchar(300),
	--	@sWhereProveedor Varchar(300)
		
	--Set @sWhereReferenciaDocto = ''
	--Set @sWhereProveedor = ''
	
	--Select @Folio = Max(Folio)
	--From INT_AMPM__RecetasElectronicas_0001_General
	--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And FolioReceta = @FolioReceta

	--Select @Clinica = UMedica
	--From INT_AMPM__RecetasElectronicas_XML G (NoLock)
	--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And Folio = @FolioReceta

	--Select @Descripcion = Descripcion, @Direccion = Direccion, @Licencia_Sanitaria = Licencia_Sanitaria, @Clue = Clue
	--From INT_AMPM__Clinicas C (NoLock)
	--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And C.Referencia_AMPM = @Clinica


	Select @Descripcion = F.Descripcion, @Direccion = F.Direccion, @Licencia_Sanitaria = F.Licencia_Sanitaria, @Clue = F.Clue
	From INT_AMPM__Clinicas F (NoLock)
	Inner Join INT_AMPM__CFG_FarmaciasClinicas C (NoLock)
	On (F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado And F.Referencia_AMPM = C.Referencia_AMPM)
	Where C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado And C.IdFarmacia = @IdFarmacia

	
	Select
		G.IdEmpresa, G.IdEstado, G.IdFarmacia, G.Folio, EsSurtido, FolioSurtido, FechaDeSurtido, EsCancelado, FechaDeCancelacion, FechaSolicitudDeCancelacion,
		CIE10, IsNull(C.Descripcion, '') As DescripcionCIE10, NHC,
		FolioReceta, FechaReceta, FechaEnvioReceta, FolioConsulta, G.IdUsuario, IdEstudiosPaciente, Indicaciones, G.Diagnostico As DiagnosticoEncabezado, FolioAfiliacionSPSS,
		FechaIniciaVigencia, FechaTerminaVigencia, Expediente,
		NombreBeneficiario, ApPaternoBeneficiario, ApMaternoBeneficiario,
		upper(ApPaternoBeneficiario + ' ' + ApMaternoBeneficiario + ' ' + NombreBeneficiario) As Beneficiario,
		Sexo, FechaNacimientoBeneficiario,
		ClaveDeMedico, NombreMedico, ApPaternoMedico, ApMaternoMedico, Upper(ApPaternoMedico + ' ' + ApMaternoMedico + ' ' + NombreMedico) As Medico,
		CedulaDeMedico, LicenciaturaDeMedico, 
		
		FirmaImagen, 
		--CAST(N'' as XML).value('xs:base64Binary(sql:column("FirmaDigital"))', 'varbinary(max)') as FirmaDigital, 
				
		procedencia,
		IdEnfermeria, Estatura, Peso, Cintura, PerimetroCadera, DiametroCefalico, DiametroAbdominal, PresionSistolica, PresionDiastolica,
		FrecuenciaCardiaca, FrecuenciaRespiratoria, Temperatura, S.IdUsuario As IdUsuario_Somatometria, FechaEnfermeria, HoraEnfermeria, Glucosa, TipoGlucosa,
		S.Diagnostico, S.Observaciones As Observaciones_Somatometria,
		I.CodigoEAN, IsNull(P.DescripcionCorta, '') As DescripcionCorta, IsNull(P.Descripcion, '') As Descripcion, IsNull(P.Presentacion, '') As Presentacion,
		IsNull(ContenidoPaquete, '') As ContenidoPaquete,
		CantidadRequerida, CantidadEntregada, Existencia, IdMedicina,
		Via, V.Descripcion As ViaDescripcion, Dosis,
		IsNull(ClaveSSA, '') As ClaveSSA, IsNull(ClaveSSA_Base, '') As ClaveSSA_Base, Unidades, IsNull(U.Descripcion, '') As UnidadesDescripcion, Frecuencia, IsNull(F.Descripcion, '') As FrecuenciaDescripcion,
		FechaInicio, FechaFin, DATEDIFF(DD, FechaInicio, FechaFin) As Dias,  I.Observaciones As Observaciones_Insumos,	
		@Descripcion As Clinica, @Direccion As Direccion_Clinica, @Licencia_Sanitaria As Licencia_Sanitaria, @Clue As Clue, GetDate() As FechaConsulta
	From INT_AMPM__RecetasElectronicas_0001_General G (NoLock)
	Inner Join INT_AMPM__RecetasElectronicas_0002_Somatometria S (NoLock) On ( G.IdEmpresa = S.IdEmpresa And G.IdEstado = S.IdEstado And G.IdFarmacia = S.IdFarmacia And G.Folio = S.Folio )
	Inner Join INT_AMPM__RecetasElectronicas_0004_Insumos I (NoLock) On ( G.IdEmpresa = I.IdEmpresa And G.IdEstado = I.IdEstado And G.IdFarmacia = I.IdFarmacia And G.Folio = I.Folio )
	Left Join vw_Productos_CodigoEAN P (NoLock) On ( I.CodigoEAN = P.CodigoEAN )
	Left Join INT_AMPM__CatGenerales V (NoLock) On ( I.Via = V.ID )
	Left Join INT_AMPM__CatGenerales F (NoLock) On ( I.Frecuencia = F.ID )
	Left Join INT_AMPM__CatGenerales U (NoLock) On ( I.Unidades = U.ID ) 
	--Left Join INT_AMPM__MedicosFirmas FM (NoLock) On ( G.FirmaImagen = FM.NombreFirma ) 
	--Inner Join INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo I (NoLock) On (G.IdEmpresa = I.IdEmpresa And G.IdEstado = I.IdEstado And G.Folio = I.Folio)
	Left Join CatCIE10_Diagnosticos C (NoLock) On (G.CIE10 = C.ClaveDiagnostico)
	Where G.IdEmpresa = @IdEmpresa And G.IdEstado = @IdEstado  And G.IdFarmacia = @IdFarmacia  And G.Folio = @FolioReceta


	--Inner Join INT_AMPM__Clinicas C (NoLock) On (C.)


End 
Go--#SQL