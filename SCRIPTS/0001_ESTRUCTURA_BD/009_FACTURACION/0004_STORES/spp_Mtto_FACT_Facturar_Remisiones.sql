------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_Facturar_Remisiones'  and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_Facturar_Remisiones  
Go--#SQL 

Create Proc spp_Mtto_FACT_Facturar_Remisiones 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '0001', 
	@FolioFactura varchar(10) = '*', @TipoDeFactura smallint = 0, @FolioRemision varchar(max) = [ '0000004968', '0000004949', '0000004946' ],  
	@FolioFacturaElectronica varchar(100) = '', @IdPersonalFactura varchar(4) = '0001', 
		
	@SubTotalSinGrabar numeric(14,4) = 0, @SubTotalGrabado numeric(14,4) = 0, 
	@Iva numeric(14,4) = 0, @Total numeric(14,4) = 0, 		
	@Observaciones varchar(200) = '', @Status varchar(1) = 'A', @MostrarSalida int = 1, @EsConcentrado int = 0   	
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sMensaje varchar(500) 
	
	Set @sSql = '' 
	Set @sMensaje = '' 

	If @FolioFactura = '*' 
	Begin 
		Select @FolioFactura = cast(max(cast(FolioFactura as int) + 1) as varchar) 
		From FACT_Facturas (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		Set @FolioFactura = IsNull(@FolioFactura, '1') 
	End 
	Select @FolioFactura = dbo.fg_FormatearCadena(@FolioFactura, '0', 10) 
	
	If @EsConcentrado = 0 
		Begin 
			If Not Exists ( Select * 
							From FACT_Facturas (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioFactura = @FolioFactura ) 
				Begin 
					Insert Into FACT_Facturas 
						(	IdEmpresa, IdEstado, IdFarmacia, FolioFactura, TipoDeFactura, FolioRemision, 
							FechaRegistro, FolioFacturaElectronica, IdPersonalFactura, 
							SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
							Observaciones, Status, Actualizado ) 
					Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioFactura, @TipoDeFactura, @FolioRemision,  
							getdate() as FechaRegistro, @FolioFacturaElectronica, @IdPersonalFactura, 
							@SubTotalSinGrabar, @SubTotalGrabado, @Iva, @Total, @Observaciones, @Status, 0 as Actualizado  		 


					Update R Set EsFacturada = 1 
					From FACT_Remisiones R (NoLock)
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmacia and FolioRemision = @FolioRemision 

					Select @sMensaje = 'La información se ha guardado satisfactoriamente con el folio [' + @FolioFactura + 
							'] y el folio de factura electronica [' + @FolioFacturaElectronica + ']'  
				End 	
		End 	


	If @EsConcentrado = 1  
		Begin 
			--		spp_Mtto_FACT_Facturar_Remisiones 

			Select Top 0 IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, cast(@FolioFactura as int) as FolioFactura_Base, cast('' as varchar(10)) as FolioFactura, identity(int, 0, 1) as Orden 
			Into #tmp_ListaRemisiones 
			From FACT_Remisiones D (NoLock) 
			Where D.IdEstado = @IdEstado And D.IdFarmaciaGenera = @IdFarmacia And D.FolioRemision = @FolioRemision 

			Set @sSql = '' + 
			'Insert Into #tmp_ListaRemisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, FolioFactura_Base, FolioFactura  ) ' + char(10) + 
			'Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, ' + char(39) + cast(@FolioFactura as varchar(10)) + char(39) + ' as FolioFactura_Base, ' + char(39) + '' + char(39) + ' as FolioFactura '    + char(10) + 
			'From FACT_Remisiones D (NoLock) ' + char(10) + 
			'Where D.IdEstado = ' + char(39) + @IdEstado + char(39) + ' And D.IdFarmaciaGenera = ' + char(39) + @IdFarmacia + char(39) + ' And D.FolioRemision in ( ' + @FolioRemision + ' ) ' + char(10) + 
			'Order by FolioRemision '
			Exec( @sSql ) 
			Print @sSql 

			Update R Set FolioFactura = right('000000000000000' + cast(FolioFactura_Base + Orden as varchar(20)), 10) 
			From #tmp_ListaRemisiones R (NoLock) 

			Insert Into FACT_Facturas 
				(	IdEmpresa, IdEstado, IdFarmacia, FolioFactura, TipoDeFactura, FolioRemision, 
					FechaRegistro, FolioFacturaElectronica, IdPersonalFactura, 
					SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
					Observaciones, Status, Actualizado ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, R.FolioFactura, @TipoDeFactura, R.FolioRemision,  
					getdate() as FechaRegistro, @FolioFacturaElectronica, @IdPersonalFactura, 
					E.SubTotalSinGrabar, E.SubTotalGrabado, E.Iva, E.Total, 
					@Observaciones, @Status, 0 as Actualizado 
			From FACT_Remisiones E (NoLock) 
			Inner Join #tmp_ListaRemisiones R (NoLock) On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmaciaGenera = R.IdFarmaciaGenera and E.FolioRemision = R.FolioRemision  )  

 		 
			Update E Set EsFacturada = 1 
			From FACT_Remisiones E (NoLock) 
			Inner Join #tmp_ListaRemisiones R (NoLock) On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmaciaGenera = R.IdFarmaciaGenera and E.FolioRemision = R.FolioRemision  )  

			Select @sMensaje = 'La información se ha guardado satisfactoriamente.' 
		End 	


--- Salida Final 	
	If @MostrarSalida = 1 
	Begin 
		Select @FolioFactura as Folio, @sMensaje as Mensaje 
	End 


End 
Go--#SQL


