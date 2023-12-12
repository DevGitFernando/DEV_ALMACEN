--------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__020_03__Claves__Costos_y_Consumos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__020_03__Claves__Costos_y_Consumos 
Go--#SQL 

Create Proc spp_BI_RPT__020_03__Claves__Costos_y_Consumos  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2018-01-01', @FechaFinal varchar(10) = '2018-01-10'	
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@dImporteTotal numeric(14,4)  

	Set @sSql = '' 
	Set @dImporteTotal = 0 


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia 
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


---		select top 1 * from RptAdmonDispensacion_Detallado 

----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		E.ClaveSSA, E.DescripcionSal as DescripcionClave, 
		cast(sum(((E.Cantidad))) as numeric(14,4)) as CantidadPiezas,  
		(P.PrecioUnitario) as PrecioUnitario,  
		cast(sum(((E.Cantidad) * P.PrecioUnitario)) as numeric(14,4)) as Importe,  
		cast(sum(0) as numeric(14,4)) as ImporteTotal,  
		cast(0 as numeric(14,4)) as Porcentaje_Participacion  
	Into #tmp_PorcentajesDispensacion 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 		
	----From VentasEnc E (NoLock) 
	----Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
	----	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.IdClaveSSA_Sal = P.IdClaveSSA ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by year(E.FechaRegistro), month(E.FechaRegistro), E.ClaveSSA, E.DescripcionSal, P.PrecioUnitario   
	Order by 1, 2 




	----Select @dImporteTotal = ( select sum(Importe) From #tmp_PorcentajesDispensacion )
	----From #tmp_PorcentajesDispensacion 

	----Update P Set ImporteTotal = @dImporteTotal, Porcentaje_Participacion = round((Importe / @dImporteTotal) * 100, 4) 
	----From #tmp_PorcentajesDispensacion P (NoLock) 


	Update P Set ImporteTotal = (select sum(Importe) From #tmp_PorcentajesDispensacion R Where R.Año = P.Año and R.Mes = P.Mes )  
	From #tmp_PorcentajesDispensacion P (NoLock) 

	Update P Set Porcentaje_Participacion = round((Importe / ImporteTotal) * 100, 4) 
	From #tmp_PorcentajesDispensacion P (NoLock) 	

---------------------		spp_BI_RPT__020_03__Claves__Costos_y_Consumos 

--	Select * from #tmp_PorcentajesDispensacion 


	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 


---------------------		spp_BI_RPT__020_03__Claves__Costos_y_Consumos 

	Select IdJurisdiccion, Jurisdiccion, E.Idestado, F.Estado, F.IdFarmacia, F.Farmacia,
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		E.ClaveSSA, E.DescripcionSal as DescripcionClave, 
		cast(sum(((E.Cantidad))) as numeric(14,4)) as CantidadPiezas,  
		(P.PrecioUnitario) as PrecioUnitario,  
		cast(sum(((E.Cantidad) * P.PrecioUnitario)) as numeric(14,4)) as Importe,  
		cast(sum(0) as numeric(14,4)) as ImporteTotal,  
		cast(0 as numeric(14,4)) as Porcentaje_Participacion  
	Into #tmp_PorcentajesDispensacion_Det 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 		
	----From VentasEnc E (NoLock) 
	----Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
	----	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.IdClaveSSA_Sal = P.IdClaveSSA ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by 
		IdJurisdiccion, Jurisdiccion, E.Idestado, F.Estado, F.IdFarmacia, F.Farmacia,
		year(E.FechaRegistro), month(E.FechaRegistro), E.ClaveSSA, E.DescripcionSal, P.PrecioUnitario   
	Order by 1, 2


		Update P Set ImporteTotal = (select sum(Importe) From #tmp_PorcentajesDispensacion_Det R Where R.Año = P.Año and R.Mes = P.Mes And R.IdEstado = P.IdEstado And R.IdFarmacia = P.IdFarmacia)  
	From #tmp_PorcentajesDispensacion_Det P (NoLock) 

	Update P Set Porcentaje_Participacion = round((Importe / ImporteTotal) * 100, 4) 
	From #tmp_PorcentajesDispensacion_Det P (NoLock) 


----------------------------------------------------- SALIDA FINAL 
	Select 
		'Año' = Año, 
		'Mes' = Mes, 
		'Clave SSA' = ClaveSSA, 
		'Descripción clave' = DescripcionClave, 
		'Cantidad dispensada' = CantidadPiezas, 
		'Precio licitado' = PrecioUnitario, 
		'Importe' = Importe, 
		'Importe total' = ImporteTotal, 
		'Porcentaje de participacion' = Porcentaje_Participacion 
	From #tmp_PorcentajesDispensacion 
	Order by 
		Año, Mes, 
		-- PrecioUnitario desc 
		-- Importe desc 
		Porcentaje_Participacion desc
	
	
	
	Select IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		'Año' = Año, 
		'Mes' = Mes, 
		'Clave SSA' = ClaveSSA, 
		'Descripción clave' = DescripcionClave, 
		'Cantidad dispensada' = CantidadPiezas, 
		'Precio licitado' = PrecioUnitario, 
		'Importe' = Importe, 
		'Importe total' = ImporteTotal, 
		'Porcentaje de participacion' = Porcentaje_Participacion 
	From #tmp_PorcentajesDispensacion_Det 
	Order by
		IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		Año, Mes, 
		-- PrecioUnitario desc 
		-- Importe desc 
		Porcentaje_Participacion desc	


	----Select 	
	----	'Año' = Año,  
	----	'Mes' = Mes, 
	----	'Cantidad receta' = CantidadRecetada, 	
	----	'Cantidad dispensada' = CantidadEntregada, 
	----	'Cantidad no dispensada' = CantidadNoEntregada, 
	----	'Porcentaje piezas dispensadas' = Porcentaje_Dispensado,  
	----	'Porcentaje piezas no dispensadas' =  Porcentaje_NoDispensado, 
		
	----	'Importe total recetado' = Costo_Recetado, 
	----	'Costo dispensado' = Costo_Dispensado,  
	----	'Costo no dispensado' =  Costo_NoDispensado, 
		
	----	'Porcejate costo dispensado' =  Porcentaje_CostoDispensado, 
	----	'Porcejate costo no dispensado' =  Porcentaje_CostoNoDispensado  

	----From #tmp_PorcentajesDispensacion  

	

End 
Go--#SQL 


