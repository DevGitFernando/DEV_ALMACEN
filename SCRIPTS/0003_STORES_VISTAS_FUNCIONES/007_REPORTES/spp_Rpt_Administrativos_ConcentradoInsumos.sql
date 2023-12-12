If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_Controlados' and xType = 'U' )
   Drop Table tmpRptAdmonDispensacion_Controlados   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_Controlados_Detallado' and xType = 'U' )
   Drop Table tmpRptAdmonDispensacion_Controlados_Detallado   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion' and xType = 'U' )
   Drop Table tmpRptAdmonDispensacion  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_ConcentradoInsumosDetallado' and xType = 'U' )
   Drop Table tmpRptAdmonDispensacion_ConcentradoInsumosDetallado  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_ConcentradoInsumos' and xType = 'U' )
   Drop Table tmpRptAdmonDispensacion_ConcentradoInsumos   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado' and xType = 'U' )
   Drop Table tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Administrativos_ConcentradoInsumos' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_ConcentradoInsumos
Go--#SQL  
 
Create Proc spp_Rpt_Administrativos_ConcentradoInsumos 
With Encryption 
As 
Begin 
Set NoCount On

/* 

	select count(*) from tmpRptAdmonDispensacion (nolock)
	select count(*) from tmpRptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 
	select count(*) from tmpRptAdmonDispensacion_ConcentradoInsumos (nolock) 		
	select count(*) from tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 		

	select sum(ImporteEAN) as ImporteEAN from tmpRptAdmonDispensacion (nolock)
	select sum(ImporteEAN) as ImporteEAN from tmpRptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 
	select sum(ImporteEAN) as ImporteEAN from tmpRptAdmonDispensacion_ConcentradoInsumos (nolock) 		
	select sum(ImporteEAN) as ImporteEAN from tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 		
	

	select top 1 * from tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 	

	select sum(cantidad), sum(ImporteEAN) from tmpRptAdmonDispensacion_ConcentradoInsumos (nolock) 
	select sum(cantidad), sum(ImporteEAN) from tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado (nolock) 	

	select sum(ImporteEAN) as ImporteEAN from tmpRptAdmonDispensacion (nolock)
	select sum(ImporteEAN) as ImporteEAN from tmpRptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 
	
	spp_Rpt_Administrativos 
	
*/

--------------------------------------------------- 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_Controlados' and xType = 'U' )
	   Drop Table tmpRptAdmonDispensacion_Controlados  

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_Controlados_Detallado' and xType = 'U' )
	   Drop Table tmpRptAdmonDispensacion_Controlados_Detallado   

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_ConcentradoInsumosDetallado' and xType = 'U' )
	   Drop Table tmpRptAdmonDispensacion_ConcentradoInsumosDetallado  

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_ConcentradoInsumos' and xType = 'U' )
	   Drop Table tmpRptAdmonDispensacion_ConcentradoInsumos   

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado' and xType = 'U' )
	   Drop Table tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado  	   
--------------------------------------------------- 
-- select top 1 * from tmpRptAdmonDispensacion 

------------------ BASE CONTROLADOS 			
	Select 
			IdGrupoPrecios, DescripcionGrupoPrecios, 
			EsSeguroPopular, TituloSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
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
			
			TipoInsumo, TipoDeInsumo, 
			IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, 
			ClaveSubGrupo, DescripcionSubGrupo, IdDiagnostico, ClaveDiagnostico, Diagnostico, 
			IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal 
	Into tmpRptAdmonDispensacion_Controlados_Detallado 
	From tmpRptAdmonDispensacion (NoLock) 
	Where EsControlado = 1 	

----- 	
	Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, sum(Cantidad) as Cantidad  
	Into tmpRptAdmonDispensacion_Controlados  
	From tmpRptAdmonDispensacion (NoLock) 
	Where EsControlado = 1 
	Group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal 
------------------ BASE CONTROLADOS 			


------------------ BASE 
	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		sum(Cantidad) as Cantidad, -- PrecioUnitario, 
		PrecioLicitacion, sum(ImporteEAN) as ImporteEAN, ImporteEAN_Licitado, 
		
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, 
		sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
		
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 
	Into tmpRptAdmonDispensacion_ConcentradoInsumosDetallado 
	From tmpRptAdmonDispensacion (NoLock) 
	Group by 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		-- Cantidad, -- PrecioUnitario, 
		PrecioLicitacion, -- ImporteEAN, 
		ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 
	Order by IdGrupoPrecios, EsSeguroPopular, TipoInsumo, IdEmpresa, IdEstado, IdFarmacia, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma 
------------------ BASE 


--  spp_Rpt_Administrativos_ConcentradoInsumos 

--	select sum(ImporteEAN) as ImporteEAN_Gral from tmpRptAdmonDispensacion (nolock)
--	select sum(ImporteEAN) as ImporteEAN from tmpRptAdmonDispensacion_ConcentradoInsumosDetallado (nolock) 

 
------------------------------------------------------------- 
	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
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
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 	
	Into tmpRptAdmonDispensacion_ConcentradoInsumosProgramaTotalizado
	From tmpRptAdmonDispensacion_ConcentradoInsumosDetallado 
	Group by 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		-- IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		-- Cantidad, -- PrecioUnitario, 
		-- PrecioLicitacion, 
		-- ImporteEAN, 
		-- ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 
	Order by IdGrupoPrecios, EsSeguroPopular, TipoInsumo, IdEmpresa, IdEstado, IdFarmacia, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma 		
		
		
------------------------------------------------------------- 
	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		-- IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		sum(Cantidad) as Cantidad, -- PrecioUnitario, 
		PrecioLicitacion, 
		sum(ImporteEAN) as ImporteEAN, 
		
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, 
		sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
						
		-- ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 	
	Into tmpRptAdmonDispensacion_ConcentradoInsumos 
	From tmpRptAdmonDispensacion_ConcentradoInsumosDetallado 
	Group by 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, 
		-- IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		-- IdProducto, CodigoEAN, Renglon, DescProducto, DescripcionCorta, 
		-- Cantidad, -- PrecioUnitario, 
		PrecioLicitacion, 
		-- ImporteEAN, 
		-- ImporteEAN_Licitado, 
		TipoInsumo, TipoDeInsumo, FechaInicial, FechaFinal 		
	Order by DescripcionSal 
				
---------------------
--	Select count(*) from tmpRptAdmonDispensacion_ConcentradoInsumos 
 			
End
Go--#SQL 