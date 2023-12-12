If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_CFDI_ObtenerDatos_VP' and xType = 'P' ) 
   Drop Proc spp_CFDI_ObtenerDatos_VP 
Go--#SQL  

Create Proc spp_CFDI_ObtenerDatos_VP 
(
	@Identificador bigint = 2 
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
	@IdEstadoReferencia Varchar(2),
	@IdFarmacia varchar(4),
	@IdFarmaciaReferencia Varchar(4),	
	@TipoDeFacturacion int,  	
	@Serie varchar(10), 
	@Folio varchar(10), 
	@IdReceptor varchar(20) 
	
	Set @IdEmpresa = '' 
	Set @IdEstado = ''
	Set @IdEstadoReferencia = ''
	Set @IdFarmacia = ''
	Set @IdFarmaciaReferencia = ''		
	Set @Serie = '' 
	Set @Folio = '' 
	Set @IdReceptor = ''

	Select @TipoDeFacturacion = dbo.fg_Parametro_TipoFacturacion( )  

	Select Top 1 @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia = IdFarmacia, @Serie = Serie, @Folio = Folio, @IdReceptor = RFC,
		@IdFarmaciaReferencia = IdFarmaciaReferencia, @IdEstadoReferencia = IdEstadoReferencia
	From CFDI_Documentos_VP D (NoLock) 
	Where Keyx = @Identificador 
	
------------------------------------------ Obtener informacion 	
	Select 
		D.IdEmpresa, D.RFC as IdCliente, D.Keyx, D.FechaRegistro as Fecha, D.FechaRegistro, 
		D.Serie, D.Folio, 
		TD.IdTipoDocumento, TD.TipoDeComprobante, TD.NombreDocumento as TipoDeDocumentoElectronico, 		
		C.NumeroDeCertificado as NoCertificado, 
		C.NombreCertificado, C.Certificado, 
		C.NombreLlavePrivada, C.LlavePrivada, C.PasswordPublico, 
		---- D.Impuesto1, D.Impuesto2, D.IEPS, D.ISH, D.RetencionISR, D.RetencionIVA, D.Descuentos, D.Documento, 
		M.Iva as Impuesto1, 
		cast(0 as numeric(14,2)) as Impuesto2, cast(0 as numeric(14,2)) as IEPS, cast(0 as numeric(14,2)) as ISH, 
		cast(0 as numeric(14,2)) as RetencionISR, cast(0 as numeric(14,2)) as RetencionIVA, 
		cast(0 as numeric(14,2)) as Descuento, 0.0 as Documento, 			
		P.IdFormaDePago, P.Descripcion as FormaDePago, 
		'' as Notas, 1 as TipoCambio, '01' as IdDivisa, 'MXN' as Moneda, 
		-- D.SubTotal, Importe as Total, 
		M.SubTotal, M.Iva, M.Importe as Total, 
		getdate() as FechaPago, '' as Cancelado, D.FechaCancelacion, 
		'01' as TipoRecibo, D.NumeroCuentaPredial, 		
		D.TipoDocumento, (case when D.TipoDocumento = 0 Then '' Else (case when D.TipoDocumento = 1 Then 'Venta' Else 'Administración' End) End) as TipoDocumentoDescripcion, 
		D.TipoInsumo, (case when D.TipoInsumo = 0 Then '' Else (case when D.TipoInsumo = 1 Then 'Material de curación' Else 'Medicamento' End) End) as TipoInsumoDescripcion, 
		'' as IdRubroFinanciamiento, '' as RubroFinanciamento, '' as IdFuenteFinanciamiento, '' as Financiamiento, 		
		D.Observaciones_01, D.Observaciones_02, D.Observaciones_03, D.Referencia,  
		D.XMLFormaPago, 
		
		D.XMLCondicionesPago, 		
		--D.XMLMetodoPago,		---	  Exec  spp_CFDI_ObtenerDatos 9 
		dbo.fg_MetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 1) as XMLMetodoPago, 
		dbo.fg_MetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 2) as XMLMetodoPagoDescripcion, 
		dbo.fg_MetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 3) as XMLNumeroCuentaPago, 				
		--'' as XMLNumeroCuentaPago, 
		D.Status
	Into #tmpComprobante 
	From CFDI_Documentos_VP D (NoLock) 
	Inner Join vw_CFDI_TiposDeDocumentos TD On ( D.IdTipoDocumento = TD.IdTipoDocumento ) 	
	Inner Join CFDI_FormaDePago P (NoLock) On ( P.IdFormaDePago = '01' ) --- D.IdFormaDePago ) 
	--Inner Join CFDI_Divisas M (NoLock) On ( D.IdDivisa = M.IdDivisa ) 
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
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstadoReferencia and IdFarmacia = @IdFarmaciaReferencia   


----			spp_CFDI_ObtenerDatos_VP 	

 
	Select E.IdEmpresa, E.IdRegimen, R.Descripcion as Regimen 
	Into #tmpEmisorRegimenFiscal 
	From CFDI_Emisores_Regimenes E (NoLock) 
	Inner Join CFDI_RegimenFiscal R (NoLock) On ( E.IdRegimen = R.IdRegimen ) 
	Where IdEmpresa = @IdEmpresa 

 
---	select @IdReceptor  
	Select 
		IdCliente, Nombre, Nombre as NombreFiscal, Nombre as NombreComercial, 
		RFC, '' as Telefonos, '' as Fax, Email, 
		Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, Status  
	Into #tmpReceptor 
	From vw_CFDI_Clientes_Informacion (NoLock) 
	Where RFC = @IdReceptor 
	
	
 	
	Select 
		----C.IdEmpresa, C.Serie, C.Folio, Partida, 
		----C.CodigoEAN as NoIdentificacion,  C.CodigoEAN, P.Descripcion, 
		----'' as Notas, 
		----C.UnidadDeMedida as Unidad, C.Cantidad, 
		----C.PrecioUnitarioFinal as ValorUnitario, DescuentoPorc, 
		----C.PrecioUnitarioFinal as PrecioUnitario, C.TasaIva as Impuesto1, 0.0 as Impuesto2, 
		----0.0 as RetencionISR, 0.0 as RetencionIVA, 
		----sum(C.SubTotal) as SubTotal, sum(C.Iva) as ImporteIva, sum(C.Importe) as Importe, '' as Status, 0 as Keyx 
		
		C.IdEmpresa, C.Serie, C.Folio, Partida, 
		C.CodigoEAN as NoIdentificacion,  C.CodigoEAN, P.Descripcion, 
		'' as Notas, 
		@TipoDeFacturacion as TipoDeFacturacion, 
		C.UnidadDeMedida as Unidad, C.Cantidad, 
		C.PrecioUnitario as PrecioUnitarioBase, 
		C.PrecioUnitarioFinal as ValorUnitario, DescuentoPorc, 
		C.PrecioUnitarioFinal as PrecioUnitario, 
		C.TasaIva, 
		C.TasaIva as Impuesto1, 0.0 as Impuesto2, 
		0.0 as RetencionISR, 0.0 as RetencionIVA, 
		sum(C.SubTotal) as SubTotal, sum(C.Iva) as ImporteIva, sum(C.Importe) as Importe, 
		
		cast(0 as numeric(14,4)) as SubTotal_Neto, cast(0 as numeric(14,4)) as ImporteIva_Neto, cast(0 as numeric(14,4)) as Importe_Neto, 	
		sum(C.SubTotal) as SubTotal_Descuento, sum(C.Iva) as ImporteIva_Descuento, sum(C.Importe) as Importe_Descuento, 
		cast(0 as numeric(14,4)) as SubTotal_Neto_Dif, cast(0 as numeric(14,4)) as ImporteIva_Neto_Dif, cast(0 as numeric(14,4)) as Importe_Neto_Dif, 					
		'' as Status, 0 as Keyx  				
	Into #tmpConceptos 
	From CFDI_Documentos_VP_Conceptos C (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( C.IdProducto = P.IdProducto and C.CodigoEAN = P.CodigoEAN ) 
	----Inner Join CFDI_Productos P (NoLock) On ( C.IdConcepto = P.IdProducto ) 
	----Inner Join CFDI_UnidadesDeMedida U (NoLock) On ( P.IdUnidad = U.IdUnidad ) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio 
		and Cantidad > 0 	
	Group By 
		C.IdEmpresa, C.Serie, C.Folio, Partida, 
		C.CodigoEAN,  P.Descripcion, 
		C.UnidadDeMedida, C.Cantidad, 
		C.PrecioUnitario, 
		C.PrecioUnitarioFinal, DescuentoPorc, C.TasaIva	

	 	
	Update C Set 
		SubTotal_Neto = PrecioUnitarioBase * Cantidad, 
		ImporteIva_Neto = (PrecioUnitarioBase * Cantidad) * (C.TasaIva / 100.00) , 
		Importe_Neto = (PrecioUnitarioBase * Cantidad) * ( 1 + (C.TasaIva / 100.00)) 		
	From #tmpConceptos C  	
	
	
	If @TipoDeFacturacion = 1 
	Begin  	
		Update C Set 
			SubTotal_Neto_Dif = SubTotal_Neto - SubTotal, 
			ImporteIva_Neto_Dif = ImporteIva_Neto - ImporteIva, 
			Importe_Neto_Dif = Importe_Neto - Importe 
		From #tmpConceptos C  	
		
		Update C Set ValorUnitario = PrecioUnitarioBase, 
			--SubTotal = SubTotal_Neto, ImporteIva = (ImporteIva_Neto - ImporteIva_Neto_Dif), Importe = Importe_Neto 
			SubTotal = SubTotal_Neto, ImporteIva = (ImporteIva_Neto - ImporteIva_Neto_Dif), Importe = Importe_Neto 			
		From #tmpConceptos C  	
		
		Select 
			Sum(SubTotal_Neto_Dif) as SubTotal_Neto_Dif, 
			Sum(SubTotal) as SubTotal, sum(ImporteIva) as ImporteIva, sum(Importe) as Importe,  
			Sum(SubTotal_Descuento) as SubTotal_Descuento, sum(ImporteIva_Descuento) as ImporteIva_Descuento, 
			sum(Importe_Descuento) as Importe_Descuento, 
			Sum(SubTotal - SubTotal_Descuento) as SubTotal_X, sum(ImporteIva - ImporteIva_Descuento) as ImporteIva_X, 
			sum(Importe - Importe_Descuento) as Importe_X   
		Into #tmp_Datos 
		From #tmpConceptos 
		
		----select * from #tmp_Datos 
		----select * from #tmpComprobante 
				
		Update C Set SubTotal = D.SubTotal, Iva = D.ImporteIVA, 
			Total = (D.SubTotal - D.SubTotal_Neto_Dif) + D.ImporteIVA , Descuento = D.SubTotal_Neto_Dif 
			--Total = (D.SubTotal - SubTotal_Descuento) + ImporteIVA, Descuento = SubTotal_Descuento 			
		From #tmpComprobante C, #tmp_Datos D 
		
	End 
		 	

	------ Calcular los IMPUESTOS 	 	
	Select Impuesto1 as TasaIva, sum(ImporteIva) as Importe    
	Into #tmpTraslados 
	From #tmpConceptos  	
	Group by Impuesto1 	

		
------------------------------------------ Obtener informacion 	

--	sp_listacolumnas CFDI_Documentos_Conceptos 

--			spp_CFDI_ObtenerDatos_VP  

----------	Salida final    NOTA: no cambiar el orden de las consultas de salida 

	Select * From #tmpComprobante
	Select * From #tmpEmisor 
	Select * From #tmpEmisorRegimenFiscal 
	Select * From #tmpEmisorDomicilioFiscal 
	Select * From #tmpEmisorExpedidoEn  
	Select * From #tmpReceptor 		
	Select * From #tmpConceptos 
	Select * From #tmpTraslados 	
	

End 
Go--#SQL 



