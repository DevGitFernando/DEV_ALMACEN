--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__003__Entradas_y_Salidas' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__003__Entradas_y_Salidas 
Go--#SQL 

/* 

	Exec spp_BI_RPT__003__Entradas_y_Salidas 
		@IdEmpresa = '001', 
		@IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '', 
		@IdFarmacia = '11', @TipoMovto = 0, @ClaveSSA = '', 
		@FechaInicial = '2018-01-01', @FechaFinal = '2018-01-05', 
		@TipoDeClave = 0, --- 0 ==> General, 1 ==> Controlado, 2 ==> Antibiotico, 3 ==> Refrigerado, 
		@FuenteDeFinanciamiento = '', 
		@Procedencia = '', 
		@Semaforizacion = 1,  --- 0 ==> TODOS, 1 ==> Proximo a caducar, 2 ==> Mediana caducidad, 3 ==> Amplia caducidad, 
		@Validar_Entradas = 0, 
		@Entrada_Menor = 1, @Entrada_Mayor = 50, 
		@Validar_Salidas = 0,  
		@Salida_Menor = 200, @Salida_Mayor = 500  
*/ 

Create Proc spp_BI_RPT__003__Entradas_y_Salidas  
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '', 
	@IdFarmacia varchar(4) = '4011', @TipoMovto int = 0, @ClaveSSA varchar(50) = '', --010.000.0101.00',  
	@FechaInicial varchar(10) = '2021-02-01', @FechaFinal varchar(10) = '2022-03-10',  
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

	Set @ClaveSSA = replace(@ClaveSSA, ' ', '%') 
	Set @sEmpresa = 'PHARMAJAL'

/* 
	Update C Set Semaforo = 1 
	From #tmp_Movimientos_Resumen C (noLock) 
	Where MesesParaCaducar <= 5 
		
	Update C Set Semaforo = 2 
	From #tmp_Movimientos_Resumen C (noLock) 
	Where Semaforo = 0 and MesesParaCaducar Between 6 and 8  
		
	Update C Set Semaforo = 3 
	From #tmp_Movimientos_Resumen C (noLock) 
	Where Semaforo = 0 and MesesParaCaducar >= 9 
*/ 

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


--------------------------------------------- FILTRO DE CLAVES 
	Select ClaveSSA, ClaveSSA as ClaveSSA_Fisica, 0 as Relacionada  
	Into #tmpListaClaves 
	From CatClavesSSA_Sales 
	Where ClaveSSA like '%' + @ClaveSSA + '%' 

	Insert Into #tmpListaClaves 
	Select ClaveSSA_Relacionada, ClaveSSA as ClaveSSA_Fisica, 1 as Relacionada  	 
	From vw_Relacion_ClavesSSA_Claves P (NoLock) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 
	Where P.ClaveSSA like '%' + @ClaveSSA + '%' 
--------------------------------------------- FILTRO DE CLAVES 



	----------------------------------- Tabla de Codigos EAN 
	Select P.* 
	Into #tmp__vw_Productos_CodigoEAN__PRCS
	From vw_Productos_CodigoEAN__PRCS P 
	Inner Join #tmpListaClaves C (NoLock) On ( P.ClaveSSA = C.ClaveSSA ) 
	-- Where ClaveSSA like '%' + @ClaveSSA + '%' 


	-- select * from #tmp__vw_Productos_CodigoEAN__PRCS 


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		IdEmpresa, IdEstado, 
		IdJurisdiccion, Jurisdiccion, 
		IdFarmacia, Farmacia, 
		min(Fecha) as FechaMenor, max(Fecha) as FechaMayor, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(Entradas) as Entradas, sum(Salidas) as Salidas, 
		0 as MovtoInicial_Base, 
		min(MovtoInicial) as MovtoInicial, max(MovtoFinal) as MovtoFinal, 
		0 as ExistenciaInicial, 0 as ExistenciaFinal 	
	Into #tmp_Movimientos 	
	From 
	( 
		Select 
			E.IdEmpresa, E.IdEstado, 
			F.IdJurisdiccion, F.Jurisdiccion, 
			E.IdFarmacia, cast(F.Farmacia as varchar(200)) as Farmacia, 
			-- L.IdSubFarmacia, 
			-- cast('' as varchar(100)) as IdSubFarmacia, 
			cast('' as varchar(100)) as Procedencia_SubFarmacia, 
			
			----P.ClaveSSA, 
			----P.DescripcionClave, 	
			----P.Laboratorio, 
			----P.Presentacion, 	
			
			----cast('' as varchar(100)) ClaveSSA, 
			----cast('' as varchar(max)) DescripcionClave, 	
			----cast('' as varchar(200)) Laboratorio, 
			----cast('' as varchar(200)) Presentacion, 		
			
			-- min(convert(varchar(10), E.FechaRegistro, 120)) as FechaMenor, 
			-- max(convert(varchar(10), E.FechaRegistro, 120)) as FechaMayor, 
			convert(varchar(10), E.FechaRegistro, 120) as Fecha,  

			D.IdProducto, D.CodigoEAN, 
			L.IdSubFarmacia, 
			L.ClaveLote, 
			-- cast('' as varchar(100)) as ClaveLote, 
					
			--(case when E.TipoES = 'E' Then cast(sum(L.Cantidad) as int) Else 0 End) as Entradas, 
			--(case when E.TipoES = 'S' Then cast(sum(L.Cantidad) as int) Else 0 End) as Salidas, 		
			
			(case when E.TipoES = 'E' Then cast(sum(L.Cantidad) as int) Else 0 End) as Entradas, 
			(case when E.TipoES = 'S' Then cast(sum(L.Cantidad) as int) Else 0 End) as Salidas, 					
			
			--min(D.Keyx) as MovtoPrevio, 
			min(D.Keyx) as MovtoInicial, min(D.Keyx) as MovtoFinal, 
			0 as ExistenciaInicial, 0 as ExistenciaFinal, 
			
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
		Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
		Inner Join #tmp__vw_Productos_CodigoEAN__PRCS P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
		Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
			and E.IdEmpresa = @IdEmpresa 
		Group By 
			E.IdEmpresa, E.IdEstado, 
			F.IdJurisdiccion, F.Jurisdiccion, 
			E.IdFarmacia, cast(F.Farmacia as varchar(200)), 
			--FechaMenor, FechaMayor, 
			-- L.IdSubFarmacia, 
			convert(varchar(10), E.FechaRegistro, 120), 
			D.IdProducto, D.CodigoEAN, 
			L.IdSubFarmacia, 
			L.ClaveLote, 
			E.TipoES  
	) T 
	Group by 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, -- FechaMenor, FechaMayor, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote  
	

-----		spp_BI_RPT__003__Entradas_y_Salidas

-------------------------- REVISION DE DATOS 	
	--Select L.* 
	--From MovtosInv_Enc E (NoLock) 
	--Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
	--	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
	--Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
	--	On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
	--		And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 
	--Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	--Inner Join #tmp__vw_Productos_CodigoEAN__PRCS P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	--Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
	--	and E.IdEmpresa = @IdEmpresa 
-------------------------- REVISION DE DATOS 	


	
------------------------------- Obtener las existencias iniciales y finales 		
	Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN
	Into #tmp_Movimientos__Productos 
	From #tmp_Movimientos D 
	group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN 
------------------------------- Obtener las existencias iniciales y finales 		
	
	
	
------------------------------- Obtener las existencias iniciales y finales 	
	Update M  Set MovtoInicial_Base = 
		IsNull( 
		(
			Select top 1 (L.Keyx) 
			From MovtosInv_Enc E (NoLock) 
			Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
			Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
				On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
					And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 
			Where convert(varchar(10), E.FechaRegistro, 120) < @FechaInicial	 
				and M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.IdSubFarmacia = L.IdSubFarmacia 
				and M.IdProducto = D.IdProducto and M.CodigoEAN = D.CodigoEAN and M.ClaveLote = L.ClaveLote 
			Order by E.FechaRegistro desc 	

			--From MovtosInv_Enc E (NoLock) 
			--Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
			--	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 	

		), MovtoInicial_Base)  
	From #tmp_Movimientos M  

	-- select * from #tmp_Movimientos 

	------------------------- Buscar el primer movimiento del periodo solicitado en caso de no existir movimientos previos 
	Update M  Set MovtoInicial_Base = 
		IsNull( 
		(
			Select top 1 (L.Keyx) 
			From MovtosInv_Enc E (NoLock) 
			Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
			Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
				On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
					And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 
			Where convert(varchar(10), E.FechaRegistro, 120) >= @FechaInicial	 
				and M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.IdSubFarmacia = L.IdSubFarmacia 
				and M.IdProducto = D.IdProducto and M.CodigoEAN = D.CodigoEAN and M.ClaveLote = L.ClaveLote 
			Order by E.FechaRegistro  	

			--From MovtosInv_Enc E (NoLock) 
			--Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
			--	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 	

		), MovtoInicial_Base)  
	From #tmp_Movimientos M  
	Where MovtoInicial_Base = 0 

	
	-- select * from #tmp_Movimientos 

	--------------		spp_BI_RPT__003__Entradas_y_Salidas 

	
	Update M Set ExistenciaInicial = D.Existencia 
	From #tmp_Movimientos M  
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( M.MovtoInicial_Base = D.Keyx ) 
	

	Update M Set ExistenciaFinal = ( ExistenciaInicial + Entradas ) - Salidas 
	From #tmp_Movimientos M  
	-- Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( M.MovtoInicial_Base = D.Keyx ) 


	--select * from #tmp_Movimientos 


	-- select * from #tmp_Movimientos 
------------------------------- Obtener las existencias iniciales y finales 	

	
	
	Select 
		D.IdEmpresa, D.IdEstado, 
		D.IdJurisdiccion, D.Jurisdiccion, 
		D.IdFarmacia, D.Farmacia, 
		D.FechaMenor, D.FechaMayor, 
		D.IdSubFarmacia, 
		cast('' as varchar(200)) as Procedencia, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento, 
		P.IdClaveSSA_Sal as IdClaveSSA, 
		P.ClaveSSA, P.ClaveSSA as ClaveSSA_Fisica, 
		0 as EsRelacionada, 
		cast('' as varchar(5000)) as NombreGenerico, P.DescripcionClave, 
		cast(P.Presentacion as varchar(200)) as Presentacion, 
		P.Laboratorio, 	
		D.IdProducto, D.CodigoEAN, 
		P.Descripcion as NombreComercial, 
		D.ClaveLote, 
		cast('' as varchar(200)) as FechaCaducidad, 

		P.EsControlado, P.EsAntibiotico, P.EsRefrigerado, 

		0 as Incluir, 
		0 as Semaforo,  
		0 as MesesParaCaducar, 

		cast(sum(D.Entradas) as int) as Entradas, 
		cast(sum(D.Salidas) as int) as Salidas, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal_Entradas, 
		cast(0 as numeric(14,4)) as CostoTotal_Salidas, 
		cast(sum(ExistenciaInicial) as int) as Cantidad_Inicial, 
		cast(sum(ExistenciaFinal) as int) as Cantidad_Final, 
		cast(sum(ExistenciaInicial) as int) as ExistenciaInicial, 
		cast(sum(ExistenciaFinal) as int) as ExistenciaFinal   
	Into #tmp_Movimientos_Resumen 
	From #tmp_Movimientos D 
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	--Inner Join #tmp__vw_Productos_CodigoEAN__PRCS P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	-- Where ClaveSSA like '%' + @ClaveSSA + '%' 
	Group by 
		D.IdEmpresa, D.IdEstado, 
		D.IdJurisdiccion, D.Jurisdiccion, 
		D.IdFarmacia, D.Farmacia, 
		D.FechaMenor, D.FechaMayor, 
		D.IdSubFarmacia, 
		P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.Laboratorio, 	
		D.IdProducto, D.CodigoEAN, P.Descripcion, D.ClaveLote, 
		P.EsControlado, P.EsAntibiotico, P.EsRefrigerado 
	
	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_Movimientos_Resumen L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Movimientos_Resumen C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 	


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
		And L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote ) 	

--	select '#tmp_Movimientos_Resumen' as tmp_Movimientos_Resumen, * from #tmp_Movimientos_Resumen 

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


---------------------		spp_BI_RPT__003__Entradas_y_Salidas 


--		select * from #tmp_Movimientos_Resumen  



---------------------    FUENTE DE FINANCIAMIENTO 
	Set @FuenteDeFinanciamiento = replace(@FuenteDeFinanciamiento, ' ', '%') 
	Set @Procedencia = replace(@Procedencia, ' ', '%')
	Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento, Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else @sEmpresa end) 
	From #tmp_Movimientos_Resumen D 
	Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 


	---------------------- ASIGNAR INFORMACION SEGUN REQUERIMIENTOS DEL CLIENTE 
	If @IdEstado = 13 
	Begin 

		Update D Set  Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else @sEmpresa end) 
		From #tmp_Movimientos_Resumen D 

		Update D Set FuenteDeFinanciamiento = C.Financiamiento  
		From #tmp_Movimientos_Resumen D 
		Inner Join vw_FACT_FuentesDeFinanciamiento_Claves_PRCS C (NoLock) On ( D.ClaveSSA = C.ClaveSSA )
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

		Update D Set NombreGenerico = C.NombreGenerico, Presentacion = C.Presentacion 
		From #tmp_Movimientos_Resumen D 
		Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

	End 
 

----------------------------------------------------- SALIDA FINAL 
	Select 
		'Juriscción' = Jurisdiccion, 
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
		'Semaforo caducidad' = Semaforo,  
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
		
		-- 'Cantidad inicial' = Cantidad_Inicial, 
		-- 'Cantidad final' = Cantidad_Final, 
		
		'Cantidad inicial' = ExistenciaInicial, 
		'Cantidad final' = ExistenciaFinal 
		
	From #tmp_Movimientos_Resumen  
	Where 
		FuenteDeFinanciamiento like '%' + @FuenteDeFinanciamiento + '%' 
		and Procedencia like '%' + @Procedencia + '%' 


End 
Go--#SQL 


