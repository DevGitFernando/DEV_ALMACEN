


	-------------------------------------------------- 
If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_Claves_Lotes_SinUbicacionesAsignadas' and xType = 'V' ) 
   Drop View vw_Claves_Lotes_SinUbicacionesAsignadas  
Go--#SQL

Create View vw_Claves_Lotes_SinUbicacionesAsignadas 
With Encryption 
As 
    select 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
        IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, 
        IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, IdLaboratorio, Laboratorio, 
        ClaveLote, Existencia, convert(varchar(7), FechaCaducidad, 120) as FechaCaducidad, MesesParaCaducar, FechaRegistro -- top 1 count(*) 
    from vw_ExistenciaPorCodigoEAN_Lotes E (Nolock)
    where E.IdEmpresa <>  '' and Existencia > 0 
          and Not Exists 
          (  
            Select IdProducto, CodigoEAN, ClaveLote 
            From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones P (NoLock) 
            Where E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia 
                  and E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN and E.ClaveLote = P.ClaveLote 
          )  
Go--#SQL