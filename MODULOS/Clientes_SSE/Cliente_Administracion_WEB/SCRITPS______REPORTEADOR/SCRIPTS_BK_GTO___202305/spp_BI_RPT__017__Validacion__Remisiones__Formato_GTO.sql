------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__017__Validacion__Remisiones__Formato_GTO' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__017__Validacion__Remisiones__Formato_GTO 
Go--#SQL 

/*	
	Exec spp_BI_RPT__017__Validacion__Remisiones__Formato_GTO 
		@IdEmpresa = '001', @IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
		@IdFarmacia = '12', @Remision = '', 
		@FechaInicial = '2018-01-01', @FechaFinal = '2018-01-05', 
		@TipoDeDispensacion = 1, @NombreSolicitante = 'ana'   -- san za 
*/ 

Create Proc spp_BI_RPT__017__Validacion__Remisiones__Formato_GTO  
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '4011', 
	@Remision varchar(100) = '', 
	@FechaInicial varchar(10) = '2021-04-01', @FechaFinal varchar(10) = '2022-04-30', 
	@TipoDeDispensacion int = 2, 
	@NombreSolicitante varchar(500) = '' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@sEmpresa varchar(100) 

	Set @sSql = '' 
	Set @sEmpresa = 'PHARMAJAL'
	-- SII_REPORTEADOR

----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into SII_REPORTEADOR..#vw_Farmacias 
	From SII_REPORTEADOR..vw_Farmacias 
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
				'From SII_REPORTEADOR..vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Set @NombreSolicitante = replace(@NombreSolicitante, ' ', '%') 


	Select 
		E.IdFarmacia + '-' + replace(convert(varchar(10), FechaRegistro, 120), '-', '') + '-' + 
			right('00' + cast(E.TipoInsumo as varchar(2)), 2) as FolioRemision, 
		convert(varchar(10), FechaRegistro, 120) as FechaRemision, 
		E.*, 
		cast('' as varchar(200)) as Procedencia, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento  		 
	Into #tmp_Remisiones 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	Inner Join SII_REPORTEADOR..#vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 	
	Inner Join SII_REPORTEADOR..#CatTiposDispensacion D (Nolock) On ( E.IdTipoDeDispensacion = D.IdTipoDeDispensacion ) 	
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 	
		and E.NumReceta like '%' + replace(@Remision, ' ', '%') + '%' 	 
		-- and E.Medico like '%' + @NombreSolicitante + '%' 
		


	----------- Quitar los datos no requeridos 
	--If @Remision <> '' 
	--Begin 
	--	Delete From #tmp_Remisiones Where FolioRemision not like '%' + @Remision + '%' 
	--End 


---	Select * from #tmp_Dispensacion_x_Medico 

		

	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_RPT__017__Validacion__Remisiones__Formato_GTO 


---------------------    FUENTE DE FINANCIAMIENTO 
	If Exists ( Select * From Sysobjects (nolock) Where Name = 'BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento' and xType = 'U' ) 
	Begin 
		Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento, Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else @sEmpresa end) 
		From #tmp_Remisiones D 
		Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 
	End 


----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Remisiones 


	Select 	

		'Folio de remisión' = FolioRemision,  
		'Fecha de remisión' = FechaRemision, 

		'Fecha de solicitud' = FechaReceta, 
		'Número de colectivo / receta' = NumReceta, 

		'Unidad' = IdFarmacia,  
		'Nombre Unidad' = Farmacia, 
		'Nombre de quien solicita' = Medico, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionSal, 	
		'Cantidad dispensada' = cast(sum(Cantidad) as int), 
		'Procedencia' = Procedencia, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento, 
		'Precio ofertado' = max(PrecioLicitacionUnitario), 		
		'Costo de distribución' = cast(8.30 as numeric(14,4)), 		
		'Precio unitario' = max(PrecioLicitacionUnitario), 
		'Costo total' = sum(TotalLicitacion) 
		, IdTipoDeDispensacion  
	From #tmp_Remisiones 
	Where Cantidad > 0 
		-- and 1 = 0 
	Group by 
		IdFarmacia, Farmacia, 
		Medico, 
		ClaveSSA, DescripcionSal, FolioRemision, FechaRemision, FechaReceta, NumReceta, Procedencia, FuenteDeFinanciamiento 
		, IdTipoDeDispensacion
	Order by FolioRemision, ClaveSSA  


End 
Go--#SQL 


