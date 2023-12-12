If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_PFAR_ProductosPedidos' And xType = 'P' )
	Drop Proc spp_COM_PFAR_ProductosPedidos
Go--#SQL 

Create Proc spp_COM_PFAR_ProductosPedidos 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FechaInicial varchar(10) = '2009-09-01', @FechaFinal varchar(10) = '2009-12-30' 
)
With Encryption 
As
Begin
Set NoCount On

	Select D.IdProducto, P.Descripcion, D.CodigoEAN, Sum(D.Cantidad) as Cantidad
	From vw_VentasEnc V(NoLock)
	Inner Join vw_VentasDet_CodigosEAN_Lotes D(NoLock) 
		On( V.IdEmpresa = D.IdEmpresa And V.IdEstado = D.IdEstado And V.IdFarmacia = D.IdFarmacia And V.Folio = D.Folio )
	Inner Join vw_Productos P(NoLock) On( D.IdProducto = P.IdProducto )
	Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
		And Convert(varchar(10), v.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
		And V.TipoDeVenta = 1 
	Group By D.IdProducto, P.Descripcion, D.CodigoEAN 
	Having sum(D.Cantidad) > 0 
	Order By IdProducto

End
Go--#SQL
