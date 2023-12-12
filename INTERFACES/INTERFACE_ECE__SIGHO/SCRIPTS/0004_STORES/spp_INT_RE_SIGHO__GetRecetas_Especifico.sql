-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__GetRecetas_Especifico' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__GetRecetas_Especifico
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__GetRecetas_Especifico
( 
	@CLUES Varchar(200) = 'GTSSA002101', @FolioDeReceta varchar(50) = '00013106'  
) 
With Encryption 
As 
Begin 

Set NoCount On 
Set DateFormat YMD   

Declare 
	@sFolio varchar(20),  
	@sMensaje varchar(200),
	@IDiasAtras int,
	@Tipo_De_Documento Varchar(3),
	@Tipo_De_Documento_Aux Varchar(3)

	Set @Tipo_De_Documento = ''
	Set @Tipo_De_Documento_Aux = ''
	Set @IDiasAtras = 2



	---------------------------- OBTENCION DE INFORMACION  
	Select R.Clues, R.IdEstado, R.IdUMedica, R.Folio 
	Into #tmp_ListaDeRecetas 
	From vw_REC_Recetas_Enc R (NoLock)  
	Inner Join REC_Recetas IR (NoLock) On ( R.IdEstado = IR.IdEstado and R.IdUMedica = IR.IdUMedica and R.Folio = IR.FolioReceta ) 
	Where R.Status = 'A' And CLUES = @CLUES and R.Folio = @FolioDeReceta 


	Update R Set Descargada = 1  
	From REC_Recetas R (NoLock)  
	Inner Join #tmp_ListaDeRecetas F (NoLock) On ( R.IdUMedica = F.IdUMedica and R.IdEstado = F.IdEstado and R.FolioReceta = F.Folio )  
	Where R.Descargada = 0 



	-------------------------------------------------------------------------------------------------------------------------------------------------------------   
	Select R.CLUES, R.IdEstado, R.IdUMedica, R.Folio, Convert(Varchar(10), R.FechaRegistro, 120) As FechaReceta, 
		R.Poliza as NumeroReferencia, 
		(case when R.TipoBeneficiario = 1 Then R.Poliza else '' End) As FolioAfiliacionSPSS, 
		Convert(Varchar(10), DATEADD(YEAR, -1, VigenciaPoliza), 120) As FechaIniciaVigencia,
		Convert(Varchar(10), VigenciaPoliza, 120) As FechaTerminaVigencia, 
		R.Beneficiario As NombreBeneficiario, 
		(Case When R.Sexo = 'Masculino' Then 1 Else 0 End) As Sexo, Convert(Varchar(10), 
		DATEADD(mm, (R.EdadFiltro * -1),  Getdate()), 120) As FechaNacimientoBeneficiario,
		(case when R.TipoBeneficiario = 2 Then R.Poliza else '' End) As FolioAfiliacionOportunidades, 
		(case when R.TipoBeneficiario = 3 Then 1 else 0 End) As EsPoblacionAbierta,
		R.NombreMedico, R.CedulaMedico As CedulaDeMedico, R.TipoBeneficiario, R.TipoBeneficiarioDesc
	into #tmp_Recetas 
	From vw_REC_Recetas_Enc R (NoLock)  
	Inner Join #tmp_ListaDeRecetas F (NoLock) On ( R.CLUES = F.CLUES and R.IdEstado = F.IdEstado and R.Folio = F.Folio )  


	Update R Set NumeroReferencia = 'S/P/OTROS'
	From #tmp_Recetas R 
	Where TipoBeneficiario = 0  

	Update R Set NumeroReferencia = 'S/P/D'
	From #tmp_Recetas R 
	Where TipoBeneficiario = 2   

	Update R Set NumeroReferencia = 'S/P/PA'
	From #tmp_Recetas R 
	Where TipoBeneficiario = 3  

	Update R Set 
		FechaIniciaVigencia = convert(varchar(10), dateadd(year, -1, getdate()), 120), 
		FechaTerminaVigencia = convert(varchar(10), dateadd(year, 5, getdate()), 120)
	From #tmp_Recetas R 
	Where TipoBeneficiario <> 1    


	-------------------------------------------------------------------------------------------------------------------------------------------------------------   
	Select CLUES, FolioReceta As Folio, Beneficio As NoIntervencionCause 
	into #tmp_Recetas_Beneficios  
	From REC_Recetas_Beneficios_CAUSES  B (NoLock)
	Inner Join #tmp_ListaDeRecetas R (NoLock) On (B.IdEstado = R.IdEstado And B.IdUMedica = R.IdUMedica And B.FolioReceta = R.Folio) 
	-- Where R.Status = 'A' And CLUES = @CLUES And TipoDocumento = @Tipo_De_Documento And TipoDocumento = @Tipo_De_Documento_Aux And DATEDIFF(DD, FechaRegistro, GetDate()) <= @IDiasAtras
  

	-------------------------------------------------------------------------------------------------------------------------------------------------------------   
	Select CLUES, FolioReceta As Folio, CIE_10 As CIE10, Descripcion 
	into #tmp_Recetas_Diagnosticos   
	From REC_Recetas_Diagnosticos  B (NoLock) 
	inner Join #tmp_ListaDeRecetas R (NoLock) On (B.IdEstado = R.IdEstado And B.IdUMedica = R.IdUMedica And B.FolioReceta = R.Folio) 
	-- Where R.Status = 'A' And CLUES = @CLUES And TipoDocumento = @Tipo_De_Documento And TipoDocumento = @Tipo_De_Documento_Aux And DATEDIFF(DD, FechaRegistro, GetDate()) <= @IDiasAtras


	-------------------------------------------------------------------------------------------------------------------------------------------------------------   
	Select CLUES, FolioReceta As Folio, ClaveSSA, Cantidad As CantidadRequerida, CantidadSurtida As CantidadEntregada
	into #tmp_Recetas_Claves 
	From REC_Recetas_ClavesSSA   B (NoLock)
	Inner Join #tmp_ListaDeRecetas R (NoLock) On (B.IdEstado = R.IdEstado And B.IdUMedica = R.IdUMedica And B.FolioReceta = R.Folio) 
	-- Where R.Status = 'A' And CLUES = @CLUES And TipoDocumento = @Tipo_De_Documento And TipoDocumento = @Tipo_De_Documento_Aux And DATEDIFF(DD, FechaRegistro, GetDate()) <= @IDiasAtras
	---------------------------- OBTENCION DE INFORMACION  
	
		

-------------------------------------------- SALIDA FINAL 
	Select * From #tmp_Recetas 
	Select * From #tmp_Recetas_Beneficios 
	Select * From #tmp_Recetas_Diagnosticos 
	Select * From #tmp_Recetas_Claves 


End 
Go--#SQL 
	