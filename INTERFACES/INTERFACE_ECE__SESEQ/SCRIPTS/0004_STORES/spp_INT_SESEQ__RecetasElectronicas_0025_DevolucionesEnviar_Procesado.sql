-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0025_DevolucionesEnviar_Procesado
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0025_DevolucionesEnviar_Procesado 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2193', 
	@Folio_SESEQ varchar(100) = '', 
	@Partida int = 0, 
	@TipoDeProceso int = 1   	
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@iPartidas int, 
	@iPartidas_Atendidas int 


------------------------------------------------- OBTENER LA INFORMACION   
	Update G Set Procesado = 1, FechaProcesado = getdate()  
	From INT_SESEQ__RecetasElectronicas_0007_Devoluciones G (NoLock)
	Where G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FolioDevolucion = @Folio_SESEQ  


	
End 
Go--#SQL 
	

-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0025_DevolucionesEnviar_ActualizarNumeroDeEnvios' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0025_DevolucionesEnviar_ActualizarNumeroDeEnvios
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0025_DevolucionesEnviar_ActualizarNumeroDeEnvios 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2193', 
	@Folio_SESEQ varchar(100) = '', 
	@Partida int = 0, 
	@TipoDeProceso int = 1   	
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	Update G Set IntentosDeEnvio = G.IntentosDeEnvio + 1 
	From INT_SESEQ__RecetasElectronicas_0007_Devoluciones G (NoLock)
	Where G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FolioDevolucion = @Folio_SESEQ  


End 
Go--#SQL 
	