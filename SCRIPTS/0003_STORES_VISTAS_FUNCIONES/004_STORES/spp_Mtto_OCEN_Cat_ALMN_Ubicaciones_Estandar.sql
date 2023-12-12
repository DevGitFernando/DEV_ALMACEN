


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_OCEN_Cat_ALMN_Ubicaciones_Estandar' And xType = 'P' )
	Drop Proc spp_Mtto_OCEN_Cat_ALMN_Ubicaciones_Estandar
Go--#SQL

		----	Exec spp_Mtto_OCEN_Cat_ALMN_Ubicaciones_Estandar 'DEVVTA', 'POSICION PARA DEVOLUCION DE VENTA', 1

Create Procedure spp_Mtto_OCEN_Cat_ALMN_Ubicaciones_Estandar 
( 
	@NombrePosicion varchar(100) = 'DEVVTA', @Descripcion varchar(500) = 'POSICION PARA DEVOLUCION DE VENTA', @Opcion tinyint = 1
)
With Encryption 
As
Begin
	Declare @Status varchar(1), 
			@Actualizado int,
			@sMensaje varchar(8000)
			
	/*Opciones
		Opcion 1.- Insertar / Actualizar
		Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @Status = 'A'
	Set @Actualizado = 0
	Set @sMensaje = ''
	
	If @Opcion = 1
		Begin	
			If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = @NombrePosicion  )
				Begin
					Insert Into Cat_ALMN_Ubicaciones_Estandar ( NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )
					Select @NombrePosicion, @Descripcion, GetDate(), @Status, @Actualizado
					
					Set @sMensaje = 'La información de la Ubicación ' + @NombrePosicion + ' se guardo exitosamente'
				End
			Else
				Begin
					Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = @Descripcion, FechaRegistro = GetDate(), 
					Status = @Status, Actualizado = @Actualizado
					Where NombrePosicion = @NombrePosicion
					
					Set @sMensaje = 'La información de la Ubicación ' + @NombrePosicion + ' se actualizo exitosamente'
				End
		End
	Else
		Begin
			Set @Status = 'C'
			
			Update Cat_ALMN_Ubicaciones_Estandar Set Status = @Status
			Where NombrePosicion = @NombrePosicion
			
			Set @sMensaje = 'La información de la Ubicación ' + @NombrePosicion + ' se cancelo exitosamente'
		End	
	 
	-- Regresar la Clave Generada
    Select @NombrePosicion as Id, @sMensaje as Mensaje 

End
Go--#SQL
