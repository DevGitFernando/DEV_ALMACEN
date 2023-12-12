---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__004__Caducidades_De_Insumos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__004__Caducidades_De_Insumos 
Go--#SQL 

Create Proc spp_BI_RPT__004__Caducidades_De_Insumos  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '', 
	@IdFarmacia varchar(4) = '3188', @Fecha varchar(10) = '2020-01-01', 
	@Status_Semaforizacion int = 0, @Procedencia varchar(100) = ''  
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 



------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '10' 
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


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		F.IdEstado, 
		F.IdJurisdiccion, F.Jurisdiccion, 
		F.IdFarmacia, cast(F.Farmacia as varchar(200)) as Farmacia, 
		E.IdSubFarmacia, 
		cast('' as varchar(100)) as Procedencia_SubFarmacia, 
		P.IdClaveSSA_Sal as IdClaveSSA, 
		P.ClaveSSA, 
		P.ClaveSSA as ClaveSSA_Fisica, 
		0 as EsRelacionada, 
		cast(P.DescripcionClave as varchar(5000)) as NombreGenerico,
		P.DescripcionClave, 
		P.CodigoEAN, P.Descripcion as NombreComercial, 
		E.ClaveLote, 
		convert(varchar(7), E.FechaCaducidad, 120) as FechaCaducidad, 
		P.Laboratorio, 
		cast(P.Presentacion as varchar(200)) as Presentacion, 
		--cast('' as varchar(200)) as Procedencia, 
		Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end), 
		cast(sum(E.Existencia) as int) as Cantidad, 
		cast('' as varchar(100)) as FuenteDeFinanciamiento, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal,
		0 as Semaforo, 
		datediff(mm, getdate(), IsNull(E.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar   
	Into #tmp_Caducidades 
	From FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaCaducidad, 120) <= @Fecha and 
		E.IdEmpresa = @IdEmpresa 
		-- and P.ClaveSSA like '%4148%'
	Group by 	
		F.IdEstado, 
		F.IdJurisdiccion, F.Jurisdiccion, 
		F.IdFarmacia, cast(F.Farmacia as varchar(200)), 
		E.IdSubFarmacia, 
		convert(varchar(7), E.FechaCaducidad, 120),  
		P.CodigoEAN, P.Descripcion, 
		P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, E.ClaveLote, P.Laboratorio, P.Presentacion,
		datediff(mm, getdate(), IsNull(E.FechaCaducidad, cast('2000-01-01' as datetime)))  
	Having 	sum(E.Existencia) > 0 
	Order By FechaCaducidad 
	

	-- select * from #tmp_Caducidades 

	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_Caducidades L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		spp_BI_RPT__004__Caducidades_De_Insumos  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Caducidades C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 
		--and C.ClaveSSA like '%4148%'  

	--Select * 
	--From #tmp_Caducidades C (NoLock) 
	--Where EsRelacionada =  1 
	--	and ClaveSSA like '%4148%'  
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 



	---------------- Solicitud de Franco, quitar los Caducados 
	Delete From #tmp_Caducidades Where MesesParaCaducar <= 0  


	Update C Set Semaforo = 1 
	From #tmp_Caducidades C 
	Where MesesParaCaducar <= 5 
		
	Update C Set Semaforo = 2 
	From #tmp_Caducidades C 
	Where Semaforo = 0 and MesesParaCaducar Between 6 and 8  
		
	Update C Set Semaforo = 3 
	From #tmp_Caducidades C 
	Where Semaforo = 0 and MesesParaCaducar >= 9 
	

	Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.Cantidad) 
	From #tmp_Caducidades E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_RPT__004__Caducidades_De_Insumos







---------------------    FUENTE DE FINANCIAMIENTO 
	Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento 
	From #tmp_Caducidades D 
	Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 


	---------------------- ASIGNAR INFORMACION SEGUN REQUERIMIENTOS DEL CLIENTE 
	If @IdEstado = 13 
	Begin 

		Update D Set  Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) 
		From #tmp_Caducidades D 

		Update D Set FuenteDeFinanciamiento = C.Financiamiento  
		From #tmp_Caducidades D 
		Inner Join vw_FACT_FuentesDeFinanciamiento_Claves_PRCS C (NoLock) On ( D.ClaveSSA = C.ClaveSSA )
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

		Update D Set NombreGenerico = C.NombreGenerico, Presentacion = C.Presentacion 
		From #tmp_Caducidades D 
		Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

	End 



----------------------------------------------------- SALIDA FINAL 
	Select 
		'Jurisdicción' = Jurisdiccion, 
		'Unidad' = Farmacia, 
		'Código EAN' = CodigoEAN, 
		'Nombre comercial' = NombreComercial, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = NombreGenerico, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Procedencia' = Procedencia, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 
		'Fuente de Financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio,  
		'Cantidad' = sum(Cantidad),  
		'Precio unitario' = max(PrecioUnitario), 
		'Costo total' = sum(CostoTotal), 
		-- max(MesesParaCaducar) as MesesParaCaducar,
		'Semaforo' = Semaforo   
	From #tmp_Caducidades 
	-- Where ClaveSSA like '%4148%' 
	Group by 
		Jurisdiccion, 
		Farmacia, 
		CodigoEAN, NombreComercial, 
		ClaveSSA, NombreGenerico, DescripcionClave, Presentacion, Procedencia, 
		ClaveLote, FechaCaducidad, FuenteDeFinanciamiento, Laboratorio, Semaforo   
	Order By   
		ClaveSSA   



End 
Go--#SQL 


