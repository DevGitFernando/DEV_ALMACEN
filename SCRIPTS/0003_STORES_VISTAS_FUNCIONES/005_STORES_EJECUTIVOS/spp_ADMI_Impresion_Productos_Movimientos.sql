
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_ADMI_Impresion_Productos_Movimientos' And xType = 'P' )
	Drop Proc spp_ADMI_Impresion_Productos_Movimientos
Go--#SQL


Create Procedure spp_ADMI_Impresion_Productos_Movimientos ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '0182', @FechaInicial varchar(10) = '2011-01-01', @FechaFinal varchar(10) = '2012-04-30', 
	@TipoDispensacion smallint = 0, @iMostrarResultado smallint = 0, @sTabla varchar(200) = 'tmpMovimientos_Cholula' )
With Encryption
As
Begin
	Declare 
		@IdTipoMovto varchar(6),
		@NombreMovto varchar(50),
		@sWhereFarmacia varchar(200), 
		@sWhereTipoDispensacion varchar(200),
		@sSql varchar(8000)			

	Set DateFormat YMD
	Set NoCount On
	Set @IdTipoMovto = ''
	Set @NombreMovto = ''
	Set @sWhereFarmacia = ''
	Set @sWhereTipoDispensacion = ''
	Set @sSql = '' 
	
	-----------------------------------------------------------
	-- Se crea la tabla donde se ingresaran los movimientos  --
	-----------------------------------------------------------
	Select	Top 0 E.IdEmpresa, Cast('' as varchar(300) ) as Empresa, E.IdEstado, Cast('' as varchar(300) ) as Estado, 
			E.IdFarmacia, Cast('' as varchar(300) ) as Farmacia, E.IdSubFarmacia, Cast('' as varchar(100) ) as SubFarmacia, 
			GetDate() as FechaImpresion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 
			P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, E.IdProducto, E.CodigoEAN, P.TipoDeProducto, 
			E.ClaveLote, 0 as EsConsignacion,
			E.FechaCaducidad, DateDiff( Month, GetDate(), E.FechaCaducidad ) as MesesPorCaducar, 
			E.FechaRegistro, P.Descripcion as DescripcionProducto, P.IdPresentacion, P.Presentacion, P.ContenidoPaquete, 
			E.Existencia, Cast( 0.0000 as Numeric(14,4) ) as PrecioLicitacionSal, Cast( '' as varchar(30) ) as Cuadro, 
			(	Select Top 1 Costo 
				From MovtosInv_Det_CodigosEAN_Lotes K(NoLock) 
				Where E.IdProducto = K.IdProducto And E.CodigoEAN = K.CodigoEAN And E.ClaveLote = K.ClaveLote
				Order By Keyx
			) as CostoInicial, 
			0 as II, 0 as IIC
	Into #tmpClaves
	From FarmaciaProductos_CodigoEAN_Lotes E(NoLock) 
	Inner Join vw_Productos_CodigoEAN P(NoLock) On (E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado 
	Order By E.IdFarmacia, P.DescripcionSal

	----------------------------------------------------
	-- Se inserta en la tabla temporal los productos  --
	----------------------------------------------------

	If @IdFarmacia <> '*'
	  Begin
		Set @sWhereFarmacia = 'And E.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' ' 
	  End

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
							E.FechaRegistro, P.Descripcion as DescripcionProducto, P.IdPresentacion, P.Presentacion, P.ContenidoPaquete, 
							E.Existencia, Cast( 0.0000 as Numeric(14,4) ) as PrecioLicitacionSal, Cast( '''' as varchar(30) ) as Cuadro, 
							IsNull( 
							(	Select Top 1 Costo 
								From MovtosInv_Det_CodigosEAN_Lotes K(NoLock) 
								Where E.IdProducto = K.IdProducto And E.CodigoEAN = K.CodigoEAN And E.ClaveLote = K.ClaveLote
								Order By Keyx
							), 0.0000) as CostoInicial, 
							0 as II, 0 as IIC
					From FarmaciaProductos_CodigoEAN_Lotes E(NoLock) 
					Inner Join vw_Productos_CodigoEAN P(NoLock) On (E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN )
					Where E.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And E.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' ' + 
					@sWhereFarmacia + @sWhereTipoDispensacion + ' ' +
					'Order By E.IdFarmacia, P.DescripcionSal '
	Exec (@sSql )

	-- Se crea la tabla temporal para las cantidades
	Select Top 0 IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, Cantidad as CantidadMovto
	Into #tmpCantidades
	From MovtosInv_Det_CodigosEAN_Lotes D (NoLock)

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
	From #tmpClaves C(NoLock)
	Inner Join vw_Claves_Precios_Asignados P(NoLock) On ( C.IdEstado = P.IdEstado And C.ClaveSSA = P.ClaveSSA ) 
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
	Select E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( D.Cantidad ), 0 ) as CantidadMovto
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdTipoMovto_Inv = 'II'
		And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Group By E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia

	-- Se actualiza la cantidad de cada clave de cada farmacia.
	Update C 
	Set II = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpCantidades I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 

	-- Se limpia la tabla de las Cantidades
	Truncate Table #tmpCantidades

	-- Se obtiene la cantidad de Inventario Inicial de Consignacion
	Insert Into #tmpCantidades
	Select E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( D.Cantidad ), 0 ) as CantidadMovto
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdTipoMovto_Inv = 'IIC'
		And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
	Group By E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia

	-- Se actualiza la cantidad de cada clave de cada farmacia.
	Update C 
	Set IIC = I.CantidadMovto 
	From #tmpClaves C (NoLock)
	Inner Join #tmpCantidades I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) 

	------------------------------------------------------------
	-- Se obtienen las Cantidades de Cada Movto de Inventario --
	------------------------------------------------------------

	Declare cCursor Cursor For	Select IdTipoMovto_Inv, Replace(Descripcion, ' ', '_' ) as Descripcion 
								From Movtos_Inv_Tipos(NoLock) 
								Where IdTipoMovto_Inv Not In( 'II', 'IIC' )
								Order By Efecto_Movto
	Open cCursor Fetch cCursor Into @IdTipoMovto, @NombreMovto 
		While (@@Fetch_Status = 0 ) 
		  Begin	

				-- Se limpia la tabla de las Cantidades
				Truncate Table #tmpCantidades

				-- Se agrega un campo a la tabla con el Tipo del movimiento
				Set @sSql = 'Alter Table #tmpClaves Add ' + @IdTipoMovto + ' int Not Null Default 0 '
				Exec (@sSql)
				
				-- Se obtiene la cantidad del Movimiento
				Insert Into #tmpCantidades
				Select E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia, IsNull( Sum( D.Cantidad ), 0 ) as CantidadMovto
				From MovtosInv_Enc E (NoLock) 
				Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
				Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdTipoMovto_Inv = @IdTipoMovto
					And E.MovtoAplicado = 'S' And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
				Group By E.IdFarmacia, CodigoEAN, ClaveLote, IdSubFarmacia

				-- Se actualiza la cantidad de cada clave de cada farmacia.
				Set @sSql = 'Update C ' + 
							'Set ' + @IdTipoMovto + ' = I.CantidadMovto ' +
							'From #tmpClaves C (NoLock) ' + 
							'Inner Join #tmpCantidades I(NoLock) On ( C.CodigoEAN = I.CodigoEAN and C.ClaveLote = I.ClaveLote And C.IdFarmacia = I.IdFarmacia And C.IdSubFarmacia = I.IdSubFarmacia ) ' 

				Exec(@sSql)

				Fetch cCursor Into @IdTipoMovto, @NombreMovto 
		  End		
	Close cCursor
	DeAllocate cCursor	

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
	

	-- Se inserta el Glosario de Tipos de Movimientos en una tabla
	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpGlosarioMovimientos' And xType = 'U' )
		Drop Table tmpGlosarioMovimientos

	Select IdTipoMovto_Inv, Descripcion, ( Case When Efecto_Movto = 'E' Then 'ENTRADA' Else 'SALIDA' End ) as Efecto 
	Into tmpGlosarioMovimientos
	From Movtos_Inv_Tipos(NoLock)
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

