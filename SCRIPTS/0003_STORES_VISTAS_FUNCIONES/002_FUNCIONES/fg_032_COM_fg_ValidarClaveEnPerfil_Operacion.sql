If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_ValidarClaveEnPerfil_Operacion' and xType = 'FN' )
   Drop Function fg_ValidarClaveEnPerfil_Operacion 
Go--#SQL     
      
Create Function dbo.fg_ValidarClaveEnPerfil_Operacion 
( 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', 
	@ValidarProducto bit = 0, 
	@IdProducto varchar(8) = '', @CodigoEAN varchar(30) = '' 
	
)       
Returns bit   
With Encryption 
As 
Begin 
Declare 
	@bValorParametro bit, 
    @sValorParametro varchar(30),  
    @sValor varchar(2) 
    -- @sArbol varchar(10)  
   
	Set @bValorParametro = 1 

	If @ValidarProducto = 1   
	Begin 
		Set @bValorParametro = 0 
		If Exists ( Select * From COM_CFG_Productos_Compras (NoLock) 
					Where IdEstado = @IdEstado and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and Status = 'A' ) 
		Begin 
			Set @bValorParametro = 1  
		End 
	End 

    return @bValorParametro
          
End 
Go--#SQL 

-- select dbo.fg_GetParametro_ValidarClaveEnPerfil('21', '0101' ) 
 