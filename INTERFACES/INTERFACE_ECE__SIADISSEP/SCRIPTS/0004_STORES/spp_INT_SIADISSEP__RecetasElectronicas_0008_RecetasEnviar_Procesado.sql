-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0008_RecetasEnviar_Procesado' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0008_RecetasEnviar_Procesado
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0008_RecetasEnviar_Procesado 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2193', 
	@Folio_SIADISSEP varchar(100) = ''  	
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


------------------------------------------------- OBTENER LA INFORMACION   
	Update G Set Procesado = 1 
	From INT_SIADISSEP__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SIADISSEP__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.Folio ) 
	Inner Join VentasEnc V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
	Where G.EsSurtido = 1 and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and Folio_SIADISSEP = @Folio_SIADISSEP  

End 
Go--#SQL 
	