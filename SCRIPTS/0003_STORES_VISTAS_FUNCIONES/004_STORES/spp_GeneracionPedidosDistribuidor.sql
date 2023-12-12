If Exists (Select Name From SysObjects Where Name = 'spp_GeneracionPedidoDistribuidor' And xType = 'P')
	Drop Proc spp_GeneracionPedidoDistribuidor
Go--#SQL

Create Proc spp_GeneracionPedidoDistribuidor (@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdDistribuidor int)
With Encryption
As

	Select Distinct RegistroCabecero 
	From vw_GeneracionPedidosNadro(NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Procesado = 0

	Select Distinct vGP.RegistroCabecero, vGP.RegistroDetalle, 'ID. Producto' = vGP.IdProducto, 'Código EAN' = vGP.CodigoEAN, 
	'Descripcion' = vPC.Descripcion, 'Cantidad' = vGP.Cantidad 
	From vw_GeneracionPedidosNadro vGP(NoLock) 
	Inner Join vw_Productos_CodigoEAN vPC(NoLock) On (vPC.IdProducto = vGP.IdProducto)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Procesado = 0

Go--#SQL