-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetInformacion_Remision_vs_Factura_ToPDF_28_SST' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetInformacion_Remision_vs_Factura_ToPDF_28_SST
Go--#SQL 

Create Proc spp_FACT_RTP__GetInformacion_Remision_vs_Factura_ToPDF_28_SST 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '19338', 
	@Serie_Facturacion varchar(10) = 'IME', @Folio_Facturacion int = 765  
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sNombreEmpresa varchar(500), 
	@sNombreEstado varchar(500), 
	@dDate datetime, 
	@sPeriodo_Descripcion varchar(100), 
	@sFechaRemision_Impresion varchar(100), 
	@sIdFarmacia_Dispensacion varchar(10) 

	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  
	Set @sNombreEmpresa = '' 
	Set @sNombreEstado = '' 
	Set @dDate = getdate() 
	Set @sPeriodo_Descripcion = '' 
	Set @sFechaRemision_Impresion = '' 
	Set @sIdFarmacia_Dispensacion = '' 


	Select @sNombreEmpresa = Nombre From CatEmpresas (NoLock) Where IdEmpresa = @IdEmpresa 
	Select top 1 @sNombreEstado = Nombre From CatEstados (NoLock) Where IdEstado = @IdEstado 


--------------------------------------------------------------------------------- 
	Select Top 1 @sIdFarmacia_Dispensacion = IdFarmacia 
	From FACT_Remisiones_Detalles E (NoLock) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision 
		--and E.Cantidad > 0 and E.Cantidad_Agrupada > 0  


--------------------------------------------------------------------------------- 
-------------------------- INFORMACION DE LA FACTURA 

	Select * 
	Into #tmp_FacturaDetalles 
	From FACT_CFD_Documentos_Generados_Detalles (NoLock) 
	Where Serie = @Serie_Facturacion and Folio = @Folio_Facturacion 
		--and Clave = '060.168.6611' 
-------------------------- INFORMACION DE LA FACTURA 
--------------------------------------------------------------------------------- 


--------------------------------------------------------------------------------- 
	Select 
		-- cast('' as varchar(500)) as Estado, 
		-- *
		GUID, 
		IdEmpresa, 
		cast('' as varchar(500)) as Empresa, 		 
		IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		
		--IdFarmaciaDispensacion, FarmaciaDispensacion, 
		cast((case when @sIdFarmacia_Dispensacion = '' then IdFarmaciaDispensacion else @sIdFarmacia_Dispensacion end) as varchar(20)) as IdFarmaciaDispensacion, 
		cast((case when @sIdFarmacia_Dispensacion = '' then FarmaciaDispensacion else '' end) as varchar(500)) as FarmaciaDispensacion, 	

		Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		--FolioVenta, 
		FechaInicial, FechaFinal, 
		cast('' as varchar(100)) as Periodo_Descripcion, 
		cast('' as varchar(20)) as FechaReceta, cast('' as varchar(20)) as NumReceta, 
		FechaRemision, 
		--cast('' as varchar(100)) as FechaRemision_Auxiliar, 
		FechaRemision as FechaRemision_Auxiliar, 
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, Alias, IdDocumento, NombreDocumento, 
		cast('' as varchar(20)) as IdPrograma, cast('' as varchar(20)) as Programa, 
		cast('' as varchar(20)) as IdSubPrograma, cast('' as varchar(20)) as SubPrograma, 
		EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		IdPersonalRemision, 
		cast(0 as numeric(14, 4)) as SubTotalSinGrabar, 
		cast(0 as numeric(14, 4)) as SubTotalGrabado, 
		cast(0 as numeric(14, 4)) as Iva, 
		cast(0 as numeric(14, 4)) as Importe, 
		Observaciones, ObservacionesRemision, Status, 
		cast('' as varchar(20)) as IdSubFarmacia, cast('' as varchar(20)) as IdProducto, cast('' as varchar(20)) as CodigoEAN, 
		cast('' as varchar(20)) as Descripcion, cast('' as varchar(20)) as ClaveLote, cast('' as varchar(20)) as Referencia_01, 
		IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
		PrecioLicitado, PrecioLicitadoUnitario, 
		sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, TasaIva, 
		cast(0 as numeric(14, 4)) as SubTotalSinGrabar_Clave, 
		cast(0 as numeric(14, 4)) as SubTotalGrabado_Clave, 
		cast(0 as numeric(14, 4)) as Iva_Clave, 
		cast(0 as numeric(14, 4)) as Importe_Clave, 
		'' as CampoDeControl  		   
		 
		, cast('' as varchar(max)) as InformacionComplemento   
		, cast('' as varchar(max)) as PartidaPresupuestariaDescripcion  
		, cast(ClaveSSA as varchar(max)) as ClaveSSA_Mascara  
		, cast(DescripcionClave as varchar(max)) as Descripcion_Mascara  
		, cast(Presentacion_ClaveSSA as varchar(max)) as Presentacion_Mascara  
		, cast(Referencia_01 as varchar(max)) as AIE   
		, cast('' as varchar(max)) as Causes_NoCauses  
		, getdate() as FechaImpresion  
	Into #vw_FACT_Remisiones_Detalles
	From vw_FACT_Remisiones_Detalles E (NoLock) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision 
		and E.Cantidad > 0 and E.Cantidad_Agrupada > 0  
		--and E.ClaveSSA = '060.456.0409' 
	Group by 
		GUID, 
		IdEmpresa, 
		IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		--IdFarmaciaDispensacion, FarmaciaDispensacion, 
		cast((case when @sIdFarmacia_Dispensacion = '' then IdFarmaciaDispensacion else @sIdFarmacia_Dispensacion end) as varchar(20)), 
		cast((case when @sIdFarmacia_Dispensacion = '' then FarmaciaDispensacion else '' end) as varchar(500)), 
		Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		--FolioVenta, 
		FechaInicial, FechaFinal, 
		FechaRemision, 
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, Alias, IdDocumento, NombreDocumento, 
		EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		IdPersonalRemision, 
		Observaciones, ObservacionesRemision, Status, 
		IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, Referencia_01, 
		PrecioLicitado, PrecioLicitadoUnitario, TasaIva 


	----Select 
	----	GUID, IdEmpresa, Empresa, 		 
	----	IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		
	----	IdFarmaciaDispensacion, FarmaciaDispensacion, 
		
	----	Referencia_Beneficiario, Referencia_NombreBeneficiario, 
	----	FechaInicial, FechaFinal, Periodo_Descripcion, FechaReceta, NumReceta, FechaRemision, FechaRemision_Auxiliar, 
	----	IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, 
	----	IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, Alias, IdDocumento, NombreDocumento, 
	----	IdPrograma, Programa, IdSubPrograma, SubPrograma, 
	----	EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
	----	IdPersonalRemision, 
			
	----	sum(SubTotalSinGrabar) as SubTotalSinGrabar, 
	----	sum(SubTotalGrabado) as SubTotalGrabado, 
	----	sum(Iva) as Iva, 
	----	sum(Importe) as Importe, 
	----	Observaciones, ObservacionesRemision, Status, 
			
	----	IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, Referencia_01, 
	----	IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, 
			
	----	sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
	----	TasaIva, 
	----	sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, 
	----	sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
	----	sum(Iva_Clave) as Iva_Clave, 
	----	sum(Importe_Clave) as Importe_Clave, 
	----	CampoDeControl  		   
		 
	----	, InformacionComplemento   
	----	, PartidaPresupuestariaDescripcion  
	----	, ClaveSSA_Mascara  
	----	, Descripcion_Mascara  
	----	, Presentacion_Mascara  
	----	, AIE   
	----	, Causes_NoCauses  
	----	, getdate() as FechaImpresion 
	----Into #vw_FACT_Remisiones_Detalles 	
	----From 
	----( 
	----	Select 
	----		-- cast('' as varchar(500)) as Estado, 
	----		-- *
	----		GUID, 
	----		IdEmpresa, 
	----		cast('' as varchar(500)) as Empresa, 		 
	----		IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		
	----		--IdFarmaciaDispensacion, FarmaciaDispensacion, 
	----		cast((case when @sIdFarmacia_Dispensacion = '' then IdFarmaciaDispensacion else @sIdFarmacia_Dispensacion end) as varchar(20)) as IdFarmaciaDispensacion, 
	----		cast((case when @sIdFarmacia_Dispensacion = '' then FarmaciaDispensacion else '' end) as varchar(500)) as FarmaciaDispensacion, 		
		
	----		Referencia_Beneficiario, Referencia_NombreBeneficiario, 
	----		--FolioVenta, 
	----		FechaInicial, FechaFinal, 
	----		cast('' as varchar(100)) as Periodo_Descripcion, 
	----		cast('' as varchar(20)) as FechaReceta, cast('' as varchar(20)) as NumReceta, 
	----		FechaRemision, 
	----		--cast('' as varchar(100)) as FechaRemision_Auxiliar, 
	----		FechaRemision as FechaRemision_Auxiliar, 
	----		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, 
	----		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, Alias, IdDocumento, NombreDocumento, 
	----		cast('' as varchar(20)) as IdPrograma, cast('' as varchar(20)) as Programa, 
	----		cast('' as varchar(20)) as IdSubPrograma, cast('' as varchar(20)) as SubPrograma, 
	----		EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
	----		IdPersonalRemision, 
	----		cast(0 as numeric(14, 4)) as SubTotalSinGrabar, 
	----		cast(0 as numeric(14, 4)) as SubTotalGrabado, 
	----		cast(0 as numeric(14, 4)) as Iva, 
	----		cast(0 as numeric(14, 4)) as Importe, 
	----		Observaciones, ObservacionesRemision, Status, 
	----		cast('' as varchar(20)) as IdSubFarmacia, cast('' as varchar(20)) as IdProducto, cast('' as varchar(20)) as CodigoEAN, 
	----		cast('' as varchar(20)) as Descripcion, cast('' as varchar(20)) as ClaveLote, cast('' as varchar(20)) as Referencia_01, 
	----		IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
	----		PrecioLicitado, PrecioLicitadoUnitario, 
	----		sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, TasaIva, 
	----		cast(0 as numeric(14, 4)) as SubTotalSinGrabar_Clave, 
	----		cast(0 as numeric(14, 4)) as SubTotalGrabado_Clave, 
	----		cast(0 as numeric(14, 4)) as Iva_Clave, 
	----		cast(0 as numeric(14, 4)) as Importe_Clave, 
	----		'' as CampoDeControl  		   
		 
	----		, cast('' as varchar(max)) as InformacionComplemento   
	----		, cast('' as varchar(max)) as PartidaPresupuestariaDescripcion  
	----		, cast(ClaveSSA as varchar(max)) as ClaveSSA_Mascara  
	----		, cast(DescripcionClave as varchar(max)) as Descripcion_Mascara  
	----		, cast(Presentacion_ClaveSSA as varchar(max)) as Presentacion_Mascara  
	----		, cast(Referencia_01 as varchar(max)) as AIE   
	----		, cast('' as varchar(max)) as Causes_NoCauses  
	----		, getdate() as FechaImpresion  
	----	From vw_FACT_Remisiones_Detalles E (NoLock) 
	----	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision 
	----		and E.Cantidad > 0 and E.Cantidad_Agrupada > 0  
	----		and E.ClaveSSA = '060.168.6611' 
	----	Group by 
	----		GUID, 
	----		IdEmpresa, 
	----		IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
	----		--IdFarmaciaDispensacion, FarmaciaDispensacion, 
	----		cast((case when @sIdFarmacia_Dispensacion = '' then IdFarmaciaDispensacion else @sIdFarmacia_Dispensacion end) as varchar(20)), 
	----		cast((case when @sIdFarmacia_Dispensacion = '' then FarmaciaDispensacion else '' end) as varchar(500)), 
	----		Referencia_Beneficiario, Referencia_NombreBeneficiario, 
	----		--FolioVenta, 
	----		FechaInicial, FechaFinal, 
	----		FechaRemision, 
	----		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, 
	----		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, Alias, IdDocumento, NombreDocumento, 
	----		EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
	----		IdPersonalRemision, 
	----		Observaciones, ObservacionesRemision, Status, 
	----		IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, Referencia_01, 
	----		PrecioLicitado, PrecioLicitadoUnitario, TasaIva 
	----) TX 
	----Group by 
	----	GUID, IdEmpresa, Empresa, 		 
	----	IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		
	----	IdFarmaciaDispensacion, FarmaciaDispensacion, 
		
	----	Referencia_Beneficiario, Referencia_NombreBeneficiario, 
	----	FechaInicial, FechaFinal, Periodo_Descripcion, FechaReceta, NumReceta, FechaRemision, FechaRemision_Auxiliar, 
	----	IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, 
	----	IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, Alias, IdDocumento, NombreDocumento, 
	----	IdPrograma, Programa, IdSubPrograma, SubPrograma, 
	----	EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
	----	IdPersonalRemision, 
			
	----	----sum(SubTotalSinGrabar) as SubTotalSinGrabar, 
	----	----sum(SubTotalGrabado) as SubTotalGrabado, 
	----	----sum(Iva) as Iva, 
	----	----sum(Importe) as Importe, 
	----	Observaciones, ObservacionesRemision, Status, 
			
	----	IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, Referencia_01, 
	----	IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, 
			
	----	----sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
	----	TasaIva, 
	----	----sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, 
	----	----sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
	----	----sum(Iva_Clave) as Iva_Clave, 
	----	----sum(Importe_Clave) as Importe_Clave, 
	----	CampoDeControl  		   
		 
	----	, InformacionComplemento   
	----	, PartidaPresupuestariaDescripcion  
	----	, ClaveSSA_Mascara  
	----	, Descripcion_Mascara  
	----	, Presentacion_Mascara  
	----	, AIE   
	----	, Causes_NoCauses  




	--------------- Determinar la fecha inicial del periodo 
	Select @dDate = min(FechaInicial) From #vw_FACT_Remisiones_Detalles (NoLock) 
	Set @sPeriodo_Descripcion = upper(dbo.fg_NombresDeMes(@dDate)) + ' ' + cast(year(@dDate) as varchar(10)) 


	Select @dDate = max(FechaFinal) From #vw_FACT_Remisiones_Detalles (NoLock) 
	Set @sFechaRemision_Impresion = cast(year(@dDate) as varchar(4)) + '-' + right('0000' + cast(month(@dDate) as varchar(4)), 2) + 
		'-' + right('0000' + cast(dbo.fg_NumeroDiasAñoMes(year(@dDate), month(@dDate)) as varchar(4)), 2)   
	--------------- Determinar la fecha inicial del periodo 


	----------------------------------------- CALCULAR IMPORTES DE LA REMISION CON EL MISMO ESQUEMA QUE LA FACTURA 

--		spp_FACT_RTP__GetInformacion_Remision_vs_Factura_ToPDF_28_SST


	Update D Set 	
		SubTotalSinGrabar_Clave = dbo.fg_PRCS_Redondear(round(PrecioLicitado * Cantidad_Agrupada, 2, 1), 2, 0) 
		--SubTotalSinGrabar_Clave = dbo.fg_PRCS_Redondear(PrecioLicitado * Cantidad_Agrupada, 2, 0) 
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA = 0 

	Update D Set 	
		--SubTotalGrabado_Clave = dbo.fg_PRCS_Redondear(round(PrecioLicitado * Cantidad_Agrupada, 2, 1), 2, 0) 
		SubTotalGrabado_Clave = dbo.fg_PRCS_Redondear(PrecioLicitado * Cantidad_Agrupada, 2, 0) 
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0 


	Update D Set 
			Iva_Clave = dbo.fg_PRCS_Redondear((SubTotalGrabado_Clave * ( TasaIva / 100.00 )), 4, 0) 
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva_Clave = round( Iva_Clave, 3, 1 ) 
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva_Clave = round( Iva_Clave, 2, 1 ) 
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0 


	------- SubTotal, Importe 
	Update D Set 
		-- SubTotal = (D.SubTotalSinGrabar + D.SubTotalGrabado), 
		Importe_Clave = (D.SubTotalSinGrabar_Clave + D.SubTotalGrabado_Clave + D.Iva_Clave)
	From #vw_FACT_Remisiones_Detalles D (NoLock) 

	----------------------------------------- CALCUAR IMPORTES DE LA REMISION CON EL MISMO ESQUEMA QUE LA FACTURA 


	---		Exec spp_FACT_RTP__GetInformacion_Remision_vs_Factura_ToPDF_28_SST 

	Update E Set DescripcionClave = DescripcionClave + ' -- ' + Presentacion_ClaveSSA, 
		Periodo_Descripcion = @sPeriodo_Descripcion, 
		FechaRemision = @sFechaRemision_Impresion  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set FarmaciaDispensacion = F.NombreOficial 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Farmacias_CLUES F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and F.Status = 'A' ) 


	Update E Set PartidaPresupuestariaDescripcion = I.Observaciones 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Remisiones_InformacionAdicional I On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmaciaGenera and E.FolioRemision = I.FolioRemision ) 

	Update E Set ClaveSSA_Mascara = M.Mascara, Descripcion_Mascara = M.DescripcionMascara, Presentacion_Mascara = M.Presentacion 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join vw_ClaveSSA_Mascara M (NoLock) On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente  and E.ClaveSSA = M.ClaveSSA ) 

	Update E Set DescripcionClave = Descripcion_Mascara + ' -- ' + Presentacion_Mascara 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	------- Modificacion 20190422.1045 
	Update E Set Descripcion_Mascara = Descripcion_Mascara + ' -- ' + Presentacion_Mascara 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 



	---		Exec spp_FACT_RTP__GetInformacion_Remision_vs_Factura_ToPDF_28_SST 

	Update E Set 
		-- AIE = F.Referencia_01, 
		Causes_NoCauses = F.Referencia_04   
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F (NoLock) 
		On ( E.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and E.IdFinanciamiento = F.IdFinanciamiento 
			and E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and E.ClaveSSA = F.ClaveSSA ) 



	Update E Set InformacionComplemento = 'REMISIÓN PARA COBRO'
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Where EsRelacionFacturaPrevia = 0  

	Update E Set InformacionComplemento = 'COMPROBACIÓN DE FACTURA', FolioFacturaElectronica = Serie + ' - ' +  cast(Folio as varchar(10)) 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Where EsRelacionFacturaPrevia = 1 

--------------------------------------------------------------------------------- 

	
	--------------------------- TEMPORAL PARA COBRO EXTRAORDINARIO 
	Update R Set FechaImpresion = dateadd(second, 5, FechaRemision) 
	From #vw_FACT_Remisiones_Detalles R 
	Where 
		FolioREmision between 8526 and 9601    
	--------------------------- TEMPORAL PARA COBRO EXTRAORDINARIO 




------------------------------------- SALIDA FINAL 

	Select 
		sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, sum(Iva_Clave) as Iva_Clave, sum(Importe_Clave) as Importe_Clave  
	From #vw_FACT_Remisiones_Detalles 


	Select * 
	From #vw_FACT_Remisiones_Detalles 


	select * 
	from #tmp_FacturaDetalles 


	---		spp_FACT_RTP__GetInformacion_Remision_vs_Factura_ToPDF_28_SST 


	Select 
		R.ClaveSSA, 
		R.DescripcionClave, 
		R.TasaIVA, 
		R.PrecioLicitado, 
		R.SubTotalGrabado_Clave, 
		R.Iva_Clave, 
		R.Importe_Clave, 
		F.TasaIVA, 
		F.PrecioUnitario, 
		F.SubTotal, F.Iva, F.Total, 
		(F.Total - R.Importe_Clave) as Diferencia   
	From 
	( 
		Select 
			ClaveSSA, 
			DescripcionClave, 
			TasaIVA, 
			PrecioLicitado, 
			sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
			sum(Iva_Clave) as Iva_Clave, 
			sum(Importe_Clave) as Importe_Clave  
		From #vw_FACT_Remisiones_Detalles 
		Group by 
			ClaveSSA, 
			DescripcionClave, 
			TasaIVA, 
			PrecioLicitado 
	) R 
	Inner Join #tmp_FacturaDetalles F On ( R.ClaveSSA = F.Clave ) 
	where F.Total <> R.Importe_Clave 
	order by DescripcionClave 


End 
Go--#SQL
