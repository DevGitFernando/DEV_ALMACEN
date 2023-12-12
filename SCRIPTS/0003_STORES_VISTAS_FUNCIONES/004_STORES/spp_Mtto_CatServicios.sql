If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatServicios' and xType = 'P')
    Drop Proc spp_Mtto_CatServicios
Go--#SQL
  
Create Proc spp_Mtto_CatServicios ( @IdServicio varchar(4), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdServicio = '*' 
	   Select @IdServicio = cast( (max(IdServicio) + 1) as varchar)  From CatServicios (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdServicio = IsNull(@IdServicio, '1')
	Set @IdServicio = right(replicate('0', 3) + @IdServicio, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatServicios (NoLock) Where IdServicio = @IdServicio ) 
			  Begin 
				 Insert Into CatServicios ( IdServicio, Descripcion, Status, Actualizado ) 
				 Select @IdServicio, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatServicios Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdServicio = @IdServicio  
              End 

			-- Se Cambia el Status del CatServicios_Areas a Activo
		   Update CatServicios_Areas Set Status = @sStatus, Actualizado = @iActualizado Where IdServicio = @IdServicio

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdServicio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C'
		   -- Se cancela el Servicio
	       Update CatServicios Set Status = @sStatus, Actualizado = @iActualizado Where IdServicio = @IdServicio 
		   Set @sMensaje = 'La información del Servicio ' + @IdServicio + ' ha sido cancelada satisfactoriamente.' 

		   -- Se cancelan las SubFamilias
		   Update CatServicios_Areas Set Status = @sStatus, Actualizado = @iActualizado Where IdServicio = @IdServicio 
	   End 

	-- Regresar la Clave Generada
    Select @IdServicio as Clave, @sMensaje as Mensaje 
End
Go--#SQL