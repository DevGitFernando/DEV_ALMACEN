--------------------------------------------- 
If Exists ( Select Name From SysObjects (NoLock) Where Name = 'vw_Padrones' and xType = 'V' ) 
   Drop View vw_Padrones  
Go--#SQL

Create View vw_Padrones 
With Encryption 
As 

    Select P.Id, P.IdEstado, E.Nombre as Estado, 
        P.IdCliente, C.Nombre as Cliente, 
        P.NombreBD, P.NombreTabla as Padron, P.Status, (case when P.Status = 'A' then 1 else 0 end ) as StatusAux, P.EsLocal 
    From CFGS_PADRON_ESTADOS P (NoLock) 
    Inner Join CFGS_BD_PADRONES B (NoLock) On ( B.NombreBD = P.NombreBD )
    Inner Join CatEstados E (NoLock) On ( P.IdEstado = E.IdEstado )
    Inner Join CatClientes C (NoLock) On ( P.IdCliente = C.IdCliente )     

Go--#SQL
 
--    select * from vw_Padrones 

