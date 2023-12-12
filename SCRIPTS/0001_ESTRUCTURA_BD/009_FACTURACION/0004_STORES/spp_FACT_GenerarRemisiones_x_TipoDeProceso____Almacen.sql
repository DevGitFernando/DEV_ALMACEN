	/* 
	Begin tran 

		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  @FoliosVenta = '34' 
		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  @FoliosVenta = '37' 

		rollback tran 
		commit tran  
	*/ 

------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen 
Go--#SQL 
    
Create Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen   
( 	
	@TipoProcesoRemision int = 3,   --- 1 => Farmacia FF | 2 ==> Farmacia incrementos FF | Almacenes  ==> 3 
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
	@IdFarmacia varchar(max) = [ ], 

	@IdCliente varchar(4) = '0042', @IdSubCliente varchar(4) = '0002', 
	@Lista__IdClienteIdSubCliente varchar(max) = [  ], 

	@IdFuenteFinanciamiento varchar(4) = '0006', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2020-08-01', @FechaFinal varchar(10) = '2020-08-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '01', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = '', 
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(max) = [  ], 
	@FechaDeRevision int = 3, ----   1 ==> Fecha registro | 2 Fecha receta | 3 Ventas directas 
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
	@Remisiones_x_Farmacia bit = 1,  				-- 0 ==> Remisión concentrada | 1 ==> Remisiones separadas por Farmacia  

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
	@sFiltro_Fechas varchar(max), 
	@Facturable bit, 
	@bEsFarmacia bit, 
	@bEsAlmacen bit,  
	@OrigenInsumo int,
	@Procesar_Asignacion_De_Documentos int, 
	@sIdBeneficiario_Proceso varchar(20), 
	@sNombreBeneficiario_Proceso varchar(500), 
	@sListaDeFolios varchar(max), 
	@sFolioVenta_Proceso varchar(20), 
	@sIdPrograma varchar(10), 
	@sIdSubPrograma varchar(10), 
	@sFecha_Menor varchar(20), 
	@sFecha_Mayor varchar(20),  
	@bEjecutar bit, 
	@sTablaProceso varchar(500),   

	@IdFarmacia_Item varchar(max), 
	@IdFarmacia_Lista varchar(max), 
	@sFiltro_FacturasRelacionadas varchar(max)  


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
	Set @sSql_aux = '' 
	Set @sFiltro = '' 
	Set @sFiltro_Fechas = '' 
	Set @ClaveSSA = IsNull(@ClaveSSA, '') 
	Set @ClaveSSA = ltrim(rtrim(@ClaveSSA)) 
	Set @Facturable = 0 
	Set @bEsFarmacia = 1 
	Set @bEsAlmacen = 1 
	Set @OrigenInsumo = @TipoDispensacion + 1 -- 0 ==> Venta | 1 Consigna  
	Set @Procesar_Asignacion_De_Documentos = 0 

	Set @sIdBeneficiario_Proceso = '' 
	Set @sNombreBeneficiario_Proceso = '' 
	Set @sListaDeFolios = '' 
	Set @sFolioVenta_Proceso = '' 
	Set @sIdPrograma = ''  
	Set @sIdSubPrograma = ''  
	Set @sFecha_Menor = '' 
	Set @sFecha_Mayor = '' 
	Set @bEjecutar = 1  

	Set @IdFarmacia_Item = '' 
	Set @IdFarmacia_Lista = '' 
	------  Almacen, forzar por folios 
	Set @FechaDeRevision = 3 
	

	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000000' + @IdEstado, 2) 
	Set @IdFarmaciaGenera = right('00000000' + @IdFarmaciaGenera, 4)

	Set @sFiltro_FacturasRelacionadas = '' 

	--Select @TipoDeBeneficiario as TipoDeBeneficiario  

	If @Identificador = ''  
		Set @Identificador = cast(NEWID() as varchar(max)) 

	---------------------------------------------------------------------------------------------------------------------------- 
	--------------------------- Preparar catalogos generales  
	--Exec spp_FACT___Preparar_Catalogos_Remisiones  
	--	@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @ForzarActualizacion = '0' 
	--------------------------- Preparar catalogos generales  
	---------------------------------------------------------------------------------------------------------------------------- 

	Set @sTablaProceso = ' VentasDet_Lotes ' 
	If @EsRemision_Complemento = 1 Set @sTablaProceso = ' FACT_Incremento___VentasDet_Lotes ' 


	--Select @Remisiones_x_Farmacia as Remisiones_x_Farmacia___CEDIS   

	---------------------------------------------------------------------------------------------------------------------------- 
	--------------------------- Información de catalogos 
	Select Top 0 *, (case when EsAlmacen = 0 then 1 else 0 end) as EsFarmacia   
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado and IdTipoUnidad = '006' 

	Set @sFiltro = '' 
	If @IdFarmacia <> '' and @IdFarmacia  <> '*' 
	Begin 
	   -- Delete From #tmpFarmacias Where IdFarmacia <> RIGHT('0000' + @IdFarmacia, 4)     
	   Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + @IdFarmacia + ' ) '  
	End 
	
	Set @sSql = 
		'Insert Into #tmpFarmacias ' + char(10) + 
		'Select *, (case when EsAlmacen = 0 then 1 else 0 end) as EsFarmacia  '  + char(10) + 
		'From vw_Farmacias '  + char(10) + 
		'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdTipoUnidad = ' + char(39) + '006' + char(39) + '  ' + @sFiltro   
	Exec(@sSql) 
	--print @sSql 


	select * 
	Into #tmp_vw_Productos_CodigoEAN
	from vw_Productos_CodigoEAN

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

	--------------------------- Validar claves de facturas relacionadas  
	Select top 0 cast('' as varchar(30)) as ClaveSSA 
	Into #tmp_Claves__FacturaRelacionada 
	
	If @EsRelacionFacturaPrevia = 1 
	Begin 
		-------------------- REVISAR ESTE PROCESO CON CALMA :p
		Set @EsRelacionFacturaPrevia = 1 
		
		
		----Set @sFiltro_FacturasRelacionadas = 'Inner Join #tmp_Claves__FacturaRelacionada FR (NoLock) On ( P.ClaveSSA = FR.ClaveSSA ) ' + char(13) 

		----Insert Into #tmp_Claves__FacturaRelacionada ( ClaveSSA ) 
		----Select ClaveSSA  
		----From FACT_Remisiones__RelacionFacturas 
		----Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and 
		----	(Cantidad_Facturada - Cantidad_Distribuida) > 0 
		----Group by ClaveSSA 

	End 

	--Select * From #tmp_Claves__FacturaRelacionada 

	--------------------------- Validar claves de facturas relacionadas  


	
	Select Top 0 
		B.IdEstado, B.IdFarmacia,  B.IdCliente, B.IdSubCliente, B.IdBeneficiario, B.IdBeneficiario as IdBeneficiario_Principal
	Into #tmpBeneficiarios  
	From CatBeneficiarios B (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( B.IdEstado = F.IdEstado and B.IdFarmacia = F.IdFarmacia ) 
	-- Where B.IdCliente = @IdCliente And B.IdSubCliente = @IdSubCliente 

	Set @sSql = 
		'Insert Into #tmpBeneficiarios ' + char(10) + 
		'Select ' + char(10) + 
		'	B.IdEstado, B.IdFarmacia,  B.IdCliente, B.IdSubCliente, B.IdBeneficiario, B.IdBeneficiario as IdBeneficiario_Principal ' + char(10) + 
		'From CatBeneficiarios B (NoLock)	' + char(10) + 
		'Inner Join #tmpFarmacias F (NoLock) On ( B.IdEstado = F.IdEstado and B.IdFarmacia = F.IdFarmacia ) 	' + char(10) + 
		'Inner Join #tmp_ClientesSubClientes C (NoLock) On ( B.IdCliente = C.IdCliente and B.IdSubCliente = C.IdSubCliente ) ' + char(10)  
		--'Where B.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And B.IdSubCliente = ' + char(39) + @IdSubCliente + char(39)  
	

	---- @IdFuenteFinanciamiento 
	if ( @NivelInformacion_Remision = 3 or @NivelInformacion_Remision = 4 or @Beneficiarios_x_Jurisdiccion = 1 ) and @TipoDeBeneficiario = 3 
	Begin 
		Set @sSql = 
			'Insert Into #tmpBeneficiarios ' + char(10) + 
			'Select ' + char(10) + 
			'	B.IdEstado, B.IdFarmacia,  B.IdCliente, B.IdSubCliente, B.IdBeneficiario_Relacionado as IdBeneficiario, B.IdBeneficiario as IdBeneficiario_Principal ' + char(10) + 
			'From FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados B (NoLock)	' + char(10) + 
			'Inner Join #tmpFarmacias F (NoLock) On ( B.IdEstado = F.IdEstado and B.IdFarmacia = F.IdFarmacia ) 	' + char(10) + 
			'Inner Join #tmp_ClientesSubClientes C (NoLock) On ( B.IdCliente = C.IdCliente and B.IdSubCliente = C.IdSubCliente ) ' + char(10) 
			--'Where B.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And B.IdSubCliente = ' + char(39) + @IdSubCliente + char(39)  
	End 



	Set @sFiltro = '' 
	If @IdBeneficiario <> '' 
	Begin 
		Set @IdBeneficiario = REPLACE(@IdBeneficiario, char(39), '') 
		---- Set @sFiltro = ' and cast(IdBeneficiario as int) In ( ' + char(39) + @IdBeneficiario + char(39) +  ' ) '  
		Set @sFiltro = ' and cast(IdBeneficiario as int) In ( ' + @IdBeneficiario +  ' ) '  
	End 

	If @IdBeneficiario_MayorIgual <> '' or @IdBeneficiario_MenorIgual <> '' 
	Begin 
		Set @IdBeneficiario_MayorIgual = REPLACE(@IdBeneficiario_MayorIgual, char(39), '') 
		Set @IdBeneficiario_MenorIgual = REPLACE(@IdBeneficiario_MenorIgual, char(39), '') 

		If @IdBeneficiario_MayorIgual <> '' and @IdBeneficiario_MenorIgual <> '' 
		Begin
			Set @sFiltro = @sFiltro + ' and cast(IdBeneficiario as int) >= ' + char(39) + @IdBeneficiario_MayorIgual + char(39) +  '  '  
			Set @sFiltro = @sFiltro + ' and cast(IdBeneficiario as int) <= ' + char(39) + @IdBeneficiario_MenorIgual + char(39) +  '  '  
		End 

		If @IdBeneficiario_MayorIgual <> '' and @IdBeneficiario_MenorIgual = '' 
		Begin
			Set @sFiltro = @sFiltro + ' and cast(IdBeneficiario as int) >= ' + char(39) + @IdBeneficiario_MayorIgual + char(39) +  '  '  
		End 

		If @IdBeneficiario_MayorIgual = '' and @IdBeneficiario_MenorIgual <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' and cast(IdBeneficiario as int) <= ' + char(39) + @IdBeneficiario_MenorIgual + char(39) +  '  '  
		End 
	End 



	Set @sSql = @sSql + @sFiltro  
	Print @sSql 
	Exec(@sSql) 



--	select * from #tmpBeneficiarios 
--		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  

--	select * from #tmpBeneficiarios 

	--------------------------- Información de catalogos 
	---------------------------------------------------------------------------------------------------------------------------- 

	/* 
	Begin tran 

		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  

		rollback tran  
		commit tran  

	*/ 



	----------------------------------------------------------------------------------------------------------------------------
	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	----------------------------------------------------


	Set @sFiltro = '' 
	Set @sFiltro_Fechas = '	and convert(varchar(10), V.FechaRegistro, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal  + char(39) + ' ' + char(10)  
	If ltrim(rtrim(@FoliosVenta)) <>  '' 
	Begin 
		Set @FoliosVenta = replace(@FoliosVenta, char(39), '') 
		Set @sFiltro = ' And cast(V.FolioVenta as int) in ( ' + @FoliosVenta + ' ) '  	
		Set @sFiltro_Fechas = '' 
	End 


	-- Se obtienen los detalles de las ventas --  
	-------------------------- Se crea la tabla temporal 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.FechaRegistro, C.IdBeneficiario, C.IdBeneficiario as IdBeneficiario_Principal, 
		V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
		--cast('' as varchar(20)) as CodigoEAN, 
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
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FechaRegistro, IdBeneficiario, IdBeneficiario_Principal, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		cast('' as varchar(10)) as TipoDeClave, 
		cast('' as varchar(50)) as ClaveLote, 
		cast(0 as numeric(14,4)) as CantidadVendida, 
		cast(0 as numeric(14,4)) as CantidadRemision_Insumo, 
		cast(0 as numeric(14,4)) as CantidadRemision_Admon
	Into #tmpInformacion_PorRemisionar_Detalles  
	From #tmpInformacion_PorRemisionar
	Where 1 = 0  


	--		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen 	  

	--select * from #tmpBeneficiarios   

	Set @sSql = 
			'Insert Into #tmpInformacion_PorRemisionar_Detalles' + char(10) + 
			'Select ' + char(10) + 	
			'	V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, ' + char(39) + '' + char(39) + ' , ' + char(39) + '' + char(39) + ', ' + char(10) + 	
			'	V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, ' + char(10) + 	

			'	P.TipoDeClave, L.ClaveLote, L.CantidadVendida, L.CantidadRemision_Insumo, L.CantidadRemision_Admon  ' + char(10) + 

			----'	cast((case when L.ClaveLote not Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end) as bit) as Venta, ' + char(10) + 	
			----'	cast((case when L.ClaveLote Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end) as bit) as Consigna, ' + char(10) + 	
			----'' + char(10) + 	
			----'	cast((case when P.TipoDeClave <> ' + char(39) + '01' + char(39) + ' then 1 else 0 end) as bit) as Medicamento, ' + char(10) + 	
			----'	cast((case when P.TipoDeClave = ' + char(39) + '01' + char(39) + ' then 1 else 0 end) as bit) as MaterialDeCuracion, ' + char(10) + 	
			----'	cast((case when ( L.CantidadRemision_Insumo < L.CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Insumo,  ' + char(10) + 	
			----'	cast((case when ( L.CantidadRemision_Admon < L.CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Servicio,  ' + char(10) + 	
			------'	cast((case when L.EnRemision_Insumo = 0 then 1 else 0 end) as bit) as Remisionar_Insumo,  ' + char(10) + 	
			------'	cast((case when L.EnRemision_Admon = 0 then 1 else 0 end) as bit) as Remisionar_Servicio,  ' + char(10) + 	
			----'	cast(0 as bit) as Remisionar_Servicio__Consigna,  ' + char(10) + 	
			----'	cast('+ char(39) + char(39) + ' as varchar(max)) as FoliosDeVenta ' + char(10) + 	
			'From VentasEnc V (NoLock)  ' + char(10) + 	
			'Inner Join #tmp_ClientesSubClientes CS (NoLock) On ( V.IdCliente = CS.IdCliente and V.IdSubCliente = CS.IdSubCliente ) ' + char(10) + 
			'Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) ' + char(10) + 	
			'Inner Join VentasDet D (NoLock)  ' + char(10) + 	
			'		On ( V.IdEmpresa = D.IdEmpresa And V.IdEstado = D.IdEstado And V.IdFarmacia = D.IdFarmacia And V.FolioVenta = D.FolioVenta ) ' + char(10) + 	
			'Inner Join ' + @sTablaProceso + '  L (NoLock)  ' + char(10) + 	
			'	On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioVenta = L.FolioVenta and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) ' + char(10) + 	
			'Inner Join #tmp_vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN )  ' + char(10) + 	
			@sFiltro_FacturasRelacionadas + 
			'Where 1 = 1 and' + char(10) + 	
			'	V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and V.IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + char(10) + 	
			--'	and V.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And V.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + ' ' + char(10) + 	
			--'	and convert(varchar(10), V.FechaRegistro, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal  + char(39) + ' ' + char(10) + 	
			--'	and V.FolioVenta like ' + char(39) + '1%' + char(39) + char(10) +
			@sFiltro_Fechas + 
			-- '	and ( L.EnRemision_Insumo = 0 or L.EnRemision_Admon = 0 ) ' + char(10) + 	
			'	and L.CantidadVendida > 0 ' + '    ' + char(10) +
			'	and ( ( L.CantidadRemision_Insumo < L.CantidadVendida ) or ( L.CantidadRemision_Admon < L.CantidadVendida ) ) ' + @sFiltro + char(10)  	
	Exec(@sSql) 


	Update V Set IdBeneficiario = C.IdBeneficiario, IdBeneficiario_Principal = B.IdBeneficiario_Principal
	from #tmpInformacion_PorRemisionar_Detalles V 
	Inner Join VentasInformacionAdicional C (NoLock)  	
		On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
	Inner Join #tmpBeneficiarios B (NoLock) 	
		On ( C.IdEstado = B.IdEstado And C.IdFarmacia = B.IdFarmacia 
			And V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and C.IdBeneficiario = B.IdBeneficiario )  	



	Insert Into #tmpInformacion_PorRemisionar 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FechaRegistro, IdBeneficiario, IdBeneficiario_Principal,  	
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		cast((case when ClaveLote not Like '%*%' then 1 else 0 end) as bit) as Venta, 
		cast((case when ClaveLote Like '%*%' then 1 else 0 end) as bit) as Consigna, 
		cast((case when TipoDeClave <> '01' then 1 else 0 end) as bit) as Medicamento, 
		cast((case when TipoDeClave = '01' then 1 else 0 end) as bit) as MaterialDeCuracion, 
		cast((case when ( CantidadRemision_Insumo < CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Insumo, 
		cast((case when ( CantidadRemision_Admon < CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Servicio, 
		cast(0 as bit) as Remisionar_Servicio__Consigna, 
		'' as FoliosDeVenta 
	From #tmpInformacion_PorRemisionar_Detalles 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FechaRegistro, IdBeneficiario, IdBeneficiario_Principal,  	
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		cast((case when ClaveLote not Like '%*%' then 1 else 0 end) as bit), 
		cast((case when ClaveLote Like '%*%' then 1 else 0 end) as bit), 
		cast((case when TipoDeClave <> '01' then 1 else 0 end) as bit), 
		cast((case when TipoDeClave = '01' then 1 else 0 end) as bit), 
		cast((case when ( CantidadRemision_Insumo < CantidadVendida ) then 1 else 0 end) as bit), 
		cast((case when ( CantidadRemision_Admon < CantidadVendida ) then 1 else 0 end) as bit) 
		



	---------- Select @TipoDeRemision, @TipoDispensacion, * from #tmpFarmacias 
	----Set @sSql = '' 
	----Set @sSql = ' ' + char(10) + 	
	----	'Insert Into #tmpInformacion_PorRemisionar ' + char(10) + 	
	----	'Select * ' + char(10) + 
	----	'From ' + char(10) + 
	----	'( ' + char(10) + 
	----		'Select ' + char(10) + 	
	----		'	V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, C.IdBeneficiario, B.IdBeneficiario_Principal,  ' + char(10) + 	
	----		'	V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, ' + char(10) + 	
	----		-- '	L.CodigoEAN, ' + char(10) + 
	----		'	cast((case when L.ClaveLote not Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end) as bit) as Venta, ' + char(10) + 	
	----		'	cast((case when L.ClaveLote Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end) as bit) as Consigna, ' + char(10) + 	
	----		'' + char(10) + 	
	----		'	cast((case when P.TipoDeClave <> ' + char(39) + '01' + char(39) + ' then 1 else 0 end) as bit) as Medicamento, ' + char(10) + 	
	----		'	cast((case when P.TipoDeClave = ' + char(39) + '01' + char(39) + ' then 1 else 0 end) as bit) as MaterialDeCuracion, ' + char(10) + 	
	----		'	cast((case when ( L.CantidadRemision_Insumo < L.CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Insumo,  ' + char(10) + 	
	----		'	cast((case when ( L.CantidadRemision_Admon < L.CantidadVendida ) then 1 else 0 end) as bit) as Remisionar_Servicio,  ' + char(10) + 	
	----		--'	cast((case when L.EnRemision_Insumo = 0 then 1 else 0 end) as bit) as Remisionar_Insumo,  ' + char(10) + 	
	----		--'	cast((case when L.EnRemision_Admon = 0 then 1 else 0 end) as bit) as Remisionar_Servicio,  ' + char(10) + 	
	----		'	cast(0 as bit) as Remisionar_Servicio__Consigna,  ' + char(10) + 	
	----		'	cast('+ char(39) + char(39) + ' as varchar(max)) as FoliosDeVenta ' + char(10) + 	
	----		'From VentasEnc V (NoLock)  ' + char(10) + 	
	----		'Inner Join #tmp_ClientesSubClientes CS (NoLock) On ( V.IdCliente = CS.IdCliente and V.IdSubCliente = CS.IdSubCliente ) ' + char(10) + 
	----		'Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) ' + char(10) + 	
	----		'Inner Join VentasInformacionAdicional C (NoLock) ' + char(10) + 	
	----		'	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) ' + char(10) + 	
	----		'Inner Join #tmpBeneficiarios B (NoLock) ' + char(10) + 	
	----		'	On ( C.IdEstado = B.IdEstado And C.IdFarmacia = B.IdFarmacia ' + char(10) + 	
	----		'		And V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and C.IdBeneficiario = B.IdBeneficiario ) ' + char(10) + 	
	----		'Inner Join VentasDet D (NoLock)  ' + char(10) + 	
	----		'		On ( V.IdEmpresa = D.IdEmpresa And V.IdEstado = D.IdEstado And V.IdFarmacia = D.IdFarmacia And V.FolioVenta = D.FolioVenta ) ' + char(10) + 	
	----		'Inner Join ' + @sTablaProceso + '  L (NoLock)  ' + char(10) + 	
	----		'	On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioVenta = L.FolioVenta and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) ' + char(10) + 	
	----		'Inner Join #tmp_vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN )  ' + char(10) + 	
	----		@sFiltro_FacturasRelacionadas + 
	----		'Where 1 = 1 and' + char(10) + 	
	----		'	V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and V.IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + char(10) + 	
	----		--'	and V.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And V.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + ' ' + char(10) + 	
	----		--'	and convert(varchar(10), V.FechaRegistro, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal  + char(39) + ' ' + char(10) + 	
	----		--'	and V.FolioVenta like ' + char(39) + '1%' + char(39) + char(10) +
	----		@sFiltro_Fechas + 
	----		-- '	and ( L.EnRemision_Insumo = 0 or L.EnRemision_Admon = 0 ) ' + char(10) + 	
	----		'	and L.CantidadVendida > 0 ' + '    ' + char(10) +
	----		'	and ( ( L.CantidadRemision_Insumo < L.CantidadVendida ) or ( L.CantidadRemision_Admon < L.CantidadVendida ) ) ' + @sFiltro + char(10) + 	
	----	') T ' + char(10) + 
	----	'Group by ' + char(10) + 
	----	'	IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FechaRegistro, IdBeneficiario, IdBeneficiario_Principal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, ' + char(10) + 
	----	'	Venta, Consigna, Medicamento, MaterialDeCuracion, Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, FoliosDeVenta '  + char(10) 		
		
	------Set @sSql_aux = @sSql + 
	------'Group by ' + char(10) + 	
	------'	V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, convert(varchar(10), V.FechaRegistro, 120), C.IdBeneficiario, B.IdBeneficiario_Principal, ' + char(10) + 	
	------'	V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma,  ' + char(10) + 	
	-------- '	L.CodigoEAN, ' + char(10) + 
	------'	(case when L.ClaveLote not Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
	------'	(case when L.ClaveLote Like ' + char(39) + '%*%' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
	------'	(case when P.TipoDeClave <> ' + char(39) + '01' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
	------'	(case when P.TipoDeClave = ' + char(39) + '01' + char(39) + ' then 1 else 0 end), ' + char(10) + 	
	------'	(case when ( L.CantidadRemision_Insumo < L.CantidadVendida ) then 1 else 0 end),  ' + char(10) + 	
	------'	(case when ( L.CantidadRemision_Admon < L.CantidadVendida ) then 1 else 0 end)   ' + char(10) + 	
	------'Order By B.IdBeneficiario_Principal, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, V.FolioVenta ' + char(10) 
	
	
	------Exec(@sSql) 
	------Exec(@sSql_aux) 	
	------Print @sSql 
	----select 'POR AQUI' as T, * from #tmpInformacion_PorRemisionar 
	
	---delete from #tmpInformacion_PorRemisionar  


	/* 
	Begin tran 

		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  

		rollback tran 
		commit tran  
	*/ 



	Update R Set Remisionar_Servicio__Consigna = (Consigna & Remisionar_Servicio) 
	From #tmpInformacion_PorRemisionar R 

	--select IdFarmacia, count(*)  from #tmpBeneficiarios group by IdFarmacia 
	--Select 'REVISION' as Campo, * from #tmpInformacion_PorRemisionar 



	--		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen 	  


	--Select * from #tmpInformacion_PorRemisionar  


	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	---------------------------------------------------------------------------------------------------------------------------- 



	--		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen 	  

	--select * from #tmpInformacion_PorRemisionar 

	-------------------------------------- APLICAR LOS FILTROS GENERALES DEL PROCESO 
	If @Procesar_Producto = 0 Update F Set Remisionar_Insumo = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_Servicio = 0 Update F Set Remisionar_Servicio = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_Servicio_Consigna = 0 Update F Set Remisionar_Servicio__Consigna = 0 From #tmpInformacion_PorRemisionar F   
		
	If @Procesar_Venta = 0 Update F Set Venta = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_Consigna = 0 Update F Set Consigna = 0 From #tmpInformacion_PorRemisionar F 

	If @Procesar_Medicamento = 0 Update F Set Medicamento = 0 From #tmpInformacion_PorRemisionar F 
	If @Procesar_MaterialDeCuracion = 0 Update F Set MaterialDeCuracion = 0 From #tmpInformacion_PorRemisionar F 

	Update F Set Remisionar_Insumo = 0 From #tmpInformacion_PorRemisionar F Where Consigna = 1 and Venta = 0 


	----Select 
	----	@Procesar_Producto as Procesar_Producto,  
	----	@Procesar_Servicio as Procesar_Servicio,  
	----	@Procesar_Servicio_Consigna as Procesar_Servicio_Consigna,  
	----	@Procesar_Venta as Procesar_Venta,  
	----	@Procesar_Medicamento as Procesar_Medicamento,  
	----	@Procesar_MaterialDeCuracion as Procesar_MaterialDeCuracion 

	--select * from #tmpInformacion_PorRemisionar 


	---------------------------------------------------------------------------------------------------------------------------- 
	--------------------------------------- OBTENER LOS FOLIOS DE VENTA POR BENEFICIARIO 
	Select 
		IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, 
		convert(varchar(10), min(FechaRegistro), 120) as FechaMenor, convert(varchar(10), max(FechaRegistro), 120) as FechaMayor, 
		IdBeneficiario_Principal as IdBeneficiario, cast('' as varchar(500)) as NombreBeneficiario, @TipoDeBeneficiario as TipoDeBeneficiario,  -- V.FolioVenta, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		Venta, Consigna, Medicamento, MaterialDeCuracion, Remisionar_Insumo, Remisionar_Servicio, 
		-- (Consigna & Remisionar_Servicio) as Remisionar_Servicio__Consigna, 
		Remisionar_Servicio__Consigna, 
		0 as NumeroDeFolios,   
		cast('' as varchar(max)) as FoliosDeVenta 
	Into #tmp_Beneficiarios_Folios 
	From #tmpInformacion_PorRemisionar 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, -- min(FechaRegistro), max(FechaRegistro), 
		IdBeneficiario_Principal, -- IdBeneficiario, -- V.FolioVenta, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		Venta, Consigna, Medicamento, MaterialDeCuracion, Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna    
	Order by IdEmpresa, IdEstado, IdFarmacia,  IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, FechaMenor, FechaMayor 

	
	Select Top 0 IdFarmacia, FolioVenta   
	Into ##tmp_Informacion_CEDIS 
	From #tmpInformacion_PorRemisionar 



	Update F Set NumeroDeFolios = IsNull(( Select count(*) From #tmpInformacion_PorRemisionar L (NoLock) Where L.IdBeneficiario = F.IdBeneficiario ), 0)
	From #tmp_Beneficiarios_Folios F (NoLock) 

	Update F Set NombreBeneficiario = B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre, TipoDeBeneficiario = B.TipoDeBeneficiario  
	From #tmp_Beneficiarios_Folios F (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( F.IdEstado = B.IdEstado and F.IdFarmacia = B.IdFarmacia and F.IdCliente = B.IdCliente and F.IdSubCliente = B.IdSubCliente and F.IdBeneficiario = B.IdBeneficiario ) 


	Update F Set NombreBeneficiario = BF.NombreBeneficiario, TipoDeBeneficiario = 3 
	From #tmp_Beneficiarios_Folios F (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados B (NoLock) 
		On ( F.IdEstado = B.IdEstado and F.IdFarmacia = B.IdFarmacia and F.IdCliente = B.IdCliente and F.IdSubCliente = B.IdSubCliente and F.IdBeneficiario = B.IdBeneficiario ) 
	Inner Join FACT_Fuentes_De_Financiamiento_Beneficiarios BF (NoLock) On ( B.IdFuenteFinanciamiento = BF.IdFuenteFinanciamiento and B.IdBeneficiario = Bf.IdBeneficiario ) 
	Where @NivelInformacion_Remision = 3 or @NivelInformacion_Remision = 4 or @TipoDeBeneficiario = 3  



---		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  



	-- select * from #tmp_Beneficiarios_Folios 
	-- select * from #tmpInformacion_PorRemisionar 

/* 


	@Procesar_Medicamento bit = 1, @Procesar_MaterialDeCuracion bit = 1, 
	@Procesar_Venta bit = 1, @Procesar_Consigna bit = 1, 	
*/ 



	/* 
	Begin tran 

		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  

		rollback tran 
		commit tran  
	*/ 

	Set @sIdBeneficiario_Proceso = '' 
	Set @sNombreBeneficiario_Proceso = '' 
	Set @sListaDeFolios = '' 
	Set @sFolioVenta_Proceso = '' 


	--------------------------------------------------------- Listado de beneficiarios 
	Declare #cursorBeneficiarios  
	Cursor For 
		Select 
			IdFarmacia, 
			Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
			Medicamento, MaterialDeCuracion, Venta, Consigna,  
			IdBeneficiario, IdPrograma, IdSubPrograma   
		From #tmp_Beneficiarios_Folios T 
		Order by IdBeneficiario 
	Open #cursorBeneficiarios 
	FETCH NEXT FROM #cursorBeneficiarios Into 
			@IdFarmacia_Item, 
			@Procesar_Producto, @Procesar_Servicio, @Procesar_Servicio_Consigna, 
			@Procesar_Medicamento, @Procesar_MaterialDeCuracion, 
			@Procesar_Venta, @Procesar_Consigna,  
			@sIdBeneficiario_Proceso, @sIdPrograma, @sIdSubPrograma    
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			Set @sListaDeFolios = '' 
			-- Print 'BX' + @sIdBeneficiario_Proceso   

			--select * 
			--From #tmpInformacion_PorRemisionar T 

			--------------------------------------------------------- Listado de folios x beneficiarios 						
			Declare #cursorFoliosVenta  
			Cursor For 
				Select FolioVenta 
				From #tmpInformacion_PorRemisionar T 
				Where IdFarmacia = @IdFarmacia_Item and 
					IdBeneficiario_Principal = @sIdBeneficiario_Proceso and IdPrograma = @sIdPrograma and IdSubPrograma = @sIdSubPrograma 
					and Remisionar_Insumo = @Procesar_Producto and Remisionar_Servicio = @Procesar_Servicio and Remisionar_Servicio__Consigna = Remisionar_Servicio__Consigna 
					and Medicamento = @Procesar_Medicamento and MaterialDeCuracion = @Procesar_MaterialDeCuracion and Venta = @Procesar_Venta and Consigna = @Procesar_Consigna  
				Order By FolioVenta 
			Open #cursorFoliosVenta 
			FETCH NEXT FROM #cursorFoliosVenta Into @sFolioVenta_Proceso 
				WHILE @@FETCH_STATUS = 0 
				BEGIN 
					-- Print @sFolioVenta_Proceso 
					If @sListaDeFolios = '' 
						Set @sListaDeFolios = char(39) + @sFolioVenta_Proceso + char(39) 
					else 
						Set @sListaDeFolios = @sListaDeFolios + ', ' + char(39) + @sFolioVenta_Proceso + char(39) 

				--		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen 			
					

					FETCH NEXT FROM #cursorFoliosVenta Into @sFolioVenta_Proceso    
				END	 
			Close #cursorFoliosVenta 
			Deallocate #cursorFoliosVenta 
			--------------------------------------------------------- Listado de folios x beneficiarios 						

			Update B Set FoliosDeVenta = @sListaDeFolios 
			From #tmp_Beneficiarios_Folios B 
			Where IdFarmacia = @IdFarmacia_Item and 
				IdBeneficiario = @sIdBeneficiario_Proceso and IdPrograma = @sIdPrograma and IdSubPrograma = @sIdSubPrograma 
					and Remisionar_Insumo = @Procesar_Producto and Remisionar_Servicio = @Procesar_Servicio and Remisionar_Servicio__Consigna = Remisionar_Servicio__Consigna 
					and Medicamento = @Procesar_Medicamento and MaterialDeCuracion = @Procesar_MaterialDeCuracion and Venta = @Procesar_Venta and Consigna = @Procesar_Consigna  

			FETCH NEXT FROM #cursorBeneficiarios Into 
				@IdFarmacia_Item, 
				@Procesar_Producto, @Procesar_Servicio, @Procesar_Servicio_Consigna, 
				@Procesar_Medicamento, @Procesar_MaterialDeCuracion, 
				@Procesar_Venta, @Procesar_Consigna,  
				@sIdBeneficiario_Proceso, @sIdPrograma, @sIdSubPrograma    
		END	 
	Close #cursorBeneficiarios  
	Deallocate #cursorBeneficiarios 	 
	--------------------------------------------------------- Listado de beneficiarios 


	/* 
	Begin tran 

		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen  

		rollback tran 
		commit tran  
	*/ 


	--Select 
	--	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, 	
	--	Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
	--	Medicamento, MaterialDeCuracion, Venta, Consigna,  
	--	FechaMenor, FechaMayor,  
	--	IdBeneficiario, NombreBeneficiario, TipoDeBeneficiario, IdPrograma, IdSubPrograma, FoliosDeVenta 
	--From #tmp_Beneficiarios_Folios T 


	--select * from #tmp_Beneficiarios_Folios  
	--Select count(*) as Beneficiarios  From #tmp_Beneficiarios_Folios 	
	--Select count(*) as Ventas, count(distinct IdBeneficiario) as Beneficiarios From #tmpInformacion_PorRemisionar 

	--select * From #tmpInformacion_PorRemisionar T 

	--------------------------------------------------------- Listado de beneficiarios 
	----------------------------- CONCENTRADO DE BENEFICIARIOS	
	If @Beneficiarios_x_Jurisdiccion = 0  -- and 1 = 0 
	Begin 
		Print 'GENERACION CONCENTRADA' 
		Set @sIdBeneficiario_Proceso = '' 
		Set @sNombreBeneficiario_Proceso = ''     
		Set @sListaDeFolios = @FoliosVenta 


		Select 
				@Procesar_Producto = sum(Remisionar_Insumo), @Procesar_Servicio = sum(Remisionar_Servicio), @Procesar_Servicio_Consigna = sum(Remisionar_Servicio__Consigna), 
				@Procesar_Medicamento = sum(Medicamento), @Procesar_MaterialDeCuracion = sum(MaterialDeCuracion), 
				@Procesar_Venta = sum(Venta), @Procesar_Consigna = sum(Consigna) 
		From #tmp_Beneficiarios_Folios (NoLock) 


		----Select 
		----		@Procesar_Producto as Procesar_Producto, @Procesar_Servicio as Procesar_Servicio, @Procesar_Servicio_Consigna as Procesar_Servicio_Consigna, 
		----		@Procesar_Medicamento as Procesar_Medicamento, @Procesar_MaterialDeCuracion as Procesar_MaterialDeCuracion, 
		----		@Procesar_Venta as Procesar_Venta, @Procesar_Consigna as Procesar_Consigna,  
		----		@FechaInicial as Fecha_Menor, @FechaFinal as Fecha_Mayor, 
		----		@sIdBeneficiario_Proceso as IdBeneficiario_Proceso, @sNombreBeneficiario_Proceso as NombreBeneficiario_Proceso, 
		----		@TipoDeBeneficiario as TipoDeBeneficiario, @sIdPrograma as IdPrograma, @sIdSubPrograma as IdSubPrograma, @sListaDeFolios as ListaDeFolios    


		If @bEjecutar = 1 
		Begin 

			--------------------- Preparar la tabla con los folios de venta a procesar 
			Delete From  ##tmp_Informacion_CEDIS  

			Insert Into ##tmp_Informacion_CEDIS 
			Select IdFarmacia, FolioVenta   
			From #tmpInformacion_PorRemisionar T 
			Where 
				--IdBeneficiario_Principal = @sIdBeneficiario_Proceso -- and IdPrograma = @sIdPrograma and IdSubPrograma = @sIdSubPrograma 
				( Remisionar_Insumo = @Procesar_Producto ) or ( Remisionar_Servicio = @Procesar_Servicio ) or ( Remisionar_Servicio__Consigna = Remisionar_Servicio__Consigna ) 
				or ( Medicamento = @Procesar_Medicamento ) or ( MaterialDeCuracion = @Procesar_MaterialDeCuracion ) or ( Venta = @Procesar_Venta ) or ( Consigna = @Procesar_Consigna )  
			Group by 
				IdFarmacia, FolioVenta
			Order By 
				FolioVenta 
			--------------------- Preparar la tabla con los folios de venta a procesar 

			---select * from ##tmp_Informacion_CEDIS 



			Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso 
					
					@Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 

					@NivelInformacion_Remision = @NivelInformacion_Remision, 
					@ProcesarParcialidades = @ProcesarParcialidades, 

					@ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
					@AsignarReferencias = @AsignarReferencias, 

					@TipoProcesoRemision = 3, 
					@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna, 
					@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
					@Procesar_Venta = @Procesar_Venta, @Procesar_Consigna = @Procesar_Consigna, 

					@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado , 
					@IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
					
					@IdFarmacia = @IdFarmacia, 
					
					
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
					@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion,
					@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
					@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
					@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
					@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
					@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, 
					@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento 
		End 
	End 


	----------------------------- DETALLADO POR BENEFICIARIOS	
	If @Beneficiarios_x_Jurisdiccion = 1 
	Begin 

		----Select 
		----	IdEmpresa, IdEstado, IdFarmaciaGenera, '' as IdFarmacia, 	
		----	Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
		----	Medicamento, MaterialDeCuracion, Venta, Consigna,  
		----	'' as FechaMenor, '' as FechaMayor,  
		----	IdBeneficiario, NombreBeneficiario, TipoDeBeneficiario, 
		----	'' as IdPrograma, '' as IdSubPrograma, '' as FoliosDeVenta 
		----From #tmp_Beneficiarios_Folios T 
		----Group by  
		----	IdEmpresa, IdEstado, IdFarmaciaGenera, 
		----	Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
		----	Medicamento, MaterialDeCuracion, Venta, Consigna,  
		----	IdBeneficiario, NombreBeneficiario, TipoDeBeneficiario 


		----Select 
		----	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, 	
		----	Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
		----	Medicamento, MaterialDeCuracion, Venta, Consigna,  
		----	FechaMenor, FechaMayor,  
		----	IdBeneficiario, NombreBeneficiario, TipoDeBeneficiario, IdPrograma, IdSubPrograma, FoliosDeVenta 
		----From #tmp_Beneficiarios_Folios T 
		------Where ( Remisionar_Insumo = 1 or Remisionar_Servicio = 1 or Remisionar_Servicio__Consigna = 1 or 
		------		Medicamento = 1 or MaterialDeCuracion = 1 or Venta = 1 or Consigna = 1 ) 
		------Where ( Medicamento = 1 or MaterialDeCuracion = 1 ) 


		Declare #cursorBeneficiarios  
		Cursor For 
			Select 
				IdEmpresa, IdEstado, IdFarmaciaGenera, '' as IdFarmacia, 	
				Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
				Medicamento, MaterialDeCuracion, Venta, Consigna,  
				'' as FechaMenor, '' as FechaMayor,  
				IdBeneficiario, NombreBeneficiario, TipoDeBeneficiario, '' as IdPrograma, '' as IdSubPrograma, '' as FoliosDeVenta 
			From #tmp_Beneficiarios_Folios T 
			----Where ( Remisionar_Insumo = 1 or Remisionar_Servicio = 1 or Remisionar_Servicio__Consigna = 1 or 
			----		Medicamento = 1 or MaterialDeCuracion = 1 or Venta = 1 or Consigna = 1 ) 
			Where ( Medicamento = 1 or MaterialDeCuracion = 1 )	
				and 1 = 1  
			Group by  
				IdEmpresa, IdEstado, IdFarmaciaGenera, 
				Remisionar_Insumo, Remisionar_Servicio, Remisionar_Servicio__Consigna, 
				Medicamento, MaterialDeCuracion, Venta, Consigna,  
				IdBeneficiario, NombreBeneficiario, TipoDeBeneficiario -- , IdPrograma, IdSubPrograma  
			Order by IdBeneficiario 
		Open #cursorBeneficiarios 
		FETCH NEXT FROM #cursorBeneficiarios Into 
				@IdEmpresa, @IdEstado, @IdFarmaciaGenera, @IdFarmacia, 
				@Procesar_Producto, @Procesar_Servicio, @Procesar_Servicio_Consigna, 
				@Procesar_Medicamento, @Procesar_MaterialDeCuracion, 
				@Procesar_Venta, @Procesar_Consigna,  
				@sFecha_Menor, @sFecha_Mayor, 
				@sIdBeneficiario_Proceso, @sNombreBeneficiario_Proceso, @TipoDeBeneficiario, @sIdPrograma, @sIdSubPrograma, @sListaDeFolios   
			WHILE @@FETCH_STATUS = 0 
			BEGIN 
				Set @sSql = replicate('-', 100) 
				----Print @sSql 

				-- Set @sListaDeFolios = ''  
				Set @GUID = cast(NEWID() as varchar(max)) 
				--Print '' 
				--Print @GUID 
				Set @sSql = '' 
				Set @sSql = 'BN' + @sIdBeneficiario_Proceso + ' TB' + cast(@TipoDeBeneficiario as varchar(10)) + '' + ' -- ' + @sNombreBeneficiario_Proceso + '  ' 
				--Set @sSql = @sSql + ' TB' + cast(@TipoDeBeneficiario as varchar(10)) + '    ' 	
				Set @sSql = @sSql + ' FI' + cast(@sFecha_Menor as varchar(10)) 	
				Set @sSql = @sSql + ' FF' + cast(@sFecha_Mayor as varchar(10)) + '  ' 


				Set @sSql = @sSql + ' P' + cast(@sIdPrograma as varchar(1)) 	
				Set @sSql = @sSql + 'S' + cast(@sIdSubPrograma as varchar(1)) + '  ' 	

				Set @sSql = @sSql + ' PP' + cast(@Procesar_Producto as varchar(1)) 	
				Set @sSql = @sSql + ' PS' + cast(@Procesar_Servicio as varchar(1)) 
				Set @sSql = @sSql + ' PSC' + cast(@Procesar_Servicio_Consigna as varchar(1)) 

				Set @sSql = @sSql + ' PMD' + cast(@Procesar_Medicamento as varchar(1)) 	
				Set @sSql = @sSql + ' PMC' + cast(@Procesar_MaterialDeCuracion as varchar(1)) 
				Set @sSql = @sSql + ' PVT' + cast(@Procesar_Venta as varchar(1)) 	
				Set @sSql = @sSql + ' PCN' + cast(@Procesar_Consigna as varchar(1))  

				Set @sSql = @sSql + char(10) + '		Folios: ' + @sListaDeFolios    
				--Print @sSql 



				--------------------- Preparar la tabla con los folios de venta a procesar 
				Delete From  ##tmp_Informacion_CEDIS 

				Insert Into ##tmp_Informacion_CEDIS 
				Select IdFarmacia, FolioVenta   
				From #tmpInformacion_PorRemisionar T 
				Where 
					IdBeneficiario_Principal = @sIdBeneficiario_Proceso -- and IdPrograma = @sIdPrograma and IdSubPrograma = @sIdSubPrograma 
					and 
					( 
						( Remisionar_Insumo = @Procesar_Producto ) or ( Remisionar_Servicio = @Procesar_Servicio ) or ( Remisionar_Servicio__Consigna = Remisionar_Servicio__Consigna ) 
						or ( Medicamento = @Procesar_Medicamento ) or ( MaterialDeCuracion = @Procesar_MaterialDeCuracion ) or ( Venta = @Procesar_Venta ) or ( Consigna = @Procesar_Consigna )  
					) 
				Group by 
					IdFarmacia, FolioVenta
				Order By 
					FolioVenta 

				--select 
				--	@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, @IdFarmacia as IdFarmacia, 
				--	@Procesar_Producto as Procesar_Producto, @Procesar_Servicio as Procesar_Servicio, @Procesar_Servicio_Consigna as Procesar_Servicio_Consigna, 
				--	@Procesar_Medicamento as Procesar_Medicamento, @Procesar_MaterialDeCuracion as Procesar_MaterialDeCuracion, 
				--	@Procesar_Venta as Procesar_Venta, @Procesar_Consigna as Procesar_Consigna,  
				--	@sFecha_Menor as sFecha_Menor, @sFecha_Mayor as sFecha_Mayor, 
				--	@sIdBeneficiario_Proceso as sIdBeneficiario_Proceso, @sNombreBeneficiario_Proceso as sNombreBeneficiario_Proceso, 
				--	@TipoDeBeneficiario as TipoDeBeneficiario, @sIdPrograma as sIdPrograma, @sIdSubPrograma as sIdSubPrograma, @sListaDeFolios as sListaDeFolios    

				---- AQUI 
				--Select * from ##tmp_Informacion_CEDIS 
				--------------------- Preparar la tabla con los folios de venta a procesar 


				------		spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen 	  

				If @bEjecutar = 1 --and 1 = 1 
				Begin 

					Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso 

							@Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 

							@NivelInformacion_Remision = @NivelInformacion_Remision, 
							@ProcesarParcialidades = @ProcesarParcialidades, 

							@ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
							@AsignarReferencias = @AsignarReferencias, 

							@TipoProcesoRemision = 3,  
							@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna, 
							@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
							@Procesar_Venta = @Procesar_Venta, @Procesar_Consigna = @Procesar_Consigna, 

							@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado , 
							@IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
							
							--@IdFarmacia = @IdFarmacia, 
							@IdFarmacia = @IdFarmacia_Lista, 
							
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
							@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion,
							@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio,  @Accion = @Accion, 
							@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
							@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
							@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
							@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, 
							@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento 

				End 
				--Print '' 
				--Print '' 

				FETCH NEXT FROM #cursorBeneficiarios Into 
					@IdEmpresa, @IdEstado, @IdFarmaciaGenera, @IdFarmacia, 
					@Procesar_Producto, @Procesar_Servicio, @Procesar_Servicio_Consigna, 
					@Procesar_Medicamento, @Procesar_MaterialDeCuracion, 
					@Procesar_Venta, @Procesar_Consigna,  
					@sFecha_Menor, @sFecha_Mayor,  
					@sIdBeneficiario_Proceso, @sNombreBeneficiario_Proceso, @TipoDeBeneficiario, @sIdPrograma, @sIdSubPrograma, @sListaDeFolios   
			END	 
		Close #cursorBeneficiarios 
		Deallocate #cursorBeneficiarios 
	End 
	--------------------------------------------------------- Listado de beneficiarios 


	--------------------------------------- OBTENER LOS FOLIOS DE VENTA POR BENEFICIARIO 
	---------------------------------------------------------------------------------------------------------------------------- 


	--------------------------------------- LIBERAR LA TABLA DE CONTROL 
	if exists ( Select * from tempdb..sysobjects where name like '##tmp_Informacion_CEDIS%' and xType = 'U' ) Drop Table ##tmp_Informacion_CEDIS 


End 
Go--#SQL 

