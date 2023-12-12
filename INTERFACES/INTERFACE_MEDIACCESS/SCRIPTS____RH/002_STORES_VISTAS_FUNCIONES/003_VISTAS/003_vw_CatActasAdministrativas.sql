

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CatActasAdministrativas' and xType = 'V' ) 
Drop View vw_CatActasAdministrativas 
Go--#SQL 
 	
Create View vw_CatActasAdministrativas 
With Encryption 
As 
	Select C.IdActa, C.IdEstado, E.Nombre as Estado, C.IdFarmacia, F.NombreFarmacia as Farmacia,	
	C.IdPersonal_Acta, P.NombreCompleto as Personal_Acta, C.IdPersonal_Representante, R.NombreCompleto as Representante,
	C.IdPersonal_Testigo_01, T1.NombreCompleto as Testigo_01, C.IdPersonal_Testigo_02, T2.NombreCompleto as Testigo_02,
	C.FechaActa, C.FechaRegistro, C.Hechos, C.Status
	From CatActasAdministrativas C (NoLock)	 
	Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia )	
	Inner Join vw_Personal P (Nolock) On ( C.IdPersonal_Acta = P.IdPersonal )
	Inner Join vw_Personal R (Nolock) On ( C.IdPersonal_Representante = R.IdPersonal )
	Inner Join vw_Personal T1 (Nolock) On ( C.IdPersonal_Testigo_01 = T1.IdPersonal )
	Inner Join vw_Personal T2 (Nolock) On ( C.IdPersonal_Testigo_02 = T2.IdPersonal )
Go--#SQL

--		Select * From vw_CatActasAdministrativas (Nolock)

	