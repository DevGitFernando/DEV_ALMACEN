If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatRegiones' and xType = 'P')
    Drop Proc spp_Mtto_CatRegiones
Go--#SQL
  
Create Proc spp_Mtto_CatRegiones ( @IdRegion varchar(6), @Descripcion varchar(52), @iOpcion smallint )
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


	If @IdRegion = '*' 
	   Select @IdRegion = cast( (max(IdRegion) + 1) as varchar)  From CatRegiones (NoLock) 

	-- Asegurar que IdRegion sea valido y formatear la cadena 
	Set @IdRegion = IsNull(@IdRegion, '1')
	Set @IdRegion = right(replicate('0', 2) + @IdRegion, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatRegiones (NoLock) Where IdRegion = @IdRegion ) 
			  Begin 
				 Insert Into CatRegiones ( IdRegion, Descripcion, Status, Actualizado ) 
				 Select @IdRegion, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatRegiones Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdRegion = @IdRegion  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la Region ' + @IdRegion 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatRegiones Set Status = @sStatus, Actualizado = @iActualizado Where IdRegion = @IdRegion
		   Update CatSubRegiones Set Status = @sStatus, Actualizado = @iActualizado Where IdRegion = @IdRegion 
		   Set @sMensaje = 'La información de la Region ' + @IdRegion + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Region Generada
    Select @IdRegion as Region, @sMensaje as Mensaje 
End
Go--#SQL