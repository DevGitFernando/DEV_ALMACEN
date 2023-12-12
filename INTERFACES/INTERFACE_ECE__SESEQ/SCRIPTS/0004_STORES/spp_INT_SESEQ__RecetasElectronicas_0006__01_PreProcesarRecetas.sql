-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas 
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
	@sFolio_Receta varchar(20), 
	@sFolio varchar(20), 
	@dFechaSurtido datetime, 
	@sMensaje varchar(200), 
	@iSecuencial int 


	--	Exec spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas @IdEmpresa = '001',  @IdEstado = '22', @IdFarmacia = '13', @TipoDeProceso = 1 

	Set @sFolio_Receta = '' 
	Set @sFolio = '' 
	Set @iSecuencial = 0 
	Set @dFechaSurtido = getdate() 
	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)  


	---------------------------------- IDENTIFICAR LAS RECETAS NORMALES 
	Update R Set EsSurtido = 1, FolioSurtido = D.FolioVenta, FechaDeSurtido = D.FechaRegistro  
	from INT_SESEQ__RecetasElectronicas_0001_General R 
	inner Join 
	( 
		select V.FechaRegistro, I.*  
		from VentasEnc V (noLock) 
		Inner Join VentasInformacionAdicional I (noLock)  On ( V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta ) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
	) D On ( R.FolioReceta = D.NumReceta ) 
	where 
		D.FechaRegistro >=  '2019-01-07'  
		and R.TipoDeProceso = @TipoDeProceso 
		and R.EsSurtido = 0 
		and R.Procesado = 0 and R.RecepcionDuplicada = 0 
		and R.EsResurtible = 0 
	---------------------------------- IDENTIFICAR LAS RECETAS NORMALES 


	---------------------------------- OBTENER LAS RECETAS RESURTIBLES  
	Select distinct R.* -- R.Folio, D.*  
	Into #tmp_FoliosDeRecetas_Base  
	from INT_SESEQ__RecetasElectronicas_0001_General R 
	--Inner Join INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas LR (NoLock) 
	--	On ( R.IdEmpresa = LR.IdEmpresa and R.IdEstado = LR.IdEstado and R.IdFarmacia = LR.IdFarmacia and R.Folio = LR.Folio )  
	inner Join 
	( 
		select V.FechaRegistro, I.*  
		from VentasEnc V (noLock) 
		Inner Join VentasInformacionAdicional I (noLock)  On ( V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta ) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
	) D On ( R.FolioReceta = D.NumReceta ) 
	where 
		D.FechaRegistro >=  '2019-01-07'  
		and R.TipoDeProceso = @TipoDeProceso 
		--and R.EsSurtido = 0 
		and R.Procesado = 0 
		and R.RecepcionDuplicada = 0 
		and R.EsResurtible = 1  
		--and R.FolioReceta = 'HENM-004602/2022' 
	--Order by FolioVenta 



	--- spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas

	Select 
		identity(int, 1, 1) as Secuencial, 
		R.*, 
		0 as FolioSurtido_Asignado 
	Into #tmp_FoliosDeRecetas_Detalles 
	from INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas R 
	Inner Join #tmp_FoliosDeRecetas_Base LR (NoLock) 
		On ( R.IdEmpresa = LR.IdEmpresa and R.IdEstado = LR.IdEstado and R.IdFarmacia = LR.IdFarmacia and R.Folio = LR.Folio )  
	where 
		LR.EsSurtido in ( 0, 1 )  
		and R.Procesado = 0 
		--R.FolioReceta = 'HENM-004602/2022' 
	Order by Partida 


	select 
		identity(int, 1, 1) as Secuencial,
		0 as FolioSurtido_Asignado,  
		V.FechaRegistro, I.*  
	Into #tmp_InformacionDispensacion 
	from VentasEnc V (noLock) 
	Inner Join VentasInformacionAdicional I (noLock)  On ( V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta ) 
	Inner Join #tmp_FoliosDeRecetas_Base R (noLock)  On ( I.IdEstado = R.IdEstado and I.IdFarmacia = R.IdFarmacia and I.NumReceta = R.FolioReceta ) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
	Order by V.FolioVenta, V.FechaRegistro 


	Update V Set FolioSurtido_Asignado = 1 
	From #tmp_InformacionDispensacion V (noLock) 
	Inner Join #tmp_FoliosDeRecetas_Detalles D (NoLock) On ( V.FolioVenta = D.FolioSurtido ) 

	---------------------------------- OBTENER LAS RECETAS RESURTIBLES  

	--Select * from #tmp_FoliosDeRecetas_Base 
	--Select * from #tmp_FoliosDeRecetas_Detalles  
	--Select * from #tmp_InformacionDispensacion 



	--------------------------------- Asignar los folios de venta a cada partida de la receta 
	Declare #cursorVentas  
	Cursor For 
		Select FolioVenta, NumReceta, FechaRegistro    
		From #tmp_InformacionDispensacion T 
		where FolioSurtido_Asignado = 0  
		Order by Secuencial 
	Open #cursorVentas 
	FETCH NEXT FROM #cursorVentas Into @sFolio, @sFolio_Receta, @dFechaSurtido  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			-------------------- Procesar receta electronica 
			Declare #cursorRecetas  
			Cursor For 
				Select top 1 Secuencial 
				From #tmp_FoliosDeRecetas_Detalles T 
				where FolioSurtido_Asignado = 0  
					and FolioReceta = @sFolio_Receta 
				Order by  Secuencial 
			Open #cursorRecetas 
			FETCH NEXT FROM #cursorRecetas Into @iSecuencial 
				WHILE @@FETCH_STATUS = 0 
				BEGIN 
			
					--Select @sFolio as Folio, @iSecuencial as Secuencial, FolioSurtido_Asignado = 1   

					Update D Set EsSurtido = 1, FolioSurtido = @sFolio, FechaDeSurtido = @dFechaSurtido, FolioSurtido_Asignado = 1   
					From #tmp_FoliosDeRecetas_Detalles D 
					Where Secuencial = @iSecuencial 

					FETCH NEXT FROM #cursorRecetas Into @iSecuencial   
				END	 
			Close #cursorRecetas 
			Deallocate #cursorRecetas 	
			-------------------- Procesar receta electronica 

			FETCH NEXT FROM #cursorVentas Into @sFolio, @sFolio_Receta, @dFechaSurtido    
		END	 
	Close #cursorVentas 
	Deallocate #cursorVentas 	
	--------------------------------- Asignar los folios de venta a cada partida de la receta 


	--------------------------------- Cruzar informacion de recetas  
	Update R Set EsSurtido = 1, FolioSurtido = D.FolioSurtido, FechaDeSurtido = D.FechaDeSurtido 
	From INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas R (NoLock) 
	Inner Join #tmp_FoliosDeRecetas_Detalles D (NoLock) 
		On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmacia and R.Folio = D.Folio and R.FolioReceta = D.FolioReceta and R.Partida = D.Partida ) 
	Where D.FolioSurtido_Asignado = 1 


	Update R Set EsSurtido = 1 
	from INT_SESEQ__RecetasElectronicas_0001_General R 
	Inner Join #tmp_FoliosDeRecetas_Detalles LR (NoLock) 
		On ( R.IdEmpresa = LR.IdEmpresa and R.IdEstado = LR.IdEstado and R.IdFarmacia = LR.IdFarmacia and R.Folio = LR.Folio )  
	Where LR.FolioSurtido_Asignado = 1 
	--------------------------------- Cruzar informacion de recetas  


	----Select * from #tmp_FoliosDeRecetas_Base 
	----Select * from #tmp_FoliosDeRecetas_Detalles  
	----Select * from #tmp_InformacionDispensacion 


	/* 

		Update R Set EsSurtido = 0, FolioSurtido = '', FechaDeSurtido = '' 
		from INT_SESEQ__RecetasElectronicas_0001_General R (nolock) 

		Update R Set EsSurtido = 0, FolioSurtido = '', FechaDeSurtido = '' 
		from INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas R (nolock) 

	*/ 


End 
Go--#SQL 
	
	--	sp_listacolumnas__stores		spp_INT_SESEQ__RecetasElectronicas_0006__01_PreProcesarRecetas , 1	
