


---------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_Cliente_SubCliente_Estado' and xType = 'TF' )
   Drop Function fg_Cliente_SubCliente_Estado  
Go--#SQL 
  
Create Function dbo.fg_Cliente_SubCliente_Estado(@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182')
returns @Tabla Table 
( 
	IdCliente varchar(4), 
	IdSubCliente varchar(4) 
)
With Encryption 
As 
Begin 
Declare 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4) 

	Set @IdCliente = ( Select Distinct Valor From Net_CFGC_Parametros (NoLock) 
    Where IdEstado = @IdEstado and IdFarmacia = right('0000' + @IdFarmacia, 4)  
          and ArbolModulo In('ALMN', 'PFAR') and NombreParametro = 'ClienteSeguroPopular' )

	Set @IdSubCliente = ( Select Distinct Valor From Net_CFGC_Parametros (NoLock) 
    Where IdEstado = @IdEstado and IdFarmacia = right('0000' + @IdFarmacia, 4)  
          and ArbolModulo In('ALMN', 'PFAR') and NombreParametro = 'IdSubCliente' )
	
	Insert Into @Tabla Values ( @IdCliente, @IdSubCliente )	
	
	return 

End
Go--#SQL 

--	Select IdCliente, IdSubCliente From dbo.fg_Cliente_SubCliente_Estado('21', '0182') 