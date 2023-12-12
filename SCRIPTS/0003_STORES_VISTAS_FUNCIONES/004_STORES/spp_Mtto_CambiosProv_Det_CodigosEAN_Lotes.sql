If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'P' ) 
	Drop Proc spp_Mtto_CambiosProv_Det_CodigosEAN_Lotes
Go--#SQL 

Create Proc spp_Mtto_CambiosProv_Det_CodigosEAN_Lotes 
(
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioCambio varchar(30), @IdProducto varchar(8), @CodigoEAN varchar(30), 
	@IdSubFarmacia varchar(2), @ClaveLote varchar(30), @EsConsignacion smallint, @Cantidad numeric(14, 4), @iOpcion int
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
		Insert Into CambiosProv_Det_CodigosEAN_Lotes (IdEmpresa, IdEstado, IdFarmacia, FolioCambio, IdProducto, CodigoEAN, IdSubFarmacia, 
			ClaveLote, EsConsignacion, Cantidad)
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCambio, @IdProducto, @CodigoEAN, @IdSubFarmacia, @ClaveLote, @EsConsignacion, @Cantidad
	End
End
Go--#SQL 