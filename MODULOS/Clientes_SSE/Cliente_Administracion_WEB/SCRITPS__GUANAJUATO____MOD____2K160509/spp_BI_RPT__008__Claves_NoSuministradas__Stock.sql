------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__008__Claves_NoSuministradas__Stock' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__008__Claves_NoSuministradas__Stock 
Go--#SQL 

Create Proc spp_BI_RPT__008__Claves_NoSuministradas__Stock 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', @ClaveSSA varchar(100) = '', 
	@Unidad_Beneficiario varchar(1000) = '', 
	@FechaInicial varchar(10) = '2015-11-01', @FechaFinal varchar(10) = '2015-11-30' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  



----------------------------------------------------- DATOS FILTRO 
	Set @IdFarmacia = '5' 
	
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
		D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion, cast(sum(D.CantidadRequerida) as int) as Cantidad 
	Into #tmp_Claves_NoSurtidas 
	From VentasEnc E (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Left Join vw_ClavesSSA___PRCS P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		--and P.ClaveSSA like '%' + @ClaveSSA + '%' 
		and D.EsCapturada = 1 	
	Group by D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion 


---	Select * From #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	
	
---------------------		spp_BI_RPT__008__Claves_NoSuministradas__Stock  

	

----------------------------------------------------- SALIDA FINAL 
	Select 
		-- IdClaveSSA, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Cantidad' = (Cantidad) 
	From #tmp_Claves_NoSurtidas 
	Order By   
		ClaveSSA   



End 
Go--#SQL 


