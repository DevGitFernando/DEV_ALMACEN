-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_AMPM__RecetasElectronicas_0008_ObtenerRecetasParaImpresion' and xType = 'P' ) 
   Drop Proc spp_INT_RE_AMPM__RecetasElectronicas_0008_ObtenerRecetasParaImpresion
Go--#SQL 

Create Proc spp_INT_RE_AMPM__RecetasElectronicas_0008_ObtenerRecetasParaImpresion 
( 
	@IdEmpresa varchar(3) = '002', 
	@IdEstado varchar(2) = '09', 
	@IdFarmacia varchar(4) = '0011',
	@FolioReceta Varchar(30) = '41',
	@NombreBeneficiario Varchar(50) = '',
	@ApPaternoBeneficiario Varchar(50) = '',
	@ApMaternoBeneficiario Varchar(50) = ''
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
		@sSql Varchar(max) = ''


------------------------------------------------- OBTENER LA INFORMACION   
	Set @sSql = ' Select 
		X.Folio as Secuenciador, X.FolioReceta, X.FechaReceta, 
		(x.NombreBeneficiario + ' + Char(39) + ' '  + Char(39) +  ' + x.ApPaternoBeneficiario + ' + Char(39) + ' ' + Char(39) + ' + x.ApMaternoBeneficiario) as NombreCompletoBeneficiario, 	
		(x.NombreMedico + '  + Char(39) + ' '  + Char(39) + ' + x.ApPaternoMedico + ' + Char(39) + ' ' + Char(39) +  ' + x.ApMaternoMedico) as NombreCompletoMedico,  
		x.NombreBeneficiario, x.ApPaternoBeneficiario, x.ApMaternoBeneficiario, x.FolioAfiliacionSPSS as NumeroReferencia, x.Sexo, 
		x.FechaIniciaVigencia, x.FechaTerminaVigencia, 
		x.ClaveDeMedico, x.CedulaDeMedico, x.NombreMedico, x.ApPaternoMedico, x.ApMaternoMedico 
		--, identity(int, 1, 1) as Keyx  
	From INT_AMPM__RecetasElectronicas_0001_General X (NoLock)
	Where X.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' and X.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' and X.IdFarmacia = ' + Char(39) + @IdFarmacia  + Char(39) + '
			  And X.FolioReceta like ' + Char(39) + '%' + @FolioReceta + '%' + Char(39) +
			' And X.NombreBeneficiario like ' + Char(39) + '%' + @NombreBeneficiario + '%' + Char(39) +
			' And X.ApPaternoBeneficiario like ' + Char(39) + '%' + @ApPaternoBeneficiario + '%' + Char(39) +
			' And X.ApMaternoBeneficiario like ' + Char(39) + '%' + @ApMaternoBeneficiario + '%' + Char(39) + '
	Order By Secuenciador Desc'
	

	Exec (@sSql)

------------------------------------------------- OBTENER LA INFORMACION   	



	
	
--------------------------- SALIDA FINAL DE DATOS 
	--Select * From #tmp_01_General 
	----Select * From #tmp_02_Medicamentos Order by ClaveSSA 
	----Select * From #tmp_03_Vales_Medicamentos Order by ClaveSSA 	
	----Select * From #tmp_04_SurtidoOtraUnidad Order by ClaveSSA 
	----Select * From #tmp_05_SurtidoOtraUnidad_Medicamentos Order by ClaveSSA 
	
	
End 
Go--#SQL 
	