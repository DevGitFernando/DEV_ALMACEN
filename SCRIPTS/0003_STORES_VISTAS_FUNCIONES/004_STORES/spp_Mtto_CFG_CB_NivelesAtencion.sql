
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFG_CB_NivelesAtencion' and xType = 'P')
    Drop Proc spp_Mtto_CFG_CB_NivelesAtencion
Go--#SQL
  
Create Proc spp_Mtto_CFG_CB_NivelesAtencion ( @IdEstado varchar(2), @IdCliente varchar(4), @IdNivel int, @Descripcion varchar(100), @iOpcion smallint )
With Encryption 
As
Begin 
Set NoCount On

	If @IdNivel = 0
	   Select @IdNivel = max(IdNivel) + 1 From CFG_CB_NivelesAtencion (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente 
	   
	Set @IdNivel = IsNull(@IdNivel, 1) 	
	If Not Exists ( Select * From CFG_CB_NivelesAtencion (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdNivel = @IdNivel ) 
	   Begin 
		  Insert Into CFG_CB_NivelesAtencion ( IdEstado, IdCliente, IdNivel, Descripcion ) 
		  Select @IdEstado, @IdCliente, @IdNivel, @Descripcion 
	   End 
	Else
	   Begin 
		  Update CFG_CB_NivelesAtencion Set FechaUpdate = getdate(), Descripcion = @Descripcion 
		  Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdNivel = @IdNivel 
	   End    
	
	Select @IdNivel as Nivel 
End
Go--#SQL  
