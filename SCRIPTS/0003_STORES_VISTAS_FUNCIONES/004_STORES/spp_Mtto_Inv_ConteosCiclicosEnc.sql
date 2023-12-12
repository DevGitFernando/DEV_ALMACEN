


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Inv_ConteosCiclicosEnc' And xType = 'P' )
	Drop Proc spp_Mtto_Inv_ConteosCiclicosEnc
Go--#SQL

		----		Exec spp_Mtto_Inv_ConteosCiclicosEnc '001', '11', '0005', '*', '0001', 1

Create Procedure spp_Mtto_Inv_ConteosCiclicosEnc 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', 
	@FolioConteo varchar(30) = '*', @IdPersonal varchar(4) = '0001', @Opcion tinyint = 1
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
	
	If @FolioConteo = '*' 
	   Select @FolioConteo = cast( (max(FolioConteo) + 1) as varchar)  From Inv_ConteosCiclicosEnc (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

	-- Asegurar que @FolioConteo sea valido y formatear la cadena 
	Set @FolioConteo = IsNull(@FolioConteo, '1')
	Set @FolioConteo = right(replicate('0', 8) + @FolioConteo, 8)
	
	If @Opcion = 1
	Begin
		If Not Exists ( Select * From Inv_ConteosCiclicosEnc  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
						and FolioConteo = @FolioConteo )
		Begin
			Insert Into Inv_ConteosCiclicosEnc  ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo, IdPersonal, FechaRegistro, Status, Actualizado )
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioConteo, @IdPersonal, GetDate(), @Status, @Actualizado
			
			Set @sMensaje = 'La información del Folio ' + @FolioConteo + ' se guardo exitosamente'
		End		
	End
	Else
	Begin
		Set @Status = 'C'
		Update Inv_ConteosCiclicosEnc Set Status = @Status, Actualizado = @Actualizado
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioConteo = @FolioConteo
		
		Set @sMensaje = 'La información del Folio ' + @FolioConteo + ' se cancelo exitosamente'
		
	End	
	 
	-- Regresar la Clave Generada
    Select @FolioConteo as Folio, @sMensaje as Mensaje 

End
Go--#SQL


