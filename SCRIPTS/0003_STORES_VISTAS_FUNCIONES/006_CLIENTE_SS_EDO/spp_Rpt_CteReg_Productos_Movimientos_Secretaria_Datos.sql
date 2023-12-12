
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_CteReg_Productos_Movimientos_Secretaria_Datos' And xType = 'P' )
	Drop Proc spp_Rpt_CteReg_Productos_Movimientos_Secretaria_Datos
Go--#SQL

Create Procedure spp_Rpt_CteReg_Productos_Movimientos_Secretaria_Datos ( @IdEstado varchar(2) = '21', @iAño int = 2012, @iMes int = 12, 
	@iTipoDeClave int = 0, @iMostrarResultado smallint = 0, @sTabla varchar(200) = 'tmpMovimientos_Secretaria_Datos' )
With Encryption
As
Begin
	Declare @sSql varchar(8000)

	Set DateFormat YMD
	Set NoCount On
	Set @sSql = ''

	----------------------------------------------------
	-- Se crea la tabla donde se insertaran los datos --
	----------------------------------------------------
	Select Top 0 P.ClaveSSA, P.DescripcionSal, M.InventarioInicial, M.EntradasDisur, 
				M.EntradasFarmaco, M.VentasDisur, M.VentasFarmaco, 
				M.DiferenciaLogica, M.TipoDeClave 
		Into #tmpClaves
		From CteReg_Productos_Movimientos_Secretaria_Concentrado M(NoLock) 
		Inner Join vw_ClavesSSA_Sales P(NoLock) On (M.ClaveSSA = P.ClaveSSA ) 
		Order By P.ClaveSSA	

	--------------------------------------------
	-- Se obtienen los datos segun el Periodo --
	--------------------------------------------
	if @iAño <> 0 
	  Begin
		-- Se obtienen los datos del Año y Mes.
		Insert Into #tmpClaves ( ClaveSSA, DescripcionSal, InventarioInicial, EntradasDisur, EntradasFarmaco, VentasDisur, VentasFarmaco, DiferenciaLogica, TipoDeClave )
		Select	ClaveSSA, '' as DescripcionSal, InventarioInicial, EntradasDisur, 
				EntradasFarmaco, VentasDisur, VentasFarmaco, 
				DiferenciaLogica, TipoDeClave 	
		From CteReg_Productos_Movimientos_Secretaria_Concentrado (NoLock) 
		Where IdEstado = @IdEstado And Año = @iAño And Mes = @iMes  
		Order By ClaveSSA	
		
		-- Se eliminan los datos segun el Tipo de Clave
		if @iTipoDeClave <> 0 
		  Begin 
			Delete From #tmpClaves Where TipoDeClave <> @iTipoDeClave
		  End
	  End

	Else
	  Begin
		-- Se obtienen los datos de todo el Periodo.
		Insert Into #tmpClaves ( ClaveSSA, DescripcionSal, InventarioInicial, EntradasDisur, EntradasFarmaco, VentasDisur, VentasFarmaco, DiferenciaLogica, TipoDeClave )
		Select	ClaveSSA, '' as DescripcionSal, Cast( 0 as int ) as InventarioInicial, 
				Sum( EntradasDisur ) as EntradasDisur, Sum( EntradasFarmaco ) as EntradasFarmaco, 
				Sum( VentasDisur ) as VentasDisur, Sum( VentasFarmaco ) as VentasFarmaco, 
				Cast( 0 as int ) as DiferenciaLogica, 
				TipoDeClave 
		From CteReg_Productos_Movimientos_Secretaria_Concentrado M(NoLock) 
		Where IdEstado = @IdEstado
		Group By ClaveSSA, TipoDeClave
		Order By ClaveSSA	

		-- Se eliminan los datos segun el Tipo de Clave
		if @iTipoDeClave <> 0 
		  Begin 
			Delete From #tmpClaves Where TipoDeClave <> @iTipoDeClave
		  End

		-- Se obtiene la cantidad de Inventario Inicial. NOTA: No importa el Año ya que siempre debe ser el mismo.
		Select Distinct ClaveSSA, InventarioInicial	
		Into #tmpInventarioInicial
		From CteReg_Productos_Movimientos_Secretaria_Concentrado(NoLock)
		Order By ClaveSSA

		Update C Set InventarioInicial = I.InventarioInicial
		From #tmpClaves C(NoLock)
		Inner Join #tmpInventarioInicial I(NoLock) On ( C.ClaveSSA = I.ClaveSSA ) 

		-- Se obtiene la Diferencia Logica
		Update #tmpClaves Set DiferenciaLogica = ( InventarioInicial + EntradasDisur + EntradasFarmaco ) - ( VentasDisur + VentasFarmaco ) 

	  End

	-- Se obtiene la descripcion de la Clave.
	Update C Set DescripcionSal = I.DescripcionSal
	From #tmpClaves C(NoLock)
	Inner Join vw_ClavesSSA_Sales I On ( C.ClaveSSA = I.ClaveSSA ) 

	------------------------------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor en caso de solicitarlo --
	------------------------------------------------------------------------------
	If @sTabla <> ''
	  Begin
		Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + Char(39) + @sTabla + Char(39) + ' and  xType = ' + Char(39) + 'U' + Char(39) + ' ) ' +
					'Drop Table ' + @sTabla + ' ' 
		Exec(@sSql)

		Set @sSql = 'Select * Into ' + @sTabla + ' From #tmpClaves(NoLock) Order by ClaveSSA '
		Exec(@sSql)
	  End
	
	----------------------------------------
	-- Se devuelven los datos del reporte --
	---------------------------------------- 
    If @iMostrarResultado = 1 
    Begin 
		Select	ClaveSSA, DescripcionSal as 'Descripción', InventarioInicial as 'Inv. Inicial', 
				EntradasDisur as 'Ent. Disur', EntradasFarmaco as 'Ent. Farmaco', 
				VentasDisur as 'Ventas Disur', VentasFarmaco as 'Ventas Farmaco', 
				DiferenciaLogica as 'Diferencia Logica' 
		From #tmpClaves(NoLock) 
		Order By ClaveSSA	   
	End    


End
Go--#SQL

