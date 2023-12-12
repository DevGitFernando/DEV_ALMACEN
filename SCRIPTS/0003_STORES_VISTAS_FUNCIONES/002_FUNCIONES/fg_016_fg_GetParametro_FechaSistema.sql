If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_GetParametro_FechaSistema' and xType = 'FN' )
   Drop Function fg_GetParametro_FechaSistema  
Go--#SQL     
      
Create Function dbo.fg_GetParametro_FechaSistema(@IdEstado varchar(2), @IdFarmacia varchar(4))       
Returns varchar(10)  
With Encryption 
As 
Begin 
Declare 
    @sValorParametro varchar(30),  
    @sValor varchar(10) 
    
    Select @sValorParametro = Valor 
    From Net_CFGC_Parametros (NoLock) 
    Where IdEstado = @IdEstado and IdFarmacia = right('0000' + @IdFarmacia, 4)  
          and ArbolModulo = 'PFAR' and NombreParametro = 'FechaOperacionSistema' 
    
    Set @sValorParametro = IsNull(@sValorParametro, convert(varchar(10), getdate(), 10)  ) 
    -- Set @sValor = 0 

    return @sValorParametro
          
End 
Go--#SQL 

--  select dbo.fg_GetParametro_FechaSistema('21', '0101' ) 


