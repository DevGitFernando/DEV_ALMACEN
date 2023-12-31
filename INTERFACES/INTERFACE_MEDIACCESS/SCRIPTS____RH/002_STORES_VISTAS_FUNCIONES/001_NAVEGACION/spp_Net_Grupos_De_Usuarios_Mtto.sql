If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Grupos_De_Usuarios_Mtto' and xType = 'P' )
	Drop Proc spp_Net_Grupos_De_Usuarios_Mtto 
Go--#SQL 

Create Proc spp_Net_Grupos_De_Usuarios_Mtto ( @IdGrupo int, @NombreGrupo varchar(50) ) 
with Encryption 
As 
Begin 
Set NoCount On

	If @IdGrupo = 0
	   Select @IdGrupo = max(IdGrupo) + 1 From Net_Grupos_De_Usuarios (NoLock) 
	   
	Set @IdGrupo = IsNull(@IdGrupo, 1) 	
	If Not Exists ( Select * From Net_Grupos_De_Usuarios (NoLock) Where IdGrupo = @IdGrupo ) 
	   Begin 
		  Insert Into Net_Grupos_De_Usuarios ( IdGrupo, NombreGrupo ) 
		  Select @IdGrupo, @NombreGrupo 
	   End 
	Else
	   Begin 
		  Update Net_Grupos_De_Usuarios Set FechaUpdate = getdate(), NombreGrupo = @NombreGrupo 
		  Where IdGrupo = @IdGrupo 
	   End    
	
	Select @IdGrupo as Grupo 
End
Go--#SQL  
