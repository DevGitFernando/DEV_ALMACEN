--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_Reporte_1N' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_Reporte_1N 
Go--#SQL  

Create Proc	spp_FACT_Rpt_Reporte_1N
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	
	@IdFuenteFinanciamiento varchar(4) = '1011', @IdFinanciamiento varchar(4) = '', 

	@TipoDeRemision int = 0, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 1, 
	@FolioInicial int = 20861, @FolioFinal int = 20861, 

	@TipoDeFecha int = 1, 
	@AplicarFiltroFechas int = 0,   
	@FechaInicial varchar(10) = '2019-11-01', @FechaFinal varchar(10) = '2019-11-16'  
)    
With Encryption 
As 
Begin 
-- Set NoCount On  
Set DateFormat YMD 
Declare @sNA varchar(10)  

--Año, Mes

Declare 
	@sSql varchar(max),  
	@sFiltro varchar(max),  
	@sFiltro_Cliente varchar(max),  
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
	Set @sFiltro_Cliente = '' 
	Set @sFiltro_Folios = '' 
	Set @sFiltro_Fechas = '' 
	Set @sFiltro_TipoDeRemision = '' 
	Set @sFiltro_OrigenInsumo = '' 
	Set @sFiltro_TipoDeInsumo = '' 





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



	If @IdCliente <> '' 
	Begin 
		If @IdCliente <> '' and @IdSubCliente <> '' 
			Begin 
				Set @sFiltro_Cliente = ' and IdCliente = ' + char(39) + right('00000000' + @IdCliente, 4) + char(39)  + '  and IdSubCliente =  ' + char(39) + right('00000000' + @IdSubCliente, 4) + char(39) + '  ' + char(13) 
			End 
		Else 
			Begin 
				If @IdCliente <> ''  
				Begin 
					Set @sFiltro_Cliente = ' and IdCliente = ' + char(39) + right('00000000' + @IdCliente, 4) + char(39)  + '  '  + char(13) 
				End 
			End 
	End 

	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
	If @AplicarFiltroFechas = 1 
	Begin 
		Set @sFiltro_Fechas = ' and ( convert(varchar(10), FechaRemision, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 

		If @TipoDeFecha = 2 
		Begin 
			Set @sFiltro_Fechas = ' and ( convert(varchar(10), FechaInicial, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 
		End 
	End 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    if  @TipoDeRemision <> 0 
	Begin 
		--Set @sFiltro_TipoDeRemision =  ' And TipoDeRemision in ( ' + cast(@TipoDeRemision as varchar(10))  + ' ) '  
		If @TipoDeRemision = 1 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 1, 3, 4, 5 ) ' + char(13) -- PRODUCTO 

		If @TipoDeRemision = 2 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 2, 6, 7, 8 ) ' + char(13) -- SERVICIO  
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
		'From vw_FACT_Remisiones (NoLock) '  + char(13) + 
		@sFiltro + @sFiltro_Cliente + @sFiltro_Folios + @sFiltro_Fechas + @sFiltro_TipoDeRemision + @sFiltro_OrigenInsumo + @sFiltro_TipoDeInsumo 
	Exec ( @sSql ) 
	Print @sSql 


	Select
		--top 400
		Cast('XXXX-XX' As Varchar(10)) As FechaRemision,
		Cast('XXXX-XX' As Varchar(10)) As FechaVenta, 
		0 as Año_Venta, 
		cast('' As varchar(50)) As Mes_Venta, 
		D.IdEmpresa, D.IdEstado, D.Idfarmacia, D.IdFarmaciaGenera, D.FolioVenta, 

		cast('' As varchar(200)) As NumFactura, 
		cast('' As varchar(200)) As FolioFiscal_SAT, 
		cast('' As varchar(200)) As FolioFiscal_SAT_Corto, 

		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As NumReceta,  
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As IdCliente, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As IdSubCliente, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As IdBeneficiario, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As ApPaterno, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As Nombre, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As FolioReferencia, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As Domicilio,
		D.FolioRemision, 'XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As IdTipoDeRemision, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As TipoDeRemisionDesc, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As TipoInsumo, 
		Partida, Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(500)) As Observaciones,
		D.IdFuenteFinanciamiento, D.IdFinanciamiento, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As  'Fuente Financiamiento', 
		0 As OrigenInsumo, D.CodigoEAN, 
		
		D.ClaveSSA, 	
		cast('' As varchar(200)) As Mascara, 
			
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As Descripción, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As Presentación,
		D.Referencia_01, 
		Cast('XXXXXXXXXXXXXXXXXXXXXXXXXXXX' As Varchar(200)) As Referencia_05, D.PrecioLicitado, D.TasaIva, 
		D.Cantidad_Agrupada as Cantidad, 
		(CAse when D.TasaIva > 0 Then D.SubTotalGrabado else D.SubTotalSinGrabar End) As SubTotal, D.Iva, D.Importe As Total
	Into #Salida 
	From FACT_Remisiones_Detalles D (NoLock)
	Inner Join #vw_FACT_Remisiones R (NoLock) On ( D.IdEstado = R.IdEstado and D.FolioRemision = R.FolioRemision )  
	Where Cantidad > 0 and Cantidad_Agrupada > 0  

	-- Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And IdFarmacia = '0004'--And FolioRemision In ('0000020839', '0000020839', '0000020840')



	---------------------------------------------------------------------------- COMPLETAR LA INFORMACION  

	Update S Set S.Observaciones = F.Observaciones, S.Referencia_05 = F.Referencia_05
	From #Salida S (NoLock) 
	Inner Join FACT_Remisiones_InformacionAdicional F (NoLock) On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmaciaGenera = F.IdFarmaciaGenera And S.FolioRemision = F.FolioRemision )

	Update S Set S.IdCliente = F.IdCliente, S.IdSubCliente = F.IdSubCliente, S.FechaVenta = Convert(Varchar(10), F.FechaRegistro, 120)
	From #Salida S (NoLock)
	Inner Join VentasEnc F (NoLock) On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmacia = F.IdFarmacia And S.FolioVenta = F.FolioVenta )

	Update S Set Año_Venta = datepart(year, FechaVenta), Mes_Venta = dbo.fg_NombresDeMes(FechaVenta) 
	From #Salida S (NoLock) 


	Update S Set S.IdTipoDeRemision = F.TipoDeRemision, S.FechaRemision = Convert(Varchar(10), F.FechaRemision, 120), OrigenInsumo = F.OrigenInsumo
	From #Salida S (NoLock)
	Inner Join FACT_Remisiones F (NoLock) On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmaciaGenera = F.IdFarmaciaGenera And S.FolioRemision = F.FolioRemision)  


	Update S Set S.TipoDeRemisionDesc = F.Descripcion
	From #Salida S (NoLock)
	Inner Join FACT_TiposDeRemisiones F (NoLock) On (S.IdTipoDeRemision = F.TipoDeRemision)


	Update S Set [Fuente Financiamiento] = F.Descripcion
	From #Salida S (NoLock)
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles F (NoLock) On ( S.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And S.IdFinanciamiento = F.IdFinanciamiento )


	Update S Set S.NumReceta = F.NumReceta, S.IdBeneficiario = F.IdBeneficiario
	From #Salida S (NoLock)
	Inner Join VentasInformacionAdicional F (NoLock) On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmacia = F.IdFarmacia And S.FolioVenta = F.FolioVenta )

	Update S Set S.ApPaterno = F.ApPaterno, S.Nombre = F.Nombre, S.FolioReferencia = F.FolioReferencia, S.Domicilio = F.Domicilio
	From #Salida S (NoLock)
	Inner Join CatBeneficiarios F (NoLock) On (S.IdEstado = F.IdEstado And S.IdFarmacia = F.IdFarmacia And S.IdCliente = F.IdCliente And S.IdSubCliente = F.IdSubCliente And S.IdBeneficiario = F.IdBeneficiario )



	Update S Set S.Mascara = F.Mascara, S.Descripción = F.Descripcion, S.Presentacion = F.Presentacion, S.TipoInsumo = F.TipoDeClaveDescripcion
	From #Salida S (NoLock) 
	Inner Join vw_ClaveSSA_Mascara F (NoLock) 
		On ( S.IdEstado = F.IdEstado and S.IdCliente = F.IdCliente and S.IdSubCliente = F.IdSubCliente and S.ClaveSSA = F.ClaveSSA ) 



	Update S Set S.NumFactura = F.FolioFacturaElectronica
	From #Salida S (NoLock)
	Inner Join FACT_Facturas F (NoLock) On ( S.IdEmpresa = F.IdEmpresa and S.IdEstado = F.IdEstado and S.IdFarmaciaGenera = F.IdFarmacia and S.FolioRemision = F.FolioRemision and F.Status = 'A' )

	Update S Set S.FolioFiscal_SAT = F.uf_FolioSat, FolioFiscal_SAT_Corto = Right(uf_FolioSat, 12)
	From #Salida S (NoLock)
	Inner Join FACT_CFDI_XML F (NoLock) On ( S.IdEmpresa = F.IdEmpresa and S.IdEstado = F.IdEstado and S.IdFarmaciaGenera = F.IdFarmacia and S.NumFactura = Serie + ' - ' + cast(Folio As varchar(100)) )

	---------------------------------------------------------------------------- COMPLETAR LA INFORMACION  



	--Select * From vw_FACT_Remisiones  
	------------------------------------------ SALIDA FINAL 
	Select 
		FechaRemision, 
		--FechaVenta, 	
		Año_Venta, 
		Mes_Venta, 		
		NumFactura, FolioFiscal_SAT, FolioFiscal_SAT_Corto, 
		FolioVenta, NumReceta, ApPaterno, Nombre, FolioReferencia, Domicilio, FolioRemision, TipoDeRemisionDesc, TipoInsumo, Partida, 
		Observaciones, IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento], 
		ClaveSSA, 
		Mascara, 
		Descripción, Presentación, Referencia_01, Referencia_05, PrecioLicitado, TasaIva, 
		sum(Cantidad) as Cantidad, sum(SubTotal) as SubTotal, sum(Iva) as Iva, sum(Total) as Total 
	From #Salida (NoLock)
	Group by 
		FechaRemision, 
		--FechaVenta, 	
		Año_Venta, 
		Mes_Venta, 		
		NumFactura, FolioFiscal_SAT, FolioFiscal_SAT_Corto, 
		FolioVenta, NumReceta, ApPaterno, Nombre, FolioReferencia, Domicilio, FolioRemision, TipoDeRemisionDesc, TipoInsumo, Partida, 
		Observaciones, IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento], 
		ClaveSSA, 
		Mascara, 
		Descripción, Presentación, Referencia_01, Referencia_05, PrecioLicitado, TasaIva 



 
End 
Go--#SQL  


