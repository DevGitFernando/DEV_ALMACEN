
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_VerificarDiferenciasOrdenCompra' and xType = 'P')
    Drop Proc spp_VerificarDiferenciasOrdenCompra
Go--#SQL

--		Exec spp_VerificarDiferenciasOrdenCompra '002', '14', '0004', '0003', '00000078'
  
Create Proc spp_VerificarDiferenciasOrdenCompra 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', 
	--@IdFarmaciaGenera varchar(4) = '', 
	@IdFarmacia varchar(4) = '', @FolioOrden varchar(8) = ''
)  
With Encryption 
As
Begin 
Set DateFormat YMD 
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500), @sTablaRegistro varchar(100) 

	Set @sSql = ''	
	Set @iResultado = 0	


	---------------------------------------------------------------------------------  
	Select P.ClaveSSA, P.DescripcionSal, Cast(Sum(T.CantidadRecibida) as int) As Cantidad
	Into #tmpRegistroOC	
	From OrdenesDeComprasDet T (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia 
	And T.FolioOrdenCompra In ( Select FolioOrdenCompra From OrdenesDeComprasEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								And FolioOrdenCompraReferencia = @FolioOrden )
	Group By P.ClaveSSA, P.DescripcionSal


	Select 
		T.ClaveSSA, T.DescripcionSal, Cast(Sum(T.Cantidad) as int) As Cantidad
	Into #tmpOrdenCompra
	From vw_OrdenesCompras_CodigosEAN_Det T (Nolock)	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.Folio = @FolioOrden
	Group By T.ClaveSSA, T.DescripcionSal
 

	Select 
		T.ClaveSSA, 'Descripción' =  T.DescripcionSal, 'Cantidad Requerida' = T.Cantidad, 'Cantidad Ingresada' = IsNull(R.Cantidad, 0), 
		'Cantidad Faltante' = Cast(ABS( T.Cantidad - ( IsNull(R.Cantidad, 0)) ) as int ) 
	Into #tmpValidarDiferencias
	From #tmpOrdenCompra T (Nolock)
	Inner Join #tmpRegistroOC R (Nolock) On ( T.ClaveSSA = R.ClaveSSA ) 
	---------------------------------------------------------------------------------  	


	------------------------------------------------------ SALIDA FINAL  
	
	Select * From #tmpValidarDiferencias (Nolock)

	Select 'Folio Orden' = D.FolioOrdenCompra, 'Clave' = P.ClaveSSA, 'Producto' = D.IdProducto, 'Codigo EAN' = D.CodigoEAN, 'Descripción' = P.Descripcion,
	'Cantidad Ingresada' = D.CantidadRecibida 
	From OrdenesDeComprasDet D (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) 
		On ( D.IdProducto = P.IdProducto And D.CodigoEAN = P.CodigoEAN )
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia 
	And D.FolioOrdenCompra In ( Select FolioOrdenCompra From OrdenesDeComprasEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								And FolioOrdenCompraReferencia = @FolioOrden )
	Order By D.FolioOrdenCompra 

	------------------------------------------------------ SALIDA FINAL   


End 
Go--#SQL	

/* 
	From OrdenesDeComprasEnc M (NoLock) 
	Inner Join COM_OCEN_OrdenesCompra_Claves_Enc OC (NoLock) 
		On ( 
				M.IdEmpresa = OC.FacturarA and M.IdEstado = OC.EstadoEntrega and M.IdFarmacia = OC.EntregarEn and M.FolioOrdenCompraReferencia = OC.FolioOrden 
		   )
*/ 

