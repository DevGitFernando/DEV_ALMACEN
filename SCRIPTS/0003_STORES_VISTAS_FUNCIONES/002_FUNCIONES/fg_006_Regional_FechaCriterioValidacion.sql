If Exists ( Select Name From Sysobjects (nolock) Where Name = 'fg_Regional_FechaCriterioValidacion' and xType = 'FN' ) 
   Drop Function fg_Regional_FechaCriterioValidacion 
Go--#SQL

Create Function dbo.fg_Regional_FechaCriterioValidacion 
( 
	@IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0010')  
Returns int  
With Encryption 
As 
Begin 
Declare @iValor int,  
	    @sModulo varchar(5),  
	    @sParametro varchar(50) 

/* 
	Valor = 1 ==>> Fecha Registro 
	Valor = 2 ==>> Fecha Receta 	
*/ 

	Set @iValor = 0 
	Set @sModulo = 'PFAR' 
	Set @sParametro = 'FechaCriterioValidacion'	 
		 
	Select @iValor = Cast(Valor as varchar) 
		From Net_CFGC_Parametros (NoLock)
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			  and ArbolModulo = @sModulo and  NombreParametro = @sParametro 
			  
	
	return IsNull(@iValor, 1)  
End 
Go--#SQL



