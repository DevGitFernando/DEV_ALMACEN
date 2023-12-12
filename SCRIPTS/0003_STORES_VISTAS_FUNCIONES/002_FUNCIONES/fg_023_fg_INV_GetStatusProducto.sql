--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_INV_GetStatusProducto' and xType = 'FN' ) 
   Drop Function fg_INV_GetStatusProducto  
Go--#SQL 

Create Function dbo.fg_INV_GetStatusProducto ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4), @IdProducto varchar(8) = '' ) 
Returns varchar(6) 
With Encryption 
As 
Begin 
Declare 
	@sStatus varchar(6) 
	
	Select @sStatus = Status 
	From FarmaciaProductos (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdProducto = @IdProducto 
		   	
	Return @sStatus 
End 
Go--#SQL 
