---------------------------------------------------------------------------------------------------------------------------	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFDI_GetListaComprobantes' and xType = 'P' ) 
   Drop Proc spp_CFDI_GetListaComprobantes  
Go--#SQL  

--		sp_listacolumnas__Stores spp_CFDI_GetListaComprobantes , 1  

--		Exec spp_CFDI_GetListaComprobantes '00000001', 2014, 2, 0, '59'  


Create Proc spp_CFDI_GetListaComprobantes 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0001', 
	@Identificador int = 2   
)
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD	

	Select 
		
		'' as Documento, 
		(D.Serie + right(replicate(0, 10) + cast(D.Folio as varchar), 10) ) as Folio, 
		(right(replicate(0, 10) + cast(D.Folio as varchar), 10) ) as FolioBase, 			
		convert(varchar(10), D.FechaRegistro, 120) as FechaEmision, 
		C.Nombre as NombreFiscal, 
		D.SubTotal, 0 as Descuentos, D.Iva as Impuesto1, 
		0 as IEPS, 0 as ISH, 
		cast(0 as numeric(14,4)) as OtrosTraslados, 
		0 as RetencionISR, 0 as RetencionIVA, 
		cast(0 as numeric(14,4)) as OtrasRetenciones, 
		-- ( D.Subtotal - D.Descuentos + D.Impuesto1 + D.Impuesto2 + D.IEPS + D.ISH - D.RetencionISR - D.RetencionIVA ) AS Total, 
		cast(( E.Subtotal + E.Iva ) as numeric(14, 4) ) AS Total,		
		'' as Status, 
		D.Status as StatusDocto, 	
		D.Status as StatusDocumento, 
		D.Observaciones_01, Observaciones_02, Observaciones_03, 
		D.Referencia, 	
		D.FechaCancelacion as FechaCancelacionCFDI, 
		
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
		X.uf_Xml_Timbrado, X.uf_xml_Impresion, ''  as EmailCliente, 
		D.Keyx as Identificador, X.uf_FolioSAT as UUID, 
	
		E.SubTotal as SubTotal_Aux, E.Iva, E.Importe, E.Importe as Total_Aux, 
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
		'' as Financiamiento  
		  
		-- D.Observaciones_01, D.Observaciones_02, D.Observaciones_03, D.Referencia      
	Into #tmpComprobantes  
	From CFDI_Documentos D (NoLock) 
	Inner Join CFDI_Clientes C (NoLock) On ( D.RFC = C.RFC ) 
	Left Join CFDI_XML X (NoLock) 
	On ( D.IdEmpresa = X.IdEmpresa and D.IdEstado = X.IdEstado and D.IdFarmacia = X.IdFarmacia 
		and D.Serie = X.Serie and D.Folio = X.Folio ) 
	Left Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, 
			cast(round(sum(SubTotal), 2) as numeric(14,2)) as SubTotal, 
			cast(round(sum(Iva), 2) as numeric(14,2)) as Iva, 
			cast(round(Sum(Importe), 2) as numeric(14,2)) as Importe 
		From CFDI_Documentos_Conceptos DC (NoLock) 
		Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio 
	) E On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Serie = D.Serie And E.Folio = D.Folio )

	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Keyx = @Identificador  
	Order By D.FechaRegistro Desc 



------	Salida Final 		
	Select * 
	From #tmpComprobantes   		
		
End 
Go--#SQL 



