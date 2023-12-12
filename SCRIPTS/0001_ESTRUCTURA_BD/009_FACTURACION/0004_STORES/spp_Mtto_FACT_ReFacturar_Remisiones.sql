------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_ReFacturar_Remisiones'  and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_ReFacturar_Remisiones  
Go--#SQL 

Create Proc spp_Mtto_FACT_ReFacturar_Remisiones 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(100) = 'PHJGTOA', @Folio int = 314, 
	@Serie_Nueva varchar(100) = 'PHJGTOA', @Folio_Nuevo int = 3314  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sMensaje varchar(500),  
	@FolioFacturaElectronica varchar(100) = '',  	
	@FolioFacturaElectronica_Nueva varchar(100) = '' 	

	
	Set @sSql = '' 
	Set @sMensaje = '' 

	Set @FolioFacturaElectronica = @Serie + ' - ' + cast(@Folio as varchar(20))
	Set @FolioFacturaElectronica_Nueva = @Serie_Nueva + ' - ' + cast(@Folio_Nuevo as varchar(20))

	--select * 
	--From FACT_Facturas F (NoLock) 
	--where FolioFacturaElectronica = @FolioFacturaElectronica 


	Update F Set Status = 'C', SubTotalSinGrabar = 0,  SubTotalGrabado = 0, Iva =0, Total = 0, 
		Observaciones = Observaciones + ' | REFACTURACÓN : ' +  @FolioFacturaElectronica_Nueva 
	From FACT_Facturas F (NoLock) 
	where FolioFacturaElectronica = @FolioFacturaElectronica 

	--select * 
	--From FACT_Facturas F (NoLock) 
	--where FolioFacturaElectronica = @FolioFacturaElectronica 


End 
Go--#SQL



--Begin tran 

--	Exec spp_Mtto_FACT_ReFacturar_Remisiones 

--	rollback tran 


