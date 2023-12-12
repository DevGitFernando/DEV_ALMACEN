If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Pedidos_Cedis_Pendientes_AsignarSurtido' and xType = 'V' ) 
   Drop View vw_Pedidos_Cedis_Pendientes_AsignarSurtido 
Go--#SQL

Create View vw_Pedidos_Cedis_Pendientes_AsignarSurtido 
With Encryption 
As 

    Select E.IdEmpresa, Em.Nombre as Empresa,  
        F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 
        E.FolioPedido as Folio, E.FechaRegistro, E.Observaciones, E.Status  
    From Pedidos_Cedis_Enc E (NoLock) 
    Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa )
    Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
    Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal ) 
    Where Not Exists 
        ( 
            Select * 
            From Pedidos_Cedis_Det_Surtido S (NoLock) 
            Where E.IdEmpresa = S.IdEmpresa and E.IdEstado = S.IdEstado and E.IdFarmacia = S.IdFarmacia and E.FolioPedido = S.FolioPedido 
        ) 
   
Go--#SQL     
  