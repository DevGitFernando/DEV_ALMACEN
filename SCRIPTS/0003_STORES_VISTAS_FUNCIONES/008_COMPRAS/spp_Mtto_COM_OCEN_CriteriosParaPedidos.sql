
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_CriteriosParaPedidos' And xType = 'P' )
	Drop Proc spp_Mtto_COM_OCEN_CriteriosParaPedidos
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_CriteriosParaPedidos 
( @IdClaveSSA varchar(4), @CodigoEAN varchar(30), @IdProveedor varchar(8), @Cantidad int, @TablaPublica varchar(50), 
	@FolioPedido varchar(6) )
As
Begin
	Declare
		@sSql varchar(8000),
		@sTabla varchar(20)

	-- Se Inicializan variables.
	Set @sSql = ''

	Set @FolioPedido = right(replicate('0', 6) + @FolioPedido, 6)

	-- Se llena la tabla temporal
	Set @sSql = 
	'Update ' + @TablaPublica + ' ' +
	'Set Cant_A_Pedir = ' + Cast( @Cantidad as varchar ) + ' ' +
	'From ' + @TablaPublica + ' (NoLock) ' + 
	'Where IdClaveSSA = ' + Char(39) + @IdClaveSSA + Char(39) + ' And CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' ' + 
	'And IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' And FolioPedido = ' + Char(39) + @FolioPedido + Char(39) + ' '
	
	Exec (@sSql)

	Set @sSql = ''

	Set @sSql =
	'Update P ' +
	'Set P.PrecioMin = T.PrecioMin, P.PrecioMax = T.PrecioMax, P.ObservacionesPrecios = T.ObservacionesPrecios, P.EsSobrePrecio = T.EsSobrePrecio ' +
	'From ' + @TablaPublica + ' P (NoLock) ' +
	'Inner Join tmpObservacionesPrecios T (Nolock) On (P.IdClaveSSA = T.IdClaveSSA And P.CodigoEAN = T.CodigoEAN And P.IdProveedor = T.IdProveedor) ' +  
	'Where P.IdClaveSSA = ' + Char(39) + @IdClaveSSA + Char(39) + ' And P.CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' ' + 
	'And P.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' And P.FolioPedido = ' + Char(39) + @FolioPedido + Char(39) + ' '
	
	Exec (@sSql)	

End
Go--#SQL



