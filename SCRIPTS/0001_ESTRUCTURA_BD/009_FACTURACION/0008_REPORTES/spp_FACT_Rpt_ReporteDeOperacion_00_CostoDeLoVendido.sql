------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_11____CostoDeLoVendido' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_11____CostoDeLoVendido 
Go--#SQL   

--	Exec spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido @AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  


------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido 
Go--#SQL   

Create Proc	 spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido  
(
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '42', @IdSubCliente varchar(4) = '', 
	
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 

	@TipoDeRemision int = 1, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 0, 
	@FolioInicial int = 555, @FolioFinal int = 556, 

	@TipoDeFecha int = 2, 
	@AplicarFiltroFechas int = 1, 
	@FechaInicial varchar(10) = '2022-03-01', @FechaFinal varchar(10) = '2022-03-31'
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
	Select * 
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

	-- sp_listacolumnas vw_FACT_Remisiones 
	
	/* 
	GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, 
	IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, EsParaExcedente, Alias, IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, 
	Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, Observaciones_Referencias, 
	TieneFacturaAsociada, FolioFacturaElectronica, EsRelacionDocumento, FolioRelacionDocumento, ReferenciaDocumento, 
	EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, 
	EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, 
	OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, TipoDeInsumo_Clasificacion, 
	TipoDeInsumo_ClasificacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal, CampoVacio
	*/ 

	Select
		--top 400 
		--identity(int, 1, 1) as Excel_Identity_Export, 
		F.EsRelacionFacturaPrevia, F.EsRelacionDocumento, F.FolioRelacionDocumento, 
		F.FolioFacturaElectronica, 
		S.FolioRemision, 
		FechaRemision,
		cast('' As varchar(10)) As FechaVenta, 
		0 as Año_Venta, 
		cast('' As varchar(50)) As Mes_Venta, 
		S.IdEmpresa, S.IdEstado, S.IdFarmacia, 
		F.IdCliente, F.IdSubCliente, 
		--cast('' As varchar(500)) As NombreOficial, 
		cast('' As varchar(500)) As NombreFarmacia,
		FolioVenta, 
		--cast('' As varchar(200)) As IdCliente, cast('' As varchar(200)) As IdSubCliente, 
		TipoDeRemision, TipoDeRemisionDesc As TipoDeRemisionDesc, TipoDeInsumoDesc,
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, F.Financiamiento, 
		F.OrigenInsumoDesc, S.IdProducto, S.CodigoEAN, S.ClaveLote, S.IdSubFarmacia, 
		S.SKU, 
		S.ClaveSSA, 
		--cast('' As varchar(200)) As Mascara, 
		cast('' As varchar(max)) As Descripcion, cast('' As varchar(200)) As Presentacion, --referencia_01 As Proyecto,

		S.TasaIva, 	
		Cantidad,									-- Se toma el Contenido Paquete comercial, a como se compra 
		Cantidad_Agrupada,							-- Se toma el Contenido Paquete de acuerdo a Licitacion 
		Cantidad_Agrupada as Cantidad_Dispensada,	-- Se toma el Contenido Paquete de acuerdo a Licitacion 
		0 as Factor_Licitacion, 

		0 as ContenidoPaquete_Comercial, 

		cast(0 as numeric(14,4)) as CostoDeCompra_Unitario, 
		cast(0 as numeric(14,4)) as CostoDeCompra, 
		cast(0 as numeric(14,4)) as SubTotal_CostoDeCompra, 
		cast(0 as numeric(14,4)) as IVA_CostoDeCompra, 
		cast(0 as numeric(14,4)) as Importe_CostoDeCompra, 

		cast(S.PrecioLicitado as numeric(14,4)) as PrecioDeVenta_Factura, 
		cast(S.PrecioLicitadoUnitario as numeric(14,4)) as PrecioDeVentaUnitario, 
		cast(S.PrecioLicitado as numeric(14,4)) as PrecioDeVenta, 
		cast(0 as numeric(14,4)) as SubTotal_PrecioDeVenta, 
		cast(0 as numeric(14,4)) as IVA_PrecioDeVenta, 
		cast(0 as numeric(14,4)) as Importe_PrecioDeVenta,  

		cast(0 as numeric(14,4)) as Diferencia_SubTotales__CostoCompra_vs_PrecioVenta,   
		cast('' as varchar(50)) as Tipo__Diferencia__CostoCompra_vs_PrecioVenta

		----(CAse when TasaIva > 0 Then S.SubTotalGrabado else S.SubTotalSinGrabar End) As SubTotal_PrecioDeVenta, Iva as IVA_PrecioDeVenta, Importe As Importe_PrecioDeVenta 
	Into #Salida 
	From FACT_Remisiones_Detalles S (NoLock) 
	Inner Join #vw_FACT_Remisiones F On ( S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmaciaGenera = F.IdFarmacia And S.FolioRemision = F.FolioRemision ) 
	Where S.Cantidad > 0 and S.Cantidad_Agrupada > 0  
		--and S.IdFarmacia = 4093 -- and S.ClaveSSA = '060.456.0334' 
		--and S.IdFarmacia = 4005 and S.ClaveSSA = '060.456.0334' 
		--and S.IdFarmacia = 4022 and S.ClaveSSA = '060.088.0850' 
		--and S.IdFarmacia = 4005 
		--and S.ClaveSSA = '060.439.0054' 
		--and S.ClaveSSA in 
		--( 
		--	----'060.456.0334', '060.456.0318', '060.203.0546', '060.088.0686', '060.203.0561', 
		--	----'060.088.0892', '060.456.0318', '060.550.0016', '060.550.0735', '060.550.2608', 
		--	----'060.203.0066', '060.130.0015', '060.456.0300', '060.550.0677', '060.550.0016', 
		--	----'060.456.0367', '060.456.0359' 

		--	'060.456.0300' 

		--	--'060.456.0334' 
		--) 
		-- and S.ClaveSSA = '060.532.0167'  

	--select * from #Salida 

--		Exec spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido		@AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  
	


	----select * from #Salida 


	--------------------------- ACTUALIZAR INFORMACION  
	--Select top 20 * 
	
	Update S Set S.IdCliente = F.IdCliente, S.IdSubCliente = F.IdSubCliente, S.FechaVenta = Convert(varchar(10), F.FechaRegistro, 120)
	From #Salida S (NoLock) 
	Inner Join VentasEnc F (NoLock) On ( S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmacia = F.IdFarmacia And S.FolioVenta = F.FolioVenta )

	Update S Set Año_Venta = datepart(year, FechaVenta), Mes_Venta = dbo.fg_NombresDeMes(FechaVenta) 
	From #Salida S (NoLock) 


	Update S Set Descripcion = P.DescripcionClave, Presentacion = P.Presentacion_ClaveSSA, ContenidoPaquete_Comercial = P.ContenidoPaquete 
	From #Salida S (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( s.IdProducto = P.IdProducto and S.CodigoEAN = P.CodigoEAN  ) 

	----Update S Set S.Mascara = F.Mascara, S.Descripcion = F.Descripcion, S.Presentacion = F.Presentacion, S.TipoInsumo = F.TipoDeClaveDescripcion
	----From #Salida S (NoLock)
	----Inner Join vw_ClaveSSA_Mascara F (NoLock) 
	----	On ( S.IdEstado = F.IdEstado and S.IdCliente = F.IdCliente and S.IdSubCliente = F.IdSubCliente and S.ClaveSSA = F.ClaveSSA ) 

	Update S Set 
		--S.NombreOficial = F.NombreFarmacia, 
		S.NombreFarmacia = F.NombreFarmacia
	From #Salida S (NoLock)
	Inner Join CatFarmacias F On ( S.IdEstado = F.IdEstado and S.IdFarmacia = F.IdFarmacia ) 

--		Exec spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido		@AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  

	-------------------------------------------------- CALCULAR EL PRECIO DE VENTA EN RAZON DEL FACTOR Y EL CONTENIDO PAQUETE COMERCIAL 
	Update S Set Factor_Licitacion = P.Factor   
	From #Salida S 
	Inner Join vw_Claves_Precios_Asignados P (noLock) 
		On ( S.IdEstado = P.IdEstado and S.IdCliente = P.IdCliente and S.IdSubCliente = P.IdSubCliente and S.ClaveSSA = P.ClaveSSA  )  
	-------------------------------------------------- CALCULAR EL PRECIO DE VENTA EN RAZON DEL FACTOR Y EL CONTENIDO PAQUETE COMERCIAL 


	-------------------------------  INFORMACIÓN PARA CALCULAR EL COSTO DE LO VENDIDO  
	Update V Set CostoDeCompra_Unitario = F.UltimoCosto, 
		--CostoDeCompra = (( F.UltimoCosto * V.ContenidoPaquete_Comercial )  / (Factor_Licitacion *1.0) ) 
		CostoDeCompra = ( F.UltimoCosto * V.ContenidoPaquete_Comercial ) 
	From #Salida V 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.IdSubFarmacia = F.IdSubFarmacia
			and V.IdProducto = F.IdProducto and V.CodigoEAN = F.CodigoEAN and V.ClaveLote = F.ClaveLote and V.SKU = F.SKU ) 
	
	Update V Set CostoDeCompra = ( V.CostoDeCompra_Unitario * 1 )  
	From #Salida V 
	Where V.Factor_Licitacion > 1 and ContenidoPaquete_Comercial > 1 
	-------------------------------  INFORMACIÓN PARA CALCULAR EL COSTO DE LO VENDIDO  


	-------------------------------------------------- CALCULAR EL PRECIO DE VENTA EN RAZON DEL FACTOR Y EL CONTENIDO PAQUETE COMERCIAL 
	----Update S Set Factor_Licitacion = P.Factor   
	----From #Salida S 
	----Inner Join vw_Claves_Precios_Asignados P (noLock) 
	----	On ( S.IdEstado = P.IdEstado and S.IdCliente = P.IdCliente and S.IdSubCliente = P.IdSubCliente and S.ClaveSSA = P.ClaveSSA  )  

	Update S Set PrecioDeVenta = ( S.PrecioDeVenta * Factor_Licitacion )
		--, CostoDeCompra_Unitario = ( CostoDeCompra / (Factor_Licitacion *1.0) ) 
		--, CostoDeCompra = ( CostoDeCompra / (Factor_Licitacion *1.0) ) 
	From #Salida S 
	Where ContenidoPaquete_Comercial = 1 and Factor_Licitacion > 1 


	--Update S Set PrecioDeVenta = ( S.PrecioDeVenta ) * 1 --P.Factor 
	--From #Salida S 
	--Inner Join vw_Claves_Precios_Asignados P (noLock) 
	--	On ( S.IdEstado = P.IdEstado and S.IdCliente = P.IdCliente and S.IdSubCliente = P.IdSubCliente and S.ClaveSSA = P.ClaveSSA  )  
	--Where Cantidad <> Cantidad_Agrupada 

	-------------------------------------------------- CALCULAR EL PRECIO DE VENTA EN RAZON DEL FACTOR Y EL CONTENIDO PAQUETE COMERCIAL 

--		Exec spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido		@AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  



	-------------------------------  CALCULAR IMPORTES 

	Update D Set 	
		SubTotal_PrecioDeVenta = dbo.fg_PRCS_Redondear(round(PrecioDeVenta * Cantidad_Dispensada, 2, 0), 2, 0), 
		SubTotal_CostoDeCompra = dbo.fg_PRCS_Redondear(round(CostoDeCompra * Cantidad_Dispensada, 2, 0), 2, 0) 		
	From #Salida D (NoLock) 
	--where TasaIVA = 0 

	Update D Set 	
		IVA_PrecioDeVenta = dbo.fg_PRCS_Redondear((SubTotal_PrecioDeVenta * ( TasaIva / 100.00 )), 2, 0), 
		IVA_CostoDeCompra = dbo.fg_PRCS_Redondear((SubTotal_CostoDeCompra * ( TasaIva / 100.00 )), 2, 0) 
	From #Salida D (NoLock) 
	where TasaIVA > 0 

	Update D Set IVA_PrecioDeVenta = round(IVA_PrecioDeVenta, 3, 1) 
	From #Salida D (NoLock) 
	where TasaIVA > 0 

	Update D Set IVA_CostoDeCompra = round(IVA_CostoDeCompra, 2, 1) 
	From #Salida D (NoLock) 
	where TasaIVA > 0 

	Update D Set Importe_PrecioDeVenta = SubTotal_PrecioDeVenta + IVA_PrecioDeVenta, Importe_CostoDeCompra = SubTotal_CostoDeCompra + IVA_CostoDeCompra 
	From #Salida D (NoLock) 

	Update D Set Diferencia_SubTotales__CostoCompra_vs_PrecioVenta = SubTotal_PrecioDeVenta - SubTotal_CostoDeCompra  
	From #Salida D (NoLock) 

	Update D Set Tipo__Diferencia__CostoCompra_vs_PrecioVenta = 'SIN DIFERENCIA' 
	From #Salida D (NoLock) 
	Where Diferencia_SubTotales__CostoCompra_vs_PrecioVenta = 0 

	Update D Set Tipo__Diferencia__CostoCompra_vs_PrecioVenta = 'DIFERENCIA NEGATIVA' 
	From #Salida D (NoLock) 
	Where Diferencia_SubTotales__CostoCompra_vs_PrecioVenta < 0 

	Update D Set Tipo__Diferencia__CostoCompra_vs_PrecioVenta = 'DIFERENCIA POSITIVA' 
	From #Salida D (NoLock) 
	Where Diferencia_SubTotales__CostoCompra_vs_PrecioVenta > 0 
	-------------------------------  CALCULAR IMPORTES 



	--------------------------- ACTUALIZAR INFORMACION  

	-----------------	QUITAR CAMPOS AUXILIARES 
	Alter table #Salida Drop Column IdCliente 
	Alter table #Salida Drop Column IdSubCliente 


	----	spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido 


	--Select * From vw_FACT_Remisiones

	--Select *--FechaRemision, FechaVenta, FolioVenta, NumReceta, ApPaterno, Nombre, FolioReferencia, Domicilio, FolioRemision, TipoDeRemisionDesc, TipoInsumo, Partida, Observaciones, IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento], ClaveSSA, Descripcion, Presentacion, Referencia_01, Referencia_05, PrecioLicitado, TasaIva, Cantidad, SubTotal, Iva, Total
	

	------------------------------------------------------------------------------------------------ GENERAR MATRIZ CRUZADA  






	------------------------------------------ SALIDA FINAL 
	Select 
		Identity(int, 1, 1) as Excel_Identity_Export, 
		* 
	Into #Salida_Final 
	From #Salida (NoLock)  
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
	Order by 
		--TipoDeProceso, 
		IdEstado, IdFarmacia, FolioVenta, ClaveSSA 
	

	----	spp_FACT_Rpt_ReporteDeOperacion_00_CostoDeLoVendido 





	------------------------------- SALIDA PARA GENERAR EL EXCEL 
	Select @iRows_Totales = count(*) from #Salida_Final 

	Set @iTamañoBloque = 20000  
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


