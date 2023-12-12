------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_Reporte_2N___TEST' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_Reporte_2N___TEST 
Go--#SQL   

--	Exec spp_FACT_Rpt_Reporte_2N___TEST @AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  

Create Proc	 spp_FACT_Rpt_Reporte_2N___TEST  
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 

	@TipoDeRemision int = 0, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 0, 
	@FolioInicial int = 20183, @FolioFinal int = 21400, 

	@TipoDeFecha int = 0, 
	@AplicarFiltroFechas int = 0, 
	@FechaInicial varchar(10) = '2019-02-20', @FechaFinal varchar(10) = '2019-03-30'
)    
With Encryption 
As 
Begin 
-- Set NoCount On  
Set DateFormat YMD 
Declare @sSql varchar(max),
		@sFiltro_Fechas varchar(max),
		@sFiltro_TipoDeRemision varchar(max),
		@sFiltro_Financiamiento varchar(max),
		@sFiltro_IdCliente varchar(max)


		Set @sFiltro_Fechas = '' 
		set @sFiltro_TipoDeRemision = ''
		Set @sFiltro_Financiamiento = ''
		Set @sFiltro_IdCliente = ''


--A�o, Mes

	Select * 
	Into #tmp__vw_ClaveSSA_Mascara 
	From vw_ClaveSSA_Mascara 
	Where IdCliente = 2 and IdSubCliente = 10 

	If @AplicarFiltroFechas = 1 
	Begin 
		Set @sFiltro_Fechas = ' and ( convert(varchar(10), FechaRemision, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 

		If @TipoDeFecha = 2 
		Begin 
			Set @sFiltro_Fechas = ' and ( convert(varchar(10), FechaInicial, 120)  between  ' + char(39) + @FechaInicial + char(39) + ' and ' +  char(39) + @FechaFinal + char(39) + ' ) ' + char(13) 
		End 
	End 


	If  @TipoDeRemision <> 0 
	Begin 
		If @TipoDeRemision = 1 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 1, 3, 4, 5 ) ' + char(13) -- PRODUCTO 

		If @TipoDeRemision = 2 
			Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( 2, 6, 7, 8 ) ' + char(13) -- SERVICIO  
	End 

	If @SegmentoTipoDeRemision <> 0 
	Begin 
		Set @sFiltro_TipoDeRemision = ' And TipoDeRemision in ( ' + cast(@SegmentoTipoDeRemision as varchar(10)) +  ' ) ' + char(13) -- SEGMENTO ESPECIFICO   
	End


	if ( @IdFuenteFinanciamiento <> '' )
	Begin
		If ( @IdFinanciamiento = '' )
			Begin
				Set @sFiltro_Financiamiento = ' And IdFuenteFinanciamiento <> ' + char(39) + @IdFuenteFinanciamiento + Char(39) -- IdFuenteFinanciamiento  
				--Delete #Salida Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento
			End
		Else
			Begin
				Set @sFiltro_Financiamiento = ' And IdFuenteFinanciamiento <> ' + char(39) + @IdFuenteFinanciamiento + Char(39) + ' And IdFinanciamiento <> ' + Char(39) + @IdFinanciamiento + Char(39) -- IdFuenteFinanciamiento  
				--Delete #Salida Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento
			End 
	End

	--if (@IdCliente <> '')
	--Begin
	--	If (@IdSubCliente = '')
	--		Begin
			
	--			Set @sFiltro_IdCliente = ' And IdCliente <> ' + char(39) + @IdCliente + Char(39) -- IdFuenteFinanciamiento  
	--			--Delete #Salida Where IdCliente = @IdCliente
	--		End
	--	Else
	--		Begin
	--			Set @sFiltro_IdCliente = ' And IdCliente <> ' + char(39) + @IdCliente + Char(39) + ' And IdSubCliente <> ' + Char(39) + @IdSubCliente + Char(39) -- IdFuenteFinanciamiento   -- IdFuenteFinanciamiento  
	--			--Delete #Salida Where IdCliente = @IdCliente And IdSubCliente = @IdSubCliente
	--		End 
	--End

---		spp_FACT_Rpt_Reporte_2N___TEST  


	If Exists ( Select * From sysobjects (nolock) where Name = 'FACT_Remisiones____RPT' and xType = 'U' ) Drop Table FACT_Remisiones____RPT   

		--Update S Set S.IdTipoDeRemision = F.TipoDeRemision, S.FechaRemision = Convert(varchar(10), F.FechaRemision, 120), OrigenInsumo = F.OrigenInsumo
	Set @sSql = 
	'Select top 10000 ' + 
	'F.IdEmpresa, F.IdEstado, F.IdFarmaciaGenera, F.FolioRemision, F.IdFuenteFinanciamiento, F.IdFinanciamiento, ' + 
	'F.TipoDeRemision As IdTipoDeRemision, Convert(varchar(10), F.FechaRemision, 120) As FechaRemision, F.OrigenInsumo ' + 
	'Into FACT_Remisiones____RPT ' + 
	'From FACT_Remisiones F (NoLock) ' +  
	'Inner Join FACT_Remisiones__RPT___20211117_1815 L (NoLock) ' +  
	'	On ( F.FolioRemision = L.FolioRemision ) ' + 
	'Where F.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) +  ' And F.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And F.IdFarmaciaGenera = ' + Char(39) + @IdFarmaciaGenera + Char(39) +
			@sFiltro_Fechas + @sFiltro_Financiamiento + @sFiltro_TipoDeRemision

	Print(@sSql) 
	Exec(@sSql)  


		If @AplicarFiltroFolios = 1 
		Begin 
			If @FolioInicial > 0 and @FolioFinal > 0 
				Begin 
					Delete FACT_Remisiones____RPT Where FolioRemision Not between @FolioInicial and @FolioFinal
				End 
			Else 
				Begin 
				
					If @FolioInicial > 0 
					Begin 
						Delete FACT_Remisiones____RPT Where FolioRemision < @FolioInicial
					End 

					If @FolioFinal > 0 
					Begin 
						Delete FACT_Remisiones____RPT Where FolioRemision > @FolioFinal
					End 

				End 
		End 


--		If @AplicarFiltroFechas = 1 
--		Begin 
--			Delete #FACT_Remisiones Where FechaRemision Not between @FechaInicial and @FechaFinal
--		End 

--		if  @TipoDeRemision <> 0 
--		Begin 
--			If @TipoDeRemision = 1 
--				Delete #FACT_Remisiones Where IdTipoDeRemision Not in ( 1, 3, 4, 5 ) -- PRODUCTO 

--			If @TipoDeRemision = 2 
--				Delete #FACT_Remisiones Where IdTipoDeRemision Not in ( 2, 6 ) -- SERVICIO  
--		End 

--		If @SegmentoTipoDeRemision <> 0 
--		Begin 
--			Delete #FACT_Remisiones Where IdTipoDeRemision <> @SegmentoTipoDeRemision -- SEGMENTO ESPECIFICO   
--		End

	--Drop Table #Salida 


	Select
		--top 400 
		FechaRemision,
		cast('' As varchar(10)) As FechaVenta, 
		0 as A�o_Venta, 
		cast('' As varchar(50)) As Mes_Venta, 
		S.IdEmpresa, S.IdEstado, S.Idfarmacia, 
		cast('' As varchar(500)) As NombreOficial, cast('' As varchar(500)) As NombreFarmacia,
		S.IdFarmaciaGenera, FolioVenta,
		cast('' As varchar(200)) As CentroDeCosto, 
		cast('' As varchar(200)) As NumFactura, 
		cast('' As varchar(200)) As FolioFiscal_SAT, 
		cast('' As varchar(200)) As FolioFiscal_SAT_Corto, 
		getdate() as FechaFactura, 
		cast('' As varchar(200)) As Nota_Credito, 
		cast('' As varchar(200)) As NumReceta, 
		cast('' As varchar(200)) As IdCliente, cast('' As varchar(200)) As IdSubCliente, cast('' As varchar(200)) As IdBeneficiario, 
		cast('' As varchar(200)) As ApPaterno, cast('' As varchar(200)) As Nombre,
		cast('' As varchar(200)) As FolioReferencia, cast('' As varchar(200)) As Domicilio,
		S.FolioRemision, IdTipoDeRemision, cast('' As varchar(200)) As TipoDeRemisionDesc, cast('' As varchar(200)) As TipoInsumo,
		Partida, cast('' As varchar(500)) As Observaciones,
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, cast('' As varchar(200)) As  'Fuente Financiamiento', 
		OrigenInsumo, CodigoEAN, 
		ClaveSSA, 
		cast('' As varchar(200)) As Mascara, 
		cast('' As varchar(max)) As Descripci�n, cast('' As varchar(200)) As Presentaci�n, referencia_01 As Proyecto,
		Referencia_01, 
		cast('' As varchar(200)) As Referencia_04, 		
		cast('' As varchar(200)) As Referencia_05, 
		PrecioLicitado, TasaIva, 	
		Cantidad_Agrupada as Cantidad, 
		(CAse when TasaIva > 0 Then SubTotalGrabado else SubTotalSinGrabar End) As SubTotal, Iva, Importe As Total
	Into #Salida
	From FACT_Remisiones_Detalles S (NoLock)
	Inner Join FACT_Remisiones____RPT F On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmaciaGenera = F.IdFarmaciaGenera And S.FolioRemision = F.FolioRemision)
	Where 
		1 = 1 
		and IdFarmacia <> '0004' 
		--and Cantidad > 0 and Cantidad_Agrupada > 0  
		--and S.ClaveSSA = '060.034.0103'  



--		Exec spp_FACT_Rpt_Reporte_2N___TEST   @AplicarFiltroFolios = 1, @FolioInicial = 19469, @FolioFinal = 19469  



	----select * from #Salida 

	Update S Set Referencia_04 = R.Referencia_04  
	From #Salida S (NoLock) 
	Inner Join  FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia R (NoLock) 
		On ( S.IdFuenteFinanciamiento = R.IdFuenteFinanciamiento and S.IdFinanciamiento = R.IdFinanciamiento and S.IdEstado = R.IdEstado and S.IdFarmacia = R.IdFarmacia and S.ClaveSSA = R.ClaveSSA ) 


	Update S Set S.CentroDeCosto = F.CentroDeCosto
	From #Salida S (NoLock)
	Inner Join Fact_Farmacias_CLUES__CentroDeCostos F (NoLock) On (S.IdEstado = F.IdEstado And S.IdFarmacia = F.IdFarmacia)

	--Select top 20 *
	Update S Set S.NumFactura = F.FolioFacturaElectronica
	From #Salida S (NoLock)
	Inner Join FACT_Facturas F (NoLock) On ( S.IdEmpresa = F.IdEmpresa and S.IdEstado = F.IdEstado and S.IdFarmaciaGenera = F.IdFarmacia and S.FolioRemision = F.FolioRemision )

	--Select * From FACT_CFDI_XML


	Update S Set S.FolioFiscal_SAT = F.uf_FolioSat, FolioFiscal_SAT_Corto = Right(uf_FolioSat, 12) 
	From #Salida S (NoLock)
	Inner Join FACT_CFDI_XML F (NoLock) 
		On ( S.IdEmpresa = F.IdEmpresa and S.IdEstado = F.IdEstado and S.IdFarmaciaGenera = F.IdFarmacia and S.NumFactura = Serie + ' - ' + cast(Folio As varchar(100)) )  

	Update S Set S.FechaFactura = F.FechaRegistro 
	From #Salida S (NoLock)
	Inner Join FACT_CFD_Documentos_Generados F (NoLock) 
		On ( S.IdEmpresa = F.IdEmpresa and S.IdEstado = F.IdEstado and S.IdFarmaciaGenera = F.IdFarmacia and S.NumFactura = Serie + ' - ' + cast(Folio As varchar(100)) )  

---		spp_FACT_Rpt_Reporte_2N___TEST  

	Update S Set S.Nota_Credito = Serie + ' - ' + cast(Folio As varchar(100))
	From #Salida S (NoLock)
	Inner Join FACT_CFDI_NotasDeCredito_DoctosRelacionados F (NoLock) On (S.IdEmpresa = F.IdEmpresa and S.IdEstado = F.IdEstado and S.IdFarmaciaGenera = F.IdFarmacia and S.NumFactura = Serie_Relacionada + ' - ' + cast(Folio_Relacionado As varchar(100)))

	--Select top 10 IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, uf_FolioSat From FACT_CFDI_XML

	--Update S Set S.NumFactura = F.FolioFacturaElectronica, FolioFiscal_SAT_Corto = Right(uf_FolioSat, 12)
	--From #Salida S (NoLock)
	--Inner Join FACT_Facturas F (NoLock) On (S.IdEmpresa = F.IdEmpresa and S.IdEstado = F.IdEstado and S.IdFarmacia = F.IdFarmacia and S.FolioRemision = F.FolioRemision)


	Update S Set S.Observaciones = F.Observaciones, S.Referencia_05 = F.Referencia_05
	From #Salida S (NoLock)
	Inner Join FACT_Remisiones_InformacionAdicional F (NoLock) On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmaciaGenera = F.IdFarmaciaGenera And S.FolioRemision = F.FolioRemision )


	Update S Set S.IdCliente = F.IdCliente, S.IdSubCliente = F.IdSubCliente, S.FechaVenta = Convert(varchar(10), F.FechaRegistro, 120)
	From #Salida S (NoLock) 
	Inner Join VentasEnc F (NoLock) On (S.IdEmpresa = F.IdEmpresa And S.IdEstado = F.IdEstado And S.IdFarmacia = F.IdFarmacia And S.FolioVenta = F.FolioVenta )


	Update S Set A�o_Venta = datepart(year, FechaVenta), Mes_Venta = dbo.fg_NombresDeMes(FechaVenta) 
	From #Salida S (NoLock) 





		if (@IdCliente <> '')
		Begin
			If (@IdSubCliente = '')
				Begin
					Delete #Salida Where IdCliente = @IdCliente
				End
			Else
				Begin
					Delete #Salida Where IdCliente = @IdCliente And IdSubCliente = @IdSubCliente
				End 
		End

		if @OrigenDeInsumos <> 0 
		Begin 
			If @OrigenDeInsumos = 1 
				Delete #Salida Where  OrigenInsumo <> 0 -- VENTA 

			If @OrigenDeInsumos = 2 
				Delete #Salida Where  OrigenInsumo <> 1 -- CONSIGNA  
		End 




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

	Update S Set S.Mascara = F.Mascara, S.Descripci�n = F.Descripcion, S.Presentacion = F.Presentacion, S.TipoInsumo = F.TipoDeClaveDescripcion
	From #Salida S (NoLock)
	Inner Join #tmp__vw_ClaveSSA_Mascara F (NoLock) 
		On ( S.IdEstado = F.IdEstado and S.IdCliente = F.IdCliente and S.IdSubCliente = F.IdSubCliente and S.ClaveSSA = F.ClaveSSA ) 


	Update S Set S.NombreOficial = F.NombreFarmacia, S.NombreFarmacia = F.NombreFarmacia
	From #Salida S (NoLock)
	Inner Join CatFarmacias F On ( S.IdEstado = F.IdEstado and S.IdFarmacia = F.IdFarmacia ) 


	--Select * From vw_FACT_Remisiones

	--Select *--FechaRemision, FechaVenta, FolioVenta, NumReceta, ApPaterno, Nombre, FolioReferencia, Domicilio, FolioRemision, TipoDeRemisionDesc, TipoInsumo, Partida, Observaciones, IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento], ClaveSSA, Descripci�n, Presentaci�n, Referencia_01, Referencia_05, PrecioLicitado, TasaIva, Cantidad, SubTotal, Iva, Total
	
---		spp_FACT_Rpt_Reporte_2N___TEST  

	------------------------------------------ SALIDA FINAL 
	If Exists ( Select * From sysobjects (nolock) where Name = 'FACT_Remisiones__RPT__Filtro' and xType = 'U' ) Drop Table FACT_Remisiones__RPT__Filtro   

	Select 
		Identity(int, 1, 1) as Secuencial, 
		A�o_Venta, 
		Mes_Venta, 
		FolioRemision, 
		FolioVenta, 
		NumFactura As 'FolioFactura Electronica', FolioFiscal_SAT, FolioFiscal_SAT_Corto, 
		FechaFactura, 

		FechaRemision, 
		---- FechaVenta, 

		--CentroDeCosto, 
		IdFarmacia, NombreOficial, NombreFarmacia, 
		--Nota_Credito, FolioRemision, 
		TipoDeRemisionDesc, Partida, TipoInsumo, 
		IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento],
		
		ClaveSSA, 
		Mascara, 
		Descripcion, 
		Presentacion, 
		Proyecto, Referencia_04 as Causes, 
		TasaIva, PrecioLicitado As PrecioUnitario 
		 
		, sum(Cantidad) as Cantidad, sum(SubTotal) as SubTotal, sum(Iva) as Iva, sum(Total) As Importe 
	Into FACT_Remisiones__RPT__Filtro 
	From #Salida (NoLock)  
	-- Where ClaveSSA = '060.034.0103' 
	Group by
		A�o_Venta, 
		Mes_Venta, 
		FolioRemision, 
		FolioVenta, 
		NumFactura, FolioFiscal_SAT, FolioFiscal_SAT_Corto, 
		FechaFactura, 
		FechaRemision, 
		---- FechaVenta, 

		--CentroDeCosto, 
		IdFarmacia, NombreOficial, NombreFarmacia, 
		--Nota_Credito, FolioRemision, 
		TipoDeRemisionDesc, Partida, TipoInsumo, 
		IdFuenteFinanciamiento, IdFinanciamiento, [Fuente Financiamiento],
		
		ClaveSSA, 
		Mascara, 
		Descripcion, 
		Presentacion, 
		Proyecto, Referencia_04, 
		TasaIva, PrecioLicitado 
	Order by FolioRemision, ClaveSSA 
	

End 
Go--#SQL  


