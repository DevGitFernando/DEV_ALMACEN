If Exists (Select Name From SysObjects (NoLock) Where Name = 'spp_OrdenDeCompra_ProductosSelectos' And xType = 'P')
	Drop Proc spp_OrdenDeCompra_ProductosSelectos
Go--#SQL 

Create Proc spp_OrdenDeCompra_ProductosSelectos
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4) = '0003', @FolioOrden varchar(8) = '00000457'
) 
With Encryption 
As
Begin
Set NoCount On
Set DateFormat YMD

	Select Distinct S.ClaveSSA, S.DescripcionSal
	From OrdenesDeComprasDet D (NoLock)
	Inner Join vw_Productos_CodigoEAN S (NoLock) On (D.IdProducto = S.IdProducto And D.CodigoEAN = S.CodigoEAN)
	Inner Join vw_ProductosSelectos C (NoLock)
		On (D.IdEmpresa = C.IdEmpresa And D.IdEstado = C.IdEstado And D.IdFarmacia = C.IdFarmacia And S.IdClaveSSA_Sal = C.IdClaveSSA_Sal)
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia And D.FolioOrdenCompra = @FolioOrden
	
End
Go--#SQL 