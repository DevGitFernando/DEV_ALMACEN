If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Proveedores_Mtto' and xType = 'P' )
   Drop Proc spp_Net_Proveedores_Mtto
Go--#SQL 

Create Proc spp_Net_Proveedores_Mtto ( @IdProveedor varchar(4), @LoginProv varchar(20), @Password varchar(500), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_Proveedores (NoLock) Where IdProveedor = @IdProveedor ) 
	   Insert Into Net_Proveedores ( IdProveedor, LoginProv, Password, Status, Actualizado ) Select @IdProveedor, @LoginProv, @Password, @Status, 0 
	else 
	   Update Net_Proveedores Set Password = @Password, Status = @Status, Actualizado = 0 Where IdProveedor = @IdProveedor 

End 
Go--#SQL
