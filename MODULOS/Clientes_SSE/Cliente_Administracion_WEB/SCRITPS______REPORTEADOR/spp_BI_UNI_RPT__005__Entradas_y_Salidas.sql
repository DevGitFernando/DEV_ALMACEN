------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__005__Entradas_y_Salidas' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__005__Entradas_y_Salidas 
Go--#SQL 

/*  

Exec spp_BI_UNI_RPT__005__Entradas_y_Salidas 
	@IdEmpresa = '004', 
	@IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
	@IdFarmacia = '4512', 
	@FechaInicial = '2022-01-01', @FechaFinal = '2022-12-31', 

	@TipoDeClave = 0, --- 0 ==> General, 1 ==> Controlado, 2 ==> Antibiotico, 3 ==> Refrigerado, 
	@FuenteDeFinanciamiento = '', 
	@Procedencia = '', 
	@Semaforizacion = 0,  --- 0 ==> TODOS, 1 ==> Proximo a caducar, 2 ==> Mediana caducidad, 3 ==> Amplia caducidad, 
	@Validar_Entradas = 0, 
	@Entrada_Menor = 0, @Entrada_Mayor = 0, 
	@Validar_Salidas = 0,  
	@Salida_Menor = 0, @Salida_Mayor = 0    

*/ 

Create Proc spp_BI_UNI_RPT__005__Entradas_y_Salidas 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '13', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '', 
	@IdFarmacia varchar(4) = '14', 
	@FechaInicial varchar(10) = '2015-11-01', @FechaFinal varchar(10) = '2017-11-30', 
	@TipoDeClave int = 0, --- 0 ==> General, 1 ==> Controlado, 2 ==> Antibiotico, 3 ==> Refrigerado, 
	@FuenteDeFinanciamiento varchar(100) = '', 
	@Procedencia varchar(100) = '', 
	@Semaforizacion int = 0,  --- 0 ==> TODOS, 1 ==> Proximo a caducar, 2 ==> Mediana caducidad, 3 ==> Amplia caducidad, 
	@Validar_Entradas smallint = 0, 
	@Entrada_Menor int = 0, @Entrada_Mayor int = 0, 
	@Validar_Salidas smallint = 0,  
	@Salida_Menor int = 0, @Salida_Mayor int = 0    
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@sEmpresa varchar(100) 

	Set @sSql = '' 

	Set @sEmpresa = 'PHARMAJAL'



------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '10' 
------------------------------------------ Generar tablas de catalogos  



----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, Estado, IdMunicipio, Municipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, Estado, IdMunicipio, Municipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, Estado, IdMunicipio, Municipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 1 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		IdEmpresa, IdEstado, Estado, Municipio, Jurisdiccion, IdFarmacia, Farmacia, FechaMenor, FechaMayor, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(Entradas) as Entradas, sum(Salidas) as Salidas  
	Into #tmp_Movimientos 	
	From 
	( 
		Select 
			E.IdEmpresa, E.IdEstado, F.Estado, F.Municipio, F.Jurisdiccion, 
			E.IdFarmacia, cast(F.Farmacia as varchar(200)) as Farmacia, 
			L.IdSubFarmacia, 
			cast('' as varchar(100)) as Procedencia_SubFarmacia, 
			
			----P.ClaveSSA, 
			----P.DescripcionClave, 	
			----P.Laboratorio, 
			----P.Presentacion, 	
			
			----cast('' as varchar(100)) ClaveSSA, 
			----cast('' as varchar(max)) DescripcionClave, 	
			----cast('' as varchar(200)) Laboratorio, 
			----cast('' as varchar(200)) Presentacion, 		
			
			min(convert(varchar(10), E.FechaRegistro, 120)) as FechaMenor, 
			max(convert(varchar(10), E.FechaRegistro, 120)) as FechaMayor, 

			D.IdProducto, D.CodigoEAN, 
			L.ClaveLote, 
					
			(case when E.TipoES = 'E' Then cast(sum(L.Cantidad) as int) Else 0 End) as Entradas, 
			(case when E.TipoES = 'S' Then cast(sum(L.Cantidad) as int) Else 0 End) as Salidas, 		
			cast('' as varchar(100)) as FuenteDeFinanciamiento, 
			cast(0 as numeric(14,4)) as PrecioUnitario, 
			cast(0 as numeric(14,4)) as CostoTotal  
			--- datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar   
		From MovtosInv_Enc E (NoLock) 
		Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
		Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
			On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
				And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 
		----Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		----	On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and D.FolioMovtoInv = F.FolioMovtoInv 
		----		And L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN ) 	
		----Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
		Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
		Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
			and E.IdEmpresa = @IdEmpresa 
		Group By 
			E.IdEmpresa, E.IdEstado, F.Estado, F.Municipio, F.Jurisdiccion, 
			E.IdFarmacia, cast(F.Farmacia as varchar(200)), 
			L.IdSubFarmacia, D.IdProducto, D.CodigoEAN, L.ClaveLote, E.TipoES  
	) T 
	Group by 
		IdEmpresa, IdEstado, Estado, Municipio, Jurisdiccion, 
		IdFarmacia, Farmacia, FechaMenor, FechaMayor, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote  
	
	
	
	Select 
		D.IdEmpresa, D.IdEstado, 
		D.Estado, D.Municipio, D.Jurisdiccion, 
		D.IdFarmacia, D.Farmacia, 
		D.FechaMenor, D.FechaMayor, 
		D.IdSubFarmacia, 
		cast('' as varchar(200)) as Procedencia, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento, 
		P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.Laboratorio, 	
		D.IdProducto, D.CodigoEAN, D.ClaveLote, 
		cast('' as varchar(200)) as FechaCaducidad, 

		P.EsControlado, P.EsAntibiotico, P.EsRefrigerado, 

		0 as Incluir, 
		0 as Semaforo,  
		0 as MesesParaCaducar, 

		sum(D.Entradas) as Entradas, sum(D.Salidas) as Salidas, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal_Entradas, 
		cast(0 as numeric(14,4)) as CostoTotal_Salidas, 
		cast(0 as int) as Cantidad_Inicial, 
		cast(0 as int) as Cantidad_Final 
	Into #tmp_Movimientos_Resumen 
	From #tmp_Movimientos D 
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
    Inner Join BI_UNI_RPT__DTS__ClavesSSA	M ON ( P.ClaveSSA = M.ClaveSSA )  	
	Group by 
		D.IdEmpresa, D.IdEstado, 
		D.Estado, D.Municipio, D.Jurisdiccion, 
		D.IdFarmacia, D.Farmacia, 
		D.FechaMenor, D.FechaMayor, 
		D.IdSubFarmacia, 
		P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.Laboratorio, 	
		D.IdProducto, D.CodigoEAN, D.ClaveLote, 
		P.EsControlado, P.EsAntibiotico, P.EsRefrigerado  
	
	
	Update E Set 
		PrecioUnitario = P.PrecioUnitario, 
		CostoTotal_Entradas = (P.PrecioUnitario * E.Entradas), 
		CostoTotal_Salidas = (P.PrecioUnitario * E.Salidas) 
	From #tmp_Movimientos_Resumen E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 


	Update L Set 
		FechaCaducidad = convert(varchar(7), F.FechaCaducidad, 120), 
		MesesParaCaducar = datediff(mm, getdate(), IsNull(F.FechaCaducidad, cast('2000-01-01' as datetime)))   
	From #tmp_Movimientos_Resumen L (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia 
		And L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN ) 	



	---------------------------- SEMAFORO	
	Update C Set Semaforo = 1 
	From #tmp_Movimientos_Resumen C (noLock) 
	Where MesesParaCaducar <= 5 
		
	Update C Set Semaforo = 2 
	From #tmp_Movimientos_Resumen C (noLock) 
	Where Semaforo = 0 and MesesParaCaducar Between 6 and 8  
		
	Update C Set Semaforo = 3 
	From #tmp_Movimientos_Resumen C (noLock) 
	Where Semaforo = 0 and MesesParaCaducar >= 9 
	---------------------------- SEMAFORO	


	If @TipoDeClave = 0 --- 0 ==> General, 1 ==> Controlado, 2 ==> Antibiotico, 3 ==> Refrigerado 	
		Begin 
			Update R Set Incluir = 1 
			From #tmp_Movimientos_Resumen R (NoLock) 
		End 
	Else 
		Begin 

			If @TipoDeClave = 1  
			Begin 
				Update R Set Incluir = 1 
				From #tmp_Movimientos_Resumen R (NoLock) 
				Where EsControlado = 1 
			End  

			If @TipoDeClave = 2  
			Begin 
				Update R Set Incluir = 1 
				From #tmp_Movimientos_Resumen R (NoLock) 
				Where EsAntibiotico = 1 
			End  

			If @TipoDeClave = 3   
			Begin 
				Update R Set Incluir = 1 
				From #tmp_Movimientos_Resumen R (NoLock) 
				Where EsRefrigerado = 1 
			End  

		End 

	------------ Eliminar los registros que no cumplan el criterio 
	Delete From #tmp_Movimientos_Resumen Where Incluir = 0 


	If @Semaforizacion <> 0 
		Begin 
			Delete #tmp_Movimientos_Resumen
			From #tmp_Movimientos_Resumen R (NoLock) 
			Where Semaforo <> @Semaforizacion  
		End 


	------------------------------------------ VALIDACION DE CANTIDADES 
	/* 
	@Validar_Entradas smallint = 0, 
	@Entrada_Menor int = 0, @Entrada_Mayor int = 0, 
	@Validar_Salidas smallint = 0,  
	@Salida_Menor int = 0, @Salida_Mayor int = 0  
	*/ 

	----- Restablecer todos los registros 
	Update R Set Incluir = 0  
	From #tmp_Movimientos_Resumen R (NoLock) 
	Where ( @Validar_Entradas = 1 or @Validar_Salidas = 1 ) 

	If @Validar_Entradas = 1 and @Validar_Salidas = 1 
		Begin
			Update R Set Incluir = 1   
			From #tmp_Movimientos_Resumen R (NoLock) 
			Where ( Entradas between @Entrada_Menor and @Entrada_Mayor) and ( Salidas between @Salida_Menor and @Salida_Mayor)  
		End 
	Else 
		Begin 
			If @Validar_Entradas = 1 
			Begin 
				Update R Set Incluir = 1   
				From #tmp_Movimientos_Resumen R (NoLock) 
				Where ( Entradas between @Entrada_Menor and @Entrada_Mayor) 
			End 

			If @Validar_Salidas = 1 
			Begin 
				Update R Set Incluir = 1   
				From #tmp_Movimientos_Resumen R (NoLock) 
				Where ( Salidas between @Salida_Menor and @Salida_Mayor) 
			End 

		End 

	------------ Eliminar los registros que no cumplan el criterio 
	Delete From #tmp_Movimientos_Resumen Where Incluir = 0 


	------------------------------------------ VALIDACION DE CANTIDADES 


---------------------		spp_BI_UNI_RPT__005__Entradas_y_Salidas 


--		select * from #tmp_Movimientos_Resumen  


---------------------    FUENTE DE FINANCIAMIENTO 
	Set @FuenteDeFinanciamiento = replace(@FuenteDeFinanciamiento, ' ', '%') 
	Set @Procedencia = replace(@Procedencia, ' ', '%')
	Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento, Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else @sEmpresa end) 
	From #tmp_Movimientos_Resumen D 
	Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 

 

----------------------------------------------------- SALIDA FINAL 
	Select 
		'Estado' = Estado, 
		'Municipio' = Municipio, 
		'Juriscción' = Jurisdiccion, 
		'Unidad' = Farmacia, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Procedencia' = Procedencia, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 
		'Fuente de Financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio,

		'Es Controlado' = EsControlado, 
		'Es Antibiotico' = EsAntibiotico, 
		'Es Refrigerado' = EsRefrigerado, 

		'Fecha inicial' = FechaMenor, 
		'Fecha final' = FechaMayor, 
		
		'Precio unitario' = (PrecioUnitario), 		  
		'Entradas' = (Entradas),  
		'Costo total entradas' = (CostoTotal_Entradas),  				
		'Salidas' = (Salidas), 
		'Costo total salidas' = (CostoTotal_Salidas), 
		'Cantidad inicial' = Cantidad_Inicial, 
		'Cantidad final' = Cantidad_Final 
	From #tmp_Movimientos_Resumen  
	Where 
		FuenteDeFinanciamiento like '%' + @FuenteDeFinanciamiento + '%' 
		and Procedencia like '%' + @Procedencia + '%' 



End 
Go--#SQL 


