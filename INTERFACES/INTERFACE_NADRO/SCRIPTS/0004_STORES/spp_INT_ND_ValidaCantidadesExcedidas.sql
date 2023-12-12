If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_ValidaCantidadesExcedidas' and xType = 'P')
    Drop Proc spp_INT_ND_ValidaCantidadesExcedidas
Go--#SQL

--		Exec spp_INT_ND_ValidaCantidadesExcedidas '003', '16', '0011', '2671063/01', 'tmpCantidadesOC_00C2C619AAAB_20140908_122259' 
  
Create Proc spp_INT_ND_ValidaCantidadesExcedidas 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @ReferenciaFolioPedido varchar(50), @Tabla varchar(100)
)
With Encryption 
As
Begin
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500), @sTablaRegistro varchar(100) 

	Set @sSql = ''	
	Set @iResultado = 0	

 
	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, T.IdProducto, T.CodigoEAN, P.Descripcion, Cast(T.CantidadRecibida as Int) As Cantidad
	Into #tmpCaptura	
	From INT_ND_PedidosDet T (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where 1 = 0
	
	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, T.IdProducto, T.CodigoEAN, P.Descripcion, Cast(Sum(T.CantidadRecibida) as int) As Cantidad
	Into #tmpRegistroOC	
	From INT_ND_PedidosDet T (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia 
	And T.FolioPedido In ( Select FolioPedido From INT_ND_PedidosEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								And ReferenciaFolioPedido = @ReferenciaFolioPedido ) 
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, T.IdProducto, T.CodigoEAN, P.Descripcion 
	
	
	Set @sSql = ' Insert #tmpCaptura (IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, Descripcion, Cantidad ) ' +
				' Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, T.IdProducto, T.CodigoEAN, P.Descripcion, Sum(T.Cantidad) As Cantidad ' +				
				' From ' + @Tabla + '  T (Nolock) ' +
				' Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )' +	
				' Where T.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' And T.IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' +  
				' And T.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' ' +
				' Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, T.IdProducto, T.CodigoEAN, P.Descripcion '
	Exec(@sSql)	
	
  	
--  	Select * from #tmpCaptura 
 	
  	
----	Insert Into #tmpRegistroOC (IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, Descripcion, Cantidad )
----	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.IdProducto, T.CodigoEAN, T.Descripcion, T.Cantidad
----	From #tmpCaptura T (Nolock)


	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, Sum(T.Cantidad) As Cantidad
	Into #tmpOrdenCompra 
	From vw_INT_ND_PedidosUnidades T (Nolock)	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.ReferenciaFolioPedido = @ReferenciaFolioPedido
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA 


	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, Sum(T.Cantidad) As Cantidad
	Into #tmpOrdenCompraTotal
	From #tmpRegistroOC T (Nolock) 
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA 	
 	
----	Select * From #tmpCaptura
----	Select * From #tmpOrdenCompra 
----	Select * From #tmpOrdenCompraTotal 


	Select T.ClaveSSA, 'Cantidad Requerida' = T.Cantidad , 'Cantidad Ingresada' = IsNull(C.Cantidad, 0), 
	OC.IdProducto, OC.CodigoEAN, 'Descripción' =  OC.Descripcion,
	'Ingreso Actual' = OC.Cantidad, 'Cantidad Excedente' = ABS((IsNull(C.Cantidad, 0) + OC.Cantidad ) - T.Cantidad )
	Into #tmpValidarCantidades
	From #tmpCaptura OC (Nolock)
	Inner Join #tmpOrdenCompra T  (Nolock) On ( T.ClaveSSA = OC.ClaveSSA )
	Left Join #tmpOrdenCompraTotal C (Nolock) On ( OC.ClaveSSA = C.ClaveSSA )	
	Where (IsNull(C.Cantidad, 0) + OC.Cantidad ) > T.Cantidad 	
	-- Group By T.ClaveSSA, OC.IdProducto, OC.CodigoEAN, OC.Descripcion
	 
	
--------------- SALIDA FINAL	
	Select * From #tmpValidarCantidades (Nolock)  
	

End 

Go--#SQL	


