


------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_COM_Perfiles_Personal' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_COM_Perfiles_Personal 
Go--#SQL

Create Proc spp_Mtto_CFG_COM_Perfiles_Personal ( @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	
	If Not Exists ( Select * From CFG_COM_Perfiles_Personal  (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal  ) 
	   Insert Into CFG_COM_Perfiles_Personal  ( IdEstado, IdFarmacia, IdPersonal, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdPersonal, @Status, 0 as Actualizado 
	Else 
	   Update CFG_COM_Perfiles_Personal  Set Status = @Status, Actualizado = 0 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal 

End 
Go--#SQL