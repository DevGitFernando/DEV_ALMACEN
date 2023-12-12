-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2406' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


--	select * from INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas  

------------------------------------------------- OBTENER LA INFORMACION   
	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.Folio as FolioInterface, 
		X.UMedica, X.Folio_SESEQ, X.TipoDeProceso, 
		G.FolioReceta, G.FechaReceta, getdate() as FechaEnvioReceta, 
		getdate() as FechaCancelacionReceta, 
		G.Expediente, 
		0 as StatusCancelacion, 
		cast('CANCELADA' as varchar(200)) as StatusCancelacionDescripcion, 
		identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock) 
	Inner Join INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.Folio ) 	
	Where 1 = 1 -- G.EsSurtido = 1 and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and Procesado = 0 


	Update C Set StatusCancelacion = 1, StatusCancelacionDescripcion = 'RECETA NO VIABLE PARA CANCELACION, YA FUE SURTIDA' 
	From #tmp_01_General C (NoLock) 
	Where Exists 
	( 
		Select * 
		From INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		Where C.IdEmpresa = G.IdEmpresa and C.IdEstado = G.IdEstado and C.IdFarmacia = G.IdFarmacia 
			and C.FolioReceta = G.FolioReceta and G.EsSurtido = 1 
	) 


------------------------------------------------- OBTENER LA INFORMACION   	
	
---		spp_INT_SESEQ__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * From #tmp_01_General 
	
End 
Go--#SQL 
	