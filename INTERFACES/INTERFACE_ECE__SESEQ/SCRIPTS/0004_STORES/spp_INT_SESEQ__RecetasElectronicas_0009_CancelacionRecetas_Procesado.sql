-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0009_CancelacionRecetas_Procesado' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0009_CancelacionRecetas_Procesado
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0009_CancelacionRecetas_Procesado 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2193', 
	@Folio_SESEQ varchar(100) = ''  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)  

--	select * from INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas  


------------------------------------------------- OBTENER LA INFORMACION   
	Update G Set Procesado = 1
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock) 
	Inner Join INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 	
	Where 1 = 1 -- G.EsSurtido = 1 and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and Procesado = 0 
		-- and Folio_SESEQ = @Folio_SESEQ 
		and G.Folio = @Folio_SESEQ 
------------------------------------------------- OBTENER LA INFORMACION   	
	
---		spp_INT_SESEQ__RecetasElectronicas_0009_CancelacionRecetas_Procesado 	
	
	
--------------------------- SALIDA FINAL DE DATOS 

	
End 
Go--#SQL 
	