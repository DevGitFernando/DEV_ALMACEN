-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetInformacion_Remision_ToExcel__11_SSG' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetInformacion_Remision_ToExcel__11_SSG
Go--#SQL
  
Create Proc spp_FACT_RTP__GetInformacion_Remision_ToExcel__11_SSG 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '1',  @FolioRemision varchar(10) = '0000021569' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set Dateformat YMD 
Declare 
	@IdFuenteFinanciamiento varchar(8), 
	@IdFinanciamiento varchar(8), 
	@iLargoMenor int, 
	@iLargoMayor int  	
	
Declare 
	@sSql varchar(max),  
	@sFiltro varchar(max)

	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  
	
	Set @sSql = '' 
	Set @sFiltro = ' and E.Cantidad > 0 and E.Cantidad_Agrupada > 0  ' 
	Set @sFiltro = ' and E.Cantidad > 0 and E.Cantidad_Agrupada > 0 and E.PrecioLicitado > 0 ' 

--	select * from FACT_Remisiones 

	------------------------------------------------------------------------------------------------------------------------------------------------------------ 
	----------------------------------------- SE OBTIENE LOS DATOS PRINCIPALES 
	


--------------------------		spp_FACT_RTP__GetInformacion_Remision__28_SST 

-----	sp_listacolumnas vw_FACT_Remisiones_Detalles 

	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		FolioVenta, FechaRemision, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		IdCliente, IdSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, cast('' as varchar(10))  as FechaDeCaducidad, 
		IdClaveSSA, ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, 
		Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, 
		getdate() as FechaVenta, cast('' as varchar(200)) as NumeroDeReceta, cast('' as varchar(20)) as FechaReceta, 
		cast('' as varchar(200)) as IdBeneficiario, cast('' as varchar(2000)) as NombreBeneficiario, cast('' as varchar(20)) as NumeroDeReferencia   

		 , cast('' as varchar(max)) as InformacionComplemento   
		 , cast('' as varchar(max)) as PartidaPresupuestariaDescripcion  
		 , cast('' as varchar(max)) as ClaveSSA_Mascara  
		 , cast('' as varchar(max)) as Descripcion_Mascara  
		 , cast('' as varchar(max)) as Presentacion_Mascara  
		 , cast('' as varchar(max)) as AIE   
		 , cast('' as varchar(max)) as Causes_NoCauses  
		 , getdate() as FechaImpresion  
	Into #vw_FACT_Remisiones_Detalles 
	From vw_FACT_Remisiones_Detalles E (NoLock) 
	Where 1 = 0  

	Set @sSql = '' + 
		'Insert Into #vw_FACT_Remisiones_Detalles ' + 
		'( ' + 
		'	IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, 
			FolioVenta, FechaRemision, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
			IdCliente, IdSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
			IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
			IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, FechaDeCaducidad, IdClaveSSA, ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, 
			Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, 
			FechaVenta, NumeroDeReceta, FechaReceta, IdBeneficiario, NombreBeneficiario, NumeroDeReferencia,  
			PartidaPresupuestariaDescripcion, ClaveSSA_Mascara, Descripcion_Mascara, AIE, Causes_NoCauses, FechaImpresion ' +
		') ' +
		'Select ' + char(10) + 
		'	IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, ' + char(10) + 
		'   Referencia_Beneficiario, Referencia_NombreBeneficiario, ' + char(10) +  
		'	FolioVenta, FechaRemision, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, ' + char(10) + 
		'	IdCliente, IdSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, ' + char(10) + 
		'	IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, ' + char(10) + 
		'	IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, CampoDeControl as FechaDeCaducidad, IdClaveSSA, ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, ' + char(10) + 
		'	Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, ' + char(10) + 
		'	CampoDeControl as FechaVenta, CampoDeControl as NumeroDeReceta, CampoDeControl as NumeroDeReferencia, ' + char(10) + 
		'	CampoDeControl as IdBeneficiario, CampoDeControl as NombreBeneficiario, CampoDeControl as FechaReceta, ' + char(10) + 
		'	CampoDeControl as PartidaPresupuestariaDescripcion, CampoDeControl as ClaveSSA_Mascara, ' + char(10) + 
		'	CampoDeControl as Descripcion_Mascara, CampoDeControl as AIE, CampoDeControl as Causes_NoCauses, getdate() as FechaImpresion '   + char(10) + 
		'From vw_FACT_Remisiones_Detalles E (NoLock)  ' + char(10) + 
		'Where E.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' And E.IdEstado = ' + char(39) + @IdEstado + char(39) + 
			' And E.IdFarmacia = ' + char(39) + @IdFarmaciaGenera + char(39) + ' And E.FolioRemision = ' + char(39) + @FolioRemision + char(39) + ' ' + @sFiltro 
	Exec(@sSql) 
	Print @sSql 


	--Update R Set  FolioFacturaElectronica = dbo.fg_FACT_GetFacturaElectronica_Remision(R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.FolioRemision) 
	--From #vw_FACT_Remisiones_Detalles R (NoLock) 
	
	Update D Set FechaDeCaducidad = convert(varchar(7), F.FechaCaducidad, 120)   
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado and D.IdFarmaciaDispensacion = F.IdFarmacia and D.IdSubFarmacia = F.IdSubFarmacia 
			and D.IdProducto = F.IdProducto and D.CodigoEAN = F.CodigoEAN and D.ClaveLote = F.ClaveLote ) 


	--Update D Set  FolioFacturaElectronica = F.FolioFacturaElectronica 
	--From #vw_FACT_Remisiones_Detalles D (NoLock) 
	--Inner Join FACT_Facturas F (NoLock) On ( D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia
	--	and F.FolioRemision = D.FolioRemision and F.Status = 'A' )  


	Update D Set  NumeroDeReceta = '', FechaReceta = '', IdBeneficiario = '', NombreBeneficiario = '', NumeroDeReferencia = '' 
	From #vw_FACT_Remisiones_Detalles D (NoLock) 


	Update D  Set FechaVenta = E.FechaRegistro, IdBeneficiario = I.IdBeneficiario, NumeroDeReceta = NumReceta, FechaReceta = convert(varchar(10), I.FechaReceta, 120) 
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	Inner Join VentasEnc E (NoLock) On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmaciaDispensacion = E.IdFarmacia and D.FolioVenta = E.FolioVenta ) 
	Inner Join VentasInformacionAdicional I (NoLock) On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	--where len(NumReceta) <= 20 


	Update D Set NombreBeneficiario = ( B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre ), NumeroDeReferencia = FolioReferencia  
	From #vw_FACT_Remisiones_Detalles D (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( D.IdEstado = B.IdEstado and D.IdFarmaciaDispensacion = B.IdFarmacia and D.IdCliente = B.IdCliente and D.IdSubCliente = B.IdSubCliente 
			and D.IdBeneficiario = B.IdBeneficiario ) 

	Update E Set ClaveSSA_Mascara = M.Mascara, Descripcion_Mascara = M.DescripcionMascara, Presentacion_Mascara = M.Presentacion 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join vw_ClaveSSA_Mascara M (NoLock) On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente  and E.ClaveSSA = M.ClaveSSA ) 



	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FechaRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, 
		Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		-- IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 	 
		sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar, sum(SubTotalGrabado_Clave) as SubTotalGrabado, 
		sum(Iva_Clave) as IVA, sum(Importe_Clave) as Total  
	Into #vw_FACT_Remisiones 
	From #vw_FACT_Remisiones_Detalles 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FechaRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, 
		Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		--IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc 

	----------------------------------------- SE OBTIENE LOS DATOS PRINCIPALES 
	------------------------------------------------------------------------------------------------------------------------------------------------------------ 



--------------------------		spp_FACT_RTP__GetInformacion_Remision__28_SST 


	---------------------------- SALIDA FINAL 
	Select 
		--IdEmpresa, 
		--IdEstado, 
		--IdFarmacia, 
		'Folio de remisión' = FolioRemision, 
		'Fecha de remisión' = FechaRemision, 
		'Folio de factura electrónica' = FolioFacturaElectronica, 
		--IdFarmaciaDispensacion, 
		'Unidad atendida' = FarmaciaDispensacion, 
		-- '' = Referencia_Beneficiario, 
		-- 'Nombre unidad atendida' = Referencia_NombreBeneficiario, 
		'Número de contrato' = NumeroDeContrato, 
		--IdFuenteFinanciamiento, 
		--IdFinanciamiento, 
		'Fuente de financiamiento' = Financiamiento, 
		--IdDocumento, 
		'Número de documento OC' = NombreDocumento, 
		--EsFacturable, 
		--TipoDeRemision, 
		'Tipo de remisión' = TipoDeRemisionDesc, 
		--OrigenInsumo, 
		'Origen de insumo' = OrigenInsumoDesc, 
		--TipoInsumo, 
		'Tipo de insumo' = TipoDeInsumoDesc, 	 
		'SubTotal sin grabar' = SubTotalSinGrabar, 'SubTotal grabado' = SubTotalGrabado, 'IVA' = IVA, 'Importe' = Total  		 
	From #vw_FACT_Remisiones 


--------------------------		spp_FACT_RTP__GetInformacion_Remision__28_SST 
/*
		IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		FolioVenta, FechaRemision, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		IdProducto, CodigoEAN, Descripcion, ClaveLote, IdClaveSSA, ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, 
		Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, 

		getdate() as FechaVenta, cast('' as varchar(20)) as NumeroDeReceta, cast('' as varchar(20)) as FechaReceta, 
		cast('' as varchar(200)) as NombreBeneficiario  

*/ 

	Select 
		-- IdPrograma, 
		--'Programa de atención' = Programa, 
		-- IdSubPrograma, 
		--'SubPrograma de atención' = SubPrograma, 
		'Folio de dispensación' = FolioVenta, 
		'Fecha de dispensación' = FechaVenta, 
		'Número de receta' = NumeroDeReceta, 
		'Fecha de receta' = FechaReceta, 
		'Nombre del beneficiario' = NombreBeneficiario, 
		'Número de referencia del beneficiario' = NumeroDeReferencia, 

		--'Clave SSA' = ClaveSSA_Mascara, 
		--'Descripción Clave SSA' = Descripcion_Mascara, 
		--'Presentaciòn' = Presentacion_Mascara, 

		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		--'Presentaciòn' = Presentacion, 


		-- TipoDeClave, 
		-- 'Tipo de insumo' = TipoDeClaveDescripcion, 
		'Id Producto' = IdProducto, 'Código EAN' = CodigoEAN, 'Descripción comercial' = Descripcion, 'Clave de lote' = ClaveLote, 
		'Caducidad' = FechaDeCaducidad, 
		'Precio licitado' = PrecioLicitado, --PrecioLicitadoUnitario, 
		--Cantidad, 
		'Cantidad' = Cantidad_Agrupada, 
		--TasaIva, 
		'SubTotal sin grabar' = SubTotalSinGrabar_Clave, 'SubTotal grabado' = SubTotalGrabado_Clave, 'IVA' = IVA_Clave, 'Importe' = Importe_Clave 	 
	From #vw_FACT_Remisiones_Detalles 
	Order by FolioVenta, ClaveSSA 

	--Select * 
	--From #tmpCodigos (NoLock) 
	--Order By IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, ClaveSSA


	--Select * From #tmpDetalles (NoLock) Order By 'Id Farmacia', IdPrograma, IdSubPrograma, 'Clave SSA'   	
	
	
End
Go--#SQL

