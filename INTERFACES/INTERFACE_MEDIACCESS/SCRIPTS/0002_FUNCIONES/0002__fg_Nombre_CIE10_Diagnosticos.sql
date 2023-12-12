----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_Nombre_CIE10_Diagnosticos' and xType = 'FN' ) 
   Drop Function fg_Nombre_CIE10_Diagnosticos
Go--#SQL  

Create Function dbo.fg_Nombre_CIE10_Diagnosticos(@ClaveDiagnostico varchar(30) = '')  
Returns Varchar(500)
With Encryption 
As 
Begin
Declare 
	@Descripcion Varchar(500) 


	Select @Descripcion = Descripcion
	From CatCIE10_Diagnosticos (NoLock)
	where ClaveDiagnostico = @ClaveDiagnostico
		
	Return IsNull(@Descripcion, '')
	

End 
Go--#SQL 

