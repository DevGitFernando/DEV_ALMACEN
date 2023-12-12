
--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento_Detalle_Documentos' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento_Detalle_Documentos
Go--#SQL

Create View vw_FACT_FuentesDeFinanciamiento_Detalle_Documentos 
With Encryption 
As 

	Select	
		M.IdEstado, M.Estado, 
		M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, 
		M.NumeroDeContrato,  
		D.Prioridad, 

		M.IdFuenteFinanciamiento, 
		D.IdFinanciamiento, D.Descripcion as Financiamiento, 
		DF.IdDocumento, DF.NombreDocumento,  

		DF.IdFuenteFinanciamiento_Relacionado, DF.IdFinanciamiento_Relacionado, DF.IdDocumento_Relacionado, 
		cast('' as varchar(max)) as NombreDocumento_Relacionado, 
		DF.EsRelacionado, 
		
		DF.OrigenDeInsumo, (case when DF.OrigenDeInsumo = 1 then 'VENTA' else 'CONSIGNA' end) as OrigenDeInsumoDescripcion, 
		DF.TipoDeDocumento, (case when DF.TipoDeDocumento = 1 then 'PRODUCTO' else 'SERVICIO' end) as TipoDeDocumentoDescripcion, 
		DF.TipoDeInsumo, 
		(
			case when DF.TipoDeInsumo = 0 then 'MEDICAMENTO Y MATERIAL DE CURACIÓN'
				    when DF.TipoDeInsumo = 1 then 'MATERIAL DE CURACIÓN'  
				    when DF.TipoDeInsumo = 2 then 'MEDICAMENTO'  
				else '' end
		) as TipoDeInsumoDescripcion, 
		
		DF.ValorNominal, DF.ImporteAplicado, DF.ImporteRestante, DF.AplicaFarmacia, DF.AplicaAlmacen, DF.EsProgramaEspecial, DF.Status as Status_Documento, 

		DF.TipoDeBeneficiario, 
		(
			case when DF.TipoDeBeneficiario = 1 then 'GENERAL'
				    when DF.TipoDeBeneficiario = 2 then 'HOSPITALES'  
				    when DF.TipoDeBeneficiario = 3 then 'JURISDICCIONES'  
				else 'NO ESPECIFICADO' end
		) as TipoDeBeneficiarioDescripcion  

	From vw_FACT_FuentesDeFinanciamiento M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento )
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_Documentos DF (NoLock) 
		On ( D.IdFuenteFinanciamiento = DF.IdFuenteFinanciamiento and D.IdFinanciamiento = DF.IdFinanciamiento )  
--	Left Join dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento( DF.IdFuenteFinanciamiento, DF.IdFinanciamiento, DF.IdDocumento ) GF 
--	Left Join dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento( '', '', '' ) GF 
	Left Join dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento(1) GF 
		On ( DF.IdFuenteFinanciamiento = GF.IdFuenteFinanciamiento and DF.IdFinanciamiento = GF.IdFinanciamiento and DF.IdDocumento = GF.IdDocumento )  


Go--#SQL 


	-- sp_listacolumnas FACT_Fuentes_De_Financiamiento_Detalles_Documentos 
