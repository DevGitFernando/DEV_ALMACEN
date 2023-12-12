-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion' and xType = 'U' ) 
	Drop table RptAdmonDispensacion 
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'tmpDatosInicialesLotes' and xType = 'U' )
      Drop Table tmpDatosInicialesLotes
Go--#SQL  


-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_001' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_001 
Go--#SQL 

Create Proc spp_Rpt_Administrativos_001 
(   
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2201', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2017-05-01', @FechaFinal varchar(10) = '2017-05-01', 
	@TipoInsumo tinyint = 0, @SubFarmacias varchar(200) = '', 
	@OrigenDispensacion tinyint = 0 --  0 ==> Todo | 1 ==> Dispensacion | 2 ==> Vales 
)  
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
		@sSql varchar(max), 
		@sWhere varchar(max), 
		@sWhereAux varchar(max),
		@sWhereSubFarmacias varchar(max), 
		@sFechaValidacion varchar(max), 
		@sFiltro__OrigenDispensacion varchar(max) 

Declare 
		@iSinAsignar tinyint, 
		@iMedicamento tinyint, 
		@iMedicamentoOtros tinyint, 
		@iMatCuracion tinyint, 
		@iDispVenta tinyint, 
		@iDispConsignacion tinyint, 
		@iCriterioFechaValidacion tinyint  
						  
------------------------------------------  
--- Asegurar que el reporte tome valores default 
	Set @IdCliente = IsNull(@IdCliente, '*') 
	Set @IdSubCliente = IsNull(@IdSubCliente, '*') 
	Set @IdPrograma = IsNull(@IdPrograma, '*') 
	Set @IdSubPrograma = IsNull(@IdSubPrograma, '*') 
	Set @TipoDispensacion = IsNull(@TipoDispensacion, 1) 
	Set @OrigenDispensacion = IsNull(@OrigenDispensacion, 0) 	
	

	If @IdCliente = '' 
	   Set @IdCliente = '*'
	
	If @IdSubCliente = '' 
	   Set @IdSubCliente = '*'
	   
	If @IdPrograma = '' 
	   Set @IdPrograma = '*'
	   
	If @IdSubPrograma = '' 
	   Set @IdSubPrograma = '*'	   	   	
------------------------------------------ 

--- Vefificar la información solicitada 
	Set @iMedicamento = 1 
	Set @iMatCuracion = 2 
	
	if @TipoInsumo = 1 
	   Set @iMatCuracion = 1
	
	if @TipoInsumo = 2 
	   Set @iMedicamento = 2 

------------------------------------------ 	   
	Set @iDispConsignacion = 1 
	Set @iDispVenta = 2 

	If @TipoDispensacion = 1 
	   Set @iDispVenta = 1 	

	If @TipoDispensacion = 2 
	   Set @iDispConsignacion = 2  	
	   

--	Select getdate() as Inicio_Parte01

	--------------------------------------------- Crear Tabla Intermedia 
	If Exists ( Select Name From Sysobjects Where Name = '#tmpDatosInicialesLotes' and xType = 'U' )
		  Drop Table #tmpDatosInicialesLotes
      
	Create Table #tmpDatosInicialesLotes
	(
		 IdEmpresa varchar(3) Not Null Default '',
		 Empresa varchar(500) Null Default '',
		 IdEstado varchar(2) Not Null Default '',
		 Estado varchar(50) Not Null Default '',
		 ClaveRenapo varchar(2) Not Null Default '',
		 IdMunicipio varchar(4) Not Null Default '',
		 Municipio varchar(50) Not Null Default '',
		 IdFarmacia varchar(4) Not Null Default '',
		 Farmacia varchar(200) Not Null Default '', 

		 CLUES_Oficial varchar(20) Not Null Default '',          ----  CLUES de la Unidad Médica donde se encuentra la Farmacia 
		 NombrePropio_UMedica varchar(200) Not Null Default '',  ----  Nombre Oicial de la Unidad Médica donde se encuentra la Farmacia 

		 IdSubFarmacia varchar(2) Not Null Default '',
		 SubFarmacia varchar(50) Not Null Default '',	
		 IdColonia varchar(4) Not Null Default '',
		 Colonia varchar(50) Not Null Default '',
		 Domicilio varchar(100) Not Null Default '',
		 Folio varchar(30) Not Null Default '',
		 FolioMovtoInv varchar(30) Not Null Default '',
		 FechaSistema datetime Null Default getdate(),
		 FechaRegistro datetime Null Default getdate(),
		 SubTotal numeric(34, 4) Not Null Default 0,
		 Descuento numeric(34, 4) Not Null Default 0,
		 Iva numeric(34, 4) Not Null Default 0,
		 Total numeric(34, 4) Not Null Default 0,
		 TipoDeVenta smallint Not Null Default 0,
		 StatusVenta varchar(1) Not Null Default '',
		 IdProducto varchar(8) Not Null Default '',
		 CodigoEAN varchar(30) Not Null Default '', 
		 Renglon int Not Null Default 0,

		 Factor numeric(34, 4) Not Null Default 1,
		 Cantidad numeric(34, 4) Not Null Default 0, 
		 Cantidad_A_Cobro numeric(34, 4) Not Null Default 0, 
		 Multiplo numeric(34, 4) Not Null Default 0, 

		 PrecioUnitario numeric(34, 4) Not Null Default 0,
		 TasaIva numeric(34, 4) Not Null Default 0,
		 ImpteIva numeric(34, 4) Not Null Default 0,
		 ImporteEAN numeric(34, 4) Null Default 0, 
		 EsConsignacion bit Not Null Default 'false', 
		 ClaveLote varchar(30) Not Null Default '', 
		 FechaCaducidad varchar(10) Not Null Default '', 
		 CantidadLote numeric(34, 4) Null Default 0,       
		 ImpteIvaLote numeric(34, 4) Null Default 0,
		 ImporteLote numeric(34, 4) Null Default 0,
		 PrecioLicitacion numeric(34, 4) Null Default 0, 

		 ProgramaSubPrograma_ForzaCajas_Habilitado bit Null Default 0, 
		 ProgramaSubPrograma_ForzaCajas bit Null Default 0, 

	----     SubTotalLicitacion_0 numeric(34, 4) Null Default 0, 
	----     SubTotalLicitacion numeric(34, 4) Null Default 0,      
	----     IvaLicitacion numeric(34, 4) Null Default 0, 
	----     TotalLicitacion numeric(34, 4) Null Default 0,      
          
          
		 ImporteEAN_Licitado numeric(34, 4) Null Default 0,
		 TipoInsumo int Not Null Default 0,
		 TipoDeInsumo varchar(100) Null Default '',
		 TipoDispensacion int Not Null Default 0,
		 IdCliente varchar(4) Not Null Default '',
		 NombreCliente varchar(100) Null Default '',
		 IdSubCliente varchar(4) Not Null Default '',
		 NombreSubCliente varchar(100) Null Default '',
		 IdPrograma varchar(4) Not Null Default '',
		 Programa varchar(200) Null Default '',
		 IdSubPrograma varchar(4) Not Null Default '',
		 SubPrograma varchar(200) Null Default '',
		 IdBeneficiario varchar(10) Null Default '',
		 Beneficiario varchar(200) Null Default '',
		 FolioReferencia varchar(20) Null Default '', 
		 CURP varchar(20) Null Default '', 

		 IdEstadoResidencia varchar(2) Null Default '', 
		 EstadoResidencia varchar(100) Null Default '', 
		 ClaveRENAPO_EstadoResidencia varchar(5) Null Default '', 
		 IdTipoDerechoHabiencia varchar(3) Null Default '', 
		 TipoDerechoHabiencia varchar(100) Null Default '', 

		 NumReceta varchar(50) Null Default '', 
		 FechaReceta datetime Not Null Default getdate(), 
     
		 EsRecetaForanea bit Not Null Default 'false', 
		 IdUMedica varchar(10) Not Null Default '', 
		 CLUES_UMedica varchar(20) Not Null Default '',
		 Nombre_UMedica varchar(500) Not Null Default '',     
     
		 IdClaveSSA_Sal varchar(4) Null Default '',
		 ClaveSSA varchar(50) Null Default '',
		 DescripcionSal varchar(8000) Null Default '',
		 DescProducto varchar(8000) Null Default '',
		 DescripcionCorta varchar(8000) Null Default '',

		 IdGrupoTerapeutico varchar(4) Not Null Default '', 
		 GrupoTerapeutico varchar(500) Not Null Default '', 

		 UnidadDeSalida int Not Null Default '',
		 IdMedico varchar(6) Null Default '',
		 Medico varchar(200) Null Default '',
		 IdGrupo int Not Null Default 0,
		 GrupoClaves varchar(50) Null Default '',
		 DescripcionGrupo varchar(300) Null Default '',
		 SubGrupo varchar(6) Null Default '',
		 ClaveSubGrupo varchar(200) Null Default '',
		 DescripcionSubGrupo varchar(300) Null Default '',
		 IdDiagnostico varchar(8) Null Default '',
		 ClaveDiagnostico varchar(6) Null Default '',
		 Diagnostico varchar(500) Null Default '',
		 IdServicio varchar(4) Null Default '',
		 Servicio varchar(100) Null Default '',
		 IdArea varchar(4) Null Default '',
		 Area varchar(100) Null Default '', 
		 UsaPrecioLicitacion tinyint Not Null Default 0,
		 Idlaboratorio Varchar(4) Not Null Default '0000',
		 laboratorio Varchar(100) Not Null Default 'Sin Asignar',
		 IdPais varchar(3) Not Null Default '000', 
		 Pais varchar(100) Not Null Default 'Mexico'
	)
	--------------------------------------------- Crear Tabla Intermedia 

	Set @sSql = '' 
	Set @sWhere = '' 
	Set @sWhereAux = '' 
	Set @sFechaValidacion = ' v.FechaRegistro ' 
	Set @iCriterioFechaValidacion = 1 
	Set @sWhereSubFarmacias = ''

--- Obtener el Tipo de Validacion que maneja la Unidad 
	Select @iCriterioFechaValidacion = dbo.fg_PtoVta_FechaCriterioValidacion(@IdEstado, @IdFarmacia)	

	if @iCriterioFechaValidacion = 2 
	   Set @sFechaValidacion = ' IA.FechaReceta ' 	

	Set @sWhere =  ' Where TipoDeVenta = 2 ' +  
		  ' and v.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) +  ' and v.IdEstado = ' + char(39) + @IdEstado +  char(39) +  ' and v.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 
		  ' and convert(varchar(10), ' + @sFechaValidacion + ', 120)  between convert(varchar(10), ' + char(39) +  @FechaInicial + char(39) + ', 120) and convert(varchar(10), ' + char(39) + @FechaFinal + char(39) + ', 120) '   


--- Aplicar los filtros de requeridos 	
	If @IdCliente <> '' and @IdCliente <> '*' 
	   Begin 
	      Set @sWhereAux  = ' and IdCliente = ' + char(39) + @IdCliente + char(39) 

	      If @IdSubCliente <> '' and @IdSubCliente <> '*' 
			Set @sWhereAux  = @sWhereAux + ' and IdSubCliente = ' + char(39) + @IdSubCliente + char(39) 
	      
	      If @IdPrograma <> '' and @IdPrograma <> '*' 
	         Begin 
			   Set @sWhereAux  = @sWhereAux + ' and IdPrograma = ' + char(39) + @IdPrograma + char(39) 
				         
	           If @IdSubPrograma <> '' and @IdSubPrograma <> '*' 
			      Set @sWhereAux  = @sWhereAux + ' and IdSubPrograma = ' + char(39) + @IdSubPrograma + char(39) 
	         End  
	   End 

	If @SubFarmacias <> ''
	  Begin
		Set @sWhereSubFarmacias = 'And L.IdSubFarmacia In ( ' + @SubFarmacias + ' ) ' 
	  End 

	Set @sFiltro__OrigenDispensacion = ''  
	If @OrigenDispensacion <> 0 
	Begin 
		If @OrigenDispensacion = 1  
			Set @sFiltro__OrigenDispensacion = ' and IA.IdTipoDeDispensacion not in ( ' + char(39) + '07' + char(39) +  ' ) ' 

		If @OrigenDispensacion = 2  
			Set @sFiltro__OrigenDispensacion = ' and IA.IdTipoDeDispensacion in ( ' + char(39) + '07' + char(39) +  ' ) ' 
	End 


-- Armar criterios para la consulta 
	Set @sWhere = @sWhere + @sWhereAux + @sWhereSubFarmacias  + @sFiltro__OrigenDispensacion 
	Set @sSql = ' 		
	Insert 	Into  #tmpDatosInicialesLotes ( IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdFarmacia, Farmacia, CLUES_Oficial, NombrePropio_UMedica, 
		IdSubFarmacia, SubFarmacia, IdColonia, Colonia, Domicilio, Folio, FolioMovtoInv, FechaSistema, FechaRegistro, SubTotal, Descuento, Iva, Total, 
		TipoDeVenta, StatusVenta, IdProducto, CodigoEAN, Renglon, Cantidad, Cantidad_A_Cobro, Multiplo, PrecioUnitario, TasaIva, ImpteIva, 
		ImporteEAN, ClaveLote, FechaCaducidad, CantidadLote, ImpteIvaLote, ImporteLote, TipoInsumo, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdEstadoResidencia, IdTipoDerechoHabiencia ) 
	select V.IdEmpresa, space(500) as Empresa, 
		V.IdEstado, F.Estado, F.ClaveRenapo, F.IdMunicipio, F.Municipio, 
		V.IdFarmacia, F.Farmacia, F.CLUES, F.NombrePropio_UMedica, 
		L.IdSubFarmacia, SF.Descripcion, F.IdColonia, F.Colonia, F.Domicilio,  
		V.FolioVenta as Folio, V.FolioMovtoInv, 
		V.FechaSistema, V.FechaRegistro, 
		V.SubTotal, V.Descuento, V.Iva, V.Total, V.TipoDeVenta, V.Status as StatusVenta, 
		D.IdProducto, D.CodigoEAN, D.Renglon, D.CantidadVendida as Cantidad, D.CantidadVendida as Cantidad_A_Cobro, 1 as Multiplo, 
		D.PrecioUnitario, D.TasaIva, D.ImpteIva, ((D.PrecioUnitario * D.CantidadVendida) + D.ImpteIva) as ImporteEAN, 
		L.ClaveLote, convert(varchar(10), getdate(), 120) as FechaCaducidad, cast(L.CantidadVendida as float) as CantidadLote, 
		( (D.PrecioUnitario * (D.TasaIva/100)) * L.CantidadVendida ) as ImpteIvaLote, 
		( (D.PrecioUnitario * (1 + (D.TasaIva/100))) * L.CantidadVendida ) ImporteLote, 
		(case when V.Iva = 0 then 1 else 2 end) as TipoInsumo, 
		v.IdCliente, v.IdSubCliente, v.IdPrograma, v.IdSubPrograma, IdEstadoResidencia, IdTipoDerechoHabiencia   
	From VentasEnc V (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 	
	Inner Join VentasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 

	--  Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.EsControlado = 1 ) 

	Inner Join VentasDet_Lotes L (NoLock) -- vw_Impresion_Ventas 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join CatFarmacias_SubFarmacias SF (NoLock) On( L.IdEstado = SF.IdEstado And L.IdFarmacia = SF.IdFarmacia And L.IdSubFarmacia = SF.IdSubFarmacia )
	Inner Join VentasInformacionAdicional IA (NoLock) 
		On ( V.IdEmpresa = IA.IdEmpresa and V.IdEstado = IA.IdEstado and V.IdFarmacia = IA.IdFarmacia and V.FolioVenta = IA.FolioVenta ) ' + 
	@sWhere +  	' order by V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, D.Renglon ' 
 	Exec(@sSql) 
  	Print 	@sSql 	  	

--	Select getdate() as Inicio_Parte02 	
 			     
--	Where 1 = 0 and 
--		  TipoDeVenta = 2 
--		  and v.IdEmpresa = @IdEmpresa and v.IdEstado = @IdEstado and v.IdFarmacia = @IdFarmacia 
--		  and convert(varchar(10), v.FechaRegistro, 120)  between convert(varchar(10), @FechaInicial, 120) and convert(varchar(10), @FechaFinal, 120)  
-- 	order by V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, D.Renglon 


------------------- 
/* 
	--- Obsoleto 
	Update X Set TipoInsumo = 1 From #tmpDatosInicialesLotes X Where TasaIva = 0  ---- Medicamentos 
	Update X Set TipoInsumo = 2 From #tmpDatosInicialesLotes X Where TasaIva > 0  ---- Material de Curacion 
*/ 


 	--- Siempre considerar el dato del catalogo 
 	Update D set TipoInsumo = (case when P.TasaIva = 0 then 1 else 2 end), D.Idlaboratorio = P.IdLaboratorio, D.Laboratorio = P.Laboratorio
 	From #tmpDatosInicialesLotes  D 
 	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 



--------------------------------------------------------------------------------------------------------------------------------------- 
---	Determinar el tipo de Inventario 
	Update L Set EsConsignacion = 1 
	From #tmpDatosInicialesLotes L 
	Where ClaveLote Like '%*%' 
  
--- Remover los Lotes segun sea el caso 
	If @TipoDispensacion <> 0 
	   Begin 
	      If @TipoDispensacion = 1 
	      Begin 
	         -- Delete From #tmpDatosInicialesLotes Where ClaveLote Not Like '%*%'  -- Consignacion 
	         Delete From #tmpDatosInicialesLotes Where EsConsignacion = 0  -- Consignacion 	         
	      End    

	      If @TipoDispensacion = 2 
	      Begin 
	         -- Delete From #tmpDatosInicialesLotes Where ClaveLote Like '%*%'  -- Venta 
	         Delete From #tmpDatosInicialesLotes Where EsConsignacion = 1  -- Venta 	         
	      End 
	   End 	

--------------------------------------------------------------------------------------------------------------------------------------- 


	   
	If @TipoInsumo <> 0 
	   Begin 
	      If @TipoInsumo = 1 
	         Begin 
	             -- Delete From #tmpDatosInicialesLotes Where TasaIva <> 0  --- Medicamentos	
	             Delete From #tmpDatosInicialesLotes Where TipoInsumo = 2 
	         End 


	      If @TipoInsumo = 2 
	         Begin 
	             -- Delete From #tmpDatosInicialesLotes Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
	             Delete From #tmpDatosInicialesLotes Where TipoInsumo = 1 
	         End 
	   End 
	   
	   
---   spp_Rpt_Administrativos_001  	
	Set @sSql = '' 
	Set @sWhere = ''  -- Where 1 = 1 ' 
	   
--------------------------------------------------------------------------------------------------------------------------------  
 ----	Agregar caducidades 
	Update R Set FechaCaducidad = convert(varchar(10), C.FechaCaducidad, 120)  
	From #tmpDatosInicialesLotes R 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes C 
		On ( R.IdEmpresa = C.IdEmpresa and R.IdEstado = C.IdEstado and R.IdFarmacia = C.IdFarmacia and R.IdSubFarmacia = C.IdSubFarmacia 
			 and R.IdProducto = C.IdProducto and R.CodigoEAN = C.CodigoEAN and R.ClaveLote = C.ClaveLote )
			 

--------------------------------------------------------------------------------------------------------------------------------  
	------------- Aplicar el dato de factor 

--------------------------------------------------------------------------------------------------------------------------------			 
	if exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion' and xType = 'U' ) 
	   Drop table RptAdmonDispensacion 

	Select * 
	Into RptAdmonDispensacion 
	From 
	( 
	Select	
			0 as IdPerfilAtencion, 0 as IdSubPerfilAtencion, 
			space(100) as PerfilDeAtencion, 0 as IdGrupoPrecios, space(100) as DescripcionGrupoPrecios, 
			2 as EsSeguroPopular, 'NO SEGURO POPULAR' + space(46) as TituloSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, CLUES_Oficial, NombrePropio_UMedica, 
			IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, CURP, 
			IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
			EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, 
			NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			1 as ContenidoPaquete_ClaveSSA, 	
			1 as ContenidoPaquete_ClaveSSA_Licitado, 	
			IdGrupoTerapeutico, GrupoTerapeutico, 
			IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, 
			1 as ContenidoPaquete, 
			0 as EsControlado, 
			Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, 
			Factor, 
			sum(CantidadLote) as Cantidad, 
			sum(CantidadLote) as Cantidad_A_Cobro, cast(1 as numeric(14,4)) as Multiplo, 
			sum(CantidadLote) as PiezasTotales, 
			-- 0 as AgrupadoMenor, 0 as AgrupadoMayor, 0 as PiezasSueltas,  
			
			0 as Agrupacion, 0 as AgrupadoMenor, 0 as AgrupadoMayor, 0 as PiezasSueltas,  
			0 as Agrupacion_Comercial, 0 as AgrupadoMenor_Comercial, 0 as AgrupadoMayor_Comercial, 0 as PiezasSueltas_Comercial,  						
			
			cast(0 as bit) as ProgramaSubPrograma_ForzaCajas, 
			cast(0 as bit) as ProgramaSubPrograma_ForzaCajas_Habilitado, 
			PrecioUnitario, PrecioLicitacion, PrecioLicitacion as PrecioLicitacionUnitario,  
			ImporteEAN, ImporteEAN_Licitado, 

			--- Agregar campos adicionales para Validación y Facturacion 			
			cast(0 as numeric(34, 4)) as SubTotalLicitacion_0, 
			cast(0 as numeric(34, 4)) as SubTotalLicitacion,      
			cast(0 as numeric(34, 4)) as IvaLicitacion, 
			cast(0 as numeric(34, 4)) as TotalLicitacion,      
     			
			
			--- Agregar campos adicionales para Validación y Facturacion 			
			0 as Cantidad__Negado,      	
			cast(0 as numeric(34, 4)) as PrecioLicitacion__Negado,      	
			cast(0 as numeric(34, 4)) as ImporteEAN__Negado,      	
											
			cast(0 as numeric(34, 4)) as SubTotalLicitacion_0__Negado, 
			cast(0 as numeric(34, 4)) as SubTotalLicitacion__Negado,      
			cast(0 as numeric(34, 4)) as IvaLicitacion__Negado, 
			cast(0 as numeric(34, 4)) as TotalLicitacion__Negado,      			
			
			
			TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area,
			Idlaboratorio, Laboratorio, Idpais, Pais,
			getdate() as FechaInicial,  getdate() as FechaFinal 
    -- Into RptAdmonDispensacion 
	From #tmpDatosInicialesLotes (NoLock) 
	Group by IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, CLUES_Oficial, NombrePropio_UMedica, 
			IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, CURP, 
			IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
			EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, 
			NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdGrupoTerapeutico, GrupoTerapeutico, 
			IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, 
			Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, Factor, 
			PrecioUnitario, PrecioLicitacion, 
			ImporteEAN, 
			ImporteEAN_Licitado, TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area,
			Idlaboratorio, Laboratorio, Idpais, Pais 
	) as T 		
	-- Where IdClaveSSA_Sal = '1683' 
 	order by IdEmpresa, IdEstado, IdFarmacia, Folio, Renglon 
 	

 
 
End 
Go--#SQL 
	
	
	