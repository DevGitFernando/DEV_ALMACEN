


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_Cat_Grupos_Recepcion' And xType = 'P' )
	Drop Proc spp_Mtto_COM_Cat_Grupos_Recepcion
Go--#SQL

		----		Exec spp_Mtto_COM_Cat_Grupos_Recepcion '*', 'DOCUMENTOS', 1

Create Procedure spp_Mtto_COM_Cat_Grupos_Recepcion 
(  
	@IdGrupo varchar(6) = '*', @DescripcionGrupo varchar(200) = 'DOCUMENTOS', @iOpcion int = 1 
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
	
	If @IdGrupo = '*' 
	   Select @IdGrupo = cast( (max(IdGrupo) + 1) as varchar)  From COM_Cat_Grupos_Recepcion (NoLock) 

	-- Asegurar que @IdGrupo sea valido y formatear la cadena 
	Set @IdGrupo = IsNull(@IdGrupo, '1')
	Set @IdGrupo = right(replicate('0', 3) + @IdGrupo, 3)
	
	
	If @iOpcion = 1
	 Begin
		If Not Exists( Select * From COM_Cat_Grupos_Recepcion (NoLock) Where IdGrupo = @IdGrupo )
		 Begin
			Insert Into COM_Cat_Grupos_Recepcion ( IdGrupo, DescripcionGrupo, Status, Actualizado )
			Select @IdGrupo, @DescripcionGrupo, @Status, @Actualizado
		 End
		Else
		 Begin
			Update COM_Cat_Grupos_Recepcion
			Set DescripcionGrupo = @DescripcionGrupo, Status = @Status, Actualizado = @Actualizado
			Where IdGrupo = @IdGrupo 
		 End

		Set @sMensaje = 'La información del Grupo ' + @IdGrupo + ' se guardo exitosamente'
	 End
	Else
	 Begin
		Set @Status = 'C'
		Update COM_Cat_Grupos_Recepcion Set Status = @Status, Actualizado = @Actualizado 
		Where IdGrupo = @IdGrupo 
		Set @sMensaje = 'La información del Grupo ' + @IdGrupo + ' ha sido cancelada satisfactoriamente.' 
	 End

	-- Regresar la Clave Generada
    Select @IdGrupo as IdGrupo, @sMensaje as Mensaje 

End
Go--#SQL


