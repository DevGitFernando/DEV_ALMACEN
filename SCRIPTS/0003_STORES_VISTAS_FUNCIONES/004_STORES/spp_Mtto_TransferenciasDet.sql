If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasDet' and xType = 'P' )
   Drop Proc spp_Mtto_TransferenciasDet 
Go--#SQL

Create Proc spp_Mtto_TransferenciasDet 
(
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @FolioTransferencia varchar(18), @IdProducto varchar(8), 
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
	
	If Not Exists ( Select IdEstado From TransferenciasDet (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTransferencia = @FolioTransferencia and 
			  IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and Renglon = @Renglon  ) 
	   Begin 
	       Insert Into TransferenciasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
				  Cant_Enviada, Cant_Devuelta, CantidadEnviada, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTransferencia, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
				  @Cant_Enviada, @Cant_Devuelta, (@Cant_Enviada - @Cant_Devuelta) as CantidadEnviada, @CostoUnitario, @TasaIva, 
				  @SubTotal, @ImpteIva, @Importe, @sStatus, @iActualizado  
	   End 

End 
Go--#SQL
