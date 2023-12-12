

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFG_ALMN_Ubicaciones_Estandar_Historico' And xType = 'P' )
	Drop Proc spp_Mtto_CFG_ALMN_Ubicaciones_Estandar_Historico
Go--#SQL

		----	Exec spp_Mtto_CFG_ALMN_Ubicaciones_Estandar_Historico '001', '21', '2182', 'DEVVTA'

Create Procedure spp_Mtto_CFG_ALMN_Ubicaciones_Estandar_Historico 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', @NombrePosicion varchar(100) = 'DEVVTA',
	@IdPersonal varchar(4) = '0001'
)
With Encryption 
As
Begin
	Declare @Status smallint, 
			@Actualizado int,
			@sMensaje varchar(8000)
			
	Insert Into CFG_ALMN_Ubicaciones_Estandar_Historico 
	( 
		IdEmpresa, IdEstado, IdFarmacia, NombrePosicion, FechaRegistro, IdRack, IdNivel, IdEntrepaño, IdPersonal, Status, Actualizado 
	)
	Select IdEmpresa, IdEstado, IdFarmacia, NombrePosicion, GetDate(), IdRack, IdNivel, IdEntrepaño, @IdPersonal as IdPersonal, Status, Actualizado
	From CFG_ALMN_Ubicaciones_Estandar 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombrePosicion = @NombrePosicion
			

End
Go--#SQL




---------------------------------------------------------------------------------------------------------------------------------------------------

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFG_ALMN_Ubicaciones_Estandar' And xType = 'P' )
	Drop Proc spp_Mtto_CFG_ALMN_Ubicaciones_Estandar
Go--#SQL

		----	Exec spp_Mtto_CFG_ALMN_Ubicaciones_Estandar '001', '21', '2182', 'DEVVTA', '0001', 0, 0, 0, 1

Create Procedure spp_Mtto_CFG_ALMN_Ubicaciones_Estandar 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', @NombrePosicion varchar(100) = 'DEVVTA', 
 	@IdPersonal varchar(4) = '0001', @IdRack int = 0, @IdNivel int = 0, @IdEntrepaño int = 0, @Opcion tinyint = 1
)
With Encryption 
As
Begin
	Declare @Status smallint, 
			@Actualizado int,
			@sMensaje varchar(8000)
			
	/*Opciones
		Opcion 1.- Insertar / Actualizar
		Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @Status = 0
	Set @Actualizado = 0
	Set @sMensaje = ''
	
	If @Opcion = 1
		Begin	
		
			Set @Status = 1
			
			If Not Exists ( Select * From CFG_ALMN_Ubicaciones_Estandar Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
						    and IdFarmacia = @IdFarmacia and NombrePosicion = @NombrePosicion  )
				Begin
					Insert Into CFG_ALMN_Ubicaciones_Estandar 
					( 
						IdEmpresa, IdEstado, IdFarmacia, NombrePosicion, IdRack, IdNivel, IdEntrepaño, IdPersonal, FechaRegistro, Status, Actualizado 
					)
					Select @IdEmpresa, @IdEstado, @IdFarmacia, @NombrePosicion, @IdRack, @IdNivel, @IdEntrepaño, @IdPersonal, GetDate(), @Status, @Actualizado
					
					Set @sMensaje = 'La información de la Ubicación ' + @NombrePosicion + ' se guardo exitosamente'
				End
			Else
				Begin
					Update CFG_ALMN_Ubicaciones_Estandar Set IdRack = @IdRack, IdNivel = @IdNivel, IdEntrepaño = @IdEntrepaño,
					Status = @Status, Actualizado = @Actualizado
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombrePosicion = @NombrePosicion
					
					Set @sMensaje = 'La información de la Ubicación ' + @NombrePosicion + ' se actualizo exitosamente'
				End
		End
	Else
		Begin
			Set @Status = 0
			
			Update CFG_ALMN_Ubicaciones_Estandar Set Status = @Status
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombrePosicion = @NombrePosicion
			
			Set @sMensaje = 'La información de la Ubicación ' + @NombrePosicion + ' se cancelo exitosamente'
		End	
	 
	 
	 Exec spp_Mtto_CFG_ALMN_Ubicaciones_Estandar_Historico @IdEmpresa, @IdEstado, @IdFarmacia, @NombrePosicion, @IdPersonal
	 
	-- Regresar la Clave Generada
    Select @NombrePosicion as Id, @sMensaje as Mensaje 

End
Go--#SQL
