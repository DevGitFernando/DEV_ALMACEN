-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det_Bloquear' and xType = 'P' ) 
    Drop Proc spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det_Bloquear 
Go--#SQL  
  
Create Proc spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det_Bloquear
( 
	@IdEmpresa Varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @FolioOrden varchar(8) = '',
	@CodigoEAN varchar(30) = '', @IdPersonal_Bloquea varchar(4) = ''
)
With Encryption 
As
Begin
Set NoCount On 

Declare @iActualizado smallint  

	Set @iActualizado = 0


	Update D Set CodigoEAN_bloqueado = 1, IdPersonal_Bloquea = @IdPersonal_Bloquea, Fecha_Bloqueado = GETDATE(), Actualizado = @iActualizado
	From COM_OCEN_OrdenesCompra_CodigosEAN_Det D
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden And CodigoEAN = @CodigoEAN
    
End 
Go--#SQL 

