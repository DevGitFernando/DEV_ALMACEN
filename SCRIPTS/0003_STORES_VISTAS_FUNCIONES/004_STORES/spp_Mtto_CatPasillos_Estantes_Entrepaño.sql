If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatPasillos_Estantes_Entrepa�o' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatPasillos_Estantes_Entrepa�o 
Go--#SQL  

Create Proc spp_Mtto_CatPasillos_Estantes_Entrepa�o 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0003', 
	@IdPasillo int = 1, @IdEstante int = 1, @IdEntrepa�o int = 1, @Descripcion varchar(50) = 'Estante # 1', 
	@Status varchar(1) = 'A', @EsPicking int = 0, @IdOrden int = 9999
) 
with Encryption 
As 
Begin
Set NoCount On 

	If Not Exists ( Select * From CatPasillos_Estantes_Entrepa�os (NoLock) 
				    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepa�o = @IdEntrepa�o ) 
		Begin 
			Insert Into CatPasillos_Estantes_Entrepa�os ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, 
																IdEntrepa�o, DescripcionEntrepa�o, Status, Actualizado, EsDePickeo, IdOrden ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @IdEntrepa�o, @Descripcion, @Status, 0 as Actualizado, @EsPicking, @IdOrden
		End 			    
	Else 
	Begin 
		Update A Set DescripcionEntrepa�o = (case when @Status = 'A' then @Descripcion else DescripcionEntrepa�o end), 
			Status = @Status, Actualizado = 0, EsDePickeo = @EsPicking, IdOrden =@IdOrden
		From CatPasillos_Estantes_Entrepa�os A (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepa�o = @IdEntrepa�o
	End 

End 
Go--#SQL 
