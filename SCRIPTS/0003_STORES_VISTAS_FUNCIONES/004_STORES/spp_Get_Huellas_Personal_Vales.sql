If Exists ( Select * From Sysobjects (NoLock) Where Name  = 'spp_Get_Huellas_Personal_Vales' and xType = 'P' ) 
   Drop Proc spp_Get_Huellas_Personal_Vales 
Go--#SQL 

Create Proc spp_Get_Huellas_Personal_Vales ( @ReferenciaHuella varchar(100) = '00000001' ) 
As 
Begin 
--Set NoCount On 

--	vw_FP_Huellas_Personal   

------------------------------ Obtener los datos 
	Select 
		NumDedo, Mano, NombreMano, Dedo, NombreDedo, 
		space(100) as ReferenciaHuella, space(200) as NombreCompleto, space(20) as FechaRegistro, 'NO' as HuellaRegistrada, space(10) as Status 
	Into #tmpDedos 
	From vw_Manos_Dedos 	
		
	Select 
		NumDedo, Mano, NombreMano, Dedo, NombreDedo, ReferenciaHuella, NombreCompleto, FechaRegistro, HuellaRegistrada, Status 
	Into #tmpHuellas 	
	From vw_FP_Huellas_Personal_Vales 
	Where ReferenciaHuella = @ReferenciaHuella 
------------------------------ Obtener los datos 
	
	
------------------------------ Cruzar las huellas vs dedos  	
	Update D Set ReferenciaHuella = H.ReferenciaHuella, NombreCompleto = H.NombreCompleto, FechaRegistro = H.FechaRegistro, HuellaRegistrada = 'SI', 
				Status = H.Status
	From #tmpDedos D 
	Inner Join #tmpHuellas H (NoLock) On ( D.NumDedo = H.NumDedo ) 
------------------------------ Cruzar las huellas vs dedos  		
	
	
----	If ( select count(*) From #tmpHuellas ) = 0   
----	Begin
----		Insert Into #tmpHuellas 
----		Select 
----			NumDedo, Mano, NombreMano, Dedo, NombreDedo, '' as ReferenciaHuella, '' as NombreCompleto, '' as FechaRegistro, 'NO' as HuellaRegistrada 
----		From vw_Manos_Dedos 		 
----	End 
	
------------------------- Huellas localizadas 
	Select NumDedo, NombreMano, NombreDedo, HuellaRegistrada, FechaRegistro, Status 
	From #tmpDedos 

End 
Go--#SQL 

   

