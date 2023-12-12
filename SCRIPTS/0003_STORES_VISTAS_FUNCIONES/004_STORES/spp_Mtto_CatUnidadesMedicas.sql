
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatUnidadesMedicas' and xType = 'P')
    Drop Proc spp_Mtto_CatUnidadesMedicas
Go--#SQL
  
Create Proc spp_Mtto_CatUnidadesMedicas ( @IdUMedica varchar(6), @IdEstado varchar(2), @IdJurisdiccion varchar(3), @CLUES varchar(30), 
	@NombreUnidadMedica varchar(200), @iOpcion smallint )
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


	If @IdUMedica = '*' 
	   Select @IdUMedica = cast( (max(IdUMedica) + 1) as varchar)  From CatUnidadesMedicas (NoLock) 

	-- Asegurar que IdUMedica sea valido y formatear la cadena 
	Set @IdUMedica = IsNull(@IdUMedica, '1')
	Set @IdUMedica = right(replicate('0', 6) + @IdUMedica, 6)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatUnidadesMedicas (NoLock) Where IdUMedica = @IdUMedica ) 
			  Begin 
				 Insert Into CatUnidadesMedicas ( IdUMedica, IdEstado, IdJurisdiccion, CLUES, NombreUnidadMedica, Status, Actualizado ) 
				 Select @IdUMedica, @IdEstado, @IdJurisdiccion, @CLUES, @NombreUnidadMedica, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatUnidadesMedicas Set 
						IdEstado = @IdEstado, IdJurisdiccion = @IdJurisdiccion, CLUES = @CLUES, 
						NombreUnidadMedica = @NombreUnidadMedica, Status = @sStatus, Actualizado = @iActualizado
				 Where IdUMedica = @IdUMedica  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdUMedica 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatUnidadesMedicas Set Status = @sStatus, Actualizado = @iActualizado Where IdUMedica = @IdUMedica 
		   Set @sMensaje = 'La información de la Unidad Medica ' + @IdUMedica + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdUMedica as Clave, @sMensaje as Mensaje 
End
Go--#SQL
