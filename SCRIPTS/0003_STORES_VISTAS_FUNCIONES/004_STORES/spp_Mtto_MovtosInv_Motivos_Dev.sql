


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_MovtosInv_Motivos_Dev' And xType = 'P' )
	Drop Proc spp_Mtto_MovtosInv_Motivos_Dev
Go--#SQL

		----		Exec spp_Mtto_MovtosInv_Motivos_Dev 'CC', '*', 'TEST DE MOTIVO DE DEVOLUCION', 1

Create Procedure spp_Mtto_MovtosInv_Motivos_Dev 
( 
	@IdTipoMovto_Inv varchar(6) = 'ED', @IdMotivo varchar(3) = '001', @Descripcion varchar(200) = 'TEST DE MOTIVO DE DEVOLUCION', @iOpcion int = 1 
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
	
	If @IdMotivo = '*' 
	   Select @IdMotivo = cast( (max(IdMotivo) + 1) as varchar)  From MovtosInv_Motivos_Dev (NoLock) Where IdTipoMovto_Inv = @IdTipoMovto_Inv 

	-- Asegurar que IdMotivo sea valido y formatear la cadena 
	Set @IdMotivo = IsNull(@IdMotivo, '1')
	Set @IdMotivo = right(replicate('0', 3) + @IdMotivo, 3)

	If @iOpcion = 1
	 Begin
		If Not Exists( Select * From MovtosInv_Motivos_Dev(NoLock) Where IdTipoMovto_Inv = @IdTipoMovto_Inv and IdMotivo = @IdMotivo )
		 Begin
			Insert Into MovtosInv_Motivos_Dev ( IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )
			Select @IdTipoMovto_Inv, @IdMotivo, @Descripcion, @Status, @Actualizado
		 End
		Else
		 Begin
			Update MovtosInv_Motivos_Dev
			Set Descripcion = @Descripcion, Status = @Status, Actualizado = @Actualizado
			Where IdTipoMovto_Inv = @IdTipoMovto_Inv and IdMotivo = @IdMotivo
		 End

		Set @sMensaje = 'La información del motivo ' + @IdMotivo + ' se guardo exitosamente'
	 End
	Else
	 Begin
		Set @Status = 'C'
		Update MovtosInv_Motivos_Dev Set Status = @Status, Actualizado = @Actualizado Where IdTipoMovto_Inv = @IdTipoMovto_Inv and IdMotivo = @IdMotivo
		Set @sMensaje = 'La información del motivo ' + @IdMotivo + ' ha sido cancelada satisfactoriamente.' 
	 End

	-- Regresar la Clave Generada
    Select @IdMotivo as IdMotivo, @sMensaje as Mensaje 

End
Go--#SQL


