--------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'fg_CalcularPrecioVenta' and xType = 'FN' ) 
   Drop Function fg_CalcularPrecioVenta 
Go--#SQL	

Create Function dbo.fg_CalcularPrecioVenta
(
	@Tipo tinyint = 1, @IdCliente varchar(4) = '0001', @IdSubCliente varchar(4) = '0001', @IdProducto varchar(8) = '00000001' 
)  
Returns numeric(14,2) 
With Encryption 
As 
Begin 
Declare @dPrecioVenta numeric(14,4), 
		@dPorceUtil_Cliente numeric(14,4), @valorPorcentaje numeric(14,4), 
		@dPrecioMaxPub numeric(14,2), @dPorcDescto numeric(14,2), @dUtilidadProducto numeric(14,2), 
		@dCostoPromedio numeric(14,4)    

	
	-- Obtener le Porcentaje de Utilidad para el SubCliente del Cliente solicitado 
	Select @dPorceUtil_Cliente =  0 --- PorcUtilidad From CatSubClientes (NoLock) Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
	Set @dPorceUtil_Cliente = IsNull(@dPorceUtil_Cliente, 0) 
	--- Se modifica el calculo de precio de venta para EPharma, se implementan descuentos 

	
	-- Obtener el Precio Maximo al Publico y el Descuento General 
	Select @dPrecioMaxPub = PrecioMaxPublico, @dPorcDescto = DescuentoGral, @dUtilidadProducto = UtilidadProducto   
		   From CatProductos (NoLock) Where IdProducto = @IdProducto 
	Set @dPrecioMaxPub = IsNull(@dPrecioMaxPub, 0) 
	Set @dPorcDescto = IsNull(@dPorcDescto, 0) 
			
	-- Obtener el Costo Promedio del Producto para el Calculo del Precio de Venta 
	Select @dCostoPromedio = CostoPromedio From CFG_CostosPromediosPrecios (NoLock) Where IdProducto = @IdProducto  
	Set @dCostoPromedio = IsNull(@dCostoPromedio, 0) 
	
	
	-- Calcular Precio de Venta 
    If @dPrecioMaxPub > 0 
       Begin 
	       Set @valorPorcentaje = ( case when @dPorcDescto > 0 then @dPorcDescto / 100.0 else 0 end )  	   
   	       Set @dPrecioVenta = @dPrecioMaxPub - ( @dPrecioMaxPub * ( @valorPorcentaje ) )  
   	   End 
   	Else 
   	   Begin 
		   Set @valorPorcentaje = ( case when @dUtilidadProducto > 0 then @dUtilidadProducto / 100.0 else 0 end )  	   
           Set @dPrecioVenta = round(@dCostoPromedio * ( 1 + @valorPorcentaje  ), 4)  

		   -- Aplicar Descuento 
		   Set @valorPorcentaje = ( case when @dPorcDescto > 0 then @dPorcDescto / 100.0 else 0 end )  	   
           -- Set @dPrecioVenta = round(@dPrecioVenta * ( 1 + @valorPorcentaje  ), 4)  
		   Set @dPrecioVenta = round(@dPrecioVenta, 4) - round(@dPrecioVenta * ( 0 + @valorPorcentaje  ), 4)             
            
		   --- Aplicar Descuento 
		   -- Set @valorPorcentaje = ( case when @dPorcDescto > 0 then @dPorcDescto / 100.0 else 0 end )  	   
		   -- Set @dPrecioVenta = round(@dPrecioVenta, 4) - round(@dPrecioVenta * ( 0 + @valorPorcentaje  ), 4) 
       End 	

	-- Incrementar Utilidad adicional sobre Ventas a Credito 
	If @Tipo <> 1 
	   Begin
           Set @valorPorcentaje = ( case when @dPorceUtil_Cliente > 0 then @dPorceUtil_Cliente / 100.0 else 0 end )  
           Set @dPrecioVenta = round(@dPrecioVenta * ( 1 + @valorPorcentaje  ), 4)  	    
	   End  	
	
	return round(@dPrecioVenta, 4)  
End 
Go--#SQL
 
