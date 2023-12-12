If Exists (Select Name From SysObjects Where Name = 'vw_GeneracionPedidosNadro' And xType = 'V')
	Drop View vw_GeneracionPedidosNadro
Go--#SQL

Create View vw_GeneracionPedidosNadro
With Encryption
As
	Select 
	'RegistroCabecero' = 'N' + 'NADRO' + FolioDistribucion + '000' + ' ' + IdEmpresa + IdEstado + IdFarmacia + FolioPedido + Convert(Varchar(8), Getdate(), 112),
	'RegistroDetalle' = Right('0000000000000000000000'+ CodigoEAN, 14) + Right('000000' + Cast(Cast(Cantidad As int) As Varchar(6)), 6), 
	IdEmpresaDist, IdEstadoDist, IdFarmaciaDist, FolioDistribucion, IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, IdProductoDistribuidor,
	Cantidad, Procesado, Status, Actualizado 

	From Pedidos_Cedis_Det_Pedido_Distribuidor(NoLock)
Go--#SQL