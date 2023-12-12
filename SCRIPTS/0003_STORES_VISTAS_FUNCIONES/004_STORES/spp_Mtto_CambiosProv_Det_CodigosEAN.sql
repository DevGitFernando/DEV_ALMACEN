If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CambiosProv_Det_CodigosEAN' and xType = 'P' ) 
   Drop Proc spp_Mtto_CambiosProv_Det_CodigosEAN
Go--#SQL

Create Proc spp_Mtto_CambiosProv_Det_CodigosEAN 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioCambio varchar(30), @IdProducto varchar(8), @CodigoEAN varchar(30), 
	@Cantidad numeric(14, 4), @Costo numeric(14, 4), @TasaIva numeric(14, 4), @Importe numeric(14, 4), @iOpcion int
)

With Encryption 
As 
Begin 
Set NoCount On 

Declare @Mensaje varchar(1000), 
		@Status varchar(1), 
		@Actualizado smallint

	Set @Mensaje = ''
	Set @Status = 'A'
	Set @Actualizado = 0

	If @iOpcion = 1
	Begin
		Insert Into CambiosProv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN, Cantidad, Costo, TasaIva, 
			Importe, Status, Actualizado)
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCambio, @IdProducto, @CodigoEAN, @Cantidad, @Costo, @TasaIva, @Importe, @Status, @Actualizado
	End

	-- Devolver el resultado
	Select @FolioCambio as Folio, @Mensaje as Mensaje
End
Go--#SQL 
