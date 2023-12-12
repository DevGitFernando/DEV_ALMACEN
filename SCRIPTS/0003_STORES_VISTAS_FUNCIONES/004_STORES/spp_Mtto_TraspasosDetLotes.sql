If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TraspasosDetLotes' and xType = 'P' )
   Drop Proc spp_Mtto_TraspasosDetLotes 
Go--#SQL

Create Proc spp_Mtto_TraspasosDetLotes 
(
    @IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), @FolioTraspaso varchar(18), @IdProducto varchar(8), 
	@CodigoEAN varchar(15), @ClaveLote varchar(30), @Renglon int, @Cantidad Numeric(14,4)
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


	If Not Exists ( Select IdEstado From TraspasosDet_Lotes (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
			  and FolioTraspaso = @FolioTraspaso and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN 
			  and ClaveLote = @ClaveLote and Renglon = @Renglon  ) 
	   Begin 
	       Insert Into TraspasosDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, CodigoEAN, 
					   ClaveLote, Renglon, Cantidad, EsConsignacion, Status, Actualizado  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioTraspaso, @IdProducto, @CodigoEAN, 
				  @ClaveLote, @Renglon, @Cantidad, @EsConsignacion, @sStatus, @iActualizado
	   End 
	   	
End 
Go--#SQL