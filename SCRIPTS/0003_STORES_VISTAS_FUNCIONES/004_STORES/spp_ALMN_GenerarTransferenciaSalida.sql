------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALMN_GenerarTransferenciaSalida' and xType = 'P' ) 
   Drop Proc spp_ALMN_GenerarTransferenciaSalida 
Go--#SQL 

Create Proc spp_ALMN_GenerarTransferenciaSalida  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', @FolioSurtido varchar(8) = '00000001',
	@IdPersonal varchar(4) = '0001', @Observaciones varchar(500) = '', 
	@IdEstadoRecibe varchar(2) = '21', @IdFarmaciaRecibe varchar(4) = '0002'   
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare 
		@FolioMovtoInv varchar(14),
		@FolioTransferencia varchar(18), 
		@IdTipoMovto_Inv varchar(6),
		@TipoTransferencia varchar(2),
		@TipoES varchar(1),
		@sMensaje varchar(1000), 
		@Referencia varchar(30),
		@IdAlmacen varchar(2), 
		@EsTransferenciaAlmacen smallint, 
		@FolioMovtoInvRef varchar(30), 
		@FolioTransferenciaRef varchar(30),
		@DestinoEsAlmacen smallint, 
		@IdAlmacenRecibe varchar(2),
		@SubTotal numeric(14,4), 
		@Iva numeric(14,4), 
		@Total numeric(14,4),
		@sStatus varchar(1), 
		@iActualizado smallint 

	Set @FolioMovtoInv = ''
	Set @FolioTransferencia = ''
	Set @IdTipoMovto_Inv = 'TS'
	Set @TipoTransferencia = 'TS'
	Set @TipoES = 'S' 
	Set @sMensaje = ''
	Set @Referencia = @FolioSurtido
	Set @IdAlmacen = '00'
	Set @EsTransferenciaAlmacen = 0
	Set @FolioMovtoInvRef = ''
	Set @FolioTransferenciaRef = ''
	Set @DestinoEsAlmacen = 0
	Set @IdAlmacenRecibe = '00'
	Set @SubTotal = 0 
	Set	@Iva = 0 
	Set	@Total = 0

	Set @sStatus = 'A'
	Set @iActualizado = 0 

	/**********************************************************
	** Se guarda el Movimiento de la Transferencia de Salida **
	***********************************************************/

	--------------------------------------------
	-- Se guarda el Encabezado del Movimiento --
	--------------------------------------------
	-- Se obtiene el Folio del Movimiento
	----Select @FolioMovtoInv = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 
	----From MovtosInv_Enc (NoLock) 
	----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdTipoMovto_Inv 
		   
	------ Actualizar el registro de folios 
	----Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioMovtoInv, 1) as int), Actualizado = 0 
	----Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdTipoMovto_Inv 

	------ Se verifica que el Folio tenga valor.
	----Set @FolioMovtoInv = IsNull(@FolioMovtoInv, '1') 
	----Set @FolioMovtoInv = @IdTipoMovto_Inv + right(replicate('0', 8) + @FolioMovtoInv, 8) 

	------ Se inserta en la tabla MovtosInv_Enc.
	----Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
	----		Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
	----Select	@IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv, @IdTipoMovto_Inv, @TipoES, 
	----		@Referencia, @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total 

	-----------------------------------------
	-- Se guarda el Detalle del Movimiento --
	-----------------------------------------
	-- Se obtienen los Codigo EAN  
	Select	IdEmpresa, IdEstado, IdFarmacia, @FolioMovtoInv as FolioMovtoInv, IdProducto, CodigoEAN, 
			Cast( 0 as Numeric(14,4) ) as TasaIva, Sum( CantidadAsignada ) as Cantidad, Cast( 0 as Numeric(14,4) ) as Costo, 
			Cast( 0 as Numeric(14,4) ) as Importe, 1 as UnidadDeSalida, @sStatus as Status, @iActualizado as Actualizado
	Into #tmpMovimientoDetalles
	From Pedidos_Cedis_Det_Surtido_Distribucion (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido And CantidadAsignada > 0 
	Group By IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN


---------------------- Concentrar por Clave y actualizar la cantidad asignada 		 
	Select  IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, Sum(CantidadAsignada) as Cantidad 
	Into #tmpClaves 
	From Pedidos_Cedis_Det_Surtido_Distribucion (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido And CantidadAsignada > 0 
	Group By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA   
	
	------Update P Set CantidadAsignada = C.Cantidad 
	------From Pedidos_Cedis_Det_Surtido P (nolock) 	
	------Inner Join #tmpClaves C 
	------	On ( P.IdEmpresa = C.IdEmpresa and P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia 
	------		and P.FolioSurtido = @FolioSurtido and P.ClaveSSA = C.ClaveSSA ) 
---------------------- Concentrar por Clave y actualizar la cantidad asignada 		 
	
	

	-- Se obtiene el Costo y el Importe
	Update T Set Costo = F.CostoPromedio, Importe = ( F.CostoPromedio * Cantidad )
	From #tmpMovimientoDetalles T (NoLock)
	Inner Join FarmaciaProductos F (NoLock) On ( T.IdProducto = F.IdProducto )

	-- Se obtiene la Tasa IVA
	Update T Set TasaIva = F.TasaIva
	From #tmpMovimientoDetalles T(NoLock)
	Inner Join vw_Productos_CodigoEAN F (NoLock) On ( T.IdProducto = F.IdProducto And T.CodigoEAN = F.CodigoEAN )
	
	------ Se inserta en la tabla de los detalles.
	----Insert Into MovtosInv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Status, Actualizado ) 
	----Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Status, Actualizado 
	----From #tmpMovimientoDetalles(NoLock)
	----Order By IdProducto, CodigoEAN 

	-----------------------------------------
	-- Se guardan los Lotes del Movimiento --
	-----------------------------------------
	-- Se obtienen los Lotes
	Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioMovtoInv as FolioMovtoInv, 
			IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
			Sum( CantidadAsignada ) as Cantidad, Cast( 0 as Numeric(14,4) ) as Costo, Cast( 0 as Numeric(14,4) ) as Importe, 
			@sStatus as Status, @iActualizado as Actualizado
	Into #tmpMovimientoLotes
	From Pedidos_Cedis_Det_Surtido_Distribucion (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido And CantidadAsignada > 0 
	Group By IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion 

	-- Se obtiene el Costo y el Importe
	Update T Set Costo = F.CostoPromedio, Importe = ( F.CostoPromedio * Cantidad )
	From #tmpMovimientoLotes T(NoLock)
	Inner Join FarmaciaProductos F(NoLock) On ( T.IdProducto = F.IdProducto )
	
	------ Se inserta en la tabla de los Lotes.
	----Insert Into MovtosInv_Det_CodigosEAN_Lotes ( IdEmpresa ,IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, Cantidad, Costo, Importe, EsConsignacion, Status, Actualizado  ) 
	----Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, Cantidad, Costo, Importe, EsConsignacion, Status, Actualizado  
	----From #tmpMovimientoLotes(NoLock)
	----Order By IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote

	-----------------------------------------------
	-- Se guardan las Ubicaciones del Movimiento --
	-----------------------------------------------
	-- Se obtienen los Lotes
	Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioMovtoInv as FolioMovtoInv, 
			IdProducto, CodigoEAN, ClaveLote, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Sum( CantidadAsignada ) as Cantidad, 
			@sStatus as Status, @iActualizado as Actualizado
	Into #tmpMovimientoUbicaciones
	From Pedidos_Cedis_Det_Surtido_Distribucion (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido And CantidadAsignada > 0 
	Group By IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño 
	
	------ Se inserta en la tabla de las Ubicaciones
	----Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
	----	( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
	----	  IdPasillo, IdEstante, IdEntrepaño, Cantidad, Status, Actualizado  ) 
	----Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion,  
	----	  IdPasillo, IdEstante, IdEntrepano, Cantidad, Status, Actualizado   
	----From #tmpMovimientoUbicaciones(NoLock)
	----Order By IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño

	/*****************************************
	** Se guarda la Transferencia de Salida **
	******************************************/
	-----------------------------------------------------------
	-- Se guarda el Encabezado de la Transferencia de Salida --
	-----------------------------------------------------------
	-- Se obtiene el consecutivo. Nota se toman solo los primeros 8 caracteres de la derecha para formar el consecutivo 
	Select @FolioTransferencia =  Max(Cast(right(FolioTransferencia, 8) as int)) + 1  
	From TransferenciasEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and TipoTransferencia = @TipoTransferencia 

	-- Se Formatea el Folio
	Set @FolioTransferencia = IsNull(@FolioTransferencia, '1') 
	Set @FolioTransferencia = @TipoTransferencia + right( Replicate('0', 8) + @FolioTransferencia, 8 )

	-- Se inserta en la tabla TransferenciasEnc
	Insert Into TransferenciasEnc ( IdEmpresa, IdEstado, IdFarmacia, IdAlmacen, EsTransferenciaAlmacen, FolioTransferencia, 
		 FolioMovtoInv, FolioMovtoInvRef, FolioTransferenciaRef, TipoTransferencia, DestinoEsAlmacen, FechaTransferencia, 
		 FechaRegistro, IdPersonal, Observaciones, SubTotal, Iva, Total, IdEstadoRecibe, IdFarmaciaRecibe, IdAlmacenRecibe, IdPersonalRegistra, Status, Actualizado ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdAlmacen, @EsTransferenciaAlmacen, @FolioTransferencia, 
		 @FolioMovtoInv, @FolioMovtoInvRef, @FolioTransferenciaRef, @TipoTransferencia, @DestinoEsAlmacen, GetDate(),  
		 getdate(), @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @IdEstadoRecibe, @IdFarmaciaRecibe, @IdAlmacenRecibe, @IdPersonal, @sStatus, @iActualizado 

	Set @sMensaje = 'Información guardada satisfactoriamente, con el folio ' + right(@FolioTransferencia, 10) 	 

	--------------------------------------------------------
	-- Se guarda el Detalle de la Transferencia de Salida --
	--------------------------------------------------------
	-- Se Copian los productos de Movimientos Detalle.
	Select	IdEmpresa, IdEstado, IdFarmacia, @FolioTransferencia as FolioTransferencia, IdProducto, CodigoEAN, UnidadDeSalida as UnidadDeEntrada, 
			Cantidad as CantidadEnviada, 0 as CantidadDevuelta,
			Costo, TasaIva, Importe as SubTotal, Cast( 0 as Numeric(14,4) ) as ImporteIva, Cast( 0 as Numeric(14,4) ) as Importe, 
			Status, Actualizado
	Into #tmpTransferenciaDetalle 
	From #tmpMovimientoDetalles(NoLock)
	Order By IdProducto, CodigoEAN 

	-- Se calcula el Importe Iva
	Update #tmpTransferenciaDetalle Set ImporteIva = IsNull( (  ((1+(TasaIva/100)) * SubTotal) - SubTotal  ), 0.0000 )

	-- Se calcula el Importe
	Update #tmpTransferenciaDetalle Set Importe = ( SubTotal + ImporteIva )

	-- Se agrega el campo Renglon
	Alter Table #tmpTransferenciaDetalle Add Renglon int identity(1,1) 

	-- Se inserta en la tabla TransferenciasDet
	Insert Into TransferenciasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		  Cant_Enviada, Cant_Devuelta, CantidadEnviada, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado  ) 
	Select IdEmpresa, IdEstado, IdFarmacia, @FolioTransferencia, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		  CantidadEnviada, CantidadDevuelta, CantidadEnviada, Costo, TasaIva, SubTotal, ImporteIva, Importe, Status, Actualizado 
	From #tmpTransferenciaDetalle(NoLock) 
	Order By IdProducto, CodigoEAN

	--------------------------------------------------------
	-- Se guardan los Lotes de la Transferencia de Salida --
	--------------------------------------------------------
	-- Se Copian los productos de Movimientos Lotes.
	Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia as IdSubFarmaciaEnvia, @FolioTransferencia as FolioTransferencia, 
			IdProducto, CodigoEAN, ClaveLote, Cast( 0 as int ) as Renglon, EsConsignacion, 
			Cantidad as CantidadEnviada, Status, Actualizado
	Into #tmpTransferenciaLotes
	From #tmpMovimientoLotes(NoLock)
	Order By IdProducto, CodigoEAN 

	-- Se obtiene el Renglon
	Update L Set Renglon = D.Renglon
	From #tmpTransferenciaLotes L(NoLock)
	Inner Join #tmpTransferenciaDetalle D(NoLock) On ( L.IdProducto = D.IdProducto And L.CodigoEAN = D.CodigoEAN )

	-- Se inserta en la tabla TransferenciasDet_Lotes
	Insert Into TransferenciasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, 
			   ClaveLote, Renglon, CantidadEnviada, EsConsignacion, Status, Actualizado, IdSubFarmaciaRecibe  ) 
	Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, 
			ClaveLote, Renglon, CantidadEnviada, EsConsignacion, Status, Actualizado, IdSubFarmaciaEnvia as IdSubFarmaciaRecibe 
	From #tmpTransferenciaLotes(NoLock)
	Order By IdSubFarmaciaEnvia, IdProducto, CodigoEAN, ClaveLote

	--------------------------------------------------------------
	-- Se guardan las Ubicaciones de la Transferencia de Salida --
	--------------------------------------------------------------
	-- Se Copian los productos de Movimientos Lotes.
	Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia as IdSubFarmaciaEnvia, @FolioTransferencia as FolioTransferencia, 
			IdProducto, CodigoEAN, ClaveLote, Cast( 0 as int ) as Renglon, EsConsignacion, 
			IdPasillo, IdEstante, IdEntrepaño, 
			Cantidad as CantidadEnviada, Status, Actualizado
	Into #tmpTransferenciaUbicaciones
	From #tmpMovimientoUbicaciones(NoLock)
	Order By IdProducto, CodigoEAN 

	-- Se obtiene el Renglon
	Update L Set Renglon = D.Renglon
	From #tmpTransferenciaUbicaciones L(NoLock)
	Inner Join #tmpTransferenciaLotes D(NoLock) On ( L.IdProducto = D.IdProducto And L.CodigoEAN = D.CodigoEAN )

	-- Se inserta en la tabla TransferenciasDet_Lotes_Ubicaciones
	Insert Into TransferenciasDet_Lotes_Ubicaciones(
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, FolioTransferencia, 
			IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, 
			IdPasillo, IdEstante, IdEntrepaño, CantidadEnviada, Status, Actualizado ) 
	Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaEnvia as IdSubFarmaciaRecibe, @FolioTransferencia, 
			IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, 
			IdPasillo, IdEstante, IdEntrepaño, CantidadEnviada, Status, Actualizado 
	From #tmpTransferenciaUbicaciones(NoLock)
	Order By IdSubFarmaciaRecibe, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño

	--------------------------------
	-- Se actualizan los Totales  --
	--------------------------------
	Select @SubTotal = Sum(SubTotal), @Iva = Sum(ImporteIva), @Total = Sum(Importe) From #tmpTransferenciaDetalle(NoLock)

	------ Se actualizan los totales del movimiento
	----Update MovtosInv_Enc Set SubTotal = @SubTotal, Iva = @Iva, Total = @Total
	----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovtoInv 

	-- Se actualizan los totales de la transferencia
	Update TransferenciasEnc Set SubTotal = @SubTotal, Iva = @Iva, Total = @Total
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTransferencia = @FolioTransferencia 


	------------------------------------------
	-- Se actualiza el status a T(Transito) --
	------------------------------------------
	Update Pedidos_Cedis_Enc_Surtido Set Status = 'E', FolioTransferenciaReferencia = @FolioTransferencia  
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido 
 
 
------------------------------ Revisar el surtimiento del pedido 
	
------------------------------ Revisar el surtimiento del pedido 
 
 
 
	---------------------------------------------------------------------
	-- Se devuelven los Folios de Movimiento y Transferencia de Salida --
	---------------------------------------------------------------------
	Select @FolioMovtoInv as FolioMovtoInv, @FolioTransferencia as FolioTransferencia, @sMensaje as Mensaje 
	
End 
Go--#SQL

