------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'V' )  
   Drop View vw_Pedidos_Cedis_Det_Surtido_Distribucion 	
Go--#SQL  

Create View vw_Pedidos_Cedis_Det_Surtido_Distribucion 
As 
	
	Select --COUNT(*)
		P.IdEmpresa, P.Empresa, P.IdEstado, P.Estado, P.ClaveRenapo, P.IdFarmacia, P.Farmacia,
		P.FolioSurtido, P.IdFarmaciaPedido, P.FarmaciaPedido, P.FolioPedido, P.FolioTransferenciaReferencia, 
		P.EsTransferencia, 
		P.TipoDePedido, 
		P.IdEstadoSolicita, P.EstadoSolicita, 
		P.IdFarmaciaSolicita, P.FarmaciaSolicita, 
		P.IdCliente, P.IdSubCliente, P.IdBeneficiario, 
		P.FechaRegistro, 
		P.IdPersonal, P.NombrePersonal, P.IdPersonalSurtido, P.PersonalSurtido, P.Observaciones, P.Status, P.StatusPedido, 
		P.ReferenciaInterna, 
		D.IdSurtimiento, V.IdClaveSSA_Sal, D.ClaveSSA, V.DescripcionSal, V.Presentacion_ClaveSSA, 
		D.IdSubFarmacia, S.Descripcion As SubFarmacia, D.IdProducto, D.CodigoEAN, V.Descripcion, V.Presentacion,
		D.ClaveLote, D.FechaCaducidad, datediff(mm, getdate(), IsNull(D.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesParaCaducar,
		D.EsConsignacion, 
		
		D.IdPasillo, cast(D.IdPasillo as varchar) As Pasillo, PE.DescripcionPasillo, 
		D.IdEstante, cast(D.IdEstante as varchar) As Estante, PE.DescripcionEstante, 
		D.IdEntrepaño, cast(D.IdEntrepaño as varchar) As Entrepaño, PE.DescripcionEntrepaño,
		PE.IdOrden,
		
		D.IdCaja, Convert(int, D.IdCaja) as Caja, 
		U.CantidadAsignada As CantidadAsignadaPorClaveSSA, 
		cast((U.CantidadAsignada / (V.ContenidoPaquete *  1.0)) as int) As CantidadAsignadaPorClaveSSA_Cajas, 		
		D.CantidadRequerida, D.CantidadAsignada, 
		
		cast((D.CantidadRequerida / (V.ContenidoPaquete *  1.0)) as int) as CantidadRequerida_Cajas, 
		cast((D.CantidadAsignada / (V.ContenidoPaquete *  1.0)) as int) as CantidadAsignada_Cajas,  	
		
		D.Validado, 


		D.Observaciones as ObservacionesSurtimiento,
		D.Keyx, P.EsManual, 
		cast((F.Existencia - F.ExistenciaEnTransito) as int) As Existencia, 	
		cast(((F.Existencia - F.ExistenciaEnTransito) / (V.ContenidoPaquete *  1.0)) as int) As Existencia_Cajas  
	From vw_PedidosCedis_Surtimiento P (NoLock)
	Inner Join Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock)
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	Inner Join vw_Productos_CodigoEAN V (Nolock) On ( D.IdProducto = V.IdProducto and D.CodigoEAN = V.CodigoEAN )
	Inner Join CatFarmacias_SubFarmacias S(NoLock) On ( D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdSubFarmacia = S.IdSubFarmacia )
	Inner Join vw_Pasillos_Estantes_Entrepaños PE (NoLock) 
		On ( D.IdEmpresa = PE.IdEmpresa And D.IdEstado = PE.IdEstado And D.IdFarmacia = PE.IdFarmacia 
		And D.IdPasillo = PE.IdPasillo And D.IdEstante = PE.IdEstante And D.IdEntrepaño = PE.IdEntrepaño )
	Inner Join Pedidos_Cedis_Det_Surtido U (NoLock) 
		On ( P.IdEmpresa = U.IdEmpresa And P.IdEstado = U.IdEstado And P.IdFarmacia = U.IdFarmacia And P.FolioSurtido = U.FolioSurtido And
			V.IdClaveSSA_Sal =  U.IdClaveSSA )
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
		On ( D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado And D.IdFarmacia = F.IdFarmacia And D.CodigoEAN = F.CodigoEAN And
			D.IdSubFarmacia = F.IdSubFarmacia And D.ClaveLote = F.ClaveLote And 
			D.IdPasillo = F.IdPasillo And D.IdEstante = F.IdEstante And D.IdEntrepaño = F.IdEntrepaño)
	
	
Go--#SQL 
	
--	Select * From Pedidos_Cedis_Det_Surtido_Distribucion (Nolock)
--	Select * From vw_Pedidos_Cedis_Det_Surtido_Distribucion (Nolock)