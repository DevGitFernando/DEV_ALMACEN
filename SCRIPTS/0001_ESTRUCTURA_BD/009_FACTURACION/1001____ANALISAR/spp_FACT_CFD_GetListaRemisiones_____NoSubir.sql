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
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(max) = '', 
	@Detallado bit = 1, @Aplicar_Mascara bit = 1, @Identificador_UUID varchar(500) = '', 
	@MostrarResumen int = 0, @EsConcentrado int = 0  
) 
With Encryption		
As 
Begin 
Set NoCount On 
Declare 
	@Anexar__Lotes_y_Caducidades bit, 
	@EsInsumos bit, 	
	@EsMedicamento bit, 
	@sUnidadDeMedida varchar(100) 
	
Declare 
	@sSql varchar(max), 
	@Keyx int, 
	@IdClaveSSA varchar(50), 
	@ClaveSSA varchar(50), 
	@ClaveSSA_Descripcion varchar(50), 
	@Informacion varchar(max), 
	@IdCliente varchar(10), 
	@IdSubCliente varchar(10), 		
	@SAT_ClaveProducto_Servicio varchar(20),  
	@SAT_UnidadDeMedida varchar(20) 

--------- Obtener datos iniciales 	
	Set @sUnidadDeMedida = ''
	Set @Keyx = 0 
	Set @ClaveSSA = '' -- 'SERVICIO' 
	Set @IdClaveSSA = '' -- 'SERVICIO' 
	Set @ClaveSSA_Descripcion = '' -- 'SERVICIO DE DISPENSACIÓN' -- DE MEDICAMENTO Y MATERIAL DE CURACIÓN' 
	Set @Informacion = '' 
	Set @EsInsumos = 0  
	Set @EsMedicamento = 0  	

	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	Set @IdCliente = '' 
	Set @IdSubCliente = '' 
	--Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 


	Set @SAT_ClaveProducto_Servicio = ''  
	Set @SAT_UnidadDeMedida = '' 
	
	Select @sUnidadDeMedida = Descripcion From FACT_CFD_UnidadesDeMedida (NoLock) Where IdUnidad = '001'
	Select @Anexar__Lotes_y_Caducidades = dbo.fg_FACT_GetParametro_AnexarLotes_y_Caducidades( @IdEstado, @IdFarmaciaGenera )  

	Select @SAT_ClaveProducto_Servicio = Valor 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_SAT_ClaveProducto__Servicio' 

	Select @SAT_UnidadDeMedida = Valor 
	From Net_CFG_Parametros_Facturacion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and NombreParametro = 'Factura_SAT_UnidadDeMedida__Servicio' 

	Set @SAT_ClaveProducto_Servicio = IsNull(@SAT_ClaveProducto_Servicio, '80141700') 
	Set @SAT_UnidadDeMedida = IsNull(@SAT_UnidadDeMedida, 'ACT') 

	If @SAT_ClaveProducto_Servicio Is null or @SAT_ClaveProducto_Servicio = '' 
	   Set @SAT_ClaveProducto_Servicio = '80141700' 

	If @SAT_UnidadDeMedida Is null or @SAT_UnidadDeMedida = '' 
	   Set @SAT_UnidadDeMedida = 'ACT' 



	------------------------------- Listado de Remisiones 
	Select Top 0 IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
	Into #tmp_ListaRemisiones 
	From FACT_Remisiones D (NoLock) 
	Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 


	If @EsConcentrado = 0 
		Begin 
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
	------------------------------- Listado de Remisiones 




--------- Obtener datos iniciales 	

	--	select * from vw_FACT_FuentesDeFinanciamiento_Detalle 

------------------- INFORMACION BASE  
	Select Top 1 @IdCliente = E.IdCliente, @IdSubCliente = E.IdSubCliente  
	From vw_FACT_FuentesDeFinanciamiento_Detalle E (NoLock)	
	Inner Join FACT_Remisiones D (NoLock) 
		On ( E.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And E.IdEstado = D.IdEstado And E.IdFinanciamiento = D.IdFinanciamiento and E.StatusDetalle = 'A' ) 
	Inner Join #tmp_ListaRemisiones R (NoLock) On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmaciaGenera = R.IdFarmaciaGenera and D.FolioRemision = R.FolioRemision  ) 
	--Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision


	Select	
			-- E.IdEmpresa, Cast( '' as varchar(100) ) as Empresa, E.IdEstado, Cast( '' as varchar(50) ) as Estado, 
			-- E.IdFarmaciaGenera, Cast( '' as varchar(100) ) as FarmaciaGenera, 
			-- sum(E.SubTotalSinGrabar) as SubTotalSinGrabar_Remision, sum(E.SubTotalGrabado) as SubTotalGrabado_Remision, 
			-- sum(E.Iva as Iva_Remision, sum(E.Total) as Total_Remision, 	
			
			identity(int, 1, 1) as Keyx, 
			-- ROW_NUMBER() OVER(ORDER BY ClaveSSA ) AS Keyx,   
			E.IdEmpresa, 
			@IdEstado as IdEstado, 
			@IdCliente as IdCliente, @IdSubCliente as IdSubCliente,   
			space(4) as IdClaveSSA, 
			ltrim(rtrim(D.ClaveSSA)) as ClaveSSA, 
			D.ClaveSSA as ClaveSSA_Base, 
			Cast( '' as varchar(max) ) as Descripcion, 
			
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
	Group by E.IdEmpresa, E.IdEstado, E.IdFarmaciaGenera, E.TipoDeRemision, D.ClaveSSA, E.TipoDeRemision, E.TipoInsumo    
	Having sum(D.Cantidad_Agrupada) > 0   


	---- SOLO ADMINISTRACIÓN	
	Delete #tmpDetalles 
	From #tmpDetalles 
	Where IdEmpresa = '001' and IdEstado = '28' and TipoDeRemision in ( 2, 6 ) and Cantidad < 1   



--	spp_FACT_CFD_GetRemision	

	-------- Eliminar Decimales menores a 1 
	----If Exists ( select top 1 TipoDeRemision From #tmpDetalles Where TipoDeRemision in ( 2, 6 )  ) 
	----Begin 
	----	Update D Set Cantidad = 0  
	----	From #tmpDetalles D (NoLock)  
	----	Where TipoDeRemision in ( 2, 6 ) and Cantidad > 0 and Cantidad < 1 
	----End 


----		spp_FACT_CFD_GetRemision @FolioRemision = '4184'	


-------------------------------- AJUSTE DE DECIMALES    
----		spp_FACT_CFD_GetRemision @FolioRemision = '3727', @MostrarResumen = 1 

---		1,283,954.2660 
--		1,310,234.5300  

	Update D Set 	
		SubTotalSinGrabar = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0) 
	From #tmpDetalles D (NoLock) 
	where TasaIVA = 0 

	Update D Set 	
		---- BASE 
		--SubTotalGrabado = round(round(round(PrecioUnitario * Cantidad, 4, 0), 4, 0), 4, 0), 
		SubTotalGrabado = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0), 
		-- SubTotalGrabado_Aux = round(round(round(PrecioUnitario * Cantidad, 4, 0), 3, 0), 2, 0) 
		SubTotalGrabado_Aux = dbo.fg_PRCS_Redondear(PrecioUnitario * Cantidad, 2, 0)  
	From #tmpDetalles D (NoLock) 
	where TasaIVA > 0 


	--Update D Set 
	--	SubTotalGrabado = round(round(PrecioUnitario * Cantidad, 4, 1), 2, 1)   
	--	SubTotalSinGrabar = round(round(round(PrecioUnitario * Cantidad, 4, 1), 3, 1), 2, 1), 
	--	SubTotalSinGrabar = round(round(round(PrecioUnitario * Cantidad, 4, 0), 4, 0), 4, 0), 
	--	SubTotalGrabado = round(PrecioUnitario * Cantidad, 4, 1), 
	--	SubTotalSinGrabar_Aux = round(round(round(PrecioUnitario * Cantidad, 4, 0), 3, 0), 2, 0) 
	--From #tmpDetalles D (NoLock) 
	--where TasaIVA = 0  


	----Update D Set 
	----	--SubTotalGrabado = round(round(PrecioUnitario * Cantidad, 4, 1), 2, 1)   
	----	SubTotalGrabado = round(PrecioUnitario * Cantidad, 3, 1),  
	----	SubTotalGrabado_Aux = round(round(round(PrecioUnitario * Cantidad, 4, 1), 3, 1), 2, 1) 
	----From #tmpDetalles D (NoLock) 
	----where TasaIVA > 0 


	----Update D Set 
	----	--SubTotalGrabado = round(round(PrecioUnitario * Cantidad, 4, 1), 2, 1)   
	----	SubTotalGrabado = round(PrecioUnitario * Cantidad, 2, 1), 
	----	SubTotalGrabado_Aux = round(round(round(PrecioUnitario * Cantidad, 4, 1), 3, 1), 2, 1) 
	----From #tmpDetalles D (NoLock) 
	----where TasaIVA > 0 


----		spp_FACT_CFD_GetRemision @FolioRemision = '5928', @MostrarResumen = 0  


	----Update D Set Iva = round(round(round(SubTotalGrabado, 4, 1), 3, 1), 2, 1)  --round(SubTotalGrabado * ( TasaIva / 100.00 ), 4, 1) 
	----From #tmpDetalles D (NoLock) 
	----where TasaIVA > 0 

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




	Select Top 1  
		@EsInsumos = (case when cast(TipoDeRemision as int) = 1 then 1 else 0 end),  	
		@EsMedicamento = (case when cast(TipoInsumo as int) = 2 then 1 else 0 end) 		
	From #tmpDetalles   

	If @EsInsumos = 0 
	Begin 
		Set @ClaveSSA = 'SERVICIO' 
		Set @IdClaveSSA = 'SERVICIO' 
		Set @ClaveSSA_Descripcion = 'SERVICIO DE DISPENSACIÓN' -- DE MEDICAMENTO Y MATERIAL DE CURACIÓN' 
	End 
------------------- INFORMACION BASE  	

--	spp_FACT_CFD_GetRemision	
	


------------------- Información Adicional 
	Select * 
	Into #vw_ClavesSSA_Sales
	From vw_ClavesSSA_Sales 
	
	Update D Set 
			IdClaveSSA = C.IdClaveSSA_Sal, 
			-- Descripcion = '[' + D.ClaveSSA + ']  ' + C.DescripcionClave + space(6) + '<< ' + C.Presentacion + ' >>', 
			Descripcion = C.DescripcionClave + ' -- ' + C.Presentacion + '', 
			UnidadDeMedida = @sUnidadDeMedida   
	From #tmpDetalles D (NoLock)  
	Inner Join #vw_ClavesSSA_Sales C On ( D.ClaveSSA = C.ClaveSSA  )  


	Update L Set ClaveSSA = P.ClaveSSA_Relacionada, EsRelacionada = 1  
	From #tmpDetalles L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente 
			and L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 





	----- MASCARAS SEGUN CLIENTE 
	if @Aplicar_Mascara = 1 
	Begin 
		Update  D Set Descripcion = M.DescripcionMascara + ' ' + ( case when ltrim(rtrim(M.Presentacion)) = '' then '' else ' -- ' + M.Presentacion + '' end)  
		From #tmpDetalles D 
		Left Join vw_ClaveSSA_Mascara M (NoLock) 
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


	If Exists ( select top 1 TipoDeRemision From #tmpDetalles Where TipoDeRemision in ( 2, 6 )  ) 
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
		Declare #cursorClaves 
		Cursor For 
		Select Keyx, ClaveSSA   
		From #tmpDetalles 
		Order By Keyx 
		Open #cursorClaves
		FETCH NEXT FROM #cursorClaves Into @Keyx, @ClaveSSA  
			WHILE @@FETCH_STATUS = 0 
			BEGIN 				
				Exec spp_FACT_CFD_GetRemision_Detalles @IdEmpresa, @IdEstado, @IdFarmaciaGenera, @FolioRemision, @ClaveSSA, @Informacion output 
				
				Update D Set Descripcion = D.Descripcion + space(3) + @Informacion 
				From #tmpDetalles D 
				Where Keyx = @Keyx   
				 
				FETCH NEXT FROM #cursorClaves Into @Keyx, @ClaveSSA 
			END
		Close #cursorClaves 
		Deallocate #cursorClaves 			
	End 	
------------------------------------------ Anexar Lotes y Caducidades 


------------------------------------------ Quitar saltos de linea 
	-- Exec sp_FormatearTabla  #tmpDetalles 


--	select * from #tmpDetalles r 

---		spp_FACT_CFD_GetRemision 

----------------------------------------------- Generar la tabla para Timbrado Masivo 
	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__02__DetallesClaves' and xType = 'U' ) 
	Begin 
		Select 
			cast(Keyx as int) as Orden, IdEstado, IdClaveSSA, 
			cast(ClaveSSA as varchar(100)) as ClaveSSA, cast(Descripcion as varchar(5000)) as Descripcion, 		
			PrecioUnitario, Cantidad, TasaIva, SubTotal, Iva, Importe,  
			UnidadDeMedida, TipoDeRemision, TipoInsumo,  SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
			@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento 
		Into FACT_CFDI_TM__02__DetallesClaves 
		From #tmpDetalles (NoLock) 	
		Where 1 = 0  
	End 
----------------------------------------------- Generar la tabla para Timbrado Masivo 



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
						From #tmpDetalles D 

						Select * 
						From #tmpDetalles D 
						Where 
							-- SubTotalGrabado <> SubTotalGrabado_Aux 
							SubTotalSinGrabar <> SubTotalSinGrabar_Aux  
					End 


					Select	
						-- @Anexar__Lotes_y_Caducidades, 
						-- @EsInsumos, @EsMedicamento,	
						ClaveSSA as IdClaveSSA, ClaveSSA, 
						SAT_ClaveProducto_Servicio,
						Descripcion, PrecioUnitario, Cantidad, TasaIva, SubTotal, Iva, Importe, UnidadDeMedida, 
						SAT_UnidadDeMedida, SubTotalGrabado_Aux 
					From #tmpDetalles D (NoLock)
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
						From #tmpDetalles D 


						Select * 
						From #tmpDetalles D 
						Where 
						( SubTotalSinGrabar <> SubTotalSinGrabar_Aux ) 
						or 
						( SubTotalGrabado <> SubTotalGrabado_Aux  ) 
						or 
						( IVA <> Iva_Aux ) 

						--Select * 
						--From #tmpDetalles D 
						--Where SubTotalGrabado <> SubTotalGrabado_Aux 
					End 
				End		
			Else	
				Begin	
					Select	
						-- @Anexar__Lotes_y_Caducidades, 
						-- @EsInsumos, @EsMedicamento,	
						@IdClaveSSA as IdClaveSSA, @ClaveSSA as ClaveSSA, @ClaveSSA_Descripcion as Descripcion, 	
						cast(sum(D.SubTotal) as numeric(14,2)) as PrecioUnitario, 
						cast(1 as numeric(14,4)) as Cantidad, 
						cast(D.TasaIva as numeric(14,2)) as TasaIva, 
						cast(sum(D.SubTotal) as numeric(14,2)) as SubTotal, 
						cast(sum(D.Iva) as numeric(14,2)) as Iva, 
						cast(sum(D.Importe) as numeric(14,2)) as Importe,  
						@sUnidadDeMedida as UnidadDeMedida	
					From #tmpDetalles D (NoLock)	
					Group by TasaIva  
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
				IdEstado, IdClaveSSA, ClaveSSA, Descripcion, 		
				PrecioUnitario, Cantidad, TasaIva, SubTotal, Iva, Importe,  
				UnidadDeMedida, TipoDeRemision, TipoInsumo,  SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
				@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento 
			From #tmpDetalles (NoLock) 
			Order by Descripcion  	
		End 
------------------------------------------ SALIDA FINAL			


End
Go--#SQL



---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_FACT_CFD_GetRemision_Detalles' and xType = 'P' )
    Drop Proc spp_FACT_CFD_GetRemision_Detalles
Go--#SQL
  
Create Proc spp_FACT_CFD_GetRemision_Detalles  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(max) = '', 
	@ClaveSSA varchar(50) = '11', @Informacion varchar(max) = '' output, 
	@EsConcentrado int = 0    
) 
With Encryption		
As
Begin
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sDatos varchar(50),  
	@sDatosCaducidad varchar(max) 

	Set @sDatos = '' 
	Set @sDatosCaducidad = '' 

----------------- Obtener datos 

---		spp_FACT_CFD_GetRemision	

		----Select distinct IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, ClaveSSA, IdProducto, CodigoEAN, ClaveLote  
		----From FACT_Remisiones_Detalles C (NoLock) 
		----Where 
		----	C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado 
		----	And C.IdFarmaciaGenera = @IdFarmaciaGenera And C.FolioRemision = @FolioRemision	
		----	And C.ClaveSSA = @ClaveSSA 	
			

	------------------------------- Listado de Remisiones 
	Select Top 0 IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
	Into #tmp_ListaRemisiones_Detalles 
	From FACT_Remisiones D (NoLock) 
	Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 


	If @EsConcentrado = 0 
		Begin 
			Insert Into #tmp_ListaRemisiones_Detalles ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision  ) 
			Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
			From FACT_Remisiones D (NoLock) 
			Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision 
		End 
	Else 
		Begin 
			Set @sSql = '' + 
			'Insert Into ##tmp_ListaRemisiones_Detalles ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision  ) ' + char(10) + 
			'Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ' + char(10) + 
			'From FACT_Remisiones D (NoLock) ' + char(10) + 
			'Where D.IdEstado = ' + char(39) + @IdEstado + char(39) + ' And D.IdFarmaciaGenera = ' + char(39) + @IdFarmaciaGenera + char(39) + ' And D.FolioRemision in ( ' + @FolioRemision + ' ) '  
			Exec( @sSql ) 
		End 
	------------------------------- Listado de Remisiones 




	Select Identity(int, 1, 1 ) as Keyx, 
		-- '[' + (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) + ']' as Informacion   
		-- (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) as Informacion   		
		('LOTE: ' + E.ClaveLote + '		CADUCIDAD: ' + IsNull('' + convert(varchar(7), L.FechaCaducidad, 120), '')) as Informacion 
	Into #tmpDetalles__Lotes  	
	From   
	( 
		Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.IdSubFarmacia, C.ClaveSSA, C.IdProducto, C.CodigoEAN, C.ClaveLote  
		From FACT_Remisiones_Detalles C (NoLock) 
		Inner Join #tmp_ListaRemisiones_Detalles D (NoLock) On ( C.IdEmpresa = D.IdEmpresa and C.IdEstado = D.IdEstado and C.IdFarmaciaGenera = D.IdFarmaciaGenera and C.FolioRemision = D.FolioRemision )
		Where 
			C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado 
			And C.IdFarmaciaGenera = @IdFarmaciaGenera -- And C.FolioRemision = @FolioRemision	
			And C.ClaveSSA = @ClaveSSA 	
		Group by C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.IdSubFarmacia, C.ClaveSSA, C.IdProducto, C.CodigoEAN, C.ClaveLote 
	) E 
	Left Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia 
			 and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote ) 
	Group by E.IdSubFarmacia, E.ClaveLote, IsNull('' + convert(varchar(7), L.FechaCaducidad, 120), '')  
	--Order by L.IdSubFarmacia, L.ClaveLote  	



----------------- Obtener datos 	
	


	--Select * from #tmpDetalles__Lotes 
	
	----Select *, char(39) + Informacion + char(39)  
	----from #tmpDetalles__Lotes 
	
	Declare #cursorDetalles 
	Cursor For 
	Select Informacion   
	From #tmpDetalles__Lotes (NoLock) 
	Order by Keyx 
	Open #cursorDetalles
	FETCH NEXT FROM #cursorDetalles Into @sDatos  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 				
			-- Set @sDatosCaducidad = @sDatosCaducidad + @sDatos + ' ' 		
			Set @sDatosCaducidad = @sDatosCaducidad + @sDatos + ', '	
			FETCH NEXT FROM #cursorDetalles Into  @sDatos  
		END
	Close #cursorDetalles 
	Deallocate #cursorDetalles 	

	Set @sDatosCaducidad = ltrim(rtrim(@sDatosCaducidad)) 
	Set @sDatosCaducidad = left(@sDatosCaducidad, len(@sDatosCaducidad)-1) 
	Select @Informacion = @sDatosCaducidad 
	Print @Informacion 

End 
Go--#SQL 
