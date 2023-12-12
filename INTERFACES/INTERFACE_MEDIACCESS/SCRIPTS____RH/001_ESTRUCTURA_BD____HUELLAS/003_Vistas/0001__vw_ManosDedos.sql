------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Manos_Dedos' and xType = 'V' ) 
   Drop View vw_Manos_Dedos  
Go--#SQL 
   
Create view vw_Manos_Dedos 
As 

	Select R.NumDedo, R.Mano, M.Descripcion as NombreMano, R.Dedo, D.Descripcion as NombreDedo   
	From FP_Manos_Dedos R (NoLock) 
	Inner Join FP_Manos M (NoLock) On ( R.Mano = M.Mano ) 
	Inner Join FP_Dedos D (NoLock) On ( R.Dedo = D.Dedo ) 
	

Go--#SQL 

Select * 
from vw_Manos_Dedos 


-- sp_listacolumnas FP_Huellas 

