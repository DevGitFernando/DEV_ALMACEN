If exists ( Select * From tempdb..Sysobjects (NoLock) Where Name = '##tmpClaves_Negadas_Valorizado' and xType = 'U' ) 	 
   Drop Table tempdb..##tmpClaves_Negadas_Valorizado  
Go--#SQL 	   
	   
--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Valorizado' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Valorizado 
Go--#SQL 

Create Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Valorizado   
( 
	-- @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', 
	@Año int = 2011, @Mes int  = 9 -- ,  @Dia int 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@IdEstado int, 
	@IdJurisdiccion int, 
	@IdFarmacia int, 
	@iAño int, @iMes int,  @Dia int, 
	@iDiasSancion int, @iEsSancion int, 
	@iKeyx int, 
	@ClaveSSA varchar(30)  
	
Declare 
	@iTope int, 
	@iSancion_Base numeric(14, 2), 
	@iSancion_1 numeric(14, 2), 
	@iSancion_2 numeric(14, 2) 

Declare
	@iDiaBase int 


		
---- Dato configurable 	
	Set @iDiaBase = 0 
	Set @iTope = 0 
	Set @iSancion_Base = 0.02 	
	Set @iSancion_1 = 0.02 
	Set @iSancion_2 = 1.02 
		
--	Select @Año as Año, @Mes as Mes 
	
	If exists ( Select * From tempdb..Sysobjects (NoLock) Where Name = '##tmpClaves_Negadas_Valorizado' and xType = 'U' ) 	 
	   Drop Table tempdb..##tmpClaves_Negadas_Valorizado  
	
	Select Distinct 
		T.IdEstado, T.IdJurisdiccion, T.IdFarmacia, 
		T.Año, T.Mes, T.Dia, 
		
		cast((cast(T.Año as varchar) + right('00' + cast(T.Mes as varchar),2) + right('00' + cast(T.Dia as varchar), 2)) as datetime ) as Fecha,   
		
		T.ClaveSSA, -- T.DescripcionClave, T.Presentacion, 
		-- 0 as CantidadRequerida, 0 as CantidadVales, 
		0 as Cantidad, PrecioLicitacion,  
		identity(int, 1, 1) as Keyx, 0 as Orden, 
		1 as DiasDiferencia, 
		0 as DiasSancion,    
		0 as DiasSancion_Aux, 
		cast(0 as numeric(14,4)) as PorcSancion, 
		cast(0 as numeric(14,4)) as ValorSancion    		
	into ##tmpClaves_Negadas_Valorizado  
	From ##tmpClaves_Negadas T (NoLock) 	
--	From dbo.fg_Mes(@Año, @Mes) M, ##tmpClaves_Negadas T (NoLock) 
--	From dbo.fg_Mes(@Año, @Mes) M Inner Join ##tmpClaves_Negadas T (NoLock) On ( M.Año = T.Año and M.Mes = T.Mes and M.Dia = T.Dia ) 
	-- Where ClaveSSA in ( '624', '2302ggg' )  
	
--	select * from ##tmpClaves_Negadas_Valorizado 
	
	
	Update T Set -- CantidadRequerida = D.CantidadRequerida, CantidadVales = D.CantidadVales, 
		Cantidad = D.Cantidad, DiasSancion = 1, DiasSancion_Aux = 1 
	From ##tmpClaves_Negadas_Valorizado T (NoLock) 
	Inner Join ##tmpClaves_Negadas D 
		On ( T.IdEstado = D.IdEstado and T.IdJurisdiccion = D.IdJurisdiccion and T.IdFarmacia = D.IdFarmacia 
			and T.Año = D.Año and T.Mes = D.Mes and T.Dia = D.Dia and T.ClaveSSA = D.ClaveSSA ) 


/* 
---------------------	
	Update T Set DiasSancion = -- T.DiasSancion + 
	IsNull
		(
			( 
				select 
					-- (case when max(O.Cantidad) = 0 then 0 else count(*) end) 
					count(*) 
					-- count(distinct O.DiasSancion) 
				From ##tmpClaves_Negadas_Valorizado O (NoLock)  
				where 
					T.IdEstado = O.IdEstado and T.IdJurisdiccion = O.IdJurisdiccion 
					and T.Año = O.Año and T.Mes = O.Mes and T.ClaveSSA = O.ClaveSSA 
					and T.Dia > (O.Dia-1) --and O.Cantidad > 0 
				-- Order By O.DiasSancion desc 	
			) +  0
		, 0)	
	From ##tmpClaves_Negadas_Valorizado T (NoLock) 	
	Where T.Cantidad > 0 		



	select * from ##tmpClaves_Negadas_Valorizado 
*/ 



--------- Determinar los dias de Sancion por Clave 
------	Select IdEstado, IdJurisdiccion, ClaveSSA, Año, Mes 
------	From ##tmpClaves_Negadas_Valorizado (NoLock) 
------	Group By IdEstado, IdJurisdiccion, ClaveSSA, Año, Mes  

 				
--- Agrupar por Claves 
    Declare #cursor_Claves 
    Cursor For 
		Select IdEstado, IdJurisdiccion, IdFarmacia, ClaveSSA, Año, Mes 
		From ##tmpClaves_Negadas_Valorizado (NoLock) 
		Group By 
			IdEstado, IdJurisdiccion, IdFarmacia, ClaveSSA, Año, Mes   	
    Open #cursor_Claves
    FETCH NEXT FROM #cursor_Claves Into @IdEstado, @IdJurisdiccion, @IdFarmacia, @ClaveSSA, @iAño, @iMes 
        WHILE @@FETCH_STATUS = 0
        BEGIN
--------------------------------------        
----------- Detalles por Clave 
			Set @iTope = 0 
			Set @iDiasSancion = 0 
			Set @iSancion_1 = 0 
			Set @iSancion_2 = 1 
			Set @iDiaBase = 0  
			
			Declare #cursor_Detalles  
			Cursor For 
				Select -- ClaveSSA, Año, Mes, 
					   Dia, DiasSancion, Keyx   
				From ##tmpClaves_Negadas_Valorizado (NoLock)	
				Where 
					IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion and IdFarmacia = @IdFarmacia 
					and ClaveSSA = @ClaveSSA and Año = @iAño and Mes = @iMes     
					-- and Dia between 4 and 15 
				Order By Dia 		
			Open #cursor_Detalles 
			FETCH NEXT FROM #cursor_Detalles  Into @Dia, @iEsSancion, @iKeyx 
				WHILE @@FETCH_STATUS = 0
				BEGIN 	           
					--- Proceso calculos dias de sancion 
					--- Set @iDiasSancion = @iDiasSancion + 1  

---		spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Valorizado	


--					Print cast(@iDiaBase as varchar) + '     ' +  cast(@Dia as varchar) 				
					If @iDiaBase = 0 
					Begin 
					   Set @iDiaBase =  @Dia 
					   Set @iDiasSancion = 1 
					End
					Else 
					Begin 
						If @iDiaBase = ( @Dia - 1 ) 
						Begin 
						   Set @iDiasSancion = @iDiasSancion + 1 
						   -- Print cast(@iDiaBase as varchar) + '     ' +  cast(@Dia as varchar) 		
						End
						Else 
						Begin 
							Set @iDiasSancion = 1 
						End   
						Set @iDiaBase =  @Dia 
					End  					


--		Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Valorizado 2011, 10   

					If @iDiasSancion <=  2
					   Set @iSancion_1 = @iSancion_Base
					Else 
					   Set @iSancion_1 = 1 + @iSancion_Base	

					If @iDiasSancion = 0 
					   Set @iSancion_1 = 0 

----					If @iDiasSancion <=  2 
----						Begin 
----							Set @iSancion_1 = @iSancion_1 + @iSancion_Base 	
----						End 
----					Else 
----						Begin 
----							if @iTope = 0 
----							Begin 
----								Set @iSancion_2 = @iSancion_2 + @iSancion_1  	
----								Set @iSancion_1 = @iSancion_2 
----								Set @iTope = 1 
----							End 
----						End 
						
					If @iEsSancion = 0 
					Begin 
						Set @iTope = 0 
						Set @iDiasSancion = 0 
						Set @iSancion_1 = 0 
						Set @iSancion_2 = 1  
					End 
					
		 
					Update D Set DiasSancion = @iDiasSancion, PorcSancion = @iSancion_1 
					From ##tmpClaves_Negadas_Valorizado D (NoLock) 
					Where Keyx = @iKeyx 
		 
					FETCH NEXT FROM #cursor_Detalles  Into  @Dia, @iEsSancion, @iKeyx  
				END
			Close #cursor_Detalles 
			Deallocate #cursor_Detalles 
----------- Detalles por Clave 
--------------------------------------        
 
			FETCH NEXT FROM #cursor_Claves Into  @IdEstado, @IdJurisdiccion, @IdFarmacia, @ClaveSSA, @Año, @Mes
        END
    Close #cursor_Claves 
    Deallocate #cursor_Claves 
 


    
--- Determinar el valor de la Sancion por Clave 
	Set @iSancion_1 = 0.02 
	Set @iSancion_2 = 1.02 
		
--	Set @iSancion_1 = 1.06 
--	Set @iSancion_2 = 1.06 

--	Update V Set ValorSancion = (Cantidad * PrecioLicitacion) * @iSancion_2  --- PorcSancion 			
	Update V Set ValorSancion = (Cantidad * PrecioLicitacion) * PorcSancion 
	From ##tmpClaves_Negadas_Valorizado V 
--	Where DiasSancion Between 1 and 2 

----	Update V Set ValorSancion = (Cantidad * PrecioLicitacion) * @iSancion_2 
----	From ##tmpClaves_Negadas_Valorizado V 
----	Where DiasSancion >= 3  

		
--	Update N Set ValorSancion = V.ValorSancion, PorcSancion = V.PorcSancion  		
	Update N Set ValorSancion = V.ValorSancion, PorcSancion = @iSancion_2 
	From ##tmpClaves_Negadas N 
	Inner Join ##tmpClaves_Negadas_Valorizado V 
		On ( N.IdEstado = V.IdEstado and N.IdJurisdiccion = V.IdJurisdiccion and N.IdFarmacia = V.IdFarmacia 
			and N.ClaveSSA = V.ClaveSSA and N.Año = V.Año and N.Mes = V.Mes and N.Dia = V.Dia ) 

------- Detalle 
----	Select IdEstado, IdJurisdiccion, ClaveSSA, Año, Mes 
----	From ##tmpClaves_Negadas_Valorizado (NoLock) 
----	Group By IdEstado, IdJurisdiccion, ClaveSSA, Año, Mes  

----	Select * 	From ##tmpClaves_Negadas (NoLock)  
--		Select * 	From ##tmpClaves_Negadas_Valorizado (NoLock)  
	
	

	
End 
Go--#SQL 
		