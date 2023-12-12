If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__004__Caducidades_De_Insumos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__004__Caducidades_De_Insumos 
Go--#SQL 

Create Proc spp_BI_RPT__004__Caducidades_De_Insumos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '7', 
	@IdFarmacia varchar(4) = '*', @Fecha varchar(10) = '2018-01-01', 
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
		F.IdEstado, 
		F.IdFarmacia, cast(F.Farmacia as varchar(200)) as Farmacia, 
		E.IdSubFarmacia, 
		cast('' as varchar(100)) as Procedencia_SubFarmacia, 
		P.ClaveSSA, P.DescripcionClave, 	
		E.ClaveLote, 
		convert(varchar(7), E.FechaCaducidad, 120) as FechaCaducidad, 
		P.Laboratorio, 
		P.Presentacion, 
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
	Group by 	
		F.IdEstado, 
		F.IdFarmacia, cast(F.Farmacia as varchar(200)), 
		E.IdSubFarmacia, 
		convert(varchar(7), E.FechaCaducidad, 120),  
		P.ClaveSSA, P.DescripcionClave, E.ClaveLote, P.Laboratorio, P.Presentacion,
		datediff(mm, getdate(), IsNull(E.FechaCaducidad, cast('2000-01-01' as datetime)))  
	Having 	sum(E.Existencia) > 0 
	Order By FechaCaducidad 
	
	
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



----------------------------------------------------- SALIDA FINAL 
	Select 
		'Unidad' = Farmacia, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Procedencia' = Procedencia_SubFarmacia, 
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
	Group by 
		Farmacia, 
		ClaveSSA, DescripcionClave, Presentacion, Procedencia_SubFarmacia, 
		ClaveLote, FechaCaducidad, FuenteDeFinanciamiento, Laboratorio, Semaforo   
	Order By   
		ClaveSSA   



End 
Go--#SQL 


