
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_CriteriosParaPedidos_Datos' And xType = 'P' )
	Drop Proc spp_COM_OCEN_CriteriosParaPedidos_Datos
Go--#SQL

Create Procedure spp_COM_OCEN_CriteriosParaPedidos_Datos ( @IdClaveSSA varchar(4), @CodigoEAN varchar(30), @TablaPublica varchar(50) )
As
Begin
	Declare
		@sSql varchar(8000),
		@sTabla varchar(20)

	-- Se Inicializan variables.
	Set @sSql = ''
	Set @sTabla = '#tmpCriterios'

	-- Se crea la tabla temporal a la conexion.
	Select top 0 IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal as DescripcionClave,  CodigoEAN, Descripcion as Producto, 
		space(4) as IdProveedor, space(100) as NombreProveedor, cast(0 as Float) as Precio,  cast(0 as Float) as PorcSurtimiento, 
		cast(0 as Float) as TiempoDeEntrega, 0 as Cant_A_Pedir    
	Into #tmpCriterios  
	From vw_Productos_CodigoEAN (NoLock) 
	Where 1 = 0
	
	-- Se llena la tabla temporal a la conexion
	Insert Into #tmpCriterios
	Select Distinct	C.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionSal, @CodigoEAN as CodigoEAN, C.Descripcion, L.IdProveedor, 
			P.Nombre, L.PrecioUnitario, 0.0 as PorcSurtimiento, 0.0 as TiempoDeEntrega, 0 as Cant_A_Pedir 
	From dbo.vw_Productos_CodigoEAN C(NoLock) 
	Inner Join COM_OCEN_ListaDePrecios L (NoLock) On ( C.IdClaveSSA_Sal = L.IdClaveSSA And C.CodigoEAN = L.CodigoEAN ) 
	Inner Join vw_Proveedores P(NoLock) On (L.IdProveedor = P.IdProveedor )
	Where C.IdClaveSSA_Sal = @IdClaveSSA And C.CodigoEAN = @CodigoEAN 

	--	-- Se llena la tabla temporal Publica
	Set @sSql = 
	'Insert Into ' + @TablaPublica + ' ' +
	'Select IdClaveSSA, ClaveSSA, DescripcionClave, CodigoEAN, Producto, IdProveedor, NombreProveedor, Precio, PorcSurtimiento, ' +
	'TiempoDeEntrega, Cant_A_Pedir ' +
	'From ' + @sTabla + '(NoLock) ' + 
	'Where Not Exists ( Select * From ' + @TablaPublica + 
		' Where IdClaveSSA = ' + Char(39) + @IdClaveSSA + Char(39) + ' And CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' )'
	Exec (@sSql)

	-- Se limpia la tabla temporal a la conexion y se llena con la informacion de la tabla temporal Publica.
	Delete From #tmpCriterios 
	Set @sSql = 
	'Insert Into ' + @sTabla + ' ' +
	'Select IdClaveSSA, ClaveSSA, DescripcionClave, CodigoEAN, Producto, IdProveedor, NombreProveedor, Precio, PorcSurtimiento, ' +
	'TiempoDeEntrega, Cant_A_Pedir ' +
	'From ' + @TablaPublica + '(NoLock) ' + 
	' Where IdClaveSSA = ' + Char(39) + @IdClaveSSA + Char(39) + ' And CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' '
	Exec (@sSql)
	

	Select	IdProveedor, NombreProveedor, Precio, PorcSurtimiento, TiempoDeEntrega, Cant_A_Pedir, 
			IdClaveSSA, ClaveSSA, DescripcionClave, CodigoEAN, Producto
	From #tmpCriterios(NoLock)

End
Go--#SQL

