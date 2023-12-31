If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Grupos_Usuarios_Miembros' and xType = 'P'  )
   Drop Proc spp_Net_Grupos_Usuarios_Miembros 
Go--#SQL 

Create Proc [dbo].[spp_Net_Grupos_Usuarios_Miembros] ( @IdEstado varchar(2), @IdSucursal varchar(4), @IdGrupo int, 
 @IdPersonal varchar(4), @LoginUser varchar(50) ) 
With Encryption 
As 
Begin 
Set NoCount On
	   
	If Not Exists ( Select * From Net_Grupos_Usuarios_Miembros (NoLock) 
			Where IdEstado = @IdEstado and Idsucursal = @IdSucursal and IdGrupo = @IdGrupo and IdPersonal = @IdPersonal ) 
	   Begin 
		  Insert Into Net_Grupos_Usuarios_Miembros ( IdEstado, IdSucursal, IdGrupo, IdPersonal, LoginUser ) 
		  Select @IdEstado, @IdSucursal, @IdGrupo, @IdPersonal, @LoginUser 
	   End 
	Else 
	   Begin 
				   
			update Net_Grupos_Usuarios_Miembros Set Status = 'A', Actualizado = 0  
			Where IdEstado = @IdEstado and Idsucursal = @IdSucursal and IdGrupo = @IdGrupo and IdPersonal = @IdPersonal 	   
	   End 
	   	   
--	Else
--	   Begin 
--		  Update Net_Grupos_Usuarios_Miembros Set NombreGrupo = @NombreGrupo 
--		  Where IdEstado = @IdEstado and Idsucursal = @IdSucursal and IdGrupo = @IdGrupo 
--	   End    
--	
--	Select @IdGrupo as Grupo 
End
Go--#SQL 
