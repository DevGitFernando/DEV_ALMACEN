If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CteReg_Auditoria_Login' and xType = 'P')
    Drop Proc spp_Mtto_CteReg_Auditoria_Login
Go--#SQL

Create Proc spp_Mtto_CteReg_Auditoria_Login ( @IdEstado varchar(2), @IdUsuario varchar(4), @IdSesion varchar(8), @MAC_Address varchar(20) )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@iActualizado smallint  

	-- Se inicializan las variables.
	Set @sMensaje = ''
	Set @iActualizado = 0 

	If @IdSesion = '*' 
	  Begin
		Select @IdSesion = cast( (max(IdSesion) + 1) as varchar)  
		From CteReg_Auditoria_Login (NoLock) 
		Where IdEstado = @IdEstado And IdUsuario = @IdUsuario
	  End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdSesion = IsNull(@IdSesion, '1')
	Set @IdSesion = right(replicate('0', 8) + @IdSesion, 8)

	-- Si la sesion no existe, se inserta.
	If Not Exists ( Select * From CteReg_Auditoria_Login (NoLock) Where IdEstado = @IdEstado And IdUsuario = @IdUsuario And IdSesion = @IdSesion ) 
	  Begin 
		 Insert Into CteReg_Auditoria_Login ( IdEstado, IdUsuario, IdSesion, FechaRegistro, MAC_Address, Actualizado ) 
		 Select @IdEstado, @IdUsuario, @IdSesion, GetDate(), @MAC_Address, @iActualizado 
	  End 
	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdSesion

	-- Regresar la Clave Generada
    Select @IdSesion as Clave, @sMensaje as Mensaje 
End
Go--#SQL 