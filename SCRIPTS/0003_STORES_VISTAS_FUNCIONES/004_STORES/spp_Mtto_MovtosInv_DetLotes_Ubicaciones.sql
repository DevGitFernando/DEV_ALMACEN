If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_MovtosInv_DetLotes_Ubicaciones' and xType = 'P' ) 
   Drop Proc spp_Mtto_MovtosInv_DetLotes_Ubicaciones
Go--#SQL

Create Proc spp_Mtto_MovtosInv_DetLotes_Ubicaciones 
( 
    @IdEmpresa varchar(3), @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', @IdSubFarmacia varchar(2) = '01', 
    @FolioMovtoInv varchar(14), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30), @Cantidad int, 
	@IdPasillo int, @IdEstante int, @IdEntrepano int, @Status varchar(1) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
		@Actualizado smallint, 
	    @EsConsignacion bit, 
	    @TipoMovto varchar(5), 
	    @TipoMovto_EPR varchar(5)  


	Set @TipoMovto = left(@FolioMovtoInv, 3) 
	Set @TipoMovto_EPR = 'EPR'
	Set @Actualizado = 0 
	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)

	If Not Exists ( Select * From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones (NoLock) 
	                Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and
					      IdSubFarmacia = @IdSubFarmacia and FolioMovtoInv = @FolioMovtoInv 
					      and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
					      and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepano  
				  ) 
	   Begin 
	       Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
	            ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
	              IdPasillo, IdEstante, IdEntrepaño, Cantidad, Status, Actualizado  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioMovtoInv, @IdProducto, @CodigoEAN, @ClaveLote, @EsConsignacion,  
	              @IdPasillo, @IdEstante, @IdEntrepano, @Cantidad, @Status, @Actualizado  
	   End 
	Else 
	   Begin 
	       Update MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
	             Set Cantidad = (case when @TipoMovto_EPR = @TipoMovto then Cantidad + @Cantidad else @Cantidad end), 
					Status = @Status, Actualizado = @Actualizado 
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
				and FolioMovtoInv = @FolioMovtoInv and IdProducto = @IdProducto  and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
				and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepano 
	   End    
End 
Go--#SQL
