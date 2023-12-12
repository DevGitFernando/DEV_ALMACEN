
---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_CFD_GetInformacionRemision' and xType = 'P' )
    Drop Proc spp_FACT_CFD_GetInformacionRemision 
Go--#SQL 
  
---		Exec spp_FACT_CFD_GetInformacionRemision  @IdEmpresa = '001', @IdEstado = '13', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000006'  

/*

--		Exec spp_FACT_CFD_GetInformacionRemision  @IdEmpresa = '001', @IdEstado = '28', @IdFarmaciaGenera = '0001' , @FolioRemision = [ '0000021668'  ], @EsConcentrado = 0   

*/ 

Create Proc spp_FACT_CFD_GetInformacionRemision 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '1',  @FolioRemision varchar(max) = '0000021668', 
	@Identificador_UUID varchar(500) = '', @EsConcentrado int = 0   
) 
With Encryption		
As 
Begin 
Set NoCount On 
Declare 
	@Anexar__Lotes_y_Caducidades bit, 
	@EsInsumos bit, 	
	@EsMedicamento bit, 
	@sUnidadDeMedida varchar(100), 
	@sFarmacia varchar(300), 
	@sPeriodo varchar(300), 
	@sFarmaciasRemision varchar(max), 
	@bEsConcentrado_Servicio bit, 
	@bRemision_Medicamento bit, 
	@bRemision_MaterialDeCuracion bit
	
Declare 
	@sSql varchar(max), 
	@Keyx int, 
	@ClaveSSA varchar(50), 
	@Informacion varchar(max)  	

Declare 
	@bObservaciones_01 bit, 
	@bObservaciones_02 bit, 
	@bObservaciones_03 bit, 
	@bReferencia_01 bit, 
	@bReferencia_02 bit, 
	@bReferencia_03 bit, 
	@bReferencia_04 bit, 
	@bReferencia_05 bit 

Declare 
	@sItem_Remision varchar(max), 
	@sItem_Factura varchar(max), 
	@sLista_Remisiones varchar(max), 
	@sLista_Facturas varchar(max) 

--------- Obtener datos iniciales 	
	Set @sUnidadDeMedida = ''
	Set @Keyx = 0 
	Set @ClaveSSA = '' 
	Set @Informacion = '' 
	Set @EsInsumos = 0  
	Set @EsMedicamento = 0  	
	Set @sFarmacia = '' 
	Set @sPeriodo = '' 
	Set @sFarmaciasRemision = '' 

	Set @sItem_Remision = ''  
	Set @sItem_Factura = ''  
	Set @sLista_Remisiones = ''  
	Set @sLista_Facturas = ''  

	Set @bObservaciones_01 = 0  
	Set @bObservaciones_02 = 0  
	Set @bObservaciones_03 = 0  
	Set @bReferencia_01 = 0  
	Set @bReferencia_02 = 0  
	Set @bReferencia_03 = 0  
	Set @bReferencia_04 = 0  
	Set @bReferencia_05 = 0 

	Set @bEsConcentrado_Servicio = 0 
	Set @bRemision_Medicamento = 0 
	Set @bRemision_MaterialDeCuracion = 0 

	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	--Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 
--------- Obtener datos iniciales 	

	
	-------------------------- Parámetros Observaciones 
	Select @bObservaciones_01 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Obs_01_Default' 

	Select @bObservaciones_02 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Obs_02_Default' 

	Select @bObservaciones_03 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Obs_03_Default' 
	-------------------------- Parámetros Observaciones 


	-------------------------- Parámetros Referencias  
	Select @bReferencia_01 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Ref_01_Default' 

	Select @bReferencia_02 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Ref_02_Default' 

	Select @bReferencia_03 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Ref_03_Default' 

	Select @bReferencia_04 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Ref_04_Default' 

	Select @bReferencia_05 = (case when Valor = 'true' then 1 else 0 end) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Ref_05_Default' 
	-------------------------- Parámetros Referencias  




	------------------------------- Listado de Remisiones 
	Select Top 0 IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
	Into #tmp_ListaRemisiones 
	From FACT_Remisiones D (NoLock) 
	Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 


	Select Top 0 IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, cast('' as varchar(100)) as Serie_Factura  
	Into #tmp_ListaRemisiones_Relacionadas  
	From FACT_Remisiones D (NoLock) 
	Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 


	If @EsConcentrado = 0 
		Begin 
			--Print @FolioRemision  
			Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 
			
			Insert Into #tmp_ListaRemisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision  ) 
			Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
			From FACT_Remisiones D (NoLock) 
			Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 


			Insert Into #tmp_ListaRemisiones_Relacionadas 
			Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision_Relacionado as FolioRemision, '' as Serie_Factura  
			From FACT_Remisiones_Relacionadas D (NoLock) 
			Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 
		End 
	Else 
		Begin 
			Print @FolioRemision 
			Set @sSql = '' + 
			'Insert Into #tmp_ListaRemisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision  ) ' + char(10) + 
			'Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ' + char(10) + 
			'From FACT_Remisiones D (NoLock) ' + char(10) + 
			'Where D.IdEstado = ' + char(39) + @IdEstado + char(39) + ' And D.IdFarmaciaGenera = ' + char(39) + @IdFarmaciaGenera + char(39) + ' And D.FolioRemision in ( ' + @FolioRemision + ' ) '  
			Exec( @sSql ) 
			Print @sSql 
		End 
	------------------------------- Listado de Remisiones 


------------------------------------------ OBTENER LISTA DE REMISIONES RELACIONADAS 
	--------- Obtener los folios de facturas de las remisiones previamente facturadas 
	Update R Set Serie_Factura = F.FolioFacturaElectronica 
	From #tmp_ListaRemisiones_Relacionadas R (NoLock) 
	Inner Join FACT_Facturas F (NoLock) On ( R.IdEmpresa = F.IdEmpresa and R.IdEstado = F.IdEstado and R.IdFarmaciaGenera = F.IdFarmacia and R.FolioRemision = F.FolioRemision and F.Status = 'A' ) 
	
	--Select * 	from #tmp_ListaRemisiones_Relacionadas 


Declare 
	@iItem int 

	Set @iItem = 0 
	Set @sItem_Remision = ''  
	Set @sItem_Factura = ''  
	Set @sLista_Remisiones = ''  
	Set @sLista_Facturas = ''  

	Declare #cursorRemisiones 
	Cursor For 
	Select cast('RM' + FolioRemision as varchar(20)), Serie_Factura 
	From #tmp_ListaRemisiones_Relacionadas 
	--Where 1 = 0 
	Order By FolioRemision  
	Open #cursorRemisiones 
	FETCH NEXT FROM #cursorRemisiones Into @sItem_Remision, @sItem_Factura  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
				
			If @iItem = 0 
				Begin 
					Set @sLista_Remisiones = @sLista_Remisiones + ltrim(rtrim(@sItem_Remision)) --- + ' ' + @sPeriodo + '  ' + char(13) 
					Set @sLista_Facturas = @sLista_Facturas + ltrim(rtrim(@sItem_Factura)) --- + ' ' + @sPeriodo + '  ' + char(13) 
				End 
			Else 
				Begin 
					Set @sLista_Remisiones = @sLista_Remisiones + ', ' + char(13) + ltrim(rtrim(@sItem_Remision))   -- + char(10)--- + ' ' + @sPeriodo + '  ' + char(13) 
					Set @sLista_Facturas = @sLista_Facturas + ', ' + char(13) + ltrim(rtrim(@sItem_Factura))   -- + char(10)--- + ' ' + @sPeriodo + '  ' + char(13) 
				End 				 

			Set @iItem = @iItem + 1 

			--print @sFarmaciasRemision 

			FETCH NEXT FROM #cursorRemisiones Into @sItem_Remision, @sItem_Factura 
		END
	Close #cursorRemisiones 
	Deallocate #cursorRemisiones 

	---------- 
	--Select @sLista_Remisiones, @sLista_Facturas   

------------------------------------------ OBTENER LISTA DE REMISIONES RELACIONADAS 


	----select * from #tmp_ListaRemisiones 

	----select E.* 
	----From FACT_Remisiones E (NoLock)	 
	----Inner Join #tmp_ListaRemisiones R (NoLock) On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmaciaGenera = R.IdFarmaciaGenera and E.FolioRemision = R.FolioRemision  ) 

--		sp_listacolumnas FACT_Remisiones 


------------------- INFORMACION BASE 
	Select	
		E.TipoDeRemision, E.TipoInsumo, 
		E.FolioRemision, 
		E.IdFuenteFinanciamiento, cast('' as varchar(1000)) as FuenteDeFinanciamiento, E.IdFinanciamiento, cast('' as varchar(1000)) as Financiamiento, 
		E.IdEmpresa, E.IdEstado, E.IdFarmaciaGenera, D.IdFarmacia, cast('' as varchar(1000)) as Farmacia, cast('' as varchar(1000)) as CLUES, 
		min(D.FolioVenta) as FolioInicial, max(D.FolioVenta) as FolioFinal, 
		cast('' as varchar(max)) as ReferenciaDePedido, 
		getdate() as FechaInicial, getdate() as FechaFinal    
	Into #tmpDetalles 
	From FACT_Remisiones E (NoLock)	 
	Inner Join #tmp_ListaRemisiones R (NoLock) On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmaciaGenera = R.IdFarmaciaGenera and E.FolioRemision = R.FolioRemision  ) 
	Inner Join FACT_Remisiones_Detalles D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision )
	--Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision	
	Where 1 = 1 
	Group by 
		E.TipoDeRemision, E.TipoInsumo, 
		E.FolioRemision, E.IdFuenteFinanciamiento, E.IdFinanciamiento, 
		E.IdEmpresa, E.IdEstado, E.IdFarmaciaGenera, D.IdFarmacia 

	--select * from #tmpDetalles 

	Update  D Set Farmacia = F.Farmacia 
	From #tmpDetalles D 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) 

	Update  D Set CLUES = F.CLUES, Farmacia = (case when F.NombreOficial = '' then Farmacia else F.NombreOficial end)
	From #tmpDetalles D 
	Inner Join FACT_Farmacias_CLUES F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) 


	Update  D Set FuenteDeFinanciamiento = F.Cliente + ' - ' + F.SubCliente, 
		Financiamiento = F.Financiamiento 
	From #tmpDetalles D 
	Inner Join vw_FACT_FuentesDeFinanciamiento_Detalle F (NoLock) 
		On ( D.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and D.IdFinanciamiento = F.IdFinanciamiento ) 


	Update D Set FechaInicial = V.FechaRegistro 
	From #tmpDetalles D 
	Inner Join VentasEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioInicial = V.FolioVenta ) 

	Update D Set FechaFinal = V.FechaRegistro 
	From #tmpDetalles D 
	Inner Join VentasEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioFinal = V.FolioVenta ) 


	Update D Set ReferenciaDePedido = V.NumReceta
	From #tmpDetalles D 
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioFinal = V.FolioVenta ) 
	Where @EsConcentrado = 0   



	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
	--------------------------------------------- Detalles 
	If @IdEstado = '28' and @sLista_Facturas != '' 
	Begin 
		Set @sLista_Remisiones = '' 
		Set @sLista_Facturas = 'Factura complementaria de la factura : ' + char(13) + ltrim(rtrim(@sLista_Facturas)) + char(13) + 
			'Lo anterior, de conformidad con lo estipulado en el punto 1.2 de las Bases de la Licitación Pública Nacional Número 57062002-003-2020; así como de la Cláusula Novena y del Convenio Modificatorio de fecha 28 de diciembre de 2020.'
	End 

	Select 
		E.IdEmpresa, E.IdEstado, D.IdFarmaciaGenera, 
		E.FolioRemision, E.TipoDeRemision, E.TipoDeRemisionDesc, E.TipoInsumo, E.TipoDeInsumo,  E.OrigenInsumo, E.OrigenInsumoDesc, 
		E.IdFuenteFinanciamiento, cast(E.Cliente + ' - ' + E.SubCliente  as varchar(1000)) as FuenteFinanciamiento, 
		E.IdFinanciamiento, E.Financiamiento, 

		E.NumeroDeContrato, 
		E.NumeroDeLicitacion,  

		cast('' as varchar(max)) as IdDocumento, 
		cast('' as varchar(max)) as NombreDocumento,  
		E.IdCliente, E.Cliente, E.IdSubCliente, E.SubCliente, E.SubTotalSinGrabar, E.SubTotalGrabado, E.Iva, E.Total, 
		
		cast('' as varchar(max)) as InformacionDeContrato, 

		cast('' as varchar(max)) as FarmaciasRemision, 
		cast('' as varchar(max)) as InformacionAdicional, 

		cast('' as varchar(max)) as InformacionAdicional_Almacen_TipoDeBeneficiario, 
		cast('' as varchar(max)) as InformacionAdicional_Almacen_Beneficiario, 
		cast('' as varchar(max)) as InformacionAdicional_Almacen_NombreBeneficiario, 

		cast(@sLista_Remisiones as varchar(max)) as Referencia, 
		cast(@sLista_Remisiones as varchar(max)) as Referencia_Remisiones, 
		cast(@sLista_Facturas as varchar(max)) as Referencia_Facturas, 

		D.ReferenciaDePedido, 

		min(D.FechaInicial) as FechaInicial, max(D.FechaFinal) as FechaFinal, 
		'' as PeriodoRemision 
	Into #tmpConcentrado 
	From vw_FACT_TipoRemisiones  E (NoLock)   
	Inner Join #tmpDetalles D On ( E.IdEmpresa = D.IdEmpresa And D.IdEstado = E.IdEstado And D.IdFarmaciaGenera = E.IdFarmacia And D.FolioRemision = E.FolioRemision ) 
	Group by 
		E.IdEmpresa, E.IdEstado, D.IdFarmaciaGenera, 
		E.FolioRemision, E.TipoDeRemision, E.TipoDeRemisionDesc, E.IdFuenteFinanciamiento, E.IdFinanciamiento, E.Financiamiento, 
		E.NumeroDeContrato, 
		E.NumeroDeLicitacion,  
		E.TipoInsumo, E.TipoDeInsumo, 
		E.OrigenInsumo, E.OrigenInsumoDesc, E.IdCliente, E.Cliente, E.IdSubCliente, E.SubCliente, 
		D.ReferenciaDePedido, 
		E.SubTotalSinGrabar, E.SubTotalGrabado, E.Iva, E.Total 



	Update D Set InformacionAdicional = I.Observaciones 
	From #tmpConcentrado D 
	Inner Join FACT_Remisiones_InformacionAdicional I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa And D.IdEstado = I.IdEstado And D.IdFarmaciaGenera = I.IdFarmaciaGenera And D.FolioRemision = I.FolioRemision ) 


	Update RD Set NombreDocumento = LD.NombreDocumento_Relacionado  
	From #tmpConcentrado RD 
	Inner Join dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento(1) LD -- Valor nominal, Importe aplicado, Importe restante se regresan en 0 para los documentos relacionados  
		On ( RD.IdFuenteFinanciamiento = LD.IdFuenteFinanciamiento and RD.IdFinanciamiento = LD.IdFinanciamiento and RD.IdDocumento = LD.IdDocumento ) 


	Update D Set 
		InformacionAdicional_Almacen_TipoDeBeneficiario = cast(IsNull(IA.TipoDeBeneficiario, '') as varchar(max)), 
		InformacionAdicional_Almacen_Beneficiario = cast(IsNull(IA.Beneficiario, '') as varchar(max)), 
		InformacionAdicional_Almacen_NombreBeneficiario = cast(IsNull(IA.NombreBeneficiario, '') as varchar(max)) 
	From #tmpConcentrado D 
	Inner Join FACT_Remisiones_InformacionAdicional_Almacenes IA (NoLock) 
		On ( D.IdEmpresa = IA.IdEmpresa And D.IdEstado = IA.IdEstado And D.IdFarmaciaGenera = IA.IdFarmaciaGenera And D.FolioRemision = IA.FolioRemision ) 


	Update D Set 
		InformacionDeContrato = 
			cast(
			( 
				'NÚMERO DE CONTRATO : ' + NumeroDeContrato + 
				(case when IsNull(NumeroDeLicitacion, '') = '' then '' else '	NÚMERO DE LICITACIÓN : ' + IsNull(NumeroDeLicitacion, '') end) + 
				(case when IsNull(IdDocumento, '') = '' then '' else '	NÚMERO DE DOCUMENTO : ' + IsNull(NombreDocumento, '') end) 
			) as varchar(max))
	From #tmpConcentrado D 


	Update D Set 
		InformacionDeContrato = 
			cast( 'NÚMERO DE CONTRATO : ' + NumeroDeContrato as varchar(max) ) 
	From #tmpConcentrado D 
	Where IdEstado = '28' 


	Update D Set InformacionAdicional = 'NO. ORDEN DE COMPRA : ' + i.Info_01 + ', ' + 'NO. SUFICIENCIA : ' + i.Info_02 + ', ' + 'F : ' + i.Info_03 + ', ' + ' NO. DE CONTROL : ' + I.Info_04
	From #tmpConcentrado D 
	Inner Join FACT_Remisiones_InformacionAdicional I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa And D.IdEstado = I.IdEstado And D.IdFarmaciaGenera = I.IdFarmaciaGenera And D.FolioRemision = I.FolioRemision ) 
	Where I.IdEstado = '22' 


	---------------------------- VALIDACION DE PROCESO CONCENTRADO 
	Select Top 1  
		@EsInsumos = (case when cast(TipoDeRemision as int) = 1 then 1 else 0 end),  	
		@EsMedicamento = (case when cast(TipoInsumo as int) = 2 then 1 else 0 end) 		
	From #tmpConcentrado   

	If @EsConcentrado = 1  
	Begin 
		Select Top 1 @bRemision_Medicamento = 1 From #tmpConcentrado (NoLock) Where TipoInsumo = '02'  
		Select Top 1 @bRemision_MaterialDeCuracion = 1 From #tmpConcentrado (NoLock) Where TipoInsumo = '01' 

		Set @bRemision_Medicamento = IsNull(@bRemision_Medicamento, 0) 
		Set @bRemision_MaterialDeCuracion = IsNull(@bRemision_MaterialDeCuracion, 0) 

		If @bRemision_Medicamento = 1 and @bRemision_MaterialDeCuracion = 1 
		Begin 
			Set @bEsConcentrado_Servicio = 1 
		End 
	End 

	Update D Set 
		--TipoDeRemision = '00', 
		TipoInsumo = '00', TipoDeInsumo = 'MEDICAMENTO Y MATERIAL DE CURACIÓN'
	From #tmpConcentrado D 
	Where @bEsConcentrado_Servicio = 1 

	--------------------------------------------- Detalles 
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 


--		Exec spp_FACT_CFD_GetInformacionRemision  @IdEmpresa = '001', @IdEstado = '13', @IdFarmaciaGenera = '0001' , @FolioRemision = [ '0000004968'  ], @EsConcentrado = 1   



------------------- INFORMACION BASE  	



	

--		sp_listacolumnas__Stores	vw_FACT_TipoRemisiones  	, 1 

---		spp_FACT_CFD_GetInformacionRemision	



------------------------------------------ OBTENER LISTA DE FARMACIAS INVOLUCRADAS  
Declare 
	@iItem_x int 

	Set @iItem = 0 

		Declare #cursorClaves 
		Cursor For 
		Select (CLUES + ' ' + Farmacia) as Farmacia, '' as Periodo --, ('(' + convert(varchar(10), FechaInicial, 120) + ' AL ' + convert(varchar(10), FechaFinal, 120) + ')' ) as Periodo    
		From #tmpDetalles 
		Group by IdFarmacia, (CLUES + ' ' + Farmacia) -- , ('(' + convert(varchar(10), FechaInicial, 120) + ' AL ' + convert(varchar(10), FechaFinal, 120) + ')' ) 
		Order By IdFarmacia  
		Open #cursorClaves 
		FETCH NEXT FROM #cursorClaves Into @sFarmacia, @sPeriodo  
			WHILE @@FETCH_STATUS = 0 
			BEGIN 
				
				If @iItem = 0 
					Begin 
						Set @sFarmaciasRemision = @sFarmaciasRemision + ltrim(rtrim(@sFarmacia)) --- + ' ' + @sPeriodo + '  ' + char(13) 
					End 
				Else 
					Begin 
						Set @sFarmaciasRemision = @sFarmaciasRemision + ', ' + char(13) + ltrim(rtrim(@sFarmacia))   -- + char(10)--- + ' ' + @sPeriodo + '  ' + char(13) 
					End 				 

				Set @iItem = @iItem + 1 

				--print @sFarmaciasRemision 

				FETCH NEXT FROM #cursorClaves Into @sFarmacia, @sPeriodo 
			END
		Close #cursorClaves 
		Deallocate #cursorClaves 

		print @sFarmaciasRemision 
		Update #tmpConcentrado Set FarmaciasRemision = (case when InformacionAdicional_Almacen_NombreBeneficiario = '' then @sFarmaciasRemision else InformacionAdicional_Almacen_NombreBeneficiario end)	 
------------------------------------------ OBTENER LISTA DE FARMACIAS INVOLUCRADAS  


	----------------------------- Validar los parámetros de Observaciones y Referencias 
	
	Update R Set 
		InformacionDeContrato = (case when @bObservaciones_01 = 1 then InformacionDeContrato else '' end), 
		InformacionAdicional = (case when @bObservaciones_02 = 1 then InformacionAdicional else '' end), 
		FarmaciasRemision = (case when @bObservaciones_03 = 1 then FarmaciasRemision else '' end), 
		Referencia = (case when @bReferencia_01 = 1 then FarmaciasRemision else '' end)  
	From #tmpConcentrado R 


	----	@bReferencia_01 bit, 
	----@bReferencia_02 bit, 
	----@bReferencia_03 bit, 
	----@bReferencia_04 bit, 
	----@bReferencia_05 bit 


	----------------------------- Validar los parámetros de Observaciones y Referencias 


----------------------------------------------- Generar la tabla para Timbrado Masivo 
	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__01__Encabezado' and xType = 'U' ) 
	Begin 
		Select 
			cast('' as varchar(500)) as FolioRemision, 
			cast('' as varchar(500)) as TipoDeRemision, cast('' as varchar(500)) as TipoDeRemisionDesc, 
			cast('' as varchar(500)) as TipoInsumo, cast('' as varchar(500)) as TipoDeInsumo,  
			cast('' as varchar(500)) as OrigenInsumo, cast('' as varchar(500)) as OrigenInsumoDesc, 
			cast('' as varchar(500)) as IdFuenteFinanciamiento, cast('' as varchar(500)) as FuenteFinanciamiento, 
			cast('' as varchar(500)) as IdFinanciamiento, cast('' as varchar(500)) as Financiamiento, 
			cast('' as varchar(500)) as IdCliente, cast('' as varchar(500)) as Cliente, 
			cast('' as varchar(500)) as IdSubCliente, cast('' as varchar(500)) as SubCliente, 
			SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
			cast('' as varchar(max)) as InformacionDeContrato, cast('' as varchar(max)) as FarmaciasRemision, 
			cast('' as varchar(max)) as InformacionAdicional, 	
			cast('' as varchar(max)) as Referencia_Remisiones, 
			cast('' as varchar(max)) as Referencia_Facturas, 
			FechaInicial, FechaFinal, cast('' as varchar(500)) as PeriodoRemision, 		
			@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento 
		Into FACT_CFDI_TM__01__Encabezado 
		From #tmpConcentrado (NoLock) 	
		Where 1 = 0  
	End 
----------------------------------------------- Generar la tabla para Timbrado Masivo 


--	drop table FACT_CFDI_TM__01__Encabezado 
		
	--	cast(@sLista_Remisiones as varchar(max)) as Referencia_Remisiones, 
	--	cast(@sLista_Facturas as varchar(max)) as Referencia_Facturas, 

------------------------------------------ SALIDA FINAL	
	If @Identificador_UUID = '' 
		Begin  
			print 'Manual'
			Select * From #tmpConcentrado 
		End 
	Else 
		Begin 

			Delete From FACT_CFDI_TM__01__Encabezado Where Identificador_UUID = @Identificador_UUID 

			Insert Into FACT_CFDI_TM__01__Encabezado 
			( 
				FolioRemision, TipoDeRemision, TipoDeRemisionDesc, TipoInsumo, TipoDeInsumo,  OrigenInsumo, OrigenInsumoDesc, 
				IdFuenteFinanciamiento, FuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
				IdCliente, Cliente, IdSubCliente, SubCliente, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
				InformacionDeContrato, FarmaciasRemision, InformacionAdicional, 
				Referencia_Remisiones, Referencia_Facturas, 
				FechaInicial, FechaFinal, PeriodoRemision, 		
				Identificador_UUID, FechaProcesamiento 
			) 
			Select 
				FolioRemision, TipoDeRemision, TipoDeRemisionDesc, TipoInsumo, TipoDeInsumo,  OrigenInsumo, OrigenInsumoDesc, 
				IdFuenteFinanciamiento, FuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
				IdCliente, Cliente, IdSubCliente, SubCliente, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
				InformacionDeContrato, FarmaciasRemision, InformacionAdicional, 
				Referencia_Remisiones, Referencia_Facturas, 
				FechaInicial, FechaFinal, PeriodoRemision, 		
				@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento 
			From #tmpConcentrado (NoLock) 	
		End 

	-- Select * From #tmpDetalles  
------------------------------------------ SALIDA FINAL	
	

End
Go--#SQL

