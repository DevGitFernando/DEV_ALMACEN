If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_ValidarClaveEnPerfil_Comprador' and xType = 'FN' )
   Drop Function fg_ValidarClaveEnPerfil_Comprador 
Go--#SQL     
      
Create Function dbo.fg_ValidarClaveEnPerfil_Comprador 
( 
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', @IdPersonal varchar(4) = '0011',
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
    @sValor varchar(2), 
	@IdClaveSSA_Sal varchar(4),
	@ClaveSSA varchar(30)  
   
	Set @IdClaveSSA_Sal = ''
	Set @ClaveSSA = ''
   
	Set @bValorParametro = 1 
	
	----- Obtener el idclavessa y la clavessa del producto ean ------------
	Select @IdClaveSSA_Sal = IdClaveSSA_Sal, @ClaveSSA = ClaveSSA From vw_Productos_CodigoEAN
	Where IdProducto = @IdProducto and CodigoEAN = @CodigoEAN
	

	If @ValidarProducto = 1   
	Begin 
		Set @bValorParametro = 0 
		If Exists ( Select * From CFG_COM_Perfiles_Personal_ClavesSSA (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal
					and IdClaveSSA_Sal = @IdClaveSSA_Sal and ClaveSSA = @ClaveSSA and Status = 'A' ) 
		Begin 
			Set @bValorParametro = 1  
		End 
	End 

    return @bValorParametro
          
End 
Go--#SQL 

-- select dbo.fg_ValidarClaveEnPerfil_Comprador('21', '0001', '0011', 1, '00003106', '7500142000065' ) 
 