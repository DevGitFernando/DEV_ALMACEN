--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_PRCS_Redondear' and xType = 'FN' ) 
   Drop Function fg_PRCS_Redondear  
Go--#SQL 

Create Function dbo.fg_PRCS_Redondear ( @Valor numeric(20, 10), @Decimales int = 2 ) 
Returns numeric(20, 10)  
With Encryption 
As 
Begin 

Declare 
	@ParteDecimal numeric(20, 10), 
	@sValorDecimal varchar(50), 
	@iDecimal int   


	-- Set @valor = 15.2950 
	Set @ParteDecimal = @valor - floor(@valor) 
	Set @sValorDecimal = cast(@ParteDecimal as varchar(50)) + replicate('0', 50) 
	Set @sValorDecimal = SUBSTRING(@sValorDecimal, @Decimales + 3, 1 ) 
	Set @iDecimal = @sValorDecimal


	If @iDecimal >= 5 
		Begin 
			Set @ParteDecimal = round(@valor, @Decimales, 0) 
		End 
	Else 
		Begin 
			Set @ParteDecimal = round(@valor, @Decimales, 1) 
		End 


	return @ParteDecimal  

End 
Go--#SQL 


---		select dbo.fg_PRCS_Redondear(15.295, 2)

