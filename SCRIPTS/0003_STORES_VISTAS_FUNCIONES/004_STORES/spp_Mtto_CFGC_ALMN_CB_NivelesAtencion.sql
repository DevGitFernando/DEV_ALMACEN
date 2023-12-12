If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN_CB_NivelesAtencion' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN_CB_NivelesAtencion
Go--#SQL

Create Proc spp_Mtto_CFGC_ALMN_CB_NivelesAtencion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	@IdPerfilAtencion varchar(10) = '1', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005', @IdPrograma varchar(4) = '0002',
	@IdSubPrograma varchar(4) = '1350', @Descripcion varchar(100) = 'Oportunidades', @iOpcion smallint 
)
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

	If @IdPerfilAtencion = '*' 
	Begin 
		Select @iIdPerfilAtencion = cast( (max(IdPerfilAtencion) + 1) as int)  From CFGC_ALMN_CB_NivelesAtencion (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia
	End 
	
	Set @iIdPerfilAtencion = IsNull(@iIdPerfilAtencion, 1) 
	Set @iIdPerfilAtencion = (Select Cast(@iIdPerfilAtencion as int)) 	
	
	

	If @iOpcion = 1 
       Begin
			IF Not Exists ( Select * From CFGC_ALMN_CB_NivelesAtencion (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia and IdPerfilAtencion = @iIdPerfilAtencion
							and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente AND IdPrograma = @IdPrograma AND IdSubPrograma = @IdSubPrograma )  
				Begin
					INSERT INTO CFGC_ALMN_CB_NivelesAtencion ( IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion, IdCliente, IdSubCliente, 
																IdPrograma, IdSubPrograma, Descripcion, Status, Actualizado)
					SELECT @IdEmpresa, @IdEstado, @IdFarmacia, @iIdPerfilAtencion, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
					@Descripcion, @sStatus, @iActualizado
				End
			ELSE
				Begin
					UPDATE CFGC_ALMN_CB_NivelesAtencion SET Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado 
					WHERE IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia and IdPerfilAtencion = @iIdPerfilAtencion
					and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente AND IdPrograma = @IdPrograma AND IdSubPrograma = @IdSubPrograma
				End
			Set @sMensaje = 'La información se guardo satisfactoriamente. ' 
	   End 
	Else 
		Begin 
			Set @sStatus = 'C' 
			Update CFGC_ALMN_CB_NivelesAtencion Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia and IdPerfilAtencion = @iIdPerfilAtencion
			and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente AND IdPrograma = @IdPrograma AND IdSubPrograma = @IdSubPrograma

		   Set @sMensaje = 'La información ha sido cancelada satisfactoriamente.' 
		End 

	
    Select @sMensaje as Mensaje 
End
Go--#SQL