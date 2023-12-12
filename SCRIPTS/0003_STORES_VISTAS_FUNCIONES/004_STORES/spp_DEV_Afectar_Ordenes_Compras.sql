If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_DEV_Afectar_Ordenes_Compras' and xType = 'P' ) 
   Drop Proc spp_DEV_Afectar_Ordenes_Compras 
Go--#SQL	

Create Proc spp_DEV_Afectar_Ordenes_Compras ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FolioOrdenCompra varchar(30) = '00000005', @FolioDevolucion varchar(30) = '00000002' ) 
With Encryption 
As 
Begin 
Set NoCount On 

------------------------- 
	-- Obtener la informacion de la Devolucion 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioOrdenCompra as FolioOrdenCompra, SubTotal, Iva, Total
	Into #tmpDevEnc 
	From DevolucionesEnc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioOrdenCompra as FolioOrdenCompra, IdProducto, CodigoEAN, UnidadDeEntrada, 
		   Cant_Devuelta, PrecioCosto_Unitario, 
		   (Cant_Devuelta * PrecioCosto_Unitario) as SubTotal, TasaIva, ImpteIva, 
		   ((Cant_Devuelta * PrecioCosto_Unitario) + ImpteIva) as Importe 
	Into #tmpDevolucion 
	From DevolucionesDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 
	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioOrdenCompra as FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote, Cant_Devuelta
	Into #tmpDevolucionLotes  
	From DevolucionesDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 

	----  Ubicaciones
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioOrdenCompra as FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño, Cant_Devuelta
	Into #tmpDevolucionLotes_Ubicaciones  
	From DevolucionesDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion	
------------------------- 
	
--	select top 1 * from OrdenesDeComprasEnc (nolock) 	
--	select top 1 * from OrdenesDeComprasDet (nolock) 	
--	select top 1 * from OrdenesDeComprasDet_Lotes (nolock) 
--	select top 1 * from OrdenesDeComprasDet_Lotes_Ubicaciones (nolock)
--	sp_listacolumnas ComprasDet_Lotes  
	
------------------------- 
	-- Obtener la informacion de la Venta 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, Renglon, 1 As UnidadEntrada, 
		Cant_Recibida, Cant_Devuelta, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe 
	Into #tmpCompras 
	From OrdenesDeComprasDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioOrdenCompra = @FolioOrdenCompra 	

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote, Renglon, Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpComprasLotes  
	From OrdenesDeComprasDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioOrdenCompra = @FolioOrdenCompra

	--	Ubicaciones
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño,
	Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpComprasLotes_Ubicaciones  
	From OrdenesDeComprasDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioOrdenCompra = @FolioOrdenCompra 	
------------------------- 


	--- Calcular las cantidades a afectar  
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), 
		CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta), 
		SubTotal = (V.SubTotal -D.SubTotal), ImpteIva = (V.ImpteIva - D.ImpteIva), Importe = (V.Importe-D.Importe) 
	From #tmpCompras V 
	Inner Join #tmpDevolucion D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpComprasLotes V 
	Inner Join #tmpDevolucionLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

	--	Ubicaciones
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpComprasLotes_Ubicaciones V 
	Inner Join #tmpDevolucionLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			 and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )


 
	--- Iniciar la actualizacion de informacion 
	Update V Set Status = 'D', SubTotal = (V.SubTotal - D.SubTotal), Iva = (V.Iva - D.Iva), Total = (V.Total - D.Total) 
	From OrdenesDeComprasEnc V (NoLock) 
	Inner Join #tmpDevEnc D On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioOrdenCompra = D.FolioOrdenCompra ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida, 
		SubTotal = (V.SubTotal - D.SubTotal), ImpteIva = (V.ImpteIva - D.ImpteIva), Importe = (V.Importe - D.Importe) 
	From OrdenesDeComprasDet V 
	Inner Join #tmpCompras D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioOrdenCompra = D.FolioOrdenCompra 
			and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From OrdenesDeComprasDet_Lotes V 
	Inner Join #tmpComprasLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioOrdenCompra = D.FolioOrdenCompra and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

	--	Ubicaciones
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From OrdenesDeComprasDet_Lotes_Ubicaciones V 
	Inner Join #tmpComprasLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioOrdenCompra = D.FolioOrdenCompra and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )
		
	--  spp_DEV_Afectar_Ordenes_Compras
	
	----Corrigue el SubTotal, ImpteIva, Importe
	Update D
	Set SubTotal = (CantidadRecibida * CostoUnitario),
		ImpteIva = ((CantidadRecibida * CostoUnitario) * (TasaIva/100)),
		Importe  = ((CantidadRecibida * CostoUnitario) * (1 + (TasaIva/100)))
	From OrdenesDeComprasDet D
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioOrdenCompra = @FolioOrdenCompra


End 
Go--#SQL	

	