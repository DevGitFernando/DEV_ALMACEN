-- 	Select * From CatPasillos_Estantes_Entrea�o_Almacen (Nolock)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatPasillos_Estantes_Entrea�o_Almacen' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatPasillos_Estantes_Entrea�o_Almacen 
Go--#SQL

Create Proc spp_Mtto_CatPasillos_Estantes_Entrea�o_Almacen 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0004', 
	@IdPasillo int = 1, @IdEstante int = 1, @IdEntrepa�o int = 1, @Descripcion varchar(50) = 'Estante # 1', 
	@Status varchar(1) = 'A'
)
with Encryption 
As 
Begin
Set NoCount On 

	If Not Exists ( Select * From CatPasillos_Estantes_Entrea�o_Almacen (NoLock) 
				    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepa�o = @IdEntrepa�o ) 
		Begin 
			Insert Into CatPasillos_Estantes_Entrea�o_Almacen ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, 
																IdEntrepa�o, DescripcionEntrepa�o, Status, Actualizado ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @IdEntrepa�o, @Descripcion, @Status, 0 as Actualizado
		End 			    
	Else 
	Begin 
		Update A Set DescripcionEntrepa�o = (case when @Status = 'A' then @Descripcion else DescripcionEntrepa�o end), 
			Status = @Status, Actualizado = 0
		From CatPasillos_Estantes_Entrea�o_Almacen A (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepa�o = @IdEntrepa�o
	End 

End 
Go--#SQL