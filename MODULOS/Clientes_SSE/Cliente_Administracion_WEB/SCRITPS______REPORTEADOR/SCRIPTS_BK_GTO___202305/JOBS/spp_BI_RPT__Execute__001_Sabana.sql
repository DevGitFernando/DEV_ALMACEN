

Set NoCount On 
set DateFormat YMD 
Go--#SQL 

---		Exec spp_BI_RPT__Execute__001_Sabana @FechaInicial_Proceso = '2017-05-01', @FechaFinal_Proceso = '2017-08-31', @IdFarmacia_Proceso = '' 

-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__Execute__001_Sabana' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__Execute__001_Sabana 
Go--#SQL 

Create Proc spp_BI_RPT__Execute__001_Sabana 
( 
	@FechaInicial_Proceso varchar(10) = '', @FechaFinal_Proceso varchar(10) = '', @IdFarmacia_Proceso varchar(4) = '', 
	@IdFarmaciaReferencia varchar(4) = '' 
) 
As  
Begin  
Set NoCount On 
Set DateFormat YMD 

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
	@MostrarResultado int, 
	@Año_Causes int, 
	@SepararCauses int   	

	
Declare   
	@EjecutarReporte bit, 
	@EjecutarParametros bit, 
	@EjecutarRemisiones bit,    	
	@ForzarActualizacionCatalogo bit  


Declare 
	@dProceso_Inicia datetime,  
	@dProceso_Termina datetime 


	Set @EjecutarRemisiones = 1  
	Set @CodigoCliente = '' 
	Set @EjecutarReporte = 1   
	Set @EjecutarParametros = 1 	
	Set @GUID = '' 
	
	Set @Año_Causes = 2012 
	Set @SepararCauses = 0 
	Set @MostrarResultado = 1  
	

	Set @ForzarActualizacionCatalogo = 1     
	
    Select 
        @IdEmpresa = '001', @IdEstado = '13', 
        @IdFarmacia = '0003', @CodigoCliente = '', @GUID = '',
				
        @IdCliente = '0002', @IdSubCliente = '0010', 
        @IdPrograma = '', @IdSubPrograma = '',  	
	    @TipoDispensacion = 0, 
--	    @FechaInicial = '', @FechaFinal = '', 
	    @TipoInsumo = 0, @TipoInsumoMedicamento = 0, @SubFarmacias = '', 
	    @MostrarPrecios = 1, 
	    @IncluirDetalleInformacion = 0, 
	    @SoloDispensacion = 0 


	If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_Detallado' and xType = 'U' ) Drop table RptAdmonDispensacion_Detallado 


	If @ForzarActualizacionCatalogo = 1 
	Begin 

		--print getdate() 

		---------- Forzar la actualizacion de las tablas de base de catalogos  
		Exec spp_Rpt_Administrativos_009 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @ForzarActualizacion = 1 

		--print getdate() 

		--return 

		--  Exec spp_Rpt_Administrativos_009 @IdEmpresa = '001', @IdEstado = '11', @IdCliente = '2', @IdSubCliente = '6', @ForzarActualizacion = 1 
	
	End 

	
	------------------------ Tomar los parámetros configurados 
	Select Top 1 @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdCliente = IdCliente, @IdSubCliente = IdSubCliente 
	From BI_RPT__DTS__Configuracion_Operacion (NoLock) 

	if @FechaInicial_Proceso = '' or @FechaFinal_Proceso = ''   
	Begin 
		Set @FechaInicial_Proceso = convert(varchar(10), getdate() - 32, 120) 
		Set @FechaFinal_Proceso = convert(varchar(10), getdate(), 120)  
	End 



----------------------------- TABLAS DE PROCESO 	
	--If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpF_XB%' and xType = 'U' )  
	--	Drop Table tempdb..#tmpF_XB 

	select space(2) as IdEstado, space(4) as IdFarmacia, space(20) as CodigoCliente Into #tmpF_XB where 1  = 0  
	Insert Into #tmpF_XB 
	Select F.IdEstado, F.IdFarmacia, '' as CodigoCliente   
	From vw_Farmacias__PRCS F (NoLock) 
	Inner Join BI_RPT__CatFarmacias_A_Procesar C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia )   
	Where MostrarEnListado = 1 and 
		F.IdEstado = @IdEstado -- and F.IdFarmacia  in ( 11 ) 
		And F.IdTipoUnidad not in ( '000', '005' ) 
		and F.IdFarmacia like '%' + @IdFarmaciaReferencia + '%' 
		--and F.IdFarmacia not In 
		--( 
		--	--3005, 3006, 
		--	3007, 3008, 3009, 3034, 3035, 3036, 3037, 3041, 3052, 3053, 3054, 3055, 3056, 3057, 3089, 3101, 3102, 3103, 
		--	3104, 3110, 3130, 3131, 3132, 3134, 3137, 3187, 3188, 3189, 3193, 3223, 3224, 3226, 3245, 3246, 3247, 3253, 3254, 3255, 3256, 3257, 3258, 3264, 3303, 3306, 
		--	3307, 3308, 3309, 3310, 3311, 3312, 3313, 3314, 3357, 3360, 3361, 3362, 3364, 3366, 3373, 3382, 3383, 3399, 3404, 3406, 3408, 3409, 3412, 3415, 3366, 3191
		--) 


	If @IdFarmacia_Proceso <>  '' 
	Begin 
		Delete From #tmpF_XB Where IdEstado = @IdEstado and IdFarmacia <> @IdFarmacia_Proceso 
	End 



	--If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpPeriodos_X%' and xType = 'U' )  
	--	Drop Table tempdb..#tmpPeriodos_X 
	
	Select top 0 identity(int, 1, 1) as Orden, 
		cast('' as varchar(10)) as FechaInicial, cast('' as varchar(10)) as FechaFinal  
	Into #tmpPeriodos_X 
	-- Where 1 = 0 

	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select @FechaInicial_Proceso, @FechaFinal_Proceso 
----------------------------- TABLAS DE PROCESO 		
	

	If exists ( Select Name From sysobjects (noLock) Where Name like 'RptAdmon_ProcesoGeneracion' and xType = 'U' )  
		Drop Table  RptAdmon_ProcesoGeneracion 
	
	Select top 0 identity(int, 1, 1) as Orden, 
		cast('' as varchar(10)) as IdFarmacia, 
		cast('' as varchar(10)) as FechaInicial, cast('' as varchar(10)) as FechaFinal,
		getdate() as InicioProceso, getdate() as TerminaProceso   
	Into RptAdmon_ProcesoGeneracion 
	-- Where 1 = 0 


	If exists ( Select Name From sysobjects (noLock) Where Name like 'RptAdmonDispensacion_Detallado__General' and xType = 'U' )  
		Drop Table  RptAdmonDispensacion_Detallado__General  


 
 	   
	Declare #cursorPeriodos  
	Cursor For 
		Select FechaInicial, FechaFinal   
		From #tmpPeriodos_X T 
		-- where xtype = 'TR' 
		Order by Orden  
	Open #cursorPeriodos 
	FETCH NEXT FROM #cursorPeriodos Into @FechaInicial,  @FechaFinal  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
		
		
			Declare #cursor_002 
			Cursor For 
				Select IdFarmacia, CodigoCliente  
				From #tmpF_XB (NoLock) 
				Where @EjecutarReporte = 1 
				Order By IdFarmacia 
			Open #cursor_002
			FETCH NEXT FROM #cursor_002 Into @IdFarmacia, @CodigoCliente  
				WHILE @@FETCH_STATUS = 0 
				BEGIN 	
			
						---- Generar el GUID para el proceso a ejecutar  	
						Set @GUID = cast(NEWID() as varchar(max)) 
						Set @dProceso_Inicia = getdate() 
						-- Print @GUID  
								
								
						-------------------------------------------------------------------------------------------------------------------------- 
						Set @TipoDispensacion = 0    
						Exec spp_Rpt_Administrativos @IdEmpresa, @IdEstado, @IdFarmacia, 
													 @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
													 @TipoDispensacion, @FechaInicial,  @FechaFinal, 
													 @TipoInsumo, @TipoInsumoMedicamento, @SubFarmacias, 
													 @MostrarPrecios -- , @IncluirDetalleInformacion, @SoloDispensacion    

						If Not exists ( Select Name From sysobjects (noLock) Where Name = 'RptAdmonDispensacion_Detallado__General' and xType = 'U' )  
						Begin 
							Select *, 0 as EsDeConsignacion 
							Into RptAdmonDispensacion_Detallado__General 
							From RptAdmonDispensacion_Detallado 
							Where 1 = 0 
						End 	

						Insert Into RptAdmonDispensacion_Detallado__General
						Select *, 1 as EsConsignacion 
						From RptAdmonDispensacion_Detallado 									


						Exec spp_BI_RPT__Exportar_A_Reporteador @FechaInicial,  @FechaFinal 
							

						Set @dProceso_Termina = getdate() 

						Insert Into RptAdmon_ProcesoGeneracion ( IdFarmacia, FechaInicial, FechaFinal, InicioProceso, TerminaProceso  ) 
						Select @IdFarmacia, @FechaInicial,  @FechaFinal, @dProceso_Inicia, @dProceso_Termina 


					FETCH NEXT FROM #cursor_002 Into  @IdFarmacia, @CodigoCliente  
				END 
			Close #cursor_002 
			Deallocate #cursor_002 			

			FETCH NEXT FROM #cursorPeriodos Into @FechaInicial,  @FechaFinal     
		END	 
	Close #cursorPeriodos 
	Deallocate #cursorPeriodos 	



/* 
	Update R Set Excluido = 1 
	From INT_ND_RptAdmonDispensacion_Detallado__General R (NoLock) 
	Inner Join INT_ND_CFG_ClavesSSA_Excluir E On ( R.IdEstado = E.IdEstado and R.ClaveSSA = E.ClaveSSA ) -- and E.Status = 'A' )   
*/ 	



/* 
@TipoDispensacion 
    0 ==> Todo 
    1 ==> Consignacion 
    2 ==> Venta 
                       
                        drop table RptAdmonDispensacion

                        select * from RptAdmonDispensacion

						select * from RptAdmonDispensacion_Detallado__General (nolock)

@TipoInsumo 
    0 ==> Todo 
    1 ==> Medicamento  
    2 ==> Material de Curacion  

*/ 


End 
Go--#SQL 



