If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_DEV_AfectarVentas_SociosComerciales' and xType = 'P' ) 
   Drop Proc spp_DEV_AfectarVentas_SociosComerciales 
Go--#SQL

Create Proc spp_DEV_AfectarVentas_SociosComerciales ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FolioVenta varchar(30) = '00000005', @FolioDevolucion varchar(30) = '00000002' ) 
With Encryption 
As 
Begin 
Set NoCount On 

------------------------- 
	-- Obtener la informacion de la Devolucion 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioVenta as FolioVenta, SubTotal, Iva, Total
	Into #tmpDevEnc 
	From DevolucionesEnc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioVenta as FolioVenta, IdProducto, CodigoEAN, UnidadDeEntrada, 
		   Cant_Devuelta, PrecioCosto_Unitario, TasaIva, ImpteIva 
	Into #tmpDevolucion 
	From DevolucionesDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 
	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioVenta as FolioVenta, IdProducto, CodigoEAN, ClaveLote, Cant_Devuelta
	Into #tmpDevolucionLotes  
	From DevolucionesDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion

	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioVenta as FolioVenta, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño, Cant_Devuelta
	Into #tmpDevolucionLotes_Ubicaciones  
	From DevolucionesDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
------------------------- 
	
	
------------------------- 
	-- Obtener la informacion de la Venta 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon, d.UnidadDeEntrada, 
		   D.Cant_Vendida, Cant_Devuelta, CantidadVendida, Precio, TasaIva, ImpteIva
	Into #tmpVentas 
	From VentasDet_SociosComerciales D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioVenta = @FolioVenta 	

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon, Cant_Vendida, Cant_Devuelta, CantidadVendida
	Into #tmpVentasLotes  
	From VentasDet_SociosComerciales_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioVenta = @FolioVenta 

	
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote,
	IdPasillo, IdEstante, IdEntrepaño,
	Cant_Vendida, Cant_Devuelta, CantidadVendida
	Into #tmpVentasLotes_Ubicaciones  
	From VentasDet_SociosComerciales_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioVenta = @FolioVenta	
------------------------- 

	--- Calcular las cantidades a afectar  
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadVendida = (V.CantidadVendida - D.Cant_Devuelta), ImpteIva = (V.ImpteIva - D.ImpteIva)  
	From #tmpVentas V 
	Inner Join #tmpDevolucion D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadVendida = (V.CantidadVendida - D.Cant_Devuelta) 
	From #tmpVentasLotes V 
	Inner Join #tmpDevolucionLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			  and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			  and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 


	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadVendida = (V.CantidadVendida - D.Cant_Devuelta) 
	From #tmpVentasLotes_Ubicaciones V 
	Inner Join #tmpDevolucionLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			  and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			  and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			  and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )



	--- Iniciar la actualizacion de informacion 
	Update V Set Status = 'D', SubTotal = (V.SubTotal - D.SubTotal), Iva = (V.Iva - D.Iva), Total = (V.Total - D.Total) 
	From VentasEnc_SociosComerciales V (NoLock) 
	Inner Join #tmpDevEnc D On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadVendida = D.CantidadVendida, ImpteIva = D.ImpteIva  
	From VentasDet_SociosComerciales V 
	Inner Join #tmpVentas D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta 
			and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadVendida = D.CantidadVendida 
	From VentasDet_SociosComerciales_Lotes V 
	Inner Join #tmpVentasLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.FolioVenta = D.FolioVenta and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

	
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadVendida = D.CantidadVendida 
	From VentasDet_SociosComerciales_Lotes_Ubicaciones V 
	Inner Join #tmpVentasLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.FolioVenta = D.FolioVenta and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			 and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )
		
	--  spp_DEV_AfectarVentas 

End 
Go--#SQL
 

