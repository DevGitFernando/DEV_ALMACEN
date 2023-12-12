------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar 
Go--#SQL 

/* 

Exec spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar 
	@IdEmpresa = '004', @IdEstado = '11', 
	@IdCliente = '42', @IdSubCliente = '4', 

	--@TipoDeUnidad = 1,  
    ---- Todas = 0, 
    ---- Farmacias = 1, 
    ---- FarmaciasUnidosis = 2, 
    ---- Almacenes = 3, 
    ---- AlmacenesUnidosis = 4 

	@Precio_Servicio = 11, 
	@Precio_Servicio_IVA = 16, 

	@FechaInicial = '2023-02-01', @FechaFinal = '2023-02-28' 

*/ 

Create Proc spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar 
( 
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '', 
	@IdCliente varchar(4) = '42', @IdSubCliente varchar(4) = '4', 


	@FechaInicial varchar(10) = '2023-04-01', @FechaFinal varchar(10) = '2023-04-19', 

	@TipoDeUnidad int = 0   
    ---- Todas = 0, 
    ---- Farmacias = 1, 
    ---- FarmaciasUnidosis = 2, 
    ---- Almacenes = 3, 
    ---- AlmacenesUnidosis = 4 

	, @Precio_Servicio numeric(14,4) = 11  
	, @Precio_Servicio_IVA numeric(14,4) = 16 
	--, @TablaProceso varchar(500) = '', @Procesar_x_Clave int = 0  

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
	@AplicarFiltroFechas int, 
	@sTipoDeUnidades varchar(500) 


Declare 
	@sSql_Segmento varchar(max), 
	@iSegmento int, 
	@iRows_Totales int, 
	@iRowInicial int, 
	@iRowFinal int, 
	@iTamañoBloque int 


Declare 
	@i int 
	--@TipoDeUnidad int = 0,  
    ---- Todas = 0, 
    ---- Farmacias = 1, 
    ---- FarmaciasUnidosis = 2, 
    ---- Almacenes = 3, 
    ---- AlmacenesUnidosis = 4 

	--@Precio_Servicio numeric(14,4) = 11, 
	--@Precio_Servicio_IVA numeric(14,4) = 16 
	


	Set @sSql_Segmento = '' 
	Set @iSegmento = 0 
	Set @iTamañoBloque = 50000  
	Set @iRows_Totales = 0 
	Set @iRowInicial = 1 
	Set @iRowFinal = @iTamañoBloque  




	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	--Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	Set @IdCliente = dbo.fg_FormatearCadena(@IdCliente, '0', 4) 
	Set @IdSubCliente = dbo.fg_FormatearCadena(@IdSubCliente, '0', 4) 

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

		--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar 
	End 

	Set @sSql = 
		'Insert Into #tmp_ListaSubClientes ( IdCliente, Cliente, IdSubCliente, SubCliente ) ' + char(13) + 
		'Select IdCliente, NombreCliente, IdSubCliente, NombreSubCliente ' + char(13) + 
		'From vw_Clientes_SubClientes (NoLock) ' + char(13) + 
		@sFiltro 
	Exec(@sSql) 
	--print @sSql 
	---------------------------- Listado de subclientes 


	---------------------------- Listado de Farmacias  
	Select IdEstado, IdFarmacia, Farmacia  
	Into #tmp_ListaFarmacias  
	From vw_Farmacias C 
	Where 1 = 0 

	Set @sTipoDeUnidades = 'TODAS LAS UNIDADES' 
	Set @sFiltro = '' 
	Set @sFiltro = 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + @sFiltro 
	If ( @TipoDeUnidad <> 0 ) 
	Begin 
		If ( @TipoDeUnidad = 1 ) 
		Begin	
			Set @sTipoDeUnidades = 'FARMACIAS' 
			Set @sFiltro = @sFiltro + ' and EsAlmacen = 0 and EsUnidosis = 0 ' 
		End 

		If ( @TipoDeUnidad = 2 ) 
		Begin	
			Set @sTipoDeUnidades = 'FARMACIAS DOSIS UNITARIA' 
			Set @sFiltro = @sFiltro + ' and EsAlmacen = 0 and EsUnidosis = 1 ' 
		End 

		If ( @TipoDeUnidad = 3 ) 
		Begin	
			Set @sTipoDeUnidades = 'ALMACENES' 
			Set @sFiltro = @sFiltro + ' and EsAlmacen = 1 and EsUnidosis = 0 ' 
		End 

		If ( @TipoDeUnidad = 4 ) 
		Begin	
			Set @sTipoDeUnidades = 'ALMACENES DOSIS UNITARIA' 
			Set @sFiltro = @sFiltro + ' and EsAlmacen = 1 and EsUnidosis = 1 ' 
		End 

		--		spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion 
	End 

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
	--where E.ClaveSSA like '%4148%'

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


	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
	------------------------------------------------------------------------------------- OBTENER INFORMACION 
	If exists ( select * from sysobjects (nolock) where name = '#tmp_InformacionDispensacion' and xType = 'U' ) Drop Table #tmp_InformacionDispensacion 
	If exists ( select * from sysobjects (nolock) where name = '#tmp_InformacionDispensacion_Detales' and xType = 'U' ) Drop Table #tmp_InformacionDispensacion_Detales 


	If exists ( select * from sysobjects (nolock) where name = '#tmp_Informacion__Comprobacion_Documentos' and xType = 'U' ) Drop Table #tmp_Informacion__Comprobacion_Documentos 
	If exists ( select * from sysobjects (nolock) where name = '#tmp_Informacion__Comprobacion_Facturas' and xType = 'U' ) Drop Table #tmp_Informacion__Comprobacion_Facturas 


	---------------------------------------	COMPROBACION  
	Select 
		D.ClaveSSA, sum( Cantidad_A_Comprobar - Cantidad_Distribuida ) as Cantidad_x_Comprobar 
	Into #tmp_Informacion__Comprobacion_Documentos 
	From FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) 
	Inner Join FACT_Remisiones__RelacionDocumentos D (NoLock) On ( E.FolioRelacion = D.FolioRelacion ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado 
		and ( Cantidad_A_Comprobar - Cantidad_Distribuida ) > 0 
		--and ClaveSSA like '%010.000.4148%' 
	Group by 
		D.ClaveSSA 


	Select 
		D.ClaveSSA, F.TipoDocumento, F.TipoDocumentoDescripcion,  
		sum( Cantidad_Facturada - Cantidad_Distribuida ) as Cantidad_x_Comprobar  
		
		, cast(max( Cantidad_Facturada / CantidadAgrupada_Facturada ) as numeric(14,4)) as ContenidoPaquete 
		, max(DF.PrecioUnitario) as Precio 
		, cast(max(DF.PrecioUnitario / (Cantidad_Facturada / CantidadAgrupada_Facturada)) as numeric(14,4)) as PrecioUnitario 
		--, sum( CantidadAgrupada_Facturada - CantidadAgrupada_Distribuida ) as Cantidad_x_Comprobar,  				
		--, sum( CantidadAgrupada_Facturada - CantidadAgrupada_Distribuida ) as Cantidad_x_Comprobar_Agrupada 		

	Into #tmp_Informacion__Comprobacion_Facturas 
	From FACT_Remisiones__RelacionFacturas_Enc E (NoLock) 
	Inner Join FACT_Remisiones__RelacionFacturas D (NoLock) On ( E.FolioRelacion = D.FolioRelacion ) 
	Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) On ( E.Serie = F.Serie and E.Folio = F.Folio ) 
	Left Join FACT_CFD_Documentos_Generados_Detalles DF (NoLock) On ( F.Serie = DF.Serie and F.Folio = DF.Folio and D.ClaveSSA = DF.Clave ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado 
		and ( Cantidad_Facturada - Cantidad_Distribuida ) > 0 
		--and ClaveSSA in ( '010.000.5428.00', '010.000.2308.00', '010.000.0476.00' ) 
		--and E.IdFarmacia between 4000 and 5000 
		--and ClaveSSA like '%010.000.4148%' 
	Group by 
		D.ClaveSSA, F.TipoDocumento, F.TipoDocumentoDescripcion  


	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   

	----select * 
	----from #tmp_Informacion__Comprobacion_Documentos 

	----select * 
	----from #tmp_Informacion__Comprobacion_Facturas 

	--where ClaveSSA in ( '010.000.5428.00', '010.000.2308.00', '010.000.0476.00' ) 


	----select 
	----	TipoDocumento, TipoDocumentoDescripcion, 
	----	sum(Cantidad_x_Comprobar) as Cantidad_x_Comprobar,   
	----	sum(Cantidad_x_Comprobar_Agrupada) as Cantidad_x_Comprobar_Agrupada
	----from #tmp_Informacion__Comprobacion_Facturas 
	----Group by 
	----	TipoDocumento, TipoDocumentoDescripcion  


	--return 
	---------------------------------------	COMPROBACION  


	--------------------------------------- DISPENSACION   
	select 
		E.IdEmpresa, E.IdEstado, 
		---E.IdFarmacia, cast('' as varchar(500)) as Farmacia, cast('' as varchar(20)) as CLUES, 
		E.IdCliente, E.IdSubCliente, 
		E.IdFarmacia, 
		L.IdProducto, L.CodigoEAN, 
		P.ClaveSSA, 
		P.ClaveSSA as ClaveSSA_Base, 
		--P.ClaveSSA_Base, 
		(case when L.ClaveLote like '%*%' then 1 else 0 end) EsConsignacion, 
		0 as ContenidoPaquete, 0 as ContenidoPaquete_Licitado, 
		P.TasaIva, 
		( L.CantidadVendida - L.CantidadRemision_Insumo ) as Cantidad_Insumo,   
		( L.CantidadVendida - L.CantidadRemision_Admon ) as Cantidad_Servicio 	
	Into #tmp_InformacionDispensacion_Detales   
	From VentasEnc E (NoLock) 
	Inner Join #tmp_ListaSubClientes C (NoLock) On ( E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente ) 
	Inner Join #tmp_ListaFarmacias LF (NoLock) On ( E.IdEstado = LF.IdEstado and E.IdFarmacia = LF.IdFarmacia ) 
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.FolioVenta = L.FolioVenta ) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado 
		and E.IdFarmacia >= 5000 
		--and E.IdCliente = @IdCliente and E.IdSubCliente = @IdSubCliente 
		and ( convert(varchar(10), E.FechaRegistro, 120)  between @FechaInicial and @FechaFinal )  
		and 
		( 
			( L.CantidadVendida - L.CantidadRemision_Insumo ) > 0 
			or 
			( L.CantidadVendida - L.CantidadRemision_Admon ) > 0 
		) 
		--and P.ClaveSSA in ( '010.000.2119.00', 'SC-MED-2195' ) 
		--and P.ClaveSSA in ( '010.000.1311.00' ) 
		--010.000.2119.00	
		--and P.ClaveSSA like '%060%300%'
		----and P.ClaveSSA in 
		----( 
		----	'010.000.5621.00', 
		----	'010.000.4582.00', '010.000.5295.01', '010.000.5309.00', '010.000.5428.00', '010.000.2542.00', '030.000.0011.00', '010.000.1924.00', '040.000.4484.00', '010.000.4241.00', 
		----	'010.000.4158.00', '010.000.5721.00', '010.000.2128.00', '010.000.0597.00', '010.000.4358.01', '010.000.0402.00', '010.000.2308.00', '010.000.3626.00', '030.000.0003.00', 
		----	'010.000.4148.01', '010.000.5117.00', '010.000.5256.00', '010.000.5187.00', '010.000.3627.00', '010.000.5191.00', '010.000.4095.00', '010.000.2540.00', '010.000.0476.00' 
		----) 
		--and P.ClaveSSA in ( '010.000.5428.00', '010.000.2308.00', '010.000.0476.00' ) 
		--and P.ClaveSSA like '%010.000.4148%' 


	Update E Set ClaveSSA = M.ClaveSSA, ClaveSSA_Base = M.ClaveSSA  
	From #tmp_InformacionDispensacion_Detales E (NoLock) 
	Inner Join  #vw_Relacion_ClavesSSA_Claves M (NoLock) On ( E.ClaveSSA_Base = M.ClaveSSA_Relacionada and M.Status = 'A' )  

	--select ClaveSSA, ClaveSSA_Base
	--from #tmp_InformacionDispensacion_Detales 
	--group by ClaveSSA, ClaveSSA_Base 

	--select EsConsignacion, sum(Cantidad_Insumo) from #tmp_InformacionDispensacion_Detales group by EsConsignacion 


	Update E Set Cantidad_Insumo = Cantidad_Insumo * M.Factor, Cantidad_Servicio = Cantidad_Servicio * M.Factor 
	From #tmp_InformacionDispensacion_Detales E (NoLock) 
	Inner Join  #vw_Claves_Precios_Asignados M (NoLock) On ( E.ClaveSSA = M.ClaveSSA and M.Status = 'A' ) 
	
	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   

	--select EsConsignacion, sum(Cantidad_Insumo) from #tmp_InformacionDispensacion_Detales  group by EsConsignacion 



	----select EsConsignacion, sum(Cantidad_Insumo) from #tmp_InformacionDispensacion_Detales where  ClaveSSA in ( '010.000.1311.00' )  group by EsConsignacion 
	----select * from #tmp_InformacionDispensacion_Detales where  ClaveSSA in ( '010.000.1311.00' )  order by Cantidad_Insumo desc 

	----return 
	------------SALIDA 


	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   



	Select 
		Identity(int, 1, 1) as Secuecial, 
		IdEmpresa, IdEstado, IdCliente, IdSubCliente, 
		cast(ClaveSSA as varchar(100)) as ClaveSSA, ClaveSSA as ClaveSSA_Base, 
		cast('' as varchar(max)) as DescripcionClave, 
		EsConsignacion, 
		0 as Relacionada, 
		cast(0 as numeric(14,4)) as ContenidoPaquete, cast(0 as numeric(14,4)) as ContenidoPaquete_Licitado,  

		cast(0 as numeric(14,4)) as Precio_Producto, 
		cast(TasaIva as numeric(14,4)) as Precio_Producto_TasaIva, 
		@Precio_Servicio as Precio_Servicio, 
		@Precio_Servicio_IVA as Precio_Servicio_TasaIva, 

		sum(Cantidad_Insumo) as Cantidad_Insumo, 
		sum(Cantidad_Servicio) as Cantidad_Servicio, 
		sum(Cantidad_Insumo) as Cantidad_Insumo_Total, 
		sum(Cantidad_Servicio) as Cantidad_Servicio_Total, 

		cast(0 as numeric(14,4)) as Cantidad_x_Comprobar_Doctos, 
		cast(0 as numeric(14,4)) as Cantidad_x_Comprobar_Facturas__Producto,    
		cast(0 as numeric(14,4)) as Cantidad_x_Comprobar_Facturas__Servicio 

		, cast(0 as numeric(14,4)) as Producto_SubTotal_General 
		, cast(0 as numeric(14,4)) as Producto_IVA_General 
		, cast(0 as numeric(14,4)) as Producto_Total_General 

		, cast(0 as numeric(14,4)) as Servicio_SubTotal_General 
		, cast(0 as numeric(14,4)) as Servicio_IVA_General 
		, cast(0 as numeric(14,4)) as Servicio_Total_General 

		, 1 as Tipo 
	Into #tmp_InformacionDispensacion  
	from #tmp_InformacionDispensacion_Detales 
	--Where 1 = 0 
	--where ClaveSSA = '010.000.2119.00' 
	Group by 
		IdEmpresa, IdEstado, IdCliente, IdSubCliente, 
		ClaveSSA, --ClaveSSA_Base, 
		EsConsignacion, 
		TasaIva 
	Order by 
		ClaveSSA, EsConsignacion 

	Update E Set ClaveSSA = M.ClaveSSA, Relacionada = 0  
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join  #vw_Relacion_ClavesSSA_Claves M (NoLock) On ( E.ClaveSSA_Base = M.ClaveSSA_Relacionada and M.Status = 'A' )  

	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   	


	--select * from #tmp_InformacionDispensacion  

	----------------- Agregar las claves no dispensadas en el periodo solicitado 
	Insert Into #tmp_InformacionDispensacion
	( 
		IdEmpresa, IdEstado, IdCliente, IdSubCliente, 
		ClaveSSA, ClaveSSA_Base, DescripcionClave, EsConsignacion, Relacionada, 
		ContenidoPaquete, ContenidoPaquete_Licitado, 
		Precio_Producto, Precio_Producto_TasaIva, Precio_Servicio, Precio_Servicio_TasaIva,
		Cantidad_Insumo, Cantidad_Servicio, Cantidad_Insumo_Total, Cantidad_Servicio_Total, 
		Cantidad_x_Comprobar_Doctos, Cantidad_x_Comprobar_Facturas__Producto, Cantidad_x_Comprobar_Facturas__Servicio, 
		Producto_SubTotal_General, Producto_IVA_General, Producto_Total_General, 
		Servicio_SubTotal_General, Servicio_IVA_General, Servicio_Total_General, 
		Tipo 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdCliente, @IdSubCliente, 
		ClaveSSA, ClaveSSA as ClaveSSA_Base, 
		cast('' as varchar(max)) as DescripcionClave, 
		1 as EsConsignacion, 
		0 as Relacionada, 
		cast(0 as numeric(14,4)) as ContenidoPaquete, cast(0 as numeric(14,4)) as ContenidoPaquete_Licitado,  

		cast(0 as numeric(14,4)) as Precio_Producto, 
		cast(0 as numeric(14,4)) as Precio_Producto_TasaIva, 
		@Precio_Servicio as Precio_Servicio, 
		@Precio_Servicio_IVA as Precio_Servicio_TasaIva, 

		cast(0 as numeric(14,4)) as Cantidad_Insumo, 
		cast(0 as numeric(14,4)) as Cantidad_Servicio, 
		cast(0 as numeric(14,4)) as Cantidad_Insumo_Total, 
		cast(0 as numeric(14,4)) as Cantidad_Servicio_Total, 

		cast(Cantidad_x_Comprobar as numeric(14,4)) as Cantidad_x_Comprobar_Doctos, 
		cast(0 as numeric(14,4)) as Cantidad_x_Comprobar_Facturas__Producto,    
		cast(0 as numeric(14,4)) as Cantidad_x_Comprobar_Facturas__Servicio 

		, cast(0 as numeric(14,4)) as Producto_SubTotal_General 
		, cast(0 as numeric(14,4)) as Producto_IVA_General 
		, cast(0 as numeric(14,4)) as Producto_Total_General 

		, cast(0 as numeric(14,4)) as Servicio_SubTotal_General 
		, cast(0 as numeric(14,4)) as Servicio_IVA_General 
		, cast(0 as numeric(14,4)) as Servicio_Total_General 
		, 2 as Tipo 
	From #tmp_Informacion__Comprobacion_Documentos D 
	Where Not Exists ( select * from #tmp_InformacionDispensacion C Where D.ClaveSSA = C.ClaveSSA and C.EsConsignacion = 1 ) 

	Insert Into #tmp_InformacionDispensacion
	( 
		IdEmpresa, IdEstado, IdCliente, IdSubCliente, 
		ClaveSSA, ClaveSSA_Base, DescripcionClave, EsConsignacion, Relacionada, 
		ContenidoPaquete, ContenidoPaquete_Licitado, 
		Precio_Producto, Precio_Producto_TasaIva, Precio_Servicio, Precio_Servicio_TasaIva,
		Cantidad_Insumo, Cantidad_Servicio, Cantidad_Insumo_Total, Cantidad_Servicio_Total, 
		Cantidad_x_Comprobar_Doctos, Cantidad_x_Comprobar_Facturas__Producto, Cantidad_x_Comprobar_Facturas__Servicio, 
		Producto_SubTotal_General, Producto_IVA_General, Producto_Total_General, 
		Servicio_SubTotal_General, Servicio_IVA_General, Servicio_Total_General, 
		Tipo 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdCliente, @IdSubCliente, 
		ClaveSSA, ClaveSSA as ClaveSSA_Base, 
		cast('' as varchar(max)) as DescripcionClave, 
		0 as EsConsignacion, 
		0 as Relacionada, 
		cast(0 as numeric(14,4)) as ContenidoPaquete, cast(0 as numeric(14,4)) as ContenidoPaquete_Licitado,  

		cast(0 as numeric(14,4)) as Precio_Producto, 
		cast(0 as numeric(14,4)) as Precio_Producto_TasaIva, 
		@Precio_Servicio as Precio_Servicio, 
		@Precio_Servicio_IVA as Precio_Servicio_TasaIva, 

		cast(0 as numeric(14,4)) as Cantidad_Insumo, 
		cast(0 as numeric(14,4)) as Cantidad_Servicio, 
		cast(0 as numeric(14,4)) as Cantidad_Insumo_Total, 
		cast(0 as numeric(14,4)) as Cantidad_Servicio_Total, 

		cast(0 as numeric(14,4)) as Cantidad_x_Comprobar_Doctos, 
		cast((case when TipoDocumento = 1 then Cantidad_x_Comprobar else 0 end) as numeric(14,4)) as Cantidad_x_Comprobar_Facturas__Producto,    
		cast((case when TipoDocumento = 2 then Cantidad_x_Comprobar else 0 end) as numeric(14,4)) as Cantidad_x_Comprobar_Facturas__Servicio 

		, cast(0 as numeric(14,4)) as Producto_SubTotal_General 
		, cast(0 as numeric(14,4)) as Producto_IVA_General 
		, cast(0 as numeric(14,4)) as Producto_Total_General 

		, cast(0 as numeric(14,4)) as Servicio_SubTotal_General 
		, cast(0 as numeric(14,4)) as Servicio_IVA_General 
		, cast(0 as numeric(14,4)) as Servicio_Total_General 
		, 3 as Tipo 
	From #tmp_Informacion__Comprobacion_Facturas D 
	Where Not Exists ( select * from #tmp_InformacionDispensacion C Where ( D.ClaveSSA = C.ClaveSSA or D.ClaveSSA = C.ClaveSSA_Base ) and C.EsConsignacion = 0  ) 
	----------------- Agregar las claves no dispensadas en el periodo solicitado 


	--select 
	--	sum(Cantidad_x_Comprobar_Doctos) as Cantidad_x_Comprobar_Doctos, 
	--	sum(Cantidad_x_Comprobar_Facturas__Producto) as Cantidad_x_Comprobar_Facturas__Producto, 
	--	sum(Cantidad_x_Comprobar_Facturas__Servicio) as Cantidad_x_Comprobar_Facturas__Servicio 
	--from #tmp_InformacionDispensacion 

	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   

	Update L Set Cantidad_x_Comprobar_Doctos = D.Cantidad_x_Comprobar 
	From #tmp_InformacionDispensacion L 
	Inner Join #tmp_Informacion__Comprobacion_Documentos D (NoLock) On ( L.ClaveSSA = D.ClaveSSA ) 
	Where EsConsignacion = 1 and Tipo = 1 


	Update L Set Cantidad_x_Comprobar_Facturas__Producto = D.Cantidad_x_Comprobar 
	From #tmp_InformacionDispensacion L 
	Inner Join #tmp_Informacion__Comprobacion_Facturas D (NoLock) On ( L.ClaveSSA = D.ClaveSSA ) 
	Where EsConsignacion = 0 and D.TipoDocumento = 1 and Tipo = 1 


	Update L Set Cantidad_x_Comprobar_Facturas__Servicio = D.Cantidad_x_Comprobar 
	From #tmp_InformacionDispensacion L 
	Inner Join #tmp_Informacion__Comprobacion_Facturas D (NoLock) On ( L.ClaveSSA = D.ClaveSSA ) 
	Where EsConsignacion = 0 and D.TipoDocumento = 2 and Tipo = 1 


	----Select Tipo, sum(Cantidad_x_Comprobar_Doctos) as tmp_InformacionDispensacion
	----from #tmp_InformacionDispensacion 
	----group by tipo, EsConsignacion  
	--------------------------------------- DISPENSACION   
	------------------------------------------------------------------------------------- OBTENER INFORMACION 

	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   


	--select * from #tmp_InformacionDispensacion --where ClaveSSA like '%060%300%' 
	--order by Secuecial 

	--select * From #tmp_InformacionDispensacion E (NoLock) 

	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 
	Update E Set ClaveSSA = M.ClaveSSA, Relacionada = 1  
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join  #vw_Relacion_ClavesSSA_Claves M (NoLock) On ( E.ClaveSSA_Base = M.ClaveSSA_Relacionada and M.Status = 'A' ) 

	Update E Set DescripcionClave = C.DescripcionClave 
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join vw_ClavesSSA_Sales C (NoLock) On ( E.ClaveSSA = C.ClaveSSA ) 

	Update E Set 
		ContenidoPaquete_Licitado = M.ContenidoPaquete_Licitado, 
		--Precio_Producto = M.Precio_Licitacion 
		Precio_Producto = (case when M.ContenidoPaquete_Licitado = 1 then M.Precio_Licitacion else M.PrecioUnitario end),  
		Precio_Servicio = (case when M.ContenidoPaquete_Licitado = 1 then @Precio_Servicio else round((@Precio_Servicio / M.ContenidoPaquete_Licitado),2) end)
	From #tmp_InformacionDispensacion E (NoLock) 
	Inner Join  #vw_Claves_Precios_Asignados M (NoLock) On ( E.ClaveSSA = M.ClaveSSA and M.Status = 'A' ) 

	----select 'PRECIOS XT' as PreciosXT, * 
	----From #tmp_InformacionDispensacion E (NoLock) 
	----Inner Join  #vw_Claves_Precios_Asignados M (NoLock) On ( E.ClaveSSA = M.ClaveSSA and M.Status = 'A' ) 

	----select * From #tmp_InformacionDispensacion E (NoLock) 
	----Select * From #vw_Claves_Precios_Asignados M (NoLock) 


	Update E Set Precio_Producto = 0 
	From #tmp_InformacionDispensacion E (NoLock) 
	Where EsConsignacion = 1  

	Update E Set 
		Cantidad_Insumo = Cantidad_Insumo / ContenidoPaquete_Licitado, 
		Cantidad_Insumo_Total = Cantidad_Insumo / ContenidoPaquete_Licitado, 
		Cantidad_Servicio = Cantidad_Servicio /  ContenidoPaquete_Licitado,  
		Cantidad_Servicio_Total = Cantidad_Servicio /  ContenidoPaquete_Licitado 
	From #tmp_InformacionDispensacion E (NoLock) 
	Where ContenidoPaquete_Licitado > 0 
	--------------------------------------------- COMPLETAR EL REGISTRO DE INFORMACION 

	--select * from #tmp_InformacionDispensacion where ClaveSSA like '%060%300%' 


	--	sum(Cantidad_Insumo) as Cantidad_Insumo_Total, 
	--	sum(Cantidad_Servicio) as Cantidad_Servicio_Total, 


	--------------------------------------------- CALCULAR IMPORTES  
	Update E Set Producto_SubTotal_General = Cantidad_Insumo * Precio_Producto 
	From #tmp_InformacionDispensacion E (NoLock) 
	Where EsConsignacion = 0 

	Update E Set Producto_IVA_General = Producto_SubTotal_General * ( Precio_Producto_TasaIva / 100.00 ) 
	From #tmp_InformacionDispensacion E (NoLock) 
	Where EsConsignacion = 0 


	Update E Set Servicio_SubTotal_General = Cantidad_Servicio_Total * Precio_Servicio 
	From #tmp_InformacionDispensacion E (NoLock) 

	Update E Set Servicio_IVA_General = Servicio_SubTotal_General * ( Precio_Servicio_TasaIva / 100.00 )  
	From #tmp_InformacionDispensacion E (NoLock) 
	--------------------------------------------- CALCULAR IMPORTES  


	--------- SALIDA 
	--select * from #tmp_InformacionDispensacion order by Secuecial 


	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   

	 
	-------------------------------------- Agregar información de catalogo 
	-------------------------------------- Agregar información de catalogo 


	--where Precio_Producto > 0 


	------------------------------- SALIDA FINAL  
	Select 
		@IdEmpresa as IdEmpresa, cast('' as varchar(500)) as Empresa, 
		@IdEstado as IdEstado, cast('' as varchar(500)) as Estado, 
		@sTipoDeUnidades As TiposDeUnidades, 
		@FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 
		getdate() as FechaImpresion, 

		ClaveSSA, DescripcionClave, 
		--Precio_Producto, Precio_Producto_TasaIva, 
		--Precio_Servicio, Precio_Servicio_TasaIva, 
		sum(Cantidad_Insumo__Venta) as Cantidad_Insumo__Venta, 
		sum(Cantidad_Insumo__Consignacion) as Cantidad_Insumo__Consignacion, 

		sum(Cantidad_Insumo) as Cantidad_Insumo, 
		sum(Producto_SubTotal_General) as Producto_SubTotal_General, 
		sum(Producto_IVA_General) as Producto_IVA_General, 
		sum(Producto_Total_General) as Producto_Total_General,  
		sum(Servicio_SubTotal_General) as Servicio_SubTotal_General, 
		sum(Servicio_IVA_General) as Servicio_IVA_General, 
		sum(Servicio_Total_General) as Servicio_Total_General,  


		sum(Cantidad_Producto__Venta) as Cantidad_Producto__Venta,  
		sum(Producto_SubTotal__Venta) as Producto_SubTotal__Venta, 
		sum(Producto_IVA__Venta) as Producto_IVA__Venta, 
		sum(Producto_Total__Venta) as Producto_Total__Venta, 


		sum(Cantidad_Servicio) as Cantidad_Servicio,  
		sum(Cantidad_Servicio__Venta) as Cantidad_Servicio__Venta, 
		sum(Servicio_SubTotal__Venta) as Servicio_SubTotal__Venta, 
		sum(Servicio_IVA__Venta) as Servicio_IVA__Venta, 
		sum(Servicio_Total__Venta) as Servicio_Total__Venta, 

		sum(Cantidad_Servicio__Consignacion) as Cantidad_Servicio__Consignacion, 
		sum(Servicio_SubTotal__Consignacion) as Servicio_SubTotal__Consignacion, 
		sum(Servicio_IVA__Consignacion) as Servicio_IVA__Consignacion, 
		sum(Servicio_Total__Consignacion) as Servicio_Total__Consignacion, 

			--(case when EsConsignacion = 1 then Cantidad_Servicio else 0 end) as Cantidad_Servicio__Consignacion, 
			--cast( (case when EsConsignacion = 1 then ( Cantidad_Servicio * Precio_Servicio ) else 0 end) as numeric(14,4)) as Servicio_SubTotal__Consignacion, 
			--cast( (case when EsConsignacion = 1 then ( Cantidad_Servicio * Precio_Servicio ) * ( Precio_Servicio_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Servicio_SubTotal__Consignacion, 
			--cast( (case when EsConsignacion = 1 then ( Cantidad_Servicio * Precio_Servicio ) * ( 1 + (Precio_Servicio_TasaIva / 100.00) )else 0 end) as numeric(14,4)) as Servicio_SubTotal__Consignacion, 




		------------------- DOCUMENTOS 
		sum(Cantidad_x_Comprobar_Doctos) as Cantidad_x_Comprobar_Doctos,  
		sum(Servicio_SubTotal_Comprobar_Documentos) as Servicio_SubTotal_Comprobar_Documentos,  
		sum(Servicio_IVA_Comprobar_Documentos) as Servicio_IVA_Comprobar_Documentos,  
		sum(Servicio_Total_Comprobar_Documentos) as Servicio_Total_Comprobar_Documentos,  

		sum(Cantidad_Parcial_x_Comprobar_Documentos) as Cantidad_Parcial_x_Comprobar_Documentos,  
		sum(Servicio_Parcial_SubTotal_Comprobar_Documentos) as Servicio_Parcial_SubTotal_Comprobar_Documentos,  
		sum(Servicio_Parcial_IVA_Comprobar_Documentos) as Servicio_Parcial_IVA_Comprobar_Documentos,  
		sum(Servicio_Parcial_Total_Comprobar_Documentos) as Servicio_Parcial_Total_Comprobar_Documentos,  
		------------------- DOCUMENTOS 


		------------------- FACTURAS PRODUCTO  
		sum(Cantidad_x_Comprobar_Facturas__Producto) as Cantidad_x_Comprobar_Facturas__Producto,  
		sum(Producto_SubTotal_Comprobar_Facturas) as Producto_SubTotal_Comprobar_Facturas,  
		sum(Producto_IVA_Comprobar_Facturas) as Producto_IVA_Comprobar_Facturas,  
		sum(Producto_Total_Comprobar_Facturas) as Producto_Total_Comprobar_Facturas,  
		
		sum(Cantidad_Parcial_x_Comprobar_Facturas__Producto) as Cantidad_Parcial_x_Comprobar_Facturas__Producto,  
		sum(Producto_Parcial_SubTotal_Comprobar_Facturas) as Producto_Parcial_SubTotal_Comprobar_Facturas,  
		sum(Producto_Parcial_IVA_Comprobar_Facturas) as Producto_Parcial_IVA_Comprobar_Facturas,  
		sum(Producto_Parcial_Total_Comprobar_Facturas) as Producto_Parcial_Total_Comprobar_Facturas,  
		------------------- FACTURAS PRODUCTO  


		------------------- FACTURAS SERVICIO  
		sum(Cantidad_x_Comprobar_Facturas__Servicio) as Cantidad_x_Comprobar_Facturas__Servicio,  
		sum(Servicio_SubTotal_Comprobar_Facturas) as Servicio_SubTotal_Comprobar_Facturas,  
		sum(Servicio_IVA_Comprobar_Facturas) as Servicio_IVA_Comprobar_Facturas,  
		sum(Servicio_Total_Comprobar_Facturas) as Servicio_Total_Comprobar_Facturas,  

		sum(Cantidad_Parcial_x_Comprobar_Facturas__Servicio) as Cantidad_Parcial_x_Comprobar_Facturas__Servicio,  
		sum(Servicio_Parcial_SubTotal_Comprobar_Facturas) as Servicio_Parcial_SubTotal_Comprobar_Facturas,  
		sum(Servicio_Parcial_IVA_Comprobar_Facturas) as Servicio_Parcial_IVA_Comprobar_Facturas,  
		sum(Servicio_Parcial_Total_Comprobar_Facturas) as Servicio_Parcial_Total_Comprobar_Facturas 
		------------------- FACTURAS SERVICIO  




		-- 0 as X 
	Into #tmp_SalidaFinal 	
	From 
	( 
		Select 
			--, @Procesar_x_Clave = 1 

			'' as ClaveSSA, '' as DescripcionClave, 
			--(case when @Procesar_x_Clave = 1 then ClaveSSA else '' end) as ClaveSSA,  
			--(case when @Procesar_x_Clave = 1 then DescripcionClave else '' end) as DescripcionClave, 

			Precio_Producto, Precio_Producto_TasaIva, 
			Precio_Servicio, Precio_Servicio_TasaIva, 

			Cantidad_Insumo, 
			Cantidad_Servicio, 
	
			Producto_SubTotal_General, Producto_IVA_General, Producto_Total_General,  
			Servicio_SubTotal_General, Servicio_IVA_General, Servicio_Total_General, 	

			(case when EsConsignacion = 0 then Cantidad_Insumo else 0 end) as Cantidad_Insumo__Venta, 
			(case when EsConsignacion = 1 then Cantidad_Insumo else 0 end) as Cantidad_Insumo__Consignacion, 


			(case when EsConsignacion = 0 then Cantidad_Insumo else 0 end) as Cantidad_Producto__Venta, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_Insumo * Precio_Producto ) else 0 end) as numeric(14,4)) as Producto_SubTotal__Venta, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_Insumo * Precio_Producto ) * ( Precio_Producto_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Producto_IVA__Venta, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_Insumo * Precio_Producto ) * ( 1 + (Precio_Producto_TasaIva / 100.00) )else 0 end) as numeric(14,4)) as Producto_Total__Venta, 


			(case when EsConsignacion = 0 then Cantidad_Servicio else 0 end) as Cantidad_Servicio__Venta, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_Servicio * Precio_Servicio ) else 0 end) as numeric(14,4)) as Servicio_SubTotal__Venta, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_Servicio * Precio_Servicio ) * ( Precio_Servicio_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Servicio_IVA__Venta, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_Servicio * Precio_Servicio ) * ( 1 + (Precio_Servicio_TasaIva / 100.00) )else 0 end) as numeric(14,4)) as Servicio_Total__Venta, 

			(case when EsConsignacion = 1 then Cantidad_Servicio else 0 end) as Cantidad_Servicio__Consignacion, 
			cast( (case when EsConsignacion = 1 then ( Cantidad_Servicio * Precio_Servicio ) else 0 end) as numeric(14,4)) as Servicio_SubTotal__Consignacion, 
			cast( (case when EsConsignacion = 1 then ( Cantidad_Servicio * Precio_Servicio ) * ( Precio_Servicio_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Servicio_IVA__Consignacion, 
			cast( (case when EsConsignacion = 1 then ( Cantidad_Servicio * Precio_Servicio ) * ( 1 + (Precio_Servicio_TasaIva / 100.00) )else 0 end) as numeric(14,4)) as Servicio_Total__Consignacion, 

			(case when EsConsignacion = 1 then Cantidad_x_Comprobar_Doctos else 0 end) as Cantidad_x_Comprobar_Doctos, 
			cast( (case when EsConsignacion = 1 then ( Cantidad_x_Comprobar_Doctos * Precio_Servicio ) else 0 end) as numeric(14,4)) as Servicio_SubTotal_Comprobar_Documentos, 
			cast( (case when EsConsignacion = 1 then ( Cantidad_x_Comprobar_Doctos * Precio_Servicio ) * ( Precio_Servicio_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Servicio_IVA_Comprobar_Documentos, 
			cast( (case when EsConsignacion = 1 then ( Cantidad_x_Comprobar_Doctos * Precio_Servicio ) * ( 1 + (Precio_Servicio_TasaIva / 100.00) ) else 0 end) as numeric(14,4)) as Servicio_Total_Comprobar_Documentos, 

			(case when ( EsConsignacion = 1 and Cantidad_Insumo <= Cantidad_x_Comprobar_Doctos ) then Cantidad_Insumo else 0 end) as Cantidad_Parcial_x_Comprobar_Documentos, 
			cast( (case when ( EsConsignacion = 1 and Cantidad_Insumo <= Cantidad_x_Comprobar_Doctos ) then ( Cantidad_Insumo * Precio_Servicio ) else 0 end) as numeric(14,4)) as Servicio_Parcial_SubTotal_Comprobar_Documentos, 
			cast( (case when ( EsConsignacion = 1 and Cantidad_Insumo <= Cantidad_x_Comprobar_Doctos ) then ( Cantidad_Insumo * Precio_Servicio ) * ( Precio_Servicio_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Servicio_Parcial_IVA_Comprobar_Documentos, 
			cast( (case when ( EsConsignacion = 1 and Cantidad_Insumo <= Cantidad_x_Comprobar_Doctos ) then ( Cantidad_Insumo * Precio_Servicio ) * ( 1 + (Precio_Servicio_TasaIva / 100.00) ) else 0 end) as numeric(14,4)) as Servicio_Parcial_Total_Comprobar_Documentos, 



			(case when EsConsignacion = 0 then Cantidad_x_Comprobar_Facturas__Producto else 0 end) as Cantidad_x_Comprobar_Facturas__Producto, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_x_Comprobar_Facturas__Producto * Precio_Producto ) else 0 end) as numeric(14,4)) as Producto_SubTotal_Comprobar_Facturas, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_x_Comprobar_Facturas__Producto * Precio_Producto ) * ( Precio_Producto_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Producto_IVA_Comprobar_Facturas, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_x_Comprobar_Facturas__Producto * Precio_Producto ) * ( 1 + (Precio_Producto_TasaIva / 100.00) ) else 0 end) as numeric(14,4)) as Producto_Total_Comprobar_Facturas,  

			(case when ( EsConsignacion = 0 and Cantidad_Insumo <= Cantidad_x_Comprobar_Facturas__Producto ) then Cantidad_Insumo else 0 end) as Cantidad_Parcial_x_Comprobar_Facturas__Producto, 
			cast( (case when ( EsConsignacion = 0 and Cantidad_Insumo <= Cantidad_x_Comprobar_Facturas__Producto ) then ( Cantidad_Insumo * Precio_Producto ) else 0 end) as numeric(14,4)) as Producto_Parcial_SubTotal_Comprobar_Facturas, 
			cast( (case when ( EsConsignacion = 0 and Cantidad_Insumo <= Cantidad_x_Comprobar_Facturas__Producto ) then ( Cantidad_Insumo * Precio_Producto ) * ( Precio_Producto_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Producto_Parcial_IVA_Comprobar_Facturas, 
			cast( (case when ( EsConsignacion = 0 and Cantidad_Insumo <= Cantidad_x_Comprobar_Facturas__Producto ) then ( Cantidad_Insumo * Precio_Producto ) * ( 1 + (Precio_Producto_TasaIva / 100.00) ) else 0 end) as numeric(14,4)) as Producto_Parcial_Total_Comprobar_Facturas,  



			(case when EsConsignacion = 0 then Cantidad_x_Comprobar_Facturas__Servicio else 0 end) as Cantidad_x_Comprobar_Facturas__Servicio,  
			cast( (case when EsConsignacion = 0 then ( Cantidad_x_Comprobar_Facturas__Servicio * Precio_Servicio ) else 0 end) as numeric(14,4)) as Servicio_SubTotal_Comprobar_Facturas, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_x_Comprobar_Facturas__Servicio * Precio_Servicio ) * ( Precio_Servicio_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Servicio_IVA_Comprobar_Facturas, 
			cast( (case when EsConsignacion = 0 then ( Cantidad_x_Comprobar_Facturas__Servicio * Precio_Servicio ) * ( 1 + (Precio_Servicio_TasaIva / 100.00) ) else 0 end) as numeric(14,4)) as Servicio_Total_Comprobar_Facturas,  

			(case when ( EsConsignacion = 0 and Cantidad_Servicio <= Cantidad_x_Comprobar_Facturas__Servicio ) then Cantidad_Servicio else 0 end) as Cantidad_Parcial_x_Comprobar_Facturas__Servicio, 
			cast( (case when ( EsConsignacion = 0 and Cantidad_Servicio <= Cantidad_x_Comprobar_Facturas__Servicio ) then ( Cantidad_Servicio * Precio_Servicio_TasaIva ) else 0 end) as numeric(14,4)) as Servicio_Parcial_SubTotal_Comprobar_Facturas, 
			cast( (case when ( EsConsignacion = 0 and Cantidad_Servicio <= Cantidad_x_Comprobar_Facturas__Servicio ) then ( Cantidad_Servicio * Precio_Servicio_TasaIva ) * ( Precio_Servicio_TasaIva / 100.00 ) else 0 end) as numeric(14,4)) as Servicio_Parcial_IVA_Comprobar_Facturas, 
			cast( (case when ( EsConsignacion = 0 and Cantidad_Servicio <= Cantidad_x_Comprobar_Facturas__Servicio ) then ( Cantidad_Servicio * Precio_Servicio_TasaIva ) * ( 1 + (Precio_Servicio_TasaIva / 100.00) ) else 0 end) as numeric(14,4)) as Servicio_Parcial_Total_Comprobar_Facturas   



		From #tmp_InformacionDispensacion 
	) C 
	Group by 
		ClaveSSA, DescripcionClave  
		--Precio_Producto, Precio_Producto_TasaIva, 
		--Precio_Servicio, Precio_Servicio_TasaIva 


	------------------------------- SALIDA FINAL  
	------ Quitar caracteres especiales 

	--select * from #tmp_InformacionDispensacion
	
	--		Exec spp_FormatearTabla #tmp_InformacionDispensacion  

	Update P Set Empresa = E.Nombre 
	From #tmp_SalidaFinal P 
	Inner join CatEmpresas E (NoLock) On ( P.IdEmpresa = E.IdEmpresa ) 

	Update P Set Estado = E.Nombre 
	From #tmp_SalidaFinal P 
	Inner join CatEstados E (NoLock) On ( P.IdEstado = E.IdEstado ) 


	------------------ REVISION DE PROCESO 
	----if ( @TablaProceso <> '' ) 
	----Begin 
	----	Set @sSql = 'if exists ( select * from sysobjects  where name = ' + char(39) + @TablaProceso + char(39) + ' and xtype = ' + char(39) + 'u' + char(39) + ' ) drop Table ' + @TablaProceso 
	----	Exec(@sSql) 
	----	--PRINT @sSql

	----	Set @sSql = 
	----	'Select ' + char(13) + 
	----	'	* ' + char(13) + 
	----		'Into ' + @TablaProceso + char(13) + 
	----	'from #tmp_SalidaFinal ' + char(13)  
		
	----	if ( @Procesar_x_Clave = 0 ) 
	----	Begin 
	----		Set @sSql = 
	----		'Select ' + char(13) + 
	----		'	' + char(39) + @FechaInicial  + char(39) + ' as FechaInicial, '  + char(39) + @FechaFinal  + char(39) + ' as FechaFinal, ' + char(13) + 
	----		'	* ' + char(13) + 
	----		'Into ' + @TablaProceso + char(13) + 
	----		'from #tmp_InformacionDispensacion ' + char(13)  
	----	End 
			
	----	Exec(@sSql) 
	----	PRINT @sSql


	----End 
	------------------ REVISION DE PROCESO 

	------------------------------- SALIDA FINAL  
	------------------------- 
	select * 
	from #tmp_SalidaFinal 




	------------------------------- SALIDA FINAL  



	--Select * from #tmp_InformacionDispensacion Order by Secuecial 


	--		spp_FACT_Rpt_OP_00_Pronostico_A_Remisionar   


End 
Go--#SQL  

