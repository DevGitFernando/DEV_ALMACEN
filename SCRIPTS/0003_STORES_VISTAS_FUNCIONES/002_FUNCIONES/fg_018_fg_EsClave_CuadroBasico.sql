If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_EsClave_CuadroBasico' and xType = 'FN' )
   Drop Function fg_EsClave_CuadroBasico  
Go--#SQL     
      
Create Function dbo.fg_EsClave_CuadroBasico(@IdEstado varchar(2), @IdFarmacia varchar(4), @ClaveSSA varchar(30))       
Returns varchar(10)  
With Encryption 
As 
Begin 
Declare 
    @sValor bit
    
    Select @sValor = 1 
    From vw_CB_CuadroBasico_Farmacias (NoLock) 
    Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		  and ClaveSSA = @ClaveSSA 
    
    Set @sValor = IsNull(@sValor, 0) 

    return @sValor
          
End 
Go--#SQL 

--  select dbo.fg_GetParametro_FechaSistema('21', '0101' ) 
