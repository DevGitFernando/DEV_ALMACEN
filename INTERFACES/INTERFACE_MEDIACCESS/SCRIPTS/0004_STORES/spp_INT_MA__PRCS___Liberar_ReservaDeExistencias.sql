-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__PRCS___Liberar_ReservaDeExistencias_Periodo' and xType = 'P' ) 
   Drop Proc spp_INT_MA__PRCS___Liberar_ReservaDeExistencias_Periodo
Go--#SQL 

Create Proc spp_INT_MA__PRCS___Liberar_ReservaDeExistencias_Periodo 
(
	@FechaProceso varchar(10) = '2016-06-13', @DiasRevision int = 7, @Ejecutar int = 0  
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 	

Declare 
	@FechaDeRevision datetime, 
	@FechaInicial datetime, 
	@sFecha varchar(10)  	
	
	
	Set @FechaDeRevision = cast(@FechaProceso as datetime) 
	Set @FechaInicial = dateadd(dd, -1 * @DiasRevision , @FechaDeRevision) 


	while @FechaInicial <= @FechaDeRevision 
	Begin 
		--- Select @FechaInicial, @FechaDeRevision 
		Set @sFecha = convert(varchar(10), @FechaInicial, 120) 
		Exec spp_INT_MA__PRCS___Liberar_ReservaDeExistencias @FechaProceso = @sFecha,  @Ejecutar = @Ejecutar 
		
		Set @FechaInicial = dateadd(dd, 1, @FechaInicial) 		
	End 


End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__PRCS___Liberar_ReservaDeExistencias' and xType = 'P' ) 
   Drop Proc spp_INT_MA__PRCS___Liberar_ReservaDeExistencias 
Go--#SQL 

Create Proc spp_INT_MA__PRCS___Liberar_ReservaDeExistencias 
(
	@FechaProceso varchar(10) = '2016-06-01', @Ejecutar int = 0  
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 	
Declare 
	@FechaDeRevision datetime 

	
	Set @FechaDeRevision = getdate() 
	If @FechaProceso <> '' 
		Set @FechaDeRevision = cast(@FechaProceso as datetime)



	-- Select @FechaDeRevision as FechaDeRevision 	
	Select 
		F.IdEmpresa, F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 
		D.CodigoEAN as IdProducto, sum(D.CantidadSolicitada) as CantidadSolicitada, 
		sum(( D.CantidadSolicitada * 1 )) as Cantidad_Reserva  
	Into #tmpDetalles 
	From INT_MA__RecetasElectronicas_001_Encabezado E (NoLock) 
	Inner Join INT_MA__RecetasElectronicas_002_Productos D (NoLock) On ( E.Folio_MA = D.Folio_MA ) 
	Inner Join vw_INT_MA__Farmacias F (NoLock) On ( E.IdFarmacia = F.Referencia_MA ) 
	Where convert(varchar(10), E.FechaRegistro, 120) <= convert(varchar(10), @FechaDeRevision, 120) 
		and E.Surtido = 0  -- and EsRecetaManual = 0 
		and D.CantidadSolicitada > 0 
	Group by 
		F.IdEmpresa, F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, D.CodigoEAN 	 
	 
	--- select * from #tmpDetalles  
	 
	Select D.*, R.Cantidad_Reserva, 
		(D.CantidadReservada - R.Cantidad_Reserva) as Diferencia,
		(case when (D.CantidadReservada - R.Cantidad_Reserva) < 0 then 
			D.CantidadReservada else (R.Cantidad_Reserva) end)  as Diferencia_Disminuir  
	Into #tmpReserva 
	From INT_MA__RecetasElectronicas_003_ReservaExistencia D (NoLock) 	
	Inner Join #tmpDetalles R  (NoLock) 
		On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmacia = R.IdFarmacia and D.IdProducto = R.IdProducto ) 
	-- Where 	(D.CantidadReservada - R.Cantidad_Reserva) < 0 
	order by Diferencia 	



----------------- Cantidades a liberar  	
	If @Ejecutar = 1 
		Begin 
			Update D Set CantidadReservada = D.CantidadReservada - Diferencia_Disminuir 
			From INT_MA__RecetasElectronicas_003_ReservaExistencia D (NoLock) 	
			Inner Join #tmpReserva R  (NoLock) 
				On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmacia = R.IdFarmacia and D.IdProducto = R.IdProducto ) 
			Where (D.CantidadReservada - Diferencia_Disminuir) > 0 			
		End 	
	Else 
		Begin 
			Select CantidadReservada_A_Liberar = D.CantidadReservada - Diferencia_Disminuir, D.*  
			From INT_MA__RecetasElectronicas_003_ReservaExistencia D (NoLock) 	
			Inner Join #tmpReserva R  (NoLock) 
				On ( D.IdEmpresa = R.IdEmpresa and D.IdEstado = R.IdEstado and D.IdFarmacia = R.IdFarmacia and D.IdProducto = R.IdProducto ) 	
			Where (D.CantidadReservada - Diferencia_Disminuir) > 0 	
		End 
	
	
--------------------------------------------	
	----Select * 
	----from INT_MA__RecetasElectronicas_003_ReservaExistencia 
	----Where CantidadReservada > 0 
			
		
--		spp_INT_MA__PRCS___Liberar_ReservaDeExistencias


End 
Go--#SQL 

