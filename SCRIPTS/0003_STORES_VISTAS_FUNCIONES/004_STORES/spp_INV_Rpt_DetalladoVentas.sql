
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_INV_Rpt_DetalladoVentas' And xType = 'P' ) 
	Drop Proc spp_INV_Rpt_DetalladoVentas
Go--#SQL

Create Proc spp_INV_Rpt_DetalladoVentas
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0010', 
    @FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-10-01', 	
	-- @Anio int = 2010, @Mes int = 01, 
	@TipoInsumo tinyint = 0, @TipoDispensacion tinyint = 0, @ClaveLote varchar(30) = '' )
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
		Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio,
		E.IdPersonal, E.NombrePersonal, E.FechaRegistro, E.NombreTipoDeVenta,
		P.ClaveSSA, P.DescripcionSal, L.IdProducto,
			(Case When P.EsControlado = 1 Then 'SI' Else 'NO' End ) As EsControlado, 
		P.Descripcion, L.CodigoEAN,
			(Case When P.IdTipoProducto = '02' Then 1 Else 2 End) as TipoInsumo, 
			(Case When P.IdTipoProducto = '02' Then 'MEDICAMENTO' Else 'MATERIAL DE CURACION' End) as TipoDeInsumo, 
		L.ClaveLote,
			(Case When L.ClaveLote Like '%*%' Then 1 Else 2 End) as EsConsignacion, 
			(Case When L.ClaveLote Like '%*%' Then 'CONSIGNACION' Else 'VENTA' End) as EsConsignacionDesc, 
		L.CantidadVendida
		From vw_VentasEnc E (Nolock)
		INNER JOIN VentasDet_Lotes L (Nolock)
			ON ( E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.Folio = L.FolioVenta )
		INNER JOIN vw_Productos P (Nolock)
			ON ( L.IdProducto = P.IdProducto )
		Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia 
		-- And DatePart( yy, E.FechaRegistro ) = @Anio And DatePart( mm, E.FechaRegistro ) = @Mes 
		and convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
		And P.EsControlado = 1
		--And L.CantidadVendida > 0
	) as T
	Where TipoInsumo In ( @iMedicamento, @iMatCuracion ) 
	And EsConsignacion In ( @iDispConsignacion, @iDispVenta )
	Order By T.Folio

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
