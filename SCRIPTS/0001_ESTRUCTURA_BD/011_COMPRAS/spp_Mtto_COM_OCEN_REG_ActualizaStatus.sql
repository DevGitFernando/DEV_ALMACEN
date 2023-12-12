

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_REG_ActualizaStatus' and xType = 'P')
    Drop Proc spp_Mtto_COM_OCEN_REG_ActualizaStatus
Go--#SQL

--		Exec spp_Mtto_COM_OCEN_REG_ActualizaStatus '001', '21', '000005', '04', '0015'
  
Create Proc spp_Mtto_COM_OCEN_REG_ActualizaStatus 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @FolioPedido varchar(6), @IdTipoPedido varchar(2), 
	@IdPersonal varchar(4), @iOpcion tinyint, @Unidad varchar(4)
)
With Encryption 
As
Begin
Set NoCount On

Declare @sStatusPedido varchar(2)
	
	Set @sStatusPedido = 'P'	
	
	If @iOpcion = 0
		Begin 
			If Not Exists ( Select * From COM_OCEN_REG_PedidosDet_Claves (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @Unidad And FolioPedido = @FolioPedido 
							And IdTipoPedido = @IdTipoPedido And CantidadASurtir > 0 ) 
			Begin 
				
				Update COM_OCEN_REG_Pedidos Set StatusPedido = @sStatusPedido
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @Unidad And FolioPedido = @FolioPedido 
				And IdTipoPedido = @IdTipoPedido And IdPersonal = @IdPersonal
			End
		End
	Else
		Begin
			Update COM_OCEN_REG_Pedidos Set StatusPedido = @sStatusPedido
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @Unidad And FolioPedido = @FolioPedido 
			And IdTipoPedido = @IdTipoPedido And IdPersonal = @IdPersonal
		End
	

End
Go--#SQL	
