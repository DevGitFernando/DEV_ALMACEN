--------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Periodos' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_Periodos
Go--#SQL
  
/* 

	Exec spp_FACT_Remisiones_Periodos  
		'001', '11', '0001', '1', '0002', '0006', '0001', '0001', [  ],  
		'2013-01-22', '2013-07-22', '0', '0003', 'NONE', '02', '0', 'E0B9A5DCDD73', '0', ''   

*/ 
  
Create Proc spp_FACT_Remisiones_Periodos 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001', @TipoDeRemision smallint = 1,
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0010', 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0001', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2017-03-31',
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '02', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = 'TEST', 
	@TipoDispensacion int = 0, @ClaveSSA varchar(1000) = ''   
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint, 
		@iMontoDisponible numeric(14,4),
		@FolioRemision varchar(10), 
		@SubTotalSinGrabar numeric(14,4), 
		@SubTotalGrabado numeric(14,4), 
		@Iva numeric(14,4), 
		@Total numeric(14,4),		
		@iOpcionFactura smallint,
		@sSql varchar(8000), 
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
	Set @ClaveSSA = IsNull(@ClaveSSA, '') 
	Set @ClaveSSA = ltrim(rtrim(@ClaveSSA)) 
	Set @Facturable = 0 

	

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
	-- Se obtienen los Folios del periodo seleccionado --
	-----------------------------------------------------
	Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioCierre, C.FechaCorte 
	Into #tmpFoliosCierres
	From Ctl_CierresDePeriodos C(NoLock) 
	Inner Join FACT_Informacion_Proceso_Facturacion F(NoLock) On( C.IdEmpresa = F.IdEmpresa And C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia And C.FolioCierre = F.FolioCierre ) 
	Where C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado And F.Identificador = @Identificador
		And Convert( varchar(10), C.FechaCorte, 120 ) Between @FechaInicial And @FechaFinal 


--	select * from #tmpFoliosCierres  


	--------------------------------------------------------------
	-- Se obtienen los Folios de venta del periodo seleccionado --
	--------------------------------------------------------------
	Select V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar  
	Into #tmpVentas
	From VentasEnc V(NoLock)
	Inner Join #tmpFoliosCierres C(NoLock) 
		On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioCierre = C.FolioCierre ) 
	Where V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
	
	----- Validar los programas seleccionados 
	Update F Set Procesar = 1 
	From #tmpVentas F 
	Inner Join #tmpProgramasDeAtencion P On ( F.IdPrograma = P.IdPrograma and F.IdSubPrograma = P.IdSubPrograma ) 
	
	Delete From #tmpVentas Where Procesar = 0 
	--------------------------------------------------------------	
	
--		spp_FACT_Remisiones_Periodos  	
	
----------------------------------------------------
-- Se obtienen los detalles de las ventas --
----------------------------------------------------
-------------------------- Se crea la tabla temporal
	Select	Top 0 L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
			V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
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
			cast(0 as numeric(14, 4)) as Cantidad_Agrupada 
	Into #tmpLotes  
	From VentasDet_Lotes L(NoLock) 
	Inner Join #tmpVentas V(NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
	Where -- ClaveLote Not Like '%*%' 
		  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 
		  

	If @TipoDeRemision = 1 
	  Begin
		-- Se obtienen los Lotes de "Insumos"
		Insert Into #tmpLotes
		Select	L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma,
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
				L.CantidadVendida, 0 as Cantidad_Agrupada  
		From VentasDet_Lotes L (NoLock) 
		Inner Join #tmpVentas V (NoLock) 
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		Where -- ClaveLote Not Like '%*%' 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 
			  and L.EnRemision_Insumo = 0 and L.RemisionFinalizada = 0 
	  End 
	Else
	  Begin
		-- Se obtienen los Lotes de "Administracion"
		Insert Into #tmpLotes
		Select	L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma,
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
				L.CantidadVendida, 0 as Cantidad_Agrupada  
		From VentasDet_Lotes L(NoLock) 
		Inner Join #tmpVentas V(NoLock) 
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		Where -- ClaveLote Like '%*%' 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion  
			  and L.EnRemision_Admon = 0 and L.RemisionFinalizada = 0 
	  End
	  
	  
--	select * from #tmpLotes   	  	  
--		spp_FACT_Remisiones_Periodos  	

	  
	-------------------------------------------
	-- Se obtiene la Clave SSA y la Tasa Iva --
	-------------------------------------------
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
	
	Update L Set 
		IdClaveSSA_P = P.IdClaveSSA_Sal, ClaveSSA_P = P.ClaveSSA, 
		IdClaveSSA = P.IdClaveSSA_Sal, 
		ClaveSSA = P.ClaveSSA, 
		ContenidoPaquete_ClaveSSA = P.ContenidoPaquete_ClaveSSA, 
		IdTipoProducto = P.IdTipoProducto, TasaIva = P.TasaIva, 
		Cantidad_Agrupada = round( (CantidadVendida / (P.ContenidoPaquete_ClaveSSA * 1.0) ), 4)    
	From #tmpLotes L(NoLock) 
	Inner Join #vw_Productos_CodigoEAN P(NoLock) On ( L.IdProducto = P.IdProducto And L.CodigoEAN = P.CodigoEAN ) 


	------------------------------------- 
	--------------- Reemplazo de Claves 
	Update L Set Relacionada = 1, 
		IdClaveSSA = P.IdClaveSSA_Relacionada, 
		ClaveSSA = P.ClaveSSA, ContenidoPaquete_ClaveSSA = P.ContenidoPaquete, 
		Cantidad_Agrupada = round( (CantidadVendida / (P.ContenidoPaquete * 1.0) ), 4)    
	From #tmpLotes L(NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P(NoLock) On ( L.IdEstado = P.IdEstado and L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 


--	Select * From #tmpLotes Where IdProducto in ( 00017883, 00017336 ) 

--	Select * From #tmpLotes Where Relacionada = 1 

	------------------------------------- 



	
----------- Se obtienen las claves de Insumos ó Administracion   
	Select 
		@IdEstado as IdEstado, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, ltrim(rtrim(ClaveSSA)) as ClaveSSA, 
		0 as Agrupacion, 
		cast(0 as numeric(14, 4)) as Precio, 
		cast(0 as numeric(14, 4)) as PrecioUnitario, 		
		cast(-1 as numeric(14, 4)) as TasaIva 
	Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento 

	Update L Set Precio = P.Precio, PrecioUnitario = P.PrecioUnitario  
	From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L(NoLock) 
	Inner Join vw_Claves_Precios_Asignados P (NoLock) On( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  


	---- Determinar las claves de administracion 
	If @TipoDeRemision = 2 
	Begin 
		Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		
		Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		Select 
			@IdEstado as IdEstado, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
			ltrim(rtrim(ClaveSSA)) as ClaveSSA, Agrupacion, 0, 0, 0   	 
		From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA (NoLock) 
		Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento 	
		
		Update L Set 
			Precio = P.Costo, 
			PrecioUnitario = P.CostoUnitario, 			
			TasaIva = P.TasaIva 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA P (NoLock) On( L.ClaveSSA = P.ClaveSSA )
		Where P.IdFuenteFinanciamiento = @IdFuenteFinanciamiento And P.IdFinanciamiento = @IdFinanciamiento		
		
		
		--------- Sustituir el agrupamiento en los Lotes 
		Update L Set ContenidoPaquete_ClaveSSA = C.Agrupacion 
		From  #tmpLotes L 
		Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C On ( L.ClaveSSA = C.ClaveSSA ) 
		
	End  
	

--	select @TipoDeRemision 
--	select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 	
	
	If @ClaveSSA <> '' and @ClaveSSA <> '*' 
	   Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA Where ClaveSSA <> @ClaveSSA			
----------- Se obtienen las claves de Insumos ó Administracion   


--	select distinct IdTipoProducto, @IdTipoProducto from #tmpLotes  
--		spp_FACT_Remisiones_Periodos  	

	---------------------------------------------------------------------------------------------
	-- Se eliminan de la tabla temporal aquellos productos que no pertenecen al Tipo de Insumo 
	Delete From #tmpLotes Where IdTipoProducto <> @IdTipoProducto 
	
	---------------------------------------------------------------------------------------------------- 
	-- Se eliminan de la tabla temporal aquellos productos cuya clave no pertenesca al financiamiento 
	Delete From #tmpLotes Where ClaveSSA Not In ( Select ClaveSSA From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) )    


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


	--------------------------------------------------------------
	-- Se insertan los datos en la tabla final de procesamiento -- 
	--------------------------------------------------------------
	Select	IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, 
			@IdFuenteFinanciamiento as IdFuenteFinanciamiento, @IdFinanciamiento as IdFinanciamiento, 
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
	Into #tmpLotesProceso  
	From #tmpLotes (NoLock)  
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
	From #tmpLotesProceso(NoLock) Where Facturable = 1 Order By Keyx Desc ) 

	-- Se obtienen los totales de cada concepto.
	Select	@SubTotalSinGrabar = IsNull( Sum( SubTotal_SinGrabar), 0 ), @SubTotalGrabado = IsNull( Sum(SubTotal_Grabado), 0 ), 
			@Iva = IsNull( Sum(Iva), 0 ), @Total = IsNull( Sum(Importe), 0 )
	From #tmpLotesProceso (NoLock)
	Where Facturable = 1 



	
---------------------------------------------------------------------------------------------------------------------------- 
-- Se insertan los datos agrupados en la tabla final de procesamiento 
	Select	
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
			Identity(int, 1, 1) as Keyx  
	Into #tmpLotesProceso_Agrupado   
	From #tmpLotesProceso (NoLock)  
	Where Facturable = 1  
	Group by 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 
			IdFuenteFinanciamiento, IdFinanciamiento,  IdPrograma, IdSubPrograma, 
			ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, TasaIva
	Order By PrecioClave Desc, CantidadVendida Desc  
	
	-- Se obtiene el Importe
	Update #tmpLotesProceso_Agrupado Set Importe = ( SubTotal_SinGrabar + SubTotal_Grabado + Iva ) 	
	
	Select	@SubTotalSinGrabar = IsNull( Sum( SubTotal_SinGrabar), 0 ), @SubTotalGrabado = IsNull( Sum(SubTotal_Grabado), 0 ), 
			@Iva = IsNull( Sum(Iva), 0 ), @Total = IsNull( Sum(Importe), 0 )
	From #tmpLotesProceso_Agrupado (NoLock) 

-- Se insertan los datos agrupados en la tabla final de procesamiento 	
---------------------------------------------------------------------------------------------------------------------------- 

--- Validar el esquema de facturacion 
	If @iMontoFacturar = 0 or @Total > 0 
		Set @Facturable = 1 
	
	Select @Facturable = (case when count(*) > 0 then 1 else 0 end)
	From #tmpLotesProceso(NoLock)
	Where Facturable = 1	
		
	---------------------------------------
	-- Se obtiene el Folio de la Factura --
	---------------------------------------
	If @Facturable = 0 
		Begin 
			Select @FolioRemision = '', @sMensaje = 'No se encontro información para generar remisión' 
		End 
	Else    
		Begin
			-- Se inserta el Encabezado de la Factura
			Exec spp_Mtto_FACT_Remisiones @IdEmpresa, @IdEstado, @IdFarmaciaGenera, @FolioRemision Output, @TipoDeRemision, @EsExcedente, 
				@IdPersonalFactura, @IdFuenteFinanciamiento, @IdFinanciamiento, @SubTotalSinGrabar, @SubTotalGrabado, @Iva, @Total, 
				@Observaciones, @iOpcionFactura, @IdTipoProducto, @TipoDispensacion 

			Set @sMensaje = 'La Remisión se genero satisfactoriamente con el Folio: ' + @FolioRemision  

			-- Se inserta el Detalle de la Factura
			Insert Into FACT_Remisiones_Detalles ( IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
												IdFuenteFinanciamiento, IdFinanciamiento, 
												IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
												PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
												TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, @FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
					ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
					CantidadVendida, 
					Cantidad_Agrupada as CantidadVendida,  
					TasaIva, SubTotal_SinGrabar, SubTotal_Grabado, 
					Iva, Importe 
			From #tmpLotesProceso(NoLock)
			Where Facturable = 1 
			Order By Keyx

			-- Se inserta el Concentrado de la Factura
			Insert Into FACT_Remisiones_Concentrado ( IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, 
												IdFuenteFinanciamiento, IdFinanciamiento, 
												IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
												PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
												TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, @FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento,  
					IdPrograma, IdSubPrograma, 
					ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
					Sum(CantidadVendida), 
					Sum(Cantidad_Agrupada) as CantidadVendida, 					
					max(TasaIva), 
					Sum(SubTotal_SinGrabar), Sum(SubTotal_Grabado), Sum(Iva), Sum(Importe)
			From #tmpLotesProceso_Agrupado (NoLock)   ----- AQUI   #tmpLotesProceso 
			Where Facturable = 1 
			Group By IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, IdFuenteFinanciamiento, IdFinanciamiento,  
					IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario 

			-- Se inserta el Resumen de la Factura
			Insert Into FACT_Remisiones_Resumen ( IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, 
												IdFuenteFinanciamiento, IdFinanciamiento,  
												IdPrograma,	IdSubPrograma, ClaveSSA, 
												PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
												TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, @FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
					IdPrograma, IdSubPrograma, ClaveSSA, 
					PrecioLicitado, PrecioLicitadoUnitario, 				
					Sum(Cantidad),
					Sum(Cantidad_Agrupada),  
					max(TasaIva), 
					Sum(SubTotalSinGrabar), Sum(SubTotalGrabado), Sum(Iva), Sum(Importe)
			From FACT_Remisiones_Concentrado (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And FolioRemision = @FolioRemision
			Group By IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, 
				IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA, PrecioLicitado, PrecioLicitadoUnitario  

			-------------------------------------------------
			-- Se eliminan las ventas que ya se facturaron -- 
			-------------------------------------------------
			-- Esta parte ya no se hace ---------------------
	----		Delete L
	----		From VentasDet_Lotes L(NoLock) 
	----		Inner Join #tmpLotesProceso V(NoLock) 
	----			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
	----				And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
	----		Where V.Facturable = 1  


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


End 
Go--#SQL

