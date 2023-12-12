-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse 
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

	--	Exec spp_INT_SESEQ__RecetasElectronicas_0024_ObtenerDevolucionesRecetasEnviarAcuse @IdEmpresa = '001',  @IdEstado = '22', @IdFarmacia = '13', @TipoDeProceso = 1 

	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)  


------------------------------------------------- OBTENER LA INFORMACION   
	Select 
		G.IdEmpresa, G.IdEstado, G.IdFarmacia, G.FolioReceta as FolioInterface, 
		G.FolioReceta as Folio_SESEQ, 
		G.TipoDeProceso, 
		G.FolioSurtido as FolioVenta, 0 as Partida, 
		G.FolioDevolucion, 
		G.FechaDeDevolucion as FechaDeSurtimiento, 
		G.FolioReceta, G.FechaReceta, 		
		identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SESEQ__RecetasElectronicas_0007_Devoluciones G (NoLock) 
	Where 
		G.TipoDeProceso = @TipoDeProceso 
		and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FechaReceta >= '2020-01-01' 
		and G.IntentosDeEnvio <= 1000 
	Order by G.FolioDevolucion 


	------------------------------------------------------------------------------------------------------------------------------------- 	

	
------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SESEQ__RecetasElectronicas_0024_ObtenerDevolucionesRecetasEnviarAcuse 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * From #tmp_01_General 
	Order by FolioReceta, FolioDevolucion   
--	Select * From #tmp_02_Medicamentos Order by ClaveSSA 

	
End 
Go--#SQL 
	