
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatEdades' and xType = 'P')
    Drop Proc spp_Mtto_CatEdades
Go--#SQL
  
Create Proc spp_Mtto_CatEdades ( @IdEdad varchar(2), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdEdad = '*' 
	   Select @IdEdad = cast( (max(IdEdad) + 1) as varchar)  From CatEdades (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdEdad = IsNull(@IdEdad, '1')
	Set @IdEdad = right(replicate('0', 2) + @IdEdad, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatEdades (NoLock) Where IdEdad = @IdEdad ) 
			  Begin 
				 Insert Into CatEdades ( IdEdad, Descripcion, Status, Actualizado ) 
				 Select @IdEdad, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatEdades Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEdad = @IdEdad  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdEdad 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatEdades Set Status = @sStatus, Actualizado = @iActualizado Where IdEdad = @IdEdad 
		   Set @sMensaje = 'La información de la Edad ' + @IdEdad + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdEdad as Clave, @sMensaje as Mensaje 
End
Go--#SQL
