---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__016__Graficas' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__016__Graficas 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__016__Graficas 
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

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
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
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		S.IdServicio, cast('' as varchar(200)) as Servicio, 
		cast(sum((D.CantidadRequerida + D.CantidadEntregada)) as numeric(14,4)) as CantidadRecetada, 
		-- D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion, cast(sum(D.CantidadRequerida) as int) as Cantidad 
		cast(sum(D.CantidadEntregada) as numeric(14,4)) as CantidadEntregada, 
		cast(sum(D.CantidadRequerida) as numeric(14,4)) as CantidadNoEntregada,
		cast(0 as numeric(14,2)) as Porcentaje_Dispensado, 
		cast(0 as numeric(14,2)) as Porcentaje_NoDispensado, 
		
		cast(sum((D.CantidadRequerida + D.CantidadEntregada) * P.PrecioUnitario) as numeric(14,4)) as Costo_Recetado, 		
		cast(sum(D.CantidadEntregada * P.PrecioUnitario) as numeric(14,4)) as Costo_Dispensado, 
		cast(sum(D.CantidadRequerida * P.PrecioUnitario) as numeric(14,4)) as Costo_NoDispensado 		
	Into #tmp_Reporte 
	From VentasEnc E (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join VentasInformacionAdicional S (NoLock) 
		On ( E.IdEmpresa = S.IdEmpresa and E.IdEstado = S.IdEstado and E.IdFarmacia = S.IdFarmacia and E.FolioVenta = S.FolioVenta ) 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA ) 
	Inner Join BI_UNI_RPT__DTS__ClavesSSA CB (NoLock) On ( P.ClaveSSA = CB.ClaveSSA )  
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by year(E.FechaRegistro), month(E.FechaRegistro), S.IdServicio  
	Order by 1, 2 


	Update P set Servicio = S.Descripcion 
	From #tmp_Reporte P 
	Inner Join CatServicios S (NoLock) On ( P.IdServicio = S.IdServicio ) 

	Update P Set 
		Porcentaje_Dispensado = round((CantidadEntregada / CantidadRecetada) * 100, 2) , 
		Porcentaje_NoDispensado = round((CantidadNoEntregada / CantidadRecetada) * 100, 2)  
	From #tmp_Reporte P 
	

--	select * from #tmp_Reporte 


---------------------		spp_BI_UNI_RPT__016__Graficas 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Remisiones 


	Select 	
		'Año' = Año,  
		'Mes' = Mes, 
		'Servicio' = Servicio, 
		
		'Cantidad piezas recetadas' = CantidadRecetada, 	
		'Cantidad piezas dispensadas' = CantidadEntregada, 
		'Cantidad piezas no dispensadas' = CantidadNoEntregada, 
		'Porcentaje piezas dispensadas' = Porcentaje_Dispensado,  
		'Porcentaje piezas no dispensadas' =  Porcentaje_NoDispensado, 
		
		'Importe total recetado' = Costo_Recetado, 
		'Costo dispensado' = Costo_Dispensado,  
		'Costo no dispensado' =  Costo_NoDispensado 
		
	From #tmp_Reporte  


End 
Go--#SQL 


