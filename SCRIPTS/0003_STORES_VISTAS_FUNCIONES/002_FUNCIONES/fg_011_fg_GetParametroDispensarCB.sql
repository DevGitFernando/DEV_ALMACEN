If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_Parametro_CTE_DispensarSoloCuadroBasico' and xType = 'FN' )
   Drop Function fg_Parametro_CTE_DispensarSoloCuadroBasico 
Go--#SQL 
      
Create Function dbo.fg_Parametro_CTE_DispensarSoloCuadroBasico(@IdEstado varchar(2), @IdFarmacia varchar(4))       
Returns bit 
With Encryption 
As 
Begin 
Declare 
    @sValorParametro varchar(30),  
    @bValor bit     
    
    Select @sValorParametro = Valor 
    From Net_CFGC_Parametros (NoLock) 
    Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
          and ArbolModulo = 'PFAR' and NombreParametro = 'DispensarSoloCuadroBasico' 
    
    Set @sValorParametro = IsNull(@sValorParametro, 'FALSE') 
    Set @bValor = 0 
    if upper(@sValorParametro) = 'TRUE' 
        Set @bValor = 1        

    return @bValor
          
End 
Go--#SQL 

