------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INV_AplicarDesaplicarExistencia__ValidarTrasitosVSTransferencias' and xType = 'P' )  
   Drop Proc spp_INV_AplicarDesaplicarExistencia__ValidarTrasitosVSTransferencias  
Go--#SQL 

Create Proc spp_INV_AplicarDesaplicarExistencia__ValidarTrasitosVSTransferencias
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1042', 
	@FolioMovto varchar(30) = 'TS00000066'  
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
	
	Set @sMensaje = 'Ocurrió un error al validar las existencias en transito VS transferencias no Aplicadas'   
	Set @sLista = '' 
	Set @sCodigo = '' 
	Set @iRegistros = 0 
	Select @EsAlmacen = EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and EsAlmacen = 1
	Set @EsAlmacen = IsNull(@EsAlmacen, 0) 

-------------------------------------------------------------------------------------------------------------------------------------------------  		
	Select Distinct IdEmpresa, IdEstado, IdFarmacia, IdProducto 
	Into #tmpProductos 
	From MovtosInv_Det_CodigosEAN M (NoLock) 
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @FolioMovto


	If ( @EsAlmacen = 1 )
		Begin 
			Select M.IdEmpresa, M.IdEstado, M.IdFarmacia, IdSubFarmacia, M.IdProducto, M.CodigoEAN, M.ClaveLote, M.IdPasillo, M.IdEstante, M.IdEntrepaño, M.ExistenciaEnTransito
			Into #ExistenciaU
			From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones M (NoLock)
			Inner Join #tmpProductos P On (M.IdEmpresa = P.IdEmpresa And M.IdEstado = P.IdEstado And M.IdFarmacia = P.IdFarmacia And M.IdProducto = P.IdProducto)
			Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia


			Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdSubFarmaciaEnvia As IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño, Sum(D.CantidadEnviada) as CantidadEnviada
			Into #TransferenciaU
			From TransferenciasEnc E (NoLock)
			Inner Join TransferenciasDet_Lotes_Ubicaciones D (NoLock)
				On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioTransferencia = D.FolioTransferencia)
			Inner Join #tmpProductos P On (D.IdEmpresa = P.IdEmpresa And D.IdEstado = P.IdEstado And D.IdFarmacia = P.IdFarmacia And D.IdProducto = P.IdProducto)
			Where E.Status = 'A' And TransferenciaAplicada = 0 And TipoTransferencia = 'TS' And E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia
			Group By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdSubFarmaciaEnvia, D.IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño



			Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdSubFarmacia, E.IdProducto, E.CodigoEAN, E.ClaveLote, E.IdPasillo, E.IdEstante, E.IdEntrepaño, E.ExistenciaEnTransito, IsNull(CantidadEnviada, 0.0000) As CantidadEnviada
			Into #tmp_ConcentradoU
			From #ExistenciaU E (NoLock)
			Left Join #TransferenciaU T (NoLock)
				On (E.IdEmpresa = T.IdEmpresa And E.IdEstado = T.IdEstado And E.IdFarmacia = T.IdFarmacia And
					E.IdSubFarmacia = T.IdSubFarmacia And E.IdProducto = T.IdProducto And E.CodigoEAN = T.CodigoEAN And E.ClaveLote = T.ClaveLote And
					E.IdPasillo = T.IdPasillo And E.IdEstante = T.IdEstante And E.IdEntrepaño = T.IdEntrepaño)
			Where ExistenciaEnTransito <> IsNull(CantidadEnviada, 0.0000)


			Select @iRegistros = count(*) From #tmp_ConcentradoU


			if @iRegistros > 0 
			Begin 
		
				Declare #cursorProductos  
				Cursor For 
					Select IdProducto 
					From #tmp_ConcentradoU 
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
	Else
		Begin
			Select M.IdEmpresa, M.IdEstado, M.IdFarmacia, IdSubFarmacia, M.IdProducto, M.CodigoEAN, M.ClaveLote, M.ExistenciaEnTransito
			Into #Existencia
			From FarmaciaProductos_CodigoEAN_Lotes M (NoLock)
			Inner Join #tmpProductos P On (M.IdEmpresa = P.IdEmpresa And M.IdEstado = P.IdEstado And M.IdFarmacia = P.IdFarmacia And M.IdProducto = P.IdProducto)
			Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia


			Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdSubFarmaciaEnvia As IdSubFarmacia, D.IdProducto, CodigoEAN, ClaveLote, Sum(CantidadEnviada) as CantidadEnviada
			Into #Transferencia
			From TransferenciasEnc E (NoLock)
			Inner Join TransferenciasDet_Lotes D (NoLock)
				On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioTransferencia = D.FolioTransferencia)
			Inner Join #tmpProductos P On (D.IdEmpresa = P.IdEmpresa And D.IdEstado = P.IdEstado And D.IdFarmacia = P.IdFarmacia And D.IdProducto = P.IdProducto)
			Where E.Status = 'A' And TransferenciaAplicada = 0 And TipoTransferencia = 'TS' And E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia
			Group By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdSubFarmaciaEnvia, D.IdProducto, CodigoEAN, ClaveLote



			Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdSubFarmacia, E.IdProducto, E.CodigoEAN, E.ClaveLote, E.ExistenciaEnTransito, IsNull(CantidadEnviada, 0.0000) As CantidadEnviada
			Into #tmp_Concentrado
			From #Existencia E (NoLock)
			Left Join #Transferencia T (NoLock)
				On (E.IdEmpresa = T.IdEmpresa And E.IdEstado = T.IdEstado And E.IdFarmacia = T.IdFarmacia And
					E.IdSubFarmacia = T.IdSubFarmacia And E.IdProducto = T.IdProducto And E.CodigoEAN = T.CodigoEAN And E.ClaveLote = T.ClaveLote)
			Where ExistenciaEnTransito <> IsNull(CantidadEnviada, 0.0000)


			Select @iRegistros = count(*) From #tmp_Concentrado


			if @iRegistros > 0 
			Begin 
		
				Declare #cursorProductos  
				Cursor For 
					Select IdProducto 
					From #tmp_Concentrado 
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


End    
Go--#SQL