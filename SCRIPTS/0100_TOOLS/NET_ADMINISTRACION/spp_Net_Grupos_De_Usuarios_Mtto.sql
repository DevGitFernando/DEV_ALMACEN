If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Grupos_De_Usuarios_Mtto' and xType = 'P' )
	Drop Proc spp_Net_Grupos_De_Usuarios_Mtto 
Go--#SQL 

Create Proc spp_Net_Grupos_De_Usuarios_Mtto ( @IdEstado varchar(2), @IdSucursal varchar(4), @IdGrupo int, @NombreGrupo varchar(50) ) 
with Encryption 
As 
Begin 
Set NoCount On

	If @IdGrupo = 0
	   Select @IdGrupo = max(IdGrupo) + 1 From Net_Grupos_De_Usuarios (NoLock) Where IdEstado = @IdEstado and Idsucursal = @IdSucursal 
	   
	Set @IdGrupo = IsNull(@IdGrupo, 1) 	
	If Not Exists ( Select * From Net_Grupos_De_Usuarios (NoLock) Where IdEstado = @IdEstado and Idsucursal = @IdSucursal and IdGrupo = @IdGrupo ) 
	   Begin 
		  Insert Into Net_Grupos_De_Usuarios ( IdEstado, IdSucursal, IdGrupo, NombreGrupo ) 
		  Select @IdEstado, @IdSucursal, @IdGrupo, @NombreGrupo 
	   End 
	Else
	   Begin 
		  Update Net_Grupos_De_Usuarios Set FechaUpdate = getdate(), NombreGrupo = @NombreGrupo 
		  Where IdEstado = @IdEstado and Idsucursal = @IdSucursal and IdGrupo = @IdGrupo 
	   End    
	
	Select @IdGrupo as Grupo 
End
Go--#SQL  
