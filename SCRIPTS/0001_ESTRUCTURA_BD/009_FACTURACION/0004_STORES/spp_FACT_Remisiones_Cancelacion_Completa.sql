------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Cancelacion_Completa' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_Cancelacion_Completa 
Go--#SQL 
  
Create Proc spp_FACT_Remisiones_Cancelacion_Completa 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001', @FolioRemision varchar(10) = '0000020182',  
	@IdPersonalCancela varchar(4) = '0001', @Observaciones varchar(200) = ''  
)
With Encryption 
As 
Begin 
Set NoCount On 

	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2) 
	Set @IdFarmaciaGenera = right('000000000000' + @IdFarmaciaGenera, 4) 
	Set @FolioRemision = right('000000000000' + @FolioRemision, 10) 
	Set @IdPersonalCancela = right('000000000000' + @IdPersonalCancela, 4) 


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
		Inner Join FACT_Remisiones_Detalles D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision ) 
		--Inner Join VentasDet_Lotes V (NoLock) 
		--	On 
		--	( 
		--		D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.IdSubFarmacia = V.IdSubFarmacia and D.FolioVenta = V.FolioVenta 
		--		and D.IdProducto = V.IdProducto and D.CodigoEAN = V.CodigoEAN and D.ClaveLote = V.ClaveLote 
		--	) 
		Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision
			and E.Status = 'A' 

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


---------- Esta parte ya no se hace, ya que la informacion solo se marca o desmarca para facturar ---------
-------------- Reintegrar información  
----	Insert Into VentasDet_Lotes 
----	(
----		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, 
----		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, 
----		Cant_Vendida, Cant_Devuelta, CantidadVendida, Status, Actualizado		 
----	) 		 
----	Select 
----		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdSubFarmacia, D.FolioVenta, 
----		D.IdProducto, D.CodigoEAN, D.ClaveLote, D.Renglon, 
----		(case when D.ClaveLote like '%*%' then 1 else 0 end) as EsConsignacion, 
----		0 as Cant_Vendida, 0 as Cant_Devuelta, Cantidad as CantidadVendida, 'A' as Status, 1 as Actualizado  
----	From #tmp_InformacionEnviarRepositorio D 
-------------- Reintegrar información  
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
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
			 			 
		Update R Set Cantidad = 0, Cantidad_Agrupada = 0, SubTotalSinGrabar = 0, SubTotalGrabado = 0, Iva = 0, Importe = 0 
		From FACT_Remisiones_Concentrado R 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 
	
		Update R Set Cantidad = 0, Cantidad_Agrupada = 0, SubTotalSinGrabar = 0, SubTotalGrabado = 0, Iva = 0, Importe = 0 
		From FACT_Remisiones_Resumen R 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
		-------------- Borrar información de Detalles  
	
	
	
		-------------- Generar el registro de Cancelacion 
		Insert Into FACT_Remisiones_Cancelaciones 
		(
			IdEmpresa, IdEstado, IdFarmaciaGenera, FechaCancelacion, FechaRemision, FolioRemision, TipoDeRemision, 
			IdPersonalCancela, IdFuenteFinanciamiento, IdFinanciamiento, TipoInsumo, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
			Observaciones, Status, Actualizado 
		) 
		Select IdEmpresa, IdEstado, IdFarmaciaGenera, getdate() as FechaCancelacion, FechaRemision, FolioRemision, TipoDeRemision, 
			@IdPersonalCancela as IdPersonalCancela, IdFuenteFinanciamiento, IdFinanciamiento, TipoInsumo, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
			@Observaciones as Observaciones, 'C' as Status, 0 as Actualizado
		From FACT_Remisiones 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 				


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
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 			
		-------------- Generar el registro de Cancelacion 

	
--	sp_listacolumnas FACT_Remisiones_Cancelaciones_Detalles  
	
--	sp_listacolumnas VentasDet_Lotes 	
	
	
End 
Go--#SQL 
