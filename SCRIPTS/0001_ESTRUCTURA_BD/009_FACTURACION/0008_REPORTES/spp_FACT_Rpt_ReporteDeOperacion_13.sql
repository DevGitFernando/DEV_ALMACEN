--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion_13' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion_13 
Go--#SQL  

Create Proc	 spp_FACT_Rpt_ReporteDeOperacion_13
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 

	@TipoDeRemision int = 0, 
	@SegmentoTipoDeRemision int = 0, 
	@OrigenDeInsumos int = 0, 
	@TipoDeInsumo int = 0, 

	@AplicarFiltroFolios int = 0, 
	@FolioInicial int = 20183, @FolioFinal int = 21400, 

	@TipoDeFecha int = 1, 
	@AplicarFiltroFechas int = 0, 
	@FechaInicial varchar(10) = '2019-05-15', @FechaFinal varchar(10) = '2019-05-16',
	@iOpcion Int = 1
)    
With Encryption 
As 
Begin 
-- Set NoCount On  
Set DateFormat YMD 
Declare @sNA varchar(10)  

	
	if (@iOpcion = 1)
	Begin 
		Exec spp_FACT_Rpt_Reporte_1N
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, 
				@IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, 
	
				@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 

				@TipoDeRemision = @TipoDeRemision, 
				@SegmentoTipoDeRemision = @SegmentoTipoDeRemision, 
				@OrigenDeInsumos = @OrigenDeInsumos, 
				@TipoDeInsumo = @TipoDeInsumo, 

				@AplicarFiltroFolios = @AplicarFiltroFolios, 
				@FolioInicial = @FolioInicial, @FolioFinal = @FolioFinal, 

				@TipoDeFecha = @TipoDeFecha, 
				@AplicarFiltroFechas = @AplicarFiltroFechas, 
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal
	End


	if (@iOpcion = 2)
	Begin
		Exec spp_FACT_Rpt_Reporte_2N
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, 
				@IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, 
	
				@IdFuenteFinanciamiento = @IdFuenteFinanciamiento, @IdFinanciamiento = @IdFinanciamiento, 

				@TipoDeRemision = @TipoDeRemision, 
				@SegmentoTipoDeRemision = @SegmentoTipoDeRemision, 
				@OrigenDeInsumos = @OrigenDeInsumos, 
				@TipoDeInsumo = @TipoDeInsumo, 

				@AplicarFiltroFolios = @AplicarFiltroFolios, 
				@FolioInicial = @FolioInicial, @FolioFinal = @FolioFinal, 

				@TipoDeFecha = @TipoDeFecha, 
				@AplicarFiltroFechas = @AplicarFiltroFechas, 
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal
	End


End 
Go--#SQL  