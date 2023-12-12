If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_RPT_Pedidos_Cedis_Validacion' and xType = 'P' ) 
   Drop Proc spp_RPT_Pedidos_Cedis_Validacion 
Go--#SQL

Create Proc spp_RPT_Pedidos_Cedis_Validacion 
	(
		@IdEmpresa varchar(3) = '001', @IdEstado varchar(4) = '11', @IdFarmacia varchar(6) = '0005', @FolioSurtido varchar(8) = '00000006'
	)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

	Select IdProducto, CodigoEAN, ClaveLote
	Into #Pedidos
	From Pedidos_Cedis_Validacion (NoLock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido

	Insert Into #Pedidos
	Select IdProducto, CodigoEAN, ClaveLote
	From Pedidos_Cedis_Det_Surtido_Distribucion (NoLock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido And CantidadAsignada > 0


	Select Distinct IdProducto, CodigoEAN, ClaveLote, 0 As Cantidad_Pedido, 0 As Cantidad_Validacion, 0 As Diferencia
	Into #Pedidos_validacion
	From #Pedidos

	Update V Set Cantidad_Validacion = Cantidad
	From #Pedidos_validacion V
	Inner Join Pedidos_Cedis_Validacion P (NoLock) On (V.IdProducto = P.IdProducto And V.CodigoEAN = P.CodigoEAN And V.ClaveLote = P.ClaveLote )
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido

	Update V Set Cantidad_Pedido = CantidadAsignada
	From #Pedidos_validacion V
	Inner Join Pedidos_Cedis_Det_Surtido_Distribucion P (NoLock) On (V.IdProducto = P.IdProducto And V.CodigoEAN = P.CodigoEAN And V.ClaveLote = P.ClaveLote )
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido


	Update #Pedidos_validacion Set Diferencia = Cantidad_Pedido - Cantidad_Validacion

	Select ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Sal', V.IdProducto As 'Código', V.CodigoEAN As 'Código EAN',
		  P.Descripcion As 'Descrión', V.ClaveLote As Lote, V.Cantidad_Pedido As 'Cantidad Pedido', V.Cantidad_Validacion As 'Cantidad Validación', V.Diferencia
	From #Pedidos_validacion V (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (V.IdProducto = P.Idproducto And V.CodigoEAN = P.CodigoEAN)
	Where Cantidad_Pedido - Cantidad_Validacion <> 0

End 
Go--#SQL