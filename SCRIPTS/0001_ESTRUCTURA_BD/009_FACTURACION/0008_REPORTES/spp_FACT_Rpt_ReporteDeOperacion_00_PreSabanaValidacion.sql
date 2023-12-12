------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion 
Go--#SQL 

/* 

Exec spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion 
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

Create Proc spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion 
( 
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '', 
	@IdCliente varchar(4) = '42', @IdSubCliente varchar(4) = '3', 

	@TipoDeUnidad int = 1,  
    ---- Todas = 0, 
    ---- Farmacias = 1, 
    ---- FarmaciasUnidosis = 2, 
    ---- Almacenes = 3, 
    ---- AlmacenesUnidosis = 4 
	@IdFarmacia varchar(4) = '', 

	@FechaInicial varchar(10) = '2022-12-01', @FechaFinal varchar(10) = '2022-12-31', 
	@TamañoBloque int = 50000  
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
	Select IdFuenteFinanciamiento, IdFinanciamiento, 1 as Tipo, ClaveSSA  
	Into #tmp_Lista_FF 
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C 
	Where 1 = 0 

	Set @sFiltro = '' 


	Set @sSql = 
		'Insert Into #tmp_Lista_FF ( IdFuenteFinanciamiento, IdFinanciamiento, Tipo, ClaveSSA ) ' + char(13) + 
		'Select IdFuenteFinanciamiento, IdFinanciamiento, 1 as Tipo, ClaveSSA ' + char(13) + 
		'From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) ' + char(13) + 
		@sFiltro 
	Exec(@sSql) 
	print @sSql 

	Set @sSql = 
		'Insert Into #tmp_Lista_FF ( IdFuenteFinanciamiento, IdFinanciamiento, Tipo, ClaveSSA ) ' + char(13) + 
		'Select IdFuenteFinanciamiento, IdFinanciamiento, 2 as Tipo, ClaveSSA ' + char(13) + 
		'From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA (NoLock) ' + char(13) + 
		@sFiltro 
	Exec(@sSql) 
	print @sSql 
	---------------------------- Listado de Fuentes de financiamiento 


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

		--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion 
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
	Select IdEstado, IdFarmacia, Farmacia  
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

		--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion 
	End 

	If @IdFarmacia <> '' 
	   Set @sFiltro = ' and IdFarmacia = ' + char(39) + dbo.fg_FormatearCadena(@IdFarmacia, '0', 4) + char(39)     

	Set @sFiltro = 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + @sFiltro 
	Set @sSql = 
		'Insert Into #tmp_ListaFarmacias ( IdEstado, IdFarmacia, Farmacia ) ' + char(13) + 
		'Select IdEstado, IdFarmacia, Farmacia ' + char(13) + 
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


	Select E.*  
	Into #vw_ClaveSSA_Mascara 
	From vw_ClaveSSA_Mascara E 
	Inner Join #tmp_ListaSubClientes LC (NoLock) On ( E.IdEstado = @IdEstado and E.IdCliente = LC.IdCliente and E.IdSubCliente = LC.IdSubCliente ) 

	--select * from #vw_Claves_Precios_Asignados 
	---------------------------- Listado de Claves y Productos   


	--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion   


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
	------------------------------------------------------------------------------------- OBTENER INFORMACION 
	If exists ( select * from sysobjects (nolock) where name = '#tmp_InformacionDispensacion' and xType = 'U' ) Drop Table #tmp_InformacionDispensacion 

	select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, cast('' as varchar(500)) as Farmacia, cast('' as varchar(20)) as CLUES, 
		E.FolioVenta, E.FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 

		0 as EsGeneradaPorVale, 
		cast('' as varchar(10)) as IdTipoDeDispensacion, 
		cast('' as varchar(200)) as TipoDispensacionDescripcion, 

		cast('' as varchar(100)) as NumeroReceta, cast('' as varchar(10)) as FechaReceta, 
		cast('' as varchar(10)) as IdBeneficiario, cast('' as varchar(200)) as Beneficiario, 
		cast('' as varchar(100)) as NumeroReferencia, 
		cast('' as varchar(10)) as IdMedico, cast('' as varchar(200)) as Medico, cast('' as varchar(100)) as CedulaMedico, 
		cast('' as varchar(10)) as IdDiagnostico, cast('' as varchar(max)) as Diagnostico, 
		L.IdSubFarmacia, L.EsConsignacion, 
		cast('' as varchar(200)) as Laboratorio, 		
		cast('' as varchar(100)) as ClaveSSA, cast('' as varchar(100)) as ClaveSSA_Base, 
		cast('' as varchar(max)) as DescripcionClave,   
		L.IdProducto, L.CodigoEAN, 0 as ContenidoPaquete, 0 as ContenidoPaquete_Licitado, 
		L.ClaveLote, cast('' as varchar(10)) as Caducidad, L.SKU, 
		L.CantidadVendida as Cantidad, 
	
		
		cast('' as varchar(100)) as Mascara, cast('' as varchar(max)) as DescripcionMascara    	

	Into #tmp_InformacionDispensacion   
	From VentasEnc E (NoLock) 
	Inner Join #tmp_ListaFarmacias LF (NoLock) On ( E.IdEstado = LF.IdEstado and E.IdFarmacia = LF.IdFarmacia ) 
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.FolioVenta = L.FolioVenta ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado 
		and ( convert(varchar(10), E.FechaRegistro, 120)  between @FechaInicial and @FechaFinal )  


	------------------------------------------------------------------------------------- OBTENER INFORMACION 




	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 

	Update R Set Farmacia = F.Farmacia 
	From #tmp_InformacionDispensacion R 
	Inner Join #tmp_ListaFarmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 

	Update R Set CLUES = F.CLUES  
	From #tmp_InformacionDispensacion R 
	Inner Join FACT_Farmacias_CLUES__CentroDeCostos F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 

	Update R Set 
		Laboratorio = P.Laboratorio, ClaveSSA = P.ClaveSSA, ClaveSSA_Base = P.ClaveSSA, 
		DescripcionClave = P.DescripcionClave, ContenidoPaquete = P.ContenidoPaquete_ClaveSSA 
	From #tmp_InformacionDispensacion R 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( R.IdProducto = P.IdProducto and R.CodigoEAN = P.CodigoEAN ) 

	Update R Set IdBeneficiario = V.IdBeneficiario, IdMedico = V.IdMedico, NumeroReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120), 
		IdDiagnostico = V.IdDiagnostico, IdTipoDeDispensacion = V.IdTipoDeDispensacion 
	From #tmp_InformacionDispensacion R 
	Inner Join VentasInformacionAdicional V (NoLock) On ( V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and R.IdFarmacia = V.IdFarmacia and R.FolioVenta = V.FolioVenta )  

	Update R Set TipoDispensacionDescripcion = V.Descripcion 
	From #tmp_InformacionDispensacion R 
	Inner Join CatTiposDispensacion V (NoLock) On ( R.IdTipoDeDispensacion = V.IdTipoDeDispensacion ) 

	Update R Set EsGeneradaPorVale = 1 
	From #tmp_InformacionDispensacion R 
	Where IdTipoDeDispensacion = '07' 

	Update R Set NumeroReferencia = (case when B.CURP <> '' then B.CURP else B.FolioReferencia end), Beneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) 
	From #tmp_InformacionDispensacion R 
	Inner Join CatBeneficiarios B (NoLock) 
		On (  R.IdEstado = B.IdEstado and R.IdFarmacia = B.IdFarmacia and R.IdCliente = B.IdCliente and R.IdSubCliente = B.IdSubCliente and R.IdBeneficiario = B.IdBeneficiario ) 


	Update R Set Medico = (replace(B.ApPaterno, char(34), '') + ' ' + replace(B.ApMaterno, char(34), '') + ' ' + replace(B.Nombre, char(34), '')), CedulaMedico = B.NumCedula 
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



	Update E Set ClaveSSA = M.ClaveSSA 
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join  #vw_Relacion_ClavesSSA_Claves M (NoLock) On ( E.ClaveSSA_Base = M.ClaveSSA_Relacionada ) 

	Update E Set ContenidoPaquete_Licitado = M.ContenidoPaquete_Licitado 
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join  #vw_Claves_Precios_Asignados M (NoLock) On ( E.ClaveSSA = M.ClaveSSA ) 

	Update E Set Mascara = M.Mascara, DescripcionMascara = M.DescripcionMascara 
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join #vw_ClaveSSA_Mascara M (NoLock) On ( E.ClaveSSA = M.ClaveSSA )

	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 



	--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion   

	 
	-------------------------------------- Agregar información de catalogo 
	-------------------------------------- Agregar información de catalogo 



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
		, 'Mascara' = Mascara
		, 'Descripción mascara' = DescripcionMascara
		, 'Contenido paquete licitado' = ContenidoPaquete_Licitado  
		, 'Es generada por Vale' = (case when EsGeneradaPorVale = 1 then 'SI' else 'NO' end) 

		, 'Tipo de dispensación' = TipoDispensacionDescripcion 
	Into #Salida_Final  
	From #tmp_InformacionDispensacion  (NoLock)  
	--Where EsGeneradaPorVale = 1 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, Farmacia, 
		FolioVenta, FechaRegistro, 
		--IdCliente, IdSubCliente, 
		FolioVenta, FechaRegistro, 
		NumeroReceta, FechaReceta, NumeroReferencia, Beneficiario, IdMedico, Medico, 
		--IdProducto, CodigoEAN, 
		ClaveSSA, ClaveLote, DescripcionClave, EsConsignacion,  
		ContenidoPaquete, Caducidad, IdDiagnostico, Diagnostico, CLUES 
		, Mascara, DescripcionMascara, ContenidoPaquete_Licitado 
		, EsGeneradaPorVale, TipoDispensacionDescripcion 
	Order by 
		--TipoDeProceso, 
		IdEstado, IdFarmacia, FolioVenta --, ClaveSSA 
		



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

 
