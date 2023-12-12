
Set dateformat YMD 
Go--#SQL 



Declare 
		@MeseCaducidad int = 1,  
		
		@sObservaciones varchar(500), 
		@FolioTransferencia varchar(20), 
		@TipoTransferencia varchar(10), 

		@FolioTransferencia_Origen varchar(20), 

		@IdSubFarmacia_Origen Varchar(2) = '05',
		@IdSubFarmacia_Destino Varchar(2) = '05', 
		@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '21', 
		@IdFarmacia Varchar(4) = '4224', 
		@IdFarmacia_Destino Varchar(4) = '4224', 
		@IdCliente varchar(6) = '0042', @IdSubCliente varchar(4) = '0001', 
		@IdPrograma varchar(4) = '0202', @IdSubPrograma varchar(4) = '0002', 
		@IdBeneficiario varchar(8) = '', @NumReceta varchar(20) = 'VENTA-MASIVA', @FechaReceta datetime = Getdate(), 
		@IdTipoDeDispensacion varchar(2) = '00', @IdUnidadMedica varchar(6) = '000000', 
		@IdMedico varchar(6) = '000001', @IdBeneficioSP varchar(4) = '0000', @IdDiagnostico varchar(6) = '0000', 
		@IdServicio varchar(3) = '001', @IdArea varchar(3) = '001', @RefObservaciones varchar(100) = '', 
		@IdTipoMovtoEntrada Varchar(8) = 'EPC',
		@iOpcion smallint = 1


Declare @FolioVenta varchar(32) = '*', @FolioMovto VarChar(32) = '*', @FechaSistema varchar(10) = '', @IdCaja varchar(2) = '', @IdPersonal varchar(6) = '0001',
		@TipoDeVenta smallint = 2, @Descuento numeric(14, 4) = 0,
		@Importe numeric(14, 4), @SubTotal numeric(14, 4) = 0, @Iva numeric(14, 4) = 0, @Total numeric(14, 4) = 0,
		@EsAlmacen bit = 0,


		--Detallado
		@IdProducto varchar(10), 
		@CodigoEAN varchar(32), @Renglon int = 0, @Cantidad Numeric(14, 4), @CostoPromedio numeric(14, 4), @UltimoCosto numeric(14, 4),
		@ImpteIva numeric(14, 4), @TasaIva numeric(14, 4), 

		--Lotes
		@ClaveLote varchar(32), @IdSubFarmacia Varchar(2), @FechaCaduca varchar(10),
		--Ubicaciones
		@IdPasillo int, @IdEstante int, @IdEntrepaño int

		 

		Set @IdEmpresa = '004' 
		Set @IdEstado = '11' 
		Set @IdFarmacia = '4005' 
		Set @IdFarmacia_Destino = '4002'
		Set @IdSubFarmacia_Origen = '05' 
		Set @IdSubFarmacia_Destino = '05' 
		Set @TipoTransferencia = 'TE' 

		Set @sObservaciones = 'TRANSFERENCIA GENERADA DE FORMA MASIVA'
		Set @FolioTransferencia_Origen = 'TS00008394' 





Begin Tran 

    Select @FolioTransferencia =  max(cast(right(FolioTransferencia, 8) as int)) + 1  
    From TransferenciasEnc (NoLock) 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia_Destino and TipoTransferencia = @TipoTransferencia 

	   
	-- Formatear el folio 
	Set @FolioTransferencia = IsNull(@FolioTransferencia, '1') 
	-- Set @FolioTransferencia = @IdEmpresa + @IdEstado + @IdFarmacia + @CveRenapo + @TipoTransferencia + right( replicate('0', 8) + @FolioTransferencia, 8 )
	Set @FolioTransferencia = @TipoTransferencia + right( replicate('0', 8) + @FolioTransferencia, 8 )


	Select @FolioTransferencia 


	------------------------------------------------------ PRODUCTOS   
	Exec spp_Mtto_Transferencias_Registrar_SKU 
		@IdEmpresa_Envia = @IdEmpresa, @IdEstado_Envia = @IdEstado, @IdFarmacia_Envia = @IdFarmacia, @FolioTransferencia = @FolioTransferencia_Origen 


	Update T Set Status = 'I' 
	From TransferenciasEnvioDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  


	Insert Into FarmaciaProductos 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CostoPromedio, UltimoCosto, Existencia, StockMinimo, StockMaximo, Status, Actualizado, 
		ExistenciaEnTransito, ExistenciaSurtidos
	) 
	Select 
		IdEmpresa, IdEstadoRecibe, IdFarmaciaRecibe, IdProducto, avg(CostoUnitario) as CostoPromedio, avg(CostoUnitario) as UltimoCosto, 
		0 as Existencia, 0 as StockMinimo, 0 as StockMaximo, 'A' as Status, 0 as Actualizado, 0 as ExistenciaEnTransito, 0 as ExistenciaSurtidos
	From TransferenciasEnvioDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  
	Group by 
		IdEmpresa, IdEstadoRecibe, IdFarmaciaRecibe, IdProducto  


	Insert Into FarmaciaProductos_CodigoEAN 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Existencia, Status, Actualizado, ExistenciaEnTransito, ExistenciaSurtidos
	) 
	Select 
		IdEmpresa, IdEstadoRecibe, IdFarmaciaRecibe, IdProducto, CodigoEAN, 0 as Existencia, 'A' as Status, 0 as Actualizado, 0 as ExistenciaEnTransito, 0 as ExistenciaSurtidos
	From TransferenciasEnvioDet (NoLock)  
	Group by 
		IdEmpresa, IdEstadoRecibe, IdFarmaciaRecibe, IdProducto, CodigoEAN 


	Insert Into FarmaciaProductos_CodigoEAN_Lotes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		Existencia, FechaCaducidad, FechaRegistro, IdPersonal, Status, Actualizado, ExistenciaEnTransito, CostoPromedio, UltimoCosto, ExistenciaSurtidos, SKU
	) 
	Select 
		IdEmpresa, IdEstadoRecibe, IdFarmaciaRecibe, IdSubFarmaciaRecibe, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		0 as Existencia, FechaCaducidad, getdate() as FechaRegistro, '0001' as IdPersonal, 'A' as Status, 0 as Actualizado, 
		0 as ExistenciaEnTransito, CostoUnitario as CostoPromedio, CostoUnitario as UltimoCosto, 0 as ExistenciaSurtidos, SKU	
	From TransferenciasEnvioDet_LotesRegistrar  T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  
	Group by  
		IdEmpresa, IdEstadoRecibe, IdFarmaciaRecibe, IdSubFarmaciaRecibe, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		FechaCaducidad, CostoUnitario, SKU 


	Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado, ExistenciaEnTransito, ExistenciaSurtidos, SKU  
	) 
	Select 
		IdEmpresa, IdEstado, @IdFarmacia_Destino as IdFarmacia, IdSubFarmaciaRecibe as IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, 0 as Existencia, 'A' as Status, 0 as Actualizado, 0 as ExistenciaEnTransito, 0 as ExistenciaSurtidos, SKU  
	From TransferenciasEnvioDet_Lotes_Ubicaciones T (NoLock)  

	------------------------------------------------------ PRODUCTOS   


	--select top 10 * from TransferenciasEnvioDet 


	--		sp_listacolumnas		FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones


	------------------------------------------------------ TRANSFERENCIA   
	Insert Into TransferenciasEnc 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdAlmacen, EsTransferenciaAlmacen, 
		FolioTransferencia, FolioMovtoInv, FolioMovtoInvRef, FolioTransferenciaRef, 
		TipoTransferencia, DestinoEsAlmacen, FechaTransferencia, FechaRegistro, 
		IdPersonal, Observaciones, 
		SubTotal, Iva, Total, IdEstadoRecibe, IdFarmaciaRecibe, IdAlmacenRecibe, Status, Actualizado, 
		TransferenciaAplicada, IdPersonalRegistra, FechaAplicada, IdPersonalCancela, FechaCancelacion, TieneDevolucion
	) 
	Select 
		IdEmpresa, IdEstadoEnvia, IdFarmaciaRecibe as IdFarmacia, '00' as AlmacenEnvia, 0 as EsTransferenciaAlmacen, 
		@FolioTransferencia as FolioTransferencia, @FolioTransferencia as FolioMovtoInv, @FolioTransferencia as FolioMovtoInvRef, @FolioTransferencia_Origen as FolioTransferenciaRef, 
		
		@TipoTransferencia as TipoTransferencia, 0 as DestinoEsAlmacen, getdate() as FechaTransferencia, getdate() as FechaRegistro, 
		'0001' as IdPersonal, '' as Observaciones, 
		T.SubTotal, T.Iva, T.Total, IdEstadoRecibe, IdFarmaciaEnvia as IdFarmaciaRecibe, T.IdAlmacenRecibe, 'A' as Status, 0 as Actualizado, 
		1 as TransferenciaAplicada, '0001' as IdPersonalRegistra, getdate() as FechaAplicada, '0001' as IdPersonalCancela, getdate() as FechaCancelacion, 0 as TieneDevolucion
		
	From TransferenciasEnvioEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  


	Insert Into TransferenciasDet 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		Cant_Enviada, Cant_Devuelta, CantidadEnviada, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado 
	) 
	Select 
		IdEmpresa, IdEstadoEnvia, IdFarmaciaRecibe as IdFarmaciaEnvia, @FolioTransferencia as FolioTransferencia, 
		T.IdProducto, T.CodigoEAN, T.Renglon, T.UnidadDeEntrada, 
		T.Cant_Enviada, T.Cant_Devuelta, T.CantidadEnviada, T.CostoUnitario, 
		T.TasaIva, T.SubTotal, T.ImpteIva, T.Importe, 'A' as Status, 0 as Actualizado  
	From TransferenciasEnvioDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  


	Insert Into  TransferenciasDet_Lotes
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, 
		FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, CantidadEnviada, Status, Actualizado, CostoUnitario, Cant_Enviada, Cant_Devuelta, SKU
	) 
	Select 
		IdEmpresa, IdEstadoEnvia, IdFarmaciaRecibe as IdFarmaciaEnvia, T.IdSubFarmaciaEnvia as IdSubFarmaciaEnvia, T.IdSubFarmaciaEnvia as IdSubFarmaciaRecibe, 
		@FolioTransferencia as FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, Cant_Enviada, Status, Actualizado, CostoUnitario, Cant_Devuelta, CantidadEnviada, SKU  
	From TransferenciasEnvioDet_Lotes T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen 	


	Insert Into TransferenciasDet_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, CantidadEnviada, Status, Actualizado, Cant_Enviada, Cant_Devuelta, SKU	
	) 
	Select 
		IdEmpresa, IdEstado, @IdFarmacia_Destino as IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, @FolioTransferencia as FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, CantidadEnviada, Status, Actualizado, Cant_Enviada, Cant_Devuelta, SKU	
	From TransferenciasEnvioDet_Lotes_Ubicaciones T 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen 	



	------------------------------------------------------ TRANSFERENCIA  
	

	------------------------------------------------------ INVENTARIOS    
	------------------- Encabezado 
	Insert Into MovtosInv_Enc 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, 
		IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado, FolioCierre, Cierre, IdPersonalHuella, SKU 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia_Destino, @FolioTransferencia, @TipoTransferencia, 'e' as TipoES, getdate() as FechaSistema, getdate() as FechaRegistro, 
		@FolioTransferencia_Origen as Referencia, 'N' as MovtoAplicado, 
		'0001' as IdPersonalRegistra, @sObservaciones as Observaciones, 
		sum(SubTotal) as SubTotal, sum(Iva) as IVA, sum(Total) as Total, 'A' as Status, 0 as Actualizado, 0 as FolioCierre, 0 as Cierre, '' as IdPersonalHuella, '' as SKU 
	From TransferenciasEnvioEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  



	Select 
		IdEmpresa, IdEstadoEnvia, @IdFarmacia_Destino as IdFarmacia, '00' as AlmacenEnvia, 0 as EsTransferenciaAlmacen, 
		@FolioTransferencia as FolioTransferencia, @FolioTransferencia as FolioMovtoInv, @FolioTransferencia as FolioMovtoInvRef, @FolioTransferencia_Origen as FolioTransferenciaRef, 
		
		@TipoTransferencia as TipoTransferencia, 0 as DestinoEsAlmacen, getdate() as FechaTransferencia, getdate() as FechaRegistro, 
		'0001' as IdPersonal, '' as Observaciones, 
		T.SubTotal, T.Iva, T.Total, IdEstadoRecibe, IdFarmaciaEnvia as IdFarmaciaRecibe, T.IdAlmacenRecibe, 'A' as Status, 0 as Actualizado, 
		1 as TransferenciaAplicada, '0001' as IdPersonalRegistra, getdate() as FechaAplicada, '0001' as IdPersonalCancela, getdate() as FechaCancelacion, 0 as TieneDevolucion
		
	From TransferenciasEnvioEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  

----	sp_listacolumnas		MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 


	Insert Into MovtosInv_Det_CodigosEAN 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia_Destino as IdFarmacia, @FolioTransferencia, IdProducto, CodigoEAN, getdate() as FechaSistema, 
		'' as UnidadDeSalida, TasaIva, sum(Cant_Enviada) as Cantidad, avg(CostoUnitario), sum(SubTotal) as Importe, 0 as Existencia, 'A' as Status, 0 as Actualizado 
	From TransferenciasEnvioDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  
	Group by 
		IdProducto, CodigoEAN, TasaIva 



	Insert Into MovtosInv_Det_CodigosEAN_Lotes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Cantidad, Costo, Importe, Existencia, Status, Actualizado, SKU
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia_Destino as IdFarmacia, IdSubFarmaciaEnvia AS IdSubFarmacia, @FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Cant_Enviada as Cantidad, CostoUnitario, (Cant_Enviada * CostoUnitario) As Importe, 
		0 as Existencia, 'A' as Status, 0 as Actualizado, SKU
	From TransferenciasEnvioDet_Lotes T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen 	


	Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado, SKU
	) 
	Select 
		IdEmpresa, IdEstado, @IdFarmacia_Destino as IdFarmacia, IdSubFarmaciaEnvia as IdSubFarmacia, @FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, Cant_Enviada Cantidad, 0 as Existencia, Status, Actualizado, SKU
	From TransferenciasEnvioDet_Lotes_Ubicaciones T 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen 	


	
	Exec spp_INV_AplicarDesaplicarExistencia  
		@IdEmpresa = @IdEmpresa,  
		@IdEstado = @IdEstado, 
		@IdFarmacia = @IdFarmacia_Destino, 
		@FolioMovto = @FolioTransferencia,  
		@Aplica = 1, @AfectarCostos = 1 	
	------------------------------------------------------ INVENTARIOS    



	--select * from TransferenciasEnc (nolock) 
	--select * from TransferenciasDet (nolock) 



---			rollback tran  


---			commit tran  
