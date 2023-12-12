

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_Adt_Rechazos_Det' And xType = 'P' )
	Drop Proc spp_Mtto_COM_Adt_Rechazos_Det
Go--#SQL

		----		Exec spp_Mtto_COM_Adt_Rechazos_Det '001', '21', '2182', '*', '00000001', '0001', 'PROVEEDOR', 'OBSERVACIONES', 1

Create Procedure spp_Mtto_COM_Adt_Rechazos_Det 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', 
	@FolioRechazo varchar(30) = '00000001', @IdRechazo varchar(3) = '001'
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
	
	
		If Not Exists ( Select * From COM_Adt_Rechazos_Det  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
						and FolioRechazo = @FolioRechazo and IdRechazo = @IdRechazo )
		Begin
			Insert Into COM_Adt_Rechazos_Det  ( IdEmpresa, IdEstado, IdFarmacia, FolioRechazo, IdRechazo, Status, Actualizado )
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioRechazo, @IdRechazo, @Status, @Actualizado
			
			Set @sMensaje = 'La información del Folio ' + @FolioRechazo + ' se guardo exitosamente'
		End		
	
	
	-- Regresar la Clave Generada
    Select @FolioRechazo as Folio, @sMensaje as Mensaje 

End
Go--#SQL


