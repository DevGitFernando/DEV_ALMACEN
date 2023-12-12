--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Ctrl_Reubicaciones' and xType = 'V' ) 
	Drop View vw_Ctrl_Reubicaciones 
Go--#SQL

Create View vw_Ctrl_Reubicaciones
With Encryption 
As 
	Select C.IdEmpresa, M.Empresa, C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia , C.Folio_Inv, C.FolioMovto_Referencia,
		   Case When C.Status = 'T' Then 1 Else 0 End As Confirmada,
		   C.IdPersonal_Asignado, N.Nombre As PersonalAsignado, C.FechaConfirmacion, C.FechaRegistro,
		   C.IdPersonal_Firma, IsNull(H.NombreCompleto, '') As Persona_Firma, 
		   C.IdPersonal_Autoriza_Extraordinario, IsNull(HH.NombreCompleto, '') As Personal_Autoriza_Extraordinario,
		   Count(P.ClaveSSA) As Claves, Count(E.CodigoEAN) As Productos, Cast(Sum(E.Cantidad) As Int) As Cantidad
	From Ctrl_Reubicaciones C (NoLock)
	Inner Join MovtosInv_Det_CodigosEAN E (NoLock)
		On (C.IdEmpresa = E.IdEmpresa And C.IdEstado = E.IdEstado And C.IdFarmacia = E.IdFarmacia And C.Folio_Inv = E.FolioMovtoInv)
	Inner Join CatPersonalCEDIS N (NoLock)
		On (C.IdEmpresa = E.IdEmpresa And C.IdEstado = E.IdEstado And C.IdFarmacia = E.IdFarmacia And C.IdPersonal_Asignado = N.IdPersonal)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (E.CodigoEAN = P.CodigoEAN)
	Left Join vw_PersonalHuellas H (NoLock) On (C.IdPersonal_Firma = H.IdPersonal)
	Left Join vw_PersonalHuellas HH (NoLock) On (C.IdPersonal_Autoriza_Extraordinario = HH.IdPersonal)
	inner Join vw_Empresas M (NoLock) On (C.IdEmpresa = M.IdEmpresa)
	Inner Join vw_Farmacias F (NoLock) On (C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia)
	Group By C.IdEmpresa, M.Empresa, C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia , C.Folio_Inv, C.FolioMovto_Referencia,
		C.Status, C.IdPersonal_Asignado, N.Nombre,
		C.FechaConfirmacion, C.FechaRegistro, C.IdPersonal_Firma, H.NombreCompleto, C.IdPersonal_Autoriza_Extraordinario, HH.NombreCompleto
	
Go--#SQL