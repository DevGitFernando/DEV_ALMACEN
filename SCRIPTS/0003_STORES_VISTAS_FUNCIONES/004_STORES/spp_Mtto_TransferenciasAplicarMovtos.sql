

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasAplicarMovtos' and xType = 'P' ) 
	Drop Proc spp_Mtto_TransferenciasAplicarMovtos
Go--#SQL

--	Exec spp_Mtto_TransferenciasAplicarMovtos '001', '21', '1182', 'TS00000033', '0004'

Create Proc spp_Mtto_TransferenciasAplicarMovtos ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182',	 
	@FolioTransferencia varchar(20) = 'TS00000033', @IdPersonalRegistra varchar(4) = '0004' ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @TipoES varchar(4),
		@IdTipoMovto_Inv varchar(6)
		
	
		Set @TipoES = 'S'
		

		/*
			Select top 1 * From TransferenciasEnc (Nolock)
			Select top 1 * From MovtosInv_Enc (Nolock)

			Select top 1 * From TransferenciasDet (Nolock)
			Select top 1 * From MovtosInv_Det_CodigosEAN (Nolock)

			Select top 1 * From TransferenciasDet_Lotes (Nolock)
			Select top 1 * From MovtosInv_Det_CodigosEAN_Lotes (Nolock)

			Select top 1 * From TransferenciasDet_Lotes_Ubicaciones (Nolock)
			Select top 1 * From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones (Nolock)
		*/

-------------  SE INSERTAN LOS MOVIMIENTOS -----------------------------------------------------------------------------------

	-- Se inserta el Encabezado 	
	Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
		Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTransferencia, TipoTransferencia, @TipoES, 
		T.FolioTransferencia, T.IdPersonal, 'Transferencia de Salida Aplicada', T.SubTotal, T.Iva, T.Total 
	From TransferenciasEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia

	-- Se inserta el Detalles
	Insert Into MovtosInv_Det_CodigosEAN( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN,  
		TasaIva, Cantidad, Costo, Importe, Status, Actualizado ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTransferencia, T.IdProducto, T.CodigoEAN,  
		T.TasaIva, T.CantidadEnviada, T.CostoUnitario, T.Importe, 'A', 0
	From TransferenciasDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia

		-- Se inserta el Lote
	Insert Into MovtosInv_Det_CodigosEAN_Lotes(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
		Cantidad, Costo, Importe, Status, Actualizado)
	Select @IdEmpresa, @IdEstado, @IdFarmacia, L.IdSubFarmaciaEnvia, @FolioTransferencia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
		L.CantidadEnviada, D.CostoUnitario, ( L.CantidadEnviada * D.CostoUnitario ) As Importe, 'A', 0
	From TransferenciasDet_Lotes L (Nolock)
	Inner Join TransferenciasDet D (Nolock)
		On( L.IdEmpresa = D.IdEmpresa And L.IdEstado = D.IdEstado And L.IdFarmacia = D.IdFarmacia 
			And L.FolioTransferencia = D.FolioTransferencia And L.IdProducto = D.IdProducto And L.CodigoEAN = D.CodigoEAN )
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.FolioTransferencia = @FolioTransferencia

--	Select top 1 * From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones (Nolock)

	-- Se inserta el Lote_Ubicaciones
	Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote,
		EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, 
		Cantidad, Status, Actualizado)
	Select @IdEmpresa, @IdEstado, @IdFarmacia, L.IdSubFarmaciaEnvia, @FolioTransferencia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
		L.EsConsignacion, L.IdPasillo, L.IdEstante, L.IdEntrepaño,
		L.CantidadEnviada, 'A', 0
	From TransferenciasDet_Lotes_Ubicaciones L (Nolock)	
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.FolioTransferencia = @FolioTransferencia

------------------------------------------------------------------------------------------------------------------------------	 

------ SE ACTUALIZAN LOS CAMPOS QUE NOS INDICAN QUE YA ESTA APLICADA LA TRANSFERENCIA
	Update T Set T.TransferenciaAplicada = 1, T.IdPersonalRegistra = @IdPersonalRegistra, T.FechaAplicada = GetDate()
	From TransferenciasEnc T (Nolock)
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia

---		spp_Mtto_TransferenciasAplicarMovtos 

End 
Go--#SQL

