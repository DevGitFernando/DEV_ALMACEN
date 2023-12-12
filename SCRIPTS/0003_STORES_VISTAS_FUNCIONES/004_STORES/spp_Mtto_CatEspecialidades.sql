If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatEspecialidades' and xType = 'P')
    Drop Proc spp_Mtto_CatEspecialidades
Go--#SQL 

Create Proc spp_Mtto_CatEspecialidades ( @IdEspecialidad varchar(4), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdEspecialidad = '*' 
	   Select @IdEspecialidad = cast( (max(IdEspecialidad) + 1) as varchar)  From CatEspecialidades (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdEspecialidad = IsNull(@IdEspecialidad, '1')
	Set @IdEspecialidad = right(replicate('0', 4) + @IdEspecialidad, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatEspecialidades (NoLock) Where IdEspecialidad = @IdEspecialidad ) 
			  Begin 
				 Insert Into CatEspecialidades ( IdEspecialidad, Descripcion, Status, Actualizado ) 
				 Select @IdEspecialidad, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatEspecialidades Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEspecialidad = @IdEspecialidad  
              End 

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdEspecialidad 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C'
		   -- Se cancela el Servicio
	       Update CatEspecialidades Set Status = @sStatus, Actualizado = @iActualizado Where IdEspecialidad = @IdEspecialidad 
		   Set @sMensaje = 'La información del Servicio ' + @IdEspecialidad + ' ha sido cancelada satisfactoriamente.' 

	   End 

	-- Regresar la Clave Generada
    Select @IdEspecialidad as Clave, @sMensaje as Mensaje 
End
Go--#SQL