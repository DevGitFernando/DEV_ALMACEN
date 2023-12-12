--------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_General' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_General 
Go--#SQL 
  
/*	

	Exec spp_FACT_GenerarRemisiones_General 
		@IdEmpresa = '001', @IdEstado = '13', 
		@IdFarmaciaGenera = '0001', @TipoDeRemision = 1, 
		@IdFarmacia = '0003', @IdCliente = '0004', @IdSubCliente = '0002', 
		@IdFuenteFinanciamiento = '0001', @IdFinanciamiento = '0001', 
		@Criterio_ProgramasAtencion = '', 
		@FechaInicial = '2017-01-01', @FechaFinal = '2017-03-31', 
		@iMontoFacturar = 0,  @IdPersonalFactura = '0001', 
		@Observaciones = 'SIN OBSERVACIONES', @IdTipoProducto = '00', 
		@EsExcedente = 0, @Identificador = 'TEST', 
		@TipoDispensacion = 0, -- 0 ==> Venta | 1 Consigna 
		@ClaveSSA = '', 
		@FechaDeRevision = 3, @FoliosVenta = [ 1, 2 ] -- , 3, 4, 5, 6, 7, 8, 9, 10 ]   


*/ 
  
Create Proc spp_FACT_GenerarRemisiones_General 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001', @TipoDeRemision smallint = 1, 
	@IdFarmacia varchar(max) = [ 0013 ], 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0010', 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2017-03-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '02', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = 'TEST', 
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(1000) = '', 
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
	Set @ClaveSSA = IsNull(@ClaveSSA, '') 
	Set @ClaveSSA = ltrim(rtrim(@ClaveSSA)) 
	Set @Facturable = 0 


--------------------- Preparar tablas de proceso 
	Exec spp_FACT_ProcesoRemisiones 


--------------------- Obtener los programas y sub-programas 	
	Select 
		ROW_NUMBER() OVER (order by IdPrograma, IdSubPrograma)as Renglon, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		(IdPrograma + IdSubPrograma) as IdAtencion, 1 as Procesar   
	Into #tmpProgramasDeAtencion 
	From vw_Clientes_Programas_Asignados_Unidad 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
	Group by IdPrograma, Programa, IdSubPrograma, SubPrograma  	
	
	If @Criterio_ProgramasAtencion <> '' 
	Begin 
	   Update #tmpProgramasDeAtencion Set Procesar = 0 
	   
	   Set @sSql = '
		   Update P Set Procesar = 1 	   	   
		   From #tmpProgramasDeAtencion P 
		   Where IdAtencion in ( ' + @Criterio_ProgramasAtencion + ' ) ' 
	   Exec(@sSql) 
	   Print @sSql 
	End 

	Delete From #tmpProgramasDeAtencion Where Procesar = 0  		
--------------------- Obtener los programas y sub-programas 


	-----------------------------------------------------
	-- Se obtiene el catalogo general --
	-----------------------------------------------------
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
	-- Where ClaveSSA like '%010.000.0104.00%' 

	-----------------------------------------------------
	-- Se obtienen los Folios del periodo seleccionado --
	-----------------------------------------------------
	----Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioCierre, C.FechaCorte 
	----Into #tmpFoliosCierres
	----From Ctl_CierresDePeriodos C(NoLock) 
	----Inner Join FACT_Informacion_Proceso_Facturacion F(NoLock) On( C.IdEmpresa = F.IdEmpresa And C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia And C.FolioCierre = F.FolioCierre ) 
	----Where C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado And F.Identificador = @Identificador
	----	And Convert( varchar(10), C.FechaCorte, 120 ) Between @FechaInicial And @FechaFinal 


--	select * from #tmpFoliosCierres    


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
	   -- Delete From #tmpFarmacias Where IdFarmacia <> RIGHT('0000' + @IdFarmacia, 4)     
	   Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + char(39) + @IdFarmacia + char(39) +  ' ) ' 
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
	-- Select * from #tmpFarmacias 



	Select Top 0 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado, 
		cast( '' as varchar(4)) as IdFuenteFinanciamiento, cast( '' as varchar(4)) as IdFinanciamiento   
	Into #tmpVentas 
	From VentasEnc V (NoLock)
	----Inner Join #tmpFoliosCierres C(NoLock) 
	----	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioCierre = C.FolioCierre ) 
	Where V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
	


	If @FechaDeRevision = 1   ---- Fecha de registro 
	Begin 
		Insert Into #tmpVentas   
		Select
			V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado, 
			'' as IdFuenteFinanciamiento, '' as IdFinanciamiento 
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
			V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado, 
			'' as IdFuenteFinanciamiento, '' as IdFinanciamiento   
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
	

--		spp_FACT_GenerarRemisiones_General  	
	

	----------------------------------------------------
	-- Se obtienen los detalles de las ventas --  
	-------------------------- Se crea la tabla temporal
	Select	Top 0 
			Cast( '' as varchar(50) ) as FolioRemision, 			
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
			V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
			V.IdFuenteFinanciamiento, V.IdFinanciamiento, 
			V.Procesado, 
			Cast( '' as varchar(50) ) as IdClaveSSA, 
			Cast( '' as varchar(50) ) as ClaveSSA, 
			0 as Relacionada, 
			Cast( '' as varchar(50) ) as IdClaveSSA_P, 
			Cast( '' as varchar(50) ) as ClaveSSA_P, 
			0 as ContenidoPaquete_ClaveSSA, 
			Cast( '' as varchar(2) ) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClave, 
			Cast( 0.0000 as numeric(14, 4) ) as PrecioClaveUnitario, 			
			Cast( 0.0000 as numeric(14, 4) ) as TasaIva, 
			L.CantidadVendida, 
			cast(0 as numeric(14, 4)) as Cantidad_Agrupada, 
			@TipoDeRemision as GrupoProceso, 0 as IncluidoEnExcepcion,    
			identity(int, 1, 1) as KeyxGeneral 
	Into #tmpLotes  
	From VentasDet_Lotes L (NoLock) 
	Inner Join #tmpVentas V (NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
	Where -- ClaveLote Not Like '%*%' 
		  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 


		  
	If @TipoDeRemision = 1  
	  Begin
		------------ Se obtienen los Lotes de "Insumos"
		Insert Into #tmpLotes 
		Select	--top 1 
				Cast( '' as varchar(50) ) as FolioRemision, 
				L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
				V.IdFuenteFinanciamiento, V.IdFinanciamiento, 
				V.Procesado, 
				Cast( '' as varchar(50) ) as IdClaveSSA, 				
				Cast( '' as varchar(50) ) as ClaveSSA, 
				Cast( '' as varchar(50) ) as IdClaveSSA_P, 
				Cast( '' as varchar(50) ) as ClaveSSA_P, 
				0 as Relacionada,  
				0 as ContenidoPaquete_ClaveSSA,  
				Cast( '' as varchar(2) ) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, 
				Cast( 0.0000 as numeric(14, 4) ) as PrecioClave, 
				Cast( 0.0000 as numeric(14, 4) ) as PrecioClaveUnitario, 
				Cast( 0.0000 as numeric(14, 4) ) as TasaIva, 
				L.CantidadVendida, 0 as Cantidad_Agrupada, @TipoDeRemision as GrupoProceso, 0 as IncluidoEnExcepcion  
		From VentasDet_Lotes L (NoLock) 
		Inner Join #tmpVentas V (NoLock) 
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) -- and P.ClaveSSA = '010.000.1937.00' ) 
		Where -- ClaveLote Not Like '%*%' 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 
			  and L.EnRemision_Insumo = 0 and L.RemisionFinalizada = 0 
	  End 
	Else 
	  Begin 
		------------ Se obtienen los Lotes de "Administracion"
		Insert Into #tmpLotes
		Select	
				Cast( '' as varchar(50) ) as FolioRemision, 
				L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
				V.IdFuenteFinanciamiento, V.IdFinanciamiento, 
				V.Procesado, 
				Cast( '' as varchar(50) ) as IdClaveSSA, 				
				Cast( '' as varchar(50) ) as ClaveSSA, 
				Cast( '' as varchar(50) ) as IdClaveSSA_P, 
				Cast( '' as varchar(50) ) as ClaveSSA_P, 
				0 as Relacionada, 
				0 as ContenidoPaquete_ClaveSSA, 
				Cast( '' as varchar(2) ) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, 
				Cast( 0.0000 as numeric(14, 4) ) as PrecioClave, 
				Cast( 0.0000 as numeric(14, 4) ) as PrecioClaveUnitario, 
				Cast( 0.0000 as numeric(14, 4) ) as TasaIva, 
				L.CantidadVendida, 0 as Cantidad_Agrupada, @TipoDeRemision as GrupoProceso, 0 as IncluidoEnExcepcion     
		From VentasDet_Lotes L(NoLock) 
		Inner Join #tmpVentas V(NoLock) 
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) -- and P.ClaveSSA = '010.000.1937.00' ) 
		Where -- ClaveLote Like '%*%' 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion  
			  and L.EnRemision_Admon = 0 and L.RemisionFinalizada = 0 
	  End 
	  
	  
--		select * from #tmpLotes   	  	  

--		spp_FACT_GenerarRemisiones_General  	

	  
	-------------------------------------------
	-- Se obtiene la Clave SSA y la Tasa Iva --
	------------------------------------------- 	
	Update L Set 
		IdClaveSSA_P = P.IdClaveSSA_Sal, ClaveSSA_P = P.ClaveSSA, 
		IdClaveSSA = P.IdClaveSSA_Sal, 
		ClaveSSA = P.ClaveSSA, 
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



--		spp_FACT_GenerarRemisiones_General  	

	--------------------------------------------------------------------------------------------------------------- 
	--------------- Se aplica el Factor de licitacion  
	Update L Set  
		CantidadVendida = (CantidadVendida * IsNull(Factor, 1)),     
		Cantidad_Agrupada = round( ((CantidadVendida * IsNull(Factor, 1)) / (ContenidoPaquete_ClaveSSA * 1.0) ), 4)    
	From #tmpLotes L(NoLock) 
	Left Join vw_Claves_Precios_Asignados P (NoLock) On( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  





--		select * from #tmpLotes   	  	  
--		spp_FACT_GenerarRemisiones_General  	


--	Select * From #tmpLotes Where IdProducto in ( 00017883, 00017336 ) 
--	Select * From #tmpLotes Where Relacionada = 1 

	------------------------------------- 


	
----------- Se obtienen las claves de Insumos ó Administracion   
	Select * 
	Into #tmp_FF 
	From FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) 
	Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and D.Status = 'A' 

	if @IdFinanciamiento <> '' and @IdFinanciamiento <> '*' 
	   Delete From #tmp_FF Where IdFinanciamiento <> @IdFinanciamiento  


	Select 
		@IdEstado as IdEstado, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
		D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
		ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 0 as ContenidoPaquete, 
		0 as Agrupacion, 
		D.CantidadPresupuestadaPiezas, D.CantidadPresupuestada, D.CantidadAsignada, 
		(D.CantidadPresupuestada - D.CantidadAsignada) as CantidadRestante, 
		cast(0 as numeric(14, 4)) as Precio, 
		cast(0 as numeric(14, 4)) as PrecioUnitario, 		
		cast(-1 as numeric(14, 4)) as TasaIva 
	Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
	Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
	Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento 
		-- and D.ClaveSSA like '%2190%' 
		-- and @FechaDeRevision in ( 1, 2 ) 

	Update L Set Precio = P.Precio, PrecioUnitario = P.PrecioUnitario, ContenidoPaquete = P.ContenidoPaquete   
	From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L(NoLock) 
	Inner Join vw_Claves_Precios_Asignados P (NoLock) On( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  





--	select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

--		select * from #tmpLotes   	  	  
--		spp_FACT_GenerarRemisiones_General  	


	---- Determinar las claves de administracion 
	If @TipoDeRemision = 2 and @FechaDeRevision in ( 1, 2 ) 
	Begin 
		Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		
		Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		Select 
			@IdEstado as IdEstado, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
			D.IdFuenteFinanciamiento, D.IdFinanciamiento, 0 as Prioridad, 
			ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 0 as ContenidoPaquete, 
			D.Agrupacion, 
			D.CantidadPresupuestadaPiezas, D.CantidadPresupuestada, D.CantidadAsignada, 
			(D.CantidadPresupuestada - D.CantidadAsignada) as CantidadRestante, 
			0, 0, 0   	 
		From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA D (NoLock) 
		Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
		Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento -- and D.Status = 'A' 
		
		Update L Set 
			Precio = P.Costo, 
			PrecioUnitario = P.CostoUnitario, 			
			TasaIva = P.TasaIva 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA )
		Where P.IdFuenteFinanciamiento = @IdFuenteFinanciamiento 	
		
		
		--------- Sustituir el agrupamiento en los Lotes 
		Update L Set ContenidoPaquete_ClaveSSA = C.Agrupacion 
		From  #tmpLotes L 
		Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C On ( L.ClaveSSA = C.ClaveSSA ) 
		
	End  
	
--		select * from #tmpLotes   	  	  
--		spp_FACT_GenerarRemisiones_General  	


	If @ClaveSSA <> '' and @ClaveSSA <> '*' 
	   Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA Where ClaveSSA <> @ClaveSSA	

	---------------------------------------------------------------------------------------------
	---- Se eliminan de la tabla temporal aquellos productos que no pertenecen al Tipo de Insumo 
	If  @FechaDeRevision in ( 1, 2 )  
	Begin 
		Delete From #tmpLotes Where IdTipoProducto <> @IdTipoProducto 
	End 

--		select * from #tmpLotes   	  	  
--		spp_FACT_GenerarRemisiones_General  	

----------- Se obtienen las claves de Insumos ó Administracion   
	
	---------------------------------------------------------------------------------------------------- 
	---- Se eliminan de la tabla temporal aquellos productos cuya clave no pertenesca al financiamiento 
	--Delete From #tmpLotes Where ClaveSSA Not In ( Select ClaveSSA From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) )    



	-----------------------------------------------
	-- Se obtienen los Precios de las Claves SSA --
	----------------------------------------------- 	
	Update L Set 
		PrecioClave = P.Precio, 
		PrecioClaveUnitario = P.PrecioUnitario, 		
		TasaIva = (case when P.TasaIva >= 0 then P.TasaIva else L.TasaIva End)
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA ) 
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 
		--and @FechaDeRevision in ( 1, 2 )  




	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA   

	--select * 
	--From #tmpLotes L (NoLock) 
	--Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA ) 
	--Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 

--		spp_FACT_GenerarRemisiones_General  	

------------------------------------------------------ MODIFICACION DEL PROCESO 

--		spp_FACT_GenerarRemisiones_General 

	Select ClaveSSA, sum(CantidadVendida) as Cantidad, identity(int, 1, 1) as Keyx    
	Into #tmp_Claves_Concentrado  
	From #tmpLotes   
	Group by ClaveSSA   
	Order by Cantidad desc  



--	Delete From #tmpLotes Where ClaveSSA <> '010.000.1937.00' 

	--Select count(*) 
	--From #tmpLotes 	

	--Select ClaveSSA, sum(CantidadVendida) as CantidadVendida 
	--From #tmpLotes 
	--Group by ClaveSSA  


	--Select * 
	--From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	--Where ClaveSSA = '010.000.1937.00'   

--	Select * from #tmpLotes 
	
	--------------------------------------------------------------------------------------------------------------------------------------------- 
	Declare 
		@sClaveSSA_Proceso varchar(20),  
		@iCantidad int, 
		@iCantidad_Restante int, 
		@sIdFuenteFinanciamiento varchar(20), 
		@sIdFinanciamiento varchar(20), 		 
		@iFolioRemision int 


	Set @sClaveSSA_Proceso = '' 
	Set @iCantidad = 0 
	Set @iCantidad_Restante = 0 
	Set @sIdFuenteFinanciamiento = '' 
	Set @sIdFinanciamiento = '' 
	Set @iFolioRemision = 0 

--		spp_FACT_GenerarRemisiones_General 

	Select Top 0 cast(KeyxGeneral as int) as KeyxGeneral, IdFarmacia, FolioVenta, CantidadVendida, 0 as Acumulado, 0 as Incluir,  
		identity(int, 1, 1) as Keyx  
	Into #tmp_Asignacion_Cantidades 
	From #tmpLotes 


	Declare #cursorClaves  
	Cursor For 
		Select ClaveSSA, Cantidad
		From #tmp_Claves_Concentrado T  
		Where @FechaDeRevision in ( 1, 2, 3 ) 
		Order by Keyx desc  
	Open #cursorClaves 
	FETCH NEXT FROM #cursorClaves Into @sClaveSSA_Proceso, @iCantidad  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			--			Print @sClaveSSA_Proceso + '    ' + cast(@iCantidad as varchar(10)) 
			------------------------------------------------------------------------------------------------------------------------------------- 
			Declare #cursorFuentesDeFinanciamiento  
			Cursor For 
				Select IdFuenteFinanciamiento, IdFinanciamiento, CantidadRestante  
				From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T 
				where ClaveSSA = @sClaveSSA_Proceso 
				Order by Prioridad, CantidadRestante  
			Open #cursorFuentesDeFinanciamiento 
			FETCH NEXT FROM #cursorFuentesDeFinanciamiento Into @sIdFuenteFinanciamiento, @sIdFinanciamiento, @iCantidad_Restante 
				WHILE @@FETCH_STATUS = 0 
				BEGIN 
					
					--		spp_FACT_GenerarRemisiones_General    
					--		Print cast(@iCantidad_Restante as varchar(20))    

					Delete From #tmp_Asignacion_Cantidades   

					Insert into #tmp_Asignacion_Cantidades ( KeyxGeneral, IdFarmacia, FolioVenta, CantidadVendida, Acumulado, Incluir )  
					Select cast(KeyxGeneral as int) as KeyxGeneral, IdFarmacia, FolioVenta, CantidadVendida, 0 as Acumulado, 0 as Incluir  
					From #tmpLotes  
					Where ClaveSSA = @sClaveSSA_Proceso and Procesado = 0  
					Order by CantidadVendida desc  
					

					Update A Set Acumulado = ( select sum(CantidadVendida) From #tmp_Asignacion_Cantidades H Where H.Keyx <= A.Keyx )
					From #tmp_Asignacion_Cantidades A 

					Update A Set Incluir = 1 
					From #tmp_Asignacion_Cantidades A 
					Where Acumulado <= @iCantidad_Restante 


					----------- La administracion se cobra completa aunque se haya excedido la cantidad 
					If ( @TipoDispensacion = 1 )  
					Begin 
						Update A Set Incluir = 1 
						From #tmp_Asignacion_Cantidades A 

						-- print 'CONSIGNA'   
					End  


					Update L Set Procesado = 1, IdFuenteFinanciamiento = @sIdFuenteFinanciamiento, IdFinanciamiento = @sIdFinanciamiento
					From #tmpLotes L 
					Inner Join #tmp_Asignacion_Cantidades P On ( L.KeyxGeneral = P.KeyxGeneral and P.Incluir = 1 ) 


					Update C Set CantidadAsignada = IsNull((select sum(CantidadVendida) From #tmpLotes L (NoLock) 
						Where L.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and L.IdFinanciamiento = C.IdFinanciamiento and ClaveSSA = @sClaveSSA_Proceso and Procesado = 1 ), 0 )  
					From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C 
					Where IdFuenteFinanciamiento = @sIdFuenteFinanciamiento and IdFinanciamiento = @sIdFinanciamiento and ClaveSSA = @sClaveSSA_Proceso 
						and @TipoDispensacion = 0 -- Solo afectar VENTA 


					--Select *   
					--From #tmp_Asignacion_Cantidades   
					--Order by Keyx   


					FETCH NEXT FROM #cursorFuentesDeFinanciamiento Into @sIdFuenteFinanciamiento, @sIdFinanciamiento, @iCantidad_Restante    
				END	 
			Close #cursorFuentesDeFinanciamiento 
			Deallocate #cursorFuentesDeFinanciamiento 	
			------------------------------------------------------------------------------------------------------------------------------------- 

			FETCH NEXT FROM #cursorClaves Into @sClaveSSA_Proceso, @iCantidad     
		END	 
	Close #cursorClaves 
	Deallocate #cursorClaves 	
	--------------------------------------------------------------------------------------------------------------------------------------------- 


	--------------------------------------------------------------------------------------------------------------------------------------------- 
	------------------------------- CLAVES CON EXCEPCÍON DE ACUERDO A CAUSES   -- 20170712.1650 
	Select  
		E.IdEstado, E.IdCliente, E.IdSubCliente, E.NumeroDePartidaLicitacion, 
		E.IdClaveSSA, E.ClaveSSA, E.PrecioBase, E.Incremento, E.PorcentajeIncremento, E.PrecioFinal, E.Status, 
		cast((E.PrecioBase / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as PrecioBase_Unitario, 
		cast((E.Incremento / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as Incremento_Unitario   
	Into #FACT_ClavesSSA_Precios__Excepciones
	From FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
	Inner Join vw_Claves_Precios_Asignados C (NoLock) 
		On ( E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente and E.ClaveSSA = C.ClaveSSA  ) 
	Where Exists 
	( 
		Select * 
		From #tmpLotes L (NoLock) 
		Where L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA 
	) and @TipoDeRemision = 1 ---- Solo producto de Venta 
	  and @TipoDispensacion = 0 

	Update L Set IncluidoEnExcepcion = 1 
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
		On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 
	-- Where E.IdEstado = @IdEstado 


	Select * 
	Into #tmpLotes___Excepcion_01 
	From #tmpLotes (NoLock) 
	Where IncluidoEnExcepcion = 1 

	-- GrupoProceso 

	Select * 
	Into #tmpLotes___Excepcion_02 
	From #tmpLotes (NoLock) 
	Where IncluidoEnExcepcion = 1 

	Update L Set PrecioClave = E.PrecioBase, PrecioClaveUnitario = PrecioBase_Unitario 
	From #tmpLotes___Excepcion_01 L (NoLock) 
	Inner Join #FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
		On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 

	Update L Set PrecioClave = E.Incremento, PrecioClaveUnitario = Incremento_Unitario, GrupoProceso = 3  
	From #tmpLotes___Excepcion_02 L (NoLock) 
	Inner Join #FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
		On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 


	--- Quitar los registros reprocesados 
	Delete From #tmpLotes Where IncluidoEnExcepcion = 1 

	Insert Into #tmpLotes 
	Select 	
		FolioRemision, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdFuenteFinanciamiento, IdFinanciamiento, 
		Procesado, IdClaveSSA, ClaveSSA, Relacionada, IdClaveSSA_P, ClaveSSA_P, 
		ContenidoPaquete_ClaveSSA, IdTipoProducto, IdProducto, CodigoEAN, ClaveLote, 
		PrecioClave, PrecioClaveUnitario, TasaIva, CantidadVendida, Cantidad_Agrupada, GrupoProceso, IncluidoEnExcepcion 
	From #tmpLotes___Excepcion_01 

	Insert Into #tmpLotes 
	Select 	
		FolioRemision, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdFuenteFinanciamiento, IdFinanciamiento, 
		Procesado, IdClaveSSA, ClaveSSA, Relacionada, IdClaveSSA_P, ClaveSSA_P, 
		ContenidoPaquete_ClaveSSA, IdTipoProducto, IdProducto, CodigoEAN, ClaveLote, 
		PrecioClave, PrecioClaveUnitario, TasaIva, CantidadVendida, Cantidad_Agrupada, GrupoProceso, IncluidoEnExcepcion 
	From #tmpLotes___Excepcion_02 


	----Select * 
	----From #tmpLotes 
	----Order by FolioVenta, GrupoProceso 


	--Select L.* 
	--From #tmpLotes L (NoLock)  
	--Inner Join FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
	--	On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 
	------------------------------- CLAVES CON EXCEPCÍON DE ACUERDO A CAUSES   -- 20170712.1650 
	--------------------------------------------------------------------------------------------------------------------------------------------- 


	--		spp_FACT_GenerarRemisiones_General 

	select * from #tmpLotes  

	------------------------------- Generar los folios de remision 
	Select @iFolioRemision = cast( (max(FolioRemision) + 1) as varchar) 
	From FACT_Remisiones (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
	Set @iFolioRemision = IsNull(@iFolioRemision, 1) 

	Select 
		IDENTITY(int, 0, 1) as Consecutivo, 
		@iFolioRemision as FolioInicial, 
		cast('' as varchar(20)) as FolioRemision, 
		IdFuenteFinanciamiento, IdFinanciamiento, GrupoProceso  
	Into #tmp_Remisiones 
	From #tmpLotes 
	Where Procesado = 1 
	Group By IdFuenteFinanciamiento, IdFinanciamiento, GrupoProceso  
	Order By IdFuenteFinanciamiento, IdFinanciamiento, GrupoProceso   

	Update R Set FolioRemision = right('0000000000000000' + cast(FolioInicial + Consecutivo as varchar(10)), 10) 
	From #tmp_Remisiones R 

	Update L Set FolioRemision = R.FolioRemision 
	From #tmpLotes L (NoLock) 
	Inner Join #tmp_Remisiones R (NoLock) 
		On ( L.IdFuenteFinanciamiento = R.IdFuenteFinanciamiento and L.IdFinanciamiento = R.IdFinanciamiento and L.GrupoProceso = R.GrupoProceso ) 

	Select @sMensaje = 'Se generaron los folios de remisión del ' + cast(min(FolioRemision) as varchar(10)) + ' al ' + cast(max(FolioRemision) as varchar(10)) 
	From #tmp_Remisiones (NoLock) 

	--Select * from #tmp_Remisiones  


	--Insert Into FACT_PRCS__RemisionesGeneradas (  IdEstado, IdFarmaciaGenera, FolioRemision, SubTotal_SinGrabar, SubTotal_Grabado, Iva, Importe, ID_Genera ) 
	--Select 
	--	@IdEstado as IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, 
	--	FolioRemision, SubTotal_SinGrabar, SubTotal_Grabado, Iva, Importe, @IdEjecutaProceso as ID_Genera 
	--From #tmp_Remisiones 	
	-- Select * From #tmp_Remisiones (NoLock) 
	------------------------------- Generar los folios de remision 


	------------------------------- Calcular las cantidades distribuidas  
	Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, sum(cantidadVendida) as Cantidad  
	Into #tmpDistribucion 
	From #tmpLotes 
	Where Procesado = 1 and GrupoProceso = 0  
	Group By IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA  


	Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Procesado, sum(cantidadVendida) as Cantidad  
	Into #tmpDistribucion__General 
	From #tmpLotes 
	Where GrupoProceso = 0  
	Group By IdFuenteFinanciamiento, IdFinanciamiento, Procesado, ClaveSSA  
	------------------------------- Calcular las cantidades distribuidas  


	--Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, sum(cantidadVendida) as CantidadVendida 
	--From #tmpLotes 
	--Group By IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA 

	--Select * From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

------------------------------------------------------ MODIFICACION DEL PROCESO 



	--------------------------------------------------------------
	-- Se insertan los datos en la tabla final de procesamiento -- 
	-------------------------------------------------------------- 
	Select	FolioRemision, 
			IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, 
			IdFuenteFinanciamiento, IdFinanciamiento, 
			IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
			ClaveSSA, 
			ContenidoPaquete_ClaveSSA, 
			IdProducto, CodigoEAN, ClaveLote, 
			PrecioClave, PrecioClaveUnitario, TasaIva, 
			CantidadVendida, 
			round( Cantidad_Agrupada, 4) as Cantidad_Agrupada, 
			IsNull( Cast(  (case when TasaIva = 0 Then ( round(Cantidad_Agrupada * PrecioClave, 4) ) Else 0 End) as Numeric(14, 4) ), 0 ) as SubTotal_SinGrabar, 
			IsNull( Cast(  (case when TasaIva > 0 Then ( round(Cantidad_Agrupada * PrecioClave, 4) ) Else 0 End) as Numeric(14, 4) ), 0 )  as SubTotal_Grabado, 			
			IsNull( Cast(  (case when TasaIva > 0 Then round( ( (Cantidad_Agrupada * PrecioClave) * (TasaIva / 100.00) ), 4) Else 0 End) as Numeric(14, 4) ), 0 )  as Iva, 
			IsNull( Cast( 0.0000 as numeric(14, 4) ), 0 )  as Importe, 
			IsNull( Cast( 0.0000 as numeric(14, 4) ), 0 )  as Acumulado, Cast( 0 as bit ) as Facturable, 
			GrupoProceso as TipoDeRemision,
			Identity(int, 1, 1) as Keyx  
	Into #tmpLotesProceso  
	From #tmpLotes (NoLock)  
	Where Procesado = 1 
	Order By PrecioClave Desc, CantidadVendida Desc  


	Select	FolioRemision, 
			IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, 
			IdFuenteFinanciamiento, IdFinanciamiento, 
			IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
			ClaveSSA, 
			ContenidoPaquete_ClaveSSA, 
			IdProducto, CodigoEAN, ClaveLote, 
			PrecioClave, PrecioClaveUnitario, TasaIva, 
			CantidadVendida, 
			round( Cantidad_Agrupada, 4) as Cantidad_Agrupada, 
			IsNull( Cast(  (case when TasaIva = 0 Then ( round(Cantidad_Agrupada * PrecioClave, 4) ) Else 0 End) as Numeric(14, 4) ), 0 ) as SubTotal_SinGrabar, 
			IsNull( Cast(  (case when TasaIva > 0 Then ( round(Cantidad_Agrupada * PrecioClave, 4) ) Else 0 End) as Numeric(14, 4) ), 0 )  as SubTotal_Grabado, 			
			IsNull( Cast(  (case when TasaIva > 0 Then round( ( (Cantidad_Agrupada * PrecioClave) * (TasaIva / 100.00) ), 4) Else 0 End) as Numeric(14, 4) ), 0 )  as Iva, 
			IsNull( Cast( 0.0000 as numeric(14, 4) ), 0 )  as Importe, 
			IsNull( Cast( 0.0000 as numeric(14, 4) ), 0 )  as Acumulado, Cast( 0 as bit ) as Facturable,
			Identity(int, 1, 1) as Keyx  
	Into #tmpLotes_NoProcesado   
	From #tmpLotes (NoLock)  
	Where Procesado = 0 
	Order By PrecioClave Desc, CantidadVendida Desc  




	-------------------------------- 
	-- Se obtienen las cantidades --
	--------------------------------
	-- Se obtiene el Importe
	Update #tmpLotesProceso Set Importe = ( SubTotal_SinGrabar + SubTotal_Grabado + Iva ) 

	-- Se obtiene el Acumulado
	Update L Set Acumulado = (Select Sum( Importe ) From #tmpLotesProceso P(NoLock) Where L.Keyx >= P.Keyx )
	From #tmpLotesProceso L (NoLock) 
	


	--------------------------------------------------------------------------
	-- Se marcan los Facturables y se obtiene el monto que queda disponible --
	--------------------------------------------------------------------------
	-- Se marcan los codigos facturables. 
	Update #tmpLotesProceso Set Facturable = 1 Where Acumulado <= @iMontoFacturar  ------ MODO POR MONTOS 
	
	If @iMontoFacturar = 0 ------ MODO LIBRE 
	Begin 
	   Update #tmpLotesProceso Set Facturable = 1  
	End 
	 

	-- Se obtiene el monto disponible.
	Select @iMontoDisponible = ( Select Top 1 ( @iMontoFacturar - Acumulado ) 
	From #tmpLotesProceso (NoLock) Where Facturable = 1 Order By Keyx Desc ) 

	-- Se obtienen los totales de cada concepto.
	Select	@SubTotalSinGrabar = IsNull( Sum( SubTotal_SinGrabar), 0 ), @SubTotalGrabado = IsNull( Sum(SubTotal_Grabado), 0 ), 
			@Iva = IsNull( Sum(Iva), 0 ), @Total = IsNull( Sum(Importe), 0 )
	From #tmpLotesProceso (NoLock) 
	Where Facturable = 1 


	--select * 
	--From #tmpLotesProceso (NoLock)  
	--Where Facturable = 1  

	
------------------------------------------------------------------------------------------------------------------------------ 
---- Se insertan los datos agrupados en la tabla final de procesamiento 
	Select	FolioRemision, 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 
			IdFuenteFinanciamiento, IdFinanciamiento,  IdPrograma, IdSubPrograma, 
			ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
			TasaIva, 
			sum(CantidadVendida) as CantidadVendida, 
			sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
			IsNull( Cast(  (case when TasaIva = 0 Then ( round(sum(Cantidad_Agrupada) * PrecioClave, 4) ) Else 0 End) as Numeric(14, 4) ), 0 ) as SubTotal_SinGrabar, 
			IsNull( Cast(  (case when TasaIva > 0 Then ( round(sum(Cantidad_Agrupada) * PrecioClave, 4) ) Else 0 End) as Numeric(14, 4) ), 0 )  as SubTotal_Grabado, 			
			IsNull( Cast(  (case when TasaIva > 0 Then round( ( (sum(Cantidad_Agrupada) * PrecioClave) * (TasaIva / 100.00) ), 4) Else 0 End) as Numeric(14, 4) ), 0 )  as Iva, 
			IsNull( Cast( 0.0000 as numeric(14, 4) ), 0 )  as Importe, 
			IsNull( Cast( 0.0000 as numeric(14, 4) ), 0 )  as Acumulado, Cast( 1 as bit ) as Facturable, 
			TipoDeRemision, 
			Identity(int, 1, 1) as Keyx  
	Into #tmpLotesProceso_Agrupado   
	From #tmpLotesProceso (NoLock)  
	Where Facturable = 1  
	Group by FolioRemision, TipoDeRemision, 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 
			IdFuenteFinanciamiento, IdFinanciamiento,  IdPrograma, IdSubPrograma, 
			ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, TasaIva 
	Order By PrecioClave Desc, CantidadVendida Desc  
	
	-- Se obtiene el Importe
	Update #tmpLotesProceso_Agrupado Set Importe = ( SubTotal_SinGrabar + SubTotal_Grabado + Iva ) 	
	
	Select	@SubTotalSinGrabar = IsNull( Sum( SubTotal_SinGrabar), 0 ), @SubTotalGrabado = IsNull( Sum(SubTotal_Grabado), 0 ), 
			@Iva = IsNull( Sum(Iva), 0 ), @Total = IsNull( Sum(Importe), 0 )
	From #tmpLotesProceso_Agrupado (NoLock) 


	
	Insert Into FACT_PRCS__RemisionesGeneradas (  IdEstado, IdFarmaciaGenera, FolioRemision, SubTotal_SinGrabar, SubTotal_Grabado, Iva, Importe, ID_Genera ) 
	Select 
		@IdEstado as IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, FolioRemision, 
		sum(SubTotal_SinGrabar) as SubTotal_SinGrabar, 
		sum(SubTotal_Grabado) as SubTotal_Grabado, sum(Iva) as Iva, sum(Importe) as Importe, @Identificador as ID_Genera 
	From #tmpLotesProceso_Agrupado 	
	Group by FolioRemision 
	-- Select * From #tmp_Remisiones (NoLock) 

---- Se insertan los datos agrupados en la tabla final de procesamiento 	
------------------------------------------------------------------------------------------------------------------------------ 


--- Validar el esquema de facturacion 
	If @iMontoFacturar = 0 or @Total > 0 
		Set @Facturable = 1 
	
	Select @Facturable = (case when count(*) > 0 then 1 else 0 end)
	From #tmpLotesProceso(NoLock)
	Where Facturable = 1	
		

	-----------------------------------------
	---- Se obtiene el Folio de la Factura --
	-----------------------------------------
	If @Facturable = 0 
		Begin 
			Select @FolioRemision = '', @sMensaje = 'No se encontro información para generar remisión' 
		End 
	Else    
		Begin
			-------- Se inserta el Encabezado de la Factura
			--Exec spp_Mtto_FACT_Remisiones @IdEmpresa, @IdEstado, @IdFarmaciaGenera, @FolioRemision Output, @TipoDeRemision, @EsExcedente, 
			--	@IdPersonalFactura, @IdFuenteFinanciamiento, '@IdFinanciamiento', @SubTotalSinGrabar, @SubTotalGrabado, @Iva, @Total, 
			--	@Observaciones, @iOpcionFactura, @IdTipoProducto, @TipoDispensacion 


			Insert Into FACT_Remisiones ( 
					IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeRemision, EsExcedente, IdPersonalRemision, IdPersonalValida, 
					IdFuenteFinanciamiento, IdFinanciamiento, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
					Observaciones, Status, Actualizado, TipoInsumo, OrigenInsumo ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeRemision, @EsExcedente, @IdPersonalFactura, @IdPersonalFactura, 
					IdFuenteFinanciamiento, IdFinanciamiento, 
					Sum(SubTotal_SinGrabar) as SubTotalSinGrabar, Sum(SubTotal_Grabado) as SubTotalGrabado, Sum(Iva) as Iva, Sum(Importe) as Total, 
					@Observaciones, 'A' as Status, 0 as Actualizado, @IdTipoProducto, @TipoDispensacion 
			From #tmpLotesProceso_Agrupado (NoLock)    
			Where Facturable = 1 
			Group By IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeRemision, IdFuenteFinanciamiento, IdFinanciamiento 

			-- Se inserta el Detalle de la Factura
			Insert Into FACT_Remisiones_Detalles ( 
					IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento, 
					IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
					PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
					TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
					ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
					CantidadVendida, 
					Cantidad_Agrupada as CantidadVendida,  
					TasaIva, SubTotal_SinGrabar, SubTotal_Grabado, 
					Iva, Importe 
			From #tmpLotesProceso (NoLock)
			Where Facturable = 1 
			Order By Keyx

			-- Se inserta el Concentrado de la Factura
			Insert Into FACT_Remisiones_Concentrado ( 
					IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento, 
					IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
					PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
					TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento,  
					IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
					PrecioClave, PrecioClaveUnitario, 
					Sum(CantidadVendida), 
					Sum(Cantidad_Agrupada) as CantidadVendida, 					
					max(TasaIva), 
					Sum(SubTotal_SinGrabar), Sum(SubTotal_Grabado), Sum(Iva), Sum(Importe)
			From #tmpLotesProceso_Agrupado (NoLock)   ----- AQUI   #tmpLotesProceso 
			Where Facturable = 1 
			Group By IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento,  
					IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario 

			-- Se inserta el Resumen de la Factura
			Insert Into FACT_Remisiones_Resumen ( 
					IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento,  
					IdPrograma,	IdSubPrograma, ClaveSSA, 
					PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
					TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
					IdPrograma, IdSubPrograma, ClaveSSA, 
					PrecioClave as PrecioLicitado, PrecioClaveUnitario as PrecioLicitadoUnitario, 
					Sum(CantidadVendida), 
					Sum(Cantidad_Agrupada) as CantidadVendida, 					
					max(TasaIva), 
					Sum(SubTotal_SinGrabar), Sum(SubTotal_Grabado), Sum(Iva), Sum(Importe)
			From #tmpLotesProceso_Agrupado (NoLock)
			-- Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And FolioRemision = @FolioRemision
			Group By IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, 
				IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA, PrecioClave, PrecioClaveUnitario   



			If @TipoDeRemision = 1 
				Begin 	
					-----------------------------------------------------------
					-- Se marcan las ventas que se facturará insumos  
					Update L Set L.EnRemision_Insumo = 1 
					From VentasDet_Lotes L(NoLock) 
					Inner Join #tmpLotesProceso V(NoLock) 
						On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
							And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
					Where V.Facturable = 1 and L.EnRemision_Insumo = 0 and L.RemisionFinalizada = 0 

					---------------- Actualizar las cantidades distribuidas 
					Update F Set CantidadAsignada = F.CantidadAsignada + D.Cantidad 
					From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA F (NoLock) 
					Inner Join #tmpDistribucion D (NoLock) 
						On ( F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and F.IdFinanciamiento = D.IdFinanciamiento and F.ClaveSSA = D.ClaveSSA ) 
					Where @TipoDispensacion = 0 
				End 
			Else 
				Begin 
					-----------------------------------------------------------
					-- Se marcan las ventas que se facturará administración 
					Update L Set L.EnRemision_Admon = 1 
					From VentasDet_Lotes L (NoLock) 
					Inner Join #tmpLotesProceso V(NoLock) 
						On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
							And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
					Where V.Facturable = 1 and L.EnRemision_Admon = 0 and L.RemisionFinalizada = 0		

					------------------ Actualizar las cantidades distribuidas 
					--Update F Set CantidadAsignada = F.CantidadAsignada + D.Cantidad  
					--From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA F (NoLock) 
					--Inner Join #tmpDistribucion D (NoLock) 
					--	On ( F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and F.IdFinanciamiento = D.IdFinanciamiento and F.ClaveSSA = D.ClaveSSA ) 
					--Where @TipoDispensacion = 0 
				End 


			-----------------------------------------------------------
			-- Se marcan los lotes que ya tienen remisión de Insumos y Administración 
			Update L Set L.RemisionFinalizada = 1 
			From VentasDet_Lotes L (NoLock) 
			Inner Join #tmpLotesProceso V(NoLock) 
				On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
					And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
			Where L.EnRemision_Insumo = 1 and L.EnRemision_Admon = 1	and L.RemisionFinalizada = 0 	
			
		End	

	---------------------------- 
	-- Se devuelven los datos -- 
	---------------------------- 
	Select @Facturable as Remisionado, @FolioRemision as Folio, @sMensaje as Mensaje , IsNull(@iMontoDisponible, 0) as MontoDisponible 



	------------------ VALIDACION FINAL 
	if Exists ( Select top 1 * From #tmpDistribucion__General Where Procesado = 0 ) 
	Begin 
		--   	Select IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Procesado, sum(cantidadVendida) as Cantidad  
		Select @TipoDeRemision as TipoDeRemision, @TipoDispensacion as TipoDispensacion, @IdTipoProducto as IdTipoProducto, * 
		From #tmpDistribucion__General Where Procesado = 0 

		If not exists ( Select Name From sysobjects (noLock) Where Name like 'RptRemision_NoProcesado' and xType = 'U' )  
		Begin 
			Select top 0 
				0 as TipoDeRemision, 0 as TipoDispensacion, 0 as IdTipoProducto, 
				cast('' as varchar(10)) as IdFuenteFinanciamiento, cast('' as varchar(10)) as IdFinanciamiento, 
				cast('' as varchar(20)) as ClaveSSA, 0 as Procesado, 0 as Cantidad  		 
			Into RptRemision_NoProcesado 
		End 

		Insert Into RptRemision_NoProcesado ( TipoDeRemision, TipoDispensacion, IdTipoProducto, IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Procesado, Cantidad )
		Select 
			@TipoDeRemision as TipoDeRemision, @TipoDispensacion as TipoDispensacion, @IdTipoProducto as IdTipoProducto, 
			IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Procesado, Cantidad  
		From #tmpDistribucion__General Where Procesado = 0 


		-- drop table RptRemision_NoProcesado__Detallado 
		If not exists ( Select Name From sysobjects (noLock) Where Name like 'RptRemision_NoProcesado__Detallado' and xType = 'U' )  
		Begin 
			Select Top 0 
				IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
				IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
				ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
				CantidadVendida as Cantidad, Cantidad_Agrupada as CantidadVendida,  
				TasaIva, SubTotal_SinGrabar, SubTotal_Grabado, 
				Iva, Importe 
			Into RptRemision_NoProcesado__Detallado 
			From #tmpLotes_NoProcesado (NoLock) 
			Order By Keyx 
		End 

		Insert Into RptRemision_NoProcesado__Detallado 
		Select 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
			IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
			ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
			CantidadVendida as Cantidad, Cantidad_Agrupada as CantidadVendida,  
			TasaIva, SubTotal_SinGrabar, SubTotal_Grabado, 
			Iva, Importe 	 
		From #tmpLotes_NoProcesado (NoLock) 
		Order By Keyx

	End 

End 
Go--#SQL 


