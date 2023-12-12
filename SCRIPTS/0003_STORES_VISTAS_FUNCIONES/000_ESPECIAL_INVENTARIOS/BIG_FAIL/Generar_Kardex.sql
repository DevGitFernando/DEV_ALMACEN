
/* 
	sp_listacolumnas MovtosInv_enc 


*/ 

/* 
	drop table tmpMovtosInv 
	
	select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, FolioMovtoInv_Aux, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, Status, 
	     identity(int, 1, 1) as Orden, 0 as Procesado, 0 as Error     
	into tmpMovtosInv 
	from MovtosInv_enc 
	order by FechaRegistro 
*/ 

/* 
	select top 1 * from FarmaciaProductos  
	update FarmaciaProductos set Existencia = 0, CostoPromedio = 0, UltimoCosto = 0 
	update FarmaciaProductos_CodigoEAN set Existencia = 0 
	update FarmaciaProductos_CodigoEAN_Lotes set Existencia = 0 
*/

--	Select * 	From tmpMovtosInv 
	

Begin tran 

/*
	update tmpMovtosInv set Procesado = 0, Error = 0  
	update FarmaciaProductos set Existencia = 0, CostoPromedio = 0, UltimoCosto = 0 
	update FarmaciaProductos_CodigoEAN set Existencia = 0 
	update FarmaciaProductos_CodigoEAN_Lotes set Existencia = 0 
*/ 
	
Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4), 	
	@FolioMovto varchar(30), 
	@iAplica smallint, 
	@iAfectaCostos smallint, 
	@iInicio int, 
	@iFinal int, 
	@iPos int, 
	@iError int	
	
	Set @iAplica = 1 
	Set @iAfectaCostos = 0 
	Set @iInicio = 5001 
	Set @iFinal = 6000  	
	Set @iPos = @iInicio   
	Set @iError  = 0 	
--			spp_INV_AplicarDesaplicarExistencia 

--  sp_truncatelog 1 

--  sp_backupdb 1, '', 'Kardex_Final_Detalle'

    Declare tmpCol Cursor For 
		Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, 
		(case when IdTipoMovto_Inv in ( 'II', 'EC', 'TE' ) then 1 else 0 end) as IdTipoMovto_Inv  
		From tmpMovtosInv  
		Where Orden between @iInicio and @iFinal 
		order by Orden  
    Open tmpCol 
    FETCH NEXT FROM tmpCol Into @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAfectaCostos 
        WHILE @@FETCH_STATUS = 0 and @iError = 0 
        BEGIN 
           
           Begin try 
	           Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAplica, @iAfectaCostos 
			   -- Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAplica, @iAfectaCostos 
			   Update tmpMovtosInv Set Procesado = 1 Where Orden = @iPos 
		   End try 
		   Begin catch 
			   Update tmpMovtosInv Set Procesado = 1, Error = 1 Where Orden = @iPos 
			   Set @iError = 1 
			   RaisError ('Error al generar Kardex ', 16,10 ) 
		   End catch 
			
           Set @iPos = @iPos + 1
           FETCH NEXT FROM tmpCol Into  @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovto, @iAfectaCostos
        END
    Close tmpCol
    Deallocate tmpCol  
    	

--		rollback tran 


--		commit tran 

