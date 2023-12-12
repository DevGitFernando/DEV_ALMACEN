-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetInformacion_RemisionManual_ToPDF_13_SSH' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetInformacion_RemisionManual_ToPDF_13_SSH
Go--#SQL 

/* 

Exec spp_FACT_RTP__GetInformacion_RemisionManual_ToPDF_13_SSH  @IdEmpresa = '001', @IdEstado = '13', @IdFarmaciaGenera = '0001',  @FolioRemision = '20629' 

*/ 


Create Proc spp_FACT_RTP__GetInformacion_RemisionManual_ToPDF_13_SSH 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '11120' 
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sNombreEmpresa varchar(500), 
	@sNombreEstado varchar(500)  

	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  
	Set @sNombreEmpresa = '' 
	Set @sNombreEstado = '' 


	Select @sNombreEmpresa = Nombre From CatEmpresas (NoLock) Where IdEmpresa = @IdEmpresa
	Select top 1 @sNombreEstado = Nombre From CatEstados (NoLock) Where IdEstado = @IdEstado 


--------------------------------------------------------------------------------- 
	Select 
		cast('' as varchar(500)) as Empresa, 
		-- cast('' as varchar(500)) as Estado, 
		'M' + E.FolioRemision as FolioRemisionManual, 
		*  
		, cast('' as varchar(max)) as Caducidad  
		-- , cast(NumReceta + ' - ' + Alias as varchar(max)) as Observaciones_02  
		, cast('' as varchar(max)) as Observaciones_02 
		, cast('' as varchar(max)) as Observaciones_1N  
		, cast('' as varchar(max)) as InformacionComplemento   
		, cast('' as varchar(max)) as PartidaPresupuestariaDescripcion  
		, cast('' as varchar(max)) as TipoDeAccion   
		, cast('' as varchar(max)) as ClaveSSA_Mascara  
		, cast('' as varchar(max)) as Descripcion_Mascara  
		, cast('' as varchar(max)) as Presentacion_Mascara  
		, cast(Referencia_01 as varchar(max)) as AIE   
		, cast('' as varchar(max)) as Causes_NoCauses  

		, 1 as TipoDeBeneficiario 
		, cast('' as varchar(max)) as Referencia_01_IdJurisidccion  
		, cast('' as varchar(max)) as Referencia_01_Jurisidccion  
		, cast('' as varchar(max)) as Referencia_02_CLUES
		, cast('' as varchar(max)) as Referencia_03_IdBeneficiario 
		, cast('' as varchar(max)) as Referencia_03_NombreBeneficiario 


		, cast('' as varchar(max)) as Nombre_01_Director 
		, cast('' as varchar(max)) as Nombre_02_Administrador  
		, cast('' as varchar(max)) as Nombre_03_Responsable 
		, cast('' as varchar(max)) as Nombre_03_Responsable_02 
		, cast('' as varchar(max)) as Nombre_04_PersonalPropio  

		----, cast('Director' as varchar(max)) as Nombre_01_Director 
		----, cast('Administrador' as varchar(max)) as Nombre_02_Administrador  
		----, cast('Responsable' as varchar(max)) as Nombre_03_Responsable 
		----, cast('PersonalPropio' as varchar(max)) as Nombre_04_PersonalPropio  

		, getdate() as FechaImpresion  
		--getdate() as FechaFinal 
		--IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		--FolioVenta, FechaRemision, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		--IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		--IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		--IdProducto, CodigoEAN, Descripcion, ClaveLote, IdClaveSSA, ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, 
		--Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave 
	Into #vw_FACT_Remisiones_Detalles
	From vw_FACT_Remisiones_Manuales_Detalles E (NoLock) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision 
		and E.Cantidad > 0 and E.Cantidad_Agrupada > 0  



	---		Exec spp_FACT_RTP__GetInformacion_RemisionManual_ToPDF_13_SSH 

	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set FarmaciaDispensacion = F.NombreOficial 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Farmacias_CLUES F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and F.Status = 'A' ) 


	Update E Set PartidaPresupuestariaDescripcion = Referencia_03, TipoDeAccion = Referencia_05  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	--Inner Join FACT_Remisiones_InformacionAdicional I On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmaciaGenera and E.FolioRemision = I.FolioRemision ) 


	Update E Set ClaveSSA_Mascara = M.Mascara, Descripcion_Mascara = M.DescripcionMascara, Presentacion_Mascara = M.Presentacion 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join vw_ClaveSSA_Mascara M (NoLock) On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente  and E.ClaveSSA = M.ClaveSSA ) 



	---		Exec spp_FACT_RTP__GetInformacion_RemisionManual_ToPDF_13_SSH 


	Update E Set Caducidad = convert(varchar(7), L.FechaCaducidad, 120)  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmaciaDispensacion = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia
			and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote  ) 



	Update E Set 
		-- AIE = F.Referencia_01, 
		Causes_NoCauses = F.Referencia_04   
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F (NoLock) 
		On ( E.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and E.IdFinanciamiento = F.IdFinanciamiento 
			and E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and E.ClaveSSA = F.ClaveSSA ) 


	Update E Set 
		-- AIE = F.Referencia_01, 
		Causes_NoCauses = F.Referencia_04   
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia F (NoLock) 
		On ( E.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and E.IdFinanciamiento = F.IdFinanciamiento 
			and E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and E.ClaveSSA = F.ClaveSSA ) 
	Where Causes_NoCauses = '' or OrigenInsumo = 1 


	Update E Set AIE = F.Referencia_01, Causes_NoCauses = F.Referencia_02  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias F (NoLock) 
		On ( E.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and E.ClaveSSA = F.ClaveSSA ) 
	Where AIE = '' or Causes_NoCauses = '' 





	Update E Set InformacionComplemento = 'REMISIÓN PARA COBRO'
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Where EsRelacionFacturaPrevia = 0  

	Update E Set 
		InformacionComplemento = 'COMPROBACIÓN', 
		FolioFacturaElectronica = (case when Serie <> '' Then Serie + ' - ' +  cast(Folio as varchar(10)) else '' end) 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Where EsRelacionFacturaPrevia = 1 


	Update E Set Observaciones_02 = 
		-- right('0000' + cast( datepart(month, FechaReceta) as varchar(20)), 2) +  (case when Alias <> '' Then '-' + Alias else '' end) + (case when EsParaExcedente = 0 Then '-O' else '-E' end) 
		right('0000' + cast( datepart(month, FechaInicial) as varchar(20)), 2) +  (case when Alias <> '' Then '-' + Alias else '' end) + (case when EsParaExcedente = 0 Then '-O' else '-E' end)
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set Observaciones_1N = 
		NumReceta + (case when Alias <> '' Then '-' + Alias else '' end) + (case when EsParaExcedente = 0 Then '-O' else '-E' end)
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	

	---		Exec spp_FACT_RTP__GetInformacion_RemisionManual_ToPDF_13_SSH 



	---------------------------- Informacion para Primer Nivel 

	Update E Set TipoDeBeneficiario = I.TipoDeBeneficiario, Referencia_03_IdBeneficiario = Beneficiario 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Remisiones_InformacionAdicional_Almacenes I (NoLock) On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmaciaGenera and E.FolioRemision = I.FolioRemision ) 
	Where 1 = 0 

	Update E Set Referencia_02_CLUES = B.FolioReferencia, Referencia_01_IdJurisidccion = B.IdJurisdiccion, 
		Referencia_03_NombreBeneficiario = ltrim(rtrim(replace(B.Nombre, B.FolioReferencia, '')))   
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) On ( E.IdEstado = B.IdEstado and E.IdFarmaciaDispensacion = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente and E.Referencia_03_IdBeneficiario = B.IdBeneficiario )
	Where E.TipoDeBeneficiario = 1 



	Update E Set Referencia_02_CLUES = B.FolioReferencia, Referencia_01_IdJurisidccion = B.IdJurisdiccion, 
		Referencia_03_NombreBeneficiario = ltrim(rtrim(replace(B.Nombre, B.FolioReferencia, '')))   
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) On ( E.IdEstado = B.IdEstado and E.IdFarmaciaDispensacion = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente and E.IdBeneficiario = B.IdBeneficiario )
	Where E.TipoDeBeneficiario = 1 and Referencia_01_IdJurisidccion = '' 



	Update E Set Referencia_03_NombreBeneficiario = ltrim(rtrim(right(Referencia_03_NombreBeneficiario, len(Referencia_03_NombreBeneficiario) - 1))) 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Where left(Referencia_03_NombreBeneficiario, 1) = '-'  

	Update E Set Referencia_01_Jurisidccion = J.Descripcion 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join CatJurisdicciones J (NoLock) On ( E.IdEstado = J.IdEstado and E.Referencia_01_IdJurisidccion = J.IdJurisdiccion ) 

	---------------------------- Informacion para Primer Nivel 



	/* 
		, cast('' as varchar(max)) as Referencia_01_IdJurisidccion  
		, cast('' as varchar(max)) as Referencia_01_Jurisidccion  
		, cast('' as varchar(max)) as Referencia_02_CLUES
		, cast('' as varchar(max)) as Referencia_03_IdBeneficiario 
		, cast('' as varchar(max)) as Referencia_03_NombreBeneficiario 
	*/ 


	------------------------------ Asignacion de firmas 
	------ MATERIAL DE CURACIÓN 
	Update E Set 
		Nombre_01_Director = F.Nombre_01_Director, 
		Nombre_02_Administrador = F.Nombre_02_Administrador, 
		Nombre_03_Responsable = F.Nombre_03_Responsable, 
		Nombre_04_PersonalPropio = F.Nombre_04_PersonalPropio 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Remisiones___Firmas F (NoLock) On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and E.TipoInsumo = F.TipoDeInsumo ) 
	Where cast(E.TipoInsumo as int) = 1 

	------ MEDICAMENTO  
	Update E Set 
		Nombre_01_Director = F.Nombre_01_Director, 
		Nombre_02_Administrador = F.Nombre_02_Administrador, 
		Nombre_03_Responsable = F.Nombre_03_Responsable, 
		Nombre_04_PersonalPropio = F.Nombre_04_PersonalPropio 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Remisiones___Firmas F (NoLock) On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and E.TipoInsumo = F.TipoDeInsumo ) 
	Where cast(E.TipoInsumo as int) = 2  

	------------------------------ Asignacion de firmas 



--EsParaExcedente 

--------------------------------------------------------------------------------- 

------------------------------------- SALIDA FINAL 
	Select * 
	From #vw_FACT_Remisiones_Detalles 


End
Go--#SQL

