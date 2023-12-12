If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Com_PFAR_SalesPedidos' And xType = 'P' )
	Drop Proc spp_Com_PFAR_SalesPedidos
Go--#SQL

Create Proc spp_Com_PFAR_SalesPedidos 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FechaInicial varchar(10) = '2009-09-01', @FechaFinal varchar(10) = '2009-12-30' 
) 
With Encryption 
As
Begin
Set NoCount On

	Select P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSAl, D.CodigoEAN, Sum(D.CantidadVendida) as Cantidad
	From VentasEnc V(NoLock)
	Inner Join VentasDet D(NoLock) 
		On( V.IdEmpresa = D.IdEmpresa And V.IdEstado = D.IdEstado And V.IdFarmacia = D.IdFarmacia And V.FolioVenta = D.FolioVenta )
	Inner Join vw_Productos P(NoLock) On( D.IdProducto = P.IdProducto )
	Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
		And Convert(varchar(10), V.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
		And V.TipoDeVenta = 2
	Group By P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSAl , D.CodigoEAN
	Having sum(D.CantidadVendida) > 0 	
	Order By P.IdClaveSSA_Sal

End 
Go--#SQL
