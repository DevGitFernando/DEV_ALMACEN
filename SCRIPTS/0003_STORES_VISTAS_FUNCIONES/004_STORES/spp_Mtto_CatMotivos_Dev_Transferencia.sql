

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatMotivos_Dev_Transferencia' and xType = 'P')
    Drop Proc spp_Mtto_CatMotivos_Dev_Transferencia
Go--#SQL
  
Create Proc spp_Mtto_CatMotivos_Dev_Transferencia ( @IdMotivo varchar(4), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdMotivo = '*' 
	   Select @IdMotivo = cast( (max(IdMotivo) + 1) as varchar)  From CatMotivos_Dev_Transferencia (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdMotivo = IsNull(@IdMotivo, '1')
	Set @IdMotivo = right(replicate('0', 2) + @IdMotivo, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatMotivos_Dev_Transferencia (NoLock) Where IdMotivo = @IdMotivo ) 
			  Begin 
				 Insert Into CatMotivos_Dev_Transferencia ( IdMotivo, Descripcion, Status, Actualizado ) 
				 Select @IdMotivo, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatMotivos_Dev_Transferencia Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdMotivo = @IdMotivo  
              End 

			Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdMotivo 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C'
		   -- Se cancela el Servicio
	       Update CatMotivos_Dev_Transferencia Set Status = @sStatus, Actualizado = @iActualizado Where IdMotivo = @IdMotivo 
		   Set @sMensaje = 'La información del Motivo ' + @IdMotivo + ' ha sido cancelada satisfactoriamente.' 
 
	   End 

	-- Regresar la Clave Generada
    Select @IdMotivo as Clave, @sMensaje as Mensaje 
End
Go--#SQL