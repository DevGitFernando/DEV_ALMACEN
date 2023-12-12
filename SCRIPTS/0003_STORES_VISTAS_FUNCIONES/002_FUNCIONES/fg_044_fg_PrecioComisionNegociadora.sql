----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'fg_PrecioComisionNegociadora' and xType = 'FN' ) 
   Drop Function fg_PrecioComisionNegociadora 
Go--#SQL 

Create Function dbo.fg_PrecioComisionNegociadora ( @IdClaveSSA varchar(4), @IdLaboratorio varchar(4) )  
Returns Numeric(14,4)
With Encryption 
As 
Begin 
Declare @iValor int

	Set @iValor = 0
	
	Select  @iValor = IsNull(Precio, 0.0000)
	From COM_OCEN_ComisionNegociadoraPrecios (NoLock)
	Where IdClaveSSA_Sal = @IdClaveSSA And IdLaboratorio = @IdLaboratorio
	
	
	Return @iValor 
	
	
End 
Go--#SQL 
