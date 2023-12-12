If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_DesviacionDispensacion' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_DesviacionDispensacion 
Go--#SQL

Create Proc spp_Rpt_Administrativos_DesviacionDispensacion ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0002', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 1, @FechaInicial varchar(10) = '2009-10-01', @FechaFinal varchar(10) = '2009-10-01', @TipoInsumo tinyint = 0 
	) 
With Encryption 	
As 
Begin 
Set NoCount On 
Declare @sSql varchar(7500), 
		@sWhere varchar(500)   


--	Set @FechaInicial = IsNull(@FechaInicial, getdate()-10)
--	Set @FechaFinal = IsNull(@FechaFinal, getdate())	

--	If Exists ( Select * From sysobjects (nolock) Where Name = 'tmpRptAdministrativos' and xType = 'U' ) 
--	   Drop table #tmpDatosIniciales 
		

	--- Solo tomar la Ventas de Credito 
	Select 
		v.IdEmpresa, v.Empresa, v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.Folio, v.FechaSistema, v.FechaRegistro, 
		-- v.IdCaja, 
		v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		v.IdClaveSSA_Sal, v.ClaveSSA, v.DescripcionSal, 
		v.IdProducto, v.CodigoEAN, v.DescProducto, v.DescripcionCorta, v.UnidadDeSalida, 
		v.TasaIva, v.Cantidad, v.Costo, v.Importe, (v.Cantidad * v.Importe) as ImporteEAN  
	Into #tmpDatosIniciales 
	From vw_Impresion_Ventas v (NoLock) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.FolioVenta ) 
	Inner Join vw_Servicios_Areas S (NoLock)  On ( I.IdServicio = S.IdServicio and I.IdArea = S.IdArea ) 
	Inner Join vw_Medicos M (NoLock) On ( I.IdEstado = M.IdEstado and I.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico ) 
	Inner Join vw_CIE10_Diagnosticos D (NoLock) On ( I.IdDiagnostico = D.ClaveDiagnostico )
	Left Join vw_Beneficiarios B (NoLock) 
		On ( I.IdEstado = B.IdEstado and I.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente 
			 and I.IdBeneficiario = B.IdBeneficiario ) 	
	Where TipoDeVenta = 2 and 
		  v.IdEmpresa = @IdEmpresa and v.IdEstado = @IdEstado and v.IdFarmacia = @IdFarmacia and 
		  convert(varchar(10), v.FechaRegistro, 120)  between convert(varchar(10), @FechaInicial, 120) and convert(varchar(10), @FechaFinal, 120)  
 

--- Proceso de Informacion  
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, -- ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
		IdPersonal, NombrePersonal, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		IdProducto, CodigoEAN, DescProducto, DescripcionCorta, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, 
		datepart(yy, FechaRegistro) as Año, datepart(mm, FechaRegistro) as Mes  
	Into #tmpDatosFinales  	
	From #tmpDatosIniciales (NoLock) 
	Where 1 = 0 


	Set @sSql = '' 
	Set @sWhere = ' Where 1 = 1 ' 
	
	If @IdCliente <> '' and @IdCliente <> '*' 
	   Begin 
	       Set @sWhere = @sWhere + ' and IdCliente = ' + char(39) + @IdCliente + char(39) 
	       If @IdSubCliente <> '' and @IdSubCliente <> '*' 
			  Set @sWhere = @sWhere + ' and IdSubCliente = ' + char(39) + @IdSubCliente + char(39) 
	   End 
	
	If @IdPrograma <> '' and @IdPrograma <> '*' 
	   Begin 
	       Set @sWhere = @sWhere + ' and IdPrograma = ' + char(39) + @IdPrograma + char(39) 
	       If @IdSubPrograma <> '' and @IdSubPrograma <> '*' 
			  Set @sWhere = @sWhere + ' and IdSubPrograma = ' + char(39) + @IdSubPrograma + char(39) 
	   End 
	   	
	
	Set @sSql = ' 
		Insert Into #tmpDatosFinales ( IdEmpresa, Empresa, IdEstado, Estado, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
			IdPersonal, NombrePersonal, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescProducto, DescripcionCorta, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Año, Mes    )	
		Select 
			IdEmpresa, Empresa, IdEstado, Estado, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
			IdPersonal, NombrePersonal, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescProducto, DescripcionCorta, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, 
			datepart(yy, FechaRegistro) as Año, datepart(mm, FechaRegistro) as Mes   ' + char(13) + 
		'	From #tmpDatosIniciales (noLock) ' + char(13) + 
		' ' + @sWhere 	
	Exec(@sSql) 

	



--- Generar tabla intermedia 
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdClaveSSA_Sal, ClaveSSA, IdProducto, DescProducto, DescripcionCorta, Cantidad,  
		0 as Año, 0 as Mes, 
		0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre 
	Into #tmpMeses 	
	From #tmpDatosFinales (NoLock) 
	Where 1 = 0
	
	
	--- Agregar todas las Claves una Unica vez 
	Insert #tmpMeses 
	Select Distinct IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdClaveSSA_Sal, ClaveSSA, IdProducto, DescProducto, DescripcionCorta, Cantidad,  
		Año, Mes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0  
	From #tmpDatosFinales 	
	Order by IdProducto --, DescripcionClaveSSA  	
	
	--  spp_Rpt_Administrativos_DesviacionDispensacion	

--- Resultado final del Proceso 	
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, IdProducto, DescProducto, DescripcionCorta, 
		Cantidad, Año, Mes, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre 
	From #tmpMeses 
	
--	sp_listaColumnas tmpMeses 
	
--	select distinct Folio from #tmpDatosIniciales (NoLock) 
	
End 
Go--#SQL
