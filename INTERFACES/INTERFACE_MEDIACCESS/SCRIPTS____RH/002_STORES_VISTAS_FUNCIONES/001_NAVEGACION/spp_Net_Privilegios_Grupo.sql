If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Privilegios_Grupo' and xType = 'P' )
   Drop Proc spp_Net_Privilegios_Grupo 
Go--#SQL  

Create Proc spp_Net_Privilegios_Grupo 
(
	@IdGrupo int, @Arbol varchar(6), 
	@Ruta varchar(20), @TipoRama varchar(1) , @Rama int, @RutaCompleta varchar(100), @Status varchar(1) = 'A'  
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_Privilegios_Grupo (NoLock) 
			Where IdGrupo = @IdGrupo and Arbol = @Arbol and Rama = @Rama ) 
		Begin 		
		   Insert Into Net_Privilegios_Grupo ( IdGrupo, Arbol, Ruta, TipoRama, Rama, RutaCompleta, Status, Actualizado ) 
		   Select @IdGrupo, @Arbol, @Ruta, @TipoRama, @Rama, @RutaCompleta, 'A', 0 
		End    
	Else 
		Begin 
		   Update Net_Privilegios_Grupo Set FechaUpdate = getdate(), Status = @Status, Actualizado = 0  
		   Where IdGrupo = @IdGrupo and Arbol = @Arbol and Rama = @Rama 
		End 
	
End 
Go--#SQL  
