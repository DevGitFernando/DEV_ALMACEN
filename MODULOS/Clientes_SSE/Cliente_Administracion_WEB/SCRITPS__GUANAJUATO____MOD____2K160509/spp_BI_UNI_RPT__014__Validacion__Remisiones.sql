If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__014__Validacion__Remisiones' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__014__Validacion__Remisiones 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__014__Validacion__Remisiones 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', 
	@Remision varchar(40) = '', 
	@FechaInicial varchar(10) = '2015-10-06', @FechaFinal varchar(10) = '2015-10-10'	
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
		E.IdFarmacia + '-' + replace(convert(varchar(10), FechaRegistro, 120), '-', '') + '-' + 
			right('00' + cast(E.TipoInsumo as varchar(2)), 2) as FolioRemision, 
		E.* 
	Into #tmp_Remisiones 
	From RptAdmonDispensacion_Detallado__General E (NoLock) 
	Inner Join BI_UNI_RPT__DTS__ClavesSSA	C ON ( E.ClaveSSA = C.ClaveSSA )  
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 	
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		

---	Select * from #tmp_Dispensacion_x_Medico 

		

	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_UNI_RPT__014__Validacion__Remisiones 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Remisiones 


	Select 	
		'Folio de remisión' = FolioRemision,  
		'Unidad' = IdFarmacia,  
		'Nombre Unidad' = Farmacia,
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionSal, 	
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
	Group by IdFarmacia, Farmacia, ClaveSSA, DescripcionSal, FolioRemision 
	Order by FolioRemision, ClaveSSA  


End 
Go--#SQL 


