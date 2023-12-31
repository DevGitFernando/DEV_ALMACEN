If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Usuarios_Mtto' and xType = 'P' )
   Drop Proc spp_Net_Usuarios_Mtto 
Go--#SQL 

Create Proc spp_Net_Usuarios_Mtto 
( 
    @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdPersonal varchar(4), @LoginUser varchar(50), @Password varchar(500), @iTipo smallint = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If @iTipo = 1 
	   Begin 	
			If Not Exists ( Select LoginUser From Net_Usuarios (NoLock) 
				Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal ) 
			   Insert Into Net_Usuarios ( IdEstado, IdFarmacia, IdPersonal, LoginUser, Password, Status, Actualizado ) 
			   Select @IdEstado, @IdFarmacia, @IdPersonal, @LoginUser, @Password, 'A', 0  
			Else 
			   Update Net_Usuarios Set Password = @Password, FechaUpdate = getdate(), Status = 'A', Actualizado = 0 
			   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal 
	   End 
    Else 
       Update Net_Usuarios Set FechaUpdate = getdate(), Status = 'C', Actualizado = 0 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal 
End
Go--#SQL 
