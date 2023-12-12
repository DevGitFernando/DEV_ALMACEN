--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rpt_ReporteDeOperacion' and xType = 'P' ) 
   Drop Proc spp_FACT_Rpt_ReporteDeOperacion 
Go--#SQL  

Create Proc	 spp_FACT_Rpt_ReporteDeOperacion
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

	----- Quitar tabla de Control 
	If Exists ( Select * From sysobjects (nolock) where Name = 'FACT_Remisiones____RPT' and xType = 'U' ) Drop Table FACT_Remisiones____RPT   
	

	if @IdEstado = '11' 
	Begin 
		Exec spp_FACT_Rpt_ReporteDeOperacion_11 
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
				@FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, 
				@iOpcion = @iOpcion 
	End



	if @IdEstado = '13' 
	Begin 
		Exec spp_FACT_Rpt_ReporteDeOperacion_13 
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


	if @IdEstado = '28' 
	Begin 
		Exec spp_FACT_Rpt_ReporteDeOperacion_28 
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