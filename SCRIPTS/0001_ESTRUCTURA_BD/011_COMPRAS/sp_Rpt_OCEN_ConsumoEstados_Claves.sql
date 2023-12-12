

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'sp_Rpt_OCEN_ConsumoEstados_Claves' And xType = 'P' ) 
	Drop Proc sp_Rpt_OCEN_ConsumoEstados_Claves
Go--#SQL

--	Exec sp_Rpt_OCEN_ConsumoEstados_Claves '001', '21', '006', '*', '2012-04-01', '2012-04-15', 0, 0

Create Procedure sp_Rpt_OCEN_ConsumoEstados_Claves( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdJurisdiccion varchar(3), @ClaveSSA varchar(30), 
	@FechaInicio varchar(10), @FechaFin varchar(10), @TipoDispensacion int, @TipoReporte int )
As
Begin
	Declare @iVenta int, @iConsignacion int,
			@sSql varchar(8000), @sCampos varchar(8000), 
			@sWhereClave varchar(100), @sTabla varchar(100),
			@sTipoDisp varchar(100), @sWhereJuris varchar(2000)

	
	Set @iVenta = 0 
	Set @iConsignacion = 1	
	Set @sSql = ''
	Set @sCampos = ''
	Set @sWhereClave = ''
	Set @sTabla = 'tmpConsumosEstados'
	Set @sTipoDisp = '(0, 1)'
	Set @sWhereJuris = ' And F.IdJurisdiccion In ( Select IdJurisdiccion From CatJurisdicciones (NoLock) Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' )'
	
	if @TipoDispensacion = 1 
	   -- Set @iConsignacion = 0 
		Set @sTipoDisp = '(0, 0)'    	
	
	if @TipoDispensacion = 2 
	   --Set @iVenta = 1
		Set @sTipoDisp = '(1, 1)' 

	if @ClaveSSA <> '*'
		Set @sWhereClave = ' And P.ClaveSSA = ' + char(39) + @ClaveSSA + char(39)

	if @IdJurisdiccion <> '*'
		Set @sWhereJuris = ' And  F.IdJurisdiccion In ( Select IdJurisdiccion From CatJurisdicciones (NoLock) Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' And IdJurisdiccion = ' + char(39) + @IdJurisdiccion + char(39) + ' )' 
	
	If Exists ( Select Name From Sysobjects Where Name = 'tmpConsumosEstados' and xType = 'U' )
      Drop Table tmpConsumosEstados

	Create Table tmpConsumosEstados
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

	-- GENERAR DETALLE DE LOS CONSUMOS
	Set @sSql = ' Insert Into tmpConsumosEstados (IdEmpresa, Empresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ' + 
		' ClaveSSA, DescripcionSal, PresentacionClaveSSA, ContenidoPaqueteClaveSSA, ' +
		' IdProducto, CodigoEAN, Descripcion, Presentacion, ContenidoPaquete, EsConsignacion, Cantidad, Cajas) ' +
		' Select E.IdEmpresa, space(100) As Empresa, E.IdEstado, space(50) As Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, ' + 
		' P.ClaveSSA, P.DescripcionSal, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA, ' +  
		' L.IdProducto, L.CodigoEAN, P.Descripcion, P.Presentacion, P.ContenidoPaquete, ' + 
		' L.EsConsignacion, Sum(L.CantidadVendida) As Cantidad, Cast(0 As numeric(14,4)) As Cajas ' +		
		' From VentasEnc E (Nolock) ' + 
		' Inner Join VentasDet_Lotes L (Nolock) ' + 
			' On (E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.FolioVenta = L.FolioVenta) ' + 
		' Inner Join vw_Productos_CodigoEAN P (NOLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN  ) ' +
		' Inner Join vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia ) ' + 
		' Where E.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' And E.IdEstado = ' + char(39) + @IdEstado + char(39) +  
		' And Convert( varchar(10), E.FechaRegistro, 120) Between ' + char(39) + @FechaInicio + char(39) + ' And ' + char(39) + @FechaFin + char(39) + 
		' And L.EsConsignacion In ' + @sTipoDisp + '  ' + @sWhereClave + ' ' + @sWhereJuris +
		' Group By E.IdEmpresa, E.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, ' +
		' P.ClaveSSA, P.DescripcionSal, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA, ' +  
		' L.IdProducto, L.CodigoEAN, P.Descripcion, P.Presentacion, P.ContenidoPaquete, L.EsConsignacion '

----	 Print 	@sSql 	  
	Exec(@sSql)	

	Update T Set T.Empresa = E.NombreEmpresa, T.Estado = E.NombreEstado
	From tmpConsumosEstados T (Nolock)
	Inner Join vw_EmpresasEstados E (Nolock) On ( T.IdEmpresa = E.IdEmpresa And T.IdEstado = E.IdEstado )

----	Update T Set T.Farmacia = F.Farmacia, T.IdJurisdiccion = F.IdJurisdiccion, T.Jurisdiccion = F.Jurisdiccion
----	From tmpConsumosEstados T (Nolock)
----	Inner Join vw_Farmacias F (Nolock) On ( T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia )

	if @TipoReporte = 0
		Begin			
			
			-- GENERAR TABLA POR FARMACIA 
			Select 'Jurisdicción' = IdJurisdiccion, 'Id Farmacia' = IdFarmacia, 'Farmacia' = Farmacia, 'Clave SSA' = ClaveSSA, 'Descripción Clave SSA' = DescripcionSal, 
			'Tipo de Dispensación' = Case When EsConsignacion = 0 Then 'VENTA' Else 'CONSIGNACION' End, 
			'Presentación' = PresentacionClaveSSA, 'Envase' = ContenidoPaqueteClaveSSA,
			Cast(Sum(Cantidad) As Int) As Piezas, 
			Cast(( (Sum(Cantidad)) /  ContenidoPaqueteClaveSSA) as numeric(14,4)) As Cajas
			From tmpConsumosEstados (Nolock)
			Group By IdJurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionSal, PresentacionClaveSSA, ContenidoPaqueteClaveSSA, EsConsignacion 
			Order By IdJurisdiccion, IdFarmacia, DescripcionSal, EsConsignacion
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
			From tmpConsumosEstados (Nolock)						
			Order By IdJurisdiccion, IdFarmacia, ClaveSSA, EsConsignacion	
		End

End
Go--#SQL
