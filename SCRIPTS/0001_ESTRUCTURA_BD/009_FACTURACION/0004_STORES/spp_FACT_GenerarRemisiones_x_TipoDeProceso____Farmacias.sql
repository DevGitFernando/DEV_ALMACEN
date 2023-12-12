	/* 
	Begin tran 

		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias  @FoliosVenta = '34' 
		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias  @FoliosVenta = '37' 

		rollback tran 
		commit tran  
	*/ 

------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias 
Go--#SQL 
    
Create Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias   
( 	
	@TipoProcesoRemision int = 0,   --- 1 => Farmacia FF | 2 ==> Farmacia incrementos FF | Almacenes  ==> 3 
	@Beneficiarios_x_Jurisdiccion int = 0, 
	----   1 ==> Agrupar por Beneficirios Principales ( Beneficiarios de Facturacion ) 

	@NivelInformacion_Remision int = 2,   
		--- 1 => General (Primer nivel de informacion) | 
		--- 2 ==> Farmacia FF (Segundo nivel de informacion) | 
		--- 3 ==> Ventas directas por Jurisdiccion 
		--- 4 ==> Ventas directas por Jurisdiccion & Documentos Importes   

	@ProcesarParcialidades int = 0,		 ---- ====> Exclusivo SSH Primer Nivel 
	@ProcesarCantidadesExcedentes int = 0,    
	@AsignarReferencias int = 1, 

	@Procesar_Producto bit = 1, @Procesar_Servicio bit = 1, @Procesar_Servicio_Consigna bit = 0, 
	@Procesar_Medicamento bit = 1, @Procesar_MaterialDeCuracion bit = 1, 
	@Procesar_Venta bit = 1, @Procesar_Consigna bit = 1, 

	----@Procesar_Producto bit = 0, @Procesar_Servicio bit = 0, @Procesar_Servicio_Consigna bit = 0, 
	----@Procesar_Medicamento bit = 0, @Procesar_MaterialDeCuracion bit = 0, 
	----@Procesar_Venta bit = 0, @Procesar_Consigna bit = 0, 

	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '0001', @TipoDeRemision smallint = 1, 
	@IdFarmacia varchar(max) = [  ], 
	
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0011', 
	@Lista__IdClienteIdSubCliente varchar(max) = [  ], 

	@IdFuenteFinanciamiento varchar(4) = '0003', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2017-08-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '01', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = '', 
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(max) = [  ], 
	@FechaDeRevision int = 1,  ----   1 ==> Fecha registro | 2 Fecha receta | 3 Ventas directas 
	@FoliosVenta varchar(max) = [   ], 
	@TipoDeBeneficiario int = 3, -- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén>  
	@Aplicar_ImporteDocumentos int = 0, 
	@EsProgramasEspeciales int = 0, 
	@IdBeneficiario varchar(max) = [  ], 
	@IdBeneficiario_MayorIgual varchar(max) = [  ], @IdBeneficiario_MenorIgual varchar(max) = [  ],
	@Remision_General int = 0, @ClaveSSA_ListaExclusion varchar(max) = [ ],    

	@EsRelacionFacturaPrevia bit = 0, 
	@FacturaPreviaEnCajas int = 0, 
	@Serie varchar(10) = '', @Folio int = 0, @EsRelacionMontos bit = 0, 
	@Accion varchar(100) = '', 

	@Procesar_SoloClavesReferenciaRemisiones bit = 0, 
	@ExcluirCantidadesConDecimales bit = 0,  
	@Separar__Venta_y_Vales bit = 0, 
	@TipoDispensacion_Venta int = 0,			-- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	@Criterio_FiltroFecha___Vales int = 1,		-- 1 ==> Fecha de emision | 2 ==> Fecha de registro de vale canjeado ( Fecha registro de Venta ) 
	@EsRemision_Complemento bit = 0,     -- 0 == > Remisiones normales | 1 ==> Remisiones de Incremento ó Diferenciador 
	@MostrarResultado bit = 0, 
	@Remisiones_x_Farmacia bit = 1,   				-- 0 ==> Remisión concentrada | 1 ==> Remisiones separadas por Farmacia  

	@EsRelacionDocumentoPrevio bit = 0, 
	@FolioRelacionDocumento varchar(6) = '' 

) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@GUID varchar(100), 
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
	@sSql_aux varchar(max), 
	@sFiltro varchar(max), 
	@Facturable bit, 
	@bEsFarmacia bit, 
	@bEsAlmacen bit,  
	@OrigenInsumo int, 
	@EsProcesoConcentrado bit, 

	@bExisteInformacion_A_Procesar int,  
	@Procesar_Asignacion_De_Documentos int, 
	@sIdBeneficiario_Proceso varchar(20), 
	@sNombreBeneficiario_Proceso varchar(500), 
	@sListaDeFolios varchar(max), 
	@sFolioVenta_Proceso varchar(20), 
	@sIdPrograma varchar(10), 
	@sIdSubPrograma varchar(10), 
	@sFecha_Menor varchar(20), 
	@sFecha_Mayor varchar(20), 
	@sFiltroFecha varchar(500), 
	@sFiltroTabla_Base varchar(1000),  
	@sTablaProceso varchar(500),   

	@IdFarmacia_Item varchar(max), 
	@IdFarmacia_Lista varchar(max), 
	@sFiltro_ClavesSSA varchar(max), 
	@sFiltro_FacturasRelacionadas varchar(max)  

	/**	Tipo de Remision **
		1.- Insumos
		2.- Administracion
	*/

	--print 'Farmacias : ' + @IdFarmacia 

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
	Set @sSql_aux = '' 
	Set @sFiltro = '' 
	Set @ClaveSSA = IsNull(@ClaveSSA, '') 
	Set @ClaveSSA = ltrim(rtrim(@ClaveSSA)) 
	Set @Facturable = 0 
	Set @bEsFarmacia = 1 
	Set @bEsAlmacen = 1 
	Set @OrigenInsumo = @TipoDispensacion + 1 -- 0 ==> Venta | 1 Consigna  
	Set @Procesar_Asignacion_De_Documentos = 0 
	Set @bExisteInformacion_A_Procesar = 0 

	Set @sIdBeneficiario_Proceso = '' 
	Set @sNombreBeneficiario_Proceso = '' 
	Set @sListaDeFolios = '' 
	Set @sFolioVenta_Proceso = '' 
	Set @sIdPrograma = ''  
	Set @sIdSubPrograma = ''  
	Set @sFecha_Menor = '' 
	Set @sFecha_Mayor = '' 
	Set @sFiltroFecha = ''   
	Set @sFiltroTabla_Base = '' 
	Set @EsProcesoConcentrado = 0 
	Set @sFiltro_FacturasRelacionadas = '' 

	Set @IdFarmacia_Item = '' 
	Set @IdFarmacia_Lista = '' 
	----------  Almacen, forzar por folios 
	----Set @FechaDeRevision = 3 


	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000000' + @IdEstado, 2) 
	Set @IdFarmaciaGenera = right('00000000' + @IdFarmaciaGenera, 4)
	

	If @Identificador = ''  
		Set @Identificador = cast(NEWID() as varchar(max)) 


	---------------------------------------------------------------------------------------------------------------------------- 
	--------------------------- Preparar catalogos generales  
	Exec spp_FACT___Preparar_Catalogos_Remisiones  
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @ForzarActualizacion = '0' 
	--------------------------- Preparar catalogos generales  
	---------------------------------------------------------------------------------------------------------------------------- 

	Set @sTablaProceso = ' VentasDet_Lotes ' 
	If @EsRemision_Complemento = 1 Set @sTablaProceso = ' FACT_Incremento___VentasDet_Lotes ' 

	
	--------------- PREPARAR CATALOGO DE CLAVES 
	Select Top 0 *, 0 as Relacionado 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
	--Where -- ClaveSSA like '%010.000.3674.00%' 
	--	ClaveSSA like '%' + @ClaveSSA + '%' 
	
	Set @sSql = 
		'Insert Into #vw_Productos_CodigoEAN ' + char(13) +  
		'Select *, 0 as Relacionado ' + char(13) + 	 
		'From vw_Productos_CodigoEAN ' --+ @sFiltro_ClavesSSA 
	Exec(@sSql) 



	----------------------------------------------- DOCUMENTOS DE COMPROBACION 
	If @EsRelacionFacturaPrevia = 1 or @EsRelacionDocumentoPrevio = 1 
	Begin 

		Select 
			D.ClaveSSA 
		into #tmp__ClavesFacturaAnticipada 
		From FACT_Remisiones__RelacionFacturas D (NoLock) 
		Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
		Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio 
		Group by D.ClaveSSA 
		Having sum(D.Cantidad_Facturada - D.Cantidad_Distribuida) > 0  


		----Select @Serie as Serie, @Folio as Folio 
		----select * 
		----From FACT_Remisiones__RelacionFacturas D (NoLock) 
		----Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
		----Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio 


		If @EsRelacionDocumentoPrevio = 1 
		Begin 
			Delete From #tmp__ClavesFacturaAnticipada 
			Insert into #tmp__ClavesFacturaAnticipada ( ClaveSSA ) 
			Select 
				D.ClaveSSA  
			From FACT_Remisiones__RelacionDocumentos D (NoLock) 
			Inner Join FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.FolioRelacion = @FolioRelacionDocumento -- and E.Serie = @Serie and E.Folio = @Folio 
			Group by D.ClaveSSA 
			Having sum(D.Cantidad_A_Comprobar - D.Cantidad_Distribuida) > 0  			
		End 

		--SELECT * FROM #tmp__ClavesFacturaAnticipada 

		---select count(*) as vw_Productos_CodigoEAN_01 from #vw_Productos_CodigoEAN  
		-------------------- ACTUALIZAR EL LISTADO DE PRODUCTOS 
		Delete From #vw_Productos_CodigoEAN   


		Insert Into #vw_Productos_CodigoEAN 
		Select L.*, 0 as Relacionado	 
		From vw_Productos_CodigoEAN L 
		Inner Join #tmp__ClavesFacturaAnticipada F On ( L.ClaveSSA = F.ClaveSSA )  


		Insert Into #vw_Productos_CodigoEAN 
		Select P.*, 1 as Relacionado 	 
		From vw_Productos_CodigoEAN P  
		Inner Join vw_Relacion_ClavesSSA_Claves L (NoLock) 
			On ( 
				L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente 
				and L.IdClaveSSA_Relacionada = P.IdClaveSSA_Sal   
				and P.Status = 'A' 
				) 
		Inner Join #tmp__ClavesFacturaAnticipada F On ( L.ClaveSSA = F.ClaveSSA )  
		Where Not Exists 
			( 
				Select * 
				From #vw_Productos_CodigoEAN X 
				Where P.IdProducto = X.IdProducto and P.CodigoEAN = X.CodigoEAN 
			) 
		-------------------- ACTUALIZAR EL LISTADO DE PRODUCTOS 

		--select count(*) as vw_Productos_CodigoEAN_02 from #vw_Productos_CodigoEAN  
		--select * from #vw_Productos_CodigoEAN 


	End 
	----------------------------------------------- DOCUMENTOS DE COMPROBACION 


	---------------------------------------------------------------------------------------------------------------------------- 
	--------------------------- Información de catalogos 
	Select Top 0 *, (case when EsAlmacen = 0 then 1 else 0 end) as EsFarmacia   
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado and IdTipoUnidad <> '006' 

	Set @sFiltro = '' 
	If @IdFarmacia = '' or @IdFarmacia = '*' 	
	Begin 
		Set @EsProcesoConcentrado = 1 	
	End 
	
	--print @IdFarmacia 
	If @IdFarmacia <> '' and @IdFarmacia  <> '*' 
	Begin 
	   -- Delete From #tmpFarmacias Where IdFarmacia <> RIGHT('0000' + @IdFarmacia, 4)     
	   --Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + char(39) + @IdFarmacia + char(39) +  ' ) '  
	   Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + @IdFarmacia + ' ) '  
	End 
	
	Set @sSql = 
		'Insert Into #tmpFarmacias ' + char(10) + 
		'Select *, (case when EsAlmacen = 0 then 1 else 0 end) as EsFarmacia  '  + char(10) + 
		'From vw_Farmacias '  + char(10) + 
		'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdTipoUnidad <> ' + char(39) + '006' + char(39) + '  ' + @sFiltro   
	Exec(@sSql) 
	--Print @sSql 

	Set @sFiltro_ClavesSSA = '' 
	If @ClaveSSA <> '' 
	Begin 
		Set @sFiltro_ClavesSSA = ' and P.ClaveSSA in ( ' + @ClaveSSA + ' ) '   
	End 

	--select * from #tmpFarmacias 

	Set @IdFarmacia_Lista = '' 
	Declare #cursor_ListaFarmacias 
	Cursor For 
		Select IdFarmacia 
		From #tmpFarmacias (NoLock) 
		Order By IdFarmacia  
	Open #cursor_ListaFarmacias
	FETCH NEXT FROM #cursor_ListaFarmacias Into @IdFarmacia_Item 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 	
			
			If @IdFarmacia_Lista = '' 
				Set @IdFarmacia_Lista = char(39) + @IdFarmacia_Item + char(39) 
			Else 
				Set @IdFarmacia_Lista = @IdFarmacia_Lista + ', ' + char(39) + @IdFarmacia_Item + char(39) 


			FETCH NEXT FROM #cursor_ListaFarmacias Into  @IdFarmacia_Item  
		END 
	Close #cursor_ListaFarmacias 
	Deallocate #cursor_ListaFarmacias 



	------------------------ Determinar si es un proceso concentrado 
	Select @EsProcesoConcentrado = (case when count(*) > 1 then 1 else 0 end) From #tmpFarmacias (nolock) 
	Set @EsProcesoConcentrado = IsNull(@EsProcesoConcentrado, 0) 
	------------------------ Determinar si es un proceso concentrado 

	
	--------------------------- Validar claves de facturas relacionadas  
	----Select top 0 cast('' as varchar(30)) as ClaveSSA 
	----Into #tmp_Claves__FacturaRelacionada 
	
	----If @EsRelacionFacturaPrevia = 1 
	----Begin 
	----	Set @sFiltro_FacturasRelacionadas = 'Inner Join #tmp_Claves__FacturaRelacionada FR (NoLock) On ( P.ClaveSSA = FR.ClaveSSA ) ' + char(13) 

	----	Insert Into #tmp_Claves__FacturaRelacionada ( ClaveSSA ) 
	----	Select ClaveSSA  
	----	From FACT_Remisiones__RelacionFacturas 
	----	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and 
	----		(Cantidad_Facturada - Cantidad_Distribuida) > 0 
	----	Group by ClaveSSA 
	----End 

	------Select * From #tmp_Claves__FacturaRelacionada 

	--------------------------- Validar claves de facturas relacionadas  



	--------------------------- Información de catalogos 
	---------------------------------------------------------------------------------------------------------------------------- 

	/* 
	Begin tran 

		spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias  

		rollback tran  
		commit tran  

	*/ 

--------------------- Obtener los datos de Cliente-SubCliente reqeridos 	
	Select IdCliente, IdSubCliente 
	Into #tmp_ClientesSubClientes 
	From CatSubClientes (NoLock) 
	Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 

	If @Lista__IdClienteIdSubCliente <> '' 
	Begin 
		Delete From #tmp_ClientesSubClientes 

	   Set @sSql = 
		'Insert Into #tmp_ClientesSubClientes ( IdCliente, IdSubCliente ) ' + char(13) + 
		'Select IdCliente, IdSubCliente '  + char(13) +  
		'From CatSubClientes (NoLock) '  + char(13) +  
		'Where IdCliente + IdSubCliente in ( ' + @Lista__IdClienteIdSubCliente + ' ) '  + char(13) 
	   Exec(@sSql) 

	End 

	--Select @Lista__IdClienteIdSubCliente, * from #tmp_ClientesSubClientes 
--------------------- Obtener los datos de Cliente-SubCliente reqeridos 	


	----------------------------------------------------------------------------------------------------------------------------
	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	----------------------------------------------------

	--Print '......' + @FoliosVenta  
	Set @sFiltro = '' 
	If ltrim(rtrim(@FoliosVenta)) <>  '' 
	Begin 
		Set @sFiltro = ' And cast(V.FolioVenta as int) in ( ' + @FoliosVenta + ' ) '  	
	End 



	-------------------- Se obtienen los detalles de las ventas --  

	-------------------------- Se crea la tabla temporal 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia,  -- V.FolioVenta, V.FechaRegistro, C.IdBeneficiario, 
		V.IdCliente, V.IdSubCliente, -- V.IdPrograma, V.IdSubPrograma, -- cast('' as varchar(10)) as IdSubFArmacia, 
		0 as Venta, 0 as Consigna, 
		0 as Medicamento, 0 as MaterialDeCuracion, 
		0 as Remisionar_Insumo, 0 as Remisionar_Servicio, 0 as Remisionar_Servicio__Consigna,   
		cast('' as varchar(max)) as FoliosDeVenta 
	Into #tmpInformacion_PorRemisionar 
	From VentasEnc V (NoLock)  
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Inner Join VentasInformacionAdicional C (NoLock) 
		On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado 
		and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
		and convert(varchar(10), C.FechaReceta, 120) Between @FechaInicial and @FechaFinal   
		and 1 = 0  ----- Crear la tabla vacia 

	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia,  -- V.FolioVenta, V.FechaRegistro, C.IdBeneficiario, 
		V.IdCliente, V.IdSubCliente, -- V.IdPrograma, V.IdSubPrograma, -- cast('' as varchar(10)) as IdSubFArmacia, 
		0 as Venta, 0 as Consigna, 
		0 as Medicamento, 0 as MaterialDeCuracion, 
		0 as Remisionar_Insumo, 0 as Remisionar_Servicio, 0 as Remisionar_Servicio__Consigna,   
		cast('' as varchar(max)) as FoliosDeVenta 
	Into #tmpInformacion_PorRemisionar_Detalles  
	From VentasEnc V (NoLock)  
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Inner Join VentasInformacionAdicional C (NoLock) 
		On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado 
		and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
		and convert(varchar(10), C.FechaReceta, 120) Between @FechaInicial and @FechaFinal   
		and 1 = 0  ----- Crear la tabla vacia 

	-------------------------- Se crea la tabla temporal 


	--		spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias 	  
	Set @sFiltroFecha = '	and convert(varchar(10), V.FechaRegistro, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal  + char(39) + ' ' + char(10) 
	Set @sFiltroTabla_Base = '' 
	
	If @FechaDeRevision = 2   ---- Fecha de receta  
	Begin 
		Set @sFiltroFecha = '	and convert(varchar(10), C.FechaReceta, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal  + char(39) + ' ' + char(10) 	
		Set @sFiltroTabla_Base =
			'Inner Join VentasInformacionAdicional C (NoLock) ' + char(10) + 	
			'	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) ' + char(10) 
	End 


	-- Select @TipoDeRemision, @TipoDispensacion, * from #tmpFarmacias 
	Set @sSql = '' 
	Set @sSql = ' ' + char(10) + 	
		'Insert Into #tmpInformacion_PorRemisionar ' + char(10) + 	
		'Select * ' + char(10) + 
		'From ' + char(10) + 
		'( ' + char(10) + 
			'Select ' + char(10) + 	
			'	V.IdEmpresa, V.IdEstado, V.IdFarmacia, ' + char(10) + 	
			'	V.IdCliente, V.IdSubCliente,  ' + char(10) + 	
			'	cast((case when L.ClaveLote not Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end) as bit) as Venta, ' + char(10) + 	
			'	cast((case when L.ClaveLote Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end) as bit) as Consigna, ' + char(10) + 	
			'' + char(10) + 	
			'	cast((case when P.TipoDeClave <> ' + char(39) + '01' + char(39) + ' then 1 else 0 end) as bit) as Medicamento, ' + char(10) + 	
			'	cast((case when P.TipoDeClave = ' + char(39) + '01' + char(39) + ' then 1 else 0 end) as bit) as MaterialDeCuracion, ' + char(10) + 	
		
			'	cast((case when ( L.CantidadRemision_Insumo < L.CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Insumo,  ' + char(10) + 	
			'	cast((case when ( L.CantidadRemision_Admon < L.CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Servicio,  ' + char(10) + 	

			'	cast(0 as bit) as Remisionar_Servicio__Consigna,  ' + char(10) + 	
			'	cast('+ char(39) + char(39) + ' as varchar(max)) as FoliosDeVenta ' + char(10) + 	
			'From VentasEnc V (NoLock)  ' + char(10) + 	
			'Inner Join #tmp_ClientesSubClientes C (NoLock) On ( V.IdCliente = C.IdCliente and V.IdSubCliente = C.IdSubCliente ) ' + char(10) + 
			'Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) ' + char(10) + 	
			@sFiltroTabla_Base + 
			'Inner Join ' + @sTablaProceso + ' L (NoLock)  ' + char(10) + 	
			'	On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) ' + char(10) + 	
			'Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ' + @sFiltro_ClavesSSA + ' ) ' + char(10) + 
			@sFiltro_FacturasRelacionadas + 
			'Where ' + char(10) + 	
			'	V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and V.IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + char(10) + 	
			@sFiltroFecha + 
			'	and L.CantidadVendida > 0 ' + '    ' + @sFiltro + char(10) + 
			'	and (	( L.CantidadVendida - L.CantidadRemision_Admon ) > 0 or ( L.CantidadVendida - L.CantidadRemision_Insumo ) > 0 ) ' + char(10) + 
		') T ' + char(10) + 
		'Group by ' + char(10) + 
		'	IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, ' + char(10) + 
		'	Venta, Consigna, Medicamento, MaterialDeCuracion, Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, FoliosDeVenta '  + char(10) 

	--Set @sSql_aux = @sSql + 
	--	'Group by ' + char(10) + 	
	--	'	V.IdEmpresa, V.IdEstado, V.IdFarmacia,  ' + char(10) + 	
	--	'	V.IdCliente, V.IdSubCliente,  ' + char(10) + 	
	--	'	(case when L.ClaveLote not Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
	--	'	(case when L.ClaveLote Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
	--	'	(case when P.TipoDeClave <> ' + char(39) + '01' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
	--	'	(case when P.TipoDeClave = ' + char(39) + '01' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
		
	--	'	(case when ( L.CantidadRemision_Insumo < L.CantidadVendida ) then 1 else 0 end),  ' + char(10) + 	
	--	'	(case when ( L.CantidadRemision_Admon < L.CantidadVendida ) then 1 else 0 end)   ' + char(10) + 	
				
	--	--'	L.EnRemision_Insumo, L.EnRemision_Admon ' + char(10) + 	

	--	'Order By V.IdCliente, V.IdSubCliente ' + char(10) 
	
	Exec(@sSql) 
	--Exec(@sSql_aux) 	
	--Print @sSql 
	--select 'POR AQUI' as TOT, * from #tmpInformacion_PorRemisionar 
	--return 

	--delete from #tmpInformacion_PorRemisionar

	-------------------- Concentrar informacion 
	-------------------- Concentrar informacion 



	Update R Set Remisionar_Servicio__Consigna = (Consigna & Remisionar_Servicio) 
	From #tmpInformacion_PorRemisionar R 


	--		spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias 	  

	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	---------------------------------------------------------------------------------------------------------------------------- 



	--		spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias 	  

	------- REVISAR 
	--Select IdFarmacia from #tmpInformacion_PorRemisionar  group by IdFarmacia 

	---- Resumen  
	--Select * from #tmpInformacion_PorRemisionar  

	-------------------------------------- APLICAR LOS FILTROS GENERALES DEL PROCESO 
	If @Procesar_Producto = 0 Update F Set Remisionar_Insumo = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_Servicio = 0 Update F Set Remisionar_Servicio = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_Servicio_Consigna = 0 Update F Set Remisionar_Servicio__Consigna = 0 From #tmpInformacion_PorRemisionar F   
		
	If @Procesar_Venta = 0 Update F Set Venta = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_Consigna = 0 Update F Set Consigna = 0 From #tmpInformacion_PorRemisionar F 

	If @Procesar_Medicamento = 0 Update F Set Medicamento = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_MaterialDeCuracion = 0 Update F Set MaterialDeCuracion = 0 From #tmpInformacion_PorRemisionar F 

	Update F Set Remisionar_Insumo = 0 From #tmpInformacion_PorRemisionar F Where Consigna = 1 and Venta = 0 
	-------------------------------------- APLICAR LOS FILTROS GENERALES DEL PROCESO 

	--Select * from #tmpInformacion_PorRemisionar  
	---- Resumen  


	--Select * from #tmpInformacion_PorRemisionar  

	--Select * 
	--From #tmpInformacion_PorRemisionar T 
	--Where ( Medicamento = 1 or MaterialDeCuracion = 1 ) and ( Venta = 1 or Consigna = 1 ) 
	--	and ( Remisionar_Insumo = 1 or Remisionar_Servicio = 1 or Remisionar_Servicio__Consigna = 1 ) 


---		spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias  


	----select * from #tmp_Beneficiarios_Folios 
	----Select count(*) as Beneficiarios  From #tmp_Beneficiarios_Folios 	
	--Select @Criterio_FiltroFecha___Vales, count(*) as Ventas From #tmpInformacion_PorRemisionar 


	--------------------------------------------------------- Listado de beneficiarios 
	----------------------------- CONCENTRADO DE FARMACIAS 
	If @EsProcesoConcentrado = 1 -- and 1 = 0 
	Begin 

		--Select 
		--		Procesar_Producto = (case when sum(Remisionar_Insumo) > 0 then 1 else 0 end), 
		--		Procesar_Servicio = (case when sum(Remisionar_Servicio) > 0 then 1 else 0 end), 
		--		Procesar_Servicio_Consigna = (case when sum(Remisionar_Servicio__Consigna) > 0 then 1 else 0 end), 
		--		Procesar_Medicamento = (case when sum(Medicamento) > 0 then 1 else 0 end), 
		--		Procesar_MaterialDeCuracion = (case when sum(MaterialDeCuracion) > 0 then 1 else 0 end), 
		--		Procesar_Venta = (case when sum(Venta) > 0 then 1 else 0 end), 
		--		Procesar_Consigna = (case when sum(Consigna) > 0 then 1 else 0 end) 
		--From #tmpInformacion_PorRemisionar (NoLock) 

		Print 'GENERACION CONCENTRADA' 
		Set @sIdBeneficiario_Proceso = '' 
		Set @sNombreBeneficiario_Proceso = ''     
		Set @sListaDeFolios = @FoliosVenta 
		--Print cast(@Remisiones_x_Farmacia as varchar(100)) + '      ' + cast(@TipoProcesoRemision as varchar(100)) + '      ' + cast(@TipoDeRemision as varchar(100)) 

		--select @IdFarmacia as ListaDeFarmacias 
		--Set @IdFarmacia = '' 
		--select @Lista__IdClienteIdSubCliente 

		--Select * From #tmpInformacion_PorRemisionar (NoLock) 

		--Select 
		--		@Procesar_Producto = sum(Remisionar_Insumo), @Procesar_Servicio = sum(Remisionar_Servicio), @Procesar_Servicio_Consigna = sum(Remisionar_Servicio__Consigna), 
		--		@Procesar_Medicamento = sum(Medicamento), @Procesar_MaterialDeCuracion = sum(MaterialDeCuracion), 
		--		@Procesar_Venta = sum(Venta), @Procesar_Consigna = sum(Consigna) 
		--From #tmpInformacion_PorRemisionar (NoLock) 


		Select 
				@Procesar_Producto = (case when sum(Remisionar_Insumo) > 0 then 1 else 0 end), 
				@Procesar_Servicio = (case when sum(Remisionar_Servicio) > 0 then 1 else 0 end), 
				@Procesar_Servicio_Consigna = (case when sum(Remisionar_Servicio__Consigna) > 0 then 1 else 0 end), 
				@Procesar_Medicamento = (case when sum(Medicamento) > 0 then 1 else 0 end), 
				@Procesar_MaterialDeCuracion = (case when sum(MaterialDeCuracion) > 0 then 1 else 0 end), 
				@Procesar_Venta = (case when sum(Venta) > 0 then 1 else 0 end), 
				@Procesar_Consigna = (case when sum(Consigna) > 0 then 1 else 0 end) 
		From #tmpInformacion_PorRemisionar (NoLock) 
		
		--Select 
		--		@Procesar_Producto as Procesar_Producto, 
		--		@Procesar_Servicio as Procesar_Servicio, 
		--		@Procesar_Servicio_Consigna as Procesar_Servicio_Consigna, 
		--		@Procesar_Medicamento as Procesar_Medicamento, 
		--		@Procesar_MaterialDeCuracion as Procesar_MaterialDeCuracion, 
		--		@Procesar_Venta as Procesar_Venta, 
		--		@Procesar_Consigna as Procesar_Consigna 



		--select * From #tmpInformacion_PorRemisionar (NoLock) 

		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso 

				@Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 

				@NivelInformacion_Remision = @NivelInformacion_Remision, 
				@TipoProcesoRemision = @TipoProcesoRemision, 
							
				@ProcesarParcialidades = @ProcesarParcialidades,		 ---- ====> Exclusivo SSH Primer Nivel 
				@ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
				@AsignarReferencias = @AsignarReferencias, 

				@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna, 
				@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
				@Procesar_Venta = @Procesar_Venta, @Procesar_Consigna = @Procesar_Consigna, 

				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado , 
				@IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
				
				@IdFarmacia = @IdFarmacia_Lista, ----@IdFarmacia, 
				
				@IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
				@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
				@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, 
				@iMontoFacturar = @iMontoFacturar,  @IdPersonalFactura = @IdPersonalFactura, 
				@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, 
				@EsExcedente = @EsExcedente, @Identificador = @Identificador, 
				@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
				@ClaveSSA = @ClaveSSA, 
				@FechaDeRevision = @FechaDeRevision, @FoliosVenta = @sListaDeFolios, 
				@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
				@Referencia_Beneficiario = @sIdBeneficiario_Proceso, @Referencia_BeneficiarioNombre = @sNombreBeneficiario_Proceso,       
				@ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
				@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
				@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
				@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
				@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado  = @MostrarResultado, 
				@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  

	End 


	----------------------------- DETALLADO POR FARMACIAS 
	If @EsProcesoConcentrado = 0 --and 1 = 0 
	Begin 
		--Print 'GENERACION DETALLADA POR FARMACIA' 


		----Select 
		----	IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, 	
		----	Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
		----	Medicamento, MaterialDeCuracion, Venta, Consigna,  
		----	@FechaInicial as FechaMenor, @FechaFinal as FechaMayor,  2 as TipoDeBeneficiario, 
		----	(Remisionar_Insumo + Remisionar_Servicio + Medicamento + MaterialDeCuracion + Venta + Consigna) as ExisteInformacion_A_Procesar
		----From #tmpInformacion_PorRemisionar T 
		----Where ( Medicamento = 1 or MaterialDeCuracion = 1 ) and ( Venta = 1 or Consigna = 1 ) 
		----	and ( Remisionar_Insumo = 1 or Remisionar_Servicio = 1 or Remisionar_Servicio__Consigna = 1 ) 


		Declare #cursorBeneficiarios  
		Cursor For 
			Select 
				IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, 	
				Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
				Medicamento, MaterialDeCuracion, Venta, Consigna,  
				@FechaInicial as FechaMenor, @FechaFinal as FechaMayor,  2 as TipoDeBeneficiario, 
				(Remisionar_Insumo + Remisionar_Servicio + Medicamento + MaterialDeCuracion + Venta + Consigna) as ExisteInformacion_A_Procesar
			From #tmpInformacion_PorRemisionar T 
			Where ( Medicamento = 1 or MaterialDeCuracion = 1 ) and ( Venta = 1 or Consigna = 1 ) 
				and ( Remisionar_Insumo = 1 or Remisionar_Servicio = 1 or Remisionar_Servicio__Consigna = 1 ) 
		Open #cursorBeneficiarios 
		FETCH NEXT FROM #cursorBeneficiarios Into 
				@IdEmpresa, @IdEstado, @IdFarmaciaGenera, @IdFarmacia, 
				@Procesar_Producto, @Procesar_Servicio, @Procesar_Servicio_Consigna, 
				@Procesar_Medicamento, @Procesar_MaterialDeCuracion, 
				@Procesar_Venta, @Procesar_Consigna,  
				@sFecha_Menor, @sFecha_Mayor, @TipoDeBeneficiario, @bExisteInformacion_A_Procesar  
			WHILE @@FETCH_STATUS = 0 
			BEGIN 
				Set @sListaDeFolios = @FoliosVenta 
				Set @sSql = replicate('-', 100) 
				--Print @sSql 

				-- Set @sListaDeFolios = ''  
				Set @GUID = cast(NEWID() as varchar(max)) 
				--Print '' 
				--Print @GUID 
				Set @sSql = '' 
				Set @sSql = 'BN' + @IdFarmacia + '  ' 
				Set @sSql = @sSql + ' FI' + cast(@sFecha_Menor as varchar(10)) 	
				Set @sSql = @sSql + ' FF' + cast(@sFecha_Mayor as varchar(10)) + '  ' 


				--Set @sSql = @sSql + ' P' + cast(@sIdPrograma as varchar(1)) 	
				--Set @sSql = @sSql + 'S' + cast(@sIdSubPrograma as varchar(1)) + '  ' 	

				Set @sSql = @sSql + ' PP' + cast(@Procesar_Producto as varchar(1)) 	
				Set @sSql = @sSql + ' PS' + cast(@Procesar_Servicio as varchar(1)) 
				Set @sSql = @sSql + ' PSC' + cast(@Procesar_Servicio_Consigna as varchar(1)) 

				Set @sSql = @sSql + ' PMD' + cast(@Procesar_Medicamento as varchar(1)) 	
				Set @sSql = @sSql + ' PMC' + cast(@Procesar_MaterialDeCuracion as varchar(1)) 
				Set @sSql = @sSql + ' PVT' + cast(@Procesar_Venta as varchar(1)) 	
				Set @sSql = @sSql + ' PCN' + cast(@Procesar_Consigna as varchar(1)) 

				If @bExisteInformacion_A_Procesar = 0 Set @sSql = @sSql + '         ' + ' No existe información para procesar ' 

				-- Set @sSql = @sSql + char(10) + '		Folios: ' + @sListaDeFolios    
				--Print '' 
				--Print @sSql 


				------		spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias 	  

				If @bExisteInformacion_A_Procesar > 0 
					Begin  
						--Print '---- PROCESO ==> spp_FACT_GenerarRemisiones_x_TipoDeProceso ' + ' ==>  '  + cast(@TipoProcesoRemision as varchar(20)) 

						Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso 

								@Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 

								@NivelInformacion_Remision = @NivelInformacion_Remision, 
								@TipoProcesoRemision = @TipoProcesoRemision, 
							
								@ProcesarParcialidades = @ProcesarParcialidades,		 ---- ====> Exclusivo SSH Primer Nivel 
								@ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
								@AsignarReferencias = @AsignarReferencias, 

								@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna, 
								@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
								@Procesar_Venta = @Procesar_Venta, @Procesar_Consigna = @Procesar_Consigna, 

								@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado , 
								@IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, @IdFarmacia = @IdFarmacia, 
								@IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
								@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
								@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
								@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, 
								@iMontoFacturar = @iMontoFacturar,  @IdPersonalFactura = @IdPersonalFactura, 
								@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, 
								@EsExcedente = @EsExcedente, @Identificador = @Identificador, 
								@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
								@ClaveSSA = @ClaveSSA, 
								@FechaDeRevision = @FechaDeRevision, @FoliosVenta = @sListaDeFolios, 
								@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
								@Referencia_Beneficiario = @sIdBeneficiario_Proceso, @Referencia_BeneficiarioNombre = @sNombreBeneficiario_Proceso,       
								@ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
								@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
								@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
								@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
								@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
								@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, 
								@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  

					End 


				Set @bExisteInformacion_A_Procesar = 0 

				--Print '' 
				--Print '' 

				FETCH NEXT FROM #cursorBeneficiarios Into 
					@IdEmpresa, @IdEstado, @IdFarmaciaGenera, @IdFarmacia, 
					@Procesar_Producto, @Procesar_Servicio, @Procesar_Servicio_Consigna, 
					@Procesar_Medicamento, @Procesar_MaterialDeCuracion, 
					@Procesar_Venta, @Procesar_Consigna,  
					@sFecha_Menor, @sFecha_Mayor, @TipoDeBeneficiario, @bExisteInformacion_A_Procesar 
			END	 
		Close #cursorBeneficiarios 
		Deallocate #cursorBeneficiarios 	
	End 
	--------------------------------------------------------- Listado de beneficiarios 


	--------------------------------------- OBTENER LOS FOLIOS DE VENTA POR BENEFICIARIO 
	---------------------------------------------------------------------------------------------------------------------------- 


End 
Go--#SQL 

