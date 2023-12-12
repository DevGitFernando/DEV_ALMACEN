---------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__03__FolioElectronico' and xType = 'U' ) 
	Drop Table FACT_CFDI_TM__03__FolioElectronico 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__02__DetallesClaves' and xType = 'U' ) 
	Drop Table FACT_CFDI_TM__02__DetallesClaves 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__01__Encabezado' and xType = 'U' ) 
	Drop Table FACT_CFDI_TM__01__Encabezado 
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_CFDI_GetInformacionTimbrado' and xType = 'P' )
    Drop Proc spp_FACT_CFDI_GetInformacionTimbrado
Go--#SQL 
  
---		Exec spp_FACT_CFDI_GetInformacionTimbrado  @IdEmpresa = '001', @IdEstado = '13', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000006'  

Create Proc spp_FACT_CFDI_GetInformacionTimbrado 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '1',  @FolioRemision varchar(10) = '1', 
	@Aplicar_Mascara int = 0, 
	@IdPersonal varchar(10) = '0001', 
	@IdClienteFiscal varchar(20) = '49', @Serie varchar(10) = 'PRUEBAS', 

	@XMLFormaPago varchar(500) = 'EN UNA SOLA EXHIBICION', @XMLCondicionesPago varchar(500) = 'EN UNA SOLA EXHIBICION 30 DÍAS', @XMLMetodoPago varchar(500) = 'NO IDENTIFICADO', 
    @MetodoDePago varchar(50) = '', @ReferenciaDePago varchar(50) = '',  

	@Identificador_UUID varchar(500) = '21A12FF4-AB2B-4180-9FD7-F0D1385C6FAC', @IncrementarFolios bit = 0, @UsoDeCFDI varchar(20) = '', 
	@Anexar_Informacion_Lotes_y_Caducidades int = 0      
) 
With Encryption		
As 
Begin 
Set NoCount On 
Declare 
	@Anexar__Lotes_y_Caducidades bit, 
	@EsInsumos bit, 	
	@EsMedicamento bit, 
	@sUnidadDeMedida varchar(100), 
	@sFarmacia varchar(300), 
	@sPeriodo varchar(300), 
	@sFarmaciasRemision varchar(max), 
	@sTipoDocumento varchar(10),   
	@sTipoImpuesto varchar(20), 
	@sRFC varchar(20), 
	@sNombreComercial varchar(500), 
	@sFolioFactura varchar(20), 
	@sFolioFacturaElectronica varchar(50), 
	@sTipoDeFactura varchar(5), 
	@sObservaciones_Factura varchar(2000) 
	-- @UsoDeCFDI varchar(20) 


Declare 
	@SubTotalSinGrabar numeric(14, 4), 
	@SubTotalGrabado numeric(14, 4), 
	@Iva numeric(14, 4), 
	@Total numeric(14, 4)  


Declare 
	@Keyx bigint, 
	@ClaveSSA varchar(50), 
	@Informacion varchar(max)  	


--------------------------- Preparar datos de entrada 
	Set @sUnidadDeMedida = ''
	Set @Keyx = 0 
	Set @ClaveSSA = '' 
	Set @Informacion = '' 
	Set @EsInsumos = 0  
	Set @EsMedicamento = 0  	
	Set @sFarmacia = '' 
	Set @sPeriodo = '' 
	Set @sFarmaciasRemision = '' 
	Set @sFolioFactura = '*' 
	Set @SubTotalSinGrabar = 0  
	Set @SubTotalGrabado = 0  
	Set @Iva = 0  
	Set @Total = 0   
	Set @sFolioFacturaElectronica = '' 
	Set @sTipoDeFactura = '' 
	Set @sObservaciones_Factura = ''

	Set @sTipoDocumento = '001'
	Set @sTipoImpuesto = 'IVA'
	Set @sRFC = ''  
	Set @sNombreComercial = ''


	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 
	Set @IdClienteFiscal = dbo.fg_FormatearCadena(@IdClienteFiscal, '0', 6)

	-------------- Buscar el valor predeterminado del parámetro 
	If @UsoDeCFDI = '' 
	Begin 
		Select top 1 @UsoDeCFDI = Valor 
		From Net_CFG_Parametros_Facturacion (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_UsoCDFI' 
	End 

	if @Identificador_UUID = '' 
	   Set @Identificador_UUID = cast(NEWID() as varchar(500))  
--------------------------- Preparar datos de entrada 



	-------------------------------------- Ejecucion de procesos 
	Exec spp_FACT_CFD_GetInformacionRemision 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision, @Identificador_UUID = @Identificador_UUID   


	Exec spp_FACT_CFD_GetRemision 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera,  @FolioRemision = @FolioRemision, 
		@Detallado = 1, @Aplicar_Mascara = @Aplicar_Mascara, @Identificador_UUID = @Identificador_UUID, 
		@Anexar_Informacion_Lotes_y_Caducidades = @Anexar_Informacion_Lotes_y_Caducidades  


	Exec spp_FACT_CFD_GetFolio 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmaciaGenera, 
		@Serie = @Serie, @Identificador_UUID = @Identificador_UUID, @Incrementar = @IncrementarFolios    
	

	-------------------------------------- Ejecucion de procesos 


------------------------------------------ AGREGAR INFORMACION PARA GENERAR EL CFDI    
	Select @sRFC = RFC,  @sNombreComercial = Nombre  
	From FACT_CFD_Clientes (NoLock) 
	Where IdCliente = @IdClienteFiscal 

	Select @sTipoDeFactura = D.TipoDeRemision, @sObservaciones_Factura = D.InformacionAdicional, 
		@SubTotalSinGrabar = D.SubTotalSinGrabar, @SubTotalGrabado = D.SubTotalGrabado, @Iva = D.Iva, @Total = D.Total 
	From FACT_CFDI_TM__01__Encabezado D 
	Where D.Identificador_UUID = @Identificador_UUID  

	Select @sFolioFacturaElectronica = D.Serie + ' - ' + cast(D.Folio as varchar(10))  
	From FACT_CFDI_TM__03__FolioElectronico D 
	Where D.Identificador_UUID = @Identificador_UUID  


---		select * from FACT_CFDI_TM__03__FolioElectronico 

---		sp_listacolumnas  FACT_CFD_Documentos_Generados_Detalles 

--		spp_FACT_CFDI_GetInformacionTimbrado  


	Insert Into FACT_CFD_Documentos_Generados 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FechaRegistro, IdTipoDocumento, UsoDeCFDI, 
		IdentificadorSerie, Serie, NombreDocumento, Folio, Importe, 
		RFC, NombreReceptor, IdCFDI, FormatoXML, FormatoPDF, TieneXML, TienePDF, Status, IdPersonalCancela, FechaCancelacion, 
		MotivoCancelacion, TieneTimbre, FormatoBase, SubTotal, Iva, 
		TipoDocumento, TipoInsumo, IdRubroFinanciamiento, IdFuenteFinanciamiento, 
		Observaciones_01, Observaciones_02, Observaciones_03, 
		Referencia, Referencia_02,  Referencia_03, Referencia_04, Referencia_05, 
		IdFormaDePago, NumeroCuentaPredial, XMLFormaPago, XMLCondicionesPago, XMLMetodoPago, IdPersonalEmite, EsRelacionConRemisiones 	  
	) 
	Select top 1 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, getdate() as FechaRegistro, @sTipoDocumento as IdTipoDocumento, @UsoDeCFDI as UsoDeCFDI, 
		E.IdentificadorSerie, E.Serie, '' as NombreDocumento, E.Folio, I.Total as Importe, 
		@sRFC as RFC, @sNombreComercial as NombreReceptor, '' as IdCFDI, 
		'' as FormatoXML, '' as FormatoPDF, 1 as TieneXML, 1 as TienePDF, 'A' as Status, '' as IdPersonalCancela, getdate() as FechaCancelacion, 
		'' as MotivoCancelacion, 1 as TieneTimbre, '' as FormatoBase, (I.SubTotalSinGrabar + I.SubTotalGrabado) as SubTotal, I.Iva, 
		I.TipoDeRemision as TipoDocumento, I.TipoInsumo as TipoInsumo, I.IdFuenteFinanciamiento as IdRubroFinanciamiento, I.IdFinanciamiento as IdFuenteFinanciamiento, 
		I.InformacionDeContrato as Observaciones_01, I.InformacionAdicional as Observaciones_02, I.FarmaciasRemision as Observaciones_03, 
		'RM' + I.FolioRemision as Referencia, 
		'' as Referencia_02,  '' as Referencia_03, D.Referencia_Remisiones as Referencia_04, D.Referencia_Facturas as Referencia_05, 		
		'00' as IdFormaDePago, '' as NumeroCuentaPredial, 
		@XMLFormaPago as XMLFormaPago, @XMLCondicionesPago as XMLCondicionesPago, @XMLMetodoPago as XMLMetodoPago, 
		@IdPersonal as IdPersonalEmite, 1 as EsRelacionConRemisiones 	  
	From FACT_CFDI_TM__01__Encabezado D (NoLock) 
	Inner Join FACT_CFDI_TM__03__FolioElectronico E (NoLock) On ( D.Identificador_UUID = E.Identificador_UUID ) 
	Inner Join FACT_CFDI_TM__01__Encabezado I (NoLock) On ( D.Identificador_UUID = I.Identificador_UUID ) 
	Where D.Identificador_UUID = @Identificador_UUID 


	Insert Into FACT_CFD_Documentos_Generados_Detalles 
	(  
		IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Identificador, Clave, DescripcionConcepto, UnidadDeMedida, Cantidad, PrecioUnitario, TasaIva, SubTotal, Iva, Total, 
		TipoImpuesto, Partida, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida  
	)  
	Select 
		-- IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Identificador, Clave, DescripcionConcepto, UnidadDeMedida, Cantidad, 
		-- PrecioUnitario, TasaIva, SubTotal, Iva, Total, TipoImpuesto, Partida 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Serie, E.Folio, ClaveSSA as Identificador, 
		D.ClaveSSA, D.Descripcion as DescripcionConcepto, D.UnidadDeMedida, D.Cantidad, D.PrecioUnitario, D.TasaIva, D.SubTotal, D.Iva, Importe, 
		@sTipoImpuesto as TipoImpuesto, Orden, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida  
	From FACT_CFDI_TM__02__DetallesClaves D 
	Inner Join FACT_CFDI_TM__03__FolioElectronico E (NoLock) On ( D.Identificador_UUID = E.Identificador_UUID ) 
	Where D.Identificador_UUID = @Identificador_UUID 
	Order By D.Orden 

	Insert Into FACT_CFD_Documentos_Generados_MetodosDePago (  IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdMetodoPago, Importe, Referencia ) 
	Select  I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.Serie, I.Folio, @MetodoDePago as IdMetodoPago, D.Total as Importe, @ReferenciaDePago as Referencia 
	From FACT_CFDI_TM__01__Encabezado D (NoLock) 
	Inner Join FACT_CFDI_TM__03__FolioElectronico I (NoLock) On ( D.Identificador_UUID = I.Identificador_UUID ) 
	Where D.Identificador_UUID = @Identificador_UUID 


	Exec spp_Mtto_FACT_Facturar_Remisiones  
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmaciaGenera, @FolioFactura = @sFolioFactura, 
		@TipoDeFactura = @sTipoDeFactura, @FolioRemision = @FolioRemision, 
        @FolioFacturaElectronica = @sFolioFacturaElectronica, @IdPersonalFactura = @IdPersonal, 
		@SubTotalSinGrabar = @SubTotalSinGrabar, @SubTotalGrabado = @SubTotalGrabado, @Iva = @Iva, @Total = @Total, 
        @Observaciones = @sObservaciones_Factura, @Status = 'A', @MostrarSalida = 0   

------------------------------------------ AGREGAR INFORMACION PARA GENERAR EL CFDI    



	---------------------------------------- SALIDA FINAL 
	Select @Keyx = Keyx 
	From FACT_CFD_Documentos_Generados D (NoLock) 
	Inner Join FACT_CFDI_TM__03__FolioElectronico E (NoLock) 
		On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.Serie = E.Serie and D.Folio = E.Folio ) 
	Where E.Identificador_UUID = @Identificador_UUID  

	Exec spp_FACT_CFDI_ObtenerDatos @Identificador = @Keyx 




	-- select * from  FACT_CFD_Documentos_Generados_Detalles 

	-- Select @Identificador_UUID as UUID  

	----Select * 
	----From FACT_CFDI_TM__01__Encabezado (NoLock) 
	----Where Identificador_UUID = @Identificador_UUID 
	
	----Select * 
	----From FACT_CFDI_TM__02__DetallesClaves (NoLock) 
	----Where Identificador_UUID = @Identificador_UUID 

	----Select * 
	----From FACT_CFDI_TM__03__FolioElectronico (NoLock) 
	----Where Identificador_UUID = @Identificador_UUID 


	---------------------------------------- SALIDA FINAL 




End
Go--#SQL

