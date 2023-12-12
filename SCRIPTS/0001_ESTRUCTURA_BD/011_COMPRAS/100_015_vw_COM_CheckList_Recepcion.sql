
--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_CheckList_Recepcion' and xType = 'V' ) 
	Drop View vw_COM_CheckList_Recepcion
Go--#SQL

Create View vw_COM_CheckList_Recepcion
With Encryption 
As 
	Select R.IdGrupo, C.DescripcionGrupo as Grupo, R.IdMotivo, R.DescripcionMotivo as Motivo, 
	R.Respuesta_SI as SI, R.Respuesta_SI_RequiereFirma AS SI_Firma,
	R.Respuesta_NO as NO, R.Respuesta_NO_RequiereFirma As NO_Firma,
	R.Respuesta_Rechazo as Rechazo, R.Respuesta_Rechazo_RequiereFirma as Rechazo_Firma,
	R.Status 
	From COM_CheckList_Recepcion R (NoLock) 
	Inner Join COM_Cat_Grupos_Recepcion C (NoLock) On ( C.IdGrupo = R.IdGrupo ) 		
	
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_Adt_CheckList_Recepcion' and xType = 'V' ) 
	Drop View vw_COM_Adt_CheckList_Recepcion
Go--#SQL

Create View vw_COM_Adt_CheckList_Recepcion
With Encryption 
As 
	Select C.IdEmpresa, E.Empresa, C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia, C.FolioOrdenCompra, 
	C.IdGrupo, R.Grupo, C.IdMotivo, R.Motivo, 
	C.Respuesta_SI, C.Respuesta_NO, C.Respuesta_Rechazo, C.Status 
	From COM_Adt_CheckList_Recepcion C (NoLock) 
	Inner Join vw_COM_CheckList_Recepcion R (NoLock) On ( C.IdGrupo = R.IdGrupo and C.IdMotivo = R.IdMotivo )
	Inner Join vw_Empresas E (Nolock) On ( E.IdEmpresa = C.IdEmpresa )
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 		
	
Go--#SQL