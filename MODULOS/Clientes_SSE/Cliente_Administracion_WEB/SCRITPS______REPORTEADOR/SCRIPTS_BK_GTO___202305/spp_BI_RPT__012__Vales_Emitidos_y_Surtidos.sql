------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__012__Vales_Emitidos_y_Surtidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__012__Vales_Emitidos_y_Surtidos 
Go--#SQL 

Create Proc spp_BI_RPT__012__Vales_Emitidos_y_Surtidos  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '13', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '111', @ClaveSSA varchar(20) = '', 
	@Benefeciario varchar(200) = '', @Poliza varchar(200) = '', 
	@FechaInicial varchar(10) = '2018-04-01', @FechaFinal varchar(10) = '2018-04-20' 
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


----		spp_BI_RPT__012__Vales_Emitidos_y_Surtidos 

----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEmpresa, E.IdEstado, 
		F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, 
		E.FolioVenta, 
		B.NombreCompleto as Beneficiario, B.FolioReferencia, 
		P.ClaveSSA, cast(P.ClaveSSA as varchar(5000)) as NombreGenerico, 
		P.DescripcionClave, 
		cast(P.Presentacion as varchar(100)) as Presentacion, 
		cast('' as varchar(100)) as FuenteDeFinanciamiento, 
		cast((VC.CantidadEntregada + VC.CantidadRequerida) as int) as CantidadRecetada,
		cast(VC.CantidadEntregada as int) as CantidadDispensada,  
		cast(D.CantidadRecibida as int) as Cantidad, 
		I.NumReceta, convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 
		E.FolioVale, convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		R.Folio as FolioCanjeVale, convert(varchar(10), R.FechaRegistro, 120) as FechaRegistroCanjeVale, 0 as Auxiliar  
	Into #tmp_ValesEmitidos 
	From Vales_EmisionEnc E (NoLock) 
	Inner Join Vales_EmisionDet DE (NoLock) 
		On ( E.IdEmpresa = DE.IdEmpresa and E.IdEstado = DE.IdEstado and E.IdFarmacia = DE.IdFarmacia and E.FolioVale = DE.FolioVale ) 
	Inner Join VentasEstadisticaClavesDispensadas VC (NoLock) 
		On ( DE.IdEmpresa = VC.IdEmpresa and DE.IdEstado = VC.IdEstado and DE.IdFarmacia = VC.IdFarmacia and E.FolioVenta = VC.FolioVenta and DE.IdClaveSSA_Sal = VC.IdClaveSSA ) 
	Inner Join ValesEnc R (NoLock) 
		On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmacia and E.FolioVale = R.FolioVale ) 	
	Inner Join ValesDet D (NoLock) 
		On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmacia and R.Folio = D.Folio ) 
	Inner Join Vales_Emision_InformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVale = I.FolioVale ) 	
	Inner Join vw_Beneficiarios__PRCS B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = E.IdSubCliente 
			and I.IdBeneficiario = B.IdBeneficiario ) 	
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		and E.IdEmpresa = @IdEmpresa 
		and ( B.NombreCompleto like '%' + @Benefeciario + '%' or FolioReferencia like '%' + @Poliza + '%' ) 
		and P.ClaveSSA like '%' + @ClaveSSA + '%' 		
	----Group by 	
	----	P.ClaveSSA, P.DescripcionClave  



-------------------------------------- VALIDACION MANUAL DE VALES 
	If ( @FechaInicial >='2017-04-01' and @FechaFinal <= '2017-05-31' ) 
	Begin  
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'BI_RPT__InformacionVales_Auxiliar' and xType = 'U' ) 
		Begin 	

			Select A.* 
			Into #tmpClaves 
			From BI_RPT__InformacionVales_Auxiliar A (NoLock) 
			Inner Join Vales_EmisionEnc V (NoLock) 
				On ( A.IdEmpresa = V.IdEmpresa and A.IdEstado = V.IdEstado and A.IdFarmacia = V.IdFarmacia and cast(A.FolioVale as int) = cast(V.FolioVale as int) ) 
			Inner Join #vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
			Where convert(varchar(10), V.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
				-- and A.FolioCanjeVale <> '' 

			----		spp_BI_RPT__012__Vales_Emitidos_y_Surtidos 


			If Exists ( Select top 1 * From #tmpClaves ) 
			Begin 
				Delete #tmp_ValesEmitidos 
				From #tmp_ValesEmitidos A (NoLock) 
				Inner Join #tmpClaves V (NoLock) 
					On ( A.IdEmpresa = V.IdEmpresa and A.IdEstado = V.IdEstado and A.IdFarmacia = V.IdFarmacia and cast(A.FolioVale as int) = cast(V.FolioVale as int) ) 

				
				Insert Into #tmp_ValesEmitidos 
				Select 
					E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta, 
					B.NombreCompleto as Beneficiario, B.FolioReferencia, 

					P.ClaveSSA, cast(P.ClaveSSA as varchar(5000)) as NombreGenerico, 
					P.DescripcionClave, 
					cast(P.Presentacion as varchar(100)) as Presentacion, 
					cast('' as varchar(100)) as FuenteDeFinanciamiento, 

					cast(R.Cantidad as int) as Cantidad, 
					I.NumReceta, convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 
					E.FolioVale, convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
					R.FolioCanjeVale as FolioCanjeVale, convert(varchar(10), R.FechaSurtidoVale, 120) as FechaRegistroCanjeVale, 1 as Auxiliar 
				From Vales_EmisionEnc E (NoLock) 
				Inner Join #tmpClaves R (NoLock) 
					On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmacia and cast(E.FolioVale as int) = cast(R.FolioVale as int) ) 	
				Inner Join Vales_Emision_InformacionAdicional I (NoLock) 
					On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVale = I.FolioVale ) 	
				Inner Join vw_Beneficiarios__PRCS B (NoLock) 
					On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = E.IdSubCliente 
						and I.IdBeneficiario = B.IdBeneficiario ) 	
				Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
				Inner Join vw_ClavesSSA___PRCS C (NoLock) On ( R.ClaveSSA = C.ClaveSSA ) 
				Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
					and E.IdEmpresa = @IdEmpresa 
					and ( B.NombreCompleto like '%' + @Benefeciario + '%' or FolioReferencia like '%' + @Poliza + '%' ) 
					and C.ClaveSSA like '%' + @ClaveSSA + '%' 		
					and R.FolioCanjeVale <> ''  
			End 

		End 
	End 

--------------------------------- 	Select * From #tmp_ValesEmitidos 
	
	
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

---------------------		spp_BI_RPT__012__Vales_Emitidos_y_Surtidos  


		
----------------------------------------------------- SALIDA FINAL 
	Select 
		-- IdFarmacia, 
		'Jurisdicción' = Jurisdiccion, 
		'Farmacia' = Farmacia, 		
		'Folio de vale' = FolioVale, 
		'Fecha emisión de vale' = FechaRegistro, 
		'Folio de canje de vale' = FolioCanjeVale,
		'Fecha de surtimiento vale' = FechaRegistroCanjeVale, 		
		'Nombre del beneficiario' = Beneficiario, 
		'Número de poliza' = FolioReferencia, 
		'Número de receta' = NumReceta, 
		'Fecha de emisión de receta' = FechaReceta, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = NombreGenerico, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Cantidad recetada' = CantidadRecetada, 
		'Cantidad dispensada' = CantidadDispensada, 
		'Cantidad no dispensada (Vale)' = (Cantidad)  
		, Auxiliar  
	From #tmp_ValesEmitidos 
	-- Where CantidadDispensada > 0 
	Order By   
		IdJurisdiccion, IdFarmacia, 
		FolioVale, Beneficiario, ClaveSSA  



End 
Go--#SQL 


