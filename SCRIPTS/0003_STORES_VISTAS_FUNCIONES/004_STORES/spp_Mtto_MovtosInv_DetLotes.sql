If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_MovtosInv_DetLotes' and xType = 'P' ) 
   Drop Proc spp_Mtto_MovtosInv_DetLotes
Go--#SQL

Create Proc spp_Mtto_MovtosInv_DetLotes ( @IdEmpresa varchar(3), @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001',
	@IdSubFarmacia varchar(2) = '01', @FolioMovtoInv varchar(14), @IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30), @Cantidad int, 
	@Costo numeric(14,4), @Importe numeric(14,4), @Status varchar(1) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @Actualizado smallint, 
	    @EsConsignacion bit 

	Set @Actualizado = 0 
	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)

	If Not Exists ( Select * From MovtosInv_Det_CodigosEAN_Lotes (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and
					IdSubFarmacia = @IdSubFarmacia and FolioMovtoInv = @FolioMovtoInv and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote ) 
	   Begin 
	       Insert Into MovtosInv_Det_CodigosEAN_Lotes ( IdEmpresa ,IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, Cantidad, Costo, Importe, EsConsignacion, Status, Actualizado  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioMovtoInv, @IdProducto, @CodigoEAN, @ClaveLote, @Cantidad, @Costo, @Importe, @EsConsignacion, @Status, @Actualizado  
	   End 
	Else 
	   Begin 
	       Update MovtosInv_Det_CodigosEAN_Lotes Set Cantidad = @Cantidad, Costo = @Costo, Importe = @Importe, Status = @Status, Actualizado = @Actualizado 
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
				and FolioMovtoInv = @FolioMovtoInv and IdProducto = @IdProducto  and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
	   End    

End 
Go--#SQL
