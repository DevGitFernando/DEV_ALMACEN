
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPresentaciones' and xType = 'P')
    Drop Proc spp_Mtto_CatPresentaciones
Go--#SQL
  
Create Proc spp_Mtto_CatPresentaciones ( @IdPresentacion varchar(5), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdPresentacion = '*' 
	   Select @IdPresentacion = cast( (max(IdPresentacion) + 1) as varchar)  From CatPresentaciones (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdPresentacion = IsNull(@IdPresentacion, '1')
	Set @IdPresentacion = right(replicate('0', 3) + @IdPresentacion, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPresentaciones (NoLock) Where IdPresentacion = @IdPresentacion ) 
			  Begin 
				 Insert Into CatPresentaciones ( IdPresentacion, Descripcion, Status, Actualizado ) 
				 Select @IdPresentacion, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatPresentaciones Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdPresentacion = @IdPresentacion  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdPresentacion 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPresentaciones Set Status = @sStatus, Actualizado = @iActualizado Where IdPresentacion = @IdPresentacion 
		   Set @sMensaje = 'La información de la Presentacion ' + @IdPresentacion + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdPresentacion as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
