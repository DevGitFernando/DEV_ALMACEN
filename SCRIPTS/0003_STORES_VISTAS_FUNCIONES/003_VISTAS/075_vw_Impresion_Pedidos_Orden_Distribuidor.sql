
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Pedidos_Orden_Distribuidor' and xType = 'V' ) 
   Drop View vw_Impresion_Pedidos_Orden_Distribuidor
Go--#SQL

Create View vw_Impresion_Pedidos_Orden_Distribuidor 
With Encryption 
As 

    Select E.IdEmpresa, Em.Nombre as Empresa,  
        F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 
        E.FolioPedido as Folio, E.FechaRegistro, E.Observaciones, 
        E.IdPersonal, P.NombreCompleto as Personal , E.Status, 
        D.IdClaveSSA, C.ClaveSSA_Base, C.ClaveSSA, C.ClaveSSA_Aux, C.DescripcionClave, C.IdPresentacion, C.Presentacion, C.ContenidoPaquete,
        D.Cantidad, D.CantidadEnCajas, D.Existencia, D.Status as StatusClave    
    From PedidosOrdenDist_Enc E (NoLock) 
    Inner Join PedidosOrdenDist_Det D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioPedido = D.FolioPedido )
    Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa )
    Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
    Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal ) 
    Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA = C.IdClaveSSA_Sal )
     
Go--#SQL     
  
