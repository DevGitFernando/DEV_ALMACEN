If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatSubRegiones' and xType = 'P')
    Drop Proc spp_Mtto_CatSubRegiones
Go--#SQL
  
Create Proc spp_Mtto_CatSubRegiones ( @IdRegion varchar(2), @IdSubRegion varchar(2), @Descripcion varchar(52), @iOpcion smallint )
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


	If @IdSubRegion = '*' 
	  Begin
	    Select @IdSubRegion = cast( (max(IdSubRegion) + 1) as varchar) From CatSubRegiones (NoLock)
		Where IdRegion = @IdRegion --And IdSubRegion = @IdSubRegion
	  End

	-- Asegurar que IdSubRegion sea valido y formatear la cadena 
	Set @IdSubRegion = IsNull(@IdSubRegion, '1')
	Set @IdSubRegion = right(replicate('0', 2) + @IdSubRegion, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatSubRegiones (NoLock) Where IdRegion = @IdRegion And IdSubRegion = @IdSubRegion ) 
			  Begin 
				 Insert Into CatSubRegiones ( IdRegion, IdSubRegion, Descripcion, Status, Actualizado ) 
				 Select @IdRegion, @IdSubRegion, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatSubRegiones Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdRegion = @IdRegion And IdSubRegion = @IdSubRegion
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la SubRegion ' + @IdSubRegion 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatSubRegiones Set Status = @sStatus, Actualizado = @iActualizado Where IdRegion = @IdRegion And IdSubRegion = @IdSubRegion
		   Set @sMensaje = 'La información de la SubRegion ' + @IdSubRegion + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdSubRegion as SubRegion, @sMensaje as Mensaje 
End
Go--#SQL	
