--------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__020_06__Dispensacion_GrupoTerapeutico' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__020_06__Dispensacion_GrupoTerapeutico 
Go--#SQL 

Create Proc spp_BI_RPT__020_06__Dispensacion_GrupoTerapeutico 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2017-11-01', @FechaFinal varchar(10) = '2018-12-10'	
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
	Into SII_REPORTEADOR..#vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
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


	Select  
		E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.IdCliente, E.NombreCliente, E.IdSubCliente, E.NombreSubCliente, 
		E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, E.StatusAsignacion 
	Into #tmp__Programas 
	From vw_Clientes_Programas_Asignados_Unidad E 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  


---------------------		spp_BI_RPT__020_06__Dispensacion_GrupoTerapeutico 

----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEstado, F.Estado,
		IdJurisdiccion, Jurisdiccion,
		E.IdFarmacia, F.Farmacia, 
		E.IdCliente, E.IdSubCliente, 
		E.IdBeneficiario, cast('' as varchar(200)) as NombreBeneficiario, 
		E.IdGrupoTerapeutico, upper(E.GrupoTerapeutico) As GrupoTerapeutico, 
		E.IdClaveSSA_Sal as IdClaveSSA, E.ClaveSSA, 
		sum(E.Cantidad) as Piezas, 
		avg(E.Cantidad) as Promedio, 
		cast(max(PrecioLicitacion) as numeric(14,4)) as PrecioUnitario, 
		cast(sum(TotalLicitacion) as numeric(14,4)) as Importe 		    
	Into #tmp_PorcentajesDispensacion 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	----Inner Join VentasDet DV (NoLock) 
	----	On ( E.IdEmpresa = DV.IdEmpresa and E.IdEstado = DV.IdEstado and E.IdFarmacia = DV.IdFarmacia and E.FolioVenta = DV.FolioVenta ) 
	----Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( DV.IdProducto = P.IdProducto and DV.CodigoEAN = P.CodigoEAN ) 
	----Inner Join VentasInformacionAdicional D (NoLock) 
	----	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	----Inner Join #tmp__Programas P (NoLock) 
	----	On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdCliente = P.IdCliente and E.IdSubCliente = P.IdSubCliente 
	----		 and E.IdPrograma = P.IdPrograma and E.IdSubPrograma = P.IdSubPrograma )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by 
		E.IdEstado, F.Estado,
		IdJurisdiccion, Jurisdiccion, 
		E.IdFarmacia, F.Farmacia, 
		E.IdCliente, E.IdSubCliente, 
		E.IdGrupoTerapeutico, E.GrupoTerapeutico, 
		E.IdClaveSSA_Sal, E.ClaveSSA, 
		E.IdBeneficiario
		


	----Update P Set NombreBeneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) -- , NumeroReferencia = B.FolioReferencia  
	----From #tmp_PorcentajesDispensacion P (NoLock) 
	----Inner Join CatBeneficiarios B (NoLock) 
	----	On ( P.IdEstado = B.IdEstado and P.IdFarmacia = B.IdFarmacia and P.IdCliente = B.IdCliente and P.IdSubCliente = B.IdSubCliente and P.IdBeneficiario = B.IdBeneficiario ) 


	----Update D Set PrecioUnitario = P.PrecioUnitario, Importe = round((P.PrecioUnitario * Piezas), 2) 
	----From #tmp_PorcentajesDispensacion D (NoLock) 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA ) 

	Select 
		IdGrupoTerapeutico, upper(GrupoTerapeutico) as GrupoTerapeutico, 
		count(distinct IdBeneficiario) as Beneficiarios, 
		cast(sum(Piezas) as int) as Piezas, cast(round(avg(Piezas), 2) as numeric(14,2)) as PromedioPiezas, 
		cast(sum(Importe) as numeric(14,2)) as ImporteTotal,  
		cast(0 as numeric(14,2)) as Promedio   
	into #tmp_PorcentajesDispensacion__Resumen 
	From #tmp_PorcentajesDispensacion 
	Group by IdGrupoTerapeutico, GrupoTerapeutico 
	Order by GrupoTerapeutico 

	Update P Set Promedio = (Piezas / (Beneficiarios * 1.0))
	From #tmp_PorcentajesDispensacion__Resumen P  


--	Select * from #tmp_PorcentajesDispensacion 


---------------------		spp_BI_RPT__020_06__Dispensacion_GrupoTerapeutico 



----------------------------------------------------- SALIDA FINAL 
	Select 
		'Grupo terapeutico' = GrupoTerapeutico, 
		'Costo total' = ImporteTotal, 
		'Beneficiarios atendidos' = Beneficiarios, 
		'Cantidad dispensada' = Piezas, 
		'Promedio de piezas por beneficiario' = Promedio    
		-- 'Promedio por beneficiario x' = Promedio 
	From #tmp_PorcentajesDispensacion__Resumen 
	Order by Promedio desc, GrupoTerapeutico


	Select 
		IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		IdGrupoTerapeutico, upper(GrupoTerapeutico) as GrupoTerapeutico, 
		count(distinct IdBeneficiario) as Beneficiarios, 
		cast(sum(Piezas) as int) as Piezas, cast(round(avg(Piezas), 2) as numeric(14,2)) as PromedioPiezas, 
		cast(sum(Importe) as numeric(14,2)) as ImporteTotal,  
		cast(0 as numeric(14,2)) as Promedio
	Into #tmp_PorcentajesDispensacion_Temp
	From #tmp_PorcentajesDispensacion 
	Group by
		IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		IdGrupoTerapeutico, GrupoTerapeutico 
	Order by
		IdEstado, IdJurisdiccion, IdFarmacia, GrupoTerapeutico 

	Update P Set Promedio = (Piezas / (Beneficiarios * 1.0))
	From #tmp_PorcentajesDispensacion_Temp P   

	Select *
	From #tmp_PorcentajesDispensacion_Temp
	

End 
Go--#SQL 


