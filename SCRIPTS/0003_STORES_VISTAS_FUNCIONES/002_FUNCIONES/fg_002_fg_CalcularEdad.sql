	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_CalcularEdad' and xType = 'FN' ) 
   Drop Function fg_CalcularEdad 
Go--#SQL	
 	

Create Function dbo.fg_CalcularEdad(@Fecha datetime) 
returns varchar(20) 
With Encryption 
As 
Begin 
Declare @sFecha varchar(20), 
		@iAnios int, 
		@iMeses int, 
		@iDias int 

	Set @sFecha = ''
	Set @iAnios = round( datediff(dd, @Fecha, getdate()) / 365.0, 0) 
	Set @iMeses = round( datediff(dd, @Fecha, getdate()) / 31.0, 0) 
	Set @iDias = datediff(dd, @Fecha, getdate()) 		
	
	--- print cast(@iAnios as varchar) + '   ' + cast(@iMeses as varchar) + '   ' + cast(@iDias as varchar) 
	If @iAnios > 1   
	   Begin 
	       Set @sFecha = cast(@iAnios as varchar) + ' a '
	   End 
    Else 
	   Begin
			If @iMeses > 1   
			   Set @sFecha = cast(@iMeses as varchar) + ' m ' 
			   If @iMeses = 12 
			      Set @sFecha = '1 a '  
			Else 
			   Set @sFecha = cast(@iDias as varchar) + ' d ' 
	   End 
	       
	return @sFecha 
End 
Go--#SQL	
 
	