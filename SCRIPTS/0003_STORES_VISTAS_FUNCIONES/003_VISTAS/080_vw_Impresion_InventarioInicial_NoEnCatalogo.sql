If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_InventarioInicial_NoEnCatalogo' and xType = 'V' ) 
   Drop View vw_Impresion_InventarioInicial_NoEnCatalogo
Go--#SQL

Create View vw_Impresion_InventarioInicial_NoEnCatalogo  
With Encryption 
As 
	Select E.IdEmpresa, Em.Nombre as Empresa,  
        F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 
        E.Folio, E.FechaRegistro, E.Observaciones, 
        E.IdPersonal, P.NombreCompleto as NombrePersonal,
        D.ClaveSSA, D.Descripcion, D.CodigoEAN,
		D.NombreComercial, D.Laboratorio, D.ClaveLote,
		D.FechaCaducidad, D.CantidadLote, E.MovtoAplicado, 
		0 as Costo, 0 as ImporteLote, D.Status 
    From MovtosInvConsignacion_NoEnCatalogo_Enc E (NoLock) 
    Inner Join MovtosInvConsignacion_NoEnCatalogo_Det D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.Folio )
	Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa )
    Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
    Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal )
	
Go--#SQL 
