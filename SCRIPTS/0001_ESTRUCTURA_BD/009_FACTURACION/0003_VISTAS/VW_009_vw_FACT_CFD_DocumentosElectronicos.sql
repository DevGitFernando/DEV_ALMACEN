--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_TiposDeDocumentos' and xType = 'V' ) 
	Drop View vw_FACT_CFD_TiposDeDocumentos 
Go--#SQL 
 	
Create View vw_FACT_CFD_TiposDeDocumentos 
With Encryption 
As 

	Select D.IdTipoDocumento, upper(D.NombreDocumento) as NombreDocumento, D.Alias, D.Status, 
		D.TipoDeComprobante, 
		( 
		case when D.TipoDeComprobante = 0 Then 'Sin espeficicar' 
		     when D.TipoDeComprobante = 1 Then 'Ingreso' 
		     when D.TipoDeComprobante = 2 Then 'Egreso' 
		     when D.TipoDeComprobante = 3 Then 'Traslado' 	     
		end 
		) as TipoDeComprobanteDescripcion 
	From FACT_CFD_TiposDeDocumentos D (NoLock) 
	
	
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_DocumentosElectronicos_MetodosDePago' and xType = 'V' ) 
	Drop View vw_FACT_CFD_DocumentosElectronicos_MetodosDePago 
Go--#SQL 

Create View vw_FACT_CFD_DocumentosElectronicos_MetodosDePago 
With Encryption 
As 

	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FechaRegistro, 
		E.Serie, E.Folio, 
		E.XMLFormaPago as FormaDePago, 
		IsNull(M.Descripcion, E.XMLMetodoPago) as MetodoDePago, 
		IsNull(MP.Referencia, '') as ReferenciaDePago 		
	From FACT_CFD_Documentos_Generados E (NoLock) 
	Left Join FACT_CFD_Documentos_Generados_MetodosDePago MP (NoLock) 
		On ( E.IdEmpresa = MP.IdEmpresa and E.IdEstado = MP.IdEstado and E.IdFarmacia = MP.IdFarmacia and E.Serie = MP.Serie And E.Folio = MP.Folio )  	
	Left Join FACT_CFD_MetodosPago M (NoLock) On ( MP.IdMetodoPago = M.IdMetodoPago )

Go--#SQL 


--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_DocumentosElectronicos' and xType = 'V' ) 
	Drop View vw_FACT_CFD_DocumentosElectronicos 
Go--#SQL 
 	
Create View vw_FACT_CFD_DocumentosElectronicos 
With Encryption 
As 

	Select  
		E.Keyx as Identificador, 
		E.IdEmpresa, EM.Nombre as Empresa, 
		E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, 
		E.FechaRegistro, E.IdTipoDocumento, T.NombreDocumento as TipoDeDocumento, 
		E.Serie, E.Folio, 
		E.EsRelacionConRemisiones, (case when E.EsRelacionConRemisiones = 1 then 'SI' else 'NO' end) as EsRelacionConRemisionesDescripcion, 
		
		--D.SubTotal, D.Iva, D.Importe, D.Importe as Total, 
		
		( case when E.IdTipoDocumento = '007' then E.SubTotal else D.SubTotal end) as SubTotal, 
		( case when E.IdTipoDocumento = '007' then E.Iva else D.Iva end) as Iva, 
		( case when E.IdTipoDocumento = '007' then E.Importe else D.Importe end) as Importe, 
		( case when E.IdTipoDocumento = '007' then E.Importe else D.Importe end) as Total, 


		--cast( (case when IsNull(DR.uf_FolioSAT, '0') = '0' then '0' else '1' end) as bit) as TieneSustitucionDeCFDI, 
		--DR.CFDI_Relacionado,
		--DR.Serie as x, 
		--DR.Folio as Y, 
		--DR.Serie_Relacionada, 
		--DR.Folio_Relacionado, 

		--cast( (case when IsNull(DR.uf_FolioSAT, '0') = '0' then '0' else '1' end) as bit) as TieneSustitucionDeCFDI, 
		dbo.fg_FACT_CFDI_ValidarRelacion( E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Serie, E.Folio  ) As TieneSustitucionDeCFDI, 
		--(case when E.Status = 'A' Then E.CFDI_Relacionado else '' end) as CFDI_Relacionado,
		--(case when E.Status = 'A' Then E.Serie_Relacionada else '' end) as Serie_Relacionada, 
		--(case when E.Status = 'A' Then cast(E.Folio_Relacionado as varchar(20)) else '' end) as Folio_Relacionado, 
		--(case when E.Status = 'A' then (case when E.CFDI_Relacionado = '' then '' else (Serie_Relacionada + ' - ' + cast(Folio_Relacionado as varchar(20))) end)  else 0 end) as SerieFolio_Relacionado, 

		E.CFDI_Relacionado, 
		E.Serie_Relacionada, 
		cast(E.Folio_Relacionado as varchar(20)) as Folio_Relacionado, 
		(Serie_Relacionada + ' - ' + cast(Folio_Relacionado as varchar(20))) as SerieFolio_Relacionado, 

		E.TienePagoRelacionado, 
		D.RegistrosDetalle, 
		E.RFC, E.NombreReceptor, 
		E.Status as StatusDocto,  
		(case when E.Status = 'A' Then 1 Else 0 End) as StatusDoctoAux, 
		(case when E.Status = 'A' Then 'Activo' Else 'Cancelado' End) as StatusDocumento, 	
		
		E.Observaciones_01, E.Observaciones_02, E.Observaciones_03, 
		E.Referencia, E.Referencia_02, E.Referencia_03, E.Referencia_04, E.Referencia_05, 
		
		E.TipoDocumento, 
		(
			----case When E.TipoDocumento = 1 Then 'Venta' 
			----	 When E.TipoDocumento = 2 Then 'Administración' 
			----else '' 
			----End 

			Upper( 
				case when E.TipoDocumento = 1 then 'INSUMOS' 
					 when E.TipoDocumento = 3 then 'INSUMOS INCREMENTO'
					 when E.TipoDocumento = 4 then 'INSUMOS VENTA DIRECTA' 
					 when E.TipoDocumento = 5 then 'INSUMOS INCREMENTO VENTA DIRECTA' 
					 when E.TipoDocumento = 2 then 'ADMINISTRACIÓN' 
					 when E.TipoDocumento = 6 then 'ADMINISTRACIÓN VENTA DIRECTA' 
				else 
					'NO ESPECIFICADO' end 
				) 

		) as TipoDocumentoDescripcion, 
		E.TipoInsumo, 
		( 
			case --When E.TipoInsumo = 2 Then 'Medicamento' 
				 When E.TipoInsumo = 0 Then 'General (Medicamento y Material de curación)' 				
				 When E.TipoInsumo = 1 Then 'Material de curación' 
				 When E.TipoInsumo = 2 Then 'Medicamento' 
			else '' 
			End 
		) as TipoInsumoDescripcion, 

		E.IdRubroFinanciamiento as RubroFinanciamento, 		 		
		
		E.IdRubroFinanciamiento as RubroFinanciamiento, 
		(IsNull(FF.Cliente, '') + ' -- ' + IsNull(FF.NumeroDeContrato, '') ) as FuenteFinanciamiento,

		E.IdFuenteFinanciamiento, E.IdFuenteFinanciamiento as IdFuenteFinanciamiento_Segmento, 
		IsNull(FF.Financiamiento, '') as Financiamiento, 
		
		E.IdPersonalEmite, 
		IsNull((Select NombreCompleto  
				From vw_Personal P (NoLock) 
				where P.IdEstado = E.IdEstado and P.IdFarmacia = E.IdFarmacia and P.IdPersonal = E.IdPersonalEmite ),'') as PersonalEmite, 				
		E.IdPersonalCancela, 
		IsNull((Select NombreCompleto  
				From vw_Personal P (NoLock) 
				where P.IdEstado = E.IdEstado and P.IdFarmacia = E.IdFarmacia and P.IdPersonal = E.IdPersonalCancela ),'') as PersonalCancela, 
		(case when E.Status = 'A' Then '' Else convert(varchar(30), E.FechaCancelacion, 120) End) as FechaCancelacion, 
		E.MotivoCancelacion, 
		IsNull(X.IdPAC, '001') as IdPAC, IsNull(P.NombrePAC, '') as NombrePAC, 
		IsNull(X.uf_CanceladoSAT, 0) as CanceladoSAT, 
		IsNull(X.uf_FolioSAT, '') as UUID,    
		IsNull(X.uf_SelloDigital, '') as Sello_Digital,    
		MP.FormaDePago, MP.MetodoDePago, MP.ReferenciaDePago 
	From FACT_CFD_Documentos_Generados E (NoLock) 
	Inner Join FACT_CFD_TiposDeDocumentos T (NoLock) On ( E.IdTipoDocumento = T.IdTipoDocumento ) 
	Inner Join CatEmpresas EM (NoLock) On ( E.IdEmpresa = EM.IdEmpresa ) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_FACT_CFD_DocumentosElectronicos_MetodosDePago MP (NoLock) 
		On ( E.IdEmpresa = MP.IdEmpresa and E.IdEstado = MP.IdEstado and E.IdFarmacia = MP.IdFarmacia and E.Serie = MP.Serie And E.Folio = MP.Folio )  	
	Left Join vw_FACT_FuentesDeFinanciamiento_Detalle FF (NoLock) On ( E.IdRubroFinanciamiento = FF.IdFuenteFinanciamiento and E.IdFuenteFinanciamiento = FF.IdFinanciamiento ) 
	
	Left Join FACT_CFDI_XML DR (NoLock) 
		--On ( E.IdEmpresa = DR.IdEmpresa and E.IdEstado = DR.IdEstado and E.IdFarmacia = DR.IdFarmacia and E.Serie_Relacionada = DR.Serie and E.Folio_Relacionado = DR.Folio ) 
		On ( E.IdEmpresa = DR.IdEmpresa and E.IdEstado = DR.IdEstado and E.IdFarmacia = DR.IdFarmacia and E.Serie_Relacionada = DR.Serie and E.Folio_Relacionado = DR.Folio ) 

	Left Join FACT_CFDI_XML X (NoLock) 
		On ( E.IdEmpresa = X.IdEmpresa and E.IdEstado = X.IdEstado and E.IdFarmacia = X.IdFarmacia and E.Serie = X.Serie And E.Folio = X.Folio )  
	Left Join FACT_CFDI_PACs P ( NoLock) On ( X.IdPAC = P.IdPAC )	
	Left Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, 
			cast(round(sum(SubTotal), 2) as numeric(14,2)) as SubTotal, 
			cast(round(sum(Iva), 2) as numeric(14,2)) as Iva, 
			cast(round(Sum(Total), 2) as numeric(14,2)) as Importe 
			, count(*) as RegistrosDetalle 
		From FACT_CFD_Documentos_Generados_Detalles DC (NoLock) 
		Where DC.SAT_ClaveProducto_Servicio <> '' and DC.SAT_UnidadDeMedida <> '' 
		Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio 
	) D On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Serie = D.Serie And E.Folio = D.Folio )


Go--#SQL 



--		select * from  vw_FACT_CFD_DocumentosElectronicos 

--		update FACT_CFD_Documentos_Generados Set Importe = 154.00 * 1.15 where Folio = 7 

--		select * from FACT_CFD_TiposDeDocumentos 

--	sp_listacolumnas vw_FACT_CFD_Sucursales_Series 
	