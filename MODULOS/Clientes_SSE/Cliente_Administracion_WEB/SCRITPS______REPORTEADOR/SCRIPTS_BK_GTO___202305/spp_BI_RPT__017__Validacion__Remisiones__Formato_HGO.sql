------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__017__Validacion__Remisiones__Formato_HGO' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__017__Validacion__Remisiones__Formato_HGO 
Go--#SQL 

Create Proc spp_BI_RPT__017__Validacion__Remisiones__Formato_HGO 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '13', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '11', 
	@Remision varchar(40) = '', 
	@FechaInicial varchar(10) = '2018-04-01', @FechaFinal varchar(10) = '2018-04-10', 
	@TipoDeDispensacion int = 0, 
	@NombreSolicitante varchar(500) = '' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare  
	@sSql varchar(max) 

	Set @sSql = '' 
	-- SII_REPORTEADOR

----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into SII_REPORTEADOR..#vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 


	Select  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia 
	Into SII_REPORTEADOR..#CatTiposDispensacion 
	From CatTiposDispensacion 
	Where 1 = 0 


	if ( @TipoDeDispensacion = 0 )   -- Todo 
	Begin 
		Insert Into #CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia ) 
		select  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia 
		from CatTiposDispensacion 
	End 


	if ( @TipoDeDispensacion = 1 )   -- Recetas 
	Begin 
		Insert Into #CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia ) 
		select  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia 
		from CatTiposDispensacion 
		Where IdTipoDeDispensacion not in ( '02', '03', '04', '23' ) 
	End 

	if ( @TipoDeDispensacion = 2 )   -- Colectivos 
	Begin 
		Insert Into #CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia ) 
		select  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia 
		from CatTiposDispensacion 
		Where IdTipoDeDispensacion in ( '02', '03', '04', '23' ) 
	End 
----------------------------------------------------- DATOS FILTRO 



	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	--Select 
	--	E.IdFarmacia + '-' + replace(convert(varchar(10), FechaRegistro, 120), '-', '') + '-' + 
	--		right('00' + cast(E.TipoInsumo as varchar(2)), 2) as FolioRemision, 
	--	convert(varchar(10), FechaRegistro, 120) as FechaRemision, 
	--	E.*, 
	--	cast('' as varchar(200)) as Procedencia, 
	--	cast('' as varchar(200)) as FuenteDeFinanciamiento  		 
	--Into #tmp_Remisiones 
	--From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	--Inner Join SII_REPORTEADOR..#vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 	
	--Inner Join SII_REPORTEADOR..#CatTiposDispensacion D (Nolock) On ( E.IdTipoDeDispensacion = D.IdTipoDeDispensacion ) 	
	--Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
	--	E.IdEmpresa = @IdEmpresa 
		





---	Select * from #tmp_Dispensacion_x_Medico 


---------------------		spp_BI_RPT__017__Validacion__Remisiones__Formato_HGO 


---------------------    FUENTE DE FINANCIAMIENTO 
	--If Exists ( Select * From Sysobjects (nolock) Where Name = 'BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento' and xType = 'U' ) 
	--Begin 
	--	Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento, Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) 
	--	From #tmp_Remisiones D 
	--	Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 
	--End 


----------------------------------------------------- SALIDA FINAL 
Set NoCount Off  
	Set @NombreSolicitante = replace(@NombreSolicitante, ' ', '%') 

	Select -- E.* 
			FolioRemision, FechaRemision, 
			-- FolioFacturaElectronica, 
			IdFarmaciaDispensacion as IdFarmacia, FarmaciaDispensacion as Farmacia, 
			-- Referencia_Beneficiario, Referencia_NombreBeneficiario, FolioVenta, FechaInicial, FechaFinal, FechaReceta, 
			
			--- IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, 
			
			IdFinanciamiento, Financiamiento, 
			
			-- IdDocumento, NombreDocumento, IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturable, 
			
			TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
			
			-- IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
			
			--IdProducto, CodigoEAN, Descripcion, ClaveLote, IdClaveSSA, 
			ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
			PrecioLicitado, PrecioLicitadoUnitario, sum(Cantidad) as Cantidad, ceiling(sum(Cantidad_Agrupada)) as Cantidad_Agrupada, 
			TasaIva, 
			sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
			sum(Iva_Clave) as Iva_Clave, sum(Importe_Clave) as Importe_Clave 

	Into #tmp_Remisiones__Facturacion  
	From SII_Facturacion_Hidalgo..vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join SII_REPORTEADOR..#vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia ) 	
	Where convert(varchar(10), E.FechaReceta, 120) between @FechaInicial and @FechaFinal  
		and E.EsFacturada = 0 
		--and E.Medico like '%' + @NombreSolicitante + '%' 
	Group by 
		FolioRemision, FechaRemision, IdFarmaciaDispensacion, FarmaciaDispensacion, 
		IdFinanciamiento, Financiamiento, 
		TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc,  
		ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
		PrecioLicitado, PrecioLicitadoUnitario, TasaIva


	Select 	
		'Folio de remisión' = FolioRemision,  
		'Fecha de remisión' = FechaRemision, 
		'Unidad' = IdFarmacia,  
		'Nombre Unidad' = Farmacia,
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = '', 
		'Descripción Clave SSA' = DescripcionClave, 	
		'Presentación' = '', 
		'Cantidad dispensada' = cast(sum(Cantidad) as int), 
		'Procedencia' = OrigenInsumoDesc, 
		'Fuente de financiamiento' = Financiamiento, 
		'Precio ofertado' = max(PrecioLicitado), 		
		'Costo de distribución' = cast(8.89 as numeric(14,4)), 		
		'Precio unitario' = max(PrecioLicitado), 
		'Costo total' = sum(Importe_Clave)  
	From #tmp_Remisiones__Facturacion 
	Where Cantidad > 0 
	Group by IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, FolioRemision, FechaRemision, OrigenInsumoDesc, Financiamiento 
	Order by FolioRemision, ClaveSSA  


	----select * 
	----from #tmp_Remisiones__Facturacion  


End 
Go--#SQL 


