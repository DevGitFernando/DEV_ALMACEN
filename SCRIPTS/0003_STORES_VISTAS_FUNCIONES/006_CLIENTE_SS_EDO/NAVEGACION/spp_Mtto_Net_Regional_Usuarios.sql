If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Net_Regional_Usuarios' and xType = 'P')
    Drop Proc spp_Mtto_Net_Regional_Usuarios
Go--#SQL
  
Create Proc spp_Mtto_Net_Regional_Usuarios 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdUsuario varchar(4), @Nombre varchar(200), 
	@Login varchar(20), @Password varchar(500), @iOpcion smallint 
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdUsuario = '*' 
	  Begin
		Select @IdUsuario = cast( (max(IdUsuario) + 1) as varchar)  
		From Net_Regional_Usuarios (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdUsuario = IsNull(@IdUsuario, '1')
	Set @IdUsuario = right(replicate('0', 4) + @IdUsuario, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Net_Regional_Usuarios (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdUsuario = @IdUsuario ) 
			  Begin 
				 Insert Into Net_Regional_Usuarios ( IdEstado, IdFarmacia, IdUsuario, Nombre, Login, Password, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdUsuario, @Nombre, @Login, @Password, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update Net_Regional_Usuarios Set Password = @Password, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdUsuario = @IdUsuario 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdUsuario 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update Net_Regional_Usuarios Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdUsuario = @IdUsuario
		   Set @sMensaje = 'La información del Usuario ' + @IdUsuario + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdUsuario as Clave, @sMensaje as Mensaje 
End
Go--#SQL

