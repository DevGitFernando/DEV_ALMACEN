If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos__Ubicaciones_Excluidas_Surtimiento' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos__Ubicaciones_Excluidas_Surtimiento 
Go--#SQL  

Create Proc spp_Mtto_Pedidos__Ubicaciones_Excluidas_Surtimiento 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0003', 
	@IdPasillo int = 1, @IdEstante int = 1, @IdEntrepaño int = 1, @Excluida varchar(1) = 'A', @IdPersonal Varchar(4)
) 
with Encryption 
As 
Begin
Set NoCount On


	If Not Exists ( Select * From Pedidos__Ubicaciones_Excluidas_Surtimiento (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepaño = @IdEntrepaño ) 
		Begin 
			Insert Into Pedidos__Ubicaciones_Excluidas_Surtimiento ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, Excluida ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @IdEntrepaño, @Excluida
		End 
	Else 
		Begin			
			Update A Set Excluida = @Excluida
			From Pedidos__Ubicaciones_Excluidas_Surtimiento A (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepaño = @IdEntrepaño
		End
		
		Insert Into Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, 
																IdEntrepaño, Excluida, IdPersonal )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @IdEntrepaño, @Excluida, @IdPersonal
End 
Go--#SQL 