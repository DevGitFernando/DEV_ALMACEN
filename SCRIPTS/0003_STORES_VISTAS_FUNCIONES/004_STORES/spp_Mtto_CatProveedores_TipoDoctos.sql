


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProveedores_TipoDoctos' And xType = 'P' )
	Drop Proc spp_Mtto_CatProveedores_TipoDoctos
Go--#SQL

		----		Exec spp_Mtto_CatProveedores_TipoDoctos '*', 'DOCUMENTO PRUEBA', 1

Create Procedure spp_Mtto_CatProveedores_TipoDoctos 
( 
	@IdDocumento varchar(2) = '*', @Descripcion varchar(200) = 'DOCUMENTO PRUEBA', @iOpcion int = 1 
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
	
	If @IdDocumento = '*' 
	   Select @IdDocumento = cast( (max(IdDocumento) + 1) as varchar)  From CatProveedores_TipoDoctos (NoLock) 

	-- Asegurar que IdDocumento sea valido y formatear la cadena 
	Set @IdDocumento = IsNull(@IdDocumento, '1')
	Set @IdDocumento = right(replicate('0', 2) + @IdDocumento, 2)

	If @iOpcion = 1
	 Begin
		If Not Exists( Select * From CatProveedores_TipoDoctos (NoLock) Where IdDocumento = @IdDocumento  )
		 Begin
			Insert Into CatProveedores_TipoDoctos ( IdDocumento, Descripcion, Status, Actualizado )
			Select @IdDocumento, @Descripcion, @Status, @Actualizado
		 End
		Else
		 Begin
			Update CatProveedores_TipoDoctos
			Set Descripcion = @Descripcion, Status = @Status, Actualizado = @Actualizado
			Where IdDocumento = @IdDocumento
		 End

		Set @sMensaje = 'La información del Tipo de Documento ' + @IdDocumento + ' se guardo exitosamente'
	 End
	Else
	 Begin
		Set @Status = 'C'
		Update CatProveedores_TipoDoctos Set Status = @Status, Actualizado = @Actualizado Where IdDocumento = @IdDocumento
		Set @sMensaje = 'La información del Tipo de Documento ' + @IdDocumento + ' ha sido cancelada satisfactoriamente.' 
	 End

	-- Regresar la Clave Generada
    Select @IdDocumento as IdDocumento, @sMensaje as Mensaje 

	-----		spp_Mtto_CatProveedores_TipoDoctos

End
Go--#SQL
