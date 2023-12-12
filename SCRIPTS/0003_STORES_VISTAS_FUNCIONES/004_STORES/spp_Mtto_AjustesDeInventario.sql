If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_AjustesDeInventario' and xType = 'P' ) 
   Drop Proc spp_Mtto_AjustesDeInventario 
Go--#SQL 

Create Proc spp_Mtto_AjustesDeInventario  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0012', @Poliza varchar(8) = '00000001', 
	@IdPersonal varchar(6) = '000001', @iMostrarResultado int = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @Actualizado smallint 
	
Declare @MovtoEntrada varchar(4), 
		@MovtoSalida varchar(4),   
		@FolioVtaEntrada varchar(20), 
		@FolioVtaSalida varchar(20), 		
		@FolioConsgEntrada varchar(20), 
		@FolioConsgSalida varchar(20),
		@Registros int,
		@sSqlTexto varchar(8000) 

	Set @MovtoEntrada = 'EAI'
	Set @MovtoSalida = 'SAI' 
	Set @FolioVtaEntrada = '' 
	Set @FolioVtaSalida = '' 	
	Set @FolioConsgEntrada = '' 
	Set @FolioConsgSalida = '' 
	Set @Registros = 0
	Set @sSqlTexto = ''

	Set @Actualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso  ( 0 - 3 ) 

	------------------------------------------------------
	-- Se obtienen los Lotes y Ubicaciones de la Poliza --
	------------------------------------------------------

	Select --U.*, D.TasaIva, 0 as TipoMovto   
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, U.Poliza, U.IdProducto, U.CodigoEAN, U.ClaveLote, U.EsConsignacion, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, sum(U.ExistenciaSistema) as ExistenciaSistema, 
		max(U.Costo) as Costo, sum(U.Importe) as Importe, sum(U.ExistenciaFisica) as ExistenciaFisica, 
		sum(U.Diferencia) as Diferencia, U.Referencia, U.Status, D.TasaIva, 0 as TipoMovto   
	Into #tmpUbicaciones 
	From AjustesInv_Det_Lotes_Ubicaciones U (noLock) 
	Inner Join AjustesInv_Det_Lotes L (noLock) 
		On ( U.IdEmpresa = L.IdEmpresa and U.IdEstado = L.IdEstado and U.IdFarmacia = L.IdFarmacia and U.IdSubFarmacia = L.IdSubFarmacia 
			 and U.Poliza = L.Poliza and U.IdProducto = L.IdProducto and U.CodigoEAN = L.CodigoEAN And U.ClaveLote = L.ClaveLote )
	Inner Join AjustesInv_Det D (noLock) On ( L.IdEmpresa = D.IdEmpresa and L.IdEstado = D.IdEstado and L.IdFarmacia = D.IdFarmacia 
			 and L.Poliza = D.Poliza and L.IdProducto = D.IdProducto and L.CodigoEAN = D.CodigoEAN )	
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.Poliza = @Poliza --And L.ExistenciaFisica > 0 
		 -- and U.ClaveLote = '10451' 
	Group by 
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, U.Poliza, U.IdProducto, U.CodigoEAN, U.ClaveLote, U.EsConsignacion, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, U.Referencia, U.Status, D.TasaIva   
	
------------------------------------------------- 	
	Select 
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.Poliza, 
		L.IdProducto, L.CodigoEAN, L.ClaveLote, L.EsConsignacion, L.ExistenciaSistema, L.Costo, L.Importe, 
		L.ExistenciaFisica, L.Diferencia, L.Referencia, L.Status, L.Keyx, L.Actualizado, 
		D.TasaIva, 0 as TipoMovto   
	Into #tmpLotes 
	From AjustesInv_Det_Lotes L (noLock) 
	Inner Join AjustesInv_Det D (noLock) 
		On ( L.IdEmpresa = D.IdEmpresa and L.IdEstado = D.IdEstado and L.IdFarmacia = D.IdFarmacia 
			 and L.Poliza = D.Poliza and L.IdProducto = D.IdProducto and L.CodigoEAN = D.CodigoEAN )	
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.Poliza = @Poliza --And L.ExistenciaFisica > 0
		-- and L.ClaveLote = '10451'  


-------------------------- SOLO ALMACENES   
	Set @Registros = 0
	Select @Registros = Count(*) From #tmpUbicaciones 
		
	-- Se verifica que haya datos en el encabezado de Venta de Entrada
	If @Registros > 0 
	Begin 
		Delete From #tmpLotes 
		
		Update #tmpUbicaciones Set EsConsignacion = 1 Where ClaveLote like '%*%' 
		Update #tmpUbicaciones Set Diferencia = (ExistenciaFisica - ExistenciaSistema)
		Update #tmpUbicaciones Set TipoMovto = (case when Diferencia >= 0 then 1 else 2 end)
		Update #tmpUbicaciones Set TipoMovto = 0 Where Diferencia = 0 		
			
		
		Insert Into #tmpLotes ( 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, 
			EsConsignacion, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Referencia, Status, Keyx, Actualizado, TasaIva, TipoMovto ) 
		Select 
			U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, U.Poliza, U.IdProducto, U.CodigoEAN, U.ClaveLote, U.EsConsignacion, 
			sum(U.ExistenciaSistema) as ExistenciaSistema, 
			max(U.Costo) as Costo, sum(U.Importe) as Importe, sum(U.ExistenciaFisica) as ExistenciaFisica, 
			sum(U.Diferencia) as Diferencia, U.Referencia, U.Status, 0, 0, U.TasaIva, TipoMovto
		From #tmpUbicaciones U 
		Group by 
			U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, U.Poliza, U.IdProducto, U.CodigoEAN, U.ClaveLote, U.EsConsignacion, 
			U.Referencia, U.Status, U.TasaIva, TipoMovto
		
		
		-- select * from #tmpLotes 
		-- select * from #tmpUbicaciones 		
		
--		sp_listacolumnas AjustesInv_Det_Lotes 
		
	End 

-------------------------- SOLO ALMACENES   

--	select * from #tmpLotes 
--	select * from #tmpUbicaciones 


	-----------------------------------------
	-- Se marcan los lotes de consignacion --
	----------------------------------------- 
	
--	select top 1 * from #tmpUbicaciones 
	

	Update #tmpUbicaciones Set EsConsignacion = 1 Where ClaveLote like '%*%' 
	Update #tmpLotes Set EsConsignacion = 1 Where ClaveLote like '%*%'

	
	Update #tmpUbicaciones Set Diferencia = (ExistenciaFisica - ExistenciaSistema)
	Update #tmpUbicaciones Set TipoMovto = (case when Diferencia >= 0 then 1 else 2 end)
	Update #tmpUbicaciones Set TipoMovto = 0 Where Diferencia = 0 


	Update #tmpLotes Set Diferencia = (ExistenciaFisica - ExistenciaSistema)
	Update #tmpLotes Set TipoMovto = (case when Diferencia >= 0 then 1 else 2 end)
	Update #tmpLotes Set TipoMovto = 0 Where Diferencia = 0 
  
  
  	
	
	-- Se seleccionan los Ajustes de Venta.
	Select * 
	Into #tmpUbicacionesVenta 
	From #tmpUbicaciones (noLock) 
	Where EsConsignacion = 0 

	Select * 
	Into #tmpUbicacionesConsignacion  
	From #tmpUbicaciones (noLock) 
	Where EsConsignacion = 1
	
	-- Se seleccionan los Ajustes de Consignacion.
	Select * 
	Into #tmpLotesVenta 
	From #tmpLotes (noLock) 
	Where EsConsignacion = 0 

	Select * 
	Into #tmpLotesConsignacion  
	From #tmpLotes (noLock) 
	Where EsConsignacion = 1
	
--	select * from #tmpLotes 
--	select * from #tmpUbicaciones 	
	
	-------------------------------
	-- Se Totalizan los Detalles --
	-------------------------------

	-- Se totalizan las Ubicaciones de la Venta
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		1 as TipoMovto
	Into #tmpUbicacionesDet 
	From #tmpUbicacionesVenta   
	Where TipoMovto = 1 
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  


	-- Se totalizan los Ubicaciones de la venta
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		2 as TipoMovto
	Into #tmpUbicacionesDet_Salida  
	From #tmpUbicacionesVenta   
	Where TipoMovto = 2 
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  

	-- Se totalizan los detalles de la Venta
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		1 as TipoMovto
	Into #tmpVentaDet 
	From #tmpLotesVenta   
	Where TipoMovto = 1 
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  

	-- Se totalizan los detalles de la venta
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		2 as TipoMovto
	Into #tmpVentaDet_Salida  
	From #tmpLotesVenta   
	Where TipoMovto = 2 
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  



----	-- Se pone el Tipo de Movimiento: 1.- Entrada, 2.- Salida, 0.- No se tomara en cuenta
----	Update #tmpVentaDet Set TipoMovto = (case when Diferencia >= 0 then 1 else 2 end)
----	Update #tmpVentaDet Set TipoMovto = 0 Where Diferencia = 0 	

	-- Se totaliza los detalles de consignacion
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		1 as TipoMovto
	Into #tmpUbicaciones_ConsignacionDet 
	From #tmpUbicacionesConsignacion   
	Where TipoMovto = 1 	
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  


	-- Se totaliza los detalles de consignacion
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		2 as TipoMovto
	Into #tmpUbicaciones_ConsignacionDet_Salida 
	From #tmpUbicacionesConsignacion   
	Where TipoMovto = 2 	
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  

	-- Se totaliza los detalles de consignacion
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		1 as TipoMovto
	Into #tmpConsignacionDet 
	From #tmpLotesConsignacion   
	Where TipoMovto = 1 	
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  


	-- Se totaliza los detalles de consignacion
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva, AVG(Costo) As Costo, 
		sum(ExistenciaSistema) as ExistenciaSistema, sum(ExistenciaFisica) as ExistenciaFisica, sum(Diferencia) as Diferencia, 
		(abs(sum(Diferencia)) * AVG(Costo)) as SubTotal, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( (TasaIva / 100.00))) as ImpteIva, 
		((abs(sum(Diferencia)) * AVG(Costo)) * ( 1 + (TasaIva / 100.00))) as Importe,
		2 as TipoMovto
	Into #tmpConsignacionDet_Salida 
	From #tmpLotesConsignacion   
	Where TipoMovto = 2 	
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, TasaIva--, Costo  

----	-- Se pone el Tipo de Movimiento: 1.- Entrada, 2.- Salida, 0.- No se tomara en cuenta
----	Update #tmpConsignacionDet Set TipoMovto = (case when Diferencia >= 0 then 1 else 2 end)
----	Update #tmpConsignacionDet Set TipoMovto = 0 Where Diferencia = 0
	
	----------------------------------
	-- Se Totalizan los Encabezados --
	---------------------------------- 
	
	-- Se obtienen los encabezados de Venta
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, 
		sum(SubTotal) as SubTotal, sum(ImpteIva) as Iva, sum(Importe) as Total  
	Into #tmpVentaEntrada
	From #tmpVentaDet
	Where TipoMovto = 1
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza 

	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, 
		sum(SubTotal) as SubTotal, sum(ImpteIva) as Iva, sum(Importe) as Total  
	Into #tmpVentaSalida
	From #tmpVentaDet_Salida  ---- #tmpVentaDet
	Where TipoMovto = 2
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza 
	
	-- Se obtienen los encabezados de Consignacion
	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, 
		sum(SubTotal) as SubTotal, sum(ImpteIva) as Iva, sum(Importe) as Total  
	Into #tmpConsignacionEntrada 
	From #tmpConsignacionDet 
	Where TipoMovto = 1  
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza 

	Select IdEmpresa, IdEstado, IdFarmacia, Poliza, 
		sum(SubTotal) as SubTotal, sum(ImpteIva) as Iva, sum(Importe) as Total  
	Into #tmpConsignacionSalida 
	From #tmpConsignacionDet_Salida ---- #tmpConsignacionDet  
	Where TipoMovto = 2 
	Group by IdEmpresa, IdEstado, IdFarmacia, Poliza 

	------------------------------------------------------------------
	-- Se Insertan los Movimientos de Entrada de Productos de Venta --
	------------------------------------------------------------------
	Set @MovtoEntrada = 'EAI'
	Set @MovtoSalida = 'SAI' 
	Set @FolioVtaEntrada = '' 
	Set @FolioVtaSalida = '' 	
	Set @FolioConsgEntrada = '' 
	Set @FolioConsgSalida = '' 

	Set @Registros = 0
	Select @Registros = Count(*) From #tmpVentaEntrada
		
	-- Se verifica que haya datos en el encabezado de Venta de Entrada
	if @Registros > 0
	  Begin
		Select @FolioVtaEntrada = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 10 From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @MovtoEntrada 

		Set @FolioVtaEntrada = IsNull(@FolioVtaEntrada, '1') 
		Set @FolioVtaEntrada = @MovtoEntrada + right(replicate('0', 8) + @FolioVtaEntrada, 8) 
		
		-- Se inserta el Encabezado 	
		Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
			Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioVtaEntrada, @MovtoEntrada, 'E', 
			@Poliza, @IdPersonal, 'Ajuste de Entrada Inventario de Venta', SubTotal, Iva, Total 
		From #tmpVentaEntrada 

		-- Se inserta el Detalles
		Insert Into MovtosInv_Det_CodigosEAN( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, 
			TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioVtaEntrada, IdProducto, CodigoEAN, 0, 
			TasaIva, Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpVentaDet (NoLock)
		Where TipoMovto = 1

		-- Se inserta el Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			Cantidad, Costo, Importe, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioVtaEntrada, IdProducto, CodigoEAN, ClaveLote,
			Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpLotesVenta
		Where TipoMovto = 1


--------		Select * 
--------		From #tmpLotesVenta
--------		Where TipoMovto = 1 
--------
--------
--------		Select * 
--------		From #tmpUbicacionesVenta U 
--------		Where TipoMovto = 1
--------			and not exists 
--------			( 
--------				select * 
--------				From #tmpLotesVenta V 
--------				Where V.TipoMovto = 1 
--------					and U.IdFarmacia = V.IdFarmacia and U.IdSubFarmacia = V.IdSubFarmacia 
--------					and U.IdProducto = V.IdProducto and U.CodigoEAN = V.CodigoEAN and U.ClaveLote = V.ClaveLote 
--------			) 	
		
		-- Se inserta la Ubicacion del Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioVtaEntrada, IdProducto, CodigoEAN, ClaveLote,
			IdPasillo, IdEstante, IdEntrepaño, Abs(Diferencia), ExistenciaSistema, 'A', 0
		From #tmpUbicacionesVenta
		Where TipoMovto = 1



		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresa + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmacia + char(39) + ', ' 
						+ char(39) + @FolioVtaEntrada + char(39) + ', '
						+ '1, 1 '
		Exec (@sSqlTexto)
	  End

	-------------------------------------------------------------------------
	-- Se Insertan los Movimientos de Entrada de Productos de Consignacion --
	-------------------------------------------------------------------------
 
	Set @Registros = 0
	Select @Registros = Count(*) From #tmpConsignacionEntrada
		
	-- Se verifica que haya datos en el encabezado de Venta de Entrada
	if @Registros > 0
	  Begin	
		Select @FolioConsgEntrada = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @MovtoEntrada 

		Set @FolioConsgEntrada = IsNull(@FolioConsgEntrada, '1') 
		Set @FolioConsgEntrada = @MovtoEntrada + right(replicate('0', 8) + @FolioConsgEntrada, 8)    
		
		-- Se inserta el Encabezado 	
		Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
			Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioConsgEntrada, @MovtoEntrada, 'E', 
			@Poliza, @IdPersonal, 'Ajuste de Entrada Inventario de Consignacion', SubTotal, Iva, Total 
		From #tmpConsignacionEntrada 	

		-- Se inserta el Detalles
		Insert Into MovtosInv_Det_CodigosEAN( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, 
			TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioConsgEntrada, IdProducto, CodigoEAN, 0, 
			TasaIva, Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpConsignacionDet(NoLock)
		Where TipoMovto = 1

		-- Se inserta el Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			Cantidad, Costo, Importe, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioConsgEntrada, IdProducto, CodigoEAN, ClaveLote,
			Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpLotesConsignacion
		Where TipoMovto = 1

		-- Se inserta la Ubicacion del Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioConsgEntrada, IdProducto, CodigoEAN, ClaveLote,
			IdPasillo, IdEstante, IdEntrepaño, Abs(Diferencia), ExistenciaSistema, 'A', 0
		From #tmpUbicacionesConsignacion
		Where TipoMovto = 1

		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresa + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmacia + char(39) + ', ' 
						+ char(39) + @FolioConsgEntrada + char(39) + ', '
						+ '1, 1 '
		Exec (@sSqlTexto)

	  End

	-----------------------------------------------------------------
	-- Se Insertan los Movimientos de Salida de Productos de Venta --
	-----------------------------------------------------------------

	Set @Registros = 0
	Select @Registros = Count(*) From #tmpVentaSalida
		
	-- Se verifica que haya datos en el encabezado de Venta de Entrada
	if @Registros > 0
	  Begin	
		Select @FolioVtaSalida = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @MovtoSalida 

		Set @FolioVtaSalida = IsNull(@FolioVtaSalida, '1') 
		Set @FolioVtaSalida = @MovtoSalida + right(replicate('0', 8) + @FolioVtaSalida, 8)    
		
		-- Se inserta el Encabezado 	
		Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
			Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioVtaSalida, @MovtoSalida, 'S', 
			@Poliza, @IdPersonal, 'Ajuste de Salida Inventario de Venta', SubTotal, Iva, Total 
		From #tmpVentaSalida 		

		-- Se inserta el Detalles
		Insert Into MovtosInv_Det_CodigosEAN( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, 
			TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioVtaSalida, IdProducto, CodigoEAN, 0, 
			TasaIva, Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpVentaDet_Salida (NoLock) -- #tmpVentaDet
		Where TipoMovto = 2

		-- Se inserta el Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			Cantidad, Costo, Importe, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioVtaSalida, IdProducto, CodigoEAN, ClaveLote,
			Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpLotesVenta
		Where TipoMovto = 2

		-- Se inserta la Ubicacion del Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioVtaSalida, IdProducto, CodigoEAN, ClaveLote,
			IdPasillo, IdEstante, IdEntrepaño, Abs(Diferencia), ExistenciaSistema, 'A', 0
		From #tmpUbicacionesVenta
		Where TipoMovto = 2

		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresa + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmacia + char(39) + ', ' 
						+ char(39) + @FolioVtaSalida + char(39) + ', '
						+ '1, 1 '
		Exec (@sSqlTexto)
	  End

	------------------------------------------------------------------------
	-- Se Insertan los Movimientos de Salida de Productos de Consignacion --
	------------------------------------------------------------------------

	Set @Registros = 0
	Select @Registros = Count(*) From #tmpConsignacionSalida
		
	-- Se verifica que haya datos en el encabezado de Venta de Entrada
	if @Registros > 0
	  Begin	
		Select @FolioConsgSalida = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @MovtoSalida 

		Set @FolioConsgSalida = IsNull(@FolioConsgSalida, '1') 
		Set @FolioConsgSalida = @MovtoSalida + right(replicate('0', 8) + @FolioConsgSalida, 8)    
		
		-- Se inserta el Encabezado 	
		Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
			Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioConsgSalida, @MovtoSalida, 'S', 
			@Poliza, @IdPersonal, 'Ajuste de Salida Inventario de Consignacion', SubTotal, Iva, Total 
		From #tmpConsignacionSalida 		

		-- Se inserta el Detalles
		Insert Into MovtosInv_Det_CodigosEAN( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, 
			TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @FolioConsgSalida, IdProducto, CodigoEAN, 0, 
			TasaIva, Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpConsignacionDet_Salida -- #tmpConsignacionDet(NoLock)
		Where TipoMovto = 2

		-- Se inserta el Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			Cantidad, Costo, Importe, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioConsgSalida, IdProducto, CodigoEAN, ClaveLote,
			Abs(Diferencia), Costo, Importe, ExistenciaSistema, 'A', 0
		From #tmpLotesConsignacion
		Where TipoMovto = 2

		-- Se inserta la Ubicacion del Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioConsgSalida, IdProducto, CodigoEAN, ClaveLote,
			IdPasillo, IdEstante, IdEntrepaño, Abs(Diferencia), ExistenciaSistema, 'A', 0
		From #tmpUbicacionesConsignacion
		Where TipoMovto = 2

		-- Se aplica la Existencia 
		Set @sSqlTexto = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
						+ char(39) + @IdEmpresa + char(39) + ', ' 
						+ char(39) + @IdEstado + char(39) + ', ' +
						+ char(39) + @IdFarmacia + char(39) + ', ' 
						+ char(39) + @FolioConsgSalida + char(39) + ', '
						+ '1, 1 '
		Exec (@sSqlTexto)
	  End

	-----------------------------------------
	-- Se Actualizan los Folios utilizados --
	-----------------------------------------
 
--	Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioMovtoInv, 1) as int), Actualizado = 0 
--	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @MovtoEntrada
--
--	Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioMovtoInv, 1) as int), Actualizado = 0 
--	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @MovtoSalida 


	------------------------------------------------
	-- Se actualizan las Referencias de la Poliza --
	------------------------------------------------

	-- Se actualiza la referencia de Ubicaciones de Entrada de Venta.
	Update A Set Referencia = @FolioVtaEntrada, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes_Ubicaciones A(NoLock)
	Inner Join #tmpUbicacionesVenta V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote And A.IdPasillo = V.IdPasillo And A.IdEstante = V.IdEstante And A.IdEntrepaño = V.IdEntrepaño )
	Where V.TipoMovto = 1 

	-- Se actualiza la referencia de Entrada de Venta.
	Update A Set Referencia = @FolioVtaEntrada, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes A(NoLock)
	Inner Join #tmpLotesVenta V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote )
	Where V.TipoMovto = 1 

	
	-- Se actualiza la referencia de Ubicacion de Entrada de Venta de Consignacion.
	Update A Set Referencia = @FolioConsgEntrada, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes_Ubicaciones A(NoLock)
	Inner Join #tmpUbicacionesConsignacion V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote And A.IdPasillo = V.IdPasillo And A.IdEstante = V.IdEstante And A.IdEntrepaño = V.IdEntrepaño )
	Where V.TipoMovto = 1 

	-- Se actualiza la referencia de Entrada de Venta de Consignacion.
	Update A Set Referencia = @FolioConsgEntrada, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes A(NoLock)
	Inner Join #tmpLotesConsignacion V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote )
	Where V.TipoMovto = 1 


	-- Se actualiza la referencia de las Ubicaciones de Salida de Venta. 
	Update A Set Referencia = @FolioVtaSalida, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes_Ubicaciones A(NoLock)
	Inner Join #tmpUbicacionesVenta V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote And A.IdPasillo = V.IdPasillo And A.IdEstante = V.IdEstante And A.IdEntrepaño = V.IdEntrepaño )
	Where V.TipoMovto = 2 

	-- Se actualiza la referencia de los Salida de Venta. 
	Update A Set Referencia = @FolioVtaSalida, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes A(NoLock)
	Inner Join #tmpLotesVenta V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote )
	Where V.TipoMovto = 2 


	-- Se actualiza la referencia de Ubicaciones de Salida de Venta de Consignacion. 
	Update A Set Referencia = @FolioConsgSalida, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes_Ubicaciones A(NoLock)
	Inner Join #tmpUbicacionesConsignacion V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote And A.IdPasillo = V.IdPasillo And A.IdEstante = V.IdEstante And A.IdEntrepaño = V.IdEntrepaño )
	Where V.TipoMovto = 2 

	-- Se actualiza la referencia de Salida de Venta de Consignacion. 
	Update A Set Referencia = @FolioConsgSalida, Diferencia = Abs(V.Diferencia), Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes A(NoLock)
	Inner Join #tmpLotesConsignacion V(NoLock) On ( A.IdEmpresa = V.IdEmpresa And A.IdEstado = V.IdEstado And A.IdFarmacia = V.IdFarmacia 
		And A.IdSubFarmacia = V.IdSubFarmacia And A.Poliza = V.Poliza And A.IdProducto = V.IdProducto And A.CodigoEAN = V.CodigoEAN 
		And A.ClaveLote = V.ClaveLote )
	Where V.TipoMovto = 2 


	-- Se actualiza la diferencia de los detalles de la Poliza.
	Update AjustesInv_Det Set Diferencia = Abs((ExistenciaFisica - ExistenciaSistema)), Actualizado = @Actualizado 
	From AjustesInv_Det(NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Poliza = @Poliza And ExistenciaFisica > 0

	----------------------------------------------------------------------------
	-- Se Actualiza el Status de la Poliza y se asignan los folios utilizados --
	----------------------------------------------------------------------------
	Update AjustesInv_Enc Set MovtoAplicado = 'S', 
		  	FolioVentaEntrada = @FolioVtaEntrada, 
			FolioVentaSalida = @FolioVtaSalida,
			FolioConsignacionEntrada = @FolioConsgEntrada,  
			FolioConsignacionSalida = @FolioConsgSalida, 
			Actualizado = @Actualizado  		  
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And Poliza = @Poliza 

	----------------------------------------------------------------------------
	-- Se Actualiza el Actualizado de la Poliza                               --
	----------------------------------------------------------------------------
	Update A Set Actualizado = @Actualizado 
	From AjustesInv_Enc A (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Poliza = @Poliza 
		
	Update A Set Actualizado = @Actualizado 
	From AjustesInv_Det A (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Poliza = @Poliza 
	
	Update A Set Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes A (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Poliza = @Poliza 	

	Update A Set Actualizado = @Actualizado 
	From AjustesInv_Det_Lotes_Ubicaciones A (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Poliza = @Poliza 			


	----------------------------------------------------------------------------
	-- Se Actualiza el Status de los Productos de la Poliza                   --
	----------------------------------------------------------------------------
	Update F Set Status = 'A', Actualizado = @Actualizado 
    From FarmaciaProductos F (NoLock) 
	Inner Join AjustesInv_Det A (NoLock) On 
	    ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto ) 
	Where A.IdEmpresa = @IdEmpresa And A.IdEstado = @IdEstado and A.IdFarmacia = @IdFarmacia And A.Poliza = @Poliza
 

	if @iMostrarResultado = 1 
	Begin 
		--------------------------------------------
		-- Se Devuelven los Folios de Movimientos --
		--------------------------------------------
		Select	@FolioVtaEntrada as FolioVentaEntrada, 
				@FolioVtaSalida as FolioVentaSalida, 
				@FolioConsgEntrada as FolioConsignacionEntrada, 
				@FolioConsgSalida as FolioConsignacionSalida  
	End 



----	Select * From #tmpVenta 
----	Select * From #tmpConsignacion
----	Select * From #tmpVentaDet 
----	Select * From #tmpConsignacionDet 	
------	Select * From #tmpLotesVenta 
------	Select * From #tmpLotesConsignacion 

End 
Go--#SQL