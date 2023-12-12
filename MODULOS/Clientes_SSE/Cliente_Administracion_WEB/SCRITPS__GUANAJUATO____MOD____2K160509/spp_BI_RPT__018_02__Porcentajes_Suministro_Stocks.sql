If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__018_02__Porcentajes_Suministro_Stocks' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__018_02__Porcentajes_Suministro_Stocks 
Go--#SQL 

Create Proc spp_BI_RPT__018_02__Porcentajes_Suministro_Stocks 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', 
	@FechaInicial varchar(10) = '2015-01-01', @FechaFinal varchar(10) = '2015-11-30'	
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
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	
	Set @IdFarmacia = '5'  
	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  



----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		cast(sum((D.CantidadRequerida + D.CantidadEntregada)) as numeric(14,4)) as CantidadRecetada, 
		-- D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion, cast(sum(D.CantidadRequerida) as int) as Cantidad 
		cast(sum(D.CantidadEntregada) as numeric(14,4)) as CantidadEntregada, 
		cast(sum(D.CantidadRequerida) as numeric(14,4)) as CantidadNoEntregada,
		cast(0 as numeric(14,2)) as Porcentaje_Dispensado, 
		cast(0 as numeric(14,2)) as Porcentaje_NoDispensado 		 
	Into #tmp_PorcentajesDispensacion 
	From VentasEnc E (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join vw_ClavesSSA___PRCS P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by year(E.FechaRegistro), month(E.FechaRegistro) 
	Order by 1, 2 


	Update P Set 
		Porcentaje_Dispensado = round((CantidadEntregada / CantidadRecetada) * 100, 2) , 
		Porcentaje_NoDispensado = round((CantidadNoEntregada / CantidadRecetada) * 100, 2)  
	From #tmp_PorcentajesDispensacion P 
	

--	Select * from #tmp_PorcentajesDispensacion 


	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 


---------------------		spp_BI_RPT__018_02__Porcentajes_Suministro_Stocks 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Remisiones 


	Select 	
		'Año' = Año,  
		'Mes' = Mes,
		'Cantidad total recetada' = cast(CantidadRecetada as int), 
		'Cantidad dispensada' = cast(CantidadEntregada as int), 
		'Cantidad no dispensada' = cast(CantidadNoEntregada as int), 
		'Porcentaje total' = Porcentaje_Dispensado + Porcentaje_NoDispensado,   
		'Porcentaje dispensado' = Porcentaje_Dispensado,  
		'Porcentaje no dispensado' =  Porcentaje_NoDispensado 
	From #tmp_PorcentajesDispensacion  



End 
Go--#SQL 


