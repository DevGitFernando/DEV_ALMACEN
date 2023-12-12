-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0032_Registrar_Colectivos_Entregados' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0032_Registrar_Colectivos_Entregados
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0032_Registrar_Colectivos_Entregados 
( 
	@IdEmpresa varchar(3) = '4', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '119', 
	@Folio_SESEQ varchar(50) = '' 
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
	Set @Folio_SESEQ = right('000000000000' + @Folio_SESEQ, 12 ) 


	Update G Set EntregaConfirmada = 1, FechaEntregaConfirmada = getdate()  
	From INT_SESEQ__RecetasElectronicas_0001_General G (NoLock)
	Where G.EsSurtido = 1 and G.Procesado = 1  
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.Folio = @Folio_SESEQ  
		--and G.EsResurtible = 0 
		and G.TipoDeProceso = 2 

	
End 
Go--#SQL 
	