If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Administrativos_CortesDiarios_Vales' And xType = 'P' )
	Drop Proc spp_Rpt_Administrativos_CortesDiarios_Vales
Go--#SQL  

Create Procedure spp_Rpt_Administrativos_CortesDiarios_Vales 
(	
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2132', 
	@FechaDeSistema varchar(10) = '2015-02-04', @EsReporteGeneral int = 1, @IdPersonal varchar(4) = '55'  
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
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, Folio, FolioVenta, 
		EsSegundoVale, FolioValeReembolso, IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, 
		FechaSistema, FechaRegistro, FechaCanje, IdPersonal, NombrePersonal, 
		space(16) as IdDispensacion, 
		-- (IdCliente + IdSubCliente + IdPrograma + IdSubPrograma) as IdDispensacion, 		
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, Status, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, DescripcionSal, DescripcionCortaClave, 
		IdPresentacion, Presentacion, Cantidad, CantidadSegundoVale, 
		cast(0.0 as float) as Costo, cast(0.0 as float) as Importe, cast(0.0 as float) as PrecioLicitacion,  
		getdate() as FechaImpresion  
	Into #tmpReporteValidacion   
	From vw_Impresion_Vales 	
------------- PREPARAR TABLA DE PROCESO 		
	
------------- Insertar datos 
	Set @sSql = 'Insert Into #tmpReporteValidacion 
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, Folio, FolioVenta, 
		EsSegundoVale, FolioValeReembolso, IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, 
		FechaSistema, FechaRegistro, FechaCanje, IdPersonal, NombrePersonal, 
		(IdCliente + IdSubCliente + IdPrograma + IdSubPrograma) as IdDispensacion, 		
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, Status, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, DescripcionSal, DescripcionCortaClave, 
		IdPresentacion, Presentacion, Cantidad, CantidadSegundoVale, 0 as Costo, 0 as Importe, 0 as PrecioLicitacion, 
		getdate() as FechaImpresion  
	From vw_Impresion_Vales (NoLocK) ' + @sFiltro 
	Exec(@sSql) 
------------- Insertar datos 	
		
----- Cargar precios faltantes 
	----Update V Set PrecioLicitacion = dbo.fg_CalcularPrecioLicitacion(V.IdEstado, V.IdCliente, V.IdSubCliente, V.IdProducto) 
	----From #tmpReporteValidacion V (NoLock) 			
	----Where PrecioLicitacion = 0 
		
	----Update V Set Importe = (Cantidad * PrecioLicitacion)
	----From #tmpReporteValidacion V (NoLock) 	
			
		
----- Mostrar precios solo al generar el reporte concentrado 		
	If @EsReporteGeneral <> 1 
	Begin 
	   Update V Set Costo = 0, Importe = 0, PrecioLicitacion = 0 
	   From #tmpReporteValidacion V (NoLock) 	
	End 	
		
		
		
--		spp_Rpt_Administrativos_CortesDiarios_Vales 

----	SALIDA FINAL 
	Select * 
	From #tmpReporteValidacion 

End
Go--#SQL  


-- sp_listacolumnas vw_Impresion_Ventas_Credito 

