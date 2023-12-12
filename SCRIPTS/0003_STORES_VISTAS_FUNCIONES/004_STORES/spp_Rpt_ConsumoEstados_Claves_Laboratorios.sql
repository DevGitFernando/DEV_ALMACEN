-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_ConsumoEstados_Claves_Laboratorios' And xType = 'P' ) 
	Drop Proc spp_Rpt_ConsumoEstados_Claves_Laboratorios
Go--#SQL

--	Exec spp_Rpt_ConsumoEstados_Claves_Laboratorios '001', '11', '2013-01-01', '2013-12-31', 0, 'MERCK'   
 
Create Procedure spp_Rpt_ConsumoEstados_Claves_Laboratorios 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
	@FechaInicio varchar(10) = '2016-12-01', @FechaFin varchar(10) = '2016-12-31', 
	@TipoReporte int = 0, @IdLaboratorio varchar(max) = 'FERRING'
) 
With Encryption 
As 
Begin 
	Declare @iVenta int, @iConsignacion int,
			@sSql varchar(max), @sCampos varchar(max), 
			@sTabla varchar(max),			
			@sWhereLab varchar(max)

	
	Set @iVenta = 0 
	Set @iConsignacion = 1	
	Set @sSql = ''
	Set @sCampos = ''
	
	Set @sTabla = '#tmpConsumosEstados'
	Set @sWhereLab = ''
	
		
	
	Set @sWhereLab = ' ( Select IdLaboratorio From CatLaboratorios Where Descripcion like ' + char(39) + '%' + @IdLaboratorio + '%' + char(39) + ')'
	
	
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

		IdCliente varchar(4) Not Null Default '',
		IdSubCliente varchar(4) Not Null Default '',		
		IdBeneficiario varchar(10) Null Default '', 
		Beneficiario varchar(200) Null Default '', 
		
		IdLaboratorio varchar(4) Not Null Default '',
		Laboratorio varchar(100) Not Null Default '',
		ClaveSSA varchar(20) Null Default '',
		DescripcionSal varchar(8000) Null Default '',
		PresentacionClaveSSA varchar(100) Null Default '',
		ContenidoPaqueteClaveSSA int Null Default 0,
		IdProducto varchar(8) Not Null Default '',
		CodigoEAN varchar(30) Not Null Default '',
		Descripcion varchar(400) Null Default '',
		Presentacion varchar(100) Null Default '',
		ContenidoPaquete int Null Default 0,
		EsConsignacion bit Not Null Default 0,
		Año int Not Null Default 0,
		Mes int Not Null Default 0,
		Cantidad numeric(14, 4) Not Null Default 0,	
		Cajas numeric(14, 4) Not Null Default 0
	)

------------------------ Obtener resumens 
	Select * 
	Into #vw_Productos_CodigoEAN
	From vw_Productos_CodigoEAN 
	Where 1 = 0 
	
	Set @sSql = 'Insert Into #vw_Productos_CodigoEAN ' + char(13) + char(10) +
				'Select * ' + char(13) + char(10) + 
				'From vw_Productos_CodigoEAN P (NoLock) ' + char(13) + char(10) +  
				'Where P.IdLaboratorio In ' + @sWhereLab + char(13) + char(10) 
	Exec(@sSql) 
	Print @sSql  
	
	Select * 
	Into #vw_Farmacias
	From vw_Farmacias
	
	
	Select * 
	Into #vw_Farmacias_Farmacias
	From vw_Farmacias F
	Where F.IdEstado = @IdEstado And F.EsAlmacen = 0
	
	
	Select * 
	Into #vw_Farmacias_Almacen
	From vw_Farmacias F
	Where F.IdEstado = @IdEstado And F.EsAlmacen = 1
	
	Select * 
	Into #vw_Empresas 
	From vw_Empresas
	
------------------------ Obtener resumens 



---------------------------------- GENERAR DETALLE DE LOS CONSUMOS    
--------------------------	 Farmacias  
	Insert Into #tmpConsumosEstados 
		(
			IdEmpresa, Empresa, IdEstado, IdFarmacia,  
			IdProducto, CodigoEAN, EsConsignacion, Año, Mes, Cantidad, Cajas  
		 )  
		 Select
				E.IdEmpresa, space(100) As Empresa, E.IdEstado, E.IdFarmacia, 
			L.IdProducto, L.CodigoEAN,
			L.EsConsignacion, datepart(YYYY, FechaRegistro) as Año, datepart(MM, FechaRegistro) as Mes, Sum(L.CantidadVendida) As Cantidad, Cast(0 As numeric(14,4)) As Cajas
		 From VentasEnc E (Nolock)  
		 Inner Join VentasDet_Lotes L (Nolock)  
			On ( E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.FolioVenta = L.FolioVenta )  
		 Inner Join #vw_Productos_CodigoEAN P (NOLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN  )  
		 Inner Join #vw_Farmacias_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia )  
		 Where Convert( varchar(10), E.FechaRegistro, 120) Between   @FechaInicio   And   @FechaFin  
		--	And P.IdLaboratorio In  + @sWhereLab 
		-- and P.IdClaveSSA_Sal =   0694  
		 Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia,
				  L.IdProducto, L.CodigoEAN, L.EsConsignacion, FechaRegistro    
	--Print @sSql 
	--Exec(@sSql)
	
	
--------------------------	 Almacenes  	
	Insert Into #tmpConsumosEstados  
		 (  
			IdEmpresa, Empresa, IdEstado, IdFarmacia,  
			IdBeneficiario, IdCliente, IdSubCliente, 
			IdProducto, CodigoEAN, EsConsignacion, Año, Mes, Cantidad, Cajas
		 )  
		 Select E.IdEmpresa, space(100) As Empresa, E.IdEstado, E.IdFarmacia,
			I.IdBeneficiario, E.IdCliente, E.IdSubCliente,  
			L.IdProducto, L.CodigoEAN,
			L.EsConsignacion, datepart(YYYY, FechaRegistro) as Año, datepart(MM, FechaRegistro) as Mes, Sum(L.CantidadVendida) As Cantidad, Cast(0 As numeric(14,4)) As Cajas
		 From VentasEnc E (Nolock)  
		 Inner Join VentasDet_Lotes L (Nolock)  
			On ( E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.FolioVenta = L.FolioVenta )  
		 Inner Join VentasInformacionAdicional I (Nolock)  
			On ( E.IdEmpresa = I.IdEmpresa And E.IdEstado = I.IdEstado And E.IdFarmacia = I.IdFarmacia And E.FolioVenta = I.FolioVenta )  		
		 Inner Join #vw_Productos_CodigoEAN P (NOLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN  )  
		 Inner Join #vw_Farmacias_Almacen F (Nolock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia and F.EsAlmacen = 1 )  
		 Where Convert( varchar(10), E.FechaRegistro, 120) Between   @FechaInicio   And   @FechaFin  
		--	And P.IdLaboratorio In  + @sWhereLab 
		-- and P.IdClaveSSA_Sal =   0694  
		 Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia,
			I.IdBeneficiario, E.IdCliente, E.IdSubCliente,
			L.IdProducto, L.CodigoEAN, L.EsConsignacion, FechaRegistro  
	
	
--		spp_Rpt_ConsumoEstados_Claves_Laboratorios  	
		  
---------------------------------- GENERAR DETALLE DE LOS CONSUMOS
	Delete T
	From #tmpConsumosEstados T (Nolock)
	Where T.IdEmpresa <> @IdEmpresa

	Update T Set T.Empresa = E.Empresa
	From #tmpConsumosEstados T (Nolock)
	Inner Join #vw_Empresas E (Nolock) On ( T.IdEmpresa = E.IdEmpresa )

	Update E Set Beneficiario = F.Farmacia
	From #tmpConsumosEstados E (NoLock) 
	Inner Join #vw_Farmacias_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia )

	Update E Set Beneficiario = (F.ApPaterno +   + F.ApMaterno +   + F.Nombre) 
	From #tmpConsumosEstados E (NoLock) 
	Inner Join CatBeneficiarios F (Nolock) 
		On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia 
			and E.IdCliente = F.IdCliente and E.IdSubCliente = F.IdSubCliente and E.IdBeneficiario = F.IdBeneficiario ) 
	Where Beneficiario =  ''
	
	
	Update E
	Set
		IdLaboratorio = P.IdLaboratorio, E.Laboratorio = P.Laboratorio, ClaveSSA = P.ClaveSSA, DescripcionSal = P.DescripcionSal,
		PresentacionClaveSSA = P.Presentacion_ClaveSSA, ContenidoPaqueteClaveSSA = P.ContenidoPaquete_ClaveSSA,
		Descripcion =  P.Descripcion, Presentacion = P.Presentacion, ContenidoPaquete = P.ContenidoPaquete
	From #tmpConsumosEstados E (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NOLock) On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN  ) 




	Update T Set T.Estado = F.Estado, T.Farmacia = F.Farmacia, T.IdJurisdiccion = F.IdJurisdiccion, T.Jurisdiccion = F.Jurisdiccion
	From #tmpConsumosEstados T (Nolock) 
	Inner Join #vw_Farmacias F (Nolock) On ( T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia )

	if @TipoReporte = 0 
		Begin 			
			-- GENERAR TABLA POR FARMACIA 
			Select 
				IdEstado, Estado, 'Jurisdicción' = IdJurisdiccion, 'Id Farmacia' = IdFarmacia, 'Farmacia' = Farmacia,
				'Beneficiario' = Beneficiario, 			
				IdLaboratorio, Laboratorio, 
				'Clave SSA' = ClaveSSA, 'Descripción Clave SSA' = DescripcionSal, 
				'Tipo de Dispensación' = Case When EsConsignacion = 0 Then 'VENTA' Else 'CONSIGNACION' End, 
				'Presentación' = PresentacionClaveSSA, 'Envase' = ContenidoPaqueteClaveSSA,
				Año, Mes,
				Cast(Sum(Cantidad) As Int) As Piezas, 
				Cast(( (Sum(Cantidad)) /  ContenidoPaqueteClaveSSA) as numeric(14,4)) As Cajas
			From #tmpConsumosEstados (Nolock)
			Group By 
				IdEstado, Estado, IdJurisdiccion, IdFarmacia, Farmacia, Beneficiario, IdLaboratorio, Laboratorio, 
				ClaveSSA, DescripcionSal, PresentacionClaveSSA, ContenidoPaqueteClaveSSA, EsConsignacion, Año, Mes 
			Order By DescripcionSal, EsConsignacion, IdJurisdiccion, IdFarmacia 
		End 
	Else 
		Begin 			
			-- GENERAR TABLA DE SALIDA DETALLADO POR PRODUCTO -- FARMACIA
			Select 
				IdEstado, Estado, 'Jurisdicción' = IdJurisdiccion, 'Id Farmacia' = IdFarmacia, 'Farmacia' = Farmacia, 
				'Beneficiario' = Beneficiario, 
				IdLaboratorio, Laboratorio, 
				'Clave SSA' = ClaveSSA, 'Producto' = IdProducto, 
				'Codigo EAN' = CodigoEAN, 'Descripción' = Descripcion,   
				'Tipo de Dispensación' = Case When EsConsignacion = 0 Then 'VENTA' Else 'CONSIGNACION' End, 
				'Presentación' = Presentacion, 'Envase' = ContenidoPaquete,
				Año, Mes,
				Cast(Cantidad As Int) As Piezas, 
				Cast(( Cantidad /  ContenidoPaquete) as numeric(14,4)) As Cajas  
			From #tmpConsumosEstados (Nolock)	
			Order By ClaveSSA, EsConsignacion, IdJurisdiccion, IdFarmacia 	
		End

End
Go--#SQL

	
	
	