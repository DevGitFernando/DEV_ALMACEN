If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatFamilias' and xType = 'P')
    Drop Proc spp_Mtto_CatFamilias
Go--#SQL	

  
Create Proc spp_Mtto_CatFamilias ( @IdFamilia varchar(4), @Descripcion varchar(52), @iOpcion smallint ) 
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


	If @IdFamilia = '*' 
	   Select @IdFamilia = cast( (max(IdFamilia) + 1) as varchar)  From CatFamilias (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdFamilia = IsNull(@IdFamilia, '1')
	Set @IdFamilia = right(replicate('0', 2) + @IdFamilia, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatFamilias (NoLock) Where IdFamilia = @IdFamilia ) 
			  Begin 
				 Insert Into CatFamilias ( IdFamilia, Descripcion, Status, Actualizado ) 
				 Select @IdFamilia, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatFamilias Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdFamilia = @IdFamilia  
              End 

		   -- Se Cambia el Status de las SubFamilias a Activo
		   Update CatSubFamilias Set Status = @sStatus, Actualizado = @iActualizado Where IdFamilia = @IdFamilia

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdFamilia 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
		   -- Se cancela la Familia
	       Update CatFamilias Set Status = @sStatus, Actualizado = @iActualizado Where IdFamilia = @IdFamilia 

		   -- Se cancelan las SubFamilias
		   Update CatSubFamilias Set Status = @sStatus, Actualizado = @iActualizado Where IdFamilia = @IdFamilia 

		   Set @sMensaje = 'La información de la familia ' + @IdFamilia + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdFamilia as Clave, @sMensaje as Mensaje 
End
Go--#SQL
