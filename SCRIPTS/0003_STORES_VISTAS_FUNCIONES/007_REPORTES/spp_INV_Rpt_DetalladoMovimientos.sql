
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_INV_Rpt_DetalladoMovimientos' And xType = 'P' ) 
	Drop Proc spp_INV_Rpt_DetalladoMovimientos
Go--#SQL

Create Proc spp_INV_Rpt_DetalladoMovimientos
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0002', 
    @FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-10-01', 	
	-- @Anio int = 2009, @Mes int = 12, 
	@TipoMovto varchar(3) = 'II', @TipoInsumo tinyint = 0, @TipoDispensacion tinyint = 0, @ClaveLote varchar(30) = '' 
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
		Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, M.TipoMovto, 
			(Case When M.Efecto = 'E' Then 'ENTRADA' Else 'SALIDA' End ) as Efecto,
			P.ClaveSSA, P.DescripcionClave, D.IdProducto, ( Case When P.EsControlado = 1 Then 'SI' ELSE 'NO' End ) as EsControlado, 
			D.CodigoEAN, P.Descripcion as DescProducto, 
			(Case When P.IdTipoProducto = '02' Then 1 Else 2 End) as TipoInsumo, 
			(Case When P.IdTipoProducto = '02' Then 'MEDICAMENTO' Else 'MATERIAL DE CURACION' End) as TipoDeInsumo, 
			P.Presentacion, D.ClaveLote, 
			(Case When L.ClaveLote Like '%*%' Then 1 Else 2 End) as EsConsignacion, 
			(Case When L.ClaveLote Like '%*%' Then 'CONSIGNACION' Else 'VENTA' End) as EsConsignacionDesc, 
			L.FechaCaducidad,  D.Cantidad
		From vw_MovtosInv_Enc M (NoLock) 
		Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioMovtoInv ) 
		Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
			On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and 
				 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
		Inner Join vw_Productos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
		Where M.IdEmpresa = @IdEmpresa And M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And M.TipoMovto = @TipoMovto
		-- And DatePart( year, M.FechaReg ) = @Anio And DatePart( month, M.FechaReg ) = @Mes 
		and convert(varchar(10), M.FechaReg, 120) Between @FechaInicial and @FechaFinal 
		and P.EsControlado = 1 
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

