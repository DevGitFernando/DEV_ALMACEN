
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatTipoContrato' and xType = 'P')
    Drop Proc spp_Mtto_CatTipoContrato
Go--#SQL
  
Create Proc spp_Mtto_CatTipoContrato ( @IdTipoContrato varchar(2), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdTipoContrato = '*' 
	   Select @IdTipoContrato = cast( (max(IdTipoContrato) + 1) as varchar)  From CatTipoContrato (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdTipoContrato = IsNull(@IdTipoContrato, '1')
	Set @IdTipoContrato = right(replicate('0', 2) + @IdTipoContrato, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatTipoContrato (NoLock) Where IdTipoContrato = @IdTipoContrato ) 
			  Begin 
				 Insert Into CatTipoContrato ( IdTipoContrato, Descripcion, Status, Actualizado ) 
				 Select @IdTipoContrato, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatTipoContrato Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdTipoContrato = @IdTipoContrato  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdTipoContrato 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatTipoContrato Set Status = @sStatus, Actualizado = @iActualizado Where IdTipoContrato = @IdTipoContrato 
		   Set @sMensaje = 'La información del TipoContrato ' + @IdTipoContrato + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdTipoContrato as Clave, @sMensaje as Mensaje 
End
Go--#SQL
