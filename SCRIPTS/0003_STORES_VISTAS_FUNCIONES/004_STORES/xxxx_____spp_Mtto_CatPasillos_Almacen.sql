If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatPasillos_Almacen' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatPasillos_Almacen 
Go--#SQL

Create Proc spp_Mtto_CatPasillos_Almacen 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0004', 
	@IdPasillo int = 1, @Descripcion varchar(50) = 'Pasillo # 1', @Status varchar(1) = 'A'
)
with Encryption 
As 
Begin
Set NoCount On 

	If Not Exists ( Select * From CatPasillos_Almacen (NoLock) 
				    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPasillo = @IdPasillo ) 
		Begin 
			Insert Into CatPasillos_Almacen ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, DescripcionPasillo, Status, Actualizado ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @Descripcion, @Status, 0 as Actualizado
		End 			    
	Else 
	Begin 
		Update A Set DescripcionPasillo = (case when @Status = 'A' then @Descripcion else DescripcionPasillo end), 
			Status = @Status, Actualizado = 0
		From CatPasillos_Almacen A (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPasillo = @IdPasillo 
	End 

End 
Go--#SQL
   
--   sp_listacolumnas CatPasillos_Almacen
   