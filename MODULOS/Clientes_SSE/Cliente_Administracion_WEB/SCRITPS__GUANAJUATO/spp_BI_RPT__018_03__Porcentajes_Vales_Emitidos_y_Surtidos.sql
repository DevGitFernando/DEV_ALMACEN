------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__018_03__Porcentajes_Vales_Emitidos_y_Surtidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__018_03__Porcentajes_Vales_Emitidos_y_Surtidos 
Go--#SQL 

Create Proc spp_BI_RPT__018_03__Porcentajes_Vales_Emitidos_y_Surtidos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', 
	@FechaInicial varchar(10) = '2016-12-13', @FechaFinal varchar(10) = '2017-01-13' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 




----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		F.Farmacia, 
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		count(Distinct E.FolioVale) as ValesEmitidos, count(Distinct R.FolioVale) as ValesCanjeados, 
		cast(0 as numeric(14,2)) as Porcentaje_Efectividad_Vale   
	Into #tmp_ValesEmitidos 
	From Vales_EmisionEnc E (NoLock) 
	Left Join ValesEnc R (NoLock) 
		On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmacia and E.FolioVale = R.FolioVale ) 	
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		and E.IdEmpresa = @IdEmpresa 
	Group by 	
		F.Farmacia, 
		year(E.FechaRegistro), month(E.FechaRegistro)


	Update P Set 
		Porcentaje_Efectividad_Vale = round((ValesCanjeados / ValesEmitidos) * 100, 2)  
	From #tmp_ValesEmitidos P 
	
		
--	Select * From #tmp_ValesEmitidos 
	
	
---------------------		spp_BI_RPT__018_03__Porcentajes_Vales_Emitidos_y_Surtidos  


		
----------------------------------------------------- SALIDA FINAL 
	Select 
		'Unidad' = Farmacia, 
		'Año' = Año,  
		'Mes' = Mes, 
		
		'Vales emitidos' = ValesEmitidos,
		'Vales surtidos' = ValesCanjeados, 
		'Porcentaje de efectividad' = Porcentaje_Efectividad_Vale 
	From #tmp_ValesEmitidos 



End 
Go--#SQL 


