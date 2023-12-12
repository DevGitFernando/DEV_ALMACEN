------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_RTP__ValidarPendienteRemisionar' and xType = 'P' ) 
   Drop Proc spp_FACT_RTP__ValidarPendienteRemisionar 
Go--#SQL 

Create Proc spp_FACT_RTP__ValidarPendienteRemisionar 
( 
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '', 
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 
	@IdCliente varchar(4) = '42', @IdSubCliente varchar(4) = '', 
	@IdPrograma varchar(4) = '', @IdSubPrograma varchar(4) = '', 

	@TipoDeUnidad int = 1,  
    ---- Todas = 0, 
    ---- Farmacias = 1, 
    ---- FarmaciasUnidosis = 2, 
    ---- Almacenes = 3, 
    ---- AlmacenesUnidosis = 4 
	@IdFarmacia varchar(4) = '', 

	@Validar_Producto int = 1, @Validar_Servicio int = 1, 
	@Validar_Venta int = 1, @Validar_Consigna int = 1, 
	@FechaInicial varchar(10) = '2022-03-01', @FechaFinal varchar(10) = '2023-03-31',  
	--, @Codigo varchar(50) = '', 
	@ClaveSSA varchar(50) = '' 
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


	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
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
	If ( @IdFuenteFinanciamiento <> '' or @IdFinanciamiento <> '' ) 
	Begin 
		If ( @IdFuenteFinanciamiento <> '' and @IdFinanciamiento <> '' ) 
			Begin 
				Set @sFiltro = 'Where IdFuenteFinanciamiento = ' + char(39) + dbo.fg_FormatearCadena(@IdFuenteFinanciamiento, '0', 4) + char(39) + ' and IdFinanciamiento = ' + char(39) + dbo.fg_FormatearCadena(@IdFinanciamiento, '0', 4) + char(39)   
			End 
		Else 
			Begin 
			If ( @IdFuenteFinanciamiento <> '' ) 
				Begin 
					Set @sFiltro = 'Where IdFuenteFinanciamiento = ' + char(39) + dbo.fg_FormatearCadena(@IdFuenteFinanciamiento, '0', 4) + char(39)     
				End 
			End 

		--		spp_FACT_RTP__ValidarPendienteRemisionar 
	End 

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

		--		spp_FACT_RTP__ValidarPendienteRemisionar 
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
	If ( @IdPrograma <> '' or @IdSubPrograma <> '' ) 
	Begin 
		If ( @IdPrograma <> '' and @IdSubPrograma <> '' ) 
			Begin 
				Set @sFiltro = 'Where IdPrograma = ' + char(39) + dbo.fg_FormatearCadena(@IdPrograma, '0', 4) + char(39) + ' and IdSubPrograma = ' + char(39) + dbo.fg_FormatearCadena(@IdSubPrograma, '0', 4) + char(39)   
			End 
		Else 
			Begin 
			If ( @IdPrograma <> '' ) 
				Begin 
					Set @sFiltro = 'Where IdPrograma = ' + char(39) + dbo.fg_FormatearCadena(@IdPrograma, '0', 4) + char(39)     
				End 
			End 

		--		spp_FACT_RTP__ValidarPendienteRemisionar 
	End 

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

		--		spp_FACT_RTP__ValidarPendienteRemisionar 
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
	Where ClaveSSA like '%' + replace(@ClaveSSA, ' ', '%') + '%' 

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


	--		spp_FACT_RTP__ValidarPendienteRemisionar   


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
	------------------------------------------------------------------------------------- OBTENER INFORMACION 
	If exists ( select * from sysobjects (nolock) where name = '#tmp__PendienteRemisionar' and xType = 'U' ) Drop Table #tmp__PendienteRemisionar 

	select 
		--top 10 
		 E.IdEmpresa, 
		 E.IdEstado, 
		 E.IdFarmacia, LF.Farmacia, 
		 
		 --E.IdCliente, LC.Cliente, E.IdSubCliente, LC.SubCliente, 
		 --E.IdPrograma, LP.Programa, E.IdSubPrograma, LP.SubPrograma, 

		 E.IdCliente, E.IdSubCliente, 
		 E.IdPrograma, E.IdSubPrograma,  

		 ----E.IdFarmacia, LF.Farmacia, 
		 ----cast('' as varchar(300)) as Farmacia, 
		 --E.IdCliente, cast('' as varchar(300)) as Cliente,  
		 --E.IdSubCliente, cast('' as varchar(300)) as SubCliente, 
		 --E.IdPrograma, cast('' as varchar(300)) as Programa, 
		 --E.IdSubPrograma, cast('' as varchar(300)) as SubPrograma, 	


		 E.FolioVenta, E.FechaRegistro, 
		 cast('' as varchar(20)) as IdBeneficiario_Principal,  cast('' as varchar(200)) as Beneficiario_Principal, 
		 cast('' as varchar(200)) as IdBeneficiario, 
		 cast('' as varchar(200)) as Beneficiario, 

		 (case when L.ClaveLote like '%*%' Then 1 else 0 end) as OrigenDeInsumo, 
		 cast('' as varchar(10)) as TipoDeClave, cast('' as varchar(500)) as TipoDeClaveDescripcion, 
		 0 as ClaveSSA_Asignada_a_Beneficiario, 
		 0 as ClaveSSA_Existe_En__CuadroBasico,  

		 0 as ClaveSSA_Existe_En__Producto,  
		 0 as ClaveSSA_Existe_En__Servicio,  

		 cast('' as varchar(50)) as ClaveSSA, 
		 cast('' as varchar(50)) as ClaveSSA_Base, 
		 cast('' as varchar(max)) as DescripcionClave, 
		 0 as EsClaveRelacionada, cast('' as varchar(20)) as ClaveRelacionada, 
		 0 as ContenidoPaquete, 0 as ContenidoPaquete_ClaveSSA, 0 as ContenidoPaquete_Licitado, cast(0 as numeric(14,4)) as Factor,  

		 L.IdProducto, L.CodigoEAN, cast('' as varchar(max)) as Descripcion, L.ClaveLote, L.Cant_Vendida, L.Cant_Devuelta, 
		 L.CantidadVendida, L.CantidadVendida as CantidadVendida_Licitacion, 
		 L.EnRemision_Insumo, L.EnRemision_Admon, L.RemisionFinalizada, 
		 L.CantidadRemision_Insumo, L.CantidadRemision_Admon, 	
		 cast(0 as numeric(14,4)) as PendienteRemisionarProducto, 
		 cast(0 as numeric(14,4)) as PendienteRemisionarServicio, 
		 0 as Producto_Pendiente, 
		 0 as Servicio_Pendiente, 
		 0 as Procesar 
		--P.ClaveSSA, P.DescripcionClave 
	Into #tmp__PendienteRemisionar   
	From VentasEnc E (NoLock) 
	Inner Join #tmp_ListaFarmacias LF (NoLock) On ( E.IdEstado = LF.IdEstado and E.IdFarmacia = LF.IdFarmacia ) 
	--Inner Join #tmp_ListaSubClientes LC (NoLock) On ( E.IdCliente = LC.IdCliente and E.IdSubCliente = LC.IdSubCliente ) 
	--Inner Join #tmp_ListaSubProgramas LP (NoLock) On ( E.IdPrograma = LP.IdPrograma and E.IdSubPrograma = LP.IdSubPrograma ) 
	----Inner Join VentasDet D (NoLock) 
	----	On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join VentasDet_Lotes L (NoLock) 
		--On ( D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.FolioVenta = L.FolioVenta ) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto  and L.CodigoEAN = P.CodigoEAN ) 	
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado 
		and ( convert(varchar(10), E.FechaRegistro, 120)  between @FechaInicial and @FechaFinal )  
		--and L.CodigoEAN like '%' + @Codigo + '%'
		--and ( (case when L.CantidadRemision_Admon < L.CantidadVendida then 1 else 0 end) = @Validar_Servicio ) 
		--and 1 = 0 
		--and ( L.CantidadVendida < L.CantidadRemision_Admon ) 

--			@Validar_Producto int = 0, @Validar_Servicio int = 0, 


	Update R Set Procesar = 1 
	From #tmp__PendienteRemisionar R 
	Where @Validar_Venta = 1 and OrigenDeInsumo = 0 
	
	Update R Set Procesar = 1 
	From #tmp__PendienteRemisionar R 
	Where @Validar_Consigna = 1 and OrigenDeInsumo = 1 

	Delete From #tmp__PendienteRemisionar Where Procesar = 0 


	--------------- Habilitar 
	--Update R Set Cliente = C.Cliente, SubCliente = C.SubCliente 
	--From #tmp__PendienteRemisionar R (NoLock) 
	--Inner Join #tmp_ListaSubClientes C (NoLock) On ( R.IdCliente = C.IdCliente and R.IdSubCliente = C.IdSubCliente ) 

	--Update R Set Programa = C.Programa, SubPrograma = C.SubPrograma 
	--From #tmp__PendienteRemisionar R (NoLock) 
	--Inner Join #tmp_ListaSubProgramas C (NoLock) On ( R.IdPrograma = C.IdPrograma and R.IdSubPrograma = C.IdSubPrograma ) 
	------------------------------------------------------------------------------------- OBTENER INFORMACION 


	--		spp_FACT_RTP__ValidarPendienteRemisionar   

	 
	-------------------------------------- Agregar información de catalogo 
	Update E Set 
		 Producto_Pendiente = ( case when @Validar_Producto = 1 then (case when CantidadVendida <> CantidadRemision_Insumo then 1 else 0 end) else 0 end), 
		 Servicio_Pendiente = ( case when @Validar_Servicio = 1 then (case when CantidadVendida <> CantidadRemision_Admon then 1 else 0 end) else 0 end)  
	from #tmp__PendienteRemisionar E (NoLock) 

	Update E Set PendienteRemisionarServicio = CantidadVendida - CantidadRemision_Admon 
	from #tmp__PendienteRemisionar E (NoLock) 

	Update E Set PendienteRemisionarProducto = CantidadVendida - CantidadRemision_Insumo 
	from #tmp__PendienteRemisionar E (NoLock) 
	Where OrigenDeInsumo = 0 


	Update E Set 
		ClaveSSA = P.ClaveSSA, ClaveSSA_Base = P.ClaveSSA, DescripcionClave = P.DescripcionClave, Descripcion = P.Descripcion, 
		TipoDeClave = P.TipoDeClave, TipoDeClaveDescripcion = P.TipoDeClaveDescripcion,
		ContenidoPaquete = P.ContenidoPaquete , ContenidoPaquete_ClaveSSA = P.ContenidoPaquete_ClaveSSA 
	from #tmp__PendienteRemisionar E (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( E.IdProducto = P.IdProducto  and E.CodigoEAN = P.CodigoEAN ) 	


	Update E Set IdBeneficiario = I.IdBeneficiario 
	from #tmp__PendienteRemisionar E (NoLock) 
	Inner Join VentasInformacionAdicional I (NoLock) On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 


	Update B Set EsClaveRelacionada = 1, ClaveRelacionada = P.ClaveSSA, ClaveSSA = P.ClaveSSA 
	From #tmp__PendienteRemisionar B (NoLock) 
	Inner Join #vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( B.IdEstado = P.IdEstado and B.IdCliente = P.IdCliente and B.IdSubCliente = P.IdSubCliente and B.ClaveSSA = P.ClaveSSA_Relacionada and P.Status = 'A' )  

	----select * 
	----From #tmp__PendienteRemisionar B (NoLock) 
	----Inner Join #vw_Relacion_ClavesSSA_Claves P (NoLock) 
	----	On ( B.IdEstado = P.IdEstado and B.IdCliente = P.IdCliente and B.IdSubCliente = P.IdSubCliente and B.ClaveSSA = P.ClaveSSA_Relacionada )  



	--delete from #tmp__PendienteRemisionar Where ClaveSSA not in ( '060.681.0067.00', '060.681.0067'  )   
	--select * from #tmp__PendienteRemisionar 

	--Select * from #vw_Claves_Precios_Asignados 

	Update B Set ClaveSSA_Existe_En__CuadroBasico = 1,  ContenidoPaquete_Licitado = P.ContenidoPaquete_Licitado, Factor = P.Factor 
	From #tmp__PendienteRemisionar B (NoLock) 
	Inner Join #vw_Claves_Precios_Asignados P (NoLock) 
		On ( B.IdEstado = P.IdEstado and B.IdCliente = P.IdCliente and B.IdSubCliente = P.IdSubCliente and B.ClaveSSA = P.ClaveSSA and P.Status = 'A' )  

	-- select * from #tmp_Lista_FF Where claveSSA = '010.000.1207.00' 

	Update B Set ClaveSSA_Existe_En__Producto = 1 
	From #tmp__PendienteRemisionar B (NoLock) 
	Inner Join #tmp_Lista_FF P (NoLock) On ( B.ClaveSSA = P.ClaveSSA and P.Tipo = 1 )  


	Update B Set ClaveSSA_Existe_En__Servicio = 1 
	From #tmp__PendienteRemisionar B (NoLock) 
	Inner Join #tmp_Lista_FF P (NoLock) On ( B.ClaveSSA = P.ClaveSSA and P.Tipo = 2 )  

	--		spp_FACT_RTP__ValidarPendienteRemisionar   

	----- Eliminar caracteres especiales 
	--Exec spp_FormatearTabla		#tmp__PendienteRemisionar
	-------------------------------------- Agregar información de catalogo 



		--		spp_FACT_RTP__ValidarPendienteRemisionar 

	----------------------------------------- Generar tablas de reporte 
	Select 
		--ClaveSSA_Asignada_a_Beneficiario, 
		--'Clave en Cuadro Básico' = ClaveSSA_Existe_En__CuadroBasico, 
		'Clave en listado licitado' = (case when ClaveSSA_Existe_En__Producto = 1 then 'SI' else 'NO' end), 
		'Clave en listado de servicio' = (case when ClaveSSA_Existe_En__Servicio = 1 then 'SI' else 'NO' end), 
		'Tipo de clave' = TipoDeClaveDescripcion, 
		'Es clave relacionada' = (case when EsClaveRelacionada = 1 then 'SI' else 'NO' end),  
		--'Es insumo propio' = (case when OrigenDeInsumo = 0 then 'SI' else 'NO' end),  
		'Origen de insumo' = (case when OrigenDeInsumo = 1 then 'CONSIGNACIÓN' else 'PROPIO' end), 
		'Cantidad dispensada' = sum(CantidadVendida), 
		'Cantidad remisionada de producto' = sum(CantidadRemision_Insumo), 
		'Cantidad remisionada de servicio' = sum(CantidadRemision_Admon), 
		'Pendiente remisionar producto' = sum(PendienteRemisionarProducto), 
		'Pendiente remisionar servicio' = sum(PendienteRemisionarServicio) 
	Into #tmp_01_General 
	From #tmp__PendienteRemisionar (noLock) 
	Group by 
		--ClaveSSA_Existe_En__CuadroBasico, 
		ClaveSSA_Existe_En__Producto, 
		ClaveSSA_Existe_En__Servicio, 
		TipoDeClaveDescripcion, EsClaveRelacionada, OrigenDeInsumo    
	Order by 
		ClaveSSA_Existe_En__Producto, ClaveSSA_Existe_En__Servicio, 
		OrigenDeInsumo, TipoDeClaveDescripcion, EsClaveRelacionada  
		

	Select 
		'Clave en listado licitado' = (case when ClaveSSA_Existe_En__Producto = 1 then 'SI' else 'NO' end), 
		'Clave en listado de servicio' = (case when ClaveSSA_Existe_En__Servicio = 1 then 'SI' else 'NO' end), 
		'Tipo de clave' = TipoDeClaveDescripcion, 
		'Es clave relacionada' = (case when EsClaveRelacionada = 1 then 'SI' else 'NO' end),  
		--'Es insumo propio' = (case when OrigenDeInsumo = 0 then 'SI' else 'NO' end),  
		'Origen de insumo' = (case when OrigenDeInsumo = 1 then 'CONSIGNACIÓN' else 'PROPIO' end), 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave' = DescripcionClave, 
		'Cantidad dispensada' = sum(CantidadVendida)--, sum(CantidadRemision_Insumo) as CantidadRemision_Insumo, sum(CantidadRemision_Admon) as CantidadRemision_Admon 
	Into #tmp_02_ClavesFueraDeCuadro  
	From #tmp__PendienteRemisionar (noLock) 
	Where ClaveSSA_Existe_En__Producto = 0 or ClaveSSA_Existe_En__Servicio = 0 
	Group by 
		ClaveSSA_Existe_En__Producto, 
		ClaveSSA_Existe_En__Servicio, 	
		TipoDeClaveDescripcion, ClaveSSA, DescripcionClave, OrigenDeInsumo, EsClaveRelacionada --, OrigenDeInsumo   


	Select 
		'Id Unidad' = IdFarmacia, 
		--'Unidad' = Farmacia, 
		'Clave en listado licitado' = (case when ClaveSSA_Existe_En__Producto = 1 then 'SI' else 'NO' end), 
		'Clave en listado de servicio' = (case when ClaveSSA_Existe_En__Servicio = 1 then 'SI' else 'NO' end), 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave' = DescripcionClave, 
		'Clave en Cuadro Básico' = ClaveSSA_Existe_En__CuadroBasico, 
		'Origen de insumo' = (case when OrigenDeInsumo = 1 then 'CONSIGNACIÓN' else 'PROPIO' end), 
		'Tipo de clave' = TipoDeClaveDescripcion, 
		'Es clave relacionada' = (case when EsClaveRelacionada = 1 then 'SI' else 'NO' end),  
		'Cantidad dispensada' = sum(CantidadVendida), 
		'Cantidad remisionada de producto' = sum(CantidadRemision_Insumo), 
		'Cantidad remisionada de servicio' = sum(CantidadRemision_Admon), 
		'Pendiente remisionar producto' = sum(PendienteRemisionarProducto), 
		'Pendiente remisionar servicio' = sum(PendienteRemisionarServicio) 	
	Into #tmp_03_ResumenFarmacia   
	From #tmp__PendienteRemisionar (noLock) 
	Where 1 = 1 
		and ( Producto_Pendiente = 1 or Servicio_Pendiente = 1 )  
		--and ClaveSSA_Existe_En__CuadroBasico = 1 
		--and ( case when @Validar_Producto = 1 then CantidadVendida <> CantidadRemision_Admon else 0 end) 
	Group by 
		IdFarmacia, 
		--Farmacia, 
		ClaveSSA_Existe_En__Producto, 
		ClaveSSA_Existe_En__Servicio, 	
		ClaveSSA, DescripcionClave, 
		ClaveSSA_Existe_En__CuadroBasico, OrigenDeInsumo, TipoDeClaveDescripcion, EsClaveRelacionada  

	--@Validar_Producto int = 0, @Validar_Servicio int = 1, 

	----------------------------------------- Generar tablas de reporte 



		--		spp_FACT_RTP__ValidarPendienteRemisionar 


	--------------------------------------------- SALIDA FINAL  
	--Select * from #tmp_ListaFarmacias 
	Select top 0 identity(int, 2, 1) as Orden, 1 as EsGeneral, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 

	Insert Into #tmpResultados ( NombreTabla, EsGeneral ) select 'Resumen', 1 
	Insert Into #tmpResultados ( NombreTabla, EsGeneral ) select 'Fuera de cuadro', 1 
	Insert Into #tmpResultados ( NombreTabla, EsGeneral ) select 'Resumen unidad', 1 
	Insert Into #tmpResultados ( NombreTabla, EsGeneral ) select 'Detallado', 0 ---- No mostrar en pantalla  


	Select * From #tmpResultados   
	Select * From #tmp_01_General  
	Select * From #tmp_02_ClavesFueraDeCuadro Order by [Descripción Clave] 
	Select * from #tmp_03_ResumenFarmacia Order by [Id Unidad] 
	Select * From #tmp__PendienteRemisionar (NoLock) Where ( Producto_Pendiente = 1 or Servicio_Pendiente = 1 ) Order by IdFarmacia, FolioVenta, DescripcionClave  


	--------------------------------------------- SALIDA FINAL  

End 
Go--#SQL 
