------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Listado_Facturacion' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_Listado_Facturacion
Go--#SQL  
  
Create Proc spp_FACT_Remisiones_Listado_Facturacion 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '1', 

	@IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '10', 

	@FechaInicial varchar(10) = '2018-04-01', 
	@FechaFinal varchar(10) = '2018-04-30' 
)  
With Encryption 
As 
Begin  
Set NoCount On 
Set DateFormat YMD 

Declare 
	@sSql varchar(max), 
	@sFiltro_TipoDeRemision varchar(max), 
	@sFiltro_TipoOrigenInsumos varchar(max), 
	@sFiltro_TipoDeInsumos varchar(max), 
	@sFiltro_TipoDispensacion varchar(max), 
	@sFiltro_BaseRemision varchar(max), 
	@sFiltro_FoliosRemision varchar(max), 
	@sFiltro_Fecha_PeriodoRemisionado varchar(max), 
	@sFiltro_Fecha_RemisionGenerada varchar(max), 
	@sFiltro_PartidaGeneral varchar(max) 

	

	Set @sSql = '' 
	Set @sFiltro_TipoDeRemision = '' 
	Set @sFiltro_TipoOrigenInsumos = '' 
	Set @sFiltro_TipoDeInsumos = '' 
	Set @sFiltro_TipoDispensacion = '' 
	Set @sFiltro_BaseRemision = '' 
	Set @sFiltro_FoliosRemision = '' 
	Set @sFiltro_Fecha_PeriodoRemisionado = '' 
	Set @sFiltro_Fecha_RemisionGenerada = '' 
	Set @sFiltro_PartidaGeneral = '' 


	Set @IdEmpresa = right('000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('000000000' + @IdFarmaciaGenera, 4) 
	Set @IdCliente = right('000000000' + @IdCliente, 4) 	
	Set @IdSubCliente = right('000000000' + @IdSubCliente, 4) 


	------------------------------ TABLA BASE 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, 
		IdCliente, Cliente, IdSubCliente, SubCliente, 
		FolioRemision, PartidaGeneral, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  
		TipoDeRemision, TipoDeRemisionDesc, 
		OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, 
		TipoDeDispensacion, TipoDeDispensacionDesc, 
		(case when EsRelacionFacturaPrevia = 1 then 'SI' else 'NO' end) as EsRelacionFacturaPrevia, 
		cast('' as varchar(500)) as Referencia_01, 
		cast('' as varchar(500)) as Referencia_02  

		----cast('' as varchar(500)) as IdCliente, cast('' as varchar(500)) as Cliente, cast('' as varchar(500)) as IdSubCliente, cast('' as varchar(500)) as SubCliente, 
		----IdFuenteFinanciamiento, IdFinanciamiento, cast('' as varchar(500)) as Financiamiento,  
		----TipoDeRemision, cast('' as varchar(500)) as TipoDeRemisionDesc, 
		----OrigenInsumo, cast('' as varchar(500)) as OrigenInsumoDesc, 
		----TipoInsumo, cast('' as varchar(500)) as TipoDeInsumoDesc, 
		----TipoDeDispensacion, cast('' as varchar(500)) as TipoDeDispensacionDesc, 
		----EsRelacionFacturaPrevia
		
		, count(*) as NumeroDeRemisiones  
	Into #tmp___ListadoDeRemisiones 
	From vw_FACT_Remisiones (NoLock) 
	Where 
		IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaGenera 
		and Total > 0  
		and IdCliente = @IdCliente -- @IdSubCliente
		and FechaInicial >= @FechaInicial and FechaFinal <= @FechaFinal  
		and EsFacturable_Base = 0 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, 
		IdCliente, Cliente, IdSubCliente, SubCliente, 
		FolioRemision, PartidaGeneral, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  
		TipoDeRemision, TipoDeRemisionDesc, 
		OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, 
		TipoDeDispensacion, TipoDeDispensacionDesc, 
		(case when EsRelacionFacturaPrevia = 1 then 'SI' else 'NO' end)  
	Order by 
		IdFuenteFinanciamiento, IdFinanciamiento,  
		TipoDeRemision, OrigenInsumo, TipoInsumo, TipoDeDispensacion, EsRelacionFacturaPrevia 



	Select 
		D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera as IdFarmacia, D.FolioRemision, D.Referencia_01 
	Into #tmp___ListadoDeRemisiones__Detalles 
	From FACT_Remisiones_Detalles D (NoLock) 
	Inner Join #tmp___ListadoDeRemisiones R (NoLock) 
		On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmaciaGenera = R.IdFarmacia and D.FolioRemision = R.FolioRemision ) 
	Group by 
		D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, D.FolioRemision, D.Referencia_01


	Update R Set Referencia_01 = D.Referencia_01 
	From #tmp___ListadoDeRemisiones R (NoLock) 
	Inner Join #tmp___ListadoDeRemisiones__Detalles D (NoLock) 
		On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmacia = R.IdFarmacia and D.FolioRemision = R.FolioRemision ) 



	--Update L Set Referencia_01 = 
	--IsNull
	--(
	--	( 
	--		Select Top 1 Referencia_01 From FACT_Remisiones_Detalles R (NoLock) 
	--		Where L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmacia = R.IdFarmaciaGenera and L.FolioRemision = R.FolioRemision 
	--	), 0 
	--) 
	--From #tmp___ListadoDeRemisiones L (NoLock) 



---		Exec spp_FACT_Remisiones_Listado_Facturacion   


---------------------	SALIDA FINAL 
	Select 
		IdCliente, Cliente, IdSubCliente, SubCliente, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  
		TipoDeRemision, TipoDeRemisionDesc, 
		OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, 
		TipoDeDispensacion, TipoDeDispensacionDesc, 
		PartidaGeneral, 
		EsRelacionFacturaPrevia, 
		Referencia_01, 
		Referencia_02,  
		count(*) as Remisiones 
	From #tmp___ListadoDeRemisiones 
	Group by  
		IdCliente, Cliente, IdSubCliente, SubCliente, 
		IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento,  
		TipoDeRemision, TipoDeRemisionDesc, 
		OrigenInsumo, OrigenInsumoDesc, 
		TipoInsumo, TipoDeInsumoDesc, 
		TipoDeDispensacion, TipoDeDispensacionDesc, 
		PartidaGeneral, 
		EsRelacionFacturaPrevia, 
		Referencia_01, 
		Referencia_02 

End
Go--#SQL 


