------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen 
Go--#SQL 

/* 

Exec spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen 
	@IdEmpresa = '004', @IdEstado = '11', 
	--@IdFarmaciaGenera = '', 
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

Create Proc spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen 
( 
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '', 
	@IdCliente varchar(4) = '42', @IdSubCliente varchar(4) = '', 

	@TipoDeUnidad int = 0,  
    ---- Todas = 0, 
    ---- Farmacias = 1, 
    ---- FarmaciasUnidosis = 2, 
    ---- Almacenes = 3, 
    ---- AlmacenesUnidosis = 4 
	@IdFarmacia varchar(4) = '', 

	@FechaInicial varchar(10) = '2022-05-11', @FechaFinal varchar(10) = '2022-05-20', 
	@TamañoBloque int = 150000  
)  
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max),  
	@sFiltro varchar(max),  
	@sFiltro_TipoDeRemision varchar(max),  
	@sFiltro_OrigenInsumo varchar(max),  
	@sFiltro_TipoDeInsumo varchar(max),  
	@sFiltro_Folios varchar(max),  
	@sFiltro_Fechas varchar(max), 
	@sFiltro_ListadoDeRemisiones varchar(max), 
	@sCampoVacio varchar(1), 
	@AplicarFiltroFechas int 


Declare 
	@sSql_Segmento varchar(max), 
	@iSegmento int, 
	@iRows_Totales int, 
	@iRowInicial int, 
	@iRowFinal int, 
	@iTamañoBloque int 


	Set @sSql_Segmento = '' 
	Set @iSegmento = 0 
	Set @iTamañoBloque = 50000  
	Set @iRows_Totales = 0 
	Set @iRowInicial = 1 
	Set @iRowFinal = @iTamañoBloque  




	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	--Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	--Set @IdCliente = dbo.fg_FormatearCadena(@IdCliente, '0', 4) 
	--Set @IdSubCliente = dbo.fg_FormatearCadena(@IdSubCliente, '0', 4) 

	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @sFiltro_Folios = '' 
	Set @sFiltro_Fechas = '' 
	Set @sFiltro_TipoDeRemision = '' 
	Set @sFiltro_OrigenInsumo = '' 
	Set @sFiltro_TipoDeInsumo = '' 
	Set @sFiltro_ListadoDeRemisiones = '' 
	Set @sCampoVacio = ''
	Set @AplicarFiltroFechas = 1 




	---------------------------- Listado de subclientes 
	Select IdCliente, NombreCliente as Cliente, IdSubCliente, NombreSubCliente as SubCliente  
	Into #tmp_ListaSubClientes 
	From vw_Clientes_SubClientes C 
	Where 1 = 0 

	Set @sFiltro = '' 
	If ( @IdCliente <> '' or @IdSubCliente <> '' ) 
	Begin 
		If ( @IdCliente <> '' and @IdSubCliente <> '' ) 
			Begin 
				Set @sFiltro = 'Where IdCliente = ' + char(39) + dbo.fg_FormatearCadena(@IdCliente, '0', 4) + char(39) + ' and IdSubCliente = ' + char(39) + dbo.fg_FormatearCadena(@IdSubCliente, '0', 4) + char(39)   
			End 
		Else 
			Begin 
			If ( @IdCliente <> '' ) 
				Begin 
					Set @sFiltro = 'Where IdCliente = ' + char(39) + dbo.fg_FormatearCadena(@IdCliente, '0', 4) + char(39)     
				End 
			End 

		--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen 
	End 

	Set @sSql = 
		'Insert Into #tmp_ListaSubClientes ( IdCliente, Cliente, IdSubCliente, SubCliente ) ' + char(13) + 
		'Select IdCliente, NombreCliente, IdSubCliente, NombreSubCliente ' + char(13) + 
		'From vw_Clientes_SubClientes (NoLock) ' + char(13) + 
		@sFiltro 
	Exec(@sSql) 
	--print @sSql 
	---------------------------- Listado de subclientes 


	---------------------------- Listado de subProgramas  
	Select IdPrograma, Programa, IdSubPrograma, SubPrograma  
	Into #tmp_ListaSubProgramas  
	From vw_Programas_SubProgramas C 
	Where 1 = 0 

	Set @sFiltro = '' 

	Set @sSql = 
		'Insert Into #tmp_ListaSubProgramas ( IdPrograma, Programa, IdSubPrograma, SubPrograma ) ' + char(13) + 
		'Select IdPrograma, Programa, IdSubPrograma, SubPrograma ' + char(13) + 
		'From vw_Programas_SubProgramas (NoLock) ' + char(13) + 
		@sFiltro 
	Exec(@sSql) 
	--print @sSql 
	---------------------------- Listado de subProgramas  


	---------------------------- Listado de Farmacias  
	Select IdEstado, IdFarmacia, Farmacia, EsAlmacen, EsUnidosis   
	Into #tmp_ListaFarmacias  
	From vw_Farmacias C 
	Where 1 = 0 

	Set @sFiltro = '' 
	If ( @TipoDeUnidad <> 0 ) 
	Begin 
		If ( @TipoDeUnidad = 1 ) 
		Begin	
			Set @sFiltro = ' and EsAlmacen = 0 and EsUnidosis = 0 ' 
		End 

		If ( @TipoDeUnidad = 2 ) 
		Begin	
			Set @sFiltro = ' and EsAlmacen = 0 and EsUnidosis = 1 ' 
		End 

		If ( @TipoDeUnidad = 3 ) 
		Begin	
			Set @sFiltro = ' and EsAlmacen = 1 and EsUnidosis = 0 ' 
		End 

		If ( @TipoDeUnidad = 4 ) 
		Begin	
			Set @sFiltro = ' and EsAlmacen = 1 and EsUnidosis = 1 ' 
		End 

		--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen 
	End 

	If @IdFarmacia <> '' 
	   Set @sFiltro = ' and IdFarmacia = ' + char(39) + dbo.fg_FormatearCadena(@IdFarmacia, '0', 4) + char(39)     

	Set @sFiltro = 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + @sFiltro 
	Set @sSql = 
		'Insert Into #tmp_ListaFarmacias ( IdEstado, IdFarmacia, Farmacia, EsAlmacen, EsUnidosis  ) ' + char(13) + 
		'Select IdEstado, IdFarmacia, Farmacia, EsAlmacen, EsUnidosis  ' + char(13) + 
		'From vw_Farmacias (NoLock) ' + char(13) + 
		@sFiltro 
	Exec(@sSql) 
	--print @sSql 
	---------------------------- Listado de Farmacias  


	---------------------------- Listado de Claves y Productos  
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 

	Select E.*  
	Into #vw_Claves_Precios_Asignados
	From vw_Claves_Precios_Asignados E 
	Inner Join #tmp_ListaSubClientes LC (NoLock) On ( E.IdEstado = @IdEstado and E.IdCliente = LC.IdCliente and E.IdSubCliente = LC.IdSubCliente ) 


	Select E.*  
	Into #vw_Relacion_ClavesSSA_Claves
	From vw_Relacion_ClavesSSA_Claves E 
	Inner Join #tmp_ListaSubClientes LC (NoLock) On ( E.IdEstado = @IdEstado and E.IdCliente = LC.IdCliente and E.IdSubCliente = LC.IdSubCliente ) 


	--select * from #vw_Claves_Precios_Asignados 
	---------------------------- Listado de Claves y Productos   


	--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen   


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
	------------------------------------------------------------------------------------- OBTENER INFORMACION 
	If exists ( select * from sysobjects (nolock) where name = '#tmp_InformacionDispensacion' and xType = 'U' ) Drop Table #tmp_InformacionDispensacion 

	select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
		LF.EsAlmacen, LF.EsUnidosis,  
		E.IdCliente, E.IdSubCliente, 
		L.IdSubFarmacia, L.EsConsignacion, 
		cast('' as varchar(200)) as Laboratorio, 		
		cast('' as varchar(100)) as ClaveSSA, cast('' as varchar(max)) as DescripcionClave,   
		L.IdProducto, L.CodigoEAN, 0 as ContenidoPaquete, -- L.ClaveLote, cast('' as varchar(10)) as Caducidad, L.SKU, 
		L.CantidadVendida as Cantidad,
		cast(0 as numeric(14,4)) as CantidadPaquetes, 
		
		cast(0 as numeric(14,4)) as SubTotal_Producto, 
		cast(0 as numeric(14,4)) as IVA_Producto, 
		cast(0 as numeric(14,4)) as Importe_Producto, 

		cast(0 as numeric(14,4)) as SubTotal_Servicio, 
		cast(0 as numeric(14,4)) as IVA_Servicio, 
		cast(0 as numeric(14,4)) as Importe_Servicio 

	Into #tmp_InformacionDispensacion   
	From VentasEnc E (NoLock) 
	Inner Join #tmp_ListaFarmacias LF (NoLock) On ( E.IdEstado = LF.IdEstado and E.IdFarmacia = LF.IdFarmacia ) 
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.FolioVenta = L.FolioVenta ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado 
		and ( convert(varchar(10), E.FechaRegistro, 120)  between @FechaInicial and @FechaFinal )  


	------------------------------------------------------------------------------------- OBTENER INFORMACION 




	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 

	----Update R Set Farmacia = F.Farmacia 
	----From #tmp_InformacionDispensacion R 
	----Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 

	Update R Set Laboratorio = P.Laboratorio, ClaveSSA = P.ClaveSSA, DescripcionClave = P.DescripcionClave, 
		ContenidoPaquete = P.ContenidoPaquete_ClaveSSA, 
		CantidadPaquetes = ( Cantidad / P.ContenidoPaquete_ClaveSSA )
	From #tmp_InformacionDispensacion R 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( R.IdProducto = P.IdProducto and R.CodigoEAN = P.CodigoEAN ) 



	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 



	--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion_Resumen   

	 
	-------------------------------------- Agregar información de catalogo 
	-------------------------------------- Agregar información de catalogo 



	------------------------------------------ SALIDA FINAL 
	Select 
		Identity(int, 1, 1) as Excel_Identity_Export,
		IdEstado, ClaveSSA, DescripcionClave, EsConsignacion, 

		sum(SubTotal_Producto) as SubTotal_Producto, 
		sum(IVA_Producto) as IVA_Producto, 
		sum(Importe_Producto) as Importe_Producto, 

		sum(SubTotal_Servicio) as SubTotal_Servicio, 
		sum(IVA_Servicio) as IVA_Servicio, 
		sum(Importe_Servicio) as Importe_Servicio 

	Into #Salida_Final 
	From #tmp_InformacionDispensacion  (NoLock)  
	Group by 
		IdEmpresa, IdEstado, 
		--IdProducto, CodigoEAN, 
		ClaveSSA, DescripcionClave, EsConsignacion   
	Order by 
		--TipoDeProceso, 
		IdEstado, ClaveSSA, DescripcionClave, EsConsignacion 
		

	------------------------------- SALIDA PARA GENERAR EL EXCEL 
	------ Quitar caracteres especiales 
	Exec spp_FormatearTabla #Salida_Final 


	----------------------------------------  VERIFICACION 
	--if exists  ( select * from sysobjects (nolock) Where name = 'RPT_FACT__SabanaValidacion' and xType = 'u' ) drop table RPT_FACT__SabanaValidacion  

	--Select * 
	--Into RPT_FACT__SabanaValidacion 
	--from #Salida_Final 
	----------------------------------------  VERIFICACION 



	Select @iRows_Totales = count(*) from #Salida_Final 

	Set @iTamañoBloque = @TamañoBloque   
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

 
