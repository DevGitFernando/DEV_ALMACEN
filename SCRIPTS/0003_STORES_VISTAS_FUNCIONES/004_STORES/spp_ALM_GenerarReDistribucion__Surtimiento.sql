---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALM_GenerarReDistribucion__Surtimiento' and xType = 'P' ) 
   Drop Proc spp_ALM_GenerarReDistribucion__Surtimiento 
Go--#SQL 

Create Proc spp_ALM_GenerarReDistribucion__Surtimiento  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', @FolioSurtido varchar(8) = '00000001', 
	@IdPersonal varchar(4) = ''	 
) 
With Encryption  
As  
Begin 
Set NoCount On 
Declare  
	@IdSurtimiento int,  
	@MesesCaducidad int, 
	@MesesCaducidad_Consigna int, 
	@TipoDeInventario int 
	
	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4) 
	Set @FolioSurtido = RIGHT('000000000000' + @FolioSurtido, 8) 
	Set @IdPersonal = RIGHT('000000000000' + @IdPersonal, 4) 


		---------- Se Des-Aplican las existencias de surtido
	Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioSurtido = @FolioSurtido, @TipoFactor = 2, @Validar = 0

---	Obtener el parametro de caducidades 	
	Select @MesesCaducidad = MesesCaducidad, @MesesCaducidad_Consigna = MesesCaducidad_Consigna, @TipoDeInventario = TipoDeInventario 
	From Pedidos_Cedis_Enc_Surtido P 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioSurtido = @FolioSurtido 	

	Set @IdSurtimiento = 1 
	Set @MesesCaducidad = IsNull(@MesesCaducidad, 1) 
	Set @MesesCaducidad_Consigna = IsNull(@MesesCaducidad_Consigna, 1) 


---	Obtener las claves a re-procesar 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, ClaveSSA, IdSubFarmacia, 
		IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, 
		CantidadRequerida, CantidadAsignada, (CantidadRequerida - CantidadAsignada) as Faltante, 
		0 as Procesar, 
		Keyx 
	Into #tmpRedistribucion 	
	From Pedidos_Cedis_Det_Surtido_Distribucion P 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioSurtido = @FolioSurtido
		 And (CantidadRequerida - CantidadAsignada) > 0 
		

	--select * 	from #tmpRedistribucion 

		
	Update D Set Procesar = 1 
	From #tmpRedistribucion  D 
	Where Faltante > 0 	
---	Obtener las claves a re-procesar 	

	 	
---	Generar tabla base de proceso 
	Select 
		identity(int, 1, 1) as Keyx, 
		IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, sum(Faltante) as CantidadRequerida, 0 as Procesado  
	Into #tmpPedido 
	From #tmpRedistribucion P (NoLock) 
	Where Procesar = 1 
	Group by IdEmpresa, IdEstado, IdFarmacia, ClaveSSA
---	Generar tabla base de proceso 


	--select * from #tmpRedistribucion  
	--select * from #tmpPedido
	
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
	-------------------------------------------------------------- OBTENER LOS LOTES Y UBICACIONES DE ACUERDO A LAS CADUCIDADES   

	----------------------------- GENERAR TABLA BASE 
	Select  F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, F.EsConsignacion, F.IdPasillo, F.IdEstante, F.IdEntrepaño, C.EsDePickeo,
		(F.Existencia - (F.ExistenciaEnTransito + F.ExistenciaSurtidos)) As Existencia,
		IdClaveSSA_Sal, ClaveSSA, L.FechaCaducidad, datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar
	Into #FarmaciaProductos
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Inner join FarmaciaProductos_CodigoEAN_Lotes L (NoLock)
		On (F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia
			 And F.IdSubFarmacia = L.IdSubFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN And F.ClaveLote = L.ClaveLote)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN)
	Inner Join CatPasillos_Estantes_Entrepaños C (NoLock)
		On ( F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia And F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño)
	Where F.Status = 'A'	
		and 1 = 0 


	----------------------------- AGREGAR VENTA 
	Insert Into #FarmaciaProductos 
	Select  F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, F.EsConsignacion, F.IdPasillo, F.IdEstante, F.IdEntrepaño, C.EsDePickeo,
		(F.Existencia - (F.ExistenciaEnTransito + F.ExistenciaSurtidos)) As Existencia,
		IdClaveSSA_Sal, ClaveSSA, L.FechaCaducidad, datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Inner join FarmaciaProductos_CodigoEAN_Lotes L (NoLock)
		On (F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia
			 And F.IdSubFarmacia = L.IdSubFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN And F.ClaveLote = L.ClaveLote)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN)
	Inner Join CatPasillos_Estantes_Entrepaños C (NoLock)
		On ( F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia And F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño)
	Where F.Status = 'A'	
		and F.ClaveLote not like '%*%' 
		and datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) >= @MesesCaducidad 
		and P.ClaveSSA in ( select ClaveSSA from #tmpPedido  )

	----------------------------- AGREGAR CONSIGNA  
	Insert Into #FarmaciaProductos  
	Select  F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, F.EsConsignacion, F.IdPasillo, F.IdEstante, F.IdEntrepaño, C.EsDePickeo,
		(F.Existencia - (F.ExistenciaEnTransito + F.ExistenciaSurtidos)) As Existencia,
		IdClaveSSA_Sal, ClaveSSA, L.FechaCaducidad, datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar  
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Inner join FarmaciaProductos_CodigoEAN_Lotes L (NoLock)
		On (F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia
			 And F.IdSubFarmacia = L.IdSubFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN And F.ClaveLote = L.ClaveLote)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN)
	Inner Join CatPasillos_Estantes_Entrepaños C (NoLock)
		On ( F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia And F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño)
	Where F.Status = 'A'	
		and F.ClaveLote like '%*%' 
		and datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) >= @MesesCaducidad_Consigna 
		and P.ClaveSSA in ( select ClaveSSA from #tmpPedido  )
		

	Select DISTINCT  
		identity(int, 1, 1) as Keyx, 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, @FolioSurtido as FolioSurtido, 
		D.ClaveSSA, D.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.FechaCaducidad, D.MesesCaducar, 
		D.EsConsignacion, D.EsDePickeo, D.IdPasillo, D.IdEstante, D.IdEntrepaño, 
		D.Existencia, 0 as CantidadAsignada, P.CantidadRequerida, 0 as Procesado   
	into #tmpDisponible 	
	From #FarmaciaProductos D (NoLock) 
	Inner Join #tmpPedido P On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.ClaveSSA = P.ClaveSSA ) 
    Where 
		-- D.MesesCaducar >= @MesesCaducidad and 
		Existencia > 0 
    Order By D.EsConsignacion, D.EsDePickeo, D.MesesCaducar, D.Existencia  


	--select * from #tmpDisponible 
	-------------------------------------------------------------- OBTENER LOS LOTES Y UBICACIONES DE ACUERDO A LAS CADUCIDADES   
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 


	------------------ Quitar Posiciones excluidas de surtimiento 
	Delete #tmpDisponible   
	From #tmpDisponible D (NoLock) 
	Inner Join Pedidos__Ubicaciones_Excluidas_Surtimiento P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and 
			 D.IdPasillo = P.IdPasillo and D.IdEstante = P.IdEstante and P.IdEntrepaño = D.IdEntrepaño and P.Excluida = 1 ) 
	------------------ Quitar Posiciones excluidas de surtimiento 
				
			 
	------------------ Quitar Posiciones que se estan reprocesando 
	Delete #tmpDisponible   
	From #tmpDisponible D (NoLock) 
	Inner Join #tmpRedistribucion P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.IdSubFarmacia = P.IdSubFarmacia and 
			 D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and D.ClaveLote = P.ClaveLote and 	
			 D.IdPasillo = P.IdPasillo and D.IdEstante = P.IdEstante and P.IdEntrepaño = D.IdEntrepaño ) 			 
	------------------ Quitar Posiciones que se estan reprocesando 			


	------------------ Quitar Tipo de Inventario NO requerido 
	If @TipoDeInventario > 0 
	Begin 

		-- Select * from #tmpDisponible 

		If @TipoDeInventario = 1 
		Begin 
			Delete From #tmpDisponible Where EsConsignacion = 0 
		End 

		If @TipoDeInventario = 2 
		Begin 
			Delete From #tmpDisponible Where EsConsignacion = 1  
		End 

		-- Select * from #tmpDisponible___Final 

	End 
	else 
	Begin 
		Set @TipoDeInventario = 0 
		-- Select 'XXXX', * from #tmpDisponible 
	End 
	------------------ Quitar Tipo de Inventario NO requerido 			

					 
---------------------------------- INICIA PROCESO DE DISTRIBUCION 
Declare 	
	@Keyx int, 
	@KeyxDetalle int, 	
	@sClaveSSA varchar(20), 
	@iCantidadAcumulado int, 	
	@iCantidadAcumulado_Recepcion int, 		
	@iCantidadAcumulado_Aux int, 			
	@iCantidad int, 
	@IdMes int,  
	@iFin int
	 
Declare 
	@sFacturas varchar(max), 
	@sFolioFactura varchar(100), 
	@iFacturas int   	 
	 
	Set @iFin = 0 
	Set @Keyx = 0 
	Set @KeyxDetalle = 0 
	Set @IdMes = 0 
	Set @sFacturas = '' 
	Set @sFolioFactura = '' 
	Set @iFacturas = 0 
	Set @sClaveSSA = '' 
	Set @iCantidad = 0 
	Set @iCantidadAcumulado = 0  
	Set @iCantidadAcumulado_Recepcion = 0 
	Set @iCantidadAcumulado_Aux = 0 
	
	----Select * From #tmpPedido 
	----Select * From #tmpDisponible 		
	
    Declare #ClavesRequeridas   
    Cursor For 
    Select Keyx, ClaveSSA, CantidadRequerida  
    From #tmpPedido 
    -- Where ClaveSSA = '060.004.0109'  
    order by Keyx  
    Open #ClavesRequeridas 
    FETCH NEXT FROM #ClavesRequeridas  Into @Keyx, @sClaveSSA, @iCantidadAcumulado     
        WHILE @@FETCH_STATUS = 0
        BEGIN 
			Set @iCantidad = 0 
			-- Set @iCantidadAcumulado = 0 
			Set @iCantidadAcumulado_Recepcion = 0 
			Set @iCantidadAcumulado_Aux = 0 
			set @iFin = 0 
			Print @sClaveSSA + '  '  + cast(@iCantidadAcumulado as varchar) 

-------------------------------------------------------------------- 
			Declare #Disponibilidad   
			Cursor For 
			Select Keyx, Existencia as Cantidad  
			From #tmpDisponible 
			Where ClaveSSA = @sClaveSSA  
			Order By Keyx  
			Open #Disponibilidad 
			FETCH NEXT FROM #Disponibilidad  Into @KeyxDetalle, @iCantidad     
				WHILE @@FETCH_STATUS = 0 and @iFin = 0 
				BEGIN 
					--print @iCantidad 
					Set @iCantidadAcumulado_Recepcion = ( @iCantidadAcumulado_Recepcion + @iCantidad ) 			
					
					If ( @iCantidadAcumulado_Recepcion <= @iCantidadAcumulado ) 
						Begin 
							Set @iCantidadAcumulado_Aux = @iCantidadAcumulado_Aux + @iCantidad 	
						End 
					Else
						Begin 
							Set @iCantidad = @iCantidadAcumulado - @iCantidadAcumulado_Aux 
							Set @iFin = 1 
						End     
			
					--- Print cast(@iCantidadAcumulado_Aux as varchar) + '   ' + cast(@iCantidadAcumulado_Recepcion as varchar) + '   ' + cast(@iCantidadAcumulado as varchar) 			
				
				
					Update C Set Procesado = 1, CantidadAsignada = IsNull(@iCantidad, 0)   
					From #tmpDisponible C (NoLock) 
					Where Keyx = @KeyxDetalle  
					
					-- Set @iCantidadAcumulado = ( @iCantidadAcumulado - @iCantidad ) 
					
				   FETCH NEXT FROM #Disponibilidad  Into  @KeyxDetalle, @iCantidad     
				END
			Close #Disponibilidad 
			Deallocate #Disponibilidad 
-------------------------------------------------------------------- 

		
			Update C Set Procesado = 1 --, Consumo_Dist =  @iCantidadAcumulado 
			From #tmpPedido C (NoLock) 
			Where Keyx = @Keyx 	


           FETCH NEXT FROM #ClavesRequeridas  Into  @Keyx, @sClaveSSA, @iCantidadAcumulado      
        END
    Close #ClavesRequeridas 
    Deallocate #ClavesRequeridas 
---------------------------------- TERMINA PROCESO DE DISTRIBUCION 
	
	-- select * from #tmpDisponible ---AQUI 	
	
-------------
	--Select * From #tmpPedido 
	--Select * From #tmpDisponible 	

 
---------------- Ajustar la existencia disponible 
--	Update E Set ExistenciaSurtidos = ( E.ExistenciaSurtidos - D.CantidadAsignada ) 
--	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) 
--	Inner Join #tmpDisponible D (NoLock) 
--		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdSubFarmacia = D.IdSubFarmacia 
--			 and E.IdProducto = D.IdProducto and E.ClaveLote = D.ClaveLote 
--			 and E.IdPasillo = D.IdPasillo and E.IdEstante = D.IdEstante and E.IdEntrepaño = D.IdEntrepaño ) 		
---------------- Ajustar la existencia disponible 
	
	
---------------------------------------------------- ACTUALIZAR LOS DATOS DEL SURTIDO			
-------- Incrementar las cantidades en caso de utilizar algun lote ya utilizada		
	Update D Set CantidadRequerida = ( D.CantidadRequerida + E.CantidadAsignada ) 
	From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) 
	Inner Join #tmpDisponible E 
	On ( 
		E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdSubFarmacia = D.IdSubFarmacia 
		and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote 
		and E.IdPasillo = D.IdPasillo and E.IdEstante = D.IdEstante and E.IdEntrepaño = D.IdEntrepaño And D.FolioSurtido = @FolioSurtido)  
-------- Incrementar las cantidades en caso de utilizar algun lote ya utilizada		


-------- Ajustar las cantidades que no se reprocesaron	
	Update D Set CantidadRequerida = E.CantidadAsignada 
	From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) 
	Inner Join #tmpRedistribucion E 
	On ( 
		E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdSubFarmacia = D.IdSubFarmacia 
		and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote 
		and E.IdPasillo = D.IdPasillo and E.IdEstante = D.IdEstante and E.IdEntrepaño = D.IdEntrepaño And D.FolioSurtido = @FolioSurtido and E.Procesar = 1 )  
-------- Ajustar las cantidades que no se reprocesaron	



	------Select IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, @IdSurtimiento, ClaveSSA, 
	------	IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, IdPasillo, IdEstante, IdEntrepaño, CantidadAsignada as Cantidad, '', 'A', 0 
	------From #tmpDisponible E 
	------Where CantidadAsignada > 0 
	------	  and Not Exists 
	------	  ( 
	------			Select * 
	------			From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) 
	------			Where 
	------				E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia 
	------				and E.FolioSurtido = D.FolioSurtido 
	------				and E.IdSubFarmacia = D.IdSubFarmacia 
	------				and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote 
	------				and E.IdPasillo = D.IdPasillo and E.IdEstante = D.IdEstante and E.IdEntrepaño = D.IdEntrepaño
	------	  ) 
		  


-------- Insertar la distribucion generada 	 
	Insert Into Pedidos_Cedis_Det_Surtido_Distribucion 
	( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, ClaveSSA, 
	  IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, IdPasillo, IdEstante, IdEntrepaño, CantidadRequerida, 
	  Observaciones, Status, Actualizado )
	Select IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, @IdSurtimiento, ClaveSSA, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, IdPasillo, IdEstante, IdEntrepaño, CantidadAsignada as Cantidad, '', 'A', 0 
	From #tmpDisponible E 
	Where CantidadAsignada > 0 
		  and Not Exists 
		  ( 
				Select * 
				From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) 
				Where 
					E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia 
					and E.FolioSurtido = D.FolioSurtido 
					and E.IdSubFarmacia = D.IdSubFarmacia 
					and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote 
					and E.IdPasillo = D.IdPasillo and E.IdEstante = D.IdEstante and E.IdEntrepaño = D.IdEntrepaño
		  ) 
-------- Insertar la distribucion generada 	 	
---------------------------------------------------- ACTUALIZAR LOS DATOS DEL SURTIDO			


---------------------------------------------------- GENERAR EL LOG DE RESURTIDOS		
	Insert Into Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso ( 	
		IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, FechaRegistro, IdPersonal, 
		ClaveSSA, IdSubFarmacia, 
		IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, CantidadRequerida, CantidadAsignada, Keyx ) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, getdate() as FechaRegistro, @IdPersonal as IdPersonal, 
		ClaveSSA, IdSubFarmacia, 
		IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, CantidadRequerida, CantidadAsignada, Keyx 
	From #tmpRedistribucion  
	Where Procesar = 1 

	----select * 
	----From #tmpRedistribucion  
	----Where Procesar = 1 
---------------------------------------------------- GENERAR EL LOG DE RESURTIDOS		

-------- Se Aplican las existencias de surtido
	Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioSurtido = @FolioSurtido, @TipoFactor = 1, @Validar = 0


------------ Se marca el surtido como habilitado para surtimiento 
----	Update Pedidos_Cedis_Enc_Surtido Set Status = 'A' 
----	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido
------------ Se marca el surtido como habilitado para surtimiento   


--		spp_ALM_GenerarReDistribucion__Surtimiento 	
	
	
End 
Go--#SQL 

