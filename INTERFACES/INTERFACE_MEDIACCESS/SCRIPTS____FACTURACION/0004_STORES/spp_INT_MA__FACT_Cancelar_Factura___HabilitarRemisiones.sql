------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__FACT_Cancelar_Factura___HabilitarRemisiones'  and xType = 'P' ) 
   Drop Proc spp_INT_MA__FACT_Cancelar_Factura___HabilitarRemisiones  
Go--#SQL 

Create Proc spp_INT_MA__FACT_Cancelar_Factura___HabilitarRemisiones 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = 'MAT', @FolioFacturaElectronica varchar(100) = '1' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(500),  
	@FolioRemision varchar(10),  
	@FolioFactura varchar(10),
	@IdEstadoOrigen Varchar(2),
	@IdFarmaciaOrigen Varchar(4)  
	
	Set @FolioRemision = 'X' 
	Set @FolioFactura = 'X' 
	
	
	
	If Exists 
	( 
		Select * 
		From FACT_Facturas (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmacia 
			and Serie = @Serie and FolioFacturaElectronica = @FolioFacturaElectronica --And Status = 'A'  
	) 
	Begin 
		Select @FolioFactura = FolioFactura, @FolioRemision = FolioRemision 
		From FACT_Facturas (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmacia 
			and Serie = @Serie and FolioFacturaElectronica = @FolioFacturaElectronica 
					
		Update F Set Status = 'C' 
		From FACT_Facturas F (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmacia and FolioFactura = @FolioFactura
		
		Select @IdEstadoOrigen  = IdEstado, @IdFarmaciaOrigen = IdFarmacia
		From FACT_Remisiones R (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstadoGenera = @IdEstado and IdFarmaciaGenera = @IdFarmacia and FolioRemision = @FolioRemision
			
		Update R Set EsFacturada = 0, EsFacturable = 1  
		From FACT_Remisiones R (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstadoGenera = @IdEstado and IdFarmaciaGenera = @IdFarmacia and FolioRemision = @FolioRemision
		
		Update D Set Facturado = 0, Fact_Serie = '', Fact_Folio = ''
		From VentasDet D (NoLock)
		Where IdEstado = @IdEstadoOrigen And IdFarmacia = @IdFarmaciaOrigen And FACT_Serie = @Serie And FACT_Folio = @FolioFacturaElectronica
			
	End 
	
	
	
--- Salida Final 	
	Select @FolioFactura as FolioFactura, @FolioRemision as FolioRemision 
	
	

End 
Go--#SQL


