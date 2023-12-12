If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_DEV_AfectarPedidosDis' and xType = 'P' ) 
   Drop Proc spp_DEV_AfectarPedidosDis 
Go--#SQL	

Create Proc spp_DEV_AfectarPedidosDis ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0188', 
	@FolioPedido varchar(30) = '00000001', @FolioDevolucion varchar(30) = '00000001' ) 
With Encryption 
As 
Begin 
Set NoCount On 

------------------------- 
	-- Obtener la informacion de la Devolucion 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioPedido as FolioPedido, SubTotal, Iva, Total
	Into #tmpDevEnc 
	From DevolucionesEnc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioPedido as FolioPedido, IdProducto, CodigoEAN, UnidadDeEntrada, 
		   Cant_Devuelta, PrecioCosto_Unitario, 
		   (Cant_Devuelta * PrecioCosto_Unitario) as SubTotal, TasaIva, ImpteIva, 
		   ((Cant_Devuelta * PrecioCosto_Unitario) + ImpteIva) as Importe 
	Into #tmpDevolucion 
	From DevolucionesDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 
	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioPedido as FolioPedido, IdProducto, CodigoEAN, ClaveLote, Cant_Devuelta
	Into #tmpDevolucionLotes  
	From DevolucionesDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 

	----  Ubicaciones
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioPedido as FolioPedido, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño, Cant_Devuelta
	Into #tmpDevolucionLotes_Ubicaciones  
	From DevolucionesDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion	
------------------------- 
	
--	select * from PedidosEnc (nolock) 	
--	select * from PedidosDet (nolock) 	
--	select * from PedidosDet_Lotes (nolock) 
--	
--	sp_listacolumnas PedidosDet_Lotes  
	
------------------------- 
	-- Obtener la informacion de la Venta 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		Cant_Recibida, Cant_Devuelta, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe 
	Into #tmpPedidos 
	From PedidosDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioPedido = @FolioPedido 	

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon, Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpPedidosLotes  
	From PedidosDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioPedido = @FolioPedido

	--	Ubicaciones
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño,
	Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpPedidosLotes_Ubicaciones  
	From PedidosDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioPedido = @FolioPedido 	
------------------------- 


	--- Calcular las cantidades a afectar  
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), 
		CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta), 
		SubTotal = (V.SubTotal -D.SubTotal), ImpteIva = (V.ImpteIva - D.ImpteIva), Importe = (V.Importe-D.Importe) 
	From #tmpPedidos V 
	Inner Join #tmpDevolucion D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpPedidosLotes V 
	Inner Join #tmpDevolucionLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

	--	Ubicaciones
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpPedidosLotes_Ubicaciones V 
	Inner Join #tmpDevolucionLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			 and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )


 
	--- Iniciar la actualizacion de informacion 
	Update V Set Status = 'D', SubTotal = (V.SubTotal - D.SubTotal), Iva = (V.Iva - D.Iva), Total = (V.Total - D.Total) 
	From PedidosEnc V (NoLock) 
	Inner Join #tmpDevEnc D On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioPedido = D.FolioPedido ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida, 
		SubTotal = (V.SubTotal - D.SubTotal), ImpteIva = (V.ImpteIva - D.ImpteIva), Importe = (V.Importe - D.Importe) 
	From PedidosDet V 
	Inner Join #tmpPedidos D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioPedido = D.FolioPedido 
			and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From PedidosDet_Lotes V 
	Inner Join #tmpPedidosLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioPedido = D.FolioPedido and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

	--	Ubicaciones
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From PedidosDet_Lotes_Ubicaciones V 
	Inner Join #tmpPedidosLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioPedido = D.FolioPedido and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )
		
	--  spp_DEV_AfectarPedidosDis 


End 
Go--#SQL	

	