If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_CTE_Privilegios_Grupo' and xType = 'P' )
   Drop Proc spp_Mtto_Net_CTE_Privilegios_Grupo 
Go--#SQL  

Create Proc spp_Mtto_Net_CTE_Privilegios_Grupo 
(
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdGrupo int, @Arbol varchar(6), 
	@Ruta varchar(20), @TipoRama varchar(1) , @Rama int, @RutaCompleta varchar(100), @Status varchar(1) = 'A'  
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_CTE_Privilegios_Grupo (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo and Arbol = @Arbol and Rama = @Rama ) 
		Begin 		
		   Insert Into Net_CTE_Privilegios_Grupo ( IdEstado, IdFarmacia, IdGrupo, Arbol, Ruta, TipoRama, Rama, RutaCompleta, Status, Actualizado ) 
		   Select @IdEstado, @IdFarmacia, @IdGrupo, @Arbol, @Ruta, @TipoRama, @Rama, @RutaCompleta, 'A', 0 
		End    
	Else 
		Begin 
		   Update Net_CTE_Privilegios_Grupo Set FechaUpdate = getdate(), Status = @Status, Actualizado = 0  
		   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo and Arbol = @Arbol and Rama = @Rama 
		End 
	
End 
Go--#SQL  
