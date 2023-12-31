If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Grupos_Usuarios_Miembros' and xType = 'P'  )
   Drop Proc spp_Net_Grupos_Usuarios_Miembros 
Go--#SQL 

Create Proc spp_Net_Grupos_Usuarios_Miembros 
( 
	@IdGrupo int, @IdPersonal varchar(8), @LoginUser varchar(50) 
) 
With Encryption 
As 
Begin 
Set NoCount On
	   
	If Not Exists ( Select * From Net_Grupos_Usuarios_Miembros (NoLock) 
			Where IdGrupo = @IdGrupo and IdPersonal = @IdPersonal ) 
	   Begin 
		  Insert Into Net_Grupos_Usuarios_Miembros ( IdGrupo, IdPersonal, LoginUser ) 
		  Select @IdGrupo, @IdPersonal, @LoginUser 
	   End 
	Else 
	   Begin 
				   
			update Net_Grupos_Usuarios_Miembros Set Status = 'A', Actualizado = 0  
			Where IdGrupo = @IdGrupo and IdPersonal = @IdPersonal 	   
	   End 
	   	   
End
Go--#SQL 
