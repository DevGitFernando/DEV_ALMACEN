-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse 
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '113', 
	@TipoDeProceso int = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set Dateformat YMD 

Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  

	--	Exec spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse @IdEmpresa = '001',  @IdEstado = '22', @IdFarmacia = '13', @TipoDeProceso = 1 

	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)  


	------------------ Identificar los folios de venta a procesar 
	Exec spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas 
		 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @TipoDeProceso = @TipoDeProceso  


	------------------ Identificar los folios de devolucion de venta a procesar 
	Exec spp_INT_SESEQ__RecetasElectronicas_0022_Devoluciones_Verificar
		 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia  



------------------------------------------------- OBTENER LA INFORMACION   
	Select 
		G.EsResurtible, 
		X.IdEmpresa, X.IdEstado, X.IdFarmacia, G.Folio as FolioInterface, 
		X.UMedica, 
		G.FolioReceta as Folio_SESEQ, 
		X.TipoDeProceso, X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, G.FolioSurtido as FolioVenta, 0 as Partida, 
		G.FechaDeSurtido as FechaDeSurtimiento, 
		G.FolioReceta, G.FechaReceta, G.FechaEnvioReceta, 
		G.FolioAfiliacionSPSS, G.FechaIniciaVigencia, G.FechaTerminaVigencia, G.Expediente, 
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.CamaPaciente, G.Sexo, G.FechaNacimientoBeneficiario, 
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
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
	Inner Join VentasEnc V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
	Where 
		G.TipoDeProceso = @TipoDeProceso and 
		G.EsSurtido = 1 and G.Procesado = 0 and EsCancelado = 0 -- and G.FolioSurtido = @FolioVenta 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FechaReceta >= '2020-01-01' 
		and G.IntentosDeEnvio <= 1000 
		and G.EsResurtible = 0 
	Order by V.FolioVenta 


	Insert Into #tmp_01_General 
	Select 
		G.EsResurtible, 
		X.IdEmpresa, X.IdEstado, X.IdFarmacia, G.Folio as FolioInterface, 
		X.UMedica, 
		G.FolioReceta as Folio_SESEQ, 
		X.TipoDeProceso, X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, GD.FolioSurtido as FolioVenta, GD.Partida, 
		GD.FechaDeSurtido as FechaDeSurtimiento, 
		G.FolioReceta, G.FechaReceta, G.FechaEnvioReceta, 
		G.FolioAfiliacionSPSS, G.FechaIniciaVigencia, G.FechaTerminaVigencia, G.Expediente, 
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.CamaPaciente, G.Sexo, G.FechaNacimientoBeneficiario, 
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
		cast('' as varchar(100)) as ApMaternoPersonaRecibe 
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
	Inner Join INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas GD (NoLock) 
		On ( G.IdEmpresa = GD.IdEmpresa and G.IdEstado = GD.IdEstado and G.IdFarmacia = GD.IdFarmacia and G.Folio = GD.Folio ) 
	Inner Join VentasEnc V (NoLock) 
		On ( GD.IdEmpresa = V.IdEmpresa and GD.IdEstado = V.IdEstado and GD.IdFarmacia = V.IdFarmacia and GD.FolioSurtido = V.FolioVenta ) 		
	Where 
		G.TipoDeProceso = @TipoDeProceso and 
		GD.EsSurtido = 1 and GD.Procesado = 0 and EsCancelado = 0 -- and G.FolioSurtido = @FolioVenta 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FechaReceta >= '2020-01-01' 
		and G.IntentosDeEnvio <= 1000 
		and G.EsResurtible = 1 
	Order by V.FolioVenta  



	
	--Select I.* 
	--From INT_SESEQ__RecetasElectronicas_0004_Insumos I 
	--Inner Join #tmp_01_General E (NoLock) 
	--	On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 
	
	

	Update G Set NombrePersonaSurte = P.Nombre, ApPaternoPersonaSurte = P.ApPaterno, ApMaternoPersonaSurte = P.ApMaterno
	From #tmp_01_General G (NoLock)  
	Inner Join CatPersonal P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdFarmacia = P.IdFarmacia and G.IdPersonalSurte = P.IdPersonal ) 
	

	------------------------------------------------------------------------------------------------------------------------------------- 	

	

------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * From #tmp_01_General Where 1 = 1  
	Order by FolioReceta, FolioVenta  
--	Select * From #tmp_02_Medicamentos Order by ClaveSSA 

	
End 
Go--#SQL 
	