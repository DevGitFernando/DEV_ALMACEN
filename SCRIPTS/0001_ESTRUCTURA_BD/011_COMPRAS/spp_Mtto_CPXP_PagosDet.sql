-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CPXP_PagosDet' and xType = 'P' ) 
    Drop Proc spp_Mtto_CPXP_PagosDet 
Go--#SQL
  
Create Proc spp_Mtto_CPXP_PagosDet
( 
	@IdEmpresa varchar(3) = '', @IdProveedor varchar(4), @Folio varchar(8), @IdEstado Varchar(2), @FolioOrdeneCompra Varchar(8),
	@TipoDeCompra Varchar(30), @Factura varchar(20), @Pago numeric(38,4), @iOpcion Int,
	@IdEstado_PersonalRegistra Varchar(2), @IdFarmacia_PersonalRegistra Varchar(4), @IdPersonal_PersonalRegistra  Varchar(4)
)
With Encryption 
As
Begin
Set NoCount On 

Declare @iActualizado smallint, @sStatus varchar(2)

	Set @iActualizado = 0
	Set @sStatus = 'A'

	If @iOpcion = 1 
		Begin 
			If Not Exists ( Select *
							From CPXP_PagosDet  (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEmpresa = @IdEmpresa And IdProveedor = @IdProveedor And Folio = @Folio And
								  FolioOrdeneCompra = @FolioOrdeneCompra And Factura = @Factura)
				Begin 
					Insert Into CPXP_PagosDet  
					   (IdEmpresa,  IdProveedor,  Folio,   IdEstado,  FolioOrdeneCompra,  TipoDeCompra,  Factura,  Pago, FechaRegistro,  Status, Actualizado ) 
					Select 
						@IdEmpresa, @IdProveedor, @Folio, @IdEstado, @FolioOrdeneCompra, @TipoDeCompra, @Factura, @Pago, GETDATE(),     @sStatus, @iActualizado
				End 
			Else 
				Begin 
					Update CPXP_PagosDet 
					Set Pago = @Pago, FechaRegistro = GETDATE() 
					Where IdEmpresa = @IdEmpresa And IdEmpresa = @IdEmpresa And IdProveedor = @IdProveedor And Folio = @Folio
						And FolioOrdeneCompra = @FolioOrdeneCompra And Factura = @Factura
				End		   
	   End 
	Else
		Begin 
			Set @sStatus = 'C' 
			Update CPXP_PagosDet  Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEmpresa = @IdEmpresa And IdProveedor = @IdProveedor And Folio = @Folio
			And FolioOrdeneCompra = @FolioOrdeneCompra And Factura = @Factura
		End
		
	Insert Into CPXP_PagosDet_Historico
	   (IdEmpresa,  IdProveedor,  Folio,   IdEstado,  FolioOrdeneCompra,  TipoDeCompra,  Factura,  Pago, FechaRegistro,  Status, Actualizado,
		 IdEstado_PersonalRegistra,  IdFarmacia_PersonalRegistra,  IdPersonal_PersonalRegistra
 ) 
	Select 
		@IdEmpresa, @IdProveedor, @Folio, @IdEstado, @FolioOrdeneCompra, @TipoDeCompra, @Factura, @Pago, GETDATE(),     @sStatus, @iActualizado,
		@IdEstado_PersonalRegistra, @IdFarmacia_PersonalRegistra, @IdPersonal_PersonalRegistra

    
End 
Go--#SQL 

