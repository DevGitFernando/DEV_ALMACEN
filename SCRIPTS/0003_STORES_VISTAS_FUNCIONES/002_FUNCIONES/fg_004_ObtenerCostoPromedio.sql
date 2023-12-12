If Exists ( Select Name From Sysobjects (nolock) Where Name = 'fg_ObtenerCostoPromedio' and xType = 'FN' ) 
   Drop Function fg_ObtenerCostoPromedio 
Go--#SQL

Create Function dbo.fg_ObtenerCostoPromedio (
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', 
	@IdFarmacia varchar(4) = '0001', @IdProducto varchar(8) = '00000010' )  
Returns numeric(14,4) 
With Encryption 
As 
Begin 
Declare @dCostoPromedio numeric(14,4) 
	
	--- Buscar el Producto en las tablas de existencia 
	If Exists ( Select CostoPromedio From FarmaciaProductos (NoLock)  
						     Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
							   and IdFarmacia = @IdFarmacia and IdProducto = @IdProducto ) 
       Begin 
			Select @dCostoPromedio = CostoPromedio 
				From FarmaciaProductos (NoLock)  
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
					and IdFarmacia = @IdFarmacia and IdProducto = @IdProducto 
	   End 
	Else 
	   Begin 
	       Select @dCostoPromedio = CostoPromedio From CFG_CostosPromediosPrecios (NoLock) Where IdProducto = @IdProducto 
	   End 
	
	return round(IsNull(@dCostoPromedio,0), 4)  
End 
Go--#SQL	
 
