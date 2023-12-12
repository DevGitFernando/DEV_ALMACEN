If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INV_AplicaDesaplicaExistenciaTransito__Validar' and xType = 'P' )
   Drop Proc spp_INV_AplicaDesaplicaExistenciaTransito__Validar 
Go--#SQL

Create Proc spp_INV_AplicaDesaplicaExistenciaTransito__Validar 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FolioTransferencia varchar(30) = 'II00000001'  
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
	
	Set @sMensaje = 'Ocurrió un error al validar las existencias en transito'   
	Set @sLista = '' 
	Set @sCodigo = '' 
	Set @iRegistros = 0 
	Select @EsAlmacen = EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and EsAlmacen = 1
	Set @EsAlmacen = IsNull(@EsAlmacen, 0) 

-------------------------------------------------------------------------------------------------------------------------------------------------  		
	Select Distinct IdEmpresa, IdEstado, IdFarmacia, IdProducto 
	Into #tmpProductos 
	From MovtosInv_Det_CodigosEAN M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @FolioTransferencia 


	Select 
		E.IdProducto, cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible, 
		0 as Existencia_EAN, 0 as ExistenciaEnTransito_EAN, 0 as ExistenciaDisponible_EAN,  
		0 as Existencia_Lote, 0 as ExistenciaEnTransito_Lote, 0 as ExistenciaDisponible_Lote,  
		0 as Existencia_U, 0 as ExistenciaEnTransito_U, 0 as ExistenciaDisponible_U, 
		0 as Error 
	Into #tmp_Concentrado 
	From FarmaciaProductos E (NoLock) 
	Inner Join #tmpProductos P (NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdProducto = P.IdProducto ) 
	
	Select 
		E.IdProducto, CodigoEAN, cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible 
	Into #tmp_Concentrado_EAN  
	From FarmaciaProductos_CodigoEAN E (NoLock) 
	Inner Join #tmpProductos P (NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdProducto = P.IdProducto ) 
	
		
	Select 
		E.IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible 
	Into #tmp_Concentrado_Lote  
	From FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
	Inner Join #tmpProductos P (NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdProducto = P.IdProducto ) 
	
	
	Select 
		E.IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, 
		IdPasillo, IdEstante, IdEntrepaño, 
		cast(Existencia as int) as Existencia, 
		cast(ExistenciaEnTransito as int) as ExistenciaEnTransito, 
		cast(( Existencia - ExistenciaEnTransito ) as int) as ExistenciaDisponible 
	Into #tmp_Concentrado_U  
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) 
	Inner Join #tmpProductos P (NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdProducto = P.IdProducto ) 
	
	
	
	Update C Set 
		Existencia_EAN = IsNull((select sum(Existencia) From #tmp_Concentrado_EAN D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaEnTransito_EAN = IsNull((select sum(ExistenciaEnTransito) From #tmp_Concentrado_EAN D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaDisponible_EAN = IsNull((select sum(ExistenciaDisponible) From #tmp_Concentrado_EAN D Where D.IdProducto = C.IdProducto ), 0) 		
	From #tmp_Concentrado C 
	
	Update C Set 
		Existencia_Lote = IsNull((select sum(Existencia) From #tmp_Concentrado_Lote D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaEnTransito_Lote = IsNull((select sum(ExistenciaEnTransito) From #tmp_Concentrado_Lote D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaDisponible_Lote = IsNull((select sum(ExistenciaDisponible) From #tmp_Concentrado_Lote D Where D.IdProducto = C.IdProducto ), 0) 		
	From #tmp_Concentrado C 
						
	Update C Set 
		Existencia_U = IsNull((select sum(Existencia) From #tmp_Concentrado_U D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaEnTransito_U = IsNull((select sum(ExistenciaEnTransito) From #tmp_Concentrado_U D Where D.IdProducto = C.IdProducto ), 0),  
		ExistenciaDisponible_U = IsNull((select sum(ExistenciaDisponible) From #tmp_Concentrado_U D Where D.IdProducto = C.IdProducto ), 0)  		
	From #tmp_Concentrado C 	
-------------------------------------------------------------------------------------------------------------------------------------------------  			

	If @EsAlmacen = 1 
		Begin 
			Update T Set Error = 1 
			From #tmp_Concentrado T 
			Where 
				(ExistenciaEnTransito <> ExistenciaEnTransito_EAN) 
					or (ExistenciaEnTransito <> ExistenciaEnTransito_Lote) 		 
					or (ExistenciaEnTransito <> ExistenciaEnTransito_U) 
				or (ExistenciaEnTransito_EAN <> ExistenciaEnTransito_Lote)
					or ( ExistenciaEnTransito_EAN <> ExistenciaEnTransito_U ) 
		End 
	Else 
		Begin 
			Update T Set Error = 1 			
			From #tmp_Concentrado T 
			Where 
				(ExistenciaEnTransito <> ExistenciaEnTransito_EAN) or (ExistenciaEnTransito <> ExistenciaEnTransito_Lote) 
				or (ExistenciaEnTransito_EAN <> ExistenciaEnTransito_Lote) 
		End 		
		

	------ Validación final 
	Select @iRegistros = count(*) From #tmp_Concentrado Where Error = 1 	
		

	if @iRegistros > 0 
	Begin 
		
		Declare #cursorProductos  
		Cursor For 
			Select IdProducto 
			From #tmp_Concentrado 
			Where Error = 1 
		Open #cursorProductos 
		FETCH NEXT FROM #cursorProductos Into @sCodigo 
			WHILE @@FETCH_STATUS = 0 
			BEGIN 
				
				Set @sLista = @sLista + @sCodigo + '    ' + char(10) + char(13) 
				
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


