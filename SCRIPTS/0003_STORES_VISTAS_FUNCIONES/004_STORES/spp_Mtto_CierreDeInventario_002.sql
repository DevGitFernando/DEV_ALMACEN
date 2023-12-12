If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario_002' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario_002 
Go--#SQL 

Create Proc spp_Mtto_CierreDeInventario_002  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182',  
	@IdPersonal varchar(6) = '0001', @FechaSistema varchar(10) = '2012-02-20' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
	
Declare  
	@IdMovto_Cierre varchar(4), 
	@TipoSalida varchar(1),
	@FolioSalidaVenta varchar(30), 		
	@FolioSalidaConsignacion varchar(30),
	@SubTotal Numeric(14,4),
	@Iva Numeric(14,4),
	@Total Numeric(14,4),
	@sSqlTexto varchar(8000),  
	@iLargoFolios int 

	Set @IdMovto_Cierre = 'SCI' 
	Set @TipoSalida = 'S' 
	Set @FolioSalidaVenta = '' 
	Set @FolioSalidaConsignacion = '' 	
	Set @sSqlTexto = ''

	Set @SubTotal = 0.0000
	Set	@Iva = 0.0000
	Set	@Total = 0.0000
	Set @Actualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso  ( 0 - 3 ) 
	Set @iLargoFolios = 6 


	------------------------------------------------------------------------------------------
	-- Se actualiza el Status de los Productos para que no se puede hacer ningun movimiento --
	------------------------------------------------------------------------------------------
	Update FarmaciaProductos Set Status = 'I'
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	----------------------------------------------------------------
	-- Se obtienen los Productos, CodigosEAN, Lotes y Ubicaciones --
	----------------------------------------------------------------

	Select	F.*, P.TasaIva, Cast(0.0000 as Numeric(14,4)) as SalidaVenta, Cast(0.000 as Numeric(14,4)) as SalidaConsignacion,
			Cast(0.0000 as Numeric(14,4)) as SubTotalVenta, Cast(0.0000 as Numeric(14,4)) as IvaVenta, Cast(0.0000 as Numeric(14,4)) as TotalVenta,
			Cast(0.0000 as Numeric(14,4)) as SubTotalConsignacion, Cast(0.0000 as Numeric(14,4)) as IvaConsignacion, Cast(0.0000 as Numeric(14,4)) as TotalConsignacion
	Into #tmpProductos
	From FarmaciaProductos F(NoLock) 
	Inner Join vw_Productos P(NoLock) On ( F.IdProducto = P.IdProducto ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And Existencia > 0

	Select	F.*, Cast(0.0000 as Numeric(14,4)) as Costo, P.TasaIva, 
			Cast(0.000 as Numeric(14,4)) as SalidaVenta,  Cast(0.000 as Numeric(14,4)) as SalidaConsignacion,
			Cast(0.0000 as Numeric(14,4)) as SubTotalVenta, Cast(0.0000 as Numeric(14,4)) as IvaVenta, Cast(0.0000 as Numeric(14,4)) as TotalVenta,
			Cast(0.0000 as Numeric(14,4)) as SubTotalConsignacion, Cast(0.0000 as Numeric(14,4)) as IvaConsignacion, Cast(0.0000 as Numeric(14,4)) as TotalConsignacion
	Into #tmpCodigosEAN
	From FarmaciaProductos_CodigoEAN F(NoLock) 
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And Existencia > 0

	Select	F.*, Cast(0.0000 as Numeric(14,4)) as Costo, P.TasaIva 
	Into #tmpLotes 
	From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And Existencia > 0

	Select	F.*, Cast(0.0000 as Numeric(14,4)) as Costo, L.TasaIva 
	Into #tmpUbicaciones 
	From #tmpLotes L(NoLock)
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F(NoLock) 
		On ( L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
			And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote )
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia And F.Existencia > 0		

	-- Se actualizan los costos
	Update C Set Costo = P.UltimoCosto
	From #tmpCodigosEAN C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto )
	
	Update C Set Costo = P.UltimoCosto
	From #tmpLotes C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto )

	Update C Set Costo = P.UltimoCosto
	From #tmpUbicaciones C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto )

	-----------------------------------------
	-- Se marcan los lotes de consignacion --
	----------------------------------------- 

	Update #tmpLotes Set EsConsignacion = 1 Where ClaveLote like '%*%'
	Update #tmpUbicaciones Set EsConsignacion = 1 Where ClaveLote like '%*%'	
	
	-- Se seleccionan los Lotes de Venta y de Consignacion.
	Select *  --- , identity(int, 1, 1) as keyx  
	Into #tmpLotesVenta 
	From #tmpLotes (noLock) 
	Where EsConsignacion = 0 
	Order By IdProducto, CodigoEAN, ClaveLote 


----	select * 
----	into tmpLotesVenta
----	from #tmpLotesVenta 
----	
----	Exec sp_help tmpLotesVenta 


	Select * 
	Into #tmpLotesConsignacion  
	From #tmpLotes (noLock) 
	Where EsConsignacion = 1

	-- Se seleccionan las Ubicaciones de Venta y Consignacion.
	Select * 
	Into #tmpUbicacionesVenta 
	From #tmpUbicaciones (noLock) 
	Where EsConsignacion = 0 

	Select * 
	Into #tmpUbicacionesConsignacion  
	From #tmpUbicaciones (noLock) 
	Where EsConsignacion = 1
	
	----------------------------------------------------------------
	-- Se obtienen los Totales de Salidas de Venta y Consignacion --
	----------------------------------------------------------------

	-- Se obtienen los totales de los CodigosEAN
	Update F Set SalidaVenta = IsNull( ( Select Sum( Existencia ) From #tmpLotesVenta L(NoLock) Where F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado And F.IdFarmacia = L.IdFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN Group By L.IdProducto, L.CodigoEAN ), 0 )
	From #tmpCodigosEAN F(NoLock)

	Update F Set SalidaConsignacion = IsNull( ( Select Sum( Existencia ) From #tmpLotesConsignacion L(NoLock) Where F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado And F.IdFarmacia = L.IdFarmacia And F.IdProducto = L.IdProducto And F.CodigoEAN = L.CodigoEAN Group By L.IdProducto, L.CodigoEAN ), 0 )
	From #tmpCodigosEAN F(NoLock)

	-- Se obtiene los totales de los Productos
	Update F Set SalidaVenta = IsNull( ( Select Sum( SalidaVenta ) From #tmpCodigosEAN L(NoLock) Where F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado And F.IdFarmacia = L.IdFarmacia And F.IdProducto = L.IdProducto  Group By IdProducto ), 0 )
	From #tmpProductos F(NoLock)

	Update F Set SalidaConsignacion = IsNull( ( Select Sum( SalidaConsignacion ) From #tmpCodigosEAN L(NoLock) Where F.IdEmpresa = L.IdEmpresa And F.IdEstado = L.IdEstado And F.IdFarmacia = L.IdFarmacia And F.IdProducto = L.IdProducto Group By L.IdProducto ), 0 )
	From #tmpProductos F(NoLock)

	----------------------------------------------------------
	-- Se obtiene el SubTotal, Iva y Total de cada Producto --
	----------------------------------------------------------
	Update F Set SubTotalVenta = IsNull( ( SalidaVenta * Costo ), 0 ), SubTotalConsignacion = IsNull( ( SalidaConsignacion * Costo ), 0 )
	From #tmpCodigosEAN F(NoLock)

	Update F Set 
			IvaVenta = IsNull( ( ( (1 + ( Cast(TasaIva as Numeric(14,4)) / 100 ) )* SubTotalVenta ) - SubTotalVenta ), 0 ),  
			IvaConsignacion = IsNull( ( ( (1 + ( Cast(TasaIva as Numeric(14,4)) / 100 ) )* SubTotalConsignacion ) - SubTotalConsignacion ), 0 )		
	From #tmpCodigosEAN F(NoLock)	

	Update F Set TotalVenta = IsNull( ( SubTotalVenta + IvaVenta ), 0 ), 
				 TotalConsignacion = IsNull( ( SubTotalConsignacion + IvaConsignacion ), 0 )
	From #tmpCodigosEAN F(NoLock)

 
	-------------------------------------------
	-- Se Insertan los movimientos de Salida --
	-------------------------------------------

		------------------------------------
		-- Se Insertan la Salida de Venta --
		------------------------------------

		-- Se obtiene el Folio de Salida de Venta
		Select @FolioSalidaVenta = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 
		From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdMovto_Cierre 
			   
		-- Actualizar el registro de folios 
		Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioSalidaVenta, 1) as int), Actualizado = 0 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdMovto_Cierre 

		-- Se asegura que el numero de Folio sea Valido.
		Set @FolioSalidaVenta = IsNull(@FolioSalidaVenta, '1') 
		Set @FolioSalidaVenta = @IdMovto_Cierre + right(replicate('0', @iLargoFolios) + @FolioSalidaVenta, @iLargoFolios) 	

		-- Se obtiene el SubTotal, Iva y Total del Encabezado.
		Set @SubTotal = 0.0000
		Set	@Iva = 0.0000
		Set	@Total = 0.0000

		Select @SubTotal = IsNull(Sum( SubTotalVenta ), 0), @Iva = IsNull(Sum(IvaVenta), 0), @Total = IsNull(Sum(TotalVenta), 0)  
		From #tmpCodigosEAN(NoLock)
		Where SalidaVenta > 0 
	
		
		-- Se Inserta el Encabezado. 
		Insert Into MovtosInv_Enc 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, 
				Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado ) 
		Select	@IdEmpresa, @IdEstado, @IdFarmacia, @FolioSalidaVenta, @IdMovto_Cierre, @TipoSalida, @FechaSistema, GetDate() as FechaRegistro, 
				'' as Referencia, 'N' as MovtoAplicado, @IdPersonal, 'CIERRE DE INVENTARIO DE VENTA', @SubTotal, @Iva, @Total, 
				'A' as Status, @Actualizado   				
				
		-- Se Insertan los CodigosEAN.
		Insert Into MovtosInv_Det_CodigosEAN 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
		Select	IdEmpresa, IdEstado, IdFarmacia, @FolioSalidaVenta, IdProducto, CodigoEAN, @FechaSistema, 0 as UnidadDeSalida, TasaIva, 
				SalidaVenta, Costo, (SalidaVenta * Costo) as Importe, 0 as Existencia, Status, @Actualizado
		From #tmpCodigosEAN(NoLock)
		Where SalidaVenta > 0 
      		
		-- Se Insertan los Lotes. 
 		Insert Into MovtosInv_Det_CodigosEAN_Lotes 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado )  		
		Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioSalidaVenta, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Existencia as Cantidad, Costo, ( Existencia * Costo ) as Importe, 0 as Existencia, Status, 0 as Actualizado 
		From #tmpLotesVenta (NoLock) 


--   		sp_listacolumnas MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
 		  	 
		-- Se Insertan las Ubicaciones.
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado ) 
		Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioSalidaVenta, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Existencia as Cantidad, 0 as Existencia, Status, @Actualizado
		From #tmpUbicacionesVenta(NoLock) 


		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresa + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmacia + char(39) + ', ' 
						+ char(39) + @FolioSalidaVenta + char(39) + ', '
						+ '1, 1'
		Exec (@sSqlTexto) 



  
		-------------------------------------------
		-- Se Insertan la Salida de Consignacion --
		-------------------------------------------

		-- Se obtiene el Folio de Salida de Venta
		Select @FolioSalidaConsignacion = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 
		From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdMovto_Cierre 
			   
		-- Actualizar el registro de folios 
		Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioSalidaConsignacion, 1) as int), Actualizado = 0 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdMovto_Cierre 
	 

		-- Se asegura que el numero de Folio sea Valido.
		Set @FolioSalidaConsignacion = IsNull(@FolioSalidaConsignacion, '1') 
		Set @FolioSalidaConsignacion = @IdMovto_Cierre + right(replicate('0', @iLargoFolios) + @FolioSalidaConsignacion, @iLargoFolios) 	

		-- Se obtiene el SubTotal, Iva y Total del Encabezado.
		Set @SubTotal = 0.0000
		Set	@Iva = 0.0000
		Set	@Total = 0.0000

		Select @SubTotal = IsNull(Sum( SubTotalConsignacion ),0), @Iva = IsNull(Sum(IvaConsignacion),0), @Total = IsNull(Sum(TotalConsignacion), 0)  
		From #tmpCodigosEAN(NoLock) 
		Where SalidaConsignacion > 0 		

		-- Se Inserta el Encabezado.
		-- Insert Into MovtosInv_Enc   --- CIERRE DE INVENTARIO DE CONSIGNACION 		
		Insert Into MovtosInv_Enc 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, 
				Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado ) 
						
		Select	@IdEmpresa, @IdEstado, @IdFarmacia, @FolioSalidaConsignacion, @IdMovto_Cierre, @TipoSalida, @FechaSistema, GetDate() as FechaRegistro, 
				'' as Referencia, 'N' as MovtoAplicado, @IdPersonal, 'CIERRE DE INVENTARIO DE CONSIGNACION', @SubTotal, @Iva, @Total, 
				'A' as Status, @Actualizado
  
		-- Se Insertan los CodigosEAN.
		Insert Into MovtosInv_Det_CodigosEAN
			(	IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 		
		Select	IdEmpresa, IdEstado, IdFarmacia, @FolioSalidaConsignacion, IdProducto, CodigoEAN, @FechaSistema, 0 as UnidadDeSalida, TasaIva, 
				SalidaConsignacion, Costo, ( SalidaConsignacion * Costo ) as Importe, 0 as Existencia, Status, @Actualizado
		From #tmpCodigosEAN(NoLock)
		Where SalidaConsignacion > 0 

 
		-- Se Insertan los Lotes.
		Insert Into MovtosInv_Det_CodigosEAN_Lotes 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Cantidad, Costo, Importe, Existencia, Status, Actualizado )  				
		Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioSalidaConsignacion, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				Existencia as Cantidad, Costo, ( Existencia * Costo ) as Importe, 0 as Existencia, Status, @Actualizado
		From #tmpLotesConsignacion(NoLock)


		-- Se Insertan las Ubicaciones.
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
			(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado ) 		
		Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioSalidaConsignacion, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
				IdPasillo, IdEstante, IdEntrepaño, Existencia as Cantidad, 0 as Existencia, Status, @Actualizado
		From #tmpUbicacionesConsignacion(NoLock) 
 
		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresa + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmacia + char(39) + ', ' 
						+ char(39) + @FolioSalidaConsignacion + char(39) + ', '
						+ '1, 1'
		Exec (@sSqlTexto) 
 		

	---------------------------------------------
	-- Se Actualiza el Status y el Actualizado --
	---------------------------------------------
	Update FarmaciaProductos Set Actualizado = 0 Where IdEmpresa = @IdEmpresa And IdFarmacia = @IdFarmacia And IdEstado = @IdEstado

/* 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpFoliosCierre' and xType = 'U' ) 
	  Begin
		Drop Table tmpFoliosCierre 	
	  End
*/ 

	--------------------------------------------------
	-- Se Insertan los Folios en una tabla temporal --
	--------------------------------------------------	
	Insert Into #tmpFoliosCierre 
	Select @FolioSalidaVenta as FolioSalidaVenta,  @FolioSalidaConsignacion as FolioSalidaConsignacion 
	

	--------------------------------------------
	-- Se Devuelven los Folios de Movimientos --
	--------------------------------------------
	--Select	@FolioSalidaVenta as FolioSalidaVenta,  @FolioSalidaConsignacion as FolioSalidaConsignacion

 			
End 
Go--#SQL 

