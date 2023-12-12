
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_AjustesInv_Det_Lotes_Ubicaciones' and xType = 'P' ) 
   Drop Proc spp_Mtto_AjustesInv_Det_Lotes_Ubicaciones
Go--#SQL

Create Proc spp_Mtto_AjustesInv_Det_Lotes_Ubicaciones 
(	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), @Poliza varchar(8), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30), @IdPasillo int, @IdEstante int, @IdEntrepaño int, 
	@ExistenciaFisica int, @Costo numeric(14,4), @Importe numeric(14,4), @ExistenciaSistema int, @Referencia varchar(30), @Status varchar(1) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @Actualizado smallint 
	Set @Actualizado = 0 
	Set @Actualizado = 3  --- Solo se marca para replicacion cuando se termina el Proceso  
	
	If Not Exists ( Select * From AjustesInv_Det_Lotes_Ubicaciones (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					And IdSubFarmacia = @IdSubFarmacia And Poliza = @Poliza and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN 
					And ClaveLote = @ClaveLote And IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepaño = @IdEntrepaño ) 
	   Begin 
	       Insert Into AjustesInv_Det_Lotes_Ubicaciones ( IdEmpresa ,IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, ExistenciaFisica, Costo, Importe, ExistenciaSistema, Referencia, Status, Actualizado ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @Poliza, @IdProducto, @CodigoEAN, @ClaveLote, @IdPasillo, @IdEstante, @IdEntrepaño, @ExistenciaFisica, @Costo, @Importe, @ExistenciaSistema, @Referencia, @Status, @Actualizado 
	   End 
	Else 
	   Begin 
	       Update AjustesInv_Det_Lotes_Ubicaciones Set 
				ExistenciaFisica = @ExistenciaFisica, Costo = @Costo, Importe = @Importe, ExistenciaSistema = @ExistenciaSistema, 
				Status = @Status, Actualizado = @Actualizado 
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
	             And Poliza = @Poliza and IdProducto = @IdProducto  and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
				 And IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepaño = @IdEntrepaño 
	   End    

End 
Go--#SQL

