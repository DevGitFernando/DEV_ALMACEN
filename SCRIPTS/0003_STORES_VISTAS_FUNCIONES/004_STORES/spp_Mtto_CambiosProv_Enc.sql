If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CambiosProv_Enc' and xType = 'P')
    Drop Proc spp_Mtto_CambiosProv_Enc
Go--#SQL 

Create Proc spp_Mtto_CambiosProv_Enc
(
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioCambio varchar(30), @IdProveedor varchar(4), @TipoMovto varchar(4), 
	@FechaRegistro datetime, @IdPersonal varchar(6), @Observaciones varchar(500), @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4),	
	@iOpcion int	
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
		Select @FolioCambio = max(right(FolioCambio, len(FolioCambio) - len(TipoMovto))) + 1 
		From CambiosProv_Enc (NoLock) 
	    Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And TipoMovto = @TipoMovto 	   		
	End 

	Set @FolioCambio = IsNull(@FolioCambio, '1') 
	Set @FolioCambio = @TipoMovto + right(replicate('0', 8) + @FolioCambio, 8) 

	--- Iniciar el proceso de guardado 
	If @iOpcion = 1 
		Begin 
			Insert Into CambiosProv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProveedor, TipoMovto, FechaRegistro, IdPersonal, Observaciones, 
				SubTotal, Iva, Total)
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCambio, @IdProveedor, @TipoMovto, @FechaRegistro, @IdPersonal, @Observaciones, @SubTotal, 
				@Iva, @Total 
		End

	-- Devolver el resultado
	Select @FolioCambio as Folio, @sMensaje as Mensaje
End 
Go--#SQL 