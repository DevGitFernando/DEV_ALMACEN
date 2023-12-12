If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_CFGC_ALMN__Rotacion' and xType = 'V' )  
   Drop View vw_CFGC_ALMN__Rotacion  	
Go--#SQL 

Create View vw_CFGC_ALMN__Rotacion
As 

Select  R.IdEmpresa, E.Nombre As Empresa, R.IdEstado, F.Estado, R.IdFarmacia, F.Farmacia, R.IdRotacion, R.NombreRotacion, R.Orden, R.Status
From CFGC_ALMN__Rotacion R (NoLock)
Inner Join CatEmpresas E (NoLock) On (R.IdEmpresa = E.IdEmpresa)
Inner Join vw_Farmacias F  (NoLock) On (R.IdEstado = F.IdEstado And R.IdFarmacia = F.IdFarmacia)


Go--#SQL