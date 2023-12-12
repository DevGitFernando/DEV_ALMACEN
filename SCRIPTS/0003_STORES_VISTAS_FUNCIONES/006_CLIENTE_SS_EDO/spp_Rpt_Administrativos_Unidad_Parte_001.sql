If exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_Unidad' and xType = 'U' ) 
	Drop table RptAdmonDispensacion_Unidad 
Go--#SQL

If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_Unidad_Parte_001' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Unidad_Parte_001 
Go--#SQL


Create Proc spp_Rpt_Administrativos_Unidad_Parte_001 
(   @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0010',
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-01-10', @TipoInsumo tinyint = 0, 
	@IdBeneficiario varchar(8) = '*', @IdMedico varchar(6) = '*', @ClaveSSA varchar(50) = '*', @Folio varchar(8) = '*' 
)  
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sSql varchar(7500), 
		@sWhere varchar(2500), 
		@sWhereAux varchar(2500), 
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
	Set @IdBeneficiario = IsNull(@IdBeneficiario, '*') 
	Set @IdMedico = IsNull(@IdMedico, '*') 
	Set @ClaveSSA = IsNull(@ClaveSSA, '*') 
	Set @Folio = IsNull(@Folio, '*') 
	
--	Set @IdFarmacia = '0001' 

	If @IdCliente = '' 
	   Set @IdCliente = '*'
	
	If @IdSubCliente = '' 
	   Set @IdSubCliente = '*'
	   
	If @IdPrograma = '' 
	   Set @IdPrograma = '*'
	   
	If @IdSubPrograma = '' 
	   Set @IdSubPrograma = '*'	

	If @IdBeneficiario = '' 
	   Set @IdBeneficiario = '*'	

	If @IdMedico = '' 
	   Set @IdMedico = '*'	

	If @ClaveSSA = '' 
	   Set @ClaveSSA = '*'	

	If @Folio = '' 
	   Set @Folio = '*'	 
  	   	
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
	   


----- Crear Tabla Intermedia 
If Exists ( Select Name From Sysobjects Where Name = 'tmpDatosInicialesLotes_Unidad' and xType = 'U' )
      Drop Table tmpDatosInicialesLotes_Unidad
      
Create Table tmpDatosInicialesLotes_Unidad
(
     IdEstado varchar(2) Not Null Default '',
     Estado varchar(50) Not Null Default '',
     ClaveRenapo varchar(2) Not Null Default '',
     IdMunicipio varchar(4) Not Null Default '',
     Municipio varchar(50) Not Null Default '',
     IdFarmacia varchar(4) Not Null Default '',
     Farmacia varchar(50) Not Null Default '',
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
     ImporteEAN numeric(30, 8) Null Default 0,
     ClaveLote varchar(30) Not Null Default 0,
     CantidadLote float Null Default 0,
     ImpteIvaLote numeric(38, 6) Null Default 0,
     ImporteLote numeric(38, 6) Null Default 0,
     PrecioLicitacion numeric(14,4) Null Default 0,
     ImporteEAN_Licitado float Null Default 0,
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
     Beneficiario varchar(100) Null Default '',
     FolioReferencia varchar(20) Null Default '',
     NumReceta varchar(50) Null Default '', 
     FechaReceta datetime Not Null Default getdate(),
     IdClaveSSA_Sal varchar(4) Null Default '',
     ClaveSSA varchar(20) Null Default '',
     DescripcionSal varchar(8000) Null Default '',
     DescProducto varchar(400) Null Default '',
     DescripcionCorta varchar(400) Null Default '',
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

--- Obtener el Tipo de Validacion que maneja la Unidad 
--	Select @iCriterioFechaValidacion = dbo.fg_PtoVta_FechaCriterioValidacion(@IdEstado, @IdFarmacia)	

	if @iCriterioFechaValidacion = 2 
	   Set @sFechaValidacion = ' IA.FechaReceta ' 	

	Set @sWhere =  ' Where TipoDeVenta = 2 ' +  
		  ' and v.IdEstado = ' + char(39) + @IdEstado +  char(39) +  ' and v.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 
		  ' and convert(varchar(10), ' + @sFechaValidacion + ', 120)  between convert(varchar(10), ' + char(39) +  @FechaInicial + char(39) + ', 120) and convert(varchar(10), ' + char(39) + @FechaFinal + char(39) + ', 120) ' + 
		  '  '-- and v.IdFarmacia IN ( Select IdFarmacia From CTE_FarmaciasProcesar CTE(NoLock) Where  CTE.IdEstado = V.IdEstado And CTE.IdFarmacia = V.IdFarmacia ) ' 


--- Aplicar los filtros de requeridos 	
	If @IdCliente <> '' and @IdCliente <> '*' 
	   Begin 
	      Set @sWhereAux  = ' and IdCliente = ' + char(39) + @IdCliente + char(39) 

	      If @IdSubCliente <> '' and @IdSubCliente <> '*' 
			Begin
			  Set @sWhereAux  = @sWhereAux + ' and IdSubCliente = ' + char(39) + @IdSubCliente + char(39) 
			  
			  If @IdBeneficiario <> '' and @IdBeneficiario <> '*' 
				Set @sWhereAux  = @sWhereAux + ' and IdBeneficiario = ' + char(39) + @IdBeneficiario + char(39) 
			End
	      
	      If @IdPrograma <> '' and @IdPrograma <> '*' 
	         Begin 
			   Set @sWhereAux  = @sWhereAux + ' and IdPrograma = ' + char(39) + @IdPrograma + char(39) 
				         
	           If @IdSubPrograma <> '' and @IdSubPrograma <> '*' 
			      Set @sWhereAux  = @sWhereAux + ' and IdSubPrograma = ' + char(39) + @IdSubPrograma + char(39) 
	         End  
	   End 

	If @IdMedico <> '' and @IdMedico <> '*' 
	  Begin 
		Set @sWhereAux  = ' and IdMedico = ' + char(39) + @IdMedico + char(39) 
	  End

	If @ClaveSSA <> '' and @ClaveSSA <> '*' 
	  Begin 
		Set @sWhereAux  = ' and D.IdProducto In ( Select IdProducto From vw_Productos(NoLock) Where ClaveSSA = ' + char(39) + @ClaveSSA + char(39) + ' ) ' 
	  End

	If @Folio <> '' and @Folio <> '*' 
	  Begin 
		Set @sWhereAux  = ' and V.FolioVenta = ' + char(39) + @Folio + char(39) 
	  End

-- Armar criterios para la consulta 
	Set @sWhere = @sWhere + @sWhereAux   
	Set @sSql = ' 		
	Insert 	Into  tmpDatosInicialesLotes_Unidad ( IdEstado, -- Estado, ClaveRenapo, IdMunicipio, Municipio, 
		IdFarmacia, 
		-- Farmacia, IdColonia, Colonia, Domicilio, 
		Folio, FolioMovtoInv, FechaSistema, FechaRegistro, SubTotal, Descuento, Iva, Total, 
		TipoDeVenta, StatusVenta, IdProducto, CodigoEAN, Renglon, Cantidad, PrecioUnitario, TasaIva, ImpteIva, 
		ImporteEAN, ClaveLote, CantidadLote, ImpteIvaLote, ImporteLote, TipoInsumo, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma ) 
	select V.IdEstado, -- F.Estado, F.ClaveRenapo, F.IdMunicipio, F.Municipio, 
		V.IdFarmacia, 
		-- F.Farmacia, F.IdColonia, F.Colonia, F.Domicilio,  
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
	-- Inner Join CatFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia) 	
	Inner Join VentasDet D (NoLock) 
		On ( V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	Inner Join VentasDet_Lotes L (NoLock) -- vw_Impresion_Ventas 
		On ( D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join VentasInformacionAdicional IA (NoLock) 
		On ( V.IdEstado = IA.IdEstado and V.IdFarmacia = IA.IdFarmacia and V.FolioVenta = IA.FolioVenta ) ' + 
	@sWhere +  	' order by V.IdEstado, V.IdFarmacia, V.FolioVenta, D.Renglon ' 
 	-- Print 	@sSql 	  
	Exec(@sSql) 
 	

	Update V Set Estado = F.Estado, ClaveRenapo = F.ClaveRenapo, 
		IdMunicipio = F.IdMunicipio, Municipio = F.Municipio, 
		Farmacia = F.Farmacia, IdColonia = F.IdColonia, Colonia = F.Colonia, Domicilio = F.Domicilio 
	From tmpDatosInicialesLotes_Unidad V (NoLock) 
 	Inner Join vw_Farmacias	F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia)  			     

  
--- Remover los Lotes segun sea el caso 
	If @TipoDispensacion <> 0 
	   Begin 
	      If @TipoDispensacion = 1 
	         Delete From tmpDatosInicialesLotes_Unidad Where ClaveLote Not Like '%*%'  -- Consignacion 

	      If @TipoDispensacion = 2
	         Delete From tmpDatosInicialesLotes_Unidad Where ClaveLote Like '%*%'  -- Venta 
	   End 	
	   
	If @TipoInsumo <> 0 
	   Begin 
	      If @TipoInsumo = 1 
	         Delete From tmpDatosInicialesLotes_Unidad Where TasaIva <> 0  --- Medicamentos 

	      If @TipoInsumo = 2
	         Delete From tmpDatosInicialesLotes_Unidad Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
	   End
  
	   
---   spp_Rpt_Administrativos_Unidad_Parte_001  	
	Set @sSql = '' 
	Set @sWhere = ''  -- Where 1 = 1 ' 


---------------------------------------------------------------- 
	if exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_Unidad' and xType = 'U' ) 
	   Drop table RptAdmonDispensacion_Unidad 

	Select * 
	Into RptAdmonDispensacion_Unidad 
	From 
	( 
	Select 0 as IdGrupoPrecios, space(100) as DescripcionGrupoPrecios, 
			2 as EsSeguroPopular, 'OTROS' + space(46) as TituloSeguroPopular, 
			IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, sum(CantidadLote) as Cantidad, 
			PrecioUnitario, PrecioLicitacion, 
			ImporteEAN, 
			ImporteEAN_Licitado, TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area, getdate() as FechaInicial,  getdate() as FechaFinal 
    -- Into RptAdmonDispensacion_Unidad 
	From tmpDatosInicialesLotes_Unidad (NoLock) 
	Group by IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, 
			PrecioUnitario, PrecioLicitacion, 
			ImporteEAN, 
			ImporteEAN_Licitado, TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area 
	) as T 		
 	order by IdEstado, IdFarmacia, Folio, Renglon 
  	
 	

	Set @sSql = ' 
		Insert Into RptAdmonDispensacion_Unidad ( IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, 
			IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
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
			IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
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
		'	From tmpDatosInicialesLotes_Unidad (noLock) ' + char(13) + 
		' ' + @sWhere + char(13) + 			
		    ' Group by ' + char(13) + 
			' IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
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
 
 

 
End 
Go--#SQL	
	
	