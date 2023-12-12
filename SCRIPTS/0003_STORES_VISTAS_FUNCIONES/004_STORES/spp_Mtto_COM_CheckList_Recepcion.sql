


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_CheckList_Recepcion' And xType = 'P' )
	Drop Proc spp_Mtto_COM_CheckList_Recepcion
Go--#SQL

		----		Exec spp_Mtto_COM_CheckList_Recepcion '*', '*', 'DOCUMENTOS', 'O.C. IMPRESA Y COLOCADA', 0, 0, 0, 1

Create Procedure spp_Mtto_COM_CheckList_Recepcion 
(  
	@IdGrupo varchar(3) = '001', @IdMotivo varchar(3) = '*', @DescripcionMotivo varchar(200) = 'O.C. IMPRESA Y COLOCADA', 
	@Respuesta_SI tinyint = 0, @Respuesta_SI_RequiereFirma tinyint = 0, @Respuesta_NO tinyint = 0, @Respuesta_NO_RequiereFirma tinyint = 0,
	@Respuesta_Rechazo tinyint = 0,@Respuesta_Rechazo_RequiereFirma tinyint = 0, @iOpcion int = 1 
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
	
		
	--------------------------------------------------------------------------------------------------------------
	If @IdMotivo = '*' 
	   Select @IdMotivo = cast( (max(IdMotivo) + 1) as varchar)  From COM_CheckList_Recepcion (NoLock) Where IdGrupo = @IdGrupo

	-- Asegurar que IdMotivo sea valido y formatear la cadena 
	Set @IdMotivo = IsNull(@IdMotivo, '1')
	Set @IdMotivo = right(replicate('0', 3) + @IdMotivo, 3)
	------------------------------------------------------------------------------------------------------------------

	If @iOpcion = 1
	 Begin
		If Not Exists( Select * From COM_CheckList_Recepcion (NoLock) Where IdGrupo = @IdGrupo and IdMotivo = @IdMotivo )
		 Begin
			Insert Into COM_CheckList_Recepcion ( IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, 
			Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )
			Select @IdGrupo, @IdMotivo, @DescripcionMotivo, @Respuesta_SI, @Respuesta_SI_RequiereFirma, @Respuesta_NO, 
			@Respuesta_NO_RequiereFirma, @Respuesta_Rechazo, @Respuesta_Rechazo_RequiereFirma, @Status, @Actualizado
		 End
		Else
		 Begin
			Update COM_CheckList_Recepcion
			Set DescripcionMotivo = @DescripcionMotivo, 
			Respuesta_SI = @Respuesta_SI, Respuesta_SI_RequiereFirma = @Respuesta_SI_RequiereFirma, 
			Respuesta_NO = @Respuesta_NO, Respuesta_NO_RequiereFirma = @Respuesta_NO_RequiereFirma, 
			Respuesta_Rechazo = @Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma = @Respuesta_Rechazo_RequiereFirma, 
			Status = @Status, Actualizado = @Actualizado
			Where IdGrupo = @IdGrupo and IdMotivo = @IdMotivo
		 End

		Set @sMensaje = 'La información del motivo ' + @IdMotivo + ' se guardo exitosamente'
	 End
	Else
	 Begin
		Set @Status = 'C'
		Update COM_CheckList_Recepcion Set Status = @Status, Actualizado = @Actualizado 
		Where IdGrupo = @IdGrupo and IdMotivo = @IdMotivo
		Set @sMensaje = 'La información del motivo ' + @IdMotivo + ' ha sido cancelada satisfactoriamente.' 
	 End

	-- Regresar la Clave Generada
    Select @IdMotivo as IdMotivo, @sMensaje as Mensaje 

End
Go--#SQL


