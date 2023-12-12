If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CambiosCartasCanje_Enc' and xType = 'P')
    Drop Proc spp_Mtto_CambiosCartasCanje_Enc
Go--#SQL 

Create Proc spp_Mtto_CambiosCartasCanje_Enc
(
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioCambio varchar(30), @FolioCarta Varchar(8),
	@FolioTransferenciaVenta varchar(8), @Tipo Varchar(1),
	@TipoMovtoInv varchar(4),  @FechaRegistro datetime, @IdPersonal varchar(6), @Observaciones varchar(500), @SubTotal numeric(14, 4),
	@Iva numeric(14, 4), @Total numeric(14, 4),	@iOpcion int	
)

With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0

-- Este SP, sólo hará inserciones...
If @FolioCambio = '*'
	Begin 
		Select @FolioCambio = max(right(FolioCambio, len(FolioCambio) - len(TipoMovtoInv))) + 1 
		From CambiosCartasCanje_Enc (NoLock) 
	    Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And TipoMovtoInv = @TipoMovtoInv 	   		
	End 

	Set @FolioCambio = IsNull(@FolioCambio, '1') 
	Set @FolioCambio = @TipoMovtoInv + right(replicate('0', 8) + @FolioCambio, 8) 

	--- Iniciar el proceso de guardado 
	If @iOpcion = 1 
		Begin 
			Insert Into CambiosCartasCanje_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, FolioCarta, FolioTransferenciaVenta, Tipo, TipoMovtoInv, FechaRegistro, IdPersonal, Observaciones, 
				SubTotal, Iva, Total)
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCambio, @FolioCarta, @FolioTransferenciaVenta , @Tipo, @TipoMovtoInv, @FechaRegistro, @IdPersonal, @Observaciones, @SubTotal, 
				@Iva, @Total 
		End

	-- Devolver el resultado
	Select @FolioCambio as Folio, @sMensaje as Mensaje
End 
Go--#SQL 