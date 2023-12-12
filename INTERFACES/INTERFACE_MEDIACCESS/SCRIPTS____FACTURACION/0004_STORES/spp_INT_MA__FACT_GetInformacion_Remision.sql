---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_GetInformacion_Remision' and xType = 'P' )
    Drop Proc spp_INT_MA__FACT_GetInformacion_Remision
Go--#SQL 

--	Exec spp_FACT_CFD_GetRemision  @IdEmpresa = '002', @IdEstado = '09', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000001', @Detallado = '1'  
--- Exec spp_INT_MA__FACT_GetInformacion_Remision  @IdEmpresa = '002', @IdEstado = '09', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000001', @Detallado = '1' 
  
Create Proc spp_INT_MA__FACT_GetInformacion_Remision 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '4984', 
	@Detallado bit = 0   
) 
With Encryption		
As 
Begin 
Set NoCount On 
Declare 
	@Anexar__Lotes_y_Caducidades bit, 
	@EsInsumos bit, 	
	@EsMedicamento bit, 
	@sUnidadDeMedida varchar(100) 
	
Declare 
	@Keyx int, 
	@ClaveSSA varchar(50), 
	@Informacion varchar(max)  	

--------- Obtener datos iniciales 	
	Set @sUnidadDeMedida = ''
	Set @Keyx = 0 
	Set @ClaveSSA = '' 
	Set @Informacion = '' 
	Set @EsInsumos = 0  
	Set @EsMedicamento = 0  	
	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmaciaGenera = right('0000' + @IdFarmaciaGenera, 4) 		
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10) 	
	
	
	
	Select @sUnidadDeMedida = Descripcion From CFDI_UnidadesDeMedida (NoLock) Where IdUnidad = '001'
	Select @Anexar__Lotes_y_Caducidades = 0		---dbo.fg_FACT_GetParametro_AnexarLotes_y_Caducidades ( @IdEstado, @IdFarmaciaGenera )  
--------- Obtener datos iniciales 	



	--select * from FACT_CFD_UnidadesDeMedida 

------------------- INFORMACION BASE 
	Select	
		identity(int, 1, 1) as Keyx, 
		D.FolioVenta, D.IdProducto, D.CodigoEAN, 
		P.ClaveSSA, P.Descripcion as DescripcionComercial, 		
		cast((round(D.PrecioUnitario, 2, 1)) as numeric(14,2)) as PrecioUnitario, 
		cast((round(D.PrecioUnitario, 2, 1)) as numeric(14,2)) as PrecioUnitario_Aseguradora, 		
		
		cast(I.Porcentaje_02 as numeric(14, 2)) as Porcenjate, 
		(I.Porcentaje_02 / 100.00) as Porcenjate_Aplicar, 
		
		cast((D.CantidadVendida) as numeric(14,4)) as Cantidad, 
		cast((D.TasaIva) as numeric(14,2)) as TasaIva, 	
		cast(0 as numeric(14,4)) as SubTotal, 
		cast(0 as numeric(14,4)) as Iva, 
		cast(0 as numeric(14,4)) as Importe,  
		cast(@sUnidadDeMedida as varchar(100)) as UnidadDeMedida, 
		TipoDeRemision, TipoInsumo, 
		cast('' as varchar(100)) as SAT_ClaveProducto_Servicio, 
		cast('' as varchar(100)) as SAT_UnidadDeMedida	  
	Into #tmpDetalles 
	From FACT_Remisiones E (NoLock)	
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioRemision = D.FolioRemision ) 
	Inner Join INT_MA_Ventas_Importes I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa And D.IdEstado = I.IdEstado And D.IdFarmacia = I.IdFarmacia And D.FolioVenta = I.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )	
	Where E.IdEmpresa = @IdEmpresa And E.IdEstadoGenera = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision	
		and I.Porcentaje_02 > 0 
		--and D.TasaIva > 0 


	Update P Set SAT_ClaveProducto_Servicio = C.SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida = C.SAT_UnidadDeMedida 
	From #tmpDetalles P (NoLock) 
	Inner Join CatProductos_CFDI C (NoLock) On ( P.IdProducto = C.IdProducto ) 
	

	-------------------------------- ASIGNACION DE IMPORTES, REDONDEADOS, LOS CALCULOS SE REALIZA POR SEPARADO POR CUESTIONES DE LOS REDONDEOS 
	Update P Set PrecioUnitario_Aseguradora = Round( (PrecioUnitario * Porcenjate_Aplicar), 2, 1 ) 
	From #tmpDetalles P (NoLock) 
	
	Update P Set 
		SubTotal = Round( (PrecioUnitario_Aseguradora * Cantidad), 2, 1 )    
--		Iva = ((PrecioUnitario_Aseguradora * Cantidad) * ( TasaIva / 100.00 )),  		
--		Importe = ((PrecioUnitario_Aseguradora * Cantidad) * ( 1 + ( TasaIva / 100.00 ))) 
	From #tmpDetalles P (NoLock) 	
	
	Update P Set 
--		SubTotal = Round( (PrecioUnitario_Aseguradora * Cantidad), 2, 1 )    
		Iva = Round( (SubTotal * ( TasaIva / 100.00 )), 4, 0)   		
--		Importe = ((PrecioUnitario_Aseguradora * Cantidad) * ( 1 + ( TasaIva / 100.00 ))) 
	From #tmpDetalles P (NoLock) 	

	
---			spp_INT_MA__FACT_GetInformacion_Remision 


	Update P Set 
--		SubTotal = Round( (PrecioUnitario_Aseguradora * Cantidad), 2, 1 )    
--		Iva = Round( (SubTotal * ( TasaIva / 100.00 )), 2, 1)   		
		Importe = SubTotal + IVA  
	From #tmpDetalles P (NoLock) 	
	-------------------------------- ASIGNACION DE IMPORTES, REDONDEADOS 


	Select Top 1  
		@EsInsumos = (case when cast(TipoDeRemision as int) = 1 then 1 else 0 end),  	
		@EsMedicamento = (case when cast(TipoInsumo as int) = 2 then 1 else 0 end) 		
	From #tmpDetalles   
------------------- INFORMACION BASE  	


--	spp_INT_MA__FACT_GetInformacion_Remision	

	
----------------------- Información Adicional 
-------- Revisar si se aplica este valor 
----	Select * 
----	Into #vw_ClavesSSA_Sales
----	From vw_ClavesSSA_Sales 
	
----	Update D Set 
----			IdClaveSSA = C.IdClaveSSA_Sal, 
----			Descripcion = '[' + D.ClaveSSA + ']  ' + C.DescripcionClave + space(6) + '<< ' + C.Presentacion + ' >>', 
----			UnidadDeMedida = @sUnidadDeMedida   
----	From #tmpDetalles D (NoLock)  
----	Inner Join #vw_ClavesSSA_Sales C On ( D.ClaveSSA = C.ClaveSSA  )  
----------------------- Información Adicional 
	
	
----		spp_INT_MA__FACT_GetInformacion_Remision   


------------------------------------------ Anexar Lotes y Caducidades 
	-- Select @Anexar__Lotes_y_Caducidades,  @EsInsumos,  @EsMedicamento 
	If @Anexar__Lotes_y_Caducidades = 1	
	Begin	
		If @EsInsumos = 1 and @EsMedicamento  = 1	
		   Set @Anexar__Lotes_y_Caducidades = 1	
		Else 
		   Set @Anexar__Lotes_y_Caducidades = 0	
	End		
		
	If @Anexar__Lotes_y_Caducidades = 1 
		Begin 
			Declare #cursorClaves 
			Cursor For 
			Select Keyx, ClaveSSA   
			From #tmpDetalles 
			Order By Keyx 
			Open #cursorClaves
			FETCH NEXT FROM #cursorClaves Into @Keyx, @ClaveSSA  
				WHILE @@FETCH_STATUS = 0 
				BEGIN 				
					Exec spp_INT_MA__FACT_GetInformacion_Remision_Detalles @IdEmpresa, @IdEstado, @IdFarmaciaGenera, @FolioRemision, @ClaveSSA, @Informacion output 
					
					Update D Set Descripcion = D.Descripcion + space(3) + @Informacion 
					From #tmpDetalles D 
					Where Keyx = @Keyx   
					 
					FETCH NEXT FROM #cursorClaves Into @Keyx, @ClaveSSA 
				END
			Close #cursorClaves 
			Deallocate #cursorClaves 			
		End 	
------------------------------------------ Anexar Lotes y Caducidades 


----		spp_INT_MA__FACT_GetInformacion_Remision   



------------------------------------------ SALIDA FINAL		
	Select	
		-- @Anexar__Lotes_y_Caducidades, 
		-- @EsInsumos, @EsMedicamento,	
		
		IdProducto, 
		SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
		CodigoEAN, DescripcionComercial, 
		
		cast(D.PrecioUnitario as numeric(14, 2)) as PrecioUnitario_Venta, 
		Porcenjate, 
				
		cast(D.PrecioUnitario_Aseguradora as numeric(14, 2)) as PrecioUnitario, 
		
		----cast(D.Cantidad as numeric(14, 2)) as Cantidad, 
		----cast(D.TasaIva as numeric(14, 2)) as TasaIva, 
		----cast(D.SubTotal as numeric(14, 2)) as SubTotal, 
		----cast(D.Iva as numeric(14, 2)) as Iva, 
		----cast(D.Importe as numeric(14, 2)) as Importe,  
		
		cast(sum(D.Cantidad) as numeric(14, 2)) as Cantidad, 
		cast(max(D.TasaIva) as numeric(14, 4)) as TasaIva, 
		cast(sum(D.SubTotal) as numeric(14, 4)) as SubTotal, 
		cast(sum(D.Iva) as numeric(14, 4)) as Iva, 
		cast(sum(D.Importe) as numeric(14, 4)) as Importe,  	
		
		@sUnidadDeMedida as UnidadDeMedida	
	Into #tmpDetalles__Final 
	From #tmpDetalles D (NoLock)	
	Group by 
		IdProducto, 
		SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida,  
		CodigoEAN, DescripcionComercial, D.PrecioUnitario, D.Porcenjate, D.PrecioUnitario_Aseguradora, D.TasaIva 
	Order By DescripcionComercial 
	

	-------------- SALIDA FINAL 

---			spp_INT_MA__FACT_GetInformacion_Remision 

	Update P Set 
		SubTotal = dbo.fg_PRCS_Redondear( SubTotal, 2 ), Iva = dbo.fg_PRCS_Redondear( Iva, 2)        
	From #tmpDetalles__Final P (NoLock) 	

	Update P Set 
		Importe = dbo.fg_PRCS_Redondear( SubTotal + IVA, 2 )         
	From #tmpDetalles__Final P (NoLock) 	


	Select 
		IdProducto, 
		SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, 
		CodigoEAN, DescripcionComercial, 
		
		cast(D.PrecioUnitario as numeric(14, 2)) as PrecioUnitario_Venta, 
		Porcenjate, 
				
		cast(D.PrecioUnitario as numeric(14, 4)) as PrecioUnitario, 
		cast((D.Cantidad) as numeric(14, 2)) as Cantidad, 
		cast((D.TasaIva) as numeric(14, 4)) as TasaIva, 
		cast((D.SubTotal) as numeric(14, 4)) as SubTotal, 
		cast((D.Iva) as numeric(14, 4)) as Iva, 
		cast((D.Importe) as numeric(14, 4)) as Importe,  	
		
		@sUnidadDeMedida as UnidadDeMedida 

	From #tmpDetalles__Final D (NoLock)	
	Order By DescripcionComercial 

------------------------------------------ SALIDA FINAL			
	
End
Go--#SQL


---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_GetInformacion_Remision_Detalles' and xType = 'P' )
    Drop Proc spp_INT_MA__FACT_GetInformacion_Remision_Detalles
Go--#SQL
  
Create Proc spp_INT_MA__FACT_GetInformacion_Remision_Detalles  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '0000000004', 
	@ClaveSSA varchar(50) = '11', @Informacion varchar(max) = '' output   
) 
With Encryption		
As
Begin
Set NoCount On 
Declare 
	@sDatos varchar(50),  
	@sDatosCaducidad varchar(max) 

	Set @sDatos = '' 
	Set @sDatosCaducidad = '' 

----------------- Obtener datos 

---		spp_INT_MA__FACT_GetInformacion_Remision	
			

----	Select Identity(int, 1, 1 ) as Keyx, 
----		-- '[' + (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) + ']' as Informacion   
----		-- (E.IdSubFarmacia + '_' + E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) as Informacion   		
----		(E.ClaveLote + IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')) as Informacion 
----	Into #tmpDetalles__Lotes  	
----	From   
----	( 
----		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, ClaveSSA, IdProducto, CodigoEAN, ClaveLote  
----		From FACT_Remisiones_Detalles C (NoLock) 
----		Where 
----			C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado 
----			And C.IdFarmaciaGenera = @IdFarmaciaGenera And C.FolioRemision = @FolioRemision	
----			--And C.ClaveSSA = @ClaveSSA 	
----	) E 
----	Left Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
----		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia 
----			 and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote ) 
----	Group by E.IdSubFarmacia, E.ClaveLote, IsNull('_' + convert(varchar(7), L.FechaCaducidad, 120), '')  
----	--Order by L.IdSubFarmacia, L.ClaveLote  	
--------------------- Obtener datos 	
	
----	--Select * from #tmpDetalles__Lotes 
	
----	----Select *, char(39) + Informacion + char(39)  
----	----from #tmpDetalles__Lotes 
	
----	Declare #cursorDetalles 
----	Cursor For 
----	Select Informacion   
----	From #tmpDetalles__Lotes (NoLock) 
----	Order by Keyx 
----	Open #cursorDetalles
----	FETCH NEXT FROM #cursorDetalles Into @sDatos  
----		WHILE @@FETCH_STATUS = 0 
----		BEGIN 				
----			-- Set @sDatosCaducidad = @sDatosCaducidad + @sDatos + ' ' 		
----			Set @sDatosCaducidad = @sDatosCaducidad + @sDatos + ', '	
----			FETCH NEXT FROM #cursorDetalles Into  @sDatos  
----		END
----	Close #cursorDetalles 
----	Deallocate #cursorDetalles 	

----	Set @sDatosCaducidad = ltrim(rtrim(@sDatosCaducidad)) 
----	Set @sDatosCaducidad = left(@sDatosCaducidad, len(@sDatosCaducidad)-1) 
----	Select @Informacion = @sDatosCaducidad 
----	Print @Informacion 
	

End 
Go--#SQL 
