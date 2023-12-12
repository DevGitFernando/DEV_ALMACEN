
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PedidosDistDet_Lotes' and xType = 'P' )
   Drop Proc spp_Mtto_PedidosDistDet_Lotes 
Go--#SQL

Create Proc spp_Mtto_PedidosDistDet_Lotes 
(
    @IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmaciaEnvia varchar(2), @FolioDistribucion varchar(18), @IdProducto varchar(8), 
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


	If Not Exists ( Select IdEstado From PedidosDistDet_Lotes (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdSubFarmaciaEnvia = @IdSubFarmaciaEnvia
			  and FolioDistribucion = @FolioDistribucion and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN 
			  and ClaveLote = @ClaveLote and Renglon = @Renglon  ) 
	   Begin 
	       Insert Into PedidosDistDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioDistribucion, IdProducto, CodigoEAN, 
					   ClaveLote, Renglon, CantidadEnviada, EsConsignacion, Status, Actualizado, IdSubFarmaciaRecibe  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmaciaEnvia, @FolioDistribucion, @IdProducto, @CodigoEAN, 
				  @ClaveLote, @Renglon, @CantidadEnviada, @EsConsignacion, @sStatus, @iActualizado, @IdSubFarmaciaEnvia as IdSubFarmaciaRecibe 
	   End 
	   	
End 
Go--#SQL