-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_0012_ObetenerRecetasAtendidas' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_0012_ObetenerRecetasAtendidas
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_0012_ObetenerRecetasAtendidas
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	--Select U.Referencia_SIADISSEP As Clues, E.*
	--From INT_RE_SIGHO__RecetasElectronicas_0001_General E (NoLock)
	--Inner Join INT_RE_SIGHO__CFG_Farmacias_UMedicas U (NoLock)
	--	On (E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and E.IdFarmacia = U.IdFarmacia)
	--Where E.EsSurtido = 1 And DateDiff(DD, FechaDeSurtido, Getdate()) <= 3
	
	
	Select Distinct IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP
	Into #INT_RE_SIGHO__CFG_Farmacias_UMedicas
	From INT_RE_SIGHO__CFG_Farmacias_UMedicas
	
	
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, U.Referencia_SIADISSEP As Clues_Surte, E.CLUES_Emisora as CLUES, E.FolioReceta, D.*
	--UPdate D Set cantidadEntregada = 1
	From INT_RE_SIGHO__RecetasElectronicas_0001_General E (NoLock)
	Inner Join #INT_RE_SIGHO__CFG_Farmacias_UMedicas U (NoLock)
		On (E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and E.IdFarmacia = U.IdFarmacia)
	Inner Join INT_RE_SIGHO__RecetasElectronicas_0004_Insumos D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio)
	Where procesado = 0 And E.EsSurtido = 1  And CantidadEntregada > 0
	
		
End 
Go--#SQL 