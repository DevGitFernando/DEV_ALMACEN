If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__006__Consumos_Dispensacion' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__006__Consumos_Dispensacion 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__006__Consumos_Dispensacion 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', 
	@Remision varchar(40) = '', 
	@FechaInicial varchar(10) = '2016-01-01', @FechaFinal varchar(10) = '2017-12-31'	 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into SII_REPORTEADOR..#vw_Farmacias 
	From SII_REPORTEADOR..vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From SII_REPORTEADOR..vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 1 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdFarmacia + '-' + replace(convert(varchar(10), FechaRegistro, 120), '-', '') + '-' + 
			right('00' + cast(E.TipoInsumo as varchar(2)), 2) as FolioRemision, 
		E.* 
	Into #tmp_Remisiones 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	Inner Join BI_UNI_RPT__DTS__ClavesSSA	C ON ( E.ClaveSSA = C.ClaveSSA )  
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 	
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		

---	Select * from #tmp_Dispensacion_x_Medico 

		

	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_UNI_RPT__006__Consumos_Dispensacion 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Remisiones 


	Select 	
		'Unidad' = IdFarmacia,  
		'Nombre Unidad' = Farmacia,
		'Fecha dispensación' = convert(varchar(10), FechaRegistro, 120), 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionSal, 	
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 
		'Cantidad dispensada' = cast(sum(Cantidad) as int), 
		'Procedencia' = '', 
		'Fuente de financiamiento' = '', 
		'Precio ofertado' = max(PrecioLicitacionUnitario), 		
		'Costo de distribución' = cast(11 as numeric(14,4)), 		
		'Precio unitario' = max(PrecioLicitacionUnitario), 
		'Costo total' = sum(TotalLicitacion)  
	From #tmp_Remisiones 
	Where Cantidad > 0 
		-- and ClaveSSA = '010.000.0103.00'  	
	Group by 
		IdFarmacia, Farmacia, convert(varchar(10), FechaRegistro, 120), ClaveSSA, DescripcionSal, 
		-- FolioRemision 
		ClaveLote, FechaCaducidad 
	Order by ClaveSSA  


End 
Go--#SQL 


