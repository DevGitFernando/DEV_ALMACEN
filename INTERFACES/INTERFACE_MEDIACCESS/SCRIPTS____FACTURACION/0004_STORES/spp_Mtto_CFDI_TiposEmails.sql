If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_TiposEmails' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_TiposEmails
Go--#SQL
  
Create Proc spp_Mtto_CFDI_TiposEmails 
( 
	@IdTipoEMail varchar(4) = '', @Descripcion varchar(100) = '', @iOpcion smallint = 1 
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


	If @IdTipoEMail = '*' 
	   Select @IdTipoEMail = cast( (max(IdTipoEMail) + 1) as varchar)  From CFDI_TiposEmail (NoLock) 

	-- Asegurar que IdTipoEMail sea valido y formatear la cadena 
	Set @IdTipoEMail = IsNull(@IdTipoEMail, '1')
	Set @IdTipoEMail = right(replicate('0', 4) + @IdTipoEMail, 4)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_TiposEmail (NoLock) Where IdTipoEMail = @IdTipoEMail ) 
			  Begin 
				 Insert Into CFDI_TiposEmail ( IdTipoEMail, Descripcion, Status, Actualizado ) 
				 Select @IdTipoEMail, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFDI_TiposEmail Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdTipoEMail = @IdTipoEMail  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdTipoEMail 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFDI_TiposEmail Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdTipoEMail = @IdTipoEMail 
		   Set @sMensaje = 'La información del Tipo de Telefono ' + @IdTipoEMail + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdTipoEMail as Clave, @sMensaje as Mensaje 
End
Go--#SQL 

