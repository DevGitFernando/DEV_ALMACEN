-- 	Select * From CatPasillos_Estantes (Nolock)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatPasillos_Estantes' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatPasillos_Estantes 
Go--#SQL

Create Proc spp_Mtto_CatPasillos_Estantes 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0003', 
	@IdPasillo int = 1, @IdEstante int = 1, @Descripcion varchar(50) = 'Estante # 1', @Status varchar(1) = 'A'
)
with Encryption 
As 
Begin
Set NoCount On 

	If Not Exists ( Select * From CatPasillos_Estantes (NoLock) 
				    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					and IdPasillo = @IdPasillo And IdEstante = @IdEstante ) 
		Begin 
			Insert Into CatPasillos_Estantes ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, DescripcionEstante, Status, Actualizado ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @Descripcion, @Status, 0 as Actualizado
		End 			    
	Else 
	Begin 
		Update A Set DescripcionEstante = (case when @Status = 'A' then @Descripcion else DescripcionEstante end), 
			Status = @Status, Actualizado = 0
		From CatPasillos_Estantes A (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and IdPasillo = @IdPasillo And IdEstante = @IdEstante
	End 

End 
Go--#SQL