---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_CFD_GetRemision' and xType = 'P' )
    Drop Proc spp_FACT_CFD_GetRemision
Go--#SQL 
  
---		Exec spp_FACT_CFD_GetRemision @Identificador_UUID = 'XX'

Create Proc spp_FACT_CFD_GetRemision 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '1006', 
	@Detallado bit = 1, @Aplicar_Mascara bit = 0, @Identificador_UUID varchar(500) = '' 
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
	@Keyx int, 
	@IdClaveSSA varchar(50), 
	@ClaveSSA varchar(50), 
	@ClaveSSA_Descripcion varchar(50), 
	@Informacion varchar(max), 
	@IdCliente varchar(10), 
	@IdSubCliente varchar(10)   	

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
	Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 
	Set @IdCliente = '' 
	Set @IdSubCliente = '' 

	
	Select @sUnidadDeMedida = Descripcion From FACT_CFD_UnidadesDeMedida (NoLock) Where IdUnidad = '001'
	Select @Anexar__Lotes_y_Caducidades = dbo.fg_FACT_GetParametro_AnexarLotes_y_Caducidades( @IdEstado, @IdFarmaciaGenera )  
--------- Obtener datos iniciales 	

	--	select * from vw_FACT_FuentesDeFinanciamiento_Detalle 

------------------- INFORMACION BASE  
	Select @IdCliente = E.IdCliente, @IdSubCliente = E.IdSubCliente  
	From vw_FACT_FuentesDeFinanciamiento_Detalle E (NoLock)	
	Inner Join FACT_Remisiones D (NoLock) 
		On ( E.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And E.IdEstado = D.IdEstado And E.IdFinanciamiento = D.IdFinanciamiento and E.StatusDetalle = 'A' )
	Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmaciaGenera And D.FolioRemision = @FolioRemision


	Select	
			-- E.IdEmpresa, Cast( '' as varchar(100) ) as Empresa, E.IdEstado, Cast( '' as varchar(50) ) as Estado, 
			-- E.IdFarmaciaGenera, Cast( '' as varchar(100) ) as FarmaciaGenera, 
			-- sum(E.SubTotalSinGrabar) as SubTotalSinGrabar_Remision, sum(E.SubTotalGrabado) as SubTotalGrabado_Remision, 
			-- sum(E.Iva as Iva_Remision, sum(E.Total) as Total_Remision, 	
			
			identity(int, 1, 1) as Keyx, 
			-- ROW_NUMBER() OVER(ORDER BY ClaveSSA ) AS Keyx,   
			@IdEstado as IdEstado, 
			@IdCliente as IdCliente, @IdSubCliente as IdSubCliente,   
			space(4) as IdClaveSSA, D.ClaveSSA, Cast( '' as varchar(max) ) as Descripcion, 
			
			cast(max(D.PrecioLicitado) as numeric(14,2)) as PrecioUnitario, 
			cast(sum(D.Cantidad_Agrupada) as numeric(14,4)) as Cantidad, 

			-- cast(max(D.PrecioLicitadoUnitario) as numeric(14,2)) as PrecioUnitario, 
			-- cast(sum(D.Cantidad) as numeric(14,4)) as Cantidad, 

			cast(max(D.TasaIva) as numeric(14,2)) as TasaIva, 
			cast(sum(D.SubTotalSinGrabar + D.SubTotalGrabado) as numeric(14,2)) as SubTotal, 
			cast(sum(D.Iva) as numeric(14,2)) as Iva, 
			cast(sum(D.Importe) as numeric(14,2)) as Importe,  
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
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision	
	Group by E.IdEmpresa, E.IdEstado, E.IdFarmaciaGenera, E.TipoDeRemision, D.ClaveSSA, E.TipoDeRemision, E.TipoInsumo    
	Having sum(D.Cantidad_Agrupada) > 0  


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


	----- MASCARAS SEGUN CLIENTE 
	if @Aplicar_Mascara = 1 
	Begin 
		Update  D Set Descripcion = M.Descripcion + ' ' + ( case when ltrim(rtrim(M.Presentacion)) = '' then '' else ' -- ' + M.Presentacion + '' end)  
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
					Select	
						-- @Anexar__Lotes_y_Caducidades, 
						-- @EsInsumos, @EsMedicamento,	
						ClaveSSA as IdClaveSSA, ClaveSSA, 
						SAT_ClaveProducto_Servicio,
						Descripcion, PrecioUnitario, Cantidad, TasaIva, SubTotal, Iva, Importe, UnidadDeMedida, 
						SAT_UnidadDeMedida 	
					From #tmpDetalles D (NoLock)
					Order By Descripcion 
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
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '0000000004', 
	@ClaveSSA varchar(50) = '11', @Informacion varchar(max) = '' output   
) 
With Encryption		
As
Begin
Set NoCount On 
Declare 
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
			

	Select Identity(int, 1, 1 ) as Keyx, 
		-- '[' + (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) + ']' as Informacion   
		-- (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) as Informacion   		
		('LOTE: ' + E.ClaveLote + '		CADUCIDAD: ' + IsNull('' + convert(varchar(7), L.FechaCaducidad, 120), '')) as Informacion 
	Into #tmpDetalles__Lotes  	
	From   
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, ClaveSSA, IdProducto, CodigoEAN, ClaveLote  
		From FACT_Remisiones_Detalles C (NoLock) 
		Where 
			C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado 
			And C.IdFarmaciaGenera = @IdFarmaciaGenera And C.FolioRemision = @FolioRemision	
			And C.ClaveSSA = @ClaveSSA 	
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
