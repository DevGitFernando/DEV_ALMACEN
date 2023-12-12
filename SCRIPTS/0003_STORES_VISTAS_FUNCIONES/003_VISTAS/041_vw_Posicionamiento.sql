---------------------- NO MOVER 
--------If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_UbicacionClavesAlmacen' and xType = 'V' ) 
--------   Drop View vw_UbicacionClavesAlmacen 
--------Go--#SQL
--------
--------Create View vw_UbicacionClavesAlmacen 
--------With Encryption 
--------As 
--------
--------	Select	P.IdEmpresa, I.NombreEmpresa, P.IdEstado, I.Estado, P.IdFarmacia, I.Farmacia, C.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionSal,  
--------			P.IdPasillo, I.NombrePasillo, P.IdEstante, I.NombreEstante, P.IdEntrepaño, I.NombreEntrepaño, P.Status  
--------	From  CatUbicacionClavesAlmacen P (Nolock)  
--------	Inner Join vw_PasillosEstantesEntrepaños I (Nolock) On( P.IdEmpresa = I.IdEmpresa And P.IdEstado = I.IdEstado And P.IdFarmacia = I.IdFarmacia  And P.IdPasillo = I.IdPasillo And P.IdEstante = I.IdEstante And P.IdEntrepaño = I.IdEntrepaño )  
--------	Inner Join  vw_ClavesSSA_Sales C (Nolock)  On( P.IdClaveSSA = C.IdClaveSSA_Sal )   
--------
--------Go--#SQL
--------
--------If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_UbicacionProductosLotes_Existencia ' and xType = 'V' ) 
--------   Drop View vw_UbicacionProductosLotes_Existencia 
--------Go--#SQL 
--------
--------Create View vw_UbicacionProductosLotes_Existencia 
--------With Encryption 
--------As 
--------	Select L.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.IdSubFarmacia, E.SubFarmacia, 
--------		E.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionClave, 
--------		E.IdProducto, E.CodigoEAN, E.ClaveLote, E.FechaCaducidad, E.MesesParaCaducar, E.FechaRegistro, E.DescripcionProducto, 
--------		E.IdPresentacion, E.Presentacion, E.ContenidoPaquete, E.IdLaboratorio, E.Laboratorio, 
--------		U.IdPasillo, U.NombrePasillo, U.IdEstante, U.NombreEstante, U.IdEntrepaño, U.NombreEntrepaño, 
--------		L.Cantidad as CantidadPosicion, cast(E.Existencia as int) as Existencia 
--------	From CatUbicaciones_CodigosEAN_Lotes L 
--------	Inner Join vw_PasillosEstantesEntrepaños U (NoLock) 
--------		On ( L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia
--------			 and L.IdPasillo = U.IdPasillo and L.IdEstante = U.IdEstante and L.IdEntrepaño = U.IdEntrepaño ) 
--------	Inner Join vw_ExistenciaPorCodigoEAN_Lotes E (NoLock) 
--------		On ( L.IdEmpresa = E.IdEmpresa and L.IdEstado = E.IdEstado and L.IdFarmacia = E.IdFarmacia and L.IdSubFarmacia = E.IdSubFarmacia 
--------			 and L.IdProducto = E.IdProducto and L.CodigoEAN = E.CodigoEAN and L.ClaveLote = E.ClaveLote  ) 
--------Go--#SQL 
--------
-------------------------------------------------- 
--------If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_Claves_Lotes_SinUbicacionesAsignadas' and xType = 'V' ) 
--------   Drop View vw_Claves_Lotes_SinUbicacionesAsignadas  
--------Go--#SQL
--------
--------Create View vw_Claves_Lotes_SinUbicacionesAsignadas 
--------With Encryption 
--------As 
--------    select 
--------        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
--------        IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, 
--------        IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, IdLaboratorio, Laboratorio, 
--------        ClaveLote, Existencia, convert(varchar(7), FechaCaducidad, 120) as FechaCaducidad, MesesParaCaducar, FechaRegistro -- top 1 count(*) 
--------    from vw_ExistenciaPorCodigoEAN_Lotes E  
--------    where E.IdEmpresa <>  '' and Existencia > 0 
--------          and Not Exists 
--------          (  
--------            Select IdProducto, CodigoEAN, ClaveLote 
--------            From CatUbicaciones_CodigosEAN_Lotes P (NoLock) 
--------            Where E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia 
--------                  and E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN and E.ClaveLote = P.ClaveLote 
--------          )  
--------Go--#SQL

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_Pasillos' and xType = 'V' ) 
   Drop View vw_Pasillos   
Go--#SQL 

Create View vw_Pasillos 
With Encryption 
As  

    Select P.IdEmpresa, P.IdEstado, P.IdFarmacia, 
           P.IdPasillo, P.DescripcionPasillo, P.Status as StatusPasillo 
    From CatPasillos P (NoLock) 
        
Go--#SQL

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_Pasillos_Estantes' and xType = 'V' ) 
   Drop View vw_Pasillos_Estantes   
Go--#SQL 

Create View vw_Pasillos_Estantes 
With Encryption 
As  

    Select P.IdEmpresa, P.IdEstado, P.IdFarmacia, 
           P.IdPasillo, P.DescripcionPasillo, P.Status as StatusPasillo, 
           Es.IdEstante, Es.DescripcionEstante, Es.Status as StatusEstante 
    From CatPasillos P (NoLock) 
    Inner Join CatPasillos_Estantes Es (NoLock) 
        On ( P.IdEmpresa = Es.IdEmpresa and P.IdEstado = Es.IdEstado and P.IdFarmacia = Es.IdFarmacia and P.IdPasillo = Es.IdPasillo ) 
        
Go--#SQL 


-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_Pasillos_Estantes_Entrepaños' and xType = 'V' ) 
   Drop View vw_Pasillos_Estantes_Entrepaños   
Go--#SQL 

Create View vw_Pasillos_Estantes_Entrepaños 
With Encryption 
As  

    Select P.IdEmpresa, P.IdEstado, P.IdFarmacia, 
           P.IdPasillo, P.DescripcionPasillo, P.Status as StatusPasillo, 
           Es.IdEstante, Es.DescripcionEstante, Es.Status as StatusEstante, 
           Ee.IdEntrepaño, Ee.DescripcionEntrepaño, Ee.Status as StatusEntrepaño  
    From CatPasillos P (NoLock) 
    Inner Join CatPasillos_Estantes Es (NoLock) 
        On ( P.IdEmpresa = Es.IdEmpresa and P.IdEstado = Es.IdEstado and P.IdFarmacia = Es.IdFarmacia and P.IdPasillo = Es.IdPasillo ) 
    Inner Join CatPasillos_Estantes_Entreaño Ee (NoLock) 
        On ( Es.IdEmpresa = Ee.IdEmpresa and Es.IdEstado = Ee.IdEstado and Es.IdFarmacia = Ee.IdFarmacia and Es.IdPasillo = Ee.IdPasillo and Es.IdEstante = Ee.IdEstante )     

Go--#SQL 
 
 
 
--  select * from vw_Pasillos 

--  select * from vw_Pasillos_Estantes 

--  select * from vw_Pasillos_Estantes_Entrepaños 



-- sp_listacolumnas CatPasillos 
    
