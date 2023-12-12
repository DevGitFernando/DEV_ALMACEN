--------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_General__FFxFarmacia' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_General__FFxFarmacia 
Go--#SQL 
  
/*	

	---		Begin tran   
	---
	---			spp_FACT_GenerarRemisiones_General__FFxFarmacia 
	---
	---				rollback tran 
	---		  
	---				commit tran   


	If exists ( Select Name From sysobjects (noLock) Where Name like 'RptRemision_NoProcesado' and xType = 'U' )  
		Truncate Table RptRemision_NoProcesado  

	If exists ( Select Name From sysobjects (noLock) Where Name like 'RptRemision_NoProcesado__Detallado' and xType = 'U' )  
		Truncate Table RptRemision_NoProcesado__Detallado  
*/ 
  
Create Proc spp_FACT_GenerarRemisiones_General__FFxFarmacia 
( 
	@NivelInformacion_Remision int = 2,   --- 1 => General (Primer nivel de informacion) | 2 ==> Farmacia FF (Segundo nivel de informacion) | 3 ==> Ventas directas por Jurisdiccion 

	@ProcesarParcialidades int = 0,		 ---- ====> Exclusivo SSH Primer Nivel 
	@ProcesarCantidadesExcedentes int = 0,   

	@AsignarReferencias int = 1, 

	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '0001', 
	@TipoDeRemision smallint = 1, ----   1 ==> Insumos | 2 Servicio 
	@IdFarmacia varchar(max) = [  ], 
	
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0011', 
	@Lista__IdClienteIdSubCliente varchar(max) = [  ], 

	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2017-05-01', @FechaFinal varchar(10) = '2017-07-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', 
	@IdTipoProducto varchar(2) = '01', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = '', 
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(max) = [ ], -- '', -- 060.167.6638', -- '0104.00', 
	@FechaDeRevision int = 2, 
	-- @FoliosVenta varchar(max) = [ '00000001', '00000031', '00000058', '00000076', '00000079', '00000106', '00000122', '00000145'  ], -- , 3, 4, 5, 6, 7, 8, 9, 10 ], 
	@FoliosVenta varchar(max) = [ ],  -- , 3, 4, 5, 6, 7, 8, 9, 10 ], 
	@TipoDeBeneficiario int = 0, -- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén> 
	@Aplicar_ImporteDocumentos int = 0, 
	@EsProgramasEspeciales int = 0, 
	-- @Referencia_Beneficiario varchar(100) = '', @Referencia_BeneficiarioNombre varchar(200) = '' 	    
	@Referencia_Beneficiario varchar(100) = '', @Referencia_BeneficiarioNombre varchar(200) = '', 
	@Remision_General int = 0, 
	@ClaveSSA_ListaExclusion varchar(max) = [ ], 

	@EsRelacionFacturaPrevia bit = 0, 
	@FacturaPreviaEnCajas int = 0, 
	@Serie varchar(10) = '', @Folio int = 0, 
	@EsRelacionMontos bit = 0,   	 
	@Accion varchar(100) = '', 

	@Procesar_SoloClavesReferenciaRemisiones bit = 0,  

	@ExcluirCantidadesConDecimales bit = 0, 

	@Separar__Venta_y_Vales bit = 0, 
	@TipoDispensacion_Venta int = 0,			-- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	@Criterio_FiltroFecha___Vales int = 1,		-- 1 ==> Fecha de emision | 2 ==> Fecha de registro de vale canjeado ( Fechar registro de Venta ) 


	@EsRemision_Complemento bit = 0,			-- 0 ==> Remisiones normales | 1 ==> Remisiones de Incremento ó Diferenciador 

	@Separar__Controlados bit = 1,				-- 0 ==> Todo | 1 ==> Generar remisiones de Controlados y General   
	@MostrarResultado bit = 1,   

	@Remisiones_x_Farmacia bit = 1,  			-- 0 ==> Remisión concentrada | 1 ==> Remisiones separadas por Farmacia  

	@EsRelacionDocumentoPrevio bit = 0, 
	@FolioRelacionDocumento varchar(6) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

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
	@sFiltro_CEDIS varchar(max), 
	@sFiltro_ClavesSSA varchar(max), 
	@sFiltro_ClavesSSA_Prima varchar(max), 
	@Facturable bit, 
	@bEsFarmacia bit, 
	@bEsAlmacen bit,  
	@OrigenInsumo int, 
	@Procesar_Asignacion_De_Documentos int, 
	@GrupoProceso_Remision int, 
	@TipoDeRemision_Auxiliar int, 
	@bPrint_Query bit, 
	@bPrint bit, 
	@bPrint_01 bit, 
	@bPrint_Documentos bit, 
	@bPrint_Importes bit, 
	@bPrint_Importes_Detalles bit, 
	@bPrint_ListaProcesable bit, 
	@bPrint_MarcaDeTiempo bit, 
	@bPrint_MarcaDeTiempo_Select bit, 
	@sSepardor varchar(500), 
	@bProcesoSegmentado bit, 
	@bVerificarAsignaciones bit, 
	@IdFuenteFinanciamiento_FacturaRelacionada varchar(4), 
	@IdFinanciamiento_FacturaRelacionada varchar(4), 
	@EsFacturada bit, 
	@EsFacturable bit, 	
	@iImporte_FacturaRelacionada numeric(14,4), 
	@bExisteInformacion_x_Procesar bit,     
	@bExisteInformacion_x_Procesar_Excepciones bit, 
	--@Criterio_FiltroFecha___Vales int, 
	@EsRelacionFacturaPrevia_x_Farmacia int 	
		 

	/**	Tipo de Remision **
		1.- Insumos 
		2.- Administracion 
	*/

	--Set DateFormat YMD  
	Set @bPrint_MarcaDeTiempo = 0   
	Set @bPrint_MarcaDeTiempo_Select = 0 
	Set @sSepardor = char(10) -- + char(13) 
	Set @bPrint_Query = 0 
	Set @bPrint = 0       
	Set @bPrint_01 = 1   
	Set @bPrint_Documentos = 0     
	Set @bPrint_Importes = 0 
	Set @bPrint_Importes_Detalles = 0  
	Set @bPrint_ListaProcesable = 0 
	Set @bProcesoSegmentado = 1  ----------- Aplica de forma masiva hasta donde alcanzen las piezas    
	Set @bVerificarAsignaciones = 0 

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
	
	Set @bEsFarmacia = 0  
	Set @bEsAlmacen = 0 

	Set @OrigenInsumo = @TipoDispensacion + 1 -- 0 ==> Venta | 1 Consigna  
	Set @Procesar_Asignacion_De_Documentos = 0 
	Set @GrupoProceso_Remision = @TipoDeRemision 
	Set @TipoDeRemision_Auxiliar = @TipoDeRemision 
	Set @IdFuenteFinanciamiento_FacturaRelacionada = '' 
	Set @IdFinanciamiento_FacturaRelacionada = '' 
	Set @EsFacturada = 0 
	Set @EsFacturable = 0   
	Set @iImporte_FacturaRelacionada = 0 
	Set @bExisteInformacion_x_Procesar = 1 
	Set @bExisteInformacion_x_Procesar_Excepciones = 0 
	--Set @Criterio_FiltroFecha___Vales = 1 
	Set @EsRelacionFacturaPrevia_x_Farmacia = 0 
	Set @sFiltro_ClavesSSA = '' 
	Set @sFiltro_ClavesSSA_Prima = '' 
	Set @sFiltro_CEDIS = '' 

	Print @OrigenInsumo 
	Print @TipoDispensacion 

	----------------- Sobreponer el proceso de Comprobacion 
	If @EsRelacionDocumentoPrevio = 1 
	Begin 
		Set @EsRelacionFacturaPrevia = 0 
		Set @FacturaPreviaEnCajas = 0 
		Set @Serie = '' 
		Set @Folio = 0  
		Set @EsRelacionMontos = 0  

		Set @EsRelacionFacturaPrevia_x_Farmacia = 0  
	End 


	---------------------- VALIDACION TEMPORAL, VALIDA HASTA EL 20180702.0900 
	--If @IdEstado = '28' 
	--Begin 
	--	Set @ExcluirCantidadesConDecimales = 1
	--End 

	--select @TipoDeRemision as TipoDeRemision

	If @IdEstado = '28' 
	Begin 
		Set @Criterio_FiltroFecha___Vales = 2 
	End 

	-- select 'AQUI VAMOS'  

--------------------- Preparar tablas de proceso 
	Exec spp_FACT_ProcesoRemisiones   

	--select 'X AQUI MERO' 

----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 
	--select @EsRelacionMontos as EsRelacionMontos
	If @EsRelacionFacturaPrevia = 0 
		Begin 
			Set @bExisteInformacion_x_Procesar = 1 
			Set @EsRelacionMontos = 0 
			Set @Serie = '' 
			Set @Folio = 0 
			Set @EsFacturada = 0 
			Set @EsFacturable = 0   
		End 
	Else 
		Begin 
			--Print 'X AQUI'

			Set @EsFacturada = 1  
			Set @EsFacturable = 0   
			Set @iImporte_FacturaRelacionada = 0 

			----------------- Información de factura 
			Select @IdFuenteFinanciamiento_FacturaRelacionada = IdRubroFinanciamiento, @IdFinanciamiento_FacturaRelacionada = IdFuenteFinanciamiento    
			From FACT_CFD_Documentos_Generados D (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and Status = 'A' 

			Set @IdFuenteFinanciamiento_FacturaRelacionada = IsNull(@IdFuenteFinanciamiento_FacturaRelacionada, '') 
			Set @IdFinanciamiento_FacturaRelacionada = IsNull(@IdFinanciamiento_FacturaRelacionada, '') 


			---------------------------------- REMOVIDO 20230412.1815 
			----------------- ASEGURAR QUE LOS DATOS DE LA FACTURA SE ENCUENTREN EN LA TABLA DE CONTROL 
			--Exec spp_FACT_GenerarRemisiones_Preparar_RelacionFacturas 
			--	@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, 

			--	@IdFuenteFinanciamiento_FacturaRelacionada = @IdFuenteFinanciamiento_FacturaRelacionada, 
			--	@IdFinanciamiento_FacturaRelacionada = @IdFinanciamiento_FacturaRelacionada, 

			--	@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, 
			--	@FacturaPreviaEnCajas = @FacturaPreviaEnCajas, 
			--	@Serie = @Serie, @Folio = @Folio, 
			--	@EsRelacionMontos = @EsRelacionMontos 
			----------------- ASEGURAR QUE LOS DATOS DE LA FACTURA SE ENCUENTREN EN LA TABLA DE CONTROL 
			---------------------------------- REMOVIDO 20230412.1815 


			Select @iImporte_FacturaRelacionada = ( Importe_Facturado - Importe_Distribuido )   
			From FACT_Remisiones__RelacionFacturas_x_Importes D (NoLock) 
			Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio 
			Set @iImporte_FacturaRelacionada = IsNull(@iImporte_FacturaRelacionada, 0) 


			/* 
			*/ 
			
			--select 
			--	@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, 
			--	@Serie as Serie, @Folio as Folio, 
			--	@IdFuenteFinanciamiento_FacturaRelacionada as IdFuenteFinanciamiento_FacturaRelacionada, 
			--	@IdFinanciamiento_FacturaRelacionada as IdFinanciamiento_FacturaRelacionada 

			--select * 
			--From FACT_CFD_Documentos_Generados D (NoLock) 
			--Where IdEmpresa = @IdEmpresa --and IdEstado = @IdEstado 
			--	  --and IdFarmacia = @IdFarmaciaGenera 
			--	  and Serie = @Serie and Folio = @Folio --and Status = 'A' 
			
			/* 
			*/ 
			----------------- Información de factura 

			--------------------------------- Validar el tipo de documento que respalda la factura 
			Set @bExisteInformacion_x_Procesar = 0 		
			----Select * From FACT_CFD_Documentos_Generados D (NoLock) 
			----Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and Status = 'A'  


			If Exists 
				( 
					---	@TipoDeRemision = 1 ==> Producto 
					---	@TipoDeRemision = 2 ==> Servicio 

					----	Print 'CORTO CIRCUITO' 	
					---		Validar el Tipo de documento de la factura vs el proceso ejecutado   
					Select * From FACT_CFD_Documentos_Generados D (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and Status = 'A'  
					and TipoDocumento = @TipoDeRemision  
				) 
			Begin 
				Set @bExisteInformacion_x_Procesar = 1   
			End 
			Else 
			Begin 
				Print 'CORTO CIRCUITO: TIPO DE FACTURA NO CORRESPONDE CON EL TIPO DE REMISIONES A GENERAR'		
				Set @bExisteInformacion_x_Procesar = 0  
			End 
			--------------------------------- Validar el tipo de documento que respalda la factura 


			-------------------------- Forzar el proceso por montos si se encuentra SERIE-FOLIO 
			If @iImporte_FacturaRelacionada < 1 and @EsRelacionMontos = 1  
			Begin 
				Set @bExisteInformacion_x_Procesar = 1 
				
				If Exists ( Select * 
							 From FACT_Remisiones__RelacionFacturas_x_Importes D (NoLock) 
							 Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
							 Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio  )
				Begin 	
					Print 'CORTO CIRCUITO'		
					Set @bExisteInformacion_x_Procesar = 0  
				End 	
			End 



			----If @iImporte_FacturaRelacionada > 0  
			----   Set @EsRelacionMontos = 1 
	End 

	--Select @bExisteInformacion_x_Procesar AS bExisteInformacion_x_Procesar 
	--print 'SALIDA DE EMERGENCIA' 
	--RETURN 

	---------------- Crear la tabla vacia 
	Select 
		Identity(int, 1, 1) as Orden, 
		E.IdFuenteFinanciamiento, E.IdFinanciamiento, 
		0 as Procesa_Venta, 0 as Procesa_Consigna, 0 as Procesa_Producto, 0 as Procesa_Servicio, 
		D.ClaveSSA, 0 as ContenidoPaquete, 
		sum(D.CantidadAgrupada_Facturada - D.CantidadAgrupada_Distribuida) as CantidadAgrupada_Disponible, 
		sum(D.Cantidad_Facturada - D.Cantidad_Distribuida) as CantidadDisponible, 
		0 as EsProcesable
	Into #tmp_ListaClaves_VentasPrevias 
	From FACT_Remisiones__RelacionFacturas D (NoLock) 
	Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
	Where 1 = 0 
	Group by 
		E.IdFuenteFinanciamiento, E.IdFinanciamiento, 
		--E.Procesa_Venta, E.Procesa_Consigna, E.Procesa_Producto, E.Procesa_Servicio, 		
		D.ClaveSSA  

----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 


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



--------------------- Obtener los programas y sub-programas 	
	Select 
		ROW_NUMBER() OVER (order by C.IdPrograma, C.IdSubPrograma)as Renglon, 
		C.IdPrograma, 
		--Programa, 
		C.IdSubPrograma, 
		--SubPrograma, 
		(C.IdPrograma + C.IdSubPrograma) as IdAtencion, 1 as Procesar   
	Into #tmpProgramasDeAtencion 
	From CFG_EstadosFarmaciasProgramasSubProgramas C (NoLock) 
	Inner Join #tmp_ClientesSubClientes L (NoLock) On ( C.IdCliente = L.IdCliente and C.IdSubCliente = L.IdSubCliente ) 
	Where C.IdEstado = @IdEstado --and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
	Group by 
		C.IdPrograma, 
		--Programa, 
		C.IdSubPrograma  
		--SubPrograma  	



	If @Criterio_ProgramasAtencion <> '' 
	Begin 
	   Update #tmpProgramasDeAtencion Set Procesar = 0 
	   
	   Set @sSql = '
		   Update P Set Procesar = 1 	   	   
		   From #tmpProgramasDeAtencion P 
		   Where IdAtencion in ( ' + @Criterio_ProgramasAtencion + ' ) ' 
	   Exec(@sSql) 
	   --Print @sSql 
	End 

	Delete From #tmpProgramasDeAtencion Where Procesar = 0  		
--------------------- Obtener los programas y sub-programas 


	----------------------------------------------------- 
	-- Se obtiene el catalogo general --
	----------------------------------------------------- 
	--Print char(39) + @ClaveSSA + char(39) 
	--Print char(39) + ltrim(rtrim(@ClaveSSA)) + char(39) 
	If ltrim(rtrim(@ClaveSSA)) <> '' 
	Begin 
		Set @sFiltro_ClavesSSA = 'Where ClaveSSA in ( ' + @ClaveSSA + ' ) ' + char(13) 
		Set @sFiltro_ClavesSSA_Prima = 'Where ClaveSSA Not in ( ' + @ClaveSSA + ' ) ' + char(13) 	 
	End 


	Select Top 0 *, 0 as Relacionado 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
	--Where -- ClaveSSA like '%010.000.3674.00%' 
	--	ClaveSSA like '%' + @ClaveSSA + '%' 
	
	Set @sSql = 
		'Insert Into #vw_Productos_CodigoEAN ' + char(13) +  
		'Select *, 0 as Relacionado ' + char(13) + 	 
		'From vw_Productos_CodigoEAN ' + @sFiltro_ClavesSSA 
	Exec(@sSql) 
	--print @sSql  


	------------- FILTRAR POR TIPO DE PRODCUCTO 
	If  @FechaDeRevision in ( 1, 2, 3 )  
	Begin 	
		If @Remision_General = 0   ------- Remisionar Medicamento y Material de Curación [ 0 separados | 1 juntos ]  
		Begin 
			--print 'borrando'   
			Delete From #vw_Productos_CodigoEAN Where IdTipoProducto <> @IdTipoProducto 
		End 
	End 


	-- select * from #vw_Productos_CodigoEAN 


	Select * 
	Into #vw_Claves_Precios_Asignados
	From vw_Claves_Precios_Asignados 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 

	Select * 
	Into #vw_Claves_Precios_Asignados_DosisUnitaria
	From vw_Claves_Precios_Asignados_DosisUnitaria 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 


	------------------- Modificacion 20220418.1630 
	If @EsRelacionFacturaPrevia = 1 or @EsRelacionDocumentoPrevio = 1 
	Begin 

		----select * 
		----From FACT_Remisiones__RelacionFacturas D (NoLock) 
		----Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio 


		Select 
			D.ClaveSSA 
		into #tmp__ClavesFacturaAnticipada 
		From FACT_Remisiones__RelacionFacturas D (NoLock) 
		Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
		Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio 
		Group by D.ClaveSSA 
		Having sum(D.Cantidad_Facturada - D.Cantidad_Distribuida) > 0  


		If @EsRelacionDocumentoPrevio = 1 
		Begin
			Insert into #tmp__ClavesFacturaAnticipada ( ClaveSSA ) 
			Select 
				D.ClaveSSA  
			From FACT_Remisiones__RelacionDocumentos D (NoLock) 
			Inner Join FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.FolioRelacion = @FolioRelacionDocumento -- and E.Serie = @Serie and E.Folio = @Folio 
			Group by D.ClaveSSA 
			Having sum(D.Cantidad_A_Comprobar - D.Cantidad_Distribuida) > 0  			
		End 


		if @sFiltro_ClavesSSA_Prima <> ''  
		Begin 
			Set @sSql = 
				'Delete ' + char(13) + 	 
				'From #tmp__ClavesFacturaAnticipada ' + @sFiltro_ClavesSSA_Prima 
			Exec(@sSql) 
			-- print @sSql 
		End 

		--Select * from #tmp__ClavesFacturaAnticipada 



		-- select 'ANTES DE LIMPIAR DATOS' as Test, count(*) From #vw_Productos_CodigoEAN 
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


		----If @ClaveSSA <> '' 
		----Begin 
		----	Set @sFiltro_ClavesSSA = 'Where ClaveSSA Not in ( ' + @ClaveSSA + ' ) ' + char(13) 
		----End 

		----Set @sSql = 
		----	'Select Top 0 *, 0 as Relacionado ' + char(13) + 	 
		----	'Delete From #vw_Productos_CodigoEAN L ' + char(13) + 	 
		----	'Inner Join #tmp__ClavesFacturaAnticipada F On ( L.ClaveSSA = F.ClaveSSA ) ' + @sFiltro_ClavesSSA 
		----Exec(@sSql) 

		--select 'DESPUES DE LIMPIAR DATOS' as Test, count(*) From #vw_Productos_CodigoEAN 

		--select * from #vw_Productos_CodigoEAN 

		--Select @bExisteInformacion_x_Procesar AS bExisteInformacion_x_Procesar 	
		--Select 'DESPUES RELACION FACTURAS' as Campo, * From #vw_Productos_CodigoEAN 
	End 


	--Select @bExisteInformacion_x_Procesar AS bExisteInformacion_x_Procesar 	
	--Select 'DESPUES RELACION FACTURAS' as Campo, * From #vw_Productos_CodigoEAN 

	--return ------------------- AQUI 


	Select ClaveSSA, DescripcionClave 
	Into #vw_Claves_A_Procesar 
	From #vw_Productos_CodigoEAN 
	Group by ClaveSSA, DescripcionClave 


	----------------------------------------------------- 
	-- Se obtiene el catalogo general --
	----------------------------------------------------- 

	-- select * 	from #vw_Productos_CodigoEAN 
	-----------------------------------------------------

	

	-----------------------------------------------------
	-- Se obtienen los Folios del periodo seleccionado --
	-----------------------------------------------------
	----Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioCierre, C.FechaCorte 
	----Into #tmpFoliosCierres
	----From Ctl_CierresDePeriodos C(NoLock) 
	----Inner Join FACT_Informacion_Proceso_Facturacion F(NoLock) On( C.IdEmpresa = F.IdEmpresa And C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia And C.FolioCierre = F.FolioCierre ) 
	----Where C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado And F.Identificador = @Identificador
	----	And Convert( varchar(10), C.FechaCorte, 120 ) Between @FechaInicial And @FechaFinal 


	----------------------------------------------------------------------------------------------------------------------------
	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	---------------------------------------------------------------------------------------------------------------------------- 
	Select top 1 @bEsAlmacen = 1 From vw_Farmacias Where EsAlmacen = 1 and IdEstado = @IdEstado  and IdFarmacia = @IdFarmacia  
	Select top 1 @bEsFarmacia = 1 From vw_Farmacias Where EsAlmacen = 0 and IdEstado = @IdEstado  and IdFarmacia = @IdFarmacia 
	
	Set @bEsAlmacen = IsNull(@bEsAlmacen, 0) 	 
	Set @bEsFarmacia = IsNull(@bEsFarmacia, 0) 



	Select Top 0 *, (case when EsAlmacen = 0 then 1 else 0 end) as EsFarmacia   
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado and IdTipoUnidad <> '006' 

	Set @sFiltro = '' 
	If @IdFarmacia <> '' and @IdFarmacia  <> '*' 
	Begin 
	   -- Delete From #tmpFarmacias Where IdFarmacia <> RIGHT('0000' + @IdFarmacia, 4)  	   
	   --Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + char(39) + @IdFarmacia + char(39) +  ' ) '  
	   Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + @IdFarmacia + ' ) '  
	   -- Set @sFiltro = ' and cast(IdFarmacia as int) In ( ' + @IdFarmacia + ' ) '  
	End 


	--------- Excluir almacenes de remisiones generales  
	If @FechaDeRevision <> 3 
	   Set @sFiltro = @sFiltro + ' and IdTipoUnidad <> ' + char(39) + '006' + char(39)


	Set @sSql = 
		'Insert Into #tmpFarmacias ' + char(10) + 
		'Select *, (case when EsAlmacen = 0 then 1 else 0 end) as EsFarmacia  '  + char(10) + 
		'From vw_Farmacias '  + char(10) + 
		'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' + @sFiltro   
	Exec(@sSql) 
	--print '' 
	--print 'AQUI' 
	--Print @sSql 
	--Select 'AQUI', * from #tmpFarmacias 



	------------------------------------------------------------- OBTENER FOLIOS DE VENTA INVOLUCRADOS  
	Select Top 0 
		0 as EsAlmacen, 0 as EsFarmacia, 0 as EsUnidosis, 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 0 as EsValeRelacionado, 0 as GrupoDispensacion, 
		cast( '' as varchar(8)) as IdBeneficiario, 
		V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado, 
		cast( '' as varchar(4)) as IdFuenteFinanciamiento, cast( '' as varchar(4)) as IdFinanciamiento, cast('' as varchar(4)) as IdDocumento, 
		0 as OrigenDeVale    
	Into #tmpVentas 
	From VentasEnc V (NoLock) 
	----Inner Join #tmpFoliosCierres C(NoLock) 
	----	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioCierre = C.FolioCierre ) 
	Where V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
	
	Set @sFiltro_CEDIS = '' 
	If exists ( Select * From tempdb.dbo.sysobjects where name like '##tmp_Informacion_CEDIS%' ) 
	Begin 
	
		If Exists ( Select top 1 * from ##tmp_Informacion_CEDIS ) 
		Begin 
			Set @sFiltro_CEDIS = 'Inner Join ##tmp_Informacion_CEDIS LF (NoLock) On ( V.IdFarmacia = LF.IdFarmacia and V.FolioVenta = LF.FolioVenta ) ' + char(10) 
		End 
	End 

	--Print 'Folios de Venta: ' + @FoliosVenta  
	If @FechaDeRevision = 1   ---- Fecha de registro 
	Begin 
		--print @sSepardor + 'Revision: Fecha registro '  
		--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS ... Fecha Registro ==>  ' + convert(varchar(50), getdate(), 120) 	 
		--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS ... Fecha Registro' as Proceso, getdate() as MarcaDeTiempo 

		Set @sFiltro = '' 
		If ltrim(rtrim(@FoliosVenta)) <>  '' 
		Begin 
			Set @sFiltro = '   and cast(V.FolioVenta as int) in ( ' + @FoliosVenta + ' ) '  
		End 

		Set @sSql = 
		'Insert Into #tmpVentas  ' + char(10) + 
		'Select ' + char(10) + 
		'	F.EsAlmacen, F.EsFarmacia, F.EsUnidosis, V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 0 as EsValeRelacionado, 0 as GrupoDispensacion, ' + char(10)+ 
		'	cast(' + char(39) + '' + char(39) + ' as varchar(8)) as IdBeneficiario, ' + 
		'	V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado,    ' + char(10) + 
		'	' + char(39) + @IdFuenteFinanciamiento + char(39) + ' as IdFuenteFinanciamiento, ' + char(39) + @IdFinanciamiento + char(39) + ' as IdFinanciamiento, ' + char(10) + 
		'	' + char(39) + '' + char(39) + ' as IdDocumento, 0 as OrigenDeVale ' + char(10) + 
		'From VentasEnc V (NoLock)  ' + char(10) + 
		@sFiltro_CEDIS + 
		'Inner Join #tmp_ClientesSubClientes C (NoLock) On ( V.IdCliente = C.IdCliente and V.IdSubCliente = C.IdSubCliente ) ' + char(10) + 
		'Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) ' + char(10) + 
		-- 'Inner Join VentasInformacionAdicional C (NoLock) ' + char(10) + 
		--'	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) ' + char(10) + 
		'Where V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and V.IdEstado = ' + char(39) + @IdEstado + char(39) + char(10) + -- ' and V.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + char(10) + 
		--'	and V.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And V.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + char(10) + 
		'	and convert(varchar(10), V.FechaRegistro, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal + char(39) + ' ' +  char(10) 
		+ ' and ' + cast(@bExisteInformacion_x_Procesar as varchar(10)) + ' = 1 '  
		Set @sSql = @sSql + '  ' + @sFiltro 
		Exec(@sSql) 

		Print cast(@FechaDeRevision as varchar(100)) 
		--Print @sSql 

		--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS ... Fecha Registro ==>  ' + convert(varchar(50), getdate(), 120) 	 
		--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS ... Fecha Registro' as Proceso, getdate() as MarcaDeTiempo 

	End     

	If @FechaDeRevision = 2   ---- Fecha de receta  
	Begin 
		--print @sSepardor + 'Revision: Fecha receta ' 
		--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS ... Fecha Receta ==>  ' + convert(varchar(50), getdate(), 120) 	 

		Set @sFiltro = '' 
		If ltrim(rtrim(@FoliosVenta)) <>  '' 
		Begin 
			Set @sFiltro = '   and cast(V.FolioVenta as int) in ( ' + @FoliosVenta + ' ) '  
		End 

		Set @sSql = 
		'Insert Into #tmpVentas  ' + char(10) + 
		'Select ' + char(10) + 
		'	F.EsAlmacen, F.EsFarmacia, F.EsUnidosis, V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 0 as EsValeRelacionado, 0 as GrupoDispensacion, ' + char(10)+ 
		'	C.IdBeneficiario, ' + 
		'	V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado,    ' + char(10) + 
		'	' + char(39) + @IdFuenteFinanciamiento + char(39) + ' as IdFuenteFinanciamiento, ' + char(39) + @IdFinanciamiento + char(39) + ' as IdFinanciamiento, ' + char(10) + 
		'	' + char(39) + '' + char(39) + ' as IdDocumento, 0 as OrigenDeVale ' + char(10) + 
		'From VentasEnc V (NoLock)  ' + char(10) + 
		@sFiltro_CEDIS + 
		'Inner Join #tmp_ClientesSubClientes CS (NoLock) On ( V.IdCliente = CS.IdCliente and V.IdSubCliente = CS.IdSubCliente ) ' + char(10) + 
		'Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) ' + char(10) + 
		'Inner Join VentasInformacionAdicional C (NoLock) ' + char(10) + 
		'	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) ' + char(10) + 
		'Where V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and V.IdEstado = ' + char(39) + @IdEstado + char(39) + char(10) + -- ' and V.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + char(10) + 
		--'	and V.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And V.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + char(10) + 
		'	and convert(varchar(10), C.FechaReceta, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal + char(39) + ' ' +  char(10) 
		+ ' and ' + cast(@bExisteInformacion_x_Procesar as varchar(10)) + ' = 1 '  
		Set @sSql = @sSql + '  ' + @sFiltro 
		Exec(@sSql) 

		Print cast(@FechaDeRevision as varchar(100)) 
		--Print @sSql 

		--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS ... Fecha Receta ==>  ' + convert(varchar(50), getdate(), 120) 	 
		--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS ... Fecha Receta' as Proceso, getdate() as MarcaDeTiempo 
	End   


	If @FechaDeRevision = 3   ---- folios especificios   
	Begin 
		--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS ... Foios de venta ==>  ' + convert(varchar(50), getdate(), 120) 	 

		--print 'Revision: Folios de venta ' + @FoliosVenta 
		Set @GrupoProceso_Remision = 4 

		Set @sFiltro = '' 
		If ltrim(rtrim(@FoliosVenta)) <>  '' 
		Begin 
			Set @sFiltro = '   and cast(V.FolioVenta as int) in ( ' + @FoliosVenta + ' ) '  
		End 

		Set @sSql = 
		'Insert Into #tmpVentas  ' + char(10) + 
		'Select ' + char(10) + 
		'	F.EsAlmacen, F.EsFarmacia, F.EsUnidosis, V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 0 as EsValeRelacionado, 0 as GrupoDispensacion, C.IdBeneficiario, ' + 
		'	V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado,    ' + char(10) + 
		'	' + char(39) + @IdFuenteFinanciamiento + char(39) + ' as IdFuenteFinanciamiento, ' + char(39) + @IdFinanciamiento + char(39) + ' as IdFinanciamiento, ' + char(10) + 
		'	' + char(39) + '' + char(39) + ' as IdDocumento, 0 as OrigenDeVale ' + char(10) + 
		'From VentasEnc V (NoLock)  ' + char(10) + 
		@sFiltro_CEDIS + 
		'Inner Join #tmp_ClientesSubClientes CS (NoLock) On ( V.IdCliente = CS.IdCliente and V.IdSubCliente = CS.IdSubCliente ) ' + char(10) + 
		'Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) ' + char(10) + 
		'Inner Join VentasInformacionAdicional C (NoLock) ' + char(10) + 
		'	On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) ' + char(10) + 
		'Where V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and V.IdEstado = ' + char(39) + @IdEstado + char(39) + char(10) + -- ' and V.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + char(10) + 
		--'	and V.IdCliente = ' + char(39) + @IdCliente + char(39) + ' And V.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + char(10) 
		'' + ' and ' + cast(@bExisteInformacion_x_Procesar as varchar(10)) + ' = 1 ' 

		Set @sSql = @sSql + '  ' + @sFiltro 
		Exec(@sSql) 

		--Print cast(@FechaDeRevision as varchar(100)) 
		--print @sFiltro_CEDIS 
		--print ''
		--print ''
		--Print @sSql 

		--select * from #tmpVentas 

		--Print @sSql 
		--if @bPrint_Query = 1 Print '' 
		--if @bPrint_Query = 1 Print @sSql 

		--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS ... Foios de venta ==>  ' + convert(varchar(50), getdate(), 120) 	 
		--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS ... Fecha de venta' as Proceso, getdate() as MarcaDeTiempo 

	End   

	--Select 'PROCESO', * from ##tmp_Informacion_CEDIS  
	------------------------------------------------------------- OBTENER FOLIOS DE VENTA INVOLUCRADOS  


	--Select @TipoDispensacion_Venta as TipoDispensacion_Venta, count(*) as Ventas from #tmpVentas 
	--------------------------------------------------------------------------------------------------------------------------------------------   
	------------------------------------------------------ MANEJO DE VALES 
	If @TipoDispensacion_Venta <> 0  -- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	Begin 
		--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS ... Vales' as Proceso, getdate() as MarcaDeTiempo 

		--Select @Separar__Venta_y_Vales as Separar__Venta_y_Vales, @TipoDispensacion_Venta as TipoDispensacion_Venta, @Criterio_FiltroFecha___Vales as Criterio_FiltroFecha___Vales   

		If @TipoDispensacion_Venta = 1 
		Begin 

			Update E Set OrigenDeVale = 1 
			From #tmpVentas E (NoLock) 
			Inner Join ValesEnc V (NoLock) 
				On ( E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioVenta = V.FolioVentaGenerado )  

			
			----Select OrigenDeVale, count(*) as Folios  
			----From #tmpVentas E (NoLock) 			
			----group by OrigenDeVale 

			Delete From #tmpVentas Where OrigenDeVale = 1 

		End 


		If @TipoDispensacion_Venta = 2  
		Begin 

			--------------------------------------- PROCESAR EN BASE A FECHA DE EMISION DEL VALE  
			Delete From #tmpVentas 

			Insert Into #tmpVentas   
			Select
				F.EsAlmacen, F.EsFarmacia, F.EsUnidosis, 
				V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 0 as EsValeRelacionado, 0 as GrupoDispensacion, cast( '' as varchar(8)) as IdBeneficiario, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado, 
				'' as IdFuenteFinanciamiento, '' as IdFinanciamiento, '' as IdDocumento, 0 as OrigenDeVale   
			From VentasEnc V (NoLock)  
			Inner Join #tmp_ClientesSubClientes C (NoLock) On ( V.IdCliente = C.IdCliente and V.IdSubCliente = C.IdSubCliente ) 
			Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
			Inner Join ValesEnc E (NoLock) 
				On ( V.IdEmpresa = E.IdEmpresa and V.IdEstado = E.IdEstado and V.IdFarmacia = E.IdFarmacia and V.FolioVenta = E.FolioVentaGenerado )  
			Inner Join Vales_EmisionEnc VE (NoLock) 
				On ( E.IdEmpresa = VE.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = VE.IdFarmacia and E.FolioVale = VE.FolioVale )  
			Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
				--and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
				and convert(varchar(10), VE.FechaRegistro, 120) Between @FechaInicial and @FechaFinal	-- Diferencia   
				and @Criterio_FiltroFecha___Vales = 1 
			--------------------------------------- PROCESAR EN BASE A FECHA DE EMISION DEL VALE  


			--select @Criterio_FiltroFecha___Vales, * from #tmpVentas 
			--------------------------------------- PROCESAR EN BASE A FECHA DE REGISTRO(CANJE) DEL VALE  
			Delete From #tmpVentas 

			Insert Into #tmpVentas   
			Select
				F.EsAlmacen, F.EsFarmacia, F.EsUnidosis, 
				V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 0 as EsValeRelacionado, 0 as GrupoDispensacion, cast( '' as varchar(8)) as IdBeneficiario, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 0 as Procesar, 0 as Procesado, 
				'' as IdFuenteFinanciamiento, '' as IdFinanciamiento, '' as IdDocumento, 0 as OrigenDeVale   
			From VentasEnc V (NoLock)  
			Inner Join #tmp_ClientesSubClientes C (NoLock) On ( V.IdCliente = C.IdCliente and V.IdSubCliente = C.IdSubCliente ) 
			Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
			Inner Join ValesEnc E (NoLock) 
				On ( V.IdEmpresa = E.IdEmpresa and V.IdEstado = E.IdEstado and V.IdFarmacia = E.IdFarmacia and V.FolioVenta = E.FolioVentaGenerado )  
			--Inner Join Vales_EmisionEnc VE (NoLock) 
			--	On ( E.IdEmpresa = VE.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = VE.IdFarmacia and E.FolioVale = VE.FolioVale )  
			Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
				--and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
				and convert(varchar(10), V.FechaRegistro, 120) Between @FechaInicial and @FechaFinal	-- Diferencia 
				and @Criterio_FiltroFecha___Vales = 2 
			--------------------------------------- PROCESAR EN BASE A FECHA DE REGISTRO(CANJE) DEL VALE  

			--select * from #tmpVentas 
		End 

		--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS ... Vales' as Proceso, getdate() as MarcaDeTiempo 
	End 


	Update V Set EsValeRelacionado = 1, GrupoDispensacion = 1 -- (case when @TipoDispensacion_Venta = 0 then 0 else 1 end) 
	From #tmpVentas V (NoLock)  
	Inner Join VentasInformacionAdicional C (NoLock) 
		On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta and C.IdTipoDeDispensacion = '07' ) 


	If @Separar__Venta_y_Vales = 0 
	Begin 
		Update V Set GrupoDispensacion = 0 
		From #tmpVentas V (NoLock)  
	End 

	--Select 'VALES EMITIDOS VS VENTAS' as Resumen, EsValeRelacionado, GrupoDispensacion, count(*) 
	--from #tmpVentas 
	--group by EsValeRelacionado, GrupoDispensacion  
	------------------------------------------------------ MANEJO DE VALES 
	--------------------------------------------------------------------------------------------------------------------------------------------   


	----- Validar los programas seleccionados 
	Update F Set Procesar = 1 
	From #tmpVentas F 
	Inner Join #tmpProgramasDeAtencion P On ( F.IdPrograma = P.IdPrograma and F.IdSubPrograma = P.IdSubPrograma ) 
	
	-- select count(*) from #tmpVentas 
	--select 'VENTAS 01' as VENTAS, IdFarmacia from #tmpVentas   group by IdFarmacia 
	Delete From #tmpVentas Where Procesar = 0 	


	--select 'VENTAS 02' as VENTAS, IdFarmacia from #tmpVentas   group by IdFarmacia 
	--Select @TipoDispensacion_Venta as TipoDispensacion_Venta, count(*) as Ventas from #tmpVentas 


	----------
	----select 'ESTE', * from #tmpVentas 
	----Delete From #tmpVentas --- QUITAR ESTO   

	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	---------------------------------------------------------------------------------------------------------------------------- 
	 
	--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS 01 ==>  ' + convert(varchar(50), getdate(), 120) 	 
	--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS 01' as Proceso, getdate() as MarcaDeTiempo 

	----------------------------------------------------
	-- Se obtienen los detalles de las ventas --  
	-------------------------- Se crea la tabla temporal
	Select	Top 0 
			cast( '' as varchar(50) ) as FolioRemision, 
			V.EsAlmacen, V.EsFarmacia, V.EsUnidosis, 
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 1 as Partida, 
			V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
			V.IdFuenteFinanciamiento, V.IdFinanciamiento, 
			cast( '' as varchar(20) ) as Referencia_01, cast( '' as varchar(50) ) as Referencia_02, cast( '' as varchar(1000) ) as Referencia_03,  
			cast( '' as varchar(20) ) as Referencia_04, cast( '' as varchar(50) ) as Referencia_05, 
			V.IdDocumento, 
			V.IdFuenteFinanciamiento as IdFuenteFinanciamiento_Relacionado, V.IdFinanciamiento as IdFinanciamiento_Relacionado, V.IdDocumento as IdDocumento_Relacionado, 
			V.Procesado, 
			0 as TipoProcesamiento_01, 
			0 as TipoProcesamiento_02, 
			cast('' as varchar(50)) as IdClaveSSA, 
			cast('' as varchar(50)) as ClaveSSA, 
			0 as Relacionada, 
			cast('' as varchar(50)) as IdClaveSSA_P, 
			cast('' as varchar(50)) as ClaveSSA_P, 
			cast(0 as numeric(14, 4)) as ContenidoPaquete_ClaveSSA, 
			cast(0 as numeric(14, 4)) as ContenidoPaquete_Licitado, 
			cast(0 as numeric(14, 4)) as Factor, 
			cast(0 as numeric(14, 4)) as MultiploRelacion, 
			cast('' as varchar(2) ) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, 
			cast(0.0000 as numeric(14, 4)) as PrecioClave, 
			cast(0.0000 as numeric(14, 4)) as PrecioClaveUnitario, 			
			cast(0.0000 as numeric(14, 4)) as TasaIva, 
		
			0 as PartidaGeneral, 
			0 as AfectaEstadistica, 
			0 as AfectaEstadisticaMontos, 

			0 as EsDecimal, 

			L.CantidadVendida, 
			L.CantidadVendida as CantidadVendida_Base, 
			cast(0 as numeric(14, 4)) as Cantidad_Agrupada,  
			cast(0 as numeric(14, 4)) as Cantidad_Remisionada, 
			cast(0 as numeric(14, 4)) as Cantidad_Remisionada_Agrupada, 

			@GrupoProceso_Remision as GrupoProceso, 0 as IncluidoEnExcepcion,  
			0 as TipoDeAsignacion, 
			0 as IdGrupoDeRemisiones, 
			0 as GrupoDispensacion,  

			0 as EsControlado, 
			0 as EsAntibiotico, 
			0 as EsRefrigerado,  

			0 as EsExcepcionPrecio, 

			identity(int, 1, 1) as KeyxGeneral 
	Into #tmpLotes  
	From VentasDet_Lotes L (NoLock) 
	Inner Join #tmpVentas V (NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
	Where (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 
		 
	
	------------------ Obtener informacion base del proceso 		  
	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, SKU, Renglon, EsConsignacion, Cant_Vendida, Cant_Devuelta, CantidadVendida, Status, 
		Actualizado, CostoUnitario, EnRemision_Insumo, EnRemision_Admon, RemisionFinalizada, FechaControl, CantidadRemision_Insumo, CantidadRemision_Admon
	Into #tmp____VentasDet_Lotes
	From VentasDet_Lotes 
	Where 1 = 0 


/* 

	EsControlado, EsAntibiotico, EsRefrigerado,  
	0 as EsControlado, 0 as EsAntibiotico, 0 as EsRefrigerado,  
				
*/ 

	--- select @EsRemision_Complemento as EsRemision_Complemento 

	--select @bExisteInformacion_x_Procesar as bExisteInformacion_x_Procesar_01
	If @EsRemision_Complemento = 0 
	Begin 

		--select @bExisteInformacion_x_Procesar as bExisteInformacion_x_Procesar

		Insert Into #tmp____VentasDet_Lotes 
		(
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, SKU, Renglon, EsConsignacion, Cant_Vendida, Cant_Devuelta, CantidadVendida, Status, 
			Actualizado, CostoUnitario, EnRemision_Insumo, EnRemision_Admon, RemisionFinalizada, FechaControl, CantidadRemision_Insumo, CantidadRemision_Admon	 
		) 
		Select 
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, L.Renglon, L.EsConsignacion, L.Cant_Vendida, L.Cant_Devuelta, L.CantidadVendida, L.Status, 
			L.Actualizado, L.CostoUnitario, L.EnRemision_Insumo, L.EnRemision_Admon, L.RemisionFinalizada, L.FechaControl, L.CantidadRemision_Insumo, L.CantidadRemision_Admon 
		From VentasDet_Lotes L (NoLock)  
		Inner Join #tmpVentas V (NoLock)  
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 
		Where 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion  
			  and 
			  ( 
				  (L.CantidadVendida - L.CantidadRemision_Insumo) > 0 
				  or 
				  (L.CantidadVendida - L.CantidadRemision_Admon) > 0 
			  ) 
			  and @bExisteInformacion_x_Procesar = 1 

	End 


	If @EsRemision_Complemento = 1  
	Begin 
		--Select 'INCREMENTO, DIFERENCIADOR, DISTRIBUCION' as OrigenDeDatos 

		Insert Into #tmp____VentasDet_Lotes 
		(
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, SKU, Renglon, EsConsignacion, Cant_Vendida, Cant_Devuelta, CantidadVendida, Status, 
			Actualizado, CostoUnitario, EnRemision_Insumo, EnRemision_Admon, RemisionFinalizada, FechaControl, CantidadRemision_Insumo, CantidadRemision_Admon
		) 
		Select 
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, L.Renglon, L.EsConsignacion, L.Cant_Vendida, L.Cant_Devuelta, L.CantidadVendida, L.Status, 
			L.Actualizado, L.CostoUnitario, L.EnRemision_Insumo, L.EnRemision_Admon, L.RemisionFinalizada, L.FechaControl, L.CantidadRemision_Insumo, L.CantidadRemision_Admon 
		From FACT_Incremento___VentasDet_Lotes L (NoLock)  
		Inner Join #tmpVentas V (NoLock)  
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 
		Where 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion  
			  and 
			  ( 
				  (L.CantidadVendida - L.CantidadRemision_Insumo) > 0 
				  or 
				  (L.CantidadVendida - L.CantidadRemision_Admon) > 0 
			  ) 
			  and @bExisteInformacion_x_Procesar = 1 
	End 
	------------------ Obtener informacion base del proceso 
	
	--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS 02 ==>  ' + convert(varchar(50), getdate(), 120) 	 


	--select * from #vw_Productos_CodigoEAN 
	--select 'VENTAS BASE', * from #tmpVentas 
	--select 'VENTAS BASE 02', * From #tmp____VentasDet_Lotes 


	If @TipoDeRemision = 1  
	  Begin 
		If @GrupoProceso_Remision = 4 
			Begin 
				Set @TipoDeRemision = 4 
			End 


		------------ Se obtienen los Lotes de "Insumos"
		Insert Into #tmpLotes 
		Select	--top 1 
				cast( '' as varchar(50) ) as FolioRemision, 
				V.EsAlmacen, V.EsFarmacia, V.EsUnidosis, 
				L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 1 as Partida, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
				V.IdFuenteFinanciamiento, V.IdFinanciamiento, 
				cast( '' as varchar(20) ) as Referencia_01, cast( '' as varchar(50) ) as Referencia_02, cast( '' as varchar(1000) ) as Referencia_03,  
				cast( '' as varchar(20) ) as Referencia_04, cast( '' as varchar(50) ) as Referencia_05, 
				V.IdDocumento, 
				V.IdFuenteFinanciamiento as IdFuenteFinanciamiento_Relacionado, V.IdFinanciamiento as IdFinanciamiento_Relacionado, V.IdDocumento as IdDocumento_Relacionado, 
				V.Procesado, 
				0 as TipoProcesamiento_01, 
				0 as TipoProcesamiento_02, 
				cast('' as varchar(50)) as IdClaveSSA, 				
				cast('' as varchar(50)) as ClaveSSA, 
				cast('' as varchar(50)) as IdClaveSSA_P, 
				cast('' as varchar(50)) as ClaveSSA_P, 
				0 as Relacionada,  
				0 as ContenidoPaquete_ClaveSSA,  
				0 as ContenidoPaquete_Licitado, 
				1 as Factor, 
				1 as MultiploRelacion, 
				cast('' as varchar(2)) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, 
				cast(0.0000 as numeric(14, 4)) as PrecioClave, 
				cast(0.0000 as numeric(14, 4)) as PrecioClaveUnitario, 
				cast(0.0000 as numeric(14, 4)) as TasaIva, 
				0 as PartidaGeneral, 
				0 as AfectaEstadistica, 
				0 as AfectaEstadisticaMontos, 
				0 as EsDecimal, 
				sum(L.CantidadVendida - L.CantidadRemision_Insumo) as CantidadVendida, 
				sum(L.CantidadVendida - L.CantidadRemision_Insumo) as CantidadVendida_Base, 
				0 as Cantidad_Agrupada, 
				0 as Cantidad_Remisionada, 0 as Cantidad_Remisionada_Agrupada, 

				@GrupoProceso_Remision as GrupoProceso, 0 as IncluidoEnExcepcion, 
				0 as TipoDeAsignacion, 0 as IdGrupoDeRemisiones, V.GrupoDispensacion,    

				0 as EsControlado, 0 as EsAntibiotico, 0 as EsRefrigerado,  

				0 as EsExcepcionPrecio

		From #tmp____VentasDet_Lotes L (NoLock)  
		Inner Join #tmpVentas V (NoLock)  
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		--Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) -- and P.ClaveSSA = '010.000.1937.00' ) 
		Where -- ClaveLote Not Like '%*%' 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 
			  --and L.EnRemision_Insumo = 0 and L.RemisionFinalizada = 0 
			  ----and L.CantidadVendida > 0 
			  and ( L.CantidadVendida - L.CantidadRemision_Insumo ) > 0 
			  and @bExisteInformacion_x_Procesar = 1 ---- Validacion de Corto circuito -- 20180523.1400 
		Group by 
				V.EsAlmacen, V.EsFarmacia, V.EsUnidosis, 
				L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
				V.IdFuenteFinanciamiento, V.IdFinanciamiento, V.IdDocumento, 
				V.Procesado, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, V.GrupoDispensacion  
	  End 
	Else 
	  Begin 
		If @GrupoProceso_Remision = 4 
			Begin 
				Set @TipoDeRemision = 6  
				Set @GrupoProceso_Remision = 6 
			End 

		------------ Se obtienen los Lotes de "Administracion" 
		Insert Into #tmpLotes 
		Select	
				cast( '' as varchar(50) ) as FolioRemision, 
				V.EsAlmacen, V.EsFarmacia, V.EsUnidosis, 
				L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 1 as Partida, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
				V.IdFuenteFinanciamiento, V.IdFinanciamiento, 
				cast( '' as varchar(20) ) as Referencia_01, cast( '' as varchar(50) ) as Referencia_02, cast( '' as varchar(1000) ) as Referencia_03,  
				cast( '' as varchar(20) ) as Referencia_04, cast( '' as varchar(50) ) as Referencia_05, 
				V.IdDocumento, 
				V.IdFuenteFinanciamiento as IdFuenteFinanciamiento_Relacionado, V.IdFinanciamiento as IdFinanciamiento_Relacionado, V.IdDocumento as IdDocumento_Relacionado, 
				V.Procesado, 
				0 as TipoProcesamiento_01, 
				0 as TipoProcesamiento_02, 
				cast('' as varchar(50)) as IdClaveSSA, 				
				cast('' as varchar(50)) as ClaveSSA, 
				cast('' as varchar(50)) as IdClaveSSA_P, 
				cast('' as varchar(50)) as ClaveSSA_P, 
				0 as Relacionada, 
				0 as ContenidoPaquete_ClaveSSA, 
				0 as ContenidoPaquete_Licitado, 
				1 as Factor, 
				1 as MultiploRelacion, 
				cast('' as varchar(2)) as IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, 
				cast(0.0000 as numeric(14, 4)) as PrecioClave, 
				cast(0.0000 as numeric(14, 4)) as PrecioClaveUnitario, 
				cast(0.0000 as numeric(14, 4)) as TasaIva, 
				0 as PartidaGeneral, 
				0 as AfectaEstadistica, 
				0 as AfectaEstadisticaMontos, 
				0 as EsDecimal, 
				sum(L.CantidadVendida - L.CantidadRemision_Admon) as CantidadVendida, 
				sum(L.CantidadVendida - L.CantidadRemision_Admon) as CantidadVendida_Base,
				0 as Cantidad_Agrupada, 
				0 as Cantidad_Remisionada, 0 as Cantidad_Remisionada_Agrupada,  

				@GrupoProceso_Remision as GrupoProceso, 0 as IncluidoEnExcepcion, 
				0 as TipoDeAsignacion, 0 as IdGrupoDeRemisiones, V.GrupoDispensacion, 
				0 as EsControlado, 0 as EsAntibiotico, 0 as EsRefrigerado, 
				
				0 as EsExcepcionPrecio  
				  
		From #tmp____VentasDet_Lotes L (NoLock) 
		Inner Join #tmpVentas V (NoLock) 
			On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 
		--Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) -- and P.ClaveSSA = '010.000.1937.00' ) 
		Where -- ClaveLote Like '%*%' 
			  (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion  
			  --and L.EnRemision_Admon = 0 and L.RemisionFinalizada = 0 
			  ----and L.CantidadVendida > 0 
			  and ( L.CantidadVendida - L.CantidadRemision_Admon ) > 0 
			  and @bExisteInformacion_x_Procesar = 1 ---- Validacion de Corto circuito -- 20180523.1400 
		Group by 
				V.EsAlmacen, V.EsFarmacia, V.EsUnidosis, 
				L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
				V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
				V.IdFuenteFinanciamiento, V.IdFinanciamiento, V.IdDocumento, 
				V.Procesado, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, V.GrupoDispensacion  
	  End 
	  
	--If @bPrint_MarcaDeTiempo = 1  Print @sSepardor + 'MT: OBTENER DATOS 03 ==>  ' + convert(varchar(50), getdate(), 120) 	 
	--If @bPrint_MarcaDeTiempo_Select = 1 Select 'OBTENER DATOS 03' as Proceso, getdate() as MarcaDeTiempo 

	--Select 'Resumen 00' as Campo, sum(CantidadVendida) as Piezas, count(*) as Registros From #tmpLotes (NoLock) 
	--select * from #tmpLotes 
	--#tmp____VentasDet_Lotes 
	--delete from #tmpLotes 
	--Select 'Resumen 0000' as Campo, sum(CantidadVendida) as Piezas, count(*) as Registros From #tmpLotes (NoLock) 

	--Select 'Resumen' as Campo, IdFarmacia, count(distinct FolioVenta) as Registros, sum(CantidadVendida) as Piezas 
	--From #tmpLotes (NoLock) 
	--Group by IdFarmacia 
	--Order by IdFarmacia 
	----Order by FolioVenta, Partida 


	----select 'VENTAS' as VENTAS, IdFarmacia, count(*) as Ventas from #tmpVentas   group by IdFarmacia 
	--select 'VENTAS' as VENTAS, * from #tmpVentas   
	--select 'LOTES' as LOTES, * from #tmpLotes   
	--select * from #tmp____VentasDet_Lotes 


	--delete from #tmpVentas 
	--return 

	--Select @Criterio_FiltroFecha___Vales as Criterio_FiltroFecha___Vales 

	--Select * 
	--From #tmp____VentasDet_Lotes L (NoLock) 
	--Inner Join #tmpVentas V (NoLock) 
	--	On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioVenta = V.FolioVenta ) 	 
	--------------------------------------------------------------------------------------------------------------- 
	------------------------------------------- Acompletar la informacion de Claves SSA 

	--select 'DATOS A PROCESAR P01' as Resumen, * from #tmpLotes     
	

	-------------------------------------------
	-- Se obtiene la Clave SSA y la Tasa Iva --
	------------------------------------------- 	
	Update L Set 
		IdClaveSSA_P = P.IdClaveSSA_Sal, ClaveSSA_P = P.ClaveSSA, 
		IdClaveSSA = P.IdClaveSSA_Sal, 
		ClaveSSA = P.ClaveSSA, 
		ContenidoPaquete_ClaveSSA = P.ContenidoPaquete_ClaveSSA, 
		IdTipoProducto = P.TipoDeClave, TasaIva = P.TasaIva, 
		Cantidad_Agrupada = round( (CantidadVendida / (P.ContenidoPaquete_ClaveSSA * 1.0) ), 4)  
		, EsControlado = (case when P.EsControlado = 1 then 1 else 2 end)		---- Separar por este campo las remisiones 
		, EsAntibiotico = 0		---- No separar por este campo las remisiones 
		, EsRefrigerado = 0		---- No separar por este campo las remisiones 
		--, EsAntibiotico = (case when P.EsAntibiotico = 1 then 1 else 2 end)	---- Separar por este campo las remisiones 
		--, EsRefrigerado = (case when P.EsRefrigerado = 1 then 1 else 2 end)	---- Separar por este campo las remisiones 
	From #tmpLotes L(NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto And L.CodigoEAN = P.CodigoEAN ) 


	Update L Set Cantidad_Agrupada = CantidadVendida, ContenidoPaquete_ClaveSSA = 1   
	From #tmpLotes L (NoLock) 
	Where EsUnidosis = 1 

	
	--select 'LOTES - X01' as LOTES, * from #tmpLotes   


	if @Separar__Controlados = 0 
	Begin	
		Update L Set EsControlado = 0 
		From #tmpLotes L (NoLock) 
	End  



	----select 'VENTAS' as VENTAS, @bExisteInformacion_x_Procesar as bExisteInformacion_x_Procesar, * from #tmpVentas   
	----select * from #tmpLotes     


	--------------------------------------------------------------------------------------------------------------- 
	--------------- Reemplazo de Claves 
	--select 'Multiplos antes de proceso' as Resumen, * from #tmpLotes     
	--- select top 5000 'Multiplos Procesados' as Resumen, * from #tmpLotes     

	--select 'DATOS A PROCESAR P02' as Resumen, * from #tmpLotes     

	Update L Set Relacionada = 1, 	
		IdClaveSSA = P.IdClaveSSA_Relacionada, 
		ClaveSSA = P.ClaveSSA, ContenidoPaquete_ClaveSSA = P.ContenidoPaquete, 
		MultiploRelacion = Multiplo, 
		CantidadVendida = (CantidadVendida / (Multiplo * 1.0)), 
		--Cantidad_Agrupada = round( (CantidadVendida / (P.ContenidoPaquete * 1.0) ), 4)  
		Cantidad_Agrupada = round( ((CantidadVendida / (Multiplo * 1.0)) / (P.ContenidoPaquete * 1.0) ), 4)    
	From #tmpLotes L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( 
			L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente 
			and L.IdClaveSSA = P.IdClaveSSA_Relacionada 
			--and L.ClaveSSA_P = P.ClaveSSA_Relacionada 
			and P.Status = 'A' 
			) 
	Where EsUnidosis = 0 

	--select 'LOTES - X02' as LOTES, * from #tmpLotes   

	Delete From #vw_Claves_A_Procesar 
	Insert Into #vw_Claves_A_Procesar 
	Select P.ClaveSSA, '' as DescripcionClave 
	From #tmpLotes P 
	Group by P.ClaveSSA  


	---------------------- 
	--select 'DATOS A PROCESAR P03' as Resumen, * from #tmpLotes     

	--Select 'DATOS A PROCESAR' as Claves, * From #vw_Claves_A_Procesar 
	---------------------- 

	------------------------------------------- Acompletar la informacion de Claves SSA 
	--------------------------------------------------------------------------------------------------------------- 


	--------------------------------------------------------------------------------------------------------------- 
	--------------- Se aplica el Factor de licitacion  
	-- Set @Aplicar_ImporteDocumentos = 0 
	If @ClaveSSA_ListaExclusion <> '' 
	Begin 
		
		Print 'Claves excluidas :  ' + @ClaveSSA_ListaExclusion  
		Set @sSql = 'Delete From #tmpLotes Where ClaveSSA In ( ' + @ClaveSSA_ListaExclusion  +  ' ) '   
		--Print @sSql 
		Exec(@sSql)   
		--Print '' 
		
	End 


	----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 
	If @EsRelacionFacturaPrevia = 1  
	Begin 

		If @EsRelacionMontos = 1 
		Begin 
			Print 'REVISAR RELACION DE MONTOS'
			/* 
			Insert Into FACT_Remisiones__RelacionFacturas_x_Importes (  IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, FechaRegistro, Importe_Facturado, Importe_Distribuido ) 
			Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, getdate() as FechaRegistro, sum(Total) as Importe_Facturado, 0 as Importe_Distribuido 
			From FACT_CFD_Documentos_Generados_Detalles D (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio
				and Not Exists 
				( 
					Select * 
					From FACT_Remisiones__RelacionFacturas_x_Importes E (NoLock) 
					Where D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.Serie = E.Serie and D.Folio = E.Folio 
				) 
			Group by  D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio 


			Select @iImporte_FacturaRelacionada = ( Importe_Facturado - Importe_Distribuido ) 
			From FACT_Remisiones__RelacionFacturas_x_Importes D (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio 


			--------------- Evitar sobrepasar el limite 
			If @iImporte_FacturaRelacionada < 0 
			   Set @iImporte_FacturaRelacionada = 0 

			---------------------- Forzar el proceso por montos si se encuentra SERIE-FOLIO 
			Set @bExisteInformacion_x_Procesar = 1 
			If @iImporte_FacturaRelacionada < 1 and @EsRelacionMontos = 1  
			   Set @bExisteInformacion_x_Procesar = 0  

			If @iImporte_FacturaRelacionada > 0  
			   Set @EsRelacionMontos = 1 
			*/ 
		End 

		----------------------------------- Leer configuracion en base a facturas-claves 
		If @EsRelacionMontos = 0  
		Begin 

			---------------------- Proceso movido a SP spp_FACT_Comprobacion_Facturas__Validar
			/* 
			Insert Into FACT_Remisiones__RelacionFacturas 
			(	IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdFuenteFinanciamiento, IdFinanciamiento, 
				FechaRegistro, ClaveSSA, ContenidoPaquete, 
				Cantidad_Facturada, Cantidad_Distribuida, 
				CantidadAgrupada_Facturada, CantidadAgrupada_Distribuida ) 
			Select 
				D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 
				@IdFuenteFinanciamiento_FacturaRelacionada as IdFuenteFinanciamiento, @IdFinanciamiento_FacturaRelacionada as IdFinanciamiento, 
				getdate() as FechaRegistro, 
				D.Clave as ClaveSSA, 
				max(C.ContenidoPaquete) as ContenidoPaquete,  
				(case when @FacturaPreviaEnCajas = 1 Then sum(Cantidad * C.ContenidoPaquete) else sum(Cantidad) end)as Cantidad_Facturada, 
				0 as Cantidad_Distribuida, 
				sum(Cantidad) as CantidadAgrupada_Facturada, 
				0 as CantidadAgrupada_Distribuida 
			From FACT_CFD_Documentos_Generados_Detalles D (NoLock) 
			Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.Clave = C.ClaveSSA ) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio 
				and @IdFuenteFinanciamiento_FacturaRelacionada <> '' and @IdFinanciamiento_FacturaRelacionada <> '' 
				and Not Exists 
				( 
					Select * 
					From FACT_Remisiones__RelacionFacturas R (NoLock) 
					Where D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmacia = R.IdFarmacia 
						and D.Serie = R.Serie and D.Folio = R.Folio and D.Clave = R.ClaveSSA 
				)  
			Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Clave
			*/ 

			-----Delete From #tmp_ListaClaves_VentasPrevias 

			--------------- Las facturas se adaptan a la Fuente de financiamento que las invoca 
			Insert Into #tmp_ListaClaves_VentasPrevias 
			Select --- 'RELACION DE FACTURA-VENTA PREVIA' as Proceso, 
				--E.IdFuenteFinanciamiento, E.IdFinanciamiento, 
				@IdFuenteFinanciamiento as IdFuenteFinanciamiento, 	
				@IdFinanciamiento as IdFinanciamiento, 
				0 as Procesa_Venta, 0 as Procesa_Consigna, 0 as Procesa_Producto, 0 as Procesa_Servicio, 
				D.ClaveSSA, D.ContenidoPaquete, 
				sum(D.CantidadAgrupada_Facturada - D.CantidadAgrupada_Distribuida) as CantidadAgrupada_Disponible, 
				sum(D.Cantidad_Facturada - D.Cantidad_Distribuida) as CantidadDisponible, 1 as EsProcesable 
			-- Into #tmp_ListaClaves_VentasPrevias  
			From FACT_Remisiones__RelacionFacturas D (NoLock) 
			Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio 
			Group by 
				--E.IdFuenteFinanciamiento, E.IdFinanciamiento, 
				--E.Procesa_Venta, E.Procesa_Consigna, E.Procesa_Producto, E.Procesa_Servicio, 
				D.ClaveSSA, D.ContenidoPaquete 
			Having sum(Cantidad_Facturada - Cantidad_Distribuida) > 0   --------------- VALIDACION POR PIEZAS 


			--select * from #tmp_ListaClaves_VentasPrevias Where ClaveSSA = '010.000.0101.00'


			----------------------- REEMPLAZO DE DATOS CON EL SEGUNDO NIVEL DE INFORMACION 
			------ Se elimina este proceso 
			/* 
			If Exists ( Select top 1 * 
						From FACT_Remisiones__RelacionFacturas_x_Farmacia (NoLock) 
						Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera 
							and Serie = @Serie and Folio = @Folio and IdFarmacia_Relacionada = @IdFarmacia 
					  )
			Begin 

				Set @EsRelacionFacturaPrevia_x_Farmacia = 1  


				Delete From #tmp_ListaClaves_VentasPrevias 

				Insert Into #tmp_ListaClaves_VentasPrevias 
				Select --- 'RELACION DE FACTURA-VENTA PREVIA' as Proceso, 
					IdFuenteFinanciamiento, IdFinanciamiento, 
					ClaveSSA, ContenidoPaquete, 
					sum(CantidadAgrupada_Facturada - CantidadAgrupada_Distribuida) as CantidadAgrupada_Disponible, 
					sum(Cantidad_Facturada - Cantidad_Distribuida) as CantidadDisponible 
				-- Into #tmp_ListaClaves_VentasPrevias  
				From FACT_Remisiones__RelacionFacturas_x_Farmacia D (NoLock) 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and IdFarmacia_Relacionada = @IdFarmacia  
				Group by IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, ContenidoPaquete 
				Having sum(Cantidad_Facturada - Cantidad_Distribuida) > 0   --------------- VALIDACION POR PIEZAS 

			End 
			*/ 
			----------------------- REEMPLAZO DE DATOS CON EL SEGUNDO NIVEL DE INFORMACION 


			--Select 'ANTES LIMPIEZA' as T, * From #tmpLotes L (NoLock) 

			Delete #tmpLotes 
			From #tmpLotes L (NoLock) 
			Where Not Exists ( Select * From #tmp_ListaClaves_VentasPrevias D (NoLock) Where L.ClaveSSA = D.ClaveSSA ) 	


			--Select 'DESPUES VALIDACION' = count(distinct ClaveSSA) From #tmpLotes 


			----Select 
			----	'Ventas Previas' = ( Select count(*) From #tmp_ListaClaves_VentasPrevias (NoLock) ),  
			----	'Dispensacion' = ( Select count(*) From #tmp_ListaClaves_VentasPrevias___Dispensacion (NoLock) ),  
			----	'Dispensacion detallado' = ( Select count(Distinct ClaveSSA) From #tmpLotes (NoLock) ) 
		End 

	End 


	------------------- Modificacion 20220418.1630 
	If @EsRelacionDocumentoPrevio = 1 
	Begin 

		Delete From #tmp_ListaClaves_VentasPrevias 

		Insert Into #tmp_ListaClaves_VentasPrevias 
		Select  
			@IdFuenteFinanciamiento as IdFuenteFinanciamiento, 	
			@IdFinanciamiento as IdFinanciamiento, 
			E.Procesa_Venta, E.Procesa_Consigna, E.Procesa_Producto, E.Procesa_Servicio, 
			D.ClaveSSA, D.ContenidoPaquete, 
			sum(D.CantidadAgrupada_A_Comprobar - D.CantidadAgrupada_Distribuida) as CantidadAgrupada_Disponible, 
			sum(D.Cantidad_A_Comprobar - D.Cantidad_Distribuida) as CantidadDisponible, 0 as EsProcesable  
		-- Into #tmp_ListaClaves_VentasPrevias  
		From FACT_Remisiones__RelacionDocumentos D (NoLock) 
		Inner Join FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) On ( D.FolioRelacion = E.FolioRelacion ) 
		Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.FolioRelacion = @FolioRelacionDocumento   
		Group by 
			--E.IdFuenteFinanciamiento, 
			--E.IdFinanciamiento, 
			E.Procesa_Venta, E.Procesa_Consigna, E.Procesa_Producto, E.Procesa_Servicio, 
			D.ClaveSSA, D.ContenidoPaquete 
		Having sum(Cantidad_A_Comprobar - Cantidad_Distribuida) > 0 

		----Update C Set EsProcesable = 1 
		----From #tmp_ListaClaves_VentasPrevias C 
		----Where 
		----	( Procesa_Venta = 1 and @TipoDispensacion = 0 ) 
		----	and (
		----			( Procesa_Producto = 1 and @TipoDeRemision = 1 ) 
		----			or 
		----			( Procesa_Servicio = 1 and @TipoDeRemision = 1 ) 
		----		) 

		----Update C Set EsProcesable = 1 
		----From #tmp_ListaClaves_VentasPrevias C 
		----Where 
		----	( Procesa_Consigna = 1 and @TipoDispensacion = 1 ) 
		----	and (
		----			( Procesa_Producto = 1 and @TipoDeRemision = 1 ) 
		----			or 
		----			( Procesa_Servicio = 1 and @TipoDeRemision = 1 ) 
		----		) 

		----select 'COMPROBACION' as Comprobacion, * from #tmp_ListaClaves_VentasPrevias 	


		Delete #tmpLotes 
		From #tmpLotes L (NoLock) 
		Where Not Exists ( Select * From #tmp_ListaClaves_VentasPrevias D (NoLock) Where L.ClaveSSA = D.ClaveSSA ) 	

	End 
	----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 


	------------------------------------------------------------------------------------------------------------------------------------------------ 
	-------------------- 20180523.1350   JESÚS DÍAZ 
	---------- DEPURACION GENERAL DEL PROCESO EN CASO DE ESTAR PROCESANDO EN BASE A FACTURAS ANTICIPADAS Y MONTOS  	
	--If @bExisteInformacion_x_Procesar = 0  
	--Begin 
	--	Select 'ELIMINANDO DATOS', (select count(*) from #tmpLotes) as Registros  
	--	Delete From #tmpLotes Where @bExisteInformacion_x_Procesar = 0   
	--End 
	---------- DEPURACION GENERAL DEL PROCESO EN CASO DE ESTAR PROCESANDO EN BASE A FACTURAS ANTICIPADAS Y MONTOS  	
	------------------------------------------------------------------------------------------------------------------------------------------------ 



--	select 'FACTOR 001', * from #tmpLotes     

	--------------------------------------------------------------------------------------------------------------- 
	--------------- Se aplica el Factor de licitacion  
	Update L Set  
		CantidadVendida = (CantidadVendida * IsNull(P.Factor, 1)),     
		Cantidad_Agrupada = round( ((CantidadVendida * IsNull(P.Factor, 1)) / (P.ContenidoPaquete_Licitado * 1.0) ), 4),    ---- Se implementa el Contenido Paquete en base a Licitacion 
		Factor = IsNull(P.Factor, 1), 
		ContenidoPaquete_Licitado = P.ContenidoPaquete_Licitado 
	From #tmpLotes L(NoLock) 
	Left Join #vw_Claves_Precios_Asignados P (NoLock) On ( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  
		and EsUnidosis = 0 

	
	----From #tmpLotes L(NoLock) 
	----Left Join vw_Claves_Precios_Asignados_DosisUnitaria P (NoLock) On ( L.ClaveSSA = P.ClaveSSA )  
	----Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  
	----	and EsUnidosis = 1 

	--select 'FACTOR 002', * from #tmpLotes     


--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
------------------------------------------------------- Se obtienen las claves de Insumos ó Administracion   
	--If @bPrint_MarcaDeTiempo_Select = 1 Select 'FUENTES DE FINANCIAMIENTO 01' as Proceso, getdate() as MarcaDeTiempo 
	Select * 
	Into #tmp_FF 
	From FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) 
	Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and D.Status = 'A' 


	--select * from #tmp_FF 


	If @IdFinanciamiento <> '' and @IdFinanciamiento <> '*' 
	Begin 
		Print 'Filtrando por Fuente de Financiamiento : ' + @IdFinanciamiento 
		Delete From #tmp_FF Where IdFinanciamiento <> @IdFinanciamiento  
	End 

	--select * from #tmp_FF 

	Select 
		@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
		D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
		ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
		cast(F.Referencia_01 as varchar(20) ) as Referencia_01, cast(F.Referencia_02 as varchar(50) ) as Referencia_02, cast(F.Referencia_03 as varchar(1000) ) as Referencia_03,  
		cast(F.Referencia_04 as varchar(20) ) as Referencia_04, cast(F.Referencia_05 as varchar(50) ) as Referencia_05, 
		0 as ContenidoPaquete, 
		0 as Agrupacion, 
		F.CantidadPresupuestadaPiezas, F.CantidadPresupuestada, F.CantidadAsignada, 
		(F.CantidadPresupuestadaPiezas - F.CantidadAsignada) as CantidadRestante, 
		cast(0 as numeric(14, 4)) as Precio, 
		cast(0 as numeric(14, 4)) as PrecioUnitario, 
		cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 
		cast(-1 as numeric(14, 4)) as TasaIva, 
		1 as Partida, 
		1 as AfectaEstadistica, 
		1 as AfectaEstadisticaMontos, 
		identity(int, 1, 1) as Keyx  
	Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F (NoLock) 
		On ( D.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and D.IdFinanciamiento = F.IdFinanciamiento 
			 and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.ClaveSSA = D.ClaveSSA ) 
	Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
	Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
	Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and D.Status = 'A' 
		  --and D.ClaveSSA like '%' + @ClaveSSA + '%'    
		-- and D.ClaveSSA like '%2190%' 
		-- and @FechaDeRevision in ( 1, 2 )  

	--Select '01' as Consulta, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 


	----select * from #tmpLotes  
	--select * from #tmp_FF 
	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 


	------------------------------------------- CONFIGURACION ESPECIAL PARA COBRO SECCIONADO DE SERVICIO     20180715.0020	
	Select 
		@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
		D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
		ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
		cast(F.Referencia_01 as varchar(20) ) as Referencia_01, cast(F.Referencia_02 as varchar(50) ) as Referencia_02, cast(F.Referencia_03 as varchar(1000) ) as Referencia_03,  
		cast(F.Referencia_04 as varchar(20) ) as Referencia_04, cast(F.Referencia_05 as varchar(50) ) as Referencia_05, 
		0 as ContenidoPaquete, 
		0 as Agrupacion, 
		F.CantidadPresupuestadaPiezas, F.CantidadPresupuestada, F.CantidadAsignada, 
		(F.CantidadPresupuestadaPiezas - F.CantidadAsignada) as CantidadRestante, 
		cast(0 as numeric(14, 4)) as Precio, 
		cast(0 as numeric(14, 4)) as PrecioUnitario, 
		cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 
		cast(-1 as numeric(14, 4)) as TasaIva, 
		0 as Partida, 
		0 as AfectaEstadistica, 
		0 as AfectaEstadisticaMontos, 
		identity(int, 1, 1) as Keyx  
	Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze  
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F (NoLock) 
		On ( D.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and D.IdFinanciamiento = F.IdFinanciamiento 
			 and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.ClaveSSA = D.ClaveSSA ) 
	--Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze DF (NoLock) 
	--	On ( F.IdFuenteFinanciamiento = DF.IdFuenteFinanciamiento and F.IdFinanciamiento = DF.IdFinanciamiento 
	--		 and F.IdEstado = DF.IdEstado and F.IdFarmacia = DF.IdFarmacia and F.ClaveSSA = DF.ClaveSSA and F.Referencia_01 = DF.Referencia_01 ) 
	Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
	Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
	Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and D.Status = 'A' 
		And 1 = 0 
	------------------------------------------- CONFIGURACION ESPECIAL PARA COBRO SECCIONADO DE SERVICIO     20180715.0020	
	

	--Select '' as Get_FF, * 
	--From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
	--Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
	--Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
	--Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento  and D.Status = 'A' 		


	--select @NivelInformacion_Remision as NivelInformacion_Remision
	If @Aplicar_ImporteDocumentos = 1 or @NivelInformacion_Remision = 1 
	Begin 

		Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA  
		Select 
			@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
			D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
			ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
			cast( '' as varchar(20) ) as Referencia_01, cast( '' as varchar(50) ) as Referencia_02, cast( '' as varchar(1000) ) as Referencia_03,  
			cast('' as varchar(20) ) as Referencia_04, cast( '' as varchar(50) ) as Referencia_05, 
			-- cast(D.Referencia_01 as varchar(20) ) as Referencia_01, cast(D.Referencia_02 as varchar(50) ) as Referencia_02, cast(D.Referencia_03 as varchar(1000) ) as Referencia_03, 	 
			0 as ContenidoPaquete, 
			0 as Agrupacion, 
			D.CantidadPresupuestadaPiezas, D.CantidadPresupuestada, D.CantidadAsignada, 
			(D.CantidadPresupuestadaPiezas - D.CantidadAsignada) as CantidadRestante, 
			cast(0 as numeric(14, 4)) as Precio, 
			cast(0 as numeric(14, 4)) as PrecioUnitario, 
		    cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 
			cast(-1 as numeric(14, 4)) as TasaIva, 
			1 as Partida, 
			1 as AfectaEstadistica, 
			1 as AfectaEstadisticaMontos  			 
		From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
		Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
		Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
		Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento  and D.Status = 'A' 		
		      -- and D.ClaveSSA like '%' + @ClaveSSA + '%'   



		----select 'CONSULTA 02' as Consulta_02_1, * from #vw_Claves_A_Procesar  

		----select 'CONSULTA 02' as Consulta_02_4, * from #tmp_FF

		----select 'CONSULTA 02' as Consulta_02_2, * 
		----From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
		----Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
		----inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 


		----select 'CONSULTA 02' as Consulta_02_3, * 
		----From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
		----inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 



		--Select 'CONSULTA 02' as Consulta_02, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 

		--Select @IdFuenteFinanciamiento as IdFuenteFinanciamiento, @Referencia_Beneficiario as Referencia_Beneficiario 
		------Select 'CONSULTA 03' as Consulta_03, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 

		--select 'Nivel_Informacion_01' as TIPO, * from #tmpLotes  
		--select 'Nivel_Informacion_01' as TIPO, * from #tmp_FF 
		--select 'Nivel_Informacion_01' as TIPO, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	End 


	If @NivelInformacion_Remision = 3  
	Begin 

		Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA  
		Select 
			@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
			D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
			ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
			--cast(Referencia_01 as varchar(20)) as Referencia_01, 
			cast(Referencia_01 as varchar(20) ) as Referencia_01, cast(Referencia_02 as varchar(50) ) as Referencia_02, cast(Referencia_03 as varchar(1000) ) as Referencia_03,  			
			cast(Referencia_04 as varchar(20) ) as Referencia_04, cast(Referencia_05 as varchar(50) ) as Referencia_05, 
			0 as ContenidoPaquete, 
			0 as Agrupacion, 
			D.CantidadPresupuestadaPiezas, D.CantidadPresupuestada, D.CantidadAsignada, 
			(D.CantidadPresupuestadaPiezas - D.CantidadAsignada) as CantidadRestante, 
			cast(0 as numeric(14, 4)) as Precio, 
			cast(0 as numeric(14, 4)) as PrecioUnitario, 
			cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 
			cast(-1 as numeric(14, 4)) as TasaIva, 
			1 as Partida, 
			1 as AfectaEstadistica, 
			1 as AfectaEstadisticaMontos  
		From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario D (NoLock) 
		Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
		Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
		Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and IdBeneficiario = @Referencia_Beneficiario  and D.Status = 'A' 
		      -- and D.ClaveSSA like '%' + @ClaveSSA + '%'   
		
		----Select @IdFuenteFinanciamiento as IdFuenteFinanciamiento, @Referencia_Beneficiario as Referencia_Beneficiario 
		--------Select 'CONSULTA 03' as Consulta_03, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 

		----select 'Nivel_Informacion_02' as TIPO, * from #tmpLotes  
		----select 'Nivel_Informacion_02' as TIPO, * from #tmp_FF 
		----select 'Nivel_Informacion_02' as TIPO, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	End 


	---------------------------- ASIGNACION DE PRECIO DE PRODUCTO 
	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	Update L Set Precio = P.Precio, PrecioUnitario = P.PrecioUnitario, ContenidoPaquete = P.ContenidoPaquete   
	From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
	Inner Join #vw_Claves_Precios_Asignados P (NoLock) On ( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.Status = 'A'  

	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	Update L Set Precio_DosisUnitaria = P.Precio_DosisUnitaria -- , PrecioUnitario = P.PrecioUnitario, ContenidoPaquete = P.ContenidoPaquete   
	From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
	Inner Join #vw_Claves_Precios_Asignados_DosisUnitaria P (NoLock) On ( L.ClaveSSA = P.ClaveSSA )  
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente  and P.StatusRelacion_DosisUnitaria = 'A'  

	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	---------------------------- ASIGNACION DE PRECIO DE PRODUCTO 



	--#vw_Claves_Precios_Asignados_DosisUnitaria 

	--	select * from vw_Claves_Precios_Asignados_DosisUnitaria 


	----------------- Revision 
	--select * from #tmpLotes  
	--select * from #tmp_FF 
	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 


	----------------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 
	------If @EsRelacionFacturaPrevia = 1  
	------Begin 
	------	------------ REVISION x FACTURA 
	------	If @EsRelacionMontos = 0 
	------	Begin  
	------		----Select * 
	------		----From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock)  
	------		----Where ClaveSSA like '%0101.00%'   

	------		--Select * From #tmp_ListaClaves_VentasPrevias D (NoLock) 
	------		--Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	------		Delete #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	------		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
	------		Where Not Exists 
	------		( 
	------			Select * From #tmp_ListaClaves_VentasPrevias D (NoLock) 
	------			Where L.ClaveSSA = D.ClaveSSA and L.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and L.IdFinanciamiento = D.IdFinanciamiento 
	------		) 	


	------		Update L Set CantidadRestante = D.CantidadDisponible 
	------		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
	------		Inner Join #tmp_ListaClaves_VentasPrevias D (NoLock) 
	------			On ( L.ClaveSSA = D.ClaveSSA and L.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and L.IdFinanciamiento = D.IdFinanciamiento ) 	


	------		--Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	------	End 

	------End 
	
	--------Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	----------------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 



	--select @TipoDeRemision as TipoDeRemision
    ------------------------------------------------------------------------------------------------------------------------------------------------ 
    ------------------------------------------------------------------------------------------------------------------------------------------------ 
	------------------------------------ Determinar las claves de administracion   
	--Select @TipoDeRemision, @Aplicar_ImporteDocumentos, @NivelInformacion_Remision 
	If @TipoDeRemision = 2 or @TipoDeRemision = 6	---- and @FechaDeRevision in ( 1, 2, 3 ) 
	Begin 
		Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		Select 
			@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
			D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
			ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
			--cast(Referencia_01 as varchar(20)) as Referencia_01, 
			--cast( '' as varchar(20) ) as Referencia_01, cast( '' as varchar(50) ) as Referencia_02, cast( '' as varchar(1000) ) as Referencia_03, 
			cast(F.Referencia_01 as varchar(20) ) as Referencia_01, cast(F.Referencia_02 as varchar(50) ) as Referencia_02, cast(F.Referencia_03 as varchar(1000) ) as Referencia_03,  	
			cast(F.Referencia_04 as varchar(20) ) as Referencia_04, cast(F.Referencia_05 as varchar(50) ) as Referencia_05, 
			0 as ContenidoPaquete, 
			D.Agrupacion, 
			F.CantidadPresupuestadaPiezas, F.CantidadPresupuestada, F.CantidadAsignada, 
			(F.CantidadPresupuestadaPiezas - F.CantidadAsignada) as CantidadRestante, 
			-- 0, 0, 0, 
			cast(0 as numeric(14, 4)) as Precio, 
			cast(0 as numeric(14, 4)) as PrecioUnitario, 
			cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 
			cast(-1 as numeric(14, 4)) as TasaIva,  
			1 as Partida, 
			1 as AfectaEstadistica, 
			1 as AfectaEstadisticaMontos     	 
		From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA D (NoLock) 
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia F (NoLock) 
			On ( D.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and D.IdFinanciamiento = F.IdFinanciamiento 
				 and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.ClaveSSA = D.ClaveSSA ) 
		Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
		Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
		Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento  and D.Status = 'A' -- and D.Status = 'A' 
			  -- and D.ClaveSSA like '%' + @ClaveSSA + '%'   

		--select 'P01' as R, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

		--select * from #tmp_FF 


		------------------------------------------- CONFIGURACION ESPECIAL PARA COBRO SECCIONADO DE SERVICIO     20180715.0020
		Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze	 
		Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze	
		Select 
			@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
			D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
			ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
			cast(F.Referencia_01 as varchar(20) ) as Referencia_01, cast(F.Referencia_02 as varchar(50) ) as Referencia_02, cast(F.Referencia_03 as varchar(1000) ) as Referencia_03,  
			cast(F.Referencia_04 as varchar(20) ) as Referencia_04, cast(F.Referencia_05 as varchar(50) ) as Referencia_05, 
			0 as ContenidoPaquete, 
			0 as Agrupacion, 
			F.CantidadPresupuestadaPiezas, F.CantidadPresupuestada, F.CantidadAsignada, 
			(F.CantidadPresupuestadaPiezas - F.CantidadAsignada) as CantidadRestante, 
			cast(DF.Costo as numeric(14, 4)) as Precio, 
			cast(DF.Costo as numeric(14, 4)) as PrecioUnitario, 	
			cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 	
			cast(DF.TasaIVA as numeric(14, 4)) as TasaIva, 
			DF.Partida, 
			DF.AfectaEstadistica, 
			DF.AfectaEstadisticaMontos  
		From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
		Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F (NoLock) 
			On ( D.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and D.IdFinanciamiento = F.IdFinanciamiento 
				 and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.ClaveSSA = D.ClaveSSA ) 
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze DF (NoLock) 
			On ( F.IdFuenteFinanciamiento = DF.IdFuenteFinanciamiento and F.IdFinanciamiento = DF.IdFinanciamiento 
				 and F.IdEstado = DF.IdEstado and F.IdFarmacia = DF.IdFarmacia and F.ClaveSSA = DF.ClaveSSA and F.Referencia_01 = DF.Referencia_01 ) 
		Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
		Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
		Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and D.Status = 'A' 
		------------------------------------------- CONFIGURACION ESPECIAL PARA COBRO SECCIONADO DE SERVICIO     20180715.0020


		----Select '04' as Consulta, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 
		----Select '05' as Consulta, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze


		--select distinct D.IdFuenteFinanciamiento, D.IdFinanciamiento 
		--from FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA d 


		If @Aplicar_ImporteDocumentos = 1 or @NivelInformacion_Remision = 1 
		Begin 
			Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA  
			
			Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA  
			Select 
				@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
				D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
				ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
				-- cast('' as varchar(20)) as Referencia_01, 
				cast('' as varchar(20) ) as Referencia_01, cast('' as varchar(50) ) as Referencia_02, cast('' as varchar(1000) ) as Referencia_03, 
				cast('' as varchar(20) ) as Referencia_04, cast( '' as varchar(50) ) as Referencia_05, 
				--cast(Referencia_01 as varchar(20) ) as Referencia_01, cast(Referencia_02 as varchar(50) ) as Referencia_02, cast(Referencia_03 as varchar(1000) ) as Referencia_03, 
				0 as ContenidoPaquete, 
				0 as Agrupacion, 
				D.CantidadPresupuestadaPiezas, D.CantidadPresupuestada, D.CantidadAsignada, 
				(D.CantidadPresupuestadaPiezas - D.CantidadAsignada) as CantidadRestante, 
				cast(0 as numeric(14, 4)) as Precio, 
				cast(0 as numeric(14, 4)) as PrecioUnitario, 
				cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 
				cast(-1 as numeric(14, 4)) as TasaIva,  
				1 as Partida, 
				1 as AfectaEstadistica, 
				1 as AfectaEstadisticaMontos 
			From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA D (NoLock) 
			Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
			Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
			Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento  and D.Status = 'A' 	
			      --and D.ClaveSSA like '%' + @ClaveSSA + '%'    	

		End 

		If @NivelInformacion_Remision = 3  
		Begin 

			Delete From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
			Insert Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA  
			Select 
				@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdCliente as IdCliente, @IdSubCliente as IdSubCliente, 
				D.IdFuenteFinanciamiento, D.IdFinanciamiento, C.Prioridad, 
				ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
				--cast(Referencia_01 as varchar(20)) as Referencia_01, 
				--cast(Referencia_01 as varchar(20) ) as Referencia_01, cast(Referencia_02 as varchar(50) ) as Referencia_02, cast(Referencia_03 as varchar(1000) ) as Referencia_03, 
				cast(Referencia_01 as varchar(20) ) as Referencia_01, cast(Referencia_02 as varchar(50) ) as Referencia_02, cast(Referencia_03 as varchar(1000) ) as Referencia_03, 
				cast(Referencia_04 as varchar(20) ) as Referencia_04, cast(Referencia_05 as varchar(50) ) as Referencia_05, 
				0 as ContenidoPaquete, 
				0 as Agrupacion, 
				D.CantidadPresupuestadaPiezas, D.CantidadPresupuestada, D.CantidadAsignada, 
				(D.CantidadPresupuestadaPiezas - D.CantidadAsignada) as CantidadRestante, 
				cast(0 as numeric(14, 4)) as Precio, 
				cast(0 as numeric(14, 4)) as PrecioUnitario, 
				cast(0 as numeric(14, 4)) as Precio_DosisUnitaria, 
				cast(-1 as numeric(14, 4)) as TasaIva,  
				1 as Partida, 
				1 as AfectaEstadistica,  
				1 as AfectaEstadisticaMontos 
			From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario D (NoLock) 
			Inner Join #tmp_FF C (NoLock) On ( D.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and D.IdFinanciamiento = C.IdFinanciamiento )  
			Inner Join #vw_Claves_A_Procesar LC (NoLock) On ( D.ClaveSSA = LC.ClaveSSA ) 
			Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento  and IdBeneficiario = @Referencia_Beneficiario  and D.Status = 'A' 
			      --and D.ClaveSSA like '%' + @ClaveSSA + '%'    

			--Select '06' as Consulta, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 
		End 


		--select '0001' as RESUMEN, * 
		--from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 


		Update L Set 
			Precio = P.Costo, 
			PrecioUnitario = P.CostoUnitario, 
			TasaIva = P.TasaIva 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA P (NoLock) 
			On ( L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente 
				and L.IdFuenteFinanciamiento = P.IdFuenteFinanciamiento and L.IdFinanciamiento = P.IdFinanciamiento
				and L.ClaveSSA = P.ClaveSSA ) 
		Where P.IdFuenteFinanciamiento = @IdFuenteFinanciamiento 	
			

		Update L Set 
			--Precio_DosisUnitaria = dbo.fg_PRCS_Redondear( P.Costo / DU.Factor_DosisUnitaria, 2, 0 ) 
			Precio_DosisUnitaria = dbo.fg_PRCS_Redondear( P.Costo , 2, 0 ) 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
		Inner Join #vw_Claves_Precios_Asignados_DosisUnitaria DU (NoLock) 
			On ( L.ClaveSSA = DU.ClaveSSA and DU.IdEstado = @IdEstado And DU.IdCliente = @IdCliente And DU.IdSubCliente = @IdSubCliente  and DU.StatusRelacion_DosisUnitaria = 'A' )  
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA P (NoLock) 
			On ( L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente 
				and L.IdFuenteFinanciamiento = P.IdFuenteFinanciamiento and L.IdFinanciamiento = P.IdFinanciamiento
				and L.ClaveSSA = P.ClaveSSA ) 
		Where P.IdFuenteFinanciamiento = @IdFuenteFinanciamiento 	

		-- select top 1 * from #tmpLotes  where ContenidoPaquete_ClaveSSA = 0 			



		----------- Sustituir el agrupamiento en los Lotes 
		Update L Set ContenidoPaquete_ClaveSSA = C.Agrupacion 
		From  #tmpLotes L 
		Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C On ( L.ClaveSSA = C.ClaveSSA ) 
		Where C.Agrupacion > 0 and 1 = 0 

		--select count(*) as ClavesSERVICIO from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
		--select '' as ClavesSERVICIO, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	End  


	-------------------------------------------------------------------- Asignar las cantidades de Factura anticipada y/o Comprobacion 
	----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 
	If @EsRelacionFacturaPrevia = 1  
	Begin 
		
		If @EsRelacionMontos = 0 
		Begin  
			--Select 'FF' as campo, * From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock)  
			----Where ClaveSSA like '%0101.00%'   


			--Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
			--Select * from #tmp_ListaClaves_VentasPrevias 


			Delete #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
			From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
			Where Not Exists 
			( 
				Select * From #tmp_ListaClaves_VentasPrevias D (NoLock) 
				Where L.ClaveSSA = D.ClaveSSA and L.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento --and L.IdFinanciamiento = D.IdFinanciamiento 
			) 	


			Update L Set CantidadRestante = D.CantidadDisponible 
			From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
			Inner Join #tmp_ListaClaves_VentasPrevias D (NoLock) 
				On 
				( 
					L.ClaveSSA = D.ClaveSSA and L.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento --and L.IdFinanciamiento = D.IdFinanciamiento 
				) 	


			--Select 'FF' as campo, * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

		End 

	End 
	----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 

	----------------------------------	Validar el tipo de proceso de remisionado, Comprobacion 
	If @EsRelacionDocumentoPrevio = 1 
	Begin  

		Update L Set CantidadRestante = 0 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 

		Update L Set CantidadRestante = D.CantidadDisponible 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
		Inner Join #tmp_ListaClaves_VentasPrevias D (NoLock) 
			On ( L.ClaveSSA = D.ClaveSSA and L.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and L.IdFinanciamiento = D.IdFinanciamiento ) 	
	End 
	----------------------------------	Validar el tipo de proceso de remisionado, Comprobacion 

	-------------------------------------------------------------------- Asignar las cantidades de Factura anticipada y/o Comprobacion 





	--If @bPrint_MarcaDeTiempo_Select = 1 Select 'FUENTES DE FINANCIAMIENTO 02' as Proceso, getdate() as MarcaDeTiempo 
	--select '0001' as X, * from #tmpLotes  




	--select * from #tmpLotes   	  	 
	---------------------------------------------------------------------------------------------
	---- Se eliminan de la tabla temporal aquellos productos que no pertenecen al Tipo de Insumo 
	If  @FechaDeRevision in ( 1, 2, 3 )  
	Begin 	
		If @Remision_General = 1   ----- Remisionar Medicamento y Material de Curación [ 0 separados | 1 juntos ]  
			Begin 
				Set @IdTipoProducto = '00'  
			End 
		Else 
			Begin 
				--print 'borrando'   
				Delete From #tmpLotes Where IdTipoProducto <> @IdTipoProducto 
			End 
	End 

	--select * from #tmpLotes   	  	 
	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
----------- Se obtienen las claves de Insumos ó Administracion   
	

	---------------------------------------------------------------------------------------------------- 
	---- Se eliminan de la tabla temporal aquellos productos cuya clave no pertenesca al financiamiento 
	--Delete From #tmpLotes Where ClaveSSA Not In ( Select ClaveSSA From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) )    



	---------------------------------------------------------------------------------------------------------------------------------------------
	------------------------------------------------- Se obtienen los Precios de las Claves SSA --
	--------------------------------------------------------------------------------------------------------------------------------------------- 	
	--Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	Update L Set 
		PrecioClave = P.Precio, 
		PrecioClaveUnitario = P.PrecioUnitario, 		
		TasaIva = (case when P.TasaIva >= 0 then P.TasaIva else L.TasaIva End) 
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On ( L.ClaveSSA = P.ClaveSSA and P.Partida = 1 )  ---- Tomar el Precio Base 
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 
		and EsUnidosis = 0 

	Update L Set 
		PrecioClave = P.Precio_DosisUnitaria, 
		PrecioClaveUnitario = P.Precio_DosisUnitaria, 		
		TasaIva = (case when P.TasaIva >= 0 then P.TasaIva else L.TasaIva End) 
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) On ( L.ClaveSSA = P.ClaveSSA and P.Partida = 1 )  ---- Tomar el Precio Base 
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 
		and EsUnidosis = 1 


		--and @FechaDeRevision in ( 1, 2 )  


	--Select 'Asignacion de precios' as Precios,  * From #tmpLotes L (NoLock) 

------------------------------------------------------ MODIFICACION DEL PROCESO 

------------------------------------------------------- Se obtienen las claves de Insumos ó Administracion   
--------------------------------------------------------------------------------------------------------------------------------------------------------------------	


	--select * from #tmpLotes     
	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D where exists ( select * from #tmpLotes L where L.ClaveSSA = D.ClaveSSA ) 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
	------------------------------------------------------------ VALIDACION DE MANEJO DE EXCEDENTES 
	If @EsExcedente = 1 and 1 = 0  
	Begin 
		
		Select 
			IdEstado, IdFarmacia, ClaveSSA, 
			sum(CantidadVendida) as Cantidad_Requerida, 
			cast(0 as numeric(14,4)) as Cantidad_Disponible, 
			cast(0 as numeric(14,4)) as Cantidad_Disponible_Proceso  			 
			, identity(int, 1, 1) as Keyx    
		Into #tmp_Claves_Concentrado__Excedentes    
		From #tmpLotes   
		Group by IdEstado, IdFarmacia, ClaveSSA   
		Order by IdEstado, IdFarmacia, ClaveSSA 
			
		Update V  Set Cantidad_Disponible = F.CantidadRestante, 
			Cantidad_Disponible_Proceso =  (case when (F.CantidadRestante - V.Cantidad_Requerida) <= 0 then abs(F.CantidadRestante - V.Cantidad_Requerida) else 0 end)
		From #tmp_Claves_Concentrado__Excedentes V (NoLock) 
		Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA F (NoLock) 
			On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.ClaveSSA = F.ClaveSSA ) 

		Update F Set CantidadRestante = F.CantidadRestante + Cantidad_Disponible_Proceso 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA F  (NoLock) 
		Inner Join #tmp_Claves_Concentrado__Excedentes V (NoLock) 
			On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.ClaveSSA = F.ClaveSSA ) 


		-- Select * from #tmp_Claves_Concentrado__Excedentes 


	End 


	---------------------------------------------------------------------------------------------------- 
	--Select @ProcesarCantidadesExcedentes as ProcesarCantidadesExcedentes, @ProcesarParcialidades as ProcesarParcialidades
	
	-------------------- Exclusivo SSH Primer Nivel 
	If @ProcesarCantidadesExcedentes = 1 
	Begin 
		print 'KIUBOLES'
		--	CantidadRestante 
		Select *, 0 as Mayor, cast('' as varchar(50)) as Referencia_Mayor  
		Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Excedente 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 

		Select 0 as Keyx_Mayor, ClaveSSA, max(CantidadPresupuestada) as CantidadPresupuestada_Mayor, 1000000 as CantidadIncremento  
		Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Excedente (NoLock) 
		Group by ClaveSSA 

		Update M Set Keyx_Mayor = IsNull( 
			( 
				Select Top 1 Keyx From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA R (NoLock) 
				Where R.ClaveSSA = M.ClaveSSA and M.CantidadPresupuestada_Mayor = R.CantidadPresupuestada 
			), 0) 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor M (NoLock) 


		--Select * 
		--From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor 
		

		----Select * 
		----From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

		----Select P.ClaveSSA,  CantidadRestante, CantidadPresupuestada, CantidadIncremento 
		----From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) 
		----Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor I (NoLock) On ( P.Keyx = I.Keyx_Mayor ) 

		Update P Set CantidadRestante = CantidadPresupuestada + CantidadIncremento 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) 
		Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor I (NoLock) On ( P.Keyx = I.Keyx_Mayor ) 

		----Select P.ClaveSSA,  CantidadRestante, CantidadPresupuestada, CantidadIncremento 
		----From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) 
		----Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor I (NoLock) On ( P.Keyx = I.Keyx_Mayor ) 


	End 
	------------------------------------------------------------ VALIDACION DE MANEJO DE EXCEDENTES 
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
	------------------------------------------------------------ FORZAR QUE SE PROCESE LA CONSIGNACION 
	If @TipoDispensacion = 1 and 1 = 0 
	Begin 

		Select 
			IdEstado, IdFarmacia, ClaveSSA, 
			sum(CantidadVendida) as Cantidad_Requerida, 
			cast(0 as numeric(14,4)) as Cantidad_Disponible, 
			cast(0 as numeric(14,4)) as Cantidad_Disponible_Proceso  			 
			, identity(int, 1, 1) as Keyx    
		Into #tmp_Claves_Concentrado__Consignacion    
		From #tmpLotes   
		Group by IdEstado, IdFarmacia, ClaveSSA   
		Order by IdEstado, IdFarmacia, ClaveSSA 			

		----select * from #tmp_Claves_Concentrado__Consignacion 
		----Select * from  #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA Where ClaveSSA = '010.000.3674.00' 


		Update V  Set Cantidad_Disponible = F.CantidadRestante, 
			Cantidad_Disponible_Proceso =  (case when (F.CantidadRestante - V.Cantidad_Requerida) <= 0 then abs(F.CantidadRestante - V.Cantidad_Requerida) else 0 end)
		From #tmp_Claves_Concentrado__Consignacion V (NoLock) 
		Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA F (NoLock) 
			On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.ClaveSSA = F.ClaveSSA ) 

		Update F Set CantidadRestante = F.CantidadRestante + Cantidad_Disponible_Proceso 
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA F  (NoLock) 
		Inner Join #tmp_Claves_Concentrado__Consignacion V (NoLock) 
			On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.ClaveSSA = F.ClaveSSA ) 		


		----select * from #tmp_Claves_Concentrado__Consignacion 
		----Select * from  #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA Where ClaveSSA = '010.000.3674.00' 


	End 
	------------------------------------------------------------ FORZAR QUE SE PROCESE LA CONSIGNACION 
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  


	-- select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) 
	
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
	------------------------------------------------------------ PROCESO DE DISTRIBUCION 
	Declare 
		@sClaveSSA_Proceso varchar(20),  
		@iCantidad numeric(14,4), 
		@iCantidad_Disponible numeric(14,4), 
		@iCantidad_AsignadaTotal numeric(14,4), 
		@sIdFuenteFinanciamiento varchar(20), 
		@sIdFinanciamiento varchar(20), 
		@sIdDocumento varchar(20), 
		@sIdFuenteFinanciamiento_Relacionado varchar(20), 
		@sIdFinanciamiento_Relacionado varchar(20), 
		@sIdDocumento_Relacionado varchar(20), 
		@iFolioRemision int, 
		@iEsRelacionado int, 
		@iExistenRegistros_Por_Procesar int, 
		@Referencia_01 varchar(20),  
		@Referencia_02 varchar(50),  
		@Referencia_03 varchar(1000),  
		@Referencia_04 varchar(50),  
		@Referencia_05 varchar(50)  
		--cast(Referencia_01 as varchar(20) ) as Referencia_01, cast(Referencia_02 as varchar(50) ) as Referencia_02, cast(Referencia_03 as varchar(1000) ) as Referencia_03,

	Declare 
		@dImporte_Disponible numeric(14,4),   
		@dCantidadAcumulado numeric(14,4), 	
		@dCantidadAcumulado_Distribucion numeric(14,4), 		
		@dCantidadAcumulado_Aux numeric(14,4)  

	Declare 	
		@Keyx int, 
		@KeyxDetalle int, 	
		@sClaveSSA varchar(20), 
		@iCantidadAcumulado numeric(14,4), 	
		@iCantidadAcumulado_Distribucion numeric(14,4), 		
		@iCantidadAcumulado_Aux numeric(14,4), 
		@IdMes int,  
		@iFin int

	Declare 
		@sFacturas varchar(max), 
		@sFolioFactura varchar(100), 
		@iFacturas int   	 
	 
	Set @iFin = 0 
	Set @Keyx = 0 
	Set @KeyxDetalle = 0 
	Set @IdMes = 0 
	Set @sFacturas = '' 
	Set @sFolioFactura = '' 
	Set @iFacturas = 0 
	Set @sClaveSSA = '' 
	Set @iCantidad = 0 
	Set @iCantidadAcumulado = 0  
	Set @iCantidadAcumulado_Distribucion = 0 
	Set @iCantidadAcumulado_Aux = 0 
	Set @iExistenRegistros_Por_Procesar = 0  

	Set @dImporte_Disponible = 0 
	Set @dCantidadAcumulado = 0  
	Set @dCantidadAcumulado_Distribucion = 0 
	Set @dCantidadAcumulado_Aux = 0 

	Set @sClaveSSA_Proceso = '' 
	Set @iCantidad = 0 
	Set @iCantidad_Disponible = 0 
	Set @iCantidad_AsignadaTotal = 0 
	Set @sIdFuenteFinanciamiento = '' 
	Set @sIdFinanciamiento = '' 
	Set @sIdDocumento = '' 
	Set @iFolioRemision = 0 
	Set @iEsRelacionado = 0 
	Set @Referencia_01 = '' 
	Set @Referencia_02 = '' 
	Set @Referencia_03 = '' 
	Set @Referencia_04 = '' 
	Set @Referencia_05 = '' 

--		spp_FACT_GenerarRemisiones_General__FFxFarmacia 

	Select ClaveSSA, sum(CantidadVendida) as Cantidad, cast(0 as numeric(14,4)) as Cantidad_Distribuir, identity(int, 1, 1) as Keyx    
	Into #tmp_Claves_Concentrado  
	From #tmpLotes   
	Group by ClaveSSA   
	Order by Cantidad desc  

	---select 'REV' as Campo, * From #tmpLotes   


	--select 'ANTES DISTRIBUCION' as Proceso, * from #tmpLotes D (NoLock)  
	--Select * from #tmp_Claves_Concentrado  
	--Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 



	Select Top 0 cast(KeyxGeneral as int) as KeyxGeneral, IdFarmacia, FolioVenta, cast('' as varchar(50)) as ClaveSSA, CantidadVendida, 
		cast(0 as numeric(14,4)) as CantidadAsignada, 
		0 as Acumulado, 0 as Incluir, 0 as TipoDeAsignacion, 
		identity(int, 1, 1) as Keyx  
	Into #tmp_Asignacion_Cantidades 
	From #tmpLotes 

	------  TEST 
	--select * from #tmp_Claves_Concentrado 
	--select 'CLAVES FF' as Proceso, * from  #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	------------------------------------------------------------ PROCESAMIENTO A NIVEL CLAVESSA
	If @bPrint = 1 Print '' 
	If @bPrint = 1 Print 'Identificador : ' + @Identificador 
	
	--If @bPrint_MarcaDeTiempo_Select = 1 Select 'ASIGNACION DE FUENTES DE FINANCIAMIENTO 01' as Proceso, getdate() as MarcaDeTiempo 

		----Select 'LISTA DE CLAVES' as ListaClaves, ClaveSSA -- , Cantidad
		----From #tmp_Claves_Concentrado T  
		------ Where @FechaDeRevision in ( 1, 2, 3 ) 	 

	--Select '01 Antes Proceso' as Paso, Procesado, count(*) as Registros  
	--From #tmpLotes 
	--group by Procesado 

	--Select 'CLAVE A PROCESAR' as CLAVE_A_PROCESAR, * From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T 


	-------------------------------- DETERMINAR LA CANTIDAD A DISTRIBUIR POR CLAVE 
	Update C Set Cantidad_Distribuir = T.CantidadRestante 
	From #tmp_Claves_Concentrado C (NoLock) 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T (NoLock) On ( C.ClaveSSA = T.ClaveSSA ) 

	--select * from #tmp_Claves_Concentrado 


	Declare #cursorClaves  
	Cursor For 
		Select ClaveSSA -- , Cantidad
		From #tmp_Claves_Concentrado T  
		Where @FechaDeRevision in ( 1, 2, 3 ) 
			and Cantidad_Distribuir > 0 
		Order by Keyx desc  
	Open #cursorClaves 
	FETCH NEXT FROM #cursorClaves Into @sClaveSSA_Proceso -- , @iCantidad  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			If @bPrint_01 = 1 Print '' 
			If @bPrint_01 = 1 Print 'CLAVE A PROCESAR: ' + @sClaveSSA_Proceso  -- + '    ' + cast(@iCantidad as varchar(10))  

			--Select * 					
			--From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T 
			--where ClaveSSA = @sClaveSSA_Proceso 
			--Order by Prioridad, CantidadRestante desc 


			--Select IdFuenteFinanciamiento, IdFinanciamiento, Prioridad, CantidadRestante, Referencia_01  
			--From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T 
			--where ClaveSSA = @sClaveSSA_Proceso 
			--Order by Prioridad, CantidadRestante desc 
	

			------------------------------------------------------------------------------------------------------------------------------------- 
			------------------------------------------------------------ PROCESAMIENTO A NIVEL FUENTE DE FINANCIAMIENTO 
			Declare #cursorFuentesDeFinanciamiento  
			Cursor For 
				Select IdFuenteFinanciamiento, IdFinanciamiento, CantidadRestante, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05    
				From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T 
				where ClaveSSA = @sClaveSSA_Proceso 
					and CantidadRestante > 0 ----- Excluir claves sin cantidad disponible  
				Order by Prioridad, CantidadRestante  --- desc 
			Open #cursorFuentesDeFinanciamiento 
			FETCH NEXT FROM #cursorFuentesDeFinanciamiento Into @sIdFuenteFinanciamiento, @sIdFinanciamiento, @iCantidad_Disponible, 
																@Referencia_01, @Referencia_02, @Referencia_03, @Referencia_04, @Referencia_05    
				WHILE @@FETCH_STATUS = 0 
				BEGIN 

					--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA where ClaveSSA = @sClaveSSA_Proceso  

					--		spp_FACT_GenerarRemisiones_General__FFxFarmacia    
					--		Print cast(@iCantidad_Disponible as varchar(20))   
					
					--select * from #tmpLotes 

					--------------------------------------------------------------------------------------------------------------------------------- 
					--------------------------------------------- ASIGNACION MASIVA 
					If @bProcesoSegmentado = 1 
					Begin 
						--Print '' 
						if @bPrint_01 = 1 Print 'PROCESO SEGMENTADO: DISTRIBUCION'   

						Delete From #tmp_Asignacion_Cantidades   

						Insert into #tmp_Asignacion_Cantidades ( KeyxGeneral, IdFarmacia, FolioVenta, ClaveSSA, CantidadVendida, CantidadAsignada, Acumulado, Incluir, TipoDeAsignacion )  
						Select 
							cast(KeyxGeneral as int) as KeyxGeneral, IdFarmacia, FolioVenta, ClaveSSA, CantidadVendida, 
							0 CantidadAsignada, 0 as Acumulado, 0 as Incluir, 0 as TipoDeAsignacion  
						From #tmpLotes  
						Where ClaveSSA = @sClaveSSA_Proceso and Procesado = 0  
						Order by CantidadVendida desc, FolioVenta 

						------- Cambio a distribución manual, se valida registro por registro  
						Update A Set Acumulado = ( select sum(CantidadVendida) From #tmp_Asignacion_Cantidades H Where H.Keyx <= A.Keyx )
						From #tmp_Asignacion_Cantidades A 

						Update A Set Incluir = 1, CantidadAsignada = CantidadVendida, TipoDeAsignacion = 1   
						From #tmp_Asignacion_Cantidades A 
						Where Acumulado <= @iCantidad_Disponible 


						if @bPrint_01 = 1 Print space(50) + cast(@iCantidad_Disponible as varchar(100)) 
						
						Select Top 1 @iCantidad_Disponible = @iCantidad_Disponible - Acumulado 
						From #tmp_Asignacion_Cantidades 
						Where Incluir = 1 
						Order by Acumulado desc 

						if @bPrint_01 = 1 Print space(50) + cast(@iCantidad_Disponible as varchar(100))  


						Update L Set Procesado = 1, TipoProcesamiento_01 = 1, TipoDeAsignacion = P.TipoDeAsignacion, Cantidad_Remisionada = P.CantidadAsignada,   
							IdFuenteFinanciamiento = @sIdFuenteFinanciamiento, IdFinanciamiento = @sIdFinanciamiento, 
							Referencia_01 = @Referencia_01, Referencia_02 = @Referencia_02, Referencia_03 = @Referencia_03, 
							Referencia_04 = @Referencia_04, Referencia_05 = @Referencia_05  
						From #tmpLotes L 
						-- Inner Join #tmp_Asignacion_Cantidades P On ( L.KeyxGeneral = P.KeyxGeneral and P.Incluir = 1 ) 
						Inner Join #tmp_Asignacion_Cantidades P On ( L.KeyxGeneral = P.KeyxGeneral and P.CantidadAsignada > 0 ) 

						Update C Set CantidadAsignada = IsNull((select sum(CantidadVendida) From #tmpLotes L (NoLock) 
							Where L.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and L.IdFinanciamiento = C.IdFinanciamiento and ClaveSSA = @sClaveSSA_Proceso and Procesado = 1 ), 0 )  
						From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C 
						Where IdFuenteFinanciamiento = @sIdFuenteFinanciamiento and IdFinanciamiento = @sIdFinanciamiento and ClaveSSA = @sClaveSSA_Proceso 
							and Referencia_01 = @Referencia_01 
							and @TipoDispensacion = 0 -- Solo afectar VENTA  

						-- select * from #tmp_Asignacion_Cantidades 
					End 
					--------------------------------------------- ASIGNACION MASIVA 
					--------------------------------------------------------------------------------------------------------------------------------- 


					------------------------------------------- ASIGNACION MANUAL DE LOS REGISTROS PENDIENTES DE PROCESAR  
					--select * from #tmpLotes 

					Delete From #tmp_Asignacion_Cantidades   

					Insert into #tmp_Asignacion_Cantidades ( KeyxGeneral, IdFarmacia, FolioVenta, ClaveSSA, CantidadVendida, CantidadAsignada, Acumulado, Incluir, TipoDeAsignacion )  
					Select 
						cast(KeyxGeneral as int) as KeyxGeneral, IdFarmacia, FolioVenta, ClaveSSA, CantidadVendida, 
						0 as CantidadAsignada, 0 as Acumulado, 0 as Incluir, 0 as TipoDeAsignacion  
					From #tmpLotes  
					Where ClaveSSA = @sClaveSSA_Proceso and Procesado = 0  
					Order by CantidadVendida desc, FolioVenta 

					--select * from #tmpLotes 


					Set @iCantidad = 0 
					-- Set @iCantidadAcumulado = 0 
					Set @iCantidadAcumulado_Distribucion = 0 
					Set @iCantidadAcumulado_Aux = 0 
					Set @iFin = 0 
					Set @iCantidadAcumulado = @iCantidad_Disponible  
					If @bPrint_01 = 1 
						Print @sClaveSSA_Proceso + '		Disponible :	'  + 
							  cast(@iCantidad_Disponible as varchar) + (case when @Referencia_01 = '' then '' else '		Referencia :	'  + @Referencia_01 end) 


					------------------------------------------------------- DISTRIBUCION A NIVEL FOLIO DE VENTA ( RECETA )  
					-----------------------------------------   ASIGNACION DE RENGLONES COMPLETOS 
					Declare #cursorDistribucion_x_Renglon    
					Cursor For 
					Select KeyxGeneral, CantidadVendida as Cantidad  
					From #tmp_Asignacion_Cantidades 
					Where ClaveSSA = @sClaveSSA_Proceso  
					Order By Keyx  
					Open #cursorDistribucion_x_Renglon 
					FETCH NEXT FROM #cursorDistribucion_x_Renglon  Into @KeyxDetalle, @iCantidad     
						WHILE @@FETCH_STATUS = 0 and @iFin = 0 
						BEGIN 
							--print @iCantidad 
							If @bPrint_01 = 1 Print '					Requerido :	'  + cast(@iCantidad as varchar) 

							If ( @iCantidad_Disponible < @iCantidad ) 
							Begin 
								Set @iCantidad = 0 
							End 

							If ( @iCantidad_Disponible >= @iCantidad ) 
							Begin 
								Set @iCantidad_Disponible = @iCantidad_Disponible - @iCantidad 
								If @bPrint = 1 Print '						Asignacion :	'  + cast(@iCantidad as varchar) 
								If @bPrint = 1 Print '						Disponible :	'  + cast(@iCantidad_Disponible as varchar) 
							End 
					
							If @iCantidad_Disponible = 0 
							Begin 
								Set @iFin = 1  
								If @bPrint = 1 Print '					Asignacion finalizada ' + (case when @Referencia_01 = '' then '' else '		Referencia :	'  + @Referencia_01 end) 
								If @bPrint = 1 Print ' '  
							End 
				
							Update C Set 
								CantidadAsignada = IsNull(@iCantidad, 0), --Cantidad_Remisionada = IsNull(@iCantidad, 0), 
								TipoDeAsignacion = (case when IsNull(@iCantidad, 0) > 0 then 2 else 0 end)   
							From #tmp_Asignacion_Cantidades C (NoLock) 
							Where KeyxGeneral = @KeyxDetalle  			 
					
							--select '01', * from #tmp_Asignacion_Cantidades 

							FETCH NEXT FROM #cursorDistribucion_x_Renglon  Into  @KeyxDetalle, @iCantidad     
						END
					Close #cursorDistribucion_x_Renglon 
					Deallocate #cursorDistribucion_x_Renglon 
					-----------------------------------------   ASIGNACION DE RENGLONES COMPLETOS 
					


					-----------------------------------------   ASIGNACION DE RENGLONES CON PARCIALIDADES 
					Set @iFin = 0 
					Declare #cursorDistribucion   
					Cursor For 
					Select KeyxGeneral, CantidadVendida as Cantidad  
					From #tmp_Asignacion_Cantidades 
					Where ClaveSSA = @sClaveSSA_Proceso  and CantidadAsignada = 0  
						and @ProcesarParcialidades = 1	---- ====> Exclusivo SSH Primer Nivel 
						--and @iCantidad_Disponible > 0   ---- ====> 
					Order By Keyx  
					Open #cursorDistribucion 
					FETCH NEXT FROM #cursorDistribucion  Into @KeyxDetalle, @iCantidad     
						WHILE @@FETCH_STATUS = 0 and @iFin = 0 
						BEGIN 
							--print @iCantidad 
							If @bPrint_01 = 1 Print '					Requerido 02 :	'  + cast(@iCantidad as varchar) 

							If ( @iCantidad_Disponible < @iCantidad ) 
							Begin 
								Set @iCantidad = @iCantidad_Disponible  
							End 

							If ( @iCantidad_Disponible >= @iCantidad ) 
							Begin 
								Set @iCantidad_Disponible = @iCantidad_Disponible - @iCantidad 
								If @bPrint_01 = 1 Print '						Asignacion 02 :	'  + cast(@iCantidad as varchar) 
								If @bPrint_01 = 1 Print '						Disponible 02 :	'  + cast(@iCantidad_Disponible as varchar) 
							End 
					
							If @iCantidad_Disponible = 0 
							Begin 
								Set @iFin = 1  
								If @bPrint_01 = 1 Print '					Asignacion finalizada 02 ' + (case when @Referencia_01 = '' then '' else '		Referencia :	'  + @Referencia_01 end) 
								If @bPrint_01 = 1 Print ' '  
							End 					
				
							Update C Set 
								CantidadAsignada = IsNull(@iCantidad, 0), --Cantidad_Remisionada = IsNull(@iCantidad, 0), 
								TipoDeAsignacion = (case when IsNull(@iCantidad, 0) > 0 then 3 else 0 end)   
							From #tmp_Asignacion_Cantidades C (NoLock) 
							Where KeyxGeneral = @KeyxDetalle  


							If Exists 
							( 
								Select *  
								From #tmp_Asignacion_Cantidades C (NoLock) 
								Where KeyxGeneral = @KeyxDetalle and CantidadAsignada > 0 
									and CantidadAsignada <> CantidadVendida and IsNull(@iCantidad, 0) > 0 									
							) 
							Begin 				

								--select * 
								--From #tmp_Asignacion_Cantidades 
								--Where KeyxGeneral = @KeyxDetalle 


								--Select IsNull(@iCantidad, 0), *  
								--From #tmp_Asignacion_Cantidades C (NoLock) 
								--Where KeyxGeneral = @KeyxDetalle and CantidadAsignada <> CantidadVendida and IsNull(@iCantidad, 0) > 0 

								--select * 
								--From #tmpLotes L (NoLock) 
								--Inner Join #tmp_Asignacion_Cantidades P (NoLock) 
								--	On ( L.KeyxGeneral = P.KeyxGeneral and P.CantidadAsignada <> L.CantidadVendida and IsNull(@iCantidad, 0) > 0 )   
								--Where P.CantidadAsignada > 0 


								Insert Into #tmpLotes  
								Select 	
									L.EsAlmacen, L.EsFarmacia, L.EsUnidosis, 
									L.FolioRemision, L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, 
									-- L.Partida, 
									(L.Partida + 1) as Partida_Aux, 
									L.IdCliente, L.IdSubCliente, L.IdPrograma, L.IdSubPrograma, 
									L.IdFuenteFinanciamiento, L.IdFinanciamiento, 
									
									L.Referencia_01, L.Referencia_02, L.Referencia_03, L.Referencia_04, L.Referencia_05, 								

									L.IdDocumento, L.IdFuenteFinanciamiento_Relacionado, L.IdFinanciamiento_Relacionado, L.IdDocumento_Relacionado, 		
									0 as Procesado, L.TipoProcesamiento_01, L.TipoProcesamiento_02, 
									L.IdClaveSSA, L.ClaveSSA, L.Relacionada, L.IdClaveSSA_P, L.ClaveSSA_P, 
									L.ContenidoPaquete_ClaveSSA, L.ContenidoPaquete_Licitado, L.Factor, L.MultiploRelacion, L.IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, 
									L.PrecioClave, L.PrecioClaveUnitario, L.TasaIva, 
									L.PartidaGeneral, L.AfectaEstadistica, L.AfectaEstadisticaMontos, 									
									EsDecimal, 
									(L.CantidadVendida - IsNull(@iCantidad, 0)) as CantidadVendida, L.CantidadVendida_Base, 
									L.Cantidad_Agrupada, 
									0 as Cantidad_Remisionada, 0 as Cantidad_Remisionada_Agrupada, 
									L.GrupoProceso, L.IncluidoEnExcepcion, L.TipoDeAsignacion, L.IdGrupoDeRemisiones, L.GrupoDispensacion, 
									L.EsControlado, L.EsAntibiotico, L.EsRefrigerado, 0 as EsExcepcionPrecio  
								From #tmpLotes L (NoLock) 
								Inner Join #tmp_Asignacion_Cantidades P (NoLock) 
									On ( L.KeyxGeneral = P.KeyxGeneral and P.CantidadAsignada <> P.CantidadVendida and IsNull(@iCantidad, 0) > 0 )   
								Where P.CantidadAsignada > 0 
							End 


							FETCH NEXT FROM #cursorDistribucion  Into  @KeyxDetalle, @iCantidad     
						END
					Close #cursorDistribucion 
					Deallocate #cursorDistribucion 
					-----------------------------------------   ASIGNACION DE RENGLONES CON PARCIALIDADES 
					------------------------------------------------------- DISTRIBUCION A NIVEL FOLIO DE VENTA ( RECETA )  



					-----------------------------------------------------   MARCAR LOS RENGLONES PROCESADOS  
					Update L Set Procesado = 1, TipoProcesamiento_01 = 2, TipoDeAsignacion = P.TipoDeAsignacion, Cantidad_Remisionada = P.CantidadAsignada,   
						IdFuenteFinanciamiento = @sIdFuenteFinanciamiento, IdFinanciamiento = @sIdFinanciamiento, 
						Referencia_01 = @Referencia_01, Referencia_02 = @Referencia_02, Referencia_03 = @Referencia_03, 
						Referencia_04 = @Referencia_04, Referencia_05 = @Referencia_05  
					From #tmpLotes L 
					-- Inner Join #tmp_Asignacion_Cantidades P On ( L.KeyxGeneral = P.KeyxGeneral and P.Incluir = 1 ) 
					Inner Join #tmp_Asignacion_Cantidades P On ( L.KeyxGeneral = P.KeyxGeneral and P.CantidadAsignada > 0 ) 


					Update C Set CantidadAsignada = IsNull((select sum(CantidadVendida) From #tmpLotes L (NoLock) 
						Where L.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and L.IdFinanciamiento = C.IdFinanciamiento and ClaveSSA = @sClaveSSA_Proceso and Procesado = 1 ), 0 )  
					From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C 
					Where IdFuenteFinanciamiento = @sIdFuenteFinanciamiento and IdFinanciamiento = @sIdFinanciamiento and ClaveSSA = @sClaveSSA_Proceso 
						and Referencia_01 = @Referencia_01 
						and @TipoDispensacion = 0 -- Solo afectar VENTA 
					-----------------------------------------------------   MARCAR LOS RENGLONES PROCESADOS  



					FETCH NEXT FROM #cursorFuentesDeFinanciamiento Into @sIdFuenteFinanciamiento, @sIdFinanciamiento, @iCantidad_Disponible, 
																        @Referencia_01, @Referencia_02, @Referencia_03, @Referencia_04, @Referencia_05       
				END	 
			Close #cursorFuentesDeFinanciamiento 
			Deallocate #cursorFuentesDeFinanciamiento 	
			------------------------------------------------------------ PROCESAMIENTO A NIVEL FUENTE DE FINANCIAMIENTO 

			FETCH NEXT FROM #cursorClaves Into @sClaveSSA_Proceso -- , @iCantidad     
		END	 
	Close #cursorClaves 
	Deallocate #cursorClaves 	
	------------------------------------------------------------ PROCESAMIENTO A NIVEL CLAVESSA 


	--Select '02 Despues Proceso' as Paso, @TipoDeRemision as TipoDeRemision, Procesado, count(*) as Registros  
	--From #tmpLotes 
	--group by Procesado 

	--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	--select 'DESPUES DISTRIBUCION' as 'DESPUES DISTRIBUCION', * from #tmpLotes D (NoLock)  
	--If @bPrint_MarcaDeTiempo_Select = 1 Select 'ASIGNACION DE FUENTES DE FINANCIAMIENTO 02' as Proceso, getdate() as MarcaDeTiempo 


	------------------------------------------------ Remisiones relacionadas a Facturas anticipadas   20200210.1600 
	--Select @Accion as Accion 
	If @Accion <> '' 
	Begin 

		--select 'ANTES DEPURACION' as Accion, Referencia_05, count(*) as Registros 
		--From #tmpLotes  
		--Group by Referencia_05 

		Delete 
		From #tmpLotes  
		Where Referencia_05 <> @Accion  

		--select 'DEPURADO' as Accion, Referencia_05, count(*) as Registros 
		--From #tmpLotes  
		--Group by Referencia_05 

	End  
	------------------------------------------------ Remisiones relacionadas a Facturas anticipadas 




	------------------------------------- Validar que exista la configuración de excepciones    20190112.1400   SSH 1N  
	Select top 1 @bExisteInformacion_x_Procesar_Excepciones = (case when count(*) > 0 then 1 else 0 end) 
	From FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones (NoLock) 
	--Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento -- and IdFinanciamiento = 
	Set @bExisteInformacion_x_Procesar_Excepciones = ISNULL(@bExisteInformacion_x_Procesar_Excepciones, 0)  


	--select @bExisteInformacion_x_Procesar_Excepciones as ExisteInformacion_x_Procesar_Excepciones

	------------------------------------------------------------- EXCEPCIONES DE PRECIOS CAUSES 
	------------------------------------- 20181204.1310   SSH 1N 
	If --@EsRemision_Complemento = 0 and 
		--( 
		--	( @IdEmpresa = '002' and @IdEstado = '09' and @IdCliente = '0002' and @IdSubCliente = '0012' ) 
		--	or 
		--	( @IdEmpresa = '002' and @IdEstado = '14' and @IdCliente = '0002' and @IdSubCliente = '0012' ) 
		--) 
		--and 
		@TipoDeRemision in ( 1, 4, 2, 6 ) 
	--If @TipoDeRemision in ( 1, 4, 2, 6 ) 
	Begin 
		
		--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

		--Select @bExisteInformacion_x_Procesar_Excepciones as ExisteInformacion_x_Procesar_Excepciones


		If @bExisteInformacion_x_Procesar_Excepciones = 0   
		Begin 
			--print 'x' 
			Set @bExisteInformacion_x_Procesar_Excepciones = @bExisteInformacion_x_Procesar_Excepciones  

			Update L Set 
				--Precio = C.Precio, 
				Precio = (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end),  
				PrecioUnitario = cast(( (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end) / (C.ContenidoPaquete / 1.0)) as numeric(14,4)), 
				ContenidoPaquete = C.ContenidoPaquete   
			From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
			Inner Join #vw_Claves_Precios_Asignados C (NoLock) On ( L.IdEstado = C.IdEstado and L.IdCliente = C.IdCliente and L.IdSubCliente = C.IdSubCliente and L.ClaveSSA = C.ClaveSSA )  
			Inner Join FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
				On ( 
						-- E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
						C.IdEstado = E.IdEstado and C.IdCliente = E.IdCliente and C.IdSubCliente = E.IdSubCliente 
						and E.ClaveSSA = C.ClaveSSA  
					) 	
		
			----Select 'xxx___FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA', * 
			----from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

		End 



		If @bExisteInformacion_x_Procesar_Excepciones = 1 
		Begin 
			--print 'xxx' 

			--Select 'EXCEPCIONES PRECIOS' as Campo  

			--Select L.*, 
			--	E.PrecioBase, E.Incremento, 
			--	Precio = (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end),  
			--	PrecioUnitario = cast(( (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end) / (C.ContenidoPaquete / 1.0)) as numeric(14,4)), 
			--	ContenidoPaquete = C.ContenidoPaquete   
			--From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
			--Inner Join #vw_Claves_Precios_Asignados C (NoLock) On( L.ClaveSSA = C.ClaveSSA )  
			--Inner Join FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones E (NoLock) 
			--	On ( 
			--		-- E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
			--		C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
			--		and E.ClaveSSA = C.ClaveSSA  
			--		) 
			--Where E.Tipo = 1 and 
			--	Exists 
			--	( 
			--		Select * 
			--		From #tmpLotes L (NoLock) 
			--		Where L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente and L.ClaveSSA = E.ClaveSSA 
			--			and L.IdFuenteFinanciamiento = E.IdFuenteFinanciamiento and L.IdFinanciamiento = E.IdFinanciamiento -- and L.Referencia_01 = E.Referencia_01
			--	) 
			--		and @TipoDeRemision in ( 1, 4  )  
			--		and @TipoDispensacion = 0 ---- Solo producto de Venta 


			--Select L.*, 
			--	E.PrecioBase, E.Incremento, 
			--	Precio = (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end),  
			--	PrecioUnitario = cast(( (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end) / (C.ContenidoPaquete / 1.0)) as numeric(14,4)), 
			--	ContenidoPaquete = C.ContenidoPaquete   
			--From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
			--Inner Join #vw_Claves_Precios_Asignados C (NoLock) On( L.ClaveSSA = C.ClaveSSA )  
			--Inner Join FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones E (NoLock) 
			--	On ( 
			--		-- E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
			--		C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
			--		and E.ClaveSSA = C.ClaveSSA  
			--		) 
			--Where E.Tipo = 2 and 
			--	Exists 
			--	( 
			--		Select * 
			--		From #tmpLotes L (NoLock) 
			--		Where L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente and L.ClaveSSA = E.ClaveSSA 
			--			and L.IdFuenteFinanciamiento = E.IdFuenteFinanciamiento and L.IdFinanciamiento = E.IdFinanciamiento --and L.Referencia_01 = E.Referencia_01
			--	) 
			--		and @TipoDeRemision in ( 2, 6  )  
			--		and @TipoDispensacion = 0 ---- Solo producto de Venta 



			---------------------------------------- PRODUCTO  
			Update L Set 
				--Precio = C.Precio, 
				Precio = (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end),  
				PrecioUnitario = cast(( (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end) / (C.ContenidoPaquete / 1.0)) as numeric(14,4)), 
				ContenidoPaquete = C.ContenidoPaquete   
			From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
			Inner Join #vw_Claves_Precios_Asignados C (NoLock) On( L.ClaveSSA = C.ClaveSSA )  
			Inner Join FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones E (NoLock) 
				On ( 
					-- E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
					C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
					and E.ClaveSSA = C.ClaveSSA  
					) 
			Where E.Tipo = 1 and 
				Exists 
				( 
					Select * 
					From #tmpLotes L (NoLock) 
					Where L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente and L.ClaveSSA = E.ClaveSSA 
						and L.IdFuenteFinanciamiento = E.IdFuenteFinanciamiento and L.IdFinanciamiento = E.IdFinanciamiento -- and L.Referencia_01 = E.Referencia_01
				) 
					and @TipoDeRemision in ( 1, 4  )  
					and @TipoDispensacion = 0 ---- Solo producto de Venta 


			Update L Set 
				--Precio = C.Precio, 
				Precio = (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end),  
				PrecioUnitario = cast(( (case when @EsRemision_Complemento = 0 Then E.PrecioBase else E.Incremento end) / (C.ContenidoPaquete / 1.0)) as numeric(14,4)), 
				ContenidoPaquete = C.ContenidoPaquete   
			From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA L (NoLock) 
			Inner Join #vw_Claves_Precios_Asignados C (NoLock) On( L.ClaveSSA = C.ClaveSSA )  
			Inner Join FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones E (NoLock) 
				On ( 
					-- E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
					C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
					and E.ClaveSSA = C.ClaveSSA  
					) 
			Where E.Tipo = 2 and 
				Exists 
				( 
					Select * 
					From #tmpLotes L (NoLock) 
					Where L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente and L.ClaveSSA = E.ClaveSSA 
						and L.IdFuenteFinanciamiento = E.IdFuenteFinanciamiento and L.IdFinanciamiento = E.IdFinanciamiento -- and L.Referencia_01 = E.Referencia_01
				) 
					and @TipoDeRemision in ( 2, 6  )  
					and @TipoDispensacion = 0 ---- Solo producto de Venta 

			--Select @TipoDeRemision as TIPO, * From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

		End 

		--select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 

	End 
	--select 'DESPUES ASIGNACION PRECIOS' as Proceso, * from #tmpLotes D (NoLock)  
	------------------------------------- 20181204.1310   SSH 1N 
	------------------------------------------------------------- EXCEPCIONES DE PRECIOS CAUSES  


	---------------------------------------------------------------------------------------------------------------------------------------------
	------------------------------------------------- Se obtienen los Precios de las Claves SSA según la Fuente de Financiamiento 
	--select * from #tmpLotes 


	Update L Set 
		PrecioClave = (case when L.EsUnidosis = 0 then P.Precio else P.Precio_DosisUnitaria end), 
		PrecioClaveUnitario = ( case when L.EsUnidosis = 0 then P.PrecioUnitario else P.Precio_DosisUnitaria end), 		
		TasaIva = (case when P.TasaIva >= 0 then P.TasaIva else L.TasaIva End), 
		AfectaEstadistica = P.AfectaEstadistica, AfectaEstadisticaMontos = P.AfectaEstadisticaMontos 
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA P (NoLock) -- On ( L.ClaveSSA = P.ClaveSSA ) 
		On ( -- L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente 
			-- and 
			L.IdFuenteFinanciamiento = P.IdFuenteFinanciamiento and L.IdFinanciamiento = P.IdFinanciamiento
			and L.ClaveSSA = P.ClaveSSA ) 
	Where P.IdEstado = @IdEstado And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente 
		--and @FechaDeRevision in ( 1, 2 )  

	--select 'DESPUES DE ASIGNAR PRECIOS', * from #tmpLotes 

	------------------------------------------------- Se obtienen los Precios de las Claves SSA según la Fuente de Financiamiento 
	--------------------------------------------------------------------------------------------------------------------------------------------- 	

	--------------------------- REPORTE DE PROCESO 
	--select 'VENTAS' as VENTAS, IdFarmacia, count(*) as Ventas from #tmpVentas   group by IdFarmacia 
	--select 'VENTAS' as VENTAS, * from #tmpVentas   
	--select 'LOTES' as LOTES, * from #tmpLotes   
	--------------------------- REPORTE DE PROCESO 

	--Select 'Resumen 000' as Campo, Procesado, sum(CantidadVendida) as Piezas From #tmpLotes (NoLock) Group by Procesado 	


	----------------------------- Verificar el tipo de procesamiento al asignar Fuente de Financiamiento 
	if @bVerificarAsignaciones = 1 
	Begin 
		select TipoProcesamiento_01, count(*) as Registros_Procesados  
		from #tmpLotes 
		Where Procesado = 1 
		Group by TipoProcesamiento_01 
	End 


	--select 'ASIGNACION' as DATO, * 
	--From #tmpLotes 
	--Where Procesado = 1 

	------------------------------------------------------------ ASIGNACION DE DOCUMENTO DE CONTROL DE IMPORTES  
	Select Top 1 @Procesar_Asignacion_De_Documentos = 1 From #tmpLotes Where Procesado = 1 
	Set @Procesar_Asignacion_De_Documentos = IsNull(@Procesar_Asignacion_De_Documentos, 0) 

	------------------------------------- 
	--Select '01 Previo Documentos' as Paso, Procesado, count(*) as Registros  
	--From #tmpLotes 
	------ Where Procesado = 1
	--group by Procesado 


	--Select '01 Antes Documentos' as Paso, Procesado, count(*) as Registros  
	--From #tmpLotes 
	--group by Procesado 


	If @Aplicar_ImporteDocumentos = 1  and @Procesar_Asignacion_De_Documentos = 1 
	Begin 
		
		Set @bEsFarmacia = 0  
		Set @bEsAlmacen = 0  


		If @ExcluirCantidadesConDecimales = 1 
		Begin 
			Update L Set EsDecimal = 1 
			From #tmpLotes L (NoLock) 
			Where ( Cantidad_Agrupada  - floor(Cantidad_Agrupada) ) > 0 

			Delete From #tmpLotes Where EsDecimal = 1 
		End 

		Select top 1 @bEsFarmacia = 1 From #tmpLotes 
		Where EsFarmacia = 1 -- Or @TipoDeBeneficiario = 2 --- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén> 
		
		Select top 1 @bEsAlmacen = 1 From #tmpLotes Where EsAlmacen = 1 
		Set @bEsFarmacia = IsNull(@bEsFarmacia, 0) 
		Set @bEsAlmacen = IsNull(@bEsAlmacen, 0) 


		--Select 'AQUI' 
		Select top 1 @bEsAlmacen = 1 From vw_Farmacias Where EsAlmacen = 1 and IdEstado = @IdEstado  and IdFarmacia = @IdFarmacia  
		Select top 1 @bEsFarmacia = 1 From vw_Farmacias Where EsAlmacen = 0 and IdEstado = @IdEstado  and IdFarmacia = @IdFarmacia 
	
		Set @bEsAlmacen = IsNull(@bEsAlmacen, 0) 	 
		Set @bEsFarmacia = IsNull(@bEsFarmacia, 0) 



		Select 
			IDENTITY(int, 1, 1) as Consecutivo, 
			IdFuenteFinanciamiento, IdFinanciamiento, 0 as TipoDeInsumo, 
			sum(((1 + (TasaIva / 100.00)) * (Cantidad_Remisionada * PrecioClaveUnitario))) as Importe_A_Asignar, 
			sum(Cantidad_Remisionada) as Cantidad_Remisionada   
		Into #tmp_FF_ListaDocumentos  
		From #tmpLotes 
		Where Procesado = 1 
		Group By IdFuenteFinanciamiento, IdFinanciamiento, GrupoProceso  
		Order By IdFuenteFinanciamiento, IdFinanciamiento, GrupoProceso   

		--select 'Lotes-Documentos', * From #tmpLotes (NoLock) 

		Update L Set Procesado = 0 From #tmpLotes L  

		--select 'Lotes-Documentos', * From #tmpLotes (NoLock) 

		Select 
			IDENTITY(int, 0, 1) as Consecutivo, 
			D.IdFuenteFinanciamiento, D.IdFinanciamiento, D.IdDocumento, D.NombreDocumento, 
			D.IdFuenteFinanciamiento_Relacionado , D.IdFinanciamiento_Relacionado, D.IdDocumento_Relacionado, D.NombreDocumento_Relacionado, 
			D.EsRelacionado, 
			D.OrigenDeInsumo, 
			-- @OrigenInsumo as Origen, @TipoDispensacion as Origen_Aux, 
			D.TipoDeDocumento, D.TipoDeInsumo, D.ValorNominal, D.ImporteAplicado, (D.ValorNominal - D.ImporteAplicado) as ImporteRestante, 
			D.AplicaFarmacia, D.AplicaAlmacen, D.EsProgramaEspecial, 
			D.Status -- , D.Actualizado 
			-- D.IdFuenteFinanciamiento, D.IdFinanciamiento 
		Into #tmp_FF_Documentos  
		-- From FACT_Fuentes_De_Financiamiento_Detalles_Documentos D (NoLock) 
		From dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento(0) D  --- 0 Para relacionar los documentos   
		Inner Join #tmp_FF_ListaDocumentos L (NoLock) 
			On ( 
				 D.IdFuenteFinanciamiento = L.IdFuenteFinanciamiento 
				 and D.IdFinanciamiento = L.IdFinanciamiento 
				 and D.TipoDeDocumento = @TipoDeRemision_Auxiliar 
				 and L.TipoDeInsumo in ( 0, cast(@IdTipoProducto as int) )   
				 )  
		Where D.Status = 'A' 
			and (D.ValorNominal > D.ImporteAplicado) --- Filtro de importes 
			and ( (AplicaAlmacen = @bEsAlmacen or AplicaFarmacia = @bEsFarmacia) ) -- or TipoDeBeneficiario = @TipoDeBeneficiario )   
			and TipoDeBeneficiario in ( 0, @TipoDeBeneficiario )  
			--and (AplicaAlmacen = @bEsAlmacen or AplicaFarmacia = @bEsFarmacia)  
			--and TipoDeBeneficiario = @TipoDeBeneficiario  
			and EsProgramaEspecial = @EsProgramasEspeciales 
		Order by ( D.ValorNominal - D.ImporteAplicado )   
		--	and D.TipoDeInsumo = @IdTipoProducto  	  



		---------------------- Datos de Pruebas 
		--Select 
		--	@EsProgramasEspeciales as EsProgramasEspeciales, @bEsFarmacia as EsFarmacia, @bEsAlmacen as EsAlmacen, 
		--	@IdTipoProducto as TipoProducto, @TipoDeBeneficiario as TipoDeBeneficiario, @TipoDeRemision_Auxiliar as TipoRemision   		
		--Select 'ANTES DISTRIBUCION DOCUMENTOS' as Proceso, * from #tmp_FF_ListaDocumentos 
		--Select * from #tmp_FF_Documentos 


		--select * 
		--From dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento(0) D  --- 0 Para relacionar los documentos   
		--Inner Join #tmp_FF_ListaDocumentos L (NoLock) 
		--	On ( 
		--		 D.IdFuenteFinanciamiento = L.IdFuenteFinanciamiento and D.IdFinanciamiento = L.IdFinanciamiento and D.TipoDeDocumento = @TipoDeRemision_Auxiliar 
		--		 and L.TipoDeInsumo in ( 0, cast(@IdTipoProducto as int) )   
		--		 )  
		--Where D.Status <> '' 
		--	and (D.ValorNominal > D.ImporteAplicado) --- Filtro de importes 
		--	and ( (AplicaAlmacen = @bEsAlmacen or AplicaFarmacia = @bEsFarmacia) ) -- or TipoDeBeneficiario = @TipoDeBeneficiario )   
		--	and TipoDeBeneficiario in ( 0, @TipoDeBeneficiario )  
		--	and EsProgramaEspecial = @EsProgramasEspeciales 
		--Order by ( D.ValorNominal - D.ImporteAplicado )   
		------------------------ Datos de Pruebas 



		Select Top 0 cast(KeyxGeneral as int) as KeyxGeneral, 
			cast( '' as varchar(4)) as IdFuenteFinanciamiento, cast( '' as varchar(4)) as IdFinanciamiento, cast('' as varchar(4)) as IdDocumento, 
			cast(0 as numeric(14,4)) as Importe_A_Distribuir, 
			---- IdFarmacia, FolioVenta, ClaveSSA, (CantidadVendida * PrecioClaveUnitario) as Importe, 
			IdFarmacia, FolioVenta, ClaveSSA, (Cantidad_Remisionada * PrecioClaveUnitario) as Importe, 
			cast(0 as numeric(14,4)) as ImporteAsignado, cast(0 as numeric(14,4)) as Acumulado, 0 as Incluir,  
			identity(int, 1, 1) as Keyx  
		Into #tmp_AsignacionImporte_Documentos  
		From #tmpLotes 
		where 1 = 0 

		------------------------------------------------------------------------------------------------------------------------------------- 
		------------------------------------------------------------ PROCESAMIENTO A NIVEL FUENTE DE FINANCIAMIENTO 
		Set @iExistenRegistros_Por_Procesar = 1   

		Declare #cursorAsignacionDocumento_Documento  
		Cursor For 
			Select 
				IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, 
				IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado, 
				EsRelacionado, AplicaFarmacia, AplicaAlmacen, ImporteRestante  
			From #tmp_FF_Documentos T 
			-- Where 1 = 0 
			Order by ImporteRestante  
		Open #cursorAsignacionDocumento_Documento 
		FETCH NEXT FROM #cursorAsignacionDocumento_Documento Into @sIdFuenteFinanciamiento, @sIdFinanciamiento, @sIdDocumento, 
																  @sIdFuenteFinanciamiento_Relacionado, @sIdFinanciamiento_Relacionado, @sIdDocumento_Relacionado,	
																  @iEsRelacionado, @bEsFarmacia, @bEsAlmacen, @iCantidad_Disponible  
			WHILE @@FETCH_STATUS = 0 and @iExistenRegistros_Por_Procesar > 0 
			BEGIN 

				Set @iCantidad = 0 
				-- Set @iCantidadAcumulado = 0 
				Set @dCantidadAcumulado_Distribucion = 0 
				Set @dCantidadAcumulado_Aux = 0 
				Set @iFin = 0 
				Set @dCantidadAcumulado = @iCantidad_Disponible  
				Set @iCantidad_AsignadaTotal = 0 
				If @bPrint_Documentos = 1 Print '	' 
				If @bPrint_Documentos = 1 Print 'FF' + @sIdFuenteFinanciamiento + '	FI' + @sIdFinanciamiento +  '	ID' + @sIdDocumento 
				-- If @bPrint_Documentos = 1 Print '			Disponible :	'  + cast(@iCantidad_Disponible as varchar) 


				Select @iCantidad_Disponible = (R.ValorNominal - R.ImporteAplicado)  --  Update R Set ImporteAplicado = (R.ImporteAplicado + @iCantidad_AsignadaTotal) 
				From FACT_Fuentes_De_Financiamiento_Detalles_Documentos R  (NoLock) 
				Where R.IdFuenteFinanciamiento = @sIdFuenteFinanciamiento_Relacionado and R.IdFinanciamiento = @sIdFinanciamiento_Relacionado 
					  and R.IdDocumento = @sIdDocumento_Relacionado and R.TipoDeDocumento = @TipoDeRemision_Auxiliar and R.Status = 'A' 
					
				If @bPrint_Documentos = 1 Print '			Clave SSA :	'  + cast(@sClaveSSA_Proceso as varchar) 
				If @bPrint_Documentos = 1 Print '			Requerido :	'  + cast(@iCantidad as varchar) 
				If @bPrint_Documentos = 1 Print '				Disponible :	'  + cast(@iCantidad_Disponible as varchar) 

			 

				--------------------------------------------------------------------------------------------------------------------------------- 
				--------------------------------------------- ASIGNACION MASIVA  
				If @bProcesoSegmentado = 1 
				Begin 
					Print '' 
					Print 'PROCESO SEGMENTADO: IMPORTES' 
					
					Delete From #tmp_AsignacionImporte_Documentos   

					Insert into #tmp_AsignacionImporte_Documentos ( 
						KeyxGeneral, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, Importe_A_Distribuir, 
						IdFarmacia, FolioVenta, ClaveSSA, Importe, ImporteAsignado, Acumulado, Incluir )  
					Select 
						cast(KeyxGeneral as int) as KeyxGeneral, 
						@sIdFuenteFinanciamiento, @sIdFinanciamiento, @sIdDocumento, @iCantidad_Disponible, 
						IdFarmacia, FolioVenta, ClaveSSA, 
					
						---- ((1 + (TasaIva / 100.00)) * (CantidadVendida * PrecioClaveUnitario)) as Importe, 
						((1 + (TasaIva / 100.00)) * (Cantidad_Remisionada * PrecioClaveUnitario)) as Importe,

						0 as ImporteAsignado, 0 as Acumulado, 0 as Incluir  
					From #tmpLotes  
					Where IdFuenteFinanciamiento = @sIdFuenteFinanciamiento and IdFinanciamiento = @sIdFinanciamiento and Procesado = 0  
							-- and ( EsAlmacen = @bEsAlmacen or EsFarmacia = @bEsFarmacia ) 
					Order by (CantidadVendida * PrecioClaveUnitario) desc, FolioVenta 


					----------------------------- Asignacion 
					Update A Set Acumulado = ( select sum(Importe) From #tmp_AsignacionImporte_Documentos H Where H.Keyx <= A.Keyx )
					From #tmp_AsignacionImporte_Documentos A 

					Update A Set Incluir = 1, ImporteAsignado = Importe 
					From #tmp_AsignacionImporte_Documentos A 
					Where Acumulado <= @iCantidad_Disponible 


					-- if @bPrint = 1 Print space(50) + cast(@iCantidad_Disponible as varchar(100)) 
					Select Top 1 @iCantidad_Disponible = @iCantidad_Disponible - Acumulado, @iCantidad_AsignadaTotal = Acumulado  
					From #tmp_AsignacionImporte_Documentos 
					Where Incluir = 1 
					Order by Acumulado desc 

					-- if @bPrint = 1 Print space(50) + cast(@iCantidad_Disponible as varchar(100))  


					Update L Set Procesado = 1, TipoProcesamiento_02 = 1, 
						IdFuenteFinanciamiento = @sIdFuenteFinanciamiento, IdFinanciamiento = @sIdFinanciamiento, IdDocumento = @sIdDocumento,  
						IdFuenteFinanciamiento_Relacionado = @sIdFuenteFinanciamiento_Relacionado, 
						IdFinanciamiento_Relacionado = @sIdFinanciamiento_Relacionado, IdDocumento_Relacionado = @sIdDocumento_Relacionado  
					From #tmpLotes L 
					Inner Join #tmp_AsignacionImporte_Documentos P On ( L.KeyxGeneral = P.KeyxGeneral and P.ImporteAsignado > 0 ) 

				
					Update R Set ImporteAplicado = (R.ImporteAplicado + @iCantidad_AsignadaTotal) 
					From FACT_Fuentes_De_Financiamiento_Detalles_Documentos R (NoLock)   
					Where R.IdFuenteFinanciamiento = @sIdFuenteFinanciamiento_Relacionado and R.IdFinanciamiento = @sIdFinanciamiento_Relacionado 
						  and R.IdDocumento = @sIdDocumento_Relacionado and R.TipoDeDocumento = @TipoDeRemision_Auxiliar and R.Status = 'A'


					If @bPrint_Documentos = 1 Print 'FF' + @sIdFuenteFinanciamiento + '	FI' + @sIdFinanciamiento +  '	ID' + @sIdDocumento + '   Monto Asignado = ' + cast(@iCantidad_AsignadaTotal as varchar(50))  

					Print '' 
					Print '' 
				End 
								
				--------------------------------------------- ASIGNACION MASIVA  
				--------------------------------------------------------------------------------------------------------------------------------- 
			

				------------------------------------------------------------------------- ASIGNACION MANUAL DE LOS REGISTROS PENDIENTES DE PROCESAR  
				Delete From #tmp_AsignacionImporte_Documentos   

				Insert into #tmp_AsignacionImporte_Documentos ( 
					KeyxGeneral, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, Importe_A_Distribuir, 
					IdFarmacia, FolioVenta, ClaveSSA, Importe, ImporteAsignado, Acumulado, Incluir )  
				Select 
					cast(KeyxGeneral as int) as KeyxGeneral, 
					@sIdFuenteFinanciamiento, @sIdFinanciamiento, @sIdDocumento, @iCantidad_Disponible, 
					IdFarmacia, FolioVenta, ClaveSSA, 	
					---- ((1 + (TasaIva / 100.00)) * (CantidadVendida * PrecioClaveUnitario)) as Importe, 
					((1 + (TasaIva / 100.00)) * (Cantidad_Remisionada * PrecioClaveUnitario)) as Importe, 
					0 as ImporteAsignado, 0 as Acumulado, 0 as Incluir  
				From #tmpLotes  
				Where IdFuenteFinanciamiento = @sIdFuenteFinanciamiento and IdFinanciamiento = @sIdFinanciamiento and Procesado = 0  
						-- and ( EsAlmacen = @bEsAlmacen or EsFarmacia = @bEsFarmacia ) 
				Order by (CantidadVendida * PrecioClaveUnitario) desc, FolioVenta 


				--Select 'Tabla : AsignacionImporte_Documentos' as Tabla ,* from #tmp_AsignacionImporte_Documentos 

				Set @iCantidad_AsignadaTotal = 0 
				Select @iCantidad_Disponible = ( R.ValorNominal - R.ImporteAplicado )   --  Update R Set ImporteAplicado = (R.ImporteAplicado + @iCantidad_AsignadaTotal) 
				From FACT_Fuentes_De_Financiamiento_Detalles_Documentos R  (NoLock) 
				Where R.IdFuenteFinanciamiento = @sIdFuenteFinanciamiento_Relacionado and R.IdFinanciamiento = @sIdFinanciamiento_Relacionado 
					  and R.IdDocumento = @sIdDocumento_Relacionado and R.TipoDeDocumento = @TipoDeRemision_Auxiliar and R.Status = 'A' 
					
				If @bPrint_Documentos = 1 Print '				Disponible :	'  + cast(@iCantidad_Disponible as varchar) 


				-------------------------------------------------------------- DISTRIBUCION A NIVEL FOLIO DE VENTA ( RECETA ) 
				Declare #cursorAsignacion_Documento   
				Cursor For 
				Select KeyxGeneral, ClaveSSA, Importe 
				From #tmp_AsignacionImporte_Documentos 
				--Where ClaveSSA = @sClaveSSA_Proceso  
				Order By Keyx  
				Open #cursorAsignacion_Documento 
				FETCH NEXT FROM #cursorAsignacion_Documento  Into @KeyxDetalle, @sClaveSSA_Proceso, @iCantidad     
					WHILE @@FETCH_STATUS = 0 and @iFin = 0 
					BEGIN 
						-- Print '					Clave SSA :	'  + cast(@sClaveSSA_Proceso as varchar) 
						-- Print '					Requerido :	'  + cast(@iCantidad as varchar) 

						If ( @iCantidad_Disponible < @iCantidad ) 
						Begin 
							Set @iCantidad = 0 
						End 

						If ( @iCantidad_Disponible >= @iCantidad ) 
						Begin 
							Set @iCantidad_Disponible = @iCantidad_Disponible - @iCantidad 
							-- Print '						Asignacion :	'  + cast(@iCantidad as varchar) 
							-- Print '						Disponible :	'  + cast(@iCantidad_Disponible as varchar) 
						End 


						If @iCantidad_Disponible = 0 
						Begin 
							Set @iFin = 1  
						End 					
			
						Set @iCantidad_AsignadaTotal = @iCantidad_AsignadaTotal + @iCantidad 
				
						Update C Set ImporteAsignado = IsNull(@iCantidad, 0)   
						From #tmp_AsignacionImporte_Documentos C (NoLock) 
						Where KeyxGeneral = @KeyxDetalle  			
				
						FETCH NEXT FROM #cursorAsignacion_Documento  Into  @KeyxDetalle, @sClaveSSA_Proceso, @iCantidad     
					END
				Close #cursorAsignacion_Documento 
				Deallocate #cursorAsignacion_Documento 
				-------------------------------------------------------------- DISTRIBUCION A NIVEL FOLIO DE VENTA ( RECETA ) 
				------------------------------------------------------------------------- ASIGNACION MANUAL DE LOS REGISTROS PENDIENTES DE PROCESAR  

				----------- La administracion se cobra completa aunque se haya excedido la cantidad 
				--If ( @TipoDispensacion = 1 )  
				--Begin 
				--	Update A Set Incluir = 1 
				--	From #tmp_Asignacion_Cantidades A 

				--	-- print 'CONSIGNA'   
				--End  


				Update L Set Procesado = 1, TipoProcesamiento_02 = 2,
					IdFuenteFinanciamiento = @sIdFuenteFinanciamiento, IdFinanciamiento = @sIdFinanciamiento, IdDocumento = @sIdDocumento,  
					IdFuenteFinanciamiento_Relacionado = @sIdFuenteFinanciamiento_Relacionado, 
					IdFinanciamiento_Relacionado = @sIdFinanciamiento_Relacionado, IdDocumento_Relacionado = @sIdDocumento_Relacionado  
				From #tmpLotes L 
				Inner Join #tmp_AsignacionImporte_Documentos P On ( L.KeyxGeneral = P.KeyxGeneral and P.ImporteAsignado > 0 ) 

				
				Update R Set ImporteAplicado = (R.ImporteAplicado + @iCantidad_AsignadaTotal) 
				From FACT_Fuentes_De_Financiamiento_Detalles_Documentos R (NoLock)   
				Where R.IdFuenteFinanciamiento = @sIdFuenteFinanciamiento_Relacionado and R.IdFinanciamiento = @sIdFinanciamiento_Relacionado 
					  and R.IdDocumento = @sIdDocumento_Relacionado and R.TipoDeDocumento = @TipoDeRemision_Auxiliar and R.Status = 'A'  


				If @bPrint_Documentos = 1 Print 'FF' + @sIdFuenteFinanciamiento + '	FI' + @sIdFinanciamiento +  '	ID' + @sIdDocumento + '   Monto Asignado = ' + cast(@iCantidad_AsignadaTotal as varchar(50))  


				------------------------------------------ Revisar si faltan registros por procesar 
				Select @iExistenRegistros_Por_Procesar = count(*) From #tmpLotes Where Procesado = 0  



				FETCH NEXT FROM #cursorAsignacionDocumento_Documento Into @sIdFuenteFinanciamiento, @sIdFinanciamiento, @sIdDocumento, 
																		  @sIdFuenteFinanciamiento_Relacionado, @sIdFinanciamiento_Relacionado, @sIdDocumento_Relacionado,	
																		  @iEsRelacionado, @bEsFarmacia, @bEsAlmacen, @iCantidad_Disponible  
				-- FETCH NEXT FROM #cursorAsignacionDocumento_Documento Into @sIdFuenteFinanciamiento, @sIdFinanciamiento, @sIdDocumento, @bEsFarmacia, @bEsAlmacen, @iCantidad_Disponible    
			END	 
		Close #cursorAsignacionDocumento_Documento 
		Deallocate #cursorAsignacionDocumento_Documento 	
		------------------------------------------------------------ PROCESAMIENTO A NIVEL FUENTE DE FINANCIAMIENTO 

		------ Asignar los importes distribuidos 
		Update C Set CantidadAsignada = IsNull((select sum(CantidadVendida) From #tmpLotes L (NoLock) 
			Where L.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and L.IdFinanciamiento = C.IdFinanciamiento and IdDocumento = IdDocumento and Procesado = 1 ), 0 )  
		From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA C (noLock) 


		Select 
			IDENTITY(int, 1, 1) as Consecutivo, 
			IdFuenteFinanciamiento, IdFinanciamiento, 0 as TipoDeInsumo, 
			sum(((1 + (TasaIva / 100.00)) * (Cantidad_Remisionada * PrecioClaveUnitario))) as Importe_A_Asignar  
		Into #tmp_FF_ListaDocumentos__AUX  
		From #tmpLotes 
		Where Procesado = 1 
		Group By IdFuenteFinanciamiento, IdFinanciamiento, GrupoProceso  
		Order By IdFuenteFinanciamiento, IdFinanciamiento, GrupoProceso   

		--Select 'DESPUES DISTRIBUCION DOCUMENTOS' as Proceso, * from #tmp_FF_ListaDocumentos__AUX

	End 
	------------------------------------------------------------ ASIGNACION DE DOCUMENTO DE CONTROL DE IMPORTES  

	--Select '02 Despues Documentos' as Paso, * From #tmpLotes 

	----Select '02 Despues Documentos' as Paso, Procesado, count(*) as Registros  
	----From #tmpLotes 
	----group by Procesado 

	If @bPrint_MarcaDeTiempo_Select = 1 Select 'INTERMEDIO 01' as Proceso, getdate() as MarcaDeTiempo 

	------------------------------------------------------------ EXCLUIR LAS H'S QUE NO ESTEN EN LA CONFIGURACION 
	------------ Proceso especial para SSH Primer Nivel 
	Update L Set IdGrupoDeRemisiones = 0  
	From #tmpLotes L (NoLock) 

	Update L Set IdGrupoDeRemisiones = -1 
	From #tmpLotes L (NoLock) 
	Where Not Exists 
		( 
			Select * 
			From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones C (NoLock) 
			Where L.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and L.IdFinanciamiento = C.IdFinanciamiento and L.Referencia_01 = C.Referencia_01 and C.Status = 'A'  
		) 
		and Procesado = 1 and @AsignarReferencias = 1 and @Procesar_SoloClavesReferenciaRemisiones = 1  

	--Select * 
	--From #tmpLotes L (NoLock) 
	--Where Not Exists 
	--	( 
	--		Select * 
	--		From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones C (NoLock) 
	--		Where L.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and L.IdFinanciamiento = C.IdFinanciamiento and L.Referencia_01 = C.Referencia_01 and C.Status = 'A'  
	--	) 
	--	and Procesado = 1 and @AsignarReferencias = 1 and @Procesar_SoloClavesReferenciaRemisiones = 1  


	--select IdGrupoDeRemisiones, count(*) as Registros  From #tmpLotes L (NoLock) group by IdGrupoDeRemisiones

	--------------------------------------------------------------------------------------------------------------------
	---- Eliminar los renglones que no estan contemplados en la separación por Referencias (H's) 
	--select * from #tmpLotes L (NoLock)   	 

	Delete From #tmpLotes Where IdGrupoDeRemisiones = -1 
	--select count(*) as Registros_Despues_Borrado From #tmpLotes L (NoLock)   	

	--select * from #tmpLotes L (NoLock)   	 

	------------ Proceso especial para SSH Primer Nivel 
	------------------------------------------------------------ EXCLUIR LAS H'S QUE NO ESTEN EN LA CONFIGURACION 


	------------------------------------------------------------ CONTROL POR IMPORTES, VENTAS ANTICIPADAS   
	If @EsRelacionFacturaPrevia = 1 and @EsRelacionMontos = 1 and Exists ( select top 1 * From #tmpLotes Where Procesado = 1 ) 
	Begin 
		------------- Inicializar todos los registros 
		--Select 'Desgloze proceso' as Paso, Procesado, sum(((1 + (TasaIva / 100.00)) * (Cantidad_Remisionada * PrecioClaveUnitario))) as Importe 
		--from #tmpLotes 
		--Group by Procesado 


		-- if @bPrint_Importes = 1 print 'Borrando informacion' 
		-- Select 'ANTES BORRADO' as Proceso, count(*) From #tmpLotes 
		Delete From #tmpLotes Where Procesado = 0   
		Update L Set Procesado = 0 From #tmpLotes L  
		--Select 'DESPUES BORRADO' as Proceso, count(*) From #tmpLotes 



		Select Top 0 cast(KeyxGeneral as int) as KeyxGeneral, 
			cast( '' as varchar(4)) as IdFuenteFinanciamiento, cast( '' as varchar(4)) as IdFinanciamiento, cast('' as varchar(4)) as IdDocumento, 
			cast(0 as numeric(14,4)) as Importe_A_Distribuir, 
			---- IdFarmacia, FolioVenta, ClaveSSA, (CantidadVendida * PrecioClaveUnitario) as Importe, 
			IdFarmacia, FolioVenta, ClaveSSA, (Cantidad_Remisionada * PrecioClaveUnitario) as Importe, 
			cast(0 as numeric(14,4)) as ImporteAsignado, cast(0 as numeric(14,4)) as Acumulado, 0 as Incluir,  
			identity(int, 1, 1) as Keyx  
		Into #tmp_Asignacion_x_Importes   
		From #tmpLotes 
		where 1 = 0 


		------------- Agregar la informacion a procesar  
		Delete From #tmp_Asignacion_x_Importes   

		Insert into #tmp_Asignacion_x_Importes ( 
			KeyxGeneral, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, Importe_A_Distribuir, 
			IdFarmacia, FolioVenta, ClaveSSA, Importe, ImporteAsignado, Acumulado, Incluir )  
		Select 
			cast(KeyxGeneral as int) as KeyxGeneral, 
			@sIdFuenteFinanciamiento, @sIdFinanciamiento, @sIdDocumento, @iCantidad_Disponible, 
			IdFarmacia, FolioVenta, ClaveSSA, 	
			---- ((1 + (TasaIva / 100.00)) * (CantidadVendida * PrecioClaveUnitario)) as Importe, 
			((1 + (TasaIva / 100.00)) * (Cantidad_Remisionada * PrecioClaveUnitario)) as Importe, 
			0 as ImporteAsignado, 0 as Acumulado, 0 as Incluir  
		From #tmpLotes  
		Where Procesado = 0  
				-- and ( EsAlmacen = @bEsAlmacen or EsFarmacia = @bEsFarmacia ) 
		Order by (CantidadVendida * PrecioClaveUnitario) desc, FolioVenta 



		-------------------------------------------------------------- DISTRIBUCION A NIVEL FOLIO DE VENTA ( RECETA ) 
		Set @iCantidad = 0 
		Set @iCantidad_Disponible = @iImporte_FacturaRelacionada 
		
		if @bPrint_Importes = 1 Print '' 
		if @bPrint_Importes = 1 Print 'ASIGNACION X IMPORTES' 

		Declare #cursorAsignacion_Documento   
		Cursor For 
		Select KeyxGeneral, ClaveSSA, Importe 
		From #tmp_Asignacion_x_Importes 
		--Where ClaveSSA = @sClaveSSA_Proceso  
		Order By Keyx  
		Open #cursorAsignacion_Documento 
		FETCH NEXT FROM #cursorAsignacion_Documento  Into @KeyxDetalle, @sClaveSSA_Proceso, @iCantidad     
			WHILE @@FETCH_STATUS = 0 and @iFin = 0 
			BEGIN 
				--if @bPrint_Importes = 1 Print '					Clave SSA :	'  + cast(@sClaveSSA_Proceso as varchar) 
				if @bPrint_Importes_Detalles = 1 Print '					Requerido :	'  + cast(@iCantidad as varchar) 

				If ( @iCantidad_Disponible < @iCantidad ) 
				Begin 
					Set @iCantidad = 0 
				End 

				If ( @iCantidad_Disponible >= @iCantidad ) 
				Begin 
					Set @iCantidad_Disponible = @iCantidad_Disponible - @iCantidad 
					if @bPrint_Importes_Detalles = 1 Print '						Asignacion :	'  + cast(@iCantidad as varchar) 
					if @bPrint_Importes_Detalles = 1 Print '						Disponible :	'  + cast(@iCantidad_Disponible as varchar) 
				End 


				If @iCantidad_Disponible = 0 
				Begin 
					Set @iFin = 1  
				End 
			
				Set @iCantidad_AsignadaTotal = @iCantidad_AsignadaTotal + @iCantidad 
				
				Update C Set ImporteAsignado = IsNull(@iCantidad, 0)   
				From #tmp_Asignacion_x_Importes C (NoLock) 
				Where KeyxGeneral = @KeyxDetalle  			
				
				FETCH NEXT FROM #cursorAsignacion_Documento  Into  @KeyxDetalle, @sClaveSSA_Proceso, @iCantidad     
			END
		Close #cursorAsignacion_Documento 
		Deallocate #cursorAsignacion_Documento 
		
		Update L Set Procesado = 1, TipoProcesamiento_02 = 3 
			--IdFuenteFinanciamiento = @sIdFuenteFinanciamiento, IdFinanciamiento = @sIdFinanciamiento, IdDocumento = @sIdDocumento,  
			--IdFuenteFinanciamiento_Relacionado = @sIdFuenteFinanciamiento_Relacionado, 
			--IdFinanciamiento_Relacionado = @sIdFinanciamiento_Relacionado, IdDocumento_Relacionado = @sIdDocumento_Relacionado  
		From #tmpLotes L 
		Inner Join #tmp_Asignacion_x_Importes P On ( L.KeyxGeneral = P.KeyxGeneral and P.ImporteAsignado > 0 ) 
		-------------------------------------------------------------- DISTRIBUCION A NIVEL FOLIO DE VENTA ( RECETA ) 


		---SELECT 'DESPUES DE ASIGNACION', * FROM #tmpLotes 

	End 
	------------------------------------------------------------ CONTROL POR IMPORTES, VENTAS ANTICIPADAS   


	If @bPrint_MarcaDeTiempo_Select = 1 Select 'INTERMEDIO 02' as Proceso, getdate() as MarcaDeTiempo 

	--Select 'Resumen 03' as Campo, 
	--	IdFarmacia, 
	--	IdFuenteFinanciamiento, IdFinanciamiento, 
	--	-- ClaveSSA, 
	--	sum(CantidadVendida) as Piezas, sum(Cantidad_Remisionada) as Cantidad_Remisionada__Piezas  
	--From #tmpLotes (NoLock) 
	--where Procesado = 1 
	--Group by IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento -- , ClaveSSA 
	--Order by IdFarmacia 


	----------------------------- Verificar el tipo de procesamiento al asignar Documento Relacionado 
	if @bVerificarAsignaciones = 1 
	Begin 
		select TipoProcesamiento_02, count(*) as Registros_Procesados  
		from #tmpLotes  
		Where Procesado = 1 
		Group by TipoProcesamiento_02 
	End 


	------------------------------------------------------------ PROCESO DE DISTRIBUCION 
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   



	------------------------------------------------------------ DETERMINAR EL NÚMERO DE PAQUETES COMPLETOS    
	--select * From #tmpLotes L (NoLock) 

	-------------- Aplica en el caso de Claves de Consigna 
	Update L Set ContenidoPaquete_Licitado = ContenidoPaquete_ClaveSSA 
	From #tmpLotes L (NoLock) 
	Where Cantidad_Remisionada > 0 and ContenidoPaquete_Licitado <= 0 

	-- select 'FACTOR 003.2', * from #tmpLotes     

	Update L Set 
		-- Cantidad_Remisionada = ( L.Cantidad_Remisionada / (Factor * 1.0)), 
		-- Cantidad_Remisionada_Agrupada = ( ( L.Cantidad_Remisionada / (Factor * 1.0)) / (ContenidoPaquete_Licitado * 1.0))  

		Cantidad_Remisionada = ( L.Cantidad_Remisionada ), 
		Cantidad_Remisionada_Agrupada = ( ( L.Cantidad_Remisionada ) / (  (case when L.EsUnidosis = 0 then ContenidoPaquete_Licitado * 1.0 else 1 end) ))  
	From #tmpLotes L (NoLock) 
	Where Cantidad_Remisionada > 0 -- and 1 = 0 

	--select * From #tmpLotes L (NoLock) 
	------------------------------------------------------------ DETERMINAR EL NÚMERO DE PAQUETES COMPLETOS    





	--------------------------------------------------------------------------------------------------------------------------------------------- 
	------------------------------- CLAVES CON EXCEPCÍON DE ACUERDO A CAUSES   -- 20170712.1650 
	Select  
		E.IdEstado, E.IdCliente, E.IdSubCliente, E.NumeroDePartidaLicitacion, 
		E.IdClaveSSA, E.ClaveSSA, E.PrecioBase, E.Incremento, E.PorcentajeIncremento, E.PrecioFinal, E.Status, 
		cast((E.PrecioBase / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as PrecioBase_Unitario, 
		cast((E.Incremento / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as Incremento_Unitario   
	Into #FACT_ClavesSSA_Precios__Excepciones 
	From FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
	Inner Join #vw_Claves_Precios_Asignados C (NoLock) 
		On ( E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente and E.ClaveSSA = C.ClaveSSA  ) 
	Where Exists 
	( 
		Select * 
		From #tmpLotes L (NoLock) 
		Where L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA 
	) and @TipoDeRemision in ( 1, 4 )  
	  and @TipoDispensacion = 0 ---- Solo producto de Venta 
	  and @EsRemision_Complemento = 0 


	--Select 'Lotes' as Lotes, @TipoDeRemision as TipoDeRemision, * from #tmpLotes 
	--Select 'Excepciones' as Excepciones, * from #FACT_ClavesSSA_Precios__Excepciones 


	Update L Set IncluidoEnExcepcion = 1 
	From #tmpLotes L (NoLock) 
	Inner Join #FACT_ClavesSSA_Precios__Excepciones E (NoLock)   
		On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 
	-- Where E.IdEstado = @IdEstado  


	------------------------------------- 20181203.1140   SSH 1N 
	--Select @bExisteInformacion_x_Procesar_Excepciones as ExisteInformacion_x_Procesar_Excepciones 
	--If ( @IdEmpresa = '002' and @IdEstado = '09' and @IdCliente = '0002' and @IdSubCliente = '0012' ) and @TipoDeRemision in ( 1, 4, 2, 6 ) 
	If @TipoDeRemision in ( 1, 4, 2, 6 ) and @EsRemision_Complemento = 0 and @bExisteInformacion_x_Procesar_Excepciones = 1  --------- solo remisiones normales [Excluir Incrementos | Diferenciador]
	Begin 

		Delete From #FACT_ClavesSSA_Precios__Excepciones   

		Update L Set IncluidoEnExcepcion = 0  
		From #tmpLotes L (NoLock) 



		------------------------------- PRODUCTO  
		Insert Into #FACT_ClavesSSA_Precios__Excepciones 
		Select  
			@IdEstado as IdEstado, @IdCliente as IdCliente, @IdSubCliente as  IdSubCliente, 0 as NumeroDePartidaLicitacion, 
			C.IdClaveSSA, E.ClaveSSA, E.PrecioBase, E.Incremento, E.PorcentajeIncremento, E.PrecioFinal, E.Status, 
			cast((E.PrecioBase / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as PrecioBase_Unitario, 
			cast((E.Incremento / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as Incremento_Unitario   
		From FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones E (NoLock) 
		Inner Join #vw_Claves_Precios_Asignados C (NoLock) 
			On ( 
				-- E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
				C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
				and E.ClaveSSA = C.ClaveSSA  
				) 
		Where E.Tipo = 1 and 
			Exists 
			( 
				Select * 
				From #tmpLotes L (NoLock) 
				Where L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente and L.ClaveSSA = E.ClaveSSA 
					and L.IdFuenteFinanciamiento = E.IdFuenteFinanciamiento and L.IdFinanciamiento = E.IdFinanciamiento and L.Referencia_01 = E.Referencia_01
			) 
				and @TipoDeRemision in ( 1, 4  )  
				and @TipoDispensacion = 0 ---- Solo producto de Venta 


		------------------------------- SERVICIO 
		Insert Into #FACT_ClavesSSA_Precios__Excepciones 
		Select  
			@IdEstado as IdEstado, @IdCliente as IdCliente, @IdSubCliente as  IdSubCliente, 0 as NumeroDePartidaLicitacion, 
			C.IdClaveSSA, E.ClaveSSA, E.PrecioBase, E.Incremento, E.PorcentajeIncremento, E.PrecioFinal, E.Status, 
			cast((E.PrecioBase / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as PrecioBase_Unitario, 
			cast((E.Incremento / (C.ContenidoPaquete / 1.0)) as numeric(14,4)) as Incremento_Unitario   
		From FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones E (NoLock) 
		Inner Join #vw_Claves_Precios_Asignados C (NoLock) 
			On ( 
				-- E.IdEstado = C.IdEstado and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
				C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
				and E.ClaveSSA = C.ClaveSSA  
				) 
		Where E.Tipo = 2 and 
			Exists 
			( 
				Select * 
				From #tmpLotes L (NoLock) 
				Where L.IdEstado = @IdEstado and L.IdCliente = @IdCliente and L.IdSubCliente = @IdSubCliente and L.ClaveSSA = E.ClaveSSA 
					and L.IdFuenteFinanciamiento = E.IdFuenteFinanciamiento and L.IdFinanciamiento = E.IdFinanciamiento and L.Referencia_01 = E.Referencia_01
			) 
				and @TipoDeRemision in ( 2, 6  )  
				and @TipoDispensacion = 0 ---- Solo producto de Venta   



		Update L Set IncluidoEnExcepcion = 1 
		From #tmpLotes L (NoLock) 
		Inner Join #FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
			On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 
		--Where @EsRemision_Complemento = 0 


	End 
		--Select 'Excepciones' as Excepciones, * from #FACT_ClavesSSA_Precios__Excepciones 
	------------------------------------- 20181203.1140   SSH 1N 


	Select * 
	Into #tmpLotes___Excepcion_01 
	From #tmpLotes (NoLock) 
	Where @EsRemision_Complemento = 0 and IncluidoEnExcepcion = 1 and Procesado = 1 ---- Solo enviar los registros procesados 

	Select * 
	Into #tmpLotes___Excepcion_02 
	From #tmpLotes (NoLock) 
	Where @EsRemision_Complemento = 0 and IncluidoEnExcepcion = 1 and 1 = 0 --- No agregarlo de nuevo 


	Update L Set PrecioClave = E.PrecioBase, PrecioClaveUnitario = PrecioBase_Unitario 
	From #tmpLotes___Excepcion_01 L (NoLock) 
	Inner Join #FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
		On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 


	Update L Set PrecioClave = E.Incremento, PrecioClaveUnitario = Incremento_Unitario, GrupoProceso = 3  
	From #tmpLotes___Excepcion_02 L (NoLock) 
	Inner Join #FACT_ClavesSSA_Precios__Excepciones E (NoLock) 
		On ( L.IdEstado = E.IdEstado and L.IdCliente = E.IdCliente and L.IdSubCliente = E.IdSubCliente and L.ClaveSSA = E.ClaveSSA ) 


	 --select * from #tmpLotes 
	 --select 'XXX', * from #tmpLotes___Excepcion_01  



	----------------------- Enviar al repositorio de Incrementos   
	Delete From FACT_Incremento___VentasDet_Lotes 
	From FACT_Incremento___VentasDet_Lotes L (NoLock) 
	Inner Join #tmpLotes___Excepcion_01 R (NoLock) 
		On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmacia = R.IdFarmacia and L.IdSubFarmacia = L.IdSubFarmacia and L.FolioVenta = R.FolioVenta and 
			 L.IdProducto = R.IdProducto and L.CodigoEAN = R.CodigoEAN and L.ClaveLote = R.ClaveLote and L.SKU = R.SKU and ( L.EnRemision_Insumo = 0 and L.EnRemision_Admon = 0 ) )   


	Insert Into FACT_Incremento___VentasDet_Lotes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, SKU, Renglon, EsConsignacion, Cant_Vendida, Cant_Devuelta, CantidadVendida, 
		Status, Actualizado, CostoUnitario, EnRemision_Insumo, EnRemision_Admon, RemisionFinalizada, FechaControl, CantidadRemision_Insumo, CantidadRemision_Admon 
	) 
	Select  
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU, L.Renglon, L.EsConsignacion, 
		L.Cant_Vendida, L.Cant_Devuelta, L.CantidadVendida, L.Status, L.Actualizado, L.CostoUnitario, 
		-- L.EnRemision_Insumo, L.EnRemision_Admon, L.RemisionFinalizada, L.FechaControl, 
		0 as EnRemision_Insumo, 0 as EnRemision_Admon, 0 as RemisionFinalizada, L.FechaControl, 
		0 as CantidadRemision_Insumo, 0 as CantidadRemision_Admon  
	From VentasDet_Lotes L (NoLock) 
	Inner Join #tmpLotes___Excepcion_01 R (NoLock) 
		On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmacia = R.IdFarmacia and L.IdSubFarmacia = L.IdSubFarmacia and L.FolioVenta = R.FolioVenta and 
			 L.IdProducto = R.IdProducto and L.CodigoEAN = R.CodigoEAN and L.ClaveLote = R.ClaveLote and L.SKU = R.SKU ) 
	Where L.ClaveLote not like '%*%' and 
		Not Exists 
		( 
			Select * 
			From FACT_Incremento___VentasDet_Lotes L (NoLock) 
			Where L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmacia = R.IdFarmacia and L.IdSubFarmacia = L.IdSubFarmacia and L.FolioVenta = R.FolioVenta and 
				 L.IdProducto = R.IdProducto and L.CodigoEAN = R.CodigoEAN and L.ClaveLote = R.ClaveLote and L.SKU = R.SKU -- and L.EnRemision_Insumo = 1  
				 and ( L.CantidadRemision_Insumo > 0 or L.CantidadRemision_Admon > 0 )
			
		)  
	----------------------- Enviar al repositorio de Incrementos   



	------ Quitar los registros reprocesados 
	--select * from #tmpLotes 
	Delete From #tmpLotes Where IncluidoEnExcepcion = 1 
	--select * from #tmpLotes 


	Insert Into #tmpLotes 
	Select 	
		EsAlmacen, EsFarmacia, EsUnidosis, 
		FolioRemision, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		IdFuenteFinanciamiento, IdFinanciamiento, 
		Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
		IdDocumento, IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado, 		
		Procesado, TipoProcesamiento_01, TipoProcesamiento_02, 
		IdClaveSSA, ClaveSSA, Relacionada, IdClaveSSA_P, ClaveSSA_P, 
		ContenidoPaquete_ClaveSSA, ContenidoPaquete_Licitado, Factor, MultiploRelacion, IdTipoProducto, IdProducto, CodigoEAN, ClaveLote, SKU, 
		PrecioClave, PrecioClaveUnitario, TasaIva, 
		PartidaGeneral, AfectaEstadistica, AfectaEstadisticaMontos, EsDecimal, CantidadVendida, CantidadVendida_Base, Cantidad_Agrupada, Cantidad_Remisionada, Cantidad_Remisionada_Agrupada, 
		GrupoProceso, IncluidoEnExcepcion, TipoDeAsignacion, IdGrupoDeRemisiones, GrupoDispensacion, EsControlado, EsAntibiotico, EsRefrigerado, EsExcepcionPrecio     
	From #tmpLotes___Excepcion_01 

	Insert Into #tmpLotes 
	Select 	
		EsAlmacen, EsFarmacia, EsUnidosis,  
		FolioRemision, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		IdFuenteFinanciamiento, IdFinanciamiento, 
		Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
		IdDocumento, IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado, 		
		Procesado, TipoProcesamiento_01, TipoProcesamiento_02, 
		IdClaveSSA, ClaveSSA, Relacionada, IdClaveSSA_P, ClaveSSA_P, 
		ContenidoPaquete_ClaveSSA, ContenidoPaquete_Licitado, Factor, MultiploRelacion, IdTipoProducto, IdProducto, CodigoEAN, ClaveLote, SKU, 
		PrecioClave, PrecioClaveUnitario, TasaIva, 
		PartidaGeneral, AfectaEstadistica, AfectaEstadisticaMontos, EsDecimal, CantidadVendida, CantidadVendida_Base, Cantidad_Agrupada, Cantidad_Remisionada, Cantidad_Remisionada_Agrupada, 
		GrupoProceso, IncluidoEnExcepcion, TipoDeAsignacion, IdGrupoDeRemisiones, GrupoDispensacion, EsControlado, EsAntibiotico, EsRefrigerado, EsExcepcionPrecio     
	From #tmpLotes___Excepcion_02 


	--select * from #tmpLotes 
	------------------------------- CLAVES CON EXCEPCÍON DE ACUERDO A CAUSES   -- 20170712.1650 
	--------------------------------------------------------------------------------------------------------------------------------------------- 


	-- delete from #tmpLotes Where KeyxGeneral >= 2 
	-- Select 'REV ADMON DISTRIBUIDA' as X, * From #tmpLotes L (NoLock) 

	-- Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	-- Select * from #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze 


	--Where ContenidoPaquete_ClaveSSA = 0 

	--Select @AsignarReferencias as AsignarReferencias, * From #tmpLotes L (NoLock) 


	--		spp_FACT_GenerarRemisiones_General__FFxFarmacia 

	--------------------------------------------------------------------------------------------------------------------------------------------- 
	------------------------------- Generar los folios de remision 
	Select @iFolioRemision = cast( (max(FolioRemision) + 1) as varchar) 
	From FACT_Remisiones (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
	Set @iFolioRemision = IsNull(@iFolioRemision, 1) 


	------------------------- AGRUPAR POR H's ó PROYECTO   
	---------- Proceso especial para SSH Primer Nivel 
	Update L Set IdGrupoDeRemisiones = 0  
	From #tmpLotes L (NoLock) 

	Update L Set IdGrupoDeRemisiones = C.IdGrupo 
	From #tmpLotes L (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones C (NoLock) 
		On ( L.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento and L.IdFinanciamiento = C.IdFinanciamiento and L.Referencia_01 = C.Referencia_01 and C.Status = 'A'  ) 
	Where Procesado = 1 and @AsignarReferencias = 1 -- and C.TipoRemision = @TipoDeRemision_Auxiliar 
	---------- Proceso especial para SSH Primer Nivel 
	------------------------- AGRUPAR POR H's ó PROYECTO  


	--select 'ANTES DE DESGLOZE' as Tipo, * from #tmpLotes 

	------------------------- ASIGNACION DE FOLIOS DE REMISION		20180715.0240 
	If Exists ( Select top 1 * From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze (NoLock) ) 
	Begin 

		Update #tmpLotes Set PartidaGeneral = -1, AfectaEstadistica = 0, AfectaEstadisticaMontos = 0 


		Select 	
			L.EsAlmacen, L.EsFarmacia, L.EsUnidosis, 
			L.FolioRemision, L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioVenta, L.Partida, 
			L.IdCliente, L.IdSubCliente, L.IdPrograma, L.IdSubPrograma, 
			L.IdFuenteFinanciamiento, L.IdFinanciamiento, 
			L.Referencia_01, L.Referencia_02, L.Referencia_03, L.Referencia_04, L.Referencia_05, 
			L.IdDocumento, 
			L.IdFuenteFinanciamiento_Relacionado, L.IdFinanciamiento_Relacionado, L.IdDocumento_Relacionado, 		
			L.Procesado, L.TipoProcesamiento_01, L.TipoProcesamiento_02, 
			L.IdClaveSSA, L.ClaveSSA, L.Relacionada, L.IdClaveSSA_P, L.ClaveSSA_P, 
			L.ContenidoPaquete_ClaveSSA, L.ContenidoPaquete_Licitado, L.Factor, L.MultiploRelacion, L.IdTipoProducto, L.IdProducto, L.CodigoEAN, L.ClaveLote, 
			
			DF.Costo as PrecioClave, DF.Costo as PrecioClaveUnitario, DF.TasaIva, 
			DF.Partida as PartidaGeneral, 
			DF.AfectaEstadistica, DF.AfectaEstadisticaMontos, 
			
			L.EsDecimal, L.CantidadVendida, L.CantidadVendida_Base, L.Cantidad_Agrupada, L.Cantidad_Remisionada, L.Cantidad_Remisionada_Agrupada, 
			L.GrupoProceso, L.IncluidoEnExcepcion, L.TipoDeAsignacion, L.IdGrupoDeRemisiones, L.GrupoDispensacion, 
			L.EsControlado, L.EsAntibiotico, L.EsRefrigerado    
		Into #tmpLotes___AdmonSegmentada 
		From #tmpLotes L (NoLock) 
		Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze DF (NoLock) 
			On ( L.IdFuenteFinanciamiento = DF.IdFuenteFinanciamiento and L.IdFinanciamiento = DF.IdFinanciamiento 
				 and L.IdEstado = DF.IdEstado and L.IdFarmacia = DF.IdFarmacia and L.ClaveSSA = DF.ClaveSSA and L.Referencia_01 = DF.Referencia_01 ) 


		If Exists ( Select top 1 * From #tmpLotes___AdmonSegmentada (NoLock) ) 
		Begin 
			---- Eliminar el registro base 
			Delete From #tmpLotes Where PartidaGeneral = -1 
		
			---- Agregar los renglones nuevos 
			Insert Into #tmpLotes 
			Select 	
				EsAlmacen, EsFarmacia, EsUnidosis, 
				FolioRemision, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, 
				IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
				IdFuenteFinanciamiento, IdFinanciamiento, 
				Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
				IdDocumento, 
				IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado, 		
				Procesado, TipoProcesamiento_01, TipoProcesamiento_02, 
				IdClaveSSA, ClaveSSA, Relacionada, IdClaveSSA_P, ClaveSSA_P, 
				ContenidoPaquete_ClaveSSA, ContenidoPaquete_Licitado, Factor, MultiploRelacion, IdTipoProducto, IdProducto, CodigoEAN, ClaveLote, SKU, 
				PrecioClave, PrecioClaveUnitario, TasaIva, 
				PartidaGeneral, AfectaEstadistica, AfectaEstadisticaMontos, EsDecimal, 
				CantidadVendida, CantidadVendida_Base, Cantidad_Agrupada, Cantidad_Remisionada, Cantidad_Remisionada_Agrupada, 
				GrupoProceso, IncluidoEnExcepcion, TipoDeAsignacion, IdGrupoDeRemisiones, GrupoDispensacion, EsControlado, EsAntibiotico, EsRefrigerado     
			From #tmpLotes___AdmonSegmentada  

		End 

		-- select 'REV SEGMENTO' as X, * from #tmpLotes 
	End 
	------------------------- ASIGNACION DE FOLIOS DE REMISION		20180715.0240 

	--select 'DESPUES DE DESGLOZE' as Tipo, * from #tmpLotes 




	------------------------- ASIGNACION DE FOLIOS DE REMISION 
	--Select @EsRemision_Complemento as EsRemision_Complemento, @bEsAlmacen as bEsAlmacen, @TipoDeRemision as TipoDeRemision
	IF @EsRemision_Complemento = 1 
	Begin 
		
		--Select 'AQUI', @EsRemision_Complemento as EsRemision_Complemento, @bEsAlmacen as bEsAlmacen, @bEsFarmacia as bEsFarmacia, @TipoDeRemision as TipoDeRemision  
	

		--select 'SEPALA', * 
		--From vw_Farmacias 
		--Where IdEstado = @IdEstado  and IdFarmacia = @IdFarmacia  

		Set @IdFarmacia = REPLACE(@IdFarmacia, char(39), '') 
		Select top 1 @bEsAlmacen = 1 From #tmpFarmacias Where EsAlmacen = 1 --and IdEstado = @IdEstado  and IdFarmacia = @IdFarmacia  
		Select top 1 @bEsFarmacia = 1 From #tmpFarmacias Where EsAlmacen = 0 --and IdEstado = @IdEstado  and IdFarmacia = @IdFarmacia 
	
		Set @bEsAlmacen = IsNull(@bEsAlmacen, 0) 	 
		Set @bEsFarmacia = IsNull(@bEsFarmacia, 0) 
			
		
		If @bEsAlmacen = 1 
		Begin 
			If @TipoDeRemision = 4 
			   Set @TipoDeRemision = 5 

			If @TipoDeRemision = 6  
			   Set @TipoDeRemision = 8 


			Update L Set GrupoProceso  = @TipoDeRemision 
			From #tmpLotes L (NoLock) 
		End 


		If @bEsFarmacia = 1  
		Begin 
			If @TipoDeRemision = 1 
			   Set @TipoDeRemision = 3 

			If @TipoDeRemision = 2  
			   Set @TipoDeRemision = 7  


			Update L Set GrupoProceso  = @TipoDeRemision 
			From #tmpLotes L (NoLock) 
		End 
		
		--Select 'AQUI 2', @EsRemision_Complemento as EsRemision_Complemento, @bEsAlmacen as bEsAlmacen, @bEsFarmacia as bEsFarmacia, @TipoDeRemision as TipoDeRemision  


	End 


	------------------------ Generar folios de remisiones 
	--Select @Remisiones_x_Farmacia as Remisiones_x_Farmacia  
	Select 
		IDENTITY(int, 0, 1) as Consecutivo, 
		@iFolioRemision as FolioInicial, 
		cast('' as varchar(20)) as FolioRemision, 
		(case when @Remisiones_x_Farmacia = 1 then IdFarmacia else cast('' as varchar(4)) end) as IdFarmacia, 
		IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, GrupoProceso, IdGrupoDeRemisiones, GrupoDispensacion, PartidaGeneral, 
		Referencia_05, 
		EsControlado, EsAntibiotico, EsRefrigerado          
	Into #tmp_Remisiones 
	From #tmpLotes 
	Where Procesado = 1 
	Group By 
		--IdFarmacia, 
		(case when @Remisiones_x_Farmacia = 1 then IdFarmacia else cast('' as varchar(4)) end), 
		IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, GrupoProceso, IdGrupoDeRemisiones, GrupoDispensacion, PartidaGeneral, Referencia_05
		, EsControlado
		, EsAntibiotico 
		, EsRefrigerado   
	Order By 
		--IdFarmacia, 
		(case when @Remisiones_x_Farmacia = 1 then IdFarmacia else cast('' as varchar(4)) end), 
		IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, GrupoProceso, IdGrupoDeRemisiones, GrupoDispensacion, PartidaGeneral, Referencia_05
		, EsControlado desc 
		, EsAntibiotico desc 
		, EsRefrigerado desc         



	--------------- Remisiones generadas  
	Update R Set FolioRemision = right('0000000000000000' + cast(FolioInicial + Consecutivo as varchar(10)), 10) 
	From #tmp_Remisiones R 


	--Select 'Remisiones generadas' as Dato, * from #tmp_Remisiones R  


	Update L Set FolioRemision = R.FolioRemision 
	From #tmpLotes L (NoLock) 
	Inner Join #tmp_Remisiones R (NoLock) 
		On 
		( 
			L.IdFuenteFinanciamiento = R.IdFuenteFinanciamiento and L.IdFinanciamiento = R.IdFinanciamiento and L.IdDocumento = R.IdDocumento 
			--and L.IdFarmacia = R.IdFarmacia 
			and L.GrupoProceso = R.GrupoProceso and L.IdGrupoDeRemisiones = R.IdGrupoDeRemisiones and L.GrupoDispensacion = R.GrupoDispensacion
			and L.PartidaGeneral = R.PartidaGeneral and L.Referencia_05 = R.Referencia_05 
			and L.EsControlado = R.EsControlado and L.EsAntibiotico = R.EsAntibiotico and L.EsRefrigerado = R.EsRefrigerado 
		) 
	Where @Remisiones_x_Farmacia = 0 


	Update L Set FolioRemision = R.FolioRemision 
	From #tmpLotes L (NoLock) 
	Inner Join #tmp_Remisiones R (NoLock) 
		On 
		( 
			L.IdFuenteFinanciamiento = R.IdFuenteFinanciamiento and L.IdFinanciamiento = R.IdFinanciamiento and L.IdDocumento = R.IdDocumento 
			and L.IdFarmacia = R.IdFarmacia 
			and L.GrupoProceso = R.GrupoProceso and L.IdGrupoDeRemisiones = R.IdGrupoDeRemisiones and L.GrupoDispensacion = R.GrupoDispensacion
			and L.PartidaGeneral = R.PartidaGeneral and L.Referencia_05 = R.Referencia_05 
			and L.EsControlado = R.EsControlado and L.EsAntibiotico = R.EsAntibiotico and L.EsRefrigerado = R.EsRefrigerado 
		) 
	Where @Remisiones_x_Farmacia = 1 

	-- select * from #tmpLotes 
	------------------------- ASIGNACION DE FOLIOS DE REMISION 



	------------------------- PREPARAR INFORMACION DE ACUERDO A LAS PARTIDAS PRESUPUESTARIAS HGO
	Select IDENTITY(int, 1, 1) as Orden, 
		Referencia_02, Referencia_03, Referencia_05  
	Into #tmp_Remisiones__PartidasPresupuestarias 
	From #tmpLotes 
	Where Procesado = 1 
	Group by Referencia_02, Referencia_03, Referencia_05  

	-- select * from #tmp_Remisiones__PartidasPresupuestarias 

	------ select * from FACT_Remisiones_InformacionAdicional 
	--Delete From #tmp_Remisiones__PartidasPresupuestarias Where Orden > 1    ---- Borrado el 20181206.1340 
	------ select * from FACT_Remisiones_InformacionAdicional 

	--select * from #tmp_Remisiones__PartidasPresupuestarias 

	------------------------- PREPARAR INFORMACION DE ACUERDO A LAS PARTIDAS PRESUPUESTARIAS HGO


	Select @sMensaje = 'Se generaron los folios de remisión del ' + cast(min(FolioRemision) as varchar(10)) + ' al ' + cast(max(FolioRemision) as varchar(10)) 
	From #tmp_Remisiones (NoLock) 



	--Select IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, GrupoProceso, IdGrupoDeRemisiones, Referencia_01 
	--From #tmpLotes 
	--Where Procesado = 1 
	--Group By IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, GrupoProceso, IdGrupoDeRemisiones, Referencia_01  
	--Order By IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, GrupoProceso, IdGrupoDeRemisiones, Referencia_01     

	------------------------------- Generar los folios de remision 
	--------------------------------------------------------------------------------------------------------------------------------------------- 


	--------------------------------------------------------------------------------------------------------------------------------------------- 
	------------------------------- Calcular las cantidades distribuidas  
	Select IdEstado, IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, @Referencia_Beneficiario as IdBeneficiario, ClaveSSA, Referencia_01, Referencia_05, PartidaGeneral, 
		cast(0 as numeric(14,4)) as Importe, 
		cast(sum(Cantidad_Remisionada / Factor) as numeric(14,4)) as Cantidad 
	Into #tmpDistribucion 
	From #tmpLotes 
	Where Procesado = 1 -- and GrupoProceso > 0  
	Group By IdEstado, IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, ClaveSSA, Referencia_01, Referencia_05, PartidaGeneral    	


	Select IdEstado, IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, 
		cast(0 as numeric(14,4)) as Importe, 
		sum(Cantidad) as Cantidad  
	Into #tmpDistribucion_Concentrada  
	From #tmpDistribucion
	Group By IdEstado, IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA 


	--select count(*) from #tmpLotes 
	--Select 'ANTES DE GENERAR CONCENTRADOS', getdate(), count(*) as Registros  From #tmpLotes  
	--Delete From #tmpLotes 
	--select count(*) from #tmpLotes 


	Select  
		IdEstado, IdFarmacia, 
		-- IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, 	
		IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado, 	
		@TipoDeRemision  as TipoDeDocumento, 
		PartidaGeneral, 
		AfectaEstadisticaMontos, 

		--IsNull( cast(  (case when TasaIva = 0 Then ( dbo.fg_PRCS_Redondear(sum(Cantidad_Remisionada_Agrupada * PrecioClave), 2, 0) ) Else 0 End) as Numeric(14, 4) ), 0 ) as SubTotal_SinGrabar, 
		--IsNull( cast(  (case when TasaIva > 0 Then ( dbo.fg_PRCS_Redondear(sum(Cantidad_Remisionada_Agrupada * PrecioClave), 2, 0) ) Else 0 End) as Numeric(14, 4) ), 0 )  as SubTotal_Grabado, 			
		--IsNull( cast(  (case when TasaIva > 0 Then dbo.fg_PRCS_Redondear( ( sum(dbo.fg_PRCS_Redondear((Cantidad_Remisionada_Agrupada * PrecioClave), 2, 0)) * (TasaIva / 100.00) ), 2, 0) Else 0 End) as Numeric(14, 4) ), 0 )  as Iva, 
		--IsNull( cast(  (case when TasaIva > 0 Then dbo.fg_PRCS_Redondear( ( sum(dbo.fg_PRCS_Redondear((Cantidad_Remisionada_Agrupada * PrecioClave), 2, 0)) * (1 + (TasaIva / 100.00)) ), 2, 0) Else 0 End) as Numeric(14, 4) ), 0 )  as Importe, 

		IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as Cantidad_Agrupada, 
		IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as SubTotal_SinGrabar, 
		IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as SubTotal_Grabado, 
		IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as Iva, 
		IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as Importe,
			
		sum(Cantidad_Remisionada) as Cantidad 
	Into #tmpDistribucion_AsignacionImportes   
	From #tmpLotes 
	Where Procesado = 1 -- and GrupoProceso > 0  
		and 1 = 0 --- VALIDAR PARA COMPROBACION DE IMPORTES 
	Group By 
		IdEstado, IdFarmacia, 
		-- IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, 
		IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado, TasaIva, PartidaGeneral, AfectaEstadisticaMontos 

	Update L Set Importe = SubTotal_SinGrabar + SubTotal_Grabado + Iva   
	From #tmpDistribucion_AsignacionImportes L 


	Select IdEstado, IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, ClaveSSA, Procesado, 
		sum(Cantidad_Remisionada) as Cantidad, sum(Cantidad_Remisionada_Agrupada) as Cantidad_Agrupada   
	Into #tmpDistribucion__General 
	From #tmpLotes 
	-- Where GrupoProceso = 0  
	Group By IdEstado, IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, Procesado, ClaveSSA  

	--select '#tmpDistribucion__General' as Consulta,* from #tmpDistribucion__General 
	-- select top 1 '1' as Distribucion, * from #tmpDistribucion 

	------------------------------- Calcular las cantidades distribuidas  
	--------------------------------------------------------------------------------------------------------------------------------------------- 


------------------------------------------------------ MODIFICACION DEL PROCESO 


	-- select 'FACTOR 004', * from #tmpLotes     
	--Select 'ANTES DE GENERAR CONCENTRADOS  0002', getdate(), Procesado, count(*) as Registros From #tmpLotes group by Procesado  
	--delete from #tmpLotes 


	--Delete From #tmpLotes Where Procesado = 0 

	--select 'Numero de lotes' as Campo, count(*) as Regitros From #tmpLotes 
	--------------------------------------------------------------
	-- Se insertan los datos en la tabla final de procesamiento -- 
	-------------------------------------------------------------- 
	Select	
		--Top 10000 
			@Serie as Serie, @Folio as Folio, 
			FolioRemision, 
			IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, 
			PartidaGeneral, AfectaEstadistica, AfectaEstadisticaMontos, 
			Partida,   ------------- REVISAR AQUI 
			IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, 
			Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05,
			IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
			ClaveSSA, 
			ContenidoPaquete_ClaveSSA, 
			IdProducto, CodigoEAN, ClaveLote, SKU, 
			PrecioClave, PrecioClaveUnitario, TasaIva, 
			
			MultiploRelacion, 
			Factor, 
			
			cast( ( MultiploRelacion * (Cantidad_Remisionada / Factor ) ) as numeric(14,4) ) as CantidadVendida_Base, 
			--cast( 0.0000 as numeric(14, 4) ) as CantidadVendida_Base, 

			--Cantidad_Remisionada as CantidadVendida, 
			cast((Cantidad_Remisionada / Factor) as numeric(14,4) ) as CantidadVendida, 
			--cast( 0.0000 as numeric(14, 4) ) as CantidadVendida, 

			dbo.fg_PRCS_Redondear(Cantidad_Remisionada_Agrupada, 4, 2) as Cantidad_Agrupada, 
			IsNull( cast(  (case when TasaIva = 0 Then ( dbo.fg_PRCS_Redondear(Cantidad_Remisionada_Agrupada * PrecioClave, 2, 0) ) Else 0 End) as Numeric(14, 4) ), 0 ) as SubTotal_SinGrabar, 
			IsNull( cast(  (case when TasaIva > 0 Then ( dbo.fg_PRCS_Redondear(Cantidad_Remisionada_Agrupada * PrecioClave, 2, 0) ) Else 0 End) as Numeric(14, 4) ), 0 )  as SubTotal_Grabado, 			
			IsNull( cast(  (case when TasaIva > 0 Then dbo.fg_PRCS_Redondear( ( (Cantidad_Remisionada_Agrupada * PrecioClave) * (TasaIva / 100.00) ), 2, 0) Else 0 End) as Numeric(14, 4) ), 0 )  as Iva, 

			--cast( 0.0000 as numeric(14, 4) ) as Cantidad_Agrupada, 
			--cast( 0.0000 as numeric(14, 4) ) as SubTotal_SinGrabar, 
			--cast( 0.0000 as numeric(14, 4) ) as SubTotal_Grabado, 
			--cast( 0.0000 as numeric(14, 4) ) as Iva, 


			cast( 0.0000 as numeric(14, 4) ) as Importe, 
			cast( 0.0000 as numeric(14, 4) ) as Acumulado, 
			cast( 0 as bit ) as Facturable, 
			GrupoProceso as TipoDeRemision, GrupoDispensacion, 
			EsControlado, -- EsAntibiotico, EsRefrigerado,  
			Identity(int, 1, 1) as Keyx  
	Into #tmpLotesProceso  
	From #tmpLotes (NoLock)  
	Where Procesado = 1 
		and 1 = 1 
	Order By PrecioClave Desc, Cantidad_Remisionada Desc  


	--Select 'ANTES DE GENERAR CONCENTRADOS  0003', getdate(), count(*) as Registros From #tmpLotesProceso  
	--delete from #tmpLotes  


	--select 'Lotes puros' as DATO, * From #tmpLotes (NoLock)  
	----select 'Lotes procesados' as DATO, * From #tmpLotesProceso (NoLock)  


	If @EsRelacionFacturaPrevia = 1  
	Begin 
		--Select * from #tmpLotesProceso 

		Update L Set Serie = '', Folio = 0 
		From #tmpLotesProceso L 
		Where AfectaEstadisticaMontos = 0 

		--Select * from #tmpLotesProceso 
	End 

	--select 'MULTIPLO PROCESO' As Campo, * from #tmpLotesProceso 


	Select	FolioRemision, 
			IdEmpresa, IdEstado, @IdFarmaciaGenera as IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, 
			PartidaGeneral, Partida, 
			IdFuenteFinanciamiento, IdFinanciamiento, 
			IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
			ClaveSSA, 
			ContenidoPaquete_ClaveSSA, 
			IdProducto, CodigoEAN, ClaveLote, SKU, 
			PrecioClave, PrecioClaveUnitario, TasaIva, 

			Cantidad_Remisionada as CantidadVendida, 

			round( Cantidad_Remisionada_Agrupada, 4) as Cantidad_Agrupada, 
			IsNull( cast(  (case when TasaIva = 0 Then ( dbo.fg_PRCS_Redondear(Cantidad_Remisionada_Agrupada * PrecioClave, 2, 0) ) Else 0 End) as Numeric(14, 4) ), 0 ) as SubTotal_SinGrabar, 
			IsNull( cast(  (case when TasaIva > 0 Then ( dbo.fg_PRCS_Redondear(Cantidad_Remisionada_Agrupada * PrecioClave, 2, 0) ) Else 0 End) as Numeric(14, 4) ), 0 )  as SubTotal_Grabado, 			
			IsNull( cast(  (case when TasaIva > 0 Then dbo.fg_PRCS_Redondear( ( (Cantidad_Remisionada_Agrupada * PrecioClave) * (TasaIva / 100.00) ), 2, 0) Else 0 End) as Numeric(14, 4) ), 0 )  as Iva, 

			--IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as SubTotal_SinGrabar, 
			--IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as SubTotal_Grabado, 
			--IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as Iva, 

			IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as Importe, 
			IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as Acumulado, cast( 0 as bit ) as Facturable,
			Identity(int, 1, 1) as Keyx  
	Into #tmpLotes_NoProcesado   
	From #tmpLotes (NoLock)  
	Where Procesado = 0 
		and 1 = 0 ------ NO GENERAR TABLA 
	Order By PrecioClave Desc, Cantidad_Remisionada Desc  

	Update L Set Importe = SubTotal_SinGrabar + SubTotal_Grabado + Iva 
	From #tmpLotes_NoProcesado L 

	-------------------------------- 
	-- Se obtienen las cantidades --
	--------------------------------
	-- Se obtiene el Importe
	Update #tmpLotesProceso Set Importe = ( SubTotal_SinGrabar + SubTotal_Grabado + Iva ) 

	-- Se obtiene el Acumulado
	Update L Set Acumulado = (Select Sum( Importe ) From #tmpLotesProceso P(NoLock) Where L.Keyx >= P.Keyx )
	From #tmpLotesProceso L (NoLock) 



	--select 'AQUI' as XT, * from #tmpLotesProceso 


	----------------------------------------------------------------------------
	---- Se marcan los Facturables y se obtiene el monto que queda disponible --
	----------------------------------------------------------------------------
	---- Se marcan los codigos facturables. 
	Update #tmpLotesProceso Set Facturable = 1 Where Acumulado <= @iMontoFacturar  ------ MODO POR MONTOS 
	
	If @iMontoFacturar = 0 ------ MODO LIBRE 
	Begin 
	   Update #tmpLotesProceso Set Facturable = 1  
	End 
	 

	------------------------ Se obtiene el monto disponible.
	Select @iMontoDisponible = ( Select Top 1 ( @iMontoFacturar - Acumulado ) 
	From #tmpLotesProceso (NoLock) Where Facturable = 1 Order By Keyx Desc ) 

	------------------------ Se obtienen los totales de cada concepto.
	Select	@SubTotalSinGrabar = IsNull( Sum( SubTotal_SinGrabar), 0 ), @SubTotalGrabado = IsNull( Sum(SubTotal_Grabado), 0 ), 
			@Iva = IsNull( Sum(Iva), 0 ), @Total = IsNull( Sum(Importe), 0 )
	From #tmpLotesProceso (NoLock) 
	Where Facturable = 1 



	--------------------- Concentrado de Ventas-Lotes para aplicar cantidades remisionadas -- 20171025.1730 
	Select	
		-- FolioRemision, 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, 
		-- IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, 
		-- Referencia_01, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		PartidaGeneral, AfectaEstadistica, AfectaEstadisticaMontos, 
		ClaveSSA, 
		ContenidoPaquete_ClaveSSA, 
		IdProducto, CodigoEAN, ClaveLote, SKU, 
		0 as PrecioClave, 0 as PrecioClaveUnitario, 0 as TasaIva, 	
		Factor, 
		sum(CantidadVendida_Base) as CantidadVendida_Base, 
		sum(CantidadVendida / (Factor / 1.00)) as CantidadVendida_BaseAux, 
		sum(CantidadVendida) as CantidadVendida, 
		sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
		sum(SubTotal_SinGrabar) as SubTotal_SinGrabar, 
		sum(SubTotal_Grabado) as SubTotal_Grabado, 
		round(sum(Iva), 2, 1) as Iva, 
		sum(Importe) as Importe, 
		sum(Acumulado) as Acumulado,   							
		Facturable, TipoDeRemision, 
		0 as GrupoDispensacion, 
		Identity(int, 1, 1) as Keyx  
	Into #tmpLotesProceso___VentasLotes 
	From #tmpLotesProceso 
	-- where AfectaEstadistica = 1 
	Group by 
		-- FolioRemision, 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, 
		-- IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, 
		-- Referencia_01, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		PartidaGeneral, AfectaEstadistica, AfectaEstadisticaMontos, 
		ClaveSSA, 
		ContenidoPaquete_ClaveSSA, 
		IdProducto, CodigoEAN, ClaveLote, SKU, 
		-- PrecioClave, PrecioClaveUnitario, TasaIva, 
		Factor, 
		Facturable, TipoDeRemision 
		---- , GrupoDispensacion  ---- Jesús Díaz [ Excluir Grupo de dispensacion por aquellos renglones con mas de un GrupoDispensacion ] 



	--select 'Agrupamientio Ventas_Lotes' as Campo, * from #tmpLotesProceso___VentasLotes 
	--select * from #tmpLotesProceso 
	--------------------- Concentrado de Ventas-Lotes para aplicar cantidades remisionadas 


	
------------------------------------------------------------------------------------------------------------------------------ 
---- Se insertan los datos agrupados en la tabla final de procesamiento 
	Select	
			Serie, Folio, 
			FolioRemision, 
			Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 
			IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, IdPrograma, IdSubPrograma, 
			PartidaGeneral, 
			ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
			TasaIva, 
			
			sum(CantidadVendida) as CantidadVendida, 
			sum(Cantidad_Agrupada) as Cantidad_Agrupada, 

			sum(SubTotal_SinGrabar) as SubTotal_SinGrabar, 
			sum(SubTotal_Grabado) as SubTotal_Grabado, 
			sum(Iva) as Iva, 
			sum(Importe) as Importe, 

			IsNull( cast( 0.0000 as numeric(14, 4) ), 0 )  as Acumulado, cast( 1 as bit ) as Facturable, 
			TipoDeRemision, GrupoDispensacion, 
			EsControlado, 
			Identity(int, 1, 1) as Keyx  
	Into #tmpLotesProceso_Agrupado   
	From #tmpLotesProceso (NoLock)  
	Where Facturable = 1  
	Group by 
			Serie, Folio, 
			FolioRemision, 
			Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, 
			TipoDeRemision, GrupoDispensacion, 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 
			IdFuenteFinanciamiento, IdFinanciamiento,  IdDocumento, IdPrograma, IdSubPrograma, 
			PartidaGeneral, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, TasaIva, 
			EsControlado  
	Order By PrecioClave Desc, CantidadVendida Desc  



	-- Cantidad_Remisionada, Cantidad_Remisionada_Agrupada

	-- Se obtiene el Importe
	Update L Set 
		--Iva = round(Iva, 2, 1), 
		--Importe = ( SubTotal_SinGrabar + SubTotal_Grabado + round(Iva, 2, 1) ) 	
		Importe = ( SubTotal_SinGrabar + SubTotal_Grabado + Iva) 	
	From #tmpLotesProceso_Agrupado L (NoLock) 
	
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
	From #tmpLotesProceso (NoLock)
	Where Facturable = 1	

	
	--Select sum(CantidadVendida) as CantidadVendida from #tmpLotesProceso___VentasLotes  		
	--Select * from #tmpLotesProceso___VentasLotes 		
	--select * from #tmpLotesProceso 

	----------------------------------------- 
	---- Se obtiene el Folio de la Factura --
	-----------------------------------------
	If @bPrint_MarcaDeTiempo_Select = 1 Select 'INTERMEDIO 99' as Proceso, getdate() as MarcaDeTiempo 
	-- Set @Facturable = 0 


	If @Facturable = 0 
		Begin 
			If @MostrarResultado = 1 
			Begin 
				Select @FolioRemision = '', @sMensaje = 'No se encontro información para generar remisión'  
			End 
		End 
	Else    
		Begin  
			--Print 'REMISIONES GENERADAS' 
			If Not Exists ( Select * From FACT_Remisiones___GUID (NoLock) 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmaciaGenera and GUID = @Identificador ) 
			Begin 
				Insert Into FACT_Remisiones___GUID (  IdEmpresa, IdEstado, IdFarmaciaGenera, GUID, HostName, FechaRegistro ) 
				Select @IdEmpresa, @IdEstado, @IdFarmaciaGenera, @Identificador as GUID, HOST_NAME() as HostName, getdate() as FechaRegistro 
			End 

			Insert Into FACT_Remisiones 
			( 
				GUID, 
				IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeRemision, EsExcedente, IdPersonalRemision, IdPersonalValida, 
				IdFuenteFinanciamiento, IdFinanciamiento, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
				Observaciones, Status, Actualizado, TipoInsumo, OrigenInsumo, 
				EsFacturada, EsFacturable, 
				FechaInicial, FechaFinal, EsRelacionFacturaPrevia, Serie, Folio, EsRelacionMontos, TipoDeDispensacion, EsDeVales, PartidaGeneral, TipoDeInsumo_Clasificacion, 
				EsRelacionDocumento, FolioRelacionDocumento 
			) 
			Select	@Identificador as GUID, IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeRemision, @EsExcedente, @IdPersonalFactura, @IdPersonalFactura, 
					IdFuenteFinanciamiento, IdFinanciamiento, 
					--Sum(SubTotal_SinGrabar) as SubTotalSinGrabar, Sum(SubTotal_Grabado) as SubTotalGrabado, Sum(Iva) as Iva, Sum(Importe) as Total, 
					round(Sum(SubTotal_SinGrabar), 2, 1) as SubTotalSinGrabar, 
					round(Sum(SubTotal_Grabado), 2, 1)  as SubTotalGrabado, 
					round(Sum(Iva), 2, 1)  as Iva, 
					round(Sum(Importe), 2, 1)  as Total, 
					@Observaciones, 'A' as Status, 0 as Actualizado, @IdTipoProducto, @TipoDispensacion, 
					@EsFacturada, @EsFacturable, 
					@FechaInicial, @FechaFinal, @EsRelacionFacturaPrevia, 
					Serie, Folio, 
					@EsRelacionMontos, @TipoDispensacion_Venta, GrupoDispensacion as EsDeVales, 
					PartidaGeneral, EsControlado as TipoDeInsumo_Clasificacion, 
					@EsRelacionDocumentoPrevio, @FolioRelacionDocumento 
			From #tmpLotesProceso_Agrupado (NoLock)    
			Where Facturable = 1 
			Group By IdEmpresa, IdEstado, IdFarmaciaGenera, Serie, Folio, FolioRemision, TipoDeRemision, IdFuenteFinanciamiento, IdFinanciamiento, GrupoDispensacion, PartidaGeneral, EsControlado   


			

			------------------ Proceso exclusivo para la Exclusión de Referencias SSH Primer Nivel
			If @AsignarReferencias = 0 
			Begin 
				Update L Set Referencia_01 = '' 
				From #tmpLotesProceso L (NoLock) 
				Where Facturable = 1 
			End 



			-- Se inserta el Detalle de la Factura
			Insert Into FACT_Remisiones_Detalles ( 
					IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento, 
					IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
					PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
					TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe, Referencia_01, SKU ) 
			Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
					ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
					CantidadVendida, 
					Cantidad_Agrupada as CantidadVendida,  
					TasaIva, 
					SubTotal_SinGrabar, 
					SubTotal_Grabado,  
					Iva, 
					Importe, 
					--round(SubTotal_SinGrabar, 2, 1), 
					--round(SubTotal_Grabado, 2, 1), 
					--round(Iva, 2, 0), 
					--round(Importe, 2, 1), 
					Referencia_01, 
					SKU 
			From #tmpLotesProceso (NoLock) 
			Where Facturable = 1  
			Order By Keyx


			----Select	'DETALLES' as Campo, 
			----		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 					
			----		FolioVenta, 
			----		FolioRemision, 
			----		IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
			----		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
			----		Partida, 
			----		Referencia_01  
			----From #tmpLotesProceso (NoLock) 
			----Where Facturable = 1  
			----Order By Keyx




			-- Se inserta el Concentrado de la Factura 
			Insert Into FACT_Remisiones_Concentrado 
			( 
					IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento, 
					IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, 
					PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
					TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe 
			) 
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


			---- Se inserta el Resumen de la Factura
			Insert Into FACT_Remisiones_Resumen 
			( 
					IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, 
					IdFuenteFinanciamiento, IdFinanciamiento,  
					IdPrograma,	IdSubPrograma, ClaveSSA, 
					PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, 
					TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe 
			) 
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

			
			--------------------------- CONTROL DE DOCUMENTOS PRESUPUESTALES 
			If @Aplicar_ImporteDocumentos = 1  
			Begin 
				Insert Into FACT_Remisiones_Documentos (  
					IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
				Select  
					IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, 
					Sum(SubTotal_SinGrabar) as SubTotal_SinGrabar, Sum(SubTotal_Grabado) as SubTotal_Grabado, Sum(Iva) as Iva, Sum(Importe) as Importe 
				From #tmpLotesProceso_Agrupado (NoLock) 
				-- Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And FolioRemision = @FolioRemision
				Group By IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento 
			End 

			
			--------------------------- INFORMACION ADICIONAL  
			If Exists ( Select * From #tmp_Remisiones__PartidasPresupuestarias (NoLock) ) 
				Begin 
					Insert Into FACT_Remisiones_InformacionAdicional 
					( 
							IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Observaciones, Referencia_05  
					) 
					Select	L.IdEmpresa, L.IdEstado, L.IdFarmaciaGenera, L.FolioRemision, 
							-- (P.Referencia_02 + ' ' + P.Referencia_03 + ' ' + 'PERIODO DEL  ' + @FechaInicial + '  AL  ' + @FechaFinal ) As Observaciones  
							(L.Referencia_02 + ' ' + L.Referencia_03) As Observaciones, Referencia_05  
					From #tmpLotesProceso_Agrupado L (NoLock)		---,  #tmp_Remisiones__PartidasPresupuestarias P (NoLock)  20181206.1340 
					Where Facturable = 1 
					Group By L.IdEmpresa, L.IdEstado, L.IdFarmaciaGenera, L.FolioRemision, L.Referencia_02, L.Referencia_03, L.Referencia_05   
				End 
			Else 
				Begin 
					Insert Into FACT_Remisiones_InformacionAdicional ( 
							IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Observaciones ) 
					Select	IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, ('PERIODO DEL  ' + @FechaInicial + '  AL  ' + @FechaFinal ) As Observaciones    
					From #tmpLotesProceso_Agrupado (NoLock)    
					Where Facturable = 1 
					Group By IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
				End 


			--------------------------- CONTROL DE VENTAS DE ALMACENES    
			If @Referencia_Beneficiario <> '' or @Referencia_BeneficiarioNombre <> '' 
			Begin 
				Insert Into FACT_Remisiones_InformacionAdicional_Almacenes (  IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeBeneficiario, Beneficiario, NombreBeneficiario ) 
				Select distinct IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, @TipoDeBeneficiario as TipoDeBeneficiario, 
					@Referencia_Beneficiario As Beneficiario, @Referencia_BeneficiarioNombre as NombreBeneficiario
				From #tmpLotesProceso_Agrupado  
			End 


			----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 
			If @EsRelacionFacturaPrevia = 1  
			Begin 
				If @EsRelacionMontos = 1  
				Begin 
					Set @EsRelacionMontos = 1 

					Update F Set Importe_Distribuido = Importe_Distribuido + IsNull 
					(
						( Select sum(Importe) From #tmpDistribucion_AsignacionImportes Where AfectaEstadisticaMontos = 1 and Importe > 0 ) , 0
					) 
					From FACT_Remisiones__RelacionFacturas_x_Importes F (NoLock) 
					Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( F.FolioRelacion = E.FolioRelacion ) 
					Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio 
				End 


				If @EsRelacionMontos = 0 
				Begin 

					Update F Set Cantidad_Distribuida = F.Cantidad_Distribuida + D.Cantidad  
					From FACT_Remisiones__RelacionFacturas F (NoLock) 
					Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( F.FolioRelacion = E.FolioRelacion ) 
 					Inner Join 
					( 
						Select ClaveSSA, sum(Cantidad) as Cantidad 
						From #tmpDistribucion X (NoLock) 
						Group by ClaveSSA 
					) D On ( F.ClaveSSA = D.ClaveSSA ) 
					Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio   


					Update F Set Cantidad_Distribuida = F.Cantidad_Distribuida + D.Cantidad  
					From FACT_Remisiones__RelacionFacturas_x_Farmacia F (NoLock) 
					Inner Join FACT_Remisiones__RelacionFacturas_Enc E (NoLock) On ( F.FolioRelacion = E.FolioRelacion ) 
 					Inner Join #tmpDistribucion D (NoLock) 
						On ( -- F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia and 
							 E.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and E.IdFinanciamiento = D.IdFinanciamiento and F.ClaveSSA = D.ClaveSSA ) 
					Where @EsRelacionFacturaPrevia_x_Farmacia = 1 and 
						E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.Serie = @Serie and E.Folio = @Folio and IdFarmacia_Relacionada = @IdFarmacia  

				End 
			End 

			If @EsRelacionDocumentoPrevio = 1 
			Begin 
				Update F Set Cantidad_Distribuida = F.Cantidad_Distribuida + D.Cantidad  
				From FACT_Remisiones__RelacionDocumentos F (NoLock) 
				Inner Join FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) On ( F.FolioRelacion = E.FolioRelacion ) 
 				Inner Join 
				( 
					Select ClaveSSA, sum(Cantidad) as Cantidad 
					From #tmpDistribucion X (NoLock) 
					Group by ClaveSSA 
				) D On ( F.ClaveSSA = D.ClaveSSA ) 
				Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmaciaGenera and E.FolioRelacion = @FolioRelacionDocumento
			End
			----------------------------------	Validar el tipo de proceso de remisionado, Ventas por Anticipado 


			---- select 'ANTES ACTUALIZAR VENTAS-LOTES', * from #tmpLotesProceso___VentasLotes 


			If @TipoDeRemision_Auxiliar = 1  ---- INSUMO --------- ó SERVICIO 
				Begin 	

					----Select L.*, 
					----	EnRemision_Insumo = ( case when L.CantidadVendida = (L.CantidadRemision_Insumo + V.CantidadVendida_Base) then 1 else 0 end ),  
					----	CantidadRemision_Insumo = (L.CantidadRemision_Insumo + V.CantidadVendida_Base)  
					----From VentasDet_Lotes L (NoLock) 
					----Inner Join #tmpLotesProceso___VentasLotes V (NoLock)  ---- #tmpLotesProceso --Cambio 20171026.1725  H's 
					----	On ( 
					----			L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
					----			And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote and L.SKU = V.SKU 
					----		    --And V.PartidaGeneral in ( 0, 1 ) 
					----			And V.AfectaEstadistica = 1 
					----		) 
					----Where @EsRemision_Complemento = 0 and V.Facturable = 1 and L.RemisionFinalizada = 0  ----and L.EnRemision_Insumo = 0  

					-----------------------------------------------------------
					-- Se marcan las ventas que se facturará insumos  
					Update L Set 
						-- L.EnRemision_Insumo = ( case when L.CantidadVendida = (L.CantidadRemision_Insumo + V.CantidadVendida) then 1 else 0 end ),  
						-- CantidadRemision_Insumo = (L.CantidadRemision_Insumo + V.CantidadVendida)  
						L.EnRemision_Insumo = ( case when L.CantidadVendida = (L.CantidadRemision_Insumo + V.CantidadVendida_Base) then 1 else 0 end ),  
						CantidadRemision_Insumo = (L.CantidadRemision_Insumo + V.CantidadVendida_Base)  
					From VentasDet_Lotes L (NoLock) 
					Inner Join #tmpLotesProceso___VentasLotes V (NoLock)  ---- #tmpLotesProceso --Cambio 20171026.1725  H's 
						On ( 
								L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
								And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote and L.SKU = V.SKU 
							    --And V.PartidaGeneral in ( 0, 1 ) 
								And V.AfectaEstadistica = 1 
							) 
					Where @EsRemision_Complemento = 0 and V.Facturable = 1 and L.RemisionFinalizada = 0  ----and L.EnRemision_Insumo = 0  


					-----------------------------------------------------------
					-- Se marcan las ventas que se facturará insumos ( Incrementos | Diferenciador ) 
					Update L Set 
						-- L.EnRemision_Insumo = ( case when L.CantidadVendida = (L.CantidadRemision_Insumo + V.CantidadVendida) then 1 else 0 end ),  
						-- CantidadRemision_Insumo = (L.CantidadRemision_Insumo + V.CantidadVendida)  
						L.EnRemision_Insumo = ( case when L.CantidadVendida = (L.CantidadRemision_Insumo + V.CantidadVendida_Base) then 1 else 0 end ),  
						CantidadRemision_Insumo = (L.CantidadRemision_Insumo + V.CantidadVendida_Base)  
					From FACT_Incremento___VentasDet_Lotes L (NoLock) 
					Inner Join #tmpLotesProceso___VentasLotes V (NoLock)  ---- #tmpLotesProceso --Cambio 20171026.1725  H's 
						On ( 
								L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
								And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote and L.SKU = V.SKU  
							    --And V.PartidaGeneral in ( 0, 1 ) 
								And V.AfectaEstadistica = 1 
							) 
					Where @EsRemision_Complemento = 1 and V.Facturable = 1 and L.RemisionFinalizada = 0  ----and L.EnRemision_Insumo = 0  


					---------------- Actualizar las cantidades distribuidas 
					Update F Set CantidadAsignada = F.CantidadAsignada + D.Cantidad 
					From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F (NoLock) 
					Inner Join #tmpDistribucion D (NoLock) 
						On  ( F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia and 
							  F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and F.IdFinanciamiento = D.IdFinanciamiento and F.ClaveSSA = D.ClaveSSA 
							  and F.Referencia_01 = D.Referencia_01 and F.Referencia_05 = D.Referencia_05 ---- 20181206.1328 
							  And D.PartidaGeneral in ( 0, 1 ) 
							  -- And V.AfectaEstadistica = 1 
							) 
					Where @TipoDispensacion = 0 


					-- select @NivelInformacion_Remision as NivelInformacion_Remision  

					Update F Set CantidadAsignada = F.CantidadAsignada + D.Cantidad 
					From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario F (NoLock) 
					Inner Join #tmpDistribucion D (NoLock) 
						On ( F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia and 
							 F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and F.IdFinanciamiento = D.IdFinanciamiento 
							 and F.IdBeneficiario = @Referencia_Beneficiario and F.ClaveSSA = D.ClaveSSA and F.Referencia_01 = D.Referencia_01 and F.Referencia_05 = D.Referencia_05 
							 And D.PartidaGeneral in ( 0, 1 )  
							 ) 
					Where @TipoDispensacion = 0 and @NivelInformacion_Remision = 3 
					 

				End 

			If @TipoDeRemision_Auxiliar = 2  ---- SERVICIO --------- ó INSUMO 
				Begin 
					-----------------------------------------------------------
					-- Se marcan las ventas que se facturará administración 
					Update L Set 
						-- L.EnRemision_Admon = ( case when L.CantidadVendida = (L.CantidadRemision_Admon + V.CantidadVendida) then 1 else 0 end ),  
						-- CantidadRemision_Admon = (L.CantidadRemision_Admon + V.CantidadVendida)  
						L.EnRemision_Admon = ( case when L.CantidadVendida = (L.CantidadRemision_Admon + V.CantidadVendida_Base) then 1 else 0 end ),  
						CantidadRemision_Admon = (L.CantidadRemision_Admon + V.CantidadVendida_Base)  
					From VentasDet_Lotes L (NoLock) 
					Inner Join #tmpLotesProceso___VentasLotes V (NoLock)  ---- #tmpLotesProceso --Cambio 20171026.1725  H's 
						On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
							And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote and L.SKU = V.SKU 
							--And V.PartidaGeneral in ( 0, 1 ) 
							And V.AfectaEstadistica = 1 
							) 
					Where @EsRemision_Complemento = 0 and V.Facturable = 1 and L.RemisionFinalizada = 0  ----and L.EnRemision_Admon = 0 		


					-- Se marcan las ventas que se facturará insumos ( Incrementos | Diferenciador ) 
					Update L Set 
						-- L.EnRemision_Admon = ( case when L.CantidadVendida = (L.CantidadRemision_Admon + V.CantidadVendida) then 1 else 0 end ),  
						-- CantidadRemision_Admon = (L.CantidadRemision_Admon + V.CantidadVendida)  
						L.EnRemision_Admon = ( case when L.CantidadVendida = (L.CantidadRemision_Admon + V.CantidadVendida_Base) then 1 else 0 end ),  
						CantidadRemision_Admon = (L.CantidadRemision_Admon + V.CantidadVendida_Base)  
					From FACT_Incremento___VentasDet_Lotes L (NoLock) 
					Inner Join #tmpLotesProceso___VentasLotes V (NoLock)  ---- #tmpLotesProceso --Cambio 20171026.1725  H's 
						On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
							And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote and L.SKU = V.SKU 
							--And V.PartidaGeneral in ( 0, 1 ) 
							And V.AfectaEstadistica = 1 
							) 
					Where @EsRemision_Complemento = 1 and V.Facturable = 1 and L.RemisionFinalizada = 0  ----and L.EnRemision_Admon = 0 		


					------------------ Actualizar las cantidades distribuidas 
					Update F Set CantidadAsignada = F.CantidadAsignada + D.Cantidad  
					From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia F (NoLock) 
					Inner Join #tmpDistribucion D (NoLock) 
						On ( F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia and 
							 F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and F.IdFinanciamiento = D.IdFinanciamiento and F.ClaveSSA = D.ClaveSSA 
							 and F.Referencia_01 = D.Referencia_01 and F.Referencia_05 = D.Referencia_05 ---- 20181206.1328 
							  And D.PartidaGeneral in ( 0, 1 ) 
							  -- And V.AfectaEstadistica = 1 
							 ) 
					Where @TipoDispensacion = 0 

					Update F Set CantidadAsignada = F.CantidadAsignada + D.Cantidad 
					From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario F (NoLock) 
					Inner Join #tmpDistribucion D (NoLock) 
						On ( F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia and 
							 F.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and F.IdFinanciamiento = D.IdFinanciamiento 
							 and F.IdBeneficiario = @Referencia_Beneficiario and F.ClaveSSA = D.ClaveSSA and F.Referencia_01 = D.Referencia_01 and F.Referencia_05 = D.Referencia_05 
							 And D.PartidaGeneral in ( 0, 1 ) 
							 ) 
					Where @TipoDispensacion = 0 and @NivelInformacion_Remision = 3 


				End 

			-----------------------------------------------------------
			-- Se marcan los lotes que ya tienen remisión de Insumos y Administración 
			Update L Set L.RemisionFinalizada = 1 
			From VentasDet_Lotes L (NoLock) 
			Inner Join #tmpLotesProceso___VentasLotes V (NoLock)  ---- #tmpLotesProceso --Cambio 20171026.1725  H's 
				On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
					And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote and L.SKU = V.SKU ) 
			-- Where L.EnRemision_Insumo = 1 and L.EnRemision_Admon = 1	and L.RemisionFinalizada = 0 	
			Where @EsRemision_Complemento = 0 and -- L.EnRemision_Insumo = 1 and L.EnRemision_Admon = 1 and 
				L.RemisionFinalizada = 0 and ( L.CantidadRemision_Insumo = L.CantidadVendida and L.CantidadRemision_Admon = L.CantidadVendida ) 	

						
		End	

	---------------------------- 
	-- Se devuelven los datos -- 
	---------------------------- 
	If @MostrarResultado = 1 
	Begin 
		Select @Facturable as Remisionado, @FolioRemision as Folio, @sMensaje as Mensaje , IsNull(@iMontoDisponible, 0) as MontoDisponible 
	End 


	------------------ VALIDACION FINAL 
	----------- Omitir esta parte del proceso, es innecesario 20220412.2120 
	/* 
	if Exists ( Select top 1 * From #tmpDistribucion__General Where Procesado = 0 ) 
	Begin 

		-- drop table RptRemision_NoProcesado 
		If not exists ( Select Name From sysobjects (noLock) Where Name like 'RptRemision_NoProcesado' and xType = 'U' )  
		Begin 
			Select top 0 
				0 as TipoProceso, 
				0 as TipoDeRemision, 0 as TipoDispensacion, 0 as IdTipoProducto, 
				cast('' as varchar(10)) as IdEstado, cast('' as varchar(10)) as IdFarmacia, 
				cast('' as varchar(10)) as IdFuenteFinanciamiento, cast('' as varchar(10)) as IdFinanciamiento, 
				cast('' as varchar(20)) as ClaveSSA, 0 as Procesado, 0 as Cantidad  		 
			Into RptRemision_NoProcesado 
		End 

		Insert Into RptRemision_NoProcesado ( TipoProceso, TipoDeRemision, TipoDispensacion, IdTipoProducto, IdEstado, IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Procesado, Cantidad )
		Select 
			1 as TipoProceso, 
			@TipoDeRemision as TipoDeRemision, @TipoDispensacion as TipoDispensacion, @IdTipoProducto as IdTipoProducto, 
			IdEstado, IdFarmacia, 
			IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Procesado, Cantidad  
		From #tmpDistribucion__General Where Procesado = 0 


		-- drop table RptRemision_NoProcesado__Detallado 
		If not exists ( Select Name From sysobjects (noLock) Where Name like 'RptRemision_NoProcesado__Detallado' and xType = 'U' )  
		Begin 
			Select Top 0 
				0 as TipoProceso, 
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
		( 
			TipoProceso, IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
			IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
			Cantidad, CantidadVendida, TasaIva, SubTotal_SinGrabar, SubTotal_Grabado, Iva, Importe --, SKU
		) 
		Select 
			1 as TipoProceso,
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
			IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
			ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, PrecioClaveUnitario, 
			CantidadVendida as Cantidad, Cantidad_Agrupada as CantidadVendida,  
			TasaIva, SubTotal_SinGrabar, SubTotal_Grabado, 
			Iva, Importe 	 
		From #tmpLotes_NoProcesado (NoLock) 
		Order By Keyx

	End 
	*/ 


	--------------------------------------------------------------------- LIBERAR LAS TABLAS TEMMPORALES	

	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_ListaClaves_VentasPrevias%'  and xType = 'U' ) Drop Table tempdb..#tmp_ListaClaves_VentasPrevias   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpProgramasDeAtencion%'  and xType = 'U' ) Drop Table tempdb..#tmpProgramasDeAtencion  	   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#vw_Productos_CodigoEAN%'  and xType = 'U' ) Drop Table tempdb..#vw_Productos_CodigoEAN 
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#vw_Claves_A_Procesar%'  and xType = 'U' ) Drop Table tempdb..#vw_Claves_A_Procesar	   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpFarmacias%'  and xType = 'U' ) Drop Table tempdb..#tmpFarmacias	   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpVentas%'  and xType = 'U' ) Drop Table tempdb..#tmpVentas	  
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotes%'  and xType = 'U' ) Drop Table tempdb..#tmpLotes 	   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp____VentasDet_Lotes%'  and xType = 'U' ) Drop Table tempdb..#tmp____VentasDet_Lotes 	   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_FF%'  and xType = 'U' ) Drop Table tempdb..#tmp_FF   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA%'  and xType = 'U' ) Drop Table tempdb..#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze%'  and xType = 'U' ) Drop Table tempdb..#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Desgloze   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_Claves_Concentrado__Excedentes%'  and xType = 'U' ) Drop Table tempdb..#tmp_Claves_Concentrado__Excedentes   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Excedente%'  and xType = 'U' ) Drop Table tempdb..#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Excedente   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor%'  and xType = 'U' ) Drop Table tempdb..#FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Referencia_Mayor
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_Claves_Concentrado__Consignacion%'  and xType = 'U' ) Drop Table tempdb..#tmp_Claves_Concentrado__Consignacion	   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_Claves_Concentrado%'  and xType = 'U' ) Drop Table tempdb..#tmp_Claves_Concentrado   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_Asignacion_Cantidades%'  and xType = 'U' ) Drop Table tempdb..#tmp_Asignacion_Cantidades   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_FF_ListaDocumentos%'  and xType = 'U' ) Drop Table tempdb..#tmp_FF_ListaDocumentos   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_FF_Documentos%'  and xType = 'U' ) Drop Table tempdb..#tmp_FF_Documentos   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_AsignacionImporte_Documentos%'  and xType = 'U' ) Drop Table tempdb..#tmp_AsignacionImporte_Documentos    
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_FF_ListaDocumentos__AUX%'  and xType = 'U' ) Drop Table tempdb..#tmp_FF_ListaDocumentos__AUX   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_Asignacion_x_Importes%'  and xType = 'U' ) Drop Table tempdb..#tmp_Asignacion_x_Importes   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#FACT_ClavesSSA_Precios__Excepciones%'  and xType = 'U' ) Drop Table tempdb..#FACT_ClavesSSA_Precios__Excepciones    
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotes___Excepcion_01%'  and xType = 'U' ) Drop Table tempdb..#tmpLotes___Excepcion_01 
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotes___Excepcion_02%'  and xType = 'U' ) Drop Table tempdb..#tmpLotes___Excepcion_02 		  
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotes___AdmonSegmentada%'  and xType = 'U' ) Drop Table tempdb..#tmpLotes___AdmonSegmentada    
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_Remisiones%'  and xType = 'U' ) Drop Table tempdb..#tmp_Remisiones    
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmp_Remisiones__PartidasPresupuestarias%'  and xType = 'U' ) Drop Table tempdb..#tmp_Remisiones__PartidasPresupuestarias   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpDistribucion%'  and xType = 'U' ) Drop Table tempdb..#tmpDistribucion   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpDistribucion_AsignacionImportes%'  and xType = 'U' ) Drop Table tempdb..#tmpDistribucion_AsignacionImportes    
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpDistribucion__General%'  and xType = 'U' ) Drop Table tempdb..#tmpDistribucion__General   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotesProceso%'  and xType = 'U' ) Drop Table tempdb..#tmpLotesProceso   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotes_NoProcesado%'  and xType = 'U' ) Drop Table tempdb..#tmpLotes_NoProcesado	   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotesProceso___VentasLotes%'  and xType = 'U' ) Drop Table tempdb..#tmpLotesProceso___VentasLotes   
	----If Exists ( select * From tempdb..sysobjects (Nolock) Where name like '#tmpLotesProceso_Agrupado%'  and xType = 'U' ) Drop Table tempdb..#tmpLotesProceso_Agrupado   

	--------------------------------------------------------------------- LIBERAR LAS TABLAS TEMMPORALES	



End 
Go--#SQL 


