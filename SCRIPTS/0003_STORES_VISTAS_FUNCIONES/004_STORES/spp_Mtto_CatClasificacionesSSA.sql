If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatClasificacionesSSA' and xType = 'P')
    Drop Proc spp_Mtto_CatClasificacionesSSA
Go--#SQL
  
Create Proc spp_Mtto_CatClasificacionesSSA ( @IdClasificacion varchar(6), @Descripcion varchar(152), @iOpcion smallint )
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


	If @IdClasificacion = '*' 
	   Select @IdClasificacion = cast( (max(IdClasificacion) + 1) as varchar)  From CatClasificacionesSSA (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdClasificacion = IsNull(@IdClasificacion, '1')
	Set @IdClasificacion = right(replicate('0', 4) + @IdClasificacion, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatClasificacionesSSA (NoLock) Where IdClasificacion = @IdClasificacion ) 
			  Begin 
				 Insert Into CatClasificacionesSSA ( IdClasificacion, Descripcion, Status, Actualizado ) 
				 Select @IdClasificacion, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatClasificacionesSSA Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdClasificacion = @IdClasificacion  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdClasificacion 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatClasificacionesSSA Set Status = @sStatus, Actualizado = @iActualizado Where IdClasificacion = @IdClasificacion 
		   Set @sMensaje = 'La información de la Clasificacion ' + @IdClasificacion + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdClasificacion as Clave, @sMensaje as Mensaje 
End
Go--#SQL