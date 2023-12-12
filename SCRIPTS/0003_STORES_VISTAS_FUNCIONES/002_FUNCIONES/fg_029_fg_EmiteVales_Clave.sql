


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_EmiteVales_Clave' and xType = 'FN' )
   Drop Function fg_EmiteVales_Clave  
Go--#SQL     
      
Create Function dbo.fg_EmiteVales_Clave
(
	@IdEstado varchar(2), @IdCliente varchar(4), @ClaveSSA varchar(30)
) 
Returns int 
With Encryption 
As 
Begin 
Declare 
	@iEmiteVales int 
    
    Select @iEmiteVales = EmiteVales   
    From CFG_CB_EmisionVales (NoLock) 
    Where IdEstado = @IdEstado and IdCliente = @IdCliente 
		  and ClaveSSA = @ClaveSSA 
    
    Set @iEmiteVales = IsNull(@iEmiteVales, 0) 

    return @iEmiteVales 
          
End 
Go--#SQL 

--  select dbo.fg_EmiteVales_Clave('11', '0002', '101' ) 
