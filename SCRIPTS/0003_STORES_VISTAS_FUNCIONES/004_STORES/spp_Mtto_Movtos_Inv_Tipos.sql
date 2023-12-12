If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Movtos_Inv_Tipos' And xType = 'P' )
	Drop Proc spp_Mtto_Movtos_Inv_Tipos
Go--#SQL

Create Procedure spp_Mtto_Movtos_Inv_Tipos ( @IdTipoMovto_Inv varchar(3), @Descripcion varchar(50), @Efecto_Movto varchar(1), @EsMovtoGral tinyint, @IdTipoMovto_Inv_ContraMovto varchar(3), @Efecto_ContraMovto varchar(1), @PermiteCaducados int, @iOpcion int )
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

	If @iOpcion = 1
	 Begin
		If Not Exists( Select IdTipoMovto_Inv From Movtos_Inv_Tipos(NoLock) Where IdTipoMovto_Inv = @IdTipoMovto_Inv )
		 Begin
			Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, PermiteCaducados, Status, Actualizado )
			Select @IdTipoMovto_Inv, @Descripcion, @Efecto_Movto, @EsMovtoGral, @IdTipoMovto_Inv_ContraMovto, @Efecto_ContraMovto, @PermiteCaducados, @Status, @Actualizado
		 End
		Else
		 Begin
			Update Movtos_Inv_Tipos
			Set EsMovtoGral = @EsMovtoGral, PermiteCaducados = @PermiteCaducados, Status = @Status, Actualizado = @Actualizado
			Where IdTipoMovto_Inv = @IdTipoMovto_Inv
		 End

		Set @sMensaje = 'La información de la Clave ' + @IdTipoMovto_Inv + ' se guardo exitosamente'
	 End
	Else
	 Begin
		Set @Status = 'C'
		Update Movtos_Inv_Tipos Set Status = @Status, Actualizado = @Actualizado Where IdTipoMovto_Inv = @IdTipoMovto_Inv
		Set @sMensaje = 'La información de la Clave ' + @IdTipoMovto_Inv + ' ha sido cancelada satisfactoriamente.' 
	 End

	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 

End
Go--#SQL


