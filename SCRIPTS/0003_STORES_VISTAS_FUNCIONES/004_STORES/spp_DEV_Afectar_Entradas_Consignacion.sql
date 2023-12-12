
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_DEV_Afectar_Entradas_Consignacion' and xType = 'P' ) 
   Drop Proc spp_DEV_Afectar_Entradas_Consignacion
Go--#SQL	

Create Proc spp_DEV_Afectar_Entradas_Consignacion ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0188', 
	@FolioEntrada varchar(30) = '00000001', @FolioDevolucion varchar(30) = '00000001' ) 
With Encryption 
As 
Begin 
Set NoCount On 

------------------------- 
	-- Obtener la informacion de la Devolucion 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioEntrada as FolioEntrada, SubTotal, Iva, Total
	Into #tmpDevEnc 
	From DevolucionesEnc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, @FolioEntrada as FolioEntrada, IdProducto, CodigoEAN, UnidadDeEntrada, 
		   Cant_Devuelta, PrecioCosto_Unitario, 
		   (Cant_Devuelta * PrecioCosto_Unitario) as SubTotal, TasaIva, ImpteIva, 
		   ((Cant_Devuelta * PrecioCosto_Unitario) + ImpteIva) as Importe 
	Into #tmpDevolucion 
	From DevolucionesDet D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 
	
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioEntrada as FolioEntrada, IdProducto, CodigoEAN, ClaveLote, Cant_Devuelta
	Into #tmpDevolucionLotes  
	From DevolucionesDet_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion 

----	----  Ubicaciones
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, @FolioEntrada as FolioEntrada, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño, Cant_Devuelta
	Into #tmpDevolucionLotes_Ubicaciones  
	From DevolucionesDet_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioDevolucion = @FolioDevolucion	
------------------------- 
	
--	select * from EntradasEnc_Consignacion (nolock) 	
--	select * from EntradasDet_Consignacion (nolock) 	
--	select * from EntradasDet_Consignacion_Lotes (nolock) 
--	
--	sp_listacolumnas EntradasDet_Consignacion_Lotes  
	
------------------------- 
	-- Obtener la informacion de la Venta 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		Cant_Recibida, Cant_Devuelta, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe 
	Into #tmpEntradas 
	From EntradasDet_Consignacion D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioEntrada = @FolioEntrada 	

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, ClaveLote, Renglon, Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpEntradasLotes  
	From EntradasDet_Consignacion_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioEntrada = @FolioEntrada

	--	Ubicaciones
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, ClaveLote, 
	IdPasillo, IdEstante, IdEntrepaño,
	Cant_Recibida, Cant_Devuelta, CantidadRecibida 
	Into #tmpEntradasLotes_Ubicaciones  
	From EntradasDet_Consignacion_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioEntrada = @FolioEntrada 	
------------------------- 


	--- Calcular las cantidades a afectar  
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), 
		CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta), 
		SubTotal = (V.SubTotal -D.SubTotal), ImpteIva = (V.ImpteIva - D.ImpteIva), Importe = (V.Importe-D.Importe) 
	From #tmpEntradas V 
	Inner Join #tmpDevolucion D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpEntradasLotes V 
	Inner Join #tmpDevolucionLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

----	--	Ubicaciones
	Update V Set Cant_Devuelta = (V.Cant_Devuelta + D.Cant_Devuelta), CantidadRecibida = (V.CantidadRecibida - D.Cant_Devuelta) 
	From #tmpEntradasLotes_Ubicaciones V 
	Inner Join #tmpDevolucionLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
			 and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
			 and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			 and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )


 
	--- Iniciar la actualizacion de informacion 
	Update V Set Status = 'D', SubTotal = (V.SubTotal - D.SubTotal), Iva = (V.Iva - D.Iva), Total = (V.Total - D.Total) 
	From EntradasEnc_Consignacion V (NoLock) 
	Inner Join #tmpDevEnc D On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioEntrada = D.FolioEntrada ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida, 
		SubTotal = (V.SubTotal - D.SubTotal), ImpteIva = (V.ImpteIva - D.ImpteIva), Importe = (V.Importe - D.Importe) 
	From EntradasDet_Consignacion V 
	Inner Join #tmpEntradas D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioEntrada = D.FolioEntrada 
			and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN ) 

	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From EntradasDet_Consignacion_Lotes V 
	Inner Join #tmpEntradasLotes D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioEntrada = D.FolioEntrada and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote ) 

----	--	Ubicaciones
	Update V Set Cant_Devuelta = D.Cant_Devuelta, CantidadRecibida = D.CantidadRecibida 
	From EntradasDet_Consignacion_Lotes_Ubicaciones V 
	Inner Join #tmpEntradasLotes_Ubicaciones D 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado 
		    and V.IdFarmacia = D.IdFarmacia and V.IdSubFarmacia = D.IdSubFarmacia 
		    and V.FolioEntrada = D.FolioEntrada and V.IdProducto = D.IdProducto and V.CodigoEAN = D.CodigoEAN and V.ClaveLote = D.ClaveLote
			and V.IdPasillo = D.IdPasillo and V.IdEstante = D.IdEstante and V.IdEntrepaño = D.IdEntrepaño )
		
	--  spp_DEV_AfectarEntradasDis 


End 
Go--#SQL	

	