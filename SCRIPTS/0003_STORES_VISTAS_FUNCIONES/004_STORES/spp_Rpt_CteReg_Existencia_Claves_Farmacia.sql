
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_CteReg_Existencia_Claves_Farmacia' and xType = 'P')
    Drop Proc spp_Rpt_CteReg_Existencia_Claves_Farmacia
Go--#SQL
  
-- Set Dateformat YMD Exec  spp_Rpt_CteReg_Existencia_Claves_Farmacia '21', '006', '1182', '1', '1', '2', '1' 
-- Set Dateformat YMD Exec  spp_Rpt_CteReg_Existencia_Claves_Farmacia '21', '006', '1182', '0', '0', '0', '0' 

Create Proc spp_Rpt_CteReg_Existencia_Claves_Farmacia 
(
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005',  
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', @IdFarmacia varchar(4) = '1182',
	@iTipoExistencia smallint = 0, @TipoInsumo tinyint = 0, @TipoDispensacion tinyint = 0, @TipoClave tinyint = 0 
)
With Encryption 
As
Begin
Set NoCount On
	Declare 
		@sCondicionExistencia varchar(1000),
		@sCondicionTipoInsumo varchar(1000), 
		@sCondicionDispensacion varchar(1000), 
		@sCondicionTipoClave varchar(1000),
		@sSql varchar(7500) 
	
	----------------------------------
	-- Se inicializan las variables --
	----------------------------------
	Set DateFormat YMD 
	Set @sCondicionExistencia = ' ' 
	Set @sCondicionTipoInsumo = ' ' 
	Set @sCondicionDispensacion = ' ' 
	Set @sCondicionTipoClave = ' ' 
	Set @sSql = '' 

	----------------------------------------------------------------
	-- Se crea la tabla temporal donde se guardara la informacion --
	----------------------------------------------------------------
	Select Top 0 Cast( '' as varchar(4) ) as IdFarmacia, Cast( '' as varchar(100) ) as Farmacia, 
				Cast( '' as varchar(50) ) as ClaveSSA, Cast( '' as varchar(7500) ) as DescripcionSal, Cast( '' as varchar(50) ) as TipoClave,
				Cast( 0 as int ) as Existencia, Cast( 0 as int ) as ExistVenta, Cast( 0 as int ) as ExistConsigna
	Into #tmpExistencias
	
	------------------------------------------
	-- Se obtienen las Farmacias del Estado --
	------------------------------------------	
	Select IdEstado, IdJurisdiccion, IdFarmacia, NombreFarmacia, Cast( 0 as int) as IdNivel 
	Into #tmpFarmacias 
	From CatFarmacias(NoLock) 
	Where IdEstado = @IdEstado And Status = 'A' 
	Order By IdJurisdiccion, IdFarmacia

	-- Si el IdJurisdiccion es diferente a "*" se eliminan las farmacias que no pertenescan a la Jurisdiccion.
	If @IdJurisdiccion <> '*' 
	  Begin
		Delete From #tmpFarmacias Where IdJurisdiccion <> @IdJurisdiccion

		If @IdFarmacia <> '*'
		  Begin
			Delete From #tmpFarmacias Where IdJurisdiccion = @IdJurisdiccion And IdFarmacia <> @IdFarmacia
		  End 
	  End 	

	-- Se obtienen los niveles de cada farmacia
	Update F Set IdNivel = M.IdNivel
	From #tmpFarmacias F(NoLock)
	Inner Join vw_CB_NivelesAtencion_Miembros M(NoLock) On ( F.IdEstado = M.IdEstado And F.IdFarmacia = M.IdFarmacia ) 

------------------ Se obtienen las claves del cuadro basico del nivel al que pertenece las farmacias
----	Select Distinct ClaveSSA 
----	Into #tmpCuadroFarmacia 
----	From vw_CB_CuadroBasico_Claves(NoLock) 
----	Where IdEstado = @IdEstado And IdNivel In ( Select IdNivel From #tmpFarmacias(NoLock) )  
----		  and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente   
	
	
------------------ Se obtienen las claves del cuadro basico del nivel al que pertenece las farmacias
    Select C.IdEstado, C.IdFarmacia, C.ClaveSSA  
    Into #tmpCuadroFarmacia 
    From vw_CB_CuadroBasico_Farmacias  C 
    Inner Join #tmpFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
    Where C.IdEstado = @IdEstado and C.StatusNivel = 'A' and StatusMiembro = 'A'  -- and IdFarmacia = @IdJurisdiccion 
	
--	select top 1 * from vw_CB_CuadroBasico_Claves 
	
	
	---------------------------------------------------------------
	-- Se obtienen las condiciones segun los tipos seleccionados --
	---------------------------------------------------------------
	-- Se obtiene la condicion de la Existencia
	If @iTipoExistencia <> 0
	  Begin	
		If @iTipoExistencia = 1
		  Begin
			Set @sCondicionExistencia = ' and F.Existencia > 0 '
		  End

		If @iTipoExistencia = 2
		  Begin
			Set @sCondicionExistencia = ' and F.Existencia = 0 '
		  End
	  End 

	-- Se obtiene la condicion del Tipo de Insumo
	If @TipoInsumo <> 0 
	  Begin 
		If @TipoInsumo = 1 
		  Begin
			Set @sCondicionTipoInsumo = ' And P.TasaIva = 0 ' 
		  End

		If @TipoInsumo = 2
		  Begin
			Set @sCondicionTipoInsumo = ' And P.TasaIva <> 0 ' 
		  End
	  End

	-- Se obtiene la condicion del Tipo de Dispensacion
	If @TipoDispensacion <> 0 
	  Begin 
		If @TipoDispensacion = 1
		  Begin
			Set @sCondicionDispensacion = ' And F.ClaveLote Like ' + Char(39) + '%*%' + Char(39) + ' ' -- Consignacion
		  End 		 

		If @TipoDispensacion = 2
		  Begin
			Set @sCondicionDispensacion = ' And F.ClaveLote Not Like ' + Char(39) + '%*%' + Char(39) + ' ' -- Venta
		  End
	  End
	
	-- Se obtiene la condicion del Tipo de Clave.
	If @TipoClave <> 0 
	  Begin
		If @TipoClave = 1
		  Begin
			Set @sCondicionTipoClave = ' And P.ClaveSSA In ( Select ClaveSSA From #tmpCuadroFarmacia C (NoLock) Where C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) '
		  End
		  
		If @TipoClave = 2
		  Begin
			Set @sCondicionTipoClave = ' And P.ClaveSSA Not In ( Select ClaveSSA From #tmpCuadroFarmacia C (NoLock) Where C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) '
		  End
	  End

	----------------------------------------------
	-- Se obtiene la sumatoria de los productos --
	----------------------------------------------
	Set @sSql = 'Insert Into #tmpExistencias( IdFarmacia, ClaveSSA, Existencia, ExistVenta, ExistConsigna) ' + 
	'Select F.IdFarmacia, P.ClaveSSA, Sum( Cast( F.Existencia as int ) ) as Existencia , 0, 0' + 
	'From FarmaciaProductos_CodigoEAN_Lotes F(NoLock) ' +
	'Inner Join vw_Productos_CodigoEAN P(NoLock) On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) ' +
	'Where F.IdEstado = ' + char(39) + @IdEstado + char(39) + ' And F.IdFarmacia In ( Select IdFarmacia From #tmpFarmacias (NoLock) ) ' + 
	' ' + @sCondicionExistencia + @sCondicionTipoInsumo + @sCondicionDispensacion + @sCondicionTipoClave + 
	'Group By F.IdFarmacia, P.ClaveSSA ' +
	'Order By F.IdFarmacia, P.ClaveSSA ' 
	Exec(@sSql)
	Print (@sSql)
		
	--Se obtiene la existencia de Venta
	If ( @TipoDispensacion = 0 or @TipoDispensacion = 2)
	Begin
		Update T
		Set ExistVenta = IsNull((Select Sum(F.Existencia)	
								 From vw_Productos_CodigoEAN P (NoLock)
								 Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
									 On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN And T.IdFarmacia = F.IdFarmacia)
								 Where F.ClaveLote Not like '%*%' And F.IdEstado = @IdEstado And T.ClaveSSA = P.ClaveSSA),0)
		From #tmpExistencias T
	End
	
	--Se obtiene la existencia de Consignación
	If ( @TipoDispensacion = 0 or @TipoDispensacion = 1)
	Begin
		Update T
		Set ExistConsigna = IsNull((Select Sum(F.Existencia)	
									From vw_Productos_CodigoEAN P (NoLock)
									Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
										On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN And T.IdFarmacia = F.IdFarmacia)
									Where F.ClaveLote like '%*%' And F.IdEstado = @IdEstado And T.ClaveSSA = P.ClaveSSA),0)
		From #tmpExistencias T
	End
	
	--Se obtiene el Tipo de clave
	Update T
	Set TipoClave = IsNull((Select Top 1 Cast('Causes' as varchar(9))
							From vw_CB_CuadroBasico_Claves C(NoLock)
							Where C.ClaveSSA = T.ClaveSSA And C.IdEstado = @IdEstado), 'No Causes')
	From #tmpExistencias T(NoLock)
	
	-- Se obtienen la descripcion de la Clave SSA y la Farmacia
	Update T Set DescripcionSal = C.DescripcionClave
	From #tmpExistencias T(NoLock) 
	Inner Join vw_Productos_CodigoEAN C(NoLock) On ( T.ClaveSSA = C.ClaveSSA ) 
	
	Update T Set Farmacia = C.NombreFarmacia
	From #tmpExistencias T(NoLock)
	Inner Join #tmpFarmacias C(NoLock) On ( T.IdFarmacia = C.IdFarmacia ) 

	---------------------------------------------
	-- Se devuelve la Existencia de las Claves -- 
	---------------------------------------------
	Select
		IdFarmacia as 'Id Farmacia', Farmacia, ClaveSSA as 'Clave SSA', DescripcionSal as 'Descripción', TipoClave,
		ExistVenta As 'Existencia de Venta', ExistConsigna As 'Existencia de Consignación', Existencia
	From #tmpExistencias (NoLock)
	Order By IdFarmacia, DescripcionSal	 
	
End
Go--#SQL
