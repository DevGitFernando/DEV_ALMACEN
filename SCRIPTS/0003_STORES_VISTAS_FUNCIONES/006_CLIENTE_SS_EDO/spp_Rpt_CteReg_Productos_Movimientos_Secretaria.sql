
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_CteReg_Productos_Movimientos_Secretaria' And xType = 'P' )
	Drop Proc spp_Rpt_CteReg_Productos_Movimientos_Secretaria
Go--#SQL

Create Procedure spp_Rpt_CteReg_Productos_Movimientos_Secretaria ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@FechaInicial varchar(10) = '2012-01-01', @FechaFinal varchar(10) = '2012-01-31', 
	@iMostrarResultado smallint = 0, @sTabla varchar(200) = 'tmpMovimientos_Secretaria' )
With Encryption
As
Begin
	Declare 
		@IdTipoMovto varchar(6),
		@NombreMovto varchar(50),
		@sWhereFarmacia varchar(200), 
		@sWhereTipoDispensacion varchar(200),
		@iAño int,
		@iMes int,
		@sSql varchar(8000)			

	Set DateFormat YMD
	Set NoCount On
	Set @IdTipoMovto = ''
	Set @NombreMovto = ''
	Set @sWhereFarmacia = ''
	Set @sWhereTipoDispensacion = ''
	Set @iAño = DatePart( year, @FechaFinal )
	Set @iMes = DatePart( month, @FechaFinal )
	Set @sSql = '' 
	
	-----------------------------------------------------------
	-- Se crea la tabla donde se ingresaran los movimientos  --
	-----------------------------------------------------------
	Select	E.IdEmpresa, Cast('' as varchar(100) ) as Empresa, E.IdEstado, Cast('' as varchar(50) ) as Estado, 
			E.IdFarmacia, Cast('' as varchar(100) ) as Farmacia, E.IdSubFarmacia, Cast('' as varchar(50) ) as SubFarmacia, 
			GetDate() as FechaImpresion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 
			P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, E.IdProducto, E.CodigoEAN, P.TipoDeProducto, 
			E.ClaveLote, 0 as EsConsignacion,
			E.FechaCaducidad, DateDiff( Month, GetDate(), E.FechaCaducidad ) as MesesPorCaducar, 
			E.FechaRegistro, P.Descripcion as DescripcionProducto, P.IdPresentacion, P.Presentacion, P.ContenidoPaquete, 
			Cast(0 as int) as InventarioInicial, Cast(0 as int) as EntradasDisur, Cast(0 as int) as EntradasFarmaco,
			Cast(0 as int) as VentasDisur, Cast(0 as int) as VentasFarmaco
	Into #tmpClaves
	From FarmaciaProductos_CodigoEAN_Lotes E(NoLock) 
	Inner Join vw_Productos_CodigoEAN P(NoLock) On (E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.ClaveLote Like '%*%'
	Order By E.IdFarmacia, P.DescripcionSal

	-----------------------------------------------------------
	-- Se obtienen los nombres de Empresa, Estado y Farmacia --
	-----------------------------------------------------------
	Update C
	Set Empresa = I.NombreEmpresa, Estado = I.Estado, Farmacia = I.Farmacia
	From #tmpClaves C 
	Inner Join vw_EmpresasFarmacias I(NoLock) On ( C.IdEmpresa = I.IdEmpresa and C.IdEstado = I.IdEstado And C.IdFarmacia = I.IdFarmacia ) 

	Update C Set SubFarmacia = dbo.fg_ObtenerNombreSubFarmacia( C.IdEstado, C.IdFarmacia, C.IdSubFarmacia ) 
	From #tmpClaves C 

	-- Se actualiza el Campo EsConsignacion
	Update #tmpClaves Set EsConsignacion = 1 Where ClaveLote Like '%*%'

	----------------------------------------------------
	-- Se obtienen las Cantidades de Entrada y Salida --
	----------------------------------------------------
	/*************
	** ENTRADAS **
	**************/
	-- Se obtiene la cantidad de Inventario Inicial de Consignacion. NOTA: ESTA ES LA TABLA DE LO QUE SE FIRMO.
	Select IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( Cantidad ), 0 ) as CantidadMovto
	Into #tmpInventarioInicial
	From CteReg_Inventario_Inicial_Secretaria (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
	Group By IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia

	-- Se obtiene la cantidad de Entrada por Consignacion. NOTA: SOLO SE TOMAN EN CUENTA CEDIS Y NADRO.
	Select E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( D.CantidadRecibida ), 0 ) as CantidadMovto
	Into #tmpEntradaConsignacion
	From EntradasEnc_Consignacion E (NoLock) 
	Inner Join EntradasDet_Consignacion_Lotes D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioEntrada = D.FolioEntrada )  
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia In ( '0004', '0182' ) And D.ClaveLote Like '%*%' 
		And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Group By E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia 

	/************
	** SALIDAS **
	*************/
	-- Se obtiene la cantidad de Salida por Venta. NOTA: SE EXCLUYE FARMACIA 0004( NADRO ) YA QUE NO HACE SALIDAS.
	Select E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( D.CantidadVendida ), 0 ) as CantidadMovto
	Into #tmpVentas
	From VentasEnc E (NoLock) 
	Inner Join VentasDet_Lotes D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta )  
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia <> '0004' And D.ClaveLote Like '%*%' 
		And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Group By E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia 

	-- Se obtiene la cantidad de Salida por Caducado. NOTA: SE EXCLUYE FARMACIA 0004( NADRO ) YA QUE NO HACE SALIDAS.
	Select E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( D.Cantidad ), 0 ) as CantidadMovto
	Into #tmpCaducado
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia <> '0004' And E.IdTipoMovto_Inv = 'SC'
		And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Group By E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia

	-- Se obtiene la cantidad de Salida por Merma. NOTA: SE EXCLUYE FARMACIA 0004( NADRO ) YA QUE NO HACE SALIDAS.
	Select E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( D.Cantidad ), 0 ) as CantidadMovto
	Into #tmpMerma
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia <> '0004' And E.IdTipoMovto_Inv = 'SM'
		And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Group By E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia

	----------------------------------
	-- Se actualizan las cantidades --
	---------------------------------- 
	/*************
	** ENTRADAS **
	**************/
	-- Se actualiza la cantidad que entro por Inventario Inicial de Consignacion.
	Update C 
	Set InventarioInicial = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpInventarioInicial I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 

	-- Se actualiza la cantidad que entro por Consignacion de DISUR.
	Update C 
	Set EntradasDisur = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpEntradaConsignacion I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '05'

	-- Se actualiza la cantidad que entro por Consignacion de FARMACO.
	Update C 
	Set EntradasFarmaco = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpEntradaConsignacion I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '06'

	/************
	** SALIDAS **
	*************/
	-- Se actualiza la cantidad que salio por Venta de DISUR y FARMACO.
	Update C 
	Set VentasDisur = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpVentas I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '05'

	Update C 
	Set VentasFarmaco = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpVentas I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '06'

	-- Se agrega a la cantidad que salio por Venta de DISUR y FARMACO, las Salidas por Caducado.
	Update C 
	Set VentasDisur = ( C.VentasDisur + I.CantidadMovto )
	From #tmpClaves C (NoLock)
	Inner Join #tmpCaducado I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '05'

	Update C 
	Set VentasFarmaco = ( C.VentasFarmaco + I.CantidadMovto )
	From #tmpClaves C (NoLock)
	Inner Join #tmpCaducado I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '06'

	-- Se agrega a la cantidad que salio por Venta de DISUR y FARMACO, las Salidas por Mermas.
	Update C 
	Set VentasDisur = ( C.VentasDisur + I.CantidadMovto )
	From #tmpClaves C (NoLock)
	Inner Join #tmpMerma I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '05'

	Update C 
	Set VentasFarmaco = ( C.VentasFarmaco + I.CantidadMovto )
	From #tmpClaves C (NoLock)
	Inner Join #tmpMerma I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	Where I.IdSubFarmacia = '06'

	-------------------------------
	-- Se obtiene el Concentrado --
	-------------------------------
	Select	ClaveSSA, Sum( InventarioInicial ) as InventarioInicial, 
			Sum( EntradasDisur ) as EntradasDisur, Sum( EntradasFarmaco ) as EntradasFarmaco, 
			Sum( VentasDisur ) as VentasDisur, Sum( VentasFarmaco ) as VentasFarmaco, 			
			Cast( 0 as int ) as DiferenciaLogica, 
			Cast(0 as int) as TipoDeClave, Cast( '' as varchar(50) ) as TipoDeClaveDescripcion 
	Into #tmpConcentrado
	From #tmpClaves(NoLock) 
	Group By ClaveSSA 
	Order By ClaveSSA  

	-- Se obtiene la Diferencia Logica.
	Update #tmpConcentrado Set DiferenciaLogica = ( InventarioInicial + EntradasDisur + EntradasFarmaco ) - ( VentasDisur + VentasFarmaco ) 

	-- Se obtiene el Tipo de Clave
	Update C Set TipoDeClave = I.TipoDeClave, TipoDeClaveDescripcion = I.TipoDeClaveDescripcion
	From #tmpConcentrado C(NoLock)
	Inner Join vw_ClavesSSA_Sales I(NoLock) On( C.ClaveSSA = I.ClaveSSA )

	-------------------------------
	-- Se Guardan los resultados --
	-------------------------------
	-- Se Borra la informacion del Año y Mes.
	Delete From CteReg_Productos_Movimientos_Secretaria_Concentrado Where Año = @iAño And Mes = @iMes
	Delete From CteReg_Productos_Movimientos_Secretaria_Detallado Where Año = @iAño And Mes = @iMes
	
	-- Se guarda el Concentrado
	Insert CteReg_Productos_Movimientos_Secretaria_Concentrado( Año, Mes, IdEmpresa, IdEstado, ClaveSSA,
		InventarioInicial, EntradasDisur, EntradasFarmaco, VentasDisur, VentasFarmaco, DiferenciaLogica, 
		TipoDeClave, TipoDeClaveDescripcion )
	Select @iAño, @iMes, @IdEmpresa, @IdEstado, ClaveSSA,
		InventarioInicial, EntradasDisur, EntradasFarmaco, VentasDisur, VentasFarmaco, DiferenciaLogica,
		TipoDeClave, TipoDeClaveDescripcion
	From #tmpConcentrado(NoLock) 
	Order By ClaveSSA

	-- Se guarda el Detalle.
	Insert CteReg_Productos_Movimientos_Secretaria_Detallado( Año, Mes, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote,
		InventarioInicial, EntradasDisur, EntradasFarmaco, VentasDisur, VentasFarmaco )
	Select @iAño, @iMes, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote,
		InventarioInicial, EntradasDisur, EntradasFarmaco, VentasDisur, VentasFarmaco
	From #tmpClaves(NoLock) 
	Order By IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN

	------------------------------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor en caso de solicitarlo --
	------------------------------------------------------------------------------
	If @sTabla <> ''
	  Begin
		Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + Char(39) + @sTabla + Char(39) + ' and  xType = ' + Char(39) + 'U' + Char(39) + ' ) ' +
					'Drop Table ' + @sTabla + ' ' 
		Exec(@sSql)

		Set @sSql = 'Select * Into ' + @sTabla + ' From #tmpClaves(NoLock) Order by IdFarmacia, DescripcionSal, IdSubFarmacia '
		Exec(@sSql)
	  End
	
	----------------------------------------
	-- Se devuelven los datos del reporte --
	---------------------------------------- 
    If @iMostrarResultado = 1 
    Begin 
	   Select * From #tmpClaves(NoLock) 
	   Order By IdFarmacia, DescripcionSal, IdSubFarmacia 
	End    


End
Go--#SQL

