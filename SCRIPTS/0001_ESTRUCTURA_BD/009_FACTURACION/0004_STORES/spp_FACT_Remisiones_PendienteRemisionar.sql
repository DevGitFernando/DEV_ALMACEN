
--------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_PendienteRemisionar' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_PendienteRemisionar 
Go--#SQL 
  
/*	

	Exec spp_FACT_Remisiones_PendienteRemisionar 
		@IdEmpresa = '001', @IdEstado = '13', 
		@IdFarmaciaGenera = '0001', @TipoDeRemision = 1, 
		@IdFarmacia = [  ],  -- '11', '12', '13', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27' ], 
		@IdCliente = '0002', @IdSubCliente = '0010', 
		@IdFuenteFinanciamiento = '0001', @IdFinanciamiento = '', 
		@FechaInicial = '2017-01-01', @FechaFinal = '2017-07-31', 
		@IdTipoProducto = '02', 
		@TipoDispensacion = 0, -- 0 ==> Venta | 1 Consigna 
		@FechaDeRevision = 2, 
		@FoliosVenta = [  ] -- , 3, 4, 5, 6, 7, 8, 9, 10 ]   


*/ 
  
Create Proc spp_FACT_Remisiones_PendienteRemisionar 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001', @TipoDeRemision smallint = 1, 
	@IdFarmacia varchar(max) = [  ],  -- '11', '12', '13', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27' ], 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0010', 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '', 
	@FechaInicial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2017-07-31', 
	@IdTipoProducto varchar(2) = '02', 
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@FechaDeRevision int = 2, 
	@FoliosVenta varchar(max) = [  ] -- , 3, 4, 5, 6, 7, 8, 9, 10 ]   
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(1000), 
	@sStatus varchar(1), 
	@iActualizado smallint, 
	@iMontoDisponible numeric(14,4), 
	@FolioRemision varchar(10), 
	@SubTotalSinGrabar numeric(14,4), 
	@SubTotalGrabado numeric(14,4), 
	@Iva numeric(14,4), 
	@Total numeric(14,4),		
	@iOpcionFactura smallint,
	@sSql varchar(max), 
	@sFiltro varchar(max), 
	@Facturable bit 

	/**	Tipo de Remision **
		1.- Insumos
		2.- Administracion
	*/

	Set DateFormat YMD 
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	Set @iMontoDisponible = 0.0000

	Set @FolioRemision = '*'
	Set @SubTotalSinGrabar = 0.0000
	Set @SubTotalGrabado = 0.0000
	Set @Iva = 0.0000
	Set @Total = 0.0000	
	Set @iOpcionFactura = 1 
	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @Facturable = 0 


--------------------- Preparar tablas de proceso 
--	Exec spp_FACT_ProcesoRemisiones 


--------------------- Obtener los programas y sub-programas 	
	Select 
		ROW_NUMBER() OVER (order by IdPrograma, IdSubPrograma)as Renglon, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		(IdPrograma + IdSubPrograma) as IdAtencion, 1 as Procesar   
	Into #tmpProgramasDeAtencion 
	From vw_Clientes_Programas_Asignados_Unidad 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
	Group by IdPrograma, Programa, IdSubPrograma, SubPrograma  	
	
--------------------- Obtener los programas y sub-programas 


	-----------------------------------------------------
	-- Se obtiene el catalogo general --
	-----------------------------------------------------
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
	--Where ClaveSSA like '%010.000.0104.00%' 


	----------------------------------------------------------------------------------------------------------------------------
	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	---------------------------------------------------------------------------------------------------------------------------- 
	Select Top 0 *  
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado and IdTipoUnidad <> '006' 

	Set @sFiltro = '' 
	If @IdFarmacia <> '' and @IdFarmacia  <> '*' 
	Begin 
		-- If 
	    Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + @IdFarmacia + ' ) ' 
	End 
	

	--------- Excluir almacenes de remisiones generales 
	If @FechaDeRevision <> 3 
	   Set @sFiltro = @sFiltro + ' and IdTipoUnidad <> ' + char(39) + '006' + char(39)


	Set @sSql = 
		'Insert Into #tmpFarmacias ' + char(10) + 
		'Select * '  + char(10) + 
		'From vw_Farmacias '  + char(10) + 
		'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + @sFiltro   
	Exec(@sSql) 
	Print @sSql 
	-- Select * from #tmpFarmacias 



	Select Top 0 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado 
	Into #tmpVentas 
	From VentasEnc V (NoLock)
	----Inner Join #tmpFoliosCierres C(NoLock) 
	----	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioCierre = C.FolioCierre ) 
	Where V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
	


	If @FechaDeRevision = 1   ---- Fecha de registro 
	Begin 
		Insert Into #tmpVentas   
		Select
			V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado 
		From VentasEnc V (NoLock)  
		Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
			and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
			and convert(varchar(10), V.FechaRegistro, 120) Between @FechaInicial and @FechaFinal   
	End     

	If @FechaDeRevision = 2   ---- Fecha de receta  
	Begin 
		Insert Into #tmpVentas   
		Select 
			V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado 
		From VentasEnc V (NoLock)  
		Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
		Inner Join VentasInformacionAdicional C (NoLock) 
			On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
			and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
			and convert(varchar(10), C.FechaReceta, 120) Between @FechaInicial and @FechaFinal   
	End   


	If @FechaDeRevision = 3   ---- folios especificios   
	Begin 
		Set @TipoDeRemision = 4 
		Set @sSql = 
		'Insert Into #tmpVentas  ' + char(10) + 
		'Select ' + char(10) + 
		'	V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 1 as Procesado,    ' + char(10) + 
		'	' + char(39) + @IdFuenteFinanciamiento + char(39) + ' as IdFuenteFinanciamiento, ' + char(39) + @IdFinanciamiento + char(39) + ' as IdFinanciamiento ' + char(10) + 
		'From VentasEnc V (NoLock)  ' + char(10) + 
		'Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) ' + char(10) + 
		'Inner Join VentasInformacionAdicional C (NoLock) ' + char(10) + 
		'	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) ' + char(10) + 
		'Where V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and V.IdEstado = ' + char(39) + @IdEstado + char(39) + char(10) + -- ' and V.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + char(10) + 
		'	and V.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And V.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + char(10) + 
		'   and cast(V.FolioVenta as int) in ( ' + @FoliosVenta + ' ) '  
	   Exec(@sSql) 
	   Print @sSql 
	End   




	-- @FechaInicial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2017-03-31', 
	-- select top 1 * from VentasInformacionAdicional 

	-- select count(*) from #tmpVentas (nolock) 


	----- Validar los programas seleccionados 
	Update F Set Procesar = 1 
	From #tmpVentas F 
	Inner Join #tmpProgramasDeAtencion P On ( F.IdPrograma = P.IdPrograma and F.IdSubPrograma = P.IdSubPrograma ) 
	
	Delete From #tmpVentas Where Procesar = 0 
	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	---------------------------------------------------------------------------------------------------------------------------- 
	

--		spp_FACT_Remisiones_PendienteRemisionar  	
	

	----------------------------------------------------
	-- Se obtienen los detalles de las ventas --  
	-------------------------- Se crea la tabla temporal
	Select	Top 0 
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, cast( '' as varchar(200)) as Farmacia, 
			L.IdSubFarmacia, L.FolioVenta, 
			cast( '' as varchar(10)) as FechaReceta, 0 as Año, 0 as Mes, 
			V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
			0 as EnCuadroBasico, 
			Cast( '' as varchar(50) ) as IdClaveSSA, 
			Cast( '' as varchar(50) ) as ClaveSSA, 
			0 as Relacionada, 
			Cast( '' as varchar(50) ) as IdClaveSSA_P, 
			Cast( '' as varchar(50) ) as ClaveSSA_P, 

			Cast( '' as varchar(max) ) as DescripcionClaveSSA, 
			Cast( '' as varchar(500) ) as Presentacion, 

			0 as ContenidoPaquete_ClaveSSA, 
			Cast( '' as varchar(2) ) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, 
			
			0 as Factor, 
			EnRemision_Insumo, 
			Cast( 0.0000 as numeric(14, 4) ) as CantidadDisponible_FF, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClave, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClaveUnitario, 	
			Cast( 0.0000 as numeric(14, 4) ) as SubTotal, 
			Cast( 0.0000 as numeric(14, 4) ) as IVA, 
			Cast( 0.0000 as numeric(14, 4) ) as Total, 
			Cast( 0.0000 as numeric(14, 4) ) as TasaIva, 

			EnRemision_Admon, 
			Cast( 0.0000 as numeric(14, 4) ) as CantidadDisponible_FF_Admon, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClave_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClaveUnitario_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as SubTotal_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as IVA_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as Total_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as TasaIva_Servicio, 


			L.CantidadVendida as CantidadVendida_Original, 
			L.CantidadVendida, 
			cast(0 as numeric(14, 4)) as Cantidad_Agrupada, 
			identity(int, 1, 1) as KeyxGeneral 
	Into #tmpLotes  
	From VentasDet_Lotes L (NoLock) 
	Inner Join #tmpVentas V (NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
	Where -- ClaveLote Not Like '%*%' 
		  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 


		  
	------------ Se obtienen los Lotes de "Insumos"
	Insert Into #tmpLotes 
	Select	--top 1 
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, cast( '' as varchar(200)) as Farmacia, L.IdSubFarmacia, L.FolioVenta, 
			convert(varchar(10), C.FechaReceta, 120) as FechaReceta, 
			year(C.FechaReceta) as Año, month(C.FechaReceta) as Mes, 

			V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
			0 as EnCuadroBasico, 
			Cast( '' as varchar(50) ) as IdClaveSSA, 				
			Cast( '' as varchar(50) ) as ClaveSSA, 
			Cast( '' as varchar(50) ) as IdClaveSSA_P, 
			Cast( '' as varchar(50) ) as ClaveSSA_P, 
			Cast( '' as varchar(max) ) as DescripcionClaveSSA, 
			Cast( '' as varchar(500) ) as Presentacion, 
			0 as Relacionada,  
			0 as ContenidoPaquete_ClaveSSA,  
			Cast( '' as varchar(2) ) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, 

			0 as Factor, 
			EnRemision_Insumo, 
			Cast( 0.0000 as numeric(14, 4) ) as CantidadDisponible_FF, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClave, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClaveUnitario, 	
			Cast( 0.0000 as numeric(14, 4) ) as SubTotal, 
			Cast( 0.0000 as numeric(14, 4) ) as IVA, 
			Cast( 0.0000 as numeric(14, 4) ) as Total, 
			Cast( 0.0000 as numeric(14, 4) ) as TasaIva, 

			L.EnRemision_Admon, 
			Cast( 0.0000 as numeric(14, 4) ) as CantidadDisponible_FF_Admon, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClave_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClaveUnitario_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as SubTotal_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as IVA_Servicio, 
			Cast( 0.0000 as numeric(14, 4) ) as Total_Servicio, 	
			Cast( 0.0000 as numeric(14, 4) ) as TasaIva_Servicio,  
			L.CantidadVendida as CantidadVendida_Original, 
			L.CantidadVendida, 0 as Cantidad_Agrupada 
	From VentasDet_Lotes L (NoLock) 
	Inner Join #tmpVentas V (NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		Inner Join VentasInformacionAdicional C (NoLock) 
			On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) -- and P.ClaveSSA = '010.000.1937.00' ) 
	Where -- ClaveLote Not Like '%*%' 
			(case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 
			and L.RemisionFinalizada = 0 and ( L.EnRemision_Insumo = 0  or L.EnRemision_Admon = 0 ) 
			and L.CantidadVendida > 0 

--		spp_FACT_Remisiones_PendienteRemisionar  	

	  
	-------------------------------------------
	-- Se obtiene la Clave SSA y la Tasa Iva --
	------------------------------------------- 	
	Update L Set 
		IdClaveSSA_P = P.IdClaveSSA_Sal, ClaveSSA_P = P.ClaveSSA, 
		IdClaveSSA = P.IdClaveSSA_Sal, 
		ClaveSSA = P.ClaveSSA, 
		DescripcionClaveSSA = P.DescripcionClave, Presentacion = P.Presentacion_ClaveSSA, 
		ContenidoPaquete_ClaveSSA = P.ContenidoPaquete_ClaveSSA, 
		IdTipoProducto = P.IdTipoProducto, TasaIva = P.TasaIva, 
		Cantidad_Agrupada = round( (CantidadVendida / (P.ContenidoPaquete_ClaveSSA * 1.0) ), 4)    
	From #tmpLotes L(NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto And L.CodigoEAN = P.CodigoEAN ) 



	--------------------------------------------------------------------------------------------------------------- 
	--------------- Reemplazo de Claves 
	Update L Set Relacionada = 1, 	
		IdClaveSSA = P.IdClaveSSA_Relacionada, 
		ClaveSSA = P.ClaveSSA, ContenidoPaquete_ClaveSSA = P.ContenidoPaquete, 
		Cantidad_Agrupada = round( (CantidadVendida / (P.ContenidoPaquete * 1.0) ), 4)    
	From #tmpLotes L(NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) On ( L.IdEstado = P.IdEstado and L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 





	--------------------------------------------------------------------------------------------------------------- 
	--------------- Asignacion de descripciones   
	Update L Set 
		DescripcionClaveSSA = P.DescripcionClave, Presentacion = P.Presentacion_ClaveSSA 
	From #tmpLotes L(NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto And L.CodigoEAN = P.CodigoEAN ) 


--		spp_FACT_Remisiones_PendienteRemisionar  	

	--------------------------------------------------------------------------------------------------------------- 
	--------------- Se aplica el Factor de licitacion  
	Update L Set  
		EnCuadroBasico = IsNull(1, 0), 
		Factor = IsNull(P.Factor, 1), 
		CantidadVendida = (CantidadVendida * IsNull(P.Factor, 1)),     
		Cantidad_Agrupada = round( ((CantidadVendida * IsNull(P.Factor, 1)) / (ContenidoPaquete_ClaveSSA * 1.0) ), 4)    
	From #tmpLotes L(NoLock) 
	Left Join vw_Claves_Precios_Asignados P (NoLock) On( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  
	------------------------------------- 


	Update L Set Farmacia = F.NombreFarmacia  
	From #tmpLotes L 
	Inner Join CatFarmacias F On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia ) 



	
----------- Se obtienen las claves de Insumos ó Administracion   
	Select * 
	Into #tmp_FF 
	From FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) 
	Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and D.Status = 'A' 

	if @IdFinanciamiento <> '' and @IdFinanciamiento <> '*' 
	   Delete From #tmp_FF Where IdFinanciamiento <> @IdFinanciamiento  


	Select 
		@IdEstado as IdEstado, D.IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
		-- D.IdFuenteFinanciamiento, D.IdFinanciamiento,  
		ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 0 as ContenidoPaquete, 
		0 as Agrupacion, 
		sum(D.CantidadPresupuestadaPiezas) as CantidadPresupuestadaPiezas, 
		sum(D.CantidadPresupuestada) as CantidadPresupuestada, 
		sum(D.CantidadAsignada) as CantidadAsignada, 
		sum(D.CantidadPresupuestada - D.CantidadAsignada) as CantidadRestante, 
		cast(0 as numeric(14, 4)) as Precio, 
		cast(0 as numeric(14, 4)) as PrecioUnitario, 		
		cast(-1 as numeric(14, 4)) as TasaIva, 
		0 as EsServicio  
	Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia D (NoLock) 
	Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
	Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento 
	    --and D.IdFarmacia = 11 
		--and D.ClaveSSA like '%0104.00%' 
		-- and @FechaDeRevision in ( 1, 2 ) 	
	Group by 
		D.IdFarmacia, 
		-- D.IdFuenteFinanciamiento, D.IdFinanciamiento, 
		ltrim(rtrim(D.ClaveSSA))  


	Update L Set Precio = P.Precio, PrecioUnitario = P.PrecioUnitario, ContenidoPaquete = P.ContenidoPaquete   
	From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L(NoLock) 
	Inner Join vw_Claves_Precios_Asignados P (NoLock) On( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  





--	select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

--		select * from #tmpLotes   	  	  
--		spp_FACT_Remisiones_PendienteRemisionar  	


	---- Determinar las claves de administracion 
	If @FechaDeRevision in ( 1, 2 ) 
	Begin 
		-- Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		
		Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		Select 
			@IdEstado as IdEstado, D.IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
			-- D.IdFuenteFinanciamiento, D.IdFinanciamiento,  
			ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 0 as ContenidoPaquete, 
			0 as Agrupacion, 
			sum(D.CantidadPresupuestadaPiezas) as CantidadPresupuestadaPiezas, 
			sum(D.CantidadPresupuestada) as CantidadPresupuestada, 
			sum(D.CantidadAsignada) as CantidadAsignada, 
			sum(D.CantidadPresupuestada - D.CantidadAsignada) as CantidadRestante, 
			cast(0 as numeric(14, 4)) as Precio, 
			cast(0 as numeric(14, 4)) as PrecioUnitario, 		
			cast(-1 as numeric(14, 4)) as TasaIva, 
			1 as EsServicio    	 
		From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia D (NoLock) 
		Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
		Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento -- and D.Status = 'A' 
			--and D.IdFarmacia = 11 
			--and D.ClaveSSA like '%0104.00%'  
		Group by 
			D.IdFarmacia, 
			-- D.IdFuenteFinanciamiento, D.IdFinanciamiento, 
			ltrim(rtrim(D.ClaveSSA))  


		Update L Set 
			Precio = P.Costo, 
			PrecioUnitario = P.CostoUnitario, 			
			TasaIva = P.TasaIva 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA )
		Where P.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and EsServicio = 1 
		
		
		--------- Sustituir el agrupamiento en los Lotes 
		--Update L Set ContenidoPaquete_ClaveSSA = C.Agrupacion 
		--From  #tmpLotes L 
		--Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C On ( L.ClaveSSA = C.ClaveSSA ) 
		
	End  
	
--	select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 


--		select * from #tmpLotes   	  	  
--		spp_FACT_Remisiones_PendienteRemisionar  	


	----If @ClaveSSA <> '' and @ClaveSSA <> '*' 
	----   Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA Where ClaveSSA <> @ClaveSSA	

	-------------------------------------------------------------------------------------------------
	-------- Se eliminan de la tabla temporal aquellos productos que no pertenecen al Tipo de Insumo 
	----If  @FechaDeRevision in ( 1, 2 )  
	----Begin 
	----	Delete From #tmpLotes Where IdTipoProducto <> @IdTipoProducto 
	----End 

--		select * from #tmpLotes   	  	  

--		spp_FACT_Remisiones_PendienteRemisionar  	


----------- Se obtienen las claves de Insumos ó Administracion   


	----------------------------------------------- 
	-- Se obtienen los Precios de las Claves SSA --
	----------------------------------------------- 	
	--select * 
	--From #tmpLotes L (NoLock) 
	--Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA ) 
	--Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 
	--	-- and P.EsServicio = 0 -- and @TipoDispensacion = 0


	Update L Set 
		PrecioClave = P.Precio, 
		PrecioClaveUnitario = P.PrecioUnitario, 		
		TasaIva = (case when P.TasaIva >= 0 then P.TasaIva else L.TasaIva End)
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA ) 
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 
		and P.EsServicio = 0 and @TipoDispensacion = 0 And EnRemision_Insumo = 0 
--L.EnRemision_Insumo = 0  or L.EnRemision_Admon

	Update L Set 
		PrecioClave_Servicio = P.Precio, 
		PrecioClaveUnitario_Servicio = P.PrecioUnitario, 		
		TasaIva_Servicio = (case when P.TasaIva >= 0 then P.TasaIva else L.TasaIva End) 
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA ) 
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 
		and P.EsServicio = 1 and EnRemision_Admon = 0 



--		spp_FACT_Remisiones_PendienteRemisionar 

---------------------- CALCULAR TOTALES 
	Update L Set 
		SubTotal = PrecioClave * Cantidad_Agrupada,  
		IVA = (PrecioClave * Cantidad_Agrupada) * ( TasaIva / 100.00), 
		Total = (PrecioClave * Cantidad_Agrupada) * ( 1 + ( TasaIva / 100.00)), 

		SubTotal_Servicio = PrecioClave_Servicio * Cantidad_Agrupada,  
		IVA_Servicio = (PrecioClave_Servicio * Cantidad_Agrupada) * ( TasaIva_Servicio / 100.00), 
		Total_Servicio = (PrecioClave_Servicio * Cantidad_Agrupada) * ( 1 + ( TasaIva_Servicio / 100.00))  
	From #tmpLotes L 

---------------------- CALCULAR TOTALES 


-------------------------------------------------- GENERAR RESUMEN  
	Select 
		IdFarmacia, Farmacia, 
		-- IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		EnCuadroBasico, Relacionada, IdClaveSSA, ClaveSSA, IdClaveSSA_P, ClaveSSA_P, DescripcionClaveSSA, Presentacion, 
		ContenidoPaquete_ClaveSSA, IdTipoProducto, 
		

		Factor,
		cast(0 as numeric(14, 4)) as CantidadDisponible_FF, 	
		cast(0 as numeric(14, 4)) as CantidadDisponible_FF_Admon, 
		sum(CantidadVendida_Original) as CantidadVendida_Original, 
		sum(CantidadVendida) as CantidadVendida, 
		sum(Cantidad_Agrupada) as Cantidad_Agrupada, 

		PrecioClave, PrecioClaveUnitario, TasaIva, 
		sum(SubTotal) as SubTotal, 
		sum(IVA) as IVA, 
		sum(Total) as Total, 

		PrecioClave_Servicio, PrecioClaveUnitario_Servicio, TasaIva_Servicio,  
		sum(SubTotal_Servicio) as SubTotal_Servicio, 
		sum(IVA_Servicio) as IVA_Servicio, 
		sum(Total_Servicio) as Total_Servicio, 

		0 as EsExcedente_Producto, 
		0 as EsExcedente_Servicio 

	Into #tmpLotes__Resumen 
	From #tmpLotes (NoLock) 
	Group by 
		IdFarmacia, Farmacia, 
		-- IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		EnCuadroBasico, Relacionada, IdClaveSSA, ClaveSSA, IdClaveSSA_P, ClaveSSA_P, DescripcionClaveSSA, Presentacion, 
		ContenidoPaquete_ClaveSSA, IdTipoProducto, 
		Factor, 
		PrecioClave, PrecioClaveUnitario, TasaIva, 
		PrecioClave_Servicio, PrecioClaveUnitario_Servicio, TasaIva_Servicio 
	Order By IdFarmacia, ClaveSSA 


	Update R Set CantidadDisponible_FF = P.CantidadRestante 
	From #tmpLotes__Resumen R 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On ( R.IdFarmacia = P.IdFarmacia and R.ClaveSSA = P.ClaveSSA ) 
	Where P.EsServicio = 0  

	Update R Set CantidadDisponible_FF_Admon = P.CantidadRestante 
	From #tmpLotes__Resumen R 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On ( R.IdFarmacia = P.IdFarmacia and R.ClaveSSA = P.ClaveSSA ) 
	Where P.EsServicio = 1  


	Update R Set EsExcedente_Producto = 1 
	From #tmpLotes__Resumen R 
	Where CantidadVendida > CantidadDisponible_FF 

	Update R Set EsExcedente_Servicio = 1 
	From #tmpLotes__Resumen R 
	Where CantidadVendida > CantidadDisponible_FF_Admon 

-------------------------------------------------- GENERAR RESUMEN  



----------------------------------------------------- SALIDA FINAL 

--		spp_FACT_Remisiones_PendienteRemisionar 

	Select * From #tmpLotes__Resumen 


	Select 
		IdFarmacia, Farmacia, 
		Año, Mes, FechaReceta, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		EnCuadroBasico, Relacionada, IdClaveSSA, ClaveSSA, IdClaveSSA_P, ClaveSSA_P, DescripcionClaveSSA, Presentacion, 
		ContenidoPaquete_ClaveSSA, IdTipoProducto, 
		
		Factor, 	
		sum(CantidadVendida_Original) as CantidadVendida_Original, 
		sum(CantidadVendida) as CantidadVendida, 
		sum(Cantidad_Agrupada) as Cantidad_Agrupada, 

		PrecioClave, PrecioClaveUnitario, TasaIva, 
		sum(SubTotal) as SubTotal, 
		sum(IVA) as IVA, 
		sum(Total) as Total, 

		PrecioClave_Servicio, PrecioClaveUnitario_Servicio, TasaIva_Servicio,  
		sum(SubTotal_Servicio) as SubTotal_Servicio, 
		sum(IVA_Servicio) as IVA_Servicio, 
		sum(Total_Servicio) as Total_Servicio  
	From #tmpLotes (NoLock) 
	Group by 
		IdFarmacia, Farmacia, 
		FechaReceta, Año, Mes,  
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		EnCuadroBasico, Relacionada, IdClaveSSA, ClaveSSA, IdClaveSSA_P, ClaveSSA_P, DescripcionClaveSSA, Presentacion, 
		ContenidoPaquete_ClaveSSA, IdTipoProducto, 
		Factor, 
		PrecioClave, PrecioClaveUnitario, TasaIva, 
		PrecioClave_Servicio, PrecioClaveUnitario_Servicio, TasaIva_Servicio 
	Order By IdFarmacia, FechaReceta 


	Select * 
	From #tmpLotes (NoLock) 
	Order By IdFarmacia, FolioVenta 
	

End 
Go--#SQL 



