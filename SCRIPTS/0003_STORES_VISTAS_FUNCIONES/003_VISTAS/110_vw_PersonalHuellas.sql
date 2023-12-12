



--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PersonalHuellas' and xType = 'V' ) 
	Drop View vw_PersonalHuellas
Go--#SQL

Create View vw_PersonalHuellas 
With Encryption 
As 
	Select IdPersonal, Nombre, ApPaterno, ApMaterno, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) As NombreCompleto ,
	IdEstado, IdFarmacia, Status
	From CatPersonalHuellas
		
	
Go--#SQL

