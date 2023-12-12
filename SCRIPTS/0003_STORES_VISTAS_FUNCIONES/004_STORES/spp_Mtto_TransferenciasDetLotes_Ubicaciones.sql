If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasDetLotes_Ubicaciones' and xType = 'P' )
   Drop Proc spp_Mtto_TransferenciasDetLotes_Ubicaciones 
Go--#SQL

Create Proc spp_Mtto_TransferenciasDetLotes_Ubicaciones 
(
    @IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmaciaEnvia varchar(2), @FolioTransferencia varchar(18), @IdProducto varchar(8), 
	@CodigoEAN varchar(15), @ClaveLote varchar(30), @Renglon int, 
	@IdPasillo int, @IdEstante int, @IdEntrepano int, @CantidadEnviada Numeric(14,4) 
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


	If Not Exists ( Select IdEstado From TransferenciasDet_Lotes_Ubicaciones (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdSubFarmaciaEnvia = @IdSubFarmaciaEnvia
			  and FolioTransferencia = @FolioTransferencia and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
			  and Renglon = @Renglon and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepano ) 
	   Begin 
	       Insert Into TransferenciasDet_Lotes_Ubicaciones 
	        (     
	              IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, FolioTransferencia, 
                  IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, 
                  IdPasillo, IdEstante, IdEntrepaño, Cant_Enviada, CantidadEnviada, Status, Actualizado
            ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmaciaEnvia, @IdSubFarmaciaEnvia as IdSubFarmaciaRecibe, @FolioTransferencia, 
	              @IdProducto, @CodigoEAN, @ClaveLote, @Renglon, @EsConsignacion, 
	              @IdPasillo, @IdEstante, @IdEntrepano, @CantidadEnviada, @CantidadEnviada, @sStatus, @iActualizado 
	   End 
	   	
End 
Go--#SQL 
