-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_0009_ObtenerRecetasParaSurtido' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_0009_ObtenerRecetasParaSurtido
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_0009_ObtenerRecetasParaSurtido 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', 
	@IdFarmacia varchar(4) = '0021' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200),
	@IDiasAtras int
	
	Set @IDiasAtras = 3


------------------------------------------------- OBTENER LA INFORMACION   
	Select 
		X.Folio as Secuenciador, X.FolioReceta, X.FechaReceta, 
		(x.NombreBeneficiario + ' ' + x.ApPaternoBeneficiario + ' ' + x.ApMaternoBeneficiario) as NombreCompletoBeneficiario, 	
		(x.NombreMedico + ' ' + x.ApPaternoMedico + ' ' + x.ApMaternoMedico) as NombreCompletoMedico,  
		x.NombreBeneficiario, x.ApPaternoBeneficiario, x.ApMaternoBeneficiario, x.FolioAfiliacionSPSS as NumeroReferencia, x.Sexo, 
		x.FechaIniciaVigencia, x.FechaTerminaVigencia, 
		x.ClaveDeMedico, x.CedulaDeMedico, x.NombreMedico, x.ApPaternoMedico, x.ApMaternoMedico 
		--, identity(int, 1, 1) as Keyx  
	--Into #tmp_01_General 
	From INT_RE_SIGHO__RecetasElectronicas_0001_General X (NoLock)
	Where X.EsSurtido = 0 and X.Procesado = 0 And DATEDIFF(DD, FechaProcesado, GetDate()) <= @IDiasAtras
		and X.IdEmpresa = @IdEmpresa and X.IdEstado = @IdEstado and X.IdFarmacia = @IdFarmacia
	Order By Secuenciador Desc


------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SIADISSEP__RecetasElectronicas_0009_ObtenerRecetasParaSurtido 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	--Select * From #tmp_01_General 
	----Select * From #tmp_02_Medicamentos Order by ClaveSSA 
	----Select * From #tmp_03_Vales_Medicamentos Order by ClaveSSA 	
	----Select * From #tmp_04_SurtidoOtraUnidad Order by ClaveSSA 
	----Select * From #tmp_05_SurtidoOtraUnidad_Medicamentos Order by ClaveSSA 
	
	
End 
Go--#SQL 
	