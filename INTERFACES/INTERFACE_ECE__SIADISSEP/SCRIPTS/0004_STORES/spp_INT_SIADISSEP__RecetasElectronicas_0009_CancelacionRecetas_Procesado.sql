-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0009_CancelacionRecetas_Procesado' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0009_CancelacionRecetas_Procesado
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0009_CancelacionRecetas_Procesado 
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


--	select * from INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas  

------------------------------------------------- OBTENER LA INFORMACION   
	Update G Set Procesado = 1
	From INT_SIADISSEP__RecetasElectronicas_XML X (NoLock) 
	Inner Join INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.Folio ) 	
	Where 1 = 1 -- G.EsSurtido = 1 and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and Procesado = 0 
		-- and Folio_SIADISSEP = @Folio_SIADISSEP 
		and X.Folio = @Folio_SIADISSEP 
------------------------------------------------- OBTENER LA INFORMACION   	
	
---		spp_INT_SIADISSEP__RecetasElectronicas_0009_CancelacionRecetas_Procesado 	
	
	
--------------------------- SALIDA FINAL DE DATOS 

	
End 
Go--#SQL 
	