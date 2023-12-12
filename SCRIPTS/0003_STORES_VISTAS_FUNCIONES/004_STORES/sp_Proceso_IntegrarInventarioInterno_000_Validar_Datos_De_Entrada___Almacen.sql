
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Almacen' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Almacen  
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Almacen  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0032', 
	@ValidarCajasCompletas smallint = 0, @TipoDeInventario smallint = 0    	 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@LargoEAN int, 
	@sCadenaFormato varchar(100) 

	Set @LargoEAN = 15 
	Set @sCadenaFormato = replicate('0', @LargoEAN) 

	Select * 
	into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
		
		
----------------------------------	 Integrar los productos-lotes que no se contemplaron en el inventario 
--- Se igualan las cantidades de los productos. 
---	Exec sp_Proceso_IntegrarInventarioInterno_001 @IdEmpresa, @IdEstado, @IdFarmacia, 1    --- Cuadrar inventario 


--------	Insert Into INV__InventarioInterno_CargaMasiva 
--------		( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
--------		  IdPasillo, IdEstante, IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Caducidad, Cantidad, EnInventario ) 
--------	Select 
--------		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
--------		IdPasillo, IdEstante, IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, space(10), 0 as Cantidad, 0 as EnInventario
--------	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
--------	Where Not Exists 
--------	(
--------		Select 	* 
--------		From INV__InventarioInterno_CargaMasiva L 
--------		Where 
--------			L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
--------			And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote 
--------	) 
--------	
--------	Update L Set Caducidad = convert(varchar(10), F.FechaCaducidad, 120) 
--------	From INV__InventarioInterno_CargaMasiva L 
--------	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
--------		On ( L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
--------			 And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote 	) 
--------	Where EnInventario = 0 	
----------------------------------	 Integrar los productos-lotes que no se contemplaron en el inventario 	
		
		
----------------------------------- FORMATEAR VALORES 		
	Update C Set IdProducto = P.IdProducto, CodigoEAN = P.CodigoEAN, TasaIva = P.TasaIVA, ContenidoPaquete = P.ContenidoPaquete, 
		ClaveSSA = P.ClaveSSA, DescripcionClaveSSA = P.DescripcionClave  
	From INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	
	Update C Set IdProducto = P.IdProducto, CodigoEAN = P.CodigoEAN, TasaIva = P.TasaIVA, ContenidoPaquete = P.ContenidoPaquete, 
		ClaveSSA = P.ClaveSSA, DescripcionClaveSSA = P.DescripcionClave  
	From INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN_Interno, @LargoEAN) ) 		
	
	Update C Set EsConsignacion = 1 
	From INV__InventarioInterno_CargaMasiva C 
	Where ClaveLote like '%*%' 
	
	Update C Set Caducidad = convert(varchar(10), Caducidad, 120) 
	From INV__InventarioInterno_CargaMasiva C 	
----------------------------------- FORMATEAR VALORES 				
		
		
------------------------------------------------------ UBICACIONES 
	Select IdPasillo, DescripcionPasillo
	into #tmpPasillos 	
	From 
	( 
		select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, ('Pasillo #' + IdPasillo) as DescripcionPasillo 
		from INV__InventarioInterno_CargaMasiva 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		Group by IdEmpresa, IdEstado, IdFarmacia, IdPasillo 
	) as T 
	Where Not Exists ( Select * From CatPasillos P 
					   Where T.IdEmpresa = P.IdEmpresa and T.IdEstado = P.IdEstado and T.IdFarmacia = P.IdFarmacia and T.IdPasillo = P.IdPasillo ) 	
	order by IdEmpresa, IdEstado, IdFarmacia, IdPasillo 
			
	
	Select IdPasillo, IdEstante, DescripcionEstante 
	into #tmpEstantes  	
	From 
	( 
		select IdEmpresa, IdEstado, IdFarmacia, 
			IdPasillo, IdEstante, ('Estante #' + IdEstante) as DescripcionEstante   
		from INV__InventarioInterno_CargaMasiva 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		Group by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante 
	) as T 
	Where Not Exists ( Select * From CatPasillos_Estantes P 
					   Where T.IdEmpresa = P.IdEmpresa and T.IdEstado = P.IdEstado and T.IdFarmacia = P.IdFarmacia 
					   and T.IdPasillo = P.IdPasillo and T.IdEstante = P.IdEstante ) 		
	order by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante 	
	
	
	Select IdPasillo, IdEstante, IdEntrepaño, DescripcionEntrepaño 
	into #tmpEntrepaño  	
	From 
	( 
		select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, 
			('Entrepaño #' + IdEntrepaño) as DescripcionEntrepaño     
		from INV__InventarioInterno_CargaMasiva 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		Group by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño 
	) as T 
	Where Not Exists ( Select * From CatPasillos_Estantes_Entrepaños P 
					   Where T.IdEmpresa = P.IdEmpresa and T.IdEstado = P.IdEstado and T.IdFarmacia = P.IdFarmacia 
					   and T.IdPasillo = P.IdPasillo and T.IdEstante = P.IdEstante and T.IdEntrepaño = P.IdEntrepaño  ) 			
	order by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño 	
	
------------------------------------------------------ UBICACIONES 


------------------------------------------------------ LOTES MULTIPLES CADUCIDADES 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, space(20) as IdProducto, CodigoEAN, ClaveLote, cast(0 as float) as TasaIva, 
		0 as EsConsignacion, Caducidad as FechaCaducidad, 0 as ExistenciaSistema, 
		cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, cast(0 as numeric(14,4)) as Costo     	  
	Into #tmpLotes	
	From INV__InventarioInterno_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, CodigoEAN, ClaveLote, Caducidad  

	Select Distinct 
		C.IdPasillo, C.IdEstante, C.IdEntrepaño, 
		P.ClaveSSA, P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, C.EnInventario  
	Into #tmpLotes_Multiples_Caducidades 
	from INV__InventarioInterno_CargaMasiva C 
	Inner Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, count(*) as Veces 
		From 
		( 
			Select Distinct	
				IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad  
			From #tmpLotes B  
			Where 
			Not Exists 
			(
				Select * From  FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
				Where B.IdEmpresa = E.IdEmpresa and B.IdEstado = E.IdEstado and B.IdFarmacia = E.IdFarmacia and B.IdSubFarmacia = E.IdSubFarmacia 
					and B.IdProducto = E.IdProducto and B.CodigoEAN = E.CodigoEAN and B.ClaveLote = E.ClaveLote 
			)	
		) as T 
		Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 
		Having count(*) > 1 
	) as R On ( C.CodigoEAN = R.CodigoEAN and C.ClaveLote = R.ClaveLote )   
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 		
	
	
	---Delete From #tmpLotes_Multiples_Caducidades Where EnInventario = 0 	
------------------------------------------------------ LOTES MULTIPLES CADUCIDADES 


------------------------------------------------------ LOTES MULTIPLES PRECIOS  
----		Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Almacen     

	Select Distinct 
		C.IdPasillo, C.IdEstante, C.IdEntrepaño, 
		P.ClaveSSA, P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 
		C.CodigoEAN, C.ClaveLote, C.Costo, C.Caducidad, C.Cantidad, C.EnInventario  
	Into #tmpLotes_Multiples_Costos  
	from INV__InventarioInterno_CargaMasiva C 
	Inner Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote, count(*) as Veces 
		From 
		( 
			Select Distinct	
				IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote, Costo   
			From INV__InventarioInterno_CargaMasiva B  
			Where Costo > 0 
		) as T 
		Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote 
		Having count(*) > 1 
	) as R On ( C.CodigoEAN = R.CodigoEAN and C.ClaveLote = R.ClaveLote )   
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	Where Costo > 0 
------------------------------------------------------ LOTES MULTIPLES PRECIOS  

----		Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Almacen     


------------------------------------------------------ CADUCIDADES 
	Select Distinct 
		C.IdPasillo, C.IdEstante, C.IdEntrepaño, 
		P.ClaveSSA, P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, C.EnInventario  
	Into #tmpLotes_Error_Caducidades 
	from INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	Where cast(left(Caducidad, 4) as int) not between 2010 and 2030 
	
	
	Insert Into #tmpLotes_Error_Caducidades 
	Select Distinct 
		C.IdPasillo, C.IdEstante, C.IdEntrepaño, 
		P.ClaveSSA, P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, C.EnInventario   
	from INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	Where cast(right(left(Caducidad, 7), 2) as int) not between 1 and 12 	
	
------------------------------------------------------ CADUCIDADES 
 

------------------------------------------------------ CODIGOS NO ENCONTRADOS 

	Select Distinct 
		C.IdPasillo, C.IdEstante, C.IdEntrepaño, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad 
	Into #tmpEAN_No_Encontrados  
	from INV__InventarioInterno_CargaMasiva C 
	Where Not Exists 
	( 
		Select * 
		From #vw_Productos_CodigoEAN P 
		Where 
			right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) or 
			right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN_Interno, @LargoEAN) 		
	) 
	
------------------------------------------------------ CODIGOS NO ENCONTRADOS  



------------------------------------------------------ CODIGOS SIN PRECIO  
	Select Distinct 
		C.IdPasillo, C.IdEstante, C.IdEntrepaño, 
		P.ClaveSSA, 'Descripción clave' = P.DescripcionClave, 
		P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 'Nombre comercial' = P.Descripcion, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, C.EnInventario  
	Into #tmpProductoSinPrecio  
	from INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 		
	Where C.Costo = 0 and C.EnInventario = 1 
------------------------------------------------------ CODIGOS SIN PRECIO  

 
------------------------------------------------------ LOTES CON SUB-FARMACIA INCORRECTA 
	Select Distinct 
		P.ClaveSSA, 'Descripción clave' = P.DescripcionClave, 
		P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 'Nombre comercial' = P.Descripcion, 
		'SubFarmacia' = C.IdSubFarmacia, 'Tipo de Sub-Farmacia' = 'VENTA' + space(20), 
		C.CodigoEAN, C.ClaveLote, 'Tipo de Sub-Farmacia según Lote' = 'CONSIGNACIÓN', 
		C.Caducidad, C.Cantidad, C.EnInventario  
	Into #tmpSubFarmaciasIncorrecta   
	From INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	Inner Join CatEstados_SubFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdSubFarmacia = F.IdSubFarmacia ) 
	Where C.EnInventario = 1 and F.EsConsignacion = 0 and C.ClaveLote like '%*%' 

	Insert Into #tmpSubFarmaciasIncorrecta   
	Select Distinct 
		P.ClaveSSA, 'Descripción clave' = P.DescripcionClave, 
		P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 'Nombre comercial' = P.Descripcion, 
		'SubFarmacia' = C.IdSubFarmacia, 'Tipo de Sub-Farmacia' = 'CONSIGNACIÓN', 
		C.CodigoEAN, C.ClaveLote, 'Tipo de Sub-Farmacia según Lote' = 'VENTA', 
		C.Caducidad, C.Cantidad, C.EnInventario  
	From INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	Inner Join CatEstados_SubFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdSubFarmacia = F.IdSubFarmacia ) 
	Where C.EnInventario = 1 and F.EsConsignacion = 1 and C.ClaveLote Not like '%*%' 		
------------------------------------------------------ LOTES CON SUB-FARMACIA INCORRECTA 


----------------------- SALIDA FINAL	
	Select * From #tmpPasillos 
	Select * From #tmpEstantes 
	Select * From #tmpEntrepaño 		

	Select * From #tmpLotes_Error_Caducidades Where EnInventario = 1  	
	Select * From #tmpLotes_Multiples_Caducidades Where EnInventario = 1  	
	Order By CodigoEAN, ClaveLote, Caducidad 
	
	Select * From #tmpEAN_No_Encontrados   
 
	Select * From #tmpProductoSinPrecio 
	Select * From #tmpLotes_Multiples_Costos Where EnInventario = 1  
	Order By CodigoEAN, ClaveLote, Costo  
	
	Select * From #tmpSubFarmaciasIncorrecta  	
----------------------- SALIDA FINAL	


---		sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Almacen


End 
Go--#SQL 



