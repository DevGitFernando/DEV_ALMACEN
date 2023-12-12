---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_CFD_GetRemision' and xType = 'P' )
    Drop Proc spp_FACT_CFD_GetRemision  
Go--#SQL 
  
/* 

	Exec spp_FACT_CFD_GetRemision	
		@IdEmpresa = '001', @IdEstado = '28', @IdFarmaciaGenera = '0001', 
		@FolioRemision = '5953', @Detallado = '1', @Aplicar_Mascara = '0', @Identificador_UUID = '', @MostrarResumen = '0' 

*/ 


Create Proc spp_FACT_CFD_GetRemision 
( 
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '22', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(max) = '31', 
	@Detallado bit = 1, @Aplicar_Mascara bit = 1, @Identificador_UUID varchar(500) = '', 
	@MostrarResumen int = 0, @EsConcentrado int = 0, @Anexar_Informacion_Lotes_y_Caducidades int = 1, 
	@Aplicar_Mascara_Claves bit = 1   
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
	@bEsConcentrado_Servicio bit, 
	@bRemision_Medicamento bit, 
	@bRemision_MaterialDeCuracion bit
	
Declare 
	@sSql varchar(max), 
	@Keyx int, 
	@sSpacios_SAT varchar(1000), 

	@IdFuenteFinanciamiento varchar(50), 
	@IdFinanciamiento varchar(50), 

	@IdClaveSSA varchar(50), 
	@ClaveSSA varchar(50), 
	@ClaveSSA_Descripcion varchar(max), 
	@Informacion varchar(max), 
	@IdCliente varchar(10), 
	@IdSubCliente varchar(10), 		
	@SAT_ClaveProducto_Servicio varchar(20),  
	@SAT_UnidadDeMedida varchar(20), 
	@dCuota_ServicioGeneral numeric(14,4), 
	@bServicioUnitario bit, 
	@dCantidad_ServicioGeneral numeric(14,4), 
	@sDescripcion_01_Servicio_Concentrado__General varchar(max), 
	@sDescripcion_01_Servicio_Concentrado__Medicamento varchar(max), 
	@sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion varchar(max),  
	@bUnidadDosisUnitaria bit   

--------- Obtener datos iniciales 	
	Set @sUnidadDeMedida = ''
	Set @Keyx = 0 
	Set @ClaveSSA = '' -- 'SERVICIO' 
	Set @IdClaveSSA = '' -- 'SERVICIO' 
	Set @ClaveSSA_Descripcion = '' -- 'SERVICIO DE DISPENSACIÓN' -- DE MEDICAMENTO Y MATERIAL DE CURACIÓN' 
	Set @Informacion = '' 
	Set @EsInsumos = 0  
	Set @EsMedicamento = 0  	
	Set @bRemision_Medicamento = 0  
	Set @bRemision_MaterialDeCuracion = 0 
	Set @bServicioUnitario = 0 
	Set @bUnidadDosisUnitaria = 0 

	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	Set @IdCliente = '' 
	Set @IdSubCliente = '' 
	Set @IdFuenteFinanciamiento = ''  
	Set @IdFinanciamiento  = ''   
	Set @sSpacios_SAT = REPLICATE('', 1000) 

	--Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 


	Set @SAT_ClaveProducto_Servicio = ''  
	Set @SAT_UnidadDeMedida = '' 
	Set @bEsConcentrado_Servicio = 0 
	Set @sDescripcion_01_Servicio_Concentrado__General = '' 
	Set @sDescripcion_01_Servicio_Concentrado__Medicamento = '' 
	Set @sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion = '' 
	
	Select @sUnidadDeMedida = Descripcion From FACT_CFD_UnidadesDeMedida (NoLock) Where IdUnidad = '001'
	Select @Anexar__Lotes_y_Caducidades = dbo.fg_FACT_GetParametro_AnexarLotes_y_Caducidades( @IdEstado, @IdFarmaciaGenera )  

	Select @SAT_ClaveProducto_Servicio = Valor 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_SAT_ClaveProducto__Servicio' 

	Select @SAT_UnidadDeMedida = Valor 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_SAT_UnidadDeMedida__Servicio' 

	------------------------------------------------------------------------------------------------------ 
	Select @dCuota_ServicioGeneral = cast('0' + Valor as numeric(14,4)) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Remision___CuotaServicio_General' 

	Select @bServicioUnitario = (case when Valor = 'true' then 1 else 0 end)  -- cast('0' + Valor as numeric(14,4)) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Remision___Servicio_Unitario' 

	Select @dCantidad_ServicioGeneral = cast('0' + Valor as numeric(14,4)) 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Remision___CantidadServicio_General' 

	Select @sDescripcion_01_Servicio_Concentrado__General = Valor 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Remision___DescripcionServicio_General' 
	------------------------------------------------------------------------------------------------------ 

	------------------------------------------------------------------------------------------------------ 
	Select @sDescripcion_01_Servicio_Concentrado__Medicamento = Valor 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Remision___DescripcionServicio_Medicamento' 

	Select @sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion = Valor 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_Remision___DescripcionServicio_MC' 
	------------------------------------------------------------------------------------------------------ 

	Set @bServicioUnitario = ISNULL(@bServicioUnitario, 1) 
	Set @dCuota_ServicioGeneral = IsNull(@dCuota_ServicioGeneral, 9000000) 
	Set @dCantidad_ServicioGeneral = IsNull(@dCantidad_ServicioGeneral, 1) 
	Set @sDescripcion_01_Servicio_Concentrado__General = IsNull(@sDescripcion_01_Servicio_Concentrado__General, 'SERVICIO DE DISPENSACIÓN DE MEDICAMENTOS Y MATERIAL EN FARMACIA SUBROGADA') 
	Set @sDescripcion_01_Servicio_Concentrado__Medicamento = IsNull(@sDescripcion_01_Servicio_Concentrado__Medicamento, 'SERVICIO DE DISPENSACIÓN DE MEDICAMENTOS EN FARMACIA SUBROGADA') 
	Set @sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion = IsNull(@sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion, 'SERVICIO DE DISPENSACIÓN DE MATERIAL DE CURACIÓN E INSUMOS EN FARMACIA SUBROGADA') 



	Set @SAT_ClaveProducto_Servicio = IsNull(@SAT_ClaveProducto_Servicio, '80141700') 
	Set @SAT_UnidadDeMedida = IsNull(@SAT_UnidadDeMedida, 'ACT') 

	If @SAT_ClaveProducto_Servicio Is null or @SAT_ClaveProducto_Servicio = '' 
	   Set @SAT_ClaveProducto_Servicio = '80141700' 

	If @SAT_UnidadDeMedida Is null or @SAT_UnidadDeMedida = '' 
	   Set @SAT_UnidadDeMedida = 'ACT' 

	----select 
	----	@sDescripcion_01_Servicio_Concentrado__Medicamento as sDescripcion_01_Servicio_Concentrado__Medicamento, 
	----	@sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion as sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion 

	If @Anexar_Informacion_Lotes_y_Caducidades = 1 
	Begin 
		Set @Anexar__Lotes_y_Caducidades = 1 
	End 

	------------------------------- Listado de Remisiones 
	--select char(39) + @FolioRemision + char(39) as XTY 
	Select Top 0 IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
	Into #tmp_ListaRemisiones 
	From FACT_Remisiones D (NoLock) 
	Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 


	----DECLARE @position INT, @string CHAR(8);  
	------ Initialize the current position and the string variables.  
	----SET @position = 1;  
	----SET @string = 'New Moon';  


	If @EsConcentrado = 0 
		Begin 
			------Print 'R    ccc      X' + @FolioRemision 
			----select * from dbo.fg_Cadena_GetChar(@FolioRemision) 

			----select char(39) + @FolioRemision + char(39) as XTY_X 			
			----Set @string = @FolioRemision 
	
			----WHILE @position <= DATALENGTH(@string)  
			----   BEGIN  
			----   SELECT ASCII(SUBSTRING(@string, @position, 1)),   
			----	  CHAR(ASCII(SUBSTRING(@string, @position, 1)))  
			----   SET @position = @position + 1  
			----   END;  

			Set @FolioRemision = ltrim(rtrim(@FolioRemision))  
			Set @FolioRemision = REPLACE(@FolioRemision, ' ', '')  
			Set @FolioRemision = REPLACE(@FolioRemision, char(9), '')  
			Set @FolioRemision = REPLACE(@FolioRemision, char(10), '')  
			Set @FolioRemision = REPLACE(@FolioRemision, char(13), '')  



	
			--select * from dbo.fg_Cadena_GetChar(@FolioRemision) 
			
			Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 

			Insert Into #tmp_ListaRemisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision  ) 
			Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
			From FACT_Remisiones D (NoLock) 
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

	--select '' as SEPALA, * from  #tmp_ListaRemisiones 
	------------------------------- Listado de Remisiones 




--------- Obtener datos iniciales 	

	--	select * from vw_FACT_FuentesDeFinanciamiento_Detalle 

------------------- INFORMACION BASE  
	Select Top 1 
		@IdCliente = E.IdCliente, @IdSubCliente = E.IdSubCliente,	
		@IdFuenteFinanciamiento = D.IdFuenteFinanciamiento,  @IdFinanciamiento = D.IdFinanciamiento       
	From vw_FACT_FuentesDeFinanciamiento_Detalle E (NoLock)	
	Inner Join FACT_Remisiones D (NoLock) 
		On ( E.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And E.IdEstado = D.IdEstado And E.IdFinanciamiento = D.IdFinanciamiento and E.StatusDetalle = 'A' ) 
	Inner Join #tmp_ListaRemisiones R (NoLock) On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmaciaGenera = R.IdFarmaciaGenera and D.FolioRemision = R.FolioRemision  ) 
	--Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision


	Select	
			-- top 10 
			-- E.IdEmpresa, Cast( '' as varchar(100) ) as Empresa, E.IdEstado, Cast( '' as varchar(50) ) as Estado, 
			-- E.IdFarmaciaGenera, Cast( '' as varchar(100) ) as FarmaciaGenera, 
			-- sum(E.SubTotalSinGrabar) as SubTotalSinGrabar_Remision, sum(E.SubTotalGrabado) as SubTotalGrabado_Remision, 
			-- sum(E.Iva as Iva_Remision, sum(E.Total) as Total_Remision, 	
			
			identity(int, 1, 1) as Keyx, 
			-- ROW_NUMBER() OVER(ORDER BY ClaveSSA ) AS Keyx,   
			E.IdEmpresa, 
			@IdEstado as IdEstado, 
			E.IdFarmaciaGenera, E.FolioRemision, D.IdFarmacia as IdFarmaciaDispensacion, 
			@IdCliente as IdCliente, @IdSubCliente as IdSubCliente,   
			space(10) as IdClaveSSA, 
			cast(ltrim(rtrim(D.ClaveSSA)) as varchar(100)) as ClaveSSA, 
			D.ClaveSSA as ClaveSSA_Base, 

			( case when @Anexar_Informacion_Lotes_y_Caducidades = 1 then D.IdSubFarmacia else '' end ) as IdSubFarmacia,  
			( case when @Anexar_Informacion_Lotes_y_Caducidades = 1 then D.CodigoEAN else '' end ) as CodigoEAN,  
			( case when @Anexar_Informacion_Lotes_y_Caducidades = 1 then D.ClaveLote else '' end ) as ClaveLote,  

			Cast( '' as varchar(max) ) as Descripcion, 
			Cast( '' as varchar(max) ) as Descripcion_Base, 
			
			0 as EsRelacionada, 

			cast(max(D.PrecioLicitado) as numeric(14,2)) as PrecioUnitario, 
			cast(sum(D.Cantidad_Agrupada) as numeric(14,4)) as Cantidad, 

			-- cast(max(D.PrecioLicitadoUnitario) as numeric(14,2)) as PrecioUnitario, 
			-- cast(sum(D.Cantidad) as numeric(14,4)) as Cantidad, 

			cast(max(D.TasaIva) as numeric(14,2)) as TasaIva, 
			sum(D.SubTotalGrabado) as SubTotalGrabado_Aux, 
			sum(D.SubTotalSinGrabar) as SubTotalSinGrabar_Aux, 

			sum(D.SubTotalSinGrabar) as SubTotalSinGrabar, 
			sum(D.SubTotalGrabado) as SubTotalGrabado, 
			cast(sum(D.SubTotalSinGrabar + D.SubTotalGrabado) as numeric(14,4)) as SubTotal, 
			cast(sum(D.Iva) as numeric(14,4)) as Iva, 
			cast(sum(D.Iva) as numeric(14,4)) as Iva_Aux, 
			cast(sum(D.Iva) as numeric(14,4)) as Iva_Aux_02, 
			cast(sum(D.Importe) as numeric(14,4)) as Importe,  
			-- E.TipoDeRemision, 
			--( Case When TipoDeRemision = 1 Then 'Insumo' Else 'Administración' End ) as Tipo 
			space(100) as UnidadDeMedida, 
			TipoDeRemision, TipoInsumo, 
			cast('' as varchar(100)) as SAT_ClaveProducto_Servicio, 
			cast('' as varchar(100)) as SAT_UnidadDeMedida	  
	Into #tmpDetalles 
	From FACT_Remisiones E (NoLock)	
	--Inner Join FACT_Remisiones_Resumen D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision )
	Inner Join FACT_Remisiones_Detalles D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision ) 
	Inner Join #tmp_ListaRemisiones R (NoLock) On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmaciaGenera = R.IdFarmaciaGenera and D.FolioRemision = R.FolioRemision  ) 
	--Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision		
		-- and ltrim(rtrim(ClaveSSA)) = '060.168.6620'  	
	--Where D.ClaveSSA in  ( '010.000.5099.00'  )  
	--Where D.ClaveSSA in  ( '010.000.4264.00' ) 
	--Where D.PrecioLicitado > 0 
	Group by E.IdEmpresa, E.IdEstado, E.IdFarmaciaGenera, E.FolioRemision, D.IdFarmacia, E.TipoDeRemision, D.ClaveSSA, E.TipoDeRemision, E.TipoInsumo 
		, ( case when @Anexar_Informacion_Lotes_y_Caducidades = 1 then D.IdSubFarmacia else '' end ) 
		, ( case when @Anexar_Informacion_Lotes_y_Caducidades = 1 then D.CodigoEAN else '' end ) 
		, ( case when @Anexar_Informacion_Lotes_y_Caducidades = 1 then D.ClaveLote else '' end ) 		
	Having sum(D.Cantidad_Agrupada) > 0   
	Order by D.ClaveSSA 

	--		spp_FACT_CFD_GetRemision 



	---------------------------- VALIDACION DE PROCESO CONCENTRADO 
	Select Top 1  
		@EsInsumos = (case when cast(TipoDeRemision as int) = 1 then 1 else 0 end),  	
		@EsMedicamento = (case when cast(TipoInsumo as int) = 2 then 1 else 0 end) 		
	From #tmpDetalles   

	Select Top 1 @bUnidadDosisUnitaria = 1 
	From #tmpDetalles D (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmaciaDispensacion = F.IdFarmacia and F.EsUnidosis = 1 )


	If @EsConcentrado = 1  
	Begin 
		--print 'POR AQUI '
		Select Top 1 @bRemision_Medicamento = 1 From #tmpDetalles (NoLock) Where TipoInsumo = '02'  
		Select Top 1 @bRemision_MaterialDeCuracion = 1 From #tmpDetalles (NoLock) Where TipoInsumo = '01' 

		Set @bRemision_Medicamento = IsNull(@bRemision_Medicamento, 0) 
		Set @bRemision_MaterialDeCuracion = IsNull(@bRemision_MaterialDeCuracion, 0) 

		If @bRemision_Medicamento = 1 and @bRemision_MaterialDeCuracion = 1 
		Begin 
			Set @bEsConcentrado_Servicio = 1 
		End 

		-- select @EsInsumos, @EsMedicamento, @bRemision_Medicamento, @bRemision_MaterialDeCuracion 
	End 


	If @EsInsumos = 0 
	Begin 
		Set @ClaveSSA = 'SERVICIO' 
		Set @IdClaveSSA = 'SERVICIO' 
		Set @ClaveSSA_Descripcion = 'SERVICIO DE DISPENSACIÓN' -- DE MEDICAMENTO Y MATERIAL DE CURACIÓN'   

		Set @ClaveSSA_Descripcion = @sDescripcion_01_Servicio_Concentrado__General 
	


		---------------------- VALIDAR INFOMRACION DE CONCEPTO 
		If Exists ( Select * 
					From FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) 
					Where D.IdFuenteFinanciamiento = @IdFuenteFinanciamiento and D.IdFinanciamiento = @IdFinanciamiento 
						and D.Alias <> '' 
				  ) 
		Begin    
			Select @ClaveSSA_Descripcion = (case when @EsMedicamento = 1 then @sDescripcion_01_Servicio_Concentrado__Medicamento else @sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion end) 
		End 

	End 
	---------------------------- VALIDACION DE PROCESO CONCENTRADO 



	----------- Forzar a 2 decimales para cumplir con las reglas del SAT 
	--Update D Set Cantidad = dbo.fg_PRCS_Redondear(Cantidad, 2, 0) 
	--from #tmpDetalles  D 


	---- SOLO ADMINISTRACIÓN	
	Delete #tmpDetalles 
	From #tmpDetalles 
	Where IdEmpresa = '001' and IdEstado = '28' and TipoDeRemision in ( 2, 6 ) and Cantidad < 1   



--		spp_FACT_CFD_GetRemision	


	Update D Set 	
		--SubTotalSinGrabar = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 1) 
		SubTotalSinGrabar = dbo.fg_PRCS_Redondear(round(PrecioUnitario * Cantidad, 2, 0), 2, 0) 
	From #tmpDetalles D (NoLock) 
	where TasaIVA = 0 

	----select * 
	----From #tmpDetalles D (NoLock) 



	Update D Set 	
		---- BASE 
		--SubTotalGrabado = round(round(round(PrecioUnitario * Cantidad, 4, 0), 4, 0), 4, 0), 
		
		
		--SubTotalGrabado = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0), 
		SubTotalGrabado = dbo.fg_PRCS_Redondear(round(PrecioUnitario * Cantidad, 2, 1), 2, 0), 
		-- SubTotalGrabado_Aux = round(round(round(PrecioUnitario * Cantidad, 4, 0), 3, 0), 2, 0) 
		SubTotalGrabado_Aux = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0)  
	From #tmpDetalles D (NoLock) 
	where TasaIVA > 0 


----		spp_FACT_CFD_GetRemision @FolioRemision = '5928', @MostrarResumen = 0  

	Update D Set 
			--Iva = round(SubTotalGrabado * ( TasaIva / 100.00 ), 4, 1),  
			Iva = dbo.fg_PRCS_Redondear((SubTotalGrabado * ( TasaIva / 100.00 )), 2, 0), 
			Iva_Aux = dbo.fg_PRCS_Redondear((SubTotalGrabado * ( TasaIva / 100.00 )), 2, 0), 
			Iva_Aux_02 = (SubTotalGrabado * ( TasaIva / 100.00 ))  				  
	From #tmpDetalles D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 3, 1) 
	From #tmpDetalles D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 2, 1) 
	From #tmpDetalles D (NoLock) 
	where TasaIVA > 0 


	------- SubTotal, Importe 
	Update D Set SubTotal = (D.SubTotalSinGrabar + D.SubTotalGrabado), Importe = (D.SubTotalSinGrabar + D.SubTotalGrabado + D.IVA)
	From #tmpDetalles D (NoLock) 
-------------------------------- AJUSTE DE DECIMALES    


	-------------------------------------- EXCLUSIVO PARA LAS UNIDADES QUE MANEJAN DOSIS UNITARIA   
	If @Detallado = 0 and @EsInsumos = 0  and @bUnidadDosisUnitaria = 1 
	Begin 

		Select	
				IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFarmaciaDispensacion, 
				IdCliente, IdSubCliente,   
				IdClaveSSA, ClaveSSA, ClaveSSA_Base, 
				IdSubFarmacia, CodigoEAN, ClaveLote,  
				Descripcion, Descripcion_Base, EsRelacionada, 

				(select sum(SubTotalGrabado) From #tmpDetalles ) as PrecioUnitario, 
				cast(1 as numeric(14,4)) as Cantidad, 


				cast(max(TasaIva) as numeric(14,2)) as TasaIva, 
				cast(0 as numeric(14,4)) as SubTotalGrabado_Aux, 
				cast(0 as numeric(14,4)) as SubTotalSinGrabar_Aux, 
				cast(0 as numeric(14,4)) as SubTotalSinGrabar, 
				cast(0 as numeric(14,4)) as SubTotalGrabado, 
				cast(0 as numeric(14,4)) as SubTotal, 

				cast(0 as numeric(14,4)) as Iva, 
				cast(0 as numeric(14,4)) as Iva_Aux, 
				cast(0 as numeric(14,4)) as Iva_Aux_02, 
				cast(0 as numeric(14,4)) as Importe, 
				UnidadDeMedida, 
				TipoDeRemision, TipoInsumo, 
				SAT_ClaveProducto_Servicio, 
				SAT_UnidadDeMedida	  
		Into #tmpDetalles_Resumen 
		From #tmpDetalles 
		Group by 
				IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFarmaciaDispensacion, 
				IdCliente, IdSubCliente,   
				IdClaveSSA, ClaveSSA, ClaveSSA_Base, 
				IdSubFarmacia, CodigoEAN, ClaveLote,  
				Descripcion, Descripcion_Base, EsRelacionada, 
				--PrecioUnitario, 
				UnidadDeMedida, 
				TipoDeRemision, TipoInsumo, 
				SAT_ClaveProducto_Servicio, 
				SAT_UnidadDeMedida		
					

		Delete from #tmpDetalles 
		Insert Into #tmpDetalles 
		Select 
			Top 1 
			--'HOOOOLLLLA', 
			IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFarmaciaDispensacion, 
			IdCliente, IdSubCliente,   
			IdClaveSSA, ClaveSSA, ClaveSSA_Base, 
			IdSubFarmacia, CodigoEAN, ClaveLote,  
			Descripcion, Descripcion_Base, EsRelacionada, 

			(PrecioUnitario) as PrecioUnitario, 
			cast(1 as numeric(14,4)) as Cantidad, 
			(TasaIva) as TasaIVA, 

			cast(0 as numeric(14,4)) as SubTotalGrabado_Aux, 
			cast(0 as numeric(14,4)) as SubTotalSinGrabar_Aux, 
			cast(0 as numeric(14,4)) as SubTotalSinGrabar, 
			cast(0 as numeric(14,4)) as SubTotalGrabado, 
			cast(0 as numeric(14,4)) as SubTotal, 

			cast(0 as numeric(14,4)) as Iva, 
			cast(0 as numeric(14,4)) as Iva_Aux, 
			cast(0 as numeric(14,4)) as Iva_Aux_02, 
			cast(0 as numeric(14,4)) as Importe, 
			UnidadDeMedida, 
			TipoDeRemision, TipoInsumo, 
			SAT_ClaveProducto_Servicio, 
			SAT_UnidadDeMedida	  
		From #tmpDetalles_Resumen 

	End 
	-------------------------------------- EXCLUSIVO PARA LAS UNIDADES QUE MANEJAN DOSIS UNITARIA   



------------------- INFORMACION BASE  	

--	spp_FACT_CFD_GetRemision	
	


------------------- Información Adicional 
	Select * 
	Into #vw_ClavesSSA_Sales
	From vw_ClavesSSA_Sales 
	
	Update D Set 
			IdClaveSSA = C.IdClaveSSA_Sal, 
			-- 20230127.1136  Descripcion = C.DescripcionClave + ' -- ' + C.Presentacion + '', 
			Descripcion = C.DescripcionClave,  
			-- 20230127.1136  Descripcion_Base = left(C.DescripcionClave + ' -- ' + C.Presentacion + '' + @sSpacios_SAT, 1000), 
			Descripcion_Base = left(C.DescripcionClave + '' + @sSpacios_SAT, 1000), 
			UnidadDeMedida = @sUnidadDeMedida   
	From #tmpDetalles D (NoLock)  
	Inner Join #vw_ClavesSSA_Sales C On ( D.ClaveSSA = C.ClaveSSA  )  


	Update L Set ClaveSSA = P.ClaveSSA_Relacionada, EsRelacionada = 1  
	From #tmpDetalles L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente 
			and L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 


	--select *, len(Descripcion), len(Descripcion_Base)  from #tmpDetalles



	----- MASCARAS SEGUN CLIENTE 
	if @Aplicar_Mascara = 1 
	Begin 
		Update  D Set 
			--Descripcion = M.DescripcionMascara + ' ' + ( case when ltrim(rtrim(M.Presentacion)) = '' then '' else ' -- ' + M.Presentacion + '' end)  
			Descripcion = M.DescripcionMascara 
		From #tmpDetalles D 
		Inner Join vw_ClaveSSA_Mascara M (NoLock) 
			On ( D.IdEstado = M.IdEstado and D.ClaveSSA = M.ClaveSSA and M.IdCliente = @IdCliente and M.IdSubCliente = @IdSubCliente  ) 
	End 

	------------- Asignar los datos según catálogo del SAT 
	Update D Set 
		SAT_ClaveProducto_Servicio = M.SAT_ClaveDeProducto_Servicio,  
		SAT_UnidadDeMedida = M.SAT_UnidadDeMedida  
	From #tmpDetalles D 
	Inner Join vw_Claves_Precios_Asignados M (NoLock) 
		On ( D.IdEstado = M.IdEstado and D.ClaveSSA = M.ClaveSSA and M.IdCliente = @IdCliente and M.IdSubCliente = @IdSubCliente  ) 

-----	spp_FACT_CFD_GetRemision 

	------------- SOBRA 
	----select * 
	----From vw_Claves_Precios_Asignados D (NoLock) 
	----Where D.ClaveSSA = '060.168.6620' 

	----select * 
	----from #tmpDetalles D 
	----Where D.ClaveSSA = '060.168.6620'  

	----select * 
	----From #tmpDetalles D 
	----Inner Join vw_Claves_Precios_Asignados M (NoLock) 
	----	On ( D.IdEstado = M.IdEstado and D.ClaveSSA = M.ClaveSSA and M.IdCliente = @IdCliente and M.IdSubCliente = @IdSubCliente  ) 
	----Where D.ClaveSSA = '060.168.6620' 


	If Exists ( select top 1 TipoDeRemision From #tmpDetalles Where TipoDeRemision in ( 2, 6, 7, 8 )  ) 
	Begin 	

	----Set @SAT_ClaveProducto_Servicio = IsNull(@SAT_ClaveProducto_Servicio, '93151507') 
	----Set @SAT_UnidadDeMedida = IsNull(@SAT_UnidadDeMedida, 
		-- print 'SAT'

		Update D Set 
			SAT_ClaveProducto_Servicio = @SAT_ClaveProducto_Servicio,  
			SAT_UnidadDeMedida = @SAT_UnidadDeMedida  
		From #tmpDetalles D 
	End 


	----Update D Set 
	----	SAT_ClaveProducto_Servicio = '01010101',  SAT_UnidadDeMedida = 'H87'
	----From #tmpDetalles D 

------------------- Información Adicional 



	
--		sp_listacolumnas__Stores	spp_FACT_CFD_GetRemision  	, 1 

---		spp_FACT_CFD_GetRemision	



------------------------------------------ Anexar Lotes y Caducidades 
	------ Select @Anexar__Lotes_y_Caducidades,  @EsInsumos,  @EsMedicamento 
	--If @Anexar__Lotes_y_Caducidades = 1	
	--Begin	
	--	If @EsInsumos = 1 and @EsMedicamento  = 1	
	--	   Set @Anexar__Lotes_y_Caducidades = 1	
	--	Else 
	--	   Set @Anexar__Lotes_y_Caducidades = 0	
	--End		
	------ Select @Anexar__Lotes_y_Caducidades,  @EsInsumos,  @EsMedicamento 	
	

	------------------------------ Aplica para SSH Primer Nivel  --===> 2017-10-24 
	Update E Set Descripcion = Descripcion + space(3) + 'MARCA: ' + M.DescripcionComercial 
	From #tmpDetalles E (NoLock) 
	Inner Join FACT_ClavesSSA_InformacionMarcaComercial M (NoLock) 
		On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente and E.ClaveSSA = M.ClaveSSA ) 
	------------------------------ Aplica para SSH Primer Nivel  --===> 2017-10-24 


	If @Anexar__Lotes_y_Caducidades = 1 
	Begin 
				

		Select Identity(int, 1, 1 ) as Keyx, 
			-- '[' + (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) + ']' as Informacion   
			-- (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) as Informacion 
			E.IdSubFarmacia, E.CodigoEAN, E.ClaveLote, 
			cast(
			(
				'EAN: ' + E.CodigoEAN + '	LOTE: ' + E.ClaveLote + '	CADUCIDAD: ' + IsNull('' + convert(varchar(7), L.FechaCaducidad, 120), '')+ '	LAB: ' 
			) as varchar(900) ) as Informacion 
		Into #tmpDetalles__Lotes  	
		From   
		( 
			Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.IdSubFarmacia, C.ClaveSSA, C.IdProducto, C.CodigoEAN, C.ClaveLote  
			From FACT_Remisiones_Detalles C (NoLock) 
			Inner Join #tmpDetalles D (NoLock) 
				On ( C.IdEmpresa = D.IdEmpresa and C.IdEstado = D.IdEstado and C.IdFarmaciaGenera = D.IdFarmaciaGenera and C.FolioRemision = D.FolioRemision and C.ClaveSSA = D.ClaveSSA  )
			Where 
				C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado 
				And C.IdFarmaciaGenera = @IdFarmaciaGenera -- And C.FolioRemision = @FolioRemision	
				-- And C.ClaveSSA = @ClaveSSA 	
			Group by C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.IdSubFarmacia, C.ClaveSSA, C.IdProducto, C.CodigoEAN, C.ClaveLote 
		) E 
		Left Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
			On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia 
				 and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote ) 
		Group by E.IdSubFarmacia, E.CodigoEAN, E.ClaveLote, IsNull('' + convert(varchar(7), L.FechaCaducidad, 120), '')  


		Update E Set Informacion = Informacion + ' ' + P.Laboratorio 
		From #tmpDetalles__Lotes E 
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( E.CodigoEAN = P.CodigoEAN ) 


--		spp_FACT_CFD_GetRemision  

		--select 'LOTES' as Campo, * 
		--from #tmpDetalles__Lotes 


		Update E Set Descripcion = ( left(E.Descripcion_Base, 994 - len(L.Informacion) )) + space(3) + char(10) + L.Informacion 
			-- 'EAN: ' + E.CodigoEAN + '	LOTE: ' + E.ClaveLote + '	CADUCIDAD: ' + IsNull('' + convert(varchar(7), L.FechaCaducidad, 120), '')  
		From #tmpDetalles E  
		Inner Join #tmpDetalles__Lotes L (NoLock) 
			-- On ( E.Keyx = L.Keyx ) 
			On ( E.IdSubFarmacia = L.IdSubFarmacia and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote ) 
		--where 1 = 0 

		--select *, len(Descripcion), len(Descripcion_Base) from  #tmpDetalles 

		----Declare #cursorClaves 
		----Cursor For 
		----Select Keyx, ClaveSSA   
		----From #tmpDetalles 
		----Order By Keyx 
		----Open #cursorClaves
		----FETCH NEXT FROM #cursorClaves Into @Keyx, @ClaveSSA  
		----	WHILE @@FETCH_STATUS = 0 
		----	BEGIN 				
		----		Exec spp_FACT_CFD_GetRemision_Detalles @IdEmpresa, @IdEstado, @IdFarmaciaGenera, @FolioRemision, @ClaveSSA, @Informacion output 
				
		----		Update D Set Descripcion = D.Descripcion + space(3) + char(10) + @Informacion 
		----		From #tmpDetalles D 
		----		Where Keyx = @Keyx   
				 
		----		FETCH NEXT FROM #cursorClaves Into @Keyx, @ClaveSSA 
		----	END
		----Close #cursorClaves 
		----Deallocate #cursorClaves 			
	End 	
------------------------------------------ Anexar Lotes y Caducidades 


------------------------------------------ Quitar saltos de linea 
	-- Exec sp_FormatearTabla  #tmpDetalles 


	----- MASCARAS SEGUN CLIENTE .............. QUERETARO 
	if @Aplicar_Mascara_Claves = 1 
	Begin 
		Update  D Set ClaveSSA = Mascara  
			-- Descripcion = M.DescripcionMascara + ' ' + ( case when ltrim(rtrim(M.Presentacion)) = '' then '' else ' -- ' + M.Presentacion + '' end)  
		From #tmpDetalles D 
		Inner Join vw_ClaveSSA_Mascara M (NoLock) 
			On ( D.IdEstado = M.IdEstado and D.ClaveSSA = M.ClaveSSA and M.IdCliente = @IdCliente and M.IdSubCliente = @IdSubCliente  ) 
	End 


--	select * from #tmpDetalles r 

---		spp_FACT_CFD_GetRemision 

----------------------------------------------- Generar la tabla para Timbrado Masivo 
	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__02__DetallesClaves' and xType = 'U' ) 
	Begin 
		Select 
			cast(Keyx as int) as Orden, 
			cast(IdEstado as varchar(100)) as IdEstado, 
			cast(IdClaveSSA as varchar(100)) as IdClaveSSA, 
			--IdEstado, 
			--IdClaveSSA, 
			cast(ClaveSSA as varchar(100)) as ClaveSSA, cast(Descripcion as varchar(max)) as Descripcion, 		
			PrecioUnitario, Cantidad, TasaIva, SubTotal, Iva, Importe,  
			UnidadDeMedida, TipoDeRemision, TipoInsumo,  SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
			@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento 
		Into FACT_CFDI_TM__02__DetallesClaves 
		From #tmpDetalles (NoLock) 	
		Where 1 = 0  
	End 

----------------------------------------------- Generar la tabla para Timbrado Masivo 


-------------------------------------------------------------------------------------------------------- 
----------------------------------------------- Generar la tabla FINAL para Timbrado  
	--------------------------------------- DETALLADO 
	Select	
		-- @Anexar__Lotes_y_Caducidades, 
		-- @EsInsumos, @EsMedicamento,	
		IdEstado, TipoDeRemision, TipoInsumo, 
		ClaveSSA as IdClaveSSA, ClaveSSA, 
		SAT_ClaveProducto_Servicio, 
		Descripcion, PrecioUnitario, 

		TasaIva, 
		UnidadDeMedida, 
		SAT_UnidadDeMedida,  

		sum(Cantidad) as Cantidad, 

		cast(0 as numeric(14, 4)) as SubTotalSinGrabar, 
		cast(0 as numeric(14, 4)) as SubTotalGrabado,  
		cast(0 as numeric(14, 4)) as SubTotal, 
		cast(0 as numeric(14, 4)) as IVA, 
		cast(0 as numeric(14, 4)) as Importe, 
		
		cast(0 as numeric(14, 4)) as SubTotalSinGrabar_Aux, 
		cast(0 as numeric(14, 4)) as SubTotalGrabado_Aux, 
		cast(0 as numeric(14, 4)) as IVA_Aux 
			  
	Into #tmpDetalles___Final 
	From #tmpDetalles D (NoLock) 
	Group by 
		IdEstado, TipoDeRemision, TipoInsumo, 
		ClaveSSA, SAT_ClaveProducto_Servicio, 
		Descripcion, PrecioUnitario, 
		TasaIva, UnidadDeMedida, SAT_UnidadDeMedida 

	--select 'AQUI', * from #tmpDetalles___Final 


	---------------- AQUI VA EL AJUSTE PARA EL CONCENTRADO 
	---------------- AQUI VA EL AJUSTE PARA EL CONCENTRADO 


	----------- Forzar a 2 decimales para cumplir con las reglas del SAT 
	Update D Set 
		Cantidad = dbo.fg_PRCS_Redondear(Cantidad, 4, 0) 
		--Cantidad = round(Cantidad, 2, 1) 
	from #tmpDetalles___Final  D 

	--select 'AQUI', * from #tmpDetalles___Final 


	Update D Set 	
		SubTotalSinGrabar = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 4, 0) 
	From #tmpDetalles___Final D (NoLock) 
	where TasaIVA = 0 


	-- select 'AQUI 02', * from #tmpDetalles___Final 


	Update D Set 	
		SubTotalGrabado = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 4, 0), 
		SubTotalGrabado_Aux = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 4, 0)  
	From #tmpDetalles___Final D (NoLock) 
	where TasaIVA > 0 

	Update D Set 
		SubTotalSinGrabar = round(SubTotalSinGrabar, 2, 1),  
		SubTotalGrabado = round(SubTotalGrabado, 2, 1) 
	From #tmpDetalles___Final D (NoLock) 

	Update D Set 
			--Iva = round(SubTotalGrabado * ( TasaIva / 100.00 ), 4, 1),  
			Iva = dbo.fg_PRCS_Redondear((SubTotalGrabado * ( TasaIva / 100.00 )), 4, 0), 
			Iva_Aux = dbo.fg_PRCS_Redondear((SubTotalGrabado * ( TasaIva / 100.00 )), 4, 0)  
	From #tmpDetalles___Final D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 3, 1) 
	From #tmpDetalles___Final D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 2, 1) 
	From #tmpDetalles___Final D (NoLock) 
	where TasaIVA > 0 


	------- SubTotal, Importe 
	Update D Set SubTotal = (D.SubTotalSinGrabar + D.SubTotalGrabado), Importe = (D.SubTotalSinGrabar + D.SubTotalGrabado + D.IVA)
	From #tmpDetalles___Final D (NoLock) 
	--------------------------------------- DETALLADO 



	--------------------------------------- CONCENTRADO DE SERVICIO 
	Select	
		-- @Anexar__Lotes_y_Caducidades, 
		-- @EsInsumos, @EsMedicamento,	
		IdEstado, 
		--TipoDeRemision, TipoInsumo, 
		(case when @bEsConcentrado_Servicio = 1 then '00' else TipoDeRemision end) as TipoDeRemision, 
		(case when @bEsConcentrado_Servicio = 1 then '00' else TipoInsumo end) as TipoInsumo, 

		@IdClaveSSA as IdClaveSSA, @ClaveSSA as ClaveSSA, 
		SAT_ClaveProducto_Servicio, 
		@ClaveSSA_Descripcion as Descripcion, 
		
		PrecioUnitario, 
		TasaIva, 
		UnidadDeMedida, 
		SAT_UnidadDeMedida,  
		sum(Cantidad) as Cantidad, 

		cast(0 as numeric(14, 4)) as SubTotalSinGrabar, 
		cast(0 as numeric(14, 4)) as SubTotalGrabado,  
		cast(0 as numeric(14, 4)) as SubTotal, 
		cast(0 as numeric(14, 4)) as IVA, 
		cast(0 as numeric(14, 4)) as Importe, 
		
		cast(0 as numeric(14, 4)) as SubTotalSinGrabar_Aux, 
		cast(0 as numeric(14, 4)) as SubTotalGrabado_Aux, 
		cast(0 as numeric(14, 4)) as IVA_Aux 
			  
	Into #tmpConcentrado_Servicio___Final 
	From #tmpDetalles___Final D (NoLock) 
	Where @EsInsumos = 0 
	Group by 
		IdEstado, 
		(case when @bEsConcentrado_Servicio = 1 then '00' else TipoDeRemision end), 
		(case when @bEsConcentrado_Servicio = 1 then '00' else TipoInsumo end), 
		--ClaveSSA, 
		SAT_ClaveProducto_Servicio, 
		-- Descripcion, 
		PrecioUnitario, 
		TasaIva, UnidadDeMedida, SAT_UnidadDeMedida 


	-- select * from #tmpConcentrado_Servicio___Final  


	--Set @sDescripcion_01_Servicio_Concentrado__General = IsNull(@sDescripcion_01_Servicio_Concentrado__General, 'SERVICIO DE DISPENSACIÓN DE MEDICAMENTOS Y MATERIAL EN FARMACIA SUBROGADA') 
	--Set @sDescripcion_01_Servicio_Concentrado__Medicamento = IsNull(@sDescripcion_01_Servicio_Concentrado__Medicamento, 'SERVICIO DE DISPENSACIÓN DE MEDICAMENTOS EN FARMACIA SUBROGADA') 
	--Set @sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion = IsNull(@sDescripcion_02_Servicio_Concentrado__MaterialDeCuracion, 'SERVICIO DE DISPENSACIÓN DE MATERIAL DE CURACIÓN E INSUMOS EN FARMACIA SUBROGADA') 
	

	If @bEsConcentrado_Servicio = 1 
	Begin 
		--		spp_FACT_CFD_GetRemision    

		Update D Set 
			TipoDeRemision = '00', TipoInsumo = '00', 
			Descripcion = @sDescripcion_01_Servicio_Concentrado__General, 
			Cantidad = @dCantidad_ServicioGeneral, 
			PrecioUnitario = (case when @bServicioUnitario = 1 then PrecioUnitario else @dCuota_ServicioGeneral end) 
		From #tmpConcentrado_Servicio___Final D 
		Where @bServicioUnitario = 0 

	End 

	--select * from #tmpConcentrado_Servicio___Final 

	--Set @bServicioUnitario = 1 
	If @bServicioUnitario  = 1  
		Begin 
			Update D Set 
				Cantidad = 1, 
				PrecioUnitario = dbo.fg_PRCS_Redondear(( select sum(PrecioUnitario * Cantidad) From #tmpConcentrado_Servicio___Final ), 2, 0) 
			From #tmpConcentrado_Servicio___Final D 
			Where @EsConcentrado = 1 
		End 


	-- set nocount off 

	-- select * from #tmpConcentrado_Servicio___Final 

	Update D Set 	
		SubTotalSinGrabar = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0) 
	From #tmpConcentrado_Servicio___Final D (NoLock) 
	where TasaIVA = 0 

	Update D Set 	
		SubTotalGrabado = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0), 
		SubTotalGrabado_Aux = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0)  
	From #tmpConcentrado_Servicio___Final D (NoLock) 
	where TasaIVA > 0 

	Update D Set 
			--Iva = round(SubTotalGrabado * ( TasaIva / 100.00 ), 4, 1),  
			Iva = dbo.fg_PRCS_Redondear((SubTotalGrabado * ( TasaIva / 100.00 )), 2, 0), 
			Iva_Aux = dbo.fg_PRCS_Redondear((SubTotalGrabado * ( TasaIva / 100.00 )), 2, 0)  
	From #tmpConcentrado_Servicio___Final D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 3, 1) 
	From #tmpConcentrado_Servicio___Final D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 2, 1) 
	From #tmpConcentrado_Servicio___Final D (NoLock) 
	where TasaIVA > 0 


	------- SubTotal, Importe 
	Update D Set SubTotal = (D.SubTotalSinGrabar + D.SubTotalGrabado), Importe = (D.SubTotalSinGrabar + D.SubTotalGrabado + D.IVA)
	From #tmpConcentrado_Servicio___Final D (NoLock) 
	--------------------------------------- CONCENTRADO DE SERVICIO 

	-- select * from #tmpConcentrado_Servicio___Final 


----------------------------------------------- Generar la tabla FINAL para Timbrado  
-------------------------------------------------------------------------------------------------------- 



	--select * from #tmpConcentrado_Servicio___Final 

	--select * from #tmpDetalles___Final 


---		spp_FACT_CFD_GetRemision  

------------------------------------------ SALIDA FINAL		
	If @Identificador_UUID = '' 
		Begin 
			If @Detallado = 1	
				Begin	

----		spp_FACT_CFD_GetRemision @FolioRemision = '3652'  , @MostrarResumen = 1 

					-------- MODO DEBUG  
					If @MostrarResumen = 1 
					Begin 
						Select 
							sum(D.SubTotalSinGrabar) as SubTotalSinGrabar, 
							sum(D.SubTotalSinGrabar_Aux) as SubTotalSinGrabar_Aux, 
							sum(D.SubTotalGrabado) as SubTotalGrabado, 
							cast(sum(D.SubTotalGrabado_Aux) as numeric(14, 2)) as SubTotalGrabado_Aux, 				
							cast(sum(D.SubTotal) as numeric(14, 4)) as SubTotal, 
							cast(sum(D.Iva) as numeric(14, 4)) as Iva, 
							cast(sum(D.Importe) as numeric(14, 4)) as Importe  	
						From #tmpDetalles___Final D 

						Select *  
						From #tmpDetalles___Final D 
						Where 
							-- SubTotalGrabado <> SubTotalGrabado_Aux 
							SubTotalSinGrabar <> SubTotalSinGrabar_Aux  
					End 

					----Select	
					----	-- @Anexar__Lotes_y_Caducidades, 
					----	-- @EsInsumos, @EsMedicamento,	
					----	ClaveSSA as IdClaveSSA, ClaveSSA, 
					----	SAT_ClaveProducto_Servicio,
					----	Descripcion, PrecioUnitario, sum(Cantidad) as Cantidad, 
					----	TasaIva, 
					----	sum(SubTotal) as SubTotal, sum(Iva) as IVA, sum(Importe) as Importe, UnidadDeMedida, 
					----	SAT_UnidadDeMedida, 
					----	sum(SubTotalGrabado_Aux) as SubTotalGrabado_Aux -- , 'X' as Extra  
					----From #tmpDetalles D (NoLock) 
					----Group by 
					----	ClaveSSA, SAT_ClaveProducto_Servicio, 
					----	Descripcion, PrecioUnitario, 
					----	TasaIva, UnidadDeMedida, SAT_UnidadDeMedida 
					----Order By Descripcion 

					Print 'TTTTTTTTTTT'
					Select	
						ClaveSSA as IdClaveSSA, ClaveSSA, 
						SAT_ClaveProducto_Servicio,
						left(Descripcion, 1000) as Descripcion, PrecioUnitario, (Cantidad) as Cantidad, 
						TasaIva, 
						(SubTotal) as SubTotal, (Iva) as IVA, (Importe) as Importe, UnidadDeMedida, 
						SAT_UnidadDeMedida, 
						(SubTotalGrabado_Aux) as SubTotalGrabado_Aux -- , 'X' as Extra  
					From #tmpDetalles___Final D (NoLock) 
					Order By Descripcion 



					If @MostrarResumen = 0  
					Begin 
						Select 
							sum(D.SubTotalSinGrabar) as SubTotalSinGrabar, 
							sum(D.SubTotalSinGrabar_Aux) as SubTotalSinGrabar_Aux, 
							sum(D.SubTotalGrabado) as SubTotalGrabado, 
							cast(sum(D.SubTotalGrabado_Aux) as numeric(14, 2)) as SubTotalGrabado_Aux, 				
							cast(sum(D.SubTotal) as numeric(14, 4)) as SubTotal, 
							cast(sum(D.Iva) as numeric(14, 4)) as Iva, 
							cast(sum(D.Importe) as numeric(14, 4)) as Importe  	
						From #tmpDetalles___Final D 


						Select * 
						From #tmpDetalles___Final D 
						Where 
						( SubTotalSinGrabar <> SubTotalSinGrabar_Aux ) 
						or 
						( SubTotalGrabado <> SubTotalGrabado_Aux  ) 
						or 
						( IVA <> Iva_Aux ) 




					End 
				End		
			Else	
				Begin	
					----Select	
					----	-- @Anexar__Lotes_y_Caducidades, 
					----	-- @EsInsumos, @EsMedicamento,	
					----	@IdClaveSSA as IdClaveSSA, @ClaveSSA as ClaveSSA, @ClaveSSA_Descripcion as Descripcion, 	
					----	cast(sum(D.SubTotal) as numeric(14,2)) as PrecioUnitario, 
					----	cast(1 as numeric(14,4)) as Cantidad, 
					----	cast(D.TasaIva as numeric(14,2)) as TasaIva, 
					----	cast(sum(D.SubTotal) as numeric(14,2)) as SubTotal, 
					----	cast(sum(D.Iva) as numeric(14,2)) as Iva, 
					----	cast(sum(D.Importe) as numeric(14,2)) as Importe,  
					----	@sUnidadDeMedida as UnidadDeMedida	
					----From #tmpDetalles___Final D (NoLock)	
					----Group by TasaIva  

					print 'XXXXXXXX'
					Select	
						ClaveSSA as IdClaveSSA, ClaveSSA, 
						SAT_ClaveProducto_Servicio,
						left(Descripcion, 1000) as Descripcion, PrecioUnitario, (Cantidad) as Cantidad, 
						TasaIva, 
						sum(SubTotal) as SubTotal, sum(Iva) as IVA, sum(Importe) as Importe, UnidadDeMedida, 
						SAT_UnidadDeMedida 
					From #tmpConcentrado_Servicio___Final  D (NoLock) 
					Group by 
						ClaveSSA, ClaveSSA, 
						SAT_ClaveProducto_Servicio,
						left(Descripcion, 1000), PrecioUnitario, Cantidad,  
						TasaIva, UnidadDeMedida, SAT_UnidadDeMedida 
					Order By Descripcion 


				End		

			----Select 
			----	cast(sum(D.SubTotal) as numeric(14,2)) as SubTotal, 
			----	cast(sum(D.Iva) as numeric(14,2)) as Iva, 
			----	cast(sum(D.Importe) as numeric(14,2)) as Importe  	
			----From #tmpDetalles D 

		End 
	Else 
		Begin 			
			Delete From FACT_CFDI_TM__02__DetallesClaves Where Identificador_UUID = @Identificador_UUID 

			Insert Into FACT_CFDI_TM__02__DetallesClaves 
			( 
				Orden, IdEstado, IdClaveSSA, ClaveSSA, Descripcion, PrecioUnitario, Cantidad, TasaIva, SubTotal, Iva, Importe, 
				UnidadDeMedida, TipoDeRemision, TipoInsumo, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, Identificador_UUID, FechaProcesamiento 
			) 
			Select 
				ROW_NUMBER() over (Order by Descripcion) as Orden, 
				-- ROW_NUMBER() OVER(ORDER BY SalesYTD DESC) AS Row,   

				-- cast(Keyx as int) as Orden, 
				IdEstado, IdClaveSSA, ClaveSSA, left(Descripcion, 1000) as Descripcion, 		
				PrecioUnitario, Cantidad, TasaIva, SubTotal, Iva, Importe,  
				UnidadDeMedida, TipoDeRemision, TipoInsumo,  SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
				@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento 
			From #tmpDetalles___Final (NoLock) 
			Order by Descripcion  	
		End 
------------------------------------------ SALIDA FINAL			


End
Go--#SQL

