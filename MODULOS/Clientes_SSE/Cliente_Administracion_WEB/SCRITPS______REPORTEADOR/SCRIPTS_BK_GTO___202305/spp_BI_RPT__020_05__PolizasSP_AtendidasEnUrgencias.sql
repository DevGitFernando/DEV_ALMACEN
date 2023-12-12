--------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__020_05__PolizasSP_AtendidasEnUrgencias' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__020_05__PolizasSP_AtendidasEnUrgencias 
Go--#SQL 

Create Proc spp_BI_RPT__020_05__PolizasSP_AtendidasEnUrgencias  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2018-12-31'	
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


	Select  IdEstado, Estado, IdFarmacia, Farmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, StatusAsignacion 
	Into #tmp__Programas 
	From vw_Clientes_Programas_Asignados_Unidad 
	Where -- IdPrograma = '0002' and IdSubPrograma = '1381' 
		IdPrograma = '0016' and IdSubPrograma = '0004'  --Gto
		--IdPrograma = '0002' and IdSubPrograma = '0001'


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion,
		E.IdFarmacia, F.Farmacia, 
		-- E.FolioVenta, 
		E.IdCliente, E.IdSubCliente, 
		E.IdPrograma, P.Programa, E.IdSubPrograma, P.SubPrograma, 
		E.IdBeneficiario, cast(E.Beneficiario as varchar(200)) as NombreBeneficiario, 
		cast(E.FolioReferencia as varchar(30)) as NumeroReferencia, 
		count(*) as Atenciones   
	Into #tmp_PorcentajesDispensacion 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	--From VentasEnc E (NoLock) 
	--Inner Join VentasInformacionAdicional D (NoLock) 
	--	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Inner Join #tmp__Programas P (NoLock) 
		On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdCliente = P.IdCliente and E.IdSubCliente = P.IdSubCliente 
			 and E.IdPrograma = P.IdPrograma and E.IdSubPrograma = P.IdSubPrograma )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- and D.CantidadRequerida <> 0 and CantidadEntregada <> 0 
	Group by 
		E.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion,
		E.IdFarmacia, F.Farmacia, 
		E.IdCliente, E.IdSubCliente, 
		E.IdPrograma, P.Programa, E.IdSubPrograma, P.SubPrograma,  
		E.IdBeneficiario, E.Beneficiario, E.FolioReferencia 
	

	----Update P Set NombreBeneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre), NumeroReferencia = B.FolioReferencia  
	----From #tmp_PorcentajesDispensacion P (NoLock) 
	----Inner Join CatBeneficiarios B (NoLock) 
	----	On ( P.IdEstado = B.IdEstado and P.IdFarmacia = B.IdFarmacia and P.IdCliente = B.IdCliente and P.IdSubCliente = B.IdSubCliente and P.IdBeneficiario = B.IdBeneficiario ) 



--	Select * from #tmp_PorcentajesDispensacion 


	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 


---------------------		spp_BI_RPT__020_05__PolizasSP_AtendidasEnUrgencias 



----------------------------------------------------- SALIDA FINAL 
	Select 
		-- 'Clave beneficiario' = IdBeneficiario, 
		'Nombre beneficiario' = NombreBeneficiario, 
		'Número de poliza' = NumeroReferencia, 
		'Número de atenciones' = sum(Atenciones), 
		'Destino' = Programa + ' -- ' + SubPrograma   
	From #tmp_PorcentajesDispensacion 
	Group by 
		NombreBeneficiario, NumeroReferencia, Programa + ' -- ' + SubPrograma   
	Order by 
		NombreBeneficiario 



		Select IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		-- 'Clave beneficiario' = IdBeneficiario, 
		'Nombre beneficiario' = NombreBeneficiario, 
		'Número de poliza' = NumeroReferencia, 
		'Número de atenciones' = sum(Atenciones), 
		'Destino' = Programa + ' -- ' + SubPrograma   
	From #tmp_PorcentajesDispensacion 
	Group by
		IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		NombreBeneficiario, NumeroReferencia, Programa + ' -- ' + SubPrograma   
	Order by 
		NombreBeneficiario 

	----	-- PrecioUnitario desc 
	----	-- Importe desc 
	----	Porcentaje_Participacion desc 



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


