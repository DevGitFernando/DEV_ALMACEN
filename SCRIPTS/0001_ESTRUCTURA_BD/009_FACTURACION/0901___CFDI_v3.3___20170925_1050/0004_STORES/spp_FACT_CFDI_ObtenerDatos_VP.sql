------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_FACT_CFDI_ObtenerDatos_VP' and xType = 'P' ) 
   Drop Proc spp_FACT_CFDI_ObtenerDatos_VP 
Go--#SQL  

Create Proc spp_FACT_CFDI_ObtenerDatos_VP 
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
	@IdFarmacia varchar(4), 	
	
	@Serie varchar(10), 
	@Folio varchar(10), 
	@IdReceptor varchar(20) 
	
	Set @IdEmpresa = '' 
	Set @IdEstado = '' 
	Set @IdFarmacia = '' 		
	Set @Serie = '' 
	Set @Folio = '' 
	Set @IdReceptor = ''

	Select Top 1 @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia = IdFarmacia, @Serie = Serie, @Folio = Folio, @IdReceptor = RFC 
	From FACT_CFD_Documentos_Generados_VP D (NoLock) 
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
		M.Iva as Impuesto1, 0.0 as Impuesto2, 0.0 as IEPS, 0.0 as ISH, 0.0 as RetencionISR, 0.0 as RetencionIVA, 0.0 as Descuentos, 0.0 as Documento, 		
		P.IdFormaDePago, P.Descripcion as FormaDePago, 
		'' as Notas, 1 as TipoCambio, '01' as IdDivisa, 'MXN' as Moneda, 
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
		D.XMLFormaPago, D.XMLCondicionesPago, 
		
		-- D.XMLMetodoPago, 
		-- '' as XMLNumeroCuentaPago, 		
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 1, 1) as XMLMetodoPago, 
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 1, 2) as XMLMetodoPagoDescripcion, 
		dbo.fg_ObtenerMetodosDePago(D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Serie, D.Folio, 1, 3) as XMLNumeroCuentaPago, 	
		
		D.Status
	Into #tmpComprobante 
	From FACT_CFD_Documentos_Generados_VP D (NoLock) 
	Inner Join vw_FACT_CFD_TiposDeDocumentos TD On ( D.IdTipoDocumento = TD.IdTipoDocumento ) 
	Inner Join FACT_CFD_FormasDePago P (NoLock) On ( P.IdFormaDePago = '01' ) --- D.IdFormaDePago ) 	
	--Inner Join CFDI_Divisas M (NoLock) On ( D.IdDivisa = M.IdDivisa ) 
	Inner Join FACT_CFD_Certificados C (NoLock) On ( D.IdEmpresa = C.IdEmpresa ) 
	Left Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, 
			cast(round(sum(SubTotal), 2) as numeric(14,2)) as SubTotal, 
			cast(round(sum(Iva), 2) as numeric(14,2)) as Iva, 
			cast(round(Sum(Total), 2) as numeric(14,2)) as Importe 
		From FACT_CFD_Documentos_Generados_Detalles_VP DC (NoLock) 
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
		RFC, '' as Telefonos, '' as Fax, Email, 
		Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, Status  
	Into #tmpReceptor 
	From FACT_CFD_Clientes (NoLock) 
	Where RFC = @IdReceptor 
	
	
 	
	Select 
		C.IdEmpresa, C.Serie, C.Folio, identity(int, 1, 1) as Partida, C.Identificador as NoIdentificacion, C.DescripcionConcepto as Descripcion, 
		'' as Notas, 
		C.UnidadDeMedida as Unidad, C.Cantidad, 
		C.PrecioUnitario as ValorUnitario, 0.0 as DescuentoPorc, 
		C.PrecioUnitario, C.TasaIva as Impuesto1, 0.0 as Impuesto2, 
		0.0 as RetencionISR, 0.0 as RetencionIVA, 
		C.SubTotal, C.Iva as ImporteIva, C.Total as Importe, '' as Status, 0 as Keyx 
	Into #tmpConceptos 
	From FACT_CFD_Documentos_Generados_Detalles_VP C (NoLock) 
	----Inner Join CFDI_Productos P (NoLock) On ( C.IdConcepto = P.IdProducto ) 
	----Inner Join CFDI_UnidadesDeMedida U (NoLock) On ( P.IdUnidad = U.IdUnidad ) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio 

	 	
	Select Impuesto1 as TasaIva, sum(ImporteIva) as Importe    
	Into #tmpTraslados 
	From #tmpConceptos  	
	Group by Impuesto1 	

		
------------------------------------------ Obtener informacion 	

--	sp_listacolumnas CFDI_Documentos_Conceptos 

--			spp_FACT_CFDI_ObtenerDatos_VP  

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



