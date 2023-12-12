------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_00_ResumenRemisionado' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_00_ResumenRemisionado 
Go--#SQL   

--	Exec spp_FACT_Rpt_ReporteDeOperacion_00_ResumenRemisionado @AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  

Create Proc	 spp_FACT_Rpt_ReporteDeOperacion_00_ResumenRemisionado  
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

	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   



	--------------------------------------------- OBTENER INFORMACION DE REMSIONES 
	Select  * 
	Into #vw_FACT_Remisiones
	From vw_FACT_Remisiones 
	Where 1 = 0 
		----IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera 
		----and EsFacturable = 1 and EsFacturada = 0 and Status = 'A' 
		----and EsRelacionFacturaPrevia = 0 


	Set @sSql = 
		'Insert Into #vw_FACT_Remisiones ' + char(13) + 
		'Select * ' + char(13) + 
		'From vw_FACT_Remisiones R (NoLock) '  + char(13) + 
		@sFiltro + @sFiltro_Folios + @sFiltro_Fechas + @sFiltro_TipoDeRemision + @sFiltro_OrigenInsumo + @sFiltro_TipoDeInsumo + @sFiltro_ListadoDeRemisiones 
	Exec ( @sSql ) 
	Print @sSql 
	--------------------------------------------- OBTENER INFORMACION DE REMSIONES 


	----select 
	----	--GUID, 
	----	min(folioremision) as FolioInicial, max(folioremision) as FolioFinal, 
	----	IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
	----	--FolioFacturaElectronica, 
	----	EsRelacionDocumento, FolioRelacionDocumento, 
	----	EsRelacionFacturaPrevia, Serie, Folio, 
	----	TipoDeRemisionDesc, OrigenInsumoDesc, TipoDeInsumoDesc, 
	----	count(distinct FolioRemision) as FoliosRemisiones, 
	----	sum(SubTotalSinGrabar + SubTotalGrabado ) as SubTotal, 
	----	sum(IVA) as IVA, 
	----	sum(Total) as Importe 
	----from vw_FACT_Remisiones 
	------where GUID in ( 'c0092ec8-cebd-48ee-a940-85b5ab7dd22e', '5e107927-8b98-48df-a67d-18ff88b913f5' ) 
	------where EsRelacionDocumento = 1 
	------where GUID = '9ba3abe8-db1d-4c00-b428-511a43572101' 
	------where GUID = 'c0092ec8-cebd-48ee-a940-85b5ab7dd22e' 
	----Group by 
	----	GUID, 
	----	IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
	----	--FolioFacturaElectronica, 
	----	EsRelacionDocumento, FolioRelacionDocumento, 
	----	EsRelacionFacturaPrevia, Serie, Folio, 
	----	TipoDeRemisionDesc, OrigenInsumoDesc, TipoDeInsumoDesc  
	----Order by   
	----	FolioInicial, GUID  


	--------------------------------------------- OBTENER INFORMACION DETALLADA DE REMSIONES 
	select 
		Identity(int, 1, 1) as Excel_Identity_Export, 
		--GUID, 
		min(folioremision) as FolioInicial, max(folioremision) as FolioFinal, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
		--FolioFacturaElectronica, 
		EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento, 
		EsRelacionFacturaPrevia, Serie, Folio, 
		TipoDeRemisionDesc, OrigenInsumoDesc, TipoDeInsumoDesc, 
		count(distinct FolioRemision) as FoliosRemisiones, 
		'SubTotal Remisioando' = sum(SubTotalSinGrabar + SubTotalGrabado), 
		'IVA Remisionado' = sum(IVA), 
		'Importe Remisionado' = sum(Total), 
		'SubTotal Facturable' = cast( (case when EsFacturable = 1 then sum(SubTotalSinGrabar + SubTotalGrabado) else 0 end) as numeric(14,4)),  
		'IVA Facturable' = cast( (case when EsFacturable = 1 then sum(IVA) else 0 end) as numeric(14,4)),  
		'Importe Facturable '= cast( (case when EsFacturable = 1 then sum(Total) else 0 end) as numeric(14,4))  
	Into #Salida_Final 
	from #vw_FACT_Remisiones 
	--where GUID in ( 'c0092ec8-cebd-48ee-a940-85b5ab7dd22e', '5e107927-8b98-48df-a67d-18ff88b913f5' ) 
	--where EsRelacionDocumento = 1 
	--where GUID = '9ba3abe8-db1d-4c00-b428-511a43572101' 
	--where GUID = 'c0092ec8-cebd-48ee-a940-85b5ab7dd22e' 
	Group by 
		GUID, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
		--FolioFacturaElectronica, 
		EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento, 
		EsRelacionFacturaPrevia, Serie, Folio, 
		TipoDeRemisionDesc, OrigenInsumoDesc, TipoDeInsumoDesc, EsFacturable 
	Order by   
		FolioInicial, GUID  

	--				spp_FACT_Rpt_ReporteDeOperacion_00_ResumenRemisionado  


	--------------------------------------------- OBTENER INFORMACION DETALLADA DE REMSIONES 



	--------------------------------- SALIDA PARA GENERAR EL EXCEL 
	-------- Quitar caracteres especiales 
	Exec spp_FormatearTabla #Salida_Final 


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

		Set @sSql_Segmento = 'Insert Into #tmpResultados ( NombreTabla, EsGeneral ) select ' + char(39) + 'Resumen_Remisiones' + right('0000' + cast(@iSegmento as varchar(10)), 4) + char(39) + ', 1 ' 
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


