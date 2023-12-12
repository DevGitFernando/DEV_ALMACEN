-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0012_AcusesInformacion' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0012_AcusesInformacion
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0012_AcusesInformacion 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2193' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 


	
	
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


--	select * from INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas  


------------------------------------------------- OBTENER LA INFORMACION SURTIDOS    
	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.Folio as FolioInterface, 
		X.UMedica, X.Folio_SIADISSEP, G.EsSurtido, G.FolioSurtido as FolioVenta,
		identity(int, 1, 1) as Keyx  
	Into #tmp_01___Surtidos 
	From INT_SIADISSEP__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SIADISSEP__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.Folio ) 
	Inner Join VentasEnc V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
	Where X.TipoDeProceso = 1 and G.EsSurtido = 1 and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia  




------------------------------------------------- OBTENER LA INFORMACION CANCELACIONES   
	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.Folio as FolioInterface, 
		X.UMedica, X.Folio_SIADISSEP, X.TipoDeProceso, 
		G.FolioReceta, G.FechaReceta, getdate() as FechaEnvioReceta, 
		getdate() as FechaCancelacionReceta, 
		G.Expediente, 
		0 as StatusCancelacion, 
		cast('CANCELADA' as varchar(200)) as StatusCancelacionDescripcion, 
		identity(int, 1, 1) as Keyx  
	Into #tmp_02___Cancelaciones 
	From INT_SIADISSEP__RecetasElectronicas_XML X (NoLock) 
	Inner Join INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.Folio ) 	
	Where X.TipoDeProceso = 2 and 
		G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia and Procesado = 0 


	Update C Set StatusCancelacion = 1, StatusCancelacionDescripcion = 'RECETA NO VIABLE PARA CANCELACION, YA FUE SURTIDA' 
	From #tmp_02___Cancelaciones C (NoLock) 
	Where Exists 
	( 
		Select * 
		From INT_SIADISSEP__RecetasElectronicas_0001_General G (NoLock) 
		Where C.IdEmpresa = G.IdEmpresa and C.IdEstado = G.IdEstado and C.IdFarmacia = G.IdFarmacia 
			and C.FolioReceta = G.FolioReceta and G.EsSurtido = 1 
	) 	



------------------------------------------------- SALIDA FINAL 
	Select * From #tmp_01___Surtidos 
	Select * From #tmp_02___Cancelaciones 

	
End 
Go--#SQL 
	
--		select * from INT_SIADISSEP__RecetasElectronicas_0001_General 
	
	