If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFGS_ConfigurarConexiones_SvrLnk' and xType = 'P' )
   Drop Proc spp_Mtto_CFGS_ConfigurarConexiones_SvrLnk 
Go--#SQL  

Create Proc spp_Mtto_CFGS_ConfigurarConexiones_SvrLnk ( @IdEstado varchar(2), @IdFarmacia varchar(4), @SvrLnk varchar(100), 
	@Host varchar(100), @NombreBD varchar(100), @Usuario varchar(100), @Password varchar(500)  ) 
With Encryption 	
As 
Begin 

	--- Solo debe existir una configuracion de conexion de servidores vinculados 
	--	Delete From CFGS_ConfigurarConexiones_SvrLnk
	--	Where IdEstado = @IdEstado 

	If Not Exists ( Select * From CFGS_ConfigurarConexiones_SvrLnk  (NoLock) 
					Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia )
		Begin
			Insert Into CFGS_ConfigurarConexiones_SvrLnk ( IdEstado, IdFarmacia, SvrLnk, Host, NombreBD, Usuario, Password ) 
			Select @IdEstado, @IdFarmacia, @SvrLnk, @Host, @NombreBD, @Usuario, @Password 
		End 
	Else
		Begin 
			 Update CFGS_ConfigurarConexiones_SvrLnk  Set SvrLnk = @SvrLnk,  Host = @Host, NombreBD = @NombreBD, 
							Usuario = @Usuario, Password = @Password, Status = 'A', Actualizado = 0
			 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		End
		
--	Exec spp_Mtto_CFGS_ConfigurarConexiones_SvrLnk '20', '0003', '200003', 'pho-oax-0003', 'SII_0003', 'sa', '123456'

End 
Go--#SQL   
