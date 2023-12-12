------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega 
Go--#SQL   

--	Exec spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega @AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  

Create Proc	 spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega  
(
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '42', @IdSubCliente varchar(4) = '4', 
	
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 

	@TipoDeRemision int = 0, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 1, 
	@FolioInicial int = 2189, @FolioFinal int = 2200, 

	@TipoDeFecha int = 2, 
	@AplicarFiltroFechas int = 0, 
	@FechaInicial varchar(10) = '2022-04-01', @FechaFinal varchar(10) = '2022-04-30'
)    
With Encryption 
As 
Begin 
Set NoCount On  
Set DateFormat YMD  
Declare 
	@sSql varchar(max),  
	@sFiltro varchar(max),  
	@sFiltro_TipoDeRemision varchar(max),  
	@sFiltro_OrigenInsumo varchar(max),  
	@sFiltro_TipoDeInsumo varchar(max),  
	@sFiltro_Folios varchar(max),  
	@sFiltro_Fechas varchar(max), 
	@sFiltro_ListadoDeRemisiones varchar(max) 	 

Declare 
	@sSql_Segmento varchar(max), 
	@iSegmento int, 
	@iRows_Totales int, 
	@iRowInicial int, 
	@iRowFinal int, 
	@iTamañoBloque int 


	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @sFiltro_Folios = '' 
	Set @sFiltro_Fechas = '' 
	Set @sFiltro_TipoDeRemision = '' 
	Set @sFiltro_OrigenInsumo = '' 
	Set @sFiltro_TipoDeInsumo = '' 
	Set @sFiltro_ListadoDeRemisiones = '' 

	Set @sSql_Segmento = '' 
	Set @iSegmento = 0 
	Set @iTamañoBloque = 50000  
	Set @iRows_Totales = 0 
	Set @iRowInicial = 1 
	Set @iRowFinal = @iTamañoBloque  



--Año, Mes
	----------------- Filtros 
	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdFarmacia = ' + + char(39) + @IdFarmaciaGenera + char(39)   

	If @AplicarFiltroFechas = 1 
	Begin 
		Set @sFiltro_Fechas = ' and ( convert(varchar(10), FechaRemision, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 

		If @TipoDeFecha = 2 
		Begin 
			Set @sFiltro_Fechas = ' and ( convert(varchar(10), FechaInicial, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 
		End 
	End 


	If @IdFuenteFinanciamiento <> '' 
	Begin 
		Set @sFiltro = @sFiltro + ' and IdFuenteFinanciamiento = ' + char(39) + right('00000000' + @IdFuenteFinanciamiento, 4) + char(39) + char(13) 

		If @IdFinanciamiento <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' and IdFinanciamiento = ' + char(39) + right('00000000' + @IdFinanciamiento, 4) + char(39) + char(13) 
		End 

	End 

	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if  @TipoDeRemision <> 0 
	Begin 
		If @TipoDeRemision = 1 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 1, 3, 4, 5 ) ' + char(13) -- PRODUCTO 

		If @TipoDeRemision = 2 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 2, 6 ) ' + char(13) -- SERVICIO  
	End 

	If @SegmentoTipoDeRemision <> 0 
	Begin 
		Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( ' + cast(@SegmentoTipoDeRemision as varchar(10)) +  ' ) ' + char(13) -- SEGMENTO ESPECIFICO   
	End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if @OrigenDeInsumos <> 0 
		Begin 
			If @OrigenDeInsumos = 1 
			   Set @sFiltro_OrigenInsumo = ' And OrigenInsumo = 0  ' + char(13) -- VENTA 

			If @OrigenDeInsumos = 2 
			   Set @sFiltro_OrigenInsumo = ' And OrigenInsumo = 1 ' + char(13) -- CONSIGNA  
		End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if @TipoDeInsumo <> 0 
		Begin 
			If @TipoDeInsumo = 1 
			   Set @sFiltro_TipoDeInsumo = ' And TipoInsumo = 2  ' + char(13) -- MEDICAMENTO 

			If @TipoDeInsumo = 2 
			   Set @sFiltro_TipoDeInsumo = ' And TipoInsumo = 1 ' + char(13) -- MATERIAL DE CURACION 
		End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
	If @AplicarFiltroFolios = 1 
	Begin 
		If @FolioInicial > 0 and @FolioFinal > 0 
			Begin 
				Set @sFiltro_Folios = ' and FolioRemision between ' + cast(@FolioInicial as varchar(20)) + ' and '  + cast(@FolioFinal as varchar(20)) + char(13) 
			End 
		Else 
			Begin 
				
				If @FolioInicial > 0 
				Begin 
					Set @sFiltro_Folios = ' and FolioRemision >= ' + cast(@FolioInicial as varchar(20)) + char(13) 
				End 

				If @FolioFinal > 0 
				Begin 
					Set @sFiltro_Folios = ' and FolioRemision <= ' + cast(@FolioFinal as varchar(20)) + char(13) 
				End 

			End 
	End 

	------	Exec spp_FACT_CFD_GetRemisiones_Descarga 

	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   



	--------------------------------------------- OBTENER INFORMACION DE REMSIONES 
	Select  
		GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
		IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, FolioFacturaElectronica, Serie, Folio, 
		EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  
		cast('' as varchar(100)) as IdFarmaciaDispensacion, cast('' as varchar(100))  as FarmaciaDispensacion, EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento  
	Into #vw_FACT_Remisiones
	From vw_FACT_Remisiones 
	Where 1 = 0 
		----IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera 
		----and EsFacturable = 1 and EsFacturada = 0 and Status = 'A' 
		----and EsRelacionFacturaPrevia = 0 


	Set @sSql = 
		'Insert Into #vw_FACT_Remisiones ' + char(13) + 
		'( ' + char(13) + 
		'	GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  ' + char(13) + 
		'	IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, FolioFacturaElectronica, Serie, Folio,  ' + char(13) + 
		'	EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc,  ' + char(13) + 
		'	EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  ' + char(13) +
		'	IdFarmaciaDispensacion, FarmaciaDispensacion, EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento '  + char(13) +
		') ' + char(13) + 
		'Select ' + char(13) + 
		'	GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  ' + char(13) + 
		'	IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, FolioFacturaElectronica, Serie, Folio,  ' + char(13) + 
		'	EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc,  ' + char(13) + 
		'	EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  ' + char(13) +
		'	GUID as IdFarmaciaDispensacion, GUID as FarmaciaDispensacion, EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento '  + char(13) + 
		'From vw_FACT_Remisiones R (NoLock) '  + char(13) + 
		@sFiltro + @sFiltro_Folios + @sFiltro_Fechas + @sFiltro_TipoDeRemision + @sFiltro_OrigenInsumo + @sFiltro_TipoDeInsumo + @sFiltro_ListadoDeRemisiones 
	Exec ( @sSql ) 
	Print @sSql 
	

	--------------------------------------------- OBTENER INFORMACION DE REMSIONES 
	Select
		--top 400 
		--identity(int, 1, 1) as Excel_Identity_Export, 
		F.EsRelacionFacturaPrevia, F.FolioFacturaElectronica, F.Serie, F.Folio, F.EsRelacionDocumento, F.FolioRelacionDocumento, F.ReferenciaDocumento, 
		FechaRemision,
		cast('' As varchar(10)) As FechaVenta, 
		0 as Año_Venta, 
		cast('' As varchar(50)) As Mes_Venta, 
		S.IdEmpresa, S.IdEstado, S.IdFarmacia, 
		cast('' As varchar(500)) As NombreOficial, cast('' As varchar(500)) As NombreFarmacia,
		S.IdFarmaciaGenera, FolioVenta,
		S.FolioRemision, TipoDeRemision as IdTipoDeRemision, cast(TipoDeRemisionDesc As varchar(200)) As TipoDeRemisionDesc, 
		cast(F.TipoInsumo As varchar(200)) As TipoInsumo, TipoDeInsumoDesc, 
		Partida, cast('' As varchar(500)) As Observaciones,
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, cast(F.Financiamiento As varchar(200)) As  'FuenteFinanciamiento', 
		OrigenInsumo, IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, 
		ClaveSSA, 
		cast('' As varchar(200)) As Mascara, 
		cast('' As varchar(200)) As Descripcion, cast('' As varchar(200)) As Presentacion, referencia_01 As Proyecto,
		PrecioLicitado, TasaIva, 
		Cantidad as CantidadDispensada, 
		Cantidad_Agrupada as Cantidad, 
		(CAse when TasaIva > 0 Then s.SubTotalGrabado else S.SubTotalSinGrabar End) As SubTotal, S.Iva, S.Importe As Total
	Into #Salida
	From FACT_Remisiones_Detalles S (NoLock)
	Inner Join #vw_FACT_Remisiones F On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmaciaGenera = F.IdFarmacia And S.FolioRemision = F.FolioRemision )
	Where Cantidad > 0 and Cantidad_Agrupada > 0	
		--and 1 = 0 
		--and S.IdFarmacia = 4011 
		--and S.FolioVenta = 9 
		--and S.ClaveSSA = '010.000.5428.00'  
		--and FolioFacturaElectronica = 'PHJGTOA - 1' 


	--	select  top 10 * from #Salida 

--		Exec spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega		@AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  

	----select PrecioLicitado, TasaIva, sum(CantidadDispensada), sum(cantidad), sum(subTotal) from #Salida 
	----group by PrecioLicitado, TasaIva 
	----select * from #Salida 


	------------------------------------------------------------------------------------------------ GENERAR MATRIZ CRUZADA  
	------------------------------------------ GENERAR TABLA PRINCIPAL  
	Select 
		1 as TipoDeProceso,		--	1 ==> Documentos | 2 ==> Facturas | 3 ==> Remisiones generales 
		Año_Venta, 
		Mes_Venta, 
		TipoDeInsumoDesc, 
		FuenteFinanciamiento, 
		IdEmpresa, IdEstado, IdFarmacia, 
		--cast('' As varchar(500)) As NombreOficial, cast('' As varchar(500)) As NombreFarmacia,
		FechaVenta, 
		FolioVenta, 

		TipoInsumo,
		ClaveSSA, 
		IdProducto, 
		CodigoEAN, 
		ClaveLote, 
		IdSubFarmacia, 
		Mascara, 
		Descripcion, Presentacion, 
		
		cast('____________' as varchar(30)) as Columna_Control, 
		
		
		0 as Proceso_Producto, 0 as Proceso_Servicio, 

		cast('' as varchar(8)) as FolioRelacionDocumento, 
		cast('' as varchar(100)) as DocumentoComprobacion, 

		cast('' as varchar(30)) as CFDI_Serie, 
		cast('' as varchar(30)) as CFDI_Folio, 

		cast('____________' as varchar(30)) as Columna_Control_02, 

		--cast('' as varchar(30)) as CFDI_Producto, 
		--cast(0 as numeric(14,4)) as PrecioLicitado_Producto, 
		--cast(0 as numeric(14,4)) as TasaIva_Producto, 	
		--cast(0 as numeric(14,4)) as CantidadDispensada_Producto, 
		--cast(0 as numeric(14,4)) as Cantidad_Producto, 
		--cast(0 as numeric(14,4)) as SubTotal_Producto, 
		--cast(0 as numeric(14,4)) as Iva_Producto, 
		--cast(0 as numeric(14,4)) as Total_Producto, 

		--cast('' as varchar(30)) as CFDI_Servicio, 
		--cast(0 as numeric(14,4)) as PrecioLicitado_Servicio, 
		--cast(0 as numeric(14,4)) as TasaIva_Servicio, 	
		--cast(0 as numeric(14,4)) as CantidadDispensada_Servicio, 
		--cast(0 as numeric(14,4)) as Cantidad_Servicio, 
		--cast(0 as numeric(14,4)) as SubTotal_Servicio, 
		--cast(0 as numeric(14,4)) as Iva_Servicio, 
		--cast(0 as numeric(14,4)) as Total_Servicio  


		cast('' as varchar(30)) as CFDI_Producto, 
		cast(0 as numeric(14,6)) as PrecioLicitado_Producto, 
		cast(0 as numeric(14,6)) as TasaIva_Producto, 	
		cast(0 as numeric(14,6)) as CantidadDispensada_Producto, 
		cast(0 as numeric(14,6)) as Cantidad_Producto, 
		cast(0 as numeric(14,6)) as SubTotal_Producto, 
		cast(0 as numeric(14,6)) as Iva_Producto, 
		cast(0 as numeric(14,6)) as Total_Producto, 

		cast('' as varchar(30)) as CFDI_Servicio, 
		cast(0 as numeric(14,6)) as PrecioLicitado_Servicio, 
		cast(0 as numeric(14,6)) as TasaIva_Servicio, 	
		cast(0 as numeric(14,6)) as CantidadDispensada_Servicio, 
		cast(0 as numeric(14,6)) as Cantidad_Servicio, 
		cast(0 as numeric(14,6)) as SubTotal_Servicio, 
		cast(0 as numeric(14,6)) as Iva_Servicio, 
		cast(0 as numeric(14,6)) as Total_Servicio  

	Into #tmp_CruzeGeneral 
	From #Salida 
	Group by 
		FuenteFinanciamiento,  
		Año_Venta, Mes_Venta, TipoDeInsumoDesc, IdEmpresa, IdEstado, IdFarmacia, FechaVenta, FolioVenta, 
		TipoInsumo, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, Mascara, Descripcion, Presentacion  


	Insert Into #tmp_CruzeGeneral 
	Select 
		2 as TipoDeProceso,		--	1 ==> Documentos | 2 ==> Facturas | 3 ==> Remisiones generales 
		Año_Venta, 
		Mes_Venta, 
		TipoDeInsumoDesc, 
		FuenteFinanciamiento, 
		IdEmpresa, IdEstado, IdFarmacia, 
		--cast('' As varchar(500)) As NombreOficial, cast('' As varchar(500)) As NombreFarmacia,
		FechaVenta, 
		FolioVenta,

		TipoInsumo, 
		ClaveSSA, 
		IdProducto, 
		CodigoEAN, 
		ClaveLote, 
		IdSubFarmacia, 
		Mascara, 
		Descripcion, Presentacion, 

		cast('____________' as varchar(30)) as Columna_Control, 
		
		
		0 as Proceso_Producto, 0 as Proceso_Servicio, 

		cast('' as varchar(8)) as FolioRelacionDocumento, 
		cast('' as varchar(100)) as DocumentoComprobacion, 

		cast('' as varchar(30)) as CFDI_Serie, 
		cast('' as varchar(30)) as CFDI_Folio, 


		cast('____________' as varchar(30)) as Columna_Control_02, 

		cast('' as varchar(30)) as CFDI_Producto, 
		cast(0 as numeric(14,4)) as PrecioLicitado_Producto, 
		cast(0 as numeric(14,4)) as TasaIva_Producto, 	
		cast(0 as numeric(14,4)) as CantidadDispensada_Producto, 
		cast(0 as numeric(14,4)) as Cantidad_Producto, 
		cast(0 as numeric(14,4)) as SubTotal_Producto, 
		cast(0 as numeric(14,4)) as Iva_Producto, 
		cast(0 as numeric(14,4)) as Total_Producto, 

		cast('' as varchar(30)) as CFDI_Servicio, 
		cast(0 as numeric(14,4)) as PrecioLicitado_Servicio, 
		cast(0 as numeric(14,4)) as TasaIva_Servicio, 	
		cast(0 as numeric(14,4)) as CantidadDispensada_Servicio, 
		cast(0 as numeric(14,4)) as Cantidad_Servicio, 
		cast(0 as numeric(14,4)) as SubTotal_Servicio, 
		cast(0 as numeric(14,4)) as Iva_Servicio, 
		cast(0 as numeric(14,4)) as Total_Servicio  

	From #Salida 
	--Where 1 = 0 
	Group by 
		FuenteFinanciamiento, 
		Año_Venta, Mes_Venta, TipoDeInsumoDesc, IdEmpresa, IdEstado, IdFarmacia, FechaVenta, FolioVenta, 
		TipoInsumo, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, Mascara, Descripcion, Presentacion  


	Insert Into #tmp_CruzeGeneral 
	Select 
		3 as TipoDeProceso,		--	1 ==> Documentos | 2 ==> Facturas | 3 ==> Remisiones generales 
		Año_Venta, 
		Mes_Venta, 
		TipoDeInsumoDesc, 
		FuenteFinanciamiento, 
		IdEmpresa, IdEstado, IdFarmacia, 
		--cast('' As varchar(500)) As NombreOficial, cast('' As varchar(500)) As NombreFarmacia,
		FechaVenta, 
		FolioVenta,

		TipoInsumo, 
		ClaveSSA, 
		IdProducto, 
		CodigoEAN, 
		ClaveLote, 
		IdSubFarmacia, 
		Mascara, 
		Descripcion, Presentacion, 

		cast('____________' as varchar(30)) as Columna_Control, 
		
		
		0 as Proceso_Producto, 0 as Proceso_Servicio, 

		cast('' as varchar(8)) as FolioRelacionDocumento, 
		cast('' as varchar(100)) as DocumentoComprobacion, 

		cast('' as varchar(30)) as CFDI_Serie, 
		cast('' as varchar(30)) as CFDI_Folio, 


		cast('____________' as varchar(30)) as Columna_Control_02, 


		cast('' as varchar(30)) as CFDI_Producto, 
		cast(0 as numeric(14,4)) as PrecioLicitado_Producto, 
		cast(0 as numeric(14,4)) as TasaIva_Producto, 	
		cast(0 as numeric(14,4)) as CantidadDispensada_Producto, 
		cast(0 as numeric(14,4)) as Cantidad_Producto, 
		cast(0 as numeric(14,4)) as SubTotal_Producto, 
		cast(0 as numeric(14,4)) as Iva_Producto, 
		cast(0 as numeric(14,4)) as Total_Producto, 

		cast('' as varchar(30)) as CFDI_Servicio, 
		cast(0 as numeric(14,4)) as PrecioLicitado_Servicio, 
		cast(0 as numeric(14,4)) as TasaIva_Servicio, 	
		cast(0 as numeric(14,4)) as CantidadDispensada_Servicio, 
		cast(0 as numeric(14,4)) as Cantidad_Servicio, 
		cast(0 as numeric(14,4)) as SubTotal_Servicio, 
		cast(0 as numeric(14,4)) as Iva_Servicio, 
		cast(0 as numeric(14,4)) as Total_Servicio  

	From #Salida 
	--Where 1 = 0 
	Group by 
		FuenteFinanciamiento, 
		Año_Venta, Mes_Venta, TipoDeInsumoDesc, IdEmpresa, IdEstado, IdFarmacia, FechaVenta, FolioVenta, 
		TipoInsumo, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, Mascara, Descripcion, Presentacion  

	------------------------------------------ GENERAR TABLA PRINCIPAL  


	----	spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega 




	------------------------------------------------------------------------------ CRUZAR INFORMACION 
	Select 
		EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento, EsRelacionFacturaPrevia, 
		OrigenInsumo, IdTipoDeRemision, 
		IdEstado, IdFarmacia, 
		FolioFacturaElectronica, 
		FolioVenta, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, ClaveSSA, 
		PrecioLicitado, TasaIva, 
		sum(CantidadDispensada) as CantidadDispensada, sum(Cantidad) as Cantidad, sum(SubTotal) as SubTotal, sum(Iva) as Iva, sum(Total) as Total 
	Into #tmp_Resumen  
	From #Salida 
	--Where FolioVenta =  9 
	----Where EsRelacionDocumento = 1 and EsRelacionFacturaPrevia = 0 and IdTipoDeRemision in ( 2 )  --- Servicio 
	----	and OrigenInsumo = 1 
	Group by 
		EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento, EsRelacionFacturaPrevia, 
		OrigenInsumo, IdTipoDeRemision, 
		IdEstado, IdFarmacia, 
		FolioFacturaElectronica, 
		FolioVenta, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, ClaveSSA, 
		PrecioLicitado, TasaIva  

	--select 'RESUMEN', * from  #tmp_Resumen 

	----select 
	----	OrigenInsumo, IdTipoDeRemision, 
	----	FolioFacturaElectronica, sum(Total) as Total  
	----from #tmp_Resumen 
	----Group by 
	----	OrigenInsumo, IdTipoDeRemision, 
	----	FolioFacturaElectronica 

	------------------------------------------ COMPROBACION DE DOCUMENTOS  
	Update C Set 
		DocumentoComprobacion = ReferenciaDocumento, 
		Proceso_Servicio = 1, 
		--PrecioLicitado_Servicio = D.PrecioLicitado, TasaIva_Servicio = D.TasaIva, 
		CantidadDispensada_Servicio = CantidadDispensada, 
		Cantidad_Servicio = Cantidad, --SubTotal_Servicio = SubTotal, Iva_Servicio = Iva, Total_Servicio = Total 
		PrecioLicitado_Servicio = 0, TasaIva_Servicio = D.TasaIva, 
		SubTotal_Servicio = 0, Iva_Servicio = 0, Total_Servicio = 0 
		, FolioRelacionDocumento = D.FolioRelacionDocumento 
	From #tmp_CruzeGeneral C 
	Inner Join #tmp_Resumen D On 
	( 
		C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia and C.FolioVenta = D.FolioVenta and C.IdSubFarmacia = D.IdSubFarmacia and 
		C.IdProducto = D.IdProducto and C.CodigoEAN = D.CodigoEAN and C.ClaveLote = D.ClaveLote and C.ClaveSSA = D.ClaveSSA 
	) 
	Where TipoDeProceso = 1 and D.EsRelacionDocumento = 1 and D.EsRelacionFacturaPrevia = 0 and D.IdTipoDeRemision in ( 2, 6 ) and D.OrigenInsumo = 1 
		--and 1 = 0 

	Update C Set 
		DocumentoComprobacion = ReferenciaDocumento, 
		Proceso_Servicio = 1, 
		--PrecioLicitado_Servicio = D.PrecioLicitado, TasaIva_Servicio = D.TasaIva, 
		CantidadDispensada_Servicio = CantidadDispensada, 
		Cantidad_Servicio = Cantidad, --SubTotal_Servicio = SubTotal, Iva_Servicio = Iva, Total_Servicio = Total 
		PrecioLicitado_Servicio = 0, TasaIva_Servicio = D.TasaIva, 
		SubTotal_Servicio = 0, Iva_Servicio = 0, Total_Servicio = 0 
		, FolioRelacionDocumento = D.FolioRelacionDocumento 
	From #tmp_CruzeGeneral C 
	Inner Join #tmp_Resumen D On 
	( 
		C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia and C.FolioVenta = D.FolioVenta and C.IdSubFarmacia = D.IdSubFarmacia and 
		C.IdProducto = D.IdProducto and C.CodigoEAN = D.CodigoEAN and C.ClaveLote = D.ClaveLote and C.ClaveSSA = D.ClaveSSA 
	) 
	Where TipoDeProceso = 1 and D.EsRelacionDocumento = 1 and D.EsRelacionFacturaPrevia = 0 and D.IdTipoDeRemision in ( 2, 6 ) and D.OrigenInsumo = 0 
		--and 1 = 0 
	------------------------------------------ COMPROBACION DE DOCUMENTOS  


	------------------------------------------ COMPROBACION DE FACTURAS  
	Update C Set 
		Proceso_Servicio = 1, 
		--PrecioLicitado_Servicio = D.PrecioLicitado, TasaIva_Servicio = D.TasaIva, 
		CantidadDispensada_Servicio = CantidadDispensada, 
		Cantidad_Servicio = Cantidad, --SubTotal_Servicio = SubTotal, Iva_Servicio = Iva, Total_Servicio = Total 
		PrecioLicitado_Servicio = 0, TasaIva_Servicio = D.TasaIva, 
		SubTotal_Servicio = 0, Iva_Servicio = 0, Total_Servicio = 0 
	From #tmp_CruzeGeneral C 
	Inner Join #tmp_Resumen D On 
	( 
		C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia and C.FolioVenta = D.FolioVenta and C.IdSubFarmacia = D.IdSubFarmacia and 
		C.IdProducto = D.IdProducto and C.CodigoEAN = D.CodigoEAN and C.ClaveLote = D.ClaveLote and C.ClaveSSA = D.ClaveSSA 
	)  
	Where TipoDeProceso = 2 and D.EsRelacionDocumento = 0 and D.EsRelacionFacturaPrevia = 1 and D.IdTipoDeRemision in ( 2, 6 ) and D.OrigenInsumo = 1 
		--and 1 = 0 
	------------------------------------------ COMPROBACION DE FACTURAS  


	------------------------------------------ REMISIONES GENERALES  
	Update C Set 
		CFDI_Servicio = FolioFacturaElectronica, 
		Proceso_Servicio = 1, 
		PrecioLicitado_Servicio = D.PrecioLicitado, TasaIva_Servicio = D.TasaIva, 
		CantidadDispensada_Servicio = CantidadDispensada, 
		Cantidad_Servicio = Cantidad, SubTotal_Servicio = SubTotal, Iva_Servicio = Iva, Total_Servicio = Total 
	From #tmp_CruzeGeneral C 
	Inner Join #tmp_Resumen D On 
	( 
		C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia and C.FolioVenta = D.FolioVenta and C.IdSubFarmacia = D.IdSubFarmacia and 
		C.IdProducto = D.IdProducto and C.CodigoEAN = D.CodigoEAN and C.ClaveLote = D.ClaveLote and C.ClaveSSA = D.ClaveSSA 
	)  
	Where TipoDeProceso = 3 and D.EsRelacionDocumento = 0 and D.EsRelacionFacturaPrevia = 0 and D.IdTipoDeRemision in ( 2, 6 ) and D.OrigenInsumo = 1 
		--and 1 = 0 

	Update C Set 
		CFDI_Producto = FolioFacturaElectronica, 
		Proceso_Producto = 1, 
		PrecioLicitado_Producto = D.PrecioLicitado, TasaIva_Producto = D.TasaIva, 
		CantidadDispensada_Producto = CantidadDispensada, 
		Cantidad_Producto = Cantidad, SubTotal_Producto = SubTotal, Iva_Producto = Iva, Total_Producto = Total 
	From #tmp_CruzeGeneral C 
	Inner Join #tmp_Resumen D On 
	( 
		C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia and C.FolioVenta = D.FolioVenta and C.IdSubFarmacia = D.IdSubFarmacia and 
		C.IdProducto = D.IdProducto and C.CodigoEAN = D.CodigoEAN and C.ClaveLote = D.ClaveLote and C.ClaveSSA = D.ClaveSSA 
	)  
	Where TipoDeProceso = 3 and D.EsRelacionDocumento = 0 and D.EsRelacionFacturaPrevia = 0 and D.IdTipoDeRemision in ( 1, 4 ) and D.OrigenInsumo = 0  
		--and 1 = 0 

	Update C Set 
		CFDI_Servicio = FolioFacturaElectronica, 
		Proceso_Servicio = 1, 
		PrecioLicitado_Servicio = D.PrecioLicitado, TasaIva_Servicio = D.TasaIva, 
		CantidadDispensada_Servicio = CantidadDispensada, 
		Cantidad_Servicio = Cantidad,  SubTotal_Servicio = SubTotal, Iva_Servicio = Iva, Total_Servicio = Total 
	From #tmp_CruzeGeneral C 
	Inner Join #tmp_Resumen D On 
	( 
		C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia and C.FolioVenta = D.FolioVenta and C.IdSubFarmacia = D.IdSubFarmacia and 
		C.IdProducto = D.IdProducto and C.CodigoEAN = D.CodigoEAN and C.ClaveLote = D.ClaveLote and C.ClaveSSA = D.ClaveSSA 
	)  
	Where TipoDeProceso = 3 and D.EsRelacionDocumento = 0 and D.EsRelacionFacturaPrevia = 0 and D.IdTipoDeRemision in ( 2, 6 ) and D.OrigenInsumo = 0  
		--and 1 = 0 
	------------------------------------------ REMISIONES GENERALES  


	------------------------------------------------------------------------------ CRUZAR INFORMACION 

	----	spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega 

		----cast(0 as numeric(14,4)) as PrecioLicitado_Servicio, 
		----cast(0 as numeric(14,4)) as TasaIva_Servicio, 	
		----cast(0 as numeric(14,4)) as Cantidad_Servicio, 
		----cast(0 as numeric(14,4)) as SubTotal_Servicio, 
		----cast(0 as numeric(14,4)) as Iva_Servicio, 
		----cast(0 as numeric(14,4)) as Total_Servicio  

	------------------------------------------------------------------------------------------------ GENERAR MATRIZ CRUZADA  






	------------------------------------------ SALIDA FINAL 
	Select 
		Identity(int, 1, 1) as Excel_Identity_Export, 
		TipoDeProceso,		--	1 ==> Documentos | 2 ==> Facturas | 3 ==> Remisiones generales 
		'Oficio' = '', 
		'Fecha de entrega' = '', 
		'Tipo de insumo' = TipoDeInsumoDesc, 
		'Tipo de dispensación' = '', 
		'COSS' ='',
		'COSS ADMON' = '', 

		'Factura VTA' = CFDI_Producto, 
		'Factura ADMON' = CFDI_Servicio, 
		'Documento de comprobación' = DocumentoComprobacion, 
		'Consolidada' = '', 
		'Fuente de financiamiento' = FuenteFinanciamiento, 

		'CLUES' = cast('' as varchar(200)), 
		'ID' = IdFarmacia, 
		'Nombre de la unidad' = cast('' as varchar(200)), -- as Farmacia, 
		'Jurisdiccion' = cast('' as varchar(10)), --  as Jurisdiccion, 
		'Centro gestor' = cast('' as varchar(200)), --  as CentroDeCostos, 
		'CURP' = cast('' as varchar(200)), --  as NumeroDeReferencia, 

		'Remisión ó Folio' = FolioVenta,
		'Mes' = Mes_Venta, 
		'Año' = Año_Venta, 

		'Clave SSA' = ClaveSSA, 
		'Descripción' = cast('' as varchar(max)), 
		'Fecha de caducidad' = cast('' as varchar(10)) , 
		'Lote' = ClaveLote, 
		IdSubFarmacia, 
		IdProducto, 
		CodigoEAN, 

		'Fecha VTA'= FechaVenta, 

		cast('' as varchar(200)) as FechaVenta_Texto, 

		'Cantidad dispensada' = (case when CantidadDispensada_Producto > 0 then CantidadDispensada_Producto else CantidadDispensada_Servicio end), 
		'Cantidad validada' = (case when CantidadDispensada_Producto > 0 then CantidadDispensada_Producto else CantidadDispensada_Servicio end), 
		
		--Columna_Control, 
		
		
		--Proceso_Producto, Proceso_Servicio, 

		FolioRelacionDocumento, 
		DocumentoComprobacion, 


		--Columna_Control_02,

		'Precio licitado' = PrecioLicitado_Producto, 
		TasaIva_Producto, 	
		--Cantidad_Producto, 
		'SubTotal VTA' = SubTotal_Producto, 
		'Iva VTA' = Iva_Producto, 
		'Total VTA' = Total_Producto, 

		'Contenido paquete' = cast(0 as numeric(14,4)), 
		'Factor' = cast(0 as numeric(14,4)), 
		'Agrupado' =  (case when Cantidad_Producto > 0 then Cantidad_Producto else Cantidad_Servicio end), 

		'PrecioAdmon' = PrecioLicitado_Servicio, 
		TasaIva_Servicio, 	
		--Cantidad_Servicio, 
		'SubTotal ADM' = SubTotal_Servicio, 
		'Iva ADM' = Iva_Servicio, 
		'Total ADM' = Total_Servicio, 

		'Importe Total' = Total_Producto + Total_Servicio, 
		
		'Fecha receta' = cast('' as varchar(100)), 
		'Fecha folio' = cast(FechaVenta as varchar(100)), 
		'Receta' = cast('' as varchar(200)), 
		cast('' as varchar(200)) as IdPaciente, 
		'Paciente' = cast('' as varchar(200)), 

		cast('' as varchar(200)) as NumMedico, 
		'Médico' = cast('' as varchar(200)), 

		0 as Consigna, 
		'Laboratorio' = cast('' as varchar(200)), 
		cast('México' as varchar(200)) as Pais, 


		cast('' as varchar(200)) as IdCliente, 
		cast('' as varchar(200)) as IdSubCliente 

		, '' as Vacio  



	Into #Salida_Final 
	From #tmp_CruzeGeneral (NoLock)  
	--Where Proceso_Producto in ( 0, 1 ) or Proceso_Servicio in ( 0, 1 )  
	Where Proceso_Producto in ( 1, 1 ) or Proceso_Servicio in ( 1, 1 )  
		---and IdFarmacia = 4011 
		--and 1 = 0 		
	Order by 
		TipoDeProceso, IdEstado, IdFarmacia, FolioVenta, ClaveSSA 
	

	----	spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega   

	--select top 10 * from #tmp_CruzeGeneral 

	---------------------------- COMPLETAR LA INFORMACION GENERAL  
	 
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 

	Select * 
	Into #vw_Claves_Precios_Asignados 
	From vw_Claves_Precios_Asignados 
	Where IdEstado = @IdEstado and IdCliente = right('000000' + @IdCliente, 4)  


	Select * 
	Into #vw_ClavesSSA_Sales
	From vw_ClavesSSA_Sales 

	Select * 
	Into #vw_Farmacias 
	From vw_Farmacias 

	----select 
	----	( select count(*) from #vw_Productos_CodigoEAN) as Productos, 
	----	( select count(*) from #vw_Claves_Precios_Asignados) as CB, 
	----	( select count(*) from #vw_ClavesSSA_Sales) as Claves  

	Update R Set Consigna = 1 
	From #Salida_Final R 
	Where Lote like '%*%'  


	Update R Set Descripcion = C.DescripcionClave --, [Contenido paquete] = C.ContenidoPaquete  
	From #Salida_Final R 
	Inner Join #vw_ClavesSSA_Sales C (NoLock) On ( R.[Clave SSA] = C.ClaveSSA ) 

	Update R Set Laboratorio = P.Laboratorio 
	From #Salida_Final R 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( R.IdProducto = P.IdProducto and R.CodigoEAN = P.CodigoEAN ) 

	Update R Set [Nombre de la unidad] = F.Farmacia, CLUES = F.CLUES, Jurisdiccion = F.IdJurisdiccion  
	From #Salida_Final R 
	Inner Join #vw_Farmacias F (NoLock) On ( F.IdEstado = @IdEstado and R.ID = F.IdFarmacia ) 

	Update S Set S.[Centro gestor] = F.CentroDeCosto, CLUES = F.CLUES 
	From #Salida_Final S (NoLock)
	Inner Join FACT_Farmacias_CLUES__CentroDeCostos F (NoLock) On ( @IdEstado = F.IdEstado And S.ID = F.IdFarmacia )  

	Update R Set IdCliente = V.IdCliente, IdSubCliente = V.IdSubCliente, [Fecha folio] = convert(varchar(10), FechaRegistro, 120) 
	From #Salida_Final R 
	Inner Join VentasEnc V (NoLock) On ( V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and R.ID = V.IdFarmacia and R.[Remisión ó Folio] = V.FolioVenta )

	--select 'pausa' as PAUSA

	Update R Set IdPaciente = V.IdBeneficiario, NumMedico = V.IdMedico, Receta = V.NumReceta, [Fecha Receta] = convert(varchar(10), V.FechaReceta, 120)   
	From #Salida_Final R 
	Inner Join VentasInformacionAdicional V (NoLock) On ( V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and R.ID = V.IdFarmacia and R.[Remisión ó Folio] = V.FolioVenta )  

	--select 'pausa 2' as PAUSA_2

	Update R Set CURP = B.FolioReferencia, Paciente = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) 
	From #Salida_Final R 
	Inner Join CatBeneficiarios B (NoLock) 
		On (  B.IdEstado = @IdEstado and R.ID = B.IdFarmacia and R.IdCliente = B.IdCliente and R.IdSubCliente = B.IdSubCliente and R.IdPaciente = B.IdBeneficiario ) 

	Update R Set Medico = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) 
	From #Salida_Final R 
	Inner Join CatMedicos B (NoLock) 
		On (  B.IdEstado = @IdEstado and R.ID = B.IdFarmacia and R.NumMedico = B.IdMedico ) 

	Update R Set 
		Factor = B.Factor, --Agrupado = B.ContenidoPaquete_Licitado, 
		[Contenido paquete] = B.ContenidoPaquete    
	From #Salida_Final R 
	Inner Join #vw_Claves_Precios_Asignados B (NoLock) 
		On (  B.IdEstado = @IdEstado and R.IdCliente = B.IdCliente and R.IdSubCliente = B.IdSubCliente and R.[Clave SSA] = B.ClaveSSA ) 

	Update E Set [Fecha de caducidad]= convert(varchar(7), L.FechaCaducidad, 120)  
	From #Salida_Final E (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and E.ID = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia
			and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.Lote = L.ClaveLote  ) 

	Update S Set Año = datepart(year, [Fecha Folio]), Mes = dbo.fg_NombresDeMes([Fecha Folio]) 
	From #Salida_Final S (NoLock) 


	----	spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega   

	Update S Set 
		[SubTotal VTA] =  dbo.fg_PRCS_Redondear([Agrupado] * [Precio licitado], 5, 1), 
		[SubTotal ADM] =  dbo.fg_PRCS_Redondear([Agrupado] * [PrecioAdmon], 5, 1)  	
		--'Iva VTA' = Iva_Producto, 
		--'Total VTA' = Total_Producto, 
	From #Salida_Final S (NoLock) 

	--Update S Set 
	--	[SubTotal VTA] = dbo.fg_PRCS_Truncar([SubTotal VTA], 2), 
	--	[SubTotal ADM] = dbo.fg_PRCS_Truncar([SubTotal ADM], 2) 
	--From #Salida_Final S (NoLock) 

	--Update S Set 
	--	[SubTotal VTA] = dbo.fg_PRCS_Redondear([SubTotal VTA], 4, 0), 
	--	[SubTotal ADM] = dbo.fg_PRCS_Redondear([SubTotal ADM], 4, 0) 
	--From #Salida_Final S (NoLock) 


	Update S Set 
		[Iva VTA] = dbo.fg_PRCS_Redondear([SubTotal VTA] * ( TasaIva_Producto / 100.0), 2, 0)  
	From #Salida_Final S (NoLock) 
	Where TasaIva_Producto > 0 

	Update S Set 	
		[Iva VTA] = round([Iva VTA], 3, 1)   
	From #Salida_Final S (NoLock) 
	Where TasaIva_Producto > 0 

	Update S Set 	
		[Iva VTA] = round([Iva VTA], 2, 1)   
	From #Salida_Final S (NoLock) 
	Where TasaIva_Producto > 0 


	Update S Set 	
		[Iva ADM] = dbo.fg_PRCS_Redondear([SubTotal ADM] * ( TasaIva_Servicio / 100.0), 2, 0)  
		--'Total VTA' = Total_Producto, 
	From #Salida_Final S (NoLock) 
	Where TasaIva_Producto > 0 

	Update S Set 	
		[Iva ADM] = round([Iva ADM], 3, 1)   
	From #Salida_Final S (NoLock) 
	Where TasaIva_Producto > 0 

	Update S Set 	
		[Iva ADM] = round([Iva ADM], 2, 1)   
	From #Salida_Final S (NoLock) 
	Where TasaIva_Producto > 0 



	Update S Set 
		[Total VTA] = [SubTotal VTA] + [Iva VTA], 
		[Total ADM] = [SubTotal ADM] + [Iva ADM], 
		[Importe Total] = (  [SubTotal VTA] + [Iva VTA]  ) + (  [SubTotal ADM] + [Iva ADM]  ) 
	From #Salida_Final S (NoLock) 



	----select sum([Agrupado]), [Precio licitado], sum([SubTotal VTA])
	----from #Salida_Final 
	----group by [Agrupado], [Precio licitado]
	
	----	spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega   

	
	
	Alter table #Salida_Final Drop Column TasaIva_Producto 
	Alter table #Salida_Final Drop Column TasaIva_Servicio 

	Alter table #Salida_Final Drop Column IdCliente 
	Alter table #Salida_Final Drop Column IdSubCliente 
	Alter table #Salida_Final Drop Column IdProducto  
	Alter table #Salida_Final Drop Column CodigoEAN  
	Alter table #Salida_Final Drop Column IdSubFarmacia 
	Alter table #Salida_Final Drop Column IdPaciente  
	Alter table #Salida_Final Drop Column NumMedico 
	Alter table #Salida_Final Drop Column [Fecha VTA] 	
	Alter table #Salida_Final Drop Column FechaVenta_Texto  

	Alter table #Salida_Final Drop Column FolioRelacionDocumento  
	Alter table #Salida_Final Drop Column DocumentoComprobacion  


	Alter table #Salida_Final Drop Column Vacio  

	---------------------------- COMPLETAR LA INFORMACION GENERAL  

	----	spp_FACT_Rpt_ReporteDeOperacion_11____SabanaEntrega   



	------------------------------- SALIDA PARA GENERAR EL EXCEL 
	------ Quitar caracteres especiales 
	--Exec spp_FormatearTabla #Salida_Final 
	Exec spp_FormatearTabla @Tabla = '#Salida_Final', @Ejecutar = 1 --, @Quitar_NoImprimibles = 1  


	----------------------------------------  VERIFICACION 
	if exists  ( select * from sysobjects (nolock) Where name = 'RPT_FACT__SabanaEntrega' and xType = 'u' ) drop table RPT_FACT__SabanaEntrega  

	Select *    --, getdate() as FechaDeEjecucion 
	Into RPT_FACT__SabanaEntrega 
	from #Salida_Final 
	----------------------------------------  VERIFICACION 

	--select top 10 * from #Salida_Final 


	Select @iRows_Totales = count(*) from #Salida_Final 

	Set @iTamañoBloque = 30000  
	--Set @iRows_Totales = 0 
	Set @iRowInicial = 1 
	Set @iRowFinal = @iTamañoBloque  


	Set @sSql = 'select * from #Salida_Final ' 
	--Select * from #tmp_Resumen order by FolioVenta, IdTipoDeRemision, ClaveSSA, Cantidad 
	
	--Select * from #Salida 
	--Select TipoDeProceso, count(*) from #Salida_Final Group by TipoDeProceso
	--Select * from #Salida_Final 


	---------------- Tabla de control para multiples resultados 

	--Select *
	Update F Set [Remisión ó Folio] = right('00000000' + Cast([Remisión ó Folio] - 10000000 As Varchar(8)), 8)
	From #Salida_Final F
	Where [Remisión ó Folio] > 1000000


	Select top 0 identity(int, 2, 1) as Orden, 1 as EsGeneral, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 



	Set @iSegmento = 0 
	Set @sSql = '' 
	Set @sSql_Segmento = ''   

	While @iRowInicial < @iRows_Totales 
	Begin 
		Set @iSegmento = @iSegmento + 1 

		Set @sSql_Segmento = 'Insert Into #tmpResultados ( NombreTabla, EsGeneral ) select ' + char(39) + 'Seccion_' + right('0000' + cast(@iSegmento as varchar(10)), 4) + char(39) + ', 1 ' 
		Exec( @sSql_Segmento ) 


		Set @sSql_Segmento =  
			'Select ' + char(13) + 
				' * ' + char(13) +  
			'From  #Salida_Final ' + char(13) +  
			'Where  Excel_Identity_Export between ' + cast(@iRowInicial as varchar(20)) + ' and ' + cast(@iRowFinal as varchar(20)) + '  ' + char(13) + 
			'Order by Excel_Identity_Export ' + char(13) + char(13) 


		Set @sSql = @sSql + @sSql_Segmento 

		Set @iRowInicial = @iRowInicial + @iTamañoBloque 
		Set @iRowFinal = @iRowFinal + @iTamañoBloque 

	End 

	Set @sSql = 'Select * From #tmpResultados' + char(13)+ char(13) + @sSql 
	print @sSql 
	Exec ( @sSql ) 



End 
Go--#SQL  


