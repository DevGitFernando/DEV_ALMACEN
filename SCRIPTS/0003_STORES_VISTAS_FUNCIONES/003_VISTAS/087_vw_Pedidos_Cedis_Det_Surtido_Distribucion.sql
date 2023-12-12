----If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'V' )  
----   Drop View vw_Pedidos_Cedis_Det_Surtido_Distribucion 	
----Go--#xSQL  

----Create View vw_Pedidos_Cedis_Det_Surtido_Distribucion 
----As 
	
----	Select 
----		P.IdEmpresa, P.Empresa, P.IdEstado, P.Estado, P.ClaveRenapo, P.IdFarmacia, P.Farmacia,
----		P.FolioSurtido, P.IdFarmaciaPedido, P.FarmaciaPedido, P.FolioPedido, P.FolioTransferenciaReferencia, P.FechaRegistro, 
----		P.IdPersonal, P.NombrePersonal, P.IdPersonalSurtido, P.PersonalSurtido, P.Observaciones, P.Status, P.StatusPedido,
----		D.IdSurtimiento, D.ClaveSSA, V.DescripcionSal, V.Presentacion_ClaveSSA, 
----		D.IdSubFarmacia, S.Descripcion As SubFarmacia, D.IdProducto, D.CodigoEAN, V.Descripcion, V.Presentacion,
----		D.ClaveLote, D.FechaCaducidad, datediff(mm, getdate(), IsNull(D.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar,
----		D.EsConsignacion, D.IdPasillo, PE.DescripcionPasillo As Pasillo, D.IdEstante, PE.DescripcionEstante As Estante, 
----		D.IdEntrepaño, PE.DescripcionEntrepaño As Entrepaño,
----		D.IdCaja, Convert(int, D.IdCaja) as Caja,
----		D.CantidadRequerida, D.CantidadAsignada, D.Observaciones as ObservacionesSurtimiento, D.Keyx
----	From vw_PedidosCedis_Surtimiento P (NoLock)
----	Inner Join Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock)
----		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
----	Inner Join vw_Productos_CodigoEAN V (Nolock) On ( D.IdProducto = V.IdProducto and D.CodigoEAN = V.CodigoEAN )
----	Inner Join CatFarmacias_SubFarmacias S(NoLock) On ( D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdSubFarmacia = S.IdSubFarmacia )
----	Inner Join vw_Pasillos_Estantes_Entrepaños PE(NoLock) On ( D.IdEmpresa = PE.IdEmpresa And D.IdEstado = PE.IdEstado And D.IdFarmacia = PE.IdFarmacia 
----		And D.IdPasillo = PE.IdPasillo And D.IdEstante = PE.IdEstante And D.IdEntrepaño = PE.IdEntrepaño ) 
	
	
----Go--#xSQL 
	
--	Select * From Pedidos_Cedis_Det_Surtido_Distribucion (Nolock)
--	Select * From vw_Pedidos_Cedis_Det_Surtido_Distribucion (Nolock)