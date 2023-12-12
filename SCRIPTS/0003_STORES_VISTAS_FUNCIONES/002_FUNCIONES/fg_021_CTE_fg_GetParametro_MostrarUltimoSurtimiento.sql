If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_GetParametro_MostrarUltimoSurtimiento' and xType = 'FN' )
   Drop Function fg_GetParametro_MostrarUltimoSurtimiento  
Go--#SQL     
      
Create Function dbo.fg_GetParametro_MostrarUltimoSurtimiento()       
Returns bit  
With Encryption 
As 
Begin 
Declare 
    @sValorParametro varchar(30),  
    @sValor varchar(10), 
    @bValor bit  
    
    Set @bValor = 1 
    Select @sValorParametro = Upper(Valor) 
    From Net_CFGS_Parametros (NoLock) 
    Where ArbolModulo = 'CTER' and NombreParametro = 'MostrarUltimoSurtimiento' 
    
    Set @sValorParametro = IsNull(@sValorParametro, 'FALSE') 
    If @sValorParametro = 'FALSE' 
       Set @bValor = 0 


    return @bValor  
    
End 
Go--#SQL 

--  select dbo.fg_GetParametro_FechaSistema('21', '0101' ) 


