------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__FACTURACION__GetRemision' and xType = 'P' ) 
   Drop Proc spp_INT_MA__FACTURACION__GetRemision
Go--#SQL 

/* 
Begin tran 

	Exec spp_INT_MA__FACTURACION__GetRemision @IdTipoInsumo = '01' 

rollback tran 

*/ 

Create Proc spp_INT_MA__FACTURACION__GetRemision 
(
	@IdEmpresa varchar(3) = '002', 
	@IdEstadoGenera varchar(2) = '09', @IdFarmaciaGenera Varchar(4) = '0001', @IdPersonal Varchar(4) = '0001', 
	@IdEstado varchar(2) = '19', @IdJurisdiccion varchar(3) = '001', @IdFarmacia varchar(4) = '0011', 
	@FechaInicial varchar(10) = '2015-04-01', @FechaFinal varchar(10) = '2017-04-10', 
    @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001',
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*', 
	@IdTipoInsumo Varchar(2) = '00', 
	@Remisionar bit = 0  
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On

Declare 
	@FolioRemision int,
	@SubTotalSinGrabar numeric(14,4),
	@Iva numeric(14,4),
	@SubTotalGrabado numeric(14,4),
	@sMensaje varchar(1000)

Declare 
	@sSql varchar(max), 
	@sFiltro varchar(200), 
	@sFiltro_Insumo_NoGravaIVA int,  
	@sFiltro_Insumo_GravaIVA int  	
	
	
	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @FolioRemision =  0 
	Set @sFiltro_Insumo_NoGravaIVA = 0 
	Set @sFiltro_Insumo_GravaIVA = 1  


	If @IdTipoInsumo <> '00' 
	Begin 
		If @IdTipoInsumo = '01' 
		Begin 
			Set @sFiltro_Insumo_NoGravaIVA = 1
			Set @sFiltro_Insumo_GravaIVA = 1 
		End 
		
		If @IdTipoInsumo = '02' 
		Begin 
			Set @sFiltro_Insumo_NoGravaIVA = 0 
			Set @sFiltro_Insumo_GravaIVA = 0 
		End 		
	End 	


----------------------- Obtener los Programas y SubProgramas a remisionar 	
	Select 
		IdPrograma, Programa, StatusPrograma, IdSubPrograma, SubPrograma, StatusSubPrograma, 
		------0 as FolioRemision_Base, 
		------cast('' as varchar(10)) as FolioRemision, 
		Identity(int, 1, 1) as Orden 		
	Into #tmp_Programas_SubProgramas 
	From vw_Programas_SubProgramas 
	Where 1 = 0 

	If @IdPrograma <> '' and @IdPrograma <> '*' 
	Begin 
		Set @sFiltro = 'Where IdPrograma = ' + char(39) + @IdPrograma + char(39) 
		
		If @IdSubPrograma <> '' and @IdSubPrograma <> '*' 
		Begin 
			Set @sFiltro = @sFiltro + ' and IdSubPrograma = ' + char(39) + @IdSubPrograma + char(39) 
		End 		
	End 

	Set @sSql = 'Insert Into #tmp_Programas_SubProgramas 
		( IdPrograma, Programa, StatusPrograma, IdSubPrograma, SubPrograma, StatusSubPrograma )  ' + char(13) + 
		'Select IdPrograma, Programa, StatusPrograma, IdSubPrograma, SubPrograma, StatusSubPrograma ' + char(13) + 
		'From vw_Programas_SubProgramas (NoLock)' + char(13) + @sFiltro + ' Order by IdPrograma, IdSubPrograma' 
	Exec ( @sSql ) 


--		spp_INT_MA__FACTURACION__GetRemision 

-------------- Asegurar que todas las ventas esten procesadas 
	Exec spp_INT_MA__PRCS__CalcularPorcentajes  


--	INT_MA_Ventas_Importes

----------------------------------------- Obtener la informacion a procesar 
	Select * 
	Into #tmp_Informacion 	
	From 
	( 
		Select
			E.IdEmpresa, E.IdEstado,
			Cast('' As Varchar(3)) As IdJurisdiccion, Cast('' As Varchar(300)) As Jurisdiccion,
			E.IdFarmacia, Cast('' As Varchar(300)) As Farmacia, E.FolioVenta, E.FechaRegistro, 
			I.IdPrograma, I.Programa, I.IdSubPrograma, I.SubPrograma,  
			D.IdProducto, D.CodigoEAN, D.TasaIva, (case when D.TasaIva = 0 Then 0 else 1 end) as GravaIVA, 
			cast('' as varchar(2)) as TipoDeClave, 
			cast(D.CantidadVendida as numeric(14, 0))  as Cantidad, 
			cast(D.PrecioUnitario as numeric(14,2)) as PrecioUnitario, 
			cast(IM.Porcentaje_01 as numeric(14,3)) as Porcentaje_01, 
			cast(IM.Porcentaje_02 as numeric(14,3)) as Porcentaje_02,				
			cast(0 as Numeric(14, 2)) as Precio_Beneficiario, 
			cast(0 as Numeric(14, 2)) as Precio_Aseguradora, 
			cast(0 as Numeric(14, 2)) as Importe_Beneficiario, 
			
			cast(0 as Numeric(14, 2)) as SubTotal_Gravado__Aseguradora, 		
			cast(0 as Numeric(14, 2)) as SubTotal_NoGravado__Aseguradora, 
			cast(0 as Numeric(14, 2)) as Iva__Aseguradora, 		
			cast(0 as Numeric(14, 2)) as Importe_Aseguradora, 
			cast('' as varchar(10)) as FolioRemision 
		-- Into #tmp_Informacion 	
		From VentasEnc E (NoLock) 
		Inner Join #tmp_Programas_SubProgramas I (NoLock) On ( E.IdPrograma = I.IdPrograma and E.IdSubPrograma = I.IdSubPrograma ) 
		Inner Join VentasDet D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
		Inner Join INT_MA_Ventas_Importes IM (NoLock) 
			On ( E.IdEmpresa = IM.IdEmpresa and E.IdEstado = IM.IdEstado and E.IdFarmacia = IM.IdFarmacia and E.FolioVenta = IM.FolioVenta ) 	
		Where 
			-- E.FolioCierre <> 0  And ----- Forzar los Cierres de Periodo 	
			E.TipoDeVenta = 2 and D.Facturado = 0 and D.EnRemision = 0  
			And E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia
			And E.IdCliente = @IdCliente And E.IdSubCliente = @IdSubCliente 
			-- And E.IdPrograma = @IdPrograma And E.IdSubPrograma = @IdSubPrograma 
			And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 	
			-- and E.IdEstado = 9 and E.IdFarmacia = 11 and e.FolioVenta = 332  
	) INF 
	Where GravaIVA in ( @sFiltro_Insumo_NoGravaIVA, @sFiltro_Insumo_GravaIVA )  
		
		
--		Exec spp_INT_MA__FACTURACION__GetRemision @IdTipoInsumo = '00' 		
		
--	select count(*)  from #tmp_Informacion 


	
----------------------------------- Programas a Remisionar  
	Select 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		0 as FolioRemision_Base, 
		cast('' as varchar(10)) as FolioRemision, 
		Identity(int, 0, 1) as Orden 			 
	Into #tmp__Remisiones 
	From #tmp_Informacion  
	Group by IdPrograma, Programa, IdSubPrograma, SubPrograma 
	Order by IdPrograma, IdSubPrograma  
	

	Update T Set 
		T.IdJurisdiccion = F.IdJurisdiccion, T.Jurisdiccion = F.Jurisdiccion,
		T.Farmacia = F.Farmacia 
	From #tmp_Informacion T
	Inner Join vw_Farmacias F (NoLock) On ( T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia ) 

	-------------------- Se excluye esta validacion 	
	----Delete T 
	----From #tmp_Informacion T
	----Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.CodigoEAN = P.CodigoEAN )
	----Where P.TipoDeClave <> @IdTipoInsumo 


----------------------------------- Programas a Remisionar  		
	
---	Select * from #tmp__Remisiones 
	
--		spp_INT_MA__FACTURACION__GetRemision 
	
		
----------------------------------- Calculos de Importes 		
	Update I Set 
		Precio_Beneficiario =  PrecioUnitario * (Porcentaje_01 / 100), 
		Precio_Aseguradora =  PrecioUnitario * (Porcentaje_02 / 100) 
	From #tmp_Informacion I 
	
	----Update I Set 
	----	Importe_Beneficiario = Precio_Beneficiario * Cantidad, 
	----	Importe_Aseguradora = Precio_Aseguradora * Cantidad 
	----From #tmp_Informacion I
		
	
	
	Update I Set 
		----SubTotal_Gravado__Aseguradora = (Cantidad * (PrecioUnitario * (Porcentaje_02 / 100))),  
		----Iva__Aseguradora =  (Cantidad * (PrecioUnitario * (Porcentaje_02 / 100))) * ( TasaIva / 100.0 ) 	
		SubTotal_Gravado__Aseguradora = (Cantidad * Precio_Aseguradora),  
		Iva__Aseguradora =  (Cantidad * (Precio_Aseguradora)) * ( TasaIva / 100.0 ) 
	From #tmp_Informacion I 
	Where TasaIva > 0  
		
		
	Update I Set 
		SubTotal_NoGravado__Aseguradora = (Cantidad * Precio_Aseguradora)   
	From #tmp_Informacion I 
	Where TasaIva = 0 
					
	Update I Set Importe_Aseguradora = ( SubTotal_Gravado__Aseguradora + Iva__Aseguradora ) + SubTotal_NoGravado__Aseguradora 
	From #tmp_Informacion I 

----------------------------------- Calculos de Importes 		


	

----------------------------------------- Obtener la informacion a procesar


----	spp_INT_MA__FACTURACION__GetRemision  

--	Select * from #tmp_Informacion 


------------------------------------------------ Salida final 
	If ( @Remisionar = 0 )
		Begin 
			Select
				IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
				Convert(Varchar(10), FechaRegistro, 120) As FechaRegistro, 				
				IdPrograma, Programa, IdSubPrograma, SubPrograma,  
				sum(SubTotal_NoGravado__Aseguradora) as SubTotal_NoGravado__Aseguradora, 
				sum(SubTotal_Gravado__Aseguradora) as SubTotal_Gravado__Aseguradora, 
				sum(Iva__Aseguradora) as Iva__Aseguradora, 
				SUM(Importe_Aseguradora) As ImporteTotal
				
				----SUM(Importe_Aseguradora) As Importe_Aseguradora, 
				----0 As Importe_Grabado, 0 As Iva, 
				----SUM(Importe_Aseguradora) As ImporteTotal
				
			From #tmp_Informacion 
			Group By 
				IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
				Convert(Varchar(10), FechaRegistro, 120), 				
				IdPrograma, Programa, IdSubPrograma, SubPrograma 
			Order By 1, 3, 5, 6, 7 
			
			
			----------------- Resumenes informativos 
			Select
				sum(SubTotal_NoGravado__Aseguradora) as SubTotal_NoGravado__Aseguradora, 
				sum(SubTotal_Gravado__Aseguradora) as SubTotal_Gravado__Aseguradora, 
				sum(Iva__Aseguradora) as Iva__Aseguradora, 
				SUM(Importe_Aseguradora) As ImporteTotal 				
			From #tmp_Informacion 			
			
			Select
				IdPrograma, Programa, IdSubPrograma, SubPrograma,  
				sum(SubTotal_NoGravado__Aseguradora) as SubTotal_NoGravado__Aseguradora, 
				sum(SubTotal_Gravado__Aseguradora) as SubTotal_Gravado__Aseguradora, 
				sum(Iva__Aseguradora) as Iva__Aseguradora, 
				SUM(Importe_Aseguradora) As ImporteTotal 				
			From #tmp_Informacion 
			Group By 
				IdPrograma, Programa, IdSubPrograma, SubPrograma 
			Order By ImporteTotal desc, IdPrograma, IdSubPrograma
			----------------- Resumenes informativos 
			
						
			
		End 
	Else
	Begin			
			Select 
				@SubTotalSinGrabar = SUM(SubTotal_NoGravado__Aseguradora),
				@SubTotalGrabado = sum(SubTotal_Gravado__Aseguradora), 
				@Iva = sum(Iva__Aseguradora) 
			From #tmp_Informacion 
			
			-- Set @SubTotalGrabado = IsNull(@SubTotalGrabado, 0.0000)			
			-- Set @Iva = IsNull(@Iva, 0.0000)
			
		    Select @FolioRemision = (max(FolioRemision) + 1) 
		    From FACT_Remisiones (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstadoGenera = @IdEstadoGenera and IdFarmaciaGenera = @IdFarmaciaGenera  
			Set @FolioRemision = IsNull(@FolioRemision, 1) 

			Update R Set --FolioRemision_Base = @FolioRemision, 
				FolioRemision = right('00000000000000000000' + cast((@FolioRemision + R.Orden) as varchar), 10)  
			From #tmp__Remisiones R (NoLock)  
			
			--- select * from #tmp__Remisiones 
				
			Update D Set FolioRemision = R.FolioRemision 	
			From #tmp__Remisiones R (NoLock)  
			Inner Join #tmp_Informacion D (NoLock) On ( R.IdPrograma = D.IdPrograma and R.IdSubPrograma = D.IdSubPrograma )  


			------ Asegurar que Folio sea valido y formatear la cadena 
			----Set @FolioRemision = IsNull(@FolioRemision, '1') 
			----Set @FolioRemision = right(replicate('0', 10) + @FolioRemision, 10 ) 
			
			Insert Into FACT_Remisiones
				(
				 IdEmpresa, IdEstadoGenera, IdFarmaciaGenera, IdEstado, IdFarmacia, FechaRemision, FolioRemision, TipoDeRemision, IdCliente, IdSubCliente,
				 IdPrograma, IdSubPrograma, IdPersonalRemision, IdPersonalValida, EsCancelada, IdPersonalCancela, TipoInsumo,
				 SubTotalSinGrabar, SubTotalGrabado, Iva, Total, FechaInicial, FechaFinal, Observaciones
				)
			Select 
				 @IdEmpresa, @IdEstadoGenera, @IdFarmaciaGenera, @IdEstado, @IdFarmacia, GETDATE(), FolioRemision, 1, @IdCliente, @IdSubCliente,
				 IdPrograma, IdSubPrograma, @IdPersonal, @IdPersonal, 0, @IdPersonal, @IdTipoInsumo,
				 SUM(SubTotal_NoGravado__Aseguradora) as SubTotalSinGrabar, 
				 sum(SubTotal_Gravado__Aseguradora)as SubTotalGrabado, 
				 sum(Iva__Aseguradora)as Iva, 
				 SUM(Importe_Aseguradora) as Importe, @FechaInicial, @FechaFinal, '' 
			From #tmp_Informacion 
			Group By FolioRemision, IdPrograma, IdSubPrograma 
				 
			
			--Insert Into  FACT_Remisiones_Detalles 
			--	( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFarmacia, SubTotalSinGrabar,
			--	SubTotalGrabado, Iva, Importe)
			--Select 
			--	IdEmpresa, IdEstadoGenera, @IdFarmaciaGenera, FolioRemision, IdFarmacia, 
			--	SUM(SubTotal_NoGravado__Aseguradora) as SubTotalSinGrabar, 
			--	sum(SubTotal_Gravado__Aseguradora)as SubTotalGrabado, 
			--	sum(Iva__Aseguradora) as Iva, 
			--	SUM(Importe_Aseguradora) as Importe 	
			--From #tmp_Informacion 
			--Group By IdEmpresa, IdEstado, IdFarmacia, FolioRemision 
			
			
			/* 
				Begin tran 
					Exec spp_INT_MA__FACTURACION__GetRemision  
				rollback tran 

			*/ 
			
			
			Update D 
				Set EnRemision = 1, FolioRemision = I.FolioRemision
			From VentasDet D 
			Inner Join #tmp_Informacion I (NoLock) 
				On ( D.IdEmpresa = I.IdEmpresa And D.IdEstado = I.IdEstado And D.IdFarmacia = I.IdFarmacia 
					 And D.FolioVenta = I.FolioVenta And D.IdProducto = I.IdProducto And D.CodigoEAN = I.CodigoEAN ) 
					
			
			Set @sMensaje = 'La información se guardo satisfactoriamente.'
					
			Select *, FolioRemision As Folio, @sMensaje As Mensaje
			From #tmp__Remisiones R (NoLock) 
	
	End
	
End 
Go--#SQL 
	