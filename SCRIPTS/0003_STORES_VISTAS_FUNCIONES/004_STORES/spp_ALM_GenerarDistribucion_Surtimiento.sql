---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_ALM_GenerarDistribucion_Surtimiento' and xType = 'P' ) 
   Drop Proc spp_ALM_GenerarDistribucion_Surtimiento 
Go--#SQL 

Create Proc spp_ALM_GenerarDistribucion_Surtimiento  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', @FolioSurtido varchar(8) = '00000001', 
	@MesesCaducidad int = 1, 
	@MesesCaducidad_Consigna int = 1, 
	@Manual Bit = 0, @TipoUbicacion int = 0, @TipoDeInventario int = 0, @IdGrupo Varchar(3) = ''
) 
With Encryption  
As 
Begin 
Set NoCount On 
Declare  
	@IdSurtimiento int, 
	@iEsPicking int, 
	@iEsAlmacenaje int  	 
	
	
	Set @iEsPicking = 1 
	Set @iEsAlmacenaje = 0  
	
	If ( @TipoUbicacion = 1 ) 
	Begin 
		Set @iEsPicking = 1 
		Set @iEsAlmacenaje = 1  	
	End 
	
	If ( @TipoUbicacion = 2 ) 
	Begin 
		Set @iEsPicking = 0 
		Set @iEsAlmacenaje = 0    	
	End


---------------------------------------- Validar el tipo de Ubicaciones a considerar 
	Select * 
	Into #tmp_Ubicaciones_Trabajo 
	From CatPasillos_Estantes_Entrepaños P (NoLock) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia
		and EsDePickeo in ( @iEsPicking, @iEsAlmacenaje )   
		
	----Select EsDePickeo, count(*) as Registros 
	----From #tmp_Ubicaciones_Trabajo 
	----Group by EsDePickeo 
		
---------------------------------------- Validar el tipo de Ubicaciones a considerar 


	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
	-------------------------------------------------------------- OBTENER LOS LOTES Y UBICACIONES DE ACUERDO A LAS CADUCIDADES   

	----------------------------- GENERAR TABLA BASE 
	Select  
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, F.EsConsignacion, F.IdPasillo, F.IdEstante, F.IdEntrepaño, C.EsDePickeo,
		(F.Existencia - (F.ExistenciaEnTransito + F.ExistenciaSurtidos)) As Existencia,
		IdClaveSSA_Sal, ClaveSSA, L.FechaCaducidad, datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar
	Into #FarmaciaProductos
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Inner join FarmaciaProductos_CodigoEAN_Lotes L (NoLock)
		On (F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia
			 And F.IdSubFarmacia = L.IdSubFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN And F.ClaveLote = L.ClaveLote)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN)
	Inner Join #tmp_Ubicaciones_Trabajo C (NoLock)
		On ( F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia And F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño)   
	Where F.Status = 'A' 
		and 1 = 0 

	
	----------------------------- AGREGAR VENTA 
	Insert Into #FarmaciaProductos
	Select  
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, F.EsConsignacion, F.IdPasillo, F.IdEstante, F.IdEntrepaño, C.EsDePickeo,
		(F.Existencia - (F.ExistenciaEnTransito + F.ExistenciaSurtidos)) As Existencia,
		IdClaveSSA_Sal, ClaveSSA, L.FechaCaducidad, datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar	
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Inner join FarmaciaProductos_CodigoEAN_Lotes L (NoLock)
		On (F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia
			 And F.IdSubFarmacia = L.IdSubFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN And F.ClaveLote = L.ClaveLote)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN)
	Inner Join #tmp_Ubicaciones_Trabajo C (NoLock)
		On ( F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia And F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño)   
	Where F.Status = 'A' 
		and F.ClaveLote not like '%*%' 
		and datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) >= @MesesCaducidad 


	----------------------------- AGREGAR CONSIGNA  
	Insert Into #FarmaciaProductos
	Select  
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, F.EsConsignacion, F.IdPasillo, F.IdEstante, F.IdEntrepaño, C.EsDePickeo,
		(F.Existencia - (F.ExistenciaEnTransito + F.ExistenciaSurtidos)) As Existencia,
		IdClaveSSA_Sal, ClaveSSA, L.FechaCaducidad, datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar	
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Inner join FarmaciaProductos_CodigoEAN_Lotes L (NoLock)
		On (F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia
			 And F.IdSubFarmacia = L.IdSubFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN And F.ClaveLote = L.ClaveLote)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN)
	Inner Join #tmp_Ubicaciones_Trabajo C (NoLock)
		On ( F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia And F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño)   
	Where F.Status = 'A' 
		and F.ClaveLote like '%*%' 
		and datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) >= @MesesCaducidad_Consigna 

	-------------------------------------------------------------- OBTENER LOS LOTES Y UBICACIONES DE ACUERDO A LAS CADUCIDADES   
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
		
		


	
------------------------------------------------------------------------------------------------------------------------------------	
	Select @IdSurtimiento = cast(IdSurtimiento as int) + 1 
	From Pedidos_Cedis_Det_Surtido_Distribucion P 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioSurtido = @FolioSurtido 	
	Set @IdSurtimiento = IsNull(@IdSurtimiento, 1) 

	
	Select 
		identity(int, 1, 1) as Keyx, 
		IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, CantidadAsignada as CantidadRequerida, 0 as Procesado  
	Into #tmpPedido 
	From Pedidos_Cedis_Det_Surtido P (NoLock) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioSurtido = @FolioSurtido 
		  and CantidadAsignada > 0 
	      -- and P.ClaveSSA = '475'  --  475
	      
	Select 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia,
		p.ClaveSSA, D.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.FechaCaducidad,
		DateDiff(MM,GETDATE(), IsNull(D.FechaCaducidad, cast('2000-01-01' as datetime))) As MesesCaducar,
		D.EsConsignacion, PE.EsDePickeo, U.IdPasillo, U.IdEstante, U.IdEntrepaño, U.Existencia
	Into #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
	From FarmaciaProductos_CodigoEAN_Lotes D (NoLock)
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
		On (D.IdEmpresa = U.IdEmpresa And D.IdEstado = U.IdEstado And D.IdFarmacia = U.IdFarmacia And
			D.IdSubFarmacia = U.IdSubFarmacia And D.CodigoEAN = U.CodigoEAN And D.ClaveLote = U.ClaveLote)
	Inner Join CatPasillos_Estantes_Entrepaños PE(NoLock)
		On ( U.IdEmpresa = PE.IdEmpresa And U.IdEstado = PE.IdEstado And U.IdFarmacia = PE.IdFarmacia 
			And U.IdPasillo = PE.IdPasillo And U.IdEstante = PE.IdEstante And U.IdEntrepaño = PE.IdEntrepaño )
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN ) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
		and 1 = 0  


 	Select Top 0  
		0 as Keyx, 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, @FolioSurtido as FolioSurtido, 
		D.ClaveSSA, D.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.FechaCaducidad, D.MesesCaducar, 
		D.EsConsignacion, D.EsDePickeo, D.IdPasillo, D.IdEstante, D.IdEntrepaño, 
		D.Existencia, 0 as CantidadAsignada, 0 as CantidadRequerida, 0 as Procesado   
	into #tmpDisponible 	
	From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones D (NoLock) 


	Insert into #tmpDisponible
	Select DISTINCT  
		--identity(int, 1, 1) as Keyx,
		ROW_NUMBER ( ) OVER ( order by D.EsConsignacion, D.EsDePickeo, D.MesesCaducar, D.Existencia ) as Keyx, 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, @FolioSurtido as FolioSurtido,
		D.ClaveSSA, D.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.FechaCaducidad, D.MesesCaducar,
		D.EsConsignacion, D.EsDePickeo, D.IdPasillo, D.IdEstante, D.IdEntrepaño,
		D.Existencia, 0 as CantidadAsignada, P.CantidadRequerida, 0 as Procesado
	--into #tmpDisponible
	From #FarmaciaProductos D (NoLock)
	Inner Join #tmpPedido P 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.ClaveSSA = P.ClaveSSA )  
	Inner Join #tmp_Ubicaciones_Trabajo U (NoLock) 
		On ( D.IdEmpresa = U.IdEmpresa and D.IdEstado = U.IdEstado and D.IdFarmacia = U.IdFarmacia and  
			D.IdPasillo = U.IdPasillo and D.IdEstante = U.IdEstante and D.IdEntrepaño = U.IdEntrepaño ) 	
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
			-- and D.MesesCaducar >= @MesesCaducidad 
			and Existencia > 0 
	Order By D.EsConsignacion desc, D.EsDePickeo, D.MesesCaducar, D.Existencia 
------------------------------------------------------------------------------------------------------------------------------------	
	      

	--IF OBJECT_ID(N'tempdb..#tmpDisponible', N'U') IS NOT NULL 
	--DROP TABLE #tmpDisponible
 	
	------If ( @Manual = 0 ) 
	------	Begin
	------		Insert into #tmpDisponible
	------		Select DISTINCT  
	------			--identity(int, 1, 1) as Keyx,
	------			ROW_NUMBER ( ) OVER ( order by D.EsConsignacion, D.EsDePickeo, D.MesesCaducar, D.Existencia ) as Keyx,
	------			D.IdEmpresa, D.IdEstado, D.IdFarmacia, @FolioSurtido as FolioSurtido,
	------			D.ClaveSSA, D.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.FechaCaducidad, D.MesesCaducar,
	------			D.EsConsignacion, D.EsDePickeo, D.IdPasillo, D.IdEstante, D.IdEntrepaño,
	------			D.Existencia, 0 as CantidadAsignada, P.CantidadRequerida, 0 as Procesado
	------		--into #tmpDisponible
	------		From #FarmaciaProductos D (NoLock)
	------		Inner Join #tmpPedido P 
	------			On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.ClaveSSA = P.ClaveSSA )  
	------		Inner Join #tmp_Ubicaciones_Trabajo U (NoLock) 
	------			On ( D.IdEmpresa = U.IdEmpresa and D.IdEstado = U.IdEstado and D.IdFarmacia = U.IdFarmacia and  
	------				D.IdPasillo = U.IdPasillo and D.IdEstante = U.IdEstante and D.IdEntrepaño = U.IdEntrepaño ) 	
	------		Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
	------			  -- and D.MesesCaducar >= @MesesCaducidad 
	------			  and Existencia > 0 
	------		Order By D.EsConsignacion desc, D.EsDePickeo, D.MesesCaducar, D.Existencia 

	------	End
	------Else
	------	Begin
	------		Insert into #tmpDisponible 
	------		Select DISTINCT 
	------			--'identity(int, 1, 1) as Keyx, ' +
	------			ROW_NUMBER ( ) OVER ( order by D.EsConsignacion, D.EsDePickeo, D.MesesCaducar, D.Existencia ) as Keyx,
	------			D.IdEmpresa, D.IdEstado, D.IdFarmacia, @FolioSurtido as FolioSurtido,
	------			D.ClaveSSA, D.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.FechaCaducidad, D.MesesCaducar,
	------			D.EsConsignacion, D.EsDePickeo, D.IdPasillo, D.IdEstante, D.IdEntrepaño,
	------			D.Existencia, 0 as CantidadAsignada, P.CantidadRequerida, 0 as Procesado
	------		--into #tmpDisponible  
	------		From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones D (NoLock)
	------		Inner Join #tmpPedido P 
	------			On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.ClaveSSA = P.ClaveSSA ) 
	------		Inner Join #tmp_Ubicaciones_Trabajo U (NoLock) 
	------			On ( D.IdEmpresa = U.IdEmpresa and D.IdEstado = U.IdEstado and D.IdFarmacia = U.IdFarmacia and 
	------				D.IdPasillo = U.IdPasillo and D.IdEstante = U.IdEstante and D.IdEntrepaño = U.IdEntrepaño ) 					
	------		Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia  
	------			  --and D.MesesCaducar >= @MesesCaducidad 
	------			  and Existencia > 0  
	------		Order By D.EsConsignacion desc, D.EsDePickeo, D.MesesCaducar, D.Existencia  
	------	End   
		
		

	-------------------- Generar tabla de control 
 	Select 
		identity(int, 1, 1) as Keyx, 
		IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, 
		ClaveSSA, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, MesesCaducar, 
		EsConsignacion, EsDePickeo, IdPasillo, IdEstante, IdEntrepaño, 
		Existencia, CantidadAsignada, CantidadRequerida, Procesado   
	into #tmpDisponible___Final 
	From #tmpDisponible 	
	Order By EsConsignacion desc, EsDePickeo, MesesCaducar, Existencia
	-------------------- Generar tabla de control 


		
	 --Exec (@sSql)
	 --Print (@sSql)
	 --Select * From #tmpDisponible
	

	------------------ Quitar Posiciones excluidas de surtimiento
	Delete #tmpDisponible___Final   
	From #tmpDisponible___Final D (NoLock) 
	Inner Join Pedidos__Ubicaciones_Excluidas_Surtimiento P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and 
			 D.IdPasillo = P.IdPasillo and D.IdEstante = P.IdEstante and P.IdEntrepaño = D.IdEntrepaño and P.Excluida = 1 ) 
	------ Select * From #tmpDisponible  


	------------------ Quitar Posiciones excluidas de surtimiento 
	Delete #tmpDisponible___Final   
	From #tmpDisponible___Final D (NoLock) 
	Inner Join CFG_ALMN_Ubicaciones_Estandar P (NoLock) 
		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and 
			 D.IdPasillo = P.IdRack and D.IdEstante = P.IdNivel and P.IdEntrepaño = D.IdEntrepaño) 
	------------------ Quitar Posiciones excluidas de surtimiento


	Select C.*
	Into #CFGC_ALMN__GruposDeUbicaciones_Det
	From CFGC_ALMN__GruposDeUbicaciones_Det C (NoLock)
	Inner Join CFGC_ALMN__GruposDeUbicaciones E (NoLock)
		On ( C.IdEmpresa = E.IdEmpresa And C.IdEstado = E.IdEstado And C.IdFarmacia = E.IdFarmacia And C.IdGrupo = E.IdGrupo )
	Where C.status = 'A' And E.Status = 'A'



	if (@IdGrupo <> '')
	Begin
		if (@IdGrupo = '000')
			Begin
				Delete  F
				From #tmpDisponible___Final F
				inner join #CFGC_ALMN__GruposDeUbicaciones_Det C (NoLock)
					On ( 
							F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado And F.IdFarmacia = C.IdFarmacia And
							F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño And 
							C.IdGrupo <> @IdGrupo And C.Status = 'A'
					   )
			End
		Else
			Begin
				Delete  F
				From #tmpDisponible___Final F
				Left Join #CFGC_ALMN__GruposDeUbicaciones_Det C (NoLock)
					On (
							F.IdEmpresa = C.IdEmpresa And F.IdEstado = C.IdEstado And F.IdFarmacia = C.IdFarmacia And
							F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño And 
							IdGrupo = @IdGrupo And Status = 'A'
					   )
				Where C.IdEmpresa Is NUll
			End
		
	End


	------------------ Quitar Tipo de Inventario NO requerido 
	If @TipoDeInventario > 0 
	Begin 

		-- Select * from #tmpDisponible___Final 

		If @TipoDeInventario = 1 
		Begin 
			Delete From #tmpDisponible___Final Where EsConsignacion = 0 
		End 

		If @TipoDeInventario = 2 
		Begin 
			Delete From #tmpDisponible___Final Where EsConsignacion = 1  
		End 

		-- Select * from #tmpDisponible___Final 

	End 
	else 
	Begin 
		Set @TipoDeInventario = 0 
		-- Select 'XXXX', * from #tmpDisponible___Final 
	End 
	------------------ Quitar Tipo de Inventario NO requerido 	



---------------------------------------------------------------------------------------------------------------------------- 
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
	
--	Select * From #tmpPedido 
--	Select * From #tmpDisponible

	
	Declare #ClavesRequeridas   
	Cursor For 
	Select Keyx, ClaveSSA, CantidadRequerida  
	From #tmpPedido 
	-- Where ClaveSSA = '060.004.0109'  
	Where @Manual = 0 
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
			From #tmpDisponible___Final 
			Where ClaveSSA = @sClaveSSA  
			Order By Keyx  
			--Order By EsConsignacion desc, EsDePickeo, MesesCaducar, Existencia 
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
					From #tmpDisponible___Final C (NoLock) 
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
---------------------------------------------------------------------------------------------------------------------------- 
	
	
-------------
--	Select * From #tmpPedido 
--	Select * From #tmpDisponible 	


	---------------- Preparar las tablas para ajustar la existencia de surtidos  
	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, sum(CantidadAsignada) as CantidadAsignada 
	into #tmpDisponible__01__Producto 
	From #tmpDisponible 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto 


	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, sum(CantidadAsignada) as CantidadAsignada 
	into #tmpDisponible__02__CodigoEAN 
	From #tmpDisponible 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN  


	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(CantidadAsignada) as CantidadAsignada 
	into #tmpDisponible__03__Lotes  
	From #tmpDisponible 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 
	---------------- Preparar las tablas para ajustar la existencia de surtidos  



---------------- Ajustar la existencia disponible
	If (@Manual = 0)
		Begin 

			Update E Set ExistenciaSurtidos = (E.ExistenciaSurtidos + D.CantidadAsignada) 
			From FarmaciaProductos E (NoLock) 
			Inner Join #tmpDisponible__01__Producto D (NoLock) 
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdProducto = D.IdProducto )  


			Update E Set ExistenciaSurtidos = (E.ExistenciaSurtidos + D.CantidadAsignada) 
			From FarmaciaProductos_CodigoEAN E (NoLock) 
			Inner Join #tmpDisponible__02__CodigoEAN D (NoLock) 
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdProducto = D.IdProducto )  


			Update E Set ExistenciaSurtidos = (E.ExistenciaSurtidos + D.CantidadAsignada) 
			From FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
			Inner Join #tmpDisponible__03__Lotes D (NoLock) 
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdSubFarmacia = D.IdSubFarmacia 
					 and E.IdProducto = D.IdProducto and E.ClaveLote = D.ClaveLote )  


			Update E Set ExistenciaSurtidos = (E.ExistenciaSurtidos + D.CantidadAsignada) 
			From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) 
			Inner Join #tmpDisponible D (NoLock) 
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdSubFarmacia = D.IdSubFarmacia 
					 and E.IdProducto = D.IdProducto and E.ClaveLote = D.ClaveLote 
					 and E.IdPasillo = D.IdPasillo and E.IdEstante = D.IdEstante and E.IdEntrepaño = D.IdEntrepaño )  


		End 
---------------- Ajustar la existencia disponible 
	
	
	
-------- Insertar la distribucion generada 
	If (@Manual = 0)
		Begin 
			Insert Into Pedidos_Cedis_Det_Surtido_Distribucion 
			( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, ClaveSSA, 
			  IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, IdPasillo, IdEstante, IdEntrepaño, CantidadRequerida, 
			  Observaciones, Status, Actualizado )
			Select IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, @IdSurtimiento, ClaveSSA, 
				IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, IdPasillo, IdEstante, IdEntrepaño, CantidadAsignada as Cantidad, '', 'A', 0 
			From #tmpDisponible___Final 
			Where CantidadAsignada > 0 
		End
	Else  
		Begin
			Insert Into Pedidos_Cedis_Det_Surtido_Distribucion 
			( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, ClaveSSA, 
			  IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, IdPasillo, IdEstante, IdEntrepaño, CantidadRequerida, 
			  Observaciones, Status, Actualizado )
			Select IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, @IdSurtimiento, ClaveSSA, 
				IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, IdPasillo, IdEstante, IdEntrepaño, 0 as CantidadRequerida,
				'', 'A', 0 
			From #tmpDisponible___Final 
		End
	
	
	---- Select * From #tmpDisponible 
	---- Select * From Pedidos_Cedis_Det_Surtido_Distribucion 
-------- Insertar la distribucion generada 	 	

-------- Se marca el surtido como habilitado para surtimiento 
	Update Pedidos_Cedis_Enc_Surtido Set Status = 'A' 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido


	-------- Regresar un registro para determinar si se genero distribución.
	Select top 1 * 
	From #tmpDisponible___Final 


-------- Se marca el surtido como habilitado para surtimiento  


--		spp_ALM_GenerarDistribucion_Surtimiento 	
	
	
End 
Go--#SQL 

