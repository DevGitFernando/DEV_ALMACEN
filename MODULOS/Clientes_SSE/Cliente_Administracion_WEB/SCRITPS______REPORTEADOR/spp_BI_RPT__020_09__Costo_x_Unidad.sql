--------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__020_09__Costo_x_Unidad' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__020_09__Costo_x_Unidad 
Go--#SQL 

Create Proc spp_BI_RPT__020_09__Costo_x_Unidad 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*',  
	@IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2016-12-01', @FechaFinal varchar(10) = '2018-12-31'	
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
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and EsAlmacen in ( 0, 1 )  and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
		Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----	select top 1 * from RptAdmonDispensacion_Detallado 


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes,
		IdJurisdiccion, Jurisdiccion, 
		E.IdFarmacia, E.Farmacia, 
		--cast(round(sum(((D.CantidadEntregada))), 0) as numeric(14,4)) as CantidadPiezas,  
		--cast(sum((round((D.CantidadEntregada), 0) * P.PrecioUnitario)) as numeric(14,4)) as Importe,  

		cast(round(sum(((E.Cantidad))), 0) as int) as CantidadPiezas,  
		cast(sum(E.TotalLicitacion) as numeric(14,4)) as Importe,  

		cast(0 as numeric(14,4)) as ImporteTotal,  
		cast(0 as numeric(14,4)) as Porcentaje_Participacion  
	Into #tmp_PorcentajesDispensacion 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado  E (NoLock) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by 
		year(E.FechaRegistro), month(E.FechaRegistro), IdJurisdiccion, Jurisdiccion, E.IdFarmacia, E.Farmacia 
		  -- , P.ClaveSSA, P.DescripcionClave   
	Order by 1, 2 


	Update P Set ImporteTotal = (select sum(Importe) From #tmp_PorcentajesDispensacion R Where R.Año = P.Año and R.Mes = P.Mes )  
	From #tmp_PorcentajesDispensacion P (NoLock) 

	Update P Set Porcentaje_Participacion = round((Importe / ImporteTotal) * 100, 4) 
	From #tmp_PorcentajesDispensacion P (NoLock) 	


---------------------		spp_BI_RPT__020_09__Costo_x_Unidad 

----------------------------------------------------------------------------------------   

	Select
		IdJurisdiccion, Jurisdiccion,
		D.IdFarmacia, D.Farmacia, 
		cast(sum(((CantidadPiezas))) as int) as CantidadPiezas, 
		cast(sum(Importe) as numeric(14,4)) as Importe,  
		cast(0 as numeric(14,4)) as ImporteTotal,  
		cast(0 as numeric(14,4)) as Porcentaje_Participacion  			   
	Into #tmp_PorcentajesDispensacion__Resumen
	From #tmp_PorcentajesDispensacion D (NoLock)  
	Group by
		IdJurisdiccion, Jurisdiccion, 
		D.IdFarmacia, D.Farmacia 


	Update P Set ImporteTotal = (select sum(Importe) From #tmp_PorcentajesDispensacion__Resumen R )  
	From #tmp_PorcentajesDispensacion__Resumen P (NoLock) 

	Update P Set Porcentaje_Participacion = round((Importe / ImporteTotal) * 100, 4) 
	From #tmp_PorcentajesDispensacion__Resumen P (NoLock) 	


---------------------		spp_BI_RPT__020_09__Costo_x_Unidad 



----------------------------------------------------- SALIDA FINAL 
	Select
		'Id Jurisdiccion' = IdJurisdiccion, Jurisdiccion,
		'Id Farmacia' = IdFarmacia, 
		'Farmacia' = Farmacia, 
		'Cantidad' = CantidadPiezas, 
		'Importe' = Importe, 
		'Importe General' = ImporteTotal, 
		'Porcentaje de participación' = Porcentaje_Participacion   
	From #tmp_PorcentajesDispensacion__Resumen 
	Order by 	
		Porcentaje_Participacion desc 

	Select 
		'Año' = Año, 
		'Mes' = Mes,
		'Id Jurisdiccion' = IdJurisdiccion, Jurisdiccion,
		'Id Farmacia' = IdFarmacia, 
		'Farmacia' = Farmacia, 
		'Cantidad' = CantidadPiezas, 
		'Importe' = Importe, 
		'Importe General' = ImporteTotal, 
		'Porcentaje de participación' = Porcentaje_Participacion   	
	From #tmp_PorcentajesDispensacion  
	Order by 
		Año, Mes, -- IdFarmacia 
		-- PrecioUnitario desc 
		-- Importe desc 
		Porcentaje_Participacion desc 


End 
Go--#SQL 


