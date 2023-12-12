If Exists ( select * from sysobjects (nolock) Where Name = 'tmpRptAdmonDispensacion' and xType = 'U' ) 
	Drop table tmpRptAdmonDispensacion 
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
(   @IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '10', @IdFarmacia varchar(4) = '0002', 
	@IdCliente varchar(4) = '*', @IdSubCliente varchar(4) = '*', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-06-30', 
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
     UsaPrecioLicitacion tinyint Not Null Default 0 
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

-- Armar criterios para la consulta 
	Set @sWhere = @sWhere + @sWhereAux + @sWhereSubFarmacias   
	Set @sSql = ' 		
	Insert 	Into  #tmpDatosInicialesLotes ( IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdFarmacia, Farmacia, 
		IdSubFarmacia, SubFarmacia, IdColonia, Colonia, Domicilio, Folio, FolioMovtoInv, FechaSistema, FechaRegistro, SubTotal, Descuento, Iva, Total, 
		TipoDeVenta, StatusVenta, IdProducto, CodigoEAN, Renglon, Cantidad, PrecioUnitario, TasaIva, ImpteIva, 
		ImporteEAN, ClaveLote, CantidadLote, ImpteIvaLote, ImporteLote, TipoInsumo, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma ) 
	select V.IdEmpresa, space(500) as Empresa, 
		V.IdEstado, F.Estado, F.ClaveRenapo, F.IdMunicipio, F.Municipio, 
		V.IdFarmacia, F.Farmacia, L.IdSubFarmacia, SF.Descripcion, F.IdColonia, F.Colonia, F.Domicilio,  
		V.FolioVenta as Folio, V.FolioMovtoInv, 
		V.FechaSistema, V.FechaRegistro, 
		V.SubTotal, V.Descuento, V.Iva, V.Total, V.TipoDeVenta, V.Status as StatusVenta, 
		D.IdProducto, D.CodigoEAN, D.Renglon, D.CantidadVendida as Cantidad, 
		D.PrecioUnitario, D.TasaIva, D.ImpteIva, ((D.PrecioUnitario * D.CantidadVendida) + D.ImpteIva) as ImporteEAN, 
		L.ClaveLote, cast(L.CantidadVendida as float)as CantidadLote, 
		( (D.PrecioUnitario * (D.TasaIva/100)) * L.CantidadVendida ) as ImpteIvaLote, 
		( (D.PrecioUnitario * (1 + (D.TasaIva/100))) * L.CantidadVendida ) ImporteLote, 
		(case when V.Iva = 0 then 1 else 2 end) as TipoInsumo, 
		v.IdCliente, v.IdSubCliente, v.IdPrograma, v.IdSubPrograma   
	From VentasEnc V (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia) 	
	Inner Join VentasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 

	--  Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.EsControlado = 1 ) 

	Inner Join VentasDet_Lotes L (NoLock) -- vw_Impresion_Ventas 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join CatFarmacias_SubFarmacias SF(NoLock) On( L.IdEstado = SF.IdEstado And L.IdFarmacia = SF.IdFarmacia And L.IdSubFarmacia = SF.IdSubFarmacia )
	Inner Join VentasInformacionAdicional IA (NoLock) 
		On ( V.IdEmpresa = IA.IdEmpresa and V.IdEstado = IA.IdEstado and V.IdFarmacia = IA.IdFarmacia and V.FolioVenta = IA.FolioVenta ) ' + 
	@sWhere +  	' order by V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, D.Renglon ' 
 	-- Print 	@sSql 	  
 	Exec(@sSql) 
 	
--	Select getdate() as Inicio_Parte02 	
 			     
--	Where 1 = 0 and 
--		  TipoDeVenta = 2 
--		  and v.IdEmpresa = @IdEmpresa and v.IdEstado = @IdEstado and v.IdFarmacia = @IdFarmacia 
--		  and convert(varchar(10), v.FechaRegistro, 120)  between convert(varchar(10), @FechaInicial, 120) and convert(varchar(10), @FechaFinal, 120)  
-- 	order by V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, D.Renglon 


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
	   
	   
---   spp_Rpt_Administrativos_001  	
	Set @sSql = '' 
	Set @sWhere = ''  -- Where 1 = 1 ' 
	   
---------------------------------------------------------------- 
	if exists ( select * from sysobjects (nolock) Where Name = 'tmpRptAdmonDispensacion' and xType = 'U' ) 
	   Drop table tmpRptAdmonDispensacion 

	Select * 
	Into tmpRptAdmonDispensacion 
	From 
	( 
	Select 0 as IdGrupoPrecios, space(100) as DescripcionGrupoPrecios, 
			2 as EsSeguroPopular, 'NO SEGURO POPULAR' + space(46) as TituloSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, EsConsignacion, 
			0 as EsControlado, 
			Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, sum(CantidadLote) as Cantidad, 
			PrecioUnitario, PrecioLicitacion, 
			ImporteEAN, ImporteEAN_Licitado, 

			--- Agregar campos adicionales para Validación y Facturacion 			
			cast(0 as numeric(14, 4)) as SubTotalLicitacion_0, 
			cast(0 as numeric(14, 4)) as SubTotalLicitacion,      
			cast(0 as numeric(14, 4)) as IvaLicitacion, 
			cast(0 as numeric(14, 4)) as TotalLicitacion,      
     			
			
			--- Agregar campos adicionales para Validación y Facturacion 			
			0 as Cantidad__Negado,      	
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
    -- Into tmpRptAdmonDispensacion 
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
 	
 	

	Set @sSql = ' 
		Insert Into tmpRptAdmonDispensacion ( IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, Cantidad, 
			PrecioLicitacion, 
			ImporteEAN, 
			ImporteEAN_Licitado, TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal   )	
		Select Distinct 0, space(100) as DescripcionGrupoPrecios, 0 as EsSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, sum(CantidadLote) as Cantidad, 
			PrecioLicitacion, 
			ImporteEAN, 
			ImporteEAN_Licitado, TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area, getdate(), getdate() ' + char(13) + 				    
		'	From #tmpDatosInicialesLotes (noLock) ' + char(13) + 
		' ' + @sWhere + char(13) + 			
		    ' Group by ' + char(13) + 
' IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, 
			PrecioLicitacion, 
			ImporteEAN, 
			ImporteEAN_Licitado, TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area'  
--	Exec(@sSql)  --- NO HABILITAR 
--	Select getdate() as Inicio_Parte03 
 

 
End 
Go--#SQL 
	
	
	