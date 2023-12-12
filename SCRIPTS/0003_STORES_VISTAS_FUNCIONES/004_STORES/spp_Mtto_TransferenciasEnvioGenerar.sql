If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasEnvioGenerar' and xType = 'P' ) 
	Drop Proc spp_Mtto_TransferenciasEnvioGenerar 
Go--#SQL

Create Proc spp_Mtto_TransferenciasEnvioGenerar ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FolioTransferencia varchar(20) = '250001SLTS00000002' ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @IdEstadoRecibe varchar(2), 
		@IdFarmaciaRecibe varchar(4),
		@EsUnidosisEnvia bit,
		@EsUnidosisRecibe bit,
		@EsAlmacenEnvia Bit

	/*
	Delete From TransferenciasEnvioDet_LotesRegistrar 
	Delete From TransferenciasEnvioDet_Lotes 
	Delete From TransferenciasEnvioDet
	Delete From TransferenciasEnvioEnc 	 	
	*/
	
	Select @IdEstadoRecibe = T.IdEstadoRecibe, @IdFarmaciaRecibe = T.IdFarmaciaRecibe 
	From TransferenciasEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia


	Select @EsUnidosisEnvia = EsUnidosis, @EsAlmacenEnvia = EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	Select @EsUnidosisRecibe = EsUnidosis From CatFarmacias (NoLock) Where IdEstado = @IdEstadoRecibe And IdFarmacia = @IdFarmaciaRecibe
	
	--- Copiar el Encabezado 
	Insert Into TransferenciasEnvioEnc ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdAlmacenEnvia, EsTransferenciaAlmacen, FolioTransferencia, 
		FolioMovtoInv, TipoTransferencia, DestinoEsAlmacen, FechaTransferencia, FechaRegistro, IdPersonal, SubTotal, Iva, Total, 
		IdEstadoRecibe, IdFarmaciaRecibe, IdAlmacenRecibe, Status, Actualizado ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstadoEnvia, @IdFarmacia as IdFarmaciaEnvia, '00' as AlmacenEnvia, 0 as EsTransferenciaAlmacen, 
		@FolioTransferencia as FolioTransferencia, T.FolioMovtoInv, T.TipoTransferencia, 0 as DestinoEsAlmacen, 
		T.FechaTransferencia, T.FechaRegistro, T.IdPersonal as IdPersonalEnvia, T.SubTotal, T.Iva, T.Total,
		@IdEstadoRecibe as IdEstadoRecibe, @IdFarmaciaRecibe as IdFarmaciaRecibe, 
		T.IdAlmacenRecibe, 'A' as Status, 0 as Actualizado 
	From TransferenciasEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia 


	--- Copiar el Detalle 
	-- sp_Listacolumnas TransferenciasEnvioDet 
	Insert Into TransferenciasEnvioDet ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, Renglon, 
		UnidadDeEntrada, Cant_Enviada, Cant_Devuelta, CantidadEnviada, CostoUnitario, 
		TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado, IdEstadoRecibe, IdFarmaciaRecibe ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstadoEnvia, @IdFarmacia as IdFarmaciaEnvia, @FolioTransferencia as FolioTransferencia, 
		T.IdProducto, T.CodigoEAN, T.Renglon, T.UnidadDeEntrada, T.Cant_Enviada, T.Cant_Devuelta, T.CantidadEnviada, T.CostoUnitario, 
		T.TasaIva, T.SubTotal, T.ImpteIva, T.Importe, 'A' as Status, 0 as Actualizado,  
		@IdEstadoRecibe as IdEstadoRecibe, @IdFarmaciaRecibe as IdFarmaciaRecibe 		 
	From TransferenciasDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia 	
		

	--- Copiar el Detalle de Lotes 
	Insert Into TransferenciasEnvioDet_Lotes ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, 
		CodigoEAN, ClaveLote, Renglon, Cant_Enviada, Cant_Devuelta, CantidadEnviada, Status, Actualizado, IdEstadoRecibe, IdFarmaciaRecibe, IdSubFarmaciaRecibe, CostoUnitario ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstadoEnvia, @IdFarmacia as IdFarmaciaEnvia, T.IdSubFarmaciaEnvia, 
		@FolioTransferencia as FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon, Cant_Enviada, Cant_Devuelta, CantidadEnviada, Status, Actualizado, 
		@IdEstadoRecibe as IdEstadoRecibe, @IdFarmaciaRecibe as IdFarmaciaRecibe, T.IdSubFarmaciaRecibe, CostoUnitario
	From TransferenciasDet_Lotes T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia 	
	

  	
	--- Copiar los Lotes que se usaron en la transferencia, para registrarlos en la Farmacia Destino 
	Insert Into TransferenciasEnvioDet_LotesRegistrar ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, 
		ClaveLote, Renglon, Existencia, FechaCaducidad, FechaRegistro, Status, Actualizado, IdEstadoRecibe, IdFarmaciaRecibe, IdSubFarmaciaRecibe, CostoUnitario ) 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstadoEnvia, @IdFarmacia as IdFarmaciaEnvia, IdSubFarmaciaEnvia, 
		@FolioTransferencia as FolioTransferencia, L.IdProducto, L.CodigoEAN, L.ClaveLote, T.Renglon, 0 as Existencia, L.FechaCaducidad, L.FechaRegistro, 'A' as Status, 
		0 as Actualizado, @IdEstadoRecibe as IdEstadoRecibe, @IdFarmaciaRecibe as IdFarmaciaRecibe, T.IdSubFarmaciaRecibe, CostoUnitario    
	From TransferenciasDet_Lotes T (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( T.IdEmpresa = L.IdEmpresa and T.IdEstado = L.IdEstado and T.IdFarmacia = L.IdFarmacia and T.IdSubFarmaciaEnvia = L.IdSubFarmacia 
		     and T.IdProducto = L.IdProducto and T.CodigoEAN = L.CodigoEAN and T.ClaveLote = L.ClaveLote ) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia
	
	
	Insert Into Transferencias_UUID_Registrar ( UUID, IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia, 
		Status, Actualizado, IdEstadoRecibe, IdFarmaciaRecibe) 
	Select UUID, IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia,
		'A' as Status, 0 as Actualizado, @IdEstadoRecibe as IdEstadoRecibe, @IdFarmaciaRecibe as IdFarmaciaRecibe   
	From Transferencias_UUID T (NoLock) 
	--Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
	--	On ( T.IdEmpresa = L.IdEmpresa and T.IdEstado = L.IdEstado and T.IdFarmacia = L.IdFarmacia and T.IdSubFarmaciaEnvia = L.IdSubFarmacia 
	--	     and T.IdProducto = L.IdProducto and T.CodigoEAN = L.CodigoEAN and T.ClaveLote = L.ClaveLote ) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia 	


---		spp_Mtto_TransferenciasEnvioGenerar

	If (@EsUnidosisEnvia = 1 And @EsUnidosisRecibe = 0)
	Begin
		if @@error = 0 
			Begin 
				Exec spp_Mtto_TransferenciasEnvioGenerar__Convertir_A_Caja @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioTransferencia = @FolioTransferencia
			End 
	End
End 
Go--#SQL

