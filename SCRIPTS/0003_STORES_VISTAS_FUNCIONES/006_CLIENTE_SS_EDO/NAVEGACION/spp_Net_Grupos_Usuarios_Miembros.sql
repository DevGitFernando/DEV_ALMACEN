If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros' and xType = 'P'  )
   Drop Proc spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros 
Go--#SQL 

Create Proc spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdGrupo int, @IdUsuario varchar(4), @LoginUser varchar(50) 
) 
With Encryption 
As 
Begin 
Set NoCount On
	   
	If Not Exists ( Select * From Net_CTE_Grupos_Usuarios_Miembros (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo and IdUsuario = @IdUsuario ) 
	   Begin 
		  Insert Into Net_CTE_Grupos_Usuarios_Miembros ( IdEstado, IdFarmacia, IdGrupo, IdUsuario, LoginUser ) 
		  Select @IdEstado, @IdFarmacia, @IdGrupo, @IdUsuario, @LoginUser 
	   End 
	Else 
	   Begin 
				   
			update Net_CTE_Grupos_Usuarios_Miembros Set Status = 'A', Actualizado = 0  
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo and IdUsuario = @IdUsuario 	   
	   End 
	   	   
--	Else
--	   Begin 
--		  Update Net_CTE_Grupos_Usuarios_Miembros Set NombreGrupo = @NombreGrupo 
--		  Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo 
--	   End    
--	
--	Select @IdGrupo as Grupo 
End
Go--#SQL 
