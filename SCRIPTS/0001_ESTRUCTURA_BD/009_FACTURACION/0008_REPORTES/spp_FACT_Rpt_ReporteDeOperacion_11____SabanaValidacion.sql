------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_11____SabanaValidacion' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_11____SabanaValidacion 
Go--#SQL   

/* 

Exec spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion 
	@IdEmpresa = '004', @IdEstado = '11', 
	@IdFarmaciaGenera = '', 
	@IdCliente = '42', @IdSubCliente = '', 

	@TipoDeUnidad = 1,  
    ---- Todas = 0, 
    ---- Farmacias = 1, 
    ---- FarmaciasUnidosis = 2, 
    ---- Almacenes = 3, 
    ---- AlmacenesUnidosis = 4 
	@IdFarmacia = '', 

	@FechaInicial = '2022-05-11', @FechaFinal = '2022-05-20', 
	@TamañoBloque = 150000  

*/ 

Create Proc	 spp_FACT_Rpt_ReporteDeOperacion_11____SabanaValidacion  
(
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 

	@TipoDeRemision int = 0, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 0, 
	@FolioInicial int = 0, @FolioFinal int = 50, 

	@TipoDeFecha int = 2, 
	@AplicarFiltroFechas int = 1, 
	@FechaInicial varchar(10) = '2022-05-01', @FechaFinal varchar(10) = '2022-05-12'
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
		IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, 
		EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  
		cast('' as varchar(100)) as IdFarmaciaDispensacion, cast('' as varchar(100))  as FarmaciaDispensacion 
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
		'	IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio,  ' + char(13) + 
		'	EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc,  ' + char(13) + 
		'	EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  ' + char(13) +
		'	IdFarmaciaDispensacion, FarmaciaDispensacion '  + char(13) +
		') ' + char(13) + 
		'Select ' + char(13) + 
		'	GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  ' + char(13) + 
		'	IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio,  ' + char(13) + 
		'	EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc,  ' + char(13) + 
		'	EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  ' + char(13) +
		'	GUID as IdFarmaciaDispensacion, GUID as FarmaciaDispensacion '  + char(13) + 
		'From vw_FACT_Remisiones R (NoLock) '  + char(13) + 
		@sFiltro + @sFiltro_Folios + @sFiltro_Fechas + @sFiltro_TipoDeRemision + @sFiltro_OrigenInsumo + @sFiltro_TipoDeInsumo + @sFiltro_ListadoDeRemisiones 
	Exec ( @sSql ) 
	Print @sSql 
	--------------------------------------------- OBTENER INFORMACION DE REMSIONES 


	--------------------------------------------- OBTENER INFORMACION DETALLADA DE REMSIONES 
	Select
		--top 100 
		S.IdEmpresa, S.IdEstado, S.IdFarmacia, S.FolioVenta 
	Into #tmp_ListadoDeVentas 
	From FACT_Remisiones_Detalles S (NoLock)
	Inner Join #vw_FACT_Remisiones F On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmaciaGenera = F.IdFarmacia And S.FolioRemision = F.FolioRemision )
	Where 1 = 1 --IdFarmacia <> '0004' 
		and Cantidad > 0 and Cantidad_Agrupada > 0  
		--and 1 = 0 
	Group by S.IdEmpresa, S.IdEstado, S.IdFarmacia, S.FolioVenta 
		--and S.ClaveSSA = '060.034.0103'  

	--------------------------------------------- OBTENER INFORMACION DETALLADA DE REMSIONES 


	--------------------------------------------- OBTENER INFORMACION DETALLADA DE VENTAS  
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, cast('' as varchar(500)) as Farmacia, cast('' as varchar(20)) as CLUES, 
		V.FolioVenta, V.FechaRegistro, 
		V.IdCliente, V.IdSubCliente, 
		cast('' as varchar(100)) as NumeroReceta, cast('' as varchar(10)) as FechaReceta, 
		cast('' as varchar(10)) as IdBeneficiario, cast('' as varchar(200)) as Beneficiario, 
		cast('' as varchar(100)) as NumeroReferencia, 
		cast('' as varchar(10)) as IdMedico, cast('' as varchar(200)) as Medico, cast('' as varchar(100)) as CedulaMedico, 
		cast('' as varchar(10)) as IdDiagnostico, cast('' as varchar(max)) as Diagnostico, 
		L.IdSubFarmacia, L.EsConsignacion, 
		cast('' as varchar(200)) as Laboratorio, 		
		cast('' as varchar(100)) as ClaveSSA, cast('' as varchar(max)) as DescripcionClave,   
		L.IdProducto, L.CodigoEAN, 0 as ContenidoPaquete, L.ClaveLote, cast('' as varchar(10)) as Caducidad, L.SKU, 
		L.CantidadVendida as Cantidad 
	Into #tmp_InformacionDispensacion 
	From VentasEnc V (NoLock) 
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( V.IdEmpresa = L.IdEmpresa and V.IdEstado = L.IdEstado and V.IdFarmacia = L.IdFarmacia and V.FolioVenta = L.FolioVenta ) 
	Inner Join #tmp_ListadoDeVentas F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.FolioVenta = F.FolioVenta ) 
	--------------------------------------------- OBTENER INFORMACION DETALLADA DE VENTAS  


	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 

	Update R Set Farmacia = F.Farmacia 
	From #tmp_InformacionDispensacion R 
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 

	Update R Set CLUES = F.CLUES  
	From #tmp_InformacionDispensacion R 
	Inner Join FACT_Farmacias_CLUES__CentroDeCostos F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 

	Update R Set Laboratorio = P.Laboratorio, ClaveSSA = P.ClaveSSA, DescripcionClave = P.DescripcionClave, ContenidoPaquete = P.ContenidoPaquete_ClaveSSA 
	From #tmp_InformacionDispensacion R 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( R.IdProducto = P.IdProducto and R.CodigoEAN = P.CodigoEAN ) 

	Update R Set IdBeneficiario = V.IdBeneficiario, IdMedico = V.IdMedico, NumeroReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120), 
		IdDiagnostico = V.IdDiagnostico 
	From #tmp_InformacionDispensacion R 
	Inner Join VentasInformacionAdicional V (NoLock) On ( V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and R.IdFarmacia = V.IdFarmacia and R.FolioVenta = V.FolioVenta )  

	Update R Set NumeroReferencia = (case when B.CURP <> '' then B.CURP else B.FolioReferencia end), Beneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) 
	From #tmp_InformacionDispensacion R 
	Inner Join CatBeneficiarios B (NoLock) 
		On (  R.IdEstado = B.IdEstado and R.IdFarmacia = B.IdFarmacia and R.IdCliente = B.IdCliente and R.IdSubCliente = B.IdSubCliente and R.IdBeneficiario = B.IdBeneficiario ) 

	Update R Set Medico = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre), CedulaMedico = B.NumCedula 
	From #tmp_InformacionDispensacion R 
	Inner Join CatMedicos B (NoLock) 
		On (  R.IdEstado = B.IdEstado and R.IdFarmacia = B.IdFarmacia and R.IdMedico = B.IdMedico ) 

	Update R Set Diagnostico = D.Descripcion  
	From #tmp_InformacionDispensacion R 
	Inner Join CatCIE10_Diagnosticos D (NoLock) On ( R.IdDiagnostico = D.ClaveDiagnostico )

	Update E Set Caducidad= convert(varchar(7), L.FechaCaducidad, 120)  
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia
			and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote  ) 
	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 



	-- select * from FACT_Remisiones____RPT 

--		sp_listacolumnas vw_FACT_Remisiones_Detalles 

--		Exec spp_FACT_Rpt_ReporteDeOperacion_11____SabanaValidacion		@AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  





	------------------------------------------ SALIDA FINAL 
	Select 
		Identity(int, 1, 1) as Excel_Identity_Export,
		'IdUnidad' = IdFarmacia, 
		'Unidad' = Farmacia, 
		'Ticket' = FolioVenta, 
		'FechaTicket' = FechaRegistro, 
		'Receta' = NumeroReceta, 
		'FechaReceta' = FechaReceta, 
		'Poliza' = NumeroReferencia, 
		'Beneficiario' = Beneficiario, 
		'IdDoctor' = IdMedico, 
		'Doctor' = Medico, 
		'ClaveSSA' = ClaveSSA, 
		'DescripcionClave' = DescripcionClave, 
		'EsConsigna' = EsConsignacion, 
		'ContenidoPaquete' = ContenidoPaquete, 
		'Lote' = ClaveLote, 
		'FechaCaducidad' = Caducidad, 
		'Cantidad' = sum(Cantidad), 
		'IdDiagnostico' = IdDiagnostico, 
		'Diagnostico' = Diagnostico, 
		'CLUES_Receta' = CLUES 
	Into #Salida_Final 
	From #tmp_InformacionDispensacion  (NoLock)  
	--Where Proceso_Producto in ( 0, 1 ) or Proceso_Servicio in ( 0, 1 )  
	--Where Proceso_Producto in ( 1, 1 ) or Proceso_Servicio in ( 1, 1 )  
	--where 1 = 0 
	-- Where ClaveSSA = '060.034.0103' 
	----Group by 
	----	FechaRemision, 
	----	---- FechaVenta, 
	----	Año_Venta, 
	----	Mes_Venta, 
	----	CentroDeCosto, IdFarmacia, NombreOficial, NumFactura, FolioFiscal_SAT, FolioFiscal_SAT_Corto,
	----	Nota_Credito, NombreFarmacia, FolioRemision, TipoDeRemisionDesc, Partida, TipoInsumo, IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento],
	----	ClaveSSA, 
	----	Mascara, 
	----	Descripcion, 
	----	Presentacion, 
	----	Proyecto, Referencia_04, 
	----	TasaIva, PrecioLicitado 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, Farmacia, 
		FolioVenta, FechaRegistro, 
		--IdCliente, IdSubCliente, 
		FolioVenta, FechaRegistro, 
		NumeroReceta, FechaReceta, NumeroReferencia, Beneficiario, IdMedico, Medico, 
		--IdProducto, CodigoEAN, 
		ClaveSSA, ClaveLote, DescripcionClave, EsConsignacion,  
		ContenidoPaquete, Caducidad, IdDiagnostico, Diagnostico, CLUES 
	Order by 
		--TipoDeProceso, 
		IdEstado, IdFarmacia, FolioVenta --, ClaveSSA 
		

	------------------------------- SALIDA PARA GENERAR EL EXCEL 
	------ Quitar caracteres especiales 
	Exec spp_FormatearTabla @Tabla = '#Salida_Final', @Ejecutar = 1 --, @Quitar_NoImprimibles = 1  
	--@Tabla varchar(500) = '', @Ejecutar int = 1, @Quitar_NoImprimibles int = 0 



	----------------------------------------  VERIFICACION 
	--if exists  ( select * from sysobjects (nolock) Where name = 'RPT_FACT__SabanaValidacion' and xType = 'u' ) drop table RPT_FACT__SabanaValidacion  

	--Select * 
	--Into RPT_FACT__SabanaValidacion 
	--from #Salida_Final 
	----------------------------------------  VERIFICACION 



	Select @iRows_Totales = count(*) from #Salida_Final 

	Set @iTamañoBloque = 50000  
	--Set @iRows_Totales = 0 
	Set @iRowInicial = 1 
	Set @iRowFinal = @iTamañoBloque  

	
	Set @sSql = 'select * from #Salida_Final ' 
	--Select * from #tmp_Resumen order by FolioVenta, IdTipoDeRemision, ClaveSSA, Cantidad 
	
	--Select * from #Salida 
	--Select TipoDeProceso, count(*) from #Salida_Final Group by TipoDeProceso
	--Select * from #Salida_Final 


	---------------- Tabla de control para multiples resultados 
	Select top 0 identity(int, 2, 1) as Orden, 1 as EsGeneral, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 



	Set @iSegmento = 0 
	Set @sSql = '' 
	Set @sSql_Segmento = ''   

	While @iRowInicial < @iRows_Totales 
	Begin 
		Set @iSegmento = @iSegmento + 1 

		Set @sSql_Segmento = 'Insert Into #tmpResultados ( NombreTabla, EsGeneral ) select ' + char(39) + 'Seccion_' + right('0000' + cast(@iSegmento as varchar(10)), 4) + char(39) + ', 1 ' 
		Exec( @sSql_Segmento ) 

		--Set @sSql_Segmento =  
		--	'Select ' + char(13) + 
		--		'FechaRemision, Año_Venta, Mes_Venta, ' + char(13) + 
		--		'CentroDeCosto, IdFarmacia, NombreOficial, [FolioFactura Electronica], FolioFiscal_SAT, FolioFiscal_SAT_Corto, ' + char(13) + 
		--		'Nota_Credito, NombreFarmacia, FolioRemision, TipoDeRemisionDesc, Partida, TipoInsumo, IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento], ' + char(13) + 
		--		'ClaveSSA, Mascara, Descripcion, Presentacion, Proyecto, Causes,  ' + char(13) + 
		--		'TasaIva, PrecioUnitario , Cantidad, SubTotal, Iva, Total  ' + char(13) +  
		--	'From  #Salida_Final ' + char(13) +  
		--	'Where  Excel_Identity_Export between ' + cast(@iRowInicial as varchar(20)) + ' and ' + cast(@iRowFinal as varchar(20)) + '  ' + char(13) + 
		--	'Order by ClaveSSA ' + char(13) + char(13) 


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


