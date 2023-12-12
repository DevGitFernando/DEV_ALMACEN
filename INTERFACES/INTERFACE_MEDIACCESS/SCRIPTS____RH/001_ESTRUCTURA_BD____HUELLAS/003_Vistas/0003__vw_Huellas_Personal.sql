------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FP_Huellas_Personal' and xType = 'V' ) 
   Drop View vw_FP_Huellas_Personal 
Go--#SQL 
   
Create view vw_FP_Huellas_Personal 
As 


	Select 
		D.NumDedo, D.Mano, D.NombreMano, D.Dedo, D.NombreDedo, 
		IsNull(H.ReferenciaHuella, '') as ReferenciaHuella, 
		IsNull(P.NombreCompleto, '') as NombreCompleto, 
		IsNull(convert(varchar(10), H.FechaRegistro, 120), '') as FechaRegistro,  
		(case when IsNull(H.Huella, '') = '' Then 'NO' Else 'SI' End) as HuellaRegistrada, 
		(Case when IsNull(H.Status, '') = 'A' Then 'ACTIVA' Else 'CANCELADA' End) as Status      
	From vw_Manos_Dedos D (NoLock) 
	Left Join FP_Huellas H (NoLock) On ( D.NumDedo = H.NumDedo ) 
	Left Join vw_Personal P (NoLock) On ( H.ReferenciaHuella = P.IdPersonal ) 

	

Go--#SQL 

		select * from vw_FP_Huellas_Personal  

--		select * from vw_Personal  

--		select * from FP_Huellas  


--	sp_listacolumnas vw_FP_Huellas_Personal   

