------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__007__Claves_Nulo_Movimiento' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__007__Claves_Nulo_Movimiento 
Go--#SQL 

Create Proc spp_BI_RPT__007__Claves_Nulo_Movimiento  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '13', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '', 
	@FechaInicial varchar(10) = '2018-01-01', @FechaFinal varchar(10) = '2018-04-15' 
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

----------------------------------------------------- DATOS FILTRO 
	Select 
		IdEstado, Estado, IdCliente, Cliente, IdClaveSSA, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionClave, DescripcionCortaClave, 
		EsAntibiotico, EsControlado, IdTipoDeClave, TipoDeClaveDescripcion, IdPresentacion, Presentacion, StatusNivel, StatusMiembro, StatusClave, 
		1 as NuloMovimiento  
	Into #vw_CB_CuadroBasico_Claves 
	From vw_CB_CuadroBasico_Claves__PRCS 
	Where IdEstado = @IdEstado and StatusClave = 'A' and StatusNivel = 'A' and StatusMiembro = 'A'  




----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		P.ClaveSSA, P.DescripcionClave  ----, P.Laboratorio, P.Presentacion 
	Into #tmp_ClavesConMovimiento 
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
	----Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
	----	On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
	----		And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 		
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		and E.IdEmpresa = @IdEmpresa 
		and E.IdTipoMovto_Inv = 'SV'
	Group by 	
		P.ClaveSSA, P.DescripcionClave  -- , P.Laboratorio, P.Presentacion 	

	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.Cantidad) 
	----From #tmp_Caducidades E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

	Update N Set NuloMovimiento = 0 
	From #vw_CB_CuadroBasico_Claves N 
	Inner Join #tmp_ClavesConMovimiento M On ( N.ClaveSSA = M.ClaveSSA ) 

	Delete From #vw_CB_CuadroBasico_Claves Where NuloMovimiento = 0 
	--- Select * From #vw_CB_CuadroBasico_Claves 	



	Select 
		F.Jurisdiccion, F.Farmacia, 
		P.ClaveSSA, cast(P.ClaveSSA as varchar(5000)) as NombreGenerico, P.DescripcionClave, 
		cast(P.Presentacion as varchar(200)) as Presentacion, P.Laboratorio, D.ClaveLote, D.FechaCaducidad, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 
		cast('' as varchar(100)) as FuenteDeFinanciamiento, 
		cast(Existencia as int) as Cantidad, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal 		 
	Into #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	From FarmaciaProductos_CodigoEAN_Lotes D 
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) 
	Where P.ClaveSSA in ( Select ClaveSSA From #vw_CB_CuadroBasico_Claves (NoLock) )  
		and Existencia > 0  

	Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.Cantidad) 
	From #vw_CB_CuadroBasico_Claves___NuloMovimiento E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 
	
	
---	Select * From #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	
	
---------------------		spp_BI_RPT__007__Claves_Nulo_Movimiento  

---------------------    FUENTE DE FINANCIAMIENTO 
	Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento -- , Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) 
	From #vw_CB_CuadroBasico_Claves___NuloMovimiento D 
	Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 



	---------------------- ASIGNAR INFORMACION SEGUN REQUERIMIENTOS DEL CLIENTE 
	If @IdEstado = 13 
	Begin 

		--Update D Set  Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) 
		--From #tmp_Caducidades D 

		Update D Set FuenteDeFinanciamiento = C.Financiamiento  
		From #vw_CB_CuadroBasico_Claves___NuloMovimiento D 
		Inner Join vw_FACT_FuentesDeFinanciamiento_Claves_PRCS C (NoLock) On ( D.ClaveSSA = C.ClaveSSA )
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

		Update D Set NombreGenerico = C.NombreGenerico, Presentacion = C.Presentacion 
		From #vw_CB_CuadroBasico_Claves___NuloMovimiento D 
		Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

	End 


----------------------------------------------------- SALIDA FINAL 
	Select 
		'Jurisdicción' = Jurisdiccion, 
		'Farmacia' = Farmacia, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = NombreGenerico, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		-- 'Procedencia' = Procedencia_SubFarmacia, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 
		'Fuente de Financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio,  
		'Tipo de insumo' = TipoDeClaveDescripcion, 
		'Cantidad' = (Cantidad),  
		'Precio unitario' = (PrecioUnitario), 
		'Costo total' = (CostoTotal) 
	From #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	Order By   
		ClaveSSA   



End 
Go--#SQL 


