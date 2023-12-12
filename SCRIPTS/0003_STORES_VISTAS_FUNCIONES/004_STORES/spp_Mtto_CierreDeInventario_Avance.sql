If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario_Avance' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario_Avance 
Go--#SQL 

Create Proc spp_Mtto_CierreDeInventario_Avance  
With Encryption 
As 
Begin 
Set NoCount On 

	If Exists ( Select Name From sysobjects (nolock) Where Name = 'tmpAvance_CierreInventario' and xType = 'U' ) 
	   Drop Table tmpAvance_CierreInventario 
	   
	Create Table tmpAvance_CierreInventario 
	( 
		IdProceso int identity(1,1) , 
		Descripcion varchar(100) Not Null Default '', 
		Porcentaje numeric(14,2) Not Null Default 0 
	)    


End 
Go--#SQL

