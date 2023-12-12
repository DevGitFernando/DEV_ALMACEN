
---		drop table RptAdmonDispensacion_Detallado 

--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__Exportar_A_Reporteador' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__Exportar_A_Reporteador 
Go--#SQL 

Create Proc spp_BI_RPT__Exportar_A_Reporteador 
(   
	@FechaInicial varchar(10) = '2016-12-01', @FechaFinal varchar(10) = '2016-12-31'
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

Declare 
	@bCrearTablaBase int 

	Set @bCrearTablaBase = 1   
	Set @sSql = '' 


	------------------------------------------ Generar tablas de catalogos     
	If Exists ( Select * From SII_REPORTEADOR..sysobjects (NoLock) Where Name = 'vw_Farmacias' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From SII_REPORTEADOR..sysobjects (NoLock) 
		Where Name = 'vw_Farmacias' and xType = 'U' and datediff(dd, crDate, getdate()) > 10 
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	
	End 	
	
	If @bCrearTablaBase = 1 
	Begin 
	
		------------------------------------------------------------------------------------------------------------------------------------ 	
		If Exists ( Select * From SII_REPORTEADOR..sysobjects (NoLock) Where Name = 'vw_Farmacias' and xType = 'U' ) 
		   Drop Table SII_REPORTEADOR..vw_Farmacias 
		   		   
		Select * 
		Into SII_REPORTEADOR..vw_Farmacias  
		From vw_Farmacias 
	End 




------------------------------------------- GENERAR LA TABLA DEL REPOSITORO 
	If Not Exists ( select * from SII_REPORTEADOR..sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Detallado' and xType = 'U' ) 
	Begin
		
		Select  
			IdPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
			IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
			IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
			IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, FolioReferencia, cast('' as varchar(4) )as IdTipoDeDispensacion, 
			EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, IdGrupoTerapeutico, GrupoTerapeutico, 
			IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, ContenidoPaquete, EsControlado, Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, TasaIva, 
			Cantidad, Cantidad_A_Cobro, Multiplo, PiezasTotales, Agrupacion, AgrupadoMenor, AgrupadoMayor, PiezasSueltas, 
			Agrupacion_Comercial, AgrupadoMenor_Comercial, AgrupadoMayor_Comercial, PiezasSueltas_Comercial, 
			PrecioUnitario, PrecioLicitacion, PrecioLicitacionUnitario, ImporteEAN, ImporteEAN_Licitado, SubTotalLicitacion_0, SubTotalLicitacion, IvaLicitacion, TotalLicitacion, 
			Cantidad__Negado, PrecioLicitacion__Negado, ImporteEAN__Negado, SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
			TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
			IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal, 
			getdate() as FechaProcesamiento 
		Into SII_REPORTEADOR..RptAdmonDispensacion_Detallado 
		From RptAdmonDispensacion_Detallado (NoLock) 
		Where 1 = 0  
	End 


----------------------------------------- ELIMINAR LA POSIBILIDAD DE DUPLICIDAD DE DATOS 
	Delete SII_REPORTEADOR..RptAdmonDispensacion_Detallado 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado D (NoLock) 
	Inner Join RptAdmonDispensacion_Detallado O (NoLock) 
		On ( D.IdEmpresa = O.IdEmpresa and D.IdEstado = O.IdEstado and D.IdFarmacia = O.IdFarmacia and D.Folio = O.Folio ) 



	Insert Into SII_REPORTEADOR..RptAdmonDispensacion_Detallado 
	( 
		IdPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, FolioReferencia, IdTipoDeDispensacion, 
		EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, IdGrupoTerapeutico, GrupoTerapeutico, 
		IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, ContenidoPaquete, EsControlado, Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, TasaIva, 
		Cantidad, Cantidad_A_Cobro, Multiplo, PiezasTotales, Agrupacion, AgrupadoMenor, AgrupadoMayor, PiezasSueltas, 
		Agrupacion_Comercial, AgrupadoMenor_Comercial, AgrupadoMayor_Comercial, PiezasSueltas_Comercial, 
		PrecioUnitario, PrecioLicitacion, PrecioLicitacionUnitario, ImporteEAN, ImporteEAN_Licitado, SubTotalLicitacion_0, SubTotalLicitacion, IvaLicitacion, TotalLicitacion, 
		Cantidad__Negado, PrecioLicitacion__Negado, ImporteEAN__Negado, SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal, 
		FechaProcesamiento 	
	) 
	Select  
		IdPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, FolioReferencia, '' as IdTipoDeDispensacion, 
		EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, NumReceta, FechaReceta, StatusVenta, SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, IdGrupoTerapeutico, GrupoTerapeutico, 
		IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, ContenidoPaquete, EsControlado, Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, TasaIva, 
		Cantidad, Cantidad_A_Cobro, Multiplo, PiezasTotales, Agrupacion, AgrupadoMenor, AgrupadoMayor, PiezasSueltas, 
		Agrupacion_Comercial, AgrupadoMenor_Comercial, AgrupadoMayor_Comercial, PiezasSueltas_Comercial, 
		PrecioUnitario, PrecioLicitacion, PrecioLicitacionUnitario, ImporteEAN, ImporteEAN_Licitado, SubTotalLicitacion_0, SubTotalLicitacion, IvaLicitacion, TotalLicitacion, 
		Cantidad__Negado, PrecioLicitacion__Negado, ImporteEAN__Negado, SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal, 
		getdate() as FechaProcesamiento 	
	From RptAdmonDispensacion_Detallado (NoLock) 




	--- Select * 
	Update D Set IdTipoDeDispensacion = I.IdTipoDeDispensacion 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado D (NoLock) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdFarmacia = I.IdFarmacia and D.Folio = I.FolioVenta ) 
	Where D.IdTipoDeDispensacion = '' -- and convert(varchar(10), FechaRegistro, 120) >= '2017-01-01' 


End 
Go--#SQL 

