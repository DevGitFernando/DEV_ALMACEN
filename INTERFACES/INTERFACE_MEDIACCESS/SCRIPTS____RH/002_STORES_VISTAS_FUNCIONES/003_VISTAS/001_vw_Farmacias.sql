
If Exists ( Select Name From Sysobjects (noLock) Where Name = 'vw_Farmacias' and xType = 'V' ) 
   Drop View vw_Farmacias 
Go--#SQL

Create View vw_Farmacias 
With Encryption 
As 
	Select  F.IdEstado, E.Nombre as Estado, E.ClaveRenapo, E.Status as EdoStatus, 
		F.IdFarmacia, F.NombreFarmacia as Farmacia, F.Status as FarmaciaStatus, 
		(Case When F.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End) as FarmaciaStatusAux 
	From CatFarmacias F (NoLock) 
	Inner Join CatEstados E (NoLock) On ( F.IdEstado = E.IdEstado ) 
Go--#SQL
