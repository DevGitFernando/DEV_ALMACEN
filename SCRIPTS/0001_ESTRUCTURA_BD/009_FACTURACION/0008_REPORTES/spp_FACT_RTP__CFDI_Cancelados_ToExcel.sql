------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_RTP__CFDI_Cancelados_ToExcel' and xType = 'P' ) 
   Drop Proc spp_FACT_RTP__CFDI_Cancelados_ToExcel 
Go--#SQL 

Create Proc spp_FACT_RTP__CFDI_Cancelados_ToExcel 
( 
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '', 
	@EsReporteGeneral int = 0, 

	@Año int = 2022, @Mes int = 0  
)  
As 
Begin 
--Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro varchar(max), 
	@sFiltro_Fechas varchar(max) 


	Set @IdEmpresa = right('000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000' + @IdFarmacia, 4) 


	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @sFiltro_Fechas = '' 

	----------------------- ARMAR CRITERIOS  
	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and CanceladoSAT = 1 ' + char(13) 


	Set @sFiltro_Fechas = ' and year(FechaRegistro) = ' + char(39) + cast(@Año as varchar(10)) + char(39) 
	If @Mes > 0 
	Begin 
		Set @sFiltro_Fechas = @sFiltro_Fechas + ' and month(FechaRegistro) = ' + char(39) + cast(@Mes as varchar(10)) + char(39) 
	End 


	If @EsReporteGeneral = 1 
	Begin 
		Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and CanceladoSAT = 1 ' + char(13)  
	End 
	----------------------- ARMAR CRITERIOS  



	----------------------- PREPARAR LA TABLA 
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
		FechaRegistro as FechaEmision, TipoDeDocumento, Serie, Folio, 
		UUID, 
		SubTotal, Iva, Total, 
		RFC, NombreReceptor,   
		StatusDocumento, StatusDoctoAux,  
		IdPersonalEmite, PersonalEmite,   
		IdPersonalCancela, PersonalCancela, FechaCancelacion, MotivoCancelacion,   
		Observaciones_01, Observaciones_02, Observaciones_03,  
		FormaDePago, MetodoDePago, ReferenciaDePago  
	Into #tmp__Resultado 
	From vw_FACT_CFD_DocumentosElectronicos (NoLock)    
	Where 1 = 0 
	----------------------- PREPARAR LA TABLA 



	----------------------- OBTENER LA INFORMACION 
	Set @sSql = 'Insert Into #tmp__Resultado ' + char(13) + 
		'Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  ' + char(13) + 
		'	FechaRegistro as FechaEmision, TipoDeDocumento, Serie, Folio, UUID, SubTotal, Iva, Total, ' + char(13) + 
		'	RFC, NombreReceptor,   ' + char(13) + 
		'	StatusDocumento, StatusDoctoAux,  ' + char(13) + 
		'	IdPersonalEmite, PersonalEmite,   ' + char(13) + 
		'	IdPersonalCancela, PersonalCancela, FechaCancelacion, MotivoCancelacion,   ' + char(13) + 
		'	Observaciones_01, Observaciones_02, Observaciones_03,  ' + char(13) + 
		'	FormaDePago, MetodoDePago, ReferenciaDePago ' + char(13) + 
		'From vw_FACT_CFD_DocumentosElectronicos (NoLock)  ' + char(13) + 
		@sFiltro + @sFiltro_Fechas 
	Exec ( @sSql ) 
	Print @sSql 
	----------------------- OBTENER LA INFORMACION 





	------------------------------------ SALIDA FINAL  
	Select 
		--IdEmpresa, Empresa, 
		IdEstado, Estado, 
		IdFarmacia, Farmacia,  
		'Fecha de emisión' = FechaEmision, 
		'Tipo de documento' = TipoDeDocumento, 
		Serie, Folio, 
		'Folio fiscal' = UUID, 
		SubTotal, Iva, Total, 
		'RFC receptor' = RFC, 
		'Nombre del receptor' = NombreReceptor,   
		'Status del documento' = StatusDocumento,  
		--IdPersonalEmite, 
		'Nombre de quien emite' = PersonalEmite,   
		--IdPersonalCancela, 
		'Nombre de quien cancela' = PersonalCancela, 
		FechaCancelacion, 
		'Motivo de cancelación' = MotivoCancelacion,   
		'Observaciones 01' = Observaciones_01, 
		'Observaciones 02' = Observaciones_02, 
		'Observaciones 03' = Observaciones_03,  
		'Forma de pago' = FormaDePago, 
		'Método de pago' = MetodoDePago, 
		'Referencia de pago' = ReferenciaDePago 
	From #tmp__Resultado  



End 
----Go--#SQL 