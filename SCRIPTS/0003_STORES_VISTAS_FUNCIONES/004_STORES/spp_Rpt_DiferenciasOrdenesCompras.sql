

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_DiferenciasOrdenesCompras' and xType = 'P')
    Drop Proc spp_Rpt_DiferenciasOrdenesCompras
Go--#SQL

--		Exec spp_Rpt_DiferenciasOrdenesCompras '001', '21', '1182', '00001429'
  
Create Proc spp_Rpt_DiferenciasOrdenesCompras 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioOrden varchar(8)
)
With Encryption 
As
Begin
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500), @sTablaRegistro varchar(100) 

	Set @sSql = ''	
	Set @iResultado = 0	

	--- 
	Select P.ClaveSSA, P.DescripcionSal, Cast(Sum(T.CantidadRecibida) as int) As Cantidad
	Into #tmpRegistroOC	
	From OrdenesDeComprasDet T (Nolock)		
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia 
	And T.FolioOrdenCompra In ( Select FolioOrdenCompra From OrdenesDeComprasEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								And FolioOrdenCompraReferencia = @FolioOrden )
	Group By P.ClaveSSA, P.DescripcionSal


	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.EntregarEn as IdFarmacia, E.FarmaciaEntregarEn as Farmacia,
	E.IdProveedor, E.Proveedor, E.FechaRegistro, E.FechaRequeridaEntrega,
	P.ClaveSSA, P.DescripcionCortaClave As DescripcionSal, P.IdPresentacion_ClaveSSA, P.Presentacion_ClaveSSA, Cast(Sum(T.Cantidad) as int) As Cantidad
	Into #tmpOrdenCompra
	From vw_OrdenesCompras_Claves_Enc E (Nolock)
	Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det T (Nolock)
		On ( E.IdEmpresa = T.IdEmpresa and E.IdEstado = T.IdEstado and E.IdFarmacia = T.IdFarmacia and E.Folio = T.FolioOrden )
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.IdProducto = P.IdProducto AND T.CodigoEAN = P.CodigoEAN ) 	
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.Folio = @FolioOrden
	Group By E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.EntregarEn, E.FarmaciaEntregarEn,
	E.IdProveedor, E.Proveedor, E.FechaRegistro, E.FechaRequeridaEntrega,
	P.ClaveSSA, P.DescripcionCortaClave, P.IdPresentacion_ClaveSSA, P.Presentacion_ClaveSSA
 

	Select T.IdEmpresa, T.Empresa, T.IdEstado, T.Estado, T.IdFarmacia, T.Farmacia, @FolioOrden As FolioOrden,
	T.IdProveedor, T.Proveedor, T.FechaRegistro, T.FechaRequeridaEntrega,
	T.ClaveSSA, T.DescripcionSal, T.IdPresentacion_ClaveSSA, T.Presentacion_ClaveSSA, 
	T.Cantidad as CantidadRequerida, IsNull(R.Cantidad, 0) as CantidadIngresada, 
	Cast(ABS( T.Cantidad - ( IsNull(R.Cantidad, 0)) ) as int ) as CantidadFaltante 
	Into #tmpValidarDiferencias
	From #tmpOrdenCompra T (Nolock)
	Inner Join #tmpRegistroOC R (Nolock)
		On ( T.ClaveSSA = R.ClaveSSA )	

	Select * From #tmpValidarDiferencias (Nolock)	

--		Exec spp_Rpt_DiferenciasOrdenesCompras '001', '21', '1182', '00001429'

End
Go--#SQL