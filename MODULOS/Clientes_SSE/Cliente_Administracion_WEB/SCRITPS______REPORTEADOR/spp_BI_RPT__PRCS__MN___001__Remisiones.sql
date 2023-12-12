Set NoCount On 
set DateFormat YMD 
Go--#SQL 

---   5, 34, 188, 224, 313  
	
	If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpF_XB%' and xType = 'U' )  
		Drop Table tempdb..#tmpF_XB 

	select space(2) as IdEstado, space(4) as IdFarmacia, space(20) as CodigoCliente Into #tmpF_XB where 1  = 0  
	Insert Into #tmpF_XB 
	Select F.IdEstado, F.IdFarmacia, '' as CodigoCliente   
	From CatFarmacias F (NoLock) 
	--- Inner Join INT_ND_Clientes C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) 
	Where F.IdEstado = 11 and F.IdFarmacia = 88    -- in ( 11  ) -- , 13, 14, 15, 18, 30, 32, 41 )  	-- and 1 = 0	
	

	If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpPeriodos_X%' and xType = 'U' )  
		Drop Table tempdb..#tmpPeriodos_X 
	
	Select top 0 identity(int, 1, 1) as Orden, 
		cast('' as varchar(10)) as FechaInicial, cast('' as varchar(10)) as FechaFinal  
	Into #tmpPeriodos_X 
	-- Where 1 = 0 
	
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-08-15', '2014-08-31' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-09-01', '2014-09-30' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-10-01', '2014-10-31' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-11-01', '2014-11-30' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-12-01', '2014-12-31' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-01-01', '2015-01-31' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-02-01', '2015-02-28' 

	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-01', '2015-05-05' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-06', '2015-05-10' 	
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-11', '2015-05-15' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-16', '2015-05-20' 	
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-21', '2015-05-25' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-26', '2015-05-31' 	
	

	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-06-01', '2015-06-05' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-06-06', '2015-06-10' 	
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-06-11', '2015-06-15' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-06-16', '2015-06-20' 	
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-06-21', '2015-06-25' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-06-26', '2015-06-30' 	
	
	
--	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-10-01', '2015-10-05' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-10-06', '2015-10-10' 	
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-10-11', '2015-10-15' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-10-16', '2015-10-20' 	
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-10-21', '2015-10-25' 
	----Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-10-26', '2015-10-31' 		
	
	
--	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-01', '2015-05-03' 		
	
--	Insert Into #tmpF_XB select 11, '0003' 
----	Insert Into #tmpF_XB select '0056' 	
----	Insert Into #tmpF_XB select '0188' 	
----	Insert Into #tmpF_XB select '0224' 
----	Insert Into #tmpF_XB select '0313' 	
	

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
	@EjecutarRemisiones bit   	


	Set @EjecutarRemisiones = 1  
	Set @CodigoCliente = '' 
	Set @EjecutarReporte = 1   
	Set @EjecutarParametros = 1 	
	Set @GUID = '' 
	
	Set @Año_Causes = 2012 
	Set @SepararCauses = 0 
	Set @MostrarResultado = 1  
	
    Select 
        @IdEmpresa = '001', @IdEstado = '11', 
        @IdFarmacia = '0003', @CodigoCliente = '', @GUID = ''  	    	
	
    Select 
        @IdEmpresa = '001', @IdEstado = '11', @IdFarmacia = '0003', 
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
			If exists ( Select Name From sysobjects (noLock) Where Name = 'RptAdmonDispensacion_Detallado__General' and xType = 'U' )  
			   Drop Table RptAdmonDispensacion_Detallado__General 


			If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion_Detallado__General' and xType = 'U' ) 
			   Drop Table INT_ND_RptAdmonDispensacion_Detallado__General 

		*/ 


		/* 

			If exists ( Select Name From sysobjects (noLock) Where Name = 'RptAdmonDispensacion_Detallado__General' and xType = 'U' )  
				Truncate Table RptAdmonDispensacion_Detallado__General 


			If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion_Detallado__General' and xType = 'U' ) 
				Truncate Table INT_ND_RptAdmonDispensacion_Detallado__General 

		*/ 



		/* 
		
		truncate table INT_ND_RptAdmonDispensacion   		
		truncate table INT_ND_RptAdmonDispensacion_Detallado__General  	   
		
		*/ 
		
	End 
	
	   
	Declare #cursorPeriodos  
	Cursor For 
		Select FechaInicial, FechaFinal  ---- @sTipoProceso +  name + ' ON ' + ( select name from sysobjects P Where T.Parent_obj = P.Id ) + char(10) + char(13) + '' 
		From #tmpPeriodos_X T 
		-- where xtype = 'TR' 
		Order by Orden  
	Open #cursorPeriodos 
	FETCH NEXT FROM #cursorPeriodos Into @FechaInicial,  @FechaFinal  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
		
		
			Declare #cursor_002 
			Cursor For 
				Select IdEstado, IdFarmacia, CodigoCliente  
				From #tmpF_XB (NoLock) 
				Where @EjecutarReporte = 1 
				Order By IdFarmacia 
			Open #cursor_002
			FETCH NEXT FROM #cursor_002 Into @IdEstado, @IdFarmacia, @CodigoCliente  
				WHILE @@FETCH_STATUS = 0 
				BEGIN 	
			
						---- Generar el GUID para el proceso a ejecutar  	
						Set @GUID = cast(NEWID() as varchar(max)) 
						-- Print @GUID  
								

						-------------------------------------------------------------------------------------------------------------------------- 								
						Set @TipoDispensacion = 1  
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
						
											
						-------------------------------------------------------------------------------------------------------------------------- 
						Set @TipoDispensacion = 2 
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
						Select *, 0 as EsDeConsignacion 
						From RptAdmonDispensacion_Detallado 
						-------------------------------------------------------------------------------------------------------------------------- 							



					FETCH NEXT FROM #cursor_002 Into  @IdEstado, @IdFarmacia, @CodigoCliente  
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

@TipoInsumo 
    0 ==> Todo 
    1 ==> Medicamento  
    2 ==> Material de Curacion  

*/ 



