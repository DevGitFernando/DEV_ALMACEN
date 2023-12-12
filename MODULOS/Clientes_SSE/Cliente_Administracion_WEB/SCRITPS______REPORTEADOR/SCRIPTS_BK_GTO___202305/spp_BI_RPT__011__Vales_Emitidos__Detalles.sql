------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__011__Vales_Emitidos__Detalles' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__011__Vales_Emitidos__Detalles 
Go--#SQL 

Create Proc spp_BI_RPT__011__Vales_Emitidos__Detalles  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '3032', @ClaveSSA varchar(20) = '', 
	@Benefeciario varchar(200) = '', 
	@FechaInicial varchar(10) = '2021-04-01', @FechaFinal varchar(10) = '2021-04-30' 
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
	Print @sSql 


----------		spp_BI_RPT__011__Vales_Emitidos__Detalles 

----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, 
		B.NombreCompleto as Beneficiario, B.FolioReferencia, 
		P.ClaveSSA, cast(P.ClaveSSA as varchar(5000)) as NombreGenerico,   
		P.DescripcionClave, 
		cast(P.Presentacion as varchar(5000)) as Presentacion, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento, 

		(VC.CantidadEntregada + VC.CantidadRequerida) as CantidadRecetada, 
		cast(D.Cantidad as int) as Cantidad, 
		I.NumReceta, convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 
		E.FolioVenta, 
		E.FolioVale, convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro  
		, VC.IdClaveSSA, EsCapturada, CantidadRequerida, CantidadEntregada, ExistenciaSistema, TieneCartaFaltante 
	Into #tmp_ValesEmitidos 
	From Vales_EmisionEnc E (NoLock) 
	Inner Join Vales_EmisionDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale ) 
	Inner Join Vales_Emision_InformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVale = I.FolioVale ) 	
	Inner Join vw_Beneficiarios__PRCS B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = E.IdSubCliente 
			and I.IdBeneficiario = B.IdBeneficiario ) 	
	Inner Join VentasEstadisticaClavesDispensadas VC (NoLock) 
		On ( E.IdEmpresa = VC.IdEmpresa and E.IdEstado = VC.IdEstado and E.IdFarmacia = VC.IdFarmacia and E.FolioVenta = VC.FolioVenta and D.IdClaveSSA_Sal = VC.IdClaveSSA ) 
	Inner Join vw_ClavesSSA___PRCS P On ( D.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		and E.IdEmpresa = @IdEmpresa 
		and B.NombreCompleto like '%' + @Benefeciario + '%' 
		and P.ClaveSSA like '%' + @ClaveSSA + '%' 		
	----Group by 	
	----	P.ClaveSSA, P.DescripcionClave  




	---------------------- ASIGNAR INFORMACION SEGUN REQUERIMIENTOS DEL CLIENTE 
	If @IdEstado = 13 
	Begin 

		--Update D Set  Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) 
		--From #tmp_Caducidades D 

		Update D Set FuenteDeFinanciamiento = C.Financiamiento  
		From #tmp_ValesEmitidos D 
		Inner Join vw_FACT_FuentesDeFinanciamiento_Claves_PRCS C (NoLock) On ( D.ClaveSSA = C.ClaveSSA )
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

		Update D Set NombreGenerico = C.NombreGenerico, Presentacion = C.Presentacion 
		From #tmp_ValesEmitidos D 
		Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

	End 

	
--------------------------------- 	Select * From #tmp_ValesEmitidos 
	
	
---------------------		spp_BI_RPT__011__Vales_Emitidos__Detalles  


		
----------------------------------------------------- SALIDA FINAL 
	Select 
		'Jurisdicción' = Jurisdiccion, 
		'Farmacia' = Farmacia, 		
		'Folio de venta' = FolioVenta, 
		'Folio de vale' = FolioVale, 
		'Fecha emisión de vale' = FechaRegistro, 
		'Nombre del beneficiario' = Beneficiario, 
		'Número de poliza' = FolioReferencia, 
		'Número de receta' = NumReceta, 
		'Fecha de emisión de receta' = FechaReceta, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = NombreGenerico, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Cantidad no dispensada' = (Cantidad) 
	From #tmp_ValesEmitidos 
	Order By   
		IdJurisdiccion, IdFarmacia, 
		FolioVale, Beneficiario, ClaveSSA  


	-- select * from #tmp_ValesEmitidos 

End 
Go--#SQL 


