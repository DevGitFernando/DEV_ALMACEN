If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatCompaniasTiempoAire' and xType = 'P')
    Drop Proc spp_Mtto_CatCompaniasTiempoAire
Go--#SQL
  
Create Proc spp_Mtto_CatCompaniasTiempoAire ( @IdCompania varchar(4), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdCompania = '*' 
	   Select @IdCompania = cast( (max(IdCompania) + 1) as varchar)  From CatCompaniasTiempoAire (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdCompania = IsNull(@IdCompania, '1')
	Set @IdCompania = right(replicate('0', 2) + @IdCompania, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatCompaniasTiempoAire (NoLock) Where IdCompania = @IdCompania ) 
			  Begin 
				 Insert Into CatCompaniasTiempoAire ( IdCompania, Descripcion, Status, Actualizado ) 
				 Select @IdCompania, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatCompaniasTiempoAire Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdCompania = @IdCompania  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCompania 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatCompaniasTiempoAire Set Status = @sStatus, Actualizado = @iActualizado Where IdCompania = @IdCompania 
		   Set @sMensaje = 'La información del Compañia ' + @IdCompania + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCompania as Clave, @sMensaje as Mensaje 
End
Go--#SQL
