If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_CTE_Grupos_De_Usuarios' and xType = 'P' )
	Drop Proc spp_Mtto_Net_CTE_Grupos_De_Usuarios 
Go--#SQL 

Create Proc spp_Mtto_Net_CTE_Grupos_De_Usuarios 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdGrupo int, @NombreGrupo varchar(50) 
) 
with Encryption 
As 
Begin 
Set NoCount On

	If @IdGrupo = 0
	   Select @IdGrupo = max(IdGrupo) + 1 From Net_CTE_Grupos_De_Usuarios (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	   
	Set @IdGrupo = IsNull(@IdGrupo, 1) 	
	If Not Exists ( Select * From Net_CTE_Grupos_De_Usuarios (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo ) 
	   Begin 
		  Insert Into Net_CTE_Grupos_De_Usuarios ( IdEstado, IdFarmacia, IdGrupo, NombreGrupo ) 
		  Select @IdEstado, @IdFarmacia, @IdGrupo, @NombreGrupo 
	   End 
	Else
	   Begin 
		  Update Net_CTE_Grupos_De_Usuarios Set FechaUpdate = getdate(), NombreGrupo = @NombreGrupo 
		  Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo 
	   End    
	
	Select @IdGrupo as Grupo 
End
Go--#SQL  
