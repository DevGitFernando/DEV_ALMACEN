
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_ADMI_Vales_Estadisticas' And xType = 'P' )
	Drop Proc spp_ADMI_Vales_Estadisticas
Go--#SQL


Create Procedure spp_ADMI_Vales_Estadisticas ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@FechaInicial varchar(10) = '2012-01-01', @FechaFinal varchar(10) = '2012-01-31', 
	@iMostrarResultado smallint = 0 )
With Encryption
As 
Begin
	Declare 
		@iAño int,
		@iMes int

	Set DateFormat YMD
	Set NoCount On

	Set @iAño = DatePart( year, @FechaFinal )
	Set @iMes = DatePart( month, @FechaFinal )
	
	------------------------------------------------------------
	-- Se obtienen las cantidades de las Claves de los Vales  --
	------------------------------------------------------------
	Select	E.IdEmpresa, Cast( '' as varchar(100) ) as Empresa, E.IdEstado, Cast( '' as varchar(50) ) as Estado, 
			E.IdFarmacia, Cast( '' as varchar(100) ) as Farmacia, E.FolioVale, IsNull( R.IdProveedor, '') as IdProveedor,
			D.IdClaveSSA_Sal, Cast( '' as varchar(50) ) as ClaveSSA, Cast( '' as varchar(7500) ) as DescripcionSal, 
			D.Cantidad, 
			GetDate() as FechaImpresion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, E.Status
	Into #tmpCantidades
	From Vales_EmisionEnc E(NoLock) 
	Inner Join Vales_EmisionDet D(NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVale = D.FolioVale )
	Left Join ValesEnc R(NoLock) On ( E.IdEmpresa = R.IdEmpresa And E.IdEstado = R.IdEstado And E.IdFarmacia = R.IdFarmacia And E.FolioVale = R.FolioVale )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado 
		And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Order By E.IdFarmacia, D.IdClaveSSA_Sal

	---------------------------------------------------------------------
	-- Se obtienen los nombres de Empresa, Estado, Farmacia y ClaveSSA --
	---------------------------------------------------------------------
	Update C
	Set Empresa = I.Nombre
	From #tmpCantidades C 
	Inner Join CatEmpresas I(NoLock) On ( C.IdEmpresa = I.IdEmpresa ) 

	Update C
	Set Estado = I.Nombre
	From #tmpCantidades C 
	Inner Join CatEstados I(NoLock) On ( C.IdEstado = I.IdEstado ) 

	Update C
	Set Farmacia = I.NombreFarmacia
	From #tmpCantidades C 
	Inner Join CatFarmacias I(NoLock) On ( C.IdEstado = I.IdEstado And C.IdFarmacia = I.IdFarmacia) 

	Update C
	Set ClaveSSA = I.ClaveSSA, DescripcionSal = I.DescripcionSal
	From #tmpCantidades C 
	Inner Join vw_ClavesSSA_Sales I(NoLock) On ( C.IdClaveSSA_Sal = I.IdClaveSSA_Sal ) 

	----------------------------------------------------
	-- Se obtiene la cantidad registrada de cada Vale --
	---------------------------------------------------- 
	Select	E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Folio, E.FolioVale, E.SubTotal, E.Iva, E.Total, E.IdProveedor,
			D.IdProducto, D.CodigoEAN, D.CantidadRecibida as Cantidad, 
			D.CostoUnitario, D.SubTotal as SubTotal_Producto, D.ImpteIva as Iva_Producto, D.Importe as Importe_Producto, P.ClaveSSA 
	Into #tmpRegistrados
	From ValesEnc E(NoLock)
	Inner Join ValesDet D(NoLock) On( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio )
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( D.IdProducto = P.IdProducto And D.CodigoEAN = P.CodigoEAN )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado 
		And Cast( E.IdEmpresa + E.IdEstado + E.IdFarmacia + E.FolioVale as varchar ) In
		( Select Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) as Folio From #tmpCantidades ) 
		--And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Order By E.IdFarmacia, P.DescripcionSal

	-------------------------------------------------------------
	-- Se crea la tabla del Concentrado de Claves de los Vales --
	-------------------------------------------------------------
	Select	@iAño as Año, @iMes as Mes, IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProveedor, 
			0 as ValesEmitidos_Mes, 0 as ValesRegistrados_Mes, 0 as PiezasEmitidas_Mes, 0 as PiezasRegistradas_Mes, 
			Cast( 0.0000 as Numeric(14,4) ) as ImporteRegistrado_Mes,
			0 as ValesEmitidos_Farmacia, 0 as ValesRegistrados_Farmacia, 0 as PiezasEmitidas_Farmacia, 0 as PiezasRegistradas_Farmacia,
			Cast( 0.0000 as Numeric(14,4) ) as ImporteRegistrado_Farmacia,
			0 as ValesEmitidos_Clave, 0 as ValesRegistrados_Clave, 0 as PiezasEmitidas_Clave, 0 as PiezasRegistradas_Clave, 
			Cast( 0.0000 as Numeric(14,4) ) as ImporteRegistrado_Clave,
			Cast( 0.0000 as Numeric(14,4) ) as PrecioLicitacion_Clave, Cast( 0.0000 as Numeric(14,4) ) as CostoMinimo_Clave , 
			Cast( 0.0000 as Numeric(14,4) ) as CostoMaximo_Clave, 
			0 as ValesRegistrados_Proveedor, 0 as PiezasRegistradas_Proveedor, Cast( 0.0000 as Numeric(14,4) ) as ImporteRegistrado_Proveedor
	Into #tmpConcentrado
	From #tmpCantidades(NoLock)
	Group By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProveedor
	Order By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProveedor

	-------------------------------------------------------------
	-- Se obtiene el concentrado de los Vales de Cada Farmacia --
	-------------------------------------------------------------	
	-- Se obtienen los vales emitidos por Farmacia
	Select IdEmpresa, IdEstado, IdFarmacia, IsNull( Count(Distinct FolioVale), 0 ) as ValesEmitidos_Farmacia
	Into #tmpValesEmitidos_Farmacia
	From #tmpCantidades (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia

	-- Se obtienen los vales registrados por Farmacia
	Select IdEmpresa, IdEstado, IdFarmacia, IsNull( Count(Distinct FolioVale), 0 ) as ValesRegistrados_Farmacia
	Into #tmpValesRegistrados_Farmacia
	From #tmpRegistrados (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia

	-- Se obtienen las piezas emitidas por Farmacia
	Select IdEmpresa, IdEstado, IdFarmacia, IsNull( Sum(Cantidad), 0 ) as PiezasEmitidas_Farmacia
	Into #tmpPiezasEmitidas_Farmacia
	From #tmpCantidades (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia

	-- Se obtienen las piezas registradas por Farmacia
	Select IdEmpresa, IdEstado, IdFarmacia, IsNull( Sum(Cantidad), 0 ) as PiezasRegistradas_Farmacia
	Into #tmpPiezasRegistradas_Farmacia
	From #tmpRegistrados (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia

	-- Se obtienen el importe registrado por Farmacia
	Select Distinct IdEmpresa, IdEstado, IdFarmacia, IsNull( Total, 0 ) as Total
	Into #tmpTotales_ImporteRegistrado_Farmacia
	From #tmpRegistrados (NoLock) 
	Order By IdEmpresa, IdEstado, IdFarmacia, Total
	
	Select IdEmpresa, IdEstado, IdFarmacia, IsNull( Sum(Total), 0 ) as ImporteRegistrado_Farmacia
	Into #tmpImporteRegistrado_Farmacia
	From #tmpTotales_ImporteRegistrado_Farmacia (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia

	-- Se actualizan las cantidades
	Update C Set ValesEmitidos_Farmacia = T.ValesEmitidos_Farmacia
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpValesEmitidos_Farmacia T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia )

	Update C Set ValesRegistrados_Farmacia = T.ValesRegistrados_Farmacia
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpValesRegistrados_Farmacia T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia )

	Update C Set PiezasEmitidas_Farmacia = T.PiezasEmitidas_Farmacia
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpPiezasEmitidas_Farmacia T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia )

	Update C Set PiezasRegistradas_Farmacia = T.PiezasRegistradas_Farmacia
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpPiezasRegistradas_Farmacia T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia )

	Update C Set ImporteRegistrado_Farmacia = T.ImporteRegistrado_Farmacia
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpImporteRegistrado_Farmacia T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia )

	----------------------------------------------------------
	-- Se obtiene el concentrado de los Vales de Cada Clave --
	----------------------------------------------------------	
	-- Se obtienen los vales emitidos por Clave
	Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IsNull( Count(Distinct FolioVale), 0 ) as ValesEmitidos_Clave
	Into #tmpValesEmitidos_Clave
	From #tmpCantidades (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA

	-- Se obtienen los vales registrados por Clave
	Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IsNull( Count(Distinct FolioVale), 0 ) as ValesRegistrados_Clave
	Into #tmpValesRegistrados_Clave
	From #tmpRegistrados (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA

	-- Se obtienen las piezas emitidas por Clave
	Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IsNull( Sum(Cantidad), 0 ) as PiezasEmitidas_Clave
	Into #tmpPiezasEmitidas_Clave
	From #tmpCantidades (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA

	-- Se obtienen las piezas registradas por Clave
	Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IsNull( Sum(Cantidad), 0 ) as PiezasRegistradas_Clave
	Into #tmpPiezasRegistradas_Clave
	From #tmpRegistrados (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA

	-- Se obtienen el importe registrado por Clave
	Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IsNull( Sum(Importe_Producto), 0 ) as ImporteRegistrado_Clave
	Into #tmpImporteRegistrado_Clave
	From #tmpRegistrados (NoLock) 
	Group By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA


	-- Se actualizan las cantidades
	Update C Set ValesEmitidos_Clave = T.ValesEmitidos_Clave
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpValesEmitidos_Clave T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia And C.ClaveSSA = T.ClaveSSA )

	Update C Set ValesRegistrados_Clave = T.ValesRegistrados_Clave
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpValesRegistrados_Clave T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia And C.ClaveSSA = T.ClaveSSA )

	Update C Set PiezasEmitidas_Clave = T.PiezasEmitidas_Clave
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpPiezasEmitidas_Clave T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia And C.ClaveSSA = T.ClaveSSA )

	Update C Set PiezasRegistradas_Clave = T.PiezasRegistradas_Clave
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpPiezasRegistradas_Clave T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia And C.ClaveSSA = T.ClaveSSA )

	Update C Set ImporteRegistrado_Clave = T.ImporteRegistrado_Clave
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpImporteRegistrado_Clave T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdFarmacia = T.IdFarmacia And C.ClaveSSA = T.ClaveSSA )

	-- Se actualiza el Precio de Licitacion de cada Clave
	Update C Set PrecioLicitacion_Clave = IsNull(P.Precio, 0.0000 )
	From #tmpConcentrado C(NoLock)
	Inner Join vw_Claves_Precios_Asignados P(NoLock) On ( C.IdEstado = P.IdEstado And C.ClaveSSA = P.ClaveSSA ) 
	Where P.IdEstado = @IdEstado

	-- Se obtienen los Costos de Compra Minimo y Maximo de Cada Clave
	Update C Set CostoMinimo_Clave = IsNull( ( Select Min( CostoUnitario ) From #tmpRegistrados A(NoLock) Where C.ClaveSSA = A.ClaveSSA Group By ClaveSSA ), 0.0000 )
	From #tmpConcentrado C(NoLock)

	Update C Set CostoMaximo_Clave = IsNull( ( Select Max( CostoUnitario ) From #tmpRegistrados A(NoLock) Where C.ClaveSSA = A.ClaveSSA Group By ClaveSSA ), 0.0000 )
	From #tmpConcentrado C(NoLock)

	--------------------------------------------------------------
	-- Se obtiene el concentrado de los Vales de Cada Proveedor --
	--------------------------------------------------------------	
	-- Se obtienen los vales registrados por Proveedor
	Select IdEmpresa, IdEstado, IdProveedor, IsNull( Count(Distinct FolioVale), 0 ) as ValesRegistrados_Proveedor
	Into #tmpValesRegistrados_Proveedor
	From #tmpRegistrados (NoLock) 
	Group By IdEmpresa, IdEstado, IdProveedor

	-- Se obtienen las piezas registradas por Proveedor
	Select IdEmpresa, IdEstado, IdProveedor, IsNull( Sum(Cantidad), 0 ) as PiezasRegistradas_Proveedor
	Into #tmpPiezasRegistradas_Proveedor
	From #tmpRegistrados (NoLock) 
	Group By IdEmpresa, IdEstado, IdProveedor

	-- Se obtienen el importe registrado por Proveedor
	Select IdEmpresa, IdEstado, IdProveedor, IsNull( Sum(Total), 0 ) as ImporteRegistrado_Proveedor
	Into #tmpImporteRegistrado_Proveedor
	From ValesEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado
		And Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) In
		( Select Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) as Folio From #tmpCantidades ) 
	Group By IdEmpresa, IdEstado, IdProveedor

	-- Se actualizan las cantidades
	Update C Set ValesRegistrados_Proveedor = T.ValesRegistrados_Proveedor
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpValesRegistrados_Proveedor T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdProveedor = T.IdProveedor )

	Update C Set PiezasRegistradas_Proveedor = T.PiezasRegistradas_Proveedor
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpPiezasRegistradas_Proveedor T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdProveedor = T.IdProveedor )

	Update C Set ImporteRegistrado_Proveedor = T.ImporteRegistrado_Proveedor
	From #tmpConcentrado C(NoLock)
	Inner Join #tmpImporteRegistrado_Proveedor T(NoLock) On ( C.IdEmpresa = T.IdEmpresa And C.IdEstado = T.IdEstado And C.IdProveedor = T.IdProveedor )

	--------------------------------------------------------
	-- Se obtiene el concentrado de los Vales de cada Mes --
	--------------------------------------------------------	
	-- Se Obtienen los totales
	Update #tmpConcentrado Set ValesEmitidos_Mes = IsNull( (Select Sum(ValesEmitidos_Farmacia) From #tmpValesEmitidos_Farmacia(NoLock) ), 0 ) 
	Update #tmpConcentrado Set ValesRegistrados_Mes = IsNull( (Select Sum(ValesRegistrados_Farmacia) From #tmpValesRegistrados_Farmacia(NoLock) ), 0 )
	Update #tmpConcentrado Set PiezasEmitidas_Mes = IsNull( (Select Sum(PiezasEmitidas_Farmacia) From #tmpPiezasEmitidas_Farmacia(NoLock) ), 0 ) 
	Update #tmpConcentrado Set PiezasRegistradas_Mes = IsNull( (Select Sum(PiezasRegistradas_Farmacia) From #tmpPiezasRegistradas_Farmacia(NoLock) ), 0 )
	Update #tmpConcentrado Set ImporteRegistrado_Mes = IsNull( (Select Sum(ImporteRegistrado_Farmacia) From #tmpImporteRegistrado_Farmacia(NoLock) ), 0 )


	-------------------------------
	-- Se Guardan los resultados --
	------------------------------- 
	Delete ADMI_Vales_Estadisticas 
	From ADMI_Vales_Estadisticas E (NoLock) 
	Inner Join #tmpConcentrado C 
		On ( E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia and E.Año = C.Año and E.Mes = C.Mes ) 
	
	
	Insert ADMI_Vales_Estadisticas( Año, Mes, IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProveedor, 
		ValesEmitidos_Mes, ValesRegistrados_Mes, PiezasEmitidas_Mes, PiezasRegistradas_Mes, ImporteRegistrado_Mes, 
		ValesEmitidos_Farmacia, ValesRegistrados_Farmacia, PiezasEmitidas_Farmacia, PiezasRegistradas_Farmacia, ImporteRegistrado_Farmacia, 
		ValesEmitidos_Clave, ValesRegistrados_Clave, PiezasEmitidas_Clave, PiezasRegistradas_Clave, ImporteRegistrado_Clave, 
		PrecioLicitacion_Clave, CostoMinimo_Clave, CostoMaximo_Clave, 
		ValesRegistrados_Proveedor, PiezasRegistradas_Proveedor, ImporteRegistrado_Proveedor )
	Select Año, Mes, IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProveedor, 
		ValesEmitidos_Mes, ValesRegistrados_Mes, PiezasEmitidas_Mes, PiezasRegistradas_Mes, ImporteRegistrado_Mes, 
		ValesEmitidos_Farmacia, ValesRegistrados_Farmacia, PiezasEmitidas_Farmacia, PiezasRegistradas_Farmacia, ImporteRegistrado_Farmacia,
		ValesEmitidos_Clave, ValesRegistrados_Clave, PiezasEmitidas_Clave, PiezasRegistradas_Clave, ImporteRegistrado_Clave, 
		PrecioLicitacion_Clave, CostoMinimo_Clave, CostoMaximo_Clave, 
		ValesRegistrados_Proveedor, PiezasRegistradas_Proveedor, ImporteRegistrado_Proveedor 
	From #tmpConcentrado(NoLock) 
	Order By IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProveedor
		
-- Select Top 1 * From ADMI_Vales_Estadisticas

	------------------------------
	-- Se devuelve el resultado --
	------------------------------
    If @iMostrarResultado = 1 
    Begin 
	   Select * From ADMI_Vales_Estadisticas(NoLock) 
	   Order By Año, Mes, IdEmpresa, IdEstado, IdFarmacia, ClaveSSA 
	End    


End
Go--#SQL

