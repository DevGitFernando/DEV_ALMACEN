
Declare 
    @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdCliente varchar(4), @IdSubCliente varchar(4), 
	@IdPrograma varchar(4), @IdSubPrograma varchar(4),  

	@TipoDispensacion tinyint, 
	@FechaInicial varchar(10), @FechaFinal varchar(10), 
	@TipoInsumo tinyint , @TipoInsumoMedicamento tinyint , @SubFarmacias varchar(200)
        
---   5, 34, 188, 224, 313  
	
	If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpF%' and xType = 'U' )  
		Drop Table tempdb..#tmpF 


	Select space(2) as IdEstado, space(4) as IdFarmacia Into #tmpF where 1  = 0  
	Insert Into #tmpF 
	Select IdEstado, IdFarmacia 
	From CatFarmacias 
	Where IdEstado = 11 and IdFarmacia = 11 
	
----	Insert Into #tmpF select '0005' 
----	Insert Into #tmpF select '0056' 	
----	Insert Into #tmpF select '0188'  	
----	Insert Into #tmpF select '0224'  
----	Insert Into #tmpF select '0313' 	 
	
	
	
    Select 
        @IdEmpresa = '001', @IdEstado = '11', @IdFarmacia = '0313', 
        @IdCliente = '*', @IdSubCliente = '*', 
        @IdPrograma = '*', @IdSubPrograma = '*',  	
	    @TipoDispensacion = 0, 
	    @FechaInicial = '2013-06-01', @FechaFinal = '2013-07-31', 
	    @TipoInsumo = 0, @TipoInsumoMedicamento = 0, @SubFarmacias = ''  

--		truncate Table tmpRptAdmonDispensacion 

--		Drop Table RptAdmonDispensacion_Detallado__General 

	If exists ( Select Name From sysobjects (noLock) Where Name = 'RptAdmonDispensacion_Detallado__General' and xType = 'U' )  
	   Drop table RptAdmonDispensacion_Detallado__General 

	Declare #cursor_002 
	Cursor For 
		Select IdFarmacia  
		From #tmpF (NoLock) 
		Order By IdFarmacia 
	Open #cursor_002
	FETCH NEXT FROM #cursor_002 Into @IdFarmacia  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 		

				----Set @TipoDispensacion = 1 
				----Exec spp_Rpt_Administrativos @IdEmpresa, @IdEstado, @IdFarmacia, 
				----							 @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
				----							 @TipoDispensacion, @FechaInicial,  @FechaFinal, 
				----							 @TipoInsumo, @TipoInsumoMedicamento, @SubFarmacias 

				If Not exists ( Select Name From sysobjects (noLock) Where Name = 'RptAdmonDispensacion_Detallado__General' and xType = 'U' )  
				Begin 
					Select *, 1 as TipoDispensacion  
					Into RptAdmonDispensacion_Detallado__General 
					From RptAdmonDispensacion_Detallado 
					Where 1 = 0  
				End 
					
				/* 	
				Insert Into RptAdmonDispensacion_Detallado__General
				Select *, @TipoDispensacion as TipoDispensacion 
				From RptAdmonDispensacion_Detallado 
				*/ 
				
					
				Set @TipoDispensacion = 2 
				Exec spp_Rpt_Administrativos @IdEmpresa, @IdEstado, @IdFarmacia, 
											 @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
											 @TipoDispensacion, @FechaInicial,  @FechaFinal, 
											 @TipoInsumo, @TipoInsumoMedicamento, @SubFarmacias 

				Insert Into RptAdmonDispensacion_Detallado__General
				Select *, @TipoDispensacion as TipoDispensacion 
				From RptAdmonDispensacion_Detallado 
				
		
			FETCH NEXT FROM #cursor_002 Into  @IdFarmacia  
		END
	Close #cursor_002 
	Deallocate #cursor_002 			





/* 
@TipoDispensacion 
    0 ==> Todo 
    1 ==> Consignacion 
    2 ==> Venta 
                       
                        drop table tmpRptAdmonDispensacion

                        select * from tmpRptAdmonDispensacion

@TipoInsumo 
    0 ==> Todo 
    1 ==> Medicamento  
    2 ==> Material de Curacion  

*/ 
