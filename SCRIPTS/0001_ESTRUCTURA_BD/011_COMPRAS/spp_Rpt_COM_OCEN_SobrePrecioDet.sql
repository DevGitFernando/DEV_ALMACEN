If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_COM_OCEN_SobrePrecioDet' and xType = 'P' )
    Drop Proc spp_Rpt_COM_OCEN_SobrePrecioDet
Go--#SQL
  
Create Proc spp_Rpt_COM_OCEN_SobrePrecioDet
(
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4) = '0001', @FolioOrden Varchar(8) =  '00003779',
	@IdProducto Varchar(8) = '00008883', @CodigoEAN Varchar(20)= '7501125102530'
)
With Encryption 
As
Begin
Set NoCount On

	Select Folio As IdMotivoSobrePrecio, Descripcion, 'C' As Status
	Into #Motivos
	From CatMotivosSobrePrecio C Where Status = 'A'

	Update C
	Set C.Status = D.Status
	From #Motivos C (NoLock)
	Inner Join COM_OCEN_SobrePrecioDet D (NoLock) On (D.IdMotivoSobrePrecio = C.IdMotivoSobrePrecio)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden And
		Idproducto = @IdProducto And CodigoEAN = @CodigoEAN

	Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia, @FolioOrden As FolioOrden,
		   @IdProducto As IdProducto, @CodigoEAN As CodigoEAN, IdMotivoSobrePrecio, Descripcion, Status
	From #Motivos

End
Go--#SQL