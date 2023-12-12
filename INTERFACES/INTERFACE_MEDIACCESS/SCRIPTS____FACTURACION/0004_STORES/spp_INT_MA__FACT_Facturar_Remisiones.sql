------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__FACT_Facturar_Remisiones'  and xType = 'P' ) 
   Drop Proc spp_INT_MA__FACT_Facturar_Remisiones  
Go--#SQL 

Create Proc spp_INT_MA__FACT_Facturar_Remisiones 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@FolioFactura varchar(10) = '*', @TipoDeFactura smallint = 0, @FolioRemision varchar(10) = '001',  
	@Serie varchar(10) = '', @FolioFacturaElectronica varchar(100) = '', 
	@IdPersonalFactura varchar(4) = '0001', 
		
	@SubTotalSinGrabar numeric(14,4) = 0, @SubTotalGrabado numeric(14,4) = 0, 
	@Iva numeric(14,4) = 0, @Total numeric(14,4) = 0, 		
	@Observaciones varchar(200) = '', @Status varchar(1) = 'A' 	
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(500) 
	
	If @FolioFactura = '*' 
	Begin 
		Select @FolioFactura = cast(max(cast(FolioFactura as int) + 1) as varchar) 
		From FACT_Facturas (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmacia 
		Set @FolioFactura = IsNull(@FolioFactura, '1') 
	End 
	Select @FolioFactura = dbo.fg_FormatearCadena(@FolioFactura, '0', 10) 
	
	
	If Not Exists ( Select * 
					From FACT_Facturas (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmacia and FolioFactura = @FolioFactura ) 
	Begin 
		Insert Into FACT_Facturas 
			(	IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura, TipoDeFactura, FolioRemision, 
				FechaRegistro, Serie, FolioFacturaElectronica, IdPersonalFactura, 
				SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
				Observaciones, Status, Actualizado ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioFactura, @TipoDeFactura, @FolioRemision,  
				getdate() as FechaRegistro, @Serie, @FolioFacturaElectronica, @IdPersonalFactura, 
				@SubTotalSinGrabar, @SubTotalGrabado, @Iva, @Total, @Observaciones, @Status, 0 as Actualizado  		 

		Update E Set Facturado = 1, FACT_Serie = @Serie, FACT_Folio = @FolioFacturaElectronica 
		From VentasDet E (NoLock) 
		Inner Join FACT_Remisiones R (NoLock) 
			On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmacia and E.FolioRemision = R.FolioRemision ) 
		Where R.IdEmpresa = @IdEmpresa and R.IdEstadoGenera = @IdEstado and R.IdFarmaciaGenera = @IdFarmacia and R.FolioRemision = @FolioRemision 


		Select @sMensaje = 'La información se ha guardado satisfactoriamente con el folio [' + @FolioFactura + 
				'] y el folio de factura electronica [' + @FolioFacturaElectronica + ']'  
	End 	
	
--- Salida Final 	
	Select @FolioFactura as Folio, @sMensaje as Mensaje 

End 
Go--#SQL


