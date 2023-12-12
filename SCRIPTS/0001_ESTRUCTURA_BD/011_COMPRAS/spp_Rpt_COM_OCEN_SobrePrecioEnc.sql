If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_COM_OCEN_SobrePrecioEnc' and xType = 'P' )
    Drop Proc spp_Rpt_COM_OCEN_SobrePrecioEnc
Go--#SQL
  
Create Proc spp_Rpt_COM_OCEN_SobrePrecioEnc
(
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4) = '0001', @FolioOrden Varchar(8) =  '00003779',
	@IdProducto Varchar(8) = '00008883', @CodigoEAN Varchar(20)= '7501125102530'
)
With Encryption 
As
Begin
Set NoCount On

	Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia, @FolioOrden As FolioOrden,
		   @IdProducto As IdProducto, @CodigoEAN As CodigoEAN,
		   CAST(0.0000 As Numeric(14, 4)) As PrecioCaja, CAST(0.0000 As Numeric(14, 4)) As PrecioReferencia,
		   Cast('' As Varchar(100)) Observaciones
	Into #Motivos	
	
	Update C
	Set C.PrecioCaja = D.PrecioCaja, C.PrecioReferencia = D.PrecioReferencia, C.Observaciones = D.ObServaciones
	From #Motivos C (NoLock)
	Inner Join COM_OCEN_SobrePrecioEnc D (NoLock)
		On (D.IdEmpresa = C.IdEmpresa And D.IdEstado = C.IdEstado And D.IdFarmacia = C.IdFarmacia And D.FolioOrden = C.FolioOrden And
		    D.Idproducto = C.IdProducto And D.CodigoEAN = C.CodigoEAN)
		
	Select * From #Motivos

End
Go--#SQL