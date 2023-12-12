

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_NumeroDeDomingos' and xType = 'FN' ) 
   Drop Function fg_NumeroDeDomingos 
Go--#SQL 
      
Create Function dbo.fg_NumeroDeDomingos(@FechaInicio datetime, @FechaFin datetime)       
Returns int
With Encryption 
As 
Begin 
Declare @iNumero int 
        
    Set @iNumero = 0    
   
	while ( @FechaInicio <= @FechaFin ) 
	Begin 
		if (Select DATEPART(dw, @FechaInicio)) = 7
		Begin
			Set @iNumero = @iNumero + 1
		End
		
		Set @FechaInicio = @FechaInicio + 1
	End 
    
    
    return @iNumero 
          
End 
Go--#SQL 



	