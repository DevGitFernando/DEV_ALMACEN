------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_RTP__Descargar_CFDI_Facturas' and xType = 'P' ) 
   Drop Proc spp_FACT_RTP__Descargar_CFDI_Facturas 
Go--#SQL 

Create Proc spp_FACT_RTP__Descargar_CFDI_Facturas 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '22', @IdFarmaciaGenera varchar(4) = '', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 

	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 


	@TipoDeRemision int = 0, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 1, 
	@FolioInicial int = 26848, @FolioFinal int = 26848, 

	@ListadoDeRemisiones varchar(max) = '', 

	@AplicarFiltroFechas int = 0, 
	@FechaInicial varchar(10) = '2019-02-12', @FechaFinal varchar(10) = '2019-02-12'  
)  
As 
Begin 
--Set NoCount On 
Declare 
	@sSql varchar(max),  
	@sFiltro varchar(max),  
	@sFiltro_TipoDeRemision varchar(max),  
	@sFiltro_OrigenInsumo varchar(max),  
	@sFiltro_TipoDeInsumo varchar(max),  
	@sFiltro_Folios varchar(max),  
	@sFiltro_Fechas varchar(max), 
	@sFiltro_ListadoDeRemisiones varchar(max), 
	@sCampoVacio varchar(1) 


	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 

	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @sFiltro_Folios = '' 
	Set @sFiltro_Fechas = '' 
	Set @sFiltro_TipoDeRemision = '' 
	Set @sFiltro_OrigenInsumo = '' 
	Set @sFiltro_TipoDeInsumo = '' 
	Set @sFiltro_ListadoDeRemisiones = '' 
	Set @sCampoVacio = ''

	----------------- Filtros 
	Set @sFiltro = 'Where R.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and R.IdEstado = ' + char(39) + @IdEstado + char(39) + ' and R.IdFarmacia = ' + + char(39) + @IdFarmaciaGenera + char(39)   


	If @IdFuenteFinanciamiento <> '' 
	Begin 
		Set @sFiltro = @sFiltro + ' and R.IdFuenteFinanciamiento = ' + char(39) + right('00000000' + @IdFuenteFinanciamiento, 4) + char(39) + char(13) 

		If @IdFinanciamiento <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' and R.IdFinanciamiento = ' + char(39) + right('00000000' + @IdFinanciamiento, 4) + char(39) + char(13) 
		End 

	End 

            --if (cboFinanciamiento.Data != "0")
            --{
            --    sFiltro += string.Format(" And IdFinanciamiento= '{0}' ", cboFinanciamiento.Data);
            --}

            --if (cboTipoInsumo.Data != "0")
            --{
            --    sFiltro += string.Format(" And TipoDeRemision = '{0}' ", cboTipoInsumo.Data);
            --}


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
	If @AplicarFiltroFechas = 1 
	Begin 
		Set @sFiltro_Fechas = ' and ( convert(varchar(10), R.FechaRemision, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 
	End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if  @TipoDeRemision <> 0 
	Begin 
		If @TipoDeRemision = 1 
			Set @sFiltro_TipoDeRemision = ' And R.TipoDeRemision in ( 1, 3, 4, 5 ) ' + char(13) -- PRODUCTO 

		If @TipoDeRemision = 2 
			Set @sFiltro_TipoDeRemision = ' And R.TipoDeRemision in ( 2, 6 ) ' + char(13) -- SERVICIO  
	End 

	If @SegmentoTipoDeRemision <> 0 
	Begin 
		Set @sFiltro_TipoDeRemision = ' And R.TipoDeRemision in ( ' + cast(@SegmentoTipoDeRemision as varchar(10)) +  ' ) ' + char(13) -- SEGMENTO ESPECIFICO   
	End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if @OrigenDeInsumos <> 0 
		Begin 
			If @OrigenDeInsumos = 1 
			   Set @sFiltro_OrigenInsumo = ' And R.OrigenInsumo = 0  ' + char(13) -- VENTA 

			If @OrigenDeInsumos = 2 
			   Set @sFiltro_OrigenInsumo = ' And R.OrigenInsumo = 1 ' + char(13) -- CONSIGNA  
		End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if @TipoDeInsumo <> 0 
		Begin 
			If @TipoDeInsumo = 1 
			   Set @sFiltro_TipoDeInsumo = ' And R.TipoInsumo = 2  ' + char(13) -- MEDICAMENTO 

			If @TipoDeInsumo = 2 
			   Set @sFiltro_TipoDeInsumo = ' And R.TipoInsumo = 1 ' + char(13) -- MATERIAL DE CURACION 
		End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
	If @AplicarFiltroFolios = 1 
	Begin 
		If @FolioInicial > 0 and @FolioFinal > 0 
			Begin 
				Set @sFiltro_Folios = ' and R.FolioRemision between ' + cast(@FolioInicial as varchar(20)) + ' and '  + cast(@FolioFinal as varchar(20)) + char(13) 
			End 
		Else 
			Begin 
				
				If @FolioInicial > 0 
				Begin 
					Set @sFiltro_Folios = ' and R.FolioRemision >= ' + cast(@FolioInicial as varchar(20)) + char(13) 
				End 

				If @FolioFinal > 0 
				Begin 
					Set @sFiltro_Folios = ' and R.FolioRemision <= ' + cast(@FolioFinal as varchar(20)) + char(13) 
				End 

			End 
	End 

	------	Exec spp_FACT_CFD_GetRemisiones_Descarga 

	------if exists ( Select top 1 * from tmp_ListadoRemisiones_Descargar ) 
	------Begin 
	------	Set @sFiltro_TipoDeRemision = '' 
	------	Set @sFiltro_OrigenInsumo = '' 
	------	Set @sFiltro_TipoDeInsumo = '' 
	------	Set @sFiltro_Folios = '' 
	------	Set @sFiltro_Fechas = '' 
		 
	------	Set @sFiltro_ListadoDeRemisiones = 'and Exists ( Select * From tmp_ListadoRemisiones_Descargar LR (NoLock) Where cast(R.FolioRemision as int) = cast(LR.FolioRemision as int) )  ' + char(13) 
	------End 
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   


	------------------------------------------------------------------------------------- OBTENER INFORMACION 
	Select 
		0 as Descargar, 
		R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.FolioRemision, 
		0 as Identificador, 
		cast(@sCampoVacio as varchar(20)) as Serie, cast(0 as int) as Folio, cast('' as varchar(2)) as StatusFactura, 
		RF.FechaRegistro as FechaDeFacturacion, 
		RF.FolioFacturaElectronica, 
		cast(@sCampoVacio as varchar(100)) as FolioFiscal_SAT, cast(getdate() as datetime) as FechaCertificacion_SAT, 	
		cast(@sCampoVacio as varchar(100)) as IdFarmaciaDispensacion, 
		cast(@sCampoVacio as varchar(100)) as FarmaciaDispensacion, 
		R.FechaRemision, 
		R.TipoDeBeneficiario, R.Referencia_Beneficiario, R.Referencia_NombreBeneficiario, 
		cast('' as varchar(20)) as FechaReceta_Menor, cast('' as varchar(20)) as FechaReceta_Mayor, 
		R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, 
		R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc,   		
		R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, R.Observaciones, R.ObservacionesRemision, R.Status, 


		cast(@sCampoVacio as varchar(500)) as RutaDescarga, 
		cast(@sCampoVacio as varchar(100)) as NombreArchivoDescarga 

	Into #FACT_RPT_Facturacion_Referencias 
	From vw_FACT_Remisiones R (NoLock) 
	Inner Join FACT_Facturas RF (NoLock) 
		On ( R.IdEmpresa = RF.IdEmpresa and R.IdEstado = RF.IdEstado and R.IdFarmacia = RF.IdFarmacia and R.FolioRemision = RF.FolioRemision And RF.Status = 'A' ) 
	Where 1 = 0 

	



	Set @sSql = 
		'Insert Into #FACT_RPT_Facturacion_Referencias ' + char(13) + 
		'( ' + char(13) +  
		'	Descargar, IdEmpresa, IdEstado, IdFarmacia, FolioRemision, Identificador, Serie, Folio, StatusFactura, FechaDeFacturacion, FolioFacturaElectronica, ' + char(13) +  
		'	FolioFiscal_SAT, FechaCertificacion_SAT, IdFarmaciaDispensacion, FarmaciaDispensacion, FechaRemision, ' + char(13) +  
		'	TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, FechaReceta_Menor, FechaReceta_Mayor, ' + char(13) +  
		'	NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, ' + char(13) +    		
		'	SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, RutaDescarga, NombreArchivoDescarga ' + char(13) +  
		') ' + char(13) + 
		'Select  '  + char(13) + 
		'	0 as Descargar, '  + char(13) + 
		'	R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.FolioRemision, '  + char(13) +  
		'	0 as Identificador, '  + char(13) +  
		'	CampoVacio as Serie, 0 as Folio, CampoVacio as StatusFactura, '  + char(13) +  
		'	RF.FechaRegistro as FechaDeFacturacion, '  + char(13) +  
		'	RF.FolioFacturaElectronica, '  + char(13) +  
		'	CampoVacio as FolioFiscal_SAT, cast(getdate() as datetime) as FechaCertificacion_SAT, '  + char(13) + 	
		'	CampoVacio as IdFarmaciaDispensacion, '  + char(13) +  
		'	CampoVacio as FarmaciaDispensacion, '  + char(13) +  
		'	R.FechaRemision, '  + char(13) +  
		'	R.TipoDeBeneficiario, R.Referencia_Beneficiario, R.Referencia_NombreBeneficiario, '  + char(13) +  
		'	CampoVacio as FechaReceta_Menor, CampoVacio as FechaReceta_Mayor, '  + char(13) +  
		'	R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, '  + char(13) +  
		'	R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, R.TipoInsumo, R.TipoDeInsumoDesc, '  + char(13) + 
		'	R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, R.Observaciones, R.ObservacionesRemision, R.Status, '  + char(13) +  
		'	CampoVacio as RutaDescarga, '  + char(13) +  
		'	CampoVacio as NombreArchivoDescarga '  + char(13) +   
		'From vw_FACT_Remisiones R (NoLock) '  + char(13) + 
		'Inner Join FACT_Facturas RF (NoLock) '  + char(13) + 
		'	On ( R.IdEmpresa = RF.IdEmpresa and R.IdEstado = RF.IdEstado and R.IdFarmacia = RF.IdFarmacia and R.FolioRemision = RF.FolioRemision And RF.Status = ' + char(39) + 'A' + char(39) + ' ) '  + char(13) + 
		@sFiltro + @sFiltro_Folios + @sFiltro_Fechas + @sFiltro_TipoDeRemision + @sFiltro_OrigenInsumo + @sFiltro_TipoDeInsumo + @sFiltro_ListadoDeRemisiones 
	Exec ( @sSql ) 
	Print @sSql 

	------------------------------------------------------------------------------------- OBTENER INFORMACION 


	Update F Set Identificador = M.Keyx, Serie = M.Serie, Folio = M.Folio, StatusFactura = M.Status, FechaDeFacturacion = M.FechaRegistro  
	From #FACT_RPT_Facturacion_Referencias F (NoLock) 
	Inner Join FACT_CFD_Documentos_Generados M (NoLock) 
		On ( 
			F.IdEstado = M.IdEstado and 'RM' + F.FolioRemision = M.Referencia 
			-- F.FolioFacturaElectronica = (M.Serie + '-' + cast(M.Folio as varchar(20))) 
			-- and M.Status = 'A' 
			) 

	Update F Set Descargar = 1 
	From #FACT_RPT_Facturacion_Referencias F 
	Where convert(varchar(10), FechaDeFacturacion, 120) between @FechaInicial and  @FechaFinal 

	Update F Set FolioFiscal_SAT = M.uf_FolioSAT, FechaCertificacion_SAT = uf_FechaHoraCerSAT  
	From #FACT_RPT_Facturacion_Referencias F (NoLock) 
	Inner Join FACT_CFDI_XML M (NoLock) On ( F.IdEmpresa = M.IdEmpresa and F.IdEstado = M.IdEstado and F.IdFarmacia = M.IdFarmacia and F.Serie = M.Serie and F.Folio = M.Folio ) 


	Update F Set 
		RutaDescarga = replace(replace((Referencia_Beneficiario + '_' + Referencia_NombreBeneficiario + '\' + IdFuenteFinanciamiento + IdFinanciamiento + '_' + Financiamiento + '\' + 
					   (case when TipoDeRemisionDesc like '%adm%' then 'ADM' else 'INS' end) + '\' + 
					   (case when TipoDeInsumoDesc like '%curacion%' then 'MC' else 'MD' end)
					   ), ' ', '_'), ',', ''), 
		NombreArchivoDescarga = Serie + right('0000000000' + cast(Folio as varchar(10)), 10) + (case when StatusFactura = 'C' then '__CANCELADO' else '' end) 
	From #FACT_RPT_Facturacion_Referencias F (NoLock) 

	----Update F Set RutaDescarga = Replace(Replace(Replace(Replace(Replace(RutaDescarga, 'A', ''), 'E', ''), 'I', ''), 'O', ''), 'U', '') 
	----From #FACT_RPT_Facturacion_Referencias F (NoLock) 


--				spp_FACT_RTP__Descargar_CFDI_Facturas  

	Select Serie, Folio, Identificador, RutaDescarga, NombreArchivoDescarga, FechaDeFacturacion, IdFinanciamiento, Financiamiento, TipoDeRemision, TipoDeRemisionDesc, Total, 
		0 as Procesar, 0 as Procesado, len(RutaDescarga) as Longitud  
	From #FACT_RPT_Facturacion_Referencias 
	-- where Folio >= 145  and Folio <> 148 
	--Where Descargar = 1
	Order by FolioRemision, Serie, Folio 

End 
----Go--#SQL 

----	select top 10 * from FACT_CFD_Documentos_Generados 

	--Select * 
	--From FACT_Remisiones_Detalles D (NoLock) 
	--Where ClaveSSA like '%060.681.0067%' 
