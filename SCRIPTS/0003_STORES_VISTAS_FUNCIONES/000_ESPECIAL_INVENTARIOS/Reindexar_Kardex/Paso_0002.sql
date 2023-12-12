

BEGIN TRAN 

/* 
	-- Acciones 
	1-. Si el proceso termina sin errores ejecutar COMMIT TRAN 
	2.- Si el proceso termina con errores ejecutar ROLLBACK TRAN 


	COMMIT TRAN 
	
	ROLLBACK TRAN 

*/ 

Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4) 

Declare 
	@FolioMovto varchar(30), 
	@iAplica smallint, 
	@iAfectaCostos smallint, 
	@iInicio int, 
	@iFinal int, 
	@iPos int, 
	@iError int, 	
	@sSql varchar(1000) 


--- rollback tran 

	Set @IdEmpresa = '001'  
	Set @IdEstado = '22' 
	Set @IdFarmacia = '0003' 

----------------------------------------
--- Desmarcar las existencias 
	update tmpProceso_Reindexado set Procesado = 0, Error = 0  
	update FarmaciaProductos set Existencia = 0, CostoPromedio = 0, UltimoCosto = 0, Actualizado = 0  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	update FarmaciaProductos_CodigoEAN set Existencia = 0, Actualizado = 0  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 	
	
	update FarmaciaProductos_CodigoEAN_Lotes set Existencia = 0, Actualizado = 0  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	update FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones set Existencia = 0, Actualizado = 0  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 	
--- Desmarcar las existencias 
----------------------------------------	 
	
	Set @sSql = '' 
	Set @iAplica = 1 
	Set @iAfectaCostos = 0 
	Set @iInicio = 1 
	Set @iFinal = 4000                
	Set @iPos = @iInicio   
	Set @iError  = 0 	
--			spp_INV_AplicarDesaplicarExistencia 

/* 

	COMMIT TRAN 
	
	ROLLBACK TRAN 

*/ 

--  sp_truncatelog 1 

--  sp_backupdb 1, '', 'Kardex_Final_Detalle'

    Declare tmpCol Cursor For 
		Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, 
		-- (case when IdTipoMovto_Inv in ( 'II', 'EC', 'TE' ) then 1 else 0 end) as IdTipoMovto_Inv  
		IdTipoMovto_Inv 
		From tmpProceso_Reindexado  
		Where Orden between @iInicio and @iFinal 
		order by Orden  
    Open tmpCol 
    FETCH NEXT FROM tmpCol Into @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAfectaCostos 
        WHILE @@FETCH_STATUS = 0 and @iError = 0 
        BEGIN 
           
           Begin try 
-----	           Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAplica, @iAfectaCostos 
			   Set @iAfectaCostos = 0 
               Set @sSql = 'Exec spp_INV_AplicarDesaplicarExistencia ' 
                    + char(39) + @IdEmpresa + char(39) + ', '   
                    + char(39) + @IdEstado + char(39) + ', '   
                    + char(39) + @IdFarmacia + char(39) + ', '   
                    + char(39) + @FolioMovto + char(39) + ', '                       
                    + char(39) + cast(@iAplica as varchar) + char(39) + ', '   
                    + char(39) + cast(@iAfectaCostos as varchar) + char(39)                                                                             
               Exec(@sSql) 
			   -- Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAplica, @iAfectaCostos 
			   Update tmpProceso_Reindexado Set Procesado = 1 Where Orden = @iPos 
		   End try 
		   Begin catch 
			   Update tmpProceso_Reindexado Set Procesado = 1, Error = 1 Where Orden = @iPos 
			   Set @iError = 1 
               Print @sSql 	--- Folio con error 		   
			   RaisError ('Error al generar Kardex ', 16,10 ) 
		   End catch 
			
           Set @iPos = @iPos + 1
           FETCH NEXT FROM tmpCol Into  @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAfectaCostos
        END
    Close tmpCol
    Deallocate tmpCol  
    	

--		rollback tran 


--		commit tran 

