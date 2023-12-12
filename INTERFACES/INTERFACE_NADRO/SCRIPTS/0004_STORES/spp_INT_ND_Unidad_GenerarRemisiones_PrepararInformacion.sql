------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_INT_ND_Unidad_GenerarRemisiones_PrepararInformacion' and xType = 'P' ) 
    Drop Proc spp_INT_ND_Unidad_GenerarRemisiones_PrepararInformacion
Go--#SQL 
  
--		ExCB spp_INT_ND_Unidad_GenerarRemisiones_PrepararInformacion '001', '11', '2181002', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_Unidad_GenerarRemisiones_PrepararInformacion 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0011', 
    @CodigoCliente varchar(20) = '2181002', @GUID varchar(100) = '55172502-8102-47A2-8513-8BF2D7476472' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  


	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion' and xType = 'U' ) 
	   Drop Table INT_ND_RptAdmonDispensacion 

	Select 
		identity(int, 1, 1) as Keyx, 
		0 as Keyx_Anexo, 
		0 as Procesado, 
		-- V.IdGrupoPrecios, V.DescripcionGrupoPrecios, V.EsSeguroPopular, V.TituloSeguroPopular, 
		V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.ClaveRenapo, V.IdMunicipio, V.Municipio, V.IdColonia, V.Colonia, V.Domicilio, 
		V.Municipio + ', ' + V.Estado + ', A ' + convert(varchar(10), getdate(), 120)  as ExpedidoEN, 
		V.IdFarmacia, V.Farmacia, @CodigoCliente as CodigoCliente, --V.IdSubFarmacia, V.SubFarmacia, 
		@GUID as GUID, 
		CAST('' as varchar(100)) as IdAnexo, CAST('' as varchar(200)) as NombreAnexo, 0 as Prioridad, '01' as Modulo, 
		CAST('' as varchar(200)) as NombrePrograma, 
		CAST('' as varchar(200)) as FolioRemision, 
		CAST('' as varchar(200)) as FolioRemision_Auxiliar, 		
		V.Folio, 
		(cast('' as varchar(5)) +  (Case When I.IdTipoDeDispensacion in ( '00', '01', '02', '06' ) Then 'R' Else 'C' End) ) as TipoDispensacion, 
		-- V.FechaSistema, 
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, 
		V.IdCliente, V.NombreCliente, V.IdSubCliente, V.NombreSubCliente, V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma,
		V.Farmacia + CAST('' as varchar(100)) as Departamento, 
		--'CEYE' + space(100) as Departamento, 
		V.IdBeneficiario, V.Beneficiario, V.FolioReferencia, V.NumReceta, V.FechaReceta, V.StatusVenta, 
		V.IdClaveSSA_Sal, 
		0 as EsCauses, 
		V.ClaveSSA, C.ClaveSSA_Base, 
		V.DescripcionSal, V.ContenidoPaquete_ClaveSSA, C.Presentacion, 
		CAST('' as varchar(20)) as ClaveSSA_ND, 
		CAST('' as varchar(20)) as ClaveSSA_Mascara, 
		CAST('' as varchar(7500)) as Descripcion_Mascara,  
		CAST('' as varchar(20)) As Lote, 
		0 as ManejaIva, 			
		cast(0 as numeric(14,2)) as PrecioVenta, 
		cast(0 as numeric(14,2)) as PrecioServicio, 
		cast(0 as numeric(14,2)) as SubTotal, 
		cast(0 as numeric(14,2)) as Iva, 
		cast(0 as numeric(14,2)) as ImporteTotal, 
		space(10) as FechaGeneracion, 
		
		V.IdSubFarmacia, V.IdProducto, V.CodigoEAN, V.ContenidoPaquete, 
		V.ContenidoPaquete as ContenidoPaquete_Auxiliar, 
		V.EsConsignacion, V.EsControlado, V.Renglon, V.DescProducto, V.DescripcionCorta, 
		V.TasaIva, 
		
		V.Cantidad as Piezas, V.Agrupacion_Comercial as Agrupacion, 
		cast(ceiling(round(V.Agrupacion_Comercial, 2, 1)) as int) as Cantidad, 
		
		cast(ceiling(round(V.Agrupacion, 2, 1)) as int) as Cantidad_ClaveSSA, 
		cast(ceiling(round(V.Agrupacion_Comercial, 2, 1)) as int) as Cantidad_Comercial, 
		
		cast(ceiling(round(V.Agrupacion_Comercial, 2, 1)) as int) as Cantidad_Auxiliar, 	
		
			
		cast(ceiling(round(V.Agrupacion_Comercial, 2, 1)) as int) as Cantidad_Proceso, 				
		
		V.TipoInsumo, V.TipoDeInsumo, 
		(V.IdMedico + space(50)) as IdMedico, 
		--(V.CedulaMedico + space(20)) as IdMedico, 		
		V.Medico, space(50) as CedulaMedico, 
		-- V.IdGrupo, V.GrupoClaves, V.DescripcionGrupo, V.SubGrupo, V.ClaveSubGrupo, V.DescripcionSubGrupo, 
		-- V.IdDiagnostico, V.ClaveDiagnostico, V.Diagnostico, 
		V.IdServicio, V.Servicio, V.IdArea, V.Area, 
		V.FechaInicial, V.FechaFinal, 
		V.FechaRegistro as FechaMenor, V.FechaRegistro as FechaMayor, 
		IsNull(FU.NombreEncargado, '') as NombreEncargado, 
		IsNull(FU.NombreDirector, '') as NombreDirector, 
		IsNull(FU.NombreAdministrador, '') as NombreAdministrador, 
		0 as EsEnResguardo, 
		1 as Incluir, 0 as TipoRelacion     
	Into INT_ND_RptAdmonDispensacion 	
	From RptAdmonDispensacion V (NoLock) 
	Inner Join vw_ClavesSSA_Sales C (NoLock) On ( V.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.FolioVenta )	 
	Left Join INT_ND_FirmasUnidad FU (NoLock) On ( V.IdEstado = FU.IdEstado and V.IdFarmacia = FU.IdFarmacia )	
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
		-- and V.ClaveSSA like '%5486%'  
		-- and V.ClaveSSA = '060.456.0318' 
		and V.Beneficiario <> '' 
	Order By V.Folio, V.FechaRegistro  


-------------------------------------- Actualizar contenido paquete de acuerdo datos NADRO 
	update 	t Set  
		ContenidoPaquete_Auxiliar = C.ContenidoPaquete, 
		Cantidad_Auxiliar = ceiling(Piezas / ( C.ContenidoPaquete * 1.0 )), 
		Cantidad_Proceso = ceiling(Piezas / ( C.ContenidoPaquete * 1.0 )) 
	From INT_ND_RptAdmonDispensacion	T 	
	inner join INT_ND_CFG_ClavesSSA_ContenidoPaquete C On ( T.ClaveSSA = C.ClaveSSA and C.Status = 'A'  ) 
	
	Update T Set Cantidad = Cantidad_Proceso 
	From INT_ND_RptAdmonDispensacion T 	
-------------------------------------- Actualizar contenido paquete de acuerdo datos NADRO 



-- select top 1 * from vw_Servicios_Areas 


-------------------------------------------------------- Actualizar datos     
	Update I Set IdMedico = M.NumCedula, CedulaMedico = M.NumCedula    
	From INT_ND_RptAdmonDispensacion I (NoLock) 
	Inner Join CatMedicos M (NoLock) On ( I.IdEstado = M.IdEstado and I.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico )  	

	Update I Set TipoDeInsumo = 'MATERIAL DE CURACIÓN' 
	From INT_ND_RptAdmonDispensacion I (NoLock) 
	Where TipoInsumo = 2 
	
	Update I Set TipoDispensacion = 'C' 
	From INT_ND_RptAdmonDispensacion I (NoLock) 
	Where IsNumeric( replace(replace(replace(FolioReferencia, '-', ''), '.', ''), ' ', '') ) = 0 
	
	
	---- POBLACION ABIERTA SE MARCA COMO RECETA 
	
	
	
	
	Update I Set 
		FechaMenor = (select min(FechaRegistro) From INT_ND_RptAdmonDispensacion (NoLock) ), 
		FechaMayor = (select max(FechaRegistro) From INT_ND_RptAdmonDispensacion (NoLock) )
	From INT_ND_RptAdmonDispensacion I (NoLock) 
	
	Update I Set EsEnResguardo = 1 
	From INT_ND_RptAdmonDispensacion I (NoLock) 
	Inner Join INT_ND_SubFarmaciasConsigna C (NoLock) On ( I.IdEstado = C.IdEstado and I.IdSubFarmacia = C.IdSubFarmacia ) 
-------------------------------------------------------- Actualizar datos     
	
	
-------------------------------------- Validación Operacion Michoacan  
	If @IdEstado = '16' 
	Begin 
		Update T Set Departamento = T.Area  
		From INT_ND_RptAdmonDispensacion T (NoLock) 
		Where FechaRegistro >= '2015-05-01' 
		
		
		Update T Set Departamento = T.Farmacia  
		From INT_ND_RptAdmonDispensacion T (NoLock) 
		Where FechaRegistro >= '2015-05-01' and IdServicio = '001' and IdArea = '001'

	End 	
-------------------------------------- Validación Operacion Michoacan  
	

-------------------------------------- Validación Operacion Chiapas 
	If @IdEstado = '07' 
	Begin 
		--- Todo se manea en unidades 
		Update T Set Cantidad = Piezas, Cantidad_Proceso = Piezas 
		From INT_ND_RptAdmonDispensacion T (NoLock)  
		
		Update T Set TipoDispensacion = 'C' 
		From INT_ND_RptAdmonDispensacion T (NoLock) 
		Where IdPrograma = '0002' and IdSubPrograma = '0015' 
			
		
		------------------ Forzar todo lo que no sea Gasto Hospitalario como RECETA 
		Update T Set TipoDispensacion = 'R' 
		From INT_ND_RptAdmonDispensacion T (NoLock) 
		Where IdPrograma + IdSubPrograma <> '00020015' 	
			  ---- and FechaRegistro >= '2015-07-01'   
		
		
		Update T Set Departamento = T.Servicio   
		From INT_ND_RptAdmonDispensacion T (NoLock) 		
			
		Update T Set Departamento = SC.Descripcion 
		From INT_ND_RptAdmonDispensacion T (NoLock) 
		Inner Join vw_Farmacias F On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia ) 
		Inner Join INT_ND_CFG_Servicios S On ( F.IdEstado = S.IdEstado and F.IdTipoUnidad = S.IdTipoUnidad ) 
		Inner Join CatServicios SC (NoLock) On ( S.IdServicio = SC.IdServicio ) 
		Where T.FechaRegistro between S.FechaInicial and S.FechaFinal 
		  
	End 	
-------------------------------------- Validación Operacion Chiapas 


	
	
-------		Drop table INT_ND_RptAdmonDispensacion_Detallado__General 


/* 


	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion' and xType = 'U' ) 
	   Drop Table INT_ND_RptAdmonDispensacion 
	   
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion_Detallado__General' and xType = 'U' ) 
	   Drop Table INT_ND_RptAdmonDispensacion_Detallado__General 

*/ 

	
------------------------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Not exists ( Select * From sysobjects (noLock) Where Name = 'INT_ND_RptAdmonDispensacion_Detallado__General' and xType = 'U' )  
	Begin 
		Select 
			Identity(int, 1, 1) as Keyx, 
			cast(Keyx as int) as Keyx_GUID, 
			Keyx_Anexo, Procesado, IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, 
			IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, ExpedidoEN, IdFarmacia, Farmacia, CodigoCliente, 
			GUID, IdAnexo, NombreAnexo, Prioridad, Modulo, NombrePrograma, FolioRemision, FolioRemision_Auxiliar, Folio, TipoDispensacion, 
			FechaRegistro, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			Departamento, IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, 
			IdClaveSSA_Sal, EsCauses, ClaveSSA, ClaveSSA_Base, DescripcionSal, ContenidoPaquete_ClaveSSA, Presentacion, 
			ClaveSSA_ND, ClaveSSA_Mascara, Descripcion_Mascara, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, 
			FechaGeneracion, IdSubFarmacia, IdProducto, CodigoEAN, ContenidoPaquete, ContenidoPaquete_Auxiliar, 
			EsConsignacion, EsControlado, Renglon, DescProducto, DescripcionCorta, TasaIva, 
			Piezas, Agrupacion, Cantidad, 
			
			Cantidad_ClaveSSA, Cantidad_Comercial, Cantidad_Auxiliar, Cantidad_Proceso, 
			
			TipoInsumo, TipoDeInsumo, IdMedico, Medico, CedulaMedico, IdServicio, Servicio, IdArea, Area, 
			FechaInicial, FechaFinal, FechaMenor, FechaMayor, NombreEncargado, NombreDirector, NombreAdministrador, 
			EsEnResguardo, Incluir, 0 as TipoRelacion, Lote
		Into INT_ND_RptAdmonDispensacion_Detallado__General 
		From INT_ND_RptAdmonDispensacion 
		Where 1 = 0 
	End 
	
	--			spp_INT_ND_Unidad_GenerarRemisiones_PrepararInformacion		
	
	Delete INT_ND_RptAdmonDispensacion_Detallado__General 
	From INT_ND_RptAdmonDispensacion_Detallado__General C (NoLock) 
	Inner Join INT_ND_RptAdmonDispensacion R (NoLock) 
		On ( C.IdEmpresa = R.IdEmpresa and C.IdEstado = R.IdEstado and C.IdFarmacia = R.IdFarmacia 
			and convert(varchar(10), R.FechaRegistro, 120) = convert(varchar(10), C.FechaRegistro, 120) ) 
	
	
	Insert Into INT_ND_RptAdmonDispensacion_Detallado__General 
	( 
		Keyx_GUID, 
		Keyx_Anexo, Procesado, IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, 
		IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, ExpedidoEN, IdFarmacia, Farmacia, CodigoCliente, 
		GUID, IdAnexo, NombreAnexo, Prioridad, Modulo, NombrePrograma, FolioRemision, FolioRemision_Auxiliar, Folio, TipoDispensacion, 
		FechaRegistro, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		Departamento, IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, 
		IdClaveSSA_Sal, EsCauses, ClaveSSA, ClaveSSA_Base, DescripcionSal, ContenidoPaquete_ClaveSSA, Presentacion, 
		ClaveSSA_ND, ClaveSSA_Mascara, Descripcion_Mascara, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, 
		FechaGeneracion, IdSubFarmacia, IdProducto, CodigoEAN, ContenidoPaquete, ContenidoPaquete_Auxiliar, 
		EsConsignacion, EsControlado, Renglon, DescProducto, DescripcionCorta, TasaIva, 
		Piezas, Agrupacion, Cantidad, 
		
		Cantidad_ClaveSSA, Cantidad_Comercial, Cantidad_Auxiliar, Cantidad_Proceso, 
		
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, CedulaMedico, IdServicio, Servicio, IdArea, Area, 
		FechaInicial, FechaFinal, FechaMenor, FechaMayor, NombreEncargado, NombreDirector, NombreAdministrador, 
		EsEnResguardo, Incluir, TipoRelacion, Lote     	
	) 
	Select 
		cast(Keyx as int) as Keyx_GUID, 
		Keyx_Anexo, Procesado, IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, 
		IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, ExpedidoEN, IdFarmacia, Farmacia, CodigoCliente, 
		GUID, IdAnexo, NombreAnexo, Prioridad, Modulo, NombrePrograma, FolioRemision, FolioRemision_Auxiliar, Folio, TipoDispensacion, 
		FechaRegistro, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		Departamento, IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, 
		IdClaveSSA_Sal, EsCauses, ClaveSSA, ClaveSSA_Base, DescripcionSal, ContenidoPaquete_ClaveSSA, Presentacion, 
		ClaveSSA_ND, ClaveSSA_Mascara, Descripcion_Mascara, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, 
		FechaGeneracion, IdSubFarmacia, IdProducto, CodigoEAN, ContenidoPaquete, ContenidoPaquete_Auxiliar, 
		EsConsignacion, EsControlado, Renglon, DescProducto, DescripcionCorta, TasaIva, 
		Piezas, Agrupacion, Cantidad, 
		
		Cantidad_ClaveSSA, Cantidad_Comercial, Cantidad_Auxiliar, Cantidad_Proceso, 
		
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, CedulaMedico, IdServicio, Servicio, IdArea, Area, 
		FechaInicial, FechaFinal, FechaMenor, FechaMayor, NombreEncargado, NombreDirector, NombreAdministrador, 
		EsEnResguardo, Incluir, 0 as TipoRelacion, Lote    	
	From INT_ND_RptAdmonDispensacion 				

 
					
		
End  
Go--#SQL   

--	select * from vw_ClavesSSA_Sales 

--	select * from RptAdmonDispensacion 