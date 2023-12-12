If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_FACT_GetParametro_AnexarLotes_y_Caducidades' and xType = 'FN' )
   Drop Function fg_FACT_GetParametro_AnexarLotes_y_Caducidades  
Go--#SQL     
      
Create Function dbo.fg_FACT_GetParametro_AnexarLotes_y_Caducidades
(
	@IdEstado varchar(2), @IdFarmacia varchar(4)	
)  
Returns bit 
With Encryption 
As 
Begin 
Declare 
    @sValorParametro varchar(30),  
    @bValor bit     
    
    Select @sValorParametro = ltrim(rtrim(Valor))	 
    From Net_CFG_Parametros_Facturacion (NoLock) 
    Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
          and ArbolModulo = 'FACT' and NombreParametro = 'AnexarLotes_y_Caducidades_EnRemision' 
    
    Set @sValorParametro = IsNull(@sValorParametro, 'FALSE') 
    Set @bValor = 0 
    if upper(@sValorParametro) = 'TRUE' 
        Set @bValor = 1        

    return @bValor
          
End 
Go--#SQL 


