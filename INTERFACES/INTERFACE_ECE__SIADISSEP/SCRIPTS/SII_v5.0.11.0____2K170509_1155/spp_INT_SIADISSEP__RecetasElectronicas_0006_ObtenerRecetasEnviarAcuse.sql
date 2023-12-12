-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2406', 
	@TipoDeProceso int = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


------------------------------------------------- OBTENER LA INFORMACION   
	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.Folio as FolioInterface, 
		X.UMedica, X.Folio_SIADISSEP, X.TipoDeProceso, X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, G.FolioSurtido as FolioVenta, G.FechaDeSurtido as FechaDeSurtimiento, 
		G.FolioReceta, G.FechaReceta, G.FechaEnvioReceta, 
		G.FolioAfiliacionSPSS, G.FechaIniciaVigencia, G.FechaTerminaVigencia, G.Expediente, 
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.Sexo, 
		G.FolioAfiliacionOportunidades, G.EsPoblacionAbierta, 
		G.ClaveDeMedico, G.NombreMedico, G.ApPaternoMedico, G.ApMaternoMedico, G.CedulaDeMedico, 
		
		V.IdCliente, V.IdSubCliente, 
		--cast(V.IdCliente as varchar(100)) as IdCliente,  
		--cast('' as varchar(100)) as IdSubCliente, 
		cast('' as varchar(100)) as IdBeneficiario, 		
		
		cast(V.IdPersonal as varchar(100)) as IdPersonalSurte,  
		cast('' as varchar(100)) as NombrePersonaSurte, 
		cast('' as varchar(100)) as ApPaternoPersonaSurte, 
		cast('' as varchar(100)) as ApMaternoPersonaSurte, 
		cast('' as varchar(200)) as Observaciones,  
		cast('' as varchar(100)) as NombrePersonaRecibe, 
		cast('' as varchar(100)) as ApPaternoPersonaRecibe, 
		cast('' as varchar(100)) as ApMaternoPersonaRecibe, 	
		identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SIADISSEP__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SIADISSEP__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.Folio ) 
	Inner Join VentasEnc V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
	Where X.TipoDeProceso = @TipoDeProceso and 
		G.EsSurtido = 1 and G.Procesado = 0 and EsCancelado = 0 -- and G.FolioSurtido = @FolioVenta 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 

	
	
	--Select I.* 
	--From INT_SIADISSEP__RecetasElectronicas_0004_Insumos I 
	--Inner Join #tmp_01_General E (NoLock) 
	--	On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 
	
	

	Update G Set NombrePersonaSurte = P.Nombre, ApPaternoPersonaSurte = P.ApPaterno, ApMaternoPersonaSurte = P.ApMaterno
	From #tmp_01_General G (NoLock)  
	Inner Join CatPersonal P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdFarmacia = P.IdFarmacia and G.IdPersonalSurte = P.IdPersonal ) 
	

	------------------------------------------------------------------------------------------------------------------------------------- 	

	

------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * From #tmp_01_General 
--	Select * From #tmp_02_Medicamentos Order by ClaveSSA 

	
End 
Go--#SQL 
	