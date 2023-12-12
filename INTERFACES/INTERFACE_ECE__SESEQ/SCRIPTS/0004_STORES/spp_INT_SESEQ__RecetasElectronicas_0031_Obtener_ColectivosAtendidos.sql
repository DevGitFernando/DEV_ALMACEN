-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0031_Obtener_ColectivosAtendidos' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0031_Obtener_ColectivosAtendidos
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0031_Obtener_ColectivosAtendidos 
( 
	@IdEmpresa varchar(3) = '4', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '119', 
	@FolioReceta varchar(50) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 


	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3 ) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2 ) 
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4 ) 
	----Set @IdCliente = right('000000000000' + @IdCliente, 4 ) 
	----Set @IdSubCliente = right('000000000000' + @IdSubCliente, 4 ) 
	----Set @FolioInterface = right('000000000000' + @FolioInterface, 12 ) 
	Set @FolioReceta = REPLACE(@FolioReceta, ' ', '%' ) 

------------------------------------------------- OBTENER LA INFORMACION   
	Select -- Distinct 
		--G.IdEmpresa, G.IdEstado, G.IdFarmacia, 
		G.Folio as Secuenciador, G.FolioReceta, G.FechaReceta, G.FolioSurtido, G.FechaDeSurtido, 
						
		identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock) 
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
	Where G.EsSurtido in ( 1 )  and 
		G.Procesado = 1 --and G.Folio = @FolioInterface 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.RecepcionDuplicada = 0  
		and G.TipoDeProceso = 2 
		and G.EntregaConfirmada = 0 
		and G.FolioReceta like '%' + @FolioReceta + '%'  

---		spp_INT_SESEQ__RecetasElectronicas_0031_Obtener_ColectivosAtendidos 


	-- select * from #tmp_01_General 


	---------------------------------------------	SALIDA FINAL 
	select 
		'Folio interno' = Secuenciador, 
		'Folio de colectivo' = FolioReceta,  
		'Fecha de colectivo' = FechaReceta, 
		'Folio de surtido ó dispensación' = FolioSurtido, 
		'Fecha de surtido' = FechaDeSurtido 
	From #tmp_01_General 
	Order by FolioReceta  


	
End 
Go--#SQL 
	