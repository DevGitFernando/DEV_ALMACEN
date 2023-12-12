--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Administrativos_CortesDiarios' And xType = 'P' )
	Drop Proc spp_Rpt_Administrativos_CortesDiarios
Go--#SQL  

Create Procedure spp_Rpt_Administrativos_CortesDiarios 
(	
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '92', 
	@FechaDeSistema varchar(10) = '2013-06-11', @EsReporteGeneral int = 1, @IdPersonal varchar(4) = '1'  
) 
With Encryption   
As
Begin
Set NoCount On 
Set DateFormat YMD 
Declare 
	@sSql varchar(max), 
	@sFiltro varchar(1000)

	Set @IdEmpresa = right('000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000' + @IdFarmacia, 4) 	
	Set @IdPersonal = right('000000' + @IdPersonal, 4) 		


	Set @sSql = '' 
	Set @sFiltro = 
		' Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + 
		' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) +  
		' and convert(varchar(10), FechaSistema, 120) = ' + char(39) + @FechaDeSistema + char(39)  

	If @EsReporteGeneral <> 1 
	   Set @sFiltro = @sFiltro + ' and IdPersonal = ' + char(39) + @IdPersonal + char(39)  


------------- PREPARAR TABLA DE PROCESO 
	Select Top 0 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
		IdPersonal, NombrePersonal, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		space(16) as IdDispensacion, 
		IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, 
		IdMedico, Medico, EsCorte, TipoDeVenta, NombreTipoDeVenta, 
		IdClaveSSA_Sal, cast(ClaveSSA as varchar(50)) as ClaveSSA, DescripcionSal, 
		DescripcionClave, cast(DescripcionCortaClave as varchar(3000)) as DescripcionCortaClave, 
		Renglon, IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, 
		UnidadDeSalida, TasaIva, cast(Cantidad as int) as Cantidad, cast(CantidadCajas as int) as CantidadCajas, Costo, Importe, PrecioLicitacion, 
		getdate() as FechaImpresion 
	Into #tmpReporteValidacion 
	From vw_Impresion_Ventas_Credito 
------------- PREPARAR TABLA DE PROCESO 		
	
------------- Insertar datos 
	Set @sSql = 'Insert Into #tmpReporteValidacion 
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
		IdPersonal, NombrePersonal, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		(IdCliente + IdSubCliente + IdPrograma + IdSubPrograma) as IdDispensacion, 
		IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, 
		IdMedico, Medico, EsCorte, TipoDeVenta, NombreTipoDeVenta, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, DescripcionCortaClave, 
		Renglon, IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, 
		UnidadDeSalida, TasaIva, Cantidad, CantidadCajas, Costo, Importe, PrecioLicitacion, getdate() as FechaImpresion  
	From vw_Impresion_Ventas_Credito (NoLocK) ' + @sFiltro 
	Exec(@sSql) 
------------- Insertar datos 	
		

		
------------------------ Aplicar Mascara : Descripciones 
	Update T Set T.ClaveSSA = IsNull(M.Mascara, T.ClaveSSA), 
		T.DescripcionCortaClave = IsNull(M.DescripcionCorta, T.DescripcionCortaClave)
	From #tmpReporteValidacion T (Nolock) 
	Inner Join CFG_ClaveSSA_Mascara M (Nolock) 
		On ( T.IdEstado = M.IdEstado and M.IdCliente = T.IdCliente and M.IdSubCliente = T.IdSubCliente 
			and T.IdClaveSSA_Sal = M.IdClaveSSA and M.Status = 'A' ) 			
------------------------ Aplicar Mascara : Descripciones 
		
		
----- Cargar precios faltantes 
	Update V Set PrecioLicitacion = dbo.fg_CalcularPrecioLicitacion(V.IdEstado, V.IdCliente, V.IdSubCliente, V.IdProducto) 
	From #tmpReporteValidacion V (NoLock) 			
	Where PrecioLicitacion = 0 
		
	Update V Set Importe = (Cantidad * PrecioLicitacion)
	From #tmpReporteValidacion V (NoLock) 			
		
----- Mostrar precios solo al generar el reporte concentrado 		
	If @EsReporteGeneral <> 1 
	Begin 
	   Update V Set Costo = 0, Importe = 0, PrecioLicitacion = 0 
	   From #tmpReporteValidacion V (NoLock) 	
	End 	
		
		
		
--		spp_Rpt_Administrativos_CortesDiarios 

----	SALIDA FINAL 
	Select * 
	From #tmpReporteValidacion 

End
Go--#SQL


-- sp_listacolumnas vw_Impresion_Ventas_Credito 

