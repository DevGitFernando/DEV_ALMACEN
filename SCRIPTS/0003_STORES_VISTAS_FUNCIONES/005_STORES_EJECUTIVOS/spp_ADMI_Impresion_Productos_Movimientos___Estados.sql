------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_ADMI_Impresion_Productos_Movimientos___Estados' And xType = 'P' )
	Drop Proc spp_ADMI_Impresion_Productos_Movimientos___Estados
Go--#SQL 

/* 

Exec spp_ADMI_Impresion_Productos_Movimientos___Estados 
	@IdEmpresa = '1', @IdEstado = '22', 
	@Filtrar_Farmacias = 0, 
	@IdFarmacia_Inicial = '4', 
	@IdFarmacia_Final = '4', 
	
	@FechaInicial = '2018-01-01', 
	@FechaFinal = '2021-12-28', 
	@TipoDispensacion = 0, @iMostrarResultado = 0, @sTabla = 'RPT__ConcentradoMovimientos__20211228', 
	@ClaveSSA = '010.000.1923.00'    -- '010.000.1344.00'  

*/ 

Create Procedure spp_ADMI_Impresion_Productos_Movimientos___Estados 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '22', 
	@Filtrar_Farmacias int = 0, 
	@IdFarmacia_Inicial varchar(4) = '4', 
	@IdFarmacia_Final varchar(4) = '4', 
	
	@FechaInicial varchar(10) = '2018-01-01', 
	@FechaFinal varchar(10) = '2021-12-28', 
	@TipoDispensacion smallint = 0, @iMostrarResultado smallint = 0, @sTabla varchar(200) = 'RPT__ConcentradoMovimientos', 
	@ClaveSSA varchar(100) = '' --- '010.000.0101.00' -- '' 
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On  

Declare 
	@IdTipoMovto varchar(6),
	@NombreMovto varchar(500), 
	@NombreCampo varchar(600), 
	@sEfectoMovto varchar(10), 
	@sListaCampos_Entrada varchar(max), 
	@sListaCampos_Salida varchar(max), 
	@sWhereFarmacia varchar(200), 
	@sWhereTipoDispensacion varchar(200) 
	-- @sSql varchar(8000)			


Declare 
	@sSql varchar(max), 
	@sFiltro varchar(max) 


	Set @IdTipoMovto = ''
	Set @NombreMovto = '' 
	Set @NombreCampo = '' 
	Set @sEfectoMovto = '' 
	Set @sListaCampos_Entrada = ''  
	Set @sListaCampos_Salida = '' 

	Set @sWhereFarmacia = ''
	Set @sWhereTipoDispensacion = '' 

	Set @sSql = '' 
	Set @sFiltro = ''  
	
	Set @IdEmpresa = right('00000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000' + @IdEstado, 2)
	
	--------------------- Farmacias   
	Select * 
	into #tmp_Farmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado and 1 = 0 

	Set @sFiltro = 'Where IdEstado = ' + char(39) + @IdEstado + char(39)
	If @Filtrar_Farmacias = 1  
	Begin 
		If @IdFarmacia_Inicial <> '' and @IdFarmacia_Final  <> '' 
			Begin 
				Set @sFiltro = @sFiltro + ' and IdFarmacia between ' + char(39) + right('0000' + @IdFarmacia_Inicial, 4) + char(39) + ' and ' + char(39) + right('0000' + @IdFarmacia_Final, 4) + char(39) 
			End 
		Else 
			Begin 
				If @IdFarmacia_Inicial <> ''  
					Begin 
						Set @sFiltro = @sFiltro + ' and IdFarmacia >= ' + char(39) + right('0000' + @IdFarmacia_Inicial, 4) + char(39) 
					End 

				If @IdFarmacia_Final <> ''  
					Begin 
						Set @sFiltro = @sFiltro + ' and IdFarmacia <= ' + char(39) + right('0000' + @IdFarmacia_Final, 4) + char(39) 
					End 
			End 
	End 

	Set @sSql = 'Insert Into #tmp_Farmacias Select * From vw_Farmacias (noLock)'
	Set @sSql = @sSql + @sFiltro 
	Exec(@sSql) 

	--Select * from #tmp_Farmacias 

	--delete from #tmp_Farmacias 
	--------------------- Farmacias   


	Select * 
	Into #vw_Productos_CodigoEAN 
	from vw_Productos_CodigoEAN 
	Where ClaveSSA like '%' + replace(@ClaveSSA, ' ', '%' ) + '%'


	-------------- Se crea la tabla temporal para las cantidades 
	Select Top 0 0 as Tipo, IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, Cantidad as CantidadMovto
	Into #tmpCantidades
	From MovtosInv_Det_CodigosEAN_Lotes D (NoLock)


	-------------- Se crea la tabla base del proceso 
	Select 
		E.FechaRegistro, E.IdTipoMovto_Inv, D.* 
	Into #tmp_GeneralMovtos 
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto And P.CodigoEAN = D.CodigoEAN ) 
	Inner Join #tmp_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
		And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal 
		--and D.CodigoEAN = '7506331302033' 
		--and D.CodigoEAN = '7501075715910' and D.ClaveLote = '*790067' 


	Select IdTipoMovto_Inv, IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( Cantidad ), 0 ) as Cantidad 
	Into #tmp_GeneralMovtos__Concentrado 
	From #tmp_GeneralMovtos 
	--Where IdTipoMovto_Inv = @IdTipoMovto  
	Group by IdTipoMovto_Inv, IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia 
	-------------- Se crea la tabla base del proceso 


	----------------------------------------------------------------------------------------------------------------------
	-- Se crea la tabla donde se ingresaran los movimientos  --
	----------------------------------------------------------------------------------------------------------------------

	Select	Top 0 
		E.IdEmpresa, Cast('' as varchar(300) ) as Empresa, E.IdEstado, Cast('' as varchar(300) ) as Estado, 
		E.IdFarmacia, Cast('' as varchar(300) ) as Farmacia, E.IdSubFarmacia, Cast('' as varchar(100) ) as SubFarmacia, 
		GetDate() as FechaImpresion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 
		P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, E.IdProducto, E.CodigoEAN, P.TipoDeProducto, 
		E.ClaveLote, 0 as EsConsignacion,
		E.FechaCaducidad, DateDiff( Month, GetDate(), E.FechaCaducidad ) as MesesPorCaducar, 
		E.FechaRegistro, P.Descripcion as DescripcionProducto, P.IdPresentacion, P.Presentacion, P.ContenidoPaquete, 
		Cast( 0.0000 as Numeric(14,4) ) as PrecioLicitacionSal, Cast( '' as varchar(30) ) as Cuadro, 
		(	Select Top 1 Costo 
			From MovtosInv_Det_CodigosEAN_Lotes K(NoLock) 
			Where E.IdProducto = K.IdProducto And E.CodigoEAN = K.CodigoEAN And E.ClaveLote = K.ClaveLote
			Order By Keyx 
		) as CostoInicial, 
		E.Existencia, 
		cast(0 as int) as Entradas, cast(0 as int) as Salidas,   
		cast(0 as int) as Transitos, 0 as Diferencia, 
		cast(0 as int) as II, cast(0 as int) as IIC   
	Into #tmpClaves 
	From FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN ) 
	Inner Join #tmp_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where E.IdEmpresa = @IdEmpresa 
		--and ClaveSSA = '010.000.4273.00' 
		--and E.CodigoEAN = '7506331302033' 
		--and E.CodigoEAN = '7501075715910' and ClaveLote = '*790067' 
	Order By E.IdFarmacia, P.DescripcionSal 


	Select 
		IdEmpresa, Empresa, IdEstado, Estado, 
		GetDate() as FechaImpresion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, TipoDeProducto, 
		IdSubFarmacia, SubFarmacia, 
		EsConsignacion, 
		--PrecioLicitacionSal, 
		Cuadro, 
		--CostoInicial, 
		Existencia, 
		--cast(0 as numeric(14,4)) as Entradas, cast(0 as numeric(14,4)) as Salidas,   
		cast(0 as int) as Entradas, cast(0 as int) as Salidas,   
		cast(0 as int) as Transitos, 
		cast(0 as int) as Diferencia, 
		cast(0 as int) as II, cast(0 as int) as IIC 
	Into #tmpClaves___Resumen 
	From #tmpClaves 

	----------------------------------------------------------------------------------------------------------------------
	-- Se crea la tabla donde se ingresaran los movimientos  --
	----------------------------------------------------------------------------------------------------------------------



	----------------------------------------------------
	-- Se inserta en la tabla temporal los productos  --
	----------------------------------------------------

	----If @IdFarmacia <> '*'
	----  Begin
	----	Set @sWhereFarmacia = 'And E.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' ' 
	----  End

	If @TipoDispensacion = 1 
	  Begin
		-- Solo Venta
		Set @sWhereTipoDispensacion = 'And E.ClaveLote Not Like ' + Char(39) + '%*%' + Char(39) + ' ' 
	  End
	Else If @TipoDispensacion = 2 
	  Begin
		-- Solo Consignacion
		Set @sWhereTipoDispensacion = 'And E.ClaveLote Like ' + Char(39) + '%*%' + Char(39) + ' ' 
	  End


	Set @sSql = '	Insert Into #tmpClaves 
					Select	E.IdEmpresa, Cast('''' as varchar(300) ) as Empresa, E.IdEstado, Cast('''' as varchar(300) ) as Estado, 
							E.IdFarmacia, Cast('''' as varchar(300) ) as Farmacia, E.IdSubFarmacia, Cast('''' as varchar(100) ) as SubFarmacia, 
							GetDate() as FechaImpresion, ' + Char(39) + @FechaInicial + Char(39) + ' as FechaInicial, ' + 
							Char(39) + @FechaFinal + Char(39) + ' as FechaFinal,
							P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, E.IdProducto, E.CodigoEAN, P.TipoDeProducto, 
							E.ClaveLote, 0 as EsConsignacion,
							E.FechaCaducidad, DateDiff( Month, GetDate(), E.FechaCaducidad ) as MesesPorCaducar, 
							--convert(varchar(16), E.FechaRegistro, 120) as FechaRegistro, 
							E.FechaRegistro, 
							P.Descripcion as DescripcionProducto, P.IdPresentacion, P.Presentacion, P.ContenidoPaquete, 
							Cast( 0.0000 as Numeric(14,4) ) as PrecioLicitacionSal, Cast( '''' as varchar(30) ) as Cuadro, 
							----IsNull( 
							----(	Select Top 1 Costo 
							----	From #tmp_GeneralMovtos K(NoLock) 
							----	Where E.IdProducto = K.IdProducto And E.CodigoEAN = K.CodigoEAN And E.ClaveLote = K.ClaveLote
							----	Order By Keyx
							----), 0.0000) as CostoInicial, 
							0 as CostoInicial, 
							sum(E.Existencia) as Existencia, 
							0 as Entradas, 0 as Salidas,   
							0 as Transitos, 
							0 as Diferencia, 
							0 as II, 0 as IIC   
					From FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
					Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN ) 
					Inner Join #tmp_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
					Where E.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' ' + 
					@sWhereFarmacia + @sWhereTipoDispensacion + ' ' + char(13) + 
					'Group by ' + char(13) + 
					'	E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdSubFarmacia, ' + char(13) + 
					'	P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, E.IdProducto, E.CodigoEAN, P.TipoDeProducto, ' + char(13) + 
					'	E.ClaveLote, ' + char(13) + 
					'	E.FechaCaducidad, DateDiff( Month, GetDate(), E.FechaCaducidad ), ' + char(13) + 
					--'	convert(varchar(16), E.FechaRegistro, 120), ' + char(13) +  
					'	E.FechaRegistro, ' + char(13) + 
					'	P.Descripcion, P.IdPresentacion, P.Presentacion, P.ContenidoPaquete ' + char(13) + 
					'Order By E.IdFarmacia, P.DescripcionSal '
	Exec ( @sSql ) 
	--Print @sSql 

	/*

		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, FechaImpresion, FechaInicial, FechaFinal, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, TipoDeProducto, ClaveLote, EsConsignacion, FechaCaducidad, MesesPorCaducar, FechaRegistro, 
		DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, PrecioLicitacionSal, Cuadro, CostoInicial, Existencia, Entradas, Salidas, Transitos, Diferencia, II, IIC

	*/ 
	Select	
		IdEmpresa, Empresa, IdEstado, Estado, 
		IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		FechaImpresion, FechaInicial, FechaFinal, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, TipoDeProducto, 
		ClaveLote, EsConsignacion,
		FechaCaducidad, MesesPorCaducar, 
		convert(varchar(16), FechaRegistro, 120) as FechaRegistro, 
		DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		PrecioLicitacionSal, Cuadro, 
		CostoInicial, 
		sum(Existencia) as Existencia, 
		cast(0 as int) as Entradas, cast(0 as int) as Salidas,   
		cast(0 as int) as Transitos, 0 as Diferencia, 
		cast(0 as int) as II, cast(0 as int) as IIC   
	Into #tmpClaves___Agrupado 
	From #tmpClaves 
	Where 1 = 1  
	Group by 
		IdEmpresa, Empresa, IdEstado, Estado, 
		IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		FechaImpresion, FechaInicial, FechaFinal, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, TipoDeProducto, 
		ClaveLote, EsConsignacion,
		FechaCaducidad, MesesPorCaducar, 
		convert(varchar(16), FechaRegistro, 120), 
		DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		PrecioLicitacionSal, Cuadro, 
		CostoInicial  


	----Exec sp_listacolumnas #tmpClaves 
	----Exec sp_listacolumnas #tmpClaves___Agrupado 


	Delete From #tmpClaves 
	Insert Into #tmpClaves 
	Select * from #tmpClaves___Agrupado 


	---  delete from #tmpClaves Where ClaveSSA <> '010.000.4273.00'  


	--select * from #tmpClaves___Resumen 


				--		spp_ADMI_Impresion_Productos_Movimientos___Estados  



	----select count(*) from  #vw_Productos_CodigoEAN 
	----select count(*) from  #tmp_GeneralMovtos 
	----select count(*) from  #tmp_GeneralMovtos__Concentrado  


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


	------------------------------------------------------
	-- Se obtiene el Precio de Licitacion de cada Clave -- 
	------------------------------------------------------
	Update C Set PrecioLicitacionSal = IsNull(P.Precio, 0 )
	From #tmpClaves C (NoLock)
	Inner Join vw_Claves_Precios_Asignados P (NoLock) On ( C.IdEstado = P.IdEstado And C.ClaveSSA = P.ClaveSSA ) 
	Where P.IdEstado = @IdEstado  

	-- Se actualiza el Cuadro de cada Clave
	Update #tmpClaves Set Cuadro = ( Case When PrecioLicitacionSal > 0 Then 'LICITADO' Else 'NO LICITADO' End ) 



	-------------------------------------------------------------------------------------------
	-- Se obtienen las Cantidades de Inventario Inicial e Inventario Inicial de Consignacion --
	-------------------------------------------------------------------------------------------

	-- Se limpia la tabla de las Cantidades
	Truncate Table #tmpCantidades

	-- Se obtiene la cantidad de Inventario Inicial
	Insert Into #tmpCantidades
	Select 1 as Tipo, IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( Cantidad ), 0 ) as CantidadMovto 
	From #tmp_GeneralMovtos 
	Where IdTipoMovto_Inv = 'II' 
	----From MovtosInv_Enc E (NoLock) 
	----Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) 
	----	On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	----Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto And P.CodigoEAN = D.CodigoEAN ) 
	----Inner Join #tmp_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	----Where E.IdEmpresa = @IdEmpresa And E.IdTipoMovto_Inv = 'II'
	----	And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal 
	----	--and D.CodigoEAN = '7506331302033' 
	----	--and D.CodigoEAN = '7501075715910' and D.ClaveLote = '*790067' 
	Group By IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia 


	--			spp_ADMI_Impresion_Productos_Movimientos___Estados   

	------ Se actualiza la cantidad de cada clave de cada farmacia.
	Update C 
	Set II = I.CantidadMovto 
	From #tmpClaves C (NoLock) 
	Inner Join #tmpCantidades I (NoLock) 
		On ( C.IdFarmacia = I.IdFarmacia and C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	--Where I.Tipo = 1 

	------ Se limpia la tabla de las Cantidades
	Truncate Table #tmpCantidades  

	------ Se obtiene la cantidad de Inventario Inicial de Consignacion
	Insert Into #tmpCantidades  
	Select 2 as Tipo, IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( Cantidad ), 0 ) as CantidadMovto 
	From #tmp_GeneralMovtos 
	Where IdTipoMovto_Inv = 'IIC' 
	--From MovtosInv_Enc E (NoLock) 
	--Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) 
	--	On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	--Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto And P.CodigoEAN = D.CodigoEAN ) 
	--Inner Join #tmp_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	--Where E.IdEmpresa = @IdEmpresa And E.IdTipoMovto_Inv = 'IIC'
	--	And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal 
	--	--and D.CodigoEAN = '7506331302033' 
	--	--and D.CodigoEAN = '7501075715910' and D.ClaveLote = '*790067' 
	Group By IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia 


	------ Se actualiza la cantidad de cada clave de cada farmacia.
	Update C 
		Set IIC = I.CantidadMovto 
	From #tmpClaves C (NoLock) 
	Inner Join #tmpCantidades I (NoLock) 
		On ( C.IdFarmacia = I.IdFarmacia and C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	--Where I.Tipo = 2 



	------------------------------------ Obtener los transitos activos 
	------ Se limpia la tabla de las Cantidades
	Truncate Table #tmpCantidades  


	------ Se obtiene la cantidad de Transitos activos   
	Insert Into #tmpCantidades 
	Select 3 as Tipo, E.IdFarmacia, D.CodigoEAN, D.ClaveLote, D.IdSubFarmaciaEnvia as IdSubFarmacia, IsNull( Sum( D.CantidadEnviada ), 0 ) as CantidadMovto
	From TransferenciasEnc E (NoLocK) 
	Inner Join TransferenciasDet_Lotes D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioTransferencia = D.FolioTransferencia ) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto And P.CodigoEAN = D.CodigoEAN ) 
	Inner Join #tmp_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	where E.TipoTransferencia = 'TS' and E.Status <> 'C' 
		and E.TransferenciaAplicada = 0  
		And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal  
		--and D.CodigoEAN = '7506331302033' 
		--and D.CodigoEAN = '7501075715910' and D.ClaveLote = '*790067' 
	Group by 
		E.IdFarmacia, D.CodigoEAN, D.ClaveLote, D.IdSubFarmaciaEnvia 
	--Order by E.FechaRegistro 

	--			spp_ADMI_Impresion_Productos_Movimientos___Estados   

	------ Se actualiza la cantidad de cada clave de cada farmacia.
	Update C 
		Set Transitos = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpCantidades I (NoLock) 
		On ( C.IdFarmacia = I.IdFarmacia and C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 
	--Where I.Tipo = 3 

	--Select * from #tmpClaves Where ClaveSSA = '010.000.4371.00'  

	------------------------------------ Obtener los transitos activos 



	------------------------------------------------------------
	-- Llenar resumen 
	------------------------------------------------------------
	Insert Into #tmpClaves___Resumen 
	select 
		IdEmpresa, Empresa, IdEstado, Estado, 
		GetDate() as FechaImpresion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, TipoDeProducto, 
		IdSubFarmacia, SubFarmacia, 
		EsConsignacion, 
		--PrecioLicitacionSal, 
		Cuadro, 
		--CostoInicial, 
		sum(Existencia) as Existencia, 
		cast(0 as int) as Entradas, cast(0 as int) as Salidas,   
		sum(Transitos) as Transitos,  
		cast(0 as int) as Diferencia, 
		sum(II) as II, sum(IIC) as IIC  
	From #tmpClaves 
	Group by 
		IdEmpresa, Empresa, IdEstado, Estado, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, TipoDeProducto, 
		IdSubFarmacia, SubFarmacia, 
		EsConsignacion, 
		--Existencia, 
		--PrecioLicitacionSal, 
		Cuadro  
		--CostoInicial, 
		-- II, IIC 

	--			spp_ADMI_Impresion_Productos_Movimientos___Estados   
	
	----select 'SSSSS', * from #tmpClaves 
	----select 'SSSSS', * from #tmpClaves___Resumen 

	------------------------------------------------------------
	-- Se obtienen las Cantidades de Cada Movto de Inventario --
	------------------------------------------------------------
	Set @sListaCampos_Entrada = ''  
	Set @sListaCampos_Salida = '' 


	Declare cCursor Cursor For	
		Select 
			-- Top 1  
			IdTipoMovto_Inv, Efecto_Movto, 
			Replace(Descripcion, ' ', '_' ) as Descripcion,  
			ltrim(rtrim((ltrim(rtrim(Efecto_Movto)) + '__' + IdTipoMovto_Inv + '___' + Replace(Descripcion, ' ', '_' )))) as NombreCampo 
			--(Efecto_Movto + ' ' + IdTipoMovto_Inv + ' ' + Descripcion) as NombreCampo 
			--IdTipoMovto_Inv as NombreCampo 
		From Movtos_Inv_Tipos( NoLock) 
		Where IdTipoMovto_Inv Not In ( 'II', 'IIC' )  
			and 1 = 1        
		--Where IdTipoMovto_Inv 
		--In 
		--( 
		--	'EAI', 'EC', 'EDC', 'EE', 	
		--	'EOC', 	'EPV', 'IAE', 	'IAS', 	
		--	'SAI', 	'SC', 	'SE', 	'SM', 	
		--	'SV', 	'TE', 	'TS' 
		--)  
		Order By Efecto_Movto 
	Open cCursor Fetch cCursor Into @IdTipoMovto, @sEfectoMovto, @NombreMovto, @NombreCampo 
		While (@@Fetch_Status = 0 ) 
		  Begin	
				-- print char(39) + @NombreCampo + char(39) 

				--		spp_ADMI_Impresion_Productos_Movimientos___Estados  
				If @sEfectoMovto = 'E' 
				   Set @sListaCampos_Entrada = @sListaCampos_Entrada + '[' + @NombreCampo + '] + ' 
				Else 
				   Set @sListaCampos_Salida = @sListaCampos_Salida + '[' + @NombreCampo + '] + ' 


				If @IdTipoMovto <> 'II' and @IdTipoMovto <> 'IIC' -- and 1 = 0  
				Begin 

					-- Se limpia la tabla de las Cantidades 
					Truncate Table #tmpCantidades  

					------ Se agrega un campo a la tabla con el Tipo del movimiento
					Set @sSql = 'Alter Table #tmpClaves Add [' + @NombreCampo + '] int Not Null Default 0 ' + char(13) + 
								'Alter Table #tmpClaves___Resumen Add [' + @NombreCampo + '] int Not Null Default 0 '
					Exec (@sSql) 
				

					-------- Se obtiene la cantidad del Movimiento  
					--Insert Into #tmpCantidades
					--Select 0 as Tipo, D.IdFarmacia, D.CodigoEAN, D.ClaveLote, D.IdSubFarmacia, IsNull( Sum( D.Cantidad ), 0 ) as CantidadMovto 						
					--From MovtosInv_Enc E (NoLock) 
					--Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) 
					--	On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
					--Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto And P.CodigoEAN = D.CodigoEAN ) 
					--Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdTipoMovto_Inv = @IdTipoMovto
					--	And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal 
					--	--and D.CodigoEAN = '7506331302033' 
					--	--and D.CodigoEAN = '7501075715910' and D.ClaveLote = '*790067' 
					--	and 1 = 0 
					--Group By D.IdFarmacia, D.CodigoEAN, D.ClaveLote, D.IdSubFarmacia 


					------ Se obtiene la cantidad del Movimiento  
					Insert Into #tmpCantidades
					Select 0 as Tipo, D.IdFarmacia, D.CodigoEAN, D.ClaveLote, D.IdSubFarmacia, IsNull( Sum( D.Cantidad ), 0 ) as CantidadMovto 						
					From #tmp_GeneralMovtos__Concentrado D (NoLock) 
					Where D.IdTipoMovto_Inv = @IdTipoMovto
						--And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal 
						--and D.CodigoEAN = '7506331302033' 
						--and D.CodigoEAN = '7501075715910' and D.ClaveLote = '*790067' 
					Group By D.IdFarmacia, D.CodigoEAN, D.ClaveLote, D.IdSubFarmacia 

					--		spp_ADMI_Impresion_Productos_Movimientos___Estados  

					-- Se actualiza la cantidad de cada clave de cada farmacia. 
					Set @sSql = 'Update C ' + 
								'Set [' + @NombreCampo  + '] = I.CantidadMovto ' + 
								'From #tmpClaves C (NoLock) ' + 
								'Inner Join #tmpCantidades I (NoLock) On ( C.IdFarmacia = I.IdFarmacia and C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) ' 
					Exec(@sSql) 
					--Print @sSql 

					Set @sSql = 'Update C ' + 
								'Set [' + @NombreCampo  + '] = IsNull( ( Select sum( [' + @NombreCampo  + '] ) From #tmpClaves I (NoLock) Where C.IdClaveSSA_Sal = I.IdClaveSSA_Sal and C.IdSubFarmacia = I.IdSubFarmacia ) , 0) ' +
								'From #tmpClaves___Resumen C (NoLock) '  
					Exec(@sSql) 
					--Print @sSql 

				End 

				Fetch cCursor Into @IdTipoMovto, @sEfectoMovto, @NombreMovto, @NombreCampo 
		  End		
	Close cCursor
	DeAllocate cCursor	


	Set @sListaCampos_Entrada = ltrim(rtrim( @sListaCampos_Entrada )) 
	Set @sListaCampos_Salida = ltrim(rtrim( @sListaCampos_Salida )) 

	If ( @sListaCampos_Entrada <> '' and @sListaCampos_Salida <> '' )
	Begin 
		Set @sListaCampos_Entrada = left(@sListaCampos_Entrada, len(@sListaCampos_Entrada) - 1)   
		Set @sListaCampos_Entrada = '[' + 'II' + '] + [' + 'IIC' + '] + ' + @sListaCampos_Entrada 
		Set @sListaCampos_Salida = left(@sListaCampos_Salida, len(@sListaCampos_Salida) - 1)  


		Set @sSql = 
			'Update R Set Entradas = ' + @sListaCampos_Entrada + ', Salidas = ' + @sListaCampos_Salida + ' ' +    
			'From #tmpClaves___Resumen R (NoLock) ' 
		Exec(@sSql) 
		--Print @sSql 



		Set @sSql = 
			'Update R Set Entradas = ' + @sListaCampos_Entrada + ', Salidas = ' + @sListaCampos_Salida + ' ' +    
			'From #tmpClaves R (NoLock) ' 
		Exec(@sSql) 
		--Print @sSql 

	End 

	--Print @sListaCampos_Entrada 
	--Print @sListaCampos_Salida 
	print '' 


	Update R Set Diferencia = (Entradas - Salidas) 
	From #tmpClaves___Resumen R 

	Update R Set Diferencia = (Entradas - Salidas) 
	From #tmpClaves R 
	----------------------------------- Crear Concentrado 

	--			spp_ADMI_Impresion_Productos_Movimientos___Estados   


	------------------------------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor en caso de solicitarlo --
	------------------------------------------------------------------------------
	If @sTabla <> ''
	  Begin 
		Declare @sTabla_Proceso varchar(max) 
		Set @sTabla_Proceso = @sTabla + '__Resumen'

		Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + Char(39) + @sTabla_Proceso + Char(39) + ' and  xType = ' + Char(39) + 'U' + Char(39) + ' ) ' +
					'Drop Table ' + @sTabla_Proceso + ' ' + char(13) 
		--Exec(@sSql)

		Set @sSql = @sSql + 'Select *, identity(int, 1, 1) as Secuencial Into ' + @sTabla_Proceso + ' From #tmpClaves___Resumen (NoLock) Order by DescripcionSal, IdSubFarmacia '
		Exec(@sSql)
		print @sSql 


		----Set @sSql = 
		----	'Update R Set Entradas = ' + @sListaCampos_Entrada + ', Salidas = ' + @sListaCampos_Salida + ' ' +    
		----	'From ' + @sTabla_Proceso + ' R ' 
		----Exec(@sSql) 
		----Print @sSql 



		Set @sTabla_Proceso = @sTabla + '__Detallado' 
		Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + Char(39) + @sTabla_Proceso + Char(39) + ' and  xType = ' + Char(39) + 'U' + Char(39) + ' ) ' +
					'Drop Table ' + @sTabla_Proceso + ' '  + char(13) 
		--Exec(@sSql) 

		Set @sSql = @sSql + 'Select *, identity(int, 1, 1) as Secuencial Into ' + @sTabla_Proceso + ' From #tmpClaves (NoLock) Order by IdFarmacia, DescripcionSal, IdSubFarmacia '
		Exec(@sSql)
		print @sSql 

		----Set @sSql = 
		----	'Update R Set Entradas = ' + @sListaCampos_Entrada + ', Salidas = ' + @sListaCampos_Salida + ' ' +    
		----	'From ' + @sTabla_Proceso + ' R ' 
		----Exec(@sSql) 
		----Print @sSql 

	  End  
	



	-- Se inserta el Glosario de Tipos de Movimientos en una tabla
	If Exists ( Select * From SysObjects(NoLock) Where Name = 'tmpGlosarioMovimientos' And xType = 'U' )
		Drop Table tmpGlosarioMovimientos

	Select IdTipoMovto_Inv, Descripcion, ( Case When Efecto_Movto = 'E' Then 'ENTRADA' Else 'SALIDA' End ) as Efecto 
	Into tmpGlosarioMovimientos
	From Movtos_Inv_Tipos (NoLock)
	Order By IdTipoMovto_Inv  
	
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

