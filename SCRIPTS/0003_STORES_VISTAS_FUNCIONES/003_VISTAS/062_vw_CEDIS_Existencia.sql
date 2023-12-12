If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CEDIS_Existencia' and xType = 'V' ) 
   Drop View vw_CEDIS_Existencia 
Go--#SQL

Create View vw_CEDIS_Existencia 
With Encryption 
As 

    Select E.IdEmpresa, Em.Nombre as Empresa,  
        F.IdEstado, F.Estado, E.IdFarmaciaCEDIS, FC.Farmacia As FarmaciaCEDIS, 
        E.Folio as Folio,
		E.IdFarmacia, F.Farmacia, 
		E.FechaRegistro, 
        E.IdPersonal, P.NombreCompleto as Personal, E.Status, 
        D.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, 
        D.Existencia, D.ExistenciaDisponible, D.Status as StatusClave    
    From CEDIS_Existencia_Enc E (NoLock) 
    Inner Join CEDIS_Existencia_Det D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmaciaCEDIS = D.IdFarmaciaCEDIS and E.Folio = D.Folio )
    Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa )
    Inner Join vw_Farmacias FC (NoLock) On ( E.IdEstado = FC.IdEstado and E.IdFarmaciaCEDIS = FC.IdFarmacia )
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
    Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal ) 
    Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA = C.IdClaveSSA_Sal )
    
--    select * from CatEmpresas 
     
Go--#SQL     
  
--  select top 1 * from vw_ClavesSSA_Sales      
  