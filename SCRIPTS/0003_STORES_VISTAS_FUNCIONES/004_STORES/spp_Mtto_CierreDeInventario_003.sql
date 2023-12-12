If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario_003' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario_003 
Go--#SQL 

Create Proc spp_Mtto_CierreDeInventario_003  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182',
	@IdEmpresaNueva varchar(3) = '001', @IdFarmaciaNueva varchar(4) = '0002', 
	@FolioSalidaVenta varchar(30) = 'SCI00000001', @FolioSalidaConsignacion varchar(30) = 'SCI00000002',
	@IdPersonal varchar(6) = '0001', @FechaSistema varchar(10) = '2012-02-20' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

---		spp_Mtto_CierreDeInventario_InventarioInicial_003  


Declare @Actualizado smallint,  
		@Status varchar(1),  
		@IdMovto_Inicial_Venta varchar(4),
		@IdMovto_Inicial_Consignacion varchar(4),
		@TipoEntrada varchar(1),    
		@FolioEntradaVenta varchar(30), 		
		@FolioEntradaConsignacion varchar(30),
		@sSqlTexto varchar(8000) 

	Set DateFormat YMD
	Set @IdMovto_Inicial_Venta = 'II' 
	Set @IdMovto_Inicial_Consignacion = 'IIC' 
	Set @TipoEntrada = 'E' 
	Set @FolioEntradaVenta = '' 
	Set @FolioEntradaConsignacion = '' 
	Set @sSqlTexto = ''
	Set @Actualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso  ( 0 - 3 ) 
	Set @Status = 'A'

	------------------------------------------------------
	-- Se Insertan las Ubicaciones en la Nueva Farmacia --
	------------------------------------------------------

	Delete From CatPasillos_Estantes_Entrepaños Where IdEmpresa = @IdEmpresaNueva And IdEstado = @IdEstado And IdFarmacia = @IdFarmaciaNueva  
	Delete From CatPasillos_Estantes Where IdEmpresa = @IdEmpresaNueva And IdEstado = @IdEstado And IdFarmacia = @IdFarmaciaNueva  
	Delete From CatPasillos Where IdEmpresa = @IdEmpresaNueva And IdEstado = @IdEstado And IdFarmacia = @IdFarmaciaNueva  
	
	Insert Into CatPasillos ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, DescripcionPasillo, Status, Actualizado ) 
	Select @IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdPasillo, DescripcionPasillo, Status, @Actualizado
	From CatPasillos(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	Order By IdPasillo

	Insert Into CatPasillos_Estantes ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, DescripcionEstante, Status, Actualizado )
	Select @IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdPasillo, IdEstante, DescripcionEstante, Status, @Actualizado
	From CatPasillos_Estantes(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	Order By IdPasillo, IdEstante

	Insert Into CatPasillos_Estantes_Entrepaños ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, DescripcionEntrepaño, EsExclusiva, Status, Actualizado ) 
	Select @IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdPasillo, IdEstante, IdEntrepaño, DescripcionEntrepaño, EsExclusiva, Status, @Actualizado
	From CatPasillos_Estantes_Entrepaños(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	Order By IdPasillo, IdEstante, IdEntrepaño

	----------------------------------------------------
	-- Se Insertan los Productos en la Nueva Farmacia --
	----------------------------------------------------

	Insert Into FarmaciaProductos 
		(	IdEmpresa, IdEstado, IdFarmacia, IdProducto, CostoPromedio, UltimoCosto, Existencia, StockMinimo, StockMaximo, Status, Actualizado )
	Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdProducto, 0.0000 as CostoPromedio, 0.0000 as UltimoCosto, 0.0000 as Existencia, 
			0.0000 as StockMinimo, 0.0000 as StockMaximo, 'A' as Status, @Actualizado
	From FarmaciaProductos (NoLock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
	Order By IdProducto

	Insert Into FarmaciaProductos_CodigoEAN ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Existencia, Status, Actualizado ) 
	Select @IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdProducto, CodigoEAN, 0.0000 as Existencia, 'A' as Status, @Actualizado
	From FarmaciaProductos_CodigoEAN(NoLock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Order By IdProducto, CodigoEAN 
 
--	 	sp_listacolumnas FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
 
	Insert Into FarmaciaProductos_CodigoEAN_Lotes 
	(		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Existencia, FechaCaducidad, FechaRegistro, IdPersonal, Status, Actualizado ) 
	Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 0.0000 as Existencia,
			FechaCaducidad, FechaRegistro, @IdPersonal, 'A' as Status, @Actualizado
	From FarmaciaProductos_CodigoEAN_Lotes(NoLock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Order By IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia


	Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
		(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
			IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado ) 
	Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
			IdPasillo, IdEstante, IdEntrepaño, 0.0000 as Existencia, 'A' as Status, @Actualizado
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones(NoLock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Order By IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, IdPasillo, IdEstante, IdEntrepaño


------------------------------------------------
------ Se Insertan los movimientos de Entrada --
------------------------------------------------

		-------------------------------------
		-- Se Insertan la Entrada de Venta --
		-------------------------------------

		-- Se obtiene el Folio de Entrada de Venta
		Select @FolioEntradaVenta = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 
		From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresaNueva And IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaNueva and IdTipoMovto_Inv = @IdMovto_Inicial_Venta 
			   
		-- Actualizar el registro de folios 
		Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioEntradaVenta, 1) as int), Actualizado = 0 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaNueva and IdTipoMovto_Inv = @IdMovto_Inicial_Venta 

		-- Se asegura que el numero de Folio sea Valido.
		Set @FolioEntradaVenta = IsNull(@FolioEntradaVenta, '1') 
		Set @FolioEntradaVenta = @IdMovto_Inicial_Venta + right(replicate('0', 8) + @FolioEntradaVenta, 8) 	
		
		-- Se Inserta el Encabezado.
		Insert Into MovtosInv_Enc 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, 
				Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado ) 		
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, @FolioEntradaVenta, @IdMovto_Inicial_Venta, @TipoEntrada, @FechaSistema, GetDate() as FechaRegistro, 
				'' as Referencia, 'N' as MovtoAplicado, @IdPersonal, 'INVENTARIO INICIAL DE VENTA', SubTotal, Iva, Total, 
				@Status, @Actualizado
		From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaVenta

		-- Se Insertan los CodigosEAN.
		Insert Into MovtosInv_Det_CodigosEAN 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 		
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, @FolioEntradaVenta, IdProducto, CodigoEAN, @FechaSistema, 0 as UnidadDeSalida, TasaIva, 
				Cantidad, Costo, Importe, 0 as Existencia, @Status, @Actualizado
		From MovtosInv_Det_CodigosEAN(NoLock)
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaVenta

		-- Se Insertan los Lotes.
		Insert Into MovtosInv_Det_CodigosEAN_Lotes 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado )  				
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdSubFarmacia, @FolioEntradaVenta, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Cantidad, Costo, Importe, 0 as Existencia, @Status, @Actualizado
		From MovtosInv_Det_CodigosEAN_Lotes(NoLock)
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaVenta

		-- Se Insertan las Ubicaciones.
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado ) 		
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdSubFarmacia, @FolioEntradaVenta, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Cantidad, 0 as Existencia, @Status, @Actualizado
		From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(NoLock)
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaVenta

		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresaNueva + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmaciaNueva + char(39) + ', ' 
						+ char(39) + @FolioEntradaVenta + char(39) + ', '
						+ '1, 1'
		Exec (@sSqlTexto)

-------------------------------------------------
------- Se Insertan la Entrada de Consignacion --
-------------------------------------------------

		-- Se obtiene el Folio de Entrada de Consignacion
		Select @FolioEntradaConsignacion = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 
		From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresaNueva And IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaNueva and IdTipoMovto_Inv = @IdMovto_Inicial_Consignacion 
			   
		-- Actualizar el registro de folios 
		Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioEntradaConsignacion, 1) as int), Actualizado = 0 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaNueva and IdTipoMovto_Inv = @IdMovto_Inicial_Consignacion 

		-- Se asegura que el numero de Folio sea Valido.
		Set @FolioEntradaConsignacion = IsNull(@FolioEntradaConsignacion, '1') 
		Set @FolioEntradaConsignacion = @IdMovto_Inicial_Consignacion + right(replicate('0', 8) + @FolioEntradaConsignacion, 8) 	
		
		-- Se Inserta el Encabezado.
		Insert Into MovtosInv_Enc 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, 
				Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado ) 		
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, @FolioEntradaConsignacion, @IdMovto_Inicial_Consignacion, @TipoEntrada, @FechaSistema, GetDate() as FechaRegistro, 
				'' as Referencia, 'N' as MovtoAplicado, @IdPersonal, 'INVENTARIO INICIAL DE CONSIGNACION', SubTotal, Iva, Total, 
				@Status, @Actualizado
		From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaConsignacion

		-- Se Insertan los CodigosEAN.
		Insert Into MovtosInv_Det_CodigosEAN 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 		
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, @FolioEntradaConsignacion, IdProducto, CodigoEAN, @FechaSistema, 0 as UnidadDeSalida, TasaIva, 
				Cantidad, Costo, Importe, 0 as Existencia, @Status, @Actualizado
		From MovtosInv_Det_CodigosEAN(NoLock)
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaConsignacion

		-- Se Insertan los Lotes.
		Insert Into MovtosInv_Det_CodigosEAN_Lotes 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado )  				
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdSubFarmacia, @FolioEntradaConsignacion, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Cantidad, Costo, Importe, 0 as Existencia, @Status, @Actualizado
		From MovtosInv_Det_CodigosEAN_Lotes(NoLock)
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaConsignacion

		-- Se Insertan las Ubicaciones.
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado ) 		
		Select	@IdEmpresaNueva, IdEstado, @IdFarmaciaNueva, IdSubFarmacia, @FolioEntradaConsignacion, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Cantidad, 0 as Existencia, @Status, @Actualizado
		From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(NoLock)
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioSalidaConsignacion

		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresaNueva + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmaciaNueva + char(39) + ', ' 
						+ char(39) + @FolioEntradaConsignacion + char(39) + ', '
						+ '1, 1'
		Exec (@sSqlTexto)

				/* 
					If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpFoliosCierre_InventarioInicial' and xType = 'U' ) 
					  Begin
						Drop Table tmpFoliosCierre_InventarioInicial 	
					  End
				*/ 


 


	--------------------------------------------------
	-- Se Insertan los Folios en una tabla temporal --
	--------------------------------------------------	
	Insert Into #tmpFoliosCierre_InventarioInicial 
	Select @FolioEntradaVenta as FolioEntradaVenta, @FolioEntradaConsignacion as FolioEntradaConsignacion 
	

	--------------------------------------------
	-- Se Devuelven los Folios de Movimientos --
	--------------------------------------------
	--Select	@FolioEntradaVenta as FolioEntradaVenta, @FolioEntradaConsignacion as FolioEntradaConsignacion
End 
Go--#SQL 


