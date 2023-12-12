-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF
Go--#SQL
  
Create Proc spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF 
( 
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '1', 
	@IdFarmaciaGenera varchar(4) = '0001',  
	@Serie varchar(10) = 'PHJGTOA', @Folio varchar(50) = '252',    
	@OrigenDeDatos int = 1   ---- 1 ==> Factura | 0 ==> Remisiones 
) 
With Encryption 
As  
Begin 
Set NoCount On 

Declare 
	@sGUID varchar(500),  
	@sNombreEmpresa varchar(500), 
	@sNombreEstado varchar(500),   
	@MostrarInformacionDeFactura int 


	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	--Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  
	Set @sNombreEmpresa = '' 
	Set @sNombreEstado = '' 
	Set @MostrarInformacionDeFactura = @OrigenDeDatos     
	Set @Folio = REPLACE(@Folio, ',', '') 

	Set @sGUID = cast(NEWID() as varchar(100)) 
	Select @sNombreEmpresa = Nombre From CatEmpresas (NoLock) Where IdEmpresa = @IdEmpresa
	Select top 1 @sNombreEstado = Nombre From CatEstados (NoLock) Where IdEstado = @IdEstado 

	
	------------------------- Tablas temporales 
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 


	Select * 
	into #vw_ClaveSSA_Mascara 
	from vw_ClaveSSA_Mascara 



--------------------------------------------------------------------------------- 
	Select 
		GUID, IdEmpresa, cast('' as varchar(500)) as Empresa, 
		IdEstado, Estado, IdFarmacia, cast(FolioRemision as varchar(50)) as FolioRemision, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, FolioVenta, FechaInicial, FechaFinal, FechaReceta, FechaRemision, 
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, Referencia_01, IdClaveSSA, 
		ClaveSSA, cast(DescripcionClave as varchar(7000)) as DescripcionClave, 
		Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
		PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, CampoDeControl	 
		, 0 as ExisteEnFactura 
		 , getdate() as FechaImpresion  
	Into #vw_FACT_Remisiones_Detalles 
	From vw_FACT_Remisiones_Detalles E (NoLock) 
	Where 1 = 0 


	---------------------------------------- Optimizar tiempo de proceso 
	Select 
		*, 
		--dbo.fg_FACT_GetFacturaElectronica_Remision(R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.FolioRemision) as FolioFacturaElectronica_Auxiliar, 
		-- cast('' as varchar(500)) as FolioFacturaElectronica, 
		cast('' as varchar(500)) as FarmaciaDispensacion, 
		cast('' as varchar(500)) as Programa, 
		cast('' as varchar(500)) as SubPrograma 
	Into #vw_FACT_Remisiones___Intermedia
	From vw_FACT_Remisiones R (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and EsRelacionFacturaPrevia = 1

	----Update F Set FolioFacturaElectronica = FolioFacturaElectronica_Auxiliar 
	----From #vw_FACT_Remisiones___Intermedia F 



	Select 
		R.GUID, R.IdEmpresa, cast('' as varchar(500)) as Empresa, 
		R.IdEstado, R.Estado, R.IdFarmacia, cast(R.FolioRemision as varchar(50)) as FolioRemision, R.EsRelacionFacturaPrevia, R.EsRelacionMontos, 
		R.Serie, R.Folio, 
		--dbo.fg_FACT_GetFacturaElectronica_Remision(R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.FolioRemision) as FolioFacturaElectronica, 
		
		R.FolioFacturaElectronica, 
		D.IdFarmacia as IdFarmaciaDispensacion, 
		R.FarmaciaDispensacion, 
		
		R.Referencia_Beneficiario, R.Referencia_NombreBeneficiario, 
		D.FolioVenta, R.FechaInicial, R.FechaFinal, 
		cast('' as varchar(500)) as FechaReceta, 
		R.FechaRemision, 
		R.IdCliente, R.Cliente, R.IdSubCliente, R.SubCliente, R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, R.IdDocumento, R.NombreDocumento, 
		IdPrograma, Programa, 
		IdSubPrograma, SubPrograma, 
		R.EsFacturada, R.EsFacturable, R.TipoDeRemision, R.TipoDeRemisionDesc, R.OrigenInsumo, R.OrigenInsumoDesc, 
		R.TipoInsumo, R.TipoDeInsumoDesc, R.IdPersonalRemision, R.SubTotalSinGrabar, R.SubTotalGrabado, R.Iva, R.Total, R.Observaciones, R.ObservacionesRemision, R.Status, 	
		
		D.IdSubFarmacia, D.IdProducto, D.CodigoEAN, cast('' as varchar(1000)) as Descripcion, D.ClaveLote, D.Referencia_01, 
		cast('' as varchar(500)) as IdClaveSSA, ClaveSSA, cast('' as varchar(7500)) as DescripcionClave, 
		cast('' as varchar(500)) as Presentacion_ClaveSSA, cast('' as varchar(500)) as TipoDeClave, cast('' as varchar(500)) as TipoDeClaveDescripcion, 
		D.PrecioLicitado, D.PrecioLicitadoUnitario, D.Cantidad, D.Cantidad_Agrupada, D.TasaIva, 
		D.SubTotalSinGrabar as SubTotalSinGrabar_Clave, D.SubTotalGrabado as SubTotalGrabado_Clave, D.Iva as Iva_Clave, D.Importe as Importe_Clave 
	
		, 0 as ExisteEnFactura 
		, getdate() as FechaImpresion 
		, cast('' as varchar(500)) as CampoDeControl 	 
	Into #vw_FACT_Remisiones_Detalles__Intermedia 
	From #vw_FACT_Remisiones___Intermedia R (NoLock) 
	Inner Join FACT_Remisiones_Detalles D (NoLock) On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision ) 
	-- Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and EsRelacionFacturaPrevia = 1
		-- and 1 = 0 
	-- Where D.ClaveSSA = '010.000.2126.00' 


	Update R Set Descripcion = S.Descripcion, DescripcionClave = S.DescripcionClave, Presentacion_ClaveSSA = S.Presentacion_ClaveSSA, TipoDeClave = S.TipoDeClave, TipoDeClaveDescripcion = S.TipoDeClaveDescripcion 
	From #vw_FACT_Remisiones_Detalles__Intermedia R (NoLock) 
	Inner Join #vw_Productos_CodigoEAN S (NoLock) On ( R.IdProducto = S.IdProducto and R.CodigoEAN = S.CodigoEAN ) 

	---------------------------------------- Optimizar tiempo de proceso 



	Insert Into #vw_FACT_Remisiones_Detalles 
	( 
		GUID, IdEmpresa, Empresa, 
		IdEstado, Estado, IdFarmacia, FolioRemision, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, FolioVenta, FechaInicial, FechaFinal, FechaReceta, FechaRemision, 
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, Referencia_01, IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
		PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, CampoDeControl, 
		ExisteEnFactura, FechaImpresion  
	) 
	Select 
		(case when @MostrarInformacionDeFactura = 1 then '' else GUID end) as GUID, 
		IdEmpresa, cast('' as varchar(500)) as Empresa, 
		IdEstado, Estado, IdFarmacia, 
		
		--(Serie + ' - ' + cast(Folio as varchar(20)) ) as FolioRemision, 
		FolioRemision,  

		EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		IdFarmaciaDispensacion, FarmaciaDispensacion, 

		(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_Beneficiario end) as Referencia_Beneficiario, 
		(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_NombreBeneficiario end) as Referencia_NombreBeneficiario, 
		(case when @MostrarInformacionDeFactura = 1 then '' else FolioVenta end) as FolioVenta, 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaInicial end) as FechaInicial, 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaFinal end) as FechaFinal, 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaReceta end) as FechaReceta, 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaRemision end) as FechaRemision, 
		
		
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		
		-- IdPrograma, Programa, IdSubPrograma, SubPrograma, 	
		'' as IdPrograma, '' as Programa, '' as IdSubPrograma, '' as SubPrograma, 

		EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		
		
		-- IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, 
		'' as IdSubFarmacia, '' as IdProducto, '' as CodigoEAN, '' as Descripcion, '' as ClaveLote, 		

		Referencia_01, 
		
		'' as IdClaveSSA, ClaveSSA, 
		(case when @MostrarInformacionDeFactura = 1 then '' else DescripcionClave end) as DescripcionClave, 	
		'' as Presentacion_ClaveSSA, 
		
		TipoDeClave, TipoDeClaveDescripcion, 
		PrecioLicitado, PrecioLicitadoUnitario, 

		sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
		TasaIva, 
		sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, sum(Iva_Clave) as Iva_Clave, sum(Importe_Clave) as Importe_Clave, 
		CampoDeControl	 

		, 0 as ExisteEnFactura 
		, getdate() as FechaImpresion  
	From #vw_FACT_Remisiones_Detalles__Intermedia E (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio and EsRelacionFacturaPrevia = 1
		--and ClaveSSA = 'SC-MC-1625'  
	Group by 
		(case when @MostrarInformacionDeFactura = 1 then '' else GUID end), 
		IdEmpresa, 
		IdEstado, Estado, IdFarmacia, 	
		FolioRemision,  
		EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		IdFarmaciaDispensacion, FarmaciaDispensacion, 
		-- Referencia_Beneficiario, Referencia_NombreBeneficiario, FolioVenta, FechaInicial, FechaFinal, FechaReceta, FechaRemision, 
		
		(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_Beneficiario end), 
		(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_NombreBeneficiario end), 
		(case when @MostrarInformacionDeFactura = 1 then '' else FolioVenta end), 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaInicial end), 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaFinal end), 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaReceta end), 
		(case when @MostrarInformacionDeFactura = 1 then '' else FechaRemision end), 
		
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 	
		EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 		
		Referencia_01, 	
		ClaveSSA, 
		(case when @MostrarInformacionDeFactura = 1 then '' else DescripcionClave end), 		
		TipoDeClave, TipoDeClaveDescripcion, 
		PrecioLicitado, PrecioLicitadoUnitario, 
		-- Cantidad, Cantidad_Agrupada, 
		TasaIva, 
		-- SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, 
		CampoDeControl	  


	--select * from #vw_FACT_Remisiones_Detalles  



	If Not Exists ( select top 1 * From #vw_FACT_Remisiones_Detalles ) 
	Begin 
		Print 'PROCESO AUXILIAR'
		Insert Into #vw_FACT_Remisiones_Detalles 
		( 
			GUID, IdEmpresa, Empresa, 
			IdEstado, Estado, IdFarmacia, FolioRemision, EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
			IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, FolioVenta, FechaInicial, FechaFinal, FechaReceta, FechaRemision, 
			IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
			IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
			TipoInsumo, TipoDeInsumoDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
			IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, Referencia_01, IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
			PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, CampoDeControl, 
			ExisteEnFactura, FechaImpresion  
		) 
		Select 
			GUID, IdEmpresa, cast('' as varchar(500)) as Empresa, 
			IdEstado, Estado, IdFarmacia, 
		
			-- (Serie + ' - ' + cast(Folio as varchar(20)) ) as FolioRemision, 
			FolioRemision,  		

			EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
			IdFarmaciaDispensacion, FarmaciaDispensacion, 
			
			-- Referencia_Beneficiario, Referencia_NombreBeneficiario, FolioVenta, FechaInicial, FechaFinal, FechaReceta, FechaRemision, 	
			(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_Beneficiario end) as Referencia_Beneficiario, 
			(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_NombreBeneficiario end) as Referencia_NombreBeneficiario, 
			(case when @MostrarInformacionDeFactura = 1 then '' else FolioVenta end) as FolioVenta, 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaInicial end) as FechaInicial, 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaFinal end) as FechaFinal, 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaReceta end) as FechaReceta, 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaRemision end) as FechaRemision, 
			
			IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		
			-- IdPrograma, Programa, IdSubPrograma, SubPrograma, 	
			'' as IdPrograma, '' as Programa, '' as IdSubPrograma, '' as SubPrograma, 	
			
			EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
			TipoInsumo, TipoDeInsumoDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
			
			-- IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, 
			'' as IdSubFarmacia, '' as IdProducto, '' as CodigoEAN, '' as Descripcion, '' as ClaveLote, 		
			
			
			Referencia_01, 
			
			'' as IdClaveSSA, ClaveSSA, 
			(case when @MostrarInformacionDeFactura = 1 then '' else DescripcionClave end) as DescripcionClave, 		
			'' as Presentacion_ClaveSSA, 
			
			TipoDeClave, TipoDeClaveDescripcion, 
			PrecioLicitado, PrecioLicitadoUnitario, 
			sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
			TasaIva, 
			sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, sum(Iva_Clave) as Iva_Clave, sum(Importe_Clave) as Importe_Clave, 
			CampoDeControl	 
			, 0 as ExisteEnFactura 
			, getdate() as FechaImpresion  
		From vw_FACT_Remisiones_Detalles E (NoLock) 
		Where -- IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera and Serie = @Serie and Folio = @Folio 
			1 = 0 and 
			Exists 
			( 
				Select * 
				From FACT_Facturas F (NoLock) 
				Where F.IdEmpresa = E.IdEmpresa and F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and F.FolioRemision = E.FolioRemision and F.Status = 'A' 
					  and F.FolioFacturaElectronica = @Serie + ' - ' + cast(@Folio as varchar(100)) 
			) 
			--and ClaveSSA = 'SC-MC-1625'  
		Group by 
			GUID, IdEmpresa, 
			IdEstado, Estado, IdFarmacia, 	
			FolioRemision,  
			EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
			IdFarmaciaDispensacion, FarmaciaDispensacion, 		
			-- Referencia_Beneficiario, Referencia_NombreBeneficiario, FolioVenta, FechaInicial, FechaFinal, FechaReceta, FechaRemision, 
		
			(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_Beneficiario end), 
			(case when @MostrarInformacionDeFactura = 1 then '' else Referencia_NombreBeneficiario end), 
			(case when @MostrarInformacionDeFactura = 1 then '' else FolioVenta end), 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaInicial end), 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaFinal end), 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaReceta end), 
			(case when @MostrarInformacionDeFactura = 1 then '' else FechaRemision end), 			
			
			
			IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 	
			EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
			TipoInsumo, TipoDeInsumoDesc, IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 		
			Referencia_01, 	
			ClaveSSA, 
			(case when @MostrarInformacionDeFactura = 1 then '' else DescripcionClave end), 		
			TipoDeClave, TipoDeClaveDescripcion, 
			PrecioLicitado, PrecioLicitadoUnitario, 
			-- Cantidad, Cantidad_Agrupada, 
			TasaIva, 
			-- SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave, 
			CampoDeControl	 



	End 









	-------------------------------  Quitar TAMAULIPAS 20181214.1351 
	--Update E Set FechaRemision = F.FechaRegistro 
	--From #vw_FACT_Remisiones_Detalles E (NoLock) 
	--Inner Join FACT_CFD_Documentos_Generados F (NoLock) 
	--	On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and E.Serie = F.Serie and E.Folio = F.Folio ) 



	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 



--------------------------------------------------------------------------------- 
--		spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF

	Select 
		0 as Agrupamiento, 
		@sGUID as GUID, 
		IdEmpresa, Empresa, 
		IdEstado, Estado, IdFarmacia, 
		--FolioRemision, 
		(case when @MostrarInformacionDeFactura = 1 then '' else FolioRemision end) as FolioRemision, 
		EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, 


		--FolioVenta, FechaInicial, FechaFinal, FechaReceta, 
		FechaRemision, 
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, 
		--IdPersonalRemision, 
		--SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		-- IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, Referencia_01, 
		cast(IdClaveSSA as varchar(50)) as IdClaveSSA, ClaveSSA, 
		DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
		PrecioLicitado, PrecioLicitadoUnitario, 
		sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
		TasaIva, 
		sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, 
		sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
		sum(Iva_Clave) as Iva_Clave, 
		sum(Importe_Clave) as Importe_Clave, CampoDeControl, 
		0 as ExisteEnFactura,  
		getdate() as FechaImpresion  	 
	Into #vw_FACT_Remisiones_Concentrado__Claves 
	From #vw_FACT_Remisiones_Detalles 
	Group by 
		--GUID, 
		IdEmpresa, Empresa, 
		IdEstado, Estado, IdFarmacia, 
		--FolioRemision, 
		(case when @MostrarInformacionDeFactura = 1 then '' else FolioRemision end), 
		EsRelacionFacturaPrevia, EsRelacionMontos, Serie, Folio, FolioFacturaElectronica, 
		IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		--FolioVenta, FechaInicial, FechaFinal, FechaReceta, 
		FechaRemision, 
		IdCliente, Cliente, IdSubCliente, SubCliente, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturada, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, 
		-- IdPersonalRemision, 
		-- SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status,
		IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion,  
		PrecioLicitado, PrecioLicitadoUnitario, 
		TasaIva, CampoDeControl  



	Update E Set  DescripcionClave = M.DescripcionMascara + ' -- ' + M.Presentacion 
--		ClaveSSA_Mascara = M.Mascara, Descripcion_Mascara = M.DescripcionMascara, Presentacion_Mascara = M.Presentacion 
	From #vw_FACT_Remisiones_Concentrado__Claves E (NoLock) 
	Inner Join #vw_ClaveSSA_Mascara M (NoLock) On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente  and E.ClaveSSA = M.ClaveSSA ) 





	----------------- ESPECIAL TAMAULIPAS 
	If @MostrarInformacionDeFactura = 1 
	Begin 


	--		spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF  

		--select * from #vw_FACT_Remisiones_Concentrado__Claves 


		--select 
		--	IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, -- TipoDeClave, TipoDeClaveDescripcion, 
		--	PrecioLicitado, PrecioLicitadoUnitario, 
		--	sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
		--	sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, 
		--	sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
		--	sum(Iva_Clave) as Iva_Clave, 
		--	sum(Importe_Clave) as Importe_Clave 
		--into #tmp__X01 
		--from #vw_FACT_Remisiones_Concentrado__Claves 
		--Group by 
		--	IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
		--	PrecioLicitado, PrecioLicitadoUnitario 


		Update C Set 
			DescripcionClave = F.DescripcionConcepto, 
			Cantidad = F.Cantidad, Cantidad_Agrupada = F.Cantidad, 
			PrecioLicitado = F.PrecioUnitario, 
			PrecioLicitadoUnitario = F.PrecioUnitario, 
			SubTotalSinGrabar_Clave = (case when C.TasaIva = 0 then F.SubTotal else 0 end), 
			SubTotalGrabado_Clave = (case when C.TasaIva > 0 then F.SubTotal else 0 end),  
			Iva_Clave = F.IVA, 
			Importe_Clave = F.Total, 
			ExisteEnFactura = 1 
		From #vw_FACT_Remisiones_Concentrado__Claves C (NoLock)
		Inner Join FACT_CFD_Documentos_Generados_Detalles F (NoLock) On ( C.ClaveSSA = F.Clave ) 
		Where 
			F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmaciaGenera and F.Serie = @Serie and F.Folio = @Folio  
			-- and F.DescripcionConcepto <> C.DescripcionClave  


		Delete From #vw_FACT_Remisiones_Concentrado__Claves Where ExisteEnFactura = 0 


		---		spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF	

		------------------------------------------ ESPECIAL TAMAULIPAS-GUANAJUATO    
		If Not Exists ( select top 1 * from #vw_FACT_Remisiones_Concentrado__Claves ) 
		Begin 

		--		sp_listacolumnas vw_FACT_CFD_DocumentosElectronicos 

			Insert Into #vw_FACT_Remisiones_Concentrado__Claves 
			Select 
				0 as Agrupamiento, 
				@sGUID as GUID, 
				E.IdEmpresa, E.Empresa, 
				E.IdEstado, Estado, E.IdFarmacia, 

				--(case when @MostrarInformacionDeFactura = 1 then '' else '' end) as FolioRemision, 
				(E.Serie + '-' + cast(E.Folio as varchar(10))) as FolioRemision,  
				0 as EsRelacionFacturaPrevia, 0 as EsRelacionMontos, E.Serie, E.Folio, (E.Serie + '-' + cast(E.Folio as varchar(10)))  as FolioFacturaElectronica, 
				
				'' as IdFarmaciaDispensacion, 
				(case when E.Observaciones_02 <> '' then E.Observaciones_02 else E.Observaciones_03 end) as FarmaciaDispensacion, 
				'' as Referencia_Beneficiario, '' as Referencia_NombreBeneficiario, 


				E.FechaRegistro as FechaRemision, 
				'' as IdCliente, E.NombreReceptor as Cliente, '' as IdSubCliente, E.NombreReceptor as SubCliente, 
				'' as NumeroDeContrato, 
				'' as IdFuenteFinanciamiento, '' as IdFinanciamiento, '' as Financiamiento, '' as IdDocumento, '' as NombreDocumento, 
				'' as IdPrograma, '' as Programa, '' as IdSubPrograma, '' as SubPrograma, 
				1 as EsFacturada, 0 as EsFacturable, 0 as TipoDeRemision, upper(TipoDocumentoDescripcion) as TipoDeRemisionDesc, 0 as OrigenInsumo, 'VENTA' as OrigenInsumoDesc, 
				0 as TipoInsumo, upper(E.TipoInsumoDescripcion) as TipoDeInsumoDesc, 


				D.Clave as IdClaveSSA, D.Clave as ClaveSSA, left(D.DescripcionConcepto, 2000) as DescripcionClave, 
				'' as Presentacion_ClaveSSA, 0 as TipoDeClave, '' as TipoDeClaveDescripcion, 
				D.PrecioUnitario as PrecioLicitado, D.PrecioUnitario as PrecioLicitadoUnitario, 
				sum(D.Cantidad) as Cantidad, sum(Cantidad) as Cantidad_Agrupada, 
				D.TasaIva, 

				sum( (case when D.TasaIVA = 0 Then D.SubTotal Else 0 End) ) as SubTotalSinGrabar_Clave, 
				sum( (case when D.TasaIVA > 0 Then D.SubTotal Else 0 End) ) as SubTotalGrabado_Clave, 
				sum(D.Iva) as Iva_Clave, 
				sum(D.Total) as Importe_Clave, 

				--sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, 
				--sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
				--sum(Iva_Clave) as Iva_Clave, 
				--sum(Importe_Clave) as Importe_Clave, 
				
				'' as CampoDeControl, 
				0 as ExisteEnFactura,  
				getdate() as FechaImpresion  	 

			From FACT_CFD_Documentos_Generados_Detalles D (NoLock) 
			Inner Join vw_FACT_CFD_DocumentosElectronicos E (NoLock) 
				On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.Serie = E.Serie and D.Folio = E.Folio )  
			Where 
				D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmaciaGenera and D.Serie = @Serie and D.Folio = @Folio 
			Group by 
				E.IdEmpresa, E.Empresa, 
				E.IdEstado, Estado, E.IdFarmacia, 

				E.Serie, E.Folio, 
				--E.Observaciones_02, 
				(case when E.Observaciones_02 <> '' then E.Observaciones_02 else E.Observaciones_03 end), 
				E.FechaRegistro, 
				E.NombreReceptor, 
				E.TipoDocumentoDescripcion, E.TipoInsumoDescripcion, 

				D.Clave, D.DescripcionConcepto, 
				D.PrecioUnitario, D.PrecioUnitario, 
				D.TasaIva   


			------Select * 
			------From FACT_CFD_Documentos_Generados_Detalles F (NoLock) 
			------Where 
			------	F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmaciaGenera and F.Serie = @Serie and F.Folio = @Folio  
		End 


		----select C.* 
		----From #vw_FACT_Remisiones_Concentrado__Claves C (NoLock)
		----Inner Join FACT_CFD_Documentos_Generados_Detalles F (NoLock) On ( C.ClaveSSA = F.Clave ) 
		----Where 
		----	F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmaciaGenera and F.Serie = @Serie and F.Folio = @Folio  
		----	and C.Importe_Clave <> F.Total 

	--		spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF  


		--select 
		--	IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, -- TipoDeClave, TipoDeClaveDescripcion, 
		--	PrecioLicitado, PrecioLicitadoUnitario, 
		--	sum(Cantidad) as Cantidad, sum(Cantidad_Agrupada) as Cantidad_Agrupada, 
		--	sum(SubTotalSinGrabar_Clave) as SubTotalSinGrabar_Clave, 
		--	sum(SubTotalGrabado_Clave) as SubTotalGrabado_Clave, 
		--	sum(Iva_Clave) as Iva_Clave, 
		--	sum(Importe_Clave) as Importe_Clave 
		--into #tmp__X02 
		--from #vw_FACT_Remisiones_Concentrado__Claves 
		--Group by 
		--	IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion_ClaveSSA, TipoDeClave, TipoDeClaveDescripcion, 
		--	PrecioLicitado, PrecioLicitadoUnitario 
		


		--select X.*, '' as Separdor, Y.PrecioLicitado, Y.Cantidad, Y.Cantidad_Agrupada, Y.Importe_Clave  
		--From #tmp__X01 X 
		--Inner Join #tmp__X02 Y On ( X.claveSSA = Y.ClaveSSA ) 
		--Where X.Cantidad <> y.Cantidad or X.Cantidad_Agrupada <> Y.Cantidad_Agrupada or X.Importe_Clave = Y.Importe_Clave 
		--Order by X.ClaveSSA 

					 
	End 





	Select identity(int, 1, 1) as Agrupamiento, DescripcionClave  
	Into #vw_FACT_Remisiones_Concentrado__Claves___Orden 
	From #vw_FACT_Remisiones_Concentrado__Claves 
	Group by DescripcionClave 


	Update D Set Agrupamiento = A.Agrupamiento 
	From #vw_FACT_Remisiones_Concentrado__Claves D 
	Inner Join #vw_FACT_Remisiones_Concentrado__Claves___Orden A On ( D.DescripcionClave = A.DescripcionClave )




------------------------------------- SALIDA FINAL   
	Select * 
	From #vw_FACT_Remisiones_Concentrado__Claves 
	Where Cantidad > 0 and Cantidad_Agrupada > 0 
		-- and 1 = 0   
	Order by Agrupamiento 
		-- ClaveSSA, 
		-- DescripcionClave, 
		-- TipoDeInsumoDesc, TipoDeRemisionDesc, OrigenInsumoDesc     



	----Select sum(Importe_Clave) as Total 
	----From #vw_FACT_Remisiones_Concentrado__Claves 
	----Where Cantidad > 0 and Cantidad_Agrupada > 0 


---		spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToPDF  


End 
Go--#SQL  




/* 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToExcel' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToExcel 
Go--#SQL
  
Create Proc spp_FACT_RTP__GetInformacion_Factura_Remisiones_ToExcel 
( 
	@IdEmpresa varchar(3) = '2', @IdEstado varchar(2) = '9', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '1' 
)
With Encryption 
As
Begin 
Set NoCount On 


	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  
	

	If @IdEstado = '09' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__09_SSH_1N @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 

	If @IdEstado = '14' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__09_SSH_1N @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 


	If @IdEstado = '13' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__13_SSH @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 

	If @IdEstado = '28' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__28_SST @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 

	
End
Go--#SQL

*/ 
