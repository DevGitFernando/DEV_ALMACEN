------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FP_Huellas_Vales' and xType = 'V' ) 
   Drop View vw_FP_Huellas_Vales 
Go--#SQL 
   
Create view vw_FP_Huellas_Vales
As 

	Select * -- IdHuella, FechaRegistro, ReferenciaHuella, Dedo, cast(Huella as ) as Huella, Status 
	From FP_Huellas_Vales (NoLock) 
	

Go--#SQL 


-- sp_listacolumnas FP_Huellas 

