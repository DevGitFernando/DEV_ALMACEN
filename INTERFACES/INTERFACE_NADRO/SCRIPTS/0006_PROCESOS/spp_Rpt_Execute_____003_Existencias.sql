Set NoCount On 
set DateFormat YMD 
Go--#SQL 

---   5, 34, 188, 224, 313  
	
	If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpF_XB_Existencias%' and xType = 'U' )  
		Drop Table tempdb..#tmpF_XB_Existencias 

	select space(2) as IdEstado, space(4) as IdFarmacia, space(20) as CodigoCliente Into #tmpF_XB_Existencias where 1  = 0  
	Insert Into #tmpF_XB_Existencias 
	Select F.IdEstado, F.IdFarmacia, C.CodigoCliente   
	From CatFarmacias F (NoLock) 
	Inner Join INT_ND_Clientes C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) 
	Where F.IdEstado = 7 and F.IdFarmacia = 101   
	

	If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpPeriodos_X_Existencias%' and xType = 'U' )  
		Drop Table tempdb..#tmpPeriodos_X_Existencias 
	
	Select top 0 identity(int, 1, 1) as Orden, 
		cast('' as varchar(10)) as FechaInicial, cast('' as varchar(10)) as FechaFinal  
	Into #tmpPeriodos_X_Existencias 

	
Declare 	
	@sFechaInicial varchar(10), @sFechaFinal varchar(10),     
	@dFechaInicial datetime, @dFechaFinal datetime, @dFecha_Periodo datetime, 
	@iDiasPeriodo int, 
	@iTerminar int  


	Set @iTerminar = 0 
	Set @iDiasPeriodo =  1  --- NOTA :  NO MODIFICAR ESTE VALOR 
	Set @sFechaInicial = '2015-04-20' 
	Set @sFechaFinal = '2015-06-30' 	

	Set @dFechaInicial = dateadd(dd, 0, cast(@sFechaInicial as datetime) )  
	Set @dFechaFinal = cast(@sFechaFinal as datetime)  	
	Set @dFecha_Periodo = @dFechaInicial   ---dateadd(dd, @iDiasPeriodo, @dFechaInicial)  
	--- Set @dFecha_Periodo = dateadd(dd, @iDiasPeriodo, @dFechaInicial)  	 
	
	-- Select @dFechaInicial, @dFecha_Periodo, @dFechaFinal 
	
	
	
	While @dFecha_Periodo <= @dFechaFinal and @iTerminar = 0 
	Begin 
		Set @dFecha_Periodo = dateadd(dd, @iDiasPeriodo - 1, @dFechaInicial)  	 
		
		If @dFecha_Periodo >= @dFechaFinal 
		Begin 
			Set @dFecha_Periodo = @dFechaFinal 
			Set @iTerminar = 1 
		End 	
		
		
		--	Select @dFechaInicial, @dFechaFinal, @dFecha_Periodo  
		Insert Into #tmpPeriodos_X_Existencias ( FechaInicial, FechaFinal ) 
		Select convert(varchar(10), @dFechaInicial, 120), convert(varchar(10), @dFecha_Periodo, 120)  
		
		Set @dFechaInicial = dateadd(dd, 1, @dFecha_Periodo)  	 		
		-- Select @dFechaInicial, @dFechaFinal, @dFecha_Periodo  

	End 


	

Declare 
    @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdCliente varchar(4), @IdSubCliente varchar(4), 
	@IdPrograma varchar(4), @IdSubPrograma varchar(4),  
	@CodigoCliente varchar(20), 
	@GUID varchar(100), 
	@sFechaProceso varchar(10), 

	@TipoDispensacion tinyint, 
	@FechaInicial varchar(10), @FechaFinal varchar(10), 
	@TipoInsumo tinyint , @TipoInsumoMedicamento tinyint , @SubFarmacias varchar(200), 
	@MostrarPrecios tinyint, @IncluirDetalleInformacion tinyint, @SoloDispensacion tinyint     	
	
Declare 
	-- @CodigoCliente varchar(20), 
	-- @GUID varchar(100), 
	@TipoDeProceso int, 
	@MostrarResultado int, 
	@Año_Causes int, 
	@SepararCauses int 
	
Declare   
	@EjecutarReporte bit, 
	@EjecutarParametros bit, 
	@EjecutarProceso bit   	


-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------
	Set @EjecutarProceso = 1  
-----------------------------------------------------------------------------------
	
	   
	Set @CodigoCliente = '' 
	Set @EjecutarReporte = 1   
	Set @EjecutarParametros = 1 	
	Set @GUID = '' 
	
	Set @Año_Causes = 2012 
	Set @SepararCauses = 0 
	Set @MostrarResultado = 0  
	Set @TipoDeProceso = 1 


	
    Select 
        @IdEmpresa = '003', @IdEstado = '07', @IdFarmacia = '0003', 
        @IdCliente = '', @IdSubCliente = '', 
        @IdPrograma = '', @IdSubPrograma = '',  	
	    @TipoDispensacion = 0, 
	    @FechaInicial = '2014-08-16', @FechaFinal = '2015-02-28', 
	    @TipoInsumo = 0, @TipoInsumoMedicamento = 0, @SubFarmacias = '', 
	    @MostrarPrecios = 1, 
	    @IncluirDetalleInformacion = 0, 
	    @SoloDispensacion = 0 

--		truncate Table INT_ND_RptAdmonDispensacion 

--		Drop Table INT_ND__Dispensacion 


	If @EjecutarReporte = 1 
	Begin 
		set @EjecutarReporte = 1 

		/*

			If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__PRCS_Existencias' and xType = 'U' ) 
				Drop Table INT_ND__PRCS_Existencias 

		*/ 
		

		/*

			If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__PRCS_Existencias' and xType = 'U' ) 
				Truncate Table INT_ND__PRCS_Existencias 

		*/ 

		
	End 
	

	---- Visualizar el listado de dias a procesar  
	If @EjecutarProceso = 0 
	Begin 
		Select * from #tmpPeriodos_X_Existencias 
	End 

	   
	Declare #cursorPeriodos  
	Cursor For 
		Select FechaInicial 
		From #tmpPeriodos_X_Existencias T 
		where @EjecutarProceso = 1 
		Order by Orden  
	Open #cursorPeriodos 
	FETCH NEXT FROM #cursorPeriodos Into @FechaInicial 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
		
		
			Declare #cursor_002 
			Cursor For 
				Select IdFarmacia, CodigoCliente  
				From #tmpF_XB_Existencias (NoLock) 
				Where @EjecutarReporte = 1 
				Order By IdFarmacia 
			Open #cursor_002
			FETCH NEXT FROM #cursor_002 Into @IdFarmacia, @CodigoCliente  
				WHILE @@FETCH_STATUS = 0 
				BEGIN 	
			
						---- Generar el GUID para el proceso a ejecutar  	
						Set @GUID = cast(NEWID() as varchar(max)) 
						-- Print @GUID  

						Exec spp_INT_ND_GenerarExistencias 
							@IdEmpresa, @IdEstado, @IdFarmacia, @CodigoCliente, @FechaInicial, @TipoDeProceso, @MostrarResultado  

					FETCH NEXT FROM #cursor_002 Into  @IdFarmacia, @CodigoCliente  
				END 
			Close #cursor_002 
			Deallocate #cursor_002 			

			FETCH NEXT FROM #cursorPeriodos Into @FechaInicial      
		END	 
	Close #cursorPeriodos 
	Deallocate #cursorPeriodos 	




