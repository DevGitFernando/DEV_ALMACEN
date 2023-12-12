------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Concentrado_Facturacion' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_Concentrado_Facturacion
Go--#SQL  
  
Create Proc spp_FACT_Remisiones_Concentrado_Facturacion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001', 
	
	@IdFuenteFinanciamiento varchar(4) = '0011', @IdFinanciamiento varchar(4) = '0003', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0011', 

	@TipoDeRemision smallint = 2,		-- 1 ==> Producto | 2 ==> Servicio 
	@TipoDeRemision_Complemento smallint = 0, 
	@TipoOrigenInsumos int = 1,			-- 1 ==> Venta | 2 ==> Consigna 
	@TipoDeInsumos int = 2,				-- 0 ==> Ambos | 1 ==> Medicamento | 2 == > Material de curacion  
	@TipoDispensacion int = 1,			-- 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	@BaseRemision int = 2,				-- 1 ==> Venta Normal | 2 ==> Relacionado a Facturas anticipadas  

	@Filtro_Folios int = 0, 
	@Folio_Inicial varchar(20) = '5728', @Folio_Final varchar(20) = '5728', 

	@Filtro_FechaPeriodoRemisionado int = 1, 
	@FechaInicial_PeriodoRemisionado varchar(10) = '2018-04-01', @FechaFinal_PeriodoRemisionado varchar(10) = '2018-04-30',  

	@Filtro_FechaEmisionRemision int = 0, 
	@FechaInicial_EmisionRemision varchar(10) = '2018-05-01', @FechaFinal_EmisionRemision varchar(10) = '2018-07-31', 
	
	@Referencia_01 varchar(100) = 'R04',  -- 'R04', 
	@Referencia_02 varchar(100) = '', 
	@Referencia_03 varchar(100) = '', 
	

	@PartidaGeneral int = 1, 
		   	 
	@Listado_de_Folios varchar(max) = ''

)  
With Encryption 
As 
Begin  
Set NoCount On 
Set DateFormat YMD 

Declare 
	@sSql varchar(max), 
	@sFiltro_TipoDeRemision varchar(max), 
	@sFiltro_TipoOrigenInsumos varchar(max), 
	@sFiltro_TipoDeInsumos varchar(max), 
	@sFiltro_TipoDispensacion varchar(max), 
	@sFiltro_BaseRemision varchar(max), 
	@sFiltro_FoliosRemision varchar(max), 
	@sFiltro_Fecha_PeriodoRemisionado varchar(max), 
	@sFiltro_Fecha_RemisionGenerada varchar(max), 
	@sFiltro_PartidaGeneral varchar(max) 

	

	Set @sSql = '' 
	Set @sFiltro_TipoDeRemision = '' 
	Set @sFiltro_TipoOrigenInsumos = '' 
	Set @sFiltro_TipoDeInsumos = '' 
	Set @sFiltro_TipoDispensacion = '' 
	Set @sFiltro_BaseRemision = '' 
	Set @sFiltro_FoliosRemision = '' 
	Set @sFiltro_Fecha_PeriodoRemisionado = '' 
	Set @sFiltro_Fecha_RemisionGenerada = '' 
	Set @sFiltro_PartidaGeneral = '' 

	------------------------------ TABLA BASE 
	Select top 0 
		1 as Agregar_Al_Listado, 
		IdEmpresa, IdEstado, IdFarmaciaGenera, FechaRemision, FechaValidacion, FolioRemision, cast('' as varchar(500)) as Farmacia, 
		Total, TipoDeRemision, TipoInsumo, OrigenInsumo, 
		EsFacturada, EsFacturable, EsExcedente, IdPersonalRemision, IdPersonalValida, IdFuenteFinanciamiento, IdFinanciamiento, 
		FechaInicial, FechaFinal, EsRelacionFacturaPrevia, Serie, Folio, EsRelacionMontos, PartidaGeneral, 
		cast('' as varchar(max)) as Referencia_01, 
		cast('' as varchar(max)) as Referencia_02, 
		cast('' as varchar(500)) as Referencia_03, 
		cast(0 as bit) as EsReferencia_01, cast(0 as bit) as EsReferencia_02, cast(0 as bit) as EsReferencia_03   
	Into #tmp__ListaRemisiones 
	From FACT_Remisiones R (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmaciaGenera 
		and IdFuenteFinanciamiento = @IdFuenteFinanciamiento  and IdFinanciamiento = @IdFinanciamiento 



	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
	------------------------------ ARMAR FILTROS  
	If @TipoDeRemision = 1 
		Begin  
			Set @sFiltro_TipoDeRemision = '    and TipoDeRemision in ( 1, 4 ) ' + char(10)   
		End 

	If @TipoDeRemision = 2 
		Begin  
			Set @sFiltro_TipoDeRemision = '    and TipoDeRemision in ( 2, 6 ) ' + char(10)   
		End 

	If @TipoDeRemision = 1 and @TipoDeRemision_Complemento = 1  
		Begin  
			Set @sFiltro_TipoDeRemision = '    and TipoDeRemision in ( 3, 5 ) ' + char(10)   
		End 

	If @TipoDeRemision = 2 and @TipoDeRemision_Complemento = 1  
		Begin  
			Set @sFiltro_TipoDeRemision = '    and TipoDeRemision in ( 7, 8 ) ' + char(10)   
		End 



	---- 1 ==> Venta | 2 ==> Consigna 
	If @TipoOrigenInsumos = 1 
		Set @sFiltro_TipoOrigenInsumos = '	and OrigenInsumo = 0 ' + char(10) 
	Else 
		Set @sFiltro_TipoOrigenInsumos = '	and OrigenInsumo = 1  ' + char(10)


	---- 0 ==> Ambos | 1 ==> Medicamento | 2 == > Material de curacion 
	If @TipoDeInsumos > 0 
	Begin 
		If @TipoDeInsumos = 1 
			Set @sFiltro_TipoDeInsumos = '	and TipoInsumo = ' + char(39) + '02' + char(39) + char(10)
		Else 
			Set @sFiltro_TipoDeInsumos = '	and TipoInsumo = ' + char(39) + '01' + char(39) + char(10)
	End 

	---- 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  
	If @TipoDispensacion = 1 
		Set @sFiltro_TipoDispensacion = '	and EsDeVales = 0 ' + char(10)
	Else 
		Set @sFiltro_TipoDispensacion = '	and EsDeVales = 1 ' + char(10)


	---- 1 ==> Venta Normal | 2 ==> Relacionado a Facturas anticipadas 
	If @BaseRemision = 1 
		Set @sFiltro_BaseRemision = '	and EsRelacionFacturaPrevia = 0 and EsFacturada = 0 ' + char(10) 
	Else 
		Set @sFiltro_BaseRemision = '	and EsRelacionFacturaPrevia = 1  ' + char(10) 


	If @Filtro_Folios = 1 
	Begin  
		If @Folio_Inicial <> ''  and @Folio_Final <> '' 
			Begin 
				Set @sFiltro_FoliosRemision = ' and FolioRemision between ' + char(39) + RIGHT('000000000000000' + @Folio_Inicial, 10) + char(39) + ' and ' + char(39) + RIGHT('000000000000000' + @Folio_Final, 10) + char(39) + char(10) 
			End 
		Else 
			Begin 
				If @Folio_Inicial <> '' 
					Begin 
						Set @sFiltro_FoliosRemision = ' and FolioRemision >= ' + char(39) + RIGHT('000000000000000' + @Folio_Inicial, 10) + char(39) 
					End 

				If @Folio_Final <> '' 
					Begin 
						Set @sFiltro_FoliosRemision = ' and FolioRemision <= ' + char(39) + RIGHT('000000000000000' + @Folio_Final, 10) + char(39)  
					End 
			End 
		Set @sFiltro_FoliosRemision = @sFiltro_FoliosRemision + char(10) 
	End 


	If @Filtro_FechaPeriodoRemisionado = 1 
	Begin  
		Set @sFiltro_Fecha_PeriodoRemisionado = ' and convert(varchar(10), FechaInicial, 120) >= ' + char(39) +  @FechaInicial_PeriodoRemisionado + char(39) + 
			' and FechaFinal <= ' + char(39) +  convert(varchar(10), @FechaFinal_PeriodoRemisionado, 120) + char(39) + char(10) 
	End 

	If @Filtro_FechaEmisionRemision = 1 
	Begin  
		Set @sFiltro_Fecha_RemisionGenerada = ' and convert(varchar(10), FechaRemision, 120) between ' + char(39) +  convert(varchar(10), @FechaInicial_EmisionRemision, 120) + char(39) + 
			' and ' + char(39) + convert(varchar(10), @FechaFinal_EmisionRemision, 120) + char(39) + char(10) 
	End 


	If @PartidaGeneral >= 0 
	Begin 
		Set @sFiltro_PartidaGeneral = '	and PartidaGeneral = ' + cast(@PartidaGeneral as varchar(20)) + char(10)  
	End 

	------------------------------ ARMAR FILTROS  
	------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 



---		select * from FACT_TiposDeRemisiones 

---		Exec spp_FACT_Remisiones_Concentrado_Facturacion 

	------------------------------ OBTENER INFORMACION    
	Set @sSql = 
		'Insert Into #tmp__ListaRemisiones ' + char(10) + 
		'( ' + char(10) + 
		'	Agregar_Al_Listado, IdEmpresa, IdEstado, IdFarmaciaGenera, FechaRemision, FechaValidacion, FolioRemision, Farmacia, Total, TipoDeRemision, TipoInsumo, OrigenInsumo, ' + char(10) + 
		'	EsFacturada, EsFacturable, EsExcedente, IdPersonalRemision, IdPersonalValida, IdFuenteFinanciamiento, IdFinanciamiento, ' + char(10) + 
		'	FechaInicial, FechaFinal, EsRelacionFacturaPrevia, Serie, Folio, EsRelacionMontos, PartidaGeneral, Referencia_01, Referencia_02, Referencia_03, EsReferencia_01, EsReferencia_02, EsReferencia_03   ' + char(10) + 
		') ' + char(10) + 
		'Select ' + char(10) +   --' + char(39) + '' + char(39) + '  
		'	1 as Agregar_Al_Listado, IdEmpresa, IdEstado, IdFarmaciaGenera, FechaRemision, FechaValidacion, FolioRemision, 0 as Farmacia, Total, TipoDeRemision, TipoInsumo, OrigenInsumo, ' + char(10) + 
		'	EsFacturada, EsFacturable, EsExcedente, IdPersonalRemision, IdPersonalValida, IdFuenteFinanciamiento, IdFinanciamiento, ' + char(10) + 
		'	FechaInicial, FechaFinal, EsRelacionFacturaPrevia, Serie, Folio, EsRelacionMontos, PartidaGeneral, ' + 
			char(39) + '' + char(39) + ' as Referencia_01, ' + char(39) + '' + char(39) + ' as Referencia_02, ' + char(39) + '' + char(39) + ' as Referencia_03, 0 as EsReferencia_01, 0 as EsReferencia_02, 0 as EsReferencia_03 ' + char(10) + 
		'From FACT_Remisiones R (NoLock) ' + char(10) +  
		'Where EsFacturable in ( 1, 1 ) and IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado  + char(39) + ' and IdFarmaciaGenera = ' + char(39) + @IdFarmaciaGenera + char(39) + ' ' + char(10) +  
		'	and IdFuenteFinanciamiento = ' + char(39) + @IdFuenteFinanciamiento + char(39) + ' and IdFinanciamiento = ' + char(39) + @IdFinanciamiento  + char(39) + '' + char(10) + 
		'	and Status = ' + char(39) + 'A' + char(39)  
		-- EsFacturada = 0 and  


	--------------- Obtener folios de remision 
	Set @sSql = @sSql + char(10) + 
		@sFiltro_PartidaGeneral + 
		@sFiltro_TipoDeRemision + 
		@sFiltro_TipoOrigenInsumos + 
		@sFiltro_TipoDeInsumos + 
		@sFiltro_TipoDispensacion + 
		@sFiltro_BaseRemision + 
		@sFiltro_FoliosRemision + 
		@sFiltro_Fecha_PeriodoRemisionado + 
		@sFiltro_Fecha_RemisionGenerada 
	Exec ( @sSql )	
	Print @sSql 



	If @Listado_de_Folios <> '' 
	Begin 
		Set @sSql = 'Update R Set Agregar_Al_Listado = 0 ' + char(10) + 
					'From #tmp__ListaRemisiones R (NoLock) ' + char(10) + 
					'Where FolioRemision not in ( ' + @Listado_de_Folios + ' ) ' 
		Exec ( @sSql )	
		Print @sSql 

		Delete #tmp__ListaRemisiones Where Agregar_Al_Listado = 0  
	End 


	-------------------------------- VALIDAR REMISIONES MARCADAS COMO FACTURADAS   
	Update R Set Agregar_Al_Listado = 0 
	From #tmp__ListaRemisiones R (NoLock) 
	Inner Join FACT_Facturas F (NoLock) On ( R.IdEmpresa = F.IdEmpresa and R.IdEstado = F.IdEstado and R.IdFarmaciaGenera = F.IdFarmacia and R.FolioRemision = F.FolioRemision and F.Status = 'A' ) 

	-------------------------------- VALIDAR REMISIONES MARCADAS COMO FACTURADAS   



	-- Select * from #tmp__ListaRemisiones 


	--------------------------------------------- Validar referencias 
	---------- PROYECTOS Ó H's ( Hidalgo 2N ) 
	--Set @Referencia_01 = ''  ---- Temporal solo para el 20190801 

	If  @Referencia_01 <> '' 
	Begin 

		Update L Set EsReferencia_01 = 
		IsNull
		(
			( 
				Select sum(1) From FACT_Remisiones_Detalles R (NoLock) 
				Where L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmaciaGenera and L.FolioRemision = R.FolioRemision 
				and R.Referencia_01 = @Referencia_01  
			), 0 
		) 
		From #tmp__ListaRemisiones L (NoLock) 

		Delete From #tmp__ListaRemisiones Where EsReferencia_01 = 0   

	End 

	---------- DOCUMENTOS ( Tamaulipas ) 
	If  @Referencia_02 <> '' 
	Begin 
		Set @Referencia_02 = @Referencia_02 
	End 


	--------------------------------------------- Validar referencias 



	--------------------------------------------- Informacio complementaria  
	Select R.IdEmpresa, R.IdEstado, R.IdFarmaciaGenera, R.IdFarmacia, R.FolioRemision, R.Referencia_01 
	Into #tmp__Detalles_Remision 
	From FACT_Remisiones_Detalles R (NoLock)
	Inner Join FACT_Remisiones L (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmaciaGenera and L.FolioRemision = R.FolioRemision ) 	
	Group by R.IdEmpresa, R.IdEstado, R.IdFarmaciaGenera, R.IdFarmacia, R.FolioRemision, R.Referencia_01 


	Update L Set Farmacia = F.NombreFarmacia, Referencia_01 = R.Referencia_01  
	From #tmp__ListaRemisiones L (NoLock) 
	Inner Join #tmp__Detalles_Remision R (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmaciaGenera and L.FolioRemision = R.FolioRemision ) 
	Inner Join CatFarmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia  ) 

	--Update L Set Referencia_01 = R.Referencia_01  
	--From #tmp__ListaRemisiones L (NoLock) 
	--Inner Join FACT_Remisiones_Detalles R (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmacia and L.FolioRemision = R.FolioRemision ) 
	--------------------------------------------- Informacio complementaria  



---		Exec spp_FACT_Remisiones_Concentrado_Facturacion   


--	@Referencia_01 varchar(100) = '', 
--	@Referencia_02 varchar(100) = '', 

		
	------------------------------ OBTENER INFORMACION    





----------------------------------------- SALIDA FINAL  
	Select 
		--* 
		L.FolioRemision, 
		convert(varchar(10), L.FechaRemision, 120) as FechaRemision, 
		dbo.fg_PRCS_Redondear(L.Total, 2, 0) as ImporteRemision, 
		convert(varchar(10), L.FechaInicial, 120) as FechaInicial, 
		convert(varchar(10), L.FechaFinal, 120) as FechaFinal, 
		L.Farmacia, 
		R.TipoDeRemisionDesc as TipoDeRemision, -- R.PartidaGeneral, 
		--'' as TipoDeRemision, 
		L.Referencia_01, L.Referencia_02, 
		0 as Procesar, 0 as Procesado
		
		, Agregar_Al_Listado  
	From #tmp__ListaRemisiones L (NoLock) 
	Inner Join vw_FACT_Remisiones R (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmacia and L.FolioRemision = R.FolioRemision ) 
	Where R.EsFacturable = 1 and 
		Agregar_Al_Listado = 1   
	Order By L.Farmacia, convert(varchar(10), L.FechaInicial, 120), convert(varchar(10), L.FechaFinal, 120), L.FolioRemision 



---		Exec spp_FACT_Remisiones_Concentrado_Facturacion   


End
Go--#SQL 





------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Concentrado__ReFacturacion' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_Concentrado__ReFacturacion
Go--#SQL  
  
Create Proc spp_FACT_Remisiones_Concentrado__ReFacturacion 
( 
	@IdEmpresa varchar(3) = '004', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '0001', 
	@Serie varchar(10) = 'PHJGTOA', @Folio varchar(20) = '313' 
)  
With Encryption 
As 
Begin  
Set NoCount On 
Set DateFormat YMD 



	------------------------------ TABLA BASE 
	Select 
		--top 0 
		1 as Agregar_Al_Listado, 
		R.IdEmpresa, R.IdEstado, R.IdFarmacia as IdFarmaciaGenera, R.FechaRemision, 
		--R.FechaValidacion, 
		R.FolioRemision, 
		cast('' as varchar(500)) as Farmacia, 
		R.Total, R.TipoDeRemision, R.TipoInsumo, R.OrigenInsumo, 
		R.EsFacturada, R.EsFacturable, 0 as EsExcedente, R.IdPersonalRemision, 
		--R.IdPersonalValida, R.IdFuenteFinanciamiento, R.IdFinanciamiento, 
		R.FechaInicial, R.FechaFinal, R.EsRelacionFacturaPrevia, R.Serie, R.Folio, R.EsRelacionMontos, R.PartidaGeneral, 
		cast('' as varchar(max)) as Referencia_01, 
		cast('' as varchar(max)) as Referencia_02, 
		cast('' as varchar(500)) as Referencia_03, 
		cast(0 as bit) as EsReferencia_01, cast(0 as bit) as EsReferencia_02, cast(0 as bit) as EsReferencia_03   
	Into #tmp__ListaRemisiones 
	From vw_FACT_Remisiones R (NoLock) 
	Inner Join FACT_Facturas F (NoLock) 
		On ( R.IdEmpresa = F.IdEmpresa and R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia and R.FolioRemision = F.FolioRemision )
	Where 
		--1 = 1 
		R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmaciaGenera 
		and R.Status = 'A' 
		and F.FolioFacturaElectronica = (@Serie + ' - ' + @Folio)  
		and F.Status = 'A' 		


	--------------------------------------------- Informacio complementaria  
	Select R.IdEmpresa, R.IdEstado, R.IdFarmaciaGenera, R.IdFarmacia, R.FolioRemision, R.Referencia_01 
	Into #tmp__Detalles_Remision 
	From FACT_Remisiones_Detalles R (NoLock)
	Inner Join #tmp__ListaRemisiones L (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmaciaGenera and L.FolioRemision = R.FolioRemision ) 	
	Group by R.IdEmpresa, R.IdEstado, R.IdFarmaciaGenera, R.IdFarmacia, R.FolioRemision, R.Referencia_01 


	Update L Set Farmacia = F.NombreFarmacia, Referencia_01 = R.Referencia_01  
	From #tmp__ListaRemisiones L (NoLock) 
	Inner Join #tmp__Detalles_Remision R (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmaciaGenera and L.FolioRemision = R.FolioRemision ) 
	Inner Join CatFarmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia  ) 

	--Update L Set Referencia_01 = R.Referencia_01  
	--From #tmp__ListaRemisiones L (NoLock) 
	--Inner Join FACT_Remisiones_Detalles R (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmacia and L.FolioRemision = R.FolioRemision ) 
	--------------------------------------------- Informacio complementaria  


	--select * from #tmp__ListaRemisiones 

----------------------------------------- SALIDA FINAL  
	Select 
		--* 
		L.FolioRemision, 
		convert(varchar(10), L.FechaRemision, 120) as FechaRemision, 
		dbo.fg_PRCS_Redondear(L.Total, 2, 0) as ImporteRemision, 
		convert(varchar(10), L.FechaInicial, 120) as FechaInicial, 
		convert(varchar(10), L.FechaFinal, 120) as FechaFinal, 
		L.Farmacia, 
		R.TipoDeRemision as TipoRemision, 
		R.TipoDeRemisionDesc as TipoDeRemision, -- R.PartidaGeneral, 
		--'' as TipoDeRemision, 
		--L.Referencia_01, L.Referencia_02, 
		1 as Procesar, 0 as Procesado
		
		, Agregar_Al_Listado 
	From #tmp__ListaRemisiones L (NoLock) 
	Inner Join vw_FACT_Remisiones R (NoLock) On ( L.IdEmpresa = R.IdEmpresa and L.IdEstado = R.IdEstado and L.IdFarmaciaGenera = R.IdFarmacia and L.FolioRemision = R.FolioRemision ) 
	Where R.EsFacturable in ( 0, 1 ) and 
		Agregar_Al_Listado = 1   
	Order By L.Farmacia, convert(varchar(10), L.FechaInicial, 120), convert(varchar(10), L.FechaFinal, 120), L.FolioRemision 



---		Exec spp_FACT_Remisiones_Concentrado__ReFacturacion   


End
Go--#SQL 




