-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_INTERMED__GetRecetas_Especifico' and xType = 'P' ) 
   Drop Proc spp_INT_RE_INTERMED__GetRecetas_Especifico
Go--#SQL 

Create Proc spp_INT_RE_INTERMED__GetRecetas_Especifico
( 
	@CLUES Varchar(200) = 'GTSSA001553', @FolioDeReceta varchar(50) = '00013885'  
) 
With Encryption 
As 
Begin 

Set NoCount On 
Set DateFormat YMD   


	Exec spp_INT_RE_INTERMED__GetRecetas @CLUES = @CLUES, @FolioDeReceta = @FolioDeReceta 
		

------------------------------------------------ SALIDA FINAL 
----	Select * From #tmp_Recetas 
----	Select * From #tmp_Recetas_Beneficios 
----	Select * From #tmp_Recetas_Diagnosticos 
----	Select * From #tmp_Recetas_Claves 


End 
Go--#SQL 
	