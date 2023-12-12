
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Consulta_Ventas_General_Farmacia' And xType = 'P' ) 
	Drop Proc spp_Rpt_Consulta_Ventas_General_Farmacia
Go--#SQL

Create Procedure spp_Rpt_Consulta_Ventas_General_Farmacia ( @FechaInicial varchar(10), @FechaFinal varchar(10), @EsDeConsignacion varchar(1) )
With Encryption
As
Begin
	/*	@EsDeConsignacion 
		0 = TODOS.
		1 = Venta.
		2 = Consignacion.
	*/
	
	-- Se insertan los datos en la tabla Temporal. NOTA: Se usan las tablas directamente ya que las vistas hacen lento la obtencion de datos.
	Select	V.IdEstado, Cast( '' as varchar(50) ) as Estado, V.IdFarmacia, Cast( '' as varchar(50) ) as Farmacia, 
			V.IdCliente, V.IdSubCliente, L.CantidadVendida as Cantidad, 
			P.IdClaveSSA_Sal, L.ClaveLote, Cast( 0 as Numeric(14,4) ) as Precio, Cast( 0 as Numeric(14,4) ) as Importe
	Into #tmpCodigos
	From VentasEnc V(NoLock)
	Inner Join VentasDet_Lotes L(NoLock) On ( V.IdEmpresa = L.IdEmpresa And V.IdEstado = L.IdEstado And V.IdFarmacia = L.IdFarmacia And V.FolioVenta = L.FolioVenta ) 
	Inner Join CatProductos P(NoLock) On ( L.IdProducto = P.IdProducto ) 
	Where V.TipoDeVenta = '2' And Convert( varchar(10), V.FechaSistema, 120 ) Between @FechaInicial And @FechaFinal 

	-- Se filtran las claves segun la opcion de Consignacion
	If @EsDeConsignacion = '1'
	  Begin
		Delete From #tmpCodigos Where ClaveLote Like '%*%'
	  End
	Else If @EsDeConsignacion = '2'
	  Begin
		Delete From #tmpCodigos Where ClaveLote Not Like '%*%'
	  End
		
	-- Se Reemplazan las Claves que no tienen Precio por una Clave Valida.
	Update V
	Set IdClaveSSA_Sal = C.IdClaveSSA
	From #tmpCodigos V (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( V.IdEstado = C.IdEstado and V.IdClaveSSA_Sal = C.IdClaveSSA_Relacionada And C.Status = 'A' ) 	

	-- Se obtiene el Precio	de las Claves SSA y el Importe
	Update V
	Set Precio = C.Precio, Importe = ( Cantidad * C.Precio )
	From #tmpCodigos V(NoLock)
	Inner Join CFG_ClavesSSA_Precios C(Nolock) On ( V.IdEstado = C.IdEstado And V.IdCliente = C.IdCliente And V.IdSubCliente = C.IdSubCliente And V.IdClaveSSA_Sal = C.IdClaveSSA_Sal )
	
	-- Se obtiene el nombre de los Estados y Farmacias
	Update V
	Set Estado = C.Estado, Farmacia = C.Farmacia
	From #tmpCodigos V(NoLock)
	Inner Join vw_Farmacias C(Nolock) On ( V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia)

	-- Se insertan los datos en la tabla temporal de las ventas
	Select IdEstado, IdFarmacia, Farmacia, Sum( Importe ) as Total
	Into #tmpVentas
	From #tmpCodigos(NoLock)
	Group by IdEstado, IdFarmacia, Farmacia  
	Order by IdFarmacia 

	-- Se devuelven los datos
	Select * From #tmpVentas(NoLock) Order By IdFarmacia
End
Go--#SQL
