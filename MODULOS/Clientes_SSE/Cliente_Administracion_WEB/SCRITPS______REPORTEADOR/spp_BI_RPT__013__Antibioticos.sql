----------------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__013__Antibioticos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__013__Antibioticos 
Go--#SQL 

/* 

Exec spp_BI_RPT__013__Antibioticos 
	@IdEmpresa = '004',  
	@IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
	@IdFarmacia = '5014', 
	@ClaveSSA = '',  @NumeroDePoliza = '', @NombreBeneficiario = '', 

	@NombreMedico = '', 
	@FechaInicial = '2023-04-01', @FechaFinal = '2023-04-05' 

*/ 

Create Proc spp_BI_RPT__013__Antibioticos  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '3512', 
	@ClaveSSA varchar(20) = '', @NumeroDePoliza varchar(20) = '', @NombreBeneficiario varchar(200) = '', 
	@NombreMedico varchar(200) = '', 	
	@FechaInicial varchar(10) = '2021-01-01', @FechaFinal varchar(10) = '2021-03-30'	
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
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into SII_REPORTEADOR..#vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
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
		E.IdEstado, E.IdFarmacia, E.IdSubFarmacia,
		-- E.FolioVenta, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		E.IdBeneficiario, 
		cast(E.Beneficiario as varchar(500)) as Beneficiario, 
		cast(E.FolioReferencia as varchar(100)) as Referencia, 
		cast(E.NumReceta as varchar(100)) as NumReceta, 
		convert(varchar(10), E.FechaReceta, 120) as FechaReceta, 

		E.IdMedico, 
		cast(E.Medico as varchar(500)) as NombreMedico, 	
		cast('' as varchar(100)) as CedulaMedico, 	

		--cast((M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as varchar(500)) as NombreMedico, 		
		--cast(M.NumCedula as varchar(100)) as CedulaMedico, 
		-- cast('' as varchar(100)) as DomicilioMedico, 		
		--E.Domicilio as DomicilioMedico, 
		cast(Domicilio + ', ' + Colonia + ', ' + Municipio + ', ' + Estado as varchar(1000)) as DomicilioMedico, 		
		E.IdClaveSSA_Sal as IdClaveSSA, 
		E.ClaveSSA, E.ClaveSSA as ClaveSSA_Fisica, 0 as EsRelacionada, 
		cast(E.DescripcionSal as varchar(5000)) as NombreGenerico, 
		E.DescripcionSal as DescripcionClave, 
		cast(P.Presentacion as varchar(100)) as Presentacion, P.Laboratorio, 
		cast(P.Presentacion as varchar(100)) as FuenteDeFinanciamiento, 
		E.ClaveLote, convert(varchar(10), EL.FechaRegistro, 120) as FechaEntrada_Lote, 
		convert(varchar(7), E.FechaCaducidad, 120) as FechaCaducidad, 
		cast('' as varchar(500)) as Procedencia, 
		cast(((E.Cantidad) + 0) as int) as CantidadRecetada,  
		cast((E.Cantidad) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		cast(PrecioLicitacion as numeric(14,4)) as PrecioUnitario, 
		cast(TotalLicitacion as numeric(14,4)) as CostoTotal 
	Into #tmp_Dispensacion_Antibioticos 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	--From VentasEnc E (NoLock) 
	--Inner Join VentasDet D (NoLock) 
	--	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	--Inner Join VentasDet_Lotes L (NoLock) 
	--	On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
	--		And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 				
	--Inner Join VentasInformacionAdicional I (NoLock) 
	--	On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS P (NoLock) 
		On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN and P.ClaveSSA like '%' + @ClaveSSA + '%%' ) 		
	--Inner Join CatBeneficiarios B (NoLock) 
	--	On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente
	--		and I.IdBeneficiario = B.IdBeneficiario ) 	
	----Inner Join CatMedicos M (NoLock) 
	----	On ( E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico ) 			
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes EL (NoLock)
		On ( E.IdEmpresa = EL.IdEmpresa and E.IdEstado = EL.IdEstado and E.IdFarmacia = EL.IdFarmacia and E.IdSubFarmacia = EL.IdSubFarmacia
			and E.IdProducto = EL.IdProducto and E.CodigoEAN = EL.CodigoEAN and E.ClaveLote = EL.ClaveLote ) 
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		and E.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		and ( E.Beneficiario like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' ) 
		-- and E.EsControlado = 1 
		-- and (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) like '%' + replace(@NombreMedico, ' ', '%') + '%' 		
	------Group by 	
	------	convert(varchar(10), E.FechaRegistro, 120), E.IdCliente, E.IdSubCliente, 
	------	I.IdBeneficiario, 
	------	cast((B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as varchar(500)), 
	------	cast(B.FolioReferencia as varchar(100)), 
	------	I.NumReceta, P.ClaveSSA, P.DescripcionClave 
	Order By FechaRegistro 


	Update E Set CedulaMedico = M.NumCedula 
	From #tmp_Dispensacion_Antibioticos E 
	Inner Join CatMedicos M (NoLock) On ( E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia and E.IdMedico = M.IdMedico ) 

	Update E Set Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACION' Else @sEmpresa end)
	From #tmp_Dispensacion_Antibioticos E 

	---------------------    FUENTE DE FINANCIAMIENTO 
	If Exists ( Select * From Sysobjects (nolock) Where Name = 'BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento' and xType = 'U' ) 
	Begin 
		Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento 
		From #tmp_Dispensacion_Antibioticos D 
		Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 
	End 


	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_Dispensacion_Antibioticos L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Dispensacion_Antibioticos C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 

	--Select * 
	--From #tmp_Dispensacion_Antibioticos  
	--Where EsRelacionada =  1 
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 



	--Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	--From #tmp_Dispensacion_Antibioticos E 
	--Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_RPT__013__Antibioticos 

	---------------------- ASIGNAR INFORMACION SEGUN REQUERIMIENTOS DEL CLIENTE 
	If @IdEstado = 13 
	Begin 

		--Update D Set  Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACI�N' else 'INTERMED' end) 
		--From #tmp_Caducidades D 

		Update D Set FuenteDeFinanciamiento = C.Financiamiento  
		From #tmp_Dispensacion_Antibioticos D 
		Inner Join vw_FACT_FuentesDeFinanciamiento_Claves_PRCS C (NoLock) On ( D.ClaveSSA = C.ClaveSSA )
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

		Update D Set NombreGenerico = C.NombreGenerico, Presentacion = C.Presentacion 
		From #tmp_Dispensacion_Antibioticos D 
		Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

	End 

----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion_Antibioticos 

	Select 
		-- IdFarmacia, 
		'Fecha de dispensaci�n' = FechaRegistro, 
		'Clave SSA' = ClaveSSA, 
		'Nombre gen�rico' = NombreGenerico, 
		'Descripci�n Clave SSA' = DescripcionClave, 
		'Presentaci�n' = Presentacion, 		
		'Fecha de entrada' = FechaEntrada_Lote, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 		
		'Laboratorio' = Laboratorio,  		
		
		'Fuente de financiamiento' = FuenteDeFinanciamiento, 
		'Procedencia' = Procedencia, 

		'Nombre de beneficiario' = Beneficiario, 
		'N�mero de poliza' = Referencia, 
		'Folio de receta' = NumReceta, 		
		'Fecha de receta' = FechaReceta, 
		'Nombre del m�dico' = NombreMedico, 
		'CedulaMedico' = CedulaMedico, 
		'DomicilioMedico' = DomicilioMedico, 				
		
		'Cantidad dispensada' = (CantidadDispensada), 
		'Precio unitario' = (PrecioUnitario), 
		'Costo total' = (CostoTotal)  
	From #tmp_Dispensacion_Antibioticos 
	Where CantidadDispensada > 0 
	----Group by -- IdFarmacia, 
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA, DescripcionClave    
	----Order By   
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA  



End 
Go--#SQL 


