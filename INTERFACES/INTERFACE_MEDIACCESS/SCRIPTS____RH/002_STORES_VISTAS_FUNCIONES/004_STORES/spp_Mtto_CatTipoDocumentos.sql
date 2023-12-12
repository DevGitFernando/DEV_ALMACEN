

	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatTipoDocumentos' and xType = 'P')
    Drop Proc spp_Mtto_CatTipoDocumentos
Go--#SQL
  
Create Proc spp_Mtto_CatTipoDocumentos ( @IdDocumento varchar(2), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdDocumento = '*' 
	   Select @IdDocumento = cast( (max(IdDocumento) + 1) as varchar)  From CatTipoDocumentos (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdDocumento = IsNull(@IdDocumento, '1')
	Set @IdDocumento = right(replicate('0', 2) + @IdDocumento, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatTipoDocumentos (NoLock) Where IdDocumento = @IdDocumento ) 
			  Begin 
				 Insert Into CatTipoDocumentos ( IdDocumento, Descripcion, Status, Actualizado ) 
				 Select @IdDocumento, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatTipoDocumentos Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdDocumento = @IdDocumento  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdDocumento 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatTipoDocumentos Set Status = @sStatus, Actualizado = @iActualizado Where IdDocumento = @IdDocumento 
		   Set @sMensaje = 'La información del Documento ' + @IdDocumento + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdDocumento as Clave, @sMensaje as Mensaje 
End
Go--#SQL
