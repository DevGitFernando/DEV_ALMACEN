If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_OCEN_ConsumoEstados_Claves' And xType = 'P' ) 
	Drop Proc spp_Rpt_OCEN_ConsumoEstados_Claves
Go--#SQL

--	Exec spp_Rpt_OCEN_ConsumoEstados_Claves '001', '21', '006', [ '101', '103' ], '2012-04-01', '2012-04-05', 2, 0 

--	Exec spp_Rpt_OCEN_ConsumoEstados_Claves '001', '21', '006', [ '101', '3422' ], '2013-01-01', '2013-01-05', 1, 0  
 
Create Procedure spp_Rpt_OCEN_ConsumoEstados_Claves 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdJurisdiccion varchar(3), 
	@ClaveSSA varchar(max), 
	@FechaInicio varchar(10), @FechaFin varchar(10), @TipoDispensacion int, @TipoReporte int 
) 
With Encryption 
As 
Begin 
	Declare @iVenta int, @iConsignacion int,
			@sSql varchar(max), @sCampos varchar(max), 
			@sWhereClave varchar(max), @sTabla varchar(max),
			@sTipoDisp varchar(max), @sWhereJuris varchar(max)

	
	Set @iVenta = 0 
	Set @iConsignacion = 1	
	Set @sSql = ''
	Set @sCampos = ''
	Set @sWhereClave = ''
	Set @sTabla = '#tmpConsumosEstados'
	Set @sTipoDisp = ' (0, 1) '
	Set @sWhereJuris = ' And F.IdJurisdiccion In ( Select IdJurisdiccion From CatJurisdicciones (NoLock) Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' ) ' +  char(13) + char(10) 
	
	if @TipoDispensacion = 1 
		Set @sTipoDisp = ' ( 0, 0 ) '    	
	
	if @TipoDispensacion = 2 
		Set @sTipoDisp = ' ( 1, 1 ) ' 

	if @ClaveSSA <> '*' 
		Set @sWhereClave = ' And P.ClaveSSA in ( ' + @ClaveSSA + ' ) ' 

	if @IdJurisdiccion <> '*'
		Set @sWhereJuris = ' And  F.IdJurisdiccion In ( Select IdJurisdiccion From CatJurisdicciones (NoLock) Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' And IdJurisdiccion = ' + char(39) + @IdJurisdiccion + char(39) + ' )' 
	
	If Exists ( Select Name From Sysobjects Where Name = '#tmpConsumosEstados' and xType = 'U' )
      Drop Table #tmpConsumosEstados

	Create Table #tmpConsumosEstados
	(
		IdEmpresa varchar(3) Not Null Default '',
		Empresa varchar(50) Not Null Default '',
		IdEstado varchar(2) Not Null Default '',
		Estado varchar(50) Not Null Default '',
		IdJurisdiccion varchar(3) Not Null Default '',
		Jurisdiccion varchar(100) Not Null Default '',
		IdFarmacia varchar(4) Not Null Default '',
		Farmacia varchar(100) Not Null Default '',
		ClaveSSA varchar(20) Null Default '',
		DescripcionSal varchar(8000) Null Default '',
		PresentacionClaveSSA varchar(100) Not Null Default '',
		ContenidoPaqueteClaveSSA int Not Null Default 0,
		IdProducto varchar(8) Not Null Default '',
		CodigoEAN varchar(30) Not Null Default '',
		Descripcion varchar(400) Not Null Default '',
		Presentacion varchar(100) Not Null Default '',
		ContenidoPaquete int Not Null Default 0,
		EsConsignacion bit Not Null Default 0,
		Cantidad numeric(14, 4) Not Null Default 0,	
		Cajas numeric(14, 4) Not Null Default 0
	) ; 

------------------------ Obtener resumens 
	Select * 
	Into #vw_Productos_CodigoEAN
	From vw_Productos_CodigoEAN 
	
	Select * 
	Into #vw_Farmacias 
	From vw_Farmacias 
	
	Select * 
	Into #vw_EmpresasEstados 
	From vw_EmpresasEstados 	
------------------------ Obtener resumens 



---------------------------------- GENERAR DETALLE DE LOS CONSUMOS    
	Set @sSql = ' Insert Into #tmpConsumosEstados (IdEmpresa, Empresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ' + char(13) + char(10) + 
		' ClaveSSA, DescripcionSal, PresentacionClaveSSA, ContenidoPaqueteClaveSSA, ' +  char(13) + char(10) +
		' IdProducto, CodigoEAN, Descripcion, Presentacion, ContenidoPaquete, EsConsignacion, Cantidad, Cajas) ' + char(13) + char(10) +
		' Select E.IdEmpresa, space(100) As Empresa, E.IdEstado, space(50) As Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, ' +  char(13) + char(10) +
		' P.ClaveSSA, P.DescripcionSal, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA, ' +   char(13) + char(10) +
		' L.IdProducto, L.CodigoEAN, P.Descripcion, P.Presentacion, P.ContenidoPaquete, ' +  char(13) + char(10) +
		' L.EsConsignacion, Sum(L.CantidadVendida) As Cantidad, Cast(0 As numeric(14,4)) As Cajas ' +		 char(13) + char(10) +
		' From VentasEnc E (Nolock) ' +  char(13) + char(10) +
		' Inner Join VentasDet_Lotes L (Nolock) ' +  char(13) + char(10) +
		'	On ( E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.FolioVenta = L.FolioVenta ) ' +  char(13) + char(10) +
		' Inner Join #vw_Productos_CodigoEAN P (NOLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN  ) ' + char(13) + char(10) +
		' Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia ) ' +  char(13) + char(10) +
		' Where E.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' And E.IdEstado = ' + char(39) + @IdEstado + char(39) +  
		'	And Convert( varchar(10), E.FechaRegistro, 120) Between ' + char(39) + @FechaInicio + char(39) + ' And ' + char(39) + @FechaFin + char(39) + 
		'	And L.EsConsignacion In ' + @sTipoDisp + '  ' +  char(13) + char(10) + 
		@sWhereClave +  char(13) + char(10) + 
		' ' + @sWhereJuris + char(13) + char(10) + 
		' Group By E.IdEmpresa, E.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, ' +
		'	P.ClaveSSA, P.DescripcionSal, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA, ' +  
		'	L.IdProducto, L.CodigoEAN, P.Descripcion, P.Presentacion, P.ContenidoPaquete, L.EsConsignacion ' 
	Exec(@sSql)	
	-- Print 	@sSql 	  
---------------------------------- GENERAR DETALLE DE LOS CONSUMOS    


	Update T Set T.Empresa = E.NombreEmpresa, T.Estado = E.NombreEstado
	From #tmpConsumosEstados T (Nolock)
	Inner Join #vw_EmpresasEstados E (Nolock) On ( T.IdEmpresa = E.IdEmpresa And T.IdEstado = E.IdEstado )


----	Update T Set T.Farmacia = F.Farmacia, T.IdJurisdiccion = F.IdJurisdiccion, T.Jurisdiccion = F.Jurisdiccion
----	From #tmpConsumosEstados T (Nolock)
----	Inner Join vw_Farmacias F (Nolock) On ( T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia )

	if @TipoReporte = 0 
		Begin 			
			-- GENERAR TABLA POR FARMACIA 
			Select 'Jurisdicción' = IdJurisdiccion, 'Id Farmacia' = IdFarmacia, 'Farmacia' = Farmacia, 'Clave SSA' = ClaveSSA, 'Descripción Clave SSA' = DescripcionSal, 
			'Tipo de Dispensación' = Case When EsConsignacion = 0 Then 'VENTA' Else 'CONSIGNACION' End, 
			'Presentación' = PresentacionClaveSSA, 'Envase' = ContenidoPaqueteClaveSSA,
			Cast(Sum(Cantidad) As Int) As Piezas, 
			Cast(( (Sum(Cantidad)) /  ContenidoPaqueteClaveSSA) as numeric(14,4)) As Cajas
			From #tmpConsumosEstados (Nolock)
			Group By IdJurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionSal, PresentacionClaveSSA, ContenidoPaqueteClaveSSA, EsConsignacion 
			Order By DescripcionSal, EsConsignacion, IdJurisdiccion, IdFarmacia 
		End 
	Else 
		Begin 			
			-- GENERAR TABLA DE SALIDA DETALLADO POR PRODUCTO -- FARMACIA
			Select 'Jurisdicción' = IdJurisdiccion, 'Id Farmacia' = IdFarmacia, 'Farmacia' = Farmacia, 'Clave SSA' = ClaveSSA, 'Producto' = IdProducto, 
			'Codigo EAN' = CodigoEAN, 'Descripción' = Descripcion,   
			'Tipo de Dispensación' = Case When EsConsignacion = 0 Then 'VENTA' Else 'CONSIGNACION' End, 
			'Presentación' = Presentacion, 'Envase' = ContenidoPaquete,
			Cast(Cantidad As Int) As Piezas, 
			Cast(( Cantidad /  ContenidoPaquete) as numeric(14,4)) As Cajas  
			From #tmpConsumosEstados (Nolock)						
			Order By ClaveSSA, EsConsignacion, IdJurisdiccion, IdFarmacia 	
		End

End
Go--#SQL
