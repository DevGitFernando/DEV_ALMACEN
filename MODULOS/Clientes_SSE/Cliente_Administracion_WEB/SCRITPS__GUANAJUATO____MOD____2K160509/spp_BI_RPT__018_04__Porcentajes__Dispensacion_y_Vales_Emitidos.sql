------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__018_04__Porcentajes__Dispensacion_y_Vales_Emitidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__018_04__Porcentajes__Dispensacion_y_Vales_Emitidos 
Go--#SQL 

Create Proc spp_BI_RPT__018_04__Porcentajes__Dispensacion_y_Vales_Emitidos 
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
		F.IdFarmacia, F.Farmacia, 
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		cast(0 as numeric(14,2)) as CantidadRecetada, 
		cast(cast(sum((D.CantidadEntregada)) as int) as numeric(14,4)) as CantidadDispensada, 
		cast(0 as numeric(14,2)) as CantidadVales, 
		cast(0 as numeric(14,2)) as Porcentaje_Dispensacion, 
		cast(0 as numeric(14,2)) as Porcentaje_Vales 				  
	Into #tmp_Dispensacion  
	From VentasEnc E (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by 
		F.IdFarmacia, F.Farmacia,  
		year(E.FechaRegistro), month(E.FechaRegistro) 
	Order by 1, 3, 4  		

	Select 
		F.IdFarmacia, F.Farmacia, 
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		cast(cast(sum(R.Cantidad) as int) as numeric(14,2)) as CantidadVales  
	Into #tmp_ValesEmitidos 
	From Vales_EmisionEnc E (NoLock) 
	Inner Join Vales_EmisionDet R (NoLock) 
		On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmacia and E.FolioVale = R.FolioVale ) 	
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		and E.IdEmpresa = @IdEmpresa 
	Group by 	
		F.IdFarmacia, F.Farmacia, 
		year(E.FechaRegistro), month(E.FechaRegistro)
	Order by 1, 3, 4  		
	

	Update D Set CantidadRecetada = ( D.CantidadDispensada + V.CantidadVales ), CantidadVales = V.CantidadVales 
	From #tmp_Dispensacion  D 
	Inner Join #tmp_ValesEmitidos V On ( D.IdFarmacia = V.IdFarmacia and D.Año = V.Año and D.Mes = V.Mes ) 


	Update P Set 
		Porcentaje_Dispensacion = round((CantidadDispensada / CantidadRecetada) * 100, 2) , 
		Porcentaje_Vales = round((CantidadVales / CantidadRecetada) * 100, 2)  
	From #tmp_Dispensacion P 
	Where CantidadRecetada > 0 
	

	------Select * From #tmp_Dispensacion 
	------Select * From #tmp_ValesEmitidos 
	


	
---------------------		spp_BI_RPT__018_04__Porcentajes__Dispensacion_y_Vales_Emitidos  


		
----------------------------------------------------------- SALIDA FINAL 
	Select 
		'Unidad' = Farmacia, 
		'Año' = Año,  
		'Mes' = Mes, 
		
		'Cantidad piezas recetadas' = cast(CantidadRecetada as int), 
		'Cantidad piezas dispensadas' = cast(CantidadDispensada as int), 
		'Cantidad piezas vales' = cast(CantidadVales as int), 
		'Porcentaje piezas dispensadas' = Porcentaje_Dispensacion, 
		'Porcenjate piezas vales' = Porcentaje_Vales 
	From #tmp_Dispensacion  

End 
Go--#SQL 


