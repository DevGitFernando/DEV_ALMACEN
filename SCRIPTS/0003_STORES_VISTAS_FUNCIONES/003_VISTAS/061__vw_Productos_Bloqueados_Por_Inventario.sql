If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Productos_Bloqueados_Por_Inventario' and xType = 'V' )
   Drop View vw_Productos_Bloqueados_Por_Inventario
Go--#SQL
 
-- vw_Productos

Create View vw_Productos_Bloqueados_Por_Inventario
With Encryption 
As 

    Select E.IdEmpresa, Em.Nombre as Empresa, 
        E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, E.Poliza, E.FechaRegistro, 
        D.IdProducto, D.CodigoEAN, P.Descripcion 
    From AjustesInv_Enc E (NoLock) 
    Inner Join AjustesInv_Det D (NoLock) 
        On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Poliza = D.Poliza ) 
    Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
    Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa ) 
    Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
    Where E.MovtoAplicado = 'N' 

Go--#SQL	
 
--    select * from  vw_Productos_Bloqueados_Por_Inventario   

--      select * from AjustesInv_Det where Poliza = 5 


--    select * from CatEmpresas 
    