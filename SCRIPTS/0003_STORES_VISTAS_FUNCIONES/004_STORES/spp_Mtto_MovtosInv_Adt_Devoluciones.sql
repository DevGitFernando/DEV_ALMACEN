


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_MovtosInv_Adt_Devoluciones' And xType = 'P' )
	Drop Proc spp_Mtto_MovtosInv_Adt_Devoluciones
Go--#SQL

		----		Exec spp_Mtto_MovtosInv_Adt_Devoluciones '001', '21', '2182', 'ED00000001', 'CC', '001'

Create Procedure spp_Mtto_MovtosInv_Adt_Devoluciones 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', 
	@FolioMovtoInv varchar(30) = 'ED00000001', @IdTipoMovto_Inv varchar(6) = 'ED', @IdMotivo varchar(3) = '001'
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
	
	If Not Exists ( Select * From MovtosInv_Adt_Devoluciones Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
					and FolioMovtoInv = @FolioMovtoInv and IdTipoMovto_Inv = @IdTipoMovto_Inv and IdMotivo = @IdMotivo )
	Begin
		Insert Into MovtosInv_Adt_Devoluciones ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, IdMotivo, Status, Actualizado )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv, @IdTipoMovto_Inv, @IdMotivo, @Status, @Actualizado
	End
	

	Set @sMensaje = 'La información del Folio ' + @FolioMovtoInv + ' se guardo exitosamente'
	 
	-- Regresar la Clave Generada
    Select @FolioMovtoInv as Folio, @sMensaje as Mensaje 

End
Go--#SQL


