
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatEscolaridades' and xType = 'P')
    Drop Proc spp_Mtto_CatEscolaridades
Go--#SQL
  
Create Proc spp_Mtto_CatEscolaridades ( @IdEscolaridad varchar(2), @Descripcion varchar(100), @iOpcion smallint )
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


	If @IdEscolaridad = '*' 
	   Select @IdEscolaridad = cast( (max(IdEscolaridad) + 1) as varchar)  From CatEscolaridades (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdEscolaridad = IsNull(@IdEscolaridad, '1')
	Set @IdEscolaridad = right(replicate('0', 2) + @IdEscolaridad, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatEscolaridades (NoLock) Where IdEscolaridad = @IdEscolaridad ) 
			  Begin 
				 Insert Into CatEscolaridades ( IdEscolaridad, Descripcion, Status, Actualizado ) 
				 Select @IdEscolaridad, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatEscolaridades Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEscolaridad = @IdEscolaridad  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdEscolaridad 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatEscolaridades Set Status = @sStatus, Actualizado = @iActualizado Where IdEscolaridad = @IdEscolaridad 
		   Set @sMensaje = 'La información de la Escolaridad ' + @IdEscolaridad + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdEscolaridad as Clave, @sMensaje as Mensaje 
End
Go--#SQL
