---------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__006__Beneficiario_Atendido__Detalle' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__006__Beneficiario_Atendido__Detalle 
Go--#SQL 

/* 


Exec spp_BI_RPT__006__Beneficiario_Atendido__Detalle 
	@IdEmpresa = '004',  
	@IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
	@IdFarmacia = '5014', 
	@NumeroDePoliza = '', @NombreBeneficiario = '', 
	@FechaInicial = '2023-04-01', @FechaFinal = '2023-04-04', 
	@ProgramaDeAtencion = ''	

*/ 

Create Proc spp_BI_RPT__006__Beneficiario_Atendido__Detalle 
( 
	@IdEmpresa varchar(3) = '004',  
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '5533', 
	@NumeroDePoliza varchar(20) = '', @NombreBeneficiario varchar(200) = '', 
	@FechaInicial varchar(10) = '2023-01-01', @FechaFinal varchar(10) = '2023-06-01', 
	@ProgramaDeAtencion varchar(200) = '', @Servicio Varchar(200) = ''	
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
	-- Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  
 	



----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia 
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
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


----------------------------------------------------- OBTENCION DE DATOS  
	Set @ProgramaDeAtencion = replace(@ProgramaDeAtencion, ' ', '%') 

	Select 
		1 as Origen, 
		E.IdEmpresa, 
		E.IdEstado, 
		F.Jurisdiccion, 
		E.IdFarmacia, 
		F.Farmacia, 
		E.Folio, cast('' as varchar(20)) as FolioVale, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		E.IdBeneficiario, 
		cast(E.Beneficiario as  varchar(500)) as Beneficiario,
		cast(E.FolioReferencia as varchar(100)) as Referencia, 
		cast(E.NumReceta as varchar(100)) as NumReceta, 
		E.IdClaveSSA_Sal, 
		E.ClaveSSA, E.ClaveSSA as ClaveSSA_Fisica, 0 as EsRelacionada, 
		cast('' as varchar(max)) as NombreGenerico, 
		E.DescripcionSal as DescripcionClave, 
		cast('' as varchar(max)) as Presentacion, 
		
		-------- 20210111.1730 
		--cast((sum(E.Cantidad) + 0) as int) as CantidadRecetada,  
		--cast(sum(E.Cantidad) as int) as CantidadDispensada,  
		-------- 20210111.1730 

		cast('' as varchar(500)) as FuenteDeFinanciamiento, 
		E.IdSubFarmacia, 
		E.IdProducto, E.CodigoEAN, 
		E.ClaveLote, 
		cast('' as varchar(10)) as Caducidad, 
		cast('' as varchar(500)) as Laboratorio, 
		cast('' as varchar(500)) as Procedencia, 

		cast((sum(E.PiezasTotales) + 0) as int) as CantidadRecetada,  
		cast(sum(E.PiezasTotales) as int) as CantidadDispensada,  

		0 as CantidadNoDispensada, 
		0 as CantidadEnVale, 
		cast(max(PrecioLicitacion) as numeric(14,4)) as PrecioUnitario, 
		cast(sum(TotalLicitacion) as numeric(14,4)) as CostoTotal 
	Into #tmp_Beneficiarios___Detallado  
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	----Inner Join VentasDet D (NoLock) 
	----	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	----Inner Join VentasInformacionAdicional I (NoLock) 
	----	On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	----Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 		
	----Inner Join vw_Beneficiarios__PRCS B (NoLock) 
	----	On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente
	----		and I.IdBeneficiario = B.IdBeneficiario ) 	
	Where 
		----E.Folio = 3610 and 
		----E.ClaveSSA like '%265%' and 
		convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		----and 
		----Exists 
		----( 
		----	Select * 
		----	From #vw_Farmacias F 
		----	Where E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia 
		----) 
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		and E.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		and (E.Beneficiario) like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' 
		and E.SubPrograma like '%' + @ProgramaDeAtencion + '%' 
		and E.Servicio like '%' + @Servicio + '%' 
		--and E.Folio in ( 2024, 2025 )  
	Group by 
		E.IdEmpresa, 	
		E.IdEstado, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, E.Folio, 
		convert(varchar(10), E.FechaRegistro, 120), E.IdCliente, E.IdSubCliente, 
		E.IdBeneficiario, 
		cast(E.Beneficiario as  varchar(500)), 
		cast(E.FolioReferencia as varchar(100)), 
		E.NumReceta, E.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionSal, 

		E.IdSubFarmacia, 
		E.IdProducto, E.CodigoEAN, 
		E.ClaveLote 
	Order By FechaRegistro 


	--select * from #tmp_Beneficiarios___Detallado 



	----------------- Folios de venta generados 
	Select IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro 
	Into #tmp_ValesEmitidos 
	From #tmp_Beneficiarios___Detallado 
	Group by IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro 


	----------------- Folios de vales generados 
	Select 
		0 as Origen, 
		E.IdEmpresa, 
		E.IdEstado, 
		F.Jurisdiccion, 
		E.IdFarmacia, 
		F.Farmacia, 
		E.FolioVenta as Folio, cast('' as varchar(20)) as FolioVale, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		I.IdBeneficiario, 
		cast('' as varchar(500)) as Beneficiario,
		cast('' as varchar(100)) as Referencia, 
		cast(I.NumReceta as varchar(100)) as NumReceta, 
		cast(D.IdClaveSSA_Sal as varchar(50)) as IdClaveSSA_Sal, 
		cast('' as varchar(50)) as ClaveSSA, 
		cast('' as varchar(50)) as ClaveSSA_Fisica, 
		0 as EsRelacionada, 
		cast('' as varchar(500)) as NombreGenerico, 
		cast('' as varchar(5000)) as DescripcionClave, 
		cast('' as varchar(500)) as Presentacion, 

		cast('' as varchar(500)) as FuenteDeFinanciamiento, 
		cast('' as varchar(10)) as IdSubFarmacia, 
		cast('' as varchar(10)) as IdProducto, cast('' as varchar(10)) as CodigoEAN, 
		cast('' as varchar(10)) as ClaveLote, 
		cast('' as varchar(10)) as Caducidad, 
		cast('' as varchar(500)) as Laboratorio, 
		cast('' as varchar(500)) as Procedencia, 

		cast(0 as int) as CantidadRecetada,  
		cast(0 as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		0 as CantidadEnVale, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal 
	Into #tmp_ValesEmitidos__Detalles  
	From Vales_EmisionEnc E (NoLock) 
	Inner Join #tmp_ValesEmitidos V (NoLock) 
		On ( E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioVenta = V.Folio ) 
	Inner Join Vales_EmisionDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale ) 
	Inner Join Vales_Emision_InformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVale = I.FolioVale ) 	
	Inner Join VentasEstadisticaClavesDispensadas VC (NoLock) 
		On ( E.IdEmpresa = VC.IdEmpresa and E.IdEstado = VC.IdEstado and E.IdFarmacia = VC.IdFarmacia and E.FolioVenta = VC.FolioVenta and D.IdClaveSSA_Sal = VC.IdClaveSSA ) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 


	Update E Set Beneficiario = B.NombreCompleto, Referencia = B.FolioReferencia 
	From #tmp_ValesEmitidos__Detalles E (NoLock) 
	Inner Join vw_Beneficiarios B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = E.IdSubCliente and E.IdBeneficiario = B.IdBeneficiario ) 	
	

	Update E Set 
		ClaveSSA = S.ClaveSSA, ClaveSSA_Fisica = S.ClaveSSA, 
		NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave, Presentacion = S.Presentacion 
	From #tmp_ValesEmitidos__Detalles E (NoLock) 
	Inner Join vw_ClavesSSA_Sales S On ( E.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) 



	Insert Into #tmp_Beneficiarios___Detallado 
	Select * 
	From #tmp_ValesEmitidos__Detalles V (NoLock) 
	Where Not Exists 
	( 
		Select * 
		From #tmp_Beneficiarios___Detallado D (NoLock) 
		Where V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.Folio = D.Folio 
			and V.IdClaveSSA_Sal = D.IdClaveSSA_Sal 
	) 


	----------------- Folios de vales generados 



	---  		spp_BI_RPT__006__Beneficiario_Atendido__Detalle

--------------------------- RESUMEN POR VALE  

	-- Select R.*, E.*   
	Update R Set FolioVale = E.FolioVale 
	From #tmp_Beneficiarios___Detallado R (NoLock) 
	Inner Join Vales_EmisionEnc E (NoLock) 
		On ( R.IdEstado = E.IdEstado and R.IdFarmacia = E.IdFarmacia and R.Folio = E.FolioVenta ) 
	Inner Join Vales_EmisionDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale and D.IdClaveSSA_Sal = R.IdClaveSSA_Sal )  
		--On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale and D.ClaveSSA = R.ClaveSSA )


	Select 
		E.IdEmpresa, 
		E.IdEstado, 
		E.IdFarmacia, 
		E.FolioVale, 
		E.IdClaveSSA_Sal as IdClaveSSA, 
		E.ClaveSSA, 
		E.ClaveSSA_Fisica, 
		0 as CantidadEnVale 
	Into #tmp_Beneficiarios___Detallado__Vales 
	From #tmp_Beneficiarios___Detallado E (NoLock) 
	Where FolioVale <> '' 


	Update E Set CantidadEnVale = 
		IsNull  
		(
			(  
				select sum(Cantidad) 
				From Vales_EmisionDet D (NoLock) 
				Where E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale and E.IdClaveSSA = D.IdClaveSSA_Sal 
				--Where E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale and E.ClaveSSA = D.ClaveSSA 
			) 
		, 0 ) 
	From #tmp_Beneficiarios___Detallado__Vales E (NoLock) 


	--select * from #tmp_Beneficiarios___Detallado__Vales 


	Update L Set ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_Beneficiarios___Detallado__Vales L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 



	--select * from #tmp_Beneficiarios___Detallado__Vales 


	Update E Set CantidadEnVale = D.CantidadEnVale 
	From #tmp_Beneficiarios___Detallado E (NoLock) 
	Inner Join #tmp_Beneficiarios___Detallado__Vales D On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale 
		and E.IdClaveSSA_Sal = D.IdClaveSSA ) 
		--and E.ClaveSSA = D.ClaveSSA ) 
	---Where E.ClaveLote = '' --- Solo considerar los registros de Vales 



	----Select * 
	----From #tmp_Beneficiarios___Detallado (NoLock) 
	----Where FolioVale <> '' 


	----Select 
	----	E.IdFarmacia, E.FolioVenta, E.FolioVale, E.ClaveSSA 
	----Into #tmp_ValesEmitidos 
	----From Vales_EmisionEnc E (NoLock) 
	----Inner Join Vales_EmisionDet D (NoLock) 
	----	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVale = D.FolioVale ) 
	----Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	----Where 
	----	Exists 
	----	(
	----		Select * 
	----		From #tmp_Beneficiarios___Detallado (NoLock) 
			 
	----	) 


--------------------------- RESUMEN POR VALE  



	---  		spp_BI_RPT__006__Beneficiario_Atendido__Detalle


	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA_Sal = P.IdClaveSSA 
	From #tmp_Beneficiarios___Detallado L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA_Sal = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		spp_BI_RPT__006__Beneficiario_Atendido__Detalle  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Beneficiarios___Detallado C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 

	Update C Set Presentacion = S.Presentacion 
	From #tmp_Beneficiarios___Detallado C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) 
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 


	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Completar la información comercial del producto 
	Update D Set Caducidad = convert(varchar(7), F.FechaCaducidad, 120)  
	From #tmp_Beneficiarios___Detallado D 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On 
		( 
			D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia and D.IdSubFarmacia = F.IdSubFarmacia 
			and D.IdProducto = F.IdProducto and D.CodigoEAN = F.CodigoEAN and D.ClaveLote = F.ClaveLote
		) 

	Update D Set Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACION' Else @sEmpresa end)
	From #tmp_Beneficiarios___Detallado D 
	Where ClaveLote <> '' 

	Update D Set Laboratorio = P.Laboratorio, Presentacion = P.Presentacion   
	From #tmp_Beneficiarios___Detallado D 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 


	---------------------    FUENTE DE FINANCIAMIENTO 
	If Exists ( Select * From Sysobjects (nolock) Where Name = 'BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento' and xType = 'U' ) 
	Begin 
		Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento 
		From #tmp_Beneficiarios___Detallado D 
		Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 
	End 
	-------------------------- Completar la información comercial del producto 	
	-------------------------------------------------------------------------------------------------------- 	


	
---------------------		spp_BI_RPT__006__Beneficiario_Atendido__Detalle

--------------------------- CONCENTRADO 
	Select 
		Jurisdiccion, Farmacia, 
		Folio, 
		FolioVale, FechaRegistro, 
		IdCliente, IdSubCliente, 
		IdBeneficiario, Beneficiario,
		Referencia, NumReceta, 

		ClaveSSA_Fisica, 
		ClaveSSA, NombreGenerico, DescripcionClave, Presentacion, 

		FuenteDeFinanciamiento, 
		Procedencia, Laboratorio, ClaveLote, Caducidad, 


		sum(CantidadRecetada + CantidadEnVale) as CantidadRecetada,  
		sum(CantidadDispensada) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		Sum(CantidadEnVale) As CantidadEnVale,
		max(PrecioUnitario) as PrecioUnitario, 
		max(CostoTotal) as CostoTotal 
	Into #tmp_Beneficiarios 
	From #tmp_Beneficiarios___Detallado E (NoLock) 
	Group by 
		Jurisdiccion, Farmacia, 
		Folio, 
		FolioVale, FechaRegistro, 
		IdCliente, IdSubCliente, 
		IdBeneficiario, Beneficiario,
		Referencia, NumReceta, 
		ClaveSSA_Fisica, 
		ClaveSSA, NombreGenerico, DescripcionClave, Presentacion, 
		FuenteDeFinanciamiento, Procedencia, Laboratorio, ClaveLote, Caducidad 


	


--------------------------- CONCENTRADO 


----------------------------------------------------- SALIDA FINAL 
	Select 
		-- IdFarmacia, 
		'Jurisdiccíón' = Jurisdiccion, 
		'Farmacia' = Farmacia, 
		'Fecha de Atención' = FechaRegistro, 
		'Nombre de beneficiario' = Beneficiario, 
		'Número de poliza' = Referencia, 
		'Folio de venta' = Folio, 
		'Folio de receta' = NumReceta, 
		'Folio de vale' = FolioVale, 
		'Observaciones' = '', 
		'Clave SSA Física' = ClaveSSA_Fisica, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = NombreGenerico, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 

		'Lote' = ClaveLote, 
		'Caducidad' = Caducidad, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio, 
		'Procedencia' = Procedencia, 

		'Cantidad recetada' = sum(CantidadRecetada), 
		'Cantidad dispensada' = sum(CantidadDispensada), 
		'Cantidad no dispensada' = sum(CantidadNoDispensada), 
		'Cantidad en vale' = Sum(CantidadEnVale),
		'Diferencia' = sum(CantidadRecetada - CantidadDispensada), 
		'Precio unitario' = max(PrecioUnitario), 
		'Costo total' = sum(CostoTotal)  
	From #tmp_Beneficiarios 
	Group by -- IdFarmacia, 
		Jurisdiccion, Farmacia, 
		FechaRegistro, 
		Folio, 
		FolioVale, Beneficiario, Referencia, NumReceta, 
		ClaveSSA_Fisica, 
		ClaveSSA, NombreGenerico, DescripcionClave, Presentacion, 
		ClaveLote, Caducidad, FuenteDeFinanciamiento, Laboratorio, Procedencia 
	Order By   
		FechaRegistro, Folio, FolioVale, Beneficiario, Referencia, NumReceta, ClaveSSA  


	select 
		Folio, 
		ClaveSSA 
	From #tmp_Beneficiarios 
	group by 
			Folio, 
		ClaveSSA 
	having count(*) >= 2 


	--select *
	--from #tmp_ValesEmitidos__Detalles 

	--select * 
	--from #tmp_Beneficiarios___Detallado__Vales 

End 
Go--#SQL 


