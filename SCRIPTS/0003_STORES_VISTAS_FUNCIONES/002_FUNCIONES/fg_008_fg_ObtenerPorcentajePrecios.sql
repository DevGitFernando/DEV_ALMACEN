If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_ObtenerPorcentajePrecios' and xType = 'FN' ) 
   Drop Function fg_ObtenerPorcentajePrecios 
Go--#SQL 

Create Function dbo.fg_ObtenerPorcentajePrecios ( @IdEstado varchar(2) = '25' )  
Returns numeric(14,4) 
With Encryption 
As 
Begin 
Declare @dPorcentaje numeric(14,4) 

	Set @dPorcentaje = 0 
	Select @dPorcentaje = Porcentaje From CFG_PorcentajesPrecios (NoLock) Where IdEstado =  @IdEstado 
	Set @dPorcentaje = IsNull(@dPorcentaje, 0) 
	
	return round(@dPorcentaje, 4)  
End  
Go--#SQL
 
