------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FP_Huellas' and xType = 'V' ) 
   Drop View vw_FP_Huellas 
Go--#SQL 
   
Create view vw_FP_Huellas 
As 

	Select * -- IdHuella, FechaRegistro, ReferenciaHuella, Dedo, cast(Huella as ) as Huella, Status 
	From FP_Huellas (NoLock) 
	

Go--#SQL 


-- sp_listacolumnas FP_Huellas 

