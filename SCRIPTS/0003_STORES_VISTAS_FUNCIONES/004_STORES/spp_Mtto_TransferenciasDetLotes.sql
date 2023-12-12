If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasDetLotes' and xType = 'P' )
   Drop Proc spp_Mtto_TransferenciasDetLotes 
Go--#SQL

Create Proc spp_Mtto_TransferenciasDetLotes 
(
    @IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmaciaEnvia varchar(2), @FolioTransferencia varchar(18), @IdProducto varchar(8), 
	@CodigoEAN varchar(15), @ClaveLote varchar(30), @Renglon int, @CantidadEnviada Numeric(14,4) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,  
	    @EsConsignacion bit 		

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)	


	If Not Exists ( Select IdEstado From TransferenciasDet_Lotes (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdSubFarmaciaEnvia = @IdSubFarmaciaEnvia
			  and FolioTransferencia = @FolioTransferencia and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN 
			  and ClaveLote = @ClaveLote and Renglon = @Renglon  ) 
	   Begin 
	       Insert Into TransferenciasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, 
					   ClaveLote, Renglon, Cant_Enviada, Cant_Devuelta, CantidadEnviada, EsConsignacion, Status, Actualizado, IdSubFarmaciaRecibe  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmaciaEnvia, @FolioTransferencia, @IdProducto, @CodigoEAN, 
				  @ClaveLote, @Renglon, @CantidadEnviada, 0, @CantidadEnviada, @EsConsignacion, @sStatus, @iActualizado, @IdSubFarmaciaEnvia as IdSubFarmaciaRecibe
				  				
		   Update L
		   Set CostoUnitario = (Select UltimoCosto From FarmaciaProductos_CodigoEAN_Lotes
									Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmaciaEnvia
											And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote)
	       From TransferenciasDet_Lotes L
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmaciaEnvia = @IdSubFarmaciaEnvia
				  And FolioTransferencia = @FolioTransferencia And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
				  And Renglon = @Renglon 
	   End 
	   	
End 
Go--#SQL