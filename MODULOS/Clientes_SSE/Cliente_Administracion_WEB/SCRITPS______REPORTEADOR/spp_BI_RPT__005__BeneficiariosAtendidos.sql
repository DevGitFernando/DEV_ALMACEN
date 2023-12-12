------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__005__BeneficiariosAtendidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__005__BeneficiariosAtendidos 
Go--#SQL 

Create Proc spp_BI_RPT__005__BeneficiariosAtendidos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '13', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', @FechaInicial varchar(10) = '2018-01-01', @FechaFinal varchar(10) = '2018-01-05', 
	@ProgramaDeAtencion varchar(200) = '' 	
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 

	--	Drop table tmp_Reporteador 

	----if not exists ( select * from sysobjects where name = 'tmp_Reporteador' and xType = 'U' ) 
	----Begin 
	----	select top 0 
	----		identity(int, 1, 1) as Orden, 
	----		cast('' as varchar(20)) as Empresa, 
	----		cast('' as varchar(20)) as Estado, 
	----		cast('' as varchar(20)) as Farmacia  
	----	Into tmp_Reporteador 
	----End 

	----Insert Into tmp_Reporteador select @IdEmpresa, @IdEstado, @IdFarmacia 


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia 
	Into SII_REPORTEADOR..#vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Set @ProgramaDeAtencion = replace(@ProgramaDeAtencion, ' ', '%') 

	Select 
		E.IdEstado, 
		F.IdJurisdiccion, F.Jurisdiccion, 
		E.IdFarmacia, E.Farmacia, 
		E.Folio as FolioVenta, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		E.IdBeneficiario, 
		cast(Beneficiario as varchar(500)) as Beneficiario, 
		cast('' as varchar(100)) as FechaDeNacimiento,   
		cast(FolioReferencia as varchar(100)) as Referencia, 
		cast('' as varchar(100)) as FechaVigencia_Inicia,   
		cast('' as varchar(100)) as FechaVigencia_Termina 
	Into #tmp_Beneficiarios 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	----Inner Join VentasInformacionAdicional I (NoLock) 
	----	On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Where convert(varchar(10), FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		and E.SubPrograma like '%' + @ProgramaDeAtencion + '%' 
		----and 
		----Exists 
		----( 
		----	Select * 
		----	From #vw_Farmacias F 
		----	Where E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia 
		----) 
	Order By FechaRegistro 


	Update E Set FechaDeNacimiento = convert(varchar(10), B.FechaNacimiento, 120), 
		FechaVigencia_Inicia = convert(varchar(10), B.FechaInicioVigencia, 120), 
		FechaVigencia_Termina = convert(varchar(10), B.FechafinVigencia, 120)
	From #tmp_Beneficiarios E 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente
			and E.IdBeneficiario = B.IdBeneficiario ) 


---------------------		spp_BI_RPT__005__BeneficiariosAtendidos


----------------------------------------------------- SALIDA FINAL 
	Select 
		-- IdFarmacia,  
		'Fecha de Atención' = FechaRegistro, 
		'Jurisdiccíón' = Jurisdiccion, 
		'Farmacia' = Farmacia,
		'Nombre de beneficiario' = Beneficiario, 
		'Fecha de nacimiento' = FechaDeNacimiento,  
		'Número de poliza' = Referencia, 
		'Fecha inicia vigencia' = FechaVigencia_Inicia,  
		'Fecha termina vigencia' = FechaVigencia_Termina 
	From #tmp_Beneficiarios 
	Group by -- IdFarmacia, 
		Jurisdiccion, Farmacia, 
		FechaRegistro, Beneficiario, FechaDeNacimiento, Referencia, FechaVigencia_Inicia, FechaVigencia_Termina  
	Order By   
		Jurisdiccion, Farmacia, 
		FechaRegistro, Beneficiario, Referencia  



End 
Go--#SQL 


