


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatFarmacias_ProveedoresVales' and xType = 'P' )
   Drop Proc spp_Mtto_CatFarmacias_ProveedoresVales 
Go--#SQL

Create Proc spp_Mtto_CatFarmacias_ProveedoresVales ( @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1246', 
					@IdProveedor varchar(4) = '0438', @EsProvReembolso tinyint = 0, @iOpcion tinyint = 1 ) 
With Encryption 
As 
Begin 
Set NoCount On 

	Declare @Status varchar(2)
	
	Set @Status = 'A'
	
	If @iOpcion = 1
		Begin 

			If Not Exists ( Select * From CatFarmacias_ProveedoresVales (NoLock) 
							Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdProveedor = @IdProveedor ) 
			   Insert Into CatFarmacias_ProveedoresVales ( IdEstado, IdFarmacia, IdProveedor, EsProv_Reembolso, Status, Actualizado ) 
			   Select @IdEstado, @IdFarmacia, @IdProveedor, @EsProvReembolso, @Status, 0 as Actualizado 
			Else 
			   Update CatFarmacias_ProveedoresVales Set Status = @Status, Actualizado = 0 
			   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdProveedor = @IdProveedor

		End
	Else
		Begin 
			Set @Status = 'C'
			Update CatFarmacias_ProveedoresVales Set Status = @Status, Actualizado = 0 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdProveedor = @IdProveedor
		End
	
End 
Go--#SQL