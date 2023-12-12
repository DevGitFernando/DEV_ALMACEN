Set NoCount On 
set DateFormat YMD 
Go--#SQL 


------------------- DEPURAR LA TABLA DESTINO 
----	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__Dispensacion' and xType = 'U' ) 
----		Drop Table INT_ND__Dispensacion 		
----Go--#SQL 

/* 
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion_Detallado__General' and xType = 'U' ) 
		Truncate Table INT_ND_RptAdmonDispensacion_Detallado__General 		
*/ 


	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__Dispensacion' and xType = 'U' ) 
		Truncate Table INT_ND__Dispensacion 		
Go--#SQL 


---   5, 34, 188, 224, 313  
	
	If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpF_X%' and xType = 'U' )  
		Drop Table tempdb..#tmpF_X 

	select space(2) as IdEstado, space(4) as IdFarmacia, space(20) as CodigoCliente Into #tmpF_X where 1  = 0  
	Insert Into #tmpF_X 
	Select F.IdEstado, F.IdFarmacia, C.CodigoCliente   
	From CatFarmacias F (NoLock) 
	Inner Join INT_ND_Clientes C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) 
	Where F.IdEstado = 16 and F.IdFarmacia = 11 -- in ( 11  ) -- , 13, 14, 15, 18, 30, 32, 41 )  	-- and 1 = 0	


	If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpPeriodos_X%' and xType = 'U' )  
		Drop Table tempdb..#tmpPeriodos_X 
	
	Select top 0 identity(int, 1, 1) as Orden, 
		cast('' as varchar(10)) as FechaInicial, cast('' as varchar(10)) as FechaFinal  
	Into #tmpPeriodos_X 
	-- Where 1 = 0 
	
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-08-15', '2014-08-31' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-09-01', '2014-09-30' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-10-01', '2014-10-31' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-11-01', '2014-11-30' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2014-12-01', '2014-12-31' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-01-01', '2015-01-31' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-02-01', '2015-02-28' 

	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-03-01', '2015-03-31' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-04-01', '2015-04-30' 
	Insert Into #tmpPeriodos_X ( FechaInicial, FechaFinal ) Select '2015-05-01', '2015-05-31' 	
	
	

--	select * from 	#tmpF_X 
	
--	Insert Into #tmpF_X select 11, '0003' 
----	Insert Into #tmpF_X select '0056' 	
----	Insert Into #tmpF_X select '0188' 	
----	Insert Into #tmpF_X select '0224' 
----	Insert Into #tmpF_X select '0313' 	
	

Declare 
    @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@CodigoCliente varchar(20), 
	@GUID varchar(100), 
	@FechaInicial varchar(10), @FechaFinal varchar(10),  
	@MostrarResultado int, 
	@Año_Causes int  

	Set @Año_Causes = 2012 
	Set @MostrarResultado = 1  
    Select 
        @IdEmpresa = '003', @IdEstado = '16', 
        @IdFarmacia = '0003', @CodigoCliente = '', @GUID = '',   
	    @FechaInicial = '2015-03-02', @FechaFinal = '2015-03-31' 


------------------- DEPURAR LA TABLA DESTINO   
	----If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__Dispensacion' and xType = 'U' ) 
	----	Drop Table INT_ND__Dispensacion 		

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
				Select IdFarmacia, CodigoCliente   
				From #tmpF_X (NoLock) 
				Order By IdFarmacia 
			Open #cursor_002
			FETCH NEXT FROM #cursor_002 Into @IdFarmacia, @CodigoCliente   
				WHILE @@FETCH_STATUS = 0 
				BEGIN 	
						---- Generar el GUID para el proceso a ejecutar  	
						Set @GUID = cast(NEWID() as varchar(max)) 
						-- Print @GUID  


						Exec spp_INT_ND_GenerarRemisiones_Periodo 
							@IdEmpresa, @IdEstado, @CodigoCliente, 
							@FechaInicial, @FechaFinal, 
							@GUID, @IdFarmacia, @Año_Causes  


					FETCH NEXT FROM #cursor_002 Into  @IdFarmacia, @CodigoCliente   
				END
			Close #cursor_002 
			Deallocate #cursor_002 		
	
			FETCH NEXT FROM #cursorPeriodos Into @FechaInicial,  @FechaFinal     
		END	 
	Close #cursorPeriodos 
	Deallocate #cursorPeriodos 	

	
	
	If @MostrarResultado = 1 
	Begin 
	
		Select EsCauses, sum(ImporteTotal)  as Importe 
		From INT_ND__Dispensacion (NoLock) 
		Group by EsCauses 
		
		
		Select IdFarmacia, NombreFarmacia, 
			EsCauses, sum(ImporteTotal)  as Importe 
		From INT_ND__Dispensacion (NoLock) 
		Group by IdFarmacia, NombreFarmacia, EsCauses 	
		
		
		Select 
			IdFarmacia, NombreFarmacia, 
			CodigoCliente, Modulo, 
			replace(convert(varchar(10), FechaRemision, 120), '-', '') as FechaRemision, 
			-- FechaRemision, 
			Prioridad, IdAnexo, NombreAnexo, Tipo, FolioRemision, 
			ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_Mascara, Descripcion_Mascara, 
			Cantidad, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, Procesado, Keyx, Keyx_Anexo, 
			EnResguardo, 
			replace(convert(varchar(10), FechaRemision, 120), '-', '') as FechaGeneracion  
		From INT_ND__Dispensacion  (NoLock)  
		Where -- Procesado = 1  
			InCluir = 1 and 
			Cantidad > 0 
		Order by FechaRemision, Prioridad, EnResguardo desc, Procesado, IdAnexo, Descripcion_Mascara   
		
		
	End 
	
	
		