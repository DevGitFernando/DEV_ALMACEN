------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia  
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0021', 
	@ValidarCajasCompletas smallint = 1, @TipoDeInventario smallint = 0   
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
	----Insert Into INV__InventarioInterno_CargaMasiva 
	----	( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
	----	  IdPasillo, IdEstante, IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Caducidad, Cantidad, EnInventario ) 
	----Select 
	----	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
	----	-1 as IdPasillo, -1 as IdEstante, -1 as IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, space(10), 0 as Cantidad, 0 as EnInventario
	----From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	----Where -- Existencia > 0 and  -- 1 = 0 and 
	----Not Exists 
	----(
	----	Select 	* 
	----	From INV__InventarioInterno_CargaMasiva L 
	----	Where 
	----		L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
	----		And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote 
	----) 
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
	
	Update C Set EsConsignacion = 1, TipoDeInventario = 2  
	From INV__InventarioInterno_CargaMasiva C 
	Where ClaveLote like '%*%' 
	
	Update C Set Caducidad = convert(varchar(10), Caducidad, 120) 
	From INV__InventarioInterno_CargaMasiva C 	


	if @ValidarCajasCompletas = 1 
	Begin 
		Update C Set CajasCompletas = (case when ((cast(round((Cantidad / ContenidoPaquete), 2) as numeric(14,2)) % ContenidoPaquete) % 1) = 0 then 1 else 0 end)  
		From INV__InventarioInterno_CargaMasiva C 	
	End 

	If @TipoDeInventario <> 0 
	Begin 
		Update C Set TipoDeInventarioCorrecto = 0 
		From INV__InventarioInterno_CargaMasiva C 
		Where TipoDeInventario <> @TipoDeInventario 
	End 
----------------------------------- FORMATEAR VALORES 				
		
		
------------------------------------------------------ LOTES MULTIPLES CADUCIDADES 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, space(20) as IdProducto, CodigoEAN, ClaveLote, cast(0 as float) as TasaIva, 
		0 as EsConsignacion, Caducidad as FechaCaducidad, 0 as ExistenciaSistema, 
		cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, cast(0 as numeric(14,4)) as Costo     	  
	Into #tmpLotes	
	From INV__InventarioInterno_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, CodigoEAN, ClaveLote, Caducidad  

	Select Distinct 
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
----		Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia     

	Select Distinct 
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

----		Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia     


------------------------------------------------------ CADUCIDADES 
	Select Distinct 
		P.ClaveSSA, P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, C.EnInventario  
	Into #tmpLotes_Error_Caducidades 
	from INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	Where cast(left(Caducidad, 4) as int) not between 2010 and 2030 
	
	
	Insert Into #tmpLotes_Error_Caducidades 
	Select Distinct 
		P.ClaveSSA, P.IdProducto, P.CodigoEAN as CodigoEAN_Base, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, C.EnInventario   
	from INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 	
	Where cast(right(left(Caducidad, 7), 2) as int) not between 1 and 12 	
	
------------------------------------------------------ CADUCIDADES 
 

------------------------------------------------------ CODIGOS NO ENCONTRADOS 

	Select Distinct 
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

 
 
------------------------------------------------------ LOTES CON FORMATO INVALIDO  
	Select Distinct 
		P.ClaveSSA, P.DescripcionClave, C.IdSubFarmacia, P.IdProducto, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, 
		( case when C.EnInventario = 1 then 'SI' else 'NO' end) as EnArchivoDeInventario 
	Into #tmpLotes_Error_Formato 
	From INV__InventarioInterno_CargaMasiva C 
	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 		
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and ( C.ClaveLote = '' or C.ClaveLote like '%' + char(39) + '%' ) 
	--------Group by P.ClaveSSA, P.DescripcionClave, C.IdSubFarmacia, P.IdProducto, C.CodigoEAN, C.ClaveLote, C.Caducidad  
------------------------------------------------------ LOTES CON FORMATO INVALIDO  
 

 ------------------------------------------------------ CAJAS INCOMPLETAS   
	Select Distinct 
		C.ClaveSSA, C.DescripcionClaveSSA, C.IdSubFarmacia, C.IdProducto, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, C.ContenidoPaquete, 
		cast(round((C.Cantidad / C.ContenidoPaquete), 2) as numeric(14,2)) as Cajas,  
		( case when C.EnInventario = 1 then 'SI' else 'NO' end) as EnArchivoDeInventario  
	Into #tmpCajas_Incompletas   
	From INV__InventarioInterno_CargaMasiva C 
---	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 		
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		And CajasCompletas = 0 and @ValidarCajasCompletas = 1 
 ------------------------------------------------------ CAJAS INCOMPLETAS 


 ------------------------------------------------------ INVENTARIO INCORRECTO  
	Select Distinct 
		C.ClaveSSA, C.DescripcionClaveSSA, C.IdSubFarmacia, C.IdProducto, 
		C.CodigoEAN, C.ClaveLote, C.Caducidad, C.Cantidad, 
		( case when C.EnInventario = 1 then 'SI' else 'NO' end) as EnArchivoDeInventario  
	Into #tmpInventario_Incorrecto    
	From INV__InventarioInterno_CargaMasiva C 
---	Inner Join #vw_Productos_CodigoEAN P On ( right(@sCadenaFormato + C.CodigoEAN, @LargoEAN) = right(@sCadenaFormato + P.CodigoEAN, @LargoEAN) ) 		
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and TipoDeInventarioCorrecto = 0  
 ------------------------------------------------------ INVENTARIO INCORRECTO  


 --		sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia  

----------------------- SALIDA FINAL	
	Select * From #tmpLotes_Error_Caducidades Where EnInventario = 1  	
	Select * From #tmpLotes_Multiples_Caducidades Where EnInventario = 1  	
	Order By CodigoEAN, ClaveLote, Caducidad 
	
	Select * From #tmpEAN_No_Encontrados   
 
	Select * From #tmpProductoSinPrecio 
	Select * From #tmpLotes_Multiples_Costos Where EnInventario = 1  
	Order By CodigoEAN, ClaveLote, Costo  
	
	Select * From #tmpSubFarmaciasIncorrecta  
	
	Select * From #tmpLotes_Error_Formato 

	Select * From #tmpCajas_Incompletas 

	Select * From #tmpInventario_Incorrecto 
----------------------- SALIDA FINAL	


---		sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia


End 
Go--#SQL 



