------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__017__Validacion__Remisiones' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__017__Validacion__Remisiones 
Go--#SQL 

/*	
	Exec spp_BI_RPT__017__Validacion__Remisiones 
		@IdEmpresa = '001', @IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
		@IdFarmacia = '12', @Remision = '', 
		@FechaInicial = '2018-01-01', @FechaFinal = '2018-01-05', 
		@TipoDeDispensacion = 0, @NombreSolicitante = ''   -- san za 
*/ 

Create Proc spp_BI_RPT__017__Validacion__Remisiones 
(  
	--@IdEmpresa varchar(3) = '001', 
	--@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	--@IdFarmacia varchar(4) = '0013', 
	--@Remision varchar(100) = '', 
	--@FechaInicial varchar(10) = '2021-01-01', @FechaFinal varchar(10) = '2021-01-10', 
	--@TipoDeDispensacion int = 2, 
	--@NombreSolicitante varchar(500) = ''  

	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '4011', 
	@Remision varchar(100) = '', 
	@FechaInicial varchar(10) = '2022-04-01', @FechaFinal varchar(10) = '2022-04-10', 
	@TipoDeDispensacion varchar(20) = '2', 
	@NombreSolicitante varchar(500) = '' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

	
	----Select 	
	----	'Folio de remisión' = 'FolioRemision',  
	----	'Fecha de remisión' = 'FechaRemision', 
	----	'Unidad' = 'IdFarmacia',  
	----	'Nombre Unidad' = 'Farmacia', 
	----	'Nombre de quien solicita' = 'Medico', 
	----	'Clave SSA' = 'ClaveSSA', 
	----	'Descripción Clave SSA' = 'DescripcionSal', 	
	----	'Cantidad dispensada' = cast(0 as numeric(14,4)), 
	----	'Procedencia' = 'Procedencia', 
	----	'Fuente de financiamiento' = 'FuenteDeFinanciamiento', 
	----	'Precio ofertado' = cast(0 as numeric(14,4)), 		
	----	'Costo de distribución' = cast(0 as numeric(14,4)), 		
	----	'Precio unitario' = cast(0 as numeric(14,4)), 
	----	'Costo total' = cast(0 as numeric(14,4)),  
	----	'IdTipoDeDispensacion' = '' 

	If @IdEstado = '11' 
	Begin 
		Exec spp_BI_RPT__017__Validacion__Remisiones__Formato_GTO 
			 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdMunicipio = @IdMunicipio, @IdJurisdiccion = @IdJurisdiccion, 
			 @IdFarmacia = @IdFarmacia, @Remision = @Remision, @FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @TipoDeDispensacion = @TipoDeDispensacion,  
			 @NombreSolicitante = '' -- @NombreSolicitante   
	End 


	If @IdEstado = '13' 
	Begin 
		Exec spp_BI_RPT__017__Validacion__Remisiones__Formato_HGO 
			 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdMunicipio = @IdMunicipio, @IdJurisdiccion = @IdJurisdiccion, 
			 @IdFarmacia = @IdFarmacia, @Remision = @Remision, @FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @TipoDeDispensacion = @TipoDeDispensacion,  
			 @NombreSolicitante = @NombreSolicitante   
	End 


	If @IdEstado = '21' 
	Begin 
		Exec spp_BI_RPT__017__Validacion__Remisiones__Formato_PL 
			 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdMunicipio = @IdMunicipio, @IdJurisdiccion = @IdJurisdiccion, 
			 @IdFarmacia = @IdFarmacia, @Remision = @Remision, @FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @TipoDeDispensacion = @TipoDeDispensacion,  
			 @NombreSolicitante = @NombreSolicitante   
	End 


	If @IdEstado = '22' 
	Begin 
		Exec spp_BI_RPT__017__Validacion__Remisiones__Formato_QRO 
			 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdMunicipio = @IdMunicipio, @IdJurisdiccion = @IdJurisdiccion, 
			 @IdFarmacia = @IdFarmacia, @Remision = @Remision, @FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @TipoDeDispensacion = @TipoDeDispensacion,  
			 @NombreSolicitante = '' -- @NombreSolicitante   
	End 

End 
Go--#SQL 


