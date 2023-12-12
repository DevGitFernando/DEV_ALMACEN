----------------------------------------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INV_ConteoRapido_NoIdentificado' and xType = 'V' ) 
   Drop View vw_INV_ConteoRapido_NoIdentificado
Go--#SQL

Create View vw_INV_ConteoRapido_NoIdentificado
With Encryption 
As 
	Select E.IdEmpresa, Em.Nombre as Empresa,  
        F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 
        E.Folio, E.FechaRegistro, E.Observaciones, 
        E.IdPersonal, P.NombreCompleto as NombrePersonal,
        D.ClaveSSA, D.Descripcion, D.CodigoEAN,
		D.NombreComercial, D.Laboratorio, D.Cantidad, E.MovtoAplicado, 
		0 as Costo, 0 as Importe, D.Status 
    From INV_ConteoRapido_NoIdentificado_Enc E (NoLock) 
    Inner Join INV_ConteoRapido_NoIdentificado_Det D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.Folio )
	Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa )
    Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
    Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal )
	
Go--#SQL 