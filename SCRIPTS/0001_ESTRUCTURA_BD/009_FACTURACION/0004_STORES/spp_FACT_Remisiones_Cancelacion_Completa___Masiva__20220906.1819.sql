
Set dateformat YMD 
Go--#SQL 

	if exists ( select * from sysobjects (nolock) Where Name = 'tmp_Remisiones__Cancelacion' and xType = 'U' ) Drop Table tmp_Remisiones__Cancelacion  
Go--#SQL 


Begin tran 

--		commit tran 

--		rollback tran 


Declare 
	@IdPersonalCancela varchar(4) = '0001', 
	@Observaciones varchar(200) = 'CANCELACIÓN MASIVA'  

	----Select * 
	----Into tmp_Remisiones__Cancelacion 
	----from vw_FACT_Remisiones 
	----where FechaInicial between '2022-08-01' and '2022-08-31' 
	----	and IdCliente = 42 
	----	and Status = 'A' 


	Select * 
	Into tmp_Remisiones__Cancelacion 
	From vw_FACT_Remisiones (NoLock) 
	Where 
		IdCliente = 42 
		and Status = 'A' 
		and GUID In 
		( 
			'33ed3d3f-6fad-4916-9d09-30002dac7380',
			'77f60a0b-9cdb-4645-bc88-742355d0fd5a' 
		) 
		and FolioRemision between '0000022235' and '0000022329' 
		and FechaRemision >= '2023-04-05'
	Order by FolioRemision 



		-------------- Obtener la información a reintregrar 
		Select 
			distinct 
			E.TipoDeRemision, 
			E.EsRelacionDocumento, E.FolioRelacionDocumento, 
			E.EsRelacionFacturaPrevia, E.Serie, E.Folio, 
			D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, D.IdFarmacia, D.IdSubFarmacia, 
			D.FolioVenta, D.FolioRemision, D.IdFuenteFinanciamiento, D.IdFinanciamiento, 
			D.IdPrograma, D.IdSubPrograma, D.ClaveSSA, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.SKU, 
			D.PrecioLicitado, D.Cantidad, D.TasaIva, D.SubTotalSinGrabar, D.SubTotalGrabado, D.Iva, D.Importe, 
			0 as Renglon  
		Into #tmp_InformacionEnviarRepositorio 	
		From FACT_Remisiones E(NoLock) 
		Inner Join tmp_Remisiones__Cancelacion R (NoLock) On ( E.FolioRemision = R.FolioRemision ) 
		Inner Join FACT_Remisiones_Detalles D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision ) 
		--Inner Join VentasDet_Lotes V (NoLock) 
		--	On 
		--	( 
		--		D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.IdSubFarmacia = V.IdSubFarmacia and D.FolioVenta = V.FolioVenta 
		--		and D.IdProducto = V.IdProducto and D.CodigoEAN = V.CodigoEAN and D.ClaveLote = V.ClaveLote 
		--	) 
		--Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision
		--	and E.Status = 'A' 

		Update E Set Renglon = D.Renglon 
		From #tmp_InformacionEnviarRepositorio E 
		Inner Join VentasDet D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia 
					And E.FolioVenta = D.FolioVenta and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN )	


		Select 
			TipoDeRemision, 
			EsRelacionDocumento, FolioRelacionDocumento, 
			EsRelacionFacturaPrevia, Serie, Folio, 
			IdEmpresa, IdEstado, IdFarmaciaGenera, 
			ClaveSSA, sum(Cantidad) as Cantidad 
		Into #tmp_InformacionEnviarRepositorio__Resumen 	
		From #tmp_InformacionEnviarRepositorio 
		Group by  
			TipoDeRemision, 
			EsRelacionDocumento, FolioRelacionDocumento, 
			EsRelacionFacturaPrevia, Serie, Folio, 
			IdEmpresa, IdEstado, IdFarmaciaGenera, 
			ClaveSSA 
		-------------- Obtener la información a reintregrar 


		--select * from #tmp_InformacionEnviarRepositorio 

----------------------------------------------------------------------------------------------------------------------------------------------
		

		---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
		---------------------------------------------- Devolver las cantidades de Documentos de Comprobacion 

		Update D Set Cantidad_Distribuida = Cantidad_Distribuida - R.Cantidad
		From FACT_Remisiones__RelacionDocumentos D (NoLock) 
		Inner Join FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
		Inner Join #tmp_InformacionEnviarRepositorio__Resumen R (Nolock) 
			On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmaciaGenera 
				and D.FolioRelacion = R.FolioRelacionDocumento and D.ClaveSSA = R.ClaveSSA )
				
		---------------------------------------------- Devolver las cantidades de Documentos de Comprobacion 
		---------------------------------------------------------------------------------------------------------------------------------------------------------------- 


		---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
		---------------------------------------------- Se Desmarca la informacion referente al folio de remision que se esta cancelando -------------------------------- 
		
		-------------------------------- REMISIONES NORMALES 
		--------------- Producto 
		Update V Set RemisionFinalizada = 0, EnRemision_Insumo = 0, CantidadRemision_Insumo = V.CantidadRemision_Insumo - R.Cantidad  
		From VentasDet_Lotes V (NoLock) 
		Inner Join #tmp_InformacionEnviarRepositorio R (NoLock) 
			On ( V.IdEmpresa = R.IdEmpresa and V.IdEstado = R.IdEstado and V.IdFarmacia = R.IdFarmacia and V.FolioVenta = R.FolioVenta 
				and V.IdSubFarmacia = R.IdSubFarmacia and V.IdProducto = R.IdProducto and V.CodigoEAN = R.CodigoEAN and V.ClaveLote = R.ClaveLote and V.SKU = R.SKU )  
		Where TipoDeRemision in ( 1, 4 )  


		--------------- Servicio  
		Update V Set RemisionFinalizada = 0, EnRemision_Admon = 0, CantidadRemision_Admon = V.CantidadRemision_Admon - R.Cantidad   
		From VentasDet_Lotes V (NoLock) 
		Inner Join #tmp_InformacionEnviarRepositorio R (NoLock) 
			On ( V.IdEmpresa = R.IdEmpresa and V.IdEstado = R.IdEstado and V.IdFarmacia = R.IdFarmacia and V.FolioVenta = R.FolioVenta 
				and V.IdSubFarmacia = R.IdSubFarmacia and V.IdProducto = R.IdProducto and V.CodigoEAN = R.CodigoEAN and V.ClaveLote = R.ClaveLote and V.SKU = R.SKU )  
		Where TipoDeRemision in ( 2, 6 ) 
		-------------------------------- REMISIONES NORMALES 


		-------------------------------- REMISIONES INCREMENTO  
		--------------- Producto 
		Update V Set RemisionFinalizada = 0, EnRemision_Insumo = 0, CantidadRemision_Insumo = V.CantidadRemision_Insumo - R.Cantidad  
		From FACT_Incremento___VentasDet_Lotes V (NoLock) 
		Inner Join #tmp_InformacionEnviarRepositorio R (NoLock) 
			On ( V.IdEmpresa = R.IdEmpresa and V.IdEstado = R.IdEstado and V.IdFarmacia = R.IdFarmacia and V.FolioVenta = R.FolioVenta 
				and V.IdSubFarmacia = R.IdSubFarmacia and V.IdProducto = R.IdProducto and V.CodigoEAN = R.CodigoEAN and V.ClaveLote = R.ClaveLote and V.SKU = R.SKU )  
		Where TipoDeRemision in ( 3, 5 )  


		--------------- Servicio  
		Update V Set RemisionFinalizada = 0, EnRemision_Admon = 0, CantidadRemision_Admon = V.CantidadRemision_Admon - R.Cantidad   
		From FACT_Incremento___VentasDet_Lotes V (NoLock) 
		Inner Join #tmp_InformacionEnviarRepositorio R (NoLock) 
			On ( V.IdEmpresa = R.IdEmpresa and V.IdEstado = R.IdEstado and V.IdFarmacia = R.IdFarmacia and V.FolioVenta = R.FolioVenta 
				and V.IdSubFarmacia = R.IdSubFarmacia and V.IdProducto = R.IdProducto and V.CodigoEAN = R.CodigoEAN and V.ClaveLote = R.ClaveLote and V.SKU = R.SKU )  
		Where TipoDeRemision in ( 7, 8 ) 

		-------------------------------- REMISIONES INCREMENTO  


		----Update L Set L.EnRemision_Insumo = 0 
		----From VentasDet_Lotes L(NoLock) 
		----Inner Join #tmp_InformacionEnviarRepositorio V (NoLock) 
		----	On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
		----		And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
		----Where L.EnRemision_Insumo = 1 and L.EnRemision_Admon = 0 and L.RemisionFinalizada = 0 

		---------------------------------------------------------------------------------------------------------------------------------------------------------------- 

-----------------------------------------------------------------------------------------------------------------------------------------------
		
	---		spp_FACT_Remisiones_Cancelacion_Completa  			
		
		-------------- Borrar información de Detalles   
		----Delete FACT_Remisiones_Detalles 
		----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
			 			 
		----Delete From FACT_Remisiones_Concentrado 
		----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 
	
		----Delete From FACT_Remisiones_Resumen 
		----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		


		Update R Set Cantidad = 0, Cantidad_Agrupada = 0, SubTotalSinGrabar = 0, SubTotalGrabado = 0, Iva = 0, Importe = 0 
		From FACT_Remisiones_Detalles R (NoLock) 
		Inner Join tmp_Remisiones__Cancelacion RE (NoLock) On ( R.FolioRemision = RE.FolioRemision ) 
		--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
			 			 
		Update R Set Cantidad = 0, Cantidad_Agrupada = 0, SubTotalSinGrabar = 0, SubTotalGrabado = 0, Iva = 0, Importe = 0 
		From FACT_Remisiones_Concentrado R 
		Inner Join tmp_Remisiones__Cancelacion RE (NoLock) On ( R.FolioRemision = RE.FolioRemision ) 
		--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
	
		Update R Set Cantidad = 0, Cantidad_Agrupada = 0, SubTotalSinGrabar = 0, SubTotalGrabado = 0, Iva = 0, Importe = 0 
		From FACT_Remisiones_Resumen R 
		Inner Join tmp_Remisiones__Cancelacion RE (NoLock) On ( R.FolioRemision = RE.FolioRemision ) 
		--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
		-------------- Borrar información de Detalles  
	
	
	
		-------------- Generar el registro de Cancelacion 
		Insert Into FACT_Remisiones_Cancelaciones 
		(
			IdEmpresa, IdEstado, IdFarmaciaGenera, FechaCancelacion, FechaRemision, FolioRemision, TipoDeRemision, 
			IdPersonalCancela, IdFuenteFinanciamiento, IdFinanciamiento, TipoInsumo, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
			Observaciones, Status, Actualizado 
		) 
		Select R.IdEmpresa, R.IdEstado, R.IdFarmaciaGenera, getdate() as FechaCancelacion, R.FechaRemision, R.FolioRemision, R.TipoDeRemision, 
			@IdPersonalCancela as IdPersonalCancela, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.TipoInsumo, R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, 
			@Observaciones as Observaciones, 'C' as Status, 0 as Actualizado
		From FACT_Remisiones R 
		Inner Join tmp_Remisiones__Cancelacion RE (NoLock) On ( R.FolioRemision = RE.FolioRemision ) 
		--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 				


		Insert Into FACT_Remisiones_Cancelaciones_Detalles 
		(
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 
			FolioVenta, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
			IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, SKU, 
			PrecioLicitado, Cantidad, TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe
		)  
		Select 
			D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, D.IdFarmacia, D.IdSubFarmacia, 
			D.FolioVenta, D.FolioRemision, D.IdFuenteFinanciamiento, D.IdFinanciamiento, 
			D.IdPrograma, D.IdSubPrograma, D.ClaveSSA, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.SKU, 
			D.PrecioLicitado, D.Cantidad, D.TasaIva, D.SubTotalSinGrabar, D.SubTotalGrabado, D.Iva, D.Importe 
		From #tmp_InformacionEnviarRepositorio D 
		--Where 1 = 0 
	

		----- Se actualiza el encabezado de la remision original 
		Update R Set Status = 'C', 
			SubTotalSinGrabar = 0, 
			SubTotalGrabado = 0, Iva = 0, Total = 0
		From FACT_Remisiones R (NoLock) 
		Inner Join tmp_Remisiones__Cancelacion RE (NoLock) On ( R.FolioRemision = RE.FolioRemision ) 
		--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 			
		-------------- Generar el registro de Cancelacion 



	
--	sp_listacolumnas FACT_Remisiones_Cancelaciones_Detalles  
	
--	sp_listacolumnas VentasDet_Lotes 	
	
	