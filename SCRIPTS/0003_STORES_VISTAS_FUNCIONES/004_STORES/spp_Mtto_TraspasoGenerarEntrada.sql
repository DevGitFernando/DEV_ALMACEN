If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TraspasoGenerarEntrada' and xType = 'P' ) 
	Drop Proc spp_Mtto_TraspasoGenerarEntrada 
Go--#SQL

Create Proc spp_Mtto_TraspasoGenerarEntrada ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182',	 
	@FolioTraspaso varchar(20) = 'TIS00000001' ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @FolioTraspasoEnt varchar(20),
		@TipoTraspaso varchar(3),
		@IdSubFarmaciaOrigen varchar(2), 
		@IdSubFarmaciaDestino varchar(2),
		@IdPersonal varchar(4),
		@sSqlTexto varchar(8000)
		
	Set @TipoTraspaso = 'TIE'
	Set @FolioTraspasoEnt = 'TIE' + right(@FolioTraspaso, 8)
	Set @sSqlTexto = ''

	/*	 
	Delete From TraspasosDet_Lotes 
	Delete From TraspasosDet
	Delete From TraspasosEnc 	

	Select * From TraspasosEnc (Nolock)
	Select * From TraspasosDet (Nolock)
	Select * From TraspasosDet_Lotes (Nolock)
	Select * From TraspasosDet_Lotes_Ubicaciones (Nolock)
	Select top 1 * From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (Nolock)

	 	
	*/

	Select @IdSubFarmaciaOrigen = T.IdSubFarmaciaOrigen, @IdSubFarmaciaDestino = T.IdSubFarmaciaDestino,
	@IdPersonal = T.IdPersonal
	From TraspasosEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTraspaso = @FolioTraspaso

----------  Se crea la temporal de lotes_Ubicaciones ----------------------------------------------------------------------------------
	Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia, @IdSubFarmaciaDestino As IdSubFarmacia, 
	L.IdProducto, L.CodigoEAN, L.ClaveLote, L.IdPasillo, L.IdEstante, L.IdEntrepaño, 
	0 As Existencia, L.EsConsignacion, L.Status, L.Actualizado
	Into #tmpLotes_Ubicaciones
	From TraspasosDet_Lotes_Ubicaciones L (Nolock)
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F ( Nolock )
		On( L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
			And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote 
			And L.IdPasillo = F.IdPasillo And L.IdEstante = F.IdEstante And L.IdEntrepaño = F.IdEntrepaño )
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.FolioTraspaso = @FolioTraspaso

----------  Se crea la temporal de lotes ----------------------------------------------------------------------------------
	Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia, @IdSubFarmaciaDestino As IdSubFarmacia, 
	L.IdProducto, L.CodigoEAN, L.ClaveLote, 0 As Existencia, F.FechaCaducidad, 
	F.FechaRegistro, @IdPersonal As IdPersonal, L.EsConsignacion, L.Status, L.Actualizado
	Into #tmpLotes
	From TraspasosDet_Lotes L (Nolock)
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F ( Nolock )
		On( L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
			And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote )
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.FolioTraspaso = @FolioTraspaso


------- SE INSERTAN LOS LOTES A LA SUBFARMACIA A LA QUE SE ESTA TRASPASANDO -------------------------------------------------

	Insert Into FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, Existencia, 
													FechaCaducidad, FechaRegistro, IdPersonal, EsConsignacion, Status, Actualizado )
	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdSubFarmacia, T.IdProducto, T.CodigoEAN, T.ClaveLote, T.Existencia, 
	T.FechaCaducidad, T.FechaRegistro, T.IdPersonal, T.EsConsignacion, T.Status, T.Actualizado
	From #tmpLotes T (Nolock)
	Where 
		Not Exists(  Select * From FarmaciaProductos_CodigoEAN_Lotes C (NoLock) 
		Where T.IdEmpresa = C.IdEmpresa And T.IdEstado = C.IdEstado And T.IdFarmacia = C.IdFarmacia And T.IdSubFarmacia = C.IdSubFarmacia 
			And T.IdProducto = C.IdProducto And T.CodigoEAN = C.CodigoEAN And T.ClaveLote = C.ClaveLote )  

------- SE INSERTAN LOS LOTES A LA SUBFARMACIA A LA QUE SE ESTA TRASPASANDO UBICACIONES -------------------------------------------------

	Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote,
													IdPasillo, IdEstante, IdEntrepaño, 
													Existencia, EsConsignacion, Status, Actualizado )
	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdSubFarmacia, T.IdProducto, T.CodigoEAN, T.ClaveLote,
	T.IdPasillo, T.IdEstante, T.IdEntrepaño, 
	T.Existencia, T.EsConsignacion, T.Status, T.Actualizado
	From #tmpLotes_Ubicaciones T (Nolock)
	Where 
		Not Exists(  Select * From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones C (NoLock) 
		Where T.IdEmpresa = C.IdEmpresa And T.IdEstado = C.IdEstado And T.IdFarmacia = C.IdFarmacia And T.IdSubFarmacia = C.IdSubFarmacia 
			And T.IdProducto = C.IdProducto And T.CodigoEAN = C.CodigoEAN And T.ClaveLote = C.ClaveLote 
			And T.IdPasillo = C.IdPasillo And T.IdEstante = C.IdEstante And T.IdEntrepaño = C.IdEntrepaño )

-----------------------------------------------------------------------------------------------------------------------------

-------------  SE INSERTAN LOS MOVIMIENTOS -----------------------------------------------------------------------------------

	-- Se inserta el Encabezado 	
	Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
		Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTraspasoEnt, @TipoTraspaso, 'E', 
		T.FolioTraspaso, T.IdPersonal, 'Traspaso Interno de Entrada', T.SubTotal, T.Iva, T.Total 
	From TraspasosEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTraspaso = @FolioTraspaso

	-- Se inserta el Detalles
	Insert Into MovtosInv_Det_CodigosEAN( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN,  
		TasaIva, Cantidad, Costo, Importe, Status, Actualizado ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTraspasoEnt, T.IdProducto, T.CodigoEAN,  
		T.TasaIva, T.Cantidad, T.CostoUnitario, T.Importe, 'A', 0
	From TraspasosDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTraspaso = @FolioTraspaso

		-- Se inserta el Lote
	Insert Into MovtosInv_Det_CodigosEAN_Lotes(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
		Cantidad, Costo, Importe, Status, Actualizado)
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmaciaDestino, @FolioTraspasoEnt, L.IdProducto, L.CodigoEAN, L.ClaveLote,
		L.Cantidad, D.CostoUnitario, ( L.Cantidad * D.CostoUnitario ) As Importe, 'A', 0
	From TraspasosDet_Lotes L (Nolock)
	Inner Join TraspasosDet D (Nolock)
		On( L.IdEmpresa = D.IdEmpresa And L.IdEstado = D.IdEstado And L.IdFarmacia = D.IdFarmacia 
			And L.FolioTraspaso = D.FolioTraspaso And L.IdProducto = D.IdProducto And L.CodigoEAN = D.CodigoEAN )
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.FolioTraspaso = @FolioTraspaso

--	Select top 1 * From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones (Nolock)

	-- Se inserta el Lote_Ubicaciones
	Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote,
		EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, 
		Cantidad, Status, Actualizado)
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmaciaDestino, @FolioTraspasoEnt, L.IdProducto, L.CodigoEAN, L.ClaveLote,
		L.EsConsignacion, L.IdPasillo, L.IdEstante, L.IdEntrepaño,
		L.Cantidad, 'A', 0
	From TraspasosDet_Lotes_Ubicaciones L (Nolock)	
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.FolioTraspaso = @FolioTraspaso

------------------------------------------------------------------------------------------------------------------------------
	
	 
	
	--- Copiar el Encabezado 
	Insert Into TraspasosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso, IdSubFarmaciaOrigen, IdSubFarmaciaDestino,
		FolioMovtoInv, FolioMovtoInvRef, FolioTraspasoRef, TipoTraspaso, FechaTraspaso, FechaRegistro, IdPersonal, Observaciones,
		SubTotal, Iva, Total, Status, Actualizado ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioTraspasoEnt, T.IdSubFarmaciaDestino, 
		T.IdSubFarmaciaOrigen, @FolioTraspasoEnt, @FolioTraspaso, @FolioTraspaso, @TipoTraspaso, 
		T.FechaTraspaso, T.FechaRegistro, T.IdPersonal, T.Observaciones, T.SubTotal, T.Iva, T.Total,
		'A' as Status, 0 as Actualizado 
	From TraspasosEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTraspaso = @FolioTraspaso 


	--- Copiar el Detalle 
	-- sp_Listacolumnas TraspasosDet 
	Insert Into TraspasosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso, IdProducto, CodigoEAN, Renglon, 
		Cantidad, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioTraspasoEnt as FolioTraspaso, 
		T.IdProducto, T.CodigoEAN, T.Renglon, T.Cantidad, T.CostoUnitario, 
		T.TasaIva, T.SubTotal, T.ImpteIva, T.Importe, 'A' as Status, 0 as Actualizado 		 
	From TraspasosDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTraspaso = @FolioTraspaso
	

	--- Copiar el Detalle de Lotes 
	Insert Into TraspasosDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, 
		CodigoEAN, ClaveLote, Renglon, EsConsignacion, Cantidad, Status, Actualizado ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdSubFarmaciaDestino, 
		@FolioTraspasoEnt as FolioTraspaso, IdProducto, CodigoEAN, ClaveLote, Renglon, T.EsConsignacion, T.Cantidad, Status, Actualizado   
	From TraspasosDet_Lotes T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTraspaso = @FolioTraspaso
	

	--- Copiar el Detalle de Lotes_Ubicaciones 
	Insert Into TraspasosDet_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, 
		CodigoEAN, ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño, EsConsignacion, Cantidad, Status, Actualizado ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdSubFarmaciaDestino, 
		@FolioTraspasoEnt as FolioTraspaso, T.IdProducto, T.CodigoEAN, T.ClaveLote, T.Renglon,
		T.IdPasillo, T.IdEstante, T.IdEntrepaño, 
		T.EsConsignacion, T.Cantidad, Status, Actualizado   
	From TraspasosDet_Lotes_Ubicaciones T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTraspaso = @FolioTraspaso 


	-- Se aplica la Existencia 
	Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
					+ char(39) + @IdEmpresa + char(39) + ', ' 
					+ char(39) + @IdEstado + char(39) + ', ' +
					+ char(39) + @IdFarmacia + char(39) + ', ' 
					+ char(39) + @FolioTraspasoEnt + char(39) + ', '
					+ '1, 1'
	Exec (@sSqlTexto)
	


---		spp_Mtto_TraspasoGenerarEntrada 

End 
Go--#SQL

