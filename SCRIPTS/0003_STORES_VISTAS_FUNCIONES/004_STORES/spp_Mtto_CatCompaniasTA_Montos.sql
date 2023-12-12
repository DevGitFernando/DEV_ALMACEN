If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatCompaniasTA_Montos' and xType = 'P')
    Drop Proc spp_Mtto_CatCompaniasTA_Montos
Go--#SQL
  
Create Proc spp_Mtto_CatCompaniasTA_Montos ( @IdCompania varchar(4), @IdMonto varchar(4), @Descripcion varchar(50), @Monto int, @iOpcion smallint )
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


	If @IdMonto = '*' 
	  Begin
	    Select @IdMonto = cast( (max(IdMonto) + 1) as varchar) From CatCompaniasTA_Montos (NoLock)
		Where IdCompania = @IdCompania
	  End

	-- Asegurar que IdMonto sea valido y formatear la cadena 
	Set @IdMonto = IsNull(@IdMonto, '1')
	Set @IdMonto = right(replicate('0', 2) + @IdMonto, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatCompaniasTA_Montos (NoLock) Where IdCompania = @IdCompania And IdMonto = @IdMonto ) 
			  Begin 
				 Insert Into CatCompaniasTA_Montos ( IdCompania, IdMonto, Descripcion, Monto, Status, Actualizado ) 
				 Select @IdCompania, @IdMonto, @Descripcion, @Monto, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatCompaniasTA_Montos Set Descripcion = @Descripcion, Monto = @Monto, Status = @sStatus, Actualizado = @iActualizado
				 Where IdCompania = @IdCompania And IdMonto = @IdMonto
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdMonto 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatCompaniasTA_Montos Set Status = @sStatus, Actualizado = @iActualizado Where IdCompania = @IdCompania And IdMonto = @IdMonto
		   Set @sMensaje = 'La información del Monto ' + @IdMonto + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdMonto as Clave, @sMensaje as Mensaje 
End
Go--#SQL
