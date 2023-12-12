If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_PedidosUnidades' and xType = 'V' ) 
   Drop View vw_INT_ND_PedidosUnidades 
Go--#SQL  

Create View vw_INT_ND_PedidosUnidades 
With Encryption 
As 

	Select  
		E.IdEmpresa, E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia,
		(F.Domicilio + ', C.P.: ' + F.CodigoPostal + ', ' + F.Colonia + ', ' + F.Municipio ) as DireccionFarmacia, 
		E.FolioPedido as Folio, E.TipoDeFarmacias, 
		E.IdProveedor, PV.Nombre as Proveedor, 
		E.IdPersonal, vP.NombreCompleto as Personal,
		E.FechaRegistro, E.FechaRequeridaEntrega as FechaPromesaEntrega, 
		D.Status, 
		D.CodigoCliente, CN.Nombre as NombreCliente, 
		D.ReferenciaPedido as ReferenciaFolioPedido, 
		D.IdFarmaciaPedido, F2.Farmacia as FarmaciaPedido,
		(F2.Domicilio + ', C.P.: ' + F2.CodigoPostal + ', ' + F2.Colonia + ', ' + F2.Municipio ) as DireccionFarmaciaPedido, 
		D.ClaveSSA, S.DescripcionClave, 
		D.CodigoProducto, P.IdProducto, D.CodigoEAN, P.Descripcion, 
		S.ContenidoPaquete, 
		D.Cantidad as CantidadCajasPrometida, 
		D.CantidadRecibida as CantidadCajas, 
		D.CantidadExcedente as CantidadCajasExcedente, 
		D.CantidadDañadoMalEstado as CantidadCajasDañadoMalEstado, 
		D.CantidadCaducado as CantidadCajasCaducado, 
		((D.Cantidad) - ((D.CantidadRecibida + D.CantidadDañadoMalEstado + D.CantidadCaducado ) + D.CantidadDañadoMalEstado + D.CantidadCaducado)) as Faltante,
		cast((case when S.ContenidoPaquete > 1 Then (S.ContenidoPaquete * D.CantidadRecibida) Else D.CantidadRecibida End) as numeric(14,4)) as Cantidad, 			
				cast((case when S.ContenidoPaquete > 1 Then (S.ContenidoPaquete * D.Cantidad) Else D.Cantidad End) as numeric(14,4)) as CantidadPrometida, 	
		D.Precio, 
		cast((case when S.ContenidoPaquete > 1 Then (S.ContenidoPaquete / D.Precio) Else D.Precio End) as numeric(14,4)) as PrecioUnitario, 	
		D.TasaIva, 
		-- D.Iva, D.SubTotal, D.Importe, 	   
		0 as Iva, 0 as SubTotal, 0 as Importe, D.EsValidado, E.TipoRemision 
	From INT_ND_Pedidos_Enviados E (NoLock) 
	Inner Join 	INT_ND_Pedidos_Enviados_Det D 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioPedido = D.FolioPedido ) 
	Left Join INT_ND_Clientes CN (NoLock) On ( D.CodigoCliente = CN.CodigoCliente )	
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Farmacias F2 (NoLock) On ( E.IdEstado = F2.IdEstado and D.IdFarmaciaPedido = F2.IdFarmacia ) 
	Inner Join CatProveedores PV (NoLock) On ( E.IdProveedor = PV.IdProveedor )  
	Inner Join vw_ClavesSSA_Sales S On ( D.ClaveSSA = S.ClaveSSA ) 
	Left Join vw_Productos_CodigoEAN P On ( D.CodigoEAN = P.CodigoEAN )
	Inner Join vw_Personal vP (Nolock) On ( vP.IdEstado = E.IdEstado and vP.IdFarmacia = E.IdFarmacia and vP.IdPersonal = E.IdPersonal ) 
	
	
Go--#SQL 	
	
	