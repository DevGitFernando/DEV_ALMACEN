If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_FormatearCadena' and xType = 'FN' )
   Drop Function fg_FormatearCadena   
Go--#SQL     
      
Create Function dbo.fg_FormatearCadena( @Cadena varchar(max) = '', @Caracter varchar(1) = '0', @Largo int = 4 )       
Returns varchar(max)  
With Encryption 
As 
Begin 
Declare 
	@sValor varchar(max) 

	If @Cadena = '' or @Cadena = '*' 
		Begin 
		    Set @sValor = IsNull(@Cadena, '') 
		End 
	Else 
		Begin 
			Set @sValor = IsNull(@Cadena, '') 
			Set @sValor = right(replicate(@Caracter, @Largo) + @Cadena, @Largo) 
		End 


    return @sValor
          
End 
Go--#SQL 

--	select len(dbo.fg_FormatearCadena('1', '0', 8)), dbo.fg_FormatearCadena('*', '0', 8)  

