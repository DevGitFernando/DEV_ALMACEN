
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_COM_OCEN_ObservacionSobrePrecioEnc' and xType = 'P' ) 
   Drop Proc spp_COM_OCEN_ObservacionSobrePrecioEnc
Go--#SQL   

Create Proc spp_COM_OCEN_ObservacionSobrePrecioEnc
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4) = '0001', @FolioOrden varchar(8) = '00000457',
	@IdProducto Varchar(8) = '00008883', @CodigoEAN Varchar(20)= '7501125102530',
	@PrecioCaja Numeric(14, 4) = 0.000, @PrecioReferencia Numeric(14, 4) = 0.0000, @Observaciones Varchar(100) = ''
)  
With Encryption 
As 
Begin 
Set NoCount On 	

	Declare @sStatus Varchar(1),
			@iActualizado int

	Set @sStatus = 'A'
	Set @iActualizado = 0

	If Not Exists (
					Select *
					From COM_OCEN_SobrePrecioEnc (NoLock)
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
						  FolioOrden = @FolioOrden And Idproducto = @IdProducto And CodigoEAN = @CodigoEAN
				   ) 
		Begin 
			Insert Into COM_OCEN_SobrePrecioEnc (
										IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProducto, CodigoEAN, PrecioCaja, PrecioReferencia, 
										ObServaciones, Status, Actualizado
										) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @IdProducto, @CodigoEAN,
				   @PrecioCaja, @PrecioReferencia, @ObServaciones, @sStatus, @iActualizado
		End 
	Else 
		Begin			
			Update COM_OCEN_SobrePrecioEnc
			Set PrecioCaja = @PrecioCaja, PrecioReferencia = @PrecioReferencia, ObServaciones = @ObServaciones, Status = @sStatus, Actualizado = @iActualizado
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
				  FolioOrden = @FolioOrden And Idproducto = @IdProducto And CodigoEAN = @CodigoEAN
		End
	End
	
Go--#SQL