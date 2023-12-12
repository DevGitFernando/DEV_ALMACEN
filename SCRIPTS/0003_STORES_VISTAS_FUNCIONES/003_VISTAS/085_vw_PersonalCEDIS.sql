
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PersonalCEDIS' and xType = 'V' ) 
   Drop View vw_PersonalCEDIS
Go--#SQL

Create View vw_PersonalCEDIS 
With Encryption 
As 
	Select  P.IdEmpresa, CE.Nombre as Empresa, P.IdEstado, E.Nombre as Estado, P.IdFarmacia, F.NombreFarmacia as Farmacia,
	P.IdPersonal, P.Nombre as Personal, P.IdPuesto, PC.Descripcion as Puesto, P.Status 
	From CatPersonalCEDIS P (noLock) 
	Inner Join CatPuestosCEDIS PC (NoLock) On ( P.IdPuesto = PC.IdPuesto )
	Inner Join CatEmpresas CE (NoLock) On ( P.IdEmpresa = CE.IdEmpresa )
	Inner Join CatEstados E (NoLock) On ( P.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 
	
Go--#SQL 	 	
	

