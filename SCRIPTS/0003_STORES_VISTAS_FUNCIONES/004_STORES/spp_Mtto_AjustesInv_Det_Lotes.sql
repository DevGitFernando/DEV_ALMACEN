
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_AjustesInv_Det_Lotes' and xType = 'P' ) 
   Drop Proc spp_Mtto_AjustesInv_Det_Lotes
Go--#SQL

Create Proc spp_Mtto_AjustesInv_Det_Lotes 
(	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), @Poliza varchar(8), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30), @ExistenciaFisica int, @Costo numeric(14,4), @Importe numeric(14,4), 
	@ExistenciaSistema int, @Referencia varchar(30), @Status varchar(1) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @Actualizado smallint 
	Set @Actualizado = 0 
	Set @Actualizado = 3  --- Solo se marca para replicacion cuando se termina el Proceso  
	
	If Not Exists ( Select * From AjustesInv_Det_Lotes (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					And IdSubFarmacia = @IdSubFarmacia And Poliza = @Poliza and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN 
					And ClaveLote = @ClaveLote ) 
	   Begin 
	       Insert Into AjustesInv_Det_Lotes ( IdEmpresa ,IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, ExistenciaFisica, Costo, Importe, ExistenciaSistema, Referencia, Status, Actualizado ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @Poliza, @IdProducto, @CodigoEAN, @ClaveLote, @ExistenciaFisica, @Costo, @Importe, @ExistenciaSistema, @Referencia, @Status, @Actualizado 
	   End 
	Else 
	   Begin 
	       Update AjustesInv_Det_Lotes Set 
				ExistenciaFisica = @ExistenciaFisica, Costo = @Costo, Importe = @Importe, ExistenciaSistema = @ExistenciaSistema, 
				Status = @Status, Actualizado = @Actualizado 
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
	             and Poliza = @Poliza and IdProducto = @IdProducto  and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
	   End    

End 
Go--#SQL
