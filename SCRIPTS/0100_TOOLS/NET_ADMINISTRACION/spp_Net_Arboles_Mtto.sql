If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Arboles_Mtto' and xType = 'P' ) 
   Drop Proc spp_Net_Arboles_Mtto 
Go--#SQL  
  
Create Proc spp_Net_Arboles_Mtto 
(
    @Arbol varchar(5), 
    @Nombre varchar(52)
)
With Encryption 
As
Begin
Begin try
        If Not Exists ( Select * From Net_Arboles (NoLock) 
           Where 
               Arbol = @Arbol  )
        Begin
            Insert Into Net_Arboles
            (
               Arbol, 
               Nombre
            )
            Values
            (
               @Arbol, 
               @Nombre
            )
            
        End
        Else
        Begin
            Update Net_Arboles Set 
                Arbol = @Arbol, 
                Nombre = @Nombre
           Where 
               Arbol = @Arbol 
            
        End
End try
Begin catch
   RaisError ('Error al actualizar en la tabla Net_Arboles', 16,10 )
End catch
End
Go--#SQL 