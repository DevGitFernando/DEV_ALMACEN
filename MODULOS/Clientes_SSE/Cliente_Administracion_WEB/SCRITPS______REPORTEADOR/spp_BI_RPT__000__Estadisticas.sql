------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__007__Estadisticas' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__007__Estadisticas 
Go--#SQL 

Create Proc spp_BI_RPT__007__Estadisticas 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '', 
	@FechaInicial varchar(10) = '2018-01-01' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@Año int, 
	@Mes int 

	Set @sSql = '' 

	Set @Año = datepart(yy, @FechaInicial) 
	Set @Mes = datepart(month, @FechaInicial)  

------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  



----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 



	Select 
		F.IdJurisdiccion, F.Jurisdiccion, 
		E.IdFarmacia, F.Farmacia, 
		E.NivelAtencion, E.TipoDeClave, E.Año, E.Mes, 
		E.PiezasSolicitadas, 
		E.PiezasSurtidas, E.PorcentajeSurtido, 
		E.PiezasVales, E.PorcentajeVales,
		E.PiezasNoSurtido, E.PorcentajeNoSurtido, 
		E.Porcentaje__Surtido_Vales  
	Into #tmp__Resultado 
	From BI_RPT__DTS__PL___Estadisticas E (NoLock) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado 
		and Año = @Año and Mes = @Mes 


--		spp_BI_RPT__007__Estadisticas


----------------------------------------------------- SALIDA FINAL 
	Select 
		'´Núm. Jurisdicción' = IdJurisdiccion, 
		'Jurisdicción' = Jurisdiccion,
		'Núm. Farmacia' = IdFarmacia,  
		'Farmacia' = Farmacia, 
		'Nivel de atención' = NivelAtencion, 
		'Tipo de clave' = TipoDeClave, 
		Año, 
		Mes = dbo.fg_NombresDeMesNumero(Mes), 
		'Piezas solicitadas' = PiezasSolicitadas, 
		'Surtido' = PiezasSurtidas, 
		'% Surtido' = PorcentajeSurtido, 

		'Vales' = PiezasVales, 
		'% Vales' = PorcentajeVales, 

		'No Surtido' = PiezasNoSurtido, 
		'% No Surtido' = PorcentajeNoSurtido, 

		'Surtido + Vales' = Porcentaje__Surtido_Vales  
	From #tmp__Resultado 
	Order By IdJurisdiccion, IdFarmacia 


End 
Go--#SQL 


