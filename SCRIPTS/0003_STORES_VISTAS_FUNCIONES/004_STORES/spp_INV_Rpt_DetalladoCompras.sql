
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_INV_Rpt_DetalladoCompras' And xType = 'P' ) 
	Drop Proc spp_INV_Rpt_DetalladoCompras 
Go--#SQL 
 
Create Proc spp_INV_Rpt_DetalladoCompras
(  @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0002', 
   -- @Anio int = 2010, @Mes int = 01, 
   @FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-10-01', 
   @TipoInsumo tinyint = 0, @TipoDispensacion tinyint = 0, @ClaveLote varchar(30) = ''
) 
With Encryption
As
Begin
Set Dateformat YMD 
Set NoCount On 
Declare 
	@iMedicamento tinyint, 
	@iMatCuracion tinyint, 
	@iDispVenta tinyint, 
	@iDispConsignacion tinyint


	--Se verifica el Tipo de Insumo
	Set @iMedicamento = 1 
	Set @iMatCuracion = 2 
	
	if @TipoInsumo = 1 
	   Set @iMatCuracion = 1
	
	if @TipoInsumo = 2 
	   Set @iMedicamento = 2 

	--Se verifica el Tipo de Dispensacion	   
	Set @iDispConsignacion = 1 
	Set @iDispVenta = 2 

	If @TipoDispensacion = 1 
	   Set @iDispVenta = 1 	

	If @TipoDispensacion = 2 
	   Set @iDispConsignacion = 2  	

	   
	-- Se obtienen los datos 
	Select * Into #tmpDatos
	From 
	(
		Select 
			   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, E.Folio, 
			   E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
			   E.IdProveedor, E.Proveedor, E.ReferenciaDocto, E.FechaDocto, E.FechaVenceDocto, E.Status,
			   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
			   D.IdProducto, ( Case When vP.EsControlado = 1 Then 'SI' ELSE 'NO' End ) as EsControlado, 
				vP.Descripcion as DescripcionProducto, D.CodigoEAN, 
				(Case When vP.IdTipoProducto = '02' Then 1 Else 2 End) as TipoInsumo, 
				(Case When vP.IdTipoProducto = '02' Then 'MEDICAMENTO' Else 'MATERIAL DE CURACION' End) as TipoDeInsumo, 
			   L.ClaveLote, 
				(Case When L.ClaveLote Like '%*%' Then 1 Else 2 End) as EsConsignacion, 
				(Case When L.ClaveLote Like '%*%' Then 'CONSIGNACION' Else 'VENTA' End) as EsConsignacionDesc, 
			   L.CantidadRecibida as CantidadLote, 
			   F.FechaCaducidad, F.FechaRegistro
		From vw_ComprasEnc E (NoLock) 
		Inner Join ComprasDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioCompra ) 
		Inner Join ComprasDet_Lotes L (NoLock) 
			On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioCompra = L.FolioCompra 
				 and D.IdProducto = L.IdProducto and D.CodigoEAN = D.CodigoEAN )  
		Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
			On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia 
				 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
		Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto ) 
		Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia  
			-- And DatePart(year, E.FechaRegistro) = @Anio And DatePart(month, E.FechaRegistro ) = @Mes 
			and convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
			and vP.EsControlado = 1 
		-- Order By E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Folio, D.IdProducto
	) as T
	Where TipoInsumo In ( @iMedicamento, @iMatCuracion ) 
	And EsConsignacion In ( @iDispConsignacion, @iDispVenta )
	Order By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.Folio, T.IdProducto

	If @ClaveLote <> '' 
	  Begin
		Select * From #tmpDatos(NoLock) Where ClaveLote = @ClaveLote Order By IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto
	  End
	Else
	  Begin
		Select * From #tmpDatos(NoLock) Order By IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto
	  End

End
Go--#SQL
