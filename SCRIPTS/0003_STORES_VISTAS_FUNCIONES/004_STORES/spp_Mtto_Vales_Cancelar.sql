If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Cancelar' and xType = 'P' )
    Drop Proc spp_Mtto_Vales_Cancelar
Go--#SQL
  
Create Proc spp_Mtto_Vales_Cancelar
( @IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioCancelacionVale varchar(32),@FolioVale varchar(32),
 @IdPersonal varchar(6),@FechaSistema varchar(10)
)

With Encryption
As
Begin
Set NoCount On
Set dateFormat YMD

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		
	Set @sMensaje = ''
	Set @sStatus = 'C'
	Set @iActualizado = 0


	If @FolioCancelacionVale = '*' 
	   Select @FolioCancelacionVale = Cast( (Max(FolioCancelacionVale) + 1) As Varchar)  From Vales_Cancelacion (NoLock) 

	Set @FolioCancelacionVale = IsNull(@FolioCancelacionVale, '1')
	Set @FolioCancelacionVale = Right(Replicate('0', 8) + @FolioCancelacionVale, 8)

	If Not Exists ( Select *
					From Vales_Cancelacion (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCancelacionVale = @FolioCancelacionVale ) 

		Begin 
			Insert Into Vales_Cancelacion 
				( IdEmpresa, IdEstado, IdFarmacia, FolioCancelacionVale, RefFolioVale, FechaSistema, IdPersonal) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCancelacionVale, @FolioVale, @FechaSistema, @IdPersonal


			--Encavezado
			Update Vales_EmisionEnc Set Status = @sStatus, Actualizado = @iActualizado
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale

			--Detallado
			Update Vales_EmisionDet Set Status = @sStatus, Actualizado = @iActualizado
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale

			--Información Adicional
			Update Vales_Emision_InformacionAdicional Set Status = @sStatus, Actualizado = @iActualizado
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale
		
			Set @sMensaje = 'La cancelación se a realizado satisfactoriamente con el folio ' + @FolioCancelacionVale + ' .'
		End
	Select @sMensaje As Mensaje, @FolioVale As Folio
		
End
Go--#SQL