If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TraspasosDet' and xType = 'P' )
   Drop Proc spp_Mtto_TraspasosDet 
Go--#SQL

Create Proc spp_Mtto_TraspasosDet 
(
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @FolioTraspaso varchar(18), @IdProducto varchar(8), 
	@CodigoEAN varchar(15), @Renglon int,
	@Cantidad Numeric(14,4), @CostoUnitario Numeric(14,4), 
	@TasaIva Numeric(14,2), @SubTotal Numeric(14,4), @ImpteIva Numeric(14,4), @Importe Numeric(14,4)
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  
		

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If Not Exists ( Select FolioTraspaso From TraspasosDet (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTraspaso = @FolioTraspaso and 
			  IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and Renglon = @Renglon  ) 
	   Begin 
	       Insert Into TraspasosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso, IdProducto, CodigoEAN, Renglon, 
				  Cantidad, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTraspaso, @IdProducto, @CodigoEAN, @Renglon, 
				  @Cantidad, @CostoUnitario, @TasaIva, @SubTotal, @ImpteIva, @Importe, @sStatus, @iActualizado  
	   End 

End 
Go--#SQL 