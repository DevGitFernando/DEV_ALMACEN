----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_PrecioMinLicitacionPorEstado' and xType = 'FN' ) 
   Drop Function fg_PrecioMinLicitacionPorEstado
Go--#SQL  

Create Function dbo.fg_PrecioMinLicitacionPorEstado (@CodigoEAN varchar(30), @IdEstado Varchar(2))  
Returns Numeric(14, 4)
With Encryption 
As 
Begin
Declare @IdClaveSSA Varchar(4),
		@PrecioCaja Numeric(14, 4)


		Select @IdClaveSSA = IdClaveSSA_Sal
		From vw_productos_CodigoEAN (NoLock)
		Where CodigoEAN = @CodigoEAN
		
		Select @PrecioCaja = Min(PrecioCaja)
		From vw_Claves_Precios_Asignados P (NoLock)
		Where Status = 'A' And @IdClaveSSA = P.IdClaveSSA And IdEStado = @IdEstado
		
		Return IsNull(@PrecioCaja, 0.0000)
	

End 
Go--#SQL 

