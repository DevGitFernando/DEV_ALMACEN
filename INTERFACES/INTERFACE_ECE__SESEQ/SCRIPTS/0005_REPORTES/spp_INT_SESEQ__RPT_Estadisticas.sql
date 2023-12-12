-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RPT_Estadisticas' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RPT_Estadisticas
Go--#SQL 

Create Proc spp_INT_SESEQ__RPT_Estadisticas 
( 
	@IdEmpresa varchar(3) = '1', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '13', 
	
	@FechaInicial varchar(10) = '2020-01-01', 
	@FechaFinal varchar(10) = '2021-01-31'  	  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@iTiene_RecepcionPrevia int, 
	@iNumeroDeRecepciones int 	   


	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 


	------------ Obtener informacion 
	Select 
		IdEstado, IdFarmacia, cast('' as varchar(500)) as Farmacia, 
		cast(@@servername as varchar(100)) as Servidor, 
		cast(db_name() as varchar(500)) as BaseDeDatos, 
		year(FechaReceta) as Año, 
		month(FechaReceta) as Mes, 
		--cast(RecepcionDuplicada as int) as RecepcionDuplicada, 

		FolioSurtido, 
		cast(0 as int) as Recetas_TotalesCapturadas, 

		cast(EsSurtido as int) as EsSurtido, cast(EsSurtido_Electronico as int) as EsSurtido_Electronico, cast(Procesado as int) as RespuestaEnviada, 

		(case when cast(EsSurtido as int) = 1 and cast(Procesado as int) = 1 then 1 else 0 end ) as RespuestaEnviada_Confirmada, 
		(case when cast(EsSurtido as int) = 1 and cast(Procesado as int) = 0 then 1 else 0 end ) as RespuestaEnviada_NoConfirmada,     

		(case when cast(EsSurtido as int) = 1 then 1 else 0 end ) as Recetas_Surtidas, 
		(case when cast(EsSurtido as int) = 0 then 1 else 0 end ) as Recetas_No_Surtidas,     

		(case when cast(EsSurtido as int) = 1 and cast(EsSurtido_Electronico as int) = 1 then 1 else 0 end ) as Recetas_Electronicas, 
		(case when cast(EsSurtido as int) = 1 and cast(EsSurtido_Electronico as int) = 0 then 1 else 0 end ) as Recetas_Manuales

	Into #tmp_Recetas 
	from INT_SESEQ__RecetasElectronicas_0001_General (nolock) 
	where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and 
		convert(varchar(10), FechaReceta, 120) between @FechaInicial and  @FechaFinal 
		and RecepcionDuplicada in ( 0, 0 ) 
	----group by 
	----	year(FechaReceta), month(FechaReceta), -- , RecepcionDuplicada, EsSurtido, EsSurtido_Electronico, Procesado
	----	--EsSurtido, EsSurtido_Electronico, Procesado  
	----	cast(EsSurtido as int), cast(EsSurtido_Electronico as int), cast(Procesado as int) 
	order by Año, Mes -- , Procesado, EsSurtido 


	Select 
		IdEstado, IdFarmacia, 
		year(FechaRegistro) as Año, 
		month(FechaRegistro) as Mes, 
		count(*) as Recetas 
	Into #tmp_Ventas
	from VentasEnc (nolock) 
	where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and 
		convert(varchar(10), FechaRegistro, 120) between @FechaInicial and  @FechaFinal 
	Group by 
		IdEstado, IdFarmacia, 
		year(FechaRegistro), 
		month(FechaRegistro)  



--		spp_INT_SESEQ__RPT_Estadisticas  

	Select 
		V.FolioVenta, V.FechaRegistro, V.IdCliente, V.IdSubCliente, I.NumReceta,  
		IsNull(R.FolioSurtido, '-1') as Valor  
	Into #tmp_VentasExtra 
	From VentasEnc V (nolock) 
	Inner join VentasInformacionAdicional I (NoLock) On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta ) 
	Left join INT_SESEQ__RecetasElectronicas_0001_General R (NoLock) On ( V.IdEstado = R.IdEstado and V.IdFarmacia = R.IdFarmacia and V.FolioVenta = R.FolioSurtido ) 
	where V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and 
		convert(varchar(10), FechaRegistro, 120) between @FechaInicial and  @FechaFinal 
	Order by V.FolioVenta 
	
	----select * 
	----from #tmp_VentasExtra 
	----where Valor = '-1'
	--FolioSurtido

	------------ Obtener informacion 



	------------ GENERAR ESTADISTICAS   
	Select 	
		IdEstado, IdFarmacia, Farmacia, 
		Servidor, BaseDeDatos, 
		
		Año, Mes, 	
		sum(Recetas_TotalesCapturadas) as Recetas_TotalesCapturadas, 
		count(*) as RecetasRecibidas, 

		sum(RespuestaEnviada_Confirmada) as RespuestaEnviada_y_Confirmada, 
		sum(RespuestaEnviada_NoConfirmada) as RespuestaEnviada_NoConfirmada,   


		sum(Recetas_Surtidas) as Recetas_Surtidas, 
		sum(Recetas_No_Surtidas) as Recetas_NoSurtidas,   
		cast(0 as numeric(14,4)) as Porcentaje__Recetas_Surtidas, 
		cast(0 as numeric(14,4)) as Porcentaje__Recetas_NoSurtidas, 
		cast(0 as numeric(14,4)) as TotalPorcentaje__Recetas, 

		sum(Recetas_Electronicas) as Recetas_Electronicas, 
		sum(Recetas_Manuales) as Recetas_Manuales,   
		cast(0 as numeric(14,4)) as Porcentaje__Recetas_Electronicas, 
		cast(0 as numeric(14,4)) as Porcentaje__Recetas_Manuales,  
		cast(0 as numeric(14,4)) as TotalPorcentaje__RecetasAtendidas   

	Into #tmp_Recetas__Resumen 
	From #tmp_Recetas 
	Group by 
		IdEstado, IdFarmacia, Farmacia, 
		Servidor, BaseDeDatos, Año, Mes 
	Order by Año, Mes 


	Update R Set 
		Porcentaje__Recetas_Surtidas = (Recetas_Surtidas / (RecetasRecibidas * 1.0)) * 100,  
		Porcentaje__Recetas_NoSurtidas = (Recetas_NoSurtidas / (RecetasRecibidas * 1.0)) * 100, 

		Porcentaje__Recetas_Electronicas = (Recetas_Electronicas / (Recetas_Surtidas * 1.0)) * 100,  
		Porcentaje__Recetas_Manuales = (Recetas_Manuales / (Recetas_Surtidas * 1.0)) * 100  
	From #tmp_Recetas__Resumen R 

	Update R Set 
		TotalPorcentaje__Recetas = Porcentaje__Recetas_Surtidas + Porcentaje__Recetas_NoSurtidas, 
		TotalPorcentaje__RecetasAtendidas = Porcentaje__Recetas_Electronicas + Porcentaje__Recetas_Manuales 
	From #tmp_Recetas__Resumen R 


	Update R Set Recetas_TotalesCapturadas = V.Recetas  
	From #tmp_Recetas__Resumen R (noLock) 
	Inner Join #tmp_Ventas V (NoLock) On ( R.IdEstado = V.IdEstado and R.IdFarmacia = V.IdFarmacia and R.Año = V.Año and R.Mes = V.Mes  ) 

	----Select 
	----	IdEstado, IdFarmacia, 
	----	year(FechaRegistro) as Año, 
	----	month(FechaRegistro) as Mes, 
	----	count(*) as Recetas 
	----Into #tmp_Ventas
	------------ GENERAR ESTADISTICAS  


--		spp_INT_SESEQ__RPT_Estadisticas  


	--------------------- Salida final 
	Select 
		--F.IdEstado, F.Estado, 
		--F.IdFarmacia, F.Farmacia,  
		--Servidor, BaseDeDatos, 
		Año, Mes, 
		
		'Total de recetas atendidas' = Recetas_TotalesCapturadas, 
		'Recetas recibidas por interface' = RecetasRecibidas, 

		'Respuestas enviadas y confirmada' = RespuestaEnviada_y_Confirmada, 
		'Respuestas enviadas no confirmada' = RespuestaEnviada_NoConfirmada,   

		'Recetas electrónicas Surtidas' = Recetas_Surtidas, 
		'Recetas electrónicas No surtidas' = Recetas_NoSurtidas,   
		'% Recetas surtidas' = Porcentaje__Recetas_Surtidas, 
		'% Recetas no surtidas' = Porcentaje__Recetas_NoSurtidas, 
		--TotalPorcentaje__Recetas, 

		'Recetas electrónicas' = Recetas_Electronicas, 
		'Recetas manuales' = Recetas_Manuales,   
		'% Recetas electrónicas' = Porcentaje__Recetas_Electronicas, 
		'% Recetas manuales' = Porcentaje__Recetas_Manuales   
		
		--TotalPorcentaje__RecetasAtendidas 
	From #tmp_Recetas__Resumen R 
	Inner Join vw_Farmacias F (noLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 
	Order by 
		R.Año, R.Mes, R.IdFarmacia 

	----select count(*) From #tmp_Recetas__Resumen  
	----select count(*) From #tmp_Ventas 
	----select count(*) From #tmp_Recetas 

End 
Go--#SQL 
	