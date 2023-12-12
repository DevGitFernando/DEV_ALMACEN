
---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_Remisiones_Manuales' and xType = 'V' ) 
	Drop View vw_FACT_Remisiones_Manuales 
Go--#SQL 

Create View vw_FACT_Remisiones_Manuales 
With Encryption 
As 

	Select 
		
		R.GUID, 
		R.IdEmpresa, R.IdEstado, F.Estado, R.IdFarmaciaGenera as IdFarmacia, R.FolioRemision, 
		0 as PartidaGeneral, 
		R.FechaRemision as FechaRemision, 
		F.IdCliente, F.Cliente, F.IdSubCliente, F.SubCliente,  
		F.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, F.Financiamiento, 
		F.EsParaExcedente, 
		F.Alias, 
		
		IsNull('', '') as IdDocumento, 
		IsNull('', '') as NombreDocumento, 

		IsNull(IA.TipoDeBeneficiario, '') as TipoDeBeneficiario, 
		IsNull(IA.Beneficiario, '') as Referencia_Beneficiario, 
		IsNull(IA.NombreBeneficiario, '') as Referencia_NombreBeneficiario, 


		IsNull(IAN.Referencia_01, '') as Referencia_01, 
		IsNull(IAN.Referencia_02, '') as Referencia_02, 
		IsNull(IAN.Referencia_03, '') as Referencia_03, 
		IsNull(IAN.Referencia_04, '') as Referencia_04, 
		IsNull(IAN.Referencia_05, '') as Referencia_05, 
		IsNull(IAN.Observaciones, '') as Observaciones_Referencias, 
		-- IsNull(IAN.NombreBeneficiario, '') as Referencia_NombreBeneficiario, 

		--R.EsRelacionFacturaPrevia, R.EsRelacionMontos, 
		--R.Serie, R.Folio, 
		--R.EsFacturable as EsFacturable_Base, R.EsFacturada as EsFacturada_Base, 
		--(case when R.EsRelacionFacturaPrevia = 0 then R.EsFacturable else 0 end) as EsFacturable, 
		--(case when R.EsRelacionFacturaPrevia = 0 then R.EsFacturada else 1 end) as EsFacturada, 

		0 as EsRelacionFacturaPrevia, 0 as EsRelacionMontos, 
		'' as Serie, 0 as Folio, 
		0 as EsFacturable_Base, 0 as EsFacturada_Base, 
		0 as EsFacturable, -- (case when R.EsRelacionFacturaPrevia = 0 then R.EsFacturable else 0 end) as EsFacturable, 
		0 as EsFacturada, -- (case when R.EsRelacionFacturaPrevia = 0 then R.EsFacturada else 1 end) as EsFacturada, 



		R.TipoDeRemision, 
		TR.TipoDeRemisionDesc, 
		TR.TipoDeRemisionDesc_Base, 
		--TR.TipoDeRemisionDesc as TipoDeRemisionDesc_X, 


		R.OrigenInsumo, 
		(
			Upper( 
			case when R.OrigenInsumo > 1 then 'Sin especificar' 
				else 
					case when R.OrigenInsumo = 0 then 'VENTA' else 'CONSIGNACI�N' end 
			end) 
		) as OrigenInsumoDesc, 
		 
		R.TipoInsumo, 
		(
			Upper( 
			case when R.TipoInsumo = 0 then 'MEDICAMENTO Y MATERIAL DE CURACI�N' 
				 when R.TipoInsumo = 1 then 'MATERIAL DE CURACI�N' 
				 when R.TipoInsumo = 2 then 'MEDICAMENTO' 
				 else 'NO ESPECIFICADO' 
			end) 
		) as TipoDeInsumoDesc, 	

		--R.EsDeVales, 
		--( 
		--	Upper( 
		--	case when R.EsDeVales = 0 then 'NO' 
		--		 when R.EsDeVales = 1 then 'SI' 
		--		 else 'NO ESPECIFICADO' 
		--	end) 
		--) as EsDeValesDesc, 	
		0 as EsDeVales,
		cast('NO' as varchar(50))  as EsDeValesDesc, 


		--R.TipoDeDispensacion, 
		--( 
		--	Upper( 
		--	case when R.TipoDeDispensacion = 0 then 'DISPENSACI�N Y VALES' 
		--		 when R.TipoDeDispensacion = 1 then 'DISPENSACI�N' 
		--		 when R.TipoDeDispensacion = 2 then 'VALES' 
		--		 else 'NO ESPECIFICADO' 
		--	end) 
		--) as TipoDeDispensacionDesc, 	
		1 as TipoDeDispensacion, 
		cast('DISPENSACI�N' as varchar(50)) as TipoDeDispensacionDesc, 


		--R.TipoDeInsumo_Clasificacion, 
		--( 
		--	Upper( 
		--	case when R.TipoDeInsumo_Clasificacion = 0 then 'GENERAL' 
		--		 when R.TipoDeInsumo_Clasificacion = 1 then 'CONTROLADOS' 
		--		 when R.TipoDeInsumo_Clasificacion = 2 then 'GENERAL-NO CONTROLADOS' 
		--		 else 'NO ESPECIFICADO' 
		--	end) 
		--) as TipoDeInsumo_ClasificacionDesc, 	
		0 as TipoDeInsumo_Clasificacion, 
		cast('GENERAL' as varchar(50)) as TipoDeInsumo_ClasificacionDesc, 

		R.IdPersonalRemision, R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, 
		R.Observaciones, R.Observaciones as ObservacionesRemision, R.Status, 
		R.FechaInicial, R.FechaFinal 
	From FACT_Remisiones_Manuales R (NoLock) 
	--Inner Join FACT_Remisiones___GUID G (NoLock) On ( R.GUID = G.GUID )   
	Inner Join dbo.fg_FACT_TiposDeRemisiones (0) TR ON ( R.IdEmpresa = TR.IdEmpresa and R.IdEstado = TR.IdEstado and R.TipoDeRemision = TR.TipoDeRemision ) 
	Inner Join vw_FACT_FuentesDeFinanciamiento_Detalle F (NoLock) 
		On ( R.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and R.IdFinanciamiento = F.IdFinanciamiento ) 	
	Left Join FACT_Remisiones_InformacionAdicional_Almacenes IA (NoLock) 
		On ( R.IdEmpresa = IA.IdEmpresa and R.IdEstado = IA.IdEstado and R.IdFarmaciaGenera = IA.IdFarmaciaGenera and R.FolioRemision = IA.FolioRemision and 1 = 0 ) 
	Left Join FACT_Remisiones_InformacionAdicional IAN (NoLock) 
		On ( R.IdEmpresa = IAN.IdEmpresa and R.IdEstado = IAN.IdEstado and R.IdFarmaciaGenera = IAN.IdFarmaciaGenera and R.FolioRemision = IAN.FolioRemision and 1 = 0  ) 
	----Left Join FACT_Remisiones_Documentos D (NoLock) 
	----	On ( R.IdEmpresa = D.IdEmpresa and R.IdFarmaciaGenera = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision and
	----		 R.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and R.IdFinanciamiento = D.IdFinanciamiento ) 
	----Left Join dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento(1) DD -- (NoLock) 
	----	On ( D.IdFuenteFinanciamiento = DD.IdFuenteFinanciamiento and D.IdFinanciamiento = DD.IdFinanciamiento and D.IdDocumento = DD.IdDocumento ) 	 


Go--#SQL 


--	select * 	from vw_FACT_Remisiones_Manuales 
	
	
-------------------------------------------------------------------------------------------------------------------------------------------- 
----If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_Remisiones_Manuales_ClavesResumen' and xType = 'V' ) 
----	Drop View vw_FACT_Remisiones_Manuales_ClavesResumen 
----Go--#SQL 

----Create View vw_FACT_Remisiones_Manuales_ClavesResumen  
----With Encryption 
----As 

----	Select 
----		R.GUID, 
----		R.IdEmpresa, R.IdEstado, R.Estado, R.IdFarmacia, R.FolioRemision, R.PartidaGeneral,  
----		C.IdFarmacia as IdFarmaciaDispensacion, F.NombreFarmacia as FarmaciaDispensacion, 
----		R.FechaRemision, R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, 
----		R.IdDocumento, R.NombreDocumento, 
----		C.IdPrograma, DD.Programa, C.IdSubPrograma, DD.SubPrograma, 
----		R.EsFacturada, R.EsFacturable, R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc, R.IdPersonalRemision, 		
----		R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, R.Observaciones, R.ObservacionesRemision, R.Status, 
----		S.IdClaveSSA_Sal as IdClaveSSA, C.ClaveSSA, S.DescripcionClave, S.TipoDeClave, S.TipoDeClaveDescripcion, 
----		C.PrecioLicitado, C.PrecioLicitadoUnitario, C.Cantidad, C.Cantidad_Agrupada, C.TasaIva, 
----		C.SubTotalSinGrabar as SubTotalSinGrabar_Clave, C.SubTotalGrabado as SubTotalGrabado_Clave, C.Iva as Iva_Clave, C.Importe as Importe_Clave, 
----		R.FechaInicial, R.FechaFinal   
----	From vw_FACT_Remisiones_Manuales R (NoLock) 
----	Inner Join FACT_Remisiones_Resumen C (NoLock) 
----		On ( R.IdEmpresa = C.IdEmpresa and R.IdEstado = C.IdEstado and R.IdFarmacia = C.IdFarmaciaGenera and R.FolioRemision = C.FolioRemision ) 
----	Inner Join vw_Programas_SubProgramas DD (NoLock) On ( C.IdPrograma = DD.IdPrograma and C.IdSubPrograma = DD.IdSubPrograma ) 
----	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
----	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.ClaveSSA = S.ClaveSSA ) 

----Go--#SQL 

---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_Remisiones_Manuales_Detalles' and xType = 'V' ) 
	Drop View vw_FACT_Remisiones_Manuales_Detalles 
Go--#SQL 

Create View vw_FACT_Remisiones_Manuales_Detalles  
With Encryption 
As 

	Select 
		-- distinct 
		R.GUID, 
		R.IdEmpresa, R.IdEstado, R.Estado, R.IdFarmacia, R.FolioRemision, R.PartidaGeneral,  

		R.EsRelacionFacturaPrevia, R.EsRelacionMontos, 
		R.Serie, R.Folio, 

		-- IsNull(FE.FolioFacturaElectronica, '') as FolioFacturaElectronica,  
		dbo.fg_FACT_GetFacturaElectronica_Remision(R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.FolioRemision) as FolioFacturaElectronica, 
		C.IdFarmacia as IdFarmaciaDispensacion, F.NombreFarmacia as FarmaciaDispensacion, 

		C.IdBeneficiario, 
		-- R.Referencia_Beneficiario, 
		C.IdBeneficiario as Referencia_Beneficiario, 
		R.Referencia_NombreBeneficiario, 
		C.FolioVenta, R.FechaInicial, R.FechaFinal,   

		IsNull(I.FechaReceta, '') as FechaReceta, 
		IsNull(I.NumReceta, C.Referencia_06) as NumReceta,  

		R.FechaRemision, 
		R.IdCliente, R.Cliente, R.IdSubCliente, R.SubCliente, 
		R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, R.EsParaExcedente, R.Alias, 
		R.IdDocumento, R.NombreDocumento, 
		
		C.IdPrograma, DD.Programa, C.IdSubPrograma, DD.SubPrograma, 
		R.EsFacturada, R.EsFacturable, R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc, R.IdPersonalRemision, 		
		R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, R.Observaciones, R.ObservacionesRemision, R.Status, 
		C.IdSubFarmacia, 
		C.IdProducto, C.CodigoEAN, S.Descripcion, C.ClaveLote, 
		--C.Referencia_01, 
		S.IdClaveSSA_Sal as IdClaveSSA, C.ClaveSSA, S.DescripcionClave, S.Presentacion_ClaveSSA, S.TipoDeClave, S.TipoDeClaveDescripcion, 
		C.PrecioLicitado, C.PrecioLicitadoUnitario, C.Cantidad, C.Cantidad_Agrupada, C.TasaIva, 
		C.SubTotalSinGrabar as SubTotalSinGrabar_Clave, C.SubTotalGrabado as SubTotalGrabado_Clave, C.Iva as Iva_Clave, C.Importe as Importe_Clave,
		
		C.Referencia_01, C.Referencia_02, C.Referencia_03, C.Referencia_04, C.Referencia_05, C.Referencia_06,  

		'' as CampoDeControl  
				 
	From vw_FACT_Remisiones_Manuales R (NoLock) 
	Inner Join FACT_Remisiones_Manuales_Detalles C (NoLock) 
		On ( R.IdEmpresa = C.IdEmpresa and R.IdEstado = C.IdEstado and R.IdFarmacia = C.IdFarmaciaGenera and R.FolioRemision = C.FolioRemision ) 
	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	Left Join VentasInformacionAdicional I (NoLock) 
		On ( C.IdEmpresa = I.IdEmpresa and C.IdEstado = I.IdEstado and C.IdFarmacia = I.IdFarmacia and C.FolioVenta = I.FolioVenta ) 
	Left Join vw_Programas_SubProgramas DD (NoLock) On ( C.IdPrograma = DD.IdPrograma and C.IdSubPrograma = DD.IdSubPrograma ) 
	Left Join vw_Productos_CodigoEAN S (NoLock) On ( C.IdProducto = S.IdProducto and C.CodigoEAN = S.CodigoEAN	)  


Go--#SQL 





-------------------------------------------------------------------------------------------------------------------------------------------- 
----If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_Remisiones_Manuales_Informacion' and xType = 'V' ) 
----	Drop View vw_FACT_Remisiones_Manuales_Informacion 
----Go--#SQL 

----Create View vw_FACT_Remisiones_Manuales_Informacion  
----With Encryption 
----As 

----	Select 
----		R.GUID, 
----		R.IdEmpresa, R.IdEstado, R.Estado, R.IdFarmacia, R.FolioRemision, R.PartidaGeneral, R.FechaRemision, 
----		IsNull(FE.FolioFacturaElectronica, '') as FolioFacturaElectronica, 
----		C.IdFarmacia as IdFarmaciaDispensacion, F.NombreFarmacia as FarmaciaDispensacion, 
----		R.Referencia_Beneficiario, R.Referencia_NombreBeneficiario, 

----		convert(varchar(10), min(V.FechaRegistro), 120) as FechaRegistro_Menor, 
----		convert(varchar(10), max(V.FechaRegistro), 120) as FechaRegistro_Mayor, 
----		convert(varchar(10), min(I.FechaReceta), 120) as FechaReceta_Menor, 
----		convert(varchar(10), max(I.FechaReceta), 120) as FechaReceta_Mayor, 

----		R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, 
----		R.IdDocumento, R.NombreDocumento, 
----		R.EsFacturada, R.EsFacturable, R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc, R.IdPersonalRemision, 		
----		-- R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, 
----		R.Observaciones, R.ObservacionesRemision, R.Status, 
----		-- S.IdClaveSSA_Sal as IdClaveSSA, C.ClaveSSA, S.DescripcionClave, S.TipoDeClave, S.TipoDeClaveDescripcion, 
----		-- C.PrecioLicitado, C.PrecioLicitadoUnitario, C.Cantidad, C.Cantidad_Agrupada, C.TasaIva, 
----		sum(C.SubTotalSinGrabar) as SubTotalSinGrabar, 
----		sum(C.SubTotalGrabado) as SubTotalGrabado, sum(C.Iva) as Iva, sum(C.Importe) as Total  
----	From vw_FACT_Remisiones_Manuales R (NoLock) 
----	Inner Join FACT_Remisiones_Detalles C (NoLock) 
----		On ( R.IdEmpresa = C.IdEmpresa and R.IdEstado = C.IdEstado and R.IdFarmacia = C.IdFarmaciaGenera and R.FolioRemision = C.FolioRemision ) 
----	Inner Join VentasEnc V (NoLock) 
----		On ( C.IdEmpresa = V.IdEmpresa and C.IdEstado = V.IdEstado and C.IdFarmacia = V.IdFarmacia and C.FolioVenta = V.FolioVenta ) 
----	Inner Join VentasInformacionAdicional I (NoLock) 
----		On ( C.IdEmpresa = I.IdEmpresa and C.IdEstado = I.IdEstado and C.IdFarmacia = I.IdFarmacia and C.FolioVenta = I.FolioVenta ) 
----	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
----	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.ClaveSSA = S.ClaveSSA ) 
----	Left Join FACT_Facturas FE (NoLock) 
----		On ( R.IdEmpresa = FE.IdEmpresa and R.IdEstado = FE.IdEstado and R.IdFarmacia = FE.IdFarmacia and R.FolioRemision = FE.FolioRemision and FE.Status = 'A' )
----	--- Where R.FolioRemision = 600 
----	Group by 
----		R.GUID, 
----		R.IdEmpresa, R.IdEstado, R.Estado, R.IdFarmacia, R.FolioRemision, R.PartidaGeneral, R.FechaRemision, 
----		IsNull(FE.FolioFacturaElectronica, ''), 
----		C.IdFarmacia, F.NombreFarmacia, R.Referencia_Beneficiario, R.Referencia_NombreBeneficiario, 
----		R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, R.IdDocumento, R.NombreDocumento, 
----		R.EsFacturada, R.EsFacturable, R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc, R.IdPersonalRemision,  		
----		R.Observaciones, R.ObservacionesRemision, R.Status 

----Go--#SQL 



-------------------------------------------------------------------------------------------------------------------------------------------- 
----If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_Remisiones_Manuales_Informacion_Resumen' and xType = 'V' ) 
----	Drop View vw_FACT_Remisiones_Manuales_Informacion_Resumen 
----Go--#SQL 

----Create View vw_FACT_Remisiones_Manuales_Informacion_Resumen  
----With Encryption 
----As 

----	----- PRUEBAS 20181214.1623  
	 
----	Select 
----		R.GUID, 
----		R.IdEmpresa, R.IdEstado, R.Estado, R.IdFarmacia, R.FolioRemision, R.PartidaGeneral, R.FechaRemision, 
----		C.IdFarmacia as IdFarmaciaDispensacion, F.NombreFarmacia as FarmaciaDispensacion, 
----		R.Referencia_Beneficiario, R.Referencia_NombreBeneficiario, 

----		R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, 
----		R.IdDocumento, R.NombreDocumento, 
----		R.EsFacturada, R.EsFacturable, R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc, R.IdPersonalRemision, 		
----		R.Observaciones, R.ObservacionesRemision, R.Status, 
----		sum(C.SubTotalSinGrabar) as SubTotalSinGrabar, 
----		sum(C.SubTotalGrabado) as SubTotalGrabado, sum(C.Iva) as Iva, sum(C.Importe) as Total  
----	From vw_FACT_Remisiones_Manuales R (NoLock) 
----	Inner Join FACT_Remisiones_Detalles C (NoLock) 
----		On ( R.IdEmpresa = C.IdEmpresa and R.IdEstado = C.IdEstado and R.IdFarmacia = C.IdFarmaciaGenera and R.FolioRemision = C.FolioRemision ) 
----	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
----	Group by 
----		R.GUID, 
----		R.IdEmpresa, R.IdEstado, R.Estado, R.IdFarmacia, R.FolioRemision, R.PartidaGeneral, R.FechaRemision,  
----		C.IdFarmacia, F.NombreFarmacia, R.Referencia_Beneficiario, R.Referencia_NombreBeneficiario, 
----		R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, R.IdDocumento, R.NombreDocumento, 
----		R.EsFacturada, R.EsFacturable, R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc, R.IdPersonalRemision,  		
----		R.Observaciones, R.ObservacionesRemision, R.Status 
	

----Go--#SQL 
	

