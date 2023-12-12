------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_CFDI_ComplementoPagos_ObtenerDatos_VP' and xType = 'P' ) 
   Drop Proc spp_CFDI_ComplementoPagos_ObtenerDatos_VP 
Go--#SQL  

--Exec  spp_CFDI_ComplementoPagos_ObtenerDatos  @Identificador = 9477 

Create Proc spp_CFDI_ComplementoPagos_ObtenerDatos_VP
(
	@Identificador bigint = 5   
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
	From CFDI_Documentos_VP D (NoLock) 
	Where Keyx = @Identificador 
	
	Select @sIdUsoDeCFDI_Descripcion = Descripcion 
	From CFDI_UsosDeCFDI (NoLock) 
	Where Clave = @sIdUsoDeCFDI 

	Select @sIdTipoRelacion_Descripcion = Descripcion 
	From CFDI_TiposDeRelacion (NoLock) 
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

		--D.IdRubroFinanciamiento, '' as RubroFinanciamento, D.IdFuenteFinanciamiento, F.Financiamiento, 		
		D.Observaciones_01, D.Observaciones_02, D.Observaciones_03, D.Referencia,  
		
		D.SAT_ClaveDeConfirmacion, D.CFDI_Relacionado_CPago, D.Serie_Relacionada_CPago, D.Folio_Relacionado_CPago, 
		D.XMLFormaPago as XMLMetodoPago, 
		( select Top 1 Descripcion From CFDI_MetodosPago P (NoLock) Where P.IdMetodoDePago = D.XMLFormaPago )  as XMLMetodoPagoDescripcion, 

		D.XMLCondicionesPago, 
		
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 0, 1) as XMLFormaPago, 
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 0, 2) as XMLFormaPagoDescripcion, 
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 0, 3) as XMLNumeroCuentaPago, 	

		D.Status
	Into #tmpComprobante 
	From CFDI_Documentos_VP D (NoLock) 
	Inner Join vw_CFDI_TiposDeDocumentos TD On ( D.IdTipoDocumento = TD.IdTipoDocumento ) 	
	-- Inner Join FACT_CFD_FormasDePago P (NoLock) On ( P.IdFormaDePago = '01' ) --- D.IdFormaDePago ) 
	Inner Join CFDI_Emisores_Certificados C (NoLock) On ( D.IdEmpresa = C.IdEmpresa ) 
	Left Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, 
			cast(round(sum(SubTotal), 2) as numeric(14,2)) as SubTotal, 
			cast(round(sum(Iva), 2) as numeric(14,2)) as Iva, 
			cast(round(Sum(Importe), 2) as numeric(14,2)) as Importe 
		From CFDI_Documentos_VP_Conceptos DC (NoLock) 
		Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio 
	) M On ( D.IdEmpresa = M.IdEmpresa and D.IdEstado = M.IdEstado and D.IdFarmacia = M.IdFarmacia and D.Serie = M.Serie And D.Folio = M.Folio )	
	--Left Join vw_FuentesDeFinanciamiento_Detalle F (NoLock) 
	--	On ( D.IdRubroFinanciamiento = F.IdFuenteFinanciamiento And D.IdFuenteFinanciamiento = F.IdFinanciamiento ) 
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Serie = @Serie and D.Folio = @Folio  


	Select IdEmpresa, NombreFiscal as Nombre, NombreFiscal, NombreComercial, RFC, Telefonos, '' as Fax, '' as Email  
	Into #tmpEmisor 
	From CFDI_Emisores  
	Where IdEmpresa = @IdEmpresa 

	Select IdEmpresa, Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia  
	Into #tmpEmisorDomicilioFiscal 
	From vw_CFDI_Emisores  
	Where IdEmpresa = @IdEmpresa
	
		Select IdEmpresa, 
		Pais as Pais, EEstado as Estado, EMunicipio as Municipio, EColonia as Colonia, ECalle as Calle, 
		ENoExterior as NoExterior, ENoInterior as NoInterior, ECodigoPostal as CodigoPostal, EReferencia as Referencia 
	Into #tmpEmisorExpedidoEn 
	From vw_CFDI_Emisores_Sucursales  
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia    
	

	
		Select E.IdEmpresa, E.IdRegimen, R.Descripcion as Regimen 
	Into #tmpEmisorRegimenFiscal 
	From CFDI_Emisores_Regimenes E (NoLock) 
	Inner Join CFDI_RegimenFiscal R (NoLock) On ( E.IdRegimen = R.IdRegimen ) 
	Where IdEmpresa = @IdEmpresa 
 

---	select @IdReceptor  
	Select 
		IdCliente, Nombre, Nombre as NombreFiscal, Nombre as NombreComercial, 
		RFC, 
		@sIdUsoDeCFDI as UsoDeCFDI, 
		-- (@sIdUsoDeCFDI + ' ' + @sIdUsoDeCFDI_Descripcion) as UsoDeCFDI_Descripcion, 
		@sIdUsoDeCFDI_Descripcion as UsoDeCFDI_Descripcion,
		'' as Telefonos, '' as Fax, Email, 
		Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, Status  
	Into #tmpReceptor 
	From vw_CFDI_Clientes_Informacion (NoLock) 
	Where RFC = @IdReceptor 
	

---		spp_FACT_CFDI_ComplementoPagos_ObtenerDatos	
 	
	Select 
		C.IdEmpresa, C.Serie, C.Folio, identity(int, 1, 1) as Partida, 

		C.SAT_ClaveProducto_Servicio as ClaveProdServ, 
		P.Descripcion as ClaveProdServ_Descripcion, 
		C.CodigoEAN as NoIdentificacion, 'Pago' As Descripcion, 
		'' as Notas, 
		C.SAT_UnidadDeMedida as ClaveUnidad, 
		U.Descripcion as ClaveUnidad_Descripcion, 
		C.UnidadDeMedida as Unidad, C.Cantidad, 		
		C.PrecioUnitario as ValorUnitario, 0.0 as DescuentoPorc, 
		C.PrecioUnitario, C.TasaIva as Impuesto1, 0.0 as Impuesto2, 
		0.0 as RetencionISR, 0.0 as RetencionIVA, 
		C.SubTotal, C.Iva as ImporteIva, C.Importe, 
	
		C.SubTotal as Impuestos_Base, 
		TI.Clave as Impuesto_ImpuestoClave, 
		C.TipoImpuesto as Impuesto_Impuesto, 
		TC.Factor as Impuestos_TipoFactor, TC.ValorMaximo as Impuestos_TasaOCuota, 
		C.Iva as Impuestos_Importe, 

		'' as Status, 0 as Keyx 
	Into #tmpConceptos 
	From CFDI_Documentos_VP_Conceptos C (NoLock) 
	--Left Join vw_Productos_CodigoEAN P (NoLock) On ( C.IdProducto = P.IdProducto and C.CodigoEAN = P.CodigoEAN ) 
	Inner Join CFDI_Productos_Servicios P (NoLock) On ( C.SAT_ClaveProducto_Servicio = P.Clave ) 
	Inner Join CFDI_UnidadesDeMedida U (NoLock) On ( C.SAT_UnidadDeMedida = U.IdUnidad ) 
	Inner Join CFDI_TasaOCuota TC (NoLock) 
		On 
		( TC.EsTraslado = 1 and TC.Clave = C.TipoImpuesto and 
			( (TC.ValorMinimo * 100) = C.TasaIva and (TC.ValorMaximo * 100) = C.TasaIva ) 
		)
	Inner Join CFDI_TiposDeImpuestos TI (NoLock) On ( TC.Clave = TI.Descripcion ) 	 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio 

	 	
	Select Impuesto_ImpuestoClave, Impuesto_Impuesto, Impuestos_TipoFactor, Impuestos_TasaOCuota, Impuesto1 as TasaIva, sum(Impuestos_Importe) as Importe    
	Into #tmpTraslados 
	From #tmpConceptos  	
	Group by Impuesto_ImpuestoClave, Impuesto_Impuesto, Impuestos_TipoFactor, Impuestos_TasaOCuota, Impuesto1 	


	Select 
		IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, FechaDePago, FormaDePago, Moneda, TipoCambio, Monto, NumeroDeOperacion, RfcEmisorCtaOrd, NomBancoOrdExt, CtaOrdenante, 
		RfcEmisorCtaBen, CtaBeneficiario, TipoCadPago, CertificadoPago, CadenaPago, SelloPago 
	Into #tmp_CFDIs_Detalles 
	From CFDI_ComplementoDePagos_Detalles_VP (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio  


	Select 
		R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Serie, R.Folio, R.Serie_Relacionada, R.Folio_Relacionado, R.UUID_Relacionado,  
		R.Moneda, R.TipoCambio, R.MetodoDePago, R.NumParcialidad, R.Importe_SaldoAnterior, R.Importe_Pagado, R.Importe_SaldoInsoluto,  

		D.XMLFormaPago as MetodoDePagoDR, 
		( select Top 1 Descripcion From CFDI_MetodosPago P (NoLock) Where P.IdMetodoDePago = D.XMLFormaPago )  as MetodoDePagoDR_Descripcion  

	Into #tmp_CFDIs_Relacionados 
	From CFDI_ComplementoDePagos_DoctosRelacionados_VP R (NoLock) 
	Inner Join CFDI_Documentos D (NoLock) 
		On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmacia and R.Serie_Relacionada = D.Serie And R.Folio_Relacionado = D.Folio ) 
	Where R.IdEmpresa = @IdEmpresa And R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia and R.Serie = @Serie and R.Folio = @Folio  


---		spp_FACT_CFDI_ComplementoPagos_ObtenerDatos	
		
------------------------------------------ Obtener informacion 	

--	sp_listacolumnas CFDI_Documentos_Conceptos 


--			Exec spp_FACT_CFDI_ComplementoPagos_ObtenerDatos  @Identificador = 960 

--			Exec spp_FACT_CFDI_ComplementoPagos_ObtenerDatos  @Identificador = 1 

----------	Salida final    NOTA: no cambiar el orden de las consultas de salida 

	Select * From #tmpComprobante
	Select * From #tmpEmisor 
	Select * From #tmpEmisorRegimenFiscal 
	Select * From #tmpEmisorDomicilioFiscal 
	Select * From #tmpEmisorExpedidoEn  
	Select * From #tmpReceptor 		
	Select * From #tmpConceptos 
	Select * From #tmpTraslados 	
	Select * From #tmp_CFDIs_Relacionados 
	Select * From #tmp_CFDIs_Detalles 
		

End 
Go--#SQL 



