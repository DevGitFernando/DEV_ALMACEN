

------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal 
Go--#SQL 
    
Create Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal 
( 	
	-- @TipoProcesoRemision int = 0,   --- 1 => General | 2 ==> Farmacia FF | Farmacia incrementos FF ==> 3 
	@NivelInformacion_Remision int = 2,   --- 1 => General (Primer nivel de informacion) | 2 ==> Farmacia FF (Segundo nivel de informacion) | 3 ==> Ventas directas por Jurisdiccion 

	@ProcesarParcialidades int = 0,		 ---- ====> Exclusivo SSH Primer Nivel 
	@ProcesarCantidadesExcedentes int = 0,    

	@AsignarReferencias int = 1, 
		
	@Remision_01_General bit = 0, @Remision_02_Farmacia_FF bit = 0, @Remision_03_Farmacia_FF_Incremento bit = 0, 

	@Procesar_Producto bit = 0, @Procesar_Servicio bit = 0, @Procesar_Servicio_Consigna bit = 0, 
	@Procesar_Medicamento bit = 0, @Procesar_MaterialDeCuracion bit = 0, 

	@Procesar_Venta bit = 0, @Procesar_Consigna bit = 0, 

	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', 
	@IdFarmaciaGenera varchar(4) = '', @TipoDeRemision smallint = 0, 
	@IdFarmacia varchar(max) = [ ], 
		
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@Lista__IdClienteIdSubCliente varchar(max) = [  ], 
 
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2001-01-01', @FechaFinal varchar(10) = '2001-12-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '00', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = '',  
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(max) = [  ], 
	@FechaDeRevision int = 2, 
	@FoliosVenta varchar(max) = [  ], 
	@TipoDeBeneficiario int = 0, -- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén>
	@Aplicar_ImporteDocumentos int = 0, 
	@EsProgramasEspeciales int = 0, 
	@Referencia_Beneficiario varchar(100) = '', @Referencia_BeneficiarioNombre varchar(200) = '', 
	@Remision_General int = 0, @ClaveSSA_ListaExclusion varchar(max) = [ ], 

	@EsRelacionFacturaPrevia bit = 0, 
	@FacturaPreviaEnCajas int = 0, 
	@Serie varchar(10) = '', @Folio int = 0, @EsRelacionMontos bit = 0, @Accion varchar(100) = '', 
	@Procesar_SoloClavesReferenciaRemisiones bit = 0, 
	@ExcluirCantidadesConDecimales bit = 0,  
	@Separar__Venta_y_Vales bit = 0, 
	@TipoDispensacion_Venta int = 0,   -- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	@Criterio_FiltroFecha___Vales int = 1,		-- 1 ==> Fecha de emision | 2 ==> Fecha de registro de vale canjeado ( Fecha registro de Venta ) 
	@EsRemision_Complemento bit = 0,     -- 0 == > Remisiones normales | 1 ==> Remisiones de Incremento ó Diferenciador 
	@MostrarResultado bit = 0, 
	@Remisiones_x_Farmacia bit = 1, 
	
	@EsRelacionDocumentoPrevio bit = 0, 
	@FolioRelacionDocumento varchar(6) = '' 
)  
With Encryption 
As 
Begin 
Set NoCount On 

		-- select  @Accion as spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal  

		If @Remision_01_General = 1 
		Begin 
			Set @Remision_01_General = 0 
			--Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso @TipoProcesoRemision = 1, 
			--	@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna, 
			--	@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
			--	@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado , 
			--	@IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, @IdFarmacia = @IdFarmacia, 
			--	@IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
			--	@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
			--	@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
			--	@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, 
			--	@iMontoFacturar = @iMontoFacturar,  @IdPersonalFactura = @IdPersonalFactura, 
			--	@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, 
			--	@EsExcedente = @EsExcedente, @Identificador = @Identificador, 
			--	@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
			--	@ClaveSSA = @ClaveSSA, 
			--	@FechaDeRevision = @FechaDeRevision    
		End	

		If @Remision_02_Farmacia_FF = 1   
		Begin 
			Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso 
				@NivelInformacion_Remision = @NivelInformacion_Remision,  @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes, 	
				@AsignarReferencias = @AsignarReferencias, 
				@TipoProcesoRemision = 2, 
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
				@FechaDeRevision = @FechaDeRevision, @FoliosVenta = @FoliosVenta, 
				@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
				@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
				@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
				@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
				@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
				@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
				@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
				@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento 
		End


		If @Remision_01_General = 1   
		Begin 
			Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso 
				@NivelInformacion_Remision = @NivelInformacion_Remision,  @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes, 
				@AsignarReferencias = @AsignarReferencias, 
				@TipoProcesoRemision = 3, 
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
				@FechaDeRevision = @FechaDeRevision, @FoliosVenta = @FoliosVenta, 
				@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
				@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
				@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
				@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
				@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
				@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
				@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
				@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento 
		End 


End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_x_TipoDeProceso' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso 
Go--#SQL 
    
Create Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso 
( 
	@NivelInformacion_Remision int = 2,   --- 1 => General (Primer nivel de informacion) | 2 ==> Farmacia FF (Segundo nivel de informacion) | 3 ==> Ventas directas por Jurisdiccion 
	@TipoProcesoRemision int = 1,   --- 1 => General | 2 ==> Farmacia FF | Farmacia incrementos FF ==> 3 
	@ProcesarParcialidades int = 0,		 ---- ====> Exclusivo SSH Primer Nivel 
	@ProcesarCantidadesExcedentes int = 0,    
	@AsignarReferencias int = 1, 

	@Procesar_Producto bit = 0, @Procesar_Servicio bit = 0, @Procesar_Servicio_Consigna bit = 0, 
	@Procesar_Medicamento bit = 0, @Procesar_MaterialDeCuracion bit = 0, 

	@Procesar_Venta bit = 0, @Procesar_Consigna bit = 0, 

	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', 
	@IdFarmaciaGenera varchar(4) = '', @TipoDeRemision smallint = 0, 
	@IdFarmacia varchar(max) = [ ], 
		
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@Lista__IdClienteIdSubCliente varchar(max) = [  ], 
 
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2001-01-01', @FechaFinal varchar(10) = '2001-12-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '00', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = '',  
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(max) = [  ], 
	@FechaDeRevision int = 2, 
	@FoliosVenta varchar(max) = [  ], 
	@TipoDeBeneficiario int = 0, -- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén>
	@Aplicar_ImporteDocumentos int = 0, 
	@EsProgramasEspeciales int = 0, 
	@Referencia_Beneficiario varchar(100) = '', @Referencia_BeneficiarioNombre varchar(200) = '', 
	@Remision_General int = 0, @ClaveSSA_ListaExclusion varchar(max) = [ ], 

	@EsRelacionFacturaPrevia bit = 0, 
	@FacturaPreviaEnCajas int = 0, 
	@Serie varchar(10) = '', @Folio int = 0, @EsRelacionMontos bit = 0, @Accion varchar(100) = '', 
	@Procesar_SoloClavesReferenciaRemisiones bit = 0, 
	@ExcluirCantidadesConDecimales bit = 0,  
	@Separar__Venta_y_Vales bit = 0, 	
	@TipoDispensacion_Venta int = 0,   -- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	@Criterio_FiltroFecha___Vales int = 1,		-- 1 ==> Fecha de emision | 2 ==> Fecha de registro de vale canjeado ( Fecha registro de Venta ) 
	@EsRemision_Complemento bit = 0,     -- 0 == > Remisiones normales | 1 ==> Remisiones de Incremento ó Diferenciador 
	@MostrarResultado bit = 0, 
	@Remisiones_x_Farmacia bit = 1, 

	@EsRelacionDocumentoPrevio bit = 0, 
	@FolioRelacionDocumento varchar(6) = '' 	
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If @TipoProcesoRemision = 1  
	Begin 
		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia 
			@NivelInformacion_Remision = @NivelInformacion_Remision,  @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
			@AsignarReferencias = @AsignarReferencias, 
			@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna, 
			@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
			@Procesar_Venta = @Procesar_Venta, @Procesar_Consigna = @Procesar_Consigna, 
			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
			@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente,  
			@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, @Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
			@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
			@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, 
			@Identificador = @Identificador, @TipoDispensacion = @TipoDispensacion, @ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, @FoliosVenta = @FoliosVenta, 
			@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
			@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
			@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
			@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
			@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
			@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
			@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
			@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
			@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  
	End 

	If @TipoProcesoRemision = 2 
	Begin 
		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia 
			@NivelInformacion_Remision = @NivelInformacion_Remision, --@ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,   
			--@AsignarReferencias = @AsignarReferencias, 
			@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna,  
			@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
			@Procesar_Venta = @Procesar_Venta, @Procesar_Consigna = @Procesar_Consigna, 
			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
			@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
			@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, @Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
			@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
			@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, 
			@Identificador = @Identificador, @TipoDispensacion = @TipoDispensacion, @ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, @FoliosVenta = @FoliosVenta, 
			@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
			@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
			@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
			@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
			@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
			@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
			@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
			@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
			@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento 			
	End 	


	If @TipoProcesoRemision = 3  ----- Procesar almacenes 
	Begin 
		Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia 
		    @NivelInformacion_Remision = @NivelInformacion_Remision,  @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes, 
			@AsignarReferencias = @AsignarReferencias, 
			@Procesar_Producto = @Procesar_Producto, @Procesar_Servicio = @Procesar_Servicio, @Procesar_Servicio_Consigna = @Procesar_Servicio_Consigna, 
			@Procesar_Medicamento = @Procesar_Medicamento, @Procesar_MaterialDeCuracion = @Procesar_MaterialDeCuracion, 
			@Procesar_Venta = @Procesar_Venta, @Procesar_Consigna = @Procesar_Consigna, 
			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
			@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
			@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, @Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
			@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
			@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, 
			@Identificador = @Identificador, @TipoDispensacion = @TipoDispensacion, @ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, @FoliosVenta = @FoliosVenta, 
			@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
			@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
			@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
			@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
			@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
			@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
			@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales, 
			@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
			@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  
	End 	


End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia 
Go--#SQL 
    
Create Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia 
( 
	-- @TipoProcesoRemision int = 0,   --- 1 => General | 2 ==> Farmacia | Farmacia incrementos ==> 3 
	
	@NivelInformacion_Remision int = 2,   --- 1 => General (Primer nivel de informacion) | 2 ==> Farmacia FF (Segundo nivel de informacion) | 3 ==> Ventas directas por Jurisdiccion 
	@ProcesarParcialidades int = 0,		 ---- ====> Exclusivo SSH Primer Nivel 
	@ProcesarCantidadesExcedentes int = 0,    
	@AsignarReferencias int = 1, 

	@Procesar_Producto bit = 0, @Procesar_Servicio bit = 0, @Procesar_Servicio_Consigna bit = 0, 
	@Procesar_Medicamento bit = 0, @Procesar_MaterialDeCuracion bit = 0, 

	@Procesar_Venta bit = 0, @Procesar_Consigna bit = 0, 

	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', 
	@IdFarmaciaGenera varchar(4) = '', @TipoDeRemision smallint = 0, 
	@IdFarmacia varchar(max) = [ ], 
		
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@Lista__IdClienteIdSubCliente varchar(max) = [  ], 
 
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2001-01-01', @FechaFinal varchar(10) = '2001-12-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '00', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = '',  
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(max) = [  ], 
	@FechaDeRevision int = 2, 
	@FoliosVenta varchar(max) = [  ], 
	@TipoDeBeneficiario int = 0, -- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén>
	@Aplicar_ImporteDocumentos int = 0, 
	@EsProgramasEspeciales int = 0, 
	@Referencia_Beneficiario varchar(100) = '', @Referencia_BeneficiarioNombre varchar(200) = '', 
	@Remision_General int = 0, @ClaveSSA_ListaExclusion varchar(max) = [ ], 

	@EsRelacionFacturaPrevia bit = 0, 
	@FacturaPreviaEnCajas int = 0, 
	@Serie varchar(10) = '', @Folio int = 0, @EsRelacionMontos bit = 0, @Accion varchar(100) = '', 
	@Procesar_SoloClavesReferenciaRemisiones bit = 0, 
	@ExcluirCantidadesConDecimales bit = 0,  
	@Separar__Venta_y_Vales bit = 0, 
	@TipoDispensacion_Venta int = 0,   -- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	@Criterio_FiltroFecha___Vales int = 1,		-- 1 ==> Fecha de emision | 2 ==> Fecha de registro de vale canjeado ( Fecha registro de Venta ) 
	@EsRemision_Complemento bit = 0,     -- 0 == > Remisiones normales | 1 ==> Remisiones de Incremento ó Diferenciador 
	@MostrarResultado bit = 0, 
	@Remisiones_x_Farmacia bit = 1, 

	@EsRelacionDocumentoPrevio bit = 0, 
	@FolioRelacionDocumento varchar(6) = '' 	
) 
With Encryption 
As 
Begin 
Set NoCount On 

	--Select 
	--	'spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia' as SP, 
	--	@Procesar_Producto as Procesar_Producto, 
	--	@Procesar_Servicio as Procesar_Servicio, 
	--	@Procesar_Servicio_Consigna as Procesar_Servicio_Consigna, 
	--	@Procesar_Medicamento as Procesar_Medicamento, 
	--	@Procesar_MaterialDeCuracion as Procesar_MaterialDeCuracion, 
	--	@Procesar_Venta as Procesar_Venta, 
	--	@Procesar_Consigna as Procesar_Consigna  
		


	If @Procesar_Producto = 1 
	Begin 
		Set @TipoDeRemision = 1     -- Insumos 
		Set @TipoDispensacion = 0 

		If @Procesar_Medicamento = 1 and @Procesar_Venta = 1 
		Begin 
			Set @IdTipoProducto = '02'  -- Medicamento 
			Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
				@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
				@AsignarReferencias = @AsignarReferencias, 
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
				@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
				@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
				@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
				@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
				@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
				@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 	
				@FoliosVenta = @FoliosVenta, 
				@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
				@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
				@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
				@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
				@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
				@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
				@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
				@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento   
		End 

		If @Procesar_MaterialDeCuracion = 1 and @Procesar_Venta = 1  
		Begin 
			Set @IdTipoProducto = '01'  -- Material de curacion  
			Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
				@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
				@AsignarReferencias = @AsignarReferencias, 
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
				@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
				@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
				@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
				@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
				@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
				@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
				@FoliosVenta = @FoliosVenta, 
				@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
				@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
				@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
				@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
				@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
				@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
				@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
				@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento   
		End 
	End 


	If @Procesar_Servicio = 1 
	Begin 
		Set @TipoDeRemision = 2     -- Servicio  
		--Set @TipoDispensacion = 0 

		If @Procesar_Medicamento = 1 
		Begin 
			Set @IdTipoProducto = '02'  -- Medicamento 

			If @Procesar_Venta = 1 
			Begin 
				Set @TipoDispensacion = 0 
				Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
					@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
					@AsignarReferencias = @AsignarReferencias, 
					@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
					@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
					@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
					@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
					@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
					@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
					@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
					@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
					@FoliosVenta = @FoliosVenta, 
					@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
					@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
					@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
					@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
					@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
					@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
					@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
					@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
					@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  
			End 

			If @Procesar_Consigna = 1 and @Procesar_Servicio_Consigna = 1 
			Begin 
				Set @TipoDispensacion = 1 
				Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
					@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes, 
					@AsignarReferencias = @AsignarReferencias, 
					@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
					@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
					@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
					@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
					@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
					@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
					@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
					@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
					@FoliosVenta = @FoliosVenta, 
					@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
					@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
					@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
					@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
					@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
					@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
					@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
					@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
					@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  
			End 

		End 

		If @Procesar_MaterialDeCuracion = 1 
		Begin 
			Set @IdTipoProducto = '01'  -- Material de curacion  
			
			If @Procesar_Venta = 1 
			Begin 
				Set @TipoDispensacion = 0 
				Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
					@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes, 
					@AsignarReferencias = @AsignarReferencias, 
					@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
					@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
					@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
					@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
					@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
					@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
					@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
					@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
					@FoliosVenta = @FoliosVenta, 
					@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
					@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
					@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
					@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
					@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
					@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
					@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
					@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
					@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  
			End 

			If @Procesar_Consigna = 1 and @Procesar_Servicio_Consigna = 1 
			Begin 
				Set @TipoDispensacion = 1 
				Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
					@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes, 
					@AsignarReferencias = @AsignarReferencias, 
					@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
					@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
					@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
					@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
					@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
					@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
					@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
					@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
					@FoliosVenta = @FoliosVenta, 
					@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
					@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
					@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
					@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
					@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
					@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
					@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
					@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
					@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  
			End 
		End 
	End  



	If @Procesar_Servicio_Consigna = 1 
	Begin 
		Set @TipoDeRemision = 2     -- Servicio  
		Set @TipoDispensacion = 1 

		If @Procesar_Medicamento = 1 
		Begin 
			Set @IdTipoProducto = '02'  -- Medicamento 
			Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
				@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes, 
				@AsignarReferencias = @AsignarReferencias, 
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
				@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
				@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
				@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
				@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
				@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
				@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
				@FoliosVenta = @FoliosVenta, 
				@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
				@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
				@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
				@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
				@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
				@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
				@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
				@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento  
		End 

		If @Procesar_MaterialDeCuracion = 1 
		Begin 
			Set @IdTipoProducto = '01'  -- Material de curacion  
			Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia 
				@NivelInformacion_Remision = @NivelInformacion_Remision, @ProcesarParcialidades = @ProcesarParcialidades, @ProcesarCantidadesExcedentes = @ProcesarCantidadesExcedentes,  
				@AsignarReferencias = @AsignarReferencias, 
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
				@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
				@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
				@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
				@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
				@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
				@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
				@FoliosVenta = @FoliosVenta, 
				@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
				@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre, 
				@Remision_General = @Remision_General, @ClaveSSA_ListaExclusion = @ClaveSSA_ListaExclusion, 
				@EsRelacionFacturaPrevia = @EsRelacionFacturaPrevia, @FacturaPreviaEnCajas = @FacturaPreviaEnCajas, @Serie = @Serie, @Folio = @Folio, @Accion = @Accion, 
				@EsRelacionMontos = @EsRelacionMontos, @Procesar_SoloClavesReferenciaRemisiones = @Procesar_SoloClavesReferenciaRemisiones, 
				@ExcluirCantidadesConDecimales = @ExcluirCantidadesConDecimales, @Separar__Venta_y_Vales = @Separar__Venta_y_Vales, @TipoDispensacion_Venta = @TipoDispensacion_Venta, 
				@Criterio_FiltroFecha___Vales = @Criterio_FiltroFecha___Vales,  
				@EsRemision_Complemento = @EsRemision_Complemento, @MostrarResultado = @MostrarResultado, @Remisiones_x_Farmacia = @Remisiones_x_Farmacia, 
				@EsRelacionDocumentoPrevio = @EsRelacionDocumentoPrevio, @FolioRelacionDocumento = @FolioRelacionDocumento   
		End 
	End  


End 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia_Incremento' and xType = 'P' )
    Drop Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia_Incremento 
Go--#SQL 
    
Create Proc spp_FACT_GenerarRemisiones_x_TipoDeProceso__FFxFarmacia_Incremento 
( 
	-- @TipoProcesoRemision int = 0,   --- 1 => General | 2 ==> Farmacia | Farmacia incrementos ==> 3 
	
	@Procesar_Producto bit = 0, @Procesar_Servicio bit = 0, @Procesar_Servicio_Consigna bit = 0, 
	@Procesar_Medicamento bit = 0, @Procesar_MaterialDeCuracion bit = 0, 

	@Procesar_Venta bit = 0, @Procesar_Consigna bit = 0, 

	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', 
	@IdFarmaciaGenera varchar(4) = '', @TipoDeRemision smallint = 0, 
	@IdFarmacia varchar(max) = [ ], 
	
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@Lista__IdClienteIdSubCliente varchar(max) = [  ], 

	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@FechaInicial varchar(10) = '2001-01-01', @FechaFinal varchar(10) = '2001-12-31', 
	@iMontoFacturar numeric(14,4) = 0,  @IdPersonalFactura varchar(4) = '0001', 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @IdTipoProducto varchar(2) = '00', 
	@EsExcedente smallint = 0, @Identificador varchar(100) = '',  
	@TipoDispensacion int = 0, -- 0 ==> Venta | 1 Consigna 
	@ClaveSSA varchar(max) = [  ], 
	@FechaDeRevision int = 2, 
	@FoliosVenta varchar(max) = [  ], 
	@TipoDeBeneficiario int = 0, -- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén>
	@Aplicar_ImporteDocumentos int = 0, 
	@EsProgramasEspeciales int = 0, 
	@Referencia_Beneficiario varchar(100) = '', @Referencia_BeneficiarioNombre varchar(200) = '', 
	@Remision_General int = 0, @ClaveSSA_ListaExclusion varchar(max) = [ ], 
	@Remisiones_x_Farmacia int = 1, 

	@EsRelacionDocumentoPrevio bit = 0, 
	@FolioRelacionDocumento varchar(6) = '' 	
)
With Encryption 
As 
Begin 
Set NoCount On 

	Set @TipoDispensacion = 0 ---- Solo venta 
	------------------------- Obsoleto apartir del 20190101 
	----If @Procesar_Producto = 1 
	----Begin 
	----	Set @TipoDeRemision = 1     -- Insumos 

	----	If @Procesar_Medicamento = 1 
	----	Begin 
	----		Set @IdTipoProducto = '02'  -- Medicamento 
	----		Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia__Incrementos 
	----			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
	----			@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
	----			@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
	----			@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
	----			@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
	----			@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
	----			@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
	----			@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
	----			@FoliosVenta = @FoliosVenta, 
	----			@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
	----			@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre 

	----	End 

	----	If @Procesar_MaterialDeCuracion = 1 
	----	Begin 
	----		Set @IdTipoProducto = '01'  -- Material de curacion  
	----		Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia__Incrementos 
	----			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
	----			@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
	----			@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
	----			@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
	----			@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
	----			@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
	----			@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
	----			@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision, 
	----			@FoliosVenta = @FoliosVenta, 
	----			@TipoDeBeneficiario = @TipoDeBeneficiario, @Aplicar_ImporteDocumentos = @Aplicar_ImporteDocumentos, @EsProgramasEspeciales = @EsProgramasEspeciales, 
	----			@Referencia_Beneficiario = @Referencia_Beneficiario, @Referencia_BeneficiarioNombre = @Referencia_BeneficiarioNombre 
	----	End 
	----End 


	----Set @Procesar_Servicio = 0 --- No se procesa el servicio 
	----If @Procesar_Servicio = 1 
	----Begin 
	----	Set @TipoDeRemision = 2     -- Insumos 

	----	If @Procesar_Medicamento = 1 
	----	Begin 
	----		Set @IdTipoProducto = '02'  -- Medicamento 
	----		Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia__Incrementos 
	----			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
	----			@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
	----			@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
	----			@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
	----			@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
	----			@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
	----			@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
	----			@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision      
	----	End 

	----	If @Procesar_MaterialDeCuracion = 1 
	----	Begin 
	----		Set @IdTipoProducto = '01'  -- Material de curacion  
	----		Exec spp_FACT_GenerarRemisiones_General__FFxFarmacia__Incrementos 
	----			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @TipoDeRemision = @TipoDeRemision, 
	----			@IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @Lista__IdClienteIdSubCliente = @Lista__IdClienteIdSubCliente, 
	----			@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 
	----			@Criterio_ProgramasAtencion = @Criterio_ProgramasAtencion, 
	----			@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @iMontoFacturar = @iMontoFacturar, @IdPersonalFactura = @IdPersonalFactura, 
	----			@Observaciones = @Observaciones, @IdTipoProducto = @IdTipoProducto, @EsExcedente = @EsExcedente, @Identificador = @Identificador, 
	----			@TipoDispensacion = @TipoDispensacion, -- 0 ==> Venta | 1 Consigna 
	----			@ClaveSSA = @ClaveSSA, @FechaDeRevision = @FechaDeRevision     
	----	End 
	----End  


End 
Go--#SQL 

