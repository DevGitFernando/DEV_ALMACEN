If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN_DIST_CB_NivelesAtencion' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN_DIST_CB_NivelesAtencion
Go--#SQL

Create Proc spp_Mtto_CFGC_ALMN_DIST_CB_NivelesAtencion ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
					@IdPerfilAtencion varchar(10) = '1', @Descripcion varchar(100) = 'Oportunidades', @iOpcion smallint )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,
		@iIdPerfilAtencion int  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	Set @iIdPerfilAtencion = (Select Cast(@IdPerfilAtencion as int))

	If @IdPerfilAtencion = '*' 
		Select @iIdPerfilAtencion = cast( (max(IdPerfilAtencion) + 1) as int)  From CFGC_ALMN_DIST_CB_NivelesAtencion (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia
	

	If @iOpcion = 1 
       Begin
			IF Not Exists ( Select * From CFGC_ALMN_DIST_CB_NivelesAtencion (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia and IdPerfilAtencion = @iIdPerfilAtencion
						 )  
				Begin
					INSERT INTO CFGC_ALMN_DIST_CB_NivelesAtencion ( IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion, Descripcion, Status, Actualizado)
					SELECT @IdEmpresa, @IdEstado, @IdFarmacia, @iIdPerfilAtencion, @Descripcion, @sStatus, @iActualizado
				End
			ELSE
				Begin
					UPDATE CFGC_ALMN_DIST_CB_NivelesAtencion SET Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado 
					WHERE IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia and IdPerfilAtencion = @iIdPerfilAtencion
					
				End
			Set @sMensaje = 'La información se guardo satisfactoriamente. ' 
	   End 
	Else 
		Begin 
			Set @iIdPerfilAtencion = (Select Cast(@IdPerfilAtencion as int))
			Set @sStatus = 'C' 
			Update CFGC_ALMN_DIST_CB_NivelesAtencion Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia and IdPerfilAtencion = @iIdPerfilAtencion
			

		   Set @sMensaje = 'La información ha sido cancelada satisfactoriamente.' 
		End 

	
    Select @sMensaje as Mensaje 
End
Go--#SQL