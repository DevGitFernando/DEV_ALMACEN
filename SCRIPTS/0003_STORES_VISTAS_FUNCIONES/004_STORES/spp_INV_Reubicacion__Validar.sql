If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INV_Reubicacion__Validar' and xType = 'P' )
   Drop Proc spp_INV_Reubicacion__Validar 
Go--#SQL

Create Proc spp_INV_Reubicacion__Validar 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', 
	@Reubicacion_Salida varchar(30) = 'SPR00000932', @Reubicacion_Entrada varchar(30) = 'EPR00000932'  
) 	
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(max), 
	@sLista varchar(max), 	
	@sCodigo varchar(100), 
	@iRegistros int, 
	@EsAlmacen int  
	
	Set @sMensaje = 'Ocurrió un error al validar las reubicaciones'   
	Set @sLista = '' 
	Set @sCodigo = '' 
	Set @iRegistros = 0 
	Select @EsAlmacen = 1 --- EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and EsAlmacen = 1
	Set @EsAlmacen = IsNull(@EsAlmacen, 0) 


-------------------------------------------------------------------------------------------------------------------------------------------------  	
	-------- Datos a nivel EAN 	
	Select IdProducto, CodigoEAN, cast(Cantidad as int) as Cantidad, 0 as CantidadReferencia, 0 as Error    
	Into #tmp_01_EAN_01 
	From MovtosInv_Det_CodigosEAN M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @Reubicacion_Salida  

	Select IdProducto, CodigoEAN, cast(Cantidad as int) as Cantidad
	Into #tmp_01_EAN_02 
	From MovtosInv_Det_CodigosEAN M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @Reubicacion_Entrada  


	-------- Datos a nivel Lote 	
	Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, cast(Cantidad as int) as Cantidad, 0 as CantidadReferencia, 0 as Error   
	Into #tmp_02_Lotes_01 
	From MovtosInv_Det_CodigosEAN_Lotes M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @Reubicacion_Salida  

	Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, cast(Cantidad as int) as Cantidad
	Into #tmp_02_Lotes_02 
	From MovtosInv_Det_CodigosEAN_Lotes M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @Reubicacion_Entrada  
	

	-------- Datos a nivel Ubicacion 	
	Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, -- IdPasillo, IdEstante, IdEntrepaño, 
		cast(sum(Cantidad) as int) as Cantidad, 0 as CantidadReferencia, 0 as Error   
	Into #tmp_03_Ubicaciones_01 
	From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @Reubicacion_Salida  
	Group by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 

	Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, -- IdPasillo, IdEstante, IdEntrepaño, 
		cast(sum(Cantidad) as int) as Cantidad, 0 as Error 
	Into #tmp_03_Ubicaciones_02 
	From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @Reubicacion_Entrada  
	Group by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 	


-------------------------------------------------------------------------------------------------------------------------------------------------  	
--------- Calcular posibles diferencias 
	Update E Set CantidadReferencia = D.Cantidad 
	From #tmp_01_EAN_01 E 
	Inner Join #tmp_01_EAN_02 D On ( E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 

	Update E Set CantidadReferencia = D.Cantidad 
	From #tmp_02_Lotes_01 E 
	Inner Join #tmp_02_Lotes_02 D 
		On ( E.IdSubFarmacia = D.IdSubFarmacia and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote ) 

	Update E Set CantidadReferencia = D.Cantidad 
	From #tmp_03_Ubicaciones_01 E 
	Inner Join #tmp_03_Ubicaciones_02 D 
		On ( E.IdSubFarmacia = D.IdSubFarmacia and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote )  


	Update E Set Error = 1 
	From #tmp_01_EAN_01 E 	
	Where Cantidad <> CantidadReferencia

	Update E Set Error = 1 
	From #tmp_02_Lotes_01 E 	
	Where Cantidad <> CantidadReferencia
	
	Update E Set Error = 1 
	From #tmp_03_Ubicaciones_01 E 	
	Where Cantidad <> CantidadReferencia	




----------------------------------------------------------------------------- 
	Select CodigoEAN 
	Into #tmpErrores 
	From #tmp_01_EAN_01 E 
	Where Error = 1  

	Insert Into #tmpErrores 	
	Select CodigoEAN 
	From #tmp_02_Lotes_01
	Where Error = 1  
	
	Insert Into #tmpErrores 	
	Select CodigoEAN 
	From #tmp_03_Ubicaciones_01
	Where Error = 1  	
	
	
---		spp_INV_Reubicacion__Validar 


-----------------------------------------------------------------------------------------------------------------------------------------------------
	------ Validación final 
	Select @iRegistros = count(*) From #tmpErrores 


	if @iRegistros > 0 
	Begin 
		
		Declare #cursorProductos  
		Cursor For 
			Select Distinct CodigoEAN 
			From #tmpErrores 
		Open #cursorProductos 
		FETCH NEXT FROM #cursorProductos Into @sCodigo 
			WHILE @@FETCH_STATUS = 0 
			BEGIN 
				
				Set @sLista = @sLista + @sCodigo + '    ' + char(10) -- + char(13) 
				
				FETCH NEXT FROM #cursorProductos Into @sCodigo   
			END	 
		Close #cursorProductos 
		Deallocate #cursorProductos 			
	
		Set @sMensaje = @sMensaje + char(10) + char(13) + @sLista 
		RaisError (@sMensaje, 16, 1 )
		Return 
		
	End 

End    
Go--#SQL


