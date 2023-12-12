------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__FACT_Facturar_Venta'  and xType = 'P' ) 
   Drop Proc spp_INT_MA__FACT_Facturar_Venta  
Go--#SQL 

Create Proc spp_INT_MA__FACT_Facturar_Venta 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0013', 
	@FolioVenta varchar(8) = '9', @Serie varchar(10) = 'FA', @FolioFacturaElectronica varchar(100) = '4', 
	@TipoFacturacion int = 1   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(500) 
	
	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2)
	Set @IdFarmacia = right('0000000000' + @IdFarmacia, 4)
	Set @FolioVenta = right('0000000000' + @FolioVenta, 8)		
	
	
	Update R Set 
		TipoFacturacion = @TipoFacturacion, Facturado_Al_Beneficiario = (case when @TipoFacturacion <= 2 then 1 else 0 end), 
		FACT_Serie_Beneficiario = @Serie, FACT_Folio_Beneficiario = @FolioFacturaElectronica 
	From VentasDet R (NoLock) 
	Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia and R.FolioVenta = @FolioVenta 


End 
Go--#SQL


