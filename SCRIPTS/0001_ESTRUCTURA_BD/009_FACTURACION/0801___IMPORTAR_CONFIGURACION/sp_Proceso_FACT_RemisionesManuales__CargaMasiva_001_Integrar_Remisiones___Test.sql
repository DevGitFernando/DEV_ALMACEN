--------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_RemisionesManuales__CargaMasiva_001_Integrar_Remisiones___Test' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_RemisionesManuales__CargaMasiva_001_Integrar_Remisiones___Test 
Go--#SQL 

Create Proc sp_Proceso_FACT_RemisionesManuales__CargaMasiva_001_Integrar_Remisiones___Test
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13'
) 
With Encryption 
As 
Begin 
--Set NoCount On 
Set Ansi_Warnings Off  --- Especial, peligroso 

Declare 
	@iFolioRemision int, 
	@sGUID varchar(200) 

	Set @sGUID = cast(NEWID() as varchar(200)) 
	Select @iFolioRemision = cast( (max(FolioRemision) + 1) as varchar) 
	From FACT_Remisiones_Manuales (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
	Set @iFolioRemision = IsNull(@iFolioRemision, 1) 


	Update R Set FolioRemision = '' 
	From FACT_Remisiones_Manuales__CargaMasiva R 


	--------------------------- Determinar los folios de remision a generar  
	Select 
		IDENTITY(int, 0, 1) as Consecutivo, 
		@iFolioRemision as FolioInicial, 
		cast('' as varchar(20)) as FolioRemision, 			
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, 
		IdCliente, IdSubCliente, IdBeneficiario,	
		IdFuenteFinanciamiento, IdFinanciamiento, 
		TipoDeRemision, TipoInsumo, OrigenInsumo, 
		Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, Referencia_06   
	Into #tmp_FoliosRemisiones 
	from FACT_Remisiones_Manuales__CargaMasiva 
	group by 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, 
		IdCliente, IdSubCliente, IdBeneficiario,	
		IdFuenteFinanciamiento, IdFinanciamiento, 
		TipoDeRemision, TipoInsumo, OrigenInsumo, 
		Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, Referencia_06   
	Order by 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, 
		IdCliente, IdSubCliente, IdBeneficiario,	
		IdFuenteFinanciamiento, IdFinanciamiento, 
		TipoDeRemision, TipoInsumo, OrigenInsumo, 
		Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, Referencia_06   


	Update R Set FolioRemision = right('0000000000000000' + cast(FolioInicial + Consecutivo as varchar(10)), 10) 
	From #tmp_FoliosRemisiones R 
	--------------------------- Determinar los folios de remision a generar  



	Update L Set FolioRemision = R.FolioRemision 
	From FACT_Remisiones_Manuales__CargaMasiva L (NoLock) 
	Inner Join #tmp_FoliosRemisiones R (NoLock) 
		On 
		( 
			L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmaciaGenera and L.IdFarmacia = R.IdFarmacia 
			and L.IdFuenteFinanciamiento = R.IdFuenteFinanciamiento and L.IdFinanciamiento = R.IdFinanciamiento -- and L.IdDocumento = R.IdDocumento 
			and L.IdCliente = R.IdCliente and L.IdSubCliente = R.IdSubCliente and L.IdBeneficiario = R.IdBeneficiario 	
			and L.TipoDeRemision = R.TipoDeRemision and L.TipoInsumo = R.TipoInsumo and L.OrigenInsumo = R.OrigenInsumo  
			and L.Referencia_01 = R.Referencia_01 
			and L.Referencia_02 = R.Referencia_02 
			and L.Referencia_03 = R.Referencia_03 
			and L.Referencia_04 = R.Referencia_04 
			and L.Referencia_05 = R.Referencia_05 															
			and L.Referencia_06 = R.Referencia_06 
			--and L.EsControlado = R.EsControlado and L.EsAntibiotico = R.EsAntibiotico and L.EsRefrigerado = R.EsRefrigerado 
		) 


	--		sp_Proceso_FACT_RemisionesManuales__CargaMasiva_001_Integrar_Remisiones___Test 

	----Select * 
	----From #tmp_FoliosRemisiones 
	
	/* 
		select FolioRemision 
		from FACT_Remisiones_Manuales__CargaMasiva 
		group by FolioRemision 
		order by FolioRemision 
	*/ 

	
	Insert Into FACT_Remisiones_Manuales 
	( 
		GUID, 
		IdEmpresa, IdEstado, IdFarmaciaGenera, FechaRemision, FechaValidacion, FolioRemision, TipoDeRemision, TipoInsumo, OrigenInsumo, 
		EsFacturada, EsFacturable, EsExcedente, IdPersonalRemision, IdPersonalValida, IdFuenteFinanciamiento, IdFinanciamiento, 
		SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, FechaInicial, FechaFinal, Status, Actualizado
	) 
	Select 
		@sGUID as GUID, 
		IdEmpresa, IdEstado, IdFarmaciaGenera, convert(varchar(10), getdate(), 120) as FechaRemision, convert(varchar(10), getdate(), 120) as FechaValidacion, 
		FolioRemision, TipoDeRemision, TipoInsumo, OrigenInsumo, 
		0 as EsFacturada, 0 as EsFacturable, 0 as EsExcedente, '' as IdPersonalRemision, '' as IdPersonalValida, 
		IdFuenteFinanciamiento, IdFinanciamiento, 
		sum(SubTotalSinGrabar) as SubTotalSinGrabar, sum(SubTotalGrabado) as SubTotalGrabado, sum(Iva) as IVA, sum(SubTotalSinGrabar + SubTotalGrabado + Iva) As Total, 'REMISIÓN MANUAL' as Observaciones, 
		FechaInicial, FechaFinal, 'A' AS Status, 0 AS Actualizado 
	From FACT_Remisiones_Manuales__CargaMasiva R (NoLock) 
	Group by 
		IdEmpresa, IdEstado, IdFarmaciaGenera, 
		FolioRemision, TipoDeRemision, TipoInsumo, OrigenInsumo, 
		IdFuenteFinanciamiento, IdFinanciamiento, 
		FechaInicial, FechaFinal 
	Order by FolioRemision 



	----Insert Into FACT_Remisiones_Manuales_Detalles 
	----( 
	----	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
	----	IdCliente, IdSubCliente, IdBeneficiario, 
	----	IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, 
	----	Cantidad_Agrupada, Cantidad, TasaIva, 
	----	SubTotalSinGrabar, SubTotalGrabado, Iva, Importe, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, Referencia_06 
	----) 
	--Select 
	--	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
	--	IdCliente, IdSubCliente, IdBeneficiario, 
	--	IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, 
	--	sum(Cantidad) as Cantidad_Agrupada, sum(Cantidad) as Cantidad, 
	--	TasaIva, 
	--	SubTotalSinGrabar, SubTotalGrabado, Iva, Importe, 
	--	sum(SubTotalSinGrabar) as SubTotalSinGrabar, sum(SubTotalGrabado) as SubTotalGrabado, sum(Iva) as IVA, sum(Importe) as Importe, 
	--	Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, Referencia_06, count(*) as Registros 
	--From FACT_Remisiones_Manuales__CargaMasiva R (NoLock) 
	--Group by  
	--	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
	--	IdCliente, IdSubCliente, IdBeneficiario, 
	--	IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, 
	--	TasaIva, 
	--	Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05, Referencia_06 
	--having count(*) >= 1 



End 
Go--#SQL 

