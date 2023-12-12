If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_NoSurtido' and xType = 'U' ) 
	Drop table RptAdmonDispensacion_NoSurtido  
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'tmpDatosInicialesLotes' and xType = 'U' )
      Drop Table tmpDatosInicialesLotes
Go--#SQL  

-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_005' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_005 
Go--#SQL 

----	spp_Rpt_Administrativos_005 ''

Create Proc spp_Rpt_Administrativos_005 
(   
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0005', 
	@IdCliente varchar(4) = '*', @IdSubCliente varchar(4) = '*', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2012-09-01', @FechaFinal varchar(10) = '2012-09-15', 
	@TipoInsumo tinyint = 0, @SubFarmacias varchar(200) = ''
)  
With Encryption 
As 
Begin 
Set NoCount On  
Set DateFormat YMD 
Declare @sSql varchar(7500), 
		@sWhere varchar(2500), 
		@sWhereAux varchar(2500),
		@sWhereSubFarmacias varchar(2500), 
		@sFechaValidacion varchar(50)

Declare @iSinAsignar tinyint, 
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

----- Crear Tabla Intermedia 
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
     Farmacia varchar(150) Not Null Default '',
	 IdSubFarmacia varchar(2) Not Null Default '',
     SubFarmacia varchar(50) Not Null Default '',	
     IdColonia varchar(4) Not Null Default '',
     Colonia varchar(50) Not Null Default '',
     Domicilio varchar(100) Not Null Default '',
     Folio varchar(30) Not Null Default '',
     FolioMovtoInv varchar(30) Not Null Default '',
     FechaSistema datetime Null Default getdate(),
     FechaRegistro datetime Null Default getdate(),
     SubTotal numeric(14, 4) Not Null Default 0,
     Descuento numeric(14, 4) Not Null Default 0,
     Iva numeric(14, 4) Not Null Default 0,
     Total numeric(14, 4) Not Null Default 0,
     TipoDeVenta smallint Not Null Default 0,
     StatusVenta varchar(1) Not Null Default '',
     IdProducto varchar(8) Not Null Default '',
     CodigoEAN varchar(30) Not Null Default '', 
     Renglon int Not Null Default 0,
     Cantidad numeric(14, 4) Not Null Default 0,
     PrecioUnitario numeric(14, 4) Not Null Default 0,
     TasaIva numeric(14, 4) Not Null Default 0,
     ImpteIva numeric(14, 4) Not Null Default 0,
     ImporteEAN numeric(14, 4) Null Default 0, 
     EsConsignacion bit Not Null Default 'false', 
     ClaveLote varchar(30) Not Null Default 0, 
     CantidadLote float Null Default 0,       
     ImpteIvaLote numeric(14, 4) Null Default 0,
     ImporteLote numeric(14, 4) Null Default 0,
     PrecioLicitacion numeric(14, 4) Null Default 0, 

----     SubTotalLicitacion_0 numeric(14, 4) Null Default 0, 
----     SubTotalLicitacion numeric(14, 4) Null Default 0,      
----     IvaLicitacion numeric(14, 4) Null Default 0, 
----     TotalLicitacion numeric(14, 4) Null Default 0,      
          
           
     ImporteEAN_Licitado numeric(14, 4) Null Default 0,
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
     NumReceta varchar(50) Null Default '', 
     FechaReceta datetime Not Null Default getdate(),
     IdClaveSSA_Sal varchar(4) Null Default '',
     ClaveSSA varchar(20) Null Default '',
     DescripcionSal varchar(8000) Null Default '',
     DescProducto varchar(8000) Null Default '',
     DescripcionCorta varchar(8000) Null Default '', 
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
     EsCauses tinyint null default 0  
)
----- Crear Tabla Intermedia  

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
	      Set @sWhereAux  = ' and V.IdCliente = ' + char(39) + @IdCliente + char(39) 

	      If @IdSubCliente <> '' and @IdSubCliente <> '*' 
			Set @sWhereAux  = @sWhereAux + ' and V.IdSubCliente = ' + char(39) + @IdSubCliente + char(39) 
	      
	      If @IdPrograma <> '' and @IdPrograma <> '*' 
	         Begin 
			   Set @sWhereAux  = @sWhereAux + ' and V.IdPrograma = ' + char(39) + @IdPrograma + char(39) 
				         
	           If @IdSubPrograma <> '' and @IdSubPrograma <> '*' 
			      Set @sWhereAux  = @sWhereAux + ' and V.IdSubPrograma = ' + char(39) + @IdSubPrograma + char(39) 
	         End  
	   End 

----	If @SubFarmacias <> ''
----	  Begin
----		Set @sWhereSubFarmacias = 'And L.IdSubFarmacia In ( ' + @SubFarmacias + ' ) ' 
----	  End

----------------------- FARMACIAS Y CLAVES 

------- Generar Perifil de Farmacia 
    Select IdEstado, Estado, IdFarmacia, Farmacia, IdCliente, IdClaveSSA, ClaveSSA, DescripcionClave 
    Into #tmpPerfil 
    From vw_CB_CuadroBasico_Farmacias  
    Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and StatusClave = 'A'  	


	Select * 
	Into #vw_Farmacias
	From vw_Farmacias 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	Select * 
	Into #vw_ClavesSSA_Sales 
	From vw_ClavesSSA_Sales 
----------------------- FARMACIAS Y CLAVES 	


--- Armar criterios para la consulta 
	Set @sWhere = @sWhere + @sWhereAux + @sWhereSubFarmacias   
	Set @sSql = ' 		
	Insert 	Into  #tmpDatosInicialesLotes ( IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdFarmacia, Farmacia, 
		IdSubFarmacia, SubFarmacia, IdColonia, Colonia, Domicilio, Folio, FolioMovtoInv, FechaSistema, FechaRegistro, SubTotal, Descuento, Iva, Total, 
		TipoDeVenta, StatusVenta, IdClaveSSA_Sal, ClaveSSA, IdProducto, CodigoEAN, Renglon, Cantidad, PrecioUnitario, TasaIva, ImpteIva, 
		ImporteEAN, ClaveLote, CantidadLote, ImpteIvaLote, ImporteLote, TipoInsumo, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma ) 
	select V.IdEmpresa, space(500) as Empresa, 
		V.IdEstado, F.Estado, F.ClaveRenapo, F.IdMunicipio, F.Municipio, 
		V.IdFarmacia, F.Farmacia, space(2) as IdSubFarmacia, space(2) as Descripcion, F.IdColonia, F.Colonia, F.Domicilio,  
		V.FolioVenta as Folio, V.FolioMovtoInv, 
		V.FechaSistema, V.FechaRegistro, 
		-- V.SubTotal, V.Descuento, V.Iva, V.Total, V.TipoDeVenta, V.Status as StatusVenta, 
		0 as SubTotal, 0 as Descuento, 0 as Iva, 0 as Total, V.TipoDeVenta, V.Status as StatusVenta, 		
		C.IdClaveSSA_Sal, C.ClaveSSA, space(2) as IdProducto, space(2) as CodigoEAN, 0 as Renglon, L.CantidadRequerida as Cantidad, 
		0 as PrecioUnitario, 0 as TasaIva, 0 as ImpteIva, 0 as ImporteEAN, 
		space(2) as ClaveLote, L.CantidadRequerida as CantidadLote, 
		0 as ImpteIvaLote, 
		0 as ImporteLote, 
		0 as TipoInsumo, 
		v.IdCliente, v.IdSubCliente, v.IdPrograma, v.IdSubPrograma   
	From VentasEnc V (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia) 	
	Inner Join VentasEstadisticaClavesDispensadas L (NoLock) 
		On ( V.IdEmpresa = L.IdEmpresa and V.IdEstado = L.IdEstado and V.IdFarmacia = L.IdFarmacia and V.FolioVenta = L.FolioVenta and L.EsCapturada = 1 )  	
	Inner Join #vw_ClavesSSA_Sales C (NoLock) On ( C.IdClaveSSA_Sal = L.IdClaveSSA ) 
	-- Inner Join CatFarmacias_SubFarmacias SF (NoLock) On( L.IdEstado = SF.IdEstado And L.IdFarmacia = SF.IdFarmacia And L.IdSubFarmacia = SF.IdSubFarmacia )
	Inner Join VentasInformacionAdicional IA (NoLock) 
		On ( V.IdEmpresa = IA.IdEmpresa and V.IdEstado = IA.IdEstado and V.IdFarmacia = IA.IdFarmacia and V.FolioVenta = IA.FolioVenta ) ' + 
	@sWhere +  	
	' And Not Exists ' + 
	'	( ' +  
	'		Select *  ' + 
	'		From Vales_EmisionEnc D (NoLock)  ' + 
	'		Inner Join Vales_EmisionDet LL (NoLock)   ' + 
	' 			On ( D.IdEmpresa = LL.IdEmpresa and D.IdEstado = LL.IdEstado and D.IdFarmacia = LL.IdFarmacia and D.FolioVale = LL.FolioVale  ) ' + 
	'		Where V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta  ' + 
	' 			and C.IdClaveSSA_Sal = LL.IdClaveSSA_Sal ' + 			
	'	) ' + 
	' order by V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta' 
 	--Print 	@sSql 	  

/*
 	Exec(@sSql)   
	No mostrar la información de No Surtido.
*/ 



----------------- Excluir claves que no pertenecen al Perfil de la Unidad 
--------- Jesús Díaz 2K121227.1020 
	Update L Set EsCauses = 1  
	From #tmpDatosInicialesLotes L (NoLock) 
	Inner Join #tmpPerfil P (NoLock) On ( L.ClaveSSA = P.ClaveSSA ) 

	Delete From #tmpDatosInicialesLotes Where EsCauses = 0   
----------------- Excluir claves que no pertenecen al Perfil de la Unidad 


/*  	

	Select top 1 * From VentasEstadisticaClavesDispensadas 

	Inner Join Vales_EmisionEnc D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	Inner Join Vales_EmisionDet L (NoLock)  
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVale = L.FolioVale  )  
*/ 		 	
 	
--	Select getdate() as Inicio_Parte02 	
 			     
--	Where 1 = 0 and 
--		  TipoDeVenta = 2 
--		  and v.IdEmpresa = @IdEmpresa and v.IdEstado = @IdEstado and v.IdFarmacia = @IdFarmacia 
--		  and convert(varchar(10), v.FechaRegistro, 120)  between convert(varchar(10), @FechaInicial, 120) and convert(varchar(10), @FechaFinal, 120)  
-- 	order by V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, D.Renglon 


---------------------------------------- CAMBIO   Jesús Díaz 2K120919.1435 
/* 
------------------- 
	Update X Set TipoInsumo = 1 From #tmpDatosInicialesLotes X Where TasaIva = 0  ---- Medicamentos 
	Update X Set TipoInsumo = 2 From #tmpDatosInicialesLotes X Where TasaIva > 0  ---- Material de Curacion 


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
	   
	   
---   spp_Rpt_Administrativos_005  	
	Set @sSql = '' 
	Set @sWhere = ''  -- Where 1 = 1 ' 
*/ 	
---------------------------------------- CAMBIO   Jesús Díaz 2K120919.1435 
	   
	   
---------------------------------------------------------------- 
	if exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_NoSurtido' and xType = 'U' ) 
	   Drop table RptAdmonDispensacion_NoSurtido 

	Select * 
	Into RptAdmonDispensacion_NoSurtido 
	From 
	( 
	Select 0 as IdGrupoPrecios, space(100) as DescripcionGrupoPrecios, 
			2 as EsSeguroPopular, 'NO SEGURO POPULAR' + space(46) as TituloSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 1 as ContenidoPaquete_ClaveSSA, 
			IdProducto, CodigoEAN, EsConsignacion, 
			0 as EsControlado, 
			Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, sum(CantidadLote) as Cantidad, 
			PrecioUnitario, PrecioLicitacion, PrecioLicitacion as PrecioLicitacionUnitario, 
			ImporteEAN, ImporteEAN_Licitado, 

			--- Agregar campos adicionales para Validación y Facturacion 			
			cast(0 as numeric(14, 4)) as SubTotalLicitacion_0, 
			cast(0 as numeric(14, 4)) as SubTotalLicitacion,      
			cast(0 as numeric(14, 4)) as IvaLicitacion, 
			cast(0 as numeric(14, 4)) as TotalLicitacion,      
     			
			--- Agregar campos adicionales para Validación y Facturacion 			
			sum(CantidadLote) as Cantidad__Negado,      			
			cast(0 as numeric(14, 4)) as PrecioLicitacion__Negado,      	
			cast(0 as numeric(14, 4)) as ImporteEAN__Negado,     

			cast(0 as numeric(14, 4)) as SubTotalLicitacion_0__Negado, 
			cast(0 as numeric(14, 4)) as SubTotalLicitacion__Negado,      
			cast(0 as numeric(14, 4)) as IvaLicitacion__Negado, 
			cast(0 as numeric(14, 4)) as TotalLicitacion__Negado,        			
			
			TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area, getdate() as FechaInicial,  getdate() as FechaFinal 
    -- Into RptAdmonDispensacion_NoSurtido 
	From #tmpDatosInicialesLotes (NoLock) 
	Group by IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, EsConsignacion, 
			Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, 
			PrecioUnitario, PrecioLicitacion, 
			ImporteEAN, 
			ImporteEAN_Licitado, TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area 
	) as T 		
	-- Where IdClaveSSA_Sal = '1683' 
 	order by IdEmpresa, IdEstado, IdFarmacia, Folio, Renglon 
 
End 
Go--#SQL 
	
	
	