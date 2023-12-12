If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_GetParametro_TipoDispensacion_Vale' and xType = 'FN' )
   Drop Function fg_GetParametro_TipoDispensacion_Vale 
Go--#SQL     
      
Create Function dbo.fg_GetParametro_TipoDispensacion_Vale(@IdEstado varchar(2), @IdFarmacia varchar(4))       
Returns varchar(2)  
With Encryption 
As 
Begin 
Declare 
    @sValorParametro varchar(30),  
    @sValor varchar(2) 
    
    Select @sValorParametro = Valor 
    From Net_CFGC_Parametros (NoLock) 
    Where IdEstado = @IdEstado and IdFarmacia = right('0000' + @IdFarmacia, 4)  
          and ArbolModulo = 'PFAR' and NombreParametro = 'TipoDispensacionVale' 
    
    Set @sValorParametro = IsNull(@sValorParametro, '00') 
    -- Set @sValor = 0 

    return @sValorParametro
          
End 
Go--#SQL 

-- select dbo.fg_GetParametro_TipoDispensacion_Vale('21', '0101' ) 
 