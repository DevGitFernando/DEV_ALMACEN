------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_RTP__Distribucion_FuentesDeFinanciamiento' and xType = 'P' ) 
   Drop Proc spp_FACT_RTP__Distribucion_FuentesDeFinanciamiento 
Go--#SQL 

Create Proc spp_FACT_RTP__Distribucion_FuentesDeFinanciamiento 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0010', 
	@IdFuenteFinanciamiento varchar(4) = '0001', 
	@FechaInicial varchar(10) = '2017-02-01', @FechaFinal varchar(10) = '2017-05-30', 
	@FechaDeRevision int = 2      
)  
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sCampos varchar(500), 
	@sFuenteFinanciamiento varchar(100) 

	Set @sSql = '' 
	Set @sCampos = '' 
	Set @sFuenteFinanciamiento = '' 
	----------------------------------------------------------------------------------------------------------------------------
	--------------------------------------- Se obtienen las fuentes de financiamiento 
	Select IdFuenteFinanciamiento, IdFinanciamiento, Descripcion 
	Into #tmpFuentes 
	From FACT_Fuentes_De_Financiamiento_Detalles 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento 

	----------------------------------------------------------------------------------------------------------------------------
	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	---------------------------------------------------------------------------------------------------------------------------- 
	Select * 
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado and IdTipoUnidad <> '006' 

	If @IdFarmacia <> '' and @IdFarmacia  <> '*' 
	   Delete From #tmpFarmacias Where IdFarmacia <> RIGHT('0000' + @IdFarmacia, 4)     


	----------------------------------------------------------------------------------------------------------------------------
	--------------------------------------- Se obtienen los Folios de venta del periodo seleccionado 
	Select Top 0 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta,  V.FechaRegistro, V.FechaRegistro as FechaReceta, 
		V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
		cast('' as varchar(20)) as IdBeneficiario, cast('' as varchar(20)) as NumReceta, cast('' as varchar(20)) as FolioReferencia, 		
		0 as Procesar  
	Into #tmpVentas 
	From VentasEnc V (NoLock) 
	Where V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 


	If @FechaDeRevision = 1   ---- Fecha de registro 
	Begin 
		Insert Into #tmpVentas   
		Select 
			V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.FechaRegistro, C.FechaReceta,
			V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
			C.IdBeneficiario, C.NumReceta, cast('' as varchar(20)) as FolioReferencia, 
			0 as Procesar  		
		From VentasEnc V (NoLock)  
		Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
		Inner Join VentasInformacionAdicional C (NoLock) 
			On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
			and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
			and convert(varchar(10), V.FechaRegistro, 120) Between @FechaInicial and @FechaFinal   
	End     


	If @FechaDeRevision = 2   ---- Fecha de receta  
	Begin 
		Insert Into #tmpVentas   
		Select 
			V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.FechaRegistro, C.FechaReceta,
			V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
			C.IdBeneficiario, C.NumReceta, cast('' as varchar(20)) as FolioReferencia, 			
			0 as Procesar  
		From VentasEnc V (NoLock)  
		Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
		Inner Join VentasInformacionAdicional C (NoLock) 
			On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
			and V.IdCliente = @IdCliente And V.IdSubCliente = @IdSubCliente 
			and convert(varchar(10), C.FechaReceta, 120) Between @FechaInicial and @FechaFinal   
	End  


--	select * from #tmpFarmacias 


	---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	--------------------------------------- Se obtienen la informacion a detalle 
	Select	
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, convert(varchar(10), V.FechaReceta, 120) as FechaReceta,
		V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
		V.IdBeneficiario, cast('' as varchar(300)) as NombreBeneficario, V.NumReceta, V.FolioReferencia, 
		V.Procesar, 
		-- D.FolioVenta, 
		D.FolioRemision, D.IdFuenteFinanciamiento, D.IdFinanciamiento, 
		D.ClaveSSA, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.PrecioLicitado, D.PrecioLicitadoUnitario, 
		D.Cantidad_Agrupada, D.Cantidad, D.TasaIva, D.SubTotalSinGrabar, D.SubTotalGrabado, D.Iva, D.Importe 
	Into #tmpDetalles 
	From FACT_Remisiones_Detalles D (NoLock) 
	Inner Join #tmpVentas V (NoLock) On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioVenta = V.FolioVenta )  
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento 
		 -- And ClaveSSA like '%060.681.0067%' 
		 -- And ClaveSSA like '%060.811.0060%' 


	Update D Set NombreBeneficario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre),  FolioReferencia = B.FolioReferencia 
	From #tmpDetalles D 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( D.IdEstado = B.IdEstado and D.IdFarmacia = B.IdFarmacia and D.IdCliente = B.IdCliente and D.IdSubCliente = B.IdSubCliente and D.IdBeneficiario = B.IdBeneficiario ) 

	Select 
		IdEstado, IdFarmacia, IdFinanciamiento, year(FechaReceta) as Año, month(FechaReceta) as Mes, ClaveSSA, 
		max(PrecioLicitadoUnitario) as PrecioLicitadoUnitario, 
		max(TasaIva) as TasaIva, sum(SubTotalSinGrabar + SubTotalGrabado) as SubTotal, sum(Iva) as Iva, sum(Importe) as Importe, 
		sum(Cantidad) as Cantidad 
	Into #tmpConcentrado 
	From #tmpDetalles 
	Group by IdEstado, IdFarmacia, IdFinanciamiento, year(FechaReceta), month(FechaReceta), ClaveSSA  


	Select  
		IdEstado, IdFarmacia, Año, Mes, ClaveSSA, sum(Cantidad) as Cantidad, 0 as CantidadTotal, 
		max(PrecioLicitadoUnitario) as PrecioLicitadoUnitario,  
		max(TasaIva) as TasaIva, sum(SubTotal) as SubTotal, sum(Iva) as Iva, sum(Importe) as Importe  
	Into #tmpConcentrado__Cruze 
	From #tmpConcentrado  
	Group by IdEstado, IdFarmacia, Año, Mes, ClaveSSA 


	Select  
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, 
		year(FechaReceta) as Año, month(FechaReceta) as Mes, 
		FechaRegistro, FechaReceta,
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		IdBeneficiario, NombreBeneficario, NumReceta, FolioReferencia, 
		FolioRemision, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, 
		Cantidad_Agrupada, Cantidad, 0 as CantidadTotal, TasaIva, (SubTotalSinGrabar + SubTotalGrabado) as SubTotal, Iva, Importe 
	Into #tmpDetalles__Cruze  
	From #tmpDetalles  
	--Group by IdEstado, IdFarmacia, year(FechaReceta), month(FechaReceta), ClaveSSA 

---		spp_FACT_RTP__Distribucion_FuentesDeFinanciamiento  

	------------------------------------------------- Agregar las columnas de Fuente de Financiamiento 
	Declare #cursorTriggers  
	Cursor For 
		Select IdFinanciamiento  
		From #tmpFuentes T  
		Order by IdFinanciamiento 
	Open #cursorTriggers 
	FETCH NEXT FROM #cursorTriggers Into @sFuenteFinanciamiento 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			Set @sCampos = @sCampos + 'FF_' + cast(@sFuenteFinanciamiento as varchar) + ', ' 
			Set @sSql = 'Alter Table #tmpConcentrado__Cruze Add FF_' + cast(@sFuenteFinanciamiento as varchar) + ' Int Not Null Default 0 ' 
			Exec(@sSql) 

			Set @sSql = 'Alter Table #tmpDetalles__Cruze Add FF_' + cast(@sFuenteFinanciamiento as varchar) + ' Int Not Null Default 0 ' 
			Exec(@sSql) 

			FETCH NEXT FROM #cursorTriggers Into @sFuenteFinanciamiento    
		END	 
	Close #cursorTriggers 
	Deallocate #cursorTriggers 	
	
	Set @sCampos = ltrim(rtrim(@sCampos)) 
	Set @sCampos = left(@sCampos, len(@sCampos) - 1)	


	------------------------------------------------- Actualizar las cantidades por Fuente de Financiamiento 
	Declare #cursorTriggers   
	Cursor For 
		Select IdFinanciamiento  
		From #tmpFuentes T  
		Order by IdFinanciamiento 
	Open #cursorTriggers 
	FETCH NEXT FROM #cursorTriggers Into @sFuenteFinanciamiento 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			-- Set @sSql = 'Alter Table #tmpConcentrado__Cruze Add FF_' + cast(@sFuenteFinanciamiento as varchar) + ' Int Not Null Default 0 ' 
			Set @sSql = '' 
			Set @sSql = 
				'Update M Set FF_' + cast(@sFuenteFinanciamiento as varchar) + ' = N.Cantidad '     + char(13) + 
				'From #tmpConcentrado__Cruze M ' + char(13) + 
				'Inner Join #tmpConcentrado N 
					On ( 
						M.IdEstado = N.IdEstado and M.IdFarmacia = N.IdFarmacia and 						 
						M.Año = N.Año and M.Mes = N.Mes and M.ClaveSSA = N.ClaveSSA and N.IdFinanciamiento = ' + cast(@sFuenteFinanciamiento as varchar ) +  ' )' +  
				''
				
			Set @sSql = @sSql + 
				'Update M Set CantidadTotal = M.CantidadTotal + N.Cantidad '     + char(13) + 
				'From #tmpConcentrado__Cruze M ' + char(13) + 
				'Inner Join #tmpConcentrado N 
					On ( 
						M.IdEstado = N.IdEstado and M.IdFarmacia = N.IdFarmacia and 						 
						M.Año = N.Año and M.Mes = N.Mes and M.ClaveSSA = N.ClaveSSA and N.IdFinanciamiento = ' + cast(@sFuenteFinanciamiento as varchar ) +  ' )' +  
				''				 
			Exec(@sSql) 


			Set @sSql = '' 
			Set @sSql = 
				'Update M Set FF_' + cast(@sFuenteFinanciamiento as varchar) + ' = N.Cantidad '     + char(13) + 
				'From #tmpDetalles__Cruze M ' + char(13) + 
				'Inner Join #tmpDetalles N 
					On (
						M.IdEstado = N.IdEstado and M.IdFarmacia = N.IdFarmacia 
						and M.ClaveSSA = N.ClaveSSA and M.FolioVenta = N.FolioVenta 
						and M.IdProducto = N.IdProducto and M.CodigoEAN = N.CodigoEAN and M.ClaveLote = N.ClaveLote 
						and N.IdFinanciamiento = ' + cast(@sFuenteFinanciamiento as varchar ) +  ' )' +  
				'' 
			
			Set @sSql = @sSql + 
				'Update M Set CantidadTotal = M.CantidadTotal + N.Cantidad '     + char(13) + 
				'From #tmpDetalles__Cruze M ' + char(13) + 
				'Inner Join #tmpDetalles N 
					On (
						M.IdEstado = N.IdEstado and M.IdFarmacia = N.IdFarmacia 
						and M.ClaveSSA = N.ClaveSSA and M.FolioVenta = N.FolioVenta 
						and M.IdProducto = N.IdProducto and M.CodigoEAN = N.CodigoEAN and M.ClaveLote = N.ClaveLote 
						and N.IdFinanciamiento = ' + cast(@sFuenteFinanciamiento as varchar ) +  ' )' +  
				'' 
			Exec(@sSql) 



			FETCH NEXT FROM #cursorTriggers Into @sFuenteFinanciamiento    
		END	 
	Close #cursorTriggers 
	Deallocate #cursorTriggers 	
	---------------------------------------------------------------------------------------------------------------------------------------------------------------- 



---		spp_FACT_RTP__Distribucion_FuentesDeFinanciamiento  


-------------------------------------- SALIDA FINAL 
	Set @sSql = 
		'Select ' + char(10) +  
		'	C.IdEstado, C.IdFarmacia, F.Farmacia, C.Año, C.Mes, ' + char(10) +  
		'   M.TipoDeClave, M.TipoDeClaveDescripcion, ' + char(10) +  
		'	C.ClaveSSA, M.Mascara, M.DescripcionMascara, M.Presentacion, ' + char(10) + 
		'   C.CantidadTotal, C.PrecioLicitadoUnitario, C.TasaIva, C.SubTotal, C.Iva, C.Importe, ' + @sCampos + char(10)  + 
		'From #tmpConcentrado__Cruze C (NoLock) ' + char(10) +  
		'Inner Join #tmpFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) ' + char(10) +  
		'Left Join vw_ClaveSSA_Mascara M (NoLock) ' + char(10) + 
		'	On ( C.IdEstado = M.IdEstado and C.ClaveSSA = M.ClaveSSA and M.IdCliente = ' + char(39) + @IdCliente + char(39) + ' and M.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + ' ) ' + char(10) + 
		'Order by C.IdFarmacia, C.Año, C.Mes, C.ClaveSSA '  
	Exec(@sSql) 
	print @sSql 

	-- IdBeneficiario, NombreBeneficario, NumReceta, FolioReferencia 
	Set @sSql = 
		'Select ' + char(10) +  
		'	C.IdEstado, C.IdFarmacia, F.Farmacia, C.Año, C.Mes, ' + char(10) +  
		'	C.FolioVenta, C.FechaRegistro, C.FechaReceta,  ' + char(10) + 
		'	C.IdBeneficiario, C.NombreBeneficario, C.NumReceta, C.FolioReferencia, ' + char(10) + 
		'	C.IdProducto, C.CodigoEAN, C.ClaveLote, ' + char(10) + 
		'	' + char(10) + 
		'   M.TipoDeClave, M.TipoDeClaveDescripcion, ' + char(10) +  
		'	C.ClaveSSA, M.Mascara, M.DescripcionMascara, M.Presentacion, ' + char(10) + 
		'   C.CantidadTotal, C.PrecioLicitadoUnitario, C.TasaIva, C.SubTotal, C.Iva, C.Importe, ' + @sCampos + char(10)  + 
		'From #tmpDetalles__Cruze C (NoLock) ' + char(10) +  
		'Inner Join #tmpFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) ' + char(10) +  
		'Left Join vw_ClaveSSA_Mascara M (NoLock) ' + char(10) + 
		'	On ( C.IdEstado = M.IdEstado and C.ClaveSSA = M.ClaveSSA and M.IdCliente = ' + char(39) + @IdCliente + char(39) + ' and M.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) + ' ) ' + char(10) + 
		'Order by C.IdFarmacia, C.Año, C.Mes, C.ClaveSSA '  
	Exec(@sSql) 
	print @sSql 


	--Select * From #tmpConcentrado  
	-- Select * From #tmpDetalles 





End 
Go--#SQL 


	--Select * 
	--From FACT_Remisiones_Detalles D (NoLock) 
	--Where ClaveSSA like '%060.681.0067%' 
