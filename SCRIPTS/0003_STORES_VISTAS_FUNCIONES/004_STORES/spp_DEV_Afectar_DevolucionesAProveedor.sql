If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_DEV_Afectar_DevolucionesAProveedor' and xType = 'P' ) 
   Drop Proc spp_DEV_Afectar_DevolucionesAProveedor 
Go--#SQL

Create Proc spp_DEV_Afectar_DevolucionesAProveedor ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', 
	@FolioDevProv varchar(30) = '00000001', @FolioDevolucion varchar(30) = '00000002' ) 
With Encryption 
As 
Begin 
Set NoCount On 

------------------------- 
	-- Obtener la informacion de la Devolucion 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioDevProv as FolioDevProv, SubTotal, Iva, Total
	Into #tmpDevEnc 
	From DevolucionesEnc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioDevProv as FolioDevProv, IdProducto, CodigoEAN, UnidadDeEntrada, 
		   Cant_Devuelta, PrecioCosto_Unitario, TasaIva, ImpteIva 
	Into #tmpDevolucion 
	From DevolucionesDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 
	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioDevProv as FolioDevProv, IdProducto, CodigoEAN, ClaveLote, Cant_Devuelta
	Into #tmpDevolucionLotes  
	From DevolucionesDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion

	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioDevProv as FolioDevProv, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño, Cant_Devuelta
	Into #tmpDevolucionLotes_Ubicaciones  
	From DevolucionesDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
------------------------- 
	
	
------------------------- 
	-- Obtener la informacion de la Venta 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevProv, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		   Cant_Recibida, Cant_Devuelta, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe
	Into #tmpDev_A_Proveedor 
	From DevolucionesProveedorDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevProv = @FolioDevProv 	

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevProv, IdProducto, CodigoEAN, ClaveLote, Renglon, Cant_Recibida, Cant_Devuelta, CantidadRecibida
	Into #tmpDev_A_ProveedorLotes  
	From DevolucionesProveedorDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevProv = @FolioDevProv 

	
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevProv, IdProducto, CodigoEAN, ClaveLote,
	IdPasillo, IdEstante, IdEntrepaño,
	Cant_Recibida, Cant_Devuelta, CantidadRecibida
	Into #tmpDev_A_ProveedorLotes_Ubicaciones  
	From DevolucionesProveedorDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevProv = @FolioDevProv	
------------------------- 

	--- Calcular las cantidades a afectar  
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta), ImpteIva = (V.ImpteIva - D.ImpteIva)  
	From #tmpDev_A_Proveedor V 
	Inner Join #tmpDevolucion D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpDev_A_ProveedorLotes V 
	Inner Join #tmpDevolucionLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			  and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			  and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 


	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpDev_A_ProveedorLotes_Ubicaciones V 
	Inner Join #tmpDevolucionLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			  and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			  and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			  and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )



	--- Iniciar la actualizacion de informacion 
	Update V Set Status = 'D', SubTotal = (V.SubTotal - D.SubTotal), Iva = (V.Iva - D.Iva), Total = (V.Total - D.Total) 
	From DevolucionesProveedorEnc V (NoLock) 
	Inner Join #tmpDevEnc D On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioDevProv = D.FolioDevProv ) 
	
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida, ImpteIva = D.ImpteIva  
	From DevolucionesProveedorDet V 
	Inner Join #tmpDev_A_Proveedor D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioDevProv = D.FolioDevProv 
			and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From DevolucionesProveedorDet_Lotes V 
	Inner Join #tmpDev_A_ProveedorLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.FolioDevProv = D.FolioDevProv and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

	
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From DevolucionesProveedorDet_Lotes_Ubicaciones V 
	Inner Join #tmpDev_A_ProveedorLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.FolioDevProv = D.FolioDevProv and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			 and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )
		
	--  spp_DEV_Afectar_DevolucionesAProveedor 

End 
Go--#SQL
 

