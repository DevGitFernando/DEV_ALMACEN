If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_TiposTelefonos' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_TiposTelefonos
Go--#SQL
  
Create Proc spp_Mtto_CFDI_TiposTelefonos 
( 
	@IdTipoTelefono varchar(4) = '', @Descripcion varchar(100) = '', @iOpcion smallint = 1 
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


	If @IdTipoTelefono = '*' 
	   Select @IdTipoTelefono = cast( (max(IdTipoTelefono) + 1) as varchar)  From CFDI_TiposTelefonos (NoLock) 

	-- Asegurar que IdTipoTelefono sea valido y formatear la cadena 
	Set @IdTipoTelefono = IsNull(@IdTipoTelefono, '1')
	Set @IdTipoTelefono = right(replicate('0', 4) + @IdTipoTelefono, 4)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_TiposTelefonos (NoLock) Where IdTipoTelefono = @IdTipoTelefono ) 
			  Begin 
				 Insert Into CFDI_TiposTelefonos ( IdTipoTelefono, Descripcion, Status, Actualizado ) 
				 Select @IdTipoTelefono, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFDI_TiposTelefonos Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdTipoTelefono = @IdTipoTelefono  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdTipoTelefono 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFDI_TiposTelefonos Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdTipoTelefono = @IdTipoTelefono 
		   Set @sMensaje = 'La información del Tipo de Telefono ' + @IdTipoTelefono + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdTipoTelefono as Clave, @sMensaje as Mensaje 
End
Go--#SQL 

