--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_PRCS_Truncar' and xType = 'FN' ) 
   Drop Function fg_PRCS_Truncar  
Go--#SQL 

Create Function dbo.fg_PRCS_Truncar ( @Valor numeric(20, 10), @Decimales int = 2  ) 
--Returns numeric(20, 10)  
Returns varchar(100) 
With Encryption 
As 
Begin 

Declare 
	@ParteEntera numeric(20, 10), 
	@ParteDecimal numeric(20, 10), 
	@Retorno numeric(20, 10), 
	@sValorDecimal varchar(50), 
	@iDecimal int, 
	@sCadena varchar(100) 

	Set @sCadena = '' 

	-- Set @valor = 15.2950 
	Set @ParteEntera = floor(@valor) 
	Set @ParteDecimal = @valor - floor(@valor) 
	Set @sValorDecimal = cast(@ParteDecimal as varchar(50)) + replicate('0', 50) 
	--Set @sValorDecimal = SUBSTRING(@sValorDecimal, @Decimales + 3, 1 ) 

	Set @sCadena = left(@sValorDecimal, 2 + @Decimales)  

	Set @Retorno = @ParteEntera + cast(@sCadena as numeric(20, 10)) 


	return @Retorno  

End 
Go--#SQL 


---		select dbo.fg_PRCS_Truncar(6230.438460, 2) 



