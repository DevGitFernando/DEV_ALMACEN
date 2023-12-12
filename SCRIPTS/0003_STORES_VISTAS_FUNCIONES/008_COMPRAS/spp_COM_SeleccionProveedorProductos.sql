

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_SeleccionProveedorProductos' And xType = 'P' )
	Drop Proc spp_COM_SeleccionProveedorProductos
Go--#SQL

Create Procedure spp_COM_SeleccionProveedorProductos ( @IdClaveSSA varchar(4), @TablaPublica varchar(50), @FolioPedido varchar(6) )
As
Begin
	Declare
		@sSql varchar(8000),
		@sTabla varchar(20),
		@PrecioMin numeric(14,4),
		@PrecioMax numeric(14,4)

	-- Se Inicializan variables.
	Set @sSql = ''
	Set @sTabla = '#tmpCriterios'

	Set @PrecioMin = 0
	Set @PrecioMax = 0
	
	Set @FolioPedido = right(replicate('0', 6) + @FolioPedido, 6)
	

	-- Se crea la tabla temporal a la conexion.
	Select top 0 Cast( '' as varchar(6) ) As FolioPedido, 
	IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal as DescripcionClave,  CodigoEAN, Descripcion as Producto, 
		space(4) as IdProveedor, space(100) as NombreProveedor, cast(0 as Numeric(14,4)) as Precio, 0 as Cant_A_Pedir,
	cast(0 as Numeric(14,4)) as PrecioMin, cast(0 as Numeric(14,4)) as PrecioMax, space(100) As ObservacionesPrecios, 0 As EsSobrePrecio   
	Into #tmpCriterios  
	From vw_Productos_CodigoEAN (NoLock) 
	Where 1 = 0
	
	-- Se llena la tabla temporal a la conexion
	Insert Into #tmpCriterios
	Select Distinct @FolioPedido As FolioPedido, C.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionSal, C.CodigoEAN, C.Descripcion, L.IdProveedor, 
			P.Nombre, L.PrecioUnitario, 0 as Cant_A_Pedir, cast(0 as Numeric(14,4)) as PrecioMin, 
	cast(0 as Numeric(14,4)) as PrecioMax, space(100) As ObservacionesPrecios, 0 As EsSobrePrecio 
	From dbo.vw_Productos_CodigoEAN C(NoLock) 
	Inner Join COM_OCEN_ListaDePrecios L (NoLock) On ( C.IdClaveSSA_Sal = L.IdClaveSSA And C.CodigoEAN = L.CodigoEAN ) 
	Inner Join vw_Proveedores P(NoLock) On (L.IdProveedor = P.IdProveedor )
	Where C.IdClaveSSA_Sal = @IdClaveSSA -- And C.CodigoEAN = @CodigoEAN 

	--	-- Se llena la tabla temporal Publica
	Set @sSql = 
	'Insert Into ' + @TablaPublica + ' ' +
	'Select ' + Char(39) + @FolioPedido + Char(39) + ' As FolioPedido, IdClaveSSA, ClaveSSA, DescripcionClave, CodigoEAN, Producto, IdProveedor, NombreProveedor, Precio, Cant_A_Pedir, ' +
	' PrecioMin, PrecioMax, ObservacionesPrecios, EsSobrePrecio ' +
	'From ' + @sTabla + '(NoLock) ' + 
	'Where Not Exists ( Select * From ' + @TablaPublica + 
		' Where IdClaveSSA = ' + Char(39) + @IdClaveSSA + Char(39) + ' )' -- ' And CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' )'
	Exec (@sSql)

	--Print (@sSql)

	-- Se limpia la tabla temporal a la conexion y se llena con la informacion de la tabla temporal Publica.
	Delete From #tmpCriterios 
	Set @sSql = 
	'Insert Into ' + @sTabla + ' ' +
	'Select FolioPedido, IdClaveSSA, ClaveSSA, DescripcionClave, CodigoEAN, Producto, IdProveedor, NombreProveedor, Precio, Cant_A_Pedir,  ' +
	'PrecioMin, PrecioMax, ObservacionesPrecios, EsSobrePrecio ' +
	'From ' + @TablaPublica + '(NoLock) ' + 
	' Where IdClaveSSA = ' + Char(39) + @IdClaveSSA + Char(39) + ' ' -- ' And CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' '
	Exec (@sSql)
	
	Set @PrecioMin = ( Select Min(Precio) From #tmpCriterios(NoLock) )

	Set @PrecioMax = ( Select Max(Precio) From #tmpCriterios(NoLock) )

	Select	T.IdProveedor, T.NombreProveedor, T.CodigoEAN, T.Producto, 
			P.Presentacion, P.ContenidoPaquete, T.Precio, T.Cant_A_Pedir, 
			0.0000 As Importe, @PrecioMin As PrecioMin, @PrecioMax As PrecioMax,   
			T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, T.CodigoEAN, T.Producto 
	From #tmpCriterios T (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock)
		On ( T.CodigoEAN = P.CodigoEAN )
	Order By T.Precio
	

End
Go--#SQL

