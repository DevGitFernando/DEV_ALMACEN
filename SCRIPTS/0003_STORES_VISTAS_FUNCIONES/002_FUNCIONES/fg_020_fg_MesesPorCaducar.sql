If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_MesesPorCaducar' and xType = 'FN' )
   Drop Function fg_MesesPorCaducar  
Go--#SQL     
      
Create Function dbo.fg_MesesPorCaducar
(
	@Fecha datetime 
) 
Returns int 
With Encryption 
As 
Begin 
Declare 
	@iMeses int 

	Set @iMeses = datediff(mm, getdate(), IsNull(@Fecha, cast('2000-01-01' as datetime))) 
	Set @iMeses = IsNull(@iMeses, 0) 
	
    return @iMeses 
          
End 
Go--#SQL 

--  select dbo.fg_GetParametro_FechaSistema('21', '0101' ) 
