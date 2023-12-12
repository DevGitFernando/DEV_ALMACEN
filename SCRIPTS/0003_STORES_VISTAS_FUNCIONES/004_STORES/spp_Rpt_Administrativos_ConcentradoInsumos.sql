
----------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Controlados' and xType = 'U' )
   Drop Table RptAdmonDispensacion_Controlados   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Controlados_Detallado' and xType = 'U' )
   Drop Table RptAdmonDispensacion_Controlados_Detallado   
Go--#SQL  


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion' and xType = 'U' )
   Drop Table RptAdmonDispensacion  
Go--#SQL  


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_ConcentradoInsumosDetallado' and xType = 'U' )
   Drop Table RptAdmonDispensacion_ConcentradoInsumosDetallado  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_ConcentradoInsumos' and xType = 'U' )
   Drop Table RptAdmonDispensacion_ConcentradoInsumos   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado' and xType = 'U' )
   Drop Table RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado  
Go--#SQL  





----------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Administrativos_ConcentradoInsumos' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_ConcentradoInsumos
Go--#SQL  
 
Create Proc spp_Rpt_Administrativos_ConcentradoInsumos 
( 
	@Ordenamiento tinyint = 1 --  1 ==> Clave SSA | 1 ==> Descripcion Clave SSA 
) 
With Encryption 
As 
Begin 
Set NoCount On

Declare 
	@iOrdenamiento int 


	Set @iOrdenamiento = @Ordenamiento  




/* 

	select count(*) from RptAdmonDispensacion (nolock)
	select count(*) from RptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 
	select count(*) from RptAdmonDispensacion_ConcentradoInsumos (nolock) 		
	select count(*) from RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 		

	select sum(ImporteEAN) as ImporteEAN from RptAdmonDispensacion (nolock)
	select sum(ImporteEAN) as ImporteEAN from RptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 
	select sum(ImporteEAN) as ImporteEAN from RptAdmonDispensacion_ConcentradoInsumos (nolock) 		
	select sum(ImporteEAN) as ImporteEAN from RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 		
	

	select top 1 * from RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 	

	select sum(cantidad), sum(ImporteEAN) from RptAdmonDispensacion_ConcentradoInsumos (nolock) 
	select sum(cantidad), sum(ImporteEAN) from RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 	

	select sum(ImporteEAN) as ImporteEAN from RptAdmonDispensacion (nolock)
	select sum(ImporteEAN) as ImporteEAN from RptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 
	
	spp_Rpt_Administrativos 
	
*/

--------------------------------------------------- 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Controlados' and xType = 'U' )
	   Drop Table RptAdmonDispensacion_Controlados  

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Controlados_Detallado' and xType = 'U' )
	   Drop Table RptAdmonDispensacion_Controlados_Detallado   

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_ConcentradoInsumosDetallado' and xType = 'U' )
	   Drop Table RptAdmonDispensacion_ConcentradoInsumosDetallado  

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_ConcentradoInsumos' and xType = 'U' )
	   Drop Table RptAdmonDispensacion_ConcentradoInsumos   

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado' and xType = 'U' )
	   Drop Table RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado  	   
--------------------------------------------------- 
-- select top 1 * from RptAdmonDispensacion 




------------------ BASE CONTROLADOS 			
	Select 
			IdGrupoPrecios, DescripcionGrupoPrecios, 
			EsSeguroPopular, TituloSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, 
			IdProducto, CodigoEAN, EsControlado, 
			Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, 
			TasaIva, Cantidad, 
			PrecioUnitario, PrecioLicitacion, 

			ImporteEAN, ImporteEAN_Licitado, 

			--- Agregar campos adicionales para Validación y Facturacion 			
			SubTotalLicitacion_0, 
			SubTotalLicitacion,      
			IvaLicitacion, 
			TotalLicitacion,      	
			
			----(ImporteEAN * 100) as ImporteEAN, (ImporteEAN_Licitado * 100) as ImporteEAN_Licitado, 

			------- Agregar campos adicionales para Validación y Facturacion 			
			----(SubTotalLicitacion_0 * 100) as SubTotalLicitacion_0, 
			----(SubTotalLicitacion * 100) as SubTotalLicitacion,      
			----(IvaLicitacion * 100) as IvaLicitacion, 
			----(TotalLicitacion * 100) as TotalLicitacion,      


			TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal, 
			IDENTITY(int, 1, 1) as Secuencial  			 
	Into RptAdmonDispensacion_Controlados_Detallado 
	From RptAdmonDispensacion (NoLock) 
	Where EsControlado = 1 	


----- 	
	Select 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, sum(Cantidad) as Cantidad, 
		IDENTITY(int, 1, 1) as Secuencial  	
	Into RptAdmonDispensacion_Controlados  
	From RptAdmonDispensacion (NoLock) 
	Where EsControlado = 1 
	Group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal 
	Order by 
		(Case When @iOrdenamiento = 1 Then ClaveSSA Else DescripcionSal End)  
------------------ BASE CONTROLADOS 			



------------------ BASE 
	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		CLUES_Oficial, NombrePropio_UMedica, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		sum(Cantidad) as Cantidad, -- PrecioUnitario, 
		PrecioLicitacion, PrecioLicitacionUnitario, 
		
		sum(ImporteEAN) as ImporteEAN, ImporteEAN_Licitado, 	
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, 
		sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
		
		--sum(ImporteEAN * 100) as ImporteEAN, (ImporteEAN_Licitado * 100) as ImporteEAN_Licitado, 
		
		--sum(SubTotalLicitacion_0 * 100) as SubTotalLicitacion_0, 
		--sum(SubTotalLicitacion * 100) as SubTotalLicitacion, 
		--sum(IvaLicitacion * 100) as IvaLicitacion, sum(TotalLicitacion * 100) as TotalLicitacion, 



		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal, 
		IDENTITY(int, 1, 1) as Secuencial  			 
	Into RptAdmonDispensacion_ConcentradoInsumosDetallado 
	From RptAdmonDispensacion (NoLock) 
	Group by 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		CLUES_Oficial, NombrePropio_UMedica, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		-- Cantidad, -- PrecioUnitario, 
		PrecioLicitacion, PrecioLicitacionUnitario, -- ImporteEAN, 
		ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 
	Order by 
		(Case When @iOrdenamiento = 1 Then ClaveSSA Else DescripcionSal End), 
		IdGrupoPrecios, EsSeguroPopular, TipoInsumo, IdEmpresa, IdEstado, IdFarmacia, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma 
------------------ BASE 


--  spp_Rpt_Administrativos_ConcentradoInsumos 

--	select sum(ImporteEAN) as ImporteEAN_Gral from RptAdmonDispensacion (nolock)
--	select sum(ImporteEAN) as ImporteEAN from RptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 

 
------------------------------------------------------------- 
	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		CLUES_Oficial, NombrePropio_UMedica, 	
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		-- IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		sum(Cantidad) as Cantidad, -- PrecioUnitario, 
		-- PrecioLicitacion, 
		sum(ImporteEAN) as ImporteEAN, 
		
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, 
		sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
		
		-- ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal, 
		IDENTITY(int, 1, 1) as Secuencial  	
	Into RptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado
	From RptAdmonDispensacion_ConcentradoInsumosDetallado 
	Group by 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		CLUES_Oficial, NombrePropio_UMedica, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		-- IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		-- Cantidad, -- PrecioUnitario, 
		-- PrecioLicitacion, 
		-- ImporteEAN, 
		-- ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 
	Order by 	
		IdGrupoPrecios, EsSeguroPopular, TipoInsumo, IdEmpresa, IdEstado, IdFarmacia, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma 		
		
		
--------------------------------------------------------------------------------------------------------------------------------  
--------------------------------------------------------------------------------------------------------------------------------  
	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		CLUES_Oficial, NombrePropio_UMedica, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
		-- IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		sum(Cantidad) as Cantidad, cast(0 as Numeric(14,4)) as Paquetes, -- PrecioUnitario, 
		PrecioLicitacion, PrecioLicitacionUnitario, 
		sum(ImporteEAN) as ImporteEAN, 
		
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, 
		sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
						
		-- ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal, 
		IDENTITY(int, 1, 1) as Secuencial  	
	Into RptAdmonDispensacion_ConcentradoInsumos 
	From RptAdmonDispensacion_ConcentradoInsumosDetallado 
	Group by 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		CLUES_Oficial, NombrePropio_UMedica, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
		-- IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		-- Cantidad, -- PrecioUnitario, 
		PrecioLicitacion, PrecioLicitacionUnitario, 
		-- ImporteEAN, 
		-- ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 		
	-- Order by DescripcionSal 
	Order by 
		(Case When @iOrdenamiento = 1 Then ClaveSSA Else DescripcionSal End) 



				
	Update C Set Paquetes = ( Cantidad / (ContenidoPaquete_ClaveSSA*1.0) ) 
	From RptAdmonDispensacion_ConcentradoInsumos C 
				
--------------------------------------------------------------------------------------------------------------------------------  
--------------------------------------------------------------------------------------------------------------------------------  				
				
---------------------
--	Select count(*) from RptAdmonDispensacion_ConcentradoInsumos 
 			
End
Go--#SQL 


