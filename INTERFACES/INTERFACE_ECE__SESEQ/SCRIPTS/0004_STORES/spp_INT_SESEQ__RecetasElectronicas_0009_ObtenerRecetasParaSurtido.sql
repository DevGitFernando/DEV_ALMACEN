-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0009_ObtenerRecetasParaSurtido' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0009_ObtenerRecetasParaSurtido
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0009_ObtenerRecetasParaSurtido 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '3193', 
	@Folio varchar(max) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200),
	@iDiasAtras int
	
	Set @iDiasAtras = 7

	If @Folio <> '' 
	Begin 
		Set @iDiasAtras = 15 
	End 


------------------------------------------------- OBTENER LA INFORMACION   
	Set @Folio = REPLACE(@Folio, ' ', '%') 

	Select 
		G.Folio as Secuenciador, G.FolioReceta, G.FechaReceta, 
		(G.NombreBeneficiario + ' ' + G.ApPaternoBeneficiario + ' ' + G.ApMaternoBeneficiario) as NombreCompletoBeneficiario, 	
		(G.NombreMedico + ' ' + G.ApPaternoMedico + ' ' + G.ApMaternoMedico) as NombreCompletoMedico,  
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.FolioAfiliacionSPSS as NumeroReferencia, G.Sexo, 
		G.FechaIniciaVigencia, G.FechaTerminaVigencia, 
		G.ClaveDeMedico, G.CedulaDeMedico, G.NombreMedico, G.ApPaternoMedico, G.ApMaternoMedico 
		, identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
	Where G.EsSurtido = 0 and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and datediff(dd, X.FechaRegistro, getdate()) <= @iDiasAtras  
		and G.RecepcionDuplicada = 0 
		and FolioReceta like '%' + @Folio + '%' 
	Order By Secuenciador Desc 

------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SESEQ__RecetasElectronicas_0009_ObtenerRecetasParaSurtido 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * From #tmp_01_General 
	Order by FolioReceta desc 
	----Select * From #tmp_02_Medicamentos Order by ClaveSSA 
	----Select * From #tmp_03_Vales_Medicamentos Order by ClaveSSA 	
	----Select * From #tmp_04_SurtidoOtraUnidad Order by ClaveSSA 
	----Select * From #tmp_05_SurtidoOtraUnidad_Medicamentos Order by ClaveSSA 
	
	
End 
Go--#SQL 
	