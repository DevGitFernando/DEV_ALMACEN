
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_COM_OCEN_ObservacionSobrePrecioDet' and xType = 'P' ) 
   Drop Proc spp_COM_OCEN_ObservacionSobrePrecioDet
Go--#SQL   

Create Proc spp_COM_OCEN_ObservacionSobrePrecioDet
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4) = '0001', @FolioOrden varchar(8) = '00000457',
	@IdProducto Varchar(8) = '00008883', @CodigoEAN Varchar(20)= '7501125102530',
	@IdMotivoSobrePrecio Varchar(4), @Status varchar(1)
)  
With Encryption 
As 
Begin 
Set NoCount On 	

	Declare @iActualizado int = 0

	If Not Exists (
					Select *
					From COM_OCEN_SobrePrecioDet (NoLock)
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
						  FolioOrden = @FolioOrden And Idproducto = @IdProducto And CodigoEAN = @CodigoEAN And IdMotivoSobrePrecio = @IdMotivoSobrePrecio
				   ) 
		Begin 
			Insert Into COM_OCEN_SobrePrecioDet (
										IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProducto, CodigoEAN, IdMotivoSobrePrecio, Status, Actualizado
										) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @IdProducto, @CodigoEAN, @IdMotivoSobrePrecio, @Status, @iActualizado
		End 
	Else 
		Begin			
			Update COM_OCEN_SobrePrecioDet
			Set Status = @Status, Actualizado = @iActualizado
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
				  FolioOrden = @FolioOrden And Idproducto = @IdProducto And CodigoEAN = @CodigoEAN And IdMotivoSobrePrecio = @IdMotivoSobrePrecio
		End
	End
	
Go--#SQL