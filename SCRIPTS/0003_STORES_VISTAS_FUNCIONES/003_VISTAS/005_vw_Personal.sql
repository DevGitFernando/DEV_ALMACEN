If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Personal' and xType = 'V' ) 
   Drop View vw_Personal 
Go--#SQL

Create View vw_Personal 
With Encryption 
As 
	Select  P.IdEstado, F.Estado, P.IdFarmacia, F.Farmacia, P.IdPersonal, 
			P.Nombre, P.ApPaterno, P.ApMaterno,
			( P.Nombre + ' ' + IsNull(P.ApPaterno, '') + ' ' + IsNull(P.ApMaterno, '') ) as NombreCompleto,   
			P.FechaRegistro, U.LoginUser, U.Password, P.Status 
	From CatPersonal P (noLock) 
	Inner Join vw_Farmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 
	Left Join Net_Usuarios U(NoLock) On (P.IdEstado = U.IdEstado And P.IdFarmacia = U.IdSucursal And P.IdPersonal = U.IdPersonal ) 
Go--#SQL 	 	
	