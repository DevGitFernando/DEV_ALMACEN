--------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__020_10__EFICIENCIA_DE_SURTIMIENTO' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__020_10__EFICIENCIA_DE_SURTIMIENTO 
Go--#SQL 

Create Proc spp_BI_RPT__020_10__EFICIENCIA_DE_SURTIMIENTO 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2016-12-01', @FechaFinal varchar(10) = '2018-12-31'	
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@dImporteTotal numeric(14,4)  

	Set @sSql = '' 
	Set @dImporteTotal = 0 


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and EsAlmacen in ( 0, 1 )  and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
		Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----	select top 1 * from RptAdmonDispensacion_Detallado 


----------------------------------------------------- OBTENCION DE DATOS  
	Select E.IdEmpresa, F.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion, F.IdFarmacia, F.Farmacia, E.FolioVenta,
		Sum((Case When CantidadRequerida < CantidadEntregada then Cast(CantidadEntregada As Int) else Cast(CantidadRequerida As Int) End)) As CantidadRequerida,
		Sum(Cast(CantidadEntregada As Int)) as CantidadEntregada
	Into #VentasEstadistica
	From VentasEstadisticaClavesDispensadas E (NoLock)
	Inner Join VentasEnc V (NoLock) On (E.IdEmpresa = V.IdEmpresa And E.IdEstado = V.IdEstado And E.IdFarmacia = V.IdFarmacia And E.FolioVenta = V.FolioVenta)
	Inner Join #vw_Farmacias F On (E.Idestado = F.IdEstado And E.IdFarmacia = F.IdFarmacia)
	Where Not (Cast(CantidadRequerida As Int) = 0 And Cast(CantidadEntregada As Int) = 0)
		And convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal and V.IdEmpresa = @IdEmpresa 
	Group By E.IdEmpresa, F.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion, F.IdFarmacia, F.Farmacia, E.FolioVenta


	Select IdEmpresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		Count(*) As Recetas_Solicitadas, 0 As RECETAS_COMPLETAS, 0 As RECETAS_PARCIALES, 0 As RECETAS_NEGADAS,
		Sum(CantidadRequerida) As PIEZAS_SOLICITADAS, 0 As PIEZAS_COMPLETAS, 0 As PIEZAS_PARCIALES, 0 As PIEZAS_NEGADAS
	Into #Final
	From #VentasEstadistica 
	Group By IdEmpresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia

	UpDate F
	Set
		RECETAS_COMPLETAS = IsNull((Select Count(*)
									From #VentasEstadistica E
									Where E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia And E.CantidadRequerida = E.CantidadEntregada), 0),
		RECETAS_PARCIALES = IsNull((Select Count(*)
									From #VentasEstadistica E
									Where E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia And E.CantidadRequerida > E.CantidadEntregada And E.CantidadEntregada > 0), 0),
		RECETAS_NEGADAS   = IsNull((Select Count(*)
									From #VentasEstadistica E
									Where E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia And  E.CantidadEntregada = 0), 0),
		PIEZAS_COMPLETAS  = IsNull((Select Sum(CantidadEntregada)
									From #VentasEstadistica E
									Where E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia And E.CantidadRequerida = E.CantidadEntregada), 0),
		PIEZAS_PARCIALES  = IsNull((Select Sum(CantidadEntregada)
									From #VentasEstadistica E
									Where E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia And E.CantidadRequerida > E.CantidadEntregada And E.CantidadEntregada > 0), 0),
		PIEZAS_NEGADAS    = IsNull((Select Sum(CantidadRequerida)
									From #VentasEstadistica E
									Where E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia And E.CantidadEntregada = 0), 0)
	From #Final F


	Select *
	From #Final



End 
Go--#SQL 


