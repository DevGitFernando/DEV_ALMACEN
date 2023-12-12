-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse 
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '119', 
	@TipoDeProceso int = 2 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set Dateformat YMD 

Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@bEnviarDigitalizacion int 

	--	Exec spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse @IdEmpresa = '001',  @IdEstado = '22', @IdFarmacia = '13', @TipoDeProceso = 1 

	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)  
	Set @bEnviarDigitalizacion = 0 

	--Exec spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas 
	--	 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @TipoDeProceso = @TipoDeProceso  
	

	----------------------- Verificar que este activo el envio de imagenes 
	Select @bEnviarDigitalizacion = EnviarDigitalizacion 
	From INT_SESEQ__CFG_Farmacias_UMedicas (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 


------------------------------------------------- OBTENER LA INFORMACION   
	Select 
		X.IdEmpresa, X.IdEstado, X.IdFarmacia, G.Folio as FolioInterface, 
		X.UMedica, 
		G.FolioReceta as Folio_SESEQ, 
		X.TipoDeProceso, G.EntregaConfirmada, 1 as Enviar, 
		X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, G.FolioSurtido as FolioVenta, G.FechaDeSurtido as FechaDeSurtimiento, 
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
		--X.TipoDeProceso = @TipoDeProceso and 
		G.EsSurtido = 1 and G.Procesado = 1 and EsCancelado = 0 -- and G.FolioSurtido = @FolioVenta 
		and G.Procesado_Digitalizacion = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FechaReceta >= '2020-01-01' 
		and G.IntentosDeEnvio <= 1000 
		and @bEnviarDigitalizacion = 1  
	
	----------- Desmarcar los Colectivos que no se han confirmado 
	Update G Set Enviar = 0 
	From #tmp_01_General G
	Where TipoDeProceso = 2 and EntregaConfirmada = 0 

	
	--Select I.* 
	--From INT_SESEQ__RecetasElectronicas_0004_Insumos I 
	--Inner Join #tmp_01_General E (NoLock) 
	--	On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 
	------------------------------------------------------------------------------------------------------------------------------------- 	

	

------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * From #tmp_01_General Where Enviar = 1 
	Order by FolioReceta 
--	Select * From #tmp_02_Medicamentos Order by ClaveSSA 

	
End 
Go--#SQL 
	