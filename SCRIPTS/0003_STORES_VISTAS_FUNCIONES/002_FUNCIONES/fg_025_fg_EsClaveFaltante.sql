If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_EsClaveFaltante' and xType = 'FN' )
   Drop Function fg_EsClaveFaltante  
Go--#SQL     
      
Create Function dbo.fg_EsClaveFaltante
(
	@ClaveSSA varchar(30)
) 
Returns bit 
With Encryption 
As 
Begin 
Declare 
	@iFaltante int 
    
    Select @iFaltante = 1  
    From COM_CartasFaltantes_ClavesSSA (NoLock) 
    Where ClaveSSA = @ClaveSSA and Status = 'F' 
    
    Set @iFaltante = IsNull(@iFaltante, 0) 

    return @iFaltante 
          
End 
Go--#SQL 

--  select dbo.fg_EsClaveFaltante( '101' ) 

