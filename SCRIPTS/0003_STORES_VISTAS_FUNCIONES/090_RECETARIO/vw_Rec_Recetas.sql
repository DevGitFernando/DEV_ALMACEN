

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Rec_Recetas' and xType = 'V' ) 
	Drop View vw_Rec_Recetas   
Go--#SQL 	

Create View vw_Rec_Recetas 
With Encryption 
As 
	Select B.IdEstado, F.Estado, B.IdFarmacia, F.Farmacia,
			B.IdReceta As Folio, C.IdBeneficiario,		   
		   (C.Nombre + ' ' + C.ApPaterno + ' ' + C.ApMaterno ) as NombreBeneficiario,
			C.Sexo, C.FechaNacimiento, dbo.fg_CalcularEdad(C.FechaNacimiento) as Edad, C.FolioReferencia,		   
		   M.IdMedico, (M.Nombre + ' ' + M.ApPaterno + ' ' + M.ApMaterno) as NombreMedico,
		   M.NumCedula, B.FechaRegistro, B.Status As StatusReceta  
	From Rec_Recetas B (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( B.IdEstado = F.IdEstado and B.IdFarmacia = F.IdFarmacia ) 
	Inner Join Rec_CatBeneficiarios C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdFarmacia = C.IdFarmacia And B.IdBeneficiario = C.IdBeneficiario )
	Inner Join Rec_CatMedicos M (Nolock) On ( B.IdEstado = M.IdEstado and B.IdFarmacia = M.IdFarmacia And B.IdMedico = M.IdMedico )  

Go--#SQL	