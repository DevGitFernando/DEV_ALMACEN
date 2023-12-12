-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado 
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
	Set @iPartidas = 0 
	Set @iPartidas_Atendidas = 0 

	Select @iPartidas = G.NumeroDePartidas 
	From INT_SESEQ__RecetasElectronicas_0001_General G 
	Where G.EsSurtido = 1 and G.Procesado = 0 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.Folio = @Folio_SESEQ  
		and G.EsResurtible = 1  



	If @TipoDeProceso = 1 
		Begin 
			----------------------- Procesar recetas/colectivos normales 
			Update G Set Procesado = 1, FechaProcesado = getdate()  
			From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
				On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			Inner Join VentasEnc V (NoLock) 
				On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
			Where G.EsSurtido = 1 and G.Procesado = 0 
				and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
				and G.Folio = @Folio_SESEQ  
				and G.EsResurtible = 0 
				and @Partida = 0 

			
			----------------------- Procesar recetas/colectivos resurtibles  
			Update GD Set Procesado = 1, FechaProcesado = getdate()  
			From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
				On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			Inner Join INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas GD (NoLock) 
				On ( G.IdEmpresa = GD.IdEmpresa and G.IdEstado = GD.IdEstado and G.IdFarmacia = GD.IdFarmacia and G.Folio = GD.Folio and GD.Partida = @Partida ) 
			Inner Join VentasEnc V (NoLock) 
				On ( GD.IdEmpresa = V.IdEmpresa and GD.IdEstado = V.IdEstado and GD.IdFarmacia = V.IdFarmacia and GD.FolioSurtido = V.FolioVenta ) 		
			Where GD.EsSurtido = 1 and G.Procesado = 0 
				and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
				and G.Folio = @Folio_SESEQ  
				and G.EsResurtible = 1  
				and @Partida >= 1    


			----- Marcar como procesado el folio completo  
			If @Partida > 1 
			Begin 

				Select @iPartidas_Atendidas = count(*)  
				From INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas G 
				Where G.EsSurtido = 1 and G.Procesado = 1  
					and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
					and G.Folio = @Folio_SESEQ  

				If @iPartidas_Atendidas = @iPartidas 
				Begin 
					Update G Set Procesado = 1, FechaProcesado = getdate()  
					From INT_SESEQ__RecetasElectronicas_0001_General G (NoLock)
					Where G.EsSurtido = 1 and G.Procesado = 0 
						and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
						and G.Folio = @Folio_SESEQ  
						and G.EsResurtible = 1  
				End 

			End 


		End 

	If @TipoDeProceso = 2  
		Begin 

			--select * 
			--From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			--Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
			--	On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			----Inner Join VentasEnc V (NoLock) 
			----	On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
			--Where G.EsSurtido = 1 
			--	--and G.Procesado = 1 
			--	--and G.Procesado_Digitalizacion = 0 
			--	and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
			--	and G.Folio = @Folio_SESEQ  
			--	--and G.EsResurtible = 0 
			--	--and @Partida = 0 

			----------------------- Procesar recetas/colectivos normales 
			Update G Set Procesado_Digitalizacion = 1, FechaProcesado_Digitalizacion = getdate()  
			From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
				On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			Inner Join VentasEnc V (NoLock) 
				On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
			Where G.EsSurtido = 1 and G.Procesado = 1 and G.Procesado_Digitalizacion = 0 
				and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
				and G.Folio = @Folio_SESEQ  
				and G.EsResurtible = 0 
				and @Partida = 0 

			----------------------- Procesar recetas/colectivos resurtibles  
			Update G Set Procesado_Digitalizacion = 1, FechaProcesado_Digitalizacion = getdate()  
			From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
				On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			Inner Join VentasEnc V (NoLock) 
				On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
			Where G.EsSurtido = 1 and G.Procesado = 1 and G.Procesado_Digitalizacion = 0 
				and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
				and G.Folio = @Folio_SESEQ  
				and G.EsResurtible = 1  
				and @Partida >= 0 


			----- Marcar como procesado el folio completo  
			If @Partida > 1 
			Begin 

				Select @iPartidas_Atendidas = count(*)  
				From INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas G 
				Where G.EsSurtido = 1 and G.Procesado_Digitalizacion = 1  
					and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
					and G.Folio = @Folio_SESEQ  

				If @iPartidas_Atendidas = @iPartidas 
				Begin 
					Update G Set Procesado_Digitalizacion = 1, FechaProcesado_Digitalizacion = getdate()  
					From INT_SESEQ__RecetasElectronicas_0001_General G (NoLock)
					Where G.EsSurtido = 1 and G.Procesado_Digitalizacion = 0 
						and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
						and G.Folio = @Folio_SESEQ  
						and G.EsResurtible = 1  
				End 

			End 
		End 


	
End 
Go--#SQL 
	

-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_ActualizarNumeroDeEnvios' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_ActualizarNumeroDeEnvios
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_ActualizarNumeroDeEnvios 
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


------------------------------------------------- OBTENER LA INFORMACION   

	If @TipoDeProceso = 1 
		Begin 
			----------------------- Procesar recetas/colectivos normales 
			Update G Set IntentosDeEnvio = G.IntentosDeEnvio + 1 
			From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
				On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			Inner Join VentasEnc V (NoLock) 
				On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
			Where G.EsSurtido = 1 and G.Procesado = 0 
				and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
				and G.Folio = @Folio_SESEQ  
				and @Partida = 0 
				and G.EsResurtible = 0 

			----------------------- Procesar recetas/colectivos resurtibles  
			Update GD Set IntentosDeEnvio = GD.IntentosDeEnvio + 1  
			From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
				On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			Inner Join INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas GD (NoLock) 
				On ( G.IdEmpresa = GD.IdEmpresa and G.IdEstado = GD.IdEstado and G.IdFarmacia = GD.IdFarmacia and G.Folio = GD.Folio and GD.Partida = @Partida ) 
			Inner Join VentasEnc V (NoLock) 
				On ( GD.IdEmpresa = V.IdEmpresa and GD.IdEstado = V.IdEstado and GD.IdFarmacia = V.IdFarmacia and GD.FolioSurtido = V.FolioVenta ) 		
			Where GD.EsSurtido = 1 and G.Procesado = 0 
				and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
				and G.Folio = @Folio_SESEQ  
				and G.EsResurtible = 1  
				and @Partida >= 1  

		End 
	Else 
		Begin 
			Update G Set IntentosEnvio_Digitalizacion = G.IntentosEnvio_Digitalizacion + 1 
			From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
			Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
				On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
			Inner Join VentasEnc V (NoLock) 
				On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
			Where G.EsSurtido = 1 and G.Procesado = 1 and G.Procesado_Digitalizacion = 0  
				and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
				and G.Folio = @Folio_SESEQ  
				and @Partida = 0 
				and G.EsResurtible = 0 
		End 

End 
Go--#SQL 
	