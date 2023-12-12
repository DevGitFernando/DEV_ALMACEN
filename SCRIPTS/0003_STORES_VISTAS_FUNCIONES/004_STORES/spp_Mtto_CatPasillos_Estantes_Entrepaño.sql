If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatPasillos_Estantes_Entrepaño' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatPasillos_Estantes_Entrepaño 
Go--#SQL  

Create Proc spp_Mtto_CatPasillos_Estantes_Entrepaño 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0003', 
	@IdPasillo int = 1, @IdEstante int = 1, @IdEntrepaño int = 1, @Descripcion varchar(50) = 'Estante # 1', 
	@Status varchar(1) = 'A', @EsPicking int = 0, @IdOrden int = 9999
) 
with Encryption 
As 
Begin
Set NoCount On 

	If Not Exists ( Select * From CatPasillos_Estantes_Entrepaños (NoLock) 
				    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepaño = @IdEntrepaño ) 
		Begin 
			Insert Into CatPasillos_Estantes_Entrepaños ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, 
																IdEntrepaño, DescripcionEntrepaño, Status, Actualizado, EsDePickeo, IdOrden ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @IdEntrepaño, @Descripcion, @Status, 0 as Actualizado, @EsPicking, @IdOrden
		End 			    
	Else 
	Begin 
		Update A Set DescripcionEntrepaño = (case when @Status = 'A' then @Descripcion else DescripcionEntrepaño end), 
			Status = @Status, Actualizado = 0, EsDePickeo = @EsPicking, IdOrden =@IdOrden
		From CatPasillos_Estantes_Entrepaños A (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepaño = @IdEntrepaño
	End 

End 
Go--#SQL 
