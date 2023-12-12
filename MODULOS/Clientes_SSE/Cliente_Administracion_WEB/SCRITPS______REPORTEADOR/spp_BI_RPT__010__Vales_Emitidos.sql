------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__010__Vales_Emitidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__010__Vales_Emitidos 
Go--#SQL 

/* 

Exec spp_BI_RPT__010__Vales_Emitidos 
	@IdEmpresa = '004',  
	@IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
	@IdFarmacia = '5014', 
	@ClaveSSA = '',  

	@Benefeciario = '', 
	@FechaInicial = '2023-04-01', @FechaFinal = '2023-04-05', 
	@NombreBeneficiario = '', @NumeroDeReceta = ''  

*/ 

Create Proc spp_BI_RPT__010__Vales_Emitidos  
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
	@sSql varchar(max), 
	@sEmpresa varchar(20) 
	 
	Set @sSql = ''   
	Set @sEmpresa = 'PHARMAJAL' 


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




----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEmpresa, E.IdEstado, 
		F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, 
		E.IdCliente, E.IdSubCliente, 
		--B.NombreCompleto as Beneficiario, B.FolioReferencia, 
		cast('' as varchar(200)) as IdBeneficiario, 
		cast('' as varchar(200)) as Beneficiario, cast('' as varchar(50)) as FolioReferencia,
		P.IdClaveSSA_Sal as IdClaveSSA, 
		P.ClaveSSA, P.ClaveSSA as ClaveSSA_Fisica, 0 as EsRelacionada, 
		P.DescripcionClave, cast(P.DescripcionClave as varchar(7500)) as NombreGenerico, 

		cast('' as varchar(500)) as FuenteDeFinanciamiento, 
		cast('' as varchar(500)) as IdSubFarmacia, 
		cast('' as varchar(500)) as IdProducto, cast('' as varchar(500)) as CodigoEAN, 
		cast('' as varchar(500)) as ClaveLote, 
		cast('' as varchar(10)) as Caducidad, 
		cast('' as varchar(500)) as Laboratorio, 
		cast(@sEmpresa as varchar(500)) as Procedencia, 

		cast(D.Cantidad as int) as Cantidad, 
		-- I.NumReceta, convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 
		cast('' as varchar(20)) as NumReceta, cast('' as varchar(20)) as FechaReceta, 

		E.FolioVenta, 
		E.FolioVale, convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		0 as IncluirEnReporte 
	Into #tmp_ValesEmitidos 
	From Vales_EmisionEnc E (NoLock) 
	Inner Join Vales_EmisionDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale ) 
	--Inner Join Vales_Emision_InformacionAdicional I (NoLock) On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVale = I.FolioVale ) 	
	--Inner Join vw_Beneficiarios__PRCS B (NoLock) 
	--	On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = E.IdSubCliente 
	--		and I.IdBeneficiario = B.IdBeneficiario ) 	
	Inner Join vw_ClavesSSA___PRCS P On ( D.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where 
		convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		and 
		E.IdEmpresa = @IdEmpresa 
		--and B.NombreCompleto like '%' + @Benefeciario + '%' 
		and P.ClaveSSA like '%' + @ClaveSSA + '%' 		
	----Group by 	
	----	P.ClaveSSA, P.DescripcionClave  



	Update E Set 
		IdBeneficiario = I.IdBeneficiario, 
		-- Beneficiario = I.NombreCompleto, FolioReferencia = I.FolioReferencia, 
		--Beneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre), FolioReferencia = B.FolioReferencia, 
		NumReceta = I.NumReceta, FechaReceta = convert(varchar(10), I.FechaReceta, 120) 
	From #tmp_ValesEmitidos E (NoLock) 
	Inner Join Vales_Emision_InformacionAdicional I (NoLock) On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVale = I.FolioVale ) 	
	----Inner Join CatBeneficiarios B (NoLock) 
	----	On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente 
	----		and I.IdBeneficiario = B.IdBeneficiario ) 	
--	Where 1 = 0 


	Update E Set 
		Beneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre), FolioReferencia = B.FolioReferencia   
	From #tmp_ValesEmitidos E (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente 
			and E.IdBeneficiario = B.IdBeneficiario ) 	



	Update E Set IncluirEnReporte = 1 
	From #tmp_ValesEmitidos E (NoLock) 
	Where Beneficiario like '%' + @Benefeciario + '%' 


	Delete From #tmp_ValesEmitidos Where IncluirEnReporte = 0 



	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_ValesEmitidos L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_ValesEmitidos C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 

	--Select * 
	--From #tmp_ValesEmitidos  
	--Where EsRelacionada =  1 
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 


	---------------------    FUENTE DE FINANCIAMIENTO 
	If Exists ( Select * From Sysobjects (nolock) Where Name = 'BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento' and xType = 'U' ) 
	Begin 
		Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento 
		From #tmp_ValesEmitidos D 
		Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 
	End 
	
	
	
---------------------		spp_BI_RPT__010__Vales_Emitidos  


		
----------------------------------------------------- SALIDA FINAL 
	Select 
		'Jurisdicción' = Jurisdiccion, 
		'Farmacia' = Farmacia, 		
		'Folio de vale' = FolioVale, 
		'Fecha emisión de vale' = FechaRegistro, 
		'Folio de venta' = FolioVenta, 
		'Nombre del beneficiario' = Beneficiario, 
		'Número de poliza' = FolioReferencia, 
		'Número de receta' = NumReceta, 
		'Fecha de emisión de receta' = FechaReceta, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = '', 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = '', 

		'Lote' = ClaveLote, 
		'Caducidad' = Caducidad, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio, 
		'Procedencia' = Procedencia, 

		'Cantidad' = (Cantidad) 
	From #tmp_ValesEmitidos 
	Order By   
		IdJurisdiccion, IdFarmacia, 
		FolioVale, Beneficiario, ClaveSSA  



End 
Go--#SQL 


