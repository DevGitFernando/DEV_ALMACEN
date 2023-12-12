


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_Adt_CheckList_Recepcion' And xType = 'P' )
	Drop Proc spp_Mtto_COM_Adt_CheckList_Recepcion
Go--#SQL

		----		Exec spp_Mtto_COM_Adt_CheckList_Recepcion '001', '21', '2182', '00000001', '001', '001', 0, 0, 0, '', 0

Create Procedure spp_Mtto_COM_Adt_CheckList_Recepcion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', 
	@FolioOrdenCompra varchar(30) = '00000001', @IdGrupo varchar(3) = '001', @IdMotivo varchar(3) = '001',
	@Respuesta_SI tinyint = 0, @Respuesta_NO tinyint = 0, @Respuesta_Rechazo tinyint = 0, @Comentario varchar(200) = '',
	@EsFirmado tinyint = 0
)
With Encryption 
As
Begin
	Declare @Status varchar(1), 
			@Actualizado int,
			@sMensaje varchar(8000)

	Set @Status = 'A'
	Set @Actualizado = 0
	Set @sMensaje = ''
	
	If Not Exists ( Select * From COM_Adt_CheckList_Recepcion Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
					and FolioOrdenCompra = @FolioOrdenCompra and IdGrupo = @IdGrupo and IdMotivo = @IdMotivo )
	Begin
		Insert Into COM_Adt_CheckList_Recepcion ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, IdGrupo, IdMotivo, 
												  Respuesta_SI, Respuesta_NO, Respuesta_Rechazo, Comentario, EsFirmado, Status, Actualizado )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrdenCompra, @IdGrupo, @IdMotivo, 
		@Respuesta_SI, @Respuesta_NO, @Respuesta_Rechazo, @Comentario, @EsFirmado, @Status, @Actualizado
	End
	

	Set @sMensaje = 'La información del Folio ' + @FolioOrdenCompra + ' se guardo exitosamente'
	 
	-- Regresar la Clave Generada
    Select @FolioOrdenCompra as Folio, @sMensaje as Mensaje 

End
Go--#SQL


