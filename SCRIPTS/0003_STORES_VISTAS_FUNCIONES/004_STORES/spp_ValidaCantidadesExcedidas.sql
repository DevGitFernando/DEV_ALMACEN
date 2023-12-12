If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_ValidaCantidadesExcedidas' and xType = 'P')
    Drop Proc spp_ValidaCantidadesExcedidas
Go--#SQL

--		Exec spp_ValidaCantidadesExcedidas '001', '21', '0001', '00000076'
  
Create Proc spp_ValidaCantidadesExcedidas 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioOrdenCompraReferencia varchar(8), @Tabla varchar(100)
)
With Encryption 
As
Begin
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500), @sTablaRegistro varchar(100), @Registros Int 

	Set @sSql = ''	
	Set @iResultado = 0	


	Select 
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, P.DescripcionSal, T.IdProducto, T.CodigoEAN, P.Descripcion, Cast(T.CantidadRecibida as Int) As Cantidad
	Into #tmpCaptura	
	From OrdenesDeComprasDet T (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where 1 = 0
	

	Select 
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, T.IdProducto, T.CodigoEAN, P.Descripcion, Cast(Sum(T.CantidadRecibida) as int) As Cantidad
	Into #tmpRegistroOC	
	From OrdenesDeComprasDet T (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia 
	And T.FolioOrdenCompra In 
		( 
			Select FolioOrdenCompra 
			From OrdenesDeComprasEnc M (Nolock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
				  And FolioOrdenCompraReferencia = @FolioOrdenCompraReferencia 
				  and convert(varchar(10), M.FechaRegistro, 120) >= '2017-01-01' 
		) 
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, T.IdProducto, T.CodigoEAN, P.Descripcion
	


	Set @sSql = ' Insert #tmpCaptura (IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, Descripcion, Cantidad ) ' +
				' Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, P.DescripcionSal, T.IdProducto, T.CodigoEAN, P.Descripcion, Sum(T.Cantidad) As Cantidad ' +				
				' From ' + @Tabla + '  T (Nolock) ' +
				' Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )' +	
				' Where T.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' And T.IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' +  
				' And T.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' ' +
				' Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, P.DescripcionSal, T.IdProducto, T.CodigoEAN, P.Descripcion '
	Exec(@sSql)	

	
----	Insert Into #tmpRegistroOC (IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, Descripcion, Cantidad )
----	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.IdProducto, T.CodigoEAN, T.Descripcion, T.Cantidad
----	From #tmpCaptura T (Nolock)


	Select 
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, Sum(T.Cantidad) As Cantidad, T.CodigoEAN_Bloqueado
	Into #tmpOrdenCompra
	From vw_OrdenesCompras_CodigosEAN_Det T (Nolock)	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado 
		And T.EntregarEn = @IdFarmacia 
		And T.Folio = @FolioOrdenCompraReferencia
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.CodigoEAN_Bloqueado


	Select 
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, Sum(T.Cantidad) As Cantidad
	Into #tmpOrdenCompraTotal
	From #tmpRegistroOC T (Nolock) 
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA 
	
----Select * From #tmpCaptura
----	Select * From #tmpOrdenCompra 
----	Select * From #tmpOrdenCompraTotal 


	Select 
		T.ClaveSSA, OC.IdProducto, OC.CodigoEAN, 'Descripción' =  OC.Descripcion,
		'Cantidad Requerida' = T.Cantidad , 'Cantidad Ingresada' = IsNull(C.Cantidad, 0),
		'Ingreso Actual' = OC.Cantidad, 'Cantidad Excedente' = ABS((IsNull(C.Cantidad, 0) + OC.Cantidad ) - T.Cantidad ),
		'Clave bloqueada para recepción' = (Case When T.CodigoEAN_Bloqueado = 1 then 'Bloqueada por compras' Else 'NO' End)
	Into #tmpValidarCantidades
	From #tmpCaptura OC (Nolock)
	Inner Join #tmpOrdenCompra T  (Nolock) On ( T.ClaveSSA = OC.ClaveSSA )
	Left Join #tmpOrdenCompraTotal C (Nolock) On ( OC.ClaveSSA = C.ClaveSSA )	
	Where (IsNull(C.Cantidad, 0) + OC.Cantidad ) > T.Cantidad or  T.CodigoEAN_Bloqueado  = 1
	-- Group By T.ClaveSSA, OC.IdProducto, OC.CodigoEAN, OC.Descripcion
	
	Select @Registros =  IsNull(COUNT(*), 0) From #tmpValidarCantidades (Nolock)	
	
	If (@Registros > 0)
		Begin
			Select * From #tmpValidarCantidades (Nolock)
		End
	Else
		Begin
			Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.DescripcionSal, Sum(T.Cantidad) As Cantidad
			Into #tmpCaptura_Resumen
			From #tmpCaptura T
			Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.DescripcionSal		
		
		
			Select CA.ClaveSSA, CA.DescripcionSal, 'Cantidad Requerida' = T.Cantidad, 'Ingresos Previos' = IsNull(C.Cantidad, 0),
				'Ingreso Actual' = CA.Cantidad, 'Cantidad Excedente' = ABS((IsNull(C.Cantidad, 0) + CA.Cantidad ) - T.Cantidad )
			From #tmpCaptura_Resumen CA
			Inner Join #tmpOrdenCompra T  (Nolock) On ( T.ClaveSSA = CA.ClaveSSA )
			Left Join #tmpOrdenCompraTotal C (Nolock) On ( CA.ClaveSSA = C.ClaveSSA )	
			Where (IsNull(C.Cantidad, 0) + CA.Cantidad ) > T.Cantidad
		End

End
Go--#SQL	


