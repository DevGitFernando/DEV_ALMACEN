------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatServicios_Areas_Farmacias' and xType = 'P' )
   Drop Proc spp_Mtto_CatServicios_Areas_Farmacias 
Go--#SQL
 
Create Proc spp_Mtto_CatServicios_Areas_Farmacias ( @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdServicio varchar(3), @IdArea varchar(3), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CatServicios_Areas_Farmacias (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdServicio = @IdServicio and IdArea = @IdArea ) 
	   Insert Into CatServicios_Areas_Farmacias ( IdEstado, IdFarmacia, IdServicio, IdArea, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdServicio, @IdArea, @Status, 0 as Actualizado 
	Else 
	   Update CatServicios_Areas_Farmacias Set Status = @Status, Actualizado = 0 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdServicio = @IdServicio and IdArea = @IdArea

End 
Go--#SQL
