------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Cancelar_Factura___HabilitarRemisiones'  and xType = 'P' ) 
   Drop Proc spp_FACT_Cancelar_Factura___HabilitarRemisiones  
Go--#SQL 

Create Proc spp_FACT_Cancelar_Factura___HabilitarRemisiones 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = 'HGOA', @FolioFacturaElectronica varchar(100) = '3' 
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
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and FolioFacturaElectronica = @Serie + ' - ' + @FolioFacturaElectronica --And Status = 'A'  
	) 
	Begin 
		
		------------------- LISTA DE FACTURA-REMISION RELACIONADAS 
		Select IdEmpresa, IdEstado, IdFarmacia, FolioFactura, FolioRemision 
		Into #tmp__Informacion 
		From FACT_Facturas (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and FolioFacturaElectronica = @Serie + ' - ' + @FolioFacturaElectronica and Status = 'A' 

		--Select @FolioFactura = FolioFactura, @FolioRemision = FolioRemision 
		--From FACT_Facturas (NoLock) 
		--Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		--	and FolioFacturaElectronica = @Serie + ' - ' + @FolioFacturaElectronica and Status = 'A' 
					

		Update F Set Status = 'C', SubTotalSinGrabar = 0, SubTotalGrabado = 0, Iva = 0, Total = 0  
		From FACT_Facturas F (NoLock) 
		Inner Join #tmp__Informacion L (NoLock) 
			On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.FolioFactura = L.FolioFactura ) 
		-- Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioFactura = @FolioFactura and Status = 'A' 
			

		Update F Set EsFacturada = 0, EsFacturable = 1  
		From FACT_Remisiones F (NoLock) 
		Inner Join #tmp__Informacion L (NoLock) 
			On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmaciaGenera = L.IdFarmacia and F.FolioRemision = L.FolioRemision ) 
		--Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmaciaGenera = @IdFarmacia and FolioRemision = @FolioRemision
		
		
		----select F.* 
		----From FACT_Remisiones F (NoLock) 
		----Inner Join #tmp__Informacion L (NoLock) 
		----	On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmaciaGenera = L.IdFarmacia and F.FolioRemision = L.FolioRemision ) 			

		--select * 
		--From FACT_Facturas F (NoLock) 
		--Inner Join #tmp__Informacion L (NoLock) 
		--	On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.FolioFactura = L.FolioFactura ) 

	End 
	

	-------------------------- Liberar Complementos de Pago 
	If Exists 
	( 
		Select * 
		From FACT_CFDI_ComplementoDePagos_DoctosRelacionados (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and Serie = @Serie and Folio = @FolioFacturaElectronica --And Status = 'A'  
	) 
	Begin 
		Update FR Set Status = 'C' 
		From FACT_CFDI_ComplementoDePagos_DoctosRelacionados FR (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and Serie = @Serie and Folio = @FolioFacturaElectronica --And Status = 'A'  		
	End 



	-------------------------- Liberar Notas de Credito 
	If Exists 
	( 
		Select * 
		From FACT_CFDI_NotasDeCredito_DoctosRelacionados (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and Serie = @Serie and Folio = @FolioFacturaElectronica --And Status = 'A'  
	) 
	Begin 
		Update FR Set Status = 'C' 
		From FACT_CFDI_NotasDeCredito_DoctosRelacionados FR (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and Serie = @Serie and Folio = @FolioFacturaElectronica --And Status = 'A'  		
	End 


--		select top 1 * from VentasDet_Lotes  
	
--- Salida Final 	
	Select @FolioFactura as FolioFactura, @FolioRemision as FolioRemision 
	
	

End 
Go--#SQL


