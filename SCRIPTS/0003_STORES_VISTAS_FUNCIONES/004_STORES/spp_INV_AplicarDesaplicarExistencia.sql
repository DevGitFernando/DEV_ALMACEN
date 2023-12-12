If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INV_AplicarDesaplicarExistencia' and xType = 'P' )
   Drop Proc spp_INV_AplicarDesaplicarExistencia 
Go--#SQL 

Create Proc spp_INV_AplicarDesaplicarExistencia 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FolioMovto varchar(30) = 'II00000001', @Aplica smallint = 1, @AfectarCostos smallint = 1 ) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare @iFactor smallint, 
		@sTipoES varchar(4) 
		
	-- Obtener el tipo de efecto del movimiento 
	Select Top 1 @sTipoES = TipoES 
	From MovtosInv_Enc (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovto 
	
	-- Determinar el tipo de Efecto de existencia 
	Set @iFactor = 1 
	If @sTipoES = 'E' 
	   Set @iFactor = 1 
	Else 
	   Set @iFactor = -1 

	-- En caso de ser una cancelacion 
	If @Aplica = 2 
	   Set @iFactor = @iFactor * - 1		
	
---------------- Obtener los movimientos de inventario 	
	-- Articulos del movimiento 
	-- Totalizar por CodigoInterno-CodigoEAN
	Select M.Keyx, M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.FolioMovtoInv as Folio,
		M.IdProducto, M.CodigoEAN, F.Existencia, (M.Cantidad * @iFactor) as Cantidad, 
		M.Costo as CostoNuevo, E.CostoPromedio as CostoAnterior    
	Into #tmpArticulosEAN 
	From MovtosInv_Det_CodigosEAN M (NoLock) 
	Inner Join FarmaciaProductos E (NoLock) 
		On ( M.IdEmpresa = E.IdEmpresa and M.IdEstado = E.IdEstado and M.IdFarmacia = E.IdFarmacia and M.IdProducto = E.IdProducto ) 			
	Inner Join FarmaciaProductos_CodigoEAN F (NoLock) 
		On ( M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN ) 		
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @FolioMovto 

--	Select * from FarmaciaProductos
--  spp_INV_AplicarDesaplicarExistencia 

	-- Totalizar por CodigoInterno 	
	Select IdEmpresa, IdEstado, IdFarmacia, Folio,IdProducto, max(CostoNuevo) as CostoNuevo, max(CostoAnterior) as CostoAnterior, 
		sum(Existencia) as Existencia, sum(Cantidad) as Cantidad   
	Into #tmpArticulos 
	From #tmpArticulosEAN (NoLock) 
	Group by IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto 

	-- Totalizar por CodigoInterno-CodigoEAN-Lotes 		
	Select M.Keyx, M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.IdSubFarmacia, M.FolioMovtoInv as Folio,
		M.IdProducto, M.CodigoEAN, M.ClaveLote, F.Existencia, (M.Cantidad * @iFactor) as Cantidad, 
		M.Costo as CostoNuevo, F.CostoPromedio as CostoAnterior     
	Into #tmpArticulosEAN_Lotes 
	From MovtosInv_Det_CodigosEAN_Lotes M (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia and M.IdSubFarmacia = F.IdSubFarmacia 
			 and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN and M.ClaveLote = F.ClaveLote ) 			
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @FolioMovto


	-- Totalizar por CodigoInterno-CodigoEAN-Lotes_Ubicaciones 		
	Select M.Keyx, M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.IdSubFarmacia, M.FolioMovtoInv as Folio,
		M.IdProducto, M.CodigoEAN, M.ClaveLote, F.IdPasillo, F.IdEstante, F.IdEntrepaño,
		F.Existencia, (M.Cantidad * @iFactor) as Cantidad   
	Into #tmpArticulosEAN_Lotes_Ubicaciones 
	From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones M (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
		On ( M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia and M.IdSubFarmacia = F.IdSubFarmacia 
			 and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN and M.ClaveLote = F.ClaveLote
			 and M.IdPasillo = F.IdPasillo and M.IdEstante = F.IdEstante and M.IdEntrepaño = F.IdEntrepaño ) 			
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @FolioMovto
 		
---------------- Obtener los movimientos de inventario 	

---------------------------- Actualizar Existencia Negativos 
---------------------------- NO BORRAR Ó MODIFICAR ESTA SECCION DEL STORE. ATTE. JESUS DIAZ 2K110523.0953 
----------------	Update F Set Existencia = F.Existencia + abs( F.Existencia + A.Cantidad )
----------------	From FarmaciaProductos F (NoLock) 
----------------	Inner Join #tmpArticulos A (NoLock) 
----------------		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto ) 
----------------	Where ( F.Existencia + A.Cantidad ) < 0 
----------------
----------------	Update F Set Existencia = F.Existencia + abs( F.Existencia + A.Cantidad )
----------------	From FarmaciaProductos_CodigoEAN F (NoLock) 
----------------	Inner Join #tmpArticulosEAN A (NoLock) 
----------------		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN ) 
----------------	Where ( F.Existencia + A.Cantidad ) < 0 	
----------------	
----------------	Update F Set Existencia = F.Existencia + abs( F.Existencia + A.Cantidad )
----------------	From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
----------------	Inner Join #tmpArticulosEAN_Lotes A (NoLock) 
----------------		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
----------------			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote ) 
----------------	Where ( F.Existencia + A.Cantidad ) < 0 			 
----------------
----------------	Update F Set Existencia = F.Existencia + abs( F.Existencia + A.Cantidad )
----------------	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
----------------	Inner Join #tmpArticulosEAN_Lotes_Ubicaciones A (NoLock) 
----------------		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
----------------			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote 
----------------			 and F.IdPasillo = A.IdPasillo and F.IdEstante = A.IdEstante and F.IdEntrepaño = A.IdEntrepaño ) 
----------------	Where ( F.Existencia + A.Cantidad ) < 0 			 
---------------------------- Actualizar Existencia Negativos 



---------------- Actualizar Existencia 
	Update F Set Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos F (NoLock) 
	Inner Join #tmpArticulos A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto ) 

	Update F Set Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos_CodigoEAN F (NoLock) 
	Inner Join #tmpArticulosEAN A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN ) 
	
	Update F Set Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	Inner Join #tmpArticulosEAN_Lotes A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote )
	
	Update F Set Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
	Inner Join #tmpArticulosEAN_Lotes_Ubicaciones A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote
			 and F.IdPasillo = A.IdPasillo and F.IdEstante = A.IdEstante and F.IdEntrepaño = A.IdEntrepaño ) 
	
	-------------------------------- PRUEBAS 
	--------Select F.*, A.*, Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	--------From FarmaciaProductos F (NoLock) 
	--------Inner Join #tmpArticulos A (NoLock) 
	--------	On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto ) 

	--------Select F.*, A.*, Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	--------From FarmaciaProductos_CodigoEAN F (NoLock) 
	--------Inner Join #tmpArticulosEAN A (NoLock) 
	--------	On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN ) 
	
	--------Select F.*, A.*, Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	--------From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	--------Inner Join #tmpArticulosEAN_Lotes A (NoLock) 
	--------	On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
	--------		 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote )
	
	--------Select F.*, A.*, Existencia = ( F.Existencia + A.Cantidad ), Actualizado = 0 
	--------From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
	--------Inner Join #tmpArticulosEAN_Lotes_Ubicaciones A (NoLock) 
	--------	On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
	--------		 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote
	--------		 and F.IdPasillo = A.IdPasillo and F.IdEstante = A.IdEstante and F.IdEntrepaño = A.IdEntrepaño )	
	-------------------------------- PRUEBAS 
---------------- Actualizar Existencia 




---------------- Actualizar Existencia en tablas de Inventarios 
	Update E Set Existencia = F.Existencia, Actualizado = 0  
	From MovtosInv_Det_CodigosEAN E (NoLock) 
	Inner Join #tmpArticulosEAN A (NoLock) On ( E.Keyx = A.Keyx ) 
	Inner Join FarmaciaProductos_CodigoEAN F (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN ) 
			 
	Update E Set Existencia = F.Existencia, Actualizado = 0  
	From MovtosInv_Det_CodigosEAN_Lotes E (NoLock) 
	Inner Join #tmpArticulosEAN_Lotes A (NoLock) On ( E.Keyx = A.Keyx ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and A.ClaveLote = F.ClaveLote )

	
	Update E Set Existencia = F.Existencia, Actualizado = 0  
	From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones E (NoLock) 
	Inner Join #tmpArticulosEAN_Lotes_Ubicaciones A (NoLock) On ( E.Keyx = A.Keyx ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote
			 and F.IdPasillo = A.IdPasillo and F.IdEstante = A.IdEstante and F.IdEntrepaño = A.IdEntrepaño ) 	
---------------- Actualizar Existencia en tablas de Inventarios 


---------------- Marcar Movimiento en tablas de Inventarios 
	Update M Set MovtoAplicado = 'S', Actualizado = 0 
	From MovtosInv_Enc M (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovto 		
---------------- Marcar Movimiento en tablas de Inventarios 


---------------- Afectar Costo Promedio 
	If @AfectarCostos = 1 
	   Begin 
	        -- Set @AfectarCostos = @AfectarCostos 
			Update F Set UltimoCosto = A.CostoNuevo, 
				 CostoPromedio = 
				 ( 
					case when (A.Existencia + A.Cantidad) <= 0 Then F.CostoPromedio 
						Else Round( ((A.Existencia * A.CostoAnterior) + (A.Cantidad * A.CostoNuevo)) / (A.Existencia + A.Cantidad), 4 ) 
					end  
				 )   
			From FarmaciaProductos F (NoLock) 
			Inner Join #tmpArticulos A (NoLock) 
				On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto ) 	        
	        
	        
	        ----------------- Jesús Díaz 2K130710.1350   
			Update F Set UltimoCosto = A.CostoNuevo, 
				 CostoPromedio = 
				 ( 
					case when (A.Existencia + A.Cantidad) <= 0 Then F.CostoPromedio 
						Else Round( ((A.Existencia * A.CostoAnterior) + (A.Cantidad * A.CostoNuevo)) / (A.Existencia + A.Cantidad), 4 ) 
					end  
				 )   
			From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
			Inner Join #tmpArticulosEAN_Lotes A (NoLock) 
				On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
						and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote )
			
			Update F Set CostoPromedio = F.UltimoCosto 
			From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
			Inner Join #tmpArticulosEAN_Lotes A (NoLock) 
				On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
						and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote )
			Where F.CostoPromedio <= 0 
	        ----------------- Jesús Díaz 2K130710.1350   
	        
	   End 
---------------- Afectar Costo Promedio 


---------------- Revisar por posibles descuadres 
	if @@error = 0 
	Begin 
		Exec spp_INV_AplicarDesaplicarExistencia__Validar @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto
	End 
---------------- Revisar por posibles descuadres 


---------------- Revisar por posibles descuadres transferencias
	if @@error = 0 
	Begin 
		Exec spp_INV_AplicarDesaplicarExistencia__ValidarTrasitosVSTransferencias @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto
	End 
---------------- Revisar por posibles descuadres transferencias





--	Select * 
--	From FarmaciaProductos F (NoLock) 
--
--	Select * from #tmpArticulos 
--	Select * from #tmpArticulosEAN 
--	Select * from #tmpArticulosEAN_Lotes  
	
--		spp_INV_AplicarDesaplicarExistencia
		
End    
Go--#SQL


