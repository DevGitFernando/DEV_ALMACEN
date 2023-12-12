If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_DEV_AfectarCompras' and xType = 'P' ) 
   Drop Proc spp_DEV_AfectarCompras 
Go--#SQL	

Create Proc spp_DEV_AfectarCompras ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FolioCompra varchar(30) = '00000005', @FolioDevolucion varchar(30) = '00000002' ) 
With Encryption 
As 
Begin 
Set NoCount On 

------------------------- 
	-- Obtener la informacion de la Devolucion 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioCompra as FolioCompra, SubTotal, Iva, Total
	Into #tmpDevEnc 
	From DevolucionesEnc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioCompra as FolioCompra, IdProducto, CodigoEAN, UnidadDeEntrada, 
		   Cant_Devuelta, PrecioCosto_Unitario, 
		   (Cant_Devuelta * PrecioCosto_Unitario) as SubTotal, TasaIva, ImpteIva, 
		   ((Cant_Devuelta * PrecioCosto_Unitario) + ImpteIva) as Importe 
	Into #tmpDevolucion 
	From DevolucionesDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 
	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioCompra as FolioCompra, IdProducto, CodigoEAN, ClaveLote, Cant_Devuelta
	Into #tmpDevolucionLotes  
	From DevolucionesDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 

	----  Ubicaciones
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioCompra as FolioCompra, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño, Cant_Devuelta
	Into #tmpDevolucionLotes_Ubicaciones  
	From DevolucionesDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion	
------------------------- 
	
--	select * from ComprasEnc (nolock) 	
--	select * from ComprasDet (nolock) 	
--	select * from ComprasDet_Lotes (nolock) 
--	
--	sp_listacolumnas ComprasDet_Lotes  
	
------------------------- 
	-- Obtener la informacion de la Venta 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioCompra, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		Cant_Recibida, Cant_Devuelta, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe 
	Into #tmpCompras 
	From ComprasDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioCompra = @FolioCompra 	

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCompra, IdProducto, CodigoEAN, ClaveLote, Renglon, Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpComprasLotes  
	From ComprasDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioCompra = @FolioCompra

	--	Ubicaciones
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCompra, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño,
	Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpComprasLotes_Ubicaciones  
	From ComprasDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioCompra = @FolioCompra 	
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
	From ComprasEnc V (NoLock) 
	Inner Join #tmpDevEnc D On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioCompra = D.FolioCompra ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida, 
		SubTotal = D.SubTotal, ImpteIva = D.ImpteIva, Importe = D.Importe
	From ComprasDet V 
	Inner Join #tmpCompras D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioCompra = D.FolioCompra 
			and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN )
	Where D.Cant_Devuelta > 0

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From ComprasDet_Lotes V 
	Inner Join #tmpComprasLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioCompra = D.FolioCompra and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote )
	Where D.Cant_Devuelta > 0

	--	Ubicaciones
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From ComprasDet_Lotes_Ubicaciones V 
	Inner Join #tmpComprasLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioCompra = D.FolioCompra and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )
	Where D.Cant_Devuelta > 0
		
	--  spp_DEV_AfectarVentas 


End 
Go--#SQL	

	