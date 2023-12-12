------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_CFDI_ComplementoPagos_ObtenerDatos' and xType = 'P' ) 
   Drop Proc spp_FACT_CFDI_ComplementoPagos_ObtenerDatos 
Go--#SQL  

Create Proc spp_FACT_CFDI_ComplementoPagos_ObtenerDatos  
(
	@Identificador bigint = 952   
	-- @IdEmpresa varchar(8) = '00000001', @Serie varchar(10) = 'F', @Folio varchar(10) = '3'
)
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 	

Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4), 		
	@Serie varchar(10), 
	@Folio varchar(10), 
	@IdReceptor varchar(20), 
	@sIdUsoDeCFDI varchar(20), 
	@sIdUsoDeCFDI_Descripcion varchar(200),  	
	@sIdTipoRelacion varchar(20), 
	@sIdTipoRelacion_Descripcion varchar(200) 

	Set @IdEmpresa = '' 
	Set @IdEstado = '' 
	Set @IdFarmacia = '' 		
	Set @Serie = '' 
	Set @Folio = '' 
	Set @IdReceptor = ''


	Select Top 1 
		@IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia = IdFarmacia, @Serie = Serie, @Folio = Folio, @IdReceptor = RFC, 
		@sIdUsoDeCFDI =  UsoDeCFDI, @sIdTipoRelacion = TipoRelacion
	From FACT_CFD_Documentos_Generados D (NoLock) 
	Where Keyx = @Identificador 
	
	Select @sIdUsoDeCFDI_Descripcion = Descripcion 
	From FACT_CFDI_UsosDeCFDI (NoLock) 
	Where Clave = @sIdUsoDeCFDI 

	Select @sIdTipoRelacion_Descripcion = Descripcion 
	From FACT_CFDI_TiposDeRelacion (NoLock) 
	Where Clave = @sIdTipoRelacion 


------------------------------------------ Obtener informacion 	
	Select 
		D.IdEmpresa, D.RFC as IdCliente, D.Keyx, D.FechaRegistro as Fecha, D.FechaRegistro, 
		D.Serie, D.Folio, 
		D.UsoDeCFDI, 
		D.TipoRelacion, 
		@sIdTipoRelacion_Descripcion as TipoRelacion_Descripcion, 
		'P' as EfectoComprobante, 'PAGO' as EfectoComprobante_Descripcion, 
		TD.IdTipoDocumento, TD.TipoDeComprobante, TD.NombreDocumento as TipoDeDocumentoElectronico, 		
		C.NumeroDeCertificado as NoCertificado, 
		C.NombreCertificado, C.Certificado, 
		C.NombreLlavePrivada, C.LlavePrivada, C.PasswordPublico, 
		---- D.Impuesto1, D.Impuesto2, D.IEPS, D.ISH, D.RetencionISR, D.RetencionIVA, D.Descuentos, D.Documento, 
		M.Iva as Impuesto1, 0.0 as Impuesto2, 0.0 as IEPS, 0.0 as ISH, 0.0 as RetencionISR, 0.0 as RetencionIVA, 0.0 as Descuentos, 0.0 as Documento, 		
		--P.IdFormaDePago, P.Descripcion as FormaDePago, 
		'' as IdFormaDePago, '' as FormaDePago, 
		'' as Notas, 1 as TipoCambio, '01' as IdDivisa, 'XXX' as Moneda, 
		-- D.SubTotal, Importe as Total, 
		M.SubTotal, M.Iva, M.Importe as Total, 
		getdate() as FechaPago, '' as Cancelado, D.FechaCancelacion, 
		'01' as TipoRecibo, D.NumeroCuentaPredial, 		

		D.TipoDocumento, --(case when D.TipoDocumento = 0 Then '' Else (case when D.TipoDocumento = 1 Then 'Venta' Else 'Administración' End) End) as TipoDocumentoDescripcion, 
		(
			Upper( 
				case when D.TipoDocumento = 1 then 'INSUMOS' 
					 when D.TipoDocumento = 3 then 'INSUMOS INCREMENTO'
					 when D.TipoDocumento = 4 then 'INSUMOS VENTA DIRECTA' 
					 when D.TipoDocumento = 5 then 'INSUMOS INCREMENTO VENTA DIRECTA' 
					 when D.TipoDocumento = 2 then 'ADMINISTRACIÓN' 
					 when D.TipoDocumento = 6 then 'ADMINISTRACIÓN VENTA DIRECTA' 
				else 
					'NO ESPECIFICADO' end 
				) 
		) as TipoDocumentoDescripcion, 
		D.TipoInsumo, --(case when D.TipoInsumo = 0 Then '' Else (case when D.TipoInsumo = 1 Then 'Material de curación' Else 'Medicamento' End) End) as TipoInsumoDescripcion, 
		(
			Upper(
			case when D.TipoInsumo = 0 then 'MEDICAMENTO Y MATERIAL DE CURACIÓN' 
				 when D.TipoInsumo = 1 then 'MATERIAL DE CURACIÓN' 
				 when D.TipoInsumo = 2 then 'MEDICAMENTO' 
				 else 'NO ESPECIFICADO' 
			end) 
		) as TipoDeInsumo, 	

		D.IdRubroFinanciamiento, '' as RubroFinanciamento, D.IdFuenteFinanciamiento, F.Financiamiento, 		
		D.Observaciones_01, D.Observaciones_02, D.Observaciones_03, D.Referencia,  
		
		D.SAT_ClaveDeConfirmacion, D.CFDI_Relacionado_CPago, D.Serie_Relacionada_CPago, D.Folio_Relacionado_CPago, 
		D.XMLFormaPago as XMLMetodoPago, 
		( select Top 1 Descripcion From FACT_CFDI_MetodosPago P (NoLock) Where P.IdMetodoDePago = D.XMLFormaPago )  as XMLMetodoPagoDescripcion, 

		D.XMLCondicionesPago, 
		
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 0, 1) as XMLFormaPago, 
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 0, 2) as XMLFormaPagoDescripcion, 
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 0, 3) as XMLNumeroCuentaPago, 	

		D.Status 
		,		cast('01' as varchar(10)) as ClaveExportacion 
	Into #tmpComprobante 
	From FACT_CFD_Documentos_Generados D (NoLock) 
	Inner Join vw_FACT_CFD_TiposDeDocumentos TD On ( D.IdTipoDocumento = TD.IdTipoDocumento ) 	
	-- Inner Join FACT_CFD_FormasDePago P (NoLock) On ( P.IdFormaDePago = '01' ) --- D.IdFormaDePago ) 
	Inner Join FACT_CFD_Certificados C (NoLock) On ( D.IdEmpresa = C.IdEmpresa ) 
	Left Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, 
			cast(round(sum(SubTotal), 2) as numeric(14,2)) as SubTotal, 
			cast(round(sum(Iva), 2) as numeric(14,2)) as Iva, 
			cast(round(Sum(Total), 2) as numeric(14,2)) as Importe 
		From FACT_CFD_Documentos_Generados_Detalles DC (NoLock) 
		Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio 
	) M On ( D.IdEmpresa = M.IdEmpresa and D.IdEstado = M.IdEstado and D.IdFarmacia = M.IdFarmacia and D.Serie = M.Serie And D.Folio = M.Folio )	
	Left Join vw_FACT_FuentesDeFinanciamiento_Detalle F (NoLock) 
		On ( D.IdRubroFinanciamiento = F.IdFuenteFinanciamiento And D.IdFuenteFinanciamiento = F.IdFinanciamiento ) 
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Serie = @Serie and D.Folio = @Folio  


	Select IdEmpresa, Nombre as Nombre, Nombre as NombreFiscal, Nombre as NombreComercial, RFC, Telefonos, '' as Fax, '' as Email  
	Into #tmpEmisor 
	From FACT_CFD_Empresas  
	Where IdEmpresa = @IdEmpresa 

	Select IdEmpresa, Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia  
	Into #tmpEmisorDomicilioFiscal 
	From FACT_CFD_Empresas_DomicilioFiscal  
	Where IdEmpresa = @IdEmpresa 
	
	Select IdEmpresa, 
		Pais as Pais, Estado as Estado, Municipio as Municipio, Colonia as Colonia, Calle as Calle, 
		NoExterior as NoExterior, NoInterior as NoInterior, CodigoPostal as CodigoPostal, Referencia as Referencia 
	Into #tmpEmisorExpedidoEn 
	From FACT_CFD_Sucursales_Domicilio 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia   

	

 
	Select E.IdEmpresa, E.IdRegimen, R.Descripcion as Regimen 
	Into #tmpEmisorRegimenFiscal 
	From FACT_CFDI_Emisores_Regimenes E (NoLock) 
	Inner Join FACT_CFDI_RegimenFiscal R (NoLock) On ( E.IdRegimen = R.IdRegimen ) 
	Where IdEmpresa = @IdEmpresa 

 
---	select @IdReceptor  
	Select 
		IdCliente, Nombre, Nombre as NombreFiscal, Nombre as NombreComercial, 
		RFC, 

		cast('' as varchar(10)) as DomicilioFiscal, 
		cast('' as varchar(10)) as RegimenFiscal, 
		cast('' as varchar(200)) as RegimenFiscal_Descripcion, 

		@sIdUsoDeCFDI as UsoDeCFDI, 
		-- (@sIdUsoDeCFDI + ' ' + @sIdUsoDeCFDI_Descripcion) as UsoDeCFDI_Descripcion, 
		@sIdUsoDeCFDI_Descripcion as UsoDeCFDI_Descripcion, 
		'' as Telefonos, '' as Fax, Email, 
		Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, Status  
	Into #tmpReceptor 
	From FACT_CFD_Clientes (NoLock) 
	Where RFC = @IdReceptor 


	----------------- Completar la información fiscal 
	Update R Set DomicilioFiscal = right('00000' + CAST(C.CodigoPostal as varchar(10)), 5), 
		RegimenFiscal = C.IdRegimen, 
		RegimenFiscal_Descripcion = F.Descripcion 
	From #tmpReceptor R (NoLock) 
	Inner Join  FACT_CFD_Clientes C (NoLock) On ( R.IdCliente = C.IdCliente )  
	Inner Join FACT_CFDI_RegimenFiscal F (NoLock) On ( C.IdRegimen = F.IdRegimen ) 


---		spp_FACT_CFDI_ComplementoPagos_ObtenerDatos	
 	
	Select 
		C.IdEmpresa, C.Serie, C.Folio, identity(int, 1, 1) as Partida, 

		C.SAT_ClaveProducto_Servicio as ClaveProdServ, 
		P.Descripcion as ClaveProdServ_Descripcion, 
		C.Identificador as NoIdentificacion, C.DescripcionConcepto as Descripcion, 
		'' as Notas, 
		C.SAT_UnidadDeMedida as ClaveUnidad, 
		U.Descripcion as ClaveUnidad_Descripcion, 
		C.UnidadDeMedida as Unidad, C.Cantidad, 		
		C.PrecioUnitario as ValorUnitario, 0.0 as DescuentoPorc, 
		C.PrecioUnitario, C.TasaIva as Impuesto1, 0.0 as Impuesto2, 
		
		0.0 as RetencionISR, 0.0 as RetencionIVA, 
		C.SubTotal, C.Iva as ImporteIva, C.Total as Importe, 
	
		--(C.TasaRetencionIVA / 100.0) as Impuestos_TasaOCuota__RetencionIVA, 	
		0.0 as Impuestos_TasaOCuota__RetencionIVA, 

		C.SubTotal as Impuestos_Base, 
		TI.Clave as Impuesto_ImpuestoClave, 
		C.TipoImpuesto as Impuesto_Impuesto, 
		TC.Factor as Impuestos_TipoFactor, TC.ValorMaximo as Impuestos_TasaOCuota, 
		C.Iva as Impuestos_Importe, 

		--cast((case when @iTipoDeComprobante = 4 then '01' else '02' end) as varchar(10)) as ClaveObjetoDeImpuesto, 	
		'01' as ClaveObjetoDeImpuesto, 

		'' as Status, 0 as Keyx 
	Into #tmpConceptos 
	From FACT_CFD_Documentos_Generados_Detalles C (NoLock) 
	Inner Join FACT_CFDI_Productos_Servicios P (NoLock) On ( C.SAT_ClaveProducto_Servicio = P.Clave ) 
	Inner Join FACT_CFD_UnidadesDeMedida U (NoLock) On ( C.SAT_UnidadDeMedida = U.IdUnidad ) 
	Inner Join FACT_CFDI_TasaOCuota TC (NoLock) 
		On 
		( TC.EsTraslado = 1 and TC.Clave = C.TipoImpuesto and 
			( (TC.ValorMinimo * 100) = C.TasaIva and (TC.ValorMaximo * 100) = C.TasaIva ) 
		)
	Inner Join FACT_CFDI_TiposDeImpuestos TI (NoLock) On ( TC.Clave = TI.Descripcion ) 	 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio 

	 	
	Select Impuesto_ImpuestoClave, Impuesto_Impuesto, Impuestos_TipoFactor, Impuestos_TasaOCuota, Impuesto1 as TasaIva, sum(Impuestos_Importe) as Importe    
	Into #tmpTraslados 
	From #tmpConceptos  	
	Group by Impuesto_ImpuestoClave, Impuesto_Impuesto, Impuestos_TipoFactor, Impuestos_TasaOCuota, Impuesto1 	


	Select 
		sum(Impuestos_Base) as Impuestos_Base, 
		Impuesto_ImpuestoClave, Impuesto_Impuesto, Impuestos_TipoFactor, 
		cast(round(Impuestos_TasaOCuota__RetencionIVA, 6) as numeric(14,6))as Impuestos_TasaOCuota, 
		cast(round(Impuestos_TasaOCuota__RetencionIVA * 100, 4) as numeric(14,4)) as TasaIva, sum(RetencionIVA) as Importe    
	Into #tmpRetenciones 
	From #tmpConceptos  	
	Where RetencionIVA > 0 
	Group by Impuesto_ImpuestoClave, Impuesto_Impuesto, Impuestos_TipoFactor, Impuestos_TasaOCuota, Impuestos_TasaOCuota__RetencionIVA 	


	Select 
		cast(0 as numeric(14,4) ) as TotalRetencionesIVA, 
		cast(0 as numeric(14,4) ) as TotalRetencionesISR, 
		cast(0 as numeric(14,4) ) as TotalRetencionesIEPS, 
		cast(0 as numeric(14,4) ) as TotalTrasladosBaseIVA16, 
		cast(0 as numeric(14,4) ) as TotalTrasladosImpuestoIVA16, 
		cast(0 as numeric(14,4) ) as TotalTrasladosBaseIVA8, 
		cast(0 as numeric(14,4) ) as TotalTrasladosImpuestoIVA8, 
		cast(0 as numeric(14,4) ) as TotalTrasladosBaseIVA0, 
		cast(0 as numeric(14,4) ) as TotalTrasladosImpuestoIVA0, 
		cast(0 as numeric(14,4) ) as TotalTrasladosBaseIVAExento, 
		cast(0 as numeric(14,4) ) as MontoTotalPagos, 		
		IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, FechaDePago, FormaDePago, Moneda, TipoCambio, Monto, NumeroDeOperacion, RfcEmisorCtaOrd, NomBancoOrdExt, CtaOrdenante, 
		RfcEmisorCtaBen, CtaBeneficiario, TipoCadPago, CertificadoPago, CadenaPago, SelloPago 
	Into #tmp_CFDIs_Detalles 
	From FACT_CFDI_ComplementoDePagos_Detalles (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio  


	Select 
		R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Serie, R.Folio, R.Serie_Relacionada, R.Folio_Relacionado, R.UUID_Relacionado,  
		R.Moneda, R.TipoCambio, R.MetodoDePago, R.NumParcialidad, R.Importe_SaldoAnterior, R.Importe_Pagado, R.Importe_SaldoInsoluto,  

		D.XMLFormaPago as MetodoDePagoDR, 
		( select Top 1 Descripcion From FACT_CFDI_MetodosPago P (NoLock) Where P.IdMetodoDePago = D.XMLFormaPago )  as MetodoDePagoDR_Descripcion,   

		'02' as ObjetoImpDR, 

		0 as TieneRetenciones, 
		0 as TieneTraslados 

	Into #tmp_CFDIs_Relacionados 
	From FACT_CFDI_ComplementoDePagos_DoctosRelacionados R (NoLock) 
	Inner Join FACT_CFD_Documentos_Generados D (NoLock) 
		On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmacia and R.Serie_Relacionada = D.Serie And R.Folio_Relacionado = D.Folio ) 
	Where R.IdEmpresa = @IdEmpresa And R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia and R.Serie = @Serie and R.Folio = @Folio  



	Select 
		R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Serie, R.Folio, R.Serie_Relacionada, R.Folio_Relacionado, R.UUID_Relacionado,  
		R.Moneda, R.TipoCambio, R.MetodoDePago, R.NumParcialidad, R.Importe_SaldoAnterior, R.Importe_Pagado, R.Importe_SaldoInsoluto,  
		R.MetodoDePagoDR, 
		R.MetodoDePagoDR_Descripcion, 
		
		R.Importe_Pagado as BaseDR, cast(0 as varchar(100)) as ImpuestoDR, cast(0 as varchar(100)) as TipoFactorDR, 
		cast(0 as numeric(14,4)) as TasaOCuotaDR, cast(0 as numeric(14,4)) as ImporteDR  
	Into #tmp_CFDIs_Relacionados_Pagos_Retenciones 
	From #tmp_CFDIs_Relacionados R (NoLock) 
	Where 1 = 0 


	Select 
		R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Serie, R.Folio, R.Serie_Relacionada, R.Folio_Relacionado, R.UUID_Relacionado,  
		R.Moneda, R.TipoCambio, R.MetodoDePago, R.NumParcialidad, R.Importe_SaldoAnterior, R.Importe_Pagado, R.Importe_SaldoInsoluto,  
		R.MetodoDePagoDR, 
		R.MetodoDePagoDR_Descripcion, 
		
		
		cast(sum(C.SubTotal) as numeric(14,4)) as BaseDR, 
		cast(TI.Clave as varchar(100)) as ImpuestoDR, 
		cast(TC.Factor as varchar(100)) as TipoFactorDR, 
		cast(TC.ValorMaximo as numeric(14,6)) as TasaOCuotaDR, 
		cast(sum(C.IVA) as numeric(14,4)) as ImporteDR,   
		(TC.ValorMaximo * 100) as TasaOCuota  

		--C.SubTotal as Impuestos_Base, 
		--TI.Clave as Impuesto_ImpuestoClave, 
		--'IVA' as Impuesto_Impuesto,	
		--TC.Factor as Impuestos_TipoFactor, TC.ValorMaximo as Impuestos_TasaOCuota, 
		--C.ImporteIva as Impuestos_Importe, 
	Into #tmp_CFDIs_Relacionados_Pagos_Traslados 
	From #tmp_CFDIs_Relacionados R (NoLock) 
	Inner Join FACT_CFD_Documentos_Generados_Detalles C (NoLock) 
		On ( R.IdEmpresa = C.IdEmpresa and R.IdEstado = C.IdEstado and R.IdFarmacia = C.IdFarmacia and R.Serie_Relacionada = C.Serie and R.Folio_Relacionado = C.Folio ) 
	Inner Join FACT_CFDI_TasaOCuota TC (NoLock) 
		On  
		-- ( TC.EsTraslado = 1 and TC.Clave = C.TipoImpuesto and 
		( TC.EsTraslado = 1 and TC.Clave = 'IVA' and 
			( (TC.ValorMinimo * 100) = C.TasaIva and (TC.ValorMaximo * 100) = C.TasaIva ) 
		) 
	Inner Join FACT_CFDI_TiposDeImpuestos TI (NoLock) On ( TC.Clave = TI.Descripcion ) 	
	Group by 
		R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Serie, R.Folio, R.Serie_Relacionada, R.Folio_Relacionado, R.UUID_Relacionado,  
		R.Moneda, R.TipoCambio, R.MetodoDePago, R.NumParcialidad, R.Importe_SaldoAnterior, R.Importe_Pagado, R.Importe_SaldoInsoluto,  
		R.MetodoDePagoDR, 
		R.MetodoDePagoDR_Descripcion, 
		TI.Clave, TC.Factor, TC.ValorMaximo 
		
	
	------------- Ajuste Version 4.0 
	Update D Set 
		BaseDR = dbo.fg_PRCS_Redondear(round(Importe_Pagado / ( 1.0 + TasaOCuotaDR ), 2, 1), 2, 0)  
	From #tmp_CFDIs_Relacionados_Pagos_Traslados D 

	Update D Set 
		--ImporteDR = dbo.fg_PRCS_Redondear(round(BaseDR * TasaOCuotaDR, 2, 1), 2, 0)  
		ImporteDR = dbo.fg_PRCS_Redondear(round(Importe_Pagado - BaseDR, 2, 1), 2, 0)
	From #tmp_CFDIs_Relacionados_Pagos_Traslados D 

	------------- Ajuste Version 4.0 



	------------ Totalizar los importes pagados 
	Update P Set MontoTotalPagos = (select sum(Importe_Pagado) From #tmp_CFDIs_Relacionados) 
	From #tmp_CFDIs_Detalles P 

	Update P Set TieneTraslados = 1 
	From #tmp_CFDIs_Relacionados P 
	Inner Join #tmp_CFDIs_Relacionados_Pagos_Traslados T (NoLock) 
		On ( P.IdEmpresa = T.IdEmpresa and P.IdEstado = T.IdEstado and P.IdFarmacia = T.IdFarmacia and P.Serie = T.Serie and P.Folio = T.Folio ) 

	Update P Set TieneRetenciones = 1 
	From #tmp_CFDIs_Relacionados P 
	Inner Join #tmp_CFDIs_Relacionados_Pagos_Retenciones T (NoLock) 
		On ( P.IdEmpresa = T.IdEmpresa and P.IdEstado = T.IdEstado and P.IdFarmacia = T.IdFarmacia and P.Serie = T.Serie and P.Folio = T.Folio ) 


	Select 1 as EsTraslado, 0 as EsRetencion, 
		TasaOCuota, 
		ImpuestoDR as ImpuestoP, TipoFactorDR as TipoFactorP, TasaOCuotaDR as TasaOCuotaP, 
		sum(BaseDR) as BaseP, sum(ImporteDR) as ImporteP  
	Into #tmp_CFDIs_Relacionados_Pagos_ImpuestosP 
	From #tmp_CFDIs_Relacionados_Pagos_Traslados 
	Group by 
		TasaOCuota, ImpuestoDR, TipoFactorDR, TasaOCuotaDR 


	Update P Set 
		TotalTrasladosBaseIVA0 = (select sum(BaseP) From #tmp_CFDIs_Relacionados_Pagos_ImpuestosP Where EsTraslado = 1 and TasaOCuota = 0), 
		TotalTrasladosImpuestoIVA0 = (select sum(ImporteP) From #tmp_CFDIs_Relacionados_Pagos_ImpuestosP Where EsTraslado = 1 and TasaOCuota = 0) 
	From #tmp_CFDIs_Detalles P 

	Update P Set 
		TotalTrasladosBaseIVA8 = (select sum(BaseP) From #tmp_CFDIs_Relacionados_Pagos_ImpuestosP Where EsTraslado = 1 and TasaOCuota = 8), 
		TotalTrasladosImpuestoIVA8 = (select sum(ImporteP) From #tmp_CFDIs_Relacionados_Pagos_ImpuestosP Where EsTraslado = 1 and TasaOCuota = 8) 
	From #tmp_CFDIs_Detalles P 

	Update P Set 
		TotalTrasladosBaseIVA16 = (select sum(BaseP) From #tmp_CFDIs_Relacionados_Pagos_ImpuestosP Where EsTraslado = 1 and TasaOCuota = 16), 
		TotalTrasladosImpuestoIVA16 = (select sum(ImporteP) From #tmp_CFDIs_Relacionados_Pagos_ImpuestosP Where EsTraslado = 1 and TasaOCuota = 16) 
	From #tmp_CFDIs_Detalles P 




---		spp_FACT_CFDI_ComplementoPagos_ObtenerDatos	
		
------------------------------------------ Obtener informacion 	

--	sp_listacolumnas CFDI_Documentos_Conceptos 


--			Exec spp_FACT_CFDI_ComplementoPagos_ObtenerDatos  @Identificador = 960 

--			Exec spp_FACT_CFDI_ComplementoPagos_ObtenerDatos  @Identificador = 1 

----------	Salida final    NOTA: no cambiar el orden de las consultas de salida 

	----Select * From #tmpComprobante
	----Select * From #tmpEmisor 
	----Select * From #tmpEmisorRegimenFiscal 
	----Select * From #tmpEmisorDomicilioFiscal 
	----Select * From #tmpEmisorExpedidoEn  
	----Select * From #tmpReceptor 		
	----Select * From #tmpConceptos 
	----Select * From #tmpTraslados 	
	----Select * From #tmpTraslados 	--- Retenciones 


	----Select * From #tmp_CFDIs_Relacionados 
	----Select * From #tmp_CFDIs_Detalles 
		


	Select 'COMPROBANTE' as Tabla, * From #tmpComprobante
	Select 'EMISOR' as Tabla, * From #tmpEmisor 
	Select 'REGIMEN FISCAL' as Tabla, * From #tmpEmisorRegimenFiscal 
	Select 'DOMICILIO FISCAL' as Tabla, * From #tmpEmisorDomicilioFiscal 
	Select 'LUGAR DE EXPEDICION' as Tabla, * From #tmpEmisorExpedidoEn  
	Select 'RECEPTOR' as Tabla, * From #tmpReceptor 		
	Select 'CONCEPTOS' as Tabla, * From #tmpConceptos 
	Select 'TRASLADOS' as Tabla, * From #tmpTraslados 	
	Select 'RETENCIONES' as Tabla, * From #tmpRetenciones 

	Select 'CFDI RELACIONADOS' as Tabla, * From #tmp_CFDIs_Relacionados 
	Select 'DETALLES COMPLEMENTO DE PAGO' as Tabla, * From #tmp_CFDIs_Detalles 

	Select 'CFDI RELACIONADOS COMPLEMENTO DE PAGO RETENCIONES' as Tabla, * From #tmp_CFDIs_Relacionados_Pagos_Retenciones 
	Select 'CFDI RELACIONADOS COMPLEMENTO DE PAGO TRASLADOS' as Tabla, * From #tmp_CFDIs_Relacionados_Pagos_Traslados 
	Select 'CFDI RELACIONADOS COMPLEMENTO DE PAGO IMPUESTOS' as Tabla, * From #tmp_CFDIs_Relacionados_Pagos_ImpuestosP



End 
Go--#SQL 



