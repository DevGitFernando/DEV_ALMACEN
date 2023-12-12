
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFG_CB_NivelesAtencion_Miembros' and xType = 'P')
    Drop Proc spp_Mtto_CFG_CB_NivelesAtencion_Miembros
Go--#SQL
  
Create Proc spp_Mtto_CFG_CB_NivelesAtencion_Miembros ( @IdEstado varchar(2), @IdCliente varchar(4), @IdNivel int, @IdFarmacia varchar(4), @iOpcion smallint )
With Encryption 
As
Begin 
Set NoCount On

	If Not Exists ( Select * From CFG_CB_NivelesAtencion_Miembros (NoLock) 
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdNivel = @IdNivel and IdFarmacia = @IdFarmacia ) 
	   Begin 
		  Insert Into CFG_CB_NivelesAtencion_Miembros ( IdEstado, IdCliente, IdNivel, IdFarmacia ) 
		  Select @IdEstado, @IdCliente, @IdNivel, @IdFarmacia 
	   End 
	Else 
	   Begin 				   
			update CFG_CB_NivelesAtencion_Miembros Set Status = 'A', Actualizado = 0  
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdNivel = @IdNivel and IdFarmacia = @IdFarmacia 	   
	   End 
	   	   
--	Select @IdNivel as Grupo 
End
Go--#SQL 
