If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__003__Entradas_y_Salidas' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__003__Entradas_y_Salidas 
Go--#SQL 

Create Proc spp_BI_RPT__003__Entradas_y_Salidas 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '', 
	@IdFarmacia varchar(4) = '12', @TipoMovto int = 0, @ClaveSSA varchar(20) = '',
	@FechaInicial varchar(10) = '2016-05-10', @FechaFinal varchar(10) = '2016-05-19'	
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
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		IdEmpresa, IdEstado, IdFarmacia, Farmacia, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(Entradas) as Entradas, sum(Salidas) as Salidas, 
		min(MovtoInicial) as MovtoInicial_Base, 
		min(MovtoInicial) as MovtoInicial, max(MovtoFinal) as MovtoFinal, 
		0 as ExistenciaInicial, 0 as ExistenciaFinal 			
	Into #tmp_Movimientos 	
	From 
	( 
		Select 
			E.IdEmpresa, E.IdEstado, 
			E.IdFarmacia, cast(F.Farmacia as varchar(200)) as Farmacia, 
			-- L.IdSubFarmacia, 
			cast('' as varchar(100)) as IdSubFarmacia, 
			cast('' as varchar(100)) as Procedencia_SubFarmacia, 
			
			----P.ClaveSSA, 
			----P.DescripcionClave, 	
			----P.Laboratorio, 
			----P.Presentacion, 	
			
			----cast('' as varchar(100)) ClaveSSA, 
			----cast('' as varchar(max)) DescripcionClave, 	
			----cast('' as varchar(200)) Laboratorio, 
			----cast('' as varchar(200)) Presentacion, 		
			
			D.IdProducto, D.CodigoEAN, 
			-- L.ClaveLote, 
			cast('' as varchar(100)) as ClaveLote, 
					
			--(case when E.TipoES = 'E' Then cast(sum(L.Cantidad) as int) Else 0 End) as Entradas, 
			--(case when E.TipoES = 'S' Then cast(sum(L.Cantidad) as int) Else 0 End) as Salidas, 		
			
			(case when E.TipoES = 'E' Then cast(sum(D.Cantidad) as int) Else 0 End) as Entradas, 
			(case when E.TipoES = 'S' Then cast(sum(D.Cantidad) as int) Else 0 End) as Salidas, 					
			
			min(D.Keyx) as MovtoInicial, min(D.Keyx) as MovtoFinal, 
			0 as ExistenciaInicial, 0 as ExistenciaFinal, 
			
			cast('' as varchar(100)) as FuenteDeFinanciamiento, 
			cast(0 as numeric(14,4)) as PrecioUnitario, 
			cast(0 as numeric(14,4)) as CostoTotal  
			--- datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar   
		From MovtosInv_Enc E (NoLock) 
		Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
		----Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		----	On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
		----		And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 
		Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
		Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
			and E.IdEmpresa = @IdEmpresa 
		Group By 
			E.IdEmpresa, E.IdEstado, 
			E.IdFarmacia, cast(F.Farmacia as varchar(200)), 
			-- L.IdSubFarmacia, 
			D.IdProducto, D.CodigoEAN, 
			-- L.ClaveLote, 
			E.TipoES  
	) T 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, Farmacia, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote  
	
	
	
------------------------------- Obtener las existencias iniciales y finales 		
	Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN
	Into #tmp_Movimientos__Productos 
	From #tmp_Movimientos D 
	group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN 
	
	
	Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, max(D.Keyx) as Keyx, 0 as ExistenciaInicial, 
		max(FechaRegistro) as Fecha 
	Into #tmp_ExistenciasIniciales 
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
	Inner Join #tmp_Movimientos__Productos P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia 
			and D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where convert(varchar(10), E.FechaRegistro, 120) < @FechaInicial -- between @FechaInicial and @FechaFinal 
		and E.IdEmpresa = @IdEmpresa 
	Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN 
		

	Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, max(D.Keyx) as Keyx, 0 as ExistenciaFinal, 
		max(FechaRegistro) as Fecha 
	Into #tmp_ExistenciasFinales  
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
	Inner Join #tmp_Movimientos__Productos P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia 
			and D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where convert(varchar(10), E.FechaRegistro, 120) <= @FechaFinal -- between @FechaInicial and @FechaFinal 
		and E.IdEmpresa = @IdEmpresa 
	Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN 
	
	
	
	Update M Set ExistenciaInicial = D.Existencia 
	From #tmp_ExistenciasIniciales M  
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) On ( M.Keyx = D.Keyx ) 
	
	Update M Set ExistenciaFinal = D.Existencia 
	From #tmp_ExistenciasFinales M  
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) On ( M.Keyx = D.Keyx ) 

		
	Update D Set ExistenciaInicial = P.ExistenciaInicial 
	From #tmp_Movimientos D (NoLock) 
	Inner Join #tmp_ExistenciasIniciales P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia 
			and D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 		

	Update D Set ExistenciaFinal = P.ExistenciaFinal 
	From #tmp_Movimientos D (NoLock) 
	Inner Join #tmp_ExistenciasFinales P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia 
			and D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN	) 	

------------------------------- Obtener las existencias iniciales y finales 		
	
	
	
------------------------------- Obtener las existencias iniciales y finales 	
	----Update M  Set MovtoInicial = 
	----	IsNull(
	----	(
	----		Select max(D.Keyx) 
	----		From MovtosInv_Enc E (NoLock) 
	----		Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
	----			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 	
	----		Where convert(varchar(10), E.FechaRegistro, 120) < @FechaInicial	 
	----			and M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia 
	----			and M.IdProducto = D.IdProducto and M.CodigoEAN = D.CodigoEAN
	----	), MovtoInicial)  
	----From #tmp_Movimientos M  
	
	
	----Update M Set ExistenciaInicial = D.Existencia 
	----From #tmp_Movimientos M  
	----Inner Join MovtosInv_Det_CodigosEAN D (NoLock) On ( M.MovtoInicial = D.Keyx ) 
	
	----Update M Set ExistenciaFinal = D.Existencia 
	----From #tmp_Movimientos M  
	----Inner Join MovtosInv_Det_CodigosEAN D (NoLock) On ( M.MovtoFinal = D.Keyx ) 
------------------------------- Obtener las existencias iniciales y finales 	

	
	
--		spp_BI_RPT__003__Entradas_y_Salidas	
	
	
	
	Select 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Farmacia, 
		D.IdSubFarmacia, 
		cast('' as varchar(200)) as Procedencia, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento, 
		P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.Laboratorio, 	
		D.IdProducto, D.CodigoEAN, D.ClaveLote, 
		cast('' as varchar(200)) as FechaCaducidad, 
		sum(D.Entradas) as Entradas, sum(D.Salidas) as Salidas, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal_Entradas, 
		cast(0 as numeric(14,4)) as CostoTotal_Salidas, 
		cast(0 as int) as Cantidad_Inicial, 
		cast(0 as int) as Cantidad_Final, 
		max(ExistenciaInicial) as ExistenciaInicial, max(ExistenciaFinal) as ExistenciaFinal   
	Into #tmp_Movimientos_Resumen 
	From #tmp_Movimientos D 
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where P.ClaveSSA like '%' + @ClaveSSA + '%'
	Group by 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.Farmacia, 
		D.IdSubFarmacia, 
		P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.Laboratorio, 	
		D.IdProducto, D.CodigoEAN, D.ClaveLote
	
	Update E Set 
		PrecioUnitario = P.PrecioUnitario, 
		CostoTotal_Entradas = (P.PrecioUnitario * E.Entradas), 
		CostoTotal_Salidas = (P.PrecioUnitario * E.Salidas) 
	From #tmp_Movimientos_Resumen E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 


	Update L Set FechaCaducidad = convert(varchar(7), F.FechaCaducidad, 120) 
	From #tmp_Movimientos_Resumen L (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia 
		And L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote ) 	


---------------------		spp_BI_RPT__003__Entradas_y_Salidas 


--		select * from #tmp_Movimientos_Resumen  

 

----------------------------------------------------- SALIDA FINAL 
	Select 
		'Unidad' = Farmacia, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Procedencia' = Procedencia, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 
		'Fuente de Financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio,
		
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




End 
Go--#SQL 


