If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__PRCS__CalcularPorcentajes' and xType = 'P' ) 
   Drop Proc spp_INT_MA__PRCS__CalcularPorcentajes
Go--#SQL 

Create Proc spp_INT_MA__PRCS__CalcularPorcentajes 
With Encryption 
As 
Begin 
Set NoCount On 

--	sp_listacolumnas INT_MA_Ventas_Importes  


----------------------------------------- Obtener la informacion a procesar 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, 
		SubTotal_SinGravar, SubTotal_Gravado, 
		(SubTotal_SinGravar + SubTotal_Gravado) as SubTotal_General, 
		DescuentoCopago, 
		Importe_SinGravar, Importe_Gravado, Iva, Importe_Neto, 
		Porcentajes_Procesados, Porcentaje_01, Porcentaje_02
	Into #tmp__INT_MA_Ventas_Importes 
	From INT_MA_Ventas_Importes 
	Where Porcentajes_Procesados = 0 
		-- IdEstado = 9 and IdFarmacia = 11 and FolioVenta = 1227 
	Order by IdEmpresa, IdEstado, IdFarmacia, FolioVenta 
	
		
	Update I Set 
		Porcentajes_Procesados = 1, 
		Porcentaje_01 = round((((SubTotal_General-DescuentoCopago) / SubTotal_General) * 100), 0),  
		Porcentaje_02 = round(((DescuentoCopago / SubTotal_General) * 100), 0) 		
	From #tmp__INT_MA_Ventas_Importes I 	
	Where SubTotal_General > 0 


----------------------------------------- Obtener la informacion a procesar 	
	

----------------------------------------- Actualizar la tabla principal 
	Update D Set Porcentajes_Procesados = I.Porcentajes_Procesados, Porcentaje_01 = I.Porcentaje_01, Porcentaje_02 = I.Porcentaje_02 
	From INT_MA_Ventas_Importes D (NoLock) 
	Inner Join #tmp__INT_MA_Ventas_Importes I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdFarmacia = I.IdFarmacia and D.FolioVenta = I.FolioVenta ) 
	
	
	--select * from #tmp__INT_MA_Ventas_Importes 
	

End 
Go--#SQL 
	