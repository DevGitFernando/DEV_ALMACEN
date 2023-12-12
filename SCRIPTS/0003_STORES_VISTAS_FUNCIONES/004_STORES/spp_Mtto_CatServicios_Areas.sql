If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatServicios_Areas' and xType = 'P')
    Drop Proc spp_Mtto_CatServicios_Areas
Go--#SQL
  
Create Proc spp_Mtto_CatServicios_Areas ( @IdServicio varchar(3), @IdArea varchar(3), @Descripcion varchar(52), @iOpcion smallint )
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


	If @IdArea = '*' 
	  Begin
	    Select @IdArea = cast( (max(IdArea) + 1) as varchar) From CatServicios_Areas (NoLock)
		Where IdServicio = @IdServicio
	  End

	-- Asegurar que IdArea sea valido y formatear la cadena 
	Set @IdArea = IsNull(@IdArea, '1')
	Set @IdArea = right(replicate('0', 3) + @IdArea, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatServicios_Areas (NoLock) Where IdServicio = @IdServicio And IdArea = @IdArea ) 
			  Begin 
				 Insert Into CatServicios_Areas ( IdServicio, IdArea, Descripcion, Status, Actualizado ) 
				 Select @IdServicio, @IdArea, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatServicios_Areas Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdServicio = @IdServicio And IdArea = @IdArea
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdArea 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatServicios_Areas Set Status = @sStatus, Actualizado = @iActualizado Where IdServicio = @IdServicio And IdArea = @IdArea
		   Set @sMensaje = 'La información del Area ' + @IdArea + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdServicio as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
