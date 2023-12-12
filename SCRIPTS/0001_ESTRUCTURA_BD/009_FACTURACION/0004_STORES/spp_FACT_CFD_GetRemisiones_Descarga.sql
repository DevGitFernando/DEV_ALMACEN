---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_CFD_GetRemisiones_Descarga' and xType = 'P' )
    Drop Proc spp_FACT_CFD_GetRemisiones_Descarga
Go--#SQL 
  
Create Proc spp_FACT_CFD_GetRemisiones_Descarga 
( 
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '11', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 

	@TipoDeRemision int = 0, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 0, 
	@FolioInicial int = 36215, @FolioFinal int = 36220, 

	@ListadoDeRemisiones varchar(max) = '', 

	@AplicarFiltroFechas int = 1, 
	@FechaInicial varchar(10) = '2022-08-01', @FechaFinal varchar(10) = '2022-08-18', 
	
	@EsValidacion int = 0  

) 
With Encryption		
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max),  
	@sFiltro varchar(max),  
	@sFiltro_TipoDeRemision varchar(max),  
	@sFiltro_OrigenInsumo varchar(max),  
	@sFiltro_TipoDeInsumo varchar(max),  
	@sFiltro_Folios varchar(max),  
	@sFiltro_Fechas varchar(max), 
	@sFiltro_ListadoDeRemisiones varchar(max),  	 
	@sFiltro_Facturable varchar(max) 	 

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
	Set @sFiltro_Facturable = '' 

	----------------- Filtros 
	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdFarmacia = ' + + char(39) + @IdFarmaciaGenera + char(39)   


	If @IdFuenteFinanciamiento <> '' 
	Begin 
		Set @sFiltro = @sFiltro + ' and IdFuenteFinanciamiento = ' + char(39) + right('00000000' + @IdFuenteFinanciamiento, 4) + char(39) + char(13) 

		If @IdFinanciamiento <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' and IdFinanciamiento = ' + char(39) + right('00000000' + @IdFinanciamiento, 4) + char(39) + char(13) 
		End 

	End 

	If @EsValidacion = 1 
	Begin 
		Set @sFiltro_Facturable = ' and ( EsRelacionDocumento = 0 and EsRelacionFacturaPrevia = 0 and EsFacturable = 0 and Status = ' + char(39) + 'A' + char(39) + ' ) ' 
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
		Set @sFiltro_Fechas = ' and ( convert(varchar(10), FechaRemision, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 
	End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if  @TipoDeRemision <> 0 
	Begin 
		If @TipoDeRemision = 1 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 1, 3, 4, 5 ) ' + char(13) -- PRODUCTO 

		If @TipoDeRemision = 2 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 2, 6 ) ' + char(13) -- SERVICIO  
	End 

	If @SegmentoTipoDeRemision <> 0 
	Begin 
		Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( ' + cast(@SegmentoTipoDeRemision as varchar(10)) +  ' ) ' + char(13) -- SEGMENTO ESPECIFICO   
	End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if @OrigenDeInsumos <> 0 
		Begin 
			If @OrigenDeInsumos = 1 
			   Set @sFiltro_OrigenInsumo = ' And OrigenInsumo = 0  ' + char(13) -- VENTA 

			If @OrigenDeInsumos = 2 
			   Set @sFiltro_OrigenInsumo = ' And OrigenInsumo = 1 ' + char(13) -- CONSIGNA  
		End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if @TipoDeInsumo <> 0 
		Begin 
			If @TipoDeInsumo = 1 
			   Set @sFiltro_TipoDeInsumo = ' And TipoInsumo = 2  ' + char(13) -- MEDICAMENTO 

			If @TipoDeInsumo = 2 
			   Set @sFiltro_TipoDeInsumo = ' And TipoInsumo = 1 ' + char(13) -- MATERIAL DE CURACION 
		End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
	If @AplicarFiltroFolios = 1 
	Begin 
		If @FolioInicial > 0 and @FolioFinal > 0 
			Begin 
				Set @sFiltro_Folios = ' and FolioRemision between ' + cast(@FolioInicial as varchar(20)) + ' and '  + cast(@FolioFinal as varchar(20)) + char(13) 
			End 
		Else 
			Begin 
				
				If @FolioInicial > 0 
				Begin 
					Set @sFiltro_Folios = ' and FolioRemision >= ' + cast(@FolioInicial as varchar(20)) + char(13) 
				End 

				If @FolioFinal > 0 
				Begin 
					Set @sFiltro_Folios = ' and FolioRemision <= ' + cast(@FolioFinal as varchar(20)) + char(13) 
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



--------------------------------------------- 
	Select  
		GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
		IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, 
		EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  
		cast('' as varchar(100)) as IdFarmaciaDispensacion, cast('' as varchar(100))  as FarmaciaDispensacion 
	Into #vw_FACT_Remisiones
	From vw_FACT_Remisiones 
	Where 1 = 0 
		----IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera 
		----and EsFacturable = 1 and EsFacturada = 0 and Status = 'A' 
		----and EsRelacionFacturaPrevia = 0 



	Set @sSql = 
		'Insert Into #vw_FACT_Remisiones ' + char(13) + 
		'( ' + 
		'	GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  ' + char(13) + 
		'	IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio,  ' + char(13) + 
		'	EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc,  ' + char(13) + 
		'	EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  ' + char(13) +
		'	IdFarmaciaDispensacion, FarmaciaDispensacion '  + char(13) +
		') ' + 
		'Select ' + 
		'	GUID, IdEmpresa, IdEstado, Estado, IdFarmacia, FolioRemision, PartidaGeneral, FechaRemision, IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  ' + char(13) + 
		'	IdDocumento, NombreDocumento, TipoDeBeneficiario, Referencia_Beneficiario, Referencia_NombreBeneficiario, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio,  ' + char(13) + 
		'	EsFacturable_Base, EsFacturada_Base, EsFacturable, EsFacturada, TipoDeRemision, TipoDeRemisionDesc, TipoDeRemisionDesc_Base, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc,  ' + char(13) + 
		'	EsDeVales, EsDeValesDesc, TipoDeDispensacion, TipoDeDispensacionDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, FechaInicial, FechaFinal,  ' + char(13) +
		'	GUID as IdFarmaciaDispensacion, GUID as FarmaciaDispensacion '  + char(13) + 
		'From vw_FACT_Remisiones R (NoLock) '  + char(13) + 
		@sFiltro + @sFiltro_Folios + @sFiltro_Fechas + @sFiltro_TipoDeRemision + @sFiltro_OrigenInsumo + @sFiltro_TipoDeInsumo + @sFiltro_ListadoDeRemisiones + @sFiltro_Facturable  
	Exec ( @sSql ) 
	Print @sSql 



	--Update R Set IdFarmaciaDispensacion = 
	--IsNull
	--( 
	--	( 
	--		Select top 1 IdFarmacia 
	--		From FACT_Remisiones_Detalles D (NoLock) 
	--		where R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision 
	--	) 
	--, '') 
	--From #vw_FACT_Remisiones R (NoLock) 



	Update R Set IdFarmaciaDispensacion = D.IdFarmacia 
	From #vw_FACT_Remisiones R (NoLock) 
	Inner Join FACT_Remisiones_Detalles D (NoLock) 
			On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision ) 



	Update R Set FarmaciaDispensacion = F.Farmacia 
	From #vw_FACT_Remisiones R (NoLock)  
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmaciaDispensacion = F.IdFarmacia )


---		spp_FACT_CFD_GetRemisiones_Descarga  


--	sp_listacolumnas vw_FACT_Remisiones 


---------------------	SALIDA FINAL 	
	Select 
		FolioRemision, convert(varchar(10), FechaRemision, 120) as FechaRemision, 
		IdFuenteFinanciamiento, 
		IdFinanciamiento, Financiamiento, TipoDeRemision, TipoDeRemisionDesc, 
		Total As Importe, 0 as Procesar, 0 as Procesado, IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario 
	From #vw_FACT_Remisiones 
	Order by FolioRemision  



End 
Go--#SQL 



