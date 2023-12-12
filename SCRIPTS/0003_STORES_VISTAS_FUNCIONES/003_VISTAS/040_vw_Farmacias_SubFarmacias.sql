If Exists ( Select Name From sysobjects (NoLock) Where Name = 'vw_Farmacias_SubFarmacias' and xType='V' )
	Drop View vw_Farmacias_SubFarmacias
Go--#SQL

Create View vw_Farmacias_SubFarmacias
With Encryption 
As
	Select  SF.IdEstado, F.Estado, SF.IdFarmacia, F.Farmacia, 
			SF.IdSubFarmacia, SF.Descripcion As SubFarmacia, SF.Descripcion As Descripcion, 			
			(case when CSF.EsConsignacion = 0 Then 'Venta' Else 'Consignación' end) as Tipo, CSF.EsConsignacion, 
			CSF.EmulaVenta, 
			SF.Status 
    From CatEstados_SubFarmacias CSF (NoLock) 
    Inner Join CatFarmacias_SubFarmacias SF (NoLock) On ( CSF.IdEstado = SF.IdEstado and CSF.IdSubFarmacia = SF.IdSubFarmacia ) 
    Inner Join vw_Farmacias F (NoLock) On ( F.IdEstado = SF.IdEstado and F.IdFarmacia = SF.IdFarmacia ) 
--	Inner Join CatFarmacias F(NoLock) On (SF.IdEstado = F.IdEstado And SF.IdFarmacia = F.IdFarmacia )
--	Inner Join CatEstados E(NoLock) On ( SF.IdEstado = E.IdEstado )
Go--#SQL 

--  select * from vw_Farmacias_SubFarmacias 
    