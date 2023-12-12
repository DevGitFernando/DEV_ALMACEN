If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_AjustesInv_EliminarProductos' and xType = 'P' ) 
   Drop Proc spp_Mtto_AjustesInv_EliminarProductos
Go--#SQL

Create Proc spp_Mtto_AjustesInv_EliminarProductos ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@Poliza varchar(8), @IdProducto varchar(8), @CodigoEAN varchar(30) ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @Actualizado smallint 
	Set @Actualizado = 0 
	Set @Actualizado = 3  --- Solo se marca para replicacion cuando se termina el Proceso  

		
	-- Se desbloquean los productos de Farmacia Productos
	Update FarmaciaProductos Set Status = 'A', Actualizado = 0 
	Where IdEmpresa= @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdProducto = @IdProducto


	-- Se eliminan los productos de la tabla AjustesInv_Det_Lotes_Ubicaciones
	Delete From AjustesInv_Det_Lotes_Ubicaciones Where IdEmpresa= @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And Poliza = @Poliza And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN

	-- Se eliminan los productos de la tabla AjustesInv_Det_Lotes
	Delete From AjustesInv_Det_Lotes Where IdEmpresa= @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And Poliza = @Poliza And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN

	-- Se eliminan los productos de la tabla AjustesInv_Det
	Delete From AjustesInv_Det Where IdEmpresa= @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And Poliza = @Poliza And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN

End 
Go--#SQL 