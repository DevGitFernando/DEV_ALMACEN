---------------------------------------------------------------------------------------------------------------------------	
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_CFDI_GetListaComprobantes' and xType = 'P' ) 
   Drop Proc spp_FACT_CFDI_GetListaComprobantes  
Go--#SQL  

--		sp_listacolumnas__Stores spp_FACT_CFDI_GetListaComprobantes , 1  

/* 

Exec spp_FACT_CFDI_GetListaComprobantes 
	@IdEmpresa = '001', @IdEstado = '11', @IdFarmacia = '0001', 
	@Identificador = 5054    

*/ 

Create Proc spp_FACT_CFDI_GetListaComprobantes 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0001', 
	@Identificador int = 5041    
)
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD	

Declare 
	@sIdEstablecimiento varchar(6),  
	@sIdEstablecimiento_Receptor varchar(6),  
	@sNombreDelEstablecimiento varchar(500),  
	@sDomicilioDelEstablecimiento varchar(1000),  
	@sNombreDelEstablecimiento_Receptor varchar(500),  
	@sDomicilioDelEstablecimiento_Receptor varchar(1000) 


	Set @sIdEstablecimiento = '' 
	Set @sIdEstablecimiento_Receptor = '' 	
	Set @sNombreDelEstablecimiento = '' 
	Set @sDomicilioDelEstablecimiento = '' 
	Set @sNombreDelEstablecimiento_Receptor = '' 
	Set @sDomicilioDelEstablecimiento_Receptor = '' 


	Select @sIdEstablecimiento = IdEstablecimiento, @sIdEstablecimiento_Receptor = IdEstablecimiento_Receptor 
	From FACT_CFD_Documentos_Generados D (NoLock) 
	Inner Join FACT_CFD_Clientes C (NoLock) On ( D.RFC = C.RFC ) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Keyx = @Identificador  

	Set @sIdEstablecimiento = IsNull(@sIdEstablecimiento, '') 
	Set @sIdEstablecimiento_Receptor = IsNull(@sIdEstablecimiento_Receptor, '') 



	--Select @sNombreDelEstablecimiento = Valor  
	--From Net_CFG_Parametros_Facturacion (NoLock) 
	--Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ArbolModulo = 'FACT' and NombreParametro = 'Establecimiento_Nombre' 

	--Select @sDomicilioDelEstablecimiento = Valor 
	--From Net_CFG_Parametros_Facturacion (NoLock) 
	--Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ArbolModulo = 'FACT' and NombreParametro = 'Establecimiento_Domicilio' 

	--		spp_FACT_CFDI_GetListaComprobantes 

	Select @sNombreDelEstablecimiento = NombreEstablecimiento,   
		 @sDomicilioDelEstablecimiento = Calle + ' No. ' + NoExterior + 
												  (case when NoInterior <> '' then 'Int. ' +  NoInterior + ', ' else ', ' end) + 
												  Colonia + ', ' + 'C.P. ' + CodigoPostal + ', ' + Municipio + ', ' + Estado + ', ' + Pais 
	From FACT_CFD_Establecimientos (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento = @sIdEstablecimiento 


	Select @sNombreDelEstablecimiento_Receptor = NombreEstablecimiento, 
		 @sDomicilioDelEstablecimiento_Receptor = Calle + ' No. ' + NoExterior + 
												  (case when NoInterior <> '' then 'Int. ' +  NoInterior + ', ' else ', ' end) + 
												  Colonia + ', ' + 'C.P. ' + CodigoPostal + ', ' + Municipio + ', ' + Estado + ', ' + Pais 
	From FACT_CFD_Establecimientos_Receptor (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento = @sIdEstablecimiento_Receptor 


	----Select * 
	----From FACT_CFD_Establecimientos (NoLock) 
	----Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento = @sIdEstablecimiento 

	----Select * 
	----From FACT_CFD_Establecimientos_Receptor (NoLock) 
	----Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento = @sIdEstablecimiento_Receptor 

	------		 NombreEstablecimiento, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, Status



	Set @sNombreDelEstablecimiento = upper(IsNull(@sNombreDelEstablecimiento, '')) 
	Set @sDomicilioDelEstablecimiento = upper(IsNull(@sDomicilioDelEstablecimiento, '')) 
	Set @sNombreDelEstablecimiento_Receptor = upper(IsNull(@sNombreDelEstablecimiento_Receptor, '')) 
	Set @sDomicilioDelEstablecimiento_Receptor = upper(IsNull(@sDomicilioDelEstablecimiento_Receptor, ''))  


	Select -- D.Keyx, -- D.IdCliente, 
		-- uf_FechaHoraCerSAT, 
		-- dbo.fg_SerieTipoDocumento(D.IdEmpresa, D.Serie) 
		'' as Documento, 
		(D.Serie + right(replicate(0, 10) + cast(D.Folio as varchar), 10) ) as Folio, 
		(right(replicate(0, 10) + cast(D.Folio as varchar), 10) ) as FolioBase, 			
		convert(varchar(10), D.FechaRegistro, 120) as FechaEmision, 
		C.Nombre as NombreFiscal, 
		0 as IEPS, 0 as ISH, 
		cast(0 as numeric(14,4)) as OtrosTraslados, 
		0 as RetencionISR, 0 as RetencionIVA, 
		cast(0 as numeric(14,4)) as OtrasRetenciones, 
		-- ( D.Subtotal - D.Descuentos + D.Impuesto1 + D.Impuesto2 + D.IEPS + D.ISH - D.RetencionISR - D.RetencionIVA ) AS Total, 
		
		0 as Descuentos, 
		--D.SubTotal, D.Iva as Impuesto1, 	
		--(D.Subtotal + D.Iva) as Total, 

		( case when D.IdTipoDocumento = '007' then D.Subtotal else E.Subtotal end) as SubTotal, 
		( case when D.IdTipoDocumento = '007' then D.Iva else E.Iva end) as Impuesto1, 
		( case when D.IdTipoDocumento = '007' then D.Importe + D.Iva else E.Importe end) as Total, 
				
		
		'' as Status, 
		D.Status as StatusDocto, 	
		D.Status as StatusDocumento, 	
		(case when IsNull(X.IdEmpresa, 0) = 0 then 'NO' else 'SI' end) AS SelloEmisor, 
		(case when IsNull(X.IdEmpresa, 0) = 0 then 'NO' 
			else 
				(case when IsNull(X.uf_FolioSAT, '') = '' then 'NO' else 'SI' end) 
		end) as Timbre, 
		(case when IsNull(X.uf_CanceladoSAT, 0) = 0 then 'NO' else 'SI' end) AS uf_CanceladoSAT, 		
		(case when D.Status = 'A' then '' else convert(varchar(20), D.FechaCancelacion, 120) end) AS FechaCancelacionDocumento,  

		IsNull(X.uf_FolioSAT, '') as uf_FolioSAT, IsNull(X.uf_noCertificadoSAT, '') as uf_noCertificadoSAT, 
		X.uf_FechaHoraCerSAT,  
		(D.Serie + right(replicate(0, 10) + cast(D.Folio as varchar), 10) ) as NombreFiles, 
		X.uf_Xml_Timbrado, X.uf_xml_Impresion, C.Email as EmailCliente, 
		D.Keyx as Identificador, X.uf_FolioSAT as UUID, 
	
		--E.SubTotal as SubTotal_Aux, E.Iva, E.Importe, E.Importe as Total_Aux, 

		( case when D.IdTipoDocumento = '007' then D.Subtotal else E.Subtotal end) as SubTotal_Aux, 
		( case when D.IdTipoDocumento = '007' then D.Iva else E.Iva end) as Iva, 
		( case when D.IdTipoDocumento = '007' then D.Importe else E.Importe end) as Importe, 
		( case when D.IdTipoDocumento = '007' then D.Importe else E.Importe end) as Total_Aux, 


		D.TipoDocumento, 
		(
			case When D.TipoDocumento = 1 Then 'Venta' 
				 When D.TipoDocumento = 2 Then 'Administración' 
			else '' 
			End 
		) as TipoDocumentoDescripcion, 
		D.TipoInsumo, 
		(
			case When D.TipoInsumo = 1 Then 'Material de curación' 
				 When D.TipoInsumo = 2 Then 'Medicamento' 
			else '' 
			End 
		) as TipoInsumoDescripcion,
		
		'' as RubroFinanciamento, 		 		
		'' as RubroFinanciamiento, 
		IsNull(FF.Financiamiento, '') Financiamiento,    
		D.Observaciones_01, D.Observaciones_02, D.Observaciones_03, 
		D.Referencia, D.Referencia_02, D.Referencia_03, D.Referencia_04, D.Referencia_05, 
		
		D.IdEstablecimiento, D.IdEstablecimiento_Receptor, 

		cast(@sNombreDelEstablecimiento as varchar(500)) as NombreDelEstablecimiento, 
		cast(@sDomicilioDelEstablecimiento as varchar(1000)) as DomicilioDelEstablecimiento, 

		cast(@sNombreDelEstablecimiento_Receptor as varchar(500)) as NombreDelEstablecimiento_Receptor, 
		cast(@sDomicilioDelEstablecimiento_Receptor as varchar(1000)) as DomicilioDelEstablecimiento_Receptor

	Into #tmpComprobantes  
	From FACT_CFD_Documentos_Generados D (NoLock) 
	Inner Join FACT_CFD_Clientes C (NoLock) On ( D.RFC = C.RFC ) 
	Left Join vw_FACT_FuentesDeFinanciamiento_Detalle FF (NoLock) 
		On ( D.IdRubroFinanciamiento = FF.IdFuenteFinanciamiento and D.IdFuenteFinanciamiento = FF.IdFinanciamiento ) 
	Left Join FACT_CFDI_XML X (NoLock) 
	On ( D.IdEmpresa = X.IdEmpresa and D.IdEstado = X.IdEstado and D.IdFarmacia = X.IdFarmacia 
		and D.Serie = X.Serie and D.Folio = X.Folio ) 
	Left Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, 
			cast(round(sum(SubTotal), 2) as numeric(14,2)) as SubTotal, 
			cast(round(sum(Iva), 2) as numeric(14,2)) as Iva, 
			cast(round(Sum(Total), 2) as numeric(14,2)) as Importe 
		From FACT_CFD_Documentos_Generados_Detalles DC (NoLock) 
		Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio 
	) E On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Serie = D.Serie And E.Folio = D.Folio )

	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Keyx = @Identificador  
	Order By D.FechaRegistro Desc 

	---		select top 1 * from FACT_CFD_Documentos_Generados 
	---		select top 1 * from vw_FACT_FuentesDeFinanciamiento_Detalle 


------	Salida Final 		
	Select * 
	From #tmpComprobantes   		
		
End 
Go--#SQL 



