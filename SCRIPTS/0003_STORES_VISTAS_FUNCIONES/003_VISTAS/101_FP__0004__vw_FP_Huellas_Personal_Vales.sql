------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FP_Huellas_Personal_Vales' and xType = 'V' ) 
   Drop View vw_FP_Huellas_Personal_Vales
Go--#SQL 
   
Create view vw_FP_Huellas_Personal_Vales 
As 


	Select 
		D.NumDedo, D.Mano, D.NombreMano, D.Dedo, D.NombreDedo, 
		IsNull(H.ReferenciaHuella, '') as ReferenciaHuella, 
		IsNull(P.ApPaterno + ' ' + P.ApMaterno + ' ' + P.Nombre, '') as NombreCompleto, 
		IsNull(convert(varchar(10), H.FechaRegistro, 120), '') as FechaRegistro,  
		(case when IsNull(H.Huella, '') = '' Then 'NO' Else 'SI' End) as HuellaRegistrada, 
		(Case when IsNull(H.Status, '') = 'A' Then 'ACTIVA' Else 'CANCELADA' End) as Status      
	From vw_Manos_Dedos D (NoLock) 
	Left Join FP_Huellas_Vales H (NoLock) On ( D.NumDedo = H.NumDedo ) 
	Left Join Vales_Huellas P (NoLock) On ( H.ReferenciaHuella = P.IdPersonaFirma )
Go--#SQL 

		--select * from vw_FP_Huellas_Personal  

--		select * from vw_Personal  

--		select * from FP_Huellas  


--	sp_listacolumnas vw_FP_Huellas_Personal   

