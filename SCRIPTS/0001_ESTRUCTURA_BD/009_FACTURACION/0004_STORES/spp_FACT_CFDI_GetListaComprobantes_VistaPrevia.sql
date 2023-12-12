---------------------------------------------------------------------------------------------------------------------------	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_FACT_CFDI_GetListaComprobantes_VP' and xType = 'P' ) 
   Drop Proc spp_FACT_CFDI_GetListaComprobantes_VP  
Go--#SQL  

Create Proc spp_FACT_CFDI_GetListaComprobantes_VP 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0001', 
	@Identificador int = 2   
)
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Select -- D.Keyx, -- D.IdCliente, 
		-- uf_FechaHoraCerSAT, 
		-- dbo.fg_SerieTipoDocumento(D.IdEmpresa, D.Serie) 
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
		( D.Subtotal + D.Iva ) AS Total,		
		'' as Status, 	
		(case when IsNull(X.IdEmpresa, 0) = 0 then 'NO' else 'SI' end) AS SelloEmisor, 
		(case when IsNull(X.IdEmpresa, 0) = 0 then 'NO' 
			else 
				(case when IsNull(X.uf_FolioSAT, '') = '' then 'NO' else 'SI' end) 
		end) as Timbre, 
		(case when IsNull(X.uf_CanceladoSAT, 0) = 0 then 'NO' else 'SI' end) AS uf_CanceladoSAT, 		
		IsNull(X.uf_FolioSAT, '') as uf_FolioSAT, IsNull(X.uf_noCertificadoSAT, '') as uf_noCertificadoSAT, 
		--IsNull(X.uf_FechaHoraCerSAT, '') as uf_FechaHoraCerSAT, 
		--(case when IsNull(X.uf_FechaHoraCerSAT, '') = '' then '' else cast(X.uf_FechaHoraCerSAT as varchar) end) As uf_FechaHoraCerSAT, 
		X.uf_FechaHoraCerSAT,  
		-- C.RFC + '__' + 
		(D.Serie + right(replicate(0, 10) + cast(D.Folio as varchar), 10) ) as NombreFiles, 
		X.uf_Xml_Timbrado, X.uf_xml_Impresion, C.Email as EmailCliente, 
		D.Keyx as Identificador, X.uf_FolioSAT as UUID   
	Into #tmpComprobantes  
	From FACT_CFD_Documentos_Generados_VP D (NoLock) 
	Inner Join FACT_CFD_Clientes C (NoLock) On ( D.RFC = C.RFC ) 
	Left Join FACT_CFDI_XML_VP X (NoLock) 
	On ( D.IdEmpresa = X.IdEmpresa and D.IdEstado = X.IdEstado and D.IdFarmacia = X.IdFarmacia 
		and D.Serie = X.Serie and D.Folio = X.Folio ) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
	Order By D.FechaRegistro Desc 
	
	
	If @Identificador = 0 
	Begin
		Select * 
		From #tmpComprobantes   
	End 
	Else 
	Begin
		Select * 
		From #tmpComprobantes  
		Where Identificador = @Identificador 
	End 
	
		
End 
Go--#SQL 
