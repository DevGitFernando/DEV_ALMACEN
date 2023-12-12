------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasEnvioGenerar__Convertir_A_Caja' and xType = 'P' )  
   Drop Proc spp_Mtto_TransferenciasEnvioGenerar__Convertir_A_Caja  
Go--#SQL 

Create Proc spp_Mtto_TransferenciasEnvioGenerar__Convertir_A_Caja 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', @FolioTransferencia varchar(20) = '250001SLTS00000002'
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
	
	Set @sMensaje = 'Ocurrió un error al validar las existencias'   
	Set @sLista = '' 
	Set @sCodigo = '' 
	Set @iRegistros = 0 
	Select @EsAlmacen = EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

------------------------------------------------------------------------------------------------------------------------------------------------- 


	Select
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.FolioTransferencia,
		T.IdProducto, T.CodigoEAN, T.ClaveLote, T.IdPasillo, T.IdEstante, T.IdEntrepaño, T.CantidadEnviada,
		Cast((T.CantidadEnviada / R.ContenidoPiezasUnitario) As Int) As CantidadCajaInt, (T.CantidadEnviada * 1.000 / R.ContenidoPiezasUnitario) As CantidadCajaDouble,
		0 As Error
	Into #Ubicaciones 
	From TransferenciasDet_Lotes_Ubicaciones T (NoLock)
	Inner Join CatProductos_CodigosRelacionados R (NoLock) On (T.CodigoEAN = R.CodigoEAN)
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia
	
	Select
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.FolioTransferencia,
		T.IdProducto, T.CodigoEAN, T.ClaveLote, T.CantidadEnviada,
		Cast((T.CantidadEnviada / R.ContenidoPiezasUnitario) As Int) As CantidadCajaInt, (T.CantidadEnviada * 1.000 / R.ContenidoPiezasUnitario) As CantidadCajaDouble,
		0 As Error
	Into #Lotes
	From TransferenciasDet_Lotes T (NoLock)
	Inner Join CatProductos_CodigosRelacionados R (NoLock) On (T.CodigoEAN = R.CodigoEAN)
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia
	
		
	Select
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.FolioTransferencia,
		T.IdProducto, T.CodigoEAN, T.CantidadEnviada,
		Cast((T.CantidadEnviada / R.ContenidoPiezasUnitario) As Int) As CantidadCajaInt, (T.CantidadEnviada * 1.000 / R.ContenidoPiezasUnitario) As CantidadCajaDouble
	Into #Det
	From TransferenciasDet T (NoLock)
	Inner Join CatProductos_CodigosRelacionados R (NoLock) On (T.CodigoEAN = R.CodigoEAN)
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia

	Update E Set E.Cant_Enviada = T.CantidadCajaInt, E.CantidadEnviada = T.CantidadCajaInt
	From TransferenciasEnvioDet_Lotes E (NoLock)
	Inner JOin #Lotes T (NoLock)
		On (T.IdEmpresa = E.IdEmpresa And T.IdEstado = E.IdEstadoEnvia And T.IdFarmacia = E.IdFarmaciaEnvia And T.FolioTransferencia = E.FolioTransferencia And 
			T.CodigoEAN = E.CodigoEAN And T.ClaveLote = E.ClaveLote )

	Update E Set E.Cant_Enviada = T.CantidadCajaInt, E.CantidadEnviada = T.CantidadCajaInt
	From TransferenciasEnvioDet E (NoLock)
	Inner JOin #Det T (NoLock) 
		On (T.IdEmpresa = E.IdEmpresa And T.IdEstado = E.IdEstadoEnvia And T.IdFarmacia = E.IdFarmaciaEnvia And T.FolioTransferencia = E.FolioTransferencia And T.CodigoEAN = E.CodigoEAN )
	
	Update T Set Error = 1 
	From #Ubicaciones T 
	Where CantidadCajaInt <> CantidadCajaDouble

	Update T Set Error = 1 			
	From #Lotes T 
	Where CantidadCajaInt <> CantidadCajaDouble
	
	If @EsAlmacen = 1 
		Begin 
			Select @iRegistros = count(*) From #Ubicaciones Where Error = 1
		End 
	Else 
		Begin 
			Select @iRegistros = count(*) From #Lotes Where Error = 1 
		End 		
		

	------ Validación final 
	


	if @iRegistros > 0 
	Begin 
		If @EsAlmacen = 1 
			Begin 
				Declare #cursorProductos  
				Cursor For 
					Select Distinct IdProducto 
					From #Ubicaciones 
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
		Else
			Begin 
				Declare #cursorProductos  
				Cursor For 
					Select Distinct IdProducto 
					From #Lotes
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

End    
Go--#SQL


