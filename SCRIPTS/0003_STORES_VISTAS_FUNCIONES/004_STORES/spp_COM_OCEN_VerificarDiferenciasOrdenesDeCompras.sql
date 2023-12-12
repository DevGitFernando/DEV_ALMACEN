

	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_VerificarDiferenciasOrdenesDeCompras' and xType = 'P')
    Drop Proc spp_COM_OCEN_VerificarDiferenciasOrdenesDeCompras
Go--#SQL

--		Exec spp_COM_OCEN_VerificarDiferenciasOrdenesDeCompras '001', '11', '0003', '00000002'
  
Create Proc spp_COM_OCEN_VerificarDiferenciasOrdenesDeCompras
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', 
	@FolioOrdenCompraReferencia varchar(10) = '00000001'
)
With Encryption 
As
Begin
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500), @sTablaRegistro varchar(100), @EsEntregaTotal bit 

	Set @sSql = ''	
	Set @iResultado = 0	
	Set @EsEntregaTotal = 0
	
	Select * Into #temp_vw_Productos_CodigoEAN From vw_Productos_CodigoEAN (Nolock)

	---------------   SE OBTIENE LOS DATOS DE LA RECEPCION DE LA ORDEN DE COMPRA   ------------------------------------
	Select P.ClaveSSA, P.DescripcionSal,
	GETDATE() AS FechaRecepcion_Inicial, GETDATE() AS FechaRecepcion_Final, 
	Cast(Sum(T.CantidadRecibida) as int) As Cantidad
	Into #tmpRegistroOC	
	From OrdenesDeComprasDet T (Nolock)
	Inner Join #temp_vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia 
	And T.FolioOrdenCompra In ( Select FolioOrdenCompra From OrdenesDeComprasEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								and FolioOrdenCompraReferencia = @FolioOrdenCompraReferencia )
	Group By P.ClaveSSA, P.DescripcionSal


	Update T Set T.FechaRecepcion_Inicial = ( Select MIN(FechaRegistro) From OrdenesDeComprasEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								and FolioOrdenCompraReferencia = @FolioOrdenCompraReferencia )
	From #tmpRegistroOC T

	Update T Set T.FechaRecepcion_Final = ( Select MAX(FechaRegistro) From OrdenesDeComprasEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								and FolioOrdenCompraReferencia = @FolioOrdenCompraReferencia )
	From #tmpRegistroOC T
	

-----------------------  SE OBTIENEN LOS DATOS DE LA ORDEN DE COMPRA   ------------------------------------------------

	Select T.Folio, T.FechaRegistro, T.IdProveedor, T.Proveedor, T.FechaRequeridaEntrega, T.FechaColocacion,
	T.ClaveSSA, T.DescripcionSal, Cast(Sum(T.Cantidad) as int) As Cantidad
	Into #tmpOrdenCompra
	From vw_Impresion_OrdenesCompras_CodigosEAN T (Nolock)	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.Folio = @FolioOrdenCompraReferencia
	Group By T.Folio, T.FechaRegistro, T.IdProveedor, T.Proveedor, T.FechaRequeridaEntrega, T.FechaColocacion,
	T.ClaveSSA, T.DescripcionSal
 
 
 
 
	----- SE INSERTA EL RESULTADO FINAL   ------------------------------------------------------------------------------
	Select T.Folio, Convert(varchar(10), T.FechaRegistro, 120) as FechaRegistro, T.IdProveedor, T.Proveedor, 
	Convert(varchar(10), T.FechaColocacion, 120) as FechaColocacion, 
	Convert(varchar(10), T.FechaRequeridaEntrega, 120) as FechaRequeridaEntrega, 
	Convert(varchar(10), R.FechaRecepcion_Inicial, 120) as FechaRecepcion_Inicial,
	Convert(varchar(10), R.FechaRecepcion_Final, 120) as FechaRecepcion_Final,
	T.ClaveSSA, T.DescripcionSal, T.Cantidad as Cantidad_Requerida, IsNull(R.Cantidad, 0) as Cantidad_Ingresada, 
	Cast(ABS( T.Cantidad - ( IsNull(R.Cantidad, 0)) ) as int ) as Cantidad_Faltante 
	Into #tmpValidarDiferencias
	From #tmpOrdenCompra T (Nolock)
	Inner Join #tmpRegistroOC R (Nolock)
		On ( T.ClaveSSA = R.ClaveSSA ) 
	
	If (Select SUM(Cantidad_Faltante) From #tmpValidarDiferencias (Nolock)) = 0
	Begin
		Set @EsEntregaTotal = 1
	End
	
	
	Select *, @EsEntregaTotal as EsEntregaTotal From #tmpValidarDiferencias (Nolock)	

End
Go--#SQL	