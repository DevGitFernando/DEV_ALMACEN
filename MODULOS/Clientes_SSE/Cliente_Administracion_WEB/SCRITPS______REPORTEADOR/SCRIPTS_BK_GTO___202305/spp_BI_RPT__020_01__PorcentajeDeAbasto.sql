--------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__020_01__PorcentajeDeAbasto' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__020_01__PorcentajeDeAbasto 
Go--#SQL 

Create Proc spp_BI_RPT__020_01__PorcentajeDeAbasto  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  

---		select top 10 * from vw_CB_CuadroBasico_Farmacias 

----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEstado, E.Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, E.Farmacia, 
		ClaveSSA, DescripcionClave, IdTipoDeClave, TipoDeClaveDescripcion, 
		0 as Abasto,  
		0 as Total_Claves, 
		0 as Total_Medicamento, 
		0 as Total_Claves_MaterialDeCuracion, 

		--count(*) as Total_Claves, 
		--(case when IdTipoDeClave = '02' then 1 else 0 end)  as Total_Medicamento, 
		--(case when IdTipoDeClave = '01' then 1 else 0 end) as Total_Claves_MaterialDeCuracion, 

		cast(0 as numeric(14,2)) as Porcentaje_Abasto, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Medicamento, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Claves_MaterialDeCuracion  
	Into #tmp_PorcentajesAbasto 
	From vw_CB_CuadroBasico_Farmacias E (NoLock) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where StatusMiembro = 'A' and StatusClave = 'A' 
	Group by 
		E.IdEstado, E.Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, E.Farmacia, 
		E.ClaveSSA, E.DescripcionClave, E.IdTipoDeClave, E.TipoDeClaveDescripcion  


	Select P.ClaveSSA, (case when sum(E.Existencia) > 0 then 1 else 0  end) as Abasto 
	Into #tmp_Existencia 
	From FarmaciaProductos_CodigoEAN E 
	Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Group by P.ClaveSSA 


	Update P Set Abasto = E.Abasto 
	From #tmp_PorcentajesAbasto P (NoLock) 
	Inner Join #tmp_Existencia E (NoLock) On ( P.ClaveSSA = E.ClaveSSA )

	Select
		IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		cast(count(*) as numeric(14,2)) as Claves_Totales, 
		cast(0 as numeric(14,2)) as Abasto, 
		cast(0 as numeric(14,2)) as Claves_Medicamentos, 
		cast(0 as numeric(14,2)) as Claves_MaterialDeCuracion, 
		cast(0 as numeric(14,2)) as Abasto_Medicamento, 
		cast(0 as numeric(14,2)) as Abasto_Claves_MaterialDeCuracion, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Medicamento, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Claves_MaterialDeCuracion  		  
	Into #tmp_PorcentajesAbasto_Resumen_Farmacia
	From #tmp_PorcentajesAbasto P (NoLock)
	Group By IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia

	Update R Set 
		Claves_Medicamentos = ( select count(*) From #tmp_PorcentajesAbasto T Where IdTipoDeClave = '02' And T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia), 
		Claves_MaterialDeCuracion = ( select count(*) From #tmp_PorcentajesAbasto T Where IdTipoDeClave = '01' And T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia), 
		Abasto = ( select count(*) From #tmp_PorcentajesAbasto T Where Abasto = 1 And T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia), 
		Abasto_Medicamento = ( select count(*) From #tmp_PorcentajesAbasto T Where Abasto = 1 and IdTipoDeClave = '02' And T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia), 
		Abasto_Claves_MaterialDeCuracion = ( select count(*) From #tmp_PorcentajesAbasto T Where Abasto = 1 and IdTipoDeClave = '01' And T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia) 
	From #tmp_PorcentajesAbasto_Resumen_Farmacia R (NoLock) 

		Update R Set 
		Porcentaje_Abasto = (case when Claves_Totales > 0 Then round(round(Abasto / Claves_Totales, 2) * 100.0, 2) else 0 end), 
		Porcentaje_Abasto_Medicamento = (case when Claves_Medicamentos > 0 then round(round(Abasto_Medicamento / Claves_Medicamentos, 2) * 100.0, 2) else 0 end), 
		Porcentaje_Abasto_Claves_MaterialDeCuracion = (case when Claves_MaterialDeCuracion > 0 then round(round(Abasto_Claves_MaterialDeCuracion / Claves_MaterialDeCuracion, 2) * 100.0, 2) else 0 end)  
	From #tmp_PorcentajesAbasto_Resumen_Farmacia R (NoLock)
	
	Select 
	 	ClaveSSA, DescripcionClave, IdTipoDeClave, TipoDeClaveDescripcion, 
		(Case When Max(Abasto) > 0 then 1 Else 0 End) As Abasto,  
		0 as Total_Claves, 
		0 as Total_Medicamento, 
		0 as Total_Claves_MaterialDeCuracion, 

		--count(*) as Total_Claves, 
		--(case when IdTipoDeClave = '02' then 1 else 0 end)  as Total_Medicamento, 
		--(case when IdTipoDeClave = '01' then 1 else 0 end) as Total_Claves_MaterialDeCuracion, 

		cast(0 as numeric(14,2)) as Porcentaje_Abasto, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Medicamento, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Claves_MaterialDeCuracion
	Into #tmp_PorcentajesAbasto_General
	 From #tmp_PorcentajesAbasto P (NoLock)
	 Group by ClaveSSA, DescripcionClave, IdTipoDeClave, TipoDeClaveDescripcion


	 --Select * From #tmp_PorcentajesAbasto
	 --Select * From #tmp_PorcentajesAbasto_General

	Select cast(count(*) as numeric(14,2)) as Claves_Totales, 
		cast(0 as numeric(14,2)) as Abasto, 
		cast(0 as numeric(14,2)) as Claves_Medicamentos, 
		cast(0 as numeric(14,2)) as Claves_MaterialDeCuracion, 
		cast(0 as numeric(14,2)) as Abasto_Medicamento, 
		cast(0 as numeric(14,2)) as Abasto_Claves_MaterialDeCuracion, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Medicamento, 
		cast(0 as numeric(14,2)) as Porcentaje_Abasto_Claves_MaterialDeCuracion  		  
	Into #tmp_PorcentajesAbasto_Resumen 
	From #tmp_PorcentajesAbasto_General P (NoLock) 

	
	Update R Set 
		Claves_Medicamentos = ( select count(*) From #tmp_PorcentajesAbasto_General Where IdTipoDeClave = '02'), 
		Claves_MaterialDeCuracion = ( select count(*) From #tmp_PorcentajesAbasto_General Where IdTipoDeClave = '01'), 
		Abasto = ( select count(*) From #tmp_PorcentajesAbasto_General Where Abasto = 1), 
		Abasto_Medicamento = ( select count(*) From #tmp_PorcentajesAbasto_General Where Abasto = 1 and IdTipoDeClave = '02'), 
		Abasto_Claves_MaterialDeCuracion = ( select count(*) From #tmp_PorcentajesAbasto_General Where Abasto = 1 and IdTipoDeClave = '01') 
	From #tmp_PorcentajesAbasto_Resumen R (NoLock) 


	Update R Set 
		Porcentaje_Abasto = (case when Claves_Totales > 0 Then round(round(Abasto / Claves_Totales, 2) * 100.0, 2) else 0 end), 
		Porcentaje_Abasto_Medicamento = (case when Claves_Medicamentos > 0 then round(round(Abasto_Medicamento / Claves_Medicamentos, 2) * 100.0, 2) else 0 end), 
		Porcentaje_Abasto_Claves_MaterialDeCuracion = (case when Claves_MaterialDeCuracion > 0 then round(round(Abasto_Claves_MaterialDeCuracion / Claves_MaterialDeCuracion, 2) * 100.0, 2) else 0 end)  
	From #tmp_PorcentajesAbasto_Resumen R (NoLock) 

---------------------		spp_BI_RPT__020_01__PorcentajeDeAbasto 


	--Update P Set Total_Claves  
	--From #tmp_PorcentajesAbasto P (NoLock) 

----------------------------------------------------- OBTENCION DE DATOS  

--	Select top 10 * from vw_CB_CuadroBasico_Farmacias 



	----Update P Set 
	----	Porcentaje_Dispensado = round((CantidadEntregada / CantidadRecetada) * 100, 2) , 
	----	Porcentaje_NoDispensado = round((CantidadNoEntregada / CantidadRecetada) * 100, 2),   
	----	Porcentaje_CostoDispensado = round((Costo_Dispensado / Costo_Recetado) * 100, 2) , 
	----	Porcentaje_CostoNoDispensado = round((Costo_NoDispensado / Costo_Recetado) * 100, 2)   
	----From #tmp_PorcentajesAbasto P 
	



---------------------		spp_BI_RPT__020_01__PorcentajeDeAbasto 



----------------------------------------------------- SALIDA FINAL 

	Select 
		'Número total de claves' = Claves_Totales, 
		'Número total de claves con abasto' = Abasto, 
		'Número de claves de medicamentos' = Claves_Medicamentos, 
		'Número de claves de material de curación' = Claves_MaterialDeCuracion, 

		'Porcentaje de abasto general' = Porcentaje_Abasto, 
		'Porcentaje de abasto medicamentos' = Porcentaje_Abasto_Medicamento, 
		'Porcentaje de abasto material de curación' = Porcentaje_Abasto_Claves_MaterialDeCuracion 
	From #tmp_PorcentajesAbasto_Resumen 


	Select
		IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		'Número total de claves' = Claves_Totales, 
		'Número total de claves con abasto' = Abasto, 
		'Número de claves de medicamentos' = Claves_Medicamentos, 
		'Número de claves de material de curación' = Claves_MaterialDeCuracion, 

		--count(*) as Total_Claves, 
		--(case when IdTipoDeClave = '02' then 1 else 0 end)  as Total_Medicamento, 
		--(case when IdTipoDeClave = '01' then 1 else 0 end) as Total_Claves_MaterialDeCuracion, 

		'Porcentaje de abasto general' = Porcentaje_Abasto, 
		'Porcentaje de abasto medicamentos' = Porcentaje_Abasto_Medicamento, 
		'Porcentaje de abasto material de curación' = Porcentaje_Abasto_Claves_MaterialDeCuracion  
	From #tmp_PorcentajesAbasto_Resumen_Farmacia P (NoLock)  


	----Select 	* 
	----	----'Año' = Año,  
	----	----'Mes' = Mes, 
	----	----'Cantidad receta' = CantidadRecetada, 	
	----	----'Cantidad dispensada' = CantidadEntregada, 
	----	----'Cantidad no dispensada' = CantidadNoEntregada, 
	----	----'Porcentaje piezas dispensadas' = Porcentaje_Dispensado,  
	----	----'Porcentaje piezas no dispensadas' =  Porcentaje_NoDispensado, 
		
	----	----'Importe total recetado' = Costo_Recetado, 
	----	----'Costo dispensado' = Costo_Dispensado,  
	----	----'Costo no dispensado' =  Costo_NoDispensado, 
		
	----	----'Porcejate costo dispensado' =  Porcentaje_CostoDispensado, 
	----	----'Porcejate costo no dispensado' =  Porcentaje_CostoNoDispensado  

	----From #tmp_PorcentajesAbasto  
	----Order by DescripcionClave  
	

End 
Go--#SQL 


