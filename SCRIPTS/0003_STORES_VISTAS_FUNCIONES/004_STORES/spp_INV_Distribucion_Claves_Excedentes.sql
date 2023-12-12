---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) where Name = 'spp_INV_Distribucion_Claves_Excedentes' and xType = 'P' ) 
   Drop Proc spp_INV_Distribucion_Claves_Excedentes 
Go--#SQL 
   
Create Proc spp_INV_Distribucion_Claves_Excedentes 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006'  
) 
With Encryption 
As 
Begin 
Set NoCount On 

--- Claves a Distribuir 
	Select ClaveSSA, Excedente, Distribuido 
	into #tmpExcedentes 
	From INV_Distribucion_Excedentes F (NoLock) 
	
	
----------------------- Claves a procesar  	
	Select F.ClaveSSA, (F.Excedente - F.Distribuido) as Excedente, 0 as Distribuido,  
		 T.Requerido, 0 as Distribuir, 
		 identity(int, 1,1) as Keyx  
	into #tmpDistribuir 
	From INV_Distribucion_Excedentes F (NoLock)
	Inner Join 
	( 
		Select ClaveSSA, sum(StockSugerido) as Requerido  
		From INV_Distribucion_Faltantes F (NoLock) 
		Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdJurisdiccion = @IdJurisdiccion 
			  and Exists ( Select * From #tmpExcedentes E Where F.ClaveSSA = E.ClaveSSA )  
		Group by ClaveSSA 
	) as T On ( F.ClaveSSA = T.ClaveSSA )  	
	Where -- F.ClaveSSA = '624'  and 
	      1 = 1 
--		  and F.ClaveSSA = '060.125.2711' 	
	Order by T.Requerido 	
----------------------- Claves a procesar  	



--------------------- Faltantes 
	Select F.IdEstado, F.IdFarmacia, F.ClaveSSA, 
		-- (StockSugerido - StockAsignado) as StockSugerido, 
		(F.StockSugerido - F.StockAsignado) as Stock_Aux, 
		E.Existencia, 
		((F.StockSugerido - F.StockAsignado) - E.Existencia ) as StockSugerido, 
		U.IdTipoUnidad as OrdenTipoUnidad 
	Into #tmpFaltantes 
	From INV_Distribucion_Faltantes F (NoLock) 	
	Inner Join INV_Distribucion_Existencias E (NoLock) 
		On ( F.IdEmpresa = E.IdEmpresa and F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and F.ClaveSSA = E.ClaveSSA ) 
	Inner Join vw_Farmacias U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia )
	Where F.IdEstado = @IdEstado and F.IdJurisdiccion = @IdJurisdiccion 
		-- and (StockSugerido - StockAsignado) > 0 
		and ((F.StockSugerido - F.StockAsignado) - E.Existencia ) > 0 		
		-- and F.ClaveSSA = '4326'  
		-- and F.ClaveSSA = '624' 
--------------------- Faltantes 

				
----------------------- Claves a Distribuir 			
	Update D Set Distribuir = 1 
	From #tmpDistribuir D 	
	Inner Join #tmpFaltantes F On ( D.ClaveSSA = F.ClaveSSA )  
----------------------- Claves a Distribuir 					

/* 
 		
select * from #tmpDistribuir 
Select * From #tmpFaltantes 	

*/ 	
	

---		spp_INV_Distribucion_Claves_Excedentes  	

/* 

	Update F Set Distribuido = 0 From INV_Distribucion_Excedentes F (NoLock)		
	Update F Set StockAsignado = 0 From INV_Distribucion_Faltantes F (NoLock)	

*/ 


Declare 
	@IdFarmacia varchar(4), 
	@ClaveSSA varchar(30), 
	@Excedente int, @Excedente_Aux int, 
	@Distribuido int, 
	@Requerido int, 
	@iKeyx int,  
	@StockSugerido int,  
	@Stock int  	

--- Iniciar proceso de distribucion 
    Declare #cExcedentes Cursor For 
    Select ClaveSSA, Excedente -- , Distribuido, Requerido, Keyx 
    From #tmpDistribuir 
    Where Distribuir = 1 
    order by keyx
    Open #cExcedentes
	FETCH NEXT FROM #cExcedentes Into @ClaveSSA, @Excedente -- , @Distribuido, @Requerido, @iKeyx 
		WHILE @@FETCH_STATUS = 0
        BEGIN  
			-- Print @ClaveSSA 	
			Set @Distribuido = 0 
			-- Set @Excedente = @Excedente_Aux  
			--  select top 1 * from INV_Distribucion_Faltantes 

---		spp_INV_Distribucion_Claves_Excedentes  
  
        
			--------------------------------- Cursor Interno 				
			Declare #cFaltanes Cursor For 
			Select F.IdFarmacia, StockSugerido 
			From #tmpFaltantes F (NoLock) 			
			Where ClaveSSA = @ClaveSSA -- and 1 = 0 
			Order by OrdenTipoUnidad, StockSugerido Desc  
			Open #cFaltanes
			FETCH NEXT FROM #cFaltanes Into @IdFarmacia, @StockSugerido 
				WHILE @@FETCH_STATUS = 0 
				BEGIN  
					
/* 

	Update F Set Distribuido = 0 From INV_Distribucion_Excedentes F (NoLock)		
	Update F Set StockAsignado = 0 From INV_Distribucion_Faltantes F (NoLock)	

*/ 
					
---		spp_INV_Distribucion_Claves_Excedentes  
					
					-- Print @IdFarmacia + ' ' + cast(@Excedente as varchar) +  '   '  + cast(@StockSugerido as varchar)  
					
					Set @Stock  = 0 
					If @StockSugerido <= @Excedente 
					Begin 
					   Set @Distribuido = @Distribuido + @StockSugerido 
					   Set @Excedente = @Excedente - @StockSugerido 
					   Set @Stock = @StockSugerido 					   
					End 					   
					
					If @Stock = 0 
					Begin    
						If @StockSugerido > @Excedente 
						Begin 
						   -- print 'sdsd' 
						   Set @Distribuido = @Distribuido + @Excedente 
						   Set @Stock = @Excedente 						   
						   Set @Excedente = 0  
						End 
					End 					   
					   
					-- Print '     ' + @IdFarmacia + ' ' + cast(@Excedente as varchar) +  '   '  + cast(@Stock as varchar) +  '  --- '  + cast(@Distribuido as varchar)
					   
					If @Stock > 0 
					Begin 
					   Update F Set StockAsignado = @Stock  
					   From INV_Distribucion_Faltantes F (NoLock) 
					   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ClaveSSA = @ClaveSSA 
					End    
					   
					
					FETCH NEXT FROM #cFaltanes Into @IdFarmacia, @StockSugerido 
				END 
			Close #cFaltanes
			Deallocate #cFaltanes
			--------------------------------- Cursor Interno 	
			
			-- print @Distribuido 
			If @Distribuido > 0 
			Begin 
			    -- Print 'Distribuido'
				Update F Set Distribuido = F.Distribuido + @Distribuido 
				From INV_Distribucion_Excedentes F (NoLock) 
				Where ClaveSSA = @ClaveSSA  
			End 
			
			FETCH NEXT FROM #cExcedentes Into @ClaveSSA, @Excedente  -- , @Distribuido, @Requerido, @iKeyx 
        END 
    Close #cExcedentes
    Deallocate #cExcedentes

---		spp_INV_Distribucion_Claves_Excedentes  


End 
Go--#SQL 

