---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_CFD_GetRemisionesFacturables' and xType = 'P' )
    Drop Proc spp_FACT_CFD_GetRemisionesFacturables
Go--#SQL 
  
Create Proc spp_FACT_CFD_GetRemisionesFacturables 
( 
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	
	@TipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 0, 
	@FolioInicial int = 0, @FolioFinal int = 0, 

	@AplicarFiltroFechas int = 2, 
	@FechaInicial varchar(10) = '2022-04-01', @FechaFinal varchar(10) = '2022-04-30'  

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
	@sFiltro_Fechas varchar(max) 


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



	----------------- Filtros 
	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdFarmacia = ' + + char(39) + @IdFarmaciaGenera + char(39) 
	Set @sFiltro = @sFiltro + char(13) + ' and EsFacturable = 1 and EsFacturada = 0 and Status = ' + char(39) + 'A' + char(39) + ' and ( EsRelacionFacturaPrevia = 0 and EsRelacionDocumento = 0  ) ' + char(13) 

	----If @IdFuenteFinanciamiento <> '' 
	----Begin 
	----	Set @sFiltro = @sFiltro + ' and IdFuenteFinanciamiento = ' + char(39) + right('00000000' + @IdFuenteFinanciamiento, 4) + char(39) + char(13) 

	----	If @IdFinanciamiento <> '' 
	----	Begin 
	----		Set @sFiltro = @sFiltro + ' and IdFinanciamiento = ' + char(39) + right('00000000' + @IdFinanciamiento, 4) + char(39) + char(13) 
	----	End 

	----End 

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
			   Set @sFiltro_OrigenInsumo = ' And TipoInsumo = 2  ' + char(13) -- MEDICAMENTO 

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
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   

--------------------------------------------- 
	Select 
		FolioRemision, Financiamiento, (SubTotalSinGrabar + SubTotalGrabado) as SubTotal, IVA, Total, 
		'' as Serie, '' as Folio, 0 as Facturar, 0 as Procesar, '' as EstatusProceso, 
		identity(int, 1, 1) as Orden  
	Into #vw_FACT_Remisiones
	From vw_FACT_Remisiones 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera 
		and EsFacturable = 1 and EsFacturada = 0 and Status = 'A' 
		and EsRelacionFacturaPrevia = 0 and EsRelacionDocumento = 0 
		and 1 = 0 
	Order by FolioRemision 

	Set @sSql = 
		'Insert Into #vw_FACT_Remisiones ' + char(13) + 
		'( ' + char(13) +  
		'	FolioRemision, Financiamiento, SubTotal, IVA, Total, Serie, Folio, Facturar, Procesar, EstatusProceso ' + char(13) + 
		') ' + char(13) +  
		'Select ' + 
		'	FolioRemision, Financiamiento, (SubTotalSinGrabar + SubTotalGrabado) as SubTotal, IVA, Total,  ' + char(13) + 
		'' + char(39) + '' + char(39) + ' as Serie, ' + char(39) + '' + char(39) + ' as Folio, 0 as Facturar, 0 as Procesar, ' + char(39) + '' + char(39) + ' as EstatusProceso ' + char(13) + 
		'From vw_FACT_Remisiones (NoLock) '  + char(13) + 
		@sFiltro + @sFiltro_Folios + @sFiltro_Fechas + @sFiltro_TipoDeRemision + @sFiltro_OrigenInsumo + @sFiltro_TipoDeInsumo + '  ' + 
		'Order by TipoDeRemision, FolioRemision '
	Exec ( @sSql ) 
	Print @sSql 




---------------------	SALIDA FINAL 	
	Select * 
	From #vw_FACT_Remisiones 
	Order by Orden 


End 
Go--#SQL 



