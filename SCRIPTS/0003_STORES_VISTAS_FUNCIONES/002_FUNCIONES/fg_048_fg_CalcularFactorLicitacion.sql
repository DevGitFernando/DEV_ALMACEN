------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_CalcularFactorLicitacion' and xType = 'FN' ) 
   Drop Function fg_CalcularFactorLicitacion 
Go--#SQL 

--			select dbo.fg_CalcularFactorLicitacion( '11', '0002', '0006', '00000353' )  

Create Function dbo.fg_CalcularFactorLicitacion
(
	@IdEstado varchar(2) = '25', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', @IdProducto varchar(8) = '00000265' 
)  
Returns numeric(14,4) 
With Encryption 
As 
Begin 
Declare 
	@dPrecioVenta numeric(14,4), 
	@dFactor numeric(14,4), 	
	@sIdClaveSSA varchar(50), 
	@sClaveSSA varchar(50), 
	@sClaveSSA_Auxiliar varchar(50)  	 

	Set @dFactor = 1 
	Set @dPrecioVenta = 0 
	Set @sIdClaveSSA = '' 
	
	Select @sIdClaveSSA = IdClaveSSA_Sal, @sClaveSSA = ClaveSSA  
	From vw_Productos (NoLock) 
	Where IdProducto = @IdProducto 
	

	------------------- Validar si esta relacionada 
	Select @sClaveSSA_Auxiliar = C.ClaveSSA 
	From vw_Relacion_ClavesSSA_Claves C (NoLock) 
	Where C.IdEstado = @IdEstado and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A'  	
	Set @sClaveSSA = IsNull(@sClaveSSA_Auxiliar, @sClaveSSA) 
					
	
	------------------- Asignacion de precios de acuerdo a la Clave Licitada  
	Select @dFactor = PC.Factor   
	From vw_Claves_Precios_Asignados PC (NoLock)  
	Where PC.IdEstado = @IdEstado and PC.IdCliente = @IdCliente and PC.IdSubCliente = @IdSubCliente and PC.ClaveSSA = @sClaveSSA  	
	
	Set @dFactor = IsNull(@dFactor, 1 ) 
	If @dFactor <= 0  
	   Set @dFactor = 1 
		

	------- SALIDA FINAL 
	return round(@dFactor, 4)  


End  
Go--#SQL
 
