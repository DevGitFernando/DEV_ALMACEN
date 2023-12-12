


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_Cat_Rechazos' And xType = 'P' )
	Drop Proc spp_Mtto_COM_Cat_Rechazos
Go--#SQL

		----		Exec spp_Mtto_COM_Cat_Rechazos '*', 'SIN ESPECIFICAR', 1

Create Procedure spp_Mtto_COM_Cat_Rechazos 
(  
	@IdRechazo varchar(6) = '*', @Descripcion varchar(200) = 'SIN ESPECIFICAR', @iOpcion int = 1 
)
With Encryption 
As
Begin
	Declare @Status varchar(1), 
			@Actualizado int,
			@sMensaje varchar(8000)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @Status = 'A'
	Set @Actualizado = 0
	Set @sMensaje = ''
	
	If @IdRechazo = '*' 
	   Select @IdRechazo = cast( (max(IdRechazo) + 1) as varchar)  From COM_Cat_Rechazos (NoLock) 

	-- Asegurar que @IdRechazo sea valido y formatear la cadena 
	Set @IdRechazo = IsNull(@IdRechazo, '1')
	Set @IdRechazo = right(replicate('0', 3) + @IdRechazo, 3)
	
	
	If @iOpcion = 1
	 Begin
		If Not Exists( Select * From COM_Cat_Rechazos (NoLock) Where IdRechazo = @IdRechazo )
		 Begin
			Insert Into COM_Cat_Rechazos ( IdRechazo, Descripcion, Status, Actualizado )
			Select @IdRechazo, @Descripcion, @Status, @Actualizado
		 End
		Else
		 Begin
			Update COM_Cat_Rechazos
			Set Descripcion = @Descripcion, Status = @Status, Actualizado = @Actualizado
			Where IdRechazo = @IdRechazo 
		 End

		Set @sMensaje = 'La información del Rechazo ' + @IdRechazo + ' se guardo exitosamente'
	 End
	Else
	 Begin
		Set @Status = 'C'
		Update COM_Cat_Rechazos Set Status = @Status, Actualizado = @Actualizado 
		Where IdRechazo = @IdRechazo 
		Set @sMensaje = 'La información del Rechazo ' + @IdRechazo + ' ha sido cancelada satisfactoriamente.' 
	 End

	-- Regresar la Clave Generada
    Select @IdRechazo as IdRechazo, @sMensaje as Mensaje 

End
Go--#SQL


