If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PedidosDistDet' and xType = 'P' )
   Drop Proc spp_Mtto_PedidosDistDet 
Go--#SQL

Create Proc spp_Mtto_PedidosDistDet 
(
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @FolioDistribucion varchar(18), @IdProducto varchar(8), 
	@CodigoEAN varchar(15), @Renglon int, @UnidadDeEntrada smallint, 
	@Cant_Enviada Numeric(14,4), @Cant_Devuelta Numeric(14,4), @CantidadEnviada Numeric(14,4), @CostoUnitario Numeric(14,4), 
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
	
	If Not Exists ( Select IdEstado From PedidosDistDet (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioDistribucion = @FolioDistribucion and 
			  IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and Renglon = @Renglon  ) 
	   Begin 
	       Insert Into PedidosDistDet ( IdEmpresa, IdEstado, IdFarmacia, FolioDistribucion, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
				  Cant_Enviada, Cant_Devuelta, CantidadEnviada, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioDistribucion, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
				  @Cant_Enviada, @Cant_Devuelta, (@Cant_Enviada - @Cant_Devuelta) as CantidadEnviada, @CostoUnitario, @TasaIva, 
				  @SubTotal, @ImpteIva, @Importe, @sStatus, @iActualizado  
	   End 

End 
Go--#SQL
